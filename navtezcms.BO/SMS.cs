using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Web;
using System.Data;


public static class SMS
{

    static string[] arrTemplate = new string[]
    {
        "Zero",
        "Dear @v0@, You are successfully registered with Paramdham, Thanks",                                                                         //1  //For Registration --
        "Dear @v0@, Your order has been placed successfully. Order Id (@v1@). Please check your mail for more details.",                             //2  //For Order Placed -- 
        "Dear @v0@, Your order with Order Id @v1@ has been successfully cancelled. If you have already paid, refund will be initiated shortly.",     //3  //For Order Cancelled --
        "Dear @v0@, Your password has been changed successfully, Thanks",                                                                            //4  //For Password changed --
        "Dear @v0@, Your order with Order Id @v1@ has been successfully delivered.",                                                                 //5  //For Order Delivered -- 
        "Your Paramdham verification code is @v0@.",                                                                                                 //6  //For Registration --
        "Your Paramdham verification code is @v0@.",                                                                                                 //7  //For Change Mobile Number --
    };

    public static void SendWithVar(string Mobile, int Template, string[] ValueArray)
    {
        try
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            WebClient client = new WebClient();
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            string smsMessage = GetString(Template, ValueArray);
            string baseurl = get_SMSBaseURL(Mobile, smsMessage);
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        catch (Exception ex)
        {

        }
    }



    public static void Send(string Mobile, string smsMessage)
    {
        try
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            WebClient client = new WebClient();
            string baseurl = get_SMSBaseURL(Mobile, smsMessage);
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        catch (Exception ex)
        {

        }
    }

    public static string GetString(int Template, string[] ValueArray)
    {
        string fileData = arrTemplate[Template];
        if ((ValueArray == null))
        {
            fileData = HttpUtility.UrlEncode(fileData);
            return fileData;
        }
        else
        {
            for (int i = ValueArray.GetLowerBound(0); i <= ValueArray.GetUpperBound(0); i++)
            {
                fileData = fileData.Replace("@v" + i.ToString() + "@", (string)ValueArray[i]);
            }
            fileData = HttpUtility.UrlEncode(fileData);
            return fileData;
        }
    }


    public static string get_SMSBaseURL(string Mobile, string smsMessage)
    {
        string str = "";
        str = "http://sms.hspsms.com/sendSMS?username=weaveotp&message=" + smsMessage + "&sendername=Soriso&smstype=TRANS&numbers=" + Mobile + "&apikey=1fae07f9-ea87-4ad3-a61a-f2d65e9cff3d";
        return str;
    }
}