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
    public class MotivalityService : IMotivalityService
    {
        private readonly IMotivalityRepository motivalityRepository;
        private readonly IMapper mapper;
        private readonly IHatchRepository hatchRepository;
        private readonly HatchValidator hatchValidator;

        public MotivalityService(IMotivalityRepository motivalityRepository, IMapper mapper, IHatchRepository hatchRepository,HatchValidator hatchValidator)
        {
            this.motivalityRepository = motivalityRepository;
            this.mapper = mapper;
            this.hatchRepository = hatchRepository;
            this.hatchValidator = hatchValidator;
        }



        public async Task<APIResponse<MotalityResponse>> AddMotivalityRecord(MotalityRequest model)
        {
            var isActive = await hatchValidator.CheckHatchStatus(model.HatchId);
            if (isActive) return APIResponse<MotalityResponse>.ErrorResponse("Hatch is completed cannot add motalities now");

            var hatch = await hatchRepository.GetByIdAsync(model.HatchId);
            if (hatch is null) return APIResponse<MotalityResponse>.ErrorResponse();

            hatch.TotalMotality += model.NoOfChicks;

            var updateHatchResponse = await hatchRepository.UpdateAsync(hatch);
            if (updateHatchResponse <= 0) return APIResponse<MotalityResponse>.ErrorResponse();

            var motivality = mapper.Map<Motality>(model);

            var res = await motivalityRepository.AddAsync(motivality);

            if (res > 0) return APIResponse<MotalityResponse>.SuccessResponse("Motality record added",result: mapper.Map<MotalityResponse>(motivality));

            return APIResponse<MotalityResponse>.ErrorResponse();
        }




        public async Task<APIResponse<IEnumerable<MotalityResponse>>> GetMotivalityRecord(Guid hatchId)
        {
            var motivalities = await motivalityRepository.FilterAsync(_ => _.HatchId == hatchId);

            if (!motivalities.Any()) return APIResponse<IEnumerable<MotalityResponse>>.ErrorResponse("No Record found");

            return APIResponse<IEnumerable<MotalityResponse>>.SuccessResponse(result: mapper.Map<IEnumerable<MotalityResponse>>(motivalities.OrderBy(_=>_.Day)));
        }
    }
}
