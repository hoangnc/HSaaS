using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MasterData.UserDepartments
{
    public class GetUserDepartmentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
