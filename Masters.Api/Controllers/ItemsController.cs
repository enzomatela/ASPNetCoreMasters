using Masters.Api.BindingModels;
using Masters.Api.Filters;
using Microsoft.AspNetCore.Mvc;

using Services;
using Services.DTO;
using System.Collections.Generic;

namespace Masters.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ItemExist]
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

        [HttpGet("{itemId}")]
        public IActionResult Get(int itemId)   
        {
            return Ok(itemServices.Get(itemId));
        }

        [HttpGet]
        [Route("FilterBy")]
        public IActionResult GetByFilters([FromQuery] Dictionary<string, string> filters)
        {
            ItemByFilterDTO itemFilter = new ItemByFilterDTO();
            itemFilter.itemFilter = filters;
            return Ok(itemServices.GetAllByFilter(itemFilter));
        }

        [HttpPost]
        public IActionResult Post([FromBody] ItemCreateBindingModel model)
        {
            if (ModelState.IsValid)
            {
                itemServices.Add(new ItemDTO { Text = model.Text });
            }

            return Ok();
        }

        [HttpPut("{itemId}")]
        public IActionResult Put(int itemId, [FromBody] ItemUpdateBindingModel itemUpdateModel)
        {
            itemServices.Update(new ItemDTO { ItemId = itemId ,Text = itemUpdateModel.Text });
            return Ok();
        }

        [HttpDelete("{itemId}")]
        public IActionResult Delete(int itemId)
        {
            itemServices.Delete(itemId);
            return Ok();
        }
    }
}
