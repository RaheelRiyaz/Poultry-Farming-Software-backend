using KashmirPoultrySoftware.Application.Abstraction.IRepository;
using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Domain.Entities;
using KashmirPoultrySoftware.Persistence.Dapper;
using KashmirPoultrySoftware.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Persistence.Repository
{
    public class UserRpository : BaseRepository<User>, IUserRepository
    {

        public UserRpository(KashmirPoultrySoftwareDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserInfo?> UserInfo(Guid id)
        {
            string query = $@"SELECT UserName,Email,
                            ContactNo
                            FROM Users
                            WHERE Id = @id";
            return await context.FirstOrdefaultAsync<UserInfo?>(query, new { id });
        }
    }
}
