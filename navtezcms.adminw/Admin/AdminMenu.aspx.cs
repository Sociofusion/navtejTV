using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using navtezcms.DAL;
using navtezcms.BO;

namespace navtezcms.adminw.Admin
{
    public partial class AdminMenu : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnAdminMenuId.Value = "";
                BindAdminMenu();
            }
        }
        

        public void BindAdminMenu()
        {
            List<PAdminMenu> lst = DBHelper.GetAllAdminMenu();

            rptAdminMenu.DataSource = lst;
            rptAdminMenu.DataBind();
        }
        
        
        protected void rptAdminMenu_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deleteAdminMenu")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());

                PAdminMenu pAdminMenu = new PAdminMenu();
                pAdminMenu.ID = id;
                DBHelper.DeleteAdminMenu(pAdminMenu);
                BindAdminMenu();
            }
            if (e.CommandName == "IsActive")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                DBHelper.IsActiveDeActiveAdminMenu(id);
                BindAdminMenu();
            }
        }
        
    }
}