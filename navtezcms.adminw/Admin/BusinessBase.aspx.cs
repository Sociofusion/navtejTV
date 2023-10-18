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
    public partial class BusinessBase : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnCategoryId.Value = "";
            }
        }

        
        protected void btnSaveCategory_Click(object sender, EventArgs e)
        {

            long titleContentId = DBHelper.GetNextTextContentID();

            Response.Redirect("BusinessBase.aspx");
        }
    }
}