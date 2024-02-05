using KashmirPoultrySoftware.Application.Abstraction.IServices;
using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Persistence.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KashmirPoultrySoftware.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HatchesController : ControllerBase
    {
        private readonly IHatchService hatchService;

        public HatchesController(IHatchService hatchService)
        {
            this.hatchService = hatchService;
        }



        [HttpPost]
        public async Task<APIResponse<HatchRequest>> AddHatch(HatchRequest model)
        {
            return await hatchService.AddHatch(model);
        }


        [HttpGet("all-hatches")]
        public async Task<APIResponse<IEnumerable<HatchResponse>>> GetAllHatches()
        {
            return await hatchService.GetAllHatches();
        }


        [HttpGet("{hatchId:guid}")]
        public async Task<APIResponse<HatchDetails>> GetHatchDetailsById(Guid hatchId)
        {
            return await hatchService.GetHatchDetailsById(hatchId);
        }


        [HttpPut("change-status")]
        public async Task<APIResponse<int>> ChangeHatchStatus(HatchUpdateRequest model)
        {
            return await hatchService.ChangeHatchStatus(model);
        }
    }
}
