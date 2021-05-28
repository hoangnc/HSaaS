using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MasterData.Departments;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MasterData.EntityFrameworkCore
{
    public class EfCoreDepartmentRepository : EfCoreRepository<IMasterDataDbContext, Department, long>,
        IDepartmentRepository
    {
        public EfCoreDepartmentRepository(IDbContextProvider<IMasterDataDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Department> GetByIdAsync(long id)
        {
            return await(await GetDbSetAsync())
               .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
