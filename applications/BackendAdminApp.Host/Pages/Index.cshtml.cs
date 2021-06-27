using HSaaS.AspNetCore.Mvc.UI.Widgets.Pages.Widgets;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BackendAdminApp.Host.Pages
{
    public class IndexModel : BackendAdminPageModel
    {
        [BindProperty]
        public List<SmallBoxInfoViewModel> Widgets { get; set; }

        public IndexModel()
        {
            Widgets = new List<SmallBoxInfoViewModel> { 
                BuildMasterDataInfo(),
                BuildDocumentInfo()
            };
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        private SmallBoxInfoViewModel BuildMasterDataInfo()
        {
            var documentInfo = new SmallBoxInfoViewModel("masterDataInfo", "fas fa-database");

            documentInfo.AddHeader(new SmallBoxHeaderInfoViewModel
            {
                DisplayValue = "Master Data",
                Description = "Master Data (Companies, Departments...).",
                ValueType = ValueType.Text
            })
                .AddFooter(new SmallBoxFooterInfoViewModel
                {
                    DisplayName = "See Master Data",
                    Url = "MasterData/Companies"
                });

            return documentInfo;
        }

        private SmallBoxInfoViewModel BuildDocumentInfo()
        {
            var documentInfo = new SmallBoxInfoViewModel("documentInfo", "fas fa-book");

            documentInfo.AddHeader(new SmallBoxHeaderInfoViewModel
            {
                DisplayValue = "Document Management",
                Description = "Document management and issuance application.",
                ValueType = ValueType.Text
            });

            documentInfo.AddFooter(new SmallBoxFooterInfoViewModel
            {
                DisplayName = "See documents",
                Url = "DocumentManagement/Documents"
            });

            return documentInfo;
        }
    }
}