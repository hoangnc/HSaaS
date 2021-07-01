using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace DocumentManagement.Documents
{
    public class DocumentFileAppService : DocumentManagementAppService, IDocumentFileAppService
    {
        private readonly IBlobContainer<DocumentFileContainer> _blobContainer;

        public DocumentFileAppService(IBlobContainer<DocumentFileContainer> blobContainer)
        {
            _blobContainer = blobContainer;
        }

        public async Task<DocumentFileDto> GetDocumentFileAsync(GetDocumentFileDto input)
        {
            var bytes = await _blobContainer.GetAllBytesAsync(input.Name);

            return new DocumentFileDto
            {
                Name = input.Name,
                Content = bytes
            };
        }

        public async Task SaveDocumentFileAsync(SaveDocumentFileDto input)
        {
            await _blobContainer.SaveAsync(input.Name, input.Content, true);
        }
    }
}
