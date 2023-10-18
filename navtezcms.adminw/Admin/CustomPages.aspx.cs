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
    public partial class CustomPages : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnCustomPageId.Value = "";
                BindCustomPage();
                BindLanguage();
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

        public void BindCustomPage()
        {
            List<PCustomPage> lst = DBHelper.GetActiveCustomePages();

            rptCustomPage.DataSource = lst;
            rptCustomPage.DataBind();
        }

        protected void btnSaveCustomPage_Click(object sender, EventArgs e)
        {

            if (hdnCustomPageId.Value == "")
                hdnCustomPageId.Value = "0";

            if (DBHelper.CanAddCustomPage(txtSlug.Text.Trim(), Convert.ToInt64(hdnCustomPageId.Value)) == 0)
            {
                lblError.InnerHtml = "Slug already exists";
            }
            else
            {
                PCustomPage pCustomPage = null;
                if (hdnCustomPageId.Value == "" || hdnCustomPageId.Value == "0")
                {
                    pCustomPage = new PCustomPage();

                    List<PTranslation> lstTitle = new List<PTranslation>();
                    PTranslation objTitle = new PTranslation();
                    objTitle.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
                    objTitle.ParentTypeID = (int)Common.ParentType.CustomPage;
                    objTitle.Translation = txtTitle.Text;
                    lstTitle.Add(objTitle);

                    List<PTranslation> lstDescription = new List<PTranslation>();
                    PTranslation objDescription = new PTranslation();
                    objDescription.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
                    objDescription.Translation = txtDescription.Text;
                    lstDescription.Add(objDescription);

                    pCustomPage.ISFooter = Convert.ToInt32(hdnIsFooter.Value);
                    
                    pCustomPage.Slug = txtSlug.Text;
                    pCustomPage.PageName = txtPageName.Text;
                    
                    pCustomPage.ISActive = 1;
                    pCustomPage.CreatedOn = System.DateTime.Now;
                   
                    DBHelper.InsertCustomPage(pCustomPage, lstTitle, lstDescription);
                }
                else
                {
                    pCustomPage = DBHelper.GetCustomPageById(Convert.ToInt32(hdnCustomPageId.Value));

                
                    pCustomPage.ISFooter = Convert.ToInt32(hdnIsFooter.Value);
                    pCustomPage.ISActive = 1;
                    pCustomPage.Slug = txtSlug.Text;
                    pCustomPage.PageName = txtPageName.Text;

                    pCustomPage.CreatedOn = System.DateTime.Now;

                    PTranslation objTitle1 = pCustomPage.TitleData.Where(i => i.TextContentID == pCustomPage.TitleContentID && i.LanguageID == Convert.ToInt32(ddlLanguage.SelectedValue)).FirstOrDefault();
                    PTranslation objDescription1 = pCustomPage.DescriptionData.Where(i => i.TextContentID == pCustomPage.DescriptionContentID && i.LanguageID == Convert.ToInt32(ddlLanguage.SelectedValue)).FirstOrDefault();

                    objTitle1.Translation = txtTitle.Text;
                    objDescription1.Translation = txtDescription.Text;
                    objTitle1.ParentTypeID = (int)Common.ParentType.CustomPage;
                    objDescription1.ParentTypeID = (int)Common.ParentType.CustomPage;
                    DBHelper.UpdateCustomPage(pCustomPage);
                }
                
                Response.Redirect("CustomPages.aspx");
            }
        }
        protected void rptCustomPage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deleteCustomPage")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                DBHelper.DeleteCustomPage(id);
                BindCustomPage();
            }
        }
    }
}