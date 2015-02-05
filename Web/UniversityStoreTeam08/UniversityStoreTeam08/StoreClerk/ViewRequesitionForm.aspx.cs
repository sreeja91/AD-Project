using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityStoreTeam08.StoreClerk
{
    public partial class ViewRequesitionForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("ViewRequisitionForm");
            li.Attributes.Add("class", "active");

            if (Session["Role"] != null)
            {
                if (Session["Role"].ToString() == "Employee")
                {
                    Response.Redirect("../DepartmentEmployee/RequestStationery.aspx");
                }
                else if (Session["Role"].ToString() == "DelegateHead")
                {
                    Response.Redirect("../DepartmentEmployee/RequestStationery.aspx");
                }
                else if (Session["Role"].ToString() == "Head")
                {
                    Response.Redirect("../DepartmentHead/PendingRequests.aspx");
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

            Label lblWelcome = (Label)this.Master.FindControl("lblWelcome");
            lblWelcome.Text = Session["Name"].ToString();//from login username

            if (!IsPostBack)
            {
                ddlDepartmentName.DataSource = viewRequisitionController.GetAllDepartments();
                ddlDepartmentName.DataTextField = "DepatmentName";
                ddlDepartmentName.DataValueField = "DepartmentCode";
                ddlDepartmentName.DataBind();

                String dept = ddlDepartmentName.SelectedValue;

            }
            
        }

        protected void btnView_Click(object sender, EventArgs e)
        {

            String dept = ddlDepartmentName.SelectedValue;
            txtApprovedBy.Text = viewRequisitionController.getDeptHead(dept);

            grdImpendingRequest.DataSource = viewRequisitionController.getAllImpendingList(dept);
            grdImpendingRequest.DataBind();

        }

        protected void ddlDepartmentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            String dept = ddlDepartmentName.SelectedValue;

            txtApprovedBy.Text = viewRequisitionController.getDeptHead(dept);

            grdImpendingRequest.DataSource = viewRequisitionController.getAllImpendingList(dept);
            grdImpendingRequest.DataBind();
        }
    }
}