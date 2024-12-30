using MTCBackend.DTOs;

namespace MTCBackend.Interfaces
{
    public interface IPatientDashboardService
    {
        Task<PatientDashboardDTO> GetPatientDashboardAsync(Guid patientId);
    }
}
