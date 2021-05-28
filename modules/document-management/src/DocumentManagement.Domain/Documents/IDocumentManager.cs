using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace DocumentManagement.Documents
{
    public interface IDocumentManager : IDomainService
    {
        Task<Document> FindByNameAsync(string name);
        Task<Document> CreateAsync(Document document);
    }
}
