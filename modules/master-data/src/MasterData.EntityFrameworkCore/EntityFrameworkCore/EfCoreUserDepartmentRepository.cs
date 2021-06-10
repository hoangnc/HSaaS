using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterData.UserDepartments;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MasterData.EntityFrameworkCore
{
    public class EfCoreUserDepartmentRepository : EfCoreRepository<IMasterDataDbContext, UserDepartment, Guid>,
        IUserDepartmentRepository
    {
        public EfCoreUserDepartmentRepository(IDbContextProvider<IMasterDataDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<UserDepartment> GetByUserNameAsync(string userName)
        {
            return await (await GetDbSetAsync())
             .FirstOrDefaultAsync(c => c.UserName == userName);
        }
    }
}
