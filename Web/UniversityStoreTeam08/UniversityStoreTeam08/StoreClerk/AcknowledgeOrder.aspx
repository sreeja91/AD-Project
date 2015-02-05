<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="AcknowledgeOrder.aspx.cs" Inherits="UniversityStoreTeam08.StoreClerk.AcknowledgeOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
          <fieldset>
            <legend>Acknowledge Order</legend>

            <div class="form-group">
            
                <asp:GridView ID="grdImpendingRequest" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False" 
                    onselectedindexchanged="grdImpendingRequest_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="ItemNumber" HeaderText="Item Code" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Price" HeaderText="Price" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" />
                </asp:GridView>
               
                
               
            </div>
              
            <div class="form-group">
              <div class="col-lg-10 col-lg-offset-10">
                  <%--</div>--%>
                <%--<button type="submit" class="btn btn-primary">Save</button>--%>
                <asp:Button ID="btnAcknowledge" runat="server" Text="Acknowledge" 
                      CssClass="btn btn-primary" onclick="btnAcknowledge_Click" />&nbsp;
                  <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" Width="111px"  CssClass="btn btn-primary" />
                 
              </div>
            </div>
            
          </fieldset>
          <%--</div>--%>
          </div>
</div>
</div>
</asp:Content>
