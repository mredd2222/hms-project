using System.ComponentModel.DataAnnotations.Schema;

namespace MTCBackend.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public Guid PatientId { get; set; }  // Foreign key to User (patient)
        public User? Patient { get; set; }   // Navigation property
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }  // Sum of all line items
        public string Status { get; set; } = "Pending";  // Status of the bill (e.g., Pending, Paid)
        public ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }

}
