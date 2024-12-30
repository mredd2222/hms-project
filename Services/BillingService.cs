using Microsoft.EntityFrameworkCore;
using MTCBackend.Data;
using MTCBackend.DTOs;
using MTCBackend.Interfaces;
using MTCBackend.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MTCBackend.Services
{
    public class BillingService : IBillingService
    {
        private readonly MTCContext _context;

        public BillingService(MTCContext context)
        {
            _context = context;
        }

        // Create a new bill from the BillDTO
        public async Task<Bill> CreateBill(BillDTO billDto)
        {
            var bill = new Bill
            {
                PatientId = billDto.PatientId,
                CreatedDate = DateTime.UtcNow,
                TotalAmount = billDto.LineItems.Sum(item => item.Amount),
                LineItems = billDto.LineItems.Select(item => new LineItem
                {
                    Description = item.Description,
                    Amount = item.Amount
                }).ToList(),
                Status = "Pending"
            };

            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            return bill;
        }

        // Apply a payment to an existing bill
        public async Task ApplyPayment(int billId, PaymentDTO paymentDto)
        {
            var bill = await _context.Bills.Include(b => b.Payments).FirstOrDefaultAsync(b => b.Id == billId);
            if (bill == null)
                throw new Exception("Bill not found");

            var payment = new Payment
            {
                BillId = billId,
                PaymentDate = DateTime.UtcNow,
                Amount = paymentDto.Amount
            };

            bill.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        // Get the ledger for a specific patient
        public async Task<Ledger> GetLedger(Guid patientId)
        {
            return await _context.Ledgers.FirstOrDefaultAsync(l => l.PatientId == patientId);
        }

        // Get a bill by its ID and return a BillDTO
        public async Task<BillDTO> GetBillById(int id)
        {
            var bill = await _context.Bills
                .Include(b => b.LineItems)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bill == null)
            {
                return null;
            }

            // Map the Bill to BillDTO
            var billDto = new BillDTO
            {
                Id = bill.Id,
                PatientId = bill.PatientId,
                CreatedDate = bill.CreatedDate,
                TotalAmount = bill.TotalAmount,
                Status = bill.Status,
                LineItems = bill.LineItems.Select(item => new LineItemDTO
                {
                    Id = item.Id,
                    Description = item.Description,
                    Amount = item.Amount
                }).ToList()
            };

            return billDto;
        }
    }
}
