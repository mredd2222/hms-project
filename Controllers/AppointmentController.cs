using Microsoft.AspNetCore.Mvc;
using MTCBackend.DTOs;
using MTCBackend.Models;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    // GET: api/appointment
    // Retrieves all appointments, including patient and staff information
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
    {
        var appointments = await _appointmentService.GetAllAppointments();
        return Ok(appointments);
    }

    // GET: api/appointment/{id}
    // Retrieves a specific appointment by its ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> GetAppointment(int id)
    {
        var appointment = await _appointmentService.GetAppointmentById(id);

        if (appointment == null)
        {
            return NotFound();
        }

        return Ok(appointment);
    }

    // POST: api/appointment
    // Creates a new appointment, associating it with both a patient and a staff member
    [HttpPost]
    public async Task<ActionResult<Appointment>> AddAppointment(AppointmentDTO appointmentDto)
    {
        // Map the DTO to the Appointment model
        var appointment = new Appointment
        {
            AppointmentDate = appointmentDto.AppointmentDate,
            Description = appointmentDto.Description,
            PatientId = appointmentDto.PatientId,
            StaffId = appointmentDto.StaffId
        };

        var createdAppointment = await _appointmentService.AddAppointment(appointment);

        if (createdAppointment == null)
        {
            return BadRequest("Invalid Patient or Staff.");
        }

        return CreatedAtAction(nameof(GetAppointment), new { id = createdAppointment.Id }, createdAppointment);
    }

    // PUT: api/appointment/{id}
    // Updates an existing appointment by ID
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment(int id, Appointment appointment)
    {
        if (id != appointment.Id)
        {
            return BadRequest("Appointment ID mismatch.");
        }

        var result = await _appointmentService.UpdateAppointment(appointment);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/appointment/{id}
    // Deletes an appointment by ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var result = await _appointmentService.DeleteAppointment(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
