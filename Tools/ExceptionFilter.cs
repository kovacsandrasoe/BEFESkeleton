using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BEFESkeleton.Tools
{
    public class ExceptionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is Exception ex)
            {
                context.ExceptionHandled = true;
                context.Result = 
                    new BadRequestObjectResult(new ErrorModel(ex.Message))
                    {
                        ContentTypes = {"application/json"}
                    };
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
