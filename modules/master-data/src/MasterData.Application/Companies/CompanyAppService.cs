using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MasterData.Companies
{
    public class CompanyAppService : MasterDataAppService, ICompanyAppService
    {
        protected ICompanyRepository CompanyRepository { get; }

        public CompanyAppService(
            ICompanyRepository companyRepository
            )
        {
            CompanyRepository = companyRepository;
        }
        public virtual async Task<CompanyDto> CreateAsync(CompanyCreateDto input)
        {
            var company = ObjectMapper.Map<CompanyCreateDto, Company>(input);

            company = await CompanyRepository.InsertAsync(company);

            return ObjectMapper.Map<Company, CompanyDto>(company);
        }

        public virtual async Task DeleteAsync(long id)
        {
            var company = await CompanyRepository.GetByIdAsync(id);
            if (company == null)
            {
                return;
            }

            await CompanyRepository.DeleteAsync(company);
        }

        public async Task<CompanyDto> GetAsync(long id)
        {
            return ObjectMapper.Map<Company, CompanyDto>(
                await CompanyRepository.GetByIdAsync(id)
            );
        }

        public virtual async Task<PagedResultDto<CompanyDto>> GetListAsync(GetCompaniesInput input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Company.Name);
            }

            var count = await CompanyRepository.GetCountAsync();

            var companies = await CompanyRepository.GetPagedListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting            
            );

            return new PagedResultDto<CompanyDto>(
                count,
                ObjectMapper.Map<List<Company>, List<CompanyDto>>(companies)
            ); ;
        }

        public async Task<CompanyDto> UpdateAsync(long id, CompanyUpdateDto input)
        {
            var company = await CompanyRepository.GetByIdAsync(id);

            company.Name = input.Name;

            return ObjectMapper.Map<Company, CompanyDto>(company);
        }
    }
}
