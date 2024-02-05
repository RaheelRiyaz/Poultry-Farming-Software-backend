using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.RRModels
{
    public record ExpenditureRequest(Guid HatchId, string Name,string Description, double TotalAmount);
    public record ExpenditureResponse(Guid HatchId, string Name,string Description, double TotalAmount,Guid id);
}
