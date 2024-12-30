using MTCBackend.Models;

public class Appointment
{
    public int Id { get; set; }  // Primary key
    public DateTime AppointmentDate { get; set; }
    public string Description { get; set; }

    public Guid? PatientId { get; set; }  // Foreign key to User (Patient)
    public User? Patient { get; set; }    // Navigation property for the patient

    public Guid? StaffId { get; set; }    // Foreign key to User (Staff)
    public User? Staff { get; set; }      // Navigation property for the staff
}
