using System;
using System.Threading.Tasks;
using MasterData.DocumentTypes;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MasterData.EntityFrameworkCore
{
    public class EfCoreDocumentTypeRepository : EfCoreRepository<IMasterDataDbContext, DocumentType, Guid>,
        IDocumentTypeRepository
    {
        public EfCoreDocumentTypeRepository(IDbContextProvider<IMasterDataDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<DocumentType> GetByIdAsync(Guid id)
        {
            return await (await GetDbSetAsync())
               .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<DocumentType> GetByCodeAsync(string code)
        {
            return await(await GetDbSetAsync())
              .FirstOrDefaultAsync(c => c.Code == code);
        }
    }
}
