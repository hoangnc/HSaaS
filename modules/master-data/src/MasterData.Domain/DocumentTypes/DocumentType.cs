using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace MasterData.DocumentTypes
{
    [Audited]
    public class DocumentType : Entity<long>
    {
        [NotNull]
        public string Code { get; set; }

        [NotNull]
        public string Name { get; set; }
    }
}
