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
    public partial class PendingPosts : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDate();
                BindLanguage();
                GetPosts();
            }
        }
        public void BindDate()
        {
            DateTime yesterday = DateTime.Today.AddDays(-1);
            DateTime today = DateTime.Now;


            txtStartDate.Text = yesterday.ToString("dd-MM-yyyy");
            txtEndDate.Text = today.ToString("dd-MM-yyyy");
        }

        public void BindLanguage()
        {
            List<PLanguage> lstLanguage = DBHelper.GetAllLanguages();
            ddlLanguage.DataSource = lstLanguage;
            ddlLanguage.DataTextField = "Name";
            ddlLanguage.DataValueField = "ID";

            ddlLanguage.DataBind();
            ddlLanguage.Items.Insert(0, new ListItem("--Select Category--", "0"));
            ddlLanguage.SelectedIndex = ddlLanguage.Items.IndexOf(ddlLanguage.Items.FindByValue(lstLanguage.Where(i => i.ISDefault == 1).FirstOrDefault().ID.ToString()));


            List<PCategory> pCategories = DBHelper.GetAllCategoriesByLanguageID(Convert.ToInt32(ddlLanguage.SelectedValue));
            ddlCategory.DataSource = pCategories;
            ddlCategory.DataTextField = "DefaultTitleToDisplay";
            ddlCategory.DataValueField = "ID";

            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("--Select Category--", "0"));
        }

        public void GetPosts(long categoryId = 0, int languageId = 0)
        {
            List<PEmployee> employee = (List<PEmployee>)SessionState.LoggedInPerson;
            if (employee != null)
            {
                List<PRole> lstRole = DBHelper.GetRolesById(employee[0].RoleID);

                int userId = employee[0].ID;
                if (employee[0].RoleID == (int)PEmployee.ERole.superadmin)
                {
                    userId = 0;
                }

                int status = 0;
                int isFeature = 0;
                int isSlider = 0;
                int isSliderLeft = 0;
                int isSliderRight = 0;
                int isTrending = 0;


                status = (int)DBHelper.EStatus.Pending;

                isFeature = 0;
                isSlider = 0;
                isSliderLeft = 0;
                isSliderRight = 0;
                isTrending = 0;

                
                string str_yesterday = txtStartDate.Text;
                DateTime dt_yesterday = DateTime.ParseExact(str_yesterday, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string yesterday = dt_yesterday.ToString("yyyy-MM-dd");

                string str_today = txtEndDate.Text;
                DateTime dt_today = DateTime.ParseExact(str_today, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string today = dt_today.ToString("yyyy-MM-dd");

                List<PPost> lstPosts = DBHelper.GetPostsForBackEnd(yesterday, today, userId, categoryId, 0, status, isFeature, isSlider, isSliderLeft, isSliderRight, isTrending, 0, languageId, lstRole[0].ISShowAll);
                rptPost.DataSource = lstPosts;
                rptPost.DataBind();
            }

        }

        protected void rptPost_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                DBHelper.SetStatusPost(ID, Convert.ToInt32(DBHelper.EStatus.Approved));
                GetPosts();
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PCategory> pCategories = DBHelper.GetAllCategoriesByLanguageID(Convert.ToInt32(ddlLanguage.SelectedValue));
            ddlCategory.DataSource = pCategories;
            ddlCategory.DataTextField = "DefaultTitleToDisplay";
            ddlCategory.DataValueField = "ID";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("--Select Category--", "0"));
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPosts(Convert.ToInt64(ddlCategory.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue));
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            GetPosts(Convert.ToInt64(ddlCategory.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue));
        }
    }
}