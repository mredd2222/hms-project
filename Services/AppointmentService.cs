using Microsoft.EntityFrameworkCore;
using MTCBackend.Data;
using MTCBackend.Interfaces;
using MTCBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AppointmentService : IAppointmentService
{
    private readonly MTCContext _context;

    public AppointmentService(MTCContext context)
    {
        _context = context;
    }

    // Retrieves all appointments, including associated patient and staff information
    public async Task<IEnumerable<Appointment>> GetAllAppointments()
    {
        return await _context.Appointments
            .Include(a => a.Patient)  // Include the patient details
            .Include(a => a.Staff)    // Include the staff details
            .ToListAsync();
    }

    // Retrieves a specific appointment by ID
    public async Task<Appointment> GetAppointmentById(int id)
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Staff)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    // Adds a new appointment, ensuring both patient and staff exist
    public async Task<Appointment> AddAppointment(Appointment appointment)
    {
        // Ensure the patient and staff exist
        var patient = await _context.Users.FindAsync(appointment.PatientId);
        var staff = await _context.Users.FindAsync(appointment.StaffId);

        // Ensure that the patient is a Patient and the staff is Staff
        if (patient == null || staff == null || patient.Role != UserType.Patient || staff.Role != UserType.Staff)
        {
            return null;  // Validation fails, return null (can also throw an exception if desired)
        }

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    // Updates an existing appointment
    public async Task<bool> UpdateAppointment(Appointment appointment)
    {
        // Ensure the appointment exists
        var existingAppointment = await _context.Appointments.FindAsync(appointment.Id);
        if (existingAppointment == null)
        {
            return false;
        }

        // Ensure that the patient and staff exist
        var patient = await _context.Users.FindAsync(appointment.PatientId);
        var staff = await _context.Users.FindAsync(appointment.StaffId);

        if (patient == null || staff == null || patient.Role != UserType.Patient || staff.Role != UserType.Staff)
        {
            return false;
        }

        // Update appointment fields
        existingAppointment.AppointmentDate = appointment.AppointmentDate;
        existingAppointment.Description = appointment.Description;
        existingAppointment.PatientId = appointment.PatientId;
        existingAppointment.StaffId = appointment.StaffId;

        // Save changes
        _context.Entry(existingAppointment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    // Deletes an appointment by ID
    public async Task<bool> DeleteAppointment(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            return false;
        }

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
        return true;
    }
}
