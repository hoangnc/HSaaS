using System;
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
    public class DocumentManagementDbContext : AbpDbContext<DocumentManagementDbContext>, IDocumentManagementDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<Document> Documents { get; set; }
        public DbSet<Appendix> Appendixes { get; set; }

        public DocumentManagementDbContext(DbContextOptions<DocumentManagementDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureDocumentManagement();


           /* builder.HasDbFunction(typeof(DocumentManagementDbContext)
                .GetMethod(nameof(StringSplit), new[] { typeof(string), typeof(string) }));*/
        }

        public IQueryable<SplitInformation> StringSplit(string text, string delimiter) => FromExpression(() => StringSplit(text, delimiter));
    }
}