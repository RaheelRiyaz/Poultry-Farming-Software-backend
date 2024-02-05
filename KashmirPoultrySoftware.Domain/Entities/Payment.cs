using KashmirPoultrySoftware.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace KashmirPoultrySoftware.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid SaleId { get; set; }
        public double Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }




        #region Navigational Properties
        [ForeignKey(nameof(SaleId))]
        public Sale Sale { get; set; } = null!;
        #endregion Navigational Properties
    }
}
