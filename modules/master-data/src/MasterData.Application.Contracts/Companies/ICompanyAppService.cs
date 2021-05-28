using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace MasterData.Companies
{
    public interface ICompanyAppService : ICrudAppService<CompanyDto, long, GetCompaniesInput, CompanyCreateDto, CompanyUpdateDto>
    {
    }
}
