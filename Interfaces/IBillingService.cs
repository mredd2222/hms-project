using MTCBackend.DTOs;
using MTCBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTCBackend.Interfaces
{
    public interface IBillingService
    {
        Task<Bill> CreateBill(BillDTO billDto);
        Task ApplyPayment(int billId, PaymentDTO paymentDto);
        Task<Ledger> GetLedger(Guid patientId);

        // Add this method to the interface
        Task<BillDTO> GetBillById(int id);  // Retrieves a Bill by its ID
    }

}
