namespace MTCBackend.Models
{
    public class LabResult
    {
        public Guid Id { get; set; }  // Primary key
        public Guid PatientId { get; set; }  // Foreign key to Patient
        public string TestName { get; set; }  // Name of the lab test (e.g., "Glucose", "Cholesterol")
        public string Result { get; set; }  // Test result value
        public DateTime Date { get; set; }  // Date of the test

        // Navigation property
        public Patient Patient { get; set; }
    }
}
