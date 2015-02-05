<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UniversityStoreTeam08.Master" AutoEventWireup="true" CodeBehind="GenerateTrendAnalysis.aspx.cs" Inherits="UniversityStoreTeam08.StoreSupervisor.GenerateTrendAnalysis" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        
     <fieldset>
        <legend>Generate Trend Analysis</legend>
        <asp:UpdatePanel ID="UpdateUpper" runat="server">
        <ContentTemplate>
            <div class="row">
                <asp:Panel BorderColor="2" ID="pa1" runat="server">
                <div class="col-lg-6">
                <div class="form-group">
                        
                        <asp:Label ID="Label2" runat="server" Text="Select Report Type:" CssClass="col-lg-4 control-label"></asp:Label>
                    <div class="col-lg-5">
                        <div class="form-control">
                            <asp:DropDownList ID="ddlReport" runat="server" Width="100%" 
                                AutoPostBack="true" onselectedindexchanged="ddlReport_SelectedIndexChanged" >
                                <asp:ListItem>Order Summary</asp:ListItem>
                                <asp:ListItem>Requisition Summary</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    
                </div>
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Month:" CssClass="col-lg-4 control-label"></asp:Label>
                    <div class="col-lg-5">
                    <div class="form-control">
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="100%" AutoPostBack="true" >
                                <asp:ListItem Value="01">January</asp:ListItem>
                                <asp:ListItem Value="02">February</asp:ListItem>
                                <asp:ListItem Value="03">March</asp:ListItem>
                                <asp:ListItem Value="04">April</asp:ListItem>
                                <asp:ListItem Value="04">May</asp:ListItem>
                                <asp:ListItem Value="06">June</asp:ListItem>
                                <asp:ListItem Value="07">July</asp:ListItem>
                                <asp:ListItem Value="08">August</asp:ListItem>
                                <asp:ListItem Value="09">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>
                    </div>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" Text="Year:" CssClass="col-lg-4 control-label"></asp:Label>
                        <div class="col-lg-5">
                            <div class="form-control">
                                    <asp:DropDownList ID="ddlYear" runat="server" Width="100%" AutoPostBack="true" >
                                        
                                        </asp:DropDownList>
                                      
                            </div>
                                
                        </div>
                </div>
                <div class="form-group">
                    
                        <div class="col-lg-5">
                            <asp:RadioButton ID="rdoOneCategoryThreeMonths" runat="server" 
                            Text="1 Categ Vs All Depts" GroupName="Report" Visible="false" AutoPostBack="True" 
                                oncheckedchanged="rdoOneCategoryThreeMonths_CheckedChanged" /><br />
                        <asp:RadioButton ID="rdoOneCategoryThreeDept" runat="server" Text="1 Categ Vs 3 Depts" 
                            GroupName="Report" Visible="false" AutoPostBack="True" 
                                oncheckedchanged="rdoOneCategoryThreeDept_CheckedChanged" /><br />
                        <asp:RadioButton ID="rdoOneDeptThreeCategory" runat="server" Text="1 Dept Vs 3 Categories" 
                            GroupName="Report" Visible="false" AutoPostBack="True" 
                                oncheckedchanged="rdoOneDeptThreeCategory_CheckedChanged" />
                        </div>
                </div>
                
                    
                </div>
                <div class="col-lg-1">
                <div>
                &nbsp;
                &nbsp;
                </div>
                <div class="form-group">
                    <asp:Button ID="btnSelect" runat="server" Text=">>" CssClass="btn btn-primary" 
                        onclick="btnSelect_Click" />
                </div>
                <div class="form-group">
                    <asp:Button ID="btnDeSelect" runat="server" Text="<<" 
                                    CssClass="btn btn-primary" onclick="btnDeSelect_Click" />
                </div>
                </div>
                <div class="col-lg-4">
                <div class="form-group">
                    <asp:ListBox ID="lbMonth" runat="server" Width="224px" Height="121px"></asp:ListBox>
                </div>
                </div>
                </asp:Panel>
            </div>
            <div class="row">
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="viewOrderSummary" runat="server" EnableTheming="True">
                <div class="form-group">
                    <asp:Button ID="btnShowReport" runat="server" Text="Show Report"  
                    CssClass="btn btn-primary" onclick="btnShowReport_Click" />
                </div>
            </asp:View>
            <asp:View ID="viewMonthWise" runat="server" EnableTheming="True">
            <div class="col-lg-6">
                <div class="form-group">
                        <asp:Label ID="Label6" runat="server" Text="Select Category:" CssClass="col-lg-4 control-label"></asp:Label>
                    <div class="col-lg-5">
                        <div class="form-control">
                            <asp:DropDownList ID="ddlMonthwiseCategory" runat="server" Width="100%" AutoPostBack="true" >
                                <%--<asp:ListItem>Clip</asp:ListItem>
                                <asp:ListItem>Envelope</asp:ListItem>
                                <asp:ListItem>Eraser</asp:ListItem>
                                <asp:ListItem>Exercise</asp:ListItem>
                                <asp:ListItem>File</asp:ListItem>
                                <asp:ListItem>Pad</asp:ListItem>
                                <asp:ListItem>Paper</asp:ListItem>
                                <asp:ListItem>Pen</asp:ListItem>
                                <asp:ListItem>Puncher</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>
                    
                </div>
                <div class="form-group">
                    <asp:Button ID="btnMonthWise" runat="server" Text="Generate Report"  
                    CssClass="btn btn-primary" onclick="btnMonthWise_Click"/>
                </div>
            </div>
            </asp:View>
            <asp:View ID="viewRequisitionSummary" runat="server" EnableTheming="true">
                <div class="col-lg-6">
                <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="Select Category:" CssClass="col-lg-4 control-label"></asp:Label>
                    <div class="col-lg-5">
                        <div class="form-control">
                            <asp:DropDownList ID="ddlCategory" runat="server" Width="100%" AutoPostBack="true" >
                                <%--<asp:ListItem>Clip</asp:ListItem>
                                <asp:ListItem>Envelope</asp:ListItem>
                                <asp:ListItem>Eraser</asp:ListItem>
                                <asp:ListItem>Exercise</asp:ListItem>
                                <asp:ListItem>File</asp:ListItem>
                                <asp:ListItem>Pad</asp:ListItem>
                                <asp:ListItem>Paper</asp:ListItem>
                                <asp:ListItem>Pen</asp:ListItem>
                                <asp:ListItem>Puncher</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>
                    
                </div>
                <br />
                <div class="form-group">
                    <asp:Label ID="Label5" runat="server" Text="Select Department:" CssClass="col-lg-4 control-label" style="margin-top:20px;"></asp:Label>
                    <asp:ListBox ID="lstFromDepartment" runat="server" style="height:121px;width:224px;margin-top: 20px;">
                        <asp:ListItem>COMM</asp:ListItem>
                          <asp:ListItem>CPSC</asp:ListItem>
                          <asp:ListItem>ENGL</asp:ListItem>
                          <asp:ListItem>REGR</asp:ListItem>
                          <asp:ListItem>ZOOL</asp:ListItem>
                    </asp:ListBox>
                </div>
                </div>
                <div class="col-lg-1">
                <div>
                &nbsp;
                &nbsp;
                </div>
                
                <div>&nbsp;</div>
                <div class="form-group">
                    <asp:Button ID="btnPut" runat="server" Text=">>" CssClass="btn btn-primary" 
                        style="margin-top:15px;" onclick="btnPut_Click"/>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnRetrieve" runat="server" Text="<<" 
                                    CssClass="btn btn-primary" onclick="btnRetrieve_Click" />
                </div>
                </div>
                <div class="col-lg-4">
                <div>
                &nbsp;
                &nbsp;
                </div>
                <div>
                &nbsp;
                &nbsp;
                </div>
                    <asp:ListBox ID="lstToDepartment" runat="server" style="height:121px;width:224px;margin-top: 15px;"></asp:ListBox><br />
                    <div class="form-group">
                    &nbsp;
                    <asp:Button ID="btnDeptWise" runat="server" Text="Generate Report"  
                    CssClass="btn btn-primary" style="margin-top:5px;" onclick="btnDeptWise_Click"/>
                    </div>
                    
                </div>
            </asp:View>
            <asp:View ID="viewCategwise" runat="server" EnableTheming="true">
                <div class="col-lg-6">
                <div class="form-group">
                        <asp:Label ID="Label7" runat="server" Text="Select Department:" CssClass="col-lg-4 control-label"></asp:Label>
                    <div class="col-lg-5">
                        <div class="form-control">
                            <asp:DropDownList ID="ddlCategwiseDepartment" runat="server" Width="100%" AutoPostBack="true" >
                                  <asp:ListItem>COMM</asp:ListItem>
                                  <asp:ListItem>CPSC</asp:ListItem>
                                  <asp:ListItem>ENGL</asp:ListItem>
                                  <asp:ListItem>REGR</asp:ListItem>
                                  <asp:ListItem>ZOOL</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    
                </div>
                <br />
                <div class="form-group">
                    <asp:Label ID="Label8" runat="server" Text="Select Category:" CssClass="col-lg-4 control-label" style="margin-top:20px;"></asp:Label>
                    <asp:ListBox ID="lstCategwiseFromCategory" runat="server" style="height:121px;width:224px;margin-top: 20px;">
                                <%--<asp:ListItem>Clip</asp:ListItem>
                                <asp:ListItem>Envelope</asp:ListItem>
                                <asp:ListItem>Eraser</asp:ListItem>
                                <asp:ListItem>Exercise</asp:ListItem>
                                <asp:ListItem>File</asp:ListItem>
                                <asp:ListItem>Pad</asp:ListItem>
                                <asp:ListItem>Paper</asp:ListItem>
                                <asp:ListItem>Pen</asp:ListItem>
                                <asp:ListItem>Puncher</asp:ListItem>--%>
                    </asp:ListBox>
                </div>
                </div>
                <div class="col-lg-1">
                <div>
                &nbsp;
                &nbsp;
                </div>
                
                <div>&nbsp;</div>
                <div class="form-group">
                    <asp:Button ID="btnPutCategory" runat="server" Text=">>" CssClass="btn btn-primary" 
                        style="margin-top:15px;" onclick="btnPutCategory_Click" />
                </div>
                <div class="form-group">
                    <asp:Button ID="btnRetrieveCategory" runat="server" Text="<<" 
                                    CssClass="btn btn-primary" 
                        onclick="btnRetrieveCategory_Click" />
                </div>
                </div>
                <div class="col-lg-4">
                <div>
                &nbsp;
                &nbsp;
                </div>
                <div>
                &nbsp;
                &nbsp;
                </div>
                    <asp:ListBox ID="lstCategwiseToCategory" runat="server" style="height:121px;width:224px;margin-top: 15px;"></asp:ListBox><br />
                    <div class="form-group">
                    &nbsp;
                    <asp:Button ID="btnCategwise" runat="server" Text="Generate Report"  
                    CssClass="btn btn-primary" style="margin-top:5px;" onclick="btnCategwise_Click" />
                    </div>
                    
                </div>
            </asp:View>
            </asp:MultiView>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        <div>
                <asp:Label ID="lblStatus" runat="server" ></asp:Label>
            </div>
            <div>
            <asp:UpdatePanel ID="updatePanelViewer" runat="server" >
            <ContentTemplate>
            <CR:CrystalReportViewer ID="CrView" runat="server" AutoDataBind="true" HasCrystalLogo="False" BorderStyle="Groove" BorderWidth="1px" ReuseParameterValuesOnRefresh="true" />
            </ContentTemplate>
            </asp:UpdatePanel>
            </div>
    </fieldset>
   </div>
</asp:Content>
