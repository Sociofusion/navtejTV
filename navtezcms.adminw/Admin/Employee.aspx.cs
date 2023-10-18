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
    public partial class Employee : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnEmployeeId.Value = "";
                BindEmployee();
                BindRole();
            }
        }
        
        public void BindEmployee()
        {
            List<PEmployee> lst = DBHelper.GetEmployee();

            rptEmployee.DataSource = lst;
            rptEmployee.DataBind();
        }

        public void BindRole()
        {
            List<PRole> lst = DBHelper.GetRoles();
            ddlRole.DataSource = lst;
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "ID";
            ddlRole.DataBind();

            ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "0"));
        }

        protected void btnSaveEmployee_Click(object sender, EventArgs e)
        {
            if (hdnEmployeeId.Value == "")
            {
                hdnEmployeeId.Value = "0";
            }

            PEmployee pEmployee = new PEmployee();
            pEmployee.Name = txtName.Text;
            pEmployee.Email = txtEmailID.Text;
            pEmployee.Password = txtPassword.Text;
            pEmployee.ISActive = Convert.ToInt32(hdnISActive.Value);
            pEmployee.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
            pEmployee.CreatedOn = DateTime.Now;
            pEmployee.UpdatedOn = DateTime.Now;
            pEmployee.LastLoginTime = DateTime.Now;

            if (hdnEmployeeId.Value == "" || hdnEmployeeId.Value == "0")
            {
                DBHelper.InsertEmployee(pEmployee);
            }
            else
            {
                pEmployee.ID = Convert.ToInt32(hdnEmployeeId.Value);
                DBHelper.UpdateEmployee(pEmployee);
            }
            Response.Redirect("Employee.aspx");
        }
        protected void rptEmployee_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deleteEmployee")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                DBHelper.DeleteEmployee(id);
                BindEmployee();
            }
        }
    }
}