using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using DocumentManagement.Localization;
using Volo.Abp.UI.Navigation;
using DocumentManagement.Permissions;
using MasterData.Departments;
using MasterData.DocumentTypes;
using System.Linq;

namespace DocumentManagement.Web.Navigation
{
    public class DocumentManagementWebMainMenuContributor : IMenuContributor
    {
        private readonly IDepartmentAppService _departmentAppService;
        private readonly IDocumentTypeAppService _documentTypeAppService;

        public DocumentManagementWebMainMenuContributor(
            IDepartmentAppService departmentAppService,
            IDocumentTypeAppService documentTypeAppService)
        {
            _departmentAppService = departmentAppService;
            _documentTypeAppService = documentTypeAppService;
        }
        public virtual async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name != StandardMenus.Main)
            {
                return;
            }

            var l = context.GetLocalizer<DocumentManagementResource>();

            var documentManagementMenuItem = new ApplicationMenuItem(DocumentManagementMenuNames.GroupName, l["Menu:DocumentManagement"], icon: "fa fa-book");
            context.Menu.AddItem(documentManagementMenuItem);

            documentManagementMenuItem.AddItem(new ApplicationMenuItem(DocumentManagementMenuNames.Documents, l["Documents"], url: "~/DocumentManagement/Documents")); //, requiredPermissionName: DocumentManagementPermissions.Documents.Default));

            var operationMenuItem = new ApplicationMenuItem(DocumentManagementMenuNames.OperationData, l["MenuOperationData"], url: "#");
            documentManagementMenuItem.AddItem(operationMenuItem);

            var documentTypes = await _documentTypeAppService.GetListAsync(new GetDocumentTypesInput
            {
                MaxResultCount = 1000,
                SkipCount = 0
            });

            if (documentTypes != null && documentTypes.Items.Any())
            {
                foreach (var documentType in documentTypes.Items)
                {
                    operationMenuItem.Items.Add(new ApplicationMenuItem(documentType.Code, documentType.Name, url: $"~/DocumentManagement/Documents/OperationData?Code={documentType.Code}"));
                }
            }

            var departmentMenuItem = new ApplicationMenuItem(DocumentManagementMenuNames.DepartmentData, l["MenuDepartmentData"], url: $"#");
            documentManagementMenuItem.AddItem(departmentMenuItem);

            var departments = await _departmentAppService.GetListAsync(new GetDepartmentsInput
            {
                MaxResultCount = 1000,
                SkipCount = 0
            });

            if (departments != null && departments.Items.Any())
            {
                foreach (var department in departments.Items)
                {
                    departmentMenuItem.Items.Add(new ApplicationMenuItem(department.Code, department.Name, url: $"~/DocumentManagement/Documents/DepartmentData?Code={department.Code}"));
                }
            }

            return;
        }
    }
}
