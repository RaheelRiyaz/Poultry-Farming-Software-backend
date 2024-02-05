using KashmirPoultrySoftware.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Domain.Entities
{
    public class Hatch : BaseEntity
    {
        public Guid EntityId { get; set; }
        public string Name { get; set; } = null!;
        public int NoOfChicks { get; set; }
        public int TotalMotality { get; set; }
        public double ChickPerPrice { get; set; }
        public HatchStatus HatchStatus { get; set; } = HatchStatus.InProcess;

        public DateTime? HatchReleaseDate { get; set; }
        #region Navigational Properties
        [ForeignKey(nameof(EntityId))]
        public User User { get; set; } = null!;


        public Expenditure? Expenditure { get; set; }
        #endregion Navigational Properties
    }
}
