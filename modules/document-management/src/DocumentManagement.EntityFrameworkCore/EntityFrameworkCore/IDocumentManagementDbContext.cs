using System.Collections.Generic;
using System.Linq;
using DocumentManagement.Appendixes;
using DocumentManagement.Documents;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DocumentManagement.EntityFrameworkCore
{
    [ConnectionStringName(DocumentManagementDbProperties.ConnectionStringName)]
    public interface IDocumentManagementDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */

        DbSet<Document> Documents { get; }
        DbSet<Appendix> Appendixes { get; }

        IQueryable<SplitInformation> StringSplit(string text, string delimiter);
    }
}