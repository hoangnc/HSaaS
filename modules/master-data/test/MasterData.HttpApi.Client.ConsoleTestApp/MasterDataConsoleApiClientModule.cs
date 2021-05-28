using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace MasterData
{
    [DependsOn(
        typeof(MasterDataHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class MasterDataConsoleApiClientModule : AbpModule
    {
        
    }
}
