using Microsoft.AspNetCore.Mvc;
using MTCBackend.DTOs;
using MTCBackend.Interfaces;
using MTCBackend.Models;
using System.Threading.Tasks;

namespace MTCBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        // POST: api/billing
        [HttpPost]
        public async Task<ActionResult<Bill>> CreateBill(BillDTO billDto)
        {
            var createdBill = await _billingService.CreateBill(billDto);
            return CreatedAtAction(nameof(GetBill), new { id = createdBill.Id }, createdBill);
        }

        // POST: api/billing/{billId}/payment
        [HttpPost("{billId}/payment")]
        public async Task<IActionResult> ApplyPayment(int billId, PaymentDTO paymentDto)
        {
            await _billingService.ApplyPayment(billId, paymentDto);
            return NoContent();
        }

        // GET: api/billing/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Bill>> GetBill(int id)
        {
            var bill = await _billingService.GetBillById(id);

            if (bill == null)
            {
                return NotFound();
            }

            return Ok(bill);
        }

        // GET: api/billing/ledger/{patientId}
        [HttpGet("ledger/{patientId}")]
        public async Task<ActionResult<Ledger>> GetLedger(Guid patientId)
        {
            var ledger = await _billingService.GetLedger(patientId);
            return Ok(ledger);
        }
    }
}
