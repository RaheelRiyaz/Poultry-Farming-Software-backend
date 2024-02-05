using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Abstraction.IRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<UserInfo?> UserInfo(Guid id);
    }
}
