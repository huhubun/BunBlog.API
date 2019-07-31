using BunBlog.Core.Domain.Categories;
using BunBlog.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly BunBlogContext _bunBlogContext;

        public CategoryService(BunBlogContext bunBlogContext)
        {
            _bunBlogContext = bunBlogContext;
        }

        public async Task<List<Category>> GetListAsync()
        {
            return await _bunBlogContext.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetByLinkNameAsync(string linkName, bool noTracking = true)
        {
            var category = _bunBlogContext.Categories.Where(t => t.LinkName == linkName);

            if (noTracking)
            {
                category = category.AsNoTracking();
            }

            return await category.SingleOrDefaultAsync();
        }

        public async Task<Category> AddAsync(Category category)
        {
            _bunBlogContext.Categories.Add(category);
            await _bunBlogContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category> EditAsync(Category category)
        {
            _bunBlogContext.Entry(category).State = EntityState.Modified;
            await _bunBlogContext.SaveChangesAsync();

            return category;
        }

        public async Task DeleteAsync(Category category)
        {
            _bunBlogContext.Categories.Remove(category);
            await _bunBlogContext.SaveChangesAsync();
        }
    }
}
