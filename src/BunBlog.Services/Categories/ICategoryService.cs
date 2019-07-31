using BunBlog.Core.Domain.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BunBlog.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<Category>> GetListAsync();

        Task<Category> GetByLinkNameAsync(string linkName, bool noTracking = true);

        Task<Category> AddAsync(Category tag);

        Task<Category> EditAsync(Category tag);

        Task DeleteAsync(Category tag);
    }
}
