using Domain.DTOs.ProductDTOs;
using Domain.DTOs.ProductDTOs.ProductRequestsDTOs;
using Domain.Entities.Concretes;
using Domain.RepositoryAbstracts.Concretes;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.EntityServices
{
	public class ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
	{
		private readonly IProductRepository _productRepository = productRepository;
		private readonly ICategoryRepository _categoryRepository = categoryRepository;



		public async Task<ProductResponse> CreateProductAsync(ProductCreateDTO productDTO)
		{


			var category = await _categoryRepository.GetByIdAsync(productDTO.CategoryId);

			if (category is not null)
			{

				var newProduct = new Product()
				{
					ProductName = productDTO.ProductName,
					Price = productDTO.Price,
					Category = category,
					CreatedTime = DateTime.UtcNow,
					LastUpdatedTime = DateTime.UtcNow,
				};

				await _productRepository.CreateAsync(newProduct);

				return new ProductResponse() { ResponseMessage = "SUCCESS:Product succeccfully created", StatusCode = HttpStatusCode.OK };
			}

			return new ProductResponse() { ResponseMessage = "FAIL:Category id is not found", StatusCode = HttpStatusCode.NotFound };

		}

		public async Task<ProductGetDTO> GetProductByIdAsync(int id)
		{
			var product = await _productRepository.GetByIdAsync(id);
			if (product is null)
				return null!;
			return new ProductGetDTO()
			{
				ProductName = product.ProductName,
				Price = product.Price,
				CategoryId = product.CategoryId,
				CategoryName = product.Category.CategoryName
			};
		}

		public async Task<List<ProductGetDTO>> GetByCategoryName(string categoryName)
		{
			var filteredProducts = await _productRepository.GetAllAsync();
			filteredProducts = filteredProducts.Where(p => p.Category.CategoryName.Contains(categoryName)).ToList();

			var productDTOs=filteredProducts.Select(p => new ProductGetDTO
			{
				Id = p.Id,
				ProductName = p.ProductName,
				Price = p.Price,
				CategoryId = p.CategoryId,
				CategoryName = p.Category.CategoryName
			});
			return productDTOs.ToList();
		}

		public async Task<List<ProductGetDTO>> GetAllProductsAsync()
		{
			var products = await _productRepository.GetAllProductsEagerLoadingAsync();



			var listOfProductDTO = products.Select(p =>
			{
				return new ProductGetDTO()
				{
					Id = p.Id,
					ProductName = p.ProductName,
					Price = p.Price,
					CategoryId = p.CategoryId,
					CategoryName = p.Category.CategoryName
				};
			}).ToList();

			return listOfProductDTO;

		}
		public async Task<ProductResponse> UpdateProductById(int ProductId, ProductUpdateDTO productUpdateDTO)
		{
			var product = await _productRepository.GetByIdAsync(ProductId);
			if (product is null)
				return new ProductResponse() { ResponseMessage = "FAIL:Product is not found", StatusCode = HttpStatusCode.NotFound };

			var category = await _categoryRepository.GetByIdAsync(productUpdateDTO.CategoryId);
			if (category is null)
				return new ProductResponse() { ResponseMessage = "FAIL:Category id is not found", StatusCode = HttpStatusCode.NotFound };


			product.ProductName = productUpdateDTO.ProductName;
			product.Price = productUpdateDTO.Price;
			product.CategoryId = productUpdateDTO.CategoryId;

			await _productRepository.UpdateAsync(product);

			return new ProductResponse() { ResponseMessage = "SUCCESS Product updated successfully", StatusCode = HttpStatusCode.OK };

		}
			
		public async Task<ProductResponse> DeleteProductAsync(int ProductId)
		{
			var product = await _productRepository.GetByIdAsync(ProductId);

			if (product is null)
				return new ProductResponse() { ResponseMessage = "FAIL:Product is not found", StatusCode = HttpStatusCode.NotFound };

			await _productRepository.DeleteAsync(ProductId);

			return new ProductResponse() { ResponseMessage = "SUCCESS Product deleted successfully", StatusCode = HttpStatusCode.OK };
		}



	}

}
