using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityStoreTeam08.Controller;

namespace UniversityStoreTeam08
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] != null)
            {
                if (Session["Role"].ToString() == "Head")
                {
                    Response.Redirect("DepartmentHead/PendingRequests.aspx");
                }
                else if (Session["Role"].ToString() == "Employee")
                {
                    Response.Redirect("DepartmentEmployee/RequestStationery.aspx");
                }
                else if (Session["Role"].ToString() == "StoreClerk")
                {
                    Response.Redirect("StoreClerk/ViewRequesitionForm.aspx");
                }
                else if (Session["Role"].ToString() == "StoreSupervisor")
                {
                    Response.Redirect("StoreSupervisor/PendingAdjustment.aspx");
                }
                else if (Session["Role"].ToString() == "StoreManager")
                {
                    Response.Redirect("StoreSupervisor/PendingAdjustment.aspx");
                }
                else
                {
                    Response.Redirect("DepartmentEmployee/RequestStationery.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Session["Name"] == null)
            {
                string usernamne = txtUserName.Text;
                string pass = txtPassword.Text;

                Employee emp = LoginController.validateLogin(txtUserName.Text, txtPassword.Text);

                if (emp != null)
                {
                    if (emp.Role == "Head")
                    {
                        Session["Name"] = emp.Name;
                        Session["EmployeeNumber"] = emp.EmployeeNumber;
                        Session["DepartmentCode"] = emp.DepartmentCode;
                        Session["Role"] = emp.Role;
                        Response.Redirect("DepartmentHead/PendingRequests.aspx");
                    }
                    else if (emp.Role == "Employee")
                    {
                        Session["Name"] = emp.Name;
                        Session["EmployeeNumber"] = emp.EmployeeNumber;
                        Session["DepartmentCode"] = emp.DepartmentCode;
                        Session["Role"] = emp.Role;
                        Response.Redirect("DepartmentEmployee/RequestStationery.aspx");
                    }
                    else if (emp.Role == "StoreClerk")
                    {
                        Session["Name"] = emp.Name;
                        Session["EmployeeNumber"] = emp.EmployeeNumber;
                        Session["DepartmentCode"] = emp.DepartmentCode;
                        Session["Role"] = emp.Role;
                        Response.Redirect("StoreClerk/StoreClerkLandingPage.aspx");
                    }
                    else if (emp.Role == "StoreSupervisor")
                    {
                        Session["Name"] = emp.Name;
                        Session["EmployeeNumber"] = emp.EmployeeNumber;
                        Session["DepartmentCode"] = emp.DepartmentCode;
                        Session["Role"] = emp.Role;
                        Response.Redirect("StoreSupervisor/PendingAdjustment.aspx");
                    }
                    else if (emp.Role == "StoreManager")
                    {
                        Session["Name"] = emp.Name;
                        Session["EmployeeNumber"] = emp.EmployeeNumber;
                        Session["DepartmentCode"] = emp.DepartmentCode;
                        Session["Role"] = emp.Role;
                        Response.Redirect("StoreSupervisor/PendingAdjustment.aspx");
                    }
                    else
                    {
                        Session["Name"] = emp.Name;
                        Session["EmployeeNumber"] = emp.EmployeeNumber;
                        Session["DepartmentCode"] = emp.DepartmentCode;
                        Session["Role"] = emp.Role;
                        Response.Redirect("DepartmentEmployee/RequestStationery.aspx");
                    }
                }
                else
                {
                    lblMessage.Text = "User name and Password does not match!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                if (Session["Role"].ToString() == "Head")
                {
                    Response.Redirect("DepartmentHead/PendingRequests.aspx");
                }
                else if (Session["Role"].ToString() == "Employee")
                {
                    Response.Redirect("DepartmentEmployee/RequestStationery.aspx");
                }
                else if (Session["Role"].ToString() == "StoreClerk")
                {
                    Response.Redirect("StoreClerk/ViewRequesitionForm.aspx");
                }
                else if (Session["Role"].ToString() == "StoreSupervisor")
                {
                    Response.Redirect("StoreSupervisor/PendingAdjustment.aspx");
                }
                else if (Session["Role"].ToString() == "StoreManager")
                {
                    Response.Redirect("StoreSupervisor/PendingAdjustment.aspx");
                }
                else
                {
                    Response.Redirect("DepartmentEmployee/RequestStationery.aspx");
                }
            }
        }
    }
}