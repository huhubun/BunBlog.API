using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bun.Blog.Web.Framework.Mvc.Controllers;
using Bun.Blog.Services.Categories;
using Bun.Blog.Web.Admin.Models.Categories;
using AutoMapper;
using Bun.Blog.Core.Domain.Categories;
using Bun.Blog.Web.Framework.Mvc.Json;

namespace Bun.Blog.Web.Admin.Controllers
{
    public class CategoryController : AdminBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult List()
        {
            var model = new CategoryListModel
            {
                CategoryList = Mapper.Map<List<Category>, List<CategoryModel>>(_categoryService.GetAll())
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var entity = _categoryService.GetById(id);

            if (entity != null)
            {
                return Json(Mapper.Map<Category, CategoryModel>(entity));
            }

            return Json(new JsonResponse(JsonResponseStatus.error, "IdNotExists"));
        }

        [HttpPost]
        public IActionResult Add(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var isCodeExists = _categoryService.CheckCodeExists(model.Code);

                if (!isCodeExists)
                {
                    var entity = Mapper.Map<CategoryModel, Category>(model);
                    _categoryService.Add(entity);

                    return Json(new JsonResponse());
                }
                else
                {
                    return Json(new JsonResponse(JsonResponseStatus.error, "CodeIsExists"));
                }
            }

            return Json(new JsonResponse(JsonResponseStatus.error, "ModelNotValid"));
        }

        [HttpPost]
        public IActionResult Edit(CategoryModel model)
        {
            return View();
        }


    }
}
