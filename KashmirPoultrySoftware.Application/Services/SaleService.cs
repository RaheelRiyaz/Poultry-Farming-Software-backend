using AutoMapper;
using KashmirPoultrySoftware.Application.Abstraction.IEmail;
using KashmirPoultrySoftware.Application.Abstraction.IRepository;
using KashmirPoultrySoftware.Application.Abstraction.IServices;
using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Application.Utilis;
using KashmirPoultrySoftware.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository saleRepository;
        private readonly IMapper mapper;
        private readonly HatchValidator hatchValidator;
        private readonly IEmailTemplateRenderer emailTemplateRenderer;

        public SaleService(ISaleRepository saleRepository, IMapper mapper,HatchValidator hatchValidator, IEmailTemplateRenderer emailTemplateRenderer)
        {
            this.saleRepository = saleRepository;
            this.mapper = mapper;
            this.hatchValidator = hatchValidator;
            this.emailTemplateRenderer = emailTemplateRenderer;
        }




        public async Task<APIResponse<Sale>> AddSale(SaleRequest model)
        {
            var isActive = await hatchValidator.CheckHatchStatus(model.HatchId);
            if (isActive) return APIResponse<Sale>.ErrorResponse("Hatch is completed cannot add sale now");

            Sale sale = mapper.Map<Sale>(model);
            var res = await saleRepository.AddAsync(sale);

            if (res > 0) return APIResponse<Sale>.SuccessResponse(result:sale);

            return APIResponse<Sale>.ErrorResponse();
        }




        public async Task<APIResponse<CustomerBill>> CustomerBill(Guid customerId, Guid hatchId)
        {
            var sales = await saleRepository.GenerateCustomerBill(customerId, hatchId);

            if (!sales.Any()) return APIResponse<CustomerBill>.ErrorResponse();

            var response = new CustomerBill()
            {
                Bills = sales,
            };

            response.SubTotal = sales.Aggregate(0.0, (acc, sale) => acc + sale.TotalAmount);

            response.BillTemplate = await emailTemplateRenderer.RenderTemplateAsync("Bill.cshtml", response);

            return APIResponse<CustomerBill>.SuccessResponse(result:response);

            
        }




        public async Task<APIResponse<IEnumerable<SaleResponse>>> FilterSaleByCustomer(Guid customerId)
        {
            var sales = await saleRepository.FilterSaleByCustomer(customerId);

            if (!sales.Any()) return APIResponse<IEnumerable<SaleResponse>>.ErrorResponse();

            return APIResponse<IEnumerable<SaleResponse>>.SuccessResponse(result: sales);
        }




        public async Task<APIResponse<IEnumerable<SaleResponse>>> NewlyAddedSales(Guid hatchId)
        {
            var sales = await saleRepository.NewlyAddedSales(hatchId);

            if (!sales.Any()) return APIResponse<IEnumerable<SaleResponse>>.ErrorResponse();

            return APIResponse<IEnumerable<SaleResponse>>.SuccessResponse(result: sales);
        }




        public async Task<APIResponse<IEnumerable<SaleResponse>>> PendingPaymentSales(Guid hatchId)
        {
            var sales = await saleRepository.ViewPendingSalePayment(hatchId);

            if (!sales.Any()) return APIResponse<IEnumerable<SaleResponse>>.ErrorResponse();

            return APIResponse<IEnumerable<SaleResponse>>.SuccessResponse(result: sales);
        }




        public async Task<APIResponse<SaleDetails>> Sale(Guid id)
        {
            var sale = await saleRepository.Sale(id);

            if (sale is null) return APIResponse<SaleDetails>.ErrorResponse();

            return APIResponse<SaleDetails>.SuccessResponse(result: sale);
        }



        public async Task<APIResponse<int>> UpdateSalePaymentStatus(UpdateSalePaymentStatus model)
        {
            var res = await saleRepository.ChangeSalePaymentStatus(model.Id, model.PaymentStatus);

            if (res > 0) return APIResponse<int>.SuccessResponse("Payment status changed", result:1);

            return APIResponse<int>.ErrorResponse();
        }



        public async Task<APIResponse<IEnumerable<SaleResponse>>> ViewSale(Guid hatchId)
        {
            var sales = await saleRepository.ViewSale(hatchId);

            if (!sales.Any()) return APIResponse<IEnumerable<SaleResponse>>.ErrorResponse();

            return APIResponse<IEnumerable<SaleResponse>>.SuccessResponse(result: sales);
        }
    }
}
