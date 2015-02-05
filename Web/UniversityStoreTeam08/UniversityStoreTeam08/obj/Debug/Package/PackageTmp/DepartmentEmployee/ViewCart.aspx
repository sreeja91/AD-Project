<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="ViewCart.aspx.cs" Inherits="UniversityStoreTeam08.DepartmentEmployee.ViewCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
          <fieldset>
            <legend>View Cart</legend>
            
              <%--</div>--%>            <%--<button type="submit" class="btn btn-primary">Save</button>--%>

              <div class="form-group">
                <asp:Label ID="lblStatus" runat="server" Text="" 
                    CssClass="control-label" Visible="true"></asp:Label>
            </div>

            <div class="form-group">
            
                <asp:GridView ID="grdViewCart" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False" 
                    onrowcommand="grdViewCart_RowCommand" EmptyDataText="No Records">
                    <Columns>
                        <asp:TemplateField HeaderText="Item Number" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblItemNumber" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblCategory" runat="server" Text=''></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text=''></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Description" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text=''></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text=''></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:TemplateField HeaderText="Unit Of Measurement" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblUnitofMeasure" runat="server" Text=''></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text=''></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/delete.png" 
                            ShowCancelButton="False" ShowDeleteButton="True" />
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" />
                </asp:GridView>
               
            </div>

            <div class="form-group">
              <div class="col-lg-10 col-lg-offset-9">
                  <%--</div>--%>
                <%--<button type="submit" class="btn btn-primary">Save</button>--%>
                <asp:Button ID="btnOrder" runat="server" Text="Order" CssClass="btn btn-primary" 
                      onclick="btnOrder_Click" />
              </div>
            </div>
            
          </fieldset>
          <%--</div>--%>
          </div>
</div>
</div>
    
</asp:Content>
