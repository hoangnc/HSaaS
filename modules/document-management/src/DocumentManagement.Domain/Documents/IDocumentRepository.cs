using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DocumentManagement.Documents
{
    public interface IDocumentRepository : IBasicRepository<Document, long>
    {
        Task<List<Document>> FilteredPagedListByDocumentTypeAsync(string filter, string documentType, string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default);
        Task<List<Document>> FilteredPagedListByDepartmentCodeAsync(string filter, string departmentCode, string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default);
        Task<Document> FindByNameAsync(string name);
        Task<Document> GetByCodeAsync(string code);
        Task<List<Document>> FilteredPagedListAsync(string filter, string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default);
        Task<Document> CheckExistDocumentForCreateAsync(Document document);
        Task<Document> CheckExistDocumentForReviewAsync(Document document);
    }
}
