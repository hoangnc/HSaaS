using AutoMapper;
using MasterData.Companies;
using MasterData.Departments;
using CreateCompanyModalModel = MasterData.Web.Pages.MasterData.Companies.CreateModalModel;
using EditCompanyModalModel = MasterData.Web.Pages.MasterData.Companies.EditModalModel;
using CreateDepartmentModalModel = MasterData.Web.Pages.MasterData.Departments.CreateModalModel;
using EditDepartmentModalModel = MasterData.Web.Pages.MasterData.Departments.EditModalModel;
using CreateDocumentTypeModalModel = MasterData.Web.Pages.MasterData.DocumentTypes.CreateModalModel;
using EditDocumentTypeModalModel = MasterData.Web.Pages.MasterData.DocumentTypes.EditModalModel;
using CreateModuleModalModel = MasterData.Web.Pages.MasterData.Modules.CreateModalModel;
using EditModuleModalModel = MasterData.Web.Pages.MasterData.Modules.EditModalModel;
using MasterData.DocumentTypes;
using MasterData.Modules;

namespace MasterData.Web
{
    public class MasterDataWebAutoMapperProfile : Profile
    {
        public MasterDataWebAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CompanyMappings();
            DepartmentMappings();
            DocumentTypeMappings();
            ModuleMappings();
        }

        protected virtual void CompanyMappings()
        {
            CreateMap<CreateCompanyModalModel.CompanyInfoViewModel, CompanyCreateDto>();
            CreateMap<EditCompanyModalModel.CompanyInfoViewModel, CompanyUpdateDto>();

            CreateMap<CompanyDto, EditCompanyModalModel.CompanyInfoViewModel>()
                .ForMember(c => c.ExtraProperties, options => options.Ignore())
                .ForMember(c => c.ConcurrencyStamp, options => options.Ignore());
        }

        protected virtual void DepartmentMappings()
        {
            CreateMap<CreateDepartmentModalModel.DepartmentInfoViewModel, DepartmentCreateDto>();
            CreateMap<EditDepartmentModalModel.DepartmentInfoViewModel, DepartmentUpdateDto>();

            CreateMap<DepartmentDto, EditDepartmentModalModel.DepartmentInfoViewModel>()
                .ForMember(c => c.ExtraProperties, options => options.Ignore())
                .ForMember(c => c.ConcurrencyStamp, options => options.Ignore());
        }

        protected virtual void DocumentTypeMappings()
        {
            CreateMap<CreateDocumentTypeModalModel.DocumentTypeInfoViewModel, DocumentTypeCreateDto>();
            CreateMap<EditDocumentTypeModalModel.DocumentTypeInfoViewModel, DocumentTypeUpdateDto>();

            CreateMap<DocumentTypeDto, EditDocumentTypeModalModel.DocumentTypeInfoViewModel>()
                .ForMember(c => c.ExtraProperties, options => options.Ignore())
                .ForMember(c => c.ConcurrencyStamp, options => options.Ignore());
        }

        protected virtual void ModuleMappings()
        {
            CreateMap<CreateModuleModalModel.ModuleInfoViewModel, ModuleCreateDto>();
            CreateMap<EditModuleModalModel.ModuleInfoViewModel, ModuleUpdateDto>();

            CreateMap<ModuleDto, EditModuleModalModel.ModuleInfoViewModel>()
                .ForMember(c => c.ExtraProperties, options => options.Ignore())
                .ForMember(c => c.ConcurrencyStamp, options => options.Ignore());
        }
    }
}