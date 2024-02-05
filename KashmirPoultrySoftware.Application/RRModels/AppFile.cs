using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.RRModels
{
    public record AppFileResponse (Guid id ,string filePath);

    public record AppFileRequest(Guid entityId,IFormFile file);
}
