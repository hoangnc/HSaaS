using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Documents;
using MailKit.Net.Smtp;
using MailKit.Security;
using MasterData.Departments;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using MimeKit;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Emailing.Smtp;
using Volo.Abp.Identity;
using Volo.Abp.MailKit;
using Volo.Abp.Users;
using Volo.Abp.VirtualFileSystem;

namespace DocumentManagement.Documents
{
    public class DocumentEmailSenderService : IDocumentEmailSenderService, ITransientDependency
    {
        private const string DefaultFullName = "Nguyễn Công Hoàng";
        private readonly IMailKitSmtpEmailSender _emailSender;
        private readonly ISmtpEmailSenderConfiguration _smtpEmailSenderConfiguration;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICurrentUser _currentUser;
        private readonly IConfiguration _configuration;
        private readonly IIdentityUserAppService _identityUserAppService;
        private readonly IDepartmentAppService _departmentAppService;

        [Obsolete]
        public DocumentEmailSenderService(
            IMailKitSmtpEmailSender emailSender,
            ICurrentUser currentUser,
            IHostingEnvironment hostingEnvironment,
            ISmtpEmailSenderConfiguration smtpEmailSenderConfiguration,
            IConfiguration configuration,
            IIdentityUserAppService identityUserAppService,
            IDepartmentAppService departmentAppService
            )
        {
            _emailSender = emailSender;
            _currentUser = currentUser;
            _hostingEnvironment = hostingEnvironment;
            _smtpEmailSenderConfiguration = smtpEmailSenderConfiguration;
            _configuration = configuration;
            _identityUserAppService = identityUserAppService;
            _departmentAppService = departmentAppService;
        }

        [Obsolete]
        public string GetMailTemplate(string fileName)
        {
            var filePath = $"{_hostingEnvironment.ContentRootPath}/Templates/Email/{fileName}";
            string fileContext = File.ReadAllText(filePath);

            return fileContext;
        }

        [Obsolete]
        public async Task<int> SendMailReleaseDocumentAsync(DocumentDto document)
        {
            string mailTemplate = GetMailTemplate("ReleaseDocument.html");

            List<MailboxAddress> groupEmails = new List<MailboxAddress>() {
               new MailboxAddress( "Nguyễn Công Hoàng", "diemahoang8488@gmail.com" )
            };

            string[] issusedToEntires = document.IssuedToEntire.Split(";");

            List<string> issusedToEntireLst = new List<string>();

            var departments = (await _departmentAppService.GetListAsync(new GetDepartmentsInput
            {
                MaxResultCount = 100,
                SkipCount = 0
            })).Items;

            if (issusedToEntires != null && issusedToEntires.Any())
            {
                foreach (string entire in issusedToEntires)
                {
                    var departmentExist = departments.FirstOrDefault(g => g.Code == entire);
                    if (departmentExist != null)
                    {
                        issusedToEntireLst.Add(departmentExist.Name);
                        string email = departmentExist.EmailAddress;
                        groupEmails.Add(new MailboxAddress(departmentExist.Name, email.Replace(";", ",")));
                    }
                }
            }

            var users = (await _identityUserAppService.GetListAsync(new GetIdentityUsersInput
            {
                MaxResultCount = 1000,
                SkipCount = 0
            })).Items;

            string drafterFullName = DefaultFullName;

            string auditorFullName = DefaultFullName;

            string approverFullName = DefaultFullName;

            if (users.Any())
            {
                if (!document.Drafter.IsNullOrEmpty())
                {
                    drafterFullName = GetUserFullName(document.Drafter, users);
                }

                if (!document.Auditor.IsNullOrEmpty())
                {
                    auditorFullName = GetUserFullName(document.Auditor, users);
                }

                if (!document.Auditor.IsNullOrEmpty())
                {
                    approverFullName = GetUserFullName(document.Approver, users);
                }
            }

            if (!_currentUser.Email.IsNullOrEmpty() && _currentUser.UserName != "admin")
            {
                var createdEmail = new MailboxAddress(GetUserFullName(_currentUser.UserName, users), _currentUser.Email);

                groupEmails.Add(createdEmail);
            }

            var department = departments.FirstOrDefault(d => d.Code == document.DepartmentCode);

            if (!mailTemplate.IsNullOrEmpty())
            {
                string effectiveDateString = string.Empty;
                if (document.EffectiveDate.HasValue)
                {
                    effectiveDateString = $"{document.EffectiveDate.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)}";
                }

                string reviewDateString = string.Empty;
                if (document.ReviewDate.HasValue)
                {
                    reviewDateString = $"{document.ReviewDate.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)}";
                }

                string linkFile = $"<a href=\"{_configuration["App:SelfUrl"]}/api/document-management/filemanager/viewfile?sourceDoc={document.FolderName}/{document.FileName}\">{document.FileName}</a>";

                string host = await _smtpEmailSenderConfiguration.GetHostAsync();

                using (SmtpClient smtpClient = new SmtpClient())
                {

                    smtpClient.CheckCertificateRevocation = false;
                    smtpClient.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                    await smtpClient.ConnectAsync("gmail.com", 587, SecureSocketOptions.StartTls);
                    await smtpClient.AuthenticateAsync("anonymous", "123456");

                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Nguyễn Công Hoàng", "diemhoang8488@gmail.com"));
   
                    message.To.AddRange(groupEmails);
                    message.Subject = "P.ISO - Ban hành tài liệu mới";
                    string selfUrl = _configuration["App:SelfUrl"];

                    string template = string.Format(mailTemplate,
                        $"P.ISO - Ban hành tài liệu mới;#{department?.Name} : {document.FileName}",
                        "Ban hành tài liệu mới",
                        document.Name,
                        document.AppliedToEntire.Replace("\n", "<br>"),
                        effectiveDateString,
                        document.Description.Replace("\n", "<br>"),
                        string.Join("; ", issusedToEntireLst),
                        document.DocumentNumber,
                        document.ReviewNumber,
                        reviewDateString,
                        drafterFullName,
                        auditorFullName,
                        approverFullName,
                        linkFile,
                        $"<a href=\"{selfUrl}/documentmanagement/documents/detail?code={document.Code}\">{selfUrl}/documentmanagement/documents/detail?code={document.Code}</a>",
                        $"<a href=\"{selfUrl}/documentmanagement/documents/operationdata?code={document.DocumentType}\">{selfUrl}/documentmanagement/operationdata/list?code={document.DocumentType}</a>"
                    );

                    message.Body = new TextPart("html")
                    {
                        Text = template
                    };

                    await smtpClient.SendAsync(message);
                }
            }

            return 1;
        }

        [Obsolete]
        public async Task<int> SendMailReviewAndReleaseAsync(DocumentDto document)
        {
            string mailTemplate = GetMailTemplate("ReviewDocument.html");

            List<MailboxAddress> groupEmails = new List<MailboxAddress>() {
               new MailboxAddress( "Nguyễn Công Hoàng", "diemhoang8488@gmail.com" ),
            };

            string[] issusedToEntires = document.IssuedToEntire.Split(";");

            List<string> issusedToEntireLst = new List<string>();

            var departments = (await _departmentAppService.GetListAsync(new GetDepartmentsInput
            {
                MaxResultCount = 100,
                SkipCount = 0
            })).Items;

            if (issusedToEntires != null && issusedToEntires.Any())
            {
                foreach (string entire in issusedToEntires)
                {
                    var departmentExist = departments.FirstOrDefault(g => g.Code == entire);
                    if (departmentExist != null)
                    {
                        issusedToEntireLst.Add(departmentExist.Name);
                        string email = departmentExist.EmailAddress;
                        groupEmails.Add(new MailboxAddress(departmentExist.Name, email.Replace(";", ",")));
                    }
                }
            }

            var users = (await _identityUserAppService.GetListAsync(new GetIdentityUsersInput
            {
                MaxResultCount = 1000,
                SkipCount = 0
            })).Items;

            string drafterFullName = DefaultFullName;

            string auditorFullName = DefaultFullName;

            string approverFullName = DefaultFullName;

            if (users.Any())
            {
                if (!document.Drafter.IsNullOrEmpty())
                {
                    drafterFullName = GetUserFullName(document.Drafter, users);
                }

                if (!document.Auditor.IsNullOrEmpty())
                {
                    auditorFullName = GetUserFullName(document.Auditor, users);
                }

                if (!document.Auditor.IsNullOrEmpty())
                {
                    approverFullName = GetUserFullName(document.Approver, users);
                }
            }

            if (!_currentUser.Email.IsNullOrEmpty() && _currentUser.UserName != "admin")
            {
                var createdEmail = new MailboxAddress(GetUserFullName(_currentUser.UserName, users), _currentUser.Email);

                groupEmails.Add(createdEmail);
            }

            var department = departments.FirstOrDefault(d => d.Code == document.DepartmentCode);

            if (!mailTemplate.IsNullOrEmpty())
            {
                string effectiveDateString = string.Empty;
                if (document.EffectiveDate.HasValue)
                {
                    effectiveDateString = $"{document.EffectiveDate.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)}";
                }

                string reviewDateString = string.Empty;
                if (document.ReviewDate.HasValue)
                {
                    reviewDateString = $"{document.ReviewDate.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)}";
                }

                string linkFile = $"<a href=\"{_configuration["App:SelfUrl"]}/api/document-management/filemanager/viewfile?sourceDoc={document.FolderName}/{document.FileName}\">{document.FileName}</a>";

                string host = await _smtpEmailSenderConfiguration.GetHostAsync();

                using (SmtpClient smtpClient = new SmtpClient())
                {

                    smtpClient.CheckCertificateRevocation = false;
                    smtpClient.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                    await smtpClient.ConnectAsync("gmail.com", 587, SecureSocketOptions.StartTls);
                    await smtpClient.AuthenticateAsync("anonymous", "123456");

                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Nguyễn Công Hoàng", "diemhoang8488@gmail.com"));
           
                    message.To.AddRange(groupEmails);
                    message.Subject = "P.ISO - Thay đổi tài liệu";
                    string selfUrl = _configuration["App:SelfUrl"];

                    string template = string.Format(mailTemplate,
                        $"P.ISO - Thay đổi tài liệu;#{department?.Name} : {document.FileName}",
                        "Ban hành tài liệu mới",
                        document.Name,
                        document.AppliedToEntire.Replace("\n", "<br>"),
                        effectiveDateString,
                        document.Description.Replace("\n", "<br>"),
                        string.Join("; ", issusedToEntireLst),
                        document.DocumentNumber,
                        document.ReviewNumber,
                        reviewDateString,
                        drafterFullName,
                        auditorFullName,
                        approverFullName,
                        linkFile,
                        $"<a href=\"{selfUrl}/documentmanagement/documents/detail?code={document.Code}\">{selfUrl}/documentmanagement/documents/detail?code={document.Code}</a>",
                        $"<a href=\"{selfUrl}/documentmanagement/documents/operationdata?code={document.DocumentType}\">{selfUrl}/documentmanagement/operationdata/list?code={document.DocumentType}</a>"
                    );

                    message.Body = new TextPart("html")
                    {
                        Text = template
                    };

                    await smtpClient.SendAsync(message);
                }
            }

            return 1;
        }

        private string GetUserFullName(string userName, IEnumerable<IdentityUserDto> users)
        {
            var user = users.FirstOrDefault(u => u.UserName == userName);
            string userFullName = string.Empty;

            if (user != null)
            {
                userFullName = $"{user.Surname} {user.Name}";
            }

            return userFullName;
        }
    }
}
