using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DocumentManagement.Documents
{
    public class GetDocumentsInput : PagedTabulatorRequestDto
    {
        public string Filter { get; set; }
    }
}
