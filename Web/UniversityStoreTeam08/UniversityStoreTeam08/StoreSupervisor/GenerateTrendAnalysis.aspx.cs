using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System.Data;
using System.Configuration;

namespace UniversityStoreTeam08.StoreSupervisor
{
    public partial class GenerateTrendAnalysis : System.Web.UI.Page
    {
        string UserName = ConfigurationManager.AppSettings["UserName"].ToString();
        string Password = ConfigurationManager.AppSettings["Password"].ToString();
        string ServerName = ConfigurationManager.AppSettings["Server"].ToString();
        string Database = ConfigurationManager.AppSettings["Database"].ToString();

        ReportDocument report = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("GenerateTrendAnalysis");
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
                

                lstCategwiseFromCategory.DataSource = ProductDAO.selectAllByGroupByCategory();
                lstCategwiseFromCategory.DataValueField = "Category";
                lstCategwiseFromCategory.DataTextField = "Category";
                lstCategwiseFromCategory.DataBind();
                

                ddlCategory.DataSource = ProductDAO.selectAllByGroupByCategory();
                ddlCategory.DataValueField = "Category";
                ddlCategory.DataTextField = "Category";
                ddlCategory.DataBind();
                

                ddlMonthwiseCategory.DataSource = ProductDAO.selectAllByGroupByCategory();
                ddlMonthwiseCategory.DataValueField = "Category";
                ddlMonthwiseCategory.DataTextField = "Category";
                ddlMonthwiseCategory.DataBind();
                
                for (int i = 1900; i <= 2030; i++)
                {
                    ddlYear.Items.Add(i.ToString());
                }
                ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
            }
            //if (Session["ReportName"] != null && Session["MonthToView"] != null)
            //{
            //    string[] month_array = (string[])Session["MonthToView"];
            //    ViewReport(Session["ReportName"].ToString(), month_array);
            //}
            //btnShowReport_Click(null, null);
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            string month = ddlMonth.SelectedValue;
            string year = ddlYear.SelectedValue;
            string monthplusyear = year + "-" + month;
            ListItem MonthToView = new ListItem(monthplusyear);

            if (!lbMonth.Items.Contains(MonthToView) && lbMonth.Items.Count<3)
            {
                lbMonth.Items.Add(MonthToView);
            }
            else
            {
                lblStatus.Text = "Exact month already exist in List";
            }
        }

        protected void btnDeSelect_Click(object sender, EventArgs e)
        {
            if (lbMonth.Items.Count != 0)
            {
                lbMonth.Items.Remove(lbMonth.SelectedItem);

            }
        }

        protected void btnShowReport_Click(object sender, EventArgs e)
        {
            if ((lbMonth.Items.Count > 0) && (ddlReport.SelectedIndex != -1) || (lbMonth.Items.Count < 4))
            {
                Reports.OrderSummary ord = new Reports.OrderSummary();
                string reportName = ddlReport.SelectedValue;

                DataSet.DataSet1 ds = new DataSet.DataSet1();
                //Session["ReportName"] = reportName;
                string[] month_arr = new string[lbMonth.Items.Count];
                for (int i = 0; i < lbMonth.Items.Count; i++)
                {
                    month_arr[i] = lbMonth.Items[i].ToString();
                }
                //Session["MonthToView"] = month_arr;
                string month1 = "", month2 = "", month3 = "";
                if (month_arr.Count() > 0)
                {

                    month1 = month_arr[0].ToString();
                    if (month_arr.Count() == 3)
                    {
                        month2 = month_arr[1].ToString();
                        month3 = month_arr[2].ToString();
                    }
                    else if (month_arr.Count() == 2)
                    {
                        month2 =month_arr[1].ToString();
                        month3 = "";
                    }
                    else
                    {
                        month2 = "";
                        month3 = "";
                    }
                }
                DataSet.DataSet1TableAdapters.OrderSummaryTableAdapter ta = new DataSet.DataSet1TableAdapters.OrderSummaryTableAdapter();
                ta.Fill(ds.OrderSummary,month1,month2,month3);

                //DataSet1TableAdapters.threemonthsreportTableAdapter tmta = new DataSet1TableAdapters.threemonthsreportTableAdapter();
                //tmta.Fill(ds.threemonthsreport, month1, month2, month3, categ);
                ord.SetDataSource(ds);
                //crp.SetDataSource(ds);
                CrView.ReportSource = ord;
            }
            else
                lblStatus.Text = "Please select Months to view";
        }

        

        protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReport.SelectedItem.Text == "Order Summary")
            {
                rdoOneCategoryThreeDept.Visible = false;
                rdoOneCategoryThreeMonths.Visible = false;
                rdoOneDeptThreeCategory.Visible = false;
                rdoOneCategoryThreeMonths.Checked = false;
                MultiView1.ActiveViewIndex = 0;
            }
            else
            {
                rdoOneCategoryThreeDept.Visible = true;
                rdoOneCategoryThreeMonths.Visible = true;
                rdoOneDeptThreeCategory.Visible = true;
                rdoOneCategoryThreeMonths.Checked = true;
                MultiView1.ActiveViewIndex = 1;
            }
        }

        protected void btnPut_Click(object sender, EventArgs e)
        {
            if (!lstToDepartment.Items.Contains(lstFromDepartment.SelectedItem) && lstToDepartment.Items.Count<3)
            {
                lstToDepartment.Items.Add(lstFromDepartment.SelectedItem.Text);
            }
        }

        protected void btnRetrieve_Click(object sender, EventArgs e)
        {
            lstToDepartment.Items.Remove(lstToDepartment.SelectedItem.Text);
        }

        protected void btnMonthWise_Click(object sender, EventArgs e)
        {

            //report.Load(Server.MapPath("~/Reports/AnyThreeMonths.rpt"));
            //report.FileName = Server.MapPath("~/Reports/AnyThreeMonths.rpt");

            Reports.CrystalReport1 anyThree = new Reports.CrystalReport1();

            

           
            int month1=0, month2=0, month3=0, year1=0, year2=0, year3 = 0;
             DataSet.DataSet1 ds= new DataSet.DataSet1();



             string categ = ddlMonthwiseCategory.SelectedItem.ToString();


            string[] month_arr = new string[lbMonth.Items.Count];
            string[] year_arr = new string[lbMonth.Items.Count];
            for (int i = 0; i < lbMonth.Items.Count; i++)
            {
                month_arr[i] = lbMonth.Items[i].ToString().Substring(5, 2);
                year_arr[i] = lbMonth.Items[i].ToString().Substring(0, 4);
            }

            if (month_arr.Count() > 0)
            {
                month1 = Convert.ToInt32(month_arr[0]);
                year1 = Convert.ToInt32(year_arr[0]);
                if (month_arr.Count() == 3)
                {
                    month2 = Convert.ToInt32(month_arr[1]);
                    year2 = Convert.ToInt32(year_arr[1]);
                    month3 = Convert.ToInt32(month_arr[2]);
                    year3 = Convert.ToInt32(year_arr[2]);
                }
                else if (month_arr.Count() == 2)
                {
                    month2 = Convert.ToInt32(month_arr[1]);
                    year2 = Convert.ToInt32(year_arr[1]);
                }
            }

            DataSet.DataSet1TableAdapters.AnythreeMonthReportTableAdapter ta = new DataSet.DataSet1TableAdapters.AnythreeMonthReportTableAdapter();
            ta.Fill(ds.AnythreeMonthReport, month1, month2, month3, year1, year2, year3, categ);

            //DataSet1TableAdapters.threemonthsreportTableAdapter tmta = new DataSet1TableAdapters.threemonthsreportTableAdapter();
            //tmta.Fill(ds.threemonthsreport, month1, month2, month3, categ);
            anyThree.SetDataSource(ds);
            //crp.SetDataSource(ds);
            CrView.ReportSource = anyThree;
        }

        protected void btnDeptWise_Click(object sender, EventArgs e)
        {
            //report.Load(Server.MapPath("~/Reports/ThreeDepts.rpt"));
            //report.FileName = Server.MapPath("~/Reports/ThreeDepts.rpt");

            Reports.ThreeDepts rpt = new Reports.ThreeDepts();

            int month1 = 0, month2 = 0, month3 = 0, year1 = 0, year2 = 0, year3 = 0;
            DataSet.DataSet1 ds = new DataSet.DataSet1();

            DataSet.DataSet1TableAdapters.ThreeDeptReportTableAdapter ta = new DataSet.DataSet1TableAdapters.ThreeDeptReportTableAdapter();


            string categ = ddlCategory.SelectedItem.ToString();

            string[] month_arr = new string[lbMonth.Items.Count];
            string[] year_arr = new string[lbMonth.Items.Count];
            for (int i = 0; i < lbMonth.Items.Count; i++)
            {
                month_arr[i] = lbMonth.Items[i].ToString().Substring(5, 2);
                year_arr[i] = lbMonth.Items[i].ToString().Substring(0, 4);
            }

            if (month_arr.Count() > 0)
            {
                month1 = Convert.ToInt32(month_arr[0]);
                year1 = Convert.ToInt32(year_arr[0]);
                if (month_arr.Count() == 3)
                {
                    month2 = Convert.ToInt32(month_arr[1]);
                    year2 = Convert.ToInt32(year_arr[1]);
                    month3 = Convert.ToInt32(month_arr[2]);
                    year3 = Convert.ToInt32(year_arr[2]);
                }
                else if (month_arr.Count() == 2)
                {
                    month2 = Convert.ToInt32(month_arr[1]);
                    year2 = Convert.ToInt32(year_arr[1]);
                }
            }
            string dept1 = "", dept2 = "", dept3 = "";
            string[] dept_arr = new string[lstToDepartment.Items.Count];
            for (int i = 0; i < lstToDepartment.Items.Count; i++)
            {
                dept_arr[i] = lstToDepartment.Items[i].ToString();
            }

            if (dept_arr.Count() > 0)
            {
                dept1 = dept_arr[0];
                if (dept_arr.Count() == 3)
                {
                    dept2 = dept_arr[1];
                    dept3 = dept_arr[2];
                }
                else if (dept_arr.Count() == 2)
                {
                    dept2 = dept_arr[1];
                }
            }


            ta.Fill(ds.ThreeDeptReport, month1, month2, month3, year1, year2, year3, dept1, dept2, dept3, categ);
            rpt.SetDataSource(ds);
            CrView.ReportSource = rpt;
        }

        protected void rdoOneCategoryThreeMonths_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoOneCategoryThreeMonths.Checked == true)
            {
                MultiView1.ActiveViewIndex = 1;
            }
        }

        protected void rdoOneCategoryThreeDept_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoOneCategoryThreeDept.Checked == true)
            {
                MultiView1.ActiveViewIndex = 2;
            }
        }

        protected void rdoOneDeptThreeCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoOneDeptThreeCategory.Checked == true)
            {
                MultiView1.ActiveViewIndex = 3;
            }
        }

        protected void btnPutCategory_Click(object sender, EventArgs e)
        {
            if (!lstCategwiseToCategory.Items.Contains(lstCategwiseFromCategory.SelectedItem) && lstCategwiseToCategory.Items.Count<3)
            {
                lstCategwiseToCategory.Items.Add(lstCategwiseFromCategory.SelectedItem.Text);
            }
        }

        protected void btnRetrieveCategory_Click(object sender, EventArgs e)
        {
            lstCategwiseToCategory.Items.Remove(lstCategwiseToCategory.SelectedItem.Text);
        }

        protected void btnCategwise_Click(object sender, EventArgs e)
        {
            //report.Load(Server.MapPath("~/Reports/Categwise.rpt"));
            //report.FileName = Server.MapPath("~/Reports/Categwise.rpt");

            Reports.Categwise rpt = new Reports.Categwise();

            int month1 = 0, month2 = 0, month3 = 0, year1 = 0, year2 = 0, year3 = 0;
           
            DataSet.DataSet1 ds=new DataSet.DataSet1();

           

           DataSet.DataSet1TableAdapters.CategwiseReportTableAdapter ta = new DataSet.DataSet1TableAdapters.CategwiseReportTableAdapter();
            
            string department = ddlCategwiseDepartment.SelectedItem.ToString();

            string[] month_arr = new string[lbMonth.Items.Count];
            string[] year_arr = new string[lbMonth.Items.Count];
            for (int i = 0; i < lbMonth.Items.Count; i++)
            {
                month_arr[i] = lbMonth.Items[i].ToString().Substring(5, 2);
                year_arr[i] = lbMonth.Items[i].ToString().Substring(0, 4);
            }

            if (month_arr.Count() > 0)
            {
                month1 = Convert.ToInt32(month_arr[0]);
                year1 = Convert.ToInt32(year_arr[0]);
                if (month_arr.Count() == 3)
                {
                    month2 = Convert.ToInt32(month_arr[1]);
                    year2 = Convert.ToInt32(year_arr[1]);
                    month3 = Convert.ToInt32(month_arr[2]);
                    year3 = Convert.ToInt32(year_arr[2]);
                }
                else if (month_arr.Count() == 2)
                {
                    month2 = Convert.ToInt32(month_arr[1]);
                    year2 = Convert.ToInt32(year_arr[1]);
                }
            }
            string cate1 = "", cate2 = "", cate3 = "";
            string[] cate_arr = new string[lstCategwiseFromCategory.Items.Count];
            for (int i = 0; i < lstCategwiseFromCategory.Items.Count; i++)
            {
                cate_arr[i] = lstCategwiseFromCategory.Items[i].ToString();
            }

            if (cate_arr.Count() > 0)
            {
                cate1 = cate_arr[0];
                if (cate_arr.Count() == 3)
                {
                    cate2 = cate_arr[1];
                    cate3 = cate_arr[2];
                }
                else if (cate_arr.Count() == 2)
                {
                    cate2 = cate_arr[1];
                }
            }


            ta.Fill(ds.CategwiseReport, month1, month2, month3, year1, year2, year3, cate1, cate2, cate3,department );
            rpt.SetDataSource(ds);
            CrView.ReportSource = rpt;
        }
    }
}