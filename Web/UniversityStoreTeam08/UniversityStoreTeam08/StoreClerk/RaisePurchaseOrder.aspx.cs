using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace UniversityStoreTeam08.StoreClerk
{
    public partial class RaisePurchaseOrder : System.Web.UI.Page
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
                List<int> totalQtyList = PurchaseOrderController.Reorderlevel();
                grdItemList.DataSource = PurchaseOrderController.ProductWithLowStock();
                grdItemList.DataBind();
                for(int i=0;i<grdItemList.Rows.Count;i++)
                {
                    DropDownList ddl = (DropDownList)grdItemList.Rows[i].Cells[6].FindControl("ddlSupplier");
                    DropDownList ddl2 = (DropDownList)grdItemList.Rows[i].Cells[9].FindControl("ddlSecondSupplier");
                    DropDownList ddl3 = (DropDownList)grdItemList.Rows[i].Cells[12].FindControl("ddlThirdSupplier");

                    Label lblTotalQuantity = (Label)grdItemList.Rows[i].Cells[5].FindControl("lblTotalQuantity");

                    List<Supplier> suppList = PurchaseOrderDAO.GetallSupplier();


                    #region Insert Select Supplier

                    ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Supplier", ""));


                    ddl2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Supplier", ""));


                    ddl3.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Supplier", ""));

                    lblTotalQuantity.Text = totalQtyList[i].ToString();

                    #endregion

                }



            }
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            DropDownList lst = (DropDownList)sender;

            GridViewRow r = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent; //((TextBox)r.FindControl("TextBox1")).Text = r.RowIndex.ToString(); 
            int id = r.RowIndex;

            DropDownList dl1 = (DropDownList)grdItemList.Rows[id].Cells[9].FindControl("ddlSecondSupplier");
            DropDownList dl2 = (DropDownList)grdItemList.Rows[id].Cells[12].FindControl("ddlThirdSupplier");

            Label lblPrice = (Label)grdItemList.Rows[id].Cells[7].FindControl("lblPrice");
            Label lblItemNumber = (Label)grdItemList.Rows[id].Cells[1].FindControl("ItemNumber");

            TextBox qty = (TextBox)grdItemList.Rows[id].Cells[8].FindControl("txtQuantity");

            if (lst.SelectedValue != "")
            {
                if (lst.SelectedValue != dl1.SelectedValue && lst.SelectedValue != dl2.SelectedValue)
                {

                    if (lst.SelectedValue == "")
                    {
                        lblPrice.Text = "";
                    }
                    else
                    {
                        int price = SupplierDAO.getPrice(lst.SelectedValue, lblItemNumber.Text);

                        lblPrice.Text = price.ToString();
                    }
                    lblError.Text = "";
                    qty.Enabled = true;
                }
                else
                {
                    lblError.Text = lblItemNumber.Text + "'s supplier cannot be duplicated";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    qty.Enabled = false;
                    lblPrice.Text = "";
                    //lst.SelectedIndex = -1;
                }
            }
            else
            {
                lblError.Text = "";
                qty.Enabled = true;
                lblPrice.Text = "";
            }
            
        }

        protected void ddlSecondSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList lst = (DropDownList)sender;

            GridViewRow r = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent; //((TextBox)r.FindControl("TextBox1")).Text = r.RowIndex.ToString(); 
            int id = r.RowIndex;

            DropDownList dl1 = (DropDownList)grdItemList.Rows[id].Cells[6].FindControl("ddlSupplier");
            DropDownList dl2 = (DropDownList)grdItemList.Rows[id].Cells[12].FindControl("ddlThirdSupplier");

            Label lblPrice = (Label)grdItemList.Rows[id].Cells[10].FindControl("lblSecondPrice");
            Label lblItemNumber = (Label)grdItemList.Rows[id].Cells[1].FindControl("ItemNumber");


            TextBox qty = (TextBox)grdItemList.Rows[id].Cells[11].FindControl("txtSecondQuantity");

            if (lst.SelectedValue != "")
            {
                if (lst.SelectedValue != dl1.SelectedValue && lst.SelectedValue != dl2.SelectedValue)
                {

                    if (lst.SelectedValue == "")
                    {
                        lblPrice.Text = "";
                    }
                    else
                    {
                        int price = SupplierDAO.getPrice(lst.SelectedValue, lblItemNumber.Text);

                        lblPrice.Text = price.ToString();
                    }
                    lblError.Text = "";
                    qty.Enabled = true;
                }
                else
                {
                    lblError.Text = lblItemNumber.Text + "'s supplier cannot be duplicated";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    //lst.SelectedIndex = -1;
                    qty.Enabled = false;
                    lblPrice.Text = "";
                }
            }
            else
            {
                lblError.Text = "";
                qty.Enabled = true;
                lblPrice.Text = "";
            }
        }

        protected void ddlThirdSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList lst = (DropDownList)sender;

            GridViewRow r = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent; //((TextBox)r.FindControl("TextBox1")).Text = r.RowIndex.ToString(); 
            int id = r.RowIndex;

            DropDownList dl1 = (DropDownList)grdItemList.Rows[id].Cells[6].FindControl("ddlSupplier");
            DropDownList dl2 = (DropDownList)grdItemList.Rows[id].Cells[9].FindControl("ddlSecondSupplier");

            Label lblPrice = (Label)grdItemList.Rows[id].Cells[13].FindControl("lblThirdPrice");
            Label lblItemNumber = (Label)grdItemList.Rows[id].Cells[1].FindControl("ItemNumber");

            TextBox qty = (TextBox)grdItemList.Rows[id].Cells[14].FindControl("txtThirdQuantity");

            if (lst.SelectedValue != "")        //if the dropdown list choose "Select Supplier"
            {
                ///////
                ///////

                if (lst.SelectedValue != dl1.SelectedValue && lst.SelectedValue != dl2.SelectedValue)
                {

                    if (lst.SelectedValue == "")
                    {
                        lblPrice.Text = "";
                    }
                    else
                    {
                        int price = SupplierDAO.getPrice(lst.SelectedValue, lblItemNumber.Text);

                        lblPrice.Text = price.ToString();
                    }
                    lblError.Text = "";
                    qty.Enabled = true;
                }
                else
                {
                    lblError.Text = lblItemNumber.Text + "'s supplier cannot be duplicated";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    //lst.SelectedIndex = -1;
                    qty.Enabled = false;
                    lblPrice.Text = "";
                }
            }
            else
            {
                lblError.Text = "";
                qty.Enabled = true;
                lblPrice.Text = "";
            }
        }

        protected void btnPurchase_Click(object sender, EventArgs e)
        {
            bool isCheck = false;
            bool isQtyError = false;
            List<String> itemCodeList = new List<String>();

            lblError.Text = "";

            List<String> supplierCodeList = new List<String>();
            List<String> secondSupplierCodeList = new List<String>();
            List<String> thirdSupplierCodeList = new List<String>();

            List<int> qtyList = new List<int>();
            List<int> secondQtyList = new List<int>();
            List<int> thirdQtyList = new List<int>();

            for (int i = 0; i < grdItemList.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdItemList.Rows[i].Cells[0].FindControl("chkSelectItem");
                if (chk.Checked)
                {
                    Label lblItemCode = (Label)grdItemList.Rows[i].Cells[1].FindControl("ItemNumber");

                    DropDownList dlSupplier = (DropDownList)grdItemList.Rows[i].Cells[6].FindControl("ddlSupplier");
                    DropDownList dlSecondSupplier = (DropDownList)grdItemList.Rows[i].Cells[9].FindControl("ddlSecondSupplier");
                    DropDownList dlThirdSupplier = (DropDownList)grdItemList.Rows[i].Cells[12].FindControl("ddlThirdSupplier");

                    TextBox txtQuantity = (TextBox)grdItemList.Rows[i].Cells[8].FindControl("txtQuantity");
                    TextBox txtSecondQuantity = (TextBox)grdItemList.Rows[i].Cells[11].FindControl("txtSecondQuantity");
                    TextBox txtThirdQuantity = (TextBox)grdItemList.Rows[i].Cells[14].FindControl("txtThirdQuantity");

                    itemCodeList.Add(lblItemCode.Text);

                    supplierCodeList.Add(dlSupplier.SelectedValue);
                    secondSupplierCodeList.Add(dlSecondSupplier.SelectedValue);
                    thirdSupplierCodeList.Add(dlThirdSupplier.SelectedValue);
                    if (dlSupplier.SelectedValue != "")
                    {
                        if (txtQuantity.Text != "")
                        {
                            if (Convert.ToInt32(txtQuantity.Text) == 0)
                            {
                                lblError.Text += lblItemCode.Text + "'s Quantity should not be '0'<br>";
                                isQtyError = false;
                            }
                            else
                            {
                                qtyList.Add(Convert.ToInt32(txtQuantity.Text));
                                isQtyError = true;
                            }
                        }
                        else
                        {
                            lblError.Text += lblItemCode.Text + "'s Quantity should not be blank<br>";
                            isQtyError = false;
                        }
                    }
                    else
                    {
                        qtyList.Add(0);
                    }
                    if (dlSecondSupplier.SelectedValue != "")
                    {
                        if (txtSecondQuantity.Text != "")
                        {
                            if (Convert.ToInt32(txtSecondQuantity.Text) == 0)
                            {
                                lblError.Text += lblItemCode.Text + "'s Quantity should not be '0'<br>";
                                isQtyError = false;
                            }
                            else
                            {
                                secondQtyList.Add(Convert.ToInt32(txtSecondQuantity.Text));
                                isQtyError = true;
                            }
                        }
                        else
                        {
                            lblError.Text += lblItemCode.Text + "'s Quantity should not be blank<br>";
                            isQtyError = false;
                        }
                    }
                    else
                    {
                        secondQtyList.Add(0);
                    }
                    if (dlThirdSupplier.SelectedValue != "")
                    {
                        if (txtThirdQuantity.Text != "")
                        {
                            if (Convert.ToInt32(txtThirdQuantity.Text) == 0)
                            {
                                lblError.Text += lblItemCode.Text + "'s Quantity should not be '0'<br>";
                                isQtyError = false;
                            }
                            else
                            {
                                thirdQtyList.Add(Convert.ToInt32(txtThirdQuantity.Text));
                                isQtyError = true;
                            }
                        }
                        else
                        {
                            lblError.Text += lblItemCode.Text + "'s Quantity should not be blank<br>";
                            isQtyError = false;
                        }
                    }
                    else
                    {
                        thirdQtyList.Add(0);
                    }
                    isCheck = true;
                }
            }
            if (isCheck == true)
            {
                if (isQtyError==true && lblError.Text=="")
                {
                    PurchaseOrderController.GetSupplyOrder(itemCodeList.ToArray(), supplierCodeList.ToArray(), qtyList.ToArray(), secondSupplierCodeList.ToArray(), secondQtyList.ToArray(), thirdSupplierCodeList.ToArray(), thirdQtyList.ToArray());
                    Response.Redirect("PendingPurchaseOrder.aspx");
                }
                else
                {
                    lblError.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                //lblError.Text = "Purchase successful";
                //lblError.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4582ec");
            }
            else
            {
                lblError.Text = "Select items to purchase";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void chkSelectItem_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            GridViewRow r = (GridViewRow)((DataControlFieldCell)((CheckBox)sender).Parent).Parent; //((TextBox)r.FindControl("TextBox1")).Text = r.RowIndex.ToString(); 
            int id = r.RowIndex;

            DropDownList dlSupplier = (DropDownList)grdItemList.Rows[id].Cells[6].FindControl("ddlSupplier");
            DropDownList dlSecondSupplier = (DropDownList)grdItemList.Rows[id].Cells[9].FindControl("ddlSecondSupplier");
            DropDownList dlThirdSupplier = (DropDownList)grdItemList.Rows[id].Cells[12].FindControl("ddlThirdSupplier");

            Label lblItemNumber = (Label)grdItemList.Rows[id].Cells[1].FindControl("ItemNumber");

            if (chk.Checked)
            {
                List<Supplier> supList = SupplierDAO.GetallSupplier(lblItemNumber.Text);

                dlSupplier.Items.Clear();
                dlSupplier.DataSource = supList;
                dlSupplier.DataValueField = "SupplierCode";
                dlSupplier.DataTextField = "Name";
                dlSupplier.DataBind();
                dlSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Supplier", ""));

                dlSecondSupplier.Items.Clear();
                dlSecondSupplier.DataSource = supList;
                dlSecondSupplier.DataValueField = "SupplierCode";
                dlSecondSupplier.DataTextField = "Name";
                dlSecondSupplier.DataBind();
                dlSecondSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Supplier", ""));

                dlThirdSupplier.Items.Clear();
                dlThirdSupplier.DataSource = supList;
                dlThirdSupplier.DataValueField = "SupplierCode";
                dlThirdSupplier.DataTextField = "Name";
                dlThirdSupplier.DataBind();
                dlThirdSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Supplier", ""));
            }
            else
            {
                dlSupplier.Items.Clear();
                dlSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Supplier", ""));

                dlSecondSupplier.Items.Clear();
                dlSecondSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Supplier", ""));

                dlThirdSupplier.Items.Clear();
                dlThirdSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Supplier", ""));

            }
        }

        
    }
}