using System;
using DocumentManagement.Appendixes;
using DocumentManagement.Documents;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace DocumentManagement.EntityFrameworkCore
{
    public static class DocumentManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureDocumentManagement(
            this ModelBuilder builder,
            Action<DocumentManagementModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new DocumentManagementModelBuilderConfigurationOptions(
                DocumentManagementDbProperties.DbTablePrefix,
                DocumentManagementDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.Entity<Document>(b =>
            {
                b.ToTable(options.TablePrefix + "Documents", options.Schema);

                b.ConfigureByConvention();

                b.Property(x => x.Code).IsRequired();
                b.Property(x => x.CompanyCode).IsRequired();
                b.Property(x => x.CompanyName);
                b.Property(x => x.DepartmentCode).IsRequired();
                b.Property(x => x.DepartmentName);
                b.Property(x => x.Module).IsRequired();
                b.Property(x => x.DocumentType).IsRequired();
                b.Property(x => x.Name).HasMaxLength(DocumentConsts.MaxNameLength).IsRequired();
                b.Property(x => x.FileName).IsRequired();
                b.Property(x => x.DocumentNumber).IsRequired();
                b.Property(x => x.ReviewNumber);
                b.Property(x => x.Description);
                b.Property(x => x.ContentChange)
                 .HasComment("Content change");

                b.Property(x => x.Drafter).IsRequired();
                b.Property(x => x.Auditor).IsRequired();
                b.Property(x => x.Approver).IsRequired();

                b.Property(x => x.EffectiveDate);
                b.Property(x => x.ReviewDate);

                b.Property(x => x.AppliedToEntire).IsRequired();
                b.Property(x => x.IssuedToEntire).IsRequired();
                
                b.Property(x => x.ReplaceFor);
                b.Property(x => x.ReplaceEffectiveDate);
                b.Property(x => x.RelateToDocuments);
                b.Property(x => x.DDCAudited);
                b.Property(x => x.FolderName);
                b.Property(x => x.LinkFile);
                b.Property(x => x.StatusId);
                b.Property(x => x.FormType);
                b.Property(x => x.Active);
                b.Property(x => x.IssuedStatusId);

                b.HasIndex(x => new { x.Name, x.CompanyName, x.DepartmentName });

                b.HasMany(x => x.Appendixes).WithOne().HasForeignKey(a => a.DocumentId);
            });

            builder.Entity<Appendix>(b =>
            {
                b.ToTable(options.TablePrefix + "Appendixes", options.Schema);

                b.ConfigureByConvention();

                b.Property(x => x.Code).IsRequired();
                b.Property(x => x.CompanyCode).IsRequired();
                b.Property(x => x.CompanyName);
                b.Property(x => x.DepartmentCode).IsRequired();
                b.Property(x => x.DepartmentName);
                b.Property(x => x.Module).IsRequired();
                b.Property(x => x.DocumentType).IsRequired();
                b.Property(x => x.Name).HasMaxLength(DocumentConsts.MaxNameLength).IsRequired();
                b.Property(x => x.FileName).IsRequired();
                b.Property(x => x.AppendixNumber).IsRequired();
                b.Property(x => x.ReviewNumber);
                b.Property(x => x.Description);
                b.Property(x => x.ContentChange);

                b.Property(x => x.Drafter).IsRequired();
                b.Property(x => x.Auditor).IsRequired();
                b.Property(x => x.Approver).IsRequired();

                b.Property(x => x.EffectiveDate);
                b.Property(x => x.ReviewDate);

                b.Property(x => x.AppliedToEntire).IsRequired();
                b.Property(x => x.IssuedToEntire).IsRequired();

                b.Property(x => x.ReplaceFor);
                b.Property(x => x.ReplaceEffectiveDate);
                b.Property(x => x.RelateToDocuments);
                b.Property(x => x.DDCAudited);
                b.Property(x => x.FolderName);
                b.Property(x => x.LinkFile);
                b.Property(x => x.StatusId);
                b.Property(x => x.FormType);
                b.Property(x => x.Active);
                b.Property(x => x.IssuedStatusId);

                b.Property(x => x.DocumentId).HasColumnName(nameof(Appendix.DocumentId));

                b.HasIndex(x => new { x.Name, x.CompanyName, x.DepartmentName });
                
            });


            /*builder.Entity<DocumentAppendix>(b =>
            {
                b.ToTable(options.TablePrefix + "DocumentAppendixes", options.Schema);

                b.ConfigureByConvention();

                b.Property(x => x.DocumentId).HasColumnName(nameof(DocumentAppendix.DocumentId));
                b.Property(x => x.AppendixId).HasColumnName(nameof(DocumentAppendix.AppendixId));

                b.HasKey(x => new { x.DocumentId, x.AppendixId });
            });*/
        }
    }
}