using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Appendixes;
using Volo.Abp.Domain.Services;

namespace DocumentManagement.Documents
{
    public class DocumentManager : DomainService, IDocumentManager
    {
        protected IDocumentRepository DocumentRepository { get; }
        protected IAppendixRepository AppendixRepository { get; }

        public DocumentManager(IDocumentRepository documentRepository,
            IAppendixRepository appendixRepository)
        {
            DocumentRepository = documentRepository;
            AppendixRepository = appendixRepository;
        }

        public async Task<Document> FindByNameAsync(string name)
        {
            return await DocumentRepository.FindByNameAsync(name);
        }

        public Task<Document> CreateAsync(Document document)
        {
            throw new NotImplementedException();
        }
    }
}
