using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityStoreTeam08.StoreClerk
{
    public partial class AcknowledgeOrder : System.Web.UI.Page
    {
        public string STATUS_PENDING = "pending";
        public string STATUS_ACKNOWLEGE = "acknowledge";

        protected void Page_Load(object sender, EventArgs e)
        {

            System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("ViewInvoice");
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

            if (Session["po"] != null)
            {
                int s = Convert.ToInt32((string)(Session["po"]));
                
                //int s = Convert.ToInt32(Response.Cookies["po"].Value);

                grdImpendingRequest.DataSource = AcknowlegeOrderController.getAllOd(s);
                //grdImpendingRequest.DataSourceID = "";
                grdImpendingRequest.DataBind();

            }

            if (Session["STATUS"] != null)
            {
                string statu = (string)Session["STATUS"];

                //if (statu == STATUS_ACKNOWLEGE)
                //{
                //    btnAcknowledge.Enabled = false;
                //}
                //else
                //{
                //    btnAcknowledge.Enabled = true;
                //}

                if (statu == STATUS_ACKNOWLEGE)
                {
                    btnAcknowledge.Visible = false;
                    btnBack.Visible = true;
                    btnBack.Enabled = true;
                }
                else
                {
                    btnAcknowledge.Enabled = true;
                    btnBack.Visible = false;
                }
            }

        }

        protected void grdImpendingRequest_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAcknowledge_Click(object sender, EventArgs e)
        {
            //string statu = (string)(Session["STATUS"]);

            if (Session["po"] != null)
            {
                int s = Convert.ToInt32((string)(Session["po"]));

                AcknowlegeOrderController.changeStatus(s);
            }

            Response.Redirect("../StoreClerk/ViewInvoice.aspx");
        
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../StoreClerk/ViewInvoice.aspx");
        }
    }
}