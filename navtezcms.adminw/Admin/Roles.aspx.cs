using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using navtezcms.BO;
using navtezcms.DAL;

namespace navtezcms.adminw.Admin
{
    public partial class Roles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRoles();
            }
        }

        public void BindRoles()
        {
            List<PRole> lst = DBHelper.GetRoles();
            rptRoles.DataSource = lst;
            rptRoles.DataBind();
        }

        protected void rptRoles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deleteRoles")
            {
                int roleID = Convert.ToInt32(e.CommandArgument.ToString());

                PRole pRole = new PRole();
                pRole.ID = roleID;
                DBHelper.DeleteRoles(pRole);
                BindRoles();
            }
        }
    }
}