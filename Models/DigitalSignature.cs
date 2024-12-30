using System;

namespace MTCBackend.Models
{
    public class DigitalSignature
    {
        public Guid Id { get; set; }

        // Assuming the signature is stored as a string or blob data
        public string Base64SignatureData { get; set; }  // Store the signature as Base64-encoded string
        public DateTime SignedDate { get; set; }  // Timestamp for when the signature was collected
        public string Hash { get; set; }  // Hash for cryptographic verification

        public Guid FormId { get; set; }  // Foreign key to Form
        public Form Form { get; set; }  // Navigation property

        public Guid PatientId { get; set; }  // Foreign key to Patient
        public Patient Patient { get; set; }  // Navigation property

        // Foreign key for FormSubmission
        public Guid FormSubmissionId { get; set; }

        // Navigation property
        public FormSubmission? FormSubmission { get; set; }
    }
}
