using MasterData.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MasterData
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(MasterDataEntityFrameworkCoreTestModule)
        )]
    public class MasterDataDomainTestModule : AbpModule
    {
        
    }
}
