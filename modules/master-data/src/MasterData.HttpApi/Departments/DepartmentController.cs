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
    public class DepartmentController : MasterDataController, IDepartmentAppService
    {
        protected IDepartmentAppService DepartmentAppService { get; }

        public DepartmentController(IDepartmentAppService departmentAppService)
        {
            DepartmentAppService = departmentAppService;
        }

        [HttpPost]
        public async Task<DepartmentDto> CreateAsync(DepartmentCreateDto input)
        {
            return await DepartmentAppService.CreateAsync(input);
        }

        [HttpDelete]
        public async Task DeleteAsync(long id)
        {
            await DepartmentAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<DepartmentDto> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<PagedResultDto<DepartmentDto>> GetListAsync(GetDepartmentsInput input)
        {
            return await DepartmentAppService.GetListAsync(input);
        }

        [HttpPut]
        public Task<DepartmentDto> UpdateAsync(long id, DepartmentUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}
