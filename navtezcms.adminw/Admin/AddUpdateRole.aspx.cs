using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using navtezcms.BO;
using navtezcms.DAL;

namespace navtezcms.adminw.Admin
{
    public partial class AddUpdateRole : System.Web.UI.Page
    {
        DataTable TreeDT = new DataTable();
        DataTable TreeDTEmpType = new DataTable();
        DataTable TreeDTRoles = new DataTable();
        DataTable TreeDTRoleInEmpType = new DataTable();
        DataTable TreeDTCategory = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    hdnRoleId.Value = Request.QueryString["id"];
                    FillData(Convert.ToInt32(Request.QueryString["id"]));
                    lblAddEdit.Text = "Update Role";
                    BindTree("GetByRoleId", Convert.ToInt32(Request.QueryString["id"]));
                    // Collapse Menu Kal Krna hai
                    BindTreeCategory("GetByRoleId", Convert.ToInt32(Request.QueryString["id"]));
                }
                else
                {
                    lblAddEdit.Text = "Add Role";
                    BindTree("GetAll", 0);
                    BindTreeCategory("GetAll", 0);
                }
            }
        }
        public void FillData(int id)
        {
            List<PRole> lstRole = DBHelper.GetRolesById(id);
            txtRoleName.Text = lstRole[0].RoleName;
            chkISActive.Checked = lstRole[0].IsActive;
            chkIsShowAll.Checked = lstRole[0].ISShowAll;
            btnSaveRole.Text = "Update";
        }
        public void BindTree(string str, int id)
        {
            TreeMenu.Nodes.Clear();

            List<PAdminMenu> lstMenu = DBHelper.GetRoleRightsMenu(str, id);
            TreeDT = navtezcms.BO.Common.ToDataTable(lstMenu);

            DataRow[] dr = TreeDT.Select("MenuLevel=1");
            for (int i = 0; i < dr.Length; i++)
            {
                TreeNode mNode = new TreeNode();
                mNode.Expanded = false;
                mNode.Text = dr[i]["MenuName"].ToString();
                mNode.Value = dr[i]["id"].ToString();
                mNode.Checked = Convert.ToBoolean(dr[i]["IsChecked"]);
                mNode.SelectAction = TreeNodeSelectAction.Expand;
                mNode.PopulateOnDemand = true;
                TreeMenu.Nodes.Add(mNode);
                mNode.ExpandAll();
            }
            TreeMenu.CollapseAll();
        }
        public void BindTreeCategory(string str, int id)
        {
            TreeMenuCategory.Nodes.Clear();
            List<PCategory> lstCategory = DBHelper.GetRoleRightsCategory(str, id);
            TreeDTCategory = navtezcms.BO.Common.ToDataTable(lstCategory);

            DataRow[] dr = TreeDTCategory.Select("ParentCategoryID=0");

            for (int i = 0; i < dr.Length; i++)
            {
                TreeNode mNode = new TreeNode();
                mNode.Expanded = false;
                mNode.Text = dr[i]["DefaultTitleToDisplay"].ToString();
                mNode.Value = dr[i]["ID"].ToString();
                mNode.Checked = Convert.ToBoolean(dr[i]["ISChecked"]);
                mNode.SelectAction = TreeNodeSelectAction.Expand;
                mNode.PopulateOnDemand = true;
                TreeMenuCategory.Nodes.Add(mNode);
                mNode.ExpandAll();
            }
            TreeMenuCategory.CollapseAll();
        }

        protected void TreeMenu_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            ShowData(e.Node);
        }

        public void ShowData(TreeNode Tnode)
        {
            DataRow[] drnod = TreeDT.Select("ParentID='" + Tnode.Value + "'");
            for (int j = 0; j < drnod.Length; j++)
            {
                TreeNode nod = new TreeNode();
                nod.Value = drnod[j]["id"].ToString();
                nod.Text = drnod[j]["MenuName"].ToString();
                nod.Checked = Convert.ToBoolean(drnod[j]["IsChecked"]);
                nod.PopulateOnDemand = true;
                nod.Expanded = false;
                nod.SelectAction = TreeNodeSelectAction.Expand;
                Tnode.ChildNodes.Add(nod);
            }
        }

        protected void TreeMenuCategory_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            ShowDataCategory(e.Node);
        }

        public void ShowDataCategory(TreeNode Tnode)
        {
            DataRow[] drnod = TreeDTCategory.Select("ParentCategoryID='" + Tnode.Value + "'");
            for (int j = 0; j < drnod.Length; j++)
            {
                TreeNode nod = new TreeNode();
                nod.Value = drnod[j]["ID"].ToString();
                nod.Text = drnod[j]["DefaultTitleToDisplay"].ToString();
                nod.Checked = Convert.ToBoolean(drnod[j]["ISChecked"]);
                nod.PopulateOnDemand = true;
                nod.Expanded = false;
                nod.SelectAction = TreeNodeSelectAction.Expand;
                Tnode.ChildNodes.Add(nod);
            }
        }

        protected void btnSaveRole_Click(object sender, EventArgs e)
        {
            if (SessionState.LoggedInPerson != null)
            {
                List<PEmployee> lstEmployee = (List<PEmployee>)SessionState.LoggedInPerson;
                if (lstEmployee.Count > 0)
                {
                    PRole obj = new PRole();
                    obj.RoleName = txtRoleName.Text;

                    // TreeMenuWeb
                    string MenuIDStr = "";
                    foreach (TreeNode node in TreeMenu.CheckedNodes)
                    {
                        MenuIDStr = MenuIDStr + node.Value + ",";
                    }
                    obj.MenuIds = MenuIDStr;


                    // TreeMenuLab
                    string LabStr = "";
                    foreach (TreeNode node in TreeMenuCategory.CheckedNodes)
                    {
                        LabStr = LabStr + node.Value + ",";
                    }
                    obj.CategoryIds = LabStr;
                    obj.CreatedDate = DateTime.Now;
                    obj.IsActive = chkISActive.Checked;
                    obj.ISShowAll = chkIsShowAll.Checked;

                    string updateId = hdnRoleId.Value;
                    int intresult = 0;
                    if (updateId != "")
                    {
                        obj.ID = Convert.ToInt32(hdnRoleId.Value);

                        intresult = Convert.ToInt32(DBHelper.UpdateRoles(obj));
                        if (intresult == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Record updated successfully');location.replace('Roles.aspx');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Role already exist');", true);
                        }

                    }
                    else
                    {
                        intresult = DBHelper.InsertRoles(obj);
                        if (intresult > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Record inserted successfully');location.replace('Roles.aspx');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Role already exist');", true);
                        }
                    }
                }
            }
        }
    }
}