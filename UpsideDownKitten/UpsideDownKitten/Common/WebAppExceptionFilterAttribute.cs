using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UpsideDownKitten.BL.Utils;

namespace UpsideDownKitten.Common
{
    public class WebAppExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is WebApiException exception)
            {
                context.Result = new JsonResult(new {exception.Message});
                context.HttpContext.Response.StatusCode = (int) System.Net.HttpStatusCode.BadRequest;
            }
            else
            {
                base.OnException(context);
            }
        }
    }
}
