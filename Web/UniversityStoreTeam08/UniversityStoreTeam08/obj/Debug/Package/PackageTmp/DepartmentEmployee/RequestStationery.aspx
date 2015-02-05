<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="RequestStationery.aspx.cs" Inherits="UniversityStoreTeam08.DepartmentEmployee.MakeOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type = "text/javascript">
<!--




function Check_Click(objRef) // if u click chk box and change the grid view row color
{
    //Get the Row based on checkbox
    var row = objRef.parentNode.parentNode;
    if(objRef.checked)
    {
        //If checked change color to Aqua
        row.style.backgroundColor = "aqua";
    }
    else
    {    
        //If not checked change back to original color
        if(row.rowIndex % 2 == 0)
        {
           //Alternating Row Color
           row.style.backgroundColor = "#C2D69B";
        }
        else
        {
           row.style.backgroundColor = "white";
        }
    }
    
    //Get the reference of GridView
    var GridView = row.parentNode;
    
    //Get all input elements in Gridview
    var inputList = GridView.getElementsByTagName("input");
    
    for (var i=0;i<inputList.length;i++)
    {
        //The First element is the Header Checkbox
        //var headerCheckBox = inputList[0];
        
        //Based on all or none checkboxes
        //are checked check/uncheck Header Checkbox
        var checked = true;
        if(inputList[i].type == "checkbox")
        {
            if(!inputList[i].checked)
            {
                checked = false;
                break;
            }
        }
    }
    //headerCheckBox.checked = checked;
    
}
</script>

    <div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
          <fieldset>
            <legend>Request Stationery</legend>

           <%-- <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Requisition Form" CssClass="col-lg-4 control-label"></asp:Label>
              <div class="col-lg-5">
                  <asp:TextBox ID="txtRequisitionID" runat="server" CssClass="form-control" 
                      ReadOnly="True"></asp:TextBox>
              </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Department Name" CssClass="col-lg-4 control-label"></asp:Label>
              <div class="col-lg-8">
                  <asp:TextBox ID="txtDepartmentName" runat="server" CssClass="form-control" 
                      ReadOnly="True"></asp:TextBox>
              </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label3" runat="server" Text="Department Code" CssClass="col-lg-4 control-label"></asp:Label>
              <div class="col-lg-5">
                  <asp:TextBox ID="txtDepartmentCode" runat="server" CssClass="form-control" 
                      ReadOnly="True"></asp:TextBox>
              </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label4" runat="server" Text="Employee Name" CssClass="col-lg-4 control-label"></asp:Label>
              <div class="col-lg-8">
                  <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="form-control" 
                      ReadOnly="True"></asp:TextBox>
              </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label5" runat="server" Text="Employee Number" CssClass="col-lg-4 control-label"></asp:Label>
              <div class="col-lg-5">
                  <asp:TextBox ID="txtEmployeeNumber" runat="server" CssClass="form-control" 
                      ReadOnly="True"></asp:TextBox>
              </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label6" runat="server" Text="Employee Email" CssClass="col-lg-4 control-label"></asp:Label>
              <div class="col-lg-5">
                  <asp:TextBox ID="txtEmployeeEmail" runat="server" CssClass="form-control" 
                      ReadOnly="True"></asp:TextBox>
              </div>
            </div>--%>

            <div class="form-group">
                <asp:Label ID="Label7" runat="server" Text="Search Item" 
                    CssClass="col-lg-4 control-label" Visible="False"></asp:Label>
              <div class="col-lg-5">
                  <asp:TextBox ID="txtSearchItem" runat="server" CssClass="form-control"></asp:TextBox>
              </div>
              <div class="col-lg-3">
                  <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="~/images/view.png" 
                      Width="30%" onclick="imgbtnSearch_Click" />
                  
              </div>
            </div>

            <div class="form-group">
                <asp:Label ID="lblError" runat="server" Text="" CssClass="col-lg-9 control-label"></asp:Label>
            </div>

            <div class="form-group">
            
                <asp:GridView ID="grdItemList" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False" 
                    EmptyDataText="No Records Found" ShowHeaderWhenEmpty="True" 
                    AllowPaging="True" onpageindexchanging="grdItemList_PageIndexChanging" PagerStyle-CssClass="paging">

                    <pagersettings mode="Numeric" position="Bottom" pagebuttoncount="10"/>

                    <pagerstyle backcolor="LightBlue" height="30px" verticalalign="Bottom" horizontalalign="Center"/>

                    <Columns>
                        
                        <asp:TemplateField HeaderText="Item Number" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblItemNumber" runat="server" Text='<%# Bind("ItemNumber") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ItemNumber") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Category" DataField="Category" />
                        
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        
                        <asp:BoundField DataField="UnitOfMeasure" HeaderText="UnitOfMeasure" />
                        
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" runat="server" Width="50px"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtQuantity" ErrorMessage="*" 
                        ForeColor="#FF3300" 
                        ValidationExpression="^[\d]{1,9}$" Display="Dynamic">Please Enter Numeric.</asp:RegularExpressionValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectItem" runat="server" onclick = "Check_Click(this)"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" />
                </asp:GridView>
               
            </div>
              
            

            <div class="form-group">
              <div class="col-lg-10 col-lg-offset-9">
                  <%--</div>--%>
                <%--<button type="submit" class="btn btn-primary">Save</button>--%>                
                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" 
                      onclick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnViewCart" runat="server" Text="View Cart" 
                      CssClass="btn btn-primary" onclick="btnViewCart_Click" />
              </div>
            </div>
            
          </fieldset>
          
          </div>
    </div>
    </div>
</asp:Content>
