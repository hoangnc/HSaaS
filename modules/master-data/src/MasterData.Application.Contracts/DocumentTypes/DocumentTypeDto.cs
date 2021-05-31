
using Volo.Abp.Application.Dtos;

namespace MasterData.DocumentTypes
{
    public class DocumentTypeDto : FullAuditedEntityDto<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
