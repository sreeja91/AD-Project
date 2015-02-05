using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace UniversityStoreTeam08.StoreClerk
{
    public partial class PendingPurchaseOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("RaisePurchaseOrder");
            Label lblWelcome = (Label)this.Master.FindControl("lblWelcome");
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
            lblWelcome.Text = Session["Name"].ToString();//from login username
            li.Attributes.Add("class", "active");
            if (!IsPostBack)
            {
                ArrayList pur_list = PurchaseOrderController.GetPendingPurchaseOrder();
                string[] ponumber = (string[])pur_list[0];
                string[] name = (string[])pur_list[1];
                DateTime[] date = (DateTime[])pur_list[2];
                DataTable dt = new DataTable();
                dt.Columns.Add("PONumber");
                dt.Columns.Add("Name");
                dt.Columns.Add("Date");
                for(int i = 0; i < ponumber.Count(); i++)
                {
                    dt.Rows.Add();
                    dt.Rows[i]["PONumber"] = ponumber[i].ToString();
                    dt.Rows[i]["Name"] = name[i].ToString();
                    dt.Rows[i]["Date"] = date[i].ToShortDateString();
                }
                
                grdPendingPurchaseOrder.DataSource = dt;
                grdPendingPurchaseOrder.DataBind();
            }

        }

        protected void grdPendingPurchaseOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int command = Convert.ToInt32(e.CommandArgument);

                string po = grdPendingPurchaseOrder.Rows[command].Cells[0].Text;
                string name = grdPendingPurchaseOrder.Rows[command].Cells[1].Text;
                string date = grdPendingPurchaseOrder.Rows[command].Cells[2].Text;

                List<PurchaseOrderDetail> pod_list = PurchaseOrderController.GetPendingItemsList(Convert.ToInt32(po));
                ExportToPDF(name, po, date, pod_list);
               
            }
        }
        protected void ExportToPDF(string name,string poNumber,string date,List<PurchaseOrderDetail> podList)
        {
            // Create a Document object
            var document = new Document(PageSize.A4, 50, 50, 25, 25);
            document.SetPageSize(iTextSharp.text.PageSize.A4_LANDSCAPE);
            // Create a new PdfWrite object, writing the output to a MemoryStream
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            // Open the Document for writing
            document.Open();

            // Read in the contents of the Receipt.htm HTML template file
            string contents = File.ReadAllText(Server.MapPath("~/PDFTemplate/PurchaseOrderTemplate.htm"));

            // Replace the placeholders with the user-specified text
            contents = contents.Replace("[Name]", name);
            contents = contents.Replace("[ORDERDATE]", date);

            var itemsTable = @"<table><tr><th style=""font-weight: bold"">Item No</th><th style=""font-weight: bold"">Description</th><th style=""font-weight: bold"">Quantity</th><th style=""font-weight: bold"">Price</th><th style=""font-weight: bold"">Amount</th></tr>";
            foreach (PurchaseOrderDetail item in podList)
            {
                // Each CheckBoxList item has a value of ITEMNAME|ITEM#|QTY, so we split on | and pull these values out...
                //var pieces = item.Value.Split("|".ToCharArray());
                int pr =(int)item.Price;
                int amount =(int) item.Quantity * pr;
                itemsTable += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>",
                                            item.ItemNumber, item.Description, item.Quantity,item.Price,amount);
            }
            itemsTable += "</table>";

            contents = contents.Replace("[ITEMS]", itemsTable);


            var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(contents), null);
            foreach (var htmlElement in parsedHtmlElements)
                document.Add(htmlElement as IElement);



            // You can add additional elements to the document. Let's add an image in the upper right corner
            //var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/LU.png"));
            //logo.SetAbsolutePosition(440, 800);
            //document.Add(logo);

            document.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Purchase Order Receipt-{0}.pdf", poNumber));
            Response.BinaryWrite(output.ToArray());
        }
    }
}