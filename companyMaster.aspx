<%@ Page Language="VB" AutoEventWireup="false" CodeFile="companyMaster.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_companyMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link rel="icon" href="images/tiltle_logo.jpg" type="image/x-icon" />
    <title>Company Registration</title>
    <script src="jvalidation/jquery.min.js"></script>
    <script src="jvalidation/jquery.validationEngine-en.js"></script>
    <script src="jvalidation/jquery.validationEngine.js"></script>
    <link href="css/ValidationEngine.css" rel="stylesheet" />
    <link href="css/red.css" rel="stylesheet" />

    <%--<script  src="functions.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <script type="text/javascript">
        function copy_data(val) {
            var a = document.getElementById(val.id).value
            document.getElementById("txtaccname").value = a
        }

        function checkKey() {
            if (window.event.keyCode != 9)
                return false;
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('[id*=btnSubmit]').bind("click", function () {
                $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
            });
        });

        function drp1(field, rules, i, options) {
            if ($('[id$=drpdegi] option:selected').val() == "0") {
                return "This Feild Required."
            }
        }
        function drp2(field, rules, i, options) {
            if ($('[id$=drpcomptype] option:selected').val() == "0") {
                return "This Feild Required."
            }
        }

    </script>
    <script type="text/javascript">
        $(function () {
            $('[id*=btnSSSubmit').bind("click", function () {
                $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
            });
        });
        function drp(field, rules, i, options) {
            if ($('[id$=drpbank] option:selected').val() == "0") {
                return "This Feild Required."
            }
        }


    </script>
    <div class="span12">                             
        <div class="widget container">
            <div class="widget-header ">
                <i class="icon-barcode"></i>&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-size:18px;font:bold;font-family:inherit;"><b>Company Details </b></span>
              <asp:TextBox ID="txtbankid" runat="server" Style="display:none;"></asp:TextBox>  <asp:Label ID="lblid" runat="server" Visible="false" Text=""></asp:Label><asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtimage" runat="server" Visible="false"></asp:TextBox>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <%-- <asp:LinkButton ID="lnknew" runat="server" Text="Add New Company" Font-Underline="false"  ></asp:LinkButton>--%>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <%--<asp:ImageButton ID="imgbtnmenuall" runat="server" ImageUrl="~/icons/list.png" Height="35px" Width="40px" ToolTip="show full form" Style="padding-left:20px;" OnClick="imgbtnmenuall_Click"  />--%>
            </div>
            <div class="widget-content">
                <asp:Panel ID="pnlmain" runat="server" Width="100%">
                    <table class="table" style="width: 100%">
                        <tr>   
                            <td colspan="3" style="padding-left: 10px; ">
                                <h3>General Information:</h3>
                            </td>
                            <td style="text-align:right;">  </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 10px; width: 17%;">Company Name*</td>
                            <td style="padding-left: 10px; width: 22%;">
                                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="validate[required]" ToolTip="Company Name" onkeyup="copy_data(this)"></asp:TextBox></td>
                            <td style="padding-left: 10px; width: 17%;">Company Abbreviation*</td>
                            <td style="padding-left: 10px; width: 23%;">
                                <asp:TextBox ID="txtCompanyAbbreviation" CssClass="validate[required]" runat="server" ToolTip="Company Abbreviation"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="padding-left: 10px;">Registration No.</td>
                            <td style="padding-left: 10px;">
                                <asp:TextBox ID="txtRegNo" runat="server" ToolTip="Registration No"></asp:TextBox></td>
                              <td style="padding-left: 10px;">Date of Regt.</td>
                            <td style="padding-left: 10px;">
                                <asp:TextBox ID="txtdate" onpaste="return false;" placeholder="dd-mm-yyyy" onkeydown="return checkKey()" oncut="return false;" CssClass="validate[required]" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="red" Enabled="True" TargetControlID="txtdate" PopupButtonID="Image1" Format="dd-MMM-yyyy" DaysModeTitleFormat="dd-MMM-yyyy">
                                </asp:CalendarExtender>
                            </td>
                            
                          
                        </tr>
                        <tr>
                              <td style="padding-left: 10px;">Company Type*</td>
                            <td style="padding-left: 10px;">
                                <asp:DropDownList ID="drpcomptype" CssClass="validate[required,funcCall[drp2[]]]" runat="server" ToolTip="Company Type"></asp:DropDownList></td>
                            <td style="padding-left: 10px;">Upload Logo</td>
                            <td style="padding-left: 10px;">
                                <asp:FileUpload ID="fupLogo" runat="server" /><br />
                                <asp:Image ID="fupLogoimg" runat="server" Visible="false" Height="80px" Width="80px" />
                            </td>
                           
                           
                        </tr>
                        <tr>
                             <td style="padding-left: 10px;">Contact Person*</td>
                            <td style="padding-left: 10px;">
                                <asp:TextBox ID="txtContactP" runat="server" CssClass="validate[required]" ToolTip="Contact Person"></asp:TextBox></td>
               <td style="padding-left: 10px;">Fax No.</td>
                            <td style="padding-left: 10px;">
                                <asp:TextBox ID="txtFaxNo" runat="server" ToolTip="Fax No."></asp:TextBox></td>

                        </tr>
                              <tr>
                            <td style="padding-left:10px;">Mobile No.*</td>
                            <td style="padding-left:10px;"><asp:TextBox ID="txtMobileNo" CssClass="validate[required,custom[onlyNumberSp]]" runat="server" ToolTip="Mobile No."></asp:TextBox>
                                </td>
                            <td style="padding-left:10px;">Telephone No.</td>
                            <td style="padding-left:10px;"><asp:TextBox ID="txtTelephoneNo" runat="server" ToolTip="Telephone No."></asp:TextBox>
                               </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 10px;">Email ID.*</td>
                            <td style="padding-left: 10px;">
                                <asp:TextBox ID="txtEmailId" runat="server" CssClass="validate[required,custom[email]]" ToolTip="Email ID"></asp:TextBox>
                            </td>
                            <td style="padding-left: 10px;">&nbsp;Website</td>
                            <td style="padding-left: 10px;">
                                <asp:TextBox ID="txtWebSite" runat="server" ToolTip="WebSite Name"></asp:TextBox>
                            </td>
                        </tr>
                      
                        <tr>
                            <td colspan="4" style="text-align:center;">
                                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSubmit_Click"   Height="43px" Width="153px" />
                            </td>
                        </tr>
                        </table>
                  </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
