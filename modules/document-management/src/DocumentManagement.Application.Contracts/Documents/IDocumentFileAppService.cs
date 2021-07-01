using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DocumentManagement.Documents
{
    public interface IDocumentFileAppService : IApplicationService
    {
        Task SaveDocumentFileAsync(SaveDocumentFileDto input);
        Task<DocumentFileDto> GetDocumentFileAsync(GetDocumentFileDto input);
    }
}
