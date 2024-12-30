using Microsoft.AspNetCore.Mvc;
using MTCBackend.DTOs;
using MTCBackend.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTCBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;

        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        // GET: api/form/{formId}
        [HttpGet("{formId}")]
        public async Task<ActionResult<FormDTO>> GetFormById(Guid formId)
        {
            var form = await _formService.GetFormById(formId);
            if (form == null)
            {
                return NotFound();
            }
            return Ok(form);
        }

        // GET: api/form/patient/{patientId}
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<List<FormDTO>>> GetFormsByPatientId(Guid patientId)
        {
            var forms = await _formService.GetFormsByPatientId(patientId);
            if (forms == null || forms.Count == 0)
            {
                return NotFound();
            }
            return Ok(forms);
        }

        // POST: api/form
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateForm(FormDTO formDto)
        {
            var formId = await _formService.CreateForm(formDto);
            return CreatedAtAction(nameof(GetFormById), new { formId }, formId);
        }

        // POST: api/form/submission
        [HttpPost("submission")]
        public async Task<ActionResult> CreateFormSubmission(FormSubmissionDTO submissionDto)
        {
            await _formService.CreateFormSubmission(submissionDto);
            return NoContent();
        }
    }
}
