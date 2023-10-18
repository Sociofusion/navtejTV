using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;



/*
 Read Me Text:

    1 first create a request url to facebook for ACCESS-TOKEN use GetLoginUrl_GraphApi()
    2 after sucessfull login we will get CODE from facebook
    3 exchanage ACCESS-TOKEN from CODE use  GetTokenFormCode()
     
     
 */


/// <summary>
/// Summary description for fbHelper
/// </summary>
public static class fbHelper
{
    public static string GetLoginUrl_GraphApi()
    {
        string url = "https://graph.facebook.com/v6.0/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}";
        string callbackurl;
        string scope;
        try
        {
            //callbackurl = HttpUtility.UrlEncode(HttpContext.Current.Application["ServerPath"] + "/fbcallback.aspx");
            // By Deepak on 04 Oct 2020 as https not taken Directly by Server path
            callbackurl = HttpUtility.UrlEncode("https://www.sorriso.in/fbcallback.aspx");
            scope = HttpUtility.UrlEncode(Convert.ToString("public_profile,email"));
            url = string.Format(url, "1131349490533205", callbackurl, scope);
        }
        catch (Exception Ex)
        {
            throw;
        }
        finally
        {
            callbackurl = null;
            scope = null;
        }
        return url;
    }
    public static AccessTokenResult GetTokenFormCode(string code)
    {
        AccessTokenResult tokenResult = new AccessTokenResult();
        try
        {
            using (WebClient oWeb = new WebClient())
            {
                var resp = oWeb.DownloadString(GetTokenExchangeUrl(code));
                AccessTokenObject accessTokenObject = JsonConvert.DeserializeObject<AccessTokenObject>(resp);
                tokenResult = new AccessTokenResult
                {
                    token = accessTokenObject.access_token,
                    expirein = accessTokenObject.expires_in,
                    tokentype = accessTokenObject.token_type
                };
            }
        }
        catch (WebException exception)
        {
            string responseText;

            if (exception.Response != null)
            {
                var responseStream = exception.Response.GetResponseStream();

                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseText = reader.ReadToEnd();
                        ErrorObject errorObject = JsonConvert.DeserializeObject<ErrorObject>(responseText);
                        tokenResult = new AccessTokenResult
                        {
                            code = errorObject.error.code,
                            error = errorObject.error.message,
                            token = string.Empty
                        };
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            tokenResult = new AccessTokenResult
            {
                code = 400,
                error = Ex.ToString(),
                token = string.Empty
            };
        }
        return tokenResult;
    }
    public static string GetTokenExchangeUrl(string code)
    {
        string callbackurl;
        string url = @"https://graph.facebook.com/v6.0/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}";
        try
        {
            //callbackurl = HttpUtility.UrlEncode(HttpContext.Current.Application["ServerPath"] + "/fbcallback.aspx");
            // By Deepak on 04 Oct 2020 as https not taken Directly by Server path
            callbackurl = HttpUtility.UrlEncode("https://www.sorriso.in/fbcallback.aspx");
            url = string.Format(url, "1131349490533205", callbackurl, "38c5b0a0666d4a1bc93236da8ceb3884", code);
        }
        catch (Exception Ex)
        {
            throw;
        }
        finally
        {
            callbackurl = null;
        }
        return url;

    }
    public static string TokenFromSession()
    {
        string token = string.Empty;
        try
        {
            if (HttpContext.Current.Session["FbToken"] != null)
            {
                token = HttpContext.Current.Session["FbToken"].ToString();
            }
            else
            {
                string cktoken = Helper.ReadCookiee("FbToken");
                if (!string.IsNullOrEmpty(cktoken))
                {
                    HttpContext.Current.Session["FbToken"] = cktoken;
                    token = cktoken;
                }
            }
        }
        catch (Exception Ex)
        {
        }
        return token;
    }

    public static FbUserObject GetUserFromToken(string token)
    {
        FbUserObject fbUser = new FbUserObject();
        try
        {
            using (WebClient client = new WebClient())
            {
                var resp = client.DownloadString(string.Format("https://graph.facebook.com/v6.0/me?fields=id%2Cname%2Cemail%2Cgender&access_token={0}", token));
                fbUser = JsonConvert.DeserializeObject<FbUserObject>(resp);
            }

        }
        catch (WebException exception)
        {
            string responseText;

            if (exception.Response != null)
            {
                var responseStream = exception.Response.GetResponseStream();

                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseText = reader.ReadToEnd();
                        ErrorObject errorObject = JsonConvert.DeserializeObject<ErrorObject>(responseText);
                        fbUser.error = errorObject.error;
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            fbUser.error.code = 400;
            fbUser.error.message = Ex.Message;
        }

        return fbUser;
    }

    public static DataTable GetUserFromDB(string FacebookUserId)
    {
        DataTable dt = new DataTable();
        cls_connection db = new cls_connection();
        try
        {
            dt = db.select_data_dt(String.Format("Exec spFaceBookLogin '{0}','{1}','{2}','','{3}'", "", "", FacebookUserId, "", "GetUser"));
        }
        catch (Exception Ex)
        {
        }
        finally
        {
            db = null;
        }
        return dt;
    }

    public static DataTable RegisterUser(string Name, string Email, string FacebookUserId, string Gender)
    {
        DataTable dt = new DataTable();
        cls_connection db = new cls_connection();
        try
        {
            dt = db.select_data_dt(String.Format("Exec spFaceBookLogin '{0}','{1}','{2}','{3}','{4}'", Name, Email, FacebookUserId, Gender, "Register"));
        }
        catch (Exception Ex)
        {
        }
        finally
        {
            db = null;
        }
        return dt;
    }
}

public class FbUserObject
{
    public string id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public Error error { get; set; }
    public string gender { get; set; }
}
public class AccessTokenResult
{
    public AccessTokenResult()
    {
        code = 1;
        error = string.Empty;
        token = string.Empty;
        tokentype = string.Empty;
        expirein = 0;
    }
    public int code { get; set; }
    public string error { get; set; }
    public string token { get; set; }
    public string tokentype { get; set; }
    public int expirein { get; set; }
}
public class Error
{
    public string message { get; set; }
    public string type { get; set; }
    public int code { get; set; }
    public int error_subcode { get; set; }
    public string error_user_title { get; set; }
    public string error_user_msg { get; set; }
    public string fbtrace_id { get; set; }
}
public class ErrorObject
{
    public Error error { get; set; }
}
public class AccessTokenObject
{
    public string access_token { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }
}