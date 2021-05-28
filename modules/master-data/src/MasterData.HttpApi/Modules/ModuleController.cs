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

namespace MasterData.Modules
{
    [RemoteService(Name = MasterDataRemoteServiceConsts.RemoteServiceName)]
    [Area("masterData")]
    [Route("api/master-data/modules")]
    [Authorize]
    public class ModuleController : MasterDataController, IModuleAppService
    {
        protected IModuleAppService ModuleAppService { get; }

        public ModuleController(IModuleAppService moduleAppService)
        {
            ModuleAppService = moduleAppService;
        }

        [HttpPost]
        [Authorize(MasterDataPermissions.Modules.Create)]
        public async Task<ModuleDto> CreateAsync(ModuleCreateDto input)
        {
            return await ModuleAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Authorize(MasterDataPermissions.Modules.Delete)]
        public async Task DeleteAsync(long id)
        {
            await ModuleAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(MasterDataPermissions.Modules.Default)]
        public async Task<ModuleDto> GetAsync(long id)
        {
            return await ModuleAppService.GetAsync(id);
        }

        [HttpGet]
        [Authorize(MasterDataPermissions.Modules.Default)]
        public async Task<PagedResultDto<ModuleDto>> GetListAsync(GetModulesInput input)
        {
            return await ModuleAppService.GetListAsync(input);
        }

        [HttpPut]
        [Authorize(MasterDataPermissions.Modules.Update)]
        public Task<ModuleDto> UpdateAsync(long id, ModuleUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}
