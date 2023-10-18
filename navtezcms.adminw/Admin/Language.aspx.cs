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
    public partial class Language : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnLanguageId.Value = "";
                BindLanguage();
            }
        }
        
        public void BindLanguage()
        {
            List<PLanguage> lst = DBHelper.GetAllLanguages();

            rptLanguage.DataSource = lst;
            rptLanguage.DataBind();
        }

       
        protected void btnSaveLanguage_Click(object sender, EventArgs e)
        {

            if (hdnLanguageId.Value == "")
                hdnLanguageId.Value = "0";

            if (DBHelper.CanAddLanguage(txtName.Text.Trim(), Convert.ToInt64(hdnLanguageId.Value)) == 0)
            {
                lblError.InnerHtml = "Language already exists";
            }
            else
            {
                PLanguage pLanguage = null;
                if (hdnLanguageId.Value == "" || hdnLanguageId.Value == "0")
                {
                    pLanguage = new PLanguage();
                    
                    pLanguage.Name = txtName.Text;
                    pLanguage.ISActive = Convert.ToInt32(hdnIsActive.Value);
                    pLanguage.ISDefault = Convert.ToInt32(hdnIsDefault.Value);

                    DBHelper.InsertLanguage(pLanguage);
                }
                else
                {
                    pLanguage = DBHelper.GetLanguageById(Convert.ToInt32(hdnLanguageId.Value));
                    pLanguage.Name = txtName.Text;
                    pLanguage.ISActive = Convert.ToInt32(hdnIsActive.Value);
                    pLanguage.ISDefault = Convert.ToInt32(hdnIsDefault.Value);

                    DBHelper.UpdateLanguage(pLanguage);
                }
                
                Response.Redirect("Language.aspx");
            }
        }
        protected void rptLanguage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deleteLanguage")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                DBHelper.DeleteLanguage(id);
                BindLanguage();
            }
        }
    }
}