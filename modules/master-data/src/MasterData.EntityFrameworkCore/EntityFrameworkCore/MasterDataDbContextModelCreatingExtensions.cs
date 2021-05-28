using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using MasterData.Companies;
using Volo.Abp.EntityFrameworkCore.Modeling;
using MasterData.Departments;
using MasterData.DocumentTypes;
using MasterData.Modules;
using MasterData.UserDepartments;

namespace MasterData.EntityFrameworkCore
{
    public static class MasterDataDbContextModelCreatingExtensions
    {
        public static void ConfigureMasterData(
            this ModelBuilder builder,
            Action<MasterDataModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new MasterDataModelBuilderConfigurationOptions(
                MasterDataDbProperties.DbTablePrefix,
                MasterDataDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.Entity<Company>(c => {
                c.ToTable(options.TablePrefix + "Companies", options.Schema);

                c.ConfigureByConvention();

                // Properties
                c.Property(c => c.Code).IsRequired().HasMaxLength(20);
                c.Property(c => c.Name).IsRequired().HasMaxLength(500);
                c.Property(c => c.EmailAddress).IsRequired().HasMaxLength(120);

                //Indexes
                c.HasIndex(c => c.Name);
            });

            builder.Entity<Department>(c => {
                c.ToTable(options.TablePrefix + "Departments", options.Schema);

                c.ConfigureByConvention();

                // Properties
                c.Property(c => c.Code).IsRequired().HasMaxLength(20);
                c.Property(c => c.Name).IsRequired().HasMaxLength(500);
                c.Property(c => c.EmailAddress).IsRequired().HasMaxLength(120);

                //Indexes
                c.HasIndex(c => c.Name);
            });

            builder.Entity<DocumentType>(c => {
                c.ToTable(options.TablePrefix + "DocumentTypes", options.Schema);

                c.ConfigureByConvention();

                // Properties
                c.Property(c => c.Code).IsRequired().HasMaxLength(20);
                c.Property(c => c.Name).IsRequired().HasMaxLength(500);

                //Indexes
                c.HasIndex(c => c.Name);
            });

            builder.Entity<Module>(c => {
                c.ToTable(options.TablePrefix + "Modules", options.Schema);

                c.ConfigureByConvention();

                // Properties
                c.Property(c => c.Code).IsRequired().HasMaxLength(20);
                c.Property(c => c.Name).IsRequired().HasMaxLength(500);

                //Indexes
                c.HasIndex(c => c.Name);
            });

            builder.Entity<UserDepartment>(c => {
                c.ToTable(options.TablePrefix + "UserDepartments", options.Schema);

                c.ConfigureByConvention();

                // Properties
                c.Property(c => c.UserName).IsRequired().HasMaxLength(20);
                c.Property(c => c.DepartmentCode).IsRequired().HasMaxLength(20);

                //Indexes
                c.HasIndex(c => new { c.UserName, c.DepartmentCode });
            });
            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */
        }
    }
}