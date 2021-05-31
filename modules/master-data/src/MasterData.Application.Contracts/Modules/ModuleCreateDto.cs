﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MasterData.Modules
{
    public class ModuleCreateDto : FullAuditedEntityDto<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
