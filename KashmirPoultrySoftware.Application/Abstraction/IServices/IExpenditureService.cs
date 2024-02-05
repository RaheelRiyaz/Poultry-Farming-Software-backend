using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Abstraction.IServices
{
    public interface IExpenditureService
    {
        Task<APIResponse<ExpenditureResponse>> AddExpenditure(ExpenditureRequest model);
        Task<APIResponse<IEnumerable<ExpenditureResponse>>> ViewExpenditures(Guid hatchId);
    }
}
