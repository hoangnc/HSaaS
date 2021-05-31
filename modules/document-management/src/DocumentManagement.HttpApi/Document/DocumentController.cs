using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace DocumentManagement.Document
{
    [Controller]
    [RemoteService(Name = DocumentManagementRemoteServiceConsts.RemoteServiceName)]
    [Area("documentManagement")]
    [Route("api/document-management/documents")]
    public class DocumentController : DocumentManagementController, IDocumentAppService
    {
        private readonly IDocumentAppService _documentAppService;
        private readonly IDocumentEmailSenderService _documentEmailSenderService;

        public DocumentController(IDocumentAppService documentAppService,
            IDocumentEmailSenderService documentEmailSenderService)
        {
            _documentAppService = documentAppService;
            _documentEmailSenderService = documentEmailSenderService;
        }

        [HttpGet]
        [Route("by-code/{code}")]
        public async Task<DocumentDto> GetByCodeAsync(string code)
        {
            return await _documentAppService.GetByCodeAsync(code);
        }

        [HttpGet]
        [Route("by-name/{name}")]
        public virtual async Task<DocumentDto> FindByNameAsync(string name)
        {
            return await _documentAppService.FindByNameAsync(name);
        }

        [HttpGet]
        [Route("by-id/{id}")]
        public async Task<DocumentDto> GetAsync(long id)
        {
            return await _documentAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("get-list")]
        public async Task<PagedResultDto<DocumentDto>> GetListAsync(GetDocumentsInput input)
        {
            return await _documentAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("filtered-paged-list")]
        public async Task<PagedResultDto<DocumentDto>> FilteredPagedListAsync(GetDocumentsInput input)
        {
            return await _documentAppService.FilteredPagedListAsync(input);
        }

        [HttpPost]
        [Route("create")]
        public async Task<DocumentDto> CreateAsync([FromForm] CreateDocumentDto input)
        {
            return await _documentAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("create-and-release")]
        public async Task<DocumentDto> CreateAndReleaseAsync([FromForm] CreateDocumentDto input)
        {
            var result = await _documentAppService.CreateAsync(input);
            await _documentEmailSenderService.SendMailReleaseDocumentAsync(result);
            return result;
        }

        [HttpPost]
        [Route("review")]
        public async Task<DocumentDto> ReviewAsync([FromForm] ReviewDocumentDto input)
        {
            return await _documentAppService.ReviewAsync(input);
        }

        [HttpPost]
        [Route("review-and-release")]
        public async Task<DocumentDto> ReviewAndReleaseAsync([FromForm] ReviewDocumentDto input)
        {
            var result = await _documentAppService.ReviewAsync(input);
            await _documentEmailSenderService.SendMailReviewAndReleaseAsync(result);
            return result;
        }

        [HttpPut]
        [Route("update")]
        public async Task<DocumentDto> UpdateAsync(long id, [FromForm] UpdateDocumentDto input)
        {
            var result = await _documentAppService.UpdateAsync(id, input);
            return result;
        }

        [HttpPut]
        [Route("update-and-release")]
        public async Task<DocumentDto> UpdateAndReleaseAsync(long id, [FromForm] UpdateDocumentDto input)
        {
            var result = await _documentAppService.UpdateAsync(id, input);
            await _documentEmailSenderService.SendMailReleaseDocumentAsync(result);
            return result;
        }

        [HttpDelete]
        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("delete-file")]
        public async Task DeleteFileByDocumentIdAndFileNameAsync([FromBody] DeleteFileByDocumentIdAndFileNameInput input)
        {
            await _documentAppService.DeleteFileByDocumentIdAndFileNameAsync(input);
        }

        [HttpGet]
        [Route("filtered-paged-list-by-document-type")]
        public async Task<PagedResultDto<DocumentDto>> FilteredPagedListByDocumentTypeAsync(GetDocumentsByDocumentTypeInput input)
        {
            return await _documentAppService.FilteredPagedListByDocumentTypeAsync(input);
        }

        [HttpGet]
        [Route("filtered-paged-list-by-department-code")]
        public async Task<PagedResultDto<DocumentDto>> FilteredPagedListByDepartmentCodeAsync(GetDocumentsByDepartmentCodeInput input)
        {
            return await _documentAppService.FilteredPagedListByDepartmentCodeAsync(input);
        }
    }
}
