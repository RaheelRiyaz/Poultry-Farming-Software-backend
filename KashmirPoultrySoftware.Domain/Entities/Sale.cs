using KashmirPoultrySoftware.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public Guid HatchId { get; set; }
        public double NoOfKilograms { get; set; }
        public double Rate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }






        #region Navigational Properties
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;


        [ForeignKey(nameof(HatchId))]
        public Hatch Hatch { get; set; } = null!;
        #endregion Navigational Properties
    }

}
