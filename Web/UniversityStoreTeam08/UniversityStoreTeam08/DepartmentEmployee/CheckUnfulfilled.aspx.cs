using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityStoreTeam08.DepartmentEmployee
{
    public partial class CheckUnfulfilled : System.Web.UI.Page
    {
        string empNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("CheckUnfulfilled");
            li.Attributes.Add("class", "active");
            if (Session["Role"] != null)
            {
                if (Session["Role"].ToString() == "Head")
                {
                    Response.Redirect("../DepartmentHead/PendingRequests.aspx");
                }
                else if (Session["Role"].ToString() == "StoreClerk")
                {
                    Response.Redirect("../StoreClerk/ViewRequesitionForm.aspx");
                }
                else if (Session["Role"].ToString() == "StoreSupervisor")
                {
                    Response.Redirect("../StoreSupervisor/PendingAdjustment.aspx");
                }
                else if (Session["Role"].ToString() == "StoreManager")
                {
                    Response.Redirect("../StoreSupervisor/PendingAdjustment.aspx");
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
            empNo = Session["EmployeeNumber"].ToString();

            Label lblWelcome = (Label)this.Master.FindControl("lblWelcome");
            
            lblWelcome.Text = Session["Name"].ToString();//from login username

            grdItemList.DataSource = CheckUnfulfilledController.GetUnfulfilledInfo(empNo);       //need to change
            grdItemList.DataBind();

        }
    }
}