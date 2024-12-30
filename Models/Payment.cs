using System.ComponentModel.DataAnnotations.Schema;

namespace MTCBackend.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int BillId { get; set; }  // Foreign key to Bill
        public Bill? Bill { get; set; }   // Navigation property
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }  // Amount paid
    }

}
