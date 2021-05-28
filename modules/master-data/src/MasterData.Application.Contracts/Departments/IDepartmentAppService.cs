using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace MasterData.Departments
{
    public interface IDepartmentAppService : ICrudAppService<DepartmentDto, long, GetDepartmentsInput, DepartmentCreateDto, DepartmentUpdateDto>
    {
    }
}
