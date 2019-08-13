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

        public async Task<IsCategoryExists> IsExistsAsync(Category category)
        {
            if (await _bunBlogContext.Categories.AnyAsync(c => c.DisplayName == category.DisplayName))
            {
                return IsCategoryExists.DisplayName;
            }

            if (await _bunBlogContext.Categories.AnyAsync(c => c.LinkName == category.LinkName))
            {
                return IsCategoryExists.LinkName;
            }

            return IsCategoryExists.None;
        }

        public async Task<Category> GetByLinkNameAsync(string linkName, bool tracking = false)
        {
            var category = _bunBlogContext.Categories.Where(t => t.LinkName == linkName);

            if (!tracking)
            {
                category = category.AsNoTracking();
            }

            return await category.SingleOrDefaultAsync();
        }

        public async Task<bool> IsInUse(int id)
        {
            return await _bunBlogContext.Posts.AnyAsync(p => p.CategoryId == id);
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
