using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;
using System.Configuration;
using System.Net.Http;
using System.Net;

namespace navtezcms.api
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            try
            {
                string token = HttpContext.Current.Request.Headers["sessionToken"];
                if (string.IsNullOrEmpty(token)|| token=="null")
                {
                    actionContext.Response = new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Content = new StringContent("sessionToken is missing from header")
                    };
                    return;
                }
                bool isValid = (token == "navtez87887");
                string message = "";
                if (string.IsNullOrEmpty(token))
                    message = "Majik: Authentication header is missing";
                else if (isValid == false)
                    message = "Majik: Session Token has expired, Please login again to get new token";
                else
                    message = "You are not authorized to access this resource";

                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Content = new StringContent(message)
                };
            }
            catch(Exception ex)
            {
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Content = new StringContent("You are unauthorized to access this resource")
                };
            }
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    string token = HttpContext.Current.Request.Headers["sessionToken"];
                    if (string.IsNullOrEmpty(token))
                        return false;
                    bool isValid = true;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}