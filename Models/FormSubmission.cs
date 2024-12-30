using System;
using System.Collections.Generic;

namespace MTCBackend.Models
{
    public class FormSubmission
    {
        public Guid Id { get; set; }
        public Guid FormId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime SubmissionDate { get; set; }

        // Navigation properties
        public Form? Form { get; set; }
        public Patient? Patient { get; set; }

        public Guid? SignatureId { get; set; }  // Nullable, as it's optional

        // One-to-One relationship with DigitalSignature
        public DigitalSignature? Signature { get; set; }

        // PDF File Upload
        public ICollection<UploadedFile> UploadedFiles { get; set; } = new List<UploadedFile>();  // Allow multiple uploaded files

    }
}
