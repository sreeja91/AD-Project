using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;

namespace UniversityStoreTeam08.DepartmentEmployee
{
    public partial class ViewCart : System.Web.UI.Page
    {
        string empNo = "";
        List<RequestStationaryCart> cartlist=null;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("ViewCart");
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

            if (!IsPostBack)
            {
                cartlist = RequestStationaryControler.getEmployeeCart(empNo);
                
                if (cartlist != null && cartlist.Count != 0)
                {
                    grdViewCart.DataSource = cartlist;
                    grdViewCart.DataBind();
                    for (int i = 0; i < grdViewCart.Rows.Count; i++)
                    {
                        Label lbl = (Label)grdViewCart.Rows[i].Cells[0].FindControl("lblItemNumber");
                        Label lblCategory = (Label)grdViewCart.Rows[i].Cells[0].FindControl("lblCategory");
                        Label lblDesc = (Label)grdViewCart.Rows[i].Cells[0].FindControl("lblDescription");
                        Label lblUOM = (Label)grdViewCart.Rows[i].Cells[0].FindControl("lblUnitofMeasure"); //newly added

                        Product prd = ProductDAO.getProductForCart(lbl.Text);
                        lblCategory.Text = prd.Category;
                        lblDesc.Text = prd.Description;
                        lblUOM.Text = prd.UnitOfMeasure; //newly added
                        
                    }
                }
                else
                {
                    grdViewCart.DataSource = null;
                    grdViewCart.DataBind();
                    btnOrder.Visible = false;
                }
            }
        }

        protected void grdViewCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                Label lbl = (Label)grdViewCart.Rows[index].Cells[0].FindControl("lblItemNumber");

                RequestStationaryControler.deleteSelectedFromCart(lbl.Text, empNo); //hard Code

                Response.Redirect("../DepartmentEmployee/ViewCart.aspx");
                
            }
        }

        protected void btnOrder_Click(object sender, EventArgs e)
        {
            MailMessage m = new MailMessage("a0122204@nus.edu.sg", "tiger.king3@gmail.com");
            m.Subject = "Request Item";
            m.Body="This is my request items :\n";
            for (int i = 0; i < grdViewCart.Rows.Count; i++)
            {
                Label lbl = (Label)grdViewCart.Rows[i].Cells[0].FindControl("lblItemNumber");
                string desc=grdViewCart.Rows[i].Cells[2].Text;
                string qty = grdViewCart.Rows[i].Cells[3].Text;
                m.Body += lbl.Text+"    |   "+desc+"    |   "+qty;
            }
            RequestStationaryControler.submitCart(empNo);
            cartlist = RequestStationaryControler.getEmployeeCart(empNo);
            
            grdViewCart.DataSource = cartlist;
            grdViewCart.DataBind();

            lblStatus.Text = "Your request has been forwarded";
            
            SmtpClient c = new SmtpClient("lynx.iss.nus.edu.sg");
            c.Send(m);
        }
    }
}