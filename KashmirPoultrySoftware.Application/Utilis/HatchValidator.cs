using KashmirPoultrySoftware.Application.Abstraction.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Utilis
{
    public class HatchValidator
    {
        private readonly IHatchRepository hatchRepository;

        public HatchValidator(IHatchRepository hatchRepository)
        {
            this.hatchRepository = hatchRepository;
        }


        public async Task<bool> CheckHatchStatus(Guid hatchId)
        {
            var hatch = await hatchRepository.GetByIdAsync(hatchId);

            if (hatch is null) return false;

            if ((int)hatch.HatchStatus == 2) return false;

            return true;
        }
    }
}
