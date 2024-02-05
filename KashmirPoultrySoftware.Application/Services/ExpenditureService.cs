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
    public class ExpenditureService : IExpenditureService
    {
        private readonly IExpenditureRepository expenditureRepository;
        private readonly IMapper mapper;
        private readonly HatchValidator hatchValidator;

        public ExpenditureService(IExpenditureRepository expenditureRepository, IMapper mapper,HatchValidator hatchValidator)
        {
            this.expenditureRepository = expenditureRepository;
            this.mapper = mapper;
            this.hatchValidator = hatchValidator;
        }



        public async Task<APIResponse<ExpenditureResponse>> AddExpenditure(ExpenditureRequest model)
        {
            var isActive = await hatchValidator.CheckHatchStatus(model.HatchId);
            if (isActive) return APIResponse<ExpenditureResponse>.ErrorResponse("Hatch is completed cannot add expenditure now");

            var expenditure = mapper.Map<Expenditure>(model);

            var res = await expenditureRepository.AddAsync(expenditure);

            if (res > 0) return APIResponse<ExpenditureResponse>.SuccessResponse(result: mapper.Map<ExpenditureResponse>(expenditure));

            return APIResponse<ExpenditureResponse>.ErrorResponse();
        }




        public async Task<APIResponse<IEnumerable<ExpenditureResponse>>> ViewExpenditures(Guid hatchId)
        {
            var expenditures = await expenditureRepository.FilterAsync(_ => _.HatchId == hatchId);

            if (!expenditures.Any()) return APIResponse<IEnumerable<ExpenditureResponse>>.ErrorResponse("No Expenditure Found");

            return APIResponse<IEnumerable<ExpenditureResponse>>.SuccessResponse(result:mapper.Map<IEnumerable<ExpenditureResponse>>(expenditures));
        }
    }
}
