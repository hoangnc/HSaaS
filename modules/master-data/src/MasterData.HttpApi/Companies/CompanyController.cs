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
    [Authorize]
    public class CompanyController : MasterDataController, ICompanyAppService
    {
        protected ICompanyAppService CompanyAppService { get; }

        public CompanyController(ICompanyAppService companyAppService)
        {
            CompanyAppService = companyAppService;
        }

        [HttpPost]
        [Authorize(MasterDataPermissions.Companies.Create)]
        public async Task<CompanyDto> CreateAsync(CompanyCreateDto input)
        {
            return await CompanyAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Authorize(MasterDataPermissions.Companies.Delete)]
        public async Task DeleteAsync(long id)
        {
            await CompanyAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(MasterDataPermissions.Companies.Default)]
        public Task<CompanyDto> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize(MasterDataPermissions.Companies.Default)]
        public async Task<PagedResultDto<CompanyDto>> GetListAsync(GetCompaniesInput input)
        {
            return await CompanyAppService.GetListAsync(input);
        }

        [HttpPut]
        [Authorize(MasterDataPermissions.Companies.Update)]
        public Task<CompanyDto> UpdateAsync(long id, CompanyUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}
