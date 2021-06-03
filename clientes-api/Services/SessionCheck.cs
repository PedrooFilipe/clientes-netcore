using clientes_api.Model.Rest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;

namespace clientes_api.Services
{
    public class SessionCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var header = context.HttpContext.Request.Headers;

            try
            {
                var token = header.TryGetValue("Authorization", out StringValues value);

                if (TokenService.Validate(value))
                {
                    base.OnActionExecuting(context);
                }
                else
                {                    
                    RestResponse<object> result = new RestResponse<object>();

                    result.ResponseMessage = "SESSION EXPIRED";

                    context.Result = new UnauthorizedObjectResult(result);
                }
            }
            catch (Exception)
            {
                RestResponse<object> result = new RestResponse<object>();

                result.ResponseMessage = "REQUEST ERROR";

                context.Result = new BadRequestObjectResult(result);
            }
        }

    } // end of class

} // end of namespace