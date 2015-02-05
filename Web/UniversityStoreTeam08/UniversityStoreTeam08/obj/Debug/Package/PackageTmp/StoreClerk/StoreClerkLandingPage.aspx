<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="StoreClerkLandingPage.aspx.cs" Inherits="UniversityStoreTeam08.StoreClerk.StoreClerkLandingPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
&nbsp;<script type="text/javascript">
          $(document).ready(function () {
              gridviewScroll();
          });

          function gridviewScroll() {
              $('#<%=grdLowStock.ClientID%>').gridviewScroll({
                  width: 800,
                  height: 400
              });
          } 
</script><div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
          <fieldset>
            
            
              <%--</div>--%>            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
             <%-- HeaderStyle-CssClass="FixedHeader"   --%> 
             <legend>Store Summary</legend>
                <asp:Button ID="btnGenerate" runat="server" Text="Generate" 
                  CssClass="btn btn-primary" onclick="btnGenerate_Click"  />
              <asp:Label ID="lblStatus" runat="server" Text="" ></asp:Label>
                      
               
              
              <br />
              <br />
              <legend>Low Stock: <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label> </legend>
            <div class="form-group">
            <%--<div style="width: 100%; height: 400px; overflow: auto">--%>
               <asp:GridView ID="grdLowStock" runat="server" 
                    CssClass="table table-hover" 
                     EmptyDataText="No Records" AutoGenerateColumns="False"  
 AlternatingRowStyle-BackColor="WhiteSmoke" >
                    <Columns>
                        <asp:BoundField DataField="ItemNumber" HeaderText="Item Code" />                                                                                                                                                    
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:TemplateField HeaderText="Quantity In Stock">
                            <ItemTemplate>
                                <asp:Label ID="lblInventBalance" runat="server" Text=''></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="GridviewScrollHeader" /> 
                    <RowStyle CssClass="GridviewScrollItem" /> 
                    <PagerStyle CssClass="GridviewScrollPager" /> 
                </asp:GridView>
               <%--</div>--%>
                </div>
                <br />
                <br />
                

              <legend>No. of Departments that have ordered:</legend> 
              <asp:Button ID="noofdepts" runat="server" CssClass="btn btn-primary" 
                  Text="Button" onclick="noofdepts_Click" />
                  <br /><br /><br />
                </fieldset>
                      </div>
                      </div>
                      </div>

</asp:Content>
