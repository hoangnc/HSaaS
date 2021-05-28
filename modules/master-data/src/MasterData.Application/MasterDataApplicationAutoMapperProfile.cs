using AutoMapper;
using MasterData.Companies;
using MasterData.Departments;
using MasterData.DocumentTypes;
using MasterData.Modules;
using MasterData.UserDepartments;

namespace MasterData
{
    public class MasterDataApplicationAutoMapperProfile : Profile
    {
        public MasterDataApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>();

            CreateMap<CompanyCreateDto, Company>().ForMember(c => c.Id, options => options.Ignore());
            CreateMap<CompanyUpdateDto, Company>();

            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();

            CreateMap<DepartmentCreateDto, Department>().ForMember(c => c.Id, options => options.Ignore());
            CreateMap<DepartmentUpdateDto, Department>();

            CreateMap<DocumentType, DocumentTypeDto>();
            CreateMap<DocumentTypeDto, DocumentType>();

            CreateMap<DocumentTypeCreateDto, DocumentType>().ForMember(c => c.Id, options => options.Ignore());
            CreateMap<DocumentTypeUpdateDto, DocumentType>();

            CreateMap<Module, ModuleDto>();
            CreateMap<ModuleDto, Module>();

            CreateMap<ModuleCreateDto, Module>().ForMember(c => c.Id, options => options.Ignore());
            CreateMap<ModuleUpdateDto, Module>();

            CreateMap<UserDepartment, UserDepartmentDto>();
            CreateMap<UserDepartmentDto, UserDepartment>();

            CreateMap<UserDepartmentCreateDto, UserDepartment>().ForMember(c => c.Id, options => options.Ignore());
            CreateMap<UserDepartmentUpdateDto, UserDepartment>();
        }
    }
}