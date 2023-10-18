using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Filters;

namespace navtezcms.api
{
    public class JsonOutputAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            ObjectContent content = actionExecutedContext.Response.Content as ObjectContent;
            var value = content.Value;
            Type targetType = actionExecutedContext.Response.Content.GetType().GetGenericArguments()[0];

            var httpResponseMsg = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                RequestMessage = actionExecutedContext.Request,
                Content = new ObjectContent(targetType, value, new JsonMediaTypeFormatter(), (string)null)
            };

            actionExecutedContext.Response = httpResponseMsg;
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}