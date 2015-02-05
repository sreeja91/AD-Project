<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="PendingRequests.aspx.cs" Inherits="UniversityStoreTeam08.DepartmentHead.ImpendingRequests" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
          <fieldset>
            <legend>Pending Requests</legend>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
            <div class="form-group">
                <asp:Label ID="lblError" runat="server" Text="" CssClass="col-lg-9 control-label"></asp:Label>
            </div>
            <div class="form-group">
            
                <asp:GridView ID="grdImpendingRequest" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False" 
                    onrowcommand="grdImpendingRequest_RowCommand" PagerStyle-CssClass="paging" EmptyDataText="There are no pending requests" 
                    >
                    <Columns>
                        <asp:BoundField DataField="RequisitionListNumber" HeaderText="Requisition ID" />
                        <asp:BoundField DataField="Name" HeaderText="Employee Name" 
                            SortExpression="Name" />
                        <asp:BoundField DataField="DateCreated" HeaderText="Requisition Date" DataFormatString="{0:MM/dd/yyyy}"/>
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="Description" HeaderText="Products Included" />
                        <asp:ButtonField ButtonType="Button" CommandName="Select" Text="Detail" 
                            ControlStyle-CssClass="btn btn-info">
                            <ControlStyle CssClass="btn btn-info"></ControlStyle>
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Button" CommandName="UP" Text="Approve" 
                            ControlStyle-CssClass="btn btn-info">
                            <ControlStyle CssClass="btn btn-info"></ControlStyle>
                        </asp:ButtonField>
                        <asp:TemplateField HeaderText="Employee ID" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblEmployeeNumber" runat="server" Text='<%# Bind("EmployeeNumber") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("EmployeeNumber") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" />
                    <PagerStyle CssClass="paging" />
                </asp:GridView>
               
            </div>
              <%--</div>--%>            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
              <asp:Label ID="lblStatus" runat="server" Text="" CssClass="col-lg-4 control-label" style="margin-top: 18px;" /><br />
              
            <div class="form-group>
             <asp:Label ID="Label4" runat="server" Text="Label" CssClass="control-label" style="margin-top: 18px;">Current Representative:</asp:Label>
             <asp:TextBox ID="txtboxrRepName" runat="server" Enabled="False" CssClass="text-center" style="margin-left: 16px;margin-bottom: 5px;"></asp:TextBox>   
             </div>

            <div class="form-group" id="invisibleDiv1" runat="server">
             
              <asp:Label ID="Label1" runat="server" Text="Label" CssClass="col-lg-4 control-label" >Department Representative:</asp:Label>
              <div class="col-lg-5">
                  <div id="ddl1" runat="server" class="form-control">
                      <asp:DropDownList ID="ddlCollectorName"  AutoPostBack="true" runat="server" 
                          Width="100%" onselectedindexchanged="ddlCollectorName_SelectedIndexChanged" >
                          </asp:DropDownList>
                          <%--<asp:ComboBox ID="ComboBox1" runat="server">
                              <asp:ListItem>Thant Sin Maung</asp:ListItem>
                              <asp:ListItem>Ngwe Phyo</asp:ListItem>
                          </asp:ComboBox>--%>
                  </div>
              </div>
            </div>

            <div class="form-group>
             <asp:Label ID="Label5" runat="server" Text="Label" CssClass="control-label" style="margin-top: 18px;">Current Collection Point:</asp:Label>
             <asp:TextBox ID="txtboxcollecpt" runat="server" Enabled="False" Width="300px" CssClass="text-center" style="margin-left: 8px;margin-bottom: 5px;"></asp:TextBox>
             </div>

            <div class="form-group" id="invisibleDiv2">
            
              <asp:Label ID="Label2" runat="server" Text="Label" CssClass="col-lg-4 control-label" >Collection Point:</asp:Label>
              <div class="col-lg-5">
                  <div id="ddl2" runat="server" class="form-control">
                      <asp:DropDownList ID="ddlCollectionPoint" runat="server" Width="100%" 
                          AutoPostBack="True" 
                          onselectedindexchanged="ddlCollectionPoint_SelectedIndexChanged">
                          </asp:DropDownList>
                          <%--<asp:ComboBox ID="ComboBox1" runat="server">
                              <asp:ListItem>Thant Sin Maung</asp:ListItem>
                              <asp:ListItem>Ngwe Phyo</asp:ListItem>
                          </asp:ComboBox>--%>
                  </div>
              </div>
            </div>

            <div class="form-group">
              <div class="col-lg-10 col-lg-offset-10">
                  <%--</div>--%>
                <%--<button type="submit" class="btn btn-primary">Save</button>--%>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" 
                      onclick="btnSave_Click" Enabled="false"/>
                  <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
              </div>
            </div>

            </ContentTemplate>
            </asp:UpdatePanel>
          </fieldset>
          <%--</div>--%>
          </div>
</div>
</div>
</asp:Content>
