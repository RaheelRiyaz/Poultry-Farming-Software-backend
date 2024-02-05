using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.RRModels
{
    public record CustomerRequest(string Name, string Email, string ContactNo,IFormFile File);


    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public Guid? FileId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
        public string filePath { get; set; } = null!;
    };

}
