using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace navtezcms.adminw.Admin
{
    public class SessionState
    {
        public static object LoggedInPerson
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["LoggedInPerson"] != null)
                {
                    return HttpContext.Current.Session["LoggedInPerson"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["LoggedInAdmin"] = value;
            }
        }

        public static object DefaultLanguageID
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["DefaultLanguageID"] != null)
                {
                    return HttpContext.Current.Session["DefaultLanguageID"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["DefaultLanguageID"] = value;
            }
        }
    }
}