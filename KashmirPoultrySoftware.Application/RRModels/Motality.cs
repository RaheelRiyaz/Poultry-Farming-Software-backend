using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.RRModels
{
    public record MotalityRequest (int day, string cause, int NoOfChicks ,Guid HatchId);
    public record MotalityResponse (int day, string cause, int NoOfChicks ,Guid HatchId);
    
}
