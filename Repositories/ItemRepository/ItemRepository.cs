using DomainModel;
using System.Linq;

namespace Repositories.ItemRepository
{
    public class ItemRepository : IItemRepository
    {
        DataContext context = new DataContext();


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
            context.Items.Add(item);
            context.Items = context.Items;
        }
    }
}