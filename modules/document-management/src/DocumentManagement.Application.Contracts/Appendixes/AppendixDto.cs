using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DocumentManagement.Appendixes
{
    public class AppendixDto : FullAuditedEntityDto<long>
    {
        public virtual string Code { get; set; }
        public virtual string CompanyCode { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string DepartmentCode { get; set; }
        public virtual string DepartmentName { get; set; }
        public virtual string Module { get; set; }
        public virtual string DocumentType { get; set; }
        public virtual string Name { get; set; }
        public virtual string FileName { get; set; }
        public virtual string AppendixNumber { get; set; }
        public virtual string ReviewNumber { get; set; }
        public virtual string Description { get; set; }
        public virtual string ContentChange { get; set; }
        public virtual string Drafter { get; set; }
        public virtual string Auditor { get; set; }
        public virtual string Approver { get; set; }
        public virtual DateTime? EffectiveDate { get; set; }
        public virtual DateTime? ReviewDate { get; set; }
        public virtual string AppliedToEntire { get; set; }
        public virtual string IssuedToEntire { get; set; }
        public virtual string ReplaceFor { get; set; }
        public virtual DateTime? ReplaceEffectiveDate { get; set; }
        public virtual string RelateToDocuments { get; set; }
        public virtual bool DDCAudited { get; set; }
        public virtual string FolderName { get; set; }
        public virtual string LinkFile { get; set; }
        public virtual int StatusId { get; set; }
        public virtual int FormType { get; set; }
        public virtual bool Active { get; set; }
        public virtual int IssuedStatusId { get; set; }
        public virtual long DocumentId { get; set; }
        public string ExtraProperties { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
