<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="CheckUnfulfilled.aspx.cs" Inherits="UniversityStoreTeam08.DepartmentEmployee.CheckUnfulfilled" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
          <fieldset>
            <legend>Check Unfulfilled</legend>

            <div class="form-group">
            
                <asp:GridView ID="grdItemList" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False" 
                    EmptyDataText="No Records Found" ShowHeaderWhenEmpty="True">
                    <Columns>
                        
                        <asp:BoundField HeaderText="Item Number" DataField="ItemNumber" 
                            Visible="False" />
                        <asp:BoundField HeaderText="Category" DataField="Category" />
                        
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="UnfulfilledQuantity" 
                            HeaderText="Unfulfilled Quantity" />
                        <asp:BoundField DataField="UnitOfMeasure" HeaderText="UnitOfMeasure" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="DateCreated" HeaderText="Created Date" 
                            DataFormatString="{0:MM/dd/yyyy}" />
                        
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" />
                </asp:GridView>
               
            </div>
              
            <%--<div class="form-group">
              <div class="col-lg-10 col-lg-offset-9">--%>
                  <%--</div>--%>
                <%--<button type="submit" class="btn btn-primary">Save</button>--%>
                <%--<asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" />
              </div>
            </div>--%>
            
          </fieldset>
          
          </div>
    </div>
    </div>
</asp:Content>
