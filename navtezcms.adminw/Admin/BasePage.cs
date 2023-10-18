using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace navtezcms.adminw.Admin
{
    public class BasePage : System.Web.UI.Page
    {
        private void Page_Init(object sender, EventArgs e)
        {
            if (SessionState.LoggedInPerson == null)
                Response.Redirect("Login.aspx");
        }

    }
}