using Microsoft.AspNetCore.Mvc;
using MTCBackend.Models;

namespace MTCBackend.Interfaces
{
    public interface IFileService
    {
        Task<Guid> UploadPdfAsync(IFormFile file, Guid formId, Guid patientId);
        Task<UploadedFile> GetUploadedFileAsync(Guid fileId);
        Task<FileResult> DownloadFileAsync(Guid fileId);
    }
}
