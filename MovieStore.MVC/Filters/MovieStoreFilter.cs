using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MovieStore.MVC.Filters
{
    public class MovieStoreFilter : ActionFilterAttribute
    {
        // will execute after the action method
        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }

        // will execute before the action method
        // scenario: track the info of how many people click on the movie detail page
        //   OR why type of movies the login user are most interested
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var data = context.HttpContext.Request.Path;
            var other = context.HttpContext.User.Identity.IsAuthenticated;
            // we can log to database or text file. Perform some ML stuff(recommendation)
        }
    }
}