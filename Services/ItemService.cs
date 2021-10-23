using DomainModel;
using Microsoft.AspNetCore.Identity;
using Repositories.ItemRepository;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ItemService : IItemService
    {
        public readonly IItemRepository repository;

        public ItemService(IItemRepository _itemRepository)
        {
            repository = _itemRepository;
        }

        public void Add(ItemDTO itemDTO, IdentityUser user)
        {
            repository.Save(new Item { 
                ItemId = itemDTO.ItemId, 
                Text = itemDTO.Text,
                CreatedBy = user.Id,
                DateCreated = DateTime.UtcNow
            });
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
                lineItem.ItemId = (int)repoData.ItemId;
                lineItem.Text = repoData.Text;
                lineItem.CreatedBy = repoData.CreatedBy;
                lineItem.DateCreated = repoData.DateCreated;
            }
            return lineItem;
        }

        public IEnumerable<ItemDTO> GetAll()
        {
            return repository.All().Select(e => new ItemDTO { ItemId = (int)e.ItemId, Text = e.Text, CreatedBy = e.CreatedBy, DateCreated = e.DateCreated }).AsEnumerable();
        }

        public IEnumerable<ItemDTO> GetAllByFilter(ItemByFilterDTO filters)
        {

            string sText = filters.itemFilter.TryGetValue("text", out sText) ? sText : string.Empty;
            string sId = filters.itemFilter.TryGetValue("id", out sId) ? sId : string.Empty;

            int key = 0;
            int.TryParse(sId, out key);
            
            return repository.All().Where(e => e.ItemId == key || e.Text == sText).
                             Select(e => new ItemDTO { ItemId = e.ItemId, Text = e.Text });
        }

        public void Update(ItemDTO itemDTO)
        {
            repository.Save(new Item { ItemId = itemDTO.ItemId, Text = itemDTO.Text });
        }

        public bool ItemExist(int itemId)
        {
            return repository.All().Any(i => i.ItemId == itemId);
        }
    }
}
