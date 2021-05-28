using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MasterData.DocumentTypes
{
    public class GetDocumentTypesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
