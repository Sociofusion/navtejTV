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
    public partial class schedulepost : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                GetScheduledPosts();
            }
        }

        public void BindData()
        {
            List<PLanguage> pLanguages = DBHelper.GetAllLanguages();
            ddlLanguage.DataSource = pLanguages;
            ddlLanguage.DataTextField = "Name";
            ddlLanguage.DataValueField = "ID";
            ddlLanguage.DataBind();

            ddlLanguage.SelectedIndex = ddlLanguage.Items.IndexOf(ddlLanguage.Items.FindByValue(pLanguages.Where(i => i.ISDefault == 1).FirstOrDefault().ID.ToString()));


            List<PCategory> pCategories = DBHelper.GetAllCategoriesByLanguageID(Convert.ToInt32(ddlLanguage.SelectedValue));
            ddlCategory.DataSource = pCategories;
            ddlCategory.DataTextField = "DefaultTitleToDisplay";
            ddlCategory.DataValueField = "ID";
            ddlCategory.DataBind();
        }

        public void GetScheduledPosts(long categoryId = 0, int languageId = 0)
        {
            PEmployee employee = (PEmployee)SessionState.LoggedInPerson;
            if (employee != null)
            {
                int userId = employee.ID;
                if (employee.RoleID == (int)PEmployee.ERole.superadmin)
                    userId = 0;

                DateTime today = DateTime.Now;
                DateTime yesterday = DateTime.Today.AddDays(-1);
                
                string str_yesterday = yesterday.ToString("yyyy-MM-dd");
                string str_today = today.ToString("yyyy-MM-dd");
                
                List<PPost> pPosts = DBHelper.GetPostsForBackEnd(str_yesterday, str_today, userId, categoryId, 0, 0, 0, 0, 0, 0, 0, 1, languageId);
            }

        }

        protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PCategory> pCategories = DBHelper.GetAllCategoriesByLanguageID(Convert.ToInt32(ddlLanguage.SelectedValue));
            ddlCategory.DataSource = pCategories;
            ddlCategory.DataTextField = "DefaultTitleToDisplay";
            ddlCategory.DataValueField = "ID";
            ddlCategory.DataBind();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetScheduledPosts(Convert.ToInt64(ddlCategory.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue));
        }
    }
}