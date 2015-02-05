using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityStoreTeam08.DepartmentHead
{
    public partial class ImpendingRequests : System.Web.UI.Page
    {
        string empNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string role = "";
            if (Session["Role"] != null)
            {
                role = Session["Role"].ToString();
            }
            if (role == "Head")
            {
                System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("ImpendingRequests");
                li.Attributes.Add("class", "active");
            }
            else if (role == "DelegateHead")
            {
                Label1.Visible = false;
                ddlCollectorName.Visible = false;
                ddl1.Attributes.Remove("class");
                ddl2.Attributes.Remove("class");
                
                Label2.Visible = false;
                ddlCollectionPoint.Visible = false;
                
                btnSave.Visible = false;
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

                grdImpendingRequest.DataSource = ApproveRequestController.populateAllPendingReq(empNo);
                grdImpendingRequest.DataBind();


                ddlCollectorName.DataSource = EditPickUpController.getAllEmployeesOfThisHead(empNo);
                ddlCollectorName.DataTextField = "Name";
                ddlCollectorName.DataValueField = "EmployeeNumber";
                
               // ddlCollectorName.SelectedValue = EditPickUpController.GetRepresentativeName("C1");
                ddlCollectorName.DataBind();
                ddlCollectorName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select One", ""));

                txtboxrRepName.Text = EditPickUpController.GetRepresentativeName(empNo);
                txtboxcollecpt.Text = EditPickUpController.GetCollectionPoint(empNo);


                ddlCollectionPoint.DataSource = EditPickUpController.getAllCollectionPoins();
                ddlCollectionPoint.DataTextField = "Name";
                ddlCollectionPoint.DataValueField = "CollectionPointID";
               // ddlCollectorName.SelectedValue = EditPickUpController.GetCollectionPoint("C1");
                ddlCollectionPoint.DataBind();
                ddlCollectionPoint.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select One", ""));
                


            }

        }

        protected void grdImpendingRequest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                int command = Convert.ToInt32(e.CommandArgument);
                string reqlistid=  grdImpendingRequest.Rows[command].Cells[0].Text;
                string name = grdImpendingRequest.Rows[command].Cells[1].Text;

                Label lbl = (Label)grdImpendingRequest.Rows[command].Cells[6].FindControl("lblEmployeeNumber");

                Session["PendingEmployeeNumber"] = lbl.Text;
                Session["EmployeeName"] = name;
                Session["ReqListID"] = reqlistid;

                Response.Redirect("../DepartmentHead/ApproveRequest.aspx");
            }
            else
            if (e.CommandName == "UP")
            {
                int command = Convert.ToInt32(e.CommandArgument);
                string reqlistid = grdImpendingRequest.Rows[command].Cells[0].Text;

                ApproveRequestController.submitApproval(reqlistid,"web");
                grdImpendingRequest.DataSource = ApproveRequestController.populateAllPendingReq(empNo);
                grdImpendingRequest.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            string collectionpt = ddlCollectionPoint.SelectedValue;
            string repno = ddlCollectorName.SelectedValue;

            if (ddlCollectionPoint.SelectedValue == "")
            {
                lblStatus.Text = "Please Select Collection Point! <br>";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                EditPickUpController.setCollectionPt(collectionpt, empNo);
            }
            if (repno == "")
            {
                lblStatus.Text = "Please Select Department Representative! <br>";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                EditPickUpController.setRepname(repno, empNo);
            }

            if (lblStatus.Text != "")
            {
                lblStatus.Text = " New changes have been saved!";
            }

            lblError.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4582ec");
            Label3.Text = "CHANGES SAVED!";

        }

        protected void ddlCollectorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtboxrRepName.Text = ddlCollectorName.SelectedItem.ToString();
            btnSave.Enabled = true;
        }

        protected void ddlCollectionPoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtboxcollecpt.Text = ddlCollectionPoint.SelectedItem.ToString();
            btnSave.Enabled = true;
        }

        

       
    }
}