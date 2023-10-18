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
    public partial class advertisement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnEditID.Value = "";
                BindData();
                BindPlacements();
            }
        }

        public void BindData()
        {
            List<PAdvertisment> lst = DBHelper.GetAllAdvertisements();

            rptData.DataSource = lst;
            rptData.DataBind();
        }

        public void BindPlacements()
        {
            List<PAdvertismentPlacement> lst = DBHelper.GetAllAdvertisePlacements();

            ddlPlacement.DataSource = lst;
            ddlPlacement.DataTextField = "PlacementAreaName";
            ddlPlacement.DataValueField = "ID";
            ddlPlacement.DataBind();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            PEmployee pEmployee = ((List<PEmployee>)SessionState.LoggedInPerson)[0];

            PAdvertisment pAdvertisment = null;
            if (hdnEditID.Value == "" || hdnEditID.Value == "0")
            {
                pAdvertisment = new PAdvertisment();
                pAdvertisment.ID = DBHelper.GetNextBusinessID();
                pAdvertisment.ISNew = 1;
            }
            else
            {
                pAdvertisment = DBHelper.GetAdvertisementByAdvertismentID(Convert.ToInt32(hdnEditID.Value));
            }

            pAdvertisment.PlacementAreaID = Convert.ToInt32(ddlPlacement.SelectedValue);
            pAdvertisment.ISActive = Convert.ToInt32(hdnActive.Value);
            pAdvertisment.AdType = Convert.ToString(ddlAdType.SelectedValue);
            pAdvertisment.AdSize = Convert.ToString(ddlAdSize.SelectedValue);
            pAdvertisment.AdCode = txtAdCode.Text;
            pAdvertisment.AdLink = txtAdLink.Text;
            pAdvertisment.ISActive = Convert.ToInt32(hdnActive.Value);


            if (Convert.ToString(ddlAdType.SelectedValue) == "AdFromAsset")
            {
                if (fupFile.HasFile)
                {
                    string fileContentType = "";
                    AssetUploader assetUploader = new AssetUploader();
                    string uniqueName = assetUploader.UploadAsset(fupFile.FileName, fupFile.PostedFile, "advertisement", out fileContentType);
                    if (string.IsNullOrEmpty(uniqueName))
                        throw new Exception("File is not uploaded");

                    PAsset pAsset = new PAsset();
                    pAsset.UniqueName = uniqueName;
                    pAsset.ActualName = System.IO.Path.GetFileName(fupFile.FileName);
                    pAsset.ContentType = fileContentType;
                    pAsset.CreatedBy = pEmployee.ID;
                    pAsset.ParentTypeID = (int)Common.ParentType.Advertisement;
                    pAsset.CreatedBy = pEmployee.ID;
                    pAsset.ParentID = pAdvertisment.ID;
                    pAsset.ISActive = 1;
                    DBHelper.DeleteAssetByParentID(pAdvertisment.ID);
                    DBHelper.InsertUpdateAsset(pAsset);
                }
                else
                {
                    lblError.Text = "Please select the image";
                    return;
                }
            }

            long id = DBHelper.InsertUpdateAdvertisment(pAdvertisment);
            Response.Redirect("advertisement.aspx");


        }

        protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                DBHelper.DeleteAdvertisment(id);
                BindData();
            }
            if (e.CommandName == "IsActive")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                DBHelper.ActiveDeActiveAdvertisment(id);
                BindData();
            }
        }
    }
}