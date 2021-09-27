using Masters.Api.BindingModels;
using Microsoft.AspNetCore.Mvc;

using Services;
using Services.DTO;
using System.Collections.Generic;

namespace Masters.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : Controller
    {
        private readonly IItemService itemServices; 

        public ItemsController(IItemService _itemService)
        {
            itemServices = _itemService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(itemServices.GetAll());
        }

        [HttpGet("/items/{itemId}")]
        public IActionResult Get(int itemId)
        {
            return Ok(itemServices.Get(itemId));
        }

        [HttpGet]
        [Route("/items/FilterBy")]
        public IActionResult GetByFilters([FromQuery] Dictionary<string, string> filters)
        {
            ItemByFilterDTO itemFilter = new ItemByFilterDTO();
            itemFilter.itemFilter = filters;
            return Ok(itemServices.GetAllByFilter(itemFilter));
        }

        [HttpPost("/items")]
        public IActionResult Post([FromBody] ItemCreateBindingModel model)
        {
            if (ModelState.IsValid)
            {
                itemServices.Add(new ItemDTO { ItemId = model.ItemId,Text = model.Text });
            }

            return Ok();
        }

        [HttpPut("/items/{itemId}")]
        public IActionResult Put(int itemId, [FromBody] ItemUpdateBindingModel itemUpdateModel)
        {
            itemServices.Update(new ItemDTO { ItemId = itemId ,Text = itemUpdateModel.Text });
            return Ok();
        }

        [HttpDelete("/items/{itemId}")]
        public IActionResult Delete(int itemId)
        {
            itemServices.Delete(itemId);
            return Ok();
        }
    }
}
