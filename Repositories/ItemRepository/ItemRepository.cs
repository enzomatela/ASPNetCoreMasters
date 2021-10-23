using DomainModel;
using System.Linq;

namespace Repositories.ItemRepository
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataDBContext context;

        //public ItemRepository(DataContext datacontext)
        //{
        //    context = datacontext;
        //}

        public ItemRepository(DataDBContext dataContext)
        {
            context = dataContext;
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
                var selectedItem = this.context.Items.Where(i => i.ItemId == item.ItemId).FirstOrDefault();
                if (selectedItem != null)
                {
                    selectedItem.Text = item.Text;
                    this.context.SaveChanges();
                }
               
            }
            else
            {
                this.context.Items.Add(item);
                this.context.SaveChanges();
            }
            
        }
    }
}