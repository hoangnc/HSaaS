using System.Threading.Tasks;

namespace DocumentManagement.Documents
{
    public interface IDocumentEmailSenderService
    {
        string GetMailTemplate(string fileName);
        Task<int> SendMailReleaseDocumentAsync(DocumentDto document);
        Task<int> SendMailReviewAndReleaseAsync(DocumentDto document);
    }
}
