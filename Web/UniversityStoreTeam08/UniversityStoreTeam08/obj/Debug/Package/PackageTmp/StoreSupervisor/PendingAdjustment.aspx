<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="PendingAdjustment.aspx.cs" Inherits="UniversityStoreTeam08.StoreSupervisor.PendingRequests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
          <fieldset>
            <legend>Pending Adjustment</legend>
            
              <%--</div>--%>            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
            

            <div class="form-group">
            
                <asp:GridView ID="grdPendingAdjustment" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False" 
                    OnRowCommand="grdPendingAdjustment_RowCommand" 
                    EmptyDataText="There are no pending adjustments" >
                    <%--OnSelectedIndexChanged="grdPendingAdjustment_SelectedIndexChanged"--%>
                    <Columns>
                                                    
                        <asp:BoundField DataField="VoucherNumber" HeaderText="Voucher Number" />
                    
                       
                        <asp:BoundField DataField="DateCreated" HeaderText="Date" />
                      
                        
                        <asp:ButtonField ButtonType="Button" CommandName="Select" Text="Select" 
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
