<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="MakeAdjustment.aspx.cs" Inherits="UniversityStoreTeam08.StoreClerk.MakeAdjustment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--</div>--%>
          <fieldset>
            <legend>Make Adjustment</legend>
            
              <%--<asp:Label ID="Label2" runat="server" Text="Requisition Form" CssClass="col-lg-4 control-label"></asp:Label>--%><%--</div>--%>
            <div class="form-group">
              <asp:Label ID="Label1" runat="server" Text="Label" CssClass="col-lg-3 control-label">Search By :</asp:Label>
              <div class="col-lg-5">
                  <div class="form-control">
                      <asp:DropDownList ID="ddlType" runat="server" Width="100%">
                          </asp:DropDownList>
                  </div>
              </div>
            </div>

            <div class="form-group">
                <div class="col-lg-3"></div>
              <div class="col-lg-5">
                  <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" 
                      ReadOnly="True"></asp:TextBox>
              </div>
              <div class="col-lg-3">
                  <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary"/>
              </div>
            </div>

            <div class="form-group">
            
                <asp:GridView ID="grdItemList" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField 
                            HeaderText="Item Code" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" />
                </asp:GridView>
               
            </div>

            <div class="form-group">
              <div class="col-lg-10 col-lg-offset-9">
                  <%--</div>--%>
                <%--<button type="submit" class="btn btn-primary">Save</button>--%>
                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" />
              </div>
            </div>

            <div class="form-group">
            
                <asp:GridView ID="grdRequestItem" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField 
                            HeaderText="Item Code" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:TemplateField HeaderText="Adjust Qty">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQty" runat="server"></asp:TextBox>
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
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" />
              </div>
            </div>
            
          </fieldset>
          <%--</div>--%>
          </div>
</div>
</div>
</asp:Content>
