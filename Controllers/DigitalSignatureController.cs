using Microsoft.AspNetCore.Mvc;
using MTCBackend.Interfaces;

namespace MTCBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DigitalSignatureController : ControllerBase
    {
        private readonly IDigitalSignatureService _digitalSignatureService;

        public DigitalSignatureController(IDigitalSignatureService digitalSignatureService)
        {
            _digitalSignatureService = digitalSignatureService;
        }

        // POST: api/digitalsignature/capture
        [HttpPost("capture")]
        public async Task<IActionResult> CaptureSignature([FromQuery] Guid formId, [FromQuery] Guid patientId, [FromBody] string base64SignatureData)
        {
            if (string.IsNullOrEmpty(base64SignatureData))
            {
                return BadRequest("Invalid signature data.");
            }

            var signatureId = await _digitalSignatureService.CaptureSignatureAsync(formId, patientId, base64SignatureData);

            return Ok(new { SignatureId = signatureId });
        }

        // POST: api/digitalsignature/verify/{signatureId}
        [HttpPost("verify/{signatureId}")]
        public async Task<IActionResult> VerifySignature(Guid signatureId, [FromBody] string signatureHash)
        {
            var isValid = await _digitalSignatureService.VerifySignatureAsync(signatureId, signatureHash);

            if (!isValid)
            {
                return BadRequest("Signature verification failed.");
            }

            return Ok("Signature verified successfully.");
        }
    }

}
