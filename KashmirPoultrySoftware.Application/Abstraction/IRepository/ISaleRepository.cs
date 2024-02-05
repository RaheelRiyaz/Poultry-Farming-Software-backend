using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Domain.Entities;
using KashmirPoultrySoftware.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Abstraction.IRepository
{
    public interface ISaleRepository : IBaseRepository<Sale>
    {
        Task<IEnumerable<SaleResponse>> ViewSale(Guid hatchId);
        Task<IEnumerable<SaleResponse>> ViewPendingSalePayment(Guid hatchId);
        Task<IEnumerable<SaleResponse>> NewlyAddedSales(Guid hatchId);
        Task<IEnumerable<SaleResponse>> FilterSaleByCustomer(Guid customerId);
        Task<SaleDetails?> Sale(Guid id);
        Task<int> ChangeSalePaymentStatus(Guid saleId, PaymentStatus paymentStatus);

        Task<IEnumerable<Bill>> GenerateCustomerBill(Guid customerId, Guid hatchId);
    }
}
