using System;
using Volo.Abp.Application.Services;

namespace MasterData.Companies
{
    public interface ICompanyAppService : ICrudAppService<CompanyDto, Guid, GetCompaniesInput, CompanyCreateDto, CompanyUpdateDto>
    {
    }
}
