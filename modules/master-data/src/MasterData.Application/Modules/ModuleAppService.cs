using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MasterData.Modules
{
    public class ModuleAppService : MasterDataAppService, IModuleAppService
    {
        protected IModuleRepository ModuleRepository { get; }

        public ModuleAppService(
            IModuleRepository moduleRepository
            )
        {
            ModuleRepository = moduleRepository;
        }
        public virtual async Task<ModuleDto> CreateAsync(ModuleCreateDto input)
        {
            var module = ObjectMapper.Map<ModuleCreateDto, Module>(input);

            var moduleExisting = await ModuleRepository.GetByCodeAsync(input.Code);

            if (moduleExisting?.Id != Guid.Empty)
            {
                throw new BusinessException(code: MasterDataErrorCodes.Module.CodeHasExisted)
                                .WithData("Code", input.Code);
            }

            module = await ModuleRepository.InsertAsync(module);

            return ObjectMapper.Map<Module, ModuleDto>(module);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var module = await ModuleRepository.GetByIdAsync(id);

            if (module == null)
            {
                return;
            }

            await ModuleRepository.DeleteAsync(module);
        }

        public async Task<ModuleDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Module, ModuleDto>(
                await ModuleRepository.GetByIdAsync(id)
            );
        }

        public virtual async Task<PagedResultDto<ModuleDto>> GetListAsync(GetModulesInput input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Module.Name);
            }

            var count = await ModuleRepository.GetCountAsync();

            var modules = await ModuleRepository.GetPagedListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
            );

            return new PagedResultDto<ModuleDto>(
                count,
                ObjectMapper.Map<List<Module>, List<ModuleDto>>(modules)
            );
        }

        public async Task<ModuleDto> UpdateAsync(Guid id, ModuleUpdateDto input)
        {
            var module = await ModuleRepository.GetByIdAsync(id);
            module.Name = input.Name;

            return ObjectMapper.Map<Module, ModuleDto>(module);
        }
    }
}
