<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="RaisePurchaseOrder.aspx.cs" Inherits="UniversityStoreTeam08.StoreClerk.RaisePurchaseOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%--<script type="text/javascript">
    function getDropdownListSelectedText() {
        var DropdownList = document.getElementById('<%=ddlSupplier.ClientID %>');
        var SelectedIndex = DropdownList.selectedIndex;
        var SelectedValue = DropdownList.value;
        var SelectedText = DropdownList.options[DropdownList.selectedIndex].text;

        //var LabelDropdownList = document.getElementById('<%=lblDropdownList.ClientID %>');
        var sValue = 'Selected Index: ' + SelectedIndex + '<br/> Selected Value: ' + SelectedValue + '<br/> Selected Text: ' + SelectedText;

        LabelDropdownList.innerHTML = sValue;
    }
</script>--%>
    
    
    <div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
          <fieldset>
            <legend>Raise Purchase Order</legend>

            <%--<div class="form-group">
                <asp:Label ID="lblError" runat="server" Text="" CssClass="col-lg-9 control-label"></asp:Label>
            </div>--%>

            <div class="form-group">
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"> <%--Using Ajax UpdatePanel only refresh on Grid View--%>
            <ContentTemplate>
                <asp:Label ID="lblError" runat="server" Text="" CssClass="col-lg-9 control-label"></asp:Label>
                
                <asp:GridView ID="grdItemList" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False" 
                    EmptyDataText="No Records Found" ShowHeaderWhenEmpty="True">
                    <Columns>
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectItem" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="chkSelectItem_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="ItemNumber" runat="server" Text='<%# Bind("ItemNumber") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ItemNumber") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Item Description" DataField="Description" />
                        
                        <asp:BoundField HeaderText="In Stock" Visible="False" />
                        <asp:BoundField HeaderText="Status" Visible="False" />
                        
                        <asp:TemplateField HeaderText="Total Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalQuantity" runat="server" Text=''></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text=''></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Supplier">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlSupplier" runat="server" 
                                    onselectedindexchanged="ddlSupplier_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:Label ID="lblPrice" runat="server" Text=''></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text=''></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" runat="server" Width="50px"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtQuantity" ErrorMessage="*" 
                        ForeColor="#FF3300" 
                        ValidationExpression="^[\d]{1,9}$" Display="Dynamic">Please Enter Numeric.</asp:RegularExpressionValidator>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Supplier">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlSecondSupplier" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlSecondSupplier_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:Label ID="lblSecondPrice" runat="server" Text=''></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text=''></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSecondQuantity" runat="server" Width="50px"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtQuantity" ErrorMessage="*" 
                        ForeColor="#FF3300" 
                        ValidationExpression="^[\d]{1,9}$" Display="Dynamic">Please Enter Numeric.</asp:RegularExpressionValidator>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Supplier">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlThirdSupplier" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlThirdSupplier_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:Label ID="lblThirdPrice" runat="server" Text=''></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text=''></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtThirdQuantity" runat="server" Width="50px"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ControlToValidate="txtQuantity" ErrorMessage="*" 
                        ForeColor="#FF3300" 
                        ValidationExpression="^[\d]{1,9}$" Display="Dynamic">Please Enter Numeric.</asp:RegularExpressionValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" />
                </asp:GridView>
                </ContentTemplate>
               </asp:UpdatePanel>
               
            </div>
              
            <div class="form-group">
              <div class="col-lg-10 col-lg-offset-9">
                  <%--</div>--%>
                <%--<button type="submit" class="btn btn-primary">Save</button>--%>
                
                <asp:Button ID="btnPurchase" runat="server" Text="Purchase" 
                      CssClass="btn btn-primary" onclick="btnPurchase_Click" />
              </div>
            </div>
            
          </fieldset>
          
          </div>
    </div>
    </div>
    
</asp:Content>
