using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Abstraction.IRepository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<IEnumerable<CustomerResponse>> GetCustomers(Guid entityId);
        Task<IEnumerable<CustomerResponse>> SearchCustomers(string name, Guid entityId);
        Task<CustomerResponse?> GetCustomer(Guid id);
    }
}
