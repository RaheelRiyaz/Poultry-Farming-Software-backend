using KashmirPoultrySoftware.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.RRModels
{
    public record SaleRequest(Guid CustomerId, Guid HatchId, double NoOfKilograms, double Rate, PaymentStatus PaymentStatus);


    public class SaleResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid HatchId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public double Rate { get; set; }
        public double TotalAmount { get; set; }
        public double NoOfKilograms { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
        public DateTime SaleDate { get; set; }
    }


    public class SaleDetails : SaleResponse
    {
        public Guid HatchId { get; set; }
        public string HatchName { get; set; } = null!;
        public int NoOfChicks { get; set; }
        public DateTime HatchDate { get; set; }
    }

    public class UpdateSalePaymentStatus
    {
        public Guid Id { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }

    public class Bill
    {
        public Guid SaleId { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CustomerContact { get; set; } = null!;
        public double Rate { get; set; }
        public double TotalAmount { get; set; }
        public double NoOfKilograms { get; set; }
        public DateTime SoldOn { get; set; }
    }


    public class CustomerBill
    {
        public double SubTotal { get; set; }
        public IEnumerable<Bill> Bills { get; set; } = null!;
        public string BillTemplate { get; set; } = null!;
    }


    public record TableRequest(string HtmlTable);
}
