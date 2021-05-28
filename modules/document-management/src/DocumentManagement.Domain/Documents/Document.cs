using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DocumentManagement.Appendixes;
using JetBrains.Annotations;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Users;

namespace DocumentManagement.Documents
{
    [Audited]
    public class Document : FullAuditedAggregateRoot<long>
    {
        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string CompanyCode { get; set; }

        [CanBeNull]
        public virtual string CompanyName { get; set; }

        [NotNull]
        public virtual string DepartmentCode { get; set; }

        [CanBeNull]
        public virtual string DepartmentName { get; set; }

        [NotNull]
        public virtual string Module { get; set; }

        [NotNull]
        public virtual string DocumentType { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string FileName { get; set; }

        [NotNull]
        public virtual string DocumentNumber { get; set; }

        [CanBeNull]
        public virtual string ReviewNumber { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        [CanBeNull]
        public virtual string ContentChange { get; set; }

        [NotNull]
        public virtual string Drafter { get; set; }

        [NotNull]
        public virtual string Auditor { get; set; }

        [NotNull]
        public virtual string Approver { get; set; }

        [CanBeNull]
        public virtual DateTime? EffectiveDate { get; set; }

        [CanBeNull]
        public virtual DateTime? ReviewDate { get; set; }

        [NotNull]
        public virtual string AppliedToEntire { get; set; }

        [NotNull]
        public virtual string IssuedToEntire { get; set; }

        [CanBeNull]
        public virtual string ReplaceFor { get; set; }

        [CanBeNull]
        public virtual DateTime? ReplaceEffectiveDate { get; set; }

        [CanBeNull]
        public virtual string RelateToDocuments { get; set; }

        [CanBeNull]
        public virtual bool DDCAudited { get; set; }

        [CanBeNull]
        public virtual string FolderName { get; set; }

        [CanBeNull]
        public virtual string LinkFile { get; set; }

        public virtual int StatusId { get; set; }
        public virtual int FormType { get; set; }
        public virtual bool Active { get; set; }
        public virtual int IssuedStatusId { get; set; }

        public virtual Collection<Appendix> Appendixes { get; protected set; }

        protected Document()
        {

        }

        public Document(
                        long id) {
            Id = id;
            Appendixes = new Collection<Appendix>();
        }

        public virtual void AddAppendix(Appendix appendix)
        {
            Appendixes.Add(appendix);
        }

        public virtual void RemoveAppendix(long appendixId)
        {
            Appendixes.RemoveAll(t => t.Id == appendixId);
        }
    }
}
