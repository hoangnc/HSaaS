﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace MasterData.DocumentTypes
{
    public interface IDocumentTypeAppService : ICrudAppService<DocumentTypeDto, Guid, GetDocumentTypesInput, DocumentTypeCreateDto, DocumentTypeUpdateDto>
    {
    }
}
