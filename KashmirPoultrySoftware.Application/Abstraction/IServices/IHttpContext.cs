using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Abstraction.IServices
{
    public interface IHttpContext
    {
        Task<Guid> GetId(); 
        Task<string> GetUserName(); 
    }
}
