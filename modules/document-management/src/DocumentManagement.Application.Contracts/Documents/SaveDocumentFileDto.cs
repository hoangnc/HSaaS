using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Documents
{
    public class SaveDocumentFileDto
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
