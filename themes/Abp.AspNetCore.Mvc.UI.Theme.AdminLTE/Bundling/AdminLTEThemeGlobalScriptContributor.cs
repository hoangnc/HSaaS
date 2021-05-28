using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Abp.AspNetCore.Mvc.UI.Theme.AdminLTE.Bundling
{
    public class AdminLTEThemeGlobalScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.Add("/plugins/jquery-ui/jquery-ui.js");
            context.Files.Add("/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js");
            context.Files.Add("/plugins/busy-load/busy-load.min.js");
            context.Files.Add("/plugins/toast/jquery.toast.min.js");
            context.Files.Add("/themes/adminlte/js/adminlte.js");
            context.Files.Add("/themes/adminlte/js/settings.js");
            context.Files.Add("/themes/adminlte/js/layout.js");
            context.Files.Add("/plugins/tabulator/js/tabulator.js");
            //context.Files.Add("/plugins/jquery-form-validator/jquery.form-validator.min.js");
            //context.Files.Add("/plugins/jquery-form-validator/jquery.form-validator.culture.vi-VN.js");
            context.Files.Add("/plugins/select2/js/select2.full.min.js");
            context.Files.Add("/plugins/jquery-fileupload/jquery.fileupload.js");
            context.Files.Add("/plugins/tmpl/tmpl.min.js");
            context.Files.Add("/plugins/moment/moment-with-locales.min.js");
        }
    }
}
