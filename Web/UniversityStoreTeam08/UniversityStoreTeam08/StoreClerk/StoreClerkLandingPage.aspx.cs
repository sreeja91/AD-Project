using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityStoreTeam08.Controller;

namespace UniversityStoreTeam08.StoreClerk
{
    public partial class StoreClerkLandingPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                List<int> inventbalance = StoreSummaryController.getTotalInventoryBalance();
                grdLowStock.DataSource = StoreSummaryController.StockIsLow();
                grdLowStock.DataBind();

                for (int i = 0; i < grdLowStock.Rows.Count; i++)
                {
                    Label lblInventBalance = (Label)grdLowStock.Rows[i].Cells[2].FindControl("lblInventBalance");
                    lblInventBalance.Text = inventbalance[i].ToString();

                }

                Label2.Text="("+grdLowStock.Rows.Count+")";

                List<ConsolidatedRequisitionList> l = viewRequisitionController.getPendingReqAllDepts();

                noofdepts.Text = l.Count.ToString();
            }
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            RetreiveStationaryController.generateDisbursementList();
            RetreiveStationaryController.generateConsolidatedListFromUnfullfilled();

            lblStatus.Text = "Consolidated List Generated Successfully";


          List<ConsolidatedRequisitionList> l=  viewRequisitionController.getPendingReqAllDepts();

          noofdepts.Text = l.Count.ToString();
        }

        protected void noofdepts_Click(object sender, EventArgs e)
        {
            Response.Redirect("../StoreClerk/ViewRequesitionForm.aspx");
        }
    }
}