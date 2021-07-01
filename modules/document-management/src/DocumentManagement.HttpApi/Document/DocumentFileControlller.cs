using DocumentManagement.Documents;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace DocumentManagement.Document
{
    [Controller]
    [RemoteService(Name = DocumentManagementRemoteServiceConsts.RemoteServiceName)]
    [Area("documentManagement")]
    [Route("api/document-management/documentfiles")]
    public class DocumentFileControlller : DocumentManagementController, IDocumentFileAppService
    {
        private readonly IDocumentFileAppService _documentFileAppService;
        private readonly IMineMappingService _mineMappingService;

        public DocumentFileControlller(IDocumentFileAppService documentFileAppService, IMineMappingService mineMappingService)
        {
            _documentFileAppService = documentFileAppService;
            _mineMappingService = mineMappingService;
        }

        [HttpGet("get-document-file")]
        public async Task<DocumentFileDto> GetDocumentFileAsync(GetDocumentFileDto input)
        {
            return await _documentFileAppService.GetDocumentFileAsync(input);
        }

        [HttpPost]
        public async Task SaveDocumentFileAsync([FromForm]SaveDocumentFileDto input)
        {
            await _documentFileAppService.SaveDocumentFileAsync(input);
        }

        [HttpGet("download/{fileName}")]
        public async Task<FileResult> GetDocumentFileAsync(string fileName)
        {
            var documentFileDto = await _documentFileAppService.GetDocumentFileAsync(new GetDocumentFileDto { Name = fileName });
            string mime = _mineMappingService.GetContentType(fileName);

            if (mime == "application/pdf")
            {
                var cd = new ContentDisposition { FileName = Path.GetFileName(documentFileDto.Name), Inline = true };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                return File(documentFileDto.Content, mime);
            }

            return File(documentFileDto.Content, mime, documentFileDto.Name);
        }
    }
}
