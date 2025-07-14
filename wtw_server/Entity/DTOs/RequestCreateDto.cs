using System.ComponentModel.DataAnnotations;

namespace Entity.DTOs
{
    public class RequestCreateDto
    {
        // [Properties]
        [Required]
        public Guid RequestTypeId { get; set; }
        
        [Required]
        public Guid RequestStatusId { get; set; }
        
        [Required]
        public object DynamicData { get; set; }
    }
}