using System.ComponentModel.DataAnnotations;

namespace Entity.Schemas
{
    public class PermissionRequestSchema
    {
        // [Properties]
        [Required]
        public DateTime StartTime { get; set; }
        
        [Required]
        public DateTime EndTime { get; set; }
        
        [Required]
        [MaxLength(300)]
        public string Reason { get; set; }
        
        public bool IsHalfDay { get; set; }
        
        [MaxLength(100)]
        public string? ContactPerson { get; set; }
    }
}