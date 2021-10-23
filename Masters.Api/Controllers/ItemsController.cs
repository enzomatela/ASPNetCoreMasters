using Masters.Api.BindingModels;
using Masters.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Services;
using Services.DTO;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Masters.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [ItemExist]
    public class ItemsController : Controller
    {
        private readonly IItemService itemServices;
        private readonly UserManager<IdentityUser> userService;
        private readonly IAuthorizationService authService;

        public ItemsController(IItemService _itemService, IAuthorizationService _authService, UserManager<IdentityUser> _userService)
        {
            itemServices = _itemService;
            authService = _authService;
            userService = _userService;
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
        public async Task<IActionResult> Post([FromBody] ItemCreateBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var email = ((ClaimsIdentity)User.Identity).FindFirst("Email");
                var user = await userService.FindByNameAsync(email.Value);
                itemServices.Add(new ItemDTO() { Text = model.Text }, user);
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
