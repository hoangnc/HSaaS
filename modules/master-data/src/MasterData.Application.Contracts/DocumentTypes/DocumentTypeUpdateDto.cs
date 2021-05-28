using System;
using System.Collections.Generic;
using System.Text;

namespace MasterData.DocumentTypes
{
    public class DocumentTypeUpdateDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
