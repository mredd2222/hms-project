using Microsoft.AspNetCore.Mvc;
using MTCBackend.Interfaces;

namespace MTCBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientDashboardService _dashboardService;

        public PatientController(IPatientDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // GET: api/patient/{id}/dashboard
        [HttpGet("{id}/dashboard")]
        public async Task<IActionResult> GetDashboard(Guid id)
        {
            var dashboardData = await _dashboardService.GetPatientDashboardAsync(id);

            if (dashboardData == null)
            {
                return NotFound("Patient dashboard data not found.");
            }

            return Ok(dashboardData);
        }
    }

}
