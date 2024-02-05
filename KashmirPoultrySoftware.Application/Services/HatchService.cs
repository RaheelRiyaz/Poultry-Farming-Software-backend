using AutoMapper;
using KashmirPoultrySoftware.Application.Abstraction.IRepository;
using KashmirPoultrySoftware.Application.Abstraction.IServices;
using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Application.Utilis;
using KashmirPoultrySoftware.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Services
{
    public class HatchService : IHatchService
    {
        private readonly IHatchRepository hatchRepository;
        private readonly IMapper mapper;
        private readonly IHttpContext httpContext;

        public HatchService(IHatchRepository hatchRepository, IMapper mapper, IHttpContext httpContext)
        {
            this.hatchRepository = hatchRepository;
            this.mapper = mapper;
            this.httpContext = httpContext;
        }



        public async Task<APIResponse<HatchRequest>> AddHatch(HatchRequest model)
        {
            var hatch = mapper.Map<Hatch>(model);
            hatch.EntityId = await httpContext.GetId();

            var initialExpenditure = new Expenditure()
            {
                HatchId = hatch.Id,
                Name = hatch.Name,
                Description = "Chicks expedniture",
                TotalAmount = hatch.NoOfChicks * hatch.ChickPerPrice
            };

            hatch.Expenditure = initialExpenditure;

            var res = await hatchRepository.AddAsync(hatch);

            if (res > 0) return APIResponse<HatchRequest>.SuccessResponse(result:model);

            return APIResponse<HatchRequest>.ErrorResponse();
        }

        public async Task<APIResponse<int>> ChangeHatchStatus(HatchUpdateRequest model)
        {
            var res = await hatchRepository.ChangeHatchStatus(model.HatchStatus, model.HatchId,model.HatchFinishDate);

            if (res > 0) return APIResponse<int>.SuccessResponse("Hatch status changes successfully", result: res);

            return APIResponse<int>.ErrorResponse();
        }

        public async Task<APIResponse<IEnumerable<HatchResponse>>> GetAllHatches()
        {
            var hatches = await hatchRepository.GetAllHatchesByEntity(await httpContext.GetId());
            if (!hatches.Any()) return APIResponse<IEnumerable<HatchResponse>>.ErrorResponse("No Hatch Found");

            return APIResponse<IEnumerable<HatchResponse>>.SuccessResponse(result: hatches);
        }



        public async Task<APIResponse<HatchDetails>> GetHatchDetailsById(Guid id)
        {
            var hatch = await hatchRepository.HatchDetailsById(id);

            if (hatch is null) return APIResponse<HatchDetails>.ErrorResponse();

            return APIResponse<HatchDetails>.SuccessResponse(result: hatch);
        }

    }
}
