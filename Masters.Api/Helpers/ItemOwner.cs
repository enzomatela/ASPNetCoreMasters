using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreMastersTodoList.API.Authorization
{
    public class ItemOwnerRequirement : IAuthorizationRequirement { }

    public class ItemOwnerHandler : AuthorizationHandler<ItemOwnerRequirement, ItemDTO>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ItemOwnerHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ItemOwnerRequirement requirement,
            ItemDTO resource)
        {
            var appUser = await _userManager.GetUserAsync(context.User);

            if (resource.CreatedBy == appUser.Id)
            {
                context.Succeed(requirement);
            }

            return;
        }
    }
}