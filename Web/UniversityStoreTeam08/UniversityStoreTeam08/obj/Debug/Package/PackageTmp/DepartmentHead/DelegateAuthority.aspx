<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="DelegateAuthority.aspx.cs" Inherits="UniversityStoreTeam08.DepartmentHead.DelegateAuthority" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">
    function SelectRadiobutton(radio) {
        var rdBtn = document.getElementById(radio.id);
        var rdBtnList = document.getElementsByTagName("input");
        for (i = 0; i < rdBtnList.length; i++) {
            if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
                rdBtnList[i].checked = false;
            }
        }
    }
</script>

    <div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
          <fieldset>
            <legend>Delegate Authority</legend>

            <div class="form-group">
                <asp:Label ID="lblCurrentEPNumber" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="Current Delegate" CssClass="col-lg-4 control-label"></asp:Label>
              <div class="col-lg-5">
                  <asp:TextBox ID="txtCurrentDelegate" runat="server" CssClass="form-control" 
                      Enabled="False"></asp:TextBox>
              </div>
              <div class="col-lg-3">
                  <asp:Button ID="btnRemove" runat="server" Text="Remove" 
                      CssClass="btn btn-danger" onclick="btnRemove_Click"/>
              </div>
            </div>

            <div class="form-group">
                <div class="col-lg-4"></div>
                <asp:Label ID="Label7" runat="server" Text="Search Employee" 
                    CssClass="col-lg-4 control-label" Visible="False"></asp:Label>
              <div class="col-lg-5">
                  <asp:TextBox ID="txtSearchEmployee" runat="server" CssClass="form-control"></asp:TextBox>
              </div>
              <div class="col-lg-3">
                  <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="~/images/view.png" 
                      Width="30%" onclick="imgbtnSearch_Click" />
                  
              </div>
            </div>

            <div class="form-group">
            
                <asp:GridView ID="grdImpendingRequest" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False" 
                    EmptyDataText="No Records Found" ShowHeaderWhenEmpty="True">
                    <Columns>
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:RadioButton ID="rdoSelect" runat="server" OnClick="javascript:SelectRadiobutton(this)"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee ID" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblEmployeeNumber" runat="server" Text='<%# Bind("EmployeeNumber") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("EmployeeNumber") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Employee Name" DataField="Name" />
                        
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" />
                </asp:GridView>
               
            </div>
              
            <div class="form-group">
              <div class="col-lg-10 col-lg-offset-9">
                  <%--</div>--%>
                <%--<button type="submit" class="btn btn-primary">Save</button>--%>
                <asp:Button ID="btnDelegate" runat="server" Text="Delegate" 
                      CssClass="btn btn-primary" onclick="btnDelegate_Click" />
              </div>
            </div>
            
          </fieldset>
          
          </div>
    </div>
    </div>
</asp:Content>
