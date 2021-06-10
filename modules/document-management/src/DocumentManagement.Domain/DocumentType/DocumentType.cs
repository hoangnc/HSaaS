using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace DocumentManagement.DocumentType
{
    public class DocumentType : Entity<Guid>
    {
        [NotNull]
        public string Code { get; set; }
        [NotNull]
        public string Name { get; set; }
    }
}
