using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BunBlog.Services.Images
{
    public interface IImageService
    {
        Task<string> Upload(string extension, string description, Stream imageStream);
    }
}
