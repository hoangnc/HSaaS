﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MasterData.UserDepartments
{
    public class UserDepartmentUpdateDto : FullAuditedEntityDto<Guid>
    {
        public string UserName { get; set; }
        public string DepartmentCode { get; set; }
    }
}
