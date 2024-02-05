using KashmirPoultrySoftware.Application.Abstraction.IServices;
using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KashmirPoultrySoftware.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }


        [HttpPost]
        public async Task<APIResponse<CustomerResponse>> AddCustomer([FromForm] CustomerRequest model)
        {
            return await customerService.AddCustomer(model);
        }



        [HttpGet]
        public async Task<APIResponse<IEnumerable<CustomerResponse>>> Customers()
        {
            return await customerService.GetCustomers();
        }



        [HttpGet("{id:guid}")]
        public async Task<APIResponse<CustomerResponse>> Customer(Guid id)
        {
            return await customerService.GetCustomer(id);
        }



        [HttpGet("search/{name}")]
        public async Task<APIResponse<IEnumerable<CustomerResponse>>> SearchCustomer(string name)
        {
            return await customerService.SearchCustomer(name);
        }
    }
}
