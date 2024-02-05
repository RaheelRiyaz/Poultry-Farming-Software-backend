using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Abstraction.IServices
{
    public interface IFileService
    {
        Task<APIResponse<AppFileResponse>> UploadFileAsync(Guid entityId,IFormFile file);
        Task<APIResponse<AppFileResponse>> DeleteFileAsync(Guid fileId);
        Task<APIResponse<IEnumerable<AppFileResponse>>> UploadFilesAsync(Guid entityId,IFormFileCollection files);
    }
}
