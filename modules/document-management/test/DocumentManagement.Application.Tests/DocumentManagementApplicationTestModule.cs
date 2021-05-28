using Volo.Abp.Modularity;

namespace DocumentManagement
{
    [DependsOn(
        typeof(DocumentManagementApplicationModule),
        typeof(DocumentManagementDomainTestModule)
        )]
    public class DocumentManagementApplicationTestModule : AbpModule
    {

    }
}
