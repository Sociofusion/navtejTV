using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using navtezcms.DAL;
using navtezcms.BO;
using System.Data;

namespace navtezcms.adminw.Admin
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["navtezUName"] != null && Request.Cookies["navtezUPassword"] != null)
                {
                    txtUserName.Text = Common.DecryptPassword(Convert.ToString(Request.Cookies["navtezUName"].Value));
                    txtPassword.Text = Common.DecryptPassword(Convert.ToString(Request.Cookies["navtezUPassword"].Value));
                    txtPassword.Attributes.Add("value", Common.DecryptPassword(Convert.ToString(Request.Cookies["navtezUPassword"].Value)));
                    chkRememberMe.Checked = true;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            RememberMe();
            string email = txtUserName.Text.Trim().Replace("'", "");
            string password = txtPassword.Text.Trim().Replace("'", "");
            List<PEmployee> lstCheckEmployee = DBHelper.GetEmployeeByEmailPassword(Convert.ToString(email), Convert.ToString(password));
            if (lstCheckEmployee.Count > 0)
            {
                SessionState.LoggedInPerson = lstCheckEmployee;
                // Set Default Language
                List<PLanguage> lst = DBHelper.GetAllLanguages();
                PLanguage language = lst.Where(i => i.ISDefault == 1).ToList().FirstOrDefault();
                SessionState.DefaultLanguageID = language.ID;

                List<PAdminMenu> lstAdminMenu = new List<PAdminMenu>();
                lstAdminMenu = DBHelper.GetAdminMenuByRoleID(lstCheckEmployee[0].RoleID);
                DataTable dtMenu = navtezcms.BO.Common.ToDataTable(lstAdminMenu);
                Session.Add("dtMenu", dtMenu);

                Helper.CreatAdminLoginCookies(new AdminSession
                {
                    Email = email,
                    Password = password
                });
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                alert.Visible = true;
                lblAlert.Text = "* Wrong login details";
            }
        }

        public void Clear()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
        }
        public void RememberMe()
        {
            if (chkRememberMe.Checked)
            {
                Response.Cookies["navtezUName"].Expires = DateTime.Now.AddDays(30);
                Response.Cookies["navtezUPassword"].Expires = DateTime.Now.AddDays(30);
            }
            else
            {
                Response.Cookies["navtezUName"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["navtezUPassword"].Expires = DateTime.Now.AddDays(-1);
            }

            Response.Cookies["navtezUName"].Value = Common.EncryptPassword(txtUserName.Text);
            Response.Cookies["navtezUPassword"].Value = Common.EncryptPassword(txtPassword.Text);
        }
    }
}