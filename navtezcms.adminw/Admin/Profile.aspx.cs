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
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProfile();
            }
        }

        public void BindProfile()
        {
            if (SessionState.LoggedInPerson != null)
            {
                List<PEmployee> lstEmployee = (List<PEmployee>)SessionState.LoggedInPerson;
                
                txtEmail.Text = lstEmployee[0].Email;
                txtName.Text = lstEmployee[0].Name;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPassword.Text.Trim();
            string NewPassword = txtConfirmPassword.Text.Trim();
            if (SessionState.LoggedInPerson != null)
            {
                List<PEmployee> lstEmployee = (List<PEmployee>)SessionState.LoggedInPerson;
                if (lstEmployee.Count > 0)
                {
                    List<PEmployee> lst = DBHelper.GetEmployeeByEmailPassword(lstEmployee[0].Email, oldPassword);
                    if (lst.Count > 0)
                    {
                        PEmployee pEmployee = new PEmployee();
                        pEmployee = lstEmployee[0];
                        pEmployee.Password = NewPassword;
                        DBHelper.UpdateEmployee(pEmployee);
                        
                        litMessage.Text = "Your Password has been changed.";
                        litMessage.Visible = true;
                    }
                    else
                    {
                        litMessage.Text = "Current Password is not correct.";
                        litMessage.Visible = true;
                    }
                }
            }
        }

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            //First check for existing email id
            if (SessionState.LoggedInPerson != null)
            {
                List<PEmployee> lstEmployee = (List<PEmployee>)SessionState.LoggedInPerson;
                if (lstEmployee.Count > 0)
                {
                    PEmployee pEmployee = lstEmployee[0];
                    
                    string name = txtName.Text.Trim().Replace("'", "");
                    string emailID = txtEmail.Text.Trim().Replace("'", "");

                    pEmployee.ID = lstEmployee[0].ID;
                    pEmployee.Name = name;
                    pEmployee.Email = emailID;
                    

                    bool result = DBHelper.UpdateEmployee(pEmployee);
                    if (result == true)
                    {
                        pEmployee = DBHelper.GetEmployeeById(lstEmployee[0].ID);
                        SessionState.LoggedInPerson = pEmployee;
                        lblTopAlert.Text = "*Your profile has been updated successfully.";
                        lblTopAlert.Visible = true;
                    }
                    else
                    {
                        lblTopAlert.Text = "*Server Error.";
                        lblTopAlert.Visible = true;
                    }
                }
            }
        }
        
    }
}