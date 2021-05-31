using Volo.Abp.Application.Dtos;

namespace MasterData.Modules
{
    public class ModuleUpdateDto : FullAuditedEntityDto<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
