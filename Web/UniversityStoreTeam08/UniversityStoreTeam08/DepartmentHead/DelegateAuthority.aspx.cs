using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace UniversityStoreTeam08.DepartmentHead
{
    public partial class DelegateAuthority : System.Web.UI.Page
    {
        string empNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("DelegateAuthority");
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
            empNo = Session["EmployeeNumber"].ToString();

            Label lblWelcome = (Label)this.Master.FindControl("lblWelcome");
            lblWelcome.Text = Session["Name"].ToString();//from login username

            if (!IsPostBack)
            {
                Load();
            }
        }

        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            grdImpendingRequest.DataSource = DelegateAuthorityController.searchByEmployeeName(txtSearchEmployee.Text, empNo);
            grdImpendingRequest.DataBind();   
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (lblCurrentEPNumber.Text != "")
            {
                DelegateAuthorityController.RemoveDelegate(lblCurrentEPNumber.Text);
                txtCurrentDelegate.Text = "";
                grdImpendingRequest.DataSource = DelegateAuthorityController.getEmployeeList(empNo);
                grdImpendingRequest.DataBind();
            }
            btnDelegate.Enabled = true;
        }

        protected void btnDelegate_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < grdImpendingRequest.Rows.Count; i++)
            {
                RadioButton rdo = (RadioButton)grdImpendingRequest.Rows[i].Cells[0].FindControl("rdoSelect");
                if (rdo.Checked)
                {

                    Label lbl = (Label)grdImpendingRequest.Rows[i].Cells[1].FindControl("lblEmployeeNumber");
                        DelegateAuthorityController.createDelegate(lbl.Text);
                        Load();
                }
            }
        }

        protected void Load()
        {
            List<Employee> CurrentDelegateList = DelegateAuthorityController.getCurrentDelegate(empNo);
            if (CurrentDelegateList != null && CurrentDelegateList.Count != 0)
            {
                txtCurrentDelegate.Text = CurrentDelegateList[0].Name;
                lblCurrentEPNumber.Text = CurrentDelegateList[0].EmployeeNumber;
            }

            if (lblCurrentEPNumber.Text != "")
            {
                btnDelegate.Enabled = false;
            }

            grdImpendingRequest.DataSource = DelegateAuthorityController.getEmployeeList(empNo);
            grdImpendingRequest.DataBind();
        }
    }
}