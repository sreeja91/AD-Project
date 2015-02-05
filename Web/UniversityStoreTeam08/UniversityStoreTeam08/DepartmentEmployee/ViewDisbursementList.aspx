<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="ViewDisbursementList.aspx.cs" Inherits="UniversityStoreTeam08.DepartmentEmployee.ViewDisbursementList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
          <fieldset>
            <legend>View Disbursement Lists</legend>
            
              <%--</div>--%>            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
            <div class="form-group">
              <asp:Label ID="Label1" runat="server" Text="Label" CssClass="col-lg-3 control-label">Date :</asp:Label>
              <div class="col-lg-5">
                  <div class="form-control">
                      <asp:DropDownList ID="ddlDate" runat="server" Width="100%">
                          </asp:DropDownList>
                          <%--<asp:ComboBox ID="ComboBox1" runat="server">
                              <asp:ListItem>Thant Sin Maung</asp:ListItem>
                              <asp:ListItem>Ngwe Phyo</asp:ListItem>
                          </asp:ComboBox>--%>
                  </div>
              </div>
            </div>

            <div class="form-group">
            
                <asp:GridView ID="grdDisbursementList" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="StationeryDescription" 
                            HeaderText="Stationery Description" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
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
