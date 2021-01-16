using BunBlog.Core.Configuration;
using BunBlog.Core.Domain.Images;
using BunBlog.Core.Exceptions;
using BunBlog.Data;
using COSXML;
using COSXML.Auth;
using COSXML.Model.Object;
using COSXML.Utils;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BunBlog.Services.Images
{
    public class ImageService : IImageService
    {
        private readonly UploadImageConfig _uploadImageConfig;
        private readonly BunBlogContext _bunBlogContext;

        public ImageService(
            BunBlogContext bunBlogContext,
            UploadImageConfig uploadImageConfig
            )
        {
            _bunBlogContext = bunBlogContext;
            _uploadImageConfig = uploadImageConfig;
        }

        public async Task<ImageUploadResult> UploadAsync(string fileName, Stream imageStream)
        {
            if (_uploadImageConfig == null)
            {
                throw new BunBlogConfigException(UploadImageConfig.ConfigSectionName, "未配置");
            }

            switch (_uploadImageConfig.Provider)
            {
                case UploadImageProvider.StaticFile:
                    return await UploadStaticFile(fileName, imageStream);

                case UploadImageProvider.Tencent:
                    return await UploadToTencentCos(fileName, imageStream);

                default:
                    throw new BunBlogConfigException(UploadImageConfig.ConfigSectionName, $"不支持的 Provider \"{_uploadImageConfig.Provider}\"");
            }
        }

        private async Task<ImageUploadResult> UploadStaticFile(string fileName, Stream imageStream)
        {
            // 图片保存路径，格式为 2020/11/22/abc.jpg，不含盘符
            var saveImageFilePath = GenerateImageUrlPath(fileName, includeFileName: false);

            // 完整路径，包含盘符
            var fullPath = Path.Combine(_uploadImageConfig.SavePath, saveImageFilePath);

            // 保存到指定路径
            await SaveImageFileAsync(fullPath, fileName, imageStream);

            // 将路径储存到数据库中
            var imageEntity = await InsertImageEntityAsync(saveImageFilePath, fileName);

            return GenerateUploadResult(imageEntity, $"{saveImageFilePath}/{fileName}");
        }

        /// <summary>
        /// 上传至腾讯云对象存储 COS
        /// </summary>
        public async Task<ImageUploadResult> UploadToTencentCos(string fileName, Stream imageStream)
        {
            var uploadImageConfig = _uploadImageConfig.Tencent;

            var extension = Path.GetExtension(fileName);
            var mimeType = GetMIMEType(extension);

            CosXmlConfig conConfig = new CosXmlConfig.Builder()
              .IsHttps(true)
              .SetAppid(uploadImageConfig.AppId)
              .SetRegion(uploadImageConfig.Region)
#if DEBUG
              .SetDebugLog(true)  //显示日志
#endif
              .Build();

            // 使用“永久密钥”方式访问腾讯云 API
            var cosCredentialProvider = new DefaultQCloudCredentialProvider(uploadImageConfig.SecretId, uploadImageConfig.SecretKey, 60 /* 请求签名有效时长，单位秒 */);
            var cosXml = new CosXmlServer(conConfig, cosCredentialProvider);

            // 对象在存储桶中的位置，即称对象键。这里生成以日期为依据生成储存图片的路径
            // COS 的路径为固定格式，blog/2020/11/22/abc.jpg
            string key = $"blog/{GenerateImageUrlPath(fileName)}";

            byte[] bytes = new byte[imageStream.Length];
            imageStream.Read(bytes, 0, bytes.Length);
            imageStream.Seek(0, SeekOrigin.Begin);

            PutObjectRequest request = new PutObjectRequest(uploadImageConfig.Bucket, key, bytes);
            request.SetRequestHeader("Content-Type", mimeType);

            // 设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.Seconds), 60);

            // 执行请求
            PutObjectResult result = cosXml.PutObject(request);

            // 将路径储存到数据库中
            var imageEntity = await InsertImageEntityAsync(key, fileName);

            return GenerateUploadResult(imageEntity, key);
        }

        private static string GetMIMEType(string extension)
        {
            if (String.IsNullOrEmpty(extension))
            {
                throw new ArgumentException($"文件扩展类型不能为 NULL 或空白字符串");
            }

            return extension switch
            {
                ".jpg" => "image/jpeg",
                ".png" => "image/png",
                _ => throw new ArgumentException($"不支持的文件扩展类型 {extension}", nameof(extension))
            };
        }

        private ImageUploadResult GenerateUploadResult(Image imageEntity, string path)
        {
            return new ImageUploadResult
            {
                Id = imageEntity.Id,
                FileName = imageEntity.FileName,
                Url = new Uri(_uploadImageConfig.Domain, path).ToString()
            };
        }

        private async Task SaveImageFileAsync(string path, string fileName, Stream imageStream)
        {
            Directory.CreateDirectory(path);

            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await imageStream.CopyToAsync(fileStream);
            }
        }

        private async Task<Image> InsertImageEntityAsync(string path, string fileName)
        {
            var image = new Image
            {
                Path = path,
                FileName = fileName,
                UploadTime = DateTime.Now
            };

            await _bunBlogContext.Images.AddAsync(image);
            await _bunBlogContext.SaveChangesAsync();

            return image;
        }

        /// <summary>
        /// 以日期为依据，生成图片存储路径。结果为 2020/11/22/abc.jpg 的格式
        /// </summary>
        /// <param name="fileName">图片文件名</param>
        /// <param name="includeFileName">结果中是否需要包含文件名</param>
        /// <param name="date">日期，生成路径的依据，不填表示使用当前时间</param>
        /// <returns></returns>
        private string GenerateImageUrlPath(string fileName, bool includeFileName = true, DateTime? date = null)
        {
            if (String.IsNullOrEmpty(fileName) || String.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("文件名不能为 null、Empty 和纯空格", nameof(fileName));
            }

            var trueDate = date ?? DateTime.Now;

            // 为同时兼容 Windows、Linux 和腾讯云对象存储的风格，统一使用 / 来表示文件夹层级关系
            var path = $"{trueDate.Year}/{trueDate.Month}/{trueDate.Day}";
            return includeFileName ? $"{path}/{fileName}" : path;
        }
    }
}
