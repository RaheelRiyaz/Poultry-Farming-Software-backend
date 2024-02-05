using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Abstraction.IServices
{
    public interface ISaleService
    {
        Task<APIResponse<Sale>> AddSale(SaleRequest sale);
        Task<APIResponse<IEnumerable<SaleResponse>>> ViewSale(Guid hatchId);
        Task<APIResponse<IEnumerable<SaleResponse>>> PendingPaymentSales(Guid hatchId);
        Task<APIResponse<IEnumerable<SaleResponse>>> NewlyAddedSales(Guid hatchId);
        Task<APIResponse<IEnumerable<SaleResponse>>> FilterSaleByCustomer(Guid customerId);
        Task<APIResponse<SaleDetails>> Sale(Guid id);
        Task<APIResponse<int>> UpdateSalePaymentStatus(UpdateSalePaymentStatus model);
        Task<APIResponse<CustomerBill>> CustomerBill(Guid customerId, Guid hatchId);
    }
}
