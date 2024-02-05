using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Abstraction.IServices
{
    public interface IStorageService
    {
        Task<string> SaveFile(IFormFile file);
        Task<List<string>> SaveFiles(IFormFileCollection files);
        Task<string> DeleteFile(string filePath); 
        Task<List<string>> DeleteFiles(List<string> filePaths); 
    }
}
