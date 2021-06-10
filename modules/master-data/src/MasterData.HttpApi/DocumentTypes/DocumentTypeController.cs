using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterData.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MasterData.DocumentTypes
{
    [RemoteService(Name = MasterDataRemoteServiceConsts.RemoteServiceName)]
    [Area("masterData")]
    [Route("api/master-data/documenttypes")]
    public class DocumentTypeController : MasterDataController, IDocumentTypeAppService
    {
        protected IDocumentTypeAppService DocumentTypeAppService { get; }

        public DocumentTypeController(IDocumentTypeAppService documentTypeAppService)
        {
            DocumentTypeAppService = documentTypeAppService;
        }

        [HttpPost]
        public async Task<DocumentTypeDto> CreateAsync(DocumentTypeCreateDto input)
        {
            return await DocumentTypeAppService.CreateAsync(input);
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await DocumentTypeAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<DocumentTypeDto> GetAsync(Guid id)
        {
            return await DocumentTypeAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<DocumentTypeDto>> GetListAsync(GetDocumentTypesInput input)
        {
            return await DocumentTypeAppService.GetListAsync(input);
        }

        [HttpPut]
        public Task<DocumentTypeDto> UpdateAsync(Guid id, DocumentTypeUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}
