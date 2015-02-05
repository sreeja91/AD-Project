using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Testing
{
    public partial class MyMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string role ="";
            if (Session["Role"] != null)
            {
                role = Session["Role"].ToString();
                if (role == "Employee" || role == "DelegateHead")
                {
                    if (role == "DelegateHead")
                    {
                        System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("DelegateImpendingRequests");
                        li.Visible = true;
                    }
                    MultiView1.ActiveViewIndex = 0;
                }
                else if (role == "Head")
                {
                    MultiView1.ActiveViewIndex = 1;
                }
                else if (role == "StoreSupervisor")
                {
                    MultiView1.ActiveViewIndex = 2;
                }
                else if (role == "StoreManager")
                {
                    MultiView1.ActiveViewIndex = 4;
                }
                else
                {
                    MultiView1.ActiveViewIndex = 3;
                }
            }
            }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("../Login.aspx");
        }
            
        
    }
}