using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AutoMapper;
using DocumentManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Uow;
using DocumentManagement.Appendixes;
using Volo.Abp;
using System.Linq;
using System.IO;
using DocumentManagement.Settings;

namespace DocumentManagement.Documents
{
    [Authorize(DocumentManagementPermissions.Documents.Default)]
    public class DocumentAppService : DocumentManagementAppService, IDocumentAppService
    {
        protected IDocumentRepository DocumentRepository { get; }
        protected IAppendixRepository AppendixRepository { get; }

        public DocumentAppService(
            IDocumentRepository documentRepository,
            IAppendixRepository appendixRepository)
        {
            DocumentRepository = documentRepository;
            AppendixRepository = appendixRepository;
        }

        public virtual async Task<DocumentDto> FindByNameAsync(string name)
        {
            return ObjectMapper.Map<Document, DocumentDto>(
                    await DocumentRepository.FindByNameAsync(name)
                );
        }

        public async Task<DocumentDto> GetByCodeAsync(string code)
        {
            return ObjectMapper.Map<Document, DocumentDto>(
                  await DocumentRepository.GetByCodeAsync(code)
              );
        }

        public virtual async Task<DocumentDto> GetAsync(long id)
        {
            return ObjectMapper.Map<Document, DocumentDto>(
                   await DocumentRepository.GetAsync(id)
               );
        }

        public virtual async Task<PagedResultDto<DocumentDto>> GetListAsync(GetDocumentsInput input)
        {

            var count = await DocumentRepository.GetCountAsync();

            var list = await DocumentRepository.GetPagedListAsync(
                (input.Page - 1) * input.Size,
                input.Size,
                "CreationTime desc",
                true
            );

            return new PagedResultDto<DocumentDto>(
                count,
                ObjectMapper.Map<List<Document>, List<DocumentDto>>(list)
            );
        }

        [UnitOfWork]
        public async Task<DocumentDto> CreateAsync(CreateDocumentDto input)
        {
            var document = ObjectMapper.Map<CreateDocumentDto, Document>(input);

            var existingDocument = await DocumentRepository.CheckExistDocumentForCreateAsync(document);

            if (existingDocument?.Id > 0)
            {
                throw new BusinessException(code: DocumentManagementErrorCodes.Document.CodeExists)
                                .WithData("Code", input.Code);
            }


            document.FileName = await UploadDocumentsAsync(input);
            document.FolderName = input.FolderName;

            document = await DocumentRepository.InsertAsync(document, true);

            if (document.Id > 0)
            {
                if (input != null
                    && input.Appendixes != null
                    && input.Appendixes.Any())
                {
                    var appendixes = input.Appendixes.Select(a => new Appendix(0)
                    {
                        Active = document.Active,
                        DDCAudited = document.DDCAudited,
                        AppendixNumber = document.DocumentNumber,
                        AppliedToEntire = document.AppliedToEntire,
                        Approver = document.Approver,
                        Auditor = document.Auditor,
                        Code = $"PL{DateTime.Now.ToString("yyyymmddtthhss")}",
                        Name = a.Name,
                        FileName = a.FileName,
                        CompanyCode = document.CompanyCode,
                        CompanyName = document.CompanyName,
                        DepartmentCode = document.DepartmentCode,
                        DepartmentName = document.DepartmentName,
                        DocumentType = "PL",
                        EffectiveDate = document.EffectiveDate,
                        Module = document.Module,
                        StatusId = document.StatusId,
                        FormType = document.FormType,
                        RelateToDocuments = document.Code,
                        DocumentId = document.Id,
                        IssuedToEntire = document.IssuedToEntire,
                        Drafter = document.Drafter,
                        Description = a.Description,
                        FolderName = document.FolderName,
                        IssuedStatusId = document.IssuedStatusId,
                        LinkFile = a.LinkFile,
                        ReplaceEffectiveDate = document.ReplaceEffectiveDate,
                        ReviewNumber = a.ReviewNumber,
                    });

                    await AppendixRepository.InsertManyAsync(appendixes, true);
                }
            }

            return ObjectMapper.Map<Document, DocumentDto>(document);
        }

        [UnitOfWork]
        public async Task<DocumentDto> UpdateAsync(long id, UpdateDocumentDto input)
        {
            var existingDocument = await DocumentRepository.GetAsync(id);

            if (existingDocument?.Id < 0)
            {
                throw new BusinessException(code: DocumentManagementErrorCodes.Document.CodeNotExists)
                                .WithData("Code", input.Code);
            }

            var document = ObjectMapper.Map<UpdateDocumentDto, Document>(input);

            existingDocument.Description = document.Description;
            existingDocument.AppliedToEntire = document.AppliedToEntire;
            existingDocument.Active = document.Active;
            existingDocument.Approver = document.Approver;
            existingDocument.Auditor = document.Auditor;
            existingDocument.CompanyCode = document.CompanyCode;
            existingDocument.ContentChange = document.ContentChange;
            existingDocument.DDCAudited = document.DDCAudited;
            existingDocument.DepartmentCode = document.DepartmentCode;
            existingDocument.DocumentNumber = document.DocumentNumber;
            existingDocument.DocumentType = document.DocumentType;
            existingDocument.Drafter = document.Drafter;
            existingDocument.EffectiveDate = document.EffectiveDate;
            existingDocument.FormType = document.FormType;
            existingDocument.IssuedToEntire = document.IssuedToEntire;
            existingDocument.IssuedStatusId = document.IssuedStatusId;
            existingDocument.Module = document.Module;
            existingDocument.Name = document.Name;
            existingDocument.RelateToDocuments = document.RelateToDocuments;
            existingDocument.ReplaceEffectiveDate = document.ReplaceEffectiveDate;
            existingDocument.ReplaceFor = document.ReplaceFor;
            existingDocument.ReviewDate = document.ReviewDate;
            existingDocument.ReviewNumber = document.ReviewNumber;
            existingDocument.StatusId = document.StatusId;

            document.FileName = await UploadDocumentsAsync(existingDocument.FolderName, input);

            if (!string.IsNullOrEmpty(existingDocument.FileName))
            {
                List<string> oldFiles = existingDocument.FileName.Split(';')
                    .Where(f => !f.IsNullOrEmpty())
                    .ToList();

                List<string> newFiles = document.FileName.Split(';')
                    .Where(f => !f.IsNullOrEmpty())
                    .ToList();

                foreach (string newFile in newFiles)
                {
                    if (!oldFiles.Any(file => file == newFile))
                    {
                        oldFiles.Add(newFile);
                    }
                }

                existingDocument.FileName = string.Join(";", oldFiles);
            }
            else
            {
                existingDocument.FileName = document.FileName;
                existingDocument.FolderName = input.FolderName;
            }

            if (existingDocument.Id > 0)
            {
                if (input != null
                    && input.Appendixes != null
                    && input.Appendixes.Any())
                {
                    var appendixesInsert = input.Appendixes.Where(a => a.Id <= 0).Select(a => new Appendix(0)
                    {
                        Active = document.Active,
                        DDCAudited = document.DDCAudited,
                        AppendixNumber = document.DocumentNumber,
                        AppliedToEntire = document.AppliedToEntire,
                        Approver = document.Approver,
                        Auditor = document.Auditor,
                        Code = $"PL{DateTime.Now.ToString("yyyymmddtthhss")}",
                        Name = a.Name,
                        FileName = a.FileName,
                        CompanyCode = document.CompanyCode,
                        CompanyName = document.CompanyName,
                        DepartmentCode = document.DepartmentCode,
                        DepartmentName = document.DepartmentName,
                        DocumentType = "PL",
                        EffectiveDate = document.EffectiveDate,
                        Module = document.Module,
                        StatusId = document.StatusId,
                        FormType = document.FormType,
                        RelateToDocuments = document.Code,
                        DocumentId = document.Id,
                        IssuedToEntire = document.IssuedToEntire,
                        Drafter = document.Drafter,
                        Description = a.Description,
                        FolderName = existingDocument.FolderName,
                        IssuedStatusId = document.IssuedStatusId,
                        LinkFile = a.LinkFile,
                        ReplaceEffectiveDate = document.ReplaceEffectiveDate,
                        ReviewNumber = a.ReviewNumber
                    });

                    await AppendixRepository.InsertManyAsync(appendixesInsert);

                    existingDocument.Appendixes.Where(a => a.Id > 0)
                        .ToList()
                        .ForEach(
                                appendix =>
                               {
                                   if (appendix?.Id > 0)
                                   {
                                       var appendixExists = input.Appendixes.FirstOrDefault(a => a.Id == appendix.Id);

                                       if (appendixExists?.Id > 0)
                                       {
                                           appendix.Name = appendixExists.Name;
                                           appendix.FileName = appendixExists.FileName;
                                           appendix.Description = appendixExists.Description;
                                           appendix.FolderName = existingDocument.FolderName;
                                           appendix.LinkFile = appendixExists.LinkFile;
                                           appendix.ReviewNumber = appendixExists.ReviewNumber;
                                       }
                                       appendix.Active = document.Active;
                                       appendix.DDCAudited = document.DDCAudited;
                                       appendix.AppendixNumber = document.DocumentNumber;
                                       appendix.AppliedToEntire = document.AppliedToEntire;
                                       appendix.Approver = document.Approver;
                                       appendix.Auditor = document.Auditor;
                                       appendix.CompanyCode = document.CompanyCode;
                                       appendix.CompanyName = document.CompanyName;
                                       appendix.DepartmentCode = document.DepartmentCode;
                                       appendix.DepartmentName = document.DepartmentName;
                                       appendix.DocumentType = "PL";
                                       appendix.EffectiveDate = document.EffectiveDate;
                                       appendix.Module = document.Module;
                                       appendix.StatusId = document.StatusId;
                                       appendix.FormType = document.FormType;
                                       appendix.RelateToDocuments = document.Code;
                                       appendix.DocumentId = document.Id;
                                       appendix.IssuedToEntire = document.IssuedToEntire;
                                       appendix.Drafter = document.Drafter;
                                       appendix.IssuedStatusId = document.IssuedStatusId;
                                       appendix.ReplaceEffectiveDate = document.ReplaceEffectiveDate;
                                   }
                               });

                    foreach (var appendix in existingDocument.Appendixes.Where(a => a.Id > 0))
                    {
                        if (!input.Appendixes.Any(a => a.Id == appendix.Id))
                        {
                            await AppendixRepository.DeleteAsync(appendix.Id, true);
                        }
                    }
                }
            }

            return ObjectMapper.Map<Document, DocumentDto>(existingDocument);
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResultDto<DocumentDto>> FilteredPagedListAsync(GetDocumentsInput input)
        {

            var list = await DocumentRepository.FilteredPagedListAsync(
                input.Filter,
                "CreationTime desc",
                (input.Page - 1) * input.Size,
                input.Size
            );

            return new PagedResultDto<DocumentDto>(
                list.Count,
                ObjectMapper.Map<List<Document>, List<DocumentDto>>(list)
            );
        }

        [UnitOfWork]
        public async Task DeleteFileByDocumentIdAndFileNameAsync(DeleteFileByDocumentIdAndFileNameInput input)
        {
            var document = await DocumentRepository.GetAsync(input.Id);

            if (document?.Id < 0)
            {
                throw new BusinessException(code: DocumentManagementErrorCodes.Document.CodeNotExists)
                                .WithData("Code", "Not Found");
            }

            if (!string.IsNullOrEmpty(document.FileName))
            {
                List<string> files = document.FileName.Split(';').ToList();
                files.Remove(input.FileName);
                document.FileName = string.Join(";", files);
                document.LinkFile = string.Empty;
                string uploadFolderPath = await SettingProvider.GetOrNullAsync(DocumentManagementSettings.UploadFilePath);
                File.Delete($"{uploadFolderPath}/{document.FolderName}/{input.FileName}");
            }
        }

        public async Task<PagedResultDto<DocumentDto>> FilteredPagedListByDocumentTypeAsync(GetDocumentsByDocumentTypeInput input)
        {
            var list = await DocumentRepository.FilteredPagedListByDocumentTypeAsync(
               input.Filter,
               input.DocumentType,
               "CreationTime desc",
               (input.Page - 1) * input.Size,
               input.Size
            );

            return new PagedResultDto<DocumentDto>(
                list.Count,
                ObjectMapper.Map<List<Document>, List<DocumentDto>>(list)
            );
        }

        public async Task<PagedResultDto<DocumentDto>> FilteredPagedListByDepartmentCodeAsync(GetDocumentsByDepartmentCodeInput input)
        {
            var list = await DocumentRepository.FilteredPagedListByDepartmentCodeAsync(
               input.Filter,
               input.DepartmentCode,
               "CreationTime desc",
               (input.Page - 1) * input.Size,
               input.Size
            );

            return new PagedResultDto<DocumentDto>(
                list.Count,
                ObjectMapper.Map<List<Document>, List<DocumentDto>>(list)
            );
        }

        [UnitOfWork]
        public async Task<DocumentDto> ReviewAsync(ReviewDocumentDto input)
        {
            var document = ObjectMapper.Map<ReviewDocumentDto, Document>(input);

            var existingDocument = await DocumentRepository.CheckExistDocumentForReviewAsync(document);

            if (existingDocument?.Id > 0)
            {
                throw new BusinessException(code: DocumentManagementErrorCodes.Document.CodeExists)
                                .WithData("Code", input.Code);
            }


            document.FileName = await UploadDocumentsAsync(input);
            document.FolderName = input.FolderName;

            document = await DocumentRepository.InsertAsync(document, true);

            if (document.Id > 0)
            {
                if (input != null
                    && input.Appendixes != null
                    && input.Appendixes.Any())
                {
                    var appendixes = input.Appendixes.Select(a => new Appendix(0)
                    {
                        Active = document.Active,
                        DDCAudited = document.DDCAudited,
                        AppendixNumber = document.DocumentNumber,
                        AppliedToEntire = document.AppliedToEntire,
                        Approver = document.Approver,
                        Auditor = document.Auditor,
                        Code = $"PL{DateTime.Now.ToString("yyyymmddtthhss")}",
                        Name = a.Name,
                        FileName = a.FileName,
                        CompanyCode = document.CompanyCode,
                        CompanyName = document.CompanyName,
                        DepartmentCode = document.DepartmentCode,
                        DepartmentName = document.DepartmentName,
                        DocumentType = "PL",
                        EffectiveDate = document.EffectiveDate,
                        Module = document.Module,
                        StatusId = document.StatusId,
                        FormType = document.FormType,
                        RelateToDocuments = document.RelateToDocuments,
                        DocumentId = document.Id,
                        IssuedToEntire = document.IssuedToEntire,
                        Drafter = document.Drafter,
                        Description = a.Description,
                        FolderName = document.FolderName,
                        IssuedStatusId = document.IssuedStatusId,
                        LinkFile = a.LinkFile,
                        ReplaceEffectiveDate = document.ReplaceEffectiveDate,
                        ReviewNumber = a.ReviewNumber,
                    });

                    await AppendixRepository.InsertManyAsync(appendixes, true);
                }
            }

            return ObjectMapper.Map<Document, DocumentDto>(document);
        }
    }
}
