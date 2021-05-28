
namespace DocumentManagement.Documents
{
    public class GetDocumentsByDocumentTypeInput : PagedTabulatorRequestDto
    {
        public string Filter { get; set; }
        public string DocumentType { get; set; }
    }
}
