using Masters.Api.BindingModels;
using Masters.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;

namespace Masters.Api.Controllers
{
    [EnableCors("CorsGetOnlyPolicy")]
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [ItemExist]
    public class ItemsController : Controller
    {
        private readonly IItemService itemServices;
        private readonly UserManager<IdentityUser> userService;
        private readonly IAuthorizationService authService;
        private readonly ILogger<ItemsController> logger;

        public ItemsController(ILogger<ItemsController> _logger, IItemService _itemService, IAuthorizationService _authService, UserManager<IdentityUser> _userService)
        {
            itemServices = _itemService;
            authService = _authService;
            userService = _userService;
            logger = _logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            logger.LogInformation("[GetAll] - {RequestDatetime}", DateTime.Now);
            return Ok(itemServices.GetAll());
        }

        [HttpGet("{itemId}")]
        public IActionResult Get(int itemId)
        {
            logger.LogInformation("[Get] - {RequestDatetime} - {Parameter}", DateTime.Now, itemId);
            return Ok(itemServices.Get(itemId));
        }

        [HttpGet]
        [Route("FilterBy")]
        public IActionResult GetByFilters([FromQuery] Dictionary<string, string> filters)
        {
            logger.LogInformation("[GetByFilters] - {RequestDatetime} - {Parameter}", DateTime.Now, filters);
            ItemByFilterDTO itemFilter = new ItemByFilterDTO();
            itemFilter.itemFilter = filters;
            return Ok(itemServices.GetAllByFilter(itemFilter));
        }

        [HttpPost]
        [DisableCors]
        public async Task<IActionResult> Post([FromBody] ItemCreateBindingModel model)
        {
            logger.LogInformation("[Add] - {RequestDatetime} - {Parameter}", DateTime.Now, JsonSerializer.Serialize(model));
            if (ModelState.IsValid)
            {
                var email = ((ClaimsIdentity)User.Identity).FindFirst("Email");
                var user = await userService.FindByNameAsync(email.Value);
                itemServices.Add(new ItemDTO() { Text = model.Text }, user);
            }

            return Ok();
        }

        [HttpPut("{itemId}")]
        [DisableCors]
        public async Task<IActionResult> Put(int itemId, [FromBody] ItemUpdateBindingModel itemUpdateModel)
        {
            logger.LogInformation("[Update] - {RequestDatetime} - {Parameter}", DateTime.Now, JsonSerializer.Serialize(itemUpdateModel));
            var item = itemServices.Get(itemId);
            var isOwner = await authService.AuthorizeAsync(User, new ItemDTO() { CreatedBy = item.CreatedBy }, "CanEditItems");

            if (!isOwner.Succeeded)
            {
                logger.LogWarning("[Update] - User is not the owner -  {RequestDatetime} - {Parameter}", DateTime.Now, JsonSerializer.Serialize(itemUpdateModel));
                return new ForbidResult();
            }

            itemServices.Update(new ItemDTO { ItemId = itemId, Text = itemUpdateModel.Text });
            return Ok();
        }

        [HttpDelete("{itemId}")]
        [DisableCors]
        public IActionResult Delete(int itemId)
        {
            logger.LogInformation("[Delete] - {RequestDatetime} - {Parameter}", DateTime.Now, itemId);
            itemServices.Delete(itemId);
            return Ok();
        }
    }
}
