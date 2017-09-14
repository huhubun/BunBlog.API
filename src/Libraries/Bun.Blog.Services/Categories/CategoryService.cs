using Bun.Blog.Core.Data;
using Bun.Blog.Core.Domain.Categories;
using System.Collections.Generic;
using System.Linq;

namespace Bun.Blog.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public Category GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<Category> GetAll()
        {
            return _repository.Table.OrderBy(c => c.Id).ToList();
        }

        public bool CheckCodeExists(string code, int? id = null)
        {
            var categories = _repository.Table;

            if (id.HasValue)
            {
                categories = categories.Where(c => c.Id != id);
            }

            return categories.Any(c => c.Code.ToUpper() == code.ToUpper());
        }

        public Category Add(Category category)
        {
            return _repository.Add(category);
        }

        public Category Update(Category category)
        {
            return _repository.Update(category);
        }
    }
}
