<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="ViewInvoice.aspx.cs" Inherits="UniversityStoreTeam08.StoreClerk.ViewInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
          <fieldset>
            <legend>View Invoice Lists</legend>
            
              <%--</div>--%>            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
            <div class="form-group">
              <asp:Label ID="Label1" runat="server" Text="Label" CssClass="col-lg-3 control-label">Supplier :</asp:Label>
              <div class="col-lg-5">
                  <div class="form-control">
                      <asp:DropDownList ID="ddlSupplier" runat="server" Width="100%">
                          </asp:DropDownList>
                  </div>
              </div>
              <div class="col-lg-3">
                  <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-primary" 
                      onclick="btnView_Click"/>
              </div>
            </div>

            <div class="form-group">
            
                <asp:GridView ID="grdDisbursementList" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False" 
                    onselectedindexchanged="grdDisbursementList_SelectedIndexChanged" 
                    onrowcommand="grdDisbursementList_RowCommand" 
                    EmptyDataText="There are no records found">
                    <Columns>
                        <asp:BoundField DataField="PONumber" HeaderText="PurchaseOrder Number" 
                            ReadOnly="True" SortExpression="PO Number" />
                    <asp:TemplateField HeaderText="PO Number" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblPONumber" runat="server" Text='<%# Bind("PONumber") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("PONumber") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SupplierCode" 
                            HeaderText="Supplier ID" />
                        <asp:BoundField DataField="DateCreated" HeaderText="Date" 
                            DataFormatString="{0:MM/dd/yyyy}" />
                        <asp:BoundField DataField="Name" HeaderText="Supplier Name" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:ButtonField ButtonType="Button" CommandName="Select" Text="Detail" 
                            ControlStyle-CssClass="btn btn-info">
                            <ControlStyle CssClass="btn btn-info"></ControlStyle>
                        </asp:ButtonField>
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
