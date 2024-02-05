using KashmirPoultrySoftware.Application.Abstraction.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Services
{
    public class StoragService : IStorageService
    {
        private readonly string webRootPath;

        public StoragService(string webRootPath)
        {
            this.webRootPath = webRootPath;
        }



        public async Task<string> DeleteFile(string filePath)
        {
            var fullPath = GetPhysicalPath() + filePath;

            await Task.Run(() =>
            {
                File.Delete(fullPath);
            });

            return fullPath;
        }




        public async Task<List<string>> DeleteFiles(List<string> filePaths)
        {
            List<string> paths = new List<string>();
            foreach (var path in filePaths)
            {
                paths.Add(await DeleteFile(path));
            }

            return paths;
        }




        public async Task<string> SaveFile(IFormFile file)
        {
            var filePath = string.Concat(Guid.NewGuid(), file.FileName);

            var fullPath = string.Concat(GetPhysicalPath(), filePath);
            var stream = new FileStream(fullPath, FileMode.Create);

            await file.CopyToAsync(stream);

            return GetVirtualPath() + filePath;
        }




        public async Task<List<string>> SaveFiles(IFormFileCollection files)
        {
            List<string> paths = new List<string>();

            foreach (var file in files)
            {
                paths.Add(await SaveFile(file));
            }

            return paths;
        }






        #region Helper Functions
        private string GetPhysicalPath()
        {
            var path = this.webRootPath + "/files/";

            if (!Path.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }



        private string GetVirtualPath() => "files/";

        #endregion Helper Functions

    }
}
