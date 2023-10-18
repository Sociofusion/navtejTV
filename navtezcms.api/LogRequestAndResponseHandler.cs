using navtezcms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MajikSFRApi
{
    public class LogRequestAndResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request, CancellationToken cancellationToken)
        {
            long auditLogId = 0;
            if (request.Content != null)
            {
                // log request body
                string requestBody = await request.Content.ReadAsStringAsync();
                string requestUrl = request.RequestUri.ToString();
                //auditLogId =  DBHelper.InsertAuditLog(requestUrl, requestBody);
            }
            // let other handlers process the request
            var result = await base.SendAsync(request, cancellationToken);

            if (result.Content != null)
            {
                // once response body is ready, log it
                var responseBody = await result.Content.ReadAsStringAsync();
                if(auditLogId>0)
                {
                  //  DBHelper.UpdateAuditLog(responseBody, auditLogId);
                }    
            }
            return result;
        }
    }
}