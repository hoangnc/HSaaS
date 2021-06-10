using System;
using Volo.Abp.Application.Dtos;

namespace MasterData.Modules
{
    public class ModuleUpdateDto : FullAuditedEntityDto<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
