using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MasterData.DocumentTypes
{
    public class DocumentTypeAppService : MasterDataAppService, IDocumentTypeAppService
    {
        protected IDocumentTypeRepository DocumentTypeRepository { get; }

        public DocumentTypeAppService(
            IDocumentTypeRepository documentTypeRepository
            )
        {
            DocumentTypeRepository = documentTypeRepository;
        }
        public virtual async Task<DocumentTypeDto> CreateAsync(DocumentTypeCreateDto input)
        {
            var documentType = ObjectMapper.Map<DocumentTypeCreateDto, DocumentType>(input);

            var documentTypeExist = await DocumentTypeRepository.GetByCodeAsync(input.Code);

            if (documentTypeExist?.Id > 0)
            {
                throw new BusinessException(code: MasterDataErrorCodes.DocumentType.CodeExists)
                                .WithData("Code", input.Code);
            }

            documentType = await DocumentTypeRepository.InsertAsync(documentType);

            return ObjectMapper.Map<DocumentType, DocumentTypeDto>(documentType);
        }

        public virtual async Task DeleteAsync(long id)
        {
            var documentType = await DocumentTypeRepository.GetByIdAsync(id);
            if (documentType == null)
            {
                return;
            }

            await DocumentTypeRepository.DeleteAsync(documentType);
        }

        public async Task<DocumentTypeDto> GetAsync(long id)
        {
            return ObjectMapper.Map<DocumentType, DocumentTypeDto>(
                await DocumentTypeRepository.GetByIdAsync(id)
            );
        }

        public virtual async Task<PagedResultDto<DocumentTypeDto>> GetListAsync(GetDocumentTypesInput input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(DocumentType.Name);
            }

            var count = await DocumentTypeRepository.GetCountAsync();

            var documentTypes = await DocumentTypeRepository.GetPagedListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
            );

            return new PagedResultDto<DocumentTypeDto>(
                count,
                ObjectMapper.Map<List<DocumentType>, List<DocumentTypeDto>>(documentTypes)
            );
        }

        public async Task<DocumentTypeDto> UpdateAsync(long id, DocumentTypeUpdateDto input)
        {
            var documentType = await DocumentTypeRepository.GetByIdAsync(id);

            documentType.Name = input.Name;

            return ObjectMapper.Map<DocumentType, DocumentTypeDto>(documentType);
        }
    }
}
