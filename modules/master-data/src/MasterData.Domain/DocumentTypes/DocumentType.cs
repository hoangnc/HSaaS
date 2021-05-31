using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace MasterData.DocumentTypes
{
    [Audited]
    public class DocumentType : FullAuditedEntity<long>
    {
        [NotNull]
        public string Code { get; set; }

        [NotNull]
        public string Name { get; set; }
    }
}
