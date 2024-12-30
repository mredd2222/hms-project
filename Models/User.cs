namespace MTCBackend.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public UserType Role { get; set; }  // This field distinguishes between staff and patients

        // Additional properties for specific roles can be added here, such as:
        public string? MedicalRecordNumber { get; set; }  // Specific to patients

        public string? StaffPosition { get; set; }  // Specific to staff

        // Navigation property for appointments if User represents a provider
        // Navigation properties
        public ICollection<Appointment> PatientAppointments { get; set; } = new List<Appointment>();
        public ICollection<Appointment> StaffAppointments { get; set; } = new List<Appointment>();
    }

    public enum UserType
    {
        Patient,
        Staff
    }
}
