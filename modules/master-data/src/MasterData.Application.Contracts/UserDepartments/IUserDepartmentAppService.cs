using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MasterData.UserDepartments
{
    public interface IUserDepartmentAppService : ICrudAppService<UserDepartmentDto, long, GetUserDepartmentsInput, UserDepartmentCreateDto, UserDepartmentUpdateDto>
    {
        Task<UserDepartmentDto> GetByUserNameAsync(string userName);
    }
}

