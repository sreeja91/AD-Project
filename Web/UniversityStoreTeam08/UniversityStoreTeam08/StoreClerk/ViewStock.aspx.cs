using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityStoreTeam08.StoreClerk
{
    public partial class ViewStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("ViewStock");
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
                grdItemList.DataSource = ViewStockController.populateAll();
                grdItemList.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlSearchType.SelectedValue == "Category")
            {
                grdItemList.PageIndex = 0;
                grdItemList.DataSource = ViewStockController.findStock(txtSearch.Text);
                grdItemList.DataBind();
                
            }
            else if(ddlSearchType.SelectedValue == "Description")
            {
                grdItemList.PageIndex = 0;
                grdItemList.DataSource = ViewStockController.findStockByDescp(txtSearch.Text);
                grdItemList.DataBind();

            }
        }

        protected void grdItemList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdItemList.PageIndex = e.NewPageIndex;

            if (txtSearch.Text != "")
            {
                if (ddlSearchType.SelectedValue == "Category")
                {
                    grdItemList.DataSource = ViewStockController.findStock(txtSearch.Text);
                    grdItemList.DataBind();
                }
                else if (ddlSearchType.SelectedValue == "Description")
                {
                    grdItemList.DataSource = ViewStockController.findStockByDescp(txtSearch.Text);
                    grdItemList.DataBind();

                }
            }
            else
            {
                grdItemList.DataSource = ViewStockController.populateAll();
                grdItemList.DataBind();
            }
        }
    }
}