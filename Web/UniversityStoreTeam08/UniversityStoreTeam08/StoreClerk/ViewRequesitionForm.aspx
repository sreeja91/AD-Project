<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="ViewRequesitionForm.aspx.cs" Inherits="UniversityStoreTeam08.StoreClerk.ViewRequesitionForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
          <fieldset>
            <legend>View Requisition Forms</legend>
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Department Name" CssClass="col-lg-4 control-label"></asp:Label>
              <div class="col-lg-5">
                  <div class="form-control">
                      <asp:DropDownList ID="ddlDepartmentName" runat="server" Width="100%" AutoPostBack="true" 
                          onselectedindexchanged="ddlDepartmentName_SelectedIndexChanged">
                          </asp:DropDownList>
                  </div>
              </div>
              <div class="col-lg-3">
                  <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-primary" 
                      onclick="btnView_Click" Visible="false"/>
              </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Approved By :" CssClass="col-lg-4 control-label"></asp:Label>
              <div class="col-lg-5">
                  <asp:TextBox ID="txtApprovedBy" runat="server" CssClass="form-control" 
                      ReadOnly="True" AutoPostBack="True"></asp:TextBox>
              </div>
            </div>

            <div class="form-group">
            
                <asp:GridView ID="grdImpendingRequest" runat="server" 
                    CssClass="table table-hover" 
                    EmptyDataText="No Records Found" ShowHeaderWhenEmpty="True">
                    <HeaderStyle BackColor="#CCCCCC" />
                </asp:GridView>
               
            </div>
              
            <%--<div class="form-group">
              <div class="col-lg-10 col-lg-offset-9">--%>
                  <%--</div>--%>
                <%--<button type="submit" class="btn btn-primary">Save</button>--%>
                <%--<asp:Button ID="btnDelegate" runat="server" Text="Delegate" CssClass="btn btn-primary" />
              </div>
            </div>--%>
            </ContentTemplate>
            </asp:UpdatePanel>
          </fieldset>
          
          </div>
    </div>
    </div>
</asp:Content>
