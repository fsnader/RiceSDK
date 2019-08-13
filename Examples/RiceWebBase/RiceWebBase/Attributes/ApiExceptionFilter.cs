using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rice.SDK.Exceptions.Api;

namespace RiceWebBase.Attributes
{
    /// <summary>
    /// Exception filter responsible for casting http exceptions and return them correctly
    /// </summary>
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {   
        /// <summary>
        /// On Exception method override
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ApiException ex:
                    context.Result = new JsonResult(ex.Message);
                    context.HttpContext.Response.StatusCode = ex.StatusCode;
                    break;

                case NotFoundException _:
                    context.Result = new JsonResult("Not Found");
                    context.HttpContext.Response.StatusCode = 404;
                    break;

                case UnauthorizedException _:
                    context.Result = new JsonResult("Unauthorized Access");
                    context.HttpContext.Response.StatusCode = 401;
                    break;

                case BadRequestException ex:
                    
                    context.Result = new JsonResult("Bad Request");
                    if (ex.ValidationErrors.Any())
                        context.Result = new JsonResult(ex.ValidationErrors);
                    
                    context.HttpContext.Response.StatusCode = 400;
                    break;

                case BusinessException _:
                    context.Result = new JsonResult(context.Exception.Message);
                    context.HttpContext.Response.StatusCode = 409;
                    break;

                default:
                    context.Result = new JsonResult(context.Exception.Message);
                    context.HttpContext.Response.StatusCode = 500;
                    break;
            }

            base.OnException(context);
        }
    }
}