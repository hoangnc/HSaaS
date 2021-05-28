using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace DocumentManagement.Documents
{

    public class MimeMappingService : IMineMappingService
    {
        private readonly FileExtensionContentTypeProvider _contentTypeProvider;

        public MimeMappingService()
        {
            _contentTypeProvider = new FileExtensionContentTypeProvider();
        }

        public string GetContentType(string fileName)
        {
            string contentType;

            if (!_contentTypeProvider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
