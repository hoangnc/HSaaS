using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace MasterData.Modules
{
    public interface IModuleAppService : ICrudAppService<ModuleDto, Guid, GetModulesInput, ModuleCreateDto, ModuleUpdateDto>
    {
    }
}
