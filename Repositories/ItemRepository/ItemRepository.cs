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
            context.Items.RemoveAt(id - 1);
            context.Items = context.Items;
        }

        public void Save(Item item)
        {
            if (item != null)
            {
                context.Items.Where(e => e.ItemId == item.ItemId).FirstOrDefault().Text = item.Text;
            } else
            {
                context.Items.Add(item);
                context.Items = context.Items;
            }
            
        }
    }
}