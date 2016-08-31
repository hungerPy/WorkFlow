<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CompanyName.aspx.vb" Inherits="CompanyName" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>   </title>
    <link rel="icon" href="images/hslogo.jpg" type="image/x-icon">
    <script src="~/js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="~/js/common.js" type="text/javascript"></script>
    <script src="~/js/bootstrap.js"></script>
    <meta name="~/apple-mobile-web-app-capable" content="yes" />
    <a href="~/font/glyphicons-halflings-regular.eot"></a>
    <%--<link href="~/css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,600italic,400,600" rel="stylesheet" />
    <%--<link href="~/css/bootstrap.css" rel="stylesheet" />--%>
    <link href="~/css/font-awesome.css" rel="stylesheet" />
    <link href="~/css/font-awesome.min.css" rel="stylesheet" />
    <script src="~/js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="~/js/common.js" type="text/javascript"></script>
    <link href="~/css/bootstrap.min.css" rel="stylesheet" />
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,600italic,400,600" rel="stylesheet" />
    <link href="~/css/style.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            $('.slidediv').show();
            $('.showhide').click(function () {
                $('.slidediv').toggle('showhide');
                $(this).attr("src", 'img/download3.jpg');

            }, function () {
                $('.slidediv').toggle('showhide');
                $(this).attr('src', 'img/icon.png');
                return false;
            });
        });


        function setNoValidate() {
            for (var f = document.forms, i = f.length; i--;) f[i].setAttribute("novalidate", i);
            document.forms[0].submit(); // replace 0 with with actual form name, like document.formSub
        }

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#blah')
                        .attr('src', e.target.result)
                        .width(100)
                        .height(100);
                };

                reader.readAsDataURL(input.files[0]);
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="navbar navbar-fixed-top">
            <div class="navbar-inner">
                <div class="container">
                    <a class="brand" href="Home.aspx" style="vertical-align: text-bottom;">
                      <%--  <img alt="" src="Images/logosakshamhalf1.png" />
                        <img alt="" src="Images/logosakshamhalf21.png" />--%>
                         <%--<img alt="" src="Images/hsslogo.png"  />--%>
                        <h3> Select Your Company </h3>
                        <%-- <img alt="" src="Images/head.jpg"  />--%>
                    </a>

                    <%-- <div class="nav-collapse">
                        <ul class="nav pull-right">
                            <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><i
                                class="icon-user"></i><%= Session("fullname")%><b class="caret"></b></a>
                                <ul class="dropdown-menu">
                                    <li><a href="Logout.ashx">Logout</a></li>
                                </ul>
                            </li>
                        </ul>

                    </div>--%>
                    <!--/.nav-collapse -->
                </div>
                <!-- /container -->
            </div>
            <!-- /navbar-inner -->
        </div>
        <div class="widget container ">
            <div class="widget-header" id="divselectcompany" runat="server" >
                <i class="icon-file"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b style="display:none;">Select Your Company Name</b>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <%--<a href="Default.aspx">Add Company</a>--%>
                <asp:LinkButton ID="lnkaddcompany" runat="server" OnClick="lnkaddcompany_Click" Visible="false" Font-Underline="false">Add Company</asp:LinkButton>

            </div>
            <div class="widget" runat="server" id="divdlcompanyname">
                <asp:DataList ID="dlcompanyname" CssClass="table" runat="server" RepeatColumns="3" DataKeyField="companyid">
                    <ItemTemplate>
                        <center> <asp:ImageButton ID="imgbtnclogo" runat="server" ImageUrl='<%# "~/logos/" + DataBinder.Eval(Container.DataItem, "logo")%>' Height="100px" Width="100px" ></asp:ImageButton><br /></center>
                        <center><asp:Label ID="lvlcompname" runat="server" Text='<%#Eval("companyname") %>' CssClass="lead" ForeColor="DarkBlue" ></asp:Label></center>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <asp:Panel ID="pnladdcompany" runat="server" Visible="false">
            <div class="widget container ">
                <div class="widget-header">
                    <i class="icon-certificate"></i>&nbsp;&nbsp;Add Company
                </div>

                <div class="widget-content ">
                    <table class="table">
                        <tr>
                            <td>Enter Company Name:</td>
                            <td>
                            <asp:TextBox ID="txtcompany" runat="server" placeholder="Enter Company Name" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="retxtcompany" runat="server"  ForeColor="Red" ErrorMessage="*" ControlToValidate="txtcompany" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td>Company Abbreviation*:</td>
                            <td>
                            <asp:TextBox ID="txtAbbreviation" runat="server" placeholder="Enter Abbreviation Name" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  ForeColor="Red" ErrorMessage="*" ControlToValidate="txtAbbreviation" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td>Company Director Name:</td>
                            <td>
                            <asp:TextBox ID="txtdirector" runat="server" placeholder="Enter Director Name" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ForeColor="Red" ErrorMessage="*" ControlToValidate="txtdirector" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>Company Logo:</td>
                            <td>
                            <asp:FileUpload ID="clogo" runat="server" name="clogo" CssClass="btn" onchange="readURL(this);" />
                                  <img id="blah" src="#" alt="" height="0" width="0" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ForeColor="Red" ErrorMessage="*" ControlToValidate="clogo" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" style="text-align:center;">
                                <asp:Button ID="btnregister" runat="server" Text="Register" CssClass="btn btn-primary " />
                            </td>
                        </tr>

                    </table>
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
