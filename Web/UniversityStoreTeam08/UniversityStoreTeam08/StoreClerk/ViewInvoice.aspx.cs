using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityStoreTeam08.StoreClerk
{
    public partial class ViewInvoice : System.Web.UI.Page
    {
        List<Object> l = new List<object>();
        //HttpCookie aCOOKIE;
        //HttpCookie bCOOKIE;
        
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

            if (!IsPostBack)
            {
                ddlSupplier.DataSource = ViewInvoiceController.getSuppliers();
                ddlSupplier.DataBind();
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            l = ViewInvoiceController.appearInvoice(ddlSupplier.SelectedItem.Text);
            grdDisbursementList.DataSource = l;
            grdDisbursementList.DataBind();
        }

        protected void grdDisbursementList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdDisbursementList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int command = Convert.ToInt32(e.CommandArgument);

                string po = grdDisbursementList.Rows[command].Cells[0].Text;
                string status = grdDisbursementList.Rows[command].Cells[5].Text;

                Session["po"] = po;
                Session["STATUS"] = status;

                //Session["po"] = grdDisbursementList.SelectedRow.Cells[0].Text;

                //Session["STATUS"] = grdDisbursementList.SelectedRow.Cells[4].Text;
                //aCOOKIE = new HttpCookie("PO");
                //aCOOKIE.Value = grdDisbursementList.SelectedRow.Cells[0].Text;
                //aCOOKIE.Expires = DateTime.Now.AddDays(1);
                //Response.Cookies.Add(aCOOKIE);


                //bCOOKIE = new HttpCookie("STATUS");
                //bCOOKIE.Value = grdDisbursementList.SelectedRow.Cells[4].Text;
                //bCOOKIE.Expires = DateTime.Now.AddDays(1);
                //Response.Cookies.Add(bCOOKIE);
               
                Response.Redirect("../StoreClerk/AcknowledgeOrder.aspx");
            }
        }
    }
}