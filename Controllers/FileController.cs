using Microsoft.AspNetCore.Mvc;
using MTCBackend.Interfaces;

namespace MTCBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        // POST: api/file/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadPdf(IFormFile file, [FromQuery] Guid formId, [FromQuery] Guid patientId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            try
            {
                var fileId = await _fileService.UploadPdfAsync(file, formId, patientId);
                return Ok(new { FileId = fileId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/file/download/{fileId}
        [HttpGet("download/{fileId}")]
        public async Task<IActionResult> DownloadPdf(Guid fileId)
        {
            try
            {
                var fileResult = await _fileService.DownloadFileAsync(fileId);
                return fileResult;
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }
    }

}
