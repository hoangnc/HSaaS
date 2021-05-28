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
    [Authorize]
    public class DocumentTypeController : MasterDataController, IDocumentTypeAppService
    {
        protected IDocumentTypeAppService DocumentTypeAppService { get; }

        public DocumentTypeController(IDocumentTypeAppService documentTypeAppService)
        {
            DocumentTypeAppService = documentTypeAppService;
        }

        [HttpPost]
        [Authorize(MasterDataPermissions.DocumentTypes.Create)]
        public async Task<DocumentTypeDto> CreateAsync(DocumentTypeCreateDto input)
        {
            return await DocumentTypeAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Authorize(MasterDataPermissions.DocumentTypes.Delete)]
        public async Task DeleteAsync(long id)
        {
            await DocumentTypeAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(MasterDataPermissions.DocumentTypes.Default)]
        public async Task<DocumentTypeDto> GetAsync(long id)
        {
            return await DocumentTypeAppService.GetAsync(id);
        }

        [HttpGet]
        [Authorize(MasterDataPermissions.DocumentTypes.Default)]
        public async Task<PagedResultDto<DocumentTypeDto>> GetListAsync(GetDocumentTypesInput input)
        {
            return await DocumentTypeAppService.GetListAsync(input);
        }

        [HttpPut]
        [Authorize(MasterDataPermissions.DocumentTypes.Update)]
        public Task<DocumentTypeDto> UpdateAsync(long id, DocumentTypeUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}
