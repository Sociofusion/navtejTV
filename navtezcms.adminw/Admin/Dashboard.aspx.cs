using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using navtezcms.DAL;

namespace navtezcms.adminw.Admin
{
    public partial class Dashboard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDashboard();
            }
        }

        #region Function
        private void GetDashboard()
        {
            DataSet ds = DBHelper.GetDashboardData();
            litCategory.Text = (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TotalCategory"].ToString()) == true) ? "0" : ds.Tables[0].Rows[0]["TotalCategory"].ToString();
            litPosts.Text = (string.IsNullOrEmpty(ds.Tables[1].Rows[0]["TotalPost"].ToString()) == true) ? "0" : ds.Tables[1].Rows[0]["TotalPost"].ToString();
            litCustomPage.Text = (string.IsNullOrEmpty(ds.Tables[2].Rows[0]["TotalCustomPage"].ToString()) == true) ? "0" : ds.Tables[2].Rows[0]["TotalCustomPage"].ToString();
            litLanguage.Text = (string.IsNullOrEmpty(ds.Tables[3].Rows[0]["TotalLanguage"].ToString()) == true) ? "0" : ds.Tables[3].Rows[0]["TotalLanguage"].ToString();
            litAdvertisements.Text = (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["TotalAdvertisement"].ToString()) == true) ? "0" : ds.Tables[4].Rows[0]["TotalAdvertisement"].ToString();
            litEmployee.Text = (string.IsNullOrEmpty(ds.Tables[5].Rows[0]["TotalEmployee"].ToString()) == true) ? "0" : ds.Tables[5].Rows[0]["TotalEmployee"].ToString();
            LitRole.Text = (string.IsNullOrEmpty(ds.Tables[6].Rows[0]["TotalRole"].ToString()) == true) ? "0" : ds.Tables[6].Rows[0]["TotalRole"].ToString();
            litAdminMenu.Text = (string.IsNullOrEmpty(ds.Tables[7].Rows[0]["TotalAdminMenu"].ToString()) == true) ? "0" : ds.Tables[7].Rows[0]["TotalAdminMenu"].ToString();
            
        }

        #endregion
    }
}