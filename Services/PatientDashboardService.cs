using Microsoft.EntityFrameworkCore;
using MTCBackend.Data;
using MTCBackend.DTOs;
using MTCBackend.Interfaces;

namespace MTCBackend.Services
{
    public class PatientDashboardService : IPatientDashboardService
    {
        private readonly MTCContext _context;

        public PatientDashboardService(MTCContext context)
        {
            _context = context;
        }

        public async Task<PatientDashboardDTO> GetPatientDashboardAsync(Guid patientId)
        {
            var patientDashboard = new PatientDashboardDTO
            {
                PatientId = patientId,
                NextAppointment = await GetNextAppointmentAsync(patientId),
                Stats = await GetPatientStatsAsync(patientId),
                RecentLabResults = await GetRecentLabResultsAsync(patientId),
                QuickLinks = GetQuickLinks(patientId)
            };

            return patientDashboard;
        }

        private async Task<AppointmentOverviewDTO> GetNextAppointmentAsync(Guid patientId)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId && a.AppointmentDate > DateTime.UtcNow)
                .OrderBy(a => a.AppointmentDate)
                .Select(a => new AppointmentOverviewDTO
                {
                    AppointmentDate = a.AppointmentDate,
                    ProviderName = a.Staff.FirstName + a.Staff.LastName,
                })
                .FirstOrDefaultAsync();
        }

        private async Task<PatientStatsDTO> GetPatientStatsAsync(Guid patientId)
        {
            var patientStats = await _context.Patients
                .Where(p => p.Id == patientId)
                .Select(p => new PatientStatsDTO
                {
                    Height = p.Height,
                    Weight = p.Weight,
                    BMI = CalculateBMI(p.Weight, p.Height),
                    BloodPressure = p.BloodPressure
                })
                .FirstOrDefaultAsync();

            return patientStats;
        }

        private async Task<List<LabResultDTO>> GetRecentLabResultsAsync(Guid patientId)
        {
            return await _context.LabResults
                .Where(lr => lr.PatientId == patientId)
                .OrderByDescending(lr => lr.Date)
                .Take(5)
                .Select(lr => new LabResultDTO
                {
                    TestName = lr.TestName,
                    Result = lr.Result,
                    Date = lr.Date
                })
                .ToListAsync();
        }

        private QuickLinksDTO GetQuickLinks(Guid patientId)
        {
            return new QuickLinksDTO
            {
                MedicationsUrl = $"/api/patient/{patientId}/medications",
                LabResultsUrl = $"/api/patient/{patientId}/labresults",
                ClinicalOrdersUrl = $"/api/patient/{patientId}/clinicalorders",
                ProgressNotesUrl = $"/api/patient/{patientId}/progressnotes"
            };
        }

        private float CalculateBMI(float weight, float height)
        {
            if (height <= 0) return 0;
            return weight / ((height / 100) * (height / 100));  // Assuming height in cm, weight in kg
        }
    }
}
