using Domain.DTOs.ProductDTOs.ProductRequestsDTOs;
using Domain.Entities.Concretes;
using Domain.RepositoryAbstracts.Concretes;
using Infrastructure.Services.EntityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController(ProductService productService) : ControllerBase
	{
		//Elgun and Emil tested pull request
        public int SomethingNew2 { get; set; }

        private readonly ProductService _productService = productService;

		// GET: api/<ProductController>
		[HttpGet("[Action]")]
		public async Task<IActionResult> GetAllProducts()
		{
			var products = await _productService.GetAllProductsAsync();
			return Ok(products);
		}

		// GET api/<ProductController>/5
		[Authorize(Roles = "SuperAdmin,Admin,Cashier")]
		[HttpGet("[Action]/{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);

			if (product is null)
				return BadRequest("Product is not found");

			return Ok(product);
		}

		// POST api/<ProductController>
		[HttpPost("[Action]")]
		[Authorize(Roles = "SuperAdmin,Admin,Cashier")]
		public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO productDTO)
		{
			var response = await _productService.CreateProductAsync(productDTO);

			if (response.StatusCode == HttpStatusCode.OK)
				return Ok(response.ResponseMessage);

			return BadRequest($"Status code:{response.StatusCode}({(int)response.StatusCode})\n{response.ResponseMessage}");
		}

		// PUT api/<ProductController>/5
		[HttpPut("[Action]/{id}")]
		[Authorize(Roles = "SuperAdmin,Admin,Cashier")]
		public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDTO productUpdateDTO)
		{
			var response = await _productService.UpdateProductById(id, productUpdateDTO);

			if (response.StatusCode == HttpStatusCode.OK)
				return Ok($"Status code:{response.StatusCode}({(int)response.StatusCode})\nMessage:{response.ResponseMessage}");

			return BadRequest($"Status code:{response.StatusCode}({(int)response.StatusCode})\nMessage:{response.ResponseMessage}");

		}

		// DELETE api/<ProductController>/5
		[Authorize(Roles = "SuperAdmin,Admin,Cashier")]
		[HttpDelete("[Action]/{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var response = await _productService.DeleteProductAsync(id);

			if (response.StatusCode == HttpStatusCode.OK)
				return Ok($"Status code:{response.StatusCode}({(int)response.StatusCode})\nMessage:{response.ResponseMessage}");

			return BadRequest($"Status code:{response.StatusCode}({(int)response.StatusCode})\nMessage:{response.ResponseMessage}");
		}

		[HttpGet("GetByCategoryName")]
		public async Task<IActionResult> GetByCategoryName(string categoryName)
		{
			var result =await _productService.GetByCategoryName(categoryName);
			return Ok(result);
		}
	}
}
