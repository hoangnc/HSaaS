using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterData.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MasterData.Companies
{
    [RemoteService(Name = MasterDataRemoteServiceConsts.RemoteServiceName)]
    [Area("masterData")]
    [Route("api/master-data/companies")]
    public class CompanyController : MasterDataController, ICompanyAppService
    {
        protected ICompanyAppService CompanyAppService { get; }

        public CompanyController(ICompanyAppService companyAppService)
        {
            CompanyAppService = companyAppService;
        }

        [HttpPost]
        public async Task<CompanyDto> CreateAsync(CompanyCreateDto input)
        {
            return await CompanyAppService.CreateAsync(input);
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await CompanyAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<CompanyDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<PagedResultDto<CompanyDto>> GetListAsync(GetCompaniesInput input)
        {
            return await CompanyAppService.GetListAsync(input);
        }

        [HttpPut]
        public Task<CompanyDto> UpdateAsync(Guid id, CompanyUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}
