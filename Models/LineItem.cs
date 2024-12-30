using System.ComponentModel.DataAnnotations.Schema;

namespace MTCBackend.Models
{
    public class LineItem
    {
        public int Id { get; set; }
        public int BillId { get; set; }  // Foreign key to Bill
        public Bill Bill { get; set; }   // Navigation property
        public string Description { get; set; }  // Description of the charge or service

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }  // Amount for the line item
    }
}
