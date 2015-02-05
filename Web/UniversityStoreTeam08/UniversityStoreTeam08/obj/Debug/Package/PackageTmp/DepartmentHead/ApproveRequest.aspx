<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="ApproveRequest.aspx.cs" Inherits="UniversityStoreTeam08.DepartmentHead.ApproveRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div class="container">
    <div class="form-horizontal">
        <div class="col-lg-6">
            <%--<button type="submit" class="btn btn-primary">Save</button>--%>
          <fieldset>
            <legend>Approve Request</legend>
            
              <%--</div>--%>            <%--<button type="submit" class="btn btn-primary">Save</button>--%>

            <div class="form-group">
            
                <asp:GridView ID="grdApproveReq" runat="server" 
                    CssClass="table table-hover" AutoGenerateColumns="False" 
                     EmptyDataText="No Records">
                    <Columns>
                        
                       <asp:BoundField HeaderText="ItemCode" DataField="ItemNmber" />
                        <asp:BoundField HeaderText="Description" DataField="Description" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                       
                        <asp:BoundField DataField="UnitOfMeasure" HeaderText="UnitOfMeasure" />
                       
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" />
                </asp:GridView>
               
            </div>
            <div> 
              COMMENT:  &nbsp;&nbsp;&nbsp; &nbsp;
           <asp:TextBox ID="txtboxcomment" runat="server" TextMode="MultiLine"></asp:TextBox> </div>
            <div class="form-group">
              <div class="col-lg-10 col-lg-offset-9">
                  <%--</div>--%>
                <%--<button type="submit" class="btn btn-primary">Save</button>--%>
                <asp:Button ID="btnApprove" runat="server" Text="Approve" 
                      CssClass="btn btn-primary" onclick="btnApprove_Click" 
                       />

                       <asp:Button ID="btnDeny" runat="server" Text="Deny" 
                      CssClass="btn btn-primary" onclick="btnDeny_Click" 
                       />
                  <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
              </div>
            </div>
            
          </fieldset>
          <%--</div>--%>
          </div>
</div>
</div>
    
</asp:Content>
