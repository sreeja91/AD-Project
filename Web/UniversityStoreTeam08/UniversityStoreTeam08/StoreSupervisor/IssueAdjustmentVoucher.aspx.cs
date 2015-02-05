using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityStoreTeam08
{
    public partial class IssueAdjustmentVoucher : System.Web.UI.Page
    {
        string role = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("PendingAdjustments");
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

            if (Session["voucherNo"] != null)
            {
                string q=(string)Session["voucherNo"];
                int s = Convert.ToInt32(q);
                txtVoucherNo.Text = q;
                string m=(string)Session["Date"];
                txtDate.Text = m;
                
                if (Session["Role"] != null)
                {
                    role = Session["Role"].ToString();
                }

                ///*******Wu's Code********///
                    //GridViewIssueadjVoucher.DataSource = adjustmentVoucherController.getTheAdjustmentVoucherDetail(s);
                    //GridViewIssueadjVoucher.DataBind();
                ///*******Wu's Code********///
                ///

                GridViewIssueadjVoucher.DataSource = IssueAdjustmentVoucherController.retrieveAllItemsWithVoucherNumber(Convert.ToInt32(q), role);
                GridViewIssueadjVoucher.DataBind();
                

            }

        }

        protected void btnIssue_Click(object sender, EventArgs e)
        {
           
            string Pid = GridViewIssueadjVoucher.Rows[0].Cells[0].Text;
            int qty =Convert.ToInt32 (GridViewIssueadjVoucher.Rows[0].Cells[1].Text);
           
            ///*****Wu's Code*****///
                //adjustmentVoucherController.UpdatePositiveStock(Pid,qty);
                ////adjustmentVoucherController.UpdatenegativeStock(Pid,qty); 
                //adjustmentVoucherController.deleteObjectDetail(Convert.ToInt32(txtVoucherNo.Text));
                //adjustmentVoucherController.deleteObject(Convert.ToInt32(txtVoucherNo.Text));
            ///*****Wu's Code*****///
            ///
            IssueAdjustmentVoucherController.approveItemsForVoucherNumber(Convert.ToInt32(txtVoucherNo.Text), role);

            Response.Redirect("~/StoreSupervisor/PendingAdjustment.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreSupervisor/PendingAdjustment.aspx");
        }
    }
}