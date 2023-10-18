using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using navtezcms.BO;
using navtezcms.DAL;

namespace navtezcms.adminw.Admin.UC
{
    public partial class Asset : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PEmployee pEmployee = ((List<PEmployee>)SessionState.LoggedInPerson)[0];
                if (pEmployee != null)
                {
                    hdnEmployeeID.Value = Convert.ToString(pEmployee.ID);
                }
            }
        }
        protected void lbDeleteGallery_Click(object sender, EventArgs e)
        {
            int galleryID = Convert.ToInt32(hdnGallery.Value);
            DBHelper.DeleteAssetByAssetID(galleryID);
        }
        
    }
}