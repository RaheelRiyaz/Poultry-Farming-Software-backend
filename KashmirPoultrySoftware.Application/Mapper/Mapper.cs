using AutoMapper;
using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserSignupResponse>();
            CreateMap<UserSignup, User>();
        }
    }




    public class HatchMapper : Profile
    {
        public HatchMapper()
        {
            CreateMap<HatchRequest, Hatch>();
            CreateMap<Hatch, HatchResponse>();
        }
    }



    public class MotivalityMapper : Profile
    {
        public MotivalityMapper()
        {
            CreateMap<MotalityRequest, Motality>();
            CreateMap<Motality, MotalityResponse>();
        }
    }



    public class ExpenditureMapper : Profile
    {
        public ExpenditureMapper()
        {
            CreateMap<ExpenditureRequest, Expenditure>();
            CreateMap<Expenditure, ExpenditureResponse>();
        }
    }



    public class AppFileMapper : Profile
    {
        public AppFileMapper()
        {
            CreateMap<AppFile, AppFileResponse>();
        }
    }



    public class CustomerMapper : Profile
    {
        public CustomerMapper()
        {
            CreateMap<CustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();
        }
    }


    public class SaleMapper : Profile
    {
        public SaleMapper()
        {
            CreateMap<SaleRequest, Sale>();
            CreateMap<Sale, SaleResponse>();
        }
    }
}
