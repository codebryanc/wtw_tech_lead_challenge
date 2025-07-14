using System.ComponentModel.DataAnnotations;

namespace Entity.Schemas
{
    public class VacationRequestSchema
    {
        // [Properties]
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        [Required]
        [Range(1, 365)]
        public int DaysRequested { get; set; }
        
        [MaxLength(500)]
        public string? Reason { get; set; }
        
        public bool IsEmergency { get; set; }
    }
}