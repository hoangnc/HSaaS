using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MasterData.Departments
{
    public class GetDepartmentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
