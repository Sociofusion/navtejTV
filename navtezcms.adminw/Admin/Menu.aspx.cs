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
    public partial class Menu : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnMenuId.Value = "";
                BindMenu();
                BindLanguage();
                BindMenuParent();
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

        public void BindMenu()
        {
            List<PMenu> lst = DBHelper.GetAllMenu();

            rptMenu.DataSource = lst;
            rptMenu.DataBind();
        }

        public void BindMenuParent()
        {
            List<PParentType> lst = DBHelper.GetParentType();
            ddlParentType.DataSource = lst;
            ddlParentType.DataTextField = "ParentType";
            ddlParentType.DataValueField = "ID";
            ddlParentType.DataBind();

            ddlParentType.Items.Insert(0, new ListItem("-- Select Parent Type --", "0"));
        }

        protected void btnSaveMenu_Click(object sender, EventArgs e)
        {

            if (hdnMenuId.Value == "")
                hdnMenuId.Value = "0";


            PMenu pMenu = null;
            if (hdnMenuId.Value == "" || hdnMenuId.Value == "0")
            {
                pMenu = new PMenu();

                List<PTranslation> lstTitle = new List<PTranslation>();
                PTranslation objTitle = new PTranslation();
                objTitle.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
                objTitle.ParentTypeID = (int)Common.ParentType.Menu;
                objTitle.Translation = txtTitle.Text;
                lstTitle.Add(objTitle);

                pMenu.ISActive = Convert.ToInt32(hdnIsActive.Value);
                pMenu.ISFooter = Convert.ToBoolean(hdnIsFooter.Value);
                pMenu.MenuOrder = Convert.ToInt32(txtMenuOrder.Text);
                pMenu.ParentTypeID = Convert.ToInt32(ddlParentType.SelectedValue);
                pMenu.ParentMenuID = Convert.ToInt32(ddlParentType.SelectedValue);
                pMenu.LinkUrl = "";
                pMenu.FooterSlug = "";

                DBHelper.InsertMenu(pMenu, lstTitle);
            }
            else
            {
                pMenu = DBHelper.GetMenuById(Convert.ToInt32(hdnMenuId.Value));
                pMenu.ISActive = Convert.ToInt32(hdnIsActive.Value);
                pMenu.ISFooter = Convert.ToBoolean(hdnIsFooter.Value);
                pMenu.MenuOrder = Convert.ToInt32(txtMenuOrder.Text);
                pMenu.ParentTypeID = Convert.ToInt32(ddlParentType.SelectedValue);
                pMenu.ParentMenuID = Convert.ToInt32(ddlParentType.SelectedValue);
                pMenu.LinkUrl = "";
                pMenu.FooterSlug = "";

                PTranslation objTitle1 = pMenu.TitleData.Where(i => i.TextContentID == pMenu.MenuTextContentID && i.LanguageID == Convert.ToInt32(ddlLanguage.SelectedValue)).FirstOrDefault();
                objTitle1.Translation = txtTitle.Text;


                DBHelper.UpdateMenu(pMenu);
            }
            
            Response.Redirect("Menu.aspx");
        }
        protected void rptMenu_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deleteMenu")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                DBHelper.DeleteMenu(id);
                BindMenu();
            }
        }
    }
}