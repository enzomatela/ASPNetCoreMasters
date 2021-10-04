using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IItemService
    {
        IEnumerable<ItemDTO> GetAll();
        IEnumerable<ItemDTO> GetAllByFilter(ItemByFilterDTO filters);
        ItemDTO Get(int itemId);
        void Add(ItemDTO itemDto);
        void Update(ItemDTO itemDTO);
        void Delete(int id);

    }
}
