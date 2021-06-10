using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace DocumentManagement.Documents
{
    public class DocumentAppendix : CreationAuditedEntity
    {
        public virtual Guid DocumentId { get; protected set; }

        public virtual Guid AppendixId { get; protected set; }

        protected DocumentAppendix()
        {

        }

        public DocumentAppendix(Guid documentId, Guid appendixId)
        {
            DocumentId = documentId;
            AppendixId = appendixId;
        }

        public override object[] GetKeys()
        {
            return new object[] { DocumentId, AppendixId };
        }
    }
}
