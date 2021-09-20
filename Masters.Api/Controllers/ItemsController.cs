using Masters.Api.BindingModels;
using Microsoft.AspNetCore.Mvc;

using Services;
using Services.DTO;

namespace Masters.Api.Controllers
{
    public class ItemsController : Controller
    {
        public ItemServices itemServices; 
        public ItemsController()
        {
            itemServices = new ItemServices();
        }

        [HttpGet]
        public int Get(int Id)
        {
            return itemServices.GetAll(Id);
        }

        [HttpPost]
        public void Post([FromBody] ItemCreateBindingModel model)
        {
            if (ModelState.IsValid)
            {
                itemServices.Save(new ItemDTO { Text = model.Text });
            }
            
        }
    }
}
