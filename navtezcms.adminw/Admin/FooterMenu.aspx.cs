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
    public partial class FooterMenu : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnFooterMenuId.Value = "";
                BindFooterMenu();
                BindLanguage();
                BindFooterMenuParent();
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

        public void BindFooterMenu()
        {
            List<PMenu> lst = DBHelper.GetAllFooterMenu();

            rptFooterMenu.DataSource = lst;
            rptFooterMenu.DataBind();
        }

        public void BindFooterMenuParent()
        {
            List<PParentType> lst = DBHelper.GetParentType();
            ddlParentType.DataSource = lst;
            ddlParentType.DataTextField = "ParentType";
            ddlParentType.DataValueField = "ID";
            ddlParentType.DataBind();

            ddlParentType.Items.Insert(0, new ListItem("-- Select Parent Type --", "0"));
        }

        protected void btnSaveFooterMenu_Click(object sender, EventArgs e)
        {

            if (hdnFooterMenuId.Value == "")
                hdnFooterMenuId.Value = "0";


            PMenu pFooterMenu = null;
            if (hdnFooterMenuId.Value == "" || hdnFooterMenuId.Value == "0")
            {
                pFooterMenu = new PMenu();

                List<PTranslation> lstTitle = new List<PTranslation>();
                PTranslation objTitle = new PTranslation();
                objTitle.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
                objTitle.ParentTypeID = (int)Common.ParentType.Menu;
                objTitle.Translation = txtTitle.Text;
                lstTitle.Add(objTitle);

                pFooterMenu.ISActive = Convert.ToInt32(hdnIsActive.Value);
                pFooterMenu.ISFooter = Convert.ToBoolean(hdnIsFooter.Value);
                pFooterMenu.MenuOrder = Convert.ToInt32(txtFooterMenuOrder.Text);
                pFooterMenu.ParentTypeID = Convert.ToInt32(ddlParentType.SelectedValue);
                pFooterMenu.ParentMenuID = 0;
                pFooterMenu.LinkUrl = "";
                pFooterMenu.FooterSlug = txtFooterSlug.Text;

                DBHelper.InsertMenu(pFooterMenu, lstTitle);
            }
            else
            {
                pFooterMenu = DBHelper.GetMenuById(Convert.ToInt32(hdnFooterMenuId.Value));
                pFooterMenu.ISActive = Convert.ToInt32(hdnIsActive.Value);
                pFooterMenu.ISFooter = Convert.ToBoolean(hdnIsFooter.Value);
                pFooterMenu.MenuOrder = Convert.ToInt32(txtFooterMenuOrder.Text);
                pFooterMenu.ParentTypeID = Convert.ToInt32(ddlParentType.SelectedValue);
                pFooterMenu.ParentMenuID = 0;
                pFooterMenu.LinkUrl = "";
                pFooterMenu.FooterSlug = txtFooterSlug.Text;

                PTranslation objTitle1 = pFooterMenu.TitleData.Where(i => i.TextContentID == pFooterMenu.MenuTextContentID && i.LanguageID == Convert.ToInt32(ddlLanguage.SelectedValue)).FirstOrDefault();
                objTitle1.Translation = txtTitle.Text;


                DBHelper.UpdateMenu(pFooterMenu);
            }
            
            Response.Redirect("FooterMenu.aspx");
        }
        protected void rptFooterMenu_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deleteFooterMenu")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                DBHelper.DeleteMenu(id);
                BindFooterMenu();
            }
        }
    }
}