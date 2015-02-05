using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace UniversityStoreTeam08.DepartmentHead
{
    public partial class ApproveRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("ImpendingRequests");
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
            Label lblWelcome = (Label)this.Master.FindControl("lblWelcome");
            lblWelcome.Text = Session["Name"].ToString();//from login username

            if (Session["ReqListID"] != null)
            {
                String id = Convert.ToString(Session["ReqListID"]);
                grdApproveReq.DataSource = ApproveRequestController.pupulateDetails(id);
                grdApproveReq.DataBind();
            }

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (Session["ReqListID"] != null)
            {
                String id = Convert.ToString(Session["ReqListID"]);
                string name = Session["EmployeeName"].ToString();

                string empNo = Session["PendingEmployeeNumber"].ToString();
                string senderID = Session["EmployeeNumber"].ToString();

                Employee receiverInfo = EmployeeDAO.getEmployeeinfoByEmployeeNumber(empNo);
                Employee senderInfo = EmployeeDAO.getEmployeeinfoByEmployeeNumber(senderID);

                string msgTitle = "Approved Request";

                string msgBody = receiverInfo.Name + "\n\n\n\n";
                msgBody += "These items are already approved.\n\n\n";
                for (int i = 0; i < grdApproveReq.Rows.Count; i++)
                {
                    string desc=grdApproveReq.Rows[i].Cells[1].Text;
                    string qty=grdApproveReq.Rows[i].Cells[2].Text;
                    msgBody += desc + "      |      " + qty;
                    msgBody += "\n";
                }

                ApproveRequestController.submitApproval(id,"web");
                SendEmail(msgTitle, msgBody, senderInfo.Email, receiverInfo.Email);


                Label1.Text = "approved";
                Response.Redirect("../DepartmentHead/PendingRequests.aspx");

                

                
            }
        }

        protected void btnDeny_Click(object sender, EventArgs e)
        {
            if (Session["ReqListID"] != null)
            {
                String id = Convert.ToString(Session["ReqListID"]);
                String comment = txtboxcomment.Text;

                string empNo = Session["PendingEmployeeNumber"].ToString();
                string senderID = Session["EmployeeNumber"].ToString();

                Employee receiverInfo = EmployeeDAO.getEmployeeinfoByEmployeeNumber(empNo);
                Employee senderInfo = EmployeeDAO.getEmployeeinfoByEmployeeNumber(senderID);

                string msgTitle = "Deny Request";

                string msgBody = receiverInfo.Name + "\n\n\n";
                msgBody += "These items are denied.\n";
                if(txtboxcomment.Text!="")
                {
                   msgBody+=" Due to this,"+txtboxcomment.Text+"\n\n";
                }
                for (int i = 0; i < grdApproveReq.Rows.Count; i++)
                {
                    string desc = grdApproveReq.Rows[i].Cells[1].Text;
                    string qty = grdApproveReq.Rows[i].Cells[2].Text;
                    msgBody += desc + " |   " + qty;
                    msgBody += "\n";
                }

                ApproveRequestController.submitRejected(id,comment);
                SendEmail(msgTitle, msgBody, senderInfo.Email, receiverInfo.Email);

                Label1.Text = "denied";
                Response.Redirect("../DepartmentHead/PendingRequests.aspx");
            }
        }

        protected void SendEmail(string title,string body,string senderEmail,string receiverEmail)
        {
            MailMessage m = new MailMessage(senderEmail, receiverEmail);
            m.Subject = title;
            m.Body = body;
            SmtpClient c = new SmtpClient("lynx.iss.nus.edu.sg");
            c.Send(m);
        }


    }
}