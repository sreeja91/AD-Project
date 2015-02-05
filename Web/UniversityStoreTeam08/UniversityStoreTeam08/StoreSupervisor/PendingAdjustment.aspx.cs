using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityStoreTeam08.StoreSupervisor
{
    public partial class PendingRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string role="";
            if (Session["Role"] != null)
            {
                role = Session["Role"].ToString();
            }
            if (role == "StoreSupervisor")
            {
                System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("PendingAdjustments");
                li.Attributes.Add("class", "active");
            }
            else
            {
                System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("AdjustmentVoucher");
                li.Attributes.Add("class", "active");
            }

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
                else if (Session["Role"].ToString() == "StoreClerk")
                {
                    Response.Redirect("../StoreClerk/StoreClerkLandingPage.aspx");
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }

            Label lblWelcome = (Label)this.Master.FindControl("lblWelcome");
            lblWelcome.Text = Session["Name"].ToString();//from login username

            ///***** Wu's Code******///
                //grdPendingAdjustment.DataSource = adjustmentVoucherController.getAllPendingAdjustmentVoucher();
                //grdPendingAdjustment.DataBind();
            ///***** Wu's Code******///
            ///
            if (!IsPostBack)
            {
                List<AdjustmentVoucher> adj_list = IssueAdjustmentVoucherController.retrieveAllIssueVoucherForRole(role);
                if (adj_list.Count == 0)
                {
                    grdPendingAdjustment.DataSource = null;
                }
                else
                {
                    grdPendingAdjustment.DataSource = IssueAdjustmentVoucherController.retrieveAllIssueVoucherForRole(role);
                }
                grdPendingAdjustment.DataBind();
            }
        }
        

        protected void grdPendingAdjustment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int command = Convert.ToInt32(e.CommandArgument);

                string s = grdPendingAdjustment.Rows[command].Cells[0].Text;
                Session["voucherNo"] = s;

                string k = grdPendingAdjustment.Rows[command].Cells[1].Text;
                Session["Date"]=k;

                Response.Redirect("../StoreSupervisor/IssueAdjustmentVoucher.aspx");
            }
        }


    }
}