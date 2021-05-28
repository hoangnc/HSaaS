using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MasterData.DocumentTypes
{
    public interface IDocumentTypeRepository : IBasicRepository<DocumentType>
    {
        Task<DocumentType> GetByIdAsync(long id);
        Task<DocumentType> GetByCodeAsync(string code);
    }
}
