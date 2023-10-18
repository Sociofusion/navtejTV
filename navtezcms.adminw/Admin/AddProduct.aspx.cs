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
    public partial class AddProduct : System.Web.UI.Page
    {
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
                List<PProductCategory> lst = DBHelper.GetParentProductCategories(Convert.ToInt32(ddlLanguage.SelectedValue));
                ddlCategory.DataSource = lst;
                ddlCategory.DataTextField = "DefaultTitleToDisplay";
                ddlCategory.DataValueField = "ID";
                ddlCategory.DataBind();

                ddlCategory.Items.Insert(0, new ListItem("-- Select Category --", "0"));
            }
        }

        protected void btnSaveProduct_Click(object sender, EventArgs e)
        {
            if (SessionState.LoggedInPerson != null)
            {
                PEmployee pEmployee = ((List<PEmployee>)SessionState.LoggedInPerson)[0];
                if (pEmployee != null)
                {
                    if (hdnProductId.Value == "")
                        hdnProductId.Value = "0";

                    if (DBHelper.CanAddProduct(txtSlug.Text.Trim(), Convert.ToInt64(hdnProductId.Value)) == 0)
                    {
                        lblError.InnerHtml = "Slug already exists";
                    }
                    else
                    {
                        PProduct pProduct = null;
                        if (hdnProductId.Value == "" || hdnProductId.Value == "0")
                        {
                            pProduct = new PProduct();

                            long ProductID = DBHelper.GetNextTextContentID();
                            pProduct.ID = ProductID;

                            // Translation
                            List<PTranslation> lstTitle = new List<PTranslation>();
                            PTranslation objTitle = new PTranslation();
                            objTitle.ParentTypeID = (int)Common.ParentType.Shop;
                            objTitle.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
                            objTitle.Translation = txtTitle.Text;
                            lstTitle.Add(objTitle);

                            List<PTranslation> lstDescription = new List<PTranslation>();
                            PTranslation objDescription = new PTranslation();
                            objDescription.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
                            objDescription.Translation = txtDescription.Text;
                            objDescription.ParentTypeID = (int)Common.ParentType.Shop;
                            lstDescription.Add(objDescription);

                            List<PTranslation> lstReturnPolicy = new List<PTranslation>();
                            PTranslation objReturnPolicy = new PTranslation();
                            objReturnPolicy.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
                            objReturnPolicy.Translation = txtReturnPolicy.Text;
                            objReturnPolicy.ParentTypeID = (int)Common.ParentType.Shop;
                            lstReturnPolicy.Add(objReturnPolicy);

                            // Translation

                            string effPrice = "";

                            if (String.IsNullOrEmpty(txtEffectivePrice.Text))
                            {
                                effPrice = txtPrice.Text;
                            }
                            else
                            {
                                effPrice = txtEffectivePrice.Text;
                            }
                            decimal price = Convert.ToDecimal(txtPrice.Text);
                            decimal afterdiscount = Convert.ToDecimal(txtEffectivePrice.Text);


                            pProduct.Price = price;
                            pProduct.EffectivePrice = afterdiscount;
                            pProduct.Off = Convert.ToInt32(txtOff.Text);
                            pProduct.ShippingCharge = Convert.ToInt32(txtShipping.Text);
                            
                            
                            pProduct.ISCOD = Convert.ToInt32(hdnISCOD.Value);
                            pProduct.ISActive = Convert.ToInt32(hdnISActive.Value);
                            pProduct.ISDigitalProduct = 0;
                            pProduct.ISDigitalProductFree = 0;

                            pProduct.CategoryID = Convert.ToInt32(hdnCategoryID.Value);
                            pProduct.Slug = txtSlug.Text;
                            pProduct.CreatedDate = System.DateTime.Now;
                            pProduct.UpdatedDate = System.DateTime.Now;
                            pProduct.CreatedBy = pEmployee.ID;


                            // upload Image
                            if (fupThumbnail_PreviewImage.HasFile)
                            {
                                string fileContentType = "";
                                AssetUploader assetUploader = new AssetUploader();
                                string uniqueName = assetUploader.UploadAsset(fupThumbnail_PreviewImage.FileName, fupThumbnail_PreviewImage.PostedFile, "shop", out fileContentType);
                                if (string.IsNullOrEmpty(uniqueName))
                                    throw new Exception("File is not uploaded");

                                PAsset pAsset = new PAsset();
                                pAsset.UniqueName = uniqueName;
                                pProduct.ImageBig = uniqueName; // Product
                                pAsset.ActualName = System.IO.Path.GetFileName(fupThumbnail_PreviewImage.FileName);
                                pAsset.ContentType = fileContentType;
                                pAsset.CreatedBy = pEmployee.ID;
                                pAsset.ParentTypeID = (int)Common.ParentType.Shop;
                                pAsset.CreatedBy = pEmployee.ID;
                                pAsset.ParentID = pProduct.ID;
                                pAsset.ISActive = 1;
                                DBHelper.InsertUpdateAsset(pAsset);
                            }

                            // upload Gallery Images
                            if (fupGalleryImages.HasFiles)
                            {
                                foreach (HttpPostedFile uploadedFile in fupGalleryImages.PostedFiles)
                                {
                                    string fileContentType = "";
                                    AssetUploader assetUploader = new AssetUploader();
                                    string uniqueName = assetUploader.UploadAsset(uploadedFile.FileName, uploadedFile, "shop", out fileContentType);
                                    if (string.IsNullOrEmpty(uniqueName))
                                        throw new Exception("File is not uploaded");

                                    PAsset pAsset = new PAsset();
                                    pAsset.UniqueName = uniqueName;
                                    pAsset.ActualName = System.IO.Path.GetFileName(uploadedFile.FileName);
                                    pAsset.ContentType = fileContentType;
                                    pAsset.CreatedBy = pEmployee.ID;
                                    pAsset.ParentTypeID = (int)Common.ParentType.Shop;
                                    pAsset.CreatedBy = pEmployee.ID;
                                    pAsset.ParentID = pProduct.ID;
                                    pAsset.ISActive = 1;
                                    pAsset.ISGallery = 1;

                                    DBHelper.InsertUpdateAsset(pAsset);
                                }
                            }

                            DBHelper.InsertProduct(pProduct, lstTitle, lstDescription, lstReturnPolicy);
                        }
                        else
                        {
                            pProduct = DBHelper.GetProductById(Convert.ToInt32(hdnProductId.Value));

                            string effPrice = "";

                            if (String.IsNullOrEmpty(txtEffectivePrice.Text))
                            {
                                effPrice = txtPrice.Text;
                            }
                            else
                            {
                                effPrice = txtEffectivePrice.Text;
                            }
                            decimal price = Convert.ToDecimal(txtPrice.Text);
                            decimal afterdiscount = Convert.ToDecimal(txtEffectivePrice.Text);


                            pProduct.Price = price;
                            pProduct.EffectivePrice = afterdiscount;
                            pProduct.Off = Convert.ToInt32(txtOff.Text);
                            pProduct.ShippingCharge = Convert.ToInt32(txtShipping.Text);


                            pProduct.ISCOD = Convert.ToInt32(hdnISCOD.Value);
                            pProduct.ISActive = Convert.ToInt32(hdnISActive.Value);
                            pProduct.ISDigitalProduct = 0;
                            pProduct.ISDigitalProductFree = 0;

                            pProduct.CategoryID = Convert.ToInt32(hdnCategoryID.Value);
                            pProduct.Slug = txtSlug.Text;
                            pProduct.CreatedDate = System.DateTime.Now;
                            pProduct.UpdatedDate = System.DateTime.Now;
                            pProduct.CreatedBy = pEmployee.ID;

                            // upload Image
                            if (fupThumbnail_PreviewImage.HasFile)
                            {
                                string fileContentType = "";
                                AssetUploader assetUploader = new AssetUploader();
                                string uniqueName = assetUploader.UploadAsset(fupThumbnail_PreviewImage.FileName, fupThumbnail_PreviewImage.PostedFile, "shop", out fileContentType);
                                if (string.IsNullOrEmpty(uniqueName))
                                    throw new Exception("File is not uploaded");

                                PAsset pAsset = new PAsset();
                                pAsset.UniqueName = uniqueName;
                                pProduct.ImageBig = uniqueName; // Product
                                pAsset.ActualName = System.IO.Path.GetFileName(fupThumbnail_PreviewImage.FileName);
                                pAsset.ContentType = fileContentType;
                                pAsset.CreatedBy = pEmployee.ID;
                                pAsset.ParentTypeID = (int)Common.ParentType.Shop;
                                pAsset.CreatedBy = pEmployee.ID;
                                pAsset.ParentID = pProduct.ID;
                                pAsset.ISActive = 1;

                                DBHelper.DeleteAssetByParentID(pProduct.ID);
                                DBHelper.InsertUpdateAsset(pAsset);
                            }
                            
                            // upload Gallery Images
                            if (fupGalleryImages.HasFiles)
                            {
                                DBHelper.DeleteAssetGalleryByParentID(pProduct.ID);
                                foreach (HttpPostedFile uploadedFile in fupGalleryImages.PostedFiles)
                                {
                                    string fileContentType = "";
                                    AssetUploader assetUploader = new AssetUploader();
                                    string uniqueName = assetUploader.UploadAsset(uploadedFile.FileName, uploadedFile, "shop", out fileContentType);
                                    if (string.IsNullOrEmpty(uniqueName))
                                        throw new Exception("File is not uploaded");

                                    PAsset pAsset = new PAsset();
                                    pAsset.UniqueName = uniqueName;
                                    pAsset.ActualName = System.IO.Path.GetFileName(uploadedFile.FileName);
                                    pAsset.ContentType = fileContentType;
                                    pAsset.CreatedBy = pEmployee.ID;
                                    pAsset.ParentTypeID = (int)Common.ParentType.Shop;
                                    pAsset.CreatedBy = pEmployee.ID;
                                    pAsset.ParentID = pProduct.ID;
                                    pAsset.ISActive = 1;
                                    pAsset.ISGallery = 1;

                                    DBHelper.InsertUpdateAsset(pAsset);
                                }
                            }


                            // Update Translation
                            PTranslation objTitle1 = pProduct.TitleData.Where(i => i.TextContentID == pProduct.TitleContentID && i.LanguageID == Convert.ToInt32(ddlLanguage.SelectedValue)).FirstOrDefault();
                            PTranslation objDescription1 = pProduct.DescriptionData.Where(i => i.TextContentID == pProduct.DescriptionContentID && i.LanguageID == Convert.ToInt32(ddlLanguage.SelectedValue)).FirstOrDefault();
                            PTranslation objReturnPolicy1 = pProduct.ReturnPolicyData.Where(i => i.TextContentID == pProduct.ReturnPolicyContentID && i.LanguageID == Convert.ToInt32(ddlLanguage.SelectedValue)).FirstOrDefault();

                            objTitle1.Translation = txtTitle.Text;
                            objDescription1.Translation = txtDescription.Text;
                            objReturnPolicy1.Translation = txtReturnPolicy.Text;

                            DBHelper.UpdateProduct(pProduct);
                        }

                        Response.Redirect("Products.aspx");
                    }
                }
            }
        }

        protected void txtOff_TextChanged(object sender, EventArgs e)
        {
            
            int off = Convert.ToInt32(txtOff.Text);
            int price = Convert.ToInt32(txtPrice.Text);
            double effectivePrice = price - (price * off / 100);
            txtEffectivePrice.Text = effectivePrice.ToString();
        }

    }
}