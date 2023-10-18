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
    public partial class adplacement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnEditID.Value = "";
                BindData();
            }
        }

        public void BindData()
        {
            List<PAdvertismentPlacement> lst = DBHelper.GetAllAdvertisePlacements();

            rptData.DataSource = lst;
            rptData.DataBind();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hdnEditID.Value))
                hdnEditID.Value = "0";

            if (DBHelper.CanAddPlacement(txtAreaName.Text.Trim(), Convert.ToInt64(hdnEditID.Value)) == 0)
            {
                lblError.Text = "Advertisment Placement already exists";
            }
            else
            {
                PAdvertismentPlacement pAdvertismentPlacement = null;
                if (hdnEditID.Value == "" || hdnEditID.Value == "0")
                {

                    pAdvertismentPlacement = new PAdvertismentPlacement();
                    pAdvertismentPlacement.CreatedOn = DateTime.Now;
                }
                else
                {
                    pAdvertismentPlacement = DBHelper.GetPlacementID(Convert.ToInt32(hdnEditID.Value));
                }

                pAdvertismentPlacement.PlacementAreaName = txtAreaName.Text;
                pAdvertismentPlacement.ISActive = Convert.ToInt32(hdnActive.Value);

                DBHelper.InsertUpdateAdvertismentPlacement(pAdvertismentPlacement);

                Response.Redirect("adplacement.aspx");
            }
        }

        protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deletePlacement")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                DBHelper.DeletePlacement(id);
                BindData();
            }
        }
    }
}