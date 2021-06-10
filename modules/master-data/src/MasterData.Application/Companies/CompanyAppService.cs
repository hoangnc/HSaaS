using MasterData.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace MasterData.Companies
{
    [Authorize(MasterDataPermissions.Companies.Default)]
    public class CompanyAppService : MasterDataAppService, ICompanyAppService
    {
        protected ICompanyRepository CompanyRepository { get; }

        public CompanyAppService(
            ICompanyRepository companyRepository
            )
        {
            CompanyRepository = companyRepository;
        }

        [Authorize(MasterDataPermissions.Companies.Create)]
        public virtual async Task<CompanyDto> CreateAsync(CompanyCreateDto input)
        {
            var company = ObjectMapper.Map<CompanyCreateDto, Company>(input);

            company = await CompanyRepository.InsertAsync(company);

            return ObjectMapper.Map<Company, CompanyDto>(company);
        }

        [Authorize(MasterDataPermissions.Companies.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var company = await CompanyRepository.GetByIdAsync(id);
            if (company == null)
            {
                return;
            }

            await CompanyRepository.DeleteAsync(company);
        }

        public async Task<CompanyDto> GetAsync(Guid id)
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

        [Authorize(MasterDataPermissions.Companies.Update)]
        public async Task<CompanyDto> UpdateAsync(Guid id, CompanyUpdateDto input)
        {
            var company = await CompanyRepository.GetByIdAsync(id);

            company.Name = input.Name;

            return ObjectMapper.Map<Company, CompanyDto>(company);
        }
    }
}
