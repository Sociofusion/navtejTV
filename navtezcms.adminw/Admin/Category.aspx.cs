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
    public partial class Category : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnCategoryId.Value = "";
                BindCategory();
                BindLanguage();
                BindCategoryParent();
            }
        }

        public void BindLanguage()
        {
            List<PLanguage> lst = DBHelper.GetAllLanguages();
            ddlLanguage.DataSource = lst;
            ddlLanguage.DataTextField = "Name";
            ddlLanguage.DataValueField = "ID";
            ddlLanguage.DataBind();
            int defaultLangId = DBHelper.GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
            ddlLanguage.SelectedIndex = ddlLanguage.Items.IndexOf(ddlLanguage.Items.FindByValue(defaultLangId.ToString()));
        }

        public void BindCategory()
        {
            List<PCategory> lst = DBHelper.GetAllCategories();

            rptCategory.DataSource = lst;
            rptCategory.DataBind();
        }

        public void BindCategoryParent()
        {
            List<PCategory> lst = DBHelper.GetAllParentCategories();
            ddlParentCategory.DataSource = lst;
            ddlParentCategory.DataTextField = "DefaultTitleToDisplay";
            ddlParentCategory.DataValueField = "ID";
            ddlParentCategory.DataBind();

            ddlParentCategory.Items.Insert(0, new ListItem("-- Select Parent Category --", "0"));
        }

        protected void btnSaveCategory_Click(object sender, EventArgs e)
        {
            if (hdnCategoryId.Value == "")
                hdnCategoryId.Value = "0";

            if (DBHelper.CanAddCategory(txtSlug.Text.Trim(), Convert.ToInt64(hdnCategoryId.Value)) == 0)
            {
                lblError.InnerHtml = "Slug already exists";
            }
            else
            {
                PCategory pCategory = null;
                if (hdnCategoryId.Value == "" || hdnCategoryId.Value == "0")
                {
                    pCategory = new PCategory();

                    List<PTranslation> lstTitle = new List<PTranslation>();
                    PTranslation objTitle = new PTranslation();
                    objTitle.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
                    objTitle.ParentTypeID = (int)Common.ParentType.Category;
                    objTitle.Translation = txtTitle.Text;
                    lstTitle.Add(objTitle);
                    
                    pCategory.Color = hdnColor.Value;
                    pCategory.CanShowAtHome = Convert.ToInt32(hdnIsShowHome.Value);
                    pCategory.CanShowOnMenu = Convert.ToInt32(hdnIsShowMenu.Value);
                    pCategory.Slug = txtSlug.Text;
                    pCategory.ISActive = 1;
                    pCategory.CategoryOrder = Convert.ToInt32(txtCategoryOrder.Text);
                    pCategory.ParentCategoryID = Convert.ToInt32(ddlParentCategory.SelectedValue);

                    DBHelper.InsertCategory(pCategory, lstTitle);
                }
                else
                {
                    pCategory = DBHelper.GetCategoryById(Convert.ToInt32(hdnCategoryId.Value));
                    pCategory.Color = hdnColor.Value;
                    pCategory.CanShowAtHome = Convert.ToInt32(hdnIsShowHome.Value);
                    pCategory.CanShowOnMenu = Convert.ToInt32(hdnIsShowMenu.Value);

                    pCategory.Slug = txtSlug.Text;
                    pCategory.ISActive = 1;
                    pCategory.CategoryOrder = Convert.ToInt32(txtCategoryOrder.Text);
                    pCategory.ParentCategoryID = Convert.ToInt32(ddlParentCategory.SelectedValue);

                    PTranslation objTitle1 = pCategory.TitleData.Where(i => i.TextContentID == pCategory.TitleContentID && i.LanguageID == Convert.ToInt32(ddlLanguage.SelectedValue)).FirstOrDefault();
                    objTitle1.Translation = txtTitle.Text;
                    objTitle1.ParentTypeID = (int)Common.ParentType.Category;

                    DBHelper.UpdateCategory(pCategory);
                }

                setMenuJson();
                Response.Redirect("Category.aspx");
            }
        }
        protected void rptCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deleteCategory")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                DBHelper.DeleteCategory(id);
                BindCategory();
            }
        }

        public void setMenuJson()
        {
            List<PLanguage> lstLanguage = DBHelper.GetAllLanguages();
            foreach (var objLanguage in lstLanguage)
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.setMenuJson(objLanguage.ID));
                DBHelper.InsertUpdateMenuJson(json, objLanguage.ID);
            }
        }

    }
}