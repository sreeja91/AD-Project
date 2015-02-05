<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="PendingPurchaseOrder.aspx.cs" Inherits="UniversityStoreTeam08.StoreClerk.PendingPurchaseOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
          <fieldset>
            <legend>Pending Purchase Order Lists</legend>
            
              <%--</div>--%>            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
            

            <div class="form-group">
            
                <asp:GridView ID="grdPendingPurchaseOrder" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False" 
                    onrowcommand="grdPendingPurchaseOrder_RowCommand" 
                   >
                    <Columns>
                        <asp:BoundField DataField="PONumber" HeaderText="Puchase Order Number" 
                            ReadOnly="True" SortExpression="PO Number" />
                        <asp:BoundField DataField="Name" 
                            HeaderText="Supplier Name" />
                            <asp:BoundField DataField="Date" 
                            HeaderText="Created Date" DataFormatString="{0:MM/dd/yyyy}" />
                        <asp:ButtonField ButtonType="Button" CommandName="Select" Text="Print" 
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
