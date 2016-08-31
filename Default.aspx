<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Admin_Default" EnableEventValidation="false"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <title>Login-MIS </title>
    <link rel="icon" href="Images/hslogo.jpg" type="image/x-icon"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!--<link href="css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />-->
    <link href="css/font-awesome.css" rel="stylesheet">
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,600italic,400,600"
        rel="stylesheet">
    <link href="css/style.css" rel="stylesheet" type="text/css">
    <link href="css/pages/signin.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form runat="server">
    <div class="navbar navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container">
                <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse"><span
                    class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span>
                </a><a class="brand" href="default.aspx">
                    <%= strname  %>
                </a>
                <div class="nav-collapse">
                    <ul class="nav pull-right">
                      <%--  <li class=""><a href='<%= CommonFunctions.GetKeyValue(3) %>' class=""><i class="icon-chevron-left"></i>Back to
                            <%= CommonFunctions.GetKeyValue(3) %> </a></li>--%>
                    </ul>
                </div>
              
                  <%--   <img alt="" src="Images/TopHeader1.gif" width="100px" />--%>
                  <!--/.nav-collapse -->
            </div>
            <!-- /container -->
        </div>
        <!-- /navbar-inner -->
    </div>
    <!-- /navbar -->
    <div class="account-container">
        <div class="content clearfix">
            <form action="#" method="post">
                <h2>
                &nbsp;Login</h2>
            <div class="login-fields">
                <asp:Label ID="lblmsg" ForeColor="Red" runat="server" Text="" Visible="false"></asp:Label>
                <p>
                    Please provide your details</p>
                <div class="field">
                    <label for="username">
                        Username</label>
                    <asp:TextBox ID="txtuser" runat="server" placeholder="Username" class="login username-field"></asp:TextBox>
                    <asp:RequiredFieldValidator ForeColor="Red" ControlToValidate="txtuser" ValidationGroup="x"
                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Username."></asp:RequiredFieldValidator>
                </div>
                <!-- /field -->
                <div class="field">
                    <label for="password">
                        Password:</label>
                    <asp:TextBox ID="txtpass" runat="server" TextMode="Password" placeholder="Password"
                        class="login password-field"></asp:TextBox>
                    <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator2" runat="server"
                        ValidationGroup="x" ControlToValidate="txtpass" ErrorMessage="Please Enter Password."></asp:RequiredFieldValidator>
                </div>
                <!-- /password -->
            </div>
            <!-- /login-fields -->
            <div class="login-actions">
                <%--<span style="text-align:left"> <asp:LinkButton ID="lnkback" runat="server" Text="Back" Font-Underline="false" ></asp:LinkButton> </span>--%>
                <asp:Button ID="btnlogin" AlternateText="" class="button btn btn-success btn-large"
                    ValidationGroup="x" Text="Sign In" runat="server" OnClick="btnlogin_Click" />
            </div>
            <!-- .actions -->
            </form>
        </div>
        <!-- /content -->
    </div>
    <!-- /account-container -->
    <!-- /login-extra -->
    <script src="js/jquery-1.7.2.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="js/signin.js"></script>
    </form>
</body>
</html>
