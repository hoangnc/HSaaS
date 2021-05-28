using AutoMapper;
using Volo.Abp.AutoMapper;
using DocumentManagement.Web.Pages.DocumentManagement.Documents;
using EditDocumentModel = DocumentManagement.Web.Pages.DocumentManagement.Documents.EditModel;
using DetailDocumentModel = DocumentManagement.Web.Pages.DocumentManagement.Documents.DetailModel;
using DocumentManagement.Documents;

namespace DocumentManagement.Web
{
    public class DocumentManagementWebAutoMapperProfile : Profile
    {
        public DocumentManagementWebAutoMapperProfile()
        {
            DocumentMappings();
        }

        protected virtual void DocumentMappings()
        {
            // CreateMap<CreateCompanyModalModel.CompanyInfoViewModel, CompanyCreateDto>();
            CreateMap<EditDocumentModel.DocumentInfoViewModel, DocumentDto>()
                .ForMember(c => c.Appendixes, options => options.Ignore())
                .ForMember(c => c.Files, options => options.Ignore())
                .ForMember(c => c.AppendixFiles, options => options.Ignore())
                .ForMember(c => c.IsDeleted, options => options.Ignore())
                .ForMember(c => c.DeleterId, options => options.Ignore())
                .ForMember(c => c.DeletionTime, options => options.Ignore())
                .ForMember(c => c.LastModificationTime, options => options.Ignore())
                .ForMember(c => c.LastModifierId, options => options.Ignore())
                .ForMember(c => c.CreationTime, options => options.Ignore())
                .ForMember(c => c.ReplaceForName, options => options.Ignore())
                .ForMember(c => c.CreatorId, options => options.Ignore());

            CreateMap<DocumentDto, EditDocumentModel.DocumentInfoViewModel>()
                .ForMember(c => c.ExtraProperties, options => options.Ignore())
                .ForMember(c => c.ConcurrencyStamp, options => options.Ignore());

            CreateMap<DocumentDto, DetailDocumentModel.DocumentInfoViewModel>()
                .ForMember(c => c.ExtraProperties, options => options.Ignore())
                .ForMember(c => c.ConcurrencyStamp, options => options.Ignore());
        }
    }
}
