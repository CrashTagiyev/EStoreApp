using Domain.DTOs.CategoryDTOs.CategoryDTORequests;
using Domain.RepositoryAbstracts.Concretes;
using Infrastructure.Services.EntityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles ="SuperAdmin,Admin,Cashier")]
	public class CategoryController(CategoryServices categoryServices) : ControllerBase
	{
		private readonly CategoryServices _categoryServices = categoryServices;


		[HttpGet("[action]")]
		public async Task<IActionResult> GetCategoryById(int id)
		{
			var categoryDTO=await _categoryServices.GetCategoryByIdAsync(id);
			if(categoryDTO is null)
				return NotFound("Category is not found by this id");

			return Ok(categoryDTO);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetAllCategories(int id)
		{
			var vategryDTOs = await _categoryServices.GetAllCategoriesAsync();
			return Ok(vategryDTOs);
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO categoryCreateDTO)
		{
			if(!ModelState.IsValid)
				return ValidationProblem(ModelState);

			await _categoryServices.CreateCategoryAsync(categoryCreateDTO);
			return Ok();
		}
		[HttpPut("[action]")]
		public async Task<IActionResult> UpdateCategory([FromQuery] int id,[FromBody] CategoryUpdateDTO categoryCreateDTO)
		{
			var response= await _categoryServices.UpdateCategory(id, categoryCreateDTO);

			if (response.StatusCode == HttpStatusCode.OK)
				return Ok($"Status code:{response.StatusCode}({(int)response.StatusCode})\nMessage:{response.ResponseMessage}");

			return BadRequest($"Status code:{response.StatusCode}({(int)response.StatusCode})\nMessage:{response.ResponseMessage}");

		}

		[HttpDelete("[action]")]
		public async Task<IActionResult> DeleteCategory([FromQuery] int id)
		{
			var response = await _categoryServices.DeleteCategory(id);
			if (response.StatusCode == HttpStatusCode.OK)
				return Ok($"Status code:{response.StatusCode}({(int)response.StatusCode})\nMessage:{response.ResponseMessage}");

			return BadRequest($"Status code:{response.StatusCode}({(int)response.StatusCode})\nMessage:{response.ResponseMessage}");
		}
	}
}
