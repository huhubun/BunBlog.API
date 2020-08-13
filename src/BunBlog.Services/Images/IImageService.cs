using System.IO;
using System.Threading.Tasks;

namespace BunBlog.Services.Images
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadAsync(string fileName, Stream imageStream);
    }
}
