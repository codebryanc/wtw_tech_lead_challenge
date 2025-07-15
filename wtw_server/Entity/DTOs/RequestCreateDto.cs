using System.ComponentModel.DataAnnotations;

namespace Entity.DTOs
{
    public class RequestCreateDto
    {
        // [Properties]
        [Required]
        public string? RequestTypeId { get; set; }
        
        [Required]
        public string? RequestStatusId { get; set; }
        
        [Required]
        public object? DynamicData { get; set; }
    }
}