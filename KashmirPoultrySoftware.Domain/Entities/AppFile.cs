using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Domain.Entities
{
    public class AppFile : BaseEntity
    {
        public string FilePath { get; set; } = null!;
        public Guid EntityId { get; set; }
    }
}
