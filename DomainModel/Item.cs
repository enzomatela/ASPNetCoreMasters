using System;

namespace DomainModel
{
    public class Item
    { 
        public int? ItemId { get; set; }
        public string Text { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
