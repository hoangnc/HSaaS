using Volo.Abp.Application.Dtos;

namespace MasterData.Modules
{
    public class ModuleDto : FullAuditedEntityDto<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
