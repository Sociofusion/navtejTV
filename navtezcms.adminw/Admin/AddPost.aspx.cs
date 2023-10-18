using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using navtezcms.DAL;
using navtezcms.BO;
using System.Data;

namespace navtezcms.adminw.Admin
{
    public partial class AddPost : System.Web.UI.Page
    {
        public TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLanguage();
                BindCategory();
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
            PEmployee pEmployee = ((List<PEmployee>)SessionState.LoggedInPerson)[0];
            if (pEmployee != null)
            {
                List<PCategory> lst = DBHelper.GetCategoryByRoleID(pEmployee.RoleID);
                ddlCategory.DataSource = lst;
                ddlCategory.DataTextField = "DefaultTitleToDisplay";
                ddlCategory.DataValueField = "ID";
                ddlCategory.DataBind();

                ddlCategory.Items.Insert(0, new ListItem("-- Select Category --", "0"));
            }
        }

        protected void btnSaveAsDraft_Click(object sender, EventArgs e)
        {
            SavePost(true);
        }

        protected void btnSavePost_Click(object sender, EventArgs e)
        {
            SavePost(false);
        }

        public void SavePost(bool IsDraft)
        {
            if (SessionState.LoggedInPerson != null)
            {
                PEmployee pEmployee = ((List<PEmployee>)SessionState.LoggedInPerson)[0];
                if (pEmployee != null)
                {
                    if (hdnPostId.Value == "")
                        hdnPostId.Value = "0";

                    if (DBHelper.CanAddPost(txtSlug.Text.Trim(), Convert.ToInt64(hdnPostId.Value)) == 0)
                    {
                        lblError.InnerHtml = "Slug already exists";
                    }
                    else
                    {
                        PPost pPost = null;
                        if (hdnPostId.Value == "" || hdnPostId.Value == "0")
                        {
                            pPost = new PPost();

                            long PostID = DBHelper.GetNextTextContentID();
                            pPost.ID = PostID;

                            // Translation
                            List<PTranslation> lstTitle = new List<PTranslation>();
                            PTranslation objTitle = new PTranslation();
                            objTitle.ParentTypeID = (int)Common.ParentType.Post;
                            objTitle.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
                            objTitle.Translation = txtTitle.Text;
                            lstTitle.Add(objTitle);

                            List<PTranslation> lstDescription = new List<PTranslation>();
                            PTranslation objDescription = new PTranslation();
                            objDescription.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
                            objDescription.Translation = txtDescription.Text;
                            objDescription.ParentTypeID = (int)Common.ParentType.Post;
                            lstDescription.Add(objDescription);

                            List<PTranslation> lstMetaTags = new List<PTranslation>();
                            PTranslation objMetaTags = new PTranslation();
                            objMetaTags.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
                            objMetaTags.ParentTypeID = (int)Common.ParentType.Post;
                            objMetaTags.Translation = txtMetaKeywords.Text;
                            lstMetaTags.Add(objMetaTags);

                            // Translation

                            pPost.ISFeature = Convert.ToInt32(hdnISFeature.Value);
                            pPost.ISSlider = Convert.ToInt32(hdnISSlider.Value);
                            pPost.ISSliderLeft = Convert.ToInt32(hdnISSliderLeft.Value);
                            pPost.ISSliderRight = Convert.ToInt32(hdnISSliderRight.Value);
                            pPost.ISTrending = Convert.ToInt32(hdnISTrending.Value);
                            pPost.ISSchedulePost = Convert.ToInt32(chkISSchedule.Checked);

                            pPost.CategoryID = Convert.ToInt32(hdnCategoryID.Value);
                            pPost.Tags = hdnTags.Value;
                            pPost.Slug = txtSlug.Text;
                            pPost.ISActive = 1;
                            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                            pPost.CreatedOn = indianTime;
                            pPost.CreatedBy = pEmployee.ID;
                            pPost.PostTypeID = 1;

                            pPost.ISFacebookEmbed = rbFacebookEmbed.Checked == true ? 1 : 0;
                            pPost.ISTwitterEmbed = rbTwitterEmbed.Checked == true ? 1 : 0;
                            pPost.ISInstagramEmbed = rbInstagramEmbed.Checked == true ? 1 : 0;
                            pPost.ISYoutubeEmbed = rbYoutubeEmbed.Checked == true ? 1 : 0;

                            pPost.EmbedSocial = txtEmbedSocial.Text;

                            // Set Status using Role
                            if (pEmployee.RoleID == Convert.ToInt32(DBHelper.ERole.superadmin))
                            {
                                pPost.StatusID = 2;
                            }
                            else if (pEmployee.RoleID == Convert.ToInt32(DBHelper.ERole.admin))
                            {
                                pPost.StatusID = 2;
                            }
                            else if (pEmployee.RoleID == Convert.ToInt32(DBHelper.ERole.reporter))
                            {
                                pPost.StatusID = 1;
                            }
                            else
                            {
                                pPost.StatusID = 1;
                            }
                            // Check IsDraft
                            if (IsDraft)
                            {
                                pPost.StatusID = 1;
                            }

                            if (chkISSchedule.Checked)
                            {
                                if (txtScheduleStartDate.Text != "")
                                {
                                    pPost.SchedulePostDate = Convert.ToDateTime(txtScheduleStartDate.Text);
                                }
                                if (txtScheduleEndDate.Text != "")
                                {
                                    pPost.SchedulePostEndDate = Convert.ToDateTime(txtScheduleEndDate.Text);
                                }
                            }

                            if (!string.IsNullOrEmpty(hdnFeaturedImage.Value))
                            {
                                pPost.ImageBig = hdnFeaturedImage.Value;
                                pPost.ImageBigAssetID = Convert.ToInt32(hdnAssetID.Value);
                            }
                            else
                            {
                                // upload Image
                                if (fupThumbnail_PreviewImage.HasFile)
                                {
                                    string fileContentType = "";
                                    AssetUploader assetUploader = new AssetUploader();
                                    string uniqueName = assetUploader.UploadAsset(fupThumbnail_PreviewImage.FileName, fupThumbnail_PreviewImage.PostedFile, "post", out fileContentType);
                                    if (string.IsNullOrEmpty(uniqueName))
                                        throw new Exception("File is not uploaded");

                                    PAsset pAsset = new PAsset();
                                    pAsset.UniqueName = uniqueName;
                                    pPost.ImageBig = uniqueName;  // Post
                                    pAsset.ActualName = System.IO.Path.GetFileName(fupThumbnail_PreviewImage.FileName);
                                    pAsset.ContentType = fileContentType;
                                    pAsset.CreatedBy = pEmployee.ID;
                                    pAsset.ParentTypeID = (int)Common.ParentType.Post;
                                    pAsset.CreatedBy = pEmployee.ID;
                                    pAsset.ParentID = pPost.ID;
                                    pAsset.ISActive = 1;
                                    DBHelper.InsertUpdateAsset(pAsset);
                                }
                            }

                            // Post
                            // upload Gallery Images
                            if (fupGalleryImages.HasFiles)
                            {
                                foreach (HttpPostedFile uploadedFile in fupGalleryImages.PostedFiles)
                                {
                                    string fileContentType = "";
                                    AssetUploader assetUploader = new AssetUploader();
                                    string uniqueName = assetUploader.UploadAsset(uploadedFile.FileName, uploadedFile, "post", out fileContentType);
                                    if (string.IsNullOrEmpty(uniqueName))
                                        throw new Exception("File is not uploaded");

                                    PAsset pAsset = new PAsset();
                                    pAsset.UniqueName = uniqueName;
                                    pAsset.ActualName = System.IO.Path.GetFileName(uploadedFile.FileName);
                                    pAsset.ContentType = fileContentType;
                                    pAsset.CreatedBy = pEmployee.ID;
                                    pAsset.ParentTypeID = (int)Common.ParentType.Post;
                                    pAsset.CreatedBy = pEmployee.ID;
                                    pAsset.ParentID = pPost.ID;
                                    pAsset.ISActive = 1;
                                    pAsset.ISGallery = 1;

                                    DBHelper.InsertUpdateAsset(pAsset);
                                }
                            }

                            DBHelper.InsertPost(pPost, lstTitle, lstDescription, lstMetaTags);
                        }
                        else
                        {
                            pPost = DBHelper.GetPostById(Convert.ToInt32(hdnPostId.Value));

                            pPost.ISFeature = Convert.ToInt32(hdnISFeature.Value);
                            pPost.ISSlider = Convert.ToInt32(hdnISSlider.Value);
                            pPost.ISSliderLeft = Convert.ToInt32(hdnISSliderLeft.Value);
                            pPost.ISSliderRight = Convert.ToInt32(hdnISSliderRight.Value);
                            pPost.ISTrending = Convert.ToInt32(hdnISTrending.Value);
                            pPost.ISSchedulePost = Convert.ToInt32(chkISSchedule.Checked);

                            pPost.CategoryID = Convert.ToInt32(hdnCategoryID.Value);
                            pPost.Tags = hdnTags.Value;
                            pPost.Slug = txtSlug.Text;
                            pPost.ISActive = 1;
                            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                            pPost.CreatedOn = indianTime;
                            pPost.CreatedBy = pEmployee.ID;
                            pPost.PostTypeID = 1;

                            pPost.ISFacebookEmbed = rbFacebookEmbed.Checked == true ? 1 : 0;
                            pPost.ISTwitterEmbed = rbTwitterEmbed.Checked == true ? 1 : 0;
                            pPost.ISInstagramEmbed = rbInstagramEmbed.Checked == true ? 1 : 0;
                            pPost.ISYoutubeEmbed = rbYoutubeEmbed.Checked == true ? 1 : 0;

                            pPost.EmbedSocial = txtEmbedSocial.Text;

                            // Set Status using Role
                            if (pEmployee.RoleID == Convert.ToInt32(DBHelper.ERole.superadmin))
                            {
                                pPost.StatusID = 2;
                            }
                            else if (pEmployee.RoleID == Convert.ToInt32(DBHelper.ERole.admin))
                            {
                                pPost.StatusID = 2;
                            }
                            else if (pEmployee.RoleID == Convert.ToInt32(DBHelper.ERole.reporter))
                            {
                                pPost.StatusID = 1;
                            }
                            // Check IsDraft
                            if (IsDraft)
                            {
                                pPost.StatusID = 1;
                            }

                            if (chkISSchedule.Checked)
                            {
                                if (txtScheduleStartDate.Text != "")
                                {
                                    pPost.SchedulePostDate = Convert.ToDateTime(txtScheduleStartDate.Text);
                                }
                                if (txtScheduleEndDate.Text != "")
                                {
                                    pPost.SchedulePostEndDate = Convert.ToDateTime(txtScheduleEndDate.Text);
                                }
                            }
                            
                            if (!string.IsNullOrEmpty(hdnFeaturedImage.Value))
                            {
                                if(ddlFeaturedImageType.SelectedValue == "1")
                                {
                                    DBHelper.DeleteAssetByParentID(pPost.ID);
                                }
                                pPost.ImageBig = hdnFeaturedImage.Value;
                                pPost.ImageBigAssetID = Convert.ToInt32(hdnAssetID.Value);
                            }
                            else
                            {
                                // upload Image
                                if (fupThumbnail_PreviewImage.HasFile)
                                {
                                    string fileContentType = "";
                                    AssetUploader assetUploader = new AssetUploader();
                                    string uniqueName = assetUploader.UploadAsset(fupThumbnail_PreviewImage.FileName, fupThumbnail_PreviewImage.PostedFile, "post", out fileContentType);
                                    if (string.IsNullOrEmpty(uniqueName))
                                        throw new Exception("File is not uploaded");

                                    PAsset pAsset = new PAsset();
                                    pAsset.UniqueName = uniqueName;
                                    pPost.ImageBig = uniqueName;  // Post
                                    pAsset.ActualName = System.IO.Path.GetFileName(fupThumbnail_PreviewImage.FileName);
                                    pAsset.ContentType = fileContentType;
                                    pAsset.CreatedBy = pEmployee.ID;
                                    pAsset.ParentTypeID = (int)Common.ParentType.Post;
                                    pAsset.CreatedBy = pEmployee.ID;
                                    pAsset.ParentID = pPost.ID;
                                    pAsset.ISActive = 1;

                                    DBHelper.DeleteAssetByParentID(pPost.ID);
                                    DBHelper.InsertUpdateAsset(pAsset);
                                }
                            }

                            // upload Gallery Images
                            if (fupGalleryImages.HasFiles)
                            {
                                DBHelper.DeleteAssetGalleryByParentID(pPost.ID);
                                foreach (HttpPostedFile uploadedFile in fupGalleryImages.PostedFiles)
                                {
                                    string fileContentType = "";
                                    AssetUploader assetUploader = new AssetUploader();
                                    string uniqueName = assetUploader.UploadAsset(uploadedFile.FileName, uploadedFile, "post", out fileContentType);
                                    if (string.IsNullOrEmpty(uniqueName))
                                        throw new Exception("File is not uploaded");

                                    PAsset pAsset = new PAsset();
                                    pAsset.UniqueName = uniqueName;
                                    pAsset.ActualName = System.IO.Path.GetFileName(uploadedFile.FileName);
                                    pAsset.ContentType = fileContentType;
                                    pAsset.CreatedBy = pEmployee.ID;
                                    pAsset.ParentTypeID = (int)Common.ParentType.Post;
                                    pAsset.CreatedBy = pEmployee.ID;
                                    pAsset.ParentID = pPost.ID;
                                    pAsset.ISActive = 1;
                                    pAsset.ISGallery = 1;

                                    DBHelper.InsertUpdateAsset(pAsset);
                                }
                            }


                            // Update Translation
                            PTranslation objTitle1 = pPost.TitleData.Where(i => i.TextContentID == pPost.TitleContentID && i.LanguageID == Convert.ToInt32(ddlLanguage.SelectedValue)).FirstOrDefault();
                            PTranslation objDescription1 = pPost.DescriptionData.Where(i => i.TextContentID == pPost.DescriptionContentID && i.LanguageID == Convert.ToInt32(ddlLanguage.SelectedValue)).FirstOrDefault();
                            PTranslation objMetaTags1 = pPost.MetaTagData.Where(i => i.TextContentID == pPost.MetaTagContentID && i.LanguageID == Convert.ToInt32(ddlLanguage.SelectedValue)).FirstOrDefault();

                            objTitle1.Translation = txtTitle.Text;
                            objDescription1.Translation = txtDescription.Text;
                            objMetaTags1.Translation = txtMetaKeywords.Text;

                            DBHelper.UpdatePost(pPost);
                        }

                        Response.Redirect("Posts.aspx");
                    }
                }
            }
        }

    }
}
