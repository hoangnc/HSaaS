﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MasterData.UserDepartments
{
    public class UserDepartmentAppService : MasterDataAppService, IUserDepartmentAppService
    {
        protected IUserDepartmentRepository UserDepartmentRepository { get; }

        public UserDepartmentAppService(
            IUserDepartmentRepository userDepartmentRepository
            )
        {
            UserDepartmentRepository = userDepartmentRepository;
        }

        public Task<UserDepartmentDto> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResultDto<UserDepartmentDto>> GetListAsync(GetUserDepartmentsInput input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(UserDepartment.UserName);
            }

            var count = await UserDepartmentRepository.GetCountAsync();

            var modules = await UserDepartmentRepository.GetPagedListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
            );

            return new PagedResultDto<UserDepartmentDto>(
                count,
                ObjectMapper.Map<List<UserDepartment>, List<UserDepartmentDto>>(modules)
            );
        }

        public async Task<UserDepartmentDto> CreateAsync(UserDepartmentCreateDto input)
        {
            var userDepartment = ObjectMapper.Map<UserDepartmentCreateDto, UserDepartment>(input);

            var userDepartmentExist = await UserDepartmentRepository.GetByUserNameAsync(input.UserName);

            if (userDepartmentExist?.Id > 0)
            {
                throw new BusinessException(code: MasterDataErrorCodes.UserDepartment.UserNameExists)
                                .WithData("UserName", input.UserName);
            }

            userDepartment = await UserDepartmentRepository.InsertAsync(userDepartment);

            return ObjectMapper.Map<UserDepartment, UserDepartmentDto>(userDepartment);
        }

        public async Task<UserDepartmentDto> UpdateAsync(long id, UserDepartmentUpdateDto input)
        {
            var userDepartment = await UserDepartmentRepository.GetByUserNameAsync(input.UserName);

            if (userDepartment != null
                && userDepartment.Id > 0)
            {
                userDepartment.UserName = input.UserName;
                userDepartment.DepartmentCode = input.DepartmentCode;
            }
            else
            {
                userDepartment = ObjectMapper.Map<UserDepartmentUpdateDto, UserDepartment>(input);
                userDepartment = await UserDepartmentRepository.InsertAsync(userDepartment);
            }

            return ObjectMapper.Map<UserDepartment, UserDepartmentDto>(userDepartment);
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDepartmentDto> GetByUserNameAsync(string userName)
        {
            return ObjectMapper.Map<UserDepartment, UserDepartmentDto>(
               await UserDepartmentRepository.GetByUserNameAsync(userName)
           );
        }
    }
}
