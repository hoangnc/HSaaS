using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MasterData.Companies
{
    public interface ICompanyRepository : IBasicRepository<Company>
    {
        Task<Company> GetByIdAsync(long id);
    }
}
