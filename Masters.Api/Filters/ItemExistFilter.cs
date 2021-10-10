using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services;
using System;

namespace Masters.Api.Filters
{

    public class ItemExistFilter : IActionFilter
    {
        private readonly IItemService itemService;

        public ItemExistFilter(IItemService _itemService)
        {
            itemService = _itemService;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("itemId", out object id);
            if (id != null)
            {
                var itemId = (int)id;

                if (!itemService.ItemExist(itemId))
                {
                    context.Result = new NotFoundResult();
                }
            }
                
            
        }
    }
    public class ItemExistAttribute : TypeFilterAttribute
    {
        public ItemExistAttribute() : base(typeof(ItemExistFilter))
        {
            
        }
    }

}
