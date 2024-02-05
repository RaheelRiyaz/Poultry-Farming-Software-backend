using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Domain.Entities
{
    public class Expenditure : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double TotalAmount { get; set; }
        public Guid HatchId { get; set; }






        #region Navigational Properties
        [ForeignKey(nameof(HatchId))]
        public Hatch Hatch { get; set; } = null!;
        #endregion Navigational Properties

    }
}
