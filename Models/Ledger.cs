using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTCBackend.Models
{
    public class Ledger
    {
        [Key]
        public int Id { get; set; }
        public Guid PatientId { get; set; }  // Foreign key to User (patient)

        [ForeignKey("PatientId")]  // Explicitly define the foreign key relationship
        public User Patient { get; set; }   // Navigation property

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalCharges { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPayments { get; set; }
        public decimal OutstandingBalance => TotalCharges - TotalPayments;
    }


}
