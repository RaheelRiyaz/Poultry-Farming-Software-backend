using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Abstraction.IServices
{
    public interface IMotivalityService 
    {
        Task<APIResponse<IEnumerable<MotalityResponse>>> GetMotivalityRecord(Guid hatchId);
        Task<APIResponse<MotalityResponse>> AddMotivalityRecord(MotalityRequest model);
    }
}
