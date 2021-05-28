using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace MasterData.UserDepartments
{
    public class UserDepartment : Entity<long>
    {
        [NotNull]
        public string UserName { get; set; }

        [NotNull]
        public string DepartmentCode { get; set; }
    }
}
