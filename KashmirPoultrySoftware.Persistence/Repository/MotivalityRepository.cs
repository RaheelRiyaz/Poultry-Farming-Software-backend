using KashmirPoultrySoftware.Application.Abstraction.IRepository;
using KashmirPoultrySoftware.Domain.Entities;
using KashmirPoultrySoftware.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Persistence.Repository
{
    public class MotivalityRepository : BaseRepository<Motality>, IMotivalityRepository
    {
        public MotivalityRepository(KashmirPoultrySoftwareDbContext dbContext) : base(dbContext)
        {
        }
    }
}
