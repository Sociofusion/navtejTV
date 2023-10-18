using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using navtezcms.DAL;
using navtezcms.BO;
using navtezcms.adminw;
namespace navtezcms.adminw.Admin
{
    public partial class mainsite : System.Web.UI.MasterPage
    {

        DataTable dtMenu = new DataTable();
        protected void Page_Init(object sender, EventArgs e)
        {
            Helper.ReLoginAdmin();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //hidWebPath.Value = Common.GetAppPath;
                hidWebPath.Value = "https://admin.navtejtv.com";

                hidLanguageCount.Value = Convert.ToString(DBHelper.GetAllLanguages().Count);
                hidLanguageNames.Value = string.Join(",", DBHelper.GetAllLanguages().Select(i => i.Name));
                hidLanguageIds.Value = string.Join(",", DBHelper.GetAllLanguages().Select(i => i.ID));
                hidDefaultLan.Value = Convert.ToString(DBHelper.GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID);
                //hidWebPath.Value = ConfigurationManager.AppSettings["CentralSystemUrl"];
                if (SessionState.LoggedInPerson != null)
                {
                    List<PEmployee> lstEmployee = (List<PEmployee>)SessionState.LoggedInPerson;

                    DataTable dtMenu = (DataTable)Session["dtMenu"];

                    lblUserName.Text = lstEmployee[0].Name;
                    lblUserNameDown.Text = lstEmployee[0].Name;
                    lblUserNameLeft.Text = lstEmployee[0].Name;
                    DateTime date = lstEmployee[0].CreatedOn;
                    litMemberSince.Text = date.ToString("dd-MMM-yyyy");
                    lblRoleName.Text = lstEmployee[0].RoleName;

                    imgProfile1.ImageUrl = "Images/nobody.jpg";
                    imgProfile2.ImageUrl = "Images/nobody.jpg";
                    imgSmall.ImageUrl = "Images/nobody.jpg";

                    // Permission Page

                    bool match = false;
                    string URL = Request.Url.ToString();
                    int id_x = URL.LastIndexOf('/') + 1;
                    int id_y = URL.LastIndexOf('?');
                    if (id_x != -1)
                    {
                        string Page = "";
                        Page = URL.Substring(id_x);
                        if (id_y != -1)
                        {
                            int length = id_y - id_x;
                            Page = URL.Substring(id_x, length);
                        }

                        foreach (DataRow dtRow in dtMenu.Rows)
                        {
                            string MenuLink = Convert.ToString(dtRow["MenuLink"]);
                            if (MenuLink.ToLower() == Page.ToLower())
                            {
                                match = true;
                            }
                        }
                    }
                    if (match == false)
                    {
                        Response.Redirect("Dashboard.aspx");
                    }

                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void lbLogOut_Click(object sender, EventArgs e)
        {
            Session["LoggedInPerson"] = null;
            Session.Abandon();
            Helper.DeleteCokkies("_Admin_SessionId_Navtej");
            Response.Redirect("Login.aspx");
        }
    }
}