using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DocumentManagement.Documents
{
    public interface IDocumentAppService : ICrudAppService<DocumentDto, long, GetDocumentsInput, CreateDocumentDto, UpdateDocumentDto>
    {
        Task<PagedResultDto<DocumentDto>> FilteredPagedListByDocumentTypeAsync(GetDocumentsByDocumentTypeInput input);
        Task<PagedResultDto<DocumentDto>> FilteredPagedListByDepartmentCodeAsync(GetDocumentsByDepartmentCodeInput input);
        Task<DocumentDto> FindByNameAsync(string name);
        Task<DocumentDto> GetByCodeAsync(string code);
        Task<PagedResultDto<DocumentDto>> FilteredPagedListAsync(GetDocumentsInput input);
        Task DeleteFileByDocumentIdAndFileNameAsync(DeleteFileByDocumentIdAndFileNameInput input);
        Task<DocumentDto> ReviewAsync(ReviewDocumentDto input);
    }
}
