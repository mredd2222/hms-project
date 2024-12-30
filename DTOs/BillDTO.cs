namespace MTCBackend.DTOs
{
    public class BillDTO
    {
        public int Id { get; set; }

        public Guid PatientId { get; set; }  // PatientId added here
        public DateTime CreatedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        public List<LineItemDTO> LineItems { get; set; }
    }

}
