using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Documents;
using DocumentManagement.Localization;
using DocumentManagement.Settings;
using Microsoft.AspNetCore.Http;
using Volo.Abp.Application.Services;
using Volo.Abp.Settings;
using Volo.Abp.Uow;

namespace DocumentManagement
{
    public abstract class DocumentManagementAppService : ApplicationService
    {
        protected string DefaultFolderPath = @"D:\DocumentManagement\UploadFile";
        protected new ISettingProvider SettingProvider { get { return LazyServiceProvider.LazyGetService<ISettingProvider>(); } }
        protected DocumentManagementAppService()
        {
         
            LocalizationResource = typeof(DocumentManagementResource);
            ObjectMapperContext = typeof(DocumentManagementApplicationModule);
        }

        protected async Task SaveAsAsync(string filePath, IFormFile httpFile)
        {
            using (FileStream file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await httpFile.CopyToAsync(memoryStream);
                    memoryStream.WriteTo(file);
                }
            }
        }

        protected async Task<string> GetFolderPathAsync(string documentCode)
        {
            string uploadFolderPath = await SettingProvider.GetOrNullAsync(DocumentManagementSettings.UploadFilePath);
            uploadFolderPath = uploadFolderPath ?? DefaultFolderPath;

            string yearFolder = $"{uploadFolderPath}/{DateTime.Now.Year}";
            string monthFolder = $"{yearFolder}/{DateTime.Now.Month}";
            string dateFolder = $"{monthFolder}/{DateTime.Now.ToString("yyyyMMdd", CultureInfo.CurrentCulture)}";
            string documentFolder = $"{dateFolder}/{documentCode}";

            if (!Directory.Exists(yearFolder))
                Directory.CreateDirectory(yearFolder);

            if (!Directory.Exists(monthFolder))
                Directory.CreateDirectory(monthFolder);

            if (!Directory.Exists(dateFolder))
                Directory.CreateDirectory(dateFolder);

            if (!Directory.Exists(documentFolder))
                Directory.CreateDirectory(documentFolder);

            return documentFolder;
        }

        [UnitOfWork]
        protected async Task<string> UploadDocumentsAsync(CreateDocumentDto document)
        {
            List<string> files = new List<string>();
            string folderPath = await GetFolderPathAsync(document.Code);
            string uploadFolderPath = await SettingProvider.GetOrNullAsync(DocumentManagementSettings.UploadFilePath);
            
            if (document.Files != null && document.Files.Any())
            {
                foreach (IFormFile file in document.Files)
                {
                    string filePath = $"{folderPath}/{file.FileName}";
                    document.FolderName = folderPath.Replace(uploadFolderPath, string.Empty);
                    document.LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(folderPath, string.Empty)}/{file.FileName}";
                    files.Add(file.FileName);
                    await SaveAsAsync(filePath, file);
                }
            }

            if (document.AppendixFiles != null && document.AppendixFiles.Any())
            {
                int index = 0;
                foreach (IFormFile file in document.AppendixFiles)
                {
                    string filePath = $"{folderPath}/{file.FileName}";
                    await SaveAsAsync(filePath, file);
                    document.Appendixes[index].FolderName = folderPath.Replace(uploadFolderPath, string.Empty);
                    document.Appendixes[index].LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(folderPath, string.Empty)}/{file.FileName}";
                    index++;
                }
            }
            return string.Join(";", files);
        }

        [UnitOfWork]
        protected async Task<string> UploadDocumentsAsync(UpdateDocumentDto document)
        {
            List<string> files = new List<string>();
            string folderPath = await GetFolderPathAsync(document.Code);
            string uploadFolderPath = await SettingProvider.GetOrNullAsync(DocumentManagementSettings.UploadFilePath);

            if (document.Files != null && document.Files.Any())
            {
                foreach (IFormFile file in document.Files)
                {
                    string filePath = $"{folderPath}/{file.FileName}";
                    document.FolderName = folderPath.Replace(uploadFolderPath, string.Empty);
                    document.LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(folderPath, string.Empty)}/{file.FileName}";
                    files.Add(file.FileName);
                    await SaveAsAsync(filePath, file);
                }
            }

            if (document.AppendixFiles != null && document.AppendixFiles.Any())
            {
                int index = 0;
                foreach (IFormFile file in document.AppendixFiles)
                {
                    string filePath = $"{folderPath}/{file.FileName}";
                    await SaveAsAsync(filePath, file);
                    document.Appendixes[index].FolderName = folderPath.Replace(uploadFolderPath, string.Empty);
                    document.Appendixes[index].LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(folderPath, string.Empty)}/{file.FileName}";
                    index++;
                }
            }
            return string.Join(";", files);
        }


        [UnitOfWork]
        protected async Task<string> UploadDocumentsAsync(ReviewDocumentDto document)
        {
            List<string> files = new List<string>();
            string folderPath = await GetFolderPathAsync(document.Code);
            string uploadFolderPath = await SettingProvider.GetOrNullAsync(DocumentManagementSettings.UploadFilePath);

            if (document.Files != null && document.Files.Any())
            {
                foreach (IFormFile file in document.Files)
                {
                    string filePath = $"{folderPath}/{file.FileName}";
                    document.FolderName = folderPath.Replace(uploadFolderPath, string.Empty);
                    document.LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(folderPath, string.Empty)}/{file.FileName}";
                    files.Add(file.FileName);
                    await SaveAsAsync(filePath, file);
                }
            }

            if (document.AppendixFiles != null && document.AppendixFiles.Any())
            {
                int index = 0;
                foreach (IFormFile file in document.AppendixFiles)
                {
                    string filePath = $"{folderPath}/{file.FileName}";
                    await SaveAsAsync(filePath, file);
                    document.Appendixes[index].FolderName = folderPath.Replace(uploadFolderPath, string.Empty);
                    document.Appendixes[index].LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(folderPath, string.Empty)}/{file.FileName}";
                    index++;
                }
            }
            return string.Join(";", files);
        }

        [UnitOfWork]
        protected async Task<string> UploadDocumentsAsync(string folderPath, UpdateDocumentDto document)
        {
            List<string> files = new List<string>();
            folderPath = string.IsNullOrEmpty(folderPath) ? await GetFolderPathAsync(document.Code) : folderPath;

            string uploadFolderPath = await SettingProvider.GetOrNullAsync(DocumentManagementSettings.UploadFilePath);
            if (document.Files != null && document.Files.Any())
            {
                foreach (IFormFile file in document.Files)
                {
                    string filePath = $"{uploadFolderPath}/{folderPath}/{file.FileName}";
                    document.FolderName = folderPath.Replace(uploadFolderPath, string.Empty);
                    document.LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(folderPath, string.Empty)}/{file.FileName}";
                    files.Add(file.FileName);

                    await SaveAsAsync(filePath, file);
                }
            }

            if (document.AppendixFiles != null && document.AppendixFiles.Any())
            {
                int index = 0;
                foreach (IFormFile file in document.AppendixFiles)
                {
                    string filePath = $"{uploadFolderPath}/{folderPath}/{file.FileName}";
                    await SaveAsAsync(filePath, file);
                    document.Appendixes[index].FolderName = folderPath.Replace(uploadFolderPath, string.Empty);
                    document.Appendixes[index].LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(folderPath, string.Empty)}/{file.FileName}";
                    index++;
                }
            }
            return string.Join(";", files);
        }
    }
}
