using Domain.DTOs.InvoiceDTOs.InvoiceRequests;
using Domain.DTOs.InvoiceItemsDTOs;
using Infrastructure.Services.EntityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InvoiceController : ControllerBase
	{
		private readonly InvoiceServices _invoiceServices;

		public InvoiceController(InvoiceServices invoiceServices)
		{
			_invoiceServices = invoiceServices;
		}

		[HttpPost("[Action]")]
		[Authorize(Roles = "SuperAdmin,Admin,Cashier")]
		public async Task<IActionResult> CreateInvoice(InvoiceCreateDTO invoiceCreateDTO)
		{
			var response = await _invoiceServices.CreateInvoice(invoiceCreateDTO);

			if (response.StatusCode == HttpStatusCode.OK)
				return Ok(response);

			return NotFound(response.ResponseMessage);
		}

		[HttpGet("[Action]")]
		[Authorize(Roles = "SuperAdmin,Admin,Cashier")]
		public async Task<IActionResult> GetInvoice(int invoiceId)
		{
			var invoice = await _invoiceServices.GetInvoiceById(invoiceId);
			if (invoice is not null)
				return Ok(invoice);

			return NotFound("Invoice did not found");
		}

		[HttpGet("[Action]")]
		[Authorize(Roles = "SuperAdmin,Admin,Cashier")]
		public async Task<IActionResult> GetAllInvoices()
		{
			var invoiceDTOs = await _invoiceServices.GetAllInvoices();
			return Ok(invoiceDTOs);
		}

		[HttpDelete("[Action]")]
		[Authorize(Roles = "SuperAdmin,Admin")]
		public async Task<IActionResult> DeleteInvoice(int invoiceId)
		{
			var response = await _invoiceServices.DeleteInvoice(invoiceId);
			if (response.StatusCode == HttpStatusCode.OK)
				return Ok(response);

			return NotFound(response);
		}

		[HttpPost("[Action]")]
		[Authorize(Roles = "SuperAdmin,Admin")]
		public async Task<IActionResult> AddInvoiceItemsToInvoice(int invoiceId, InvoiceItemsCreateDTO invoiceItemsCreateDTO)
		{
			var response = await _invoiceServices.AddInvoiceItmes(invoiceId, invoiceItemsCreateDTO);
			
			if (response.StatusCode == HttpStatusCode.OK)
				return Ok(response);

			return NotFound(response);
		}
	}
}
