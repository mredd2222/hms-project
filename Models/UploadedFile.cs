using System;

namespace MTCBackend.Models
{
    public class UploadedFile
    {
        public Guid Id { get; set; } // Primary key

        public string? FilePath { get; set; } // Path to the stored file
        public string? FileName { get; set; } // Original file name
        public DateTime UploadTimestamp { get; set; } = DateTime.UtcNow; // Timestamp for file upload

        // Foreign key to FormSubmission
        public Guid FormSubmissionId { get; set; }
        public FormSubmission? FormSubmission { get; set; } // Navigation property

        // Foreign keys to link to Patient and Form
        public Guid PatientId { get; set; } // Foreign key to Patient
        public Patient? Patient { get; set; } // Navigation property to Patient

        public Guid FormId { get; set; } // Foreign key to Form
        public Form? Form { get; set; } // Navigation property to Form
    }

}
