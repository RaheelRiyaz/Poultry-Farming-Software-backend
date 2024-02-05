using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Abstraction.IServices
{
    public interface IHatchService 
    {
        Task<APIResponse<IEnumerable<HatchResponse>>> GetAllHatches();

        Task<APIResponse<HatchRequest>> AddHatch(HatchRequest model);

        Task<APIResponse<HatchDetails>> GetHatchDetailsById(Guid id);

        Task<APIResponse<int>> ChangeHatchStatus(HatchUpdateRequest model);
    }
}
