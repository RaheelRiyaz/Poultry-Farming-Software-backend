using KashmirPoultrySoftware.Application.Abstraction.IServices;
using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KashmirPoultrySoftware.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotalitiesController : ControllerBase
    {
        private readonly IMotivalityService motivalityService;

        public MotalitiesController(IMotivalityService motivalityService)
        {
            this.motivalityService = motivalityService;
        }


        [HttpGet("{hatchId:guid}")]
        public async Task<APIResponse<IEnumerable<MotalityResponse>>> GetMotalityRecord(Guid hatchId)
        {
            return await motivalityService.GetMotivalityRecord(hatchId);
        }



        [HttpPost]
        public async Task<APIResponse<MotalityResponse>> AddMotalityRecord(MotalityRequest model)
        {
            return await motivalityService.AddMotivalityRecord(model);
        }
    }
}
