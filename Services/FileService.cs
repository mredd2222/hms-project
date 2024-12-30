using Microsoft.AspNetCore.Mvc;
using MTCBackend.Data;
using MTCBackend.Interfaces;
using MTCBackend.Models;

namespace MTCBackend.Services
{
    public class FileService : IFileService
    {
        private readonly MTCContext _context;
        private readonly IWebHostEnvironment _environment;

        public FileService(MTCContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<Guid> UploadPdfAsync(IFormFile file, Guid formId, Guid patientId)
        {
            // Validate file type and size
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty.");
            }

            if (file.ContentType != "application/pdf")
            {
                throw new ArgumentException("Only PDF files are allowed.");
            }

            var maxFileSizeInMB = 10;
            if (file.Length > maxFileSizeInMB * 1024 * 1024)
            {
                throw new ArgumentException($"File size exceeds {maxFileSizeInMB}MB.");
            }

            // Set up file storage path
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Save the file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Store file info in the database
            var uploadedFile = new UploadedFile
            {
                FileName = file.FileName,
                FilePath = filePath,
                FormId = formId,
                PatientId = patientId,
                UploadTimestamp = DateTime.UtcNow
            };

            _context.UploadedFiles.Add(uploadedFile);
            await _context.SaveChangesAsync();

            return uploadedFile.Id;
        }

        public async Task<UploadedFile> GetUploadedFileAsync(Guid fileId)
        {
            return await _context.UploadedFiles.FindAsync(fileId);
        }

        public async Task<FileResult> DownloadFileAsync(Guid fileId)
        {
            var file = await GetUploadedFileAsync(fileId);
            if (file == null)
            {
                throw new FileNotFoundException("File not found.");
            }

            var fileBytes = await File.ReadAllBytesAsync(file.FilePath);
            return new FileContentResult(fileBytes, "application/pdf")
            {
                FileDownloadName = file.FileName
            };
        }
    }

}
