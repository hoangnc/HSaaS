using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace MasterData.Departments
{
    public class DepartmentAppService : MasterDataAppService, IDepartmentAppService
    {
        protected IDepartmentRepository DepartmentRepository { get; }

        public DepartmentAppService(
            IDepartmentRepository departmentRepository
            )
        {
            DepartmentRepository = departmentRepository;
        }
        public virtual async Task<DepartmentDto> CreateAsync(DepartmentCreateDto input)
        {
            var department = ObjectMapper.Map<DepartmentCreateDto, Department>(input);

            department = await DepartmentRepository.InsertAsync(department);

            return ObjectMapper.Map<Department, DepartmentDto>(department);
        }

        public virtual async Task DeleteAsync(long id)
        {
            var department = await DepartmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return;
            }

            await DepartmentRepository.DeleteAsync(department);
        }

        public async Task<DepartmentDto> GetAsync(long id)
        {
            return ObjectMapper.Map<Department, DepartmentDto>(
                await DepartmentRepository.GetByIdAsync(id)
            );
        }

        public virtual async Task<PagedResultDto<DepartmentDto>> GetListAsync(GetDepartmentsInput input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Department.Name);
            }

            var count = await DepartmentRepository.GetCountAsync();

            var departments = await DepartmentRepository.GetPagedListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
            );

            return new PagedResultDto<DepartmentDto>(
                count,
                ObjectMapper.Map<List<Department>, List<DepartmentDto>>(departments)
            ); ;
        }

        public async Task<DepartmentDto> UpdateAsync(long id, DepartmentUpdateDto input)
        {
            var department = await DepartmentRepository.GetByIdAsync(id);
            department.Name = input.Name;
            department.EmailAddress = input.EmailAddress;

            return ObjectMapper.Map<Department, DepartmentDto>(department);
        }
    }
}
