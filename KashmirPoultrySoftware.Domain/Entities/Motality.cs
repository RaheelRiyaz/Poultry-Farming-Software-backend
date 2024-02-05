using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Domain.Entities
{
    public class Motality : BaseEntity
    {
        public int Day { get; set; }
        public string Cause { get; set; } = null!;
        public int NoOfChicks { get; set; }
        public Guid HatchId { get; set; }





        #region Navigational Properties
        [ForeignKey(nameof(HatchId))]
        public Hatch Hatch { get; set; } = null!;
        #endregion Navigational Properties
    }
}
