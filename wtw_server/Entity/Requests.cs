using System;

namespace Entity
{
    public class Requests
    {
        // [Properties]
        public Guid reqId { get; set; }
        public Guid rtyId { get; set; }
        public Guid resId { get; set; }
        public DateTime createdAt { get; set; }
        public string? data { get; set; }
    }
}