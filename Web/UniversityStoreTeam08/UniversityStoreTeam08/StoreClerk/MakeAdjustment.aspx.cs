using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityStoreTeam08.StoreClerk
{
    public partial class MakeAdjustment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("MakeAdjustments");
            li.Attributes.Add("class", "active");

            Label lblWelcome = (Label)this.Master.FindControl("lblWelcome");
            lblWelcome.Text = "Store Clerk";//from login username
        }
    }
}