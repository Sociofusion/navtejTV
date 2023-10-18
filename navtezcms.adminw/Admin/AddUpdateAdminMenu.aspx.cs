using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using navtezcms.DAL;
using navtezcms.BO;
using System.Reflection;

namespace navtezcms.adminw.Admin
{
    public partial class AddUpdateAdminMenu : System.Web.UI.Page
    {
        DataTable dtfilterised = new DataTable();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            getallmenu();
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    FillData(Convert.ToInt32(Request.QueryString["id"]));
                    hdnAdminMenuId.Value = Convert.ToString(Request.QueryString["id"]);
                    lblAddEdit.Text = "Update Menu";
                }
                else
                {
                    lblAddEdit.Text = "Add Menu";
                }
            }
            
        }


        #region [Insert | Update]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            PEmployee pEmployee = ((List<PEmployee>)SessionState.LoggedInPerson)[0];
            if (pEmployee != null)
            {
                if (txtMenuName.Text.Trim() == "")
                { ScriptManager.RegisterStartupScript(Page, this.GetType(), "Error", "<script>alert('Enter Menu Name !!');</script>", false); }
                else if (txtlink.Text.Trim() == "")
                { ScriptManager.RegisterStartupScript(Page, this.GetType(), "Error", "<script>alert('Enter Menu Link !!');</script>", false); }

                DataTable dt = new DataTable();
                string parentid = "";
                string menustr = "";
                if (ddllevel.SelectedValue == "1") { parentid = "0"; menustr = ""; }
                else if (ddllevel.SelectedValue == "2") { parentid = ddlp1.SelectedValue; menustr = ddlp1.SelectedValue + "," + txtid.Value.ToString().Trim(); }
                else if (ddllevel.SelectedValue == "3") { parentid = ddlp2.SelectedValue; menustr = ddlp1.SelectedValue + "," + ddlp2.SelectedValue + "," + txtid.Value.ToString().Trim(); }
                if (txtid.Value == "") { txtid.Value = "0"; }


                PAdminMenu pAdminMenu = new PAdminMenu();
                pAdminMenu.MenuName = txtMenuName.Text;
                pAdminMenu.MenuLink = txtlink.Text;
                pAdminMenu.MenuLevel = Convert.ToInt32(ddllevel.SelectedValue);
                pAdminMenu.ParentID = Convert.ToInt32(parentid);
                pAdminMenu.MenuStr = menustr;
                pAdminMenu.Position = Convert.ToInt32(txtPosition.Text);
                pAdminMenu.Icon = txtIcon.Text;
                pAdminMenu.ISActive = true;
                pAdminMenu.AddDate = System.DateTime.Now;

                if (Request.QueryString["id"] == null)
                {
                    Int64 intresult = 0;
                    intresult = DBHelper.InsertAdminMenu(pAdminMenu);
                    if (intresult > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Record inserted successfully');location.replace('AdminMenu.aspx');", true);
                        clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Menu Name Already Exists');", true);
                    }
                }
                else
                {
                    #region [Update]
                    bool intresult = false;
                    pAdminMenu.ID = Convert.ToInt32(Request.QueryString["id"]);
                    intresult = DBHelper.UpdateAdminMenu(pAdminMenu);
                    if (intresult == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Record updated successfully');location.replace('AdminMenu.aspx');", true);
                        clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Menu Name Already Exists');", true);
                    }
                    #endregion
                }
            }

        }
        #endregion

        #region [Reset]
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
        }
        #endregion

        #region [All Functions]
        private void FillData(Int64 id)
        {

            PAdminMenu pAdminMenu = DBHelper.GetAdminMenuById(id);
            if (pAdminMenu != null)
            {
                ddllevel.SelectedValue = pAdminMenu.MenuLevel.ToString();
                lblparent.Text = "Level " + pAdminMenu.MenuLevel.ToString();
                txtMenuName.Text = pAdminMenu.MenuName.ToString();
                txtPosition.Text = pAdminMenu.Position.ToString();
                txtIcon.Text = pAdminMenu.Icon.ToString();
                FillP1P2();
                if (Convert.ToInt32(pAdminMenu.ParentID) > 0)
                {
                    if (ddllevel.SelectedValue == "2")
                        ddlp1.SelectedValue = pAdminMenu.ParentID.ToString();
                    else if (ddllevel.SelectedValue == "3")
                    {
                        DataTable dtkalu = new DataTable();
                        DataTable dtcomplete = new DataTable();

                        List<PAdminMenu> lstMenu = DBHelper.GetAdminMenuByMenuLevel();
                        if (lstMenu.Count > 0)
                        {
                            dtcomplete.DefaultView.RowFilter = "ParentID='" + pAdminMenu.ParentID.ToString() + "'";
                            dtcomplete = dtcomplete.DefaultView.ToTable();
                        }
                        dtkalu = dtcomplete.Copy();
                        ddlp1.SelectedValue = dtkalu.Rows[0]["parentid"].ToString();
                    }
                }
                Fillddlp2();
                if (ddllevel.SelectedValue == "3")
                    ddlp2.SelectedValue = pAdminMenu.ParentID.ToString();
                txtlink.Text = pAdminMenu.MenuLink.ToString();


            }
        }

        private void clear()
        {
            txtMenuName.Text = "";
        }



        protected void ddllevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillP1P2();
        }
        protected void ddlp1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fillddlp2();
        }

        private void getallmenu()
        {
            List<PAdminMenu> lstMenu = DBHelper.GetAllAdminMenu();
            DataTable dt = navtezcms.BO.Common.ToDataTable(lstMenu);
            if (dt.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = "MenuLevel=1";
                dt = dt.DefaultView.ToTable();
            }
            dtfilterised = dt.Copy();

        }



        #endregion

        #region [Deepak Code for Bind Dropdown Acording to their Parent-ID]
        protected void FillP1P2()
        {
            if (ddllevel.SelectedValue == "1")
            {
                ddlp1.Items.Clear();
                ddlp2.Items.Clear();
                lblparent.Text = "Self";
            }
            else if (ddllevel.SelectedValue == "2")
            {
                ddlp1.DataTextField = "Menuname";
                ddlp1.DataValueField = "id";
                ddlp1.DataSource = dtfilterised;
                ddlp1.DataBind();
                ddlp1.SelectedIndex = 0;
                ddlp2.Items.Clear();
                lblparent.Text = "Level 1";
            }
            else
            {
                ddlp1.DataTextField = "Menuname";
                ddlp1.DataValueField = "id";
                ddlp1.DataSource = dtfilterised;
                ddlp1.DataBind();
                ddlp1.SelectedIndex = 0;
                ddlp2.Items.Clear();
                lblparent.Text = "Level 2";
            }
        }
        protected void Fillddlp2()
        {
            if (ddllevel.SelectedValue == "3")
            {
                DataTable dtkalu = new DataTable();
                DataTable dtcomplete = new DataTable();

                List<PAdminMenu> lstMenu = DBHelper.GetAdminMenuByMenuLevel();
                if (lstMenu.Count > 0)
                {
                    dtcomplete.DefaultView.RowFilter = "ParentID='" + ddlp1.SelectedValue + "'";
                    dtcomplete = dtcomplete.DefaultView.ToTable();
                }
                dtkalu = dtcomplete.Copy();

                ddlp2.DataTextField = "Menuname";
                ddlp2.DataValueField = "id";
                ddlp2.DataSource = dtkalu;
                ddlp2.DataBind();
                if (ddlp2.Items.Count > 0)
                    ddlp2.SelectedIndex = 0;
                lblparent.Text = "Level 2";
            }
        }

        
        #endregion
    }
}