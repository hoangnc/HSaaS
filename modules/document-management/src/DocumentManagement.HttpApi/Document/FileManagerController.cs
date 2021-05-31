using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Volo.Abp;
using Volo.Abp.Settings;

namespace DocumentManagement.Document
{
    [Controller]
    [RemoteService(Name = DocumentManagementRemoteServiceConsts.RemoteServiceName)]
    [Area("documentManagement")]
    [Route("api/document-management/filemanager")]
    [Authorize]
    public class FileManagerController : DocumentManagementController
    {
        private const string UploadFolderPath = "DocumentManagement.UploadFilePath";
        private ISettingProvider SettingProvider { get { return LazyServiceProvider.LazyGetService<ISettingProvider>(); } }
        private readonly IMineMappingService _mineMappingService;
        public FileManagerController(IMineMappingService mineMappingService)
        {
            _mineMappingService = mineMappingService;
        }

        [HttpGet]
        [Route("view-file")]
        public async Task<FileResult> ViewFile([FromQuery] string sourceDoc)
        {
            string uploadFolderPath = await SettingProvider.GetOrNullAsync(UploadFolderPath);
            string filePath = $"{uploadFolderPath}\\{sourceDoc.Replace("/", "\\")}";
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string mime = _mineMappingService.GetContentType(sourceDoc);

            if (mime == "application/pdf")
            {
                var cd = new ContentDisposition { FileName = Path.GetFileName(sourceDoc), Inline = true };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                return File(fileBytes, _mineMappingService.GetContentType(sourceDoc));
            }

            return File(fileBytes, _mineMappingService.GetContentType(sourceDoc), Path.GetFileName(sourceDoc));
        }
    }
}
