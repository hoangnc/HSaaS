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
            CreateMap<CreateCompanyModalModel.CompanyInfoViewModel, CompanyCreateDto>()
                 .ForMember(c => c.Id, options => options.Ignore())
                .ForMember(c => c.IsDeleted, options => options.Ignore())
                .ForMember(c => c.DeleterId, options => options.Ignore())
                .ForMember(c => c.DeletionTime, options => options.Ignore())
                .ForMember(c => c.LastModificationTime, options => options.Ignore())
                .ForMember(c => c.LastModifierId, options => options.Ignore())
                .ForMember(c => c.CreationTime, options => options.Ignore())
                .ForMember(c => c.CreatorId, options => options.Ignore());

            CreateMap<EditCompanyModalModel.CompanyInfoViewModel, CompanyUpdateDto>()
                .ForMember(c => c.IsDeleted, options => options.Ignore())
                .ForMember(c => c.DeleterId, options => options.Ignore())
                .ForMember(c => c.DeletionTime, options => options.Ignore())
                .ForMember(c => c.LastModificationTime, options => options.Ignore())
                .ForMember(c => c.LastModifierId, options => options.Ignore())
                .ForMember(c => c.CreationTime, options => options.Ignore())
                .ForMember(c => c.CreatorId, options => options.Ignore());

            CreateMap<CompanyDto, EditCompanyModalModel.CompanyInfoViewModel>()
                .ForMember(c => c.ExtraProperties, options => options.Ignore())
                .ForMember(c => c.ConcurrencyStamp, options => options.Ignore());
        }

        protected virtual void DepartmentMappings()
        {
            CreateMap<CreateDepartmentModalModel.DepartmentInfoViewModel, DepartmentCreateDto>()
                .ForMember(c => c.Id, options => options.Ignore())
                .ForMember(c => c.IsDeleted, options => options.Ignore())
                .ForMember(c => c.DeleterId, options => options.Ignore())
                .ForMember(c => c.DeletionTime, options => options.Ignore())
                .ForMember(c => c.LastModificationTime, options => options.Ignore())
                .ForMember(c => c.LastModifierId, options => options.Ignore())
                .ForMember(c => c.CreationTime, options => options.Ignore())
                .ForMember(c => c.CreatorId, options => options.Ignore());

            CreateMap<EditDepartmentModalModel.DepartmentInfoViewModel, DepartmentUpdateDto>()
                .ForMember(c => c.IsDeleted, options => options.Ignore())
                .ForMember(c => c.DeleterId, options => options.Ignore())
                .ForMember(c => c.DeletionTime, options => options.Ignore())
                .ForMember(c => c.LastModificationTime, options => options.Ignore())
                .ForMember(c => c.LastModifierId, options => options.Ignore())
                .ForMember(c => c.CreationTime, options => options.Ignore())
                .ForMember(c => c.CreatorId, options => options.Ignore());

            CreateMap<DepartmentDto, EditDepartmentModalModel.DepartmentInfoViewModel>()
                .ForMember(c => c.ExtraProperties, options => options.Ignore())
                .ForMember(c => c.ConcurrencyStamp, options => options.Ignore());
        }

        protected virtual void DocumentTypeMappings()
        {
            CreateMap<CreateDocumentTypeModalModel.DocumentTypeInfoViewModel, DocumentTypeCreateDto>()
                .ForMember(c => c.Id, options => options.Ignore())
                .ForMember(c => c.IsDeleted, options => options.Ignore())
                .ForMember(c => c.DeleterId, options => options.Ignore())
                .ForMember(c => c.DeletionTime, options => options.Ignore())
                .ForMember(c => c.LastModificationTime, options => options.Ignore())
                .ForMember(c => c.LastModifierId, options => options.Ignore())
                .ForMember(c => c.CreationTime, options => options.Ignore())
                .ForMember(c => c.CreatorId, options => options.Ignore());

            CreateMap<EditDocumentTypeModalModel.DocumentTypeInfoViewModel, DocumentTypeUpdateDto>()
                .ForMember(c => c.IsDeleted, options => options.Ignore())
                .ForMember(c => c.DeleterId, options => options.Ignore())
                .ForMember(c => c.DeletionTime, options => options.Ignore())
                .ForMember(c => c.LastModificationTime, options => options.Ignore())
                .ForMember(c => c.LastModifierId, options => options.Ignore())
                .ForMember(c => c.CreationTime, options => options.Ignore())
                .ForMember(c => c.CreatorId, options => options.Ignore());

            CreateMap<DocumentTypeDto, EditDocumentTypeModalModel.DocumentTypeInfoViewModel>()
                .ForMember(c => c.ExtraProperties, options => options.Ignore())
                .ForMember(c => c.ConcurrencyStamp, options => options.Ignore());
        }

        protected virtual void ModuleMappings()
        {
            CreateMap<CreateModuleModalModel.ModuleInfoViewModel, ModuleCreateDto>()
                .ForMember(c => c.Id, options => options.Ignore())
                .ForMember(c => c.IsDeleted, options => options.Ignore())
                .ForMember(c => c.DeleterId, options => options.Ignore())
                .ForMember(c => c.DeletionTime, options => options.Ignore())
                .ForMember(c => c.LastModificationTime, options => options.Ignore())
                .ForMember(c => c.LastModifierId, options => options.Ignore())
                .ForMember(c => c.CreationTime, options => options.Ignore())
                .ForMember(c => c.CreatorId, options => options.Ignore());

            CreateMap<EditModuleModalModel.ModuleInfoViewModel, ModuleUpdateDto>()
                .ForMember(c => c.IsDeleted, options => options.Ignore())
                .ForMember(c => c.DeleterId, options => options.Ignore())
                .ForMember(c => c.DeletionTime, options => options.Ignore())
                .ForMember(c => c.LastModificationTime, options => options.Ignore())
                .ForMember(c => c.LastModifierId, options => options.Ignore())
                .ForMember(c => c.CreationTime, options => options.Ignore())
                .ForMember(c => c.CreatorId, options => options.Ignore());

            CreateMap<ModuleDto, EditModuleModalModel.ModuleInfoViewModel>()
                .ForMember(c => c.ExtraProperties, options => options.Ignore())
                .ForMember(c => c.ConcurrencyStamp, options => options.Ignore());
        }
    }
}