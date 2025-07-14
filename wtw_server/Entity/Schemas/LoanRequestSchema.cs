using System.ComponentModel.DataAnnotations;

namespace Entity.Schemas
{
    public class LoanRequestSchema
    {
        // [Properties]
        [Required]
        [Range(1, 1000000)]
        public decimal Amount { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Purpose { get; set; }
        
        [Required]
        [Range(1, 60)]
        public int PaymentMonths { get; set; }
        
        public decimal MonthlyIncome { get; set; }
        
        [MaxLength(100)]
        public string? Guarantor { get; set; }
    }
}