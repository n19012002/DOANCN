using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace DOANCN
{
    public class RequireLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userID = context.HttpContext.Session.GetLong("ID");
            if (!userID.HasValue)
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
        }
    }
}
