using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace pro.Filters
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (CurrentUser.IsUserAuthenticated() == false) 
            {
                context.Result = new RedirectToActionResult("Authorization", "Main", null);
            }
        }
    }
}
