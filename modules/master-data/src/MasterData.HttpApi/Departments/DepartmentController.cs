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

namespace MasterData.Departments
{
    [RemoteService(Name = MasterDataRemoteServiceConsts.RemoteServiceName)]
    [Area("masterData")]
    [Route("api/master-data/departments")]
    [Authorize]
    public class DepartmentController : MasterDataController, IDepartmentAppService
    {
        protected IDepartmentAppService DepartmentAppService { get; }

        public DepartmentController(IDepartmentAppService departmentAppService)
        {
            DepartmentAppService = departmentAppService;
        }

        [HttpPost]
        [Authorize(MasterDataPermissions.Departments.Create)]
        public async Task<DepartmentDto> CreateAsync(DepartmentCreateDto input)
        {
            return await DepartmentAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Authorize(MasterDataPermissions.Departments.Delete)]
        public async Task DeleteAsync(long id)
        {
            await DepartmentAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(MasterDataPermissions.Departments.Default)]
        public Task<DepartmentDto> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize(MasterDataPermissions.Departments.Default)]
        public async Task<PagedResultDto<DepartmentDto>> GetListAsync(GetDepartmentsInput input)
        {
            return await DepartmentAppService.GetListAsync(input);
        }

        [HttpPut]
        [Authorize(MasterDataPermissions.Departments.Update)]
        public Task<DepartmentDto> UpdateAsync(long id, DepartmentUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}
