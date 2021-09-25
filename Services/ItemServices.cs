using DomainModel;
using Services.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ItemServices
    {
        public int GetAll()
        {
            return 0;
        }

        public int GetAllById(int Id)
        {
            return 0;
        }

        public int GetByFilters(Dictionary<string, string> filters)
        {
            return 0;
        }

        public void Save(ItemDTO itemDTO)
        {
            Item item = itemDTO;
        }

        public void Update(int itemId, ItemDTO itemDTO)
        {

        }

        public void Delete()
        {
            
        }

    }
}
