using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DocumentManagement.Documents;
using System.Threading;

namespace DocumentManagement.EntityFrameworkCore
{
    public class EfCoreDocumentRepository : EfCoreRepository<IDocumentManagementDbContext, Document, long>,
        IDocumentRepository
    {
        public EfCoreDocumentRepository(IDbContextProvider<IDocumentManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Document>> FilteredPagedListByDepartmentCodeAsync(string filter, string departmentCode, string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet
                .Where(d => d.DepartmentCode == departmentCode)
                .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.Code.Contains(filter)
                || x.Name.Contains(filter)
                || x.Description.Contains(filter)
                || x.ContentChange.Contains(filter))
                .Skip(skipCount)
                .OrderByDescending(d => d.CreationTime)
                .Take(maxResultCount)
                .Include(d => d.Appendixes)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Document>> FilteredPagedListAsync(string filter, string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            var dbContext = await GetDbContextAsync();

            return await dbSet
                .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.Code.Contains(filter)
                || x.Name.Contains(filter)
                || x.Description.Contains(filter)
                || x.ContentChange.Contains(filter))
                .Skip(skipCount)
                .OrderByDescending(d => d.CreationTime)
                .Take(maxResultCount)
                .Include(d => d.Appendixes)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Document>> FilteredPagedListByDocumentTypeAsync(string filter, string documentType, string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet
                .Where(d => d.DocumentType == documentType)
                .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.Code.Contains(filter)
                || x.Name.Contains(filter)
                || x.Description.Contains(filter)
                || x.ContentChange.Contains(filter))
                .Skip(skipCount)
                .OrderByDescending(d => d.CreationTime)
                .Take(maxResultCount)
                .Include(d => d.Appendixes)
                .ToListAsync(cancellationToken);
        }

        public async Task<Document> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet.FirstOrDefaultAsync(d => d.Name == name);
        }

        public async Task<Document> GetByCodeAsync(string code)
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet.Where(d => d.Code == code)
                .Include(d => d.Appendixes)
                .FirstOrDefaultAsync();
        }

        public async Task<Document> CheckExistDocumentForCreateAsync(Document document)
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet.Where(d =>
               d.CompanyCode == document.CompanyCode
               && d.DepartmentCode == document.DepartmentCode
               && d.Description == document.Description
               && d.DocumentNumber == document.DocumentNumber
               && d.DocumentType == document.DocumentType
               && d.EffectiveDate == document.EffectiveDate
               && d.Module == document.Module
               && d.Name == document.Name
               && d.RelateToDocuments == document.RelateToDocuments
               && d.ReviewDate == document.ReviewDate
               && d.ReviewNumber == document.ReviewNumber
               && d.AppliedToEntire == document.AppliedToEntire
               && d.IssuedToEntire == document.IssuedToEntire
            )
            .FirstOrDefaultAsync();
        }

        public async Task<Document> CheckExistDocumentForReviewAsync(Document document)
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet.Where(d =>
                d.CompanyCode == document.CompanyCode
               && d.DepartmentCode == document.DepartmentCode
               && d.Description == document.Description
               && d.DocumentNumber == document.DocumentNumber
               && d.DocumentType == document.DocumentType
               && d.EffectiveDate == document.EffectiveDate
               && d.FileName == document.FileName
               && d.Module == document.Module
               && d.Name == document.Name
               && d.RelateToDocuments == document.RelateToDocuments
               && d.ReviewDate == document.ReviewDate
               && d.ReviewNumber == document.ReviewNumber
               && d.AppliedToEntire == document.AppliedToEntire
               && d.IssuedToEntire == document.IssuedToEntire
               && d.ReplaceFor == document.ReplaceFor
            )
            .FirstOrDefaultAsync();
        }
    }
}
