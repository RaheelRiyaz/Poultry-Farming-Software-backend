using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Abstraction.IServices
{
    public interface ICustomerService
    {
        Task<APIResponse<CustomerResponse>> AddCustomer(CustomerRequest model);
        Task<APIResponse<IEnumerable<CustomerResponse>>> GetCustomers();
        Task<APIResponse<IEnumerable<CustomerResponse>>> SearchCustomer(string name);
        Task<APIResponse<CustomerResponse>> GetCustomer(Guid id);
    }
}
