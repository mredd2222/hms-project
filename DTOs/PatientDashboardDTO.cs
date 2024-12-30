namespace MTCBackend.DTOs
{
    public class PatientDashboardDTO
    {
        public Guid PatientId { get; set; }
        public AppointmentOverviewDTO NextAppointment { get; set; }
        public PatientStatsDTO Stats { get; set; }
        public List<LabResultDTO> RecentLabResults { get; set; } = new List<LabResultDTO>();
        public QuickLinksDTO QuickLinks { get; set; }
    }

    public class AppointmentOverviewDTO
    {
        public DateTime? AppointmentDate { get; set; }
        public string ProviderName { get; set; }
    }

    public class PatientStatsDTO
    {
        public float Height { get; set; }
        public float Weight { get; set; }
        public float BMI { get; set; }
        public string BloodPressure { get; set; }
    }

    public class LabResultDTO
    {
        public string TestName { get; set; }
        public string Result { get; set; }
        public DateTime Date { get; set; }
    }

    public class QuickLinksDTO
    {
        public string MedicationsUrl { get; set; }
        public string LabResultsUrl { get; set; }
        public string ClinicalOrdersUrl { get; set; }
        public string ProgressNotesUrl { get; set; }
    }
}
