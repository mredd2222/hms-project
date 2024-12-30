using MTCBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAppointmentService
{
    Task<IEnumerable<Appointment>> GetAllAppointments();
    Task<Appointment> GetAppointmentById(int id);
    Task<Appointment> AddAppointment(Appointment appointment);
    Task<bool> UpdateAppointment(Appointment appointment);
    Task<bool> DeleteAppointment(int id);
}
