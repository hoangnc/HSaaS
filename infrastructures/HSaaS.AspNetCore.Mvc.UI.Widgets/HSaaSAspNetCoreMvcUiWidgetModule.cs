using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Modularity;

namespace HSaaS.AspNetCore.Mvc.UI.Widgets
{

    public class HSaaSAspNetCoreMvcUiWidgetModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(HSaaSAspNetCoreMvcUiWidgetModule).Assembly);
            });
        }
    }
}
