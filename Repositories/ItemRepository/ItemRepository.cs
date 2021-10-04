using DomainModel;
using System.Linq;

namespace Repositories.ItemRepository
{
    public class ItemRepository : IItemRepository
    {
        DataContext context;

        public ItemRepository(DataContext datacontext)
        {
            context = datacontext;
        }

        public IQueryable<Item> All()
        { 
            return context.Items.AsQueryable();
        }

        public void Delete(int id)
        {
            var itemToRemove = context.Items.Single(r => r.ItemId == id);
            context.Items.Remove(itemToRemove);
        }

        public void Save(Item item)
        {
            if (item.ItemId != null)
            {
                context.Items.Where(e => e.ItemId == item.ItemId).FirstOrDefault().Text = item.Text;
            }
            else
            {
                item.ItemId = context.Items.OrderByDescending(e => e.ItemId).FirstOrDefault().ItemId + 1;
                context.Items.Add(item);
            }
            
        }
    }
}