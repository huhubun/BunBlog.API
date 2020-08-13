using System;

namespace BunBlog.Core.Configuration
{
    public class UploadImageConfig
    {
        public static string ConfigSectionName => "UploadImage";

        /// <summary>
        /// 访问图片使用的域名
        /// </summary>
        public Uri Domain { get; set; }

        /// <summary>
        /// 图片上传方式
        /// </summary>
        public UploadImageProvider Provider { get; set; }

        /// <summary>
        /// 图片储存路径，Provider 为 StaticFile 时使用
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// 腾讯云 COS 对象存储配置，Provider 为 Tencent 时使用
        /// </summary>
        public UploadImageProviderTencentConfig Tencent { get; set; }
    }

    /// <summary>
    /// 图片上传方式（提供器）枚举
    /// </summary>
    public enum UploadImageProvider
    {
        /// <summary>
        /// 静态文件
        /// </summary>
        StaticFile = 1,

        /// <summary>
        /// 腾讯云 COS 对象存储
        /// </summary>
        Tencent
    }

    public class UploadImageProviderTencentConfig
    {
        /// <summary>
        /// 腾讯云账户的账户标识 APPID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 设置一个默认的存储桶地域
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 云 API 密钥 SecretId，用于使用“永久密钥”方式访问腾讯云 API
        /// </summary>
        public string SecretId { get; set; }

        /// <summary>
        /// 云 API 密钥 SecretKey，用于使用“永久密钥”方式访问腾讯云 API
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 存储桶，格式：BucketName-APPID
        /// </summary>
        public string Bucket { get; set; }
    }
}
