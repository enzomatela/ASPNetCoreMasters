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
        public ItemServices itemServices; 
        public ItemsController()
        {
            itemServices = new ItemServices();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(itemServices.GetAll());
        }

        [HttpGet("/{itemId}")]
        public IActionResult Get(int itemId)
        {
            return Ok(itemServices.GetAllById(itemId));
        }

        [HttpGet]
        [Route("/FilterBy")]
        public IActionResult GetByFilters([FromQuery] Dictionary<string, string> filters)
        {
            return Ok(itemServices.GetByFilters(filters));
        }

        [HttpPost]
        public IActionResult Post([FromBody] ItemCreateBindingModel model)
        {
            if (ModelState.IsValid)
            {
                itemServices.Save(new ItemDTO { Text = model.Text });
            }

            return Ok();
        }

        [HttpPut("/{itemId}")]
        public IActionResult Put(int itemId, [FromBody] ItemUpdateBindingModel itemUpdateModel)
        {
            itemServices.Update(itemId, new ItemDTO { Text = itemUpdateModel.Text });
            return Ok();
        }

        [HttpDelete("/{itemId}")]
        public IActionResult Delete(int itemId)
        {
            itemServices.Delete();
            return Ok();
        }
    }
}
