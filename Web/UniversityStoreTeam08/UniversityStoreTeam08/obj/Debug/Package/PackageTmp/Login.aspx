<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UniversityStoreTeam08.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
       
    </title>
     <link href="Styles/bootstrap.min.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="Styles/Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form runat="server">
                <section class="container">
                <div class="login">
                  <h1>Login to Logic University Web App</h1>
                  <form method="post" action="index.html">
                    <p>
                        <asp:TextBox ID="txtUserName" runat="server" placeholder="Username" required></asp:TextBox>
                        
                        <%--<input type="text" name="login" value="" placeholder="Username or Email">--%>
                    </p>
                    <p>
                        <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" 
                            TextMode="Password" required></asp:TextBox>

                        <%--<input type="password" name="password" value="" placeholder="Password">--%>
                    </p>
                    <%--<p class="remember_me">
                      <label>
                        <input type="checkbox" name="remember_me" id="remember_me">
                        Remember me on this computer
                      </label>
                    </p>--%>
                    <p class="submit">
                        <asp:Button ID="btnLogin" runat="server" Text="Login" onclick="btnLogin_Click"></asp:Button>
                        <%--<input type="submit" name="commit" value="Login">--%>
                    </p>
                    <p>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </p>

                  </form>
                </div>

                <%--<div class="login-help">
                  <p>Forgot your password? <a href="index.html">Click here to reset it</a>.</p>
                </div>--%>
              </section>
              </form>
</body>
</html>
