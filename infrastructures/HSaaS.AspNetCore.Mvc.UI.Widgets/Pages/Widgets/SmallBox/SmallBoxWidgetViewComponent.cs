using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace HSaaS.AspNetCore.Mvc.UI.Widgets.Pages.Widgets
{
    [Widget]
    public class SmallBoxWidgetViewComponent : AbpViewComponent
    {
        public IViewComponentResult Invoke(SmallBoxInfoViewModel smallBoxInfoViewModel)
        {
            return View("~/Pages/Widgets/SmallBox/Default.cshtml", smallBoxInfoViewModel);
        }
    }

    public class SmallBoxInfoViewModel
    {
        private readonly Dictionary<SmallBoxType, string> MachingCssClasses = new Dictionary<SmallBoxType, string> {
            { SmallBoxType.Info, "small-box bg-info" },
            { SmallBoxType.Success, "small-box bg-success" },
            { SmallBoxType.Warning, "small-box bg-warning" },
            { SmallBoxType.Danger, "small-box bg-danger" }
        };

        public string Name { get; private set; }
        public SmallBoxType Type { get; set; } = SmallBoxType.Info;
        public string Icon { get; private set; }
        public SmallBoxHeaderInfoViewModel Header { get; private set; }
        public SmallBoxFooterInfoViewModel Footer { get; private set; }

        public SmallBoxInfoViewModel(string name,
                                     string icon = "fas fa-chart-pie")
        {
            Name = name;
            Icon = icon;         
        }

        public SmallBoxInfoViewModel AddHeader(SmallBoxHeaderInfoViewModel smallBoxHeaderInfoViewModel)
        {
            Header = smallBoxHeaderInfoViewModel;
            return this;
        }

        public SmallBoxInfoViewModel AddFooter(SmallBoxFooterInfoViewModel smallBoxFooterInfoViewModel)
        {
            Footer = smallBoxFooterInfoViewModel;
            return this;
        }

        public string GetCssClass()
        {
            return MachingCssClasses[Type];
        }
    }

    public class SmallBoxHeaderInfoViewModel
    {
        public ValueType ValueType { get; set; }
        public string DisplayValue { get; set; }
        public string Description { get; set; }
        public string CssClass { get; set; }
    }

    public class SmallBoxFooterInfoViewModel
    {
        public string CssClass { get; set; } = "small-box-footer";
        public string Url { get; set; } = "#";
        public string DisplayName { get; set; }
    }

    public enum SmallBoxType
    {
        Info = 1,
        Success = 2,
        Warning = 3,
        Danger = 4
    }

    public enum ValueType
    {
        Text = 1,
        Number = 2,
        Percent = 3
    }
}
