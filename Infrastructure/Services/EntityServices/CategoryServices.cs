using Domain.DTOs.CategoryDTOs;
using Domain.DTOs.CategoryDTOs.CategoryDTORequests;
using Domain.DTOs.ProductDTOs;
using Domain.Entities.Concretes;
using Domain.RepositoryAbstracts.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.EntityServices
{
    public class CategoryServices(ICategoryRepository categoryRepository)
	{
		private readonly ICategoryRepository _categoryRepository = categoryRepository;


		public async Task CreateCategoryAsync(CategoryCreateDTO categoryDTO)
		{
			var newCategory = new Category()
			{
				CategoryName = categoryDTO.CategoryName,
				CreatedTime = DateTime.UtcNow,
				LastUpdatedTime = DateTime.UtcNow,
			};

			await _categoryRepository.CreateAsync(newCategory);
		}

		public async Task<CategoryGetDTO> GetCategoryByIdAsync(int id)
		{
			var category = await _categoryRepository.GetByIdAsync(id);

			if (category is not null)
				return new CategoryGetDTO()
				{
					Id=category.Id,
					CategoryName = category.CategoryName,
				};

			return null!;
		}

		public async Task<List<CategoryGetDTO>> GetAllCategoriesAsync()
		{

			var categories =await _categoryRepository.GetAllAsync();
			var listOfCategoryGetDTOs = categories.Select(c => new CategoryGetDTO
			{
				Id=c.Id,
				CategoryName=c.CategoryName,
			});

			return  listOfCategoryGetDTOs.ToList();
		}
		public async Task<CategoryResponse> UpdateCategory(int id,CategoryUpdateDTO categoryUpdateDTO)
		{
			var category = await _categoryRepository.GetByIdAsync(id);
			
			if(category is null)
				return new CategoryResponse() { ResponseMessage = "FAIL: Category is not found", StatusCode = HttpStatusCode.NotFound };
			
			category.CategoryName= categoryUpdateDTO.CategoryName;

			await _categoryRepository.UpdateAsync(category);
			return new CategoryResponse() { ResponseMessage = "SUCCESS: Category is updated successfully", StatusCode = HttpStatusCode.OK };
		}

		public async Task<CategoryResponse> DeleteCategory(int id)
		{
			var category = await _categoryRepository.GetByIdAsync(id);
			if(category is null)
			return new CategoryResponse() { ResponseMessage = "FAIL: Category is not found", StatusCode = HttpStatusCode.NotFound };

			await _categoryRepository.DeleteAsync(id);
			return new CategoryResponse() { ResponseMessage = "SUCCESS: Category is deleted successfully", StatusCode = HttpStatusCode.OK };
		}

	}
}
