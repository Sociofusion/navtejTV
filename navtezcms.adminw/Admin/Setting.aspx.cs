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
    public partial class Setting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSetting();
            }
        }

        public void BindSetting()
        {
            List<PSetting> lst = DBHelper.GetSetting();
            txtFacebookLink.Text = lst[0].FacebookLink;
            txtYoutubeLink.Text = lst[0].YoutubeLink;
            txtInstagramLink.Text = lst[0].InstagramLink;
            txtTwitterLink.Text = lst[0].TwitterLink;
            txtYoutubeVideoURL.Text = lst[0].YoutubeVideoURL;

            txtMetaTags.Text = lst[0].MetaTags;
            txtGoogleAnalytics.Text = lst[0].GoogleAnalytics;
            txtAddress.Text = lst[0].Address;
            txtMailID.Text = lst[0].MailID;
            txtMobile1.Text = lst[0].Mobile1;
            txtMobile2.Text = lst[0].Mobile2;
            txtCopyright.Text = lst[0].Copyright;

            hdnID.Value = Convert.ToString(lst[0].ID);

            imgLogo.ImageUrl = lst[0].LogoLiveUrl;
            imgLogo.Visible = true;

            imgLogoFooter.ImageUrl = lst[0].FooterLogoLiveUrl;
            imgLogoFooter.Visible = true;
        }

        protected void btnUpdateSetting_Click(object sender, EventArgs e)
        {
            PEmployee pEmployee = ((List<PEmployee>)SessionState.LoggedInPerson)[0];
            if (pEmployee != null)
            {
                List<PSetting> lst = DBHelper.GetSetting();
                PSetting pSetting = new PSetting();
                pSetting.ID = Convert.ToInt32(hdnID.Value);
                pSetting.FacebookLink = txtFacebookLink.Text;
                pSetting.TwitterLink = txtTwitterLink.Text;
                pSetting.YoutubeLink = txtYoutubeLink.Text;
                pSetting.InstagramLink = txtInstagramLink.Text;
                pSetting.YoutubeVideoURL = txtYoutubeVideoURL.Text;


                pSetting.MetaTags = txtMetaTags.Text;
                pSetting.GoogleAnalytics = txtGoogleAnalytics.Text;
                pSetting.Address = txtAddress.Text;
                pSetting.MailID = txtMailID.Text;
                pSetting.Mobile1 = txtMobile1.Text;
                pSetting.Mobile2 = txtMobile2.Text;
                pSetting.Copyright = txtCopyright.Text;

                
                pSetting.Logo = lst[0].Logo;
                pSetting.FooterLogo = lst[0].FooterLogo;

                pSetting.ISActive = 1;
                
                // upload Logo
                if (fupLogo_PreviewImage.HasFile)
                {
                    string fileContentType = "";
                    AssetUploader assetUploader = new AssetUploader();
                    string uniqueName = assetUploader.UploadAsset(fupLogo_PreviewImage.FileName, fupLogo_PreviewImage.PostedFile, "setting", out fileContentType);
                    if (string.IsNullOrEmpty(uniqueName))
                        throw new Exception("File is not uploaded");

                    PAsset pAsset = new PAsset();
                    pAsset.UniqueName = uniqueName;
                    pSetting.Logo = uniqueName;  // Logo
                    pAsset.ActualName = System.IO.Path.GetFileName(fupLogo_PreviewImage.FileName);
                    pAsset.ContentType = fileContentType;
                    pAsset.CreatedBy = pEmployee.ID;
                    pAsset.ParentTypeID = (int)Common.ParentType.Setting;
                    pAsset.CreatedBy = pEmployee.ID;
                    pAsset.ParentID = pSetting.ID;
                    pAsset.ISActive = 1;
                    DBHelper.DeleteAssetByParentID(pSetting.ID);
                    DBHelper.InsertUpdateAsset(pAsset);
                }

                // upload Footer Logo
                if (fupFooterLogo_PreviewImage.HasFile)
                {
                    string fileContentType = "";
                    AssetUploader assetUploader = new AssetUploader();
                    string uniqueName = assetUploader.UploadAsset(fupFooterLogo_PreviewImage.FileName, fupFooterLogo_PreviewImage.PostedFile, "setting", out fileContentType);
                    if (string.IsNullOrEmpty(uniqueName))
                        throw new Exception("File is not uploaded");

                    PAsset pAsset = new PAsset();
                    pAsset.UniqueName = uniqueName;
                    pSetting.FooterLogo = uniqueName;  // Logo
                    pAsset.ActualName = System.IO.Path.GetFileName(fupFooterLogo_PreviewImage.FileName);
                    pAsset.ContentType = fileContentType;
                    pAsset.CreatedBy = pEmployee.ID;
                    pAsset.ParentTypeID = (int)Common.ParentType.Setting;
                    pAsset.CreatedBy = pEmployee.ID;
                    pAsset.ParentID = pSetting.ID;
                    pAsset.ISActive = 1;
                    DBHelper.DeleteAssetByParentID(pSetting.ID);
                    DBHelper.InsertUpdateAsset(pAsset);
                }

                DBHelper.UpdateSetting(pSetting);
            }
        }
    }
}