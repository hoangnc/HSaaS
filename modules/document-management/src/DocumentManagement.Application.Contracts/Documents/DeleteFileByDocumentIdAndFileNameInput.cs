using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Documents
{
    public class DeleteFileByDocumentIdAndFileNameInput
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
    }
}
