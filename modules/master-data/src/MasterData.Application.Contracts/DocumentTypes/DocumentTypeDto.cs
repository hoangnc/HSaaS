
using System;
using Volo.Abp.Application.Dtos;

namespace MasterData.DocumentTypes
{
    public class DocumentTypeDto : FullAuditedEntityDto<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
