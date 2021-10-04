using DomainModel;
using System.Collections.Generic;

namespace Services.DTO
{
    public class ItemByFilterDTO 
    {
        public Dictionary<string, string> itemFilter { get; set; }
    }
}
