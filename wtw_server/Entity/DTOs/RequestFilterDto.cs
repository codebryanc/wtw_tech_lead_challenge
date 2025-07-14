
namespace Entity.DTOs
{
    public class RequestFilterDto
    {
        // [Properties]
        public Guid? RequestTypeId { get; set; }
        public Guid? RequestStatusId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? JsonProperty { get; set; }
        public string? JsonValue { get; set; }
    }
}