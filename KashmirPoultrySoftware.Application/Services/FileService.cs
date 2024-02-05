using AutoMapper;
using KashmirPoultrySoftware.Application.Abstraction.IRepository;
using KashmirPoultrySoftware.Application.Abstraction.IServices;
using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IStorageService storageService;
        private readonly IFileRepository fileRepository;
        private readonly IMapper mapper;

        public FileService(
            IStorageService storageService, IFileRepository fileRepository, IMapper mapper)
        {
            this.storageService = storageService;
            this.fileRepository = fileRepository;
            this.mapper = mapper;
        }

        public async Task<APIResponse<AppFileResponse>> DeleteFileAsync(Guid fileId)
        {
            var file = await fileRepository.GetByIdAsync(fileId);
            if (file is null) return APIResponse<AppFileResponse>.ErrorResponse();

            var storageResponse = await storageService.DeleteFile(file.FilePath);

            var res = await fileRepository.DeleteAsync(file);

            if (res > 0) return APIResponse<AppFileResponse>.SuccessResponse(result: mapper.Map<AppFileResponse>(file));

            return APIResponse<AppFileResponse>.ErrorResponse();
        }




        public async Task<APIResponse<AppFileResponse>> UploadFileAsync(Guid entityId, IFormFile file)
        {
            var storageResponse = await storageService.SaveFile(file);

            var appfile = new AppFile()
            {
                EntityId = entityId,
                FilePath = storageResponse,
            };

            var res = await fileRepository.AddAsync(appfile);

            if (res > 0) return APIResponse<AppFileResponse>.SuccessResponse(result: mapper.Map<AppFileResponse>(appfile));

            return APIResponse<AppFileResponse>.ErrorResponse();
        }




        public async Task<APIResponse<IEnumerable<AppFileResponse>>> UploadFilesAsync(Guid entityId, IFormFileCollection files)
        {
            var storageResponses = await storageService.SaveFiles(files);

            List<AppFile> appFiles = new List<AppFile>();

            foreach (var file in storageResponses)
            {
                appFiles.Add(new AppFile()
                {
                    EntityId = entityId,
                    FilePath = file
                });
            }

            var res = await fileRepository.AddRangeAsync(appFiles);

            if(res > 0) return APIResponse<IEnumerable<AppFileResponse>>.SuccessResponse(result:mapper.Map<IEnumerable<AppFileResponse>>(appFiles));

            return APIResponse<IEnumerable<AppFileResponse>>.ErrorResponse();
        }
    }
}
