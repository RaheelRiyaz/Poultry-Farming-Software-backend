using KashmirPoultrySoftware.Application.Abstraction.IServices;
using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KashmirPoultrySoftware.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService saleService;
        private readonly IPdfService pdfService;

        public SalesController(ISaleService saleService, IPdfService pdfService)
        {
            this.saleService = saleService;
            this.pdfService = pdfService;
        }

        [HttpPost]
        public async Task<APIResponse<Sale>> AddSale(SaleRequest model)
        {
            return await saleService.AddSale(model);
        }


        [HttpGet("hatch/{hatchId:guid}")]
        public async Task<APIResponse<IEnumerable<SaleResponse>>> ViewSale(Guid hatchId)
        {
            return await saleService.ViewSale(hatchId);
        }


        [HttpGet("{id:guid}")]
        public async Task<APIResponse<SaleDetails>> Sale(Guid id)
        {
            return await saleService.Sale(id);
        }


        [HttpPut("change-payment-status")]
        public async Task<APIResponse<int>> ChangePaymentStatus(UpdateSalePaymentStatus model)
        {
            return await saleService.UpdateSalePaymentStatus(model);
        }


        [HttpGet("pending-payment-sales/{hatchId}")]
        public async Task<APIResponse<IEnumerable<SaleResponse>>> PendingPaymentSales(Guid hatchId)
        {
            return await saleService.PendingPaymentSales(hatchId);
        } 
        
        
        [HttpGet("newly-added/{hatchId}")]
        public async Task<APIResponse<IEnumerable<SaleResponse>>> NewlyAddedSales(Guid hatchId)
        {
            return await saleService.NewlyAddedSales(hatchId);
        }


        [HttpGet("filter-by-customer/{customerId}")]
        public async Task<APIResponse<IEnumerable<SaleResponse>>> FilterSalesByCustomer(Guid customerId)
        {
            return await saleService.FilterSaleByCustomer(customerId);
        }


        [HttpGet("generate-bill/{customerId:guid}/{hatchId:guid}")]
        public async Task<APIResponse<CustomerBill>> CustomerBill(Guid customerId, Guid hatchId)
        {
            return await saleService.CustomerBill(customerId, hatchId);
        }



        [AllowAnonymous]
        [HttpGet("generate-pdf")]
        public IActionResult GeneratePdf()
        {
            // Replace 'yourHtmlTableContent' with the HTML content of your table
            string yourHtmlTableContent = $@"<table
  style=""
    border: 1px solid black;
    border-collapse: collapse;
    font-size: 18px;
    width: 100%;
    box-shadow: 5px 2px 10px 0px gray;
  ""
>
  <tr style=""background-color: #f6f6f6"">
    <th
      style=""border: 1px solid #adadad; padding: 10px; text-align: start""
      colspan=""3""
    >
      Invoice #12345ABC
    </th>
    <th style=""border: 1px solid #adadad; padding: 10px; text-align: start"">
      7 Jan 2022
    </th>
  </tr>
  <tr>
    <td
      colspan=""2""
      style=""border: 1px solid #adadad; padding: 10px; text-align: start""
    >
      <h3>Pay To:</h3>
      <p>The Tech Park</p>
      <p>123 Willow street</p>
      <p>Boulevard, LA - 567892</p>
    </td>
        <td colspan=""2"" style=""border: 1px solid #adadad; padding: 10px; text-align: start"">
            <h3>Customer:</h3>
            <p>tariqkeena@yahoo.com</p>
            <p>987654377</p>
        </td>
  </tr>
  <tr style=""background-color: #f6f6f6;margin-right:5px"">
    <th>Qty.</th>
    <th>MRP</th>
    <th>Amount</th>
    <th>Sold on</th>
  </tr>
        <tr>
            <td style=""border: 1px solid #adadad; padding: 10px; text-align: start"">80 kgs</td>
            <td style=""border: 1px solid #adadad; padding: 10px; text-align: start"">&#x20B9; 106.00</td>
            <td style=""border: 1px solid #adadad; padding: 10px; text-align: start"">&#x20B9; 8,480.00</td>
            <td style=""border: 1px solid #adadad; padding: 10px; text-align: start"">2024-01-02</td>
        </tr>
    
    <tr>
        <th style=""background-color: #f6f6f6;margin-right:5px"" colspan=""3"">Subtotal:</th>
        <td>&#x20B9; 8,480.00</td>
    </tr>

</table>";

            // Generate PDF bytes
            byte[] pdfBytes = pdfService.GeneratePdf(yourHtmlTableContent);

            if (pdfBytes != null && pdfBytes.Length > 0)
            {
                // Return the PDF as a file
                return File(pdfBytes, "application/pdf", "output.pdf");
            }
            else
            {
                // Handle the case where PDF generation failed
                return BadRequest("PDF generation failed.");
            }
        }
    }
}
