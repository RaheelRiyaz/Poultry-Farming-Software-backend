using AutoMapper;
using KashmirPoultrySoftware.Application.Abstraction.IRepository;
using KashmirPoultrySoftware.Application.Abstraction.IServices;
using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IFileService fileService;
        private readonly IMapper mapper;
        private readonly IHttpContext httpContext;

        public CustomerService(ICustomerRepository customerRepository, IFileService fileService, IMapper mapper, IHttpContext httpContext)
        {
            this.customerRepository = customerRepository;
            this.fileService = fileService;
            this.mapper = mapper;
            this.httpContext = httpContext;
        }



        public async Task<APIResponse<CustomerResponse>> AddCustomer(CustomerRequest model)
        {
            var customer = mapper.Map<Customer>(model);

            var fileResponse = await fileService.UploadFileAsync(customer.Id, model.File);

            if (fileResponse.Result is null) return APIResponse<CustomerResponse>.ErrorResponse();

            customer.EntityId = await httpContext.GetId();

            var res = await customerRepository.AddAsync(customer);

            if(res > 0) return APIResponse<CustomerResponse>.SuccessResponse(message:"Customer added successfully", result:mapper.Map<CustomerResponse>(customer));

            return APIResponse<CustomerResponse>.ErrorResponse();
        }




        public async Task<APIResponse<CustomerResponse>> GetCustomer(Guid id)
        {
            var customer = await customerRepository.GetCustomer(id);

            if (customer is null) return APIResponse<CustomerResponse>.ErrorResponse();

            return APIResponse<CustomerResponse>.SuccessResponse(result: customer);
        }




        public async Task<APIResponse<IEnumerable<CustomerResponse>>> GetCustomers()
        {
            var customers = await customerRepository.GetCustomers(await httpContext.GetId());

            if (!customers.Any()) return APIResponse<IEnumerable<CustomerResponse>>.ErrorResponse();

            return APIResponse<IEnumerable<CustomerResponse>>.SuccessResponse(result: customers);
        }


        public async Task<APIResponse<IEnumerable<CustomerResponse>>> SearchCustomer(string name)
        {
            var entityId = await httpContext.GetId();

            var customers = await customerRepository.SearchCustomers(name,entityId);

            if (!customers.Any()) return APIResponse<IEnumerable<CustomerResponse>>.ErrorResponse();

            return APIResponse<IEnumerable<CustomerResponse>>.SuccessResponse(result: mapper.Map<IEnumerable<CustomerResponse>>(customers));
        }
    }
}
