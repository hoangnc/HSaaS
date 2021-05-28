using DocumentManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DocumentManagement
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(DocumentManagementEntityFrameworkCoreTestModule)
        )]
    public class DocumentManagementDomainTestModule : AbpModule
    {
        
    }
}
