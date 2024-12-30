using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace MTCBackend.Models
{
    public class Form
    {
        public Guid Id { get; set; } // Form ID (Primary Key)
        public required string Name { get; set; } // Form Name
        public ICollection<FormField> Fields { get; set; } = new List<FormField>(); // Linked form fields
        public ICollection<FormSubmission> Submissions { get; set; } = new List<FormSubmission>(); // Submissions linked to this form
        public Guid PatientId { get; set; } // Foreign key to Patient
        public Patient Patient { get; set; } // Navigation property to Patient
        public ICollection<UploadedFile> UploadedFiles { get; set; } = new List<UploadedFile>();
        public ICollection<DigitalSignature> DigitalSignatures { get; set; } = new List<DigitalSignature>();
    }
}
