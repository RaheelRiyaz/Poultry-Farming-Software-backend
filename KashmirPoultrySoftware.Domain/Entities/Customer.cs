using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public Guid EntityId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ContactNo { get; set; } = null!;






        #region Navigational Properties
        [ForeignKey(nameof(EntityId))]
        public User User { get; set; } = null!;
        #endregion Navigational Properties
    }
}
