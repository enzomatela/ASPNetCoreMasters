using DomainModel;
using Repositories.ItemRepository;
using Services.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ItemService : IItemService
    {
        IItemRepository repository = new ItemRepository();

        public void Add(ItemDTO itemDto)
        {
            repository.Save(new Item { ItemId = itemDto.ItemId, Text = itemDto.Text });
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public ItemDTO Get(int itemId)
        {
            ItemDTO lineItem = new ItemDTO();
            var repoData = repository.All().Where(e => e.ItemId == itemId).FirstOrDefault();
            if (repoData != null)
            {
                lineItem.ItemId = repoData.ItemId;
                lineItem.Text = repoData.Text;
            }
            return lineItem;
        }

        public IEnumerable<ItemDTO> GetAll()
        {
            return repository.All().Select(e => new ItemDTO { ItemId = e.ItemId, Text = e.Text }).AsEnumerable();
        }

        public IEnumerable<ItemDTO> GetAllByFilter(ItemByFilterDTO filters)
        {
            return Enumerable.Empty<ItemDTO>();
        }

        public void Update(ItemDTO itemDTO)
        {
           
        }
    }
}
