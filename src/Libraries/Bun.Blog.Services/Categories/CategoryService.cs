using System;
using System.Collections.Generic;
using System.Text;
using Bun.Blog.Core.Domain.Categories;
using Bun.Blog.Core.Data;
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
            return _repository.Table.ToList();
        }

        public bool CheckCodeExists(string code)
        {
            return _repository.Table.Any(c => c.Code.ToUpper() == code.ToUpper());
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
