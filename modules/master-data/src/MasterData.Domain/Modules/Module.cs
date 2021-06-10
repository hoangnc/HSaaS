using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace MasterData.Modules
{
    [Audited]
    public class Module : FullAuditedEntity<Guid>
    {
        [NotNull]
        public string Code { get; set; }

        [NotNull]
        public string Name { get; set; }
    }
}
