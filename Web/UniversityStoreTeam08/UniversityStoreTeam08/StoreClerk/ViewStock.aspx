<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="ViewStock.aspx.cs" Inherits="UniversityStoreTeam08.StoreClerk.ViewStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--</div>--%>
          <fieldset>
            <legend>View Stock</legend>
            
              <%--<asp:Label ID="Label2" runat="server" Text="Requisition Form" CssClass="col-lg-4 control-label"></asp:Label>--%><%--</div>--%>
            <div class="form-group">
              <asp:Label ID="Label1" runat="server" Text="Label" CssClass="col-lg-3 control-label">Search By :</asp:Label>
              <div class="col-lg-5">
                  <div class="form-control">
                      <asp:DropDownList ID="ddlSearchType" runat="server" Width="100%">
                          <asp:ListItem>Category</asp:ListItem>
                          <asp:ListItem>Description</asp:ListItem>
                          </asp:DropDownList>
                  </div>
              </div>
            </div>

            <div class="form-group">
                <div class="col-lg-3"></div>
              <div class="col-lg-5">
                  <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
              </div>
              <div class="col-lg-3">
                  <asp:Button ID="btnSearch" runat="server" Text="Search" 
                      CssClass="btn btn-primary" onclick="btnSearch_Click"/>
              </div>
            </div>

            <div class="form-group">
            
                <asp:GridView ID="grdItemList" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False" 
                    AllowPaging="True" onpageindexchanging="grdItemList_PageIndexChanging" 
                    PagerStyle-CssClass="paging" EmptyDataText="There are  no records found">
                    <pagersettings mode="Numeric" position="Bottom" pagebuttoncount="10"/>

                    <pagerstyle backcolor="LightBlue" height="30px" verticalalign="Bottom" horizontalalign="Center"/>
                    <Columns>
                        <asp:BoundField 
                            HeaderText="Item Code" DataField="ItemNumber" />
                        <asp:BoundField DataField="Category" HeaderText="Category" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="ReorderLevel" HeaderText="Reorder Level" />
                        <asp:BoundField DataField="ReorderQuantity" HeaderText="Reorder Quantity" />
                        <asp:BoundField DataField="TotalInventoryBalance" 
                            HeaderText="Actual Quantity" />
                        <asp:BoundField DataField="AdjustmentVoucherPrice" HeaderText="Price ( $ )" />
                        <asp:BoundField DataField="UnitOfMeasure" 
                            HeaderText="Unit Of Measurement" />
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" />
                </asp:GridView>
               
            </div>

            
            
          </fieldset>
          <%--</div>--%>
          </div>
</div>
</div>
</asp:Content>
