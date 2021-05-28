using AutoMapper;
using DocumentManagement.Appendixes;
using DocumentManagement.Documents;

namespace DocumentManagement
{
    public class DocumentManagementApplicationAutoMapperProfile : Profile
    {
        public DocumentManagementApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<Appendix, AppendixDto>();
            CreateMap<AppendixDto, Appendix>();

            CreateMap<Document, DocumentDto>()
                // .ForMember(d => d.Appendixes, options => options.Ignore())
                .ForMember(d => d.AppendixFiles, options => options.Ignore())
                .ForMember(d => d.Files, options => options.Ignore())
                .ForMember(d => d.ReplaceForName, options => options.Ignore())
                .ForMember(d => d.RelateToDocumentNames, options => options.Ignore());

            CreateMap<DocumentDto, Document>()
                  .ForMember(d => d.Appendixes, options => options.Ignore());

            CreateMap<CreateDocumentDto, Document>()
                 .ForMember(d => d.Appendixes, options => options.Ignore());

            CreateMap<UpdateDocumentDto, Document>()
                 .ForMember(d => d.Appendixes, options => options.Ignore());

            CreateMap<ReviewDocumentDto, Document>()
                 .ForMember(d => d.Appendixes, options => options.Ignore());
        }
    }
}