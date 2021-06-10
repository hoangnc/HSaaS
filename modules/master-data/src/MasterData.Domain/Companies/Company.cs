using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace MasterData.Companies
{
    [Audited]
    public class Company : FullAuditedEntity<Guid>
    {
        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string EmailAddress { get; set; }

        protected Company()
        {

        }

        public Company(
            Guid id
            )
        {
            Id = id;
        }
    }
}
