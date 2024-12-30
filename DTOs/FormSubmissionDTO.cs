using System;
using System.Collections.Generic;

namespace MTCBackend.DTOs
{
    public class FormSubmissionDTO
    {
        public Guid Id { get; set; }
        public Guid FormId { get; set; }
        public Guid PatientId { get; set; }
        public Guid? SignatureId { get; set; }
        public List<UploadedFileDTO> UploadedFiles { get; set; } = new List<UploadedFileDTO>();  // Collection of uploaded files
    }

    public class UploadedFileDTO
    {
        public Guid Id { get; set; }
        public string? FilePath { get; set; }
        public IFormFile File { get; set; }  // This is the uploaded file
    }
}
