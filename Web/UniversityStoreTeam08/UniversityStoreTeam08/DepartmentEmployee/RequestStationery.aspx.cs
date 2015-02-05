using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace UniversityStoreTeam08.DepartmentEmployee
{
    public partial class MakeOrder : System.Web.UI.Page
    {
        string empNo = "";
        List<RequestStationaryCart> cartList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
    
            #region for CheckBox

            ArrayList CheckBoxArray;
            Dictionary<int,string> qtyArray;
            if (ViewState["CheckBoxArray"] != null)
            {
                CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
            }
            else
            {
                CheckBoxArray = new ArrayList();
            }

            if (ViewState["QtyArray"] != null)
            {
                qtyArray = (Dictionary<int,string>)ViewState["QtyArray"];
            }
            else
            {
                qtyArray = new Dictionary<int,string>();
            }
            #endregion

            #region Mendatory

            System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("MakeOrder");
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

            #endregion

            cartList = RequestStationaryControler.getEmployeeCart(empNo);

            btnViewCart.Text = "View Cart ( " + cartList.Count + " ) ";

            if (IsPostBack)
            {
                #region for CheckBox

                int CheckBoxIndex;
                bool CheckAllWasChecked = false;

                int qtyIndex;
                #endregion


                #region Bind To Gridview
                

                for (int i = 0; i < grdItemList.Rows.Count; i++)
                {
                    if (grdItemList.Rows[i].RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chk = (CheckBox)grdItemList.Rows[i].Cells[5].FindControl("chkSelectItem");
                        TextBox qty = (TextBox)grdItemList.Rows[i].Cells[4].FindControl("txtQuantity");
                        CheckBoxIndex = grdItemList.PageSize * grdItemList.PageIndex + (i + 1);
                        if (chk.Checked)
                        {
                            if (CheckBoxArray.IndexOf(CheckBoxIndex) == -1 && !CheckAllWasChecked)
                            {
                                CheckBoxArray.Add(CheckBoxIndex);
                                qtyArray.Add(CheckBoxIndex,qty.Text);
                            }
                        }
                        else
                        {
                            if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1 || CheckAllWasChecked)
                            {
                                CheckBoxArray.Remove(CheckBoxIndex);
                                qtyArray.Remove(CheckBoxIndex);
                            }
                        }
                    }
                }
                
                

                #endregion


            }
            grdItemList.DataSource = RequestStationaryControler.acceptAllProducts();
            grdItemList.DataBind();
            ViewState["CheckBoxArray"] = CheckBoxArray;
            ViewState["QtyArray"] = qtyArray;
        }

        protected void grdItemList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdItemList.PageIndex = e.NewPageIndex;
            if (txtSearchItem.Text != "")
            {
                grdItemList.DataSource = RequestStationaryControler.displaySerachResults(txtSearchItem.Text);
                grdItemList.DataBind();
                if (ViewState["CheckBoxArray"] != null)
                {
                    ArrayList CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
                    Dictionary<int, string> qtyArray = (Dictionary<int, string>)ViewState["QtyArray"];
                    string checkAllIndex = "chkAll-" + grdItemList.PageIndex;
                    for (int i = 0; i < grdItemList.Rows.Count; i++)
                    {
                        if (grdItemList.Rows[i].RowType == DataControlRowType.DataRow)
                        {
                            if (CheckBoxArray.IndexOf(checkAllIndex) != -1)
                            {
                                CheckBox chk = (CheckBox)grdItemList.Rows[i].Cells[4].FindControl("chkSelectItem");
                                TextBox qty = (TextBox)grdItemList.Rows[i].Cells[4].FindControl("txtQuantity");
                                chk.Checked = true;
                                grdItemList.Rows[i].Attributes.Add("style", "background-color:aqua");
                            }
                            else
                            {
                                int CheckBoxIndex = grdItemList.PageSize * (grdItemList.PageIndex) + (i + 1);
                                if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1)
                                {
                                    CheckBox chk = (CheckBox)grdItemList.Rows[i].Cells[4].FindControl("chkSelectItem");
                                    TextBox qty = (TextBox)grdItemList.Rows[i].Cells[4].FindControl("txtQuantity");

                                    qty.Text = qtyArray[CheckBoxIndex];
                                    chk.Checked = true;
                                    grdItemList.Rows[i].Attributes.Add("style", "background-color:aqua");
                                }
                            }
                        }
                    }
                }
                    
                
            }
            else
            {
                grdItemList.DataSource = RequestStationaryControler.acceptAllProducts();
                grdItemList.DataBind();
                if (ViewState["CheckBoxArray"] != null)
                {
                    ArrayList CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
                    Dictionary<int, string> qtyArray = (Dictionary<int, string>)ViewState["QtyArray"];
                    string checkAllIndex = "chkAll-" + grdItemList.PageIndex;
                    for (int i = 0; i < grdItemList.Rows.Count; i++)
                    {
                        if (grdItemList.Rows[i].RowType == DataControlRowType.DataRow)
                        {
                            if (CheckBoxArray.IndexOf(checkAllIndex) != -1)
                            {
                                CheckBox chk = (CheckBox)grdItemList.Rows[i].Cells[4].FindControl("chkSelectItem");
                                chk.Checked = true;
                                grdItemList.Rows[i].Attributes.Add("style", "background-color:aqua");
                            }
                            else
                            {
                                int CheckBoxIndex = grdItemList.PageSize * (grdItemList.PageIndex) + (i + 1);
                                if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1)
                                {
                                    CheckBox chk = (CheckBox)grdItemList.Rows[i].Cells[4].FindControl("chkSelectItem");
                                    TextBox qty = (TextBox)grdItemList.Rows[i].Cells[4].FindControl("txtQuantity");

                                    qty.Text = qtyArray[CheckBoxIndex];
                                    chk.Checked = true;
                                    grdItemList.Rows[i].Attributes.Add("style", "background-color:aqua");
                                }
                            }
                        }
                    }
                }
                
            }
        }

        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
                grdItemList.PageIndex = 0;
                grdItemList.DataSource = RequestStationaryControler.displaySerachResults(txtSearchItem.Text);
                grdItemList.DataBind();                
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            List<String> itemNumberList = new List<string>();
            List<int> qtyList = new List<int>();
            cartList = RequestStationaryControler.getEmployeeCart(empNo); //hard code

            ArrayList CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
            Dictionary<int, string> qtyArray = (Dictionary<int, string>)ViewState["QtyArray"];

            bool isExist = false;
            bool isCheck = false;
            bool isZero = false;
            lblError.Text = "";
            if (txtSearchItem.Text != "")
            {
                grdItemList.DataSource = RequestStationaryControler.displaySerachResults(txtSearchItem.Text);
            }
            else
            {
                grdItemList.DataSource = RequestStationaryControler.acceptAllProducts();
            }
            grdItemList.AllowPaging = false;
            this.DataBind();
            for (int index = 0; index < CheckBoxArray.Count; index++)
            {
                CheckBox chk = (CheckBox)grdItemList.Rows[Convert.ToInt32(CheckBoxArray[index].ToString())-1].Cells[4].FindControl("chkSelectItem");
                chk.Checked = true;
                TextBox txt = (TextBox)grdItemList.Rows[Convert.ToInt32(CheckBoxArray[index].ToString())-1].Cells[3].FindControl("txtQuantity");
                txt.Text = qtyArray[Convert.ToInt32(CheckBoxArray[index].ToString())];
            }
            for (int i = 0; i < grdItemList.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdItemList.Rows[i].Cells[4].FindControl("chkSelectItem");
                if (chk.Checked)
                {

                    Label lbl = (Label)grdItemList.Rows[i].Cells[0].FindControl("lblItemNumber");
                    TextBox txt = (TextBox)grdItemList.Rows[i].Cells[3].FindControl("txtQuantity");
                    for (int j = 0; j < cartList.Count; j++)
                    {
                        RequestStationaryCart cart = (RequestStationaryCart)cartList[j];
                        if (lbl.Text == cart.ItemCode)
                        {
                            if (lblError.Text == "")
                            {
                                lblError.Text += "Item Code " + lbl.Text;
                            }
                            else
                            {
                                lblError.Text += " , " + lbl.Text;
                            }
                            isExist = true;
                        }
                    }
                    if (isExist == false)
                    {
                        itemNumberList.Add(lbl.Text);
                        if (txt.Text == "")
                        {
                            qtyList.Add(0);
                        }
                        else
                        {
                            qtyList.Add(Convert.ToInt32(txt.Text));
                        }

                    }
                    isCheck = true;
                }
            }
            
            if (isExist == false)
            {
                for (int i = 0; i < qtyList.Count; i++)
                {
                    if (qtyList[i] == 0)
                    {
                        lblError.Text += itemNumberList[i].ToString()+"'s should not be '0'!<br>";
                        lblError.ForeColor = System.Drawing.Color.Red;
                        isZero = true;
                    }
                }
                if (isZero == false)
                {
                    RequestStationaryControler.addSelectedItemsToCart(itemNumberList.ToArray(), qtyList.ToArray(), empNo);
                }
                if (isCheck == true && isZero==false)
                {
                    lblError.Text = " Your requested items are already added to cart";
                    lblError.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4582ec");
                }
                else if(isCheck==false)
                {
                    lblError.Text = "Please select items to add";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
                
 
            }
            else
            {
                lblError.Text += " already requested !";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
                cartList = RequestStationaryControler.getEmployeeCart(empNo); //to calculate the view cart count
                btnViewCart.Text = "View Cart ( " + cartList.Count + " ) ";
                grdItemList.AllowPaging = true;
                this.DataBind();
        }

        protected void btnViewCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("../DepartmentEmployee/ViewCart.aspx");
        }
    }
}