using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Domain.Entities;
using KashmirPoultrySoftware.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Abstraction.IRepository
{
    public interface IHatchRepository : IBaseRepository<Hatch>
    {
        Task<IEnumerable<HatchResponse>> GetAllHatchesByEntity(Guid entityId);
        Task<HatchDetails?> HatchDetailsById(Guid hatchId);
        Task<int> ChangeHatchStatus(HatchStatus hatchStatus, Guid hatchId, DateTime HatchFinishDate);
    }
}
