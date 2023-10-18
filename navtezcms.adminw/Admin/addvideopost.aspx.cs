using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using navtezcms.BO;
using navtezcms.DAL;


namespace navtezcms.adminw.Admin
{
    public partial class addvideopost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindLanguage();
        }

        public void SaveData()
        {
            //if (fupFile.HasFile)
            //{
            //    string fileContentType = "";
            //    AssetUploader assetUploader = new AssetUploader();
            //    string uniqueName = assetUploader.UploadAsset(fupFile.FileName, fupFile.PostedFile, out fileContentType);
            //    if (string.IsNullOrEmpty(uniqueName))
            //        throw new Exception("File is not uploaded");

            //    PAsset pAsset = new PAsset();
            //    pAsset.ActualName = System.IO.Path.GetFileName(fupFile.FileName);
            //    pAsset.ContentType = fileContentType;
            //    pAsset.CreatedBy = ((PEmployee)SessionState.LoggedInPerson).ID;
            //    pAsset.ParentTypeID = (int)Common.ParentType.Advertisement;

            //    PAdvertisment pAdvertisment = null;
            //    if (hdnEditID.Value == "" || hdnEditID.Value == "0")
            //    {
            //        pAdvertisment = new PAdvertisment();
            //    }
            //    else
            //    {
            //        pAdvertisment = DBHelper.GetAdvertisementByAdvertismentID(Convert.ToInt32(hdnEditID.Value));
            //    }

            //    pAdvertisment.PlacementAreaID = Convert.ToInt32(ddlPlacement.SelectedValue);
            //    pAdvertisment.ISActive = Convert.ToInt32(hdnActive.Value);
            //    pAdvertisment.AdType = Convert.ToString(ddlAdType.SelectedValue);
            //    pAdvertisment.AdSize = Convert.ToString(ddlAdSize.SelectedValue);
            //    pAdvertisment.AdCode = txtAdCode.Text;
            //    pAdvertisment.AdLink = txtAdLink.Text;
            //    pAsset.CreatedBy = ((PEmployee)SessionState.LoggedInPerson).ID;

            //    long id = DBHelper.InsertUpdateAdvertisment(pAdvertisment);

            //    pAsset.ParentID = id;
            //    DBHelper.InsertUpdateAsset(pAsset);

            //    Response.Redirect("advertisement.aspx");
            //}

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

        protected void btnSaveAsDraft_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddPost_Click(object sender, EventArgs e)
        {

        }
    }
}