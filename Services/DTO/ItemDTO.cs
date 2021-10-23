using DomainModel;
using System;

namespace Services.DTO
{
    public class ItemDTO
    {
        public int? ItemId { get; set; }
        public string Text { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
