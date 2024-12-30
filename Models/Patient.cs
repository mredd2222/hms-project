using Microsoft.AspNetCore.SignalR.Protocol;
using System;
using System.Collections.Generic;

namespace MTCBackend.Models
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Patient physical stats
        public float Height { get; set; }  // In centimeters or inches
        public float Weight { get; set; }  // In kilograms or pounds
        public string BloodPressure { get; set; }

        // Navigation properties
        public ICollection<Form>? Forms { get; set; }
        public ICollection<FormSubmission>? Submissions { get; set; }
        public ICollection<UploadedFile> UploadedFiles { get; set; } = new List<UploadedFile>();
        public ICollection<DigitalSignature> DigitalSignatures { get; set; } = new List<DigitalSignature>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<LabResult> LabResults { get; set; } = new List<LabResult>();
    }
}
