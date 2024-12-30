namespace MTCBackend.DTOs
{
    public class AppointmentDTO
    {
        public DateTime AppointmentDate { get; set; }

        public string Description { get; set; }

        public Guid PatientId { get; set; }  // ID of the patient

        public Guid StaffId { get; set; }    // ID of the staff member
    }

}
