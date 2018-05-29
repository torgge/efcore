using System.Collections.Generic;
using EFCore.Models;
using EFCore.Repositories;
using EFCore.ViewModels;
using EFCore.ViewModels.CategoryViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repository;
        
        public CategoryController(CategoryRepository repository)
        {
            _repository = repository;
        }
        
        [Route("v1/categories")]
        [HttpGet]
        [ResponseCache(Duration = 3600)]
        public IEnumerable<Category> Get()
        {
            return _repository.Get();
        }

        [Route("v1/categories/{id}")]
        [HttpGet]
        public Category Get(int id)
        {
            // Find() ainda não suporta AsNoTracking
            var category = _repository.Get(id);
            category.Products = _repository.GetProductsFromCategory(id);
            return category;
        }

        [Route("v1/categories/{id}/products")]
        [HttpGet]
        [ResponseCache(Duration = 30)]
        public IEnumerable<Product> GetProducts(int id)
        {
            return _repository.GetProductsFromCategory(id);
        }

        [Route("v1/categories")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorCategoryViewModel model)
        {
            model.Validate();
            if (model.Invalid)
                return new ResultViewModel
                {
                    Success = model.Valid,
                    Message = "Não foi possível adicionar nova categoria",
                    Data = model.Notifications

                };
            
            Category category = new Category();
            category.Id = model.Id;
            category.Title = model.Title;
            
            _repository.Save(category);
            return new ResultViewModel
            {
                Success = model.Valid,
                Message = "Produto criado com sucesso!",
                Data = category
            };
        }

        [Route("v1/categories")]
        [HttpPut]
        public Category Put([FromBody]Category category)
        {
            _repository.Update(category);
            return category;
        }

        [Route("v1/categories")]
        [HttpDelete]
        public Category Delete([FromBody]Category category)
        {
            _repository.Delete(category);
            return category;
        }
    }
}