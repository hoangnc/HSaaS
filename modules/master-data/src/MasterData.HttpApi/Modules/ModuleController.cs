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
    public class ModuleController : MasterDataController, IModuleAppService
    {
        protected IModuleAppService ModuleAppService { get; }

        public ModuleController(IModuleAppService moduleAppService)
        {
            ModuleAppService = moduleAppService;
        }

        [HttpPost]
        public async Task<ModuleDto> CreateAsync(ModuleCreateDto input)
        {
            return await ModuleAppService.CreateAsync(input);
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await ModuleAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ModuleDto> GetAsync(Guid id)
        {
            return await ModuleAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<ModuleDto>> GetListAsync(GetModulesInput input)
        {
            return await ModuleAppService.GetListAsync(input);
        }

        [HttpPut]
        public Task<ModuleDto> UpdateAsync(Guid id, ModuleUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}
