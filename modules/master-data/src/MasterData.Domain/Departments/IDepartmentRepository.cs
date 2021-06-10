using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MasterData.Departments
{
    public interface IDepartmentRepository : IBasicRepository<Department>
    {
        Task<Department> GetByIdAsync(Guid id);
    }
}
