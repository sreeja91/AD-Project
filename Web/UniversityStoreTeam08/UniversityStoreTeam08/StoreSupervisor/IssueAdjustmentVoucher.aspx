<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="IssueAdjustmentVoucher.aspx.cs" Inherits="UniversityStoreTeam08.IssueAdjustmentVoucher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
          <fieldset>
            <legend>Issue Adjustment Voucher</legend>
            
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Voucher Number :" CssClass="col-lg-4 control-label"></asp:Label>
                <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="form-control" 
                      ReadOnly="True"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="Issue Date :" CssClass="col-lg-4 control-label"></asp:Label>
                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" 
                      ReadOnly="True"></asp:TextBox>
            
                <asp:GridView ID="GridViewIssueadjVoucher" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False" >
             
                    <Columns>
                        <asp:BoundField DataField="ItemNumber" HeaderText="Item Code" />
                        <asp:BoundField DataField="Quantity" HeaderText="Adjust quantity" />
                        <asp:BoundField DataField="Comment" HeaderText="Reason" />
                       
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" />
                </asp:GridView>
               
                
               
            </div>
              
              <div class="form-group">
              <div class="col-lg-10 col-lg-offset-10">
                  <%--</div>--%>
                <%--<button type="submit" class="btn btn-primary">Save</button>--%>
                <asp:Button ID="btnIssue" runat="server" Text="Issue" 
                      CssClass="btn btn-primary" OnClick="btnIssue_Click" />&nbsp;
                  <asp:Button ID="btnBack" runat="server" Text="Back" Width="111px"  CssClass="btn btn-primary" OnClick="btnBack_Click" />
                 
              </div>
            </div>
            
          </fieldset>
          <%--</div>--%>
          </div>
</div>
</div>
</asp:Content>

