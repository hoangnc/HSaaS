using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MasterData.UserDepartments
{
    public interface IUserDepartmentRepository : IBasicRepository<UserDepartment, Guid>
    {
        Task<UserDepartment> GetByUserNameAsync(string userName);
    }
}
