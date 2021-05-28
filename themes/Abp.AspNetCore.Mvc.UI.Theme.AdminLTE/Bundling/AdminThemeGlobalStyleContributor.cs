using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Abp.AspNetCore.Mvc.UI.Theme.AdminLTE.Bundling
{
    public class AdminThemeGlobalStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.Add("/libs/Ionicons/css/ionicons.min.css");
            context.Files.Add("/plugins/overlayScrollbars/css/OverlayScrollbars.min.css");
            context.Files.Add("/plugins/busy-load/busy-load.min.css");
            context.Files.Add("/plugins/toast/jquery.toast.min.css");
            context.Files.Add("/themes/adminlte/css/adminlte.min.css");
            context.Files.Add("/themes/adminlte/css/layout.css");
            context.Files.Add("/libs/famfamfam_flags/famfamfam-flags.css");
            context.Files.Add("/plugins/tabulator/css/tabulator.css");
            context.Files.Add("/plugins/tabulator/css/bootstrap/tabulator_bootstrap4.css");
            //context.Files.Add("/plugins/jquery-form-validator/theme-default.min.css");
            context.Files.Add("/plugins/select2-bootstrap4-theme/select2-bootstrap4.min.css");
        }
    }
}
