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
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(KashmirPoultrySoftwareDbContext dbContext) : base(dbContext)
        {
        }
        private readonly string baseQuery = $@"SELECT C.Id,C.Name,
                            C.Email,C.ContactNo,
                            ISNULL(A.FilePath,'') AS FilePath,
                            A.Id as FileId
                            FROM Customers C
                            LEFT JOIN AppFiles A
                            ON A.EntityId = C.Id  ";


        public async Task<IEnumerable<CustomerResponse>> GetCustomers(Guid entityId)
        {
            string query = $@" {baseQuery} WHERE C.EntityId = @entityId ";

            return await context.QueryAsync<CustomerResponse>(query, new { entityId });
        }


        public async Task<CustomerResponse?> GetCustomer(Guid id)
        {
            string query = $@" {baseQuery} WHERE C.Id = @id ";
            return await context.FirstOrdefaultAsync<CustomerResponse?>(query, new { id });
        }


        public async Task<IEnumerable<CustomerResponse>> SearchCustomers(string name, Guid entityId)
        {
            string query = $@"{baseQuery} WHERE C.Name LIKE '{name}%'";

            return await context.QueryAsync<CustomerResponse>(query, new { entityId });
        }


    }
}
