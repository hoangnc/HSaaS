
namespace DocumentManagement.Documents
{
    public class GetDocumentsByDepartmentCodeInput : PagedTabulatorRequestDto
    {
        public string Filter { get; set; }
        public string DepartmentCode { get; set; }
    }
}
