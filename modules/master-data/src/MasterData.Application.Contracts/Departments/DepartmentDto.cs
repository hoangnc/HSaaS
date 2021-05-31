using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MasterData.Departments
{
    public class DepartmentDto : FullAuditedEntityDto<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
    }
}
