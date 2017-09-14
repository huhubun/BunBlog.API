using Bun.Blog.Core.Domain.Categories;
using System.Collections.Generic;

namespace Bun.Blog.Services.Categories
{
    public interface ICategoryService
    {
        Category GetById(int id);

        List<Category> GetAll();

        bool CheckCodeExists(string code, int? id = null);

        Category Add(Category category);

        Category Update(Category category);
    }
}
