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

namespace MasterData.UserDepartments
{
    [RemoteService(Name = MasterDataRemoteServiceConsts.RemoteServiceName)]
    [Area("masterData")]
    [Route("api/master-data/userdepartments")]
    public class UserDepartmentController : MasterDataController, IUserDepartmentAppService
    {
        protected IUserDepartmentAppService UserDepartmentAppService { get; }

        public UserDepartmentController(IUserDepartmentAppService userDepartmentAppService)
        {
            UserDepartmentAppService = userDepartmentAppService;
        }

        [HttpGet]
        [Route("by-username/{userName}")]
        public async Task<UserDepartmentDto> GetByUserNameAsync(string userName)
        {
            return await UserDepartmentAppService.GetByUserNameAsync(userName);
        }


        [HttpGet]
        [Route("{id}")]
        public Task<UserDepartmentDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("getlist")]
        public async Task<PagedResultDto<UserDepartmentDto>> GetListAsync(GetUserDepartmentsInput input)
        {
            return await UserDepartmentAppService.GetListAsync(input);
        }

        [HttpPost]
        public async Task<UserDepartmentDto> CreateAsync(UserDepartmentCreateDto input)
        {
            return await UserDepartmentAppService.CreateAsync(input); 
        }

        [HttpPut]
        public Task<UserDepartmentDto> UpdateAsync(Guid id, UserDepartmentUpdateDto input)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
