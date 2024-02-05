using KashmirPoultrySoftware.Application.Abstraction.IServices;
using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KashmirPoultrySoftware.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpendituresController : ControllerBase
    {
        private readonly IExpenditureService expenditureService;

        public ExpendituresController(IExpenditureService expenditureService)
        {
            this.expenditureService = expenditureService;
        }


        [HttpPost]
        public async Task<APIResponse<ExpenditureResponse>> AddExpenditure(ExpenditureRequest model)
        {
            return await expenditureService.AddExpenditure(model);
        }



        [HttpGet("{hatchId:guid}")]
        public async Task<APIResponse<IEnumerable<ExpenditureResponse>>> GetALlExpenditures(Guid hatchId)
        {
            return await expenditureService.ViewExpenditures(hatchId);
        }
    }
}
