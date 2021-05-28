using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace MasterData.Companies
{
    [Audited]
    public class Company : Entity<long>
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
            long id
            )
        {
            Id = id;
        }
    }
}
