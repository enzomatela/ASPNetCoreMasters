using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Services;


namespace Masters.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : Controller
    {
        public ItemServices itemServices; 
        public ItemsController()
        {
            itemServices = new ItemServices();
        }

        [HttpGet]
        public IEnumerable<string> Get(int Id)
        {
            return itemServices.GetAll(Id);
        }
    }
}
