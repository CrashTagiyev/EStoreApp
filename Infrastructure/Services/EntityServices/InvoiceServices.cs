using Domain.DTOs.AppUserDTOs;
using Domain.DTOs.InvoiceDTOs;
using Domain.DTOs.InvoiceDTOs.InvoiceRequests;
using Domain.DTOs.InvoiceItemsDTOs;
using Domain.DTOs.ProductDTOs;
using Domain.DTOs.ProductDTOs.ProductRequestsDTOs;
using Domain.Entities.Concretes;
using Domain.RepositoryAbstracts.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.EntityServices
{
	public class InvoiceServices
	{
		private readonly IInvoiceRepository _invoiceRepository;
		private readonly IInvoiceItemRepository _invoiceItemsRepository;
		private readonly IAppUserRepository _appUserRepository;
		private readonly IAppUserRoleRepository _appUserRoleRepository;
		private readonly IProductRepository _productRepository;

		private readonly ProductService _productService;
		private readonly AppUserServices _appUserServices;
		public InvoiceServices(IInvoiceRepository invoiceRepository, IInvoiceItemRepository invoiceItemsRepository, IAppUserRepository appUserRepository, IAppUserRoleRepository appUserRoleRepository, AppUserServices appUserServices, ProductService productService, IProductRepository productRepository)
		{
			_invoiceRepository = invoiceRepository;
			_invoiceItemsRepository = invoiceItemsRepository;
			_appUserRepository = appUserRepository;
			_appUserRoleRepository = appUserRoleRepository;
			_appUserServices = appUserServices;
			_productService = productService;
			_productRepository = productRepository;
		}

		public async Task<InvoiceResponse> CreateInvoice(InvoiceCreateDTO invoiceCreateDTO)
		{
			var checkUsers = await CheckInvoiceUsers(invoiceCreateDTO);
			if (checkUsers.ResponseMessage.StartsWith("SUCCESS"))
			{
				var cashier = await _appUserRepository.GetByIdAsync(invoiceCreateDTO.CashierId);
				var customer = await _appUserRepository.GetByIdAsync(invoiceCreateDTO.CustomerId);
				var newInvoice = new Invoice()
				{
					InvoiceBarCode = RandomNumberGenerator.GetInt32(9999999, 99999999).ToString(),
					BarCode = RandomNumberGenerator.GetInt32(10000, 99999).ToString(),
					Cashier = cashier,
					Customer = customer,
				};

				await _invoiceRepository.CreateAsync(newInvoice);
			}

			return checkUsers;


		}
		public async Task<ICollection<InvoiceGetDTO>> GetAllInvoices()
		{
			var invoices = await _invoiceRepository.GetAllAsync();

			var invoiceDTOs = invoices.Select(i =>
			{
				var invoiceItemsDTOs = i.InvoiceItems.Select(ii =>
				new InvoiceItemsGetDTO
				{
					Product = new ProductGetDTO
					{
						Id = ii.Product.Id,
						CategoryId = ii.Product.CategoryId,
						ProductName = ii.Product.ProductName,
						CategoryName = ii.Product.Category.CategoryName,
						Price = ii.Product.Price,
					},
					Quantity = ii.Quantity
				}).ToList();

				var cashier = (AppUserGetDTO)i.Cashier;
				var customer = (AppUserGetDTO)i.Customer;

				var result = new InvoiceGetDTO()
				{
					InvoiceBarCode = i.BarCode,
					BarCode = i.BarCode,
					InvoiceItems = invoiceItemsDTOs,
					Cashier = cashier,
					Customer = customer,

				};
				return result;
			}).ToList();
			return invoiceDTOs;
		}

		public async Task<InvoiceGetDTO> GetInvoiceById(int InvoiceId)
		{
			var invoice = await _invoiceRepository.GetByIdAsync(InvoiceId);
			if (invoice is not null)
			{
				var invoiceItemsDTOs = invoice.InvoiceItems.Select(ii =>
				new InvoiceItemsGetDTO
				{
					Product = new ProductGetDTO
					{
						Id = ii.Product.Id,
						CategoryId = ii.Product.CategoryId,
						ProductName = ii.Product.ProductName,
						CategoryName = ii.Product.Category.CategoryName,
						Price = ii.Product.Price,
					},
					Quantity = ii.Quantity
				}).ToList();

				var cashier = await _appUserServices.GetAppUser(invoice.CashierId);
				var customer = await _appUserServices.GetAppUser(invoice.CustomerId);

				var result = new InvoiceGetDTO()
				{
					InvoiceBarCode = invoice.BarCode,
					BarCode = invoice.BarCode,
					InvoiceItems = invoiceItemsDTOs,
					Cashier = cashier,
					Customer = customer,

				};

				return result;
			}
			return null!;
		}


		public async Task<InvoiceResponse> AddInvoiceItmes(int invoiceId, InvoiceItemsCreateDTO InvoiceItemDTO)
		{
			var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
			if (invoice is null)
				return new InvoiceResponse() { ResponseMessage = "FAIL:Invoice by this id  did not found", StatusCode = HttpStatusCode.NotFound };

			var product = await _productRepository.GetByIdAsync(InvoiceItemDTO.ProductId);

			if (product is null)
				return new InvoiceResponse() { ResponseMessage = "FAIL:Product by this id  did not found", StatusCode = HttpStatusCode.NotFound };


			var invoiceItem = new InvoiceItems()
			{
				Product = product,
				Quantity = InvoiceItemDTO.Quantity,
				CreatedTime = DateTime.UtcNow,
				LastUpdatedTime = DateTime.UtcNow,
				Invoice = invoice
			};

			await _invoiceItemsRepository.CreateAsync(invoiceItem);


			return new InvoiceResponse() { ResponseMessage = "SUCCESS:Invoice item successfully added to invoice ", StatusCode = HttpStatusCode.OK };

		}

		public async Task<InvoiceResponse> DeleteInvoice(int invoiceId)
		{
			var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
			if (invoice is null)
				return new InvoiceResponse() { ResponseMessage = "FAIL:Invoice by this id  did not found", StatusCode = HttpStatusCode.NotFound };

			await _invoiceItemsRepository.DeleteInRange(invoice.InvoiceItems);
			await _invoiceRepository.DeleteAsync(invoiceId);
			return new InvoiceResponse() { ResponseMessage = "SUCCESS:Invoice  successfully deleted from the database ", StatusCode = HttpStatusCode.OK };
		}

		private async Task<InvoiceResponse> CheckInvoiceUsers(InvoiceCreateDTO invoiceCreateDTO)
		{

			var cashier = await _appUserRepository.GetByIdAsync(invoiceCreateDTO.CashierId);
			if (cashier is null)
				return new InvoiceResponse() { ResponseMessage = "FAIL:Cashier by this id is not found", StatusCode = HttpStatusCode.NotFound };

			var cashierRoles = await _appUserRoleRepository.GetUserRoles(cashier);
			bool isCashier = cashierRoles.Any(roles => roles == "Cashier");

			if (!isCashier)
				return new InvoiceResponse() { ResponseMessage = "FAIL:Cashier by this id is not found", StatusCode = HttpStatusCode.NotFound };

			var Customer = await _appUserRepository.GetByIdAsync(invoiceCreateDTO.CustomerId);
			if (Customer is null)
				return new InvoiceResponse() { ResponseMessage = "FAIL:Customer by this id is not found", StatusCode = HttpStatusCode.NotFound };

			var customerRoles = await _appUserRoleRepository.GetUserRoles(Customer);
			bool isCustomer = customerRoles.Any(roles => roles == "Customer");

			if (!isCustomer)
				return new InvoiceResponse() { ResponseMessage = "FAIL:Customer by this id is not found", StatusCode = HttpStatusCode.NotFound };

			return new InvoiceResponse() { ResponseMessage = "SUCCESS:Everything is okay!", StatusCode = HttpStatusCode.OK };
		}
	}
}
