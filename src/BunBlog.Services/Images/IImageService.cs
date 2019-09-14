using BunBlog.Core.Domain.Images;
using System.IO;
using System.Threading.Tasks;

namespace BunBlog.Services.Images
{
    public interface IImageService
    {
        Task<Image> Upload(string extension, Stream imageStream);
    }
}
