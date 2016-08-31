<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmpRegistration.aspx.vb" MasterPageFile="MasterPage.master" MaintainScrollPositionOnPostback="true"  Inherits="EmpRegistration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link rel="icon" href="images/hslogo.jpg" type="image/x-icon" />
    <title>Employee Registration</title>
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
        function CalBcost(br, ar, bc) {
            if (br.value == "")
                br.value = "0"
            if (ar.value == "")
                ar.value = "0"
            bc.value = (parseFloat(br.value) * parseFloat(ar.value)).toFixed(0)
        }
        function calArea(t1, t2, t) {
            if (t1.value == "") t1.value = "0"
            if (t2.value == "") t2.value = "0"
            t.value = ((parseFloat(t1.value) * parseFloat(t2.value)) / 9).toFixed(2)
            var am;
            am = document.getElementById("txtAreaM")
            am.value = (parseFloat(t.value) * 0.837).toFixed(2)

        }
        function checkKey() {
            if (window.event.keyCode != 9)
                return false;
        }

        function checkKey1() {
            if (window.event.keyCode != 9)   
                return false;
        }

        function calMtr() {
            var a, m;
            a = document.getElementById("txtArea")
            b = document.getElementById("txtAreaM")
            if (a.value == "") {
                a.value = "0"
                b.value = "0"
            }
            b.value = (parseFloat(a.value) * 0.837).toFixed(0);
        }

        function copy_data(val) {
            var a = document.getElementById(val.id).value
            document.getElementById("txtaccname").value = a
        }
    </script>
    <style type="text/css">
        form li#send button {
            background: #003366 url(images/css-form-send.gif) no-repeat 8px 50%;
            border: none;
            align: center;
            padding: 4px 8px 4px 28px;
            border-radius: 15%; /* Don't expect this to work on IE6 or 7 */
            -moz-border-radius: 15%;
            -webkit-border-radius: 15%;
            color: #fff;
            margin-left: 77px; /* Total width of the labels + their right margin */
            cursor: pointer;
        }

            form li#send button:hover {
                background-color: #006633;
            }
    </style>
    <script type="text/javascript">
        function NewWindow() {
            document.forms[0].target = "_blank";
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('[id*=btnSubmit]').bind("click", function () {
                $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
            });
        });



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
        function drp2(field, rules, i, options) {
            if ($('[id$=DrpDivision] option:selected').val() == "0") {
                return "This Feild Required."
            }
        }

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

    <div class="widget container">
        <div class="widget-header ">
            <i class="icon-user"></i>&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-size: 18px; font: bold; font-family: inherit;"><b> Employee Registration : &nbsp;&nbsp;<asp:Label ID="lblcompnyname" runat="server"> </asp:Label></b></span><asp:Label ID="lblid" runat="server" Visible="false" Text=""></asp:Label><asp:TextBox ID="txtempno" runat="server" Visible="false"></asp:TextBox><asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>

              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           
     

        </div>

        <div class="widget-content">
            <asp:Panel ID="pnlpersonaldata" runat="server" Width="100%">
                <table class="table">
                    <tr>
                        <td colspan="4" style="background-color: white">
                            <asp:Label ID="lblerror" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                           <h3> Personal information:</h3>
                        </td>
                    </tr>

                    <tr>
                        <td width="20%" valign="middle" style="padding-left: 10px;">Employee Name*</td>
                        <td  valign="middle">
                            <asp:TextBox ID="txtfirstname" runat="server" CssClass="validate[required]" placeholder="Employee name"></asp:TextBox>
                        </td>
                         <td style="padding-left: 10px;">Sex</td>
                        <td  valign="middle">
                            <asp:RadioButtonList ID="rdbsex" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="M" Selected="True">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                      <tr>
                           <td style="padding-left: 10px;">Qualification*</td>
                        <td> <asp:TextBox ID="txtqualification" ToolTip="Qualification" CssClass="validate[required]" runat="server" ></asp:TextBox></td>
                           <td style="padding-left: 10px;">
                             Mobile No*
                         </td>
                         <td><asp:TextBox ID="txtContactNo" CssClass="validate[required]" ToolTip="Mobile No." runat="server" ></asp:TextBox></td>
                    </tr>
                     <tr>
                        
                        <td style="padding-left: 10px;">Email-ID *</td>
                        <td>
                            <asp:TextBox ID="txtEmailid" ToolTip="Personal Email-Id" CssClass="validate[required,custom[email]]" runat="server" ></asp:TextBox></td>
                          <td style="padding-left: 10px;">Employee Photo:</td> 
                        <td>
                        <asp:FileUpload ID="fupphoto" runat="server" Height="25px" Width="300px"  name="clogo"  onchange="readURL(this);"  ToolTip="(Image Format : JPEG,BMP,PNG)"></asp:FileUpload>&nbsp;<br />(Image Size : 150px * 150px approx. )
                            <img id="blah" src="#" alt="" height="0" width="0" />
                        </td>
                    </tr>
                    </table>
            </asp:Panel>


            <asp:Panel ID="pnlposition" runat="server">
                    <table class="table">
                        <tr>
                            <td colspan="4">&nbsp;<h3>Position:</h3>
                            </td>
                        </tr>
                    <tr>
                        <td style="padding-left: 10px;">Date of Joining</td>
                        <td>
                               <asp:TextBox ID="txtdate" onpaste="return false;" placeholder="dd-mm-yyyy" onkeydown="return checkKey()" oncut="return false;" CssClass="validate[required]" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="red" Enabled="True" TargetControlID="txtdate" PopupButtonID="Image1" Format="dd-MMM-yyyy" DaysModeTitleFormat="dd-MMM-yyyy">
                                </asp:CalendarExtender></td>
                        <td>Department Name</td>
                        <td>   <asp:DropDownList ID="DrpDivision" runat="server" CssClass="validate[required,funcCall[drp2[]]]" ToolTip="Division Name"></asp:DropDownList></td>
                    </tr>

                    <tr>
                         <td style="padding-left: 10px;">Employee Type</td>
                        <td>
                            <asp:DropDownList ID="drpType" runat="server">
                                <asp:ListItem Value="Payrole">Onrole</asp:ListItem>  
                                <asp:ListItem Value="Part Time">Part Time</asp:ListItem>
                                <asp:ListItem Value="Contract">Contract</asp:ListItem>
                            </asp:DropDownList></td>
                         <td width="20%" valign="middle" style="padding-left: 10px;">Employee Designation</td>
                        <td colspan="3">
                            <asp:DropDownList ID="drpempdegi" ToolTip="Employee Degination" runat="server"></asp:DropDownList></td>
                        
                       
                    </tr>
                    <tr>
                        <td style="padding-left: 10px;">Reporting To</td>
                        <td>
                            <asp:DropDownList ID="drprepotTo" runat="server" ToolTip="Reporting To">
                            </asp:DropDownList>
                        </td>
                         <td style="padding-left: 10px;" valign="top" width="20%">Company Email-ID</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtcompmailid" runat="server" ToolTip="Company Email-Id" ></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                Salary(CTC)
                            </td>
                            <td>
                                <asp:TextBox ID="txtsalary" runat="server" ToolTip="Salary in CTC"></asp:TextBox>
                            </td>
                            <td>

                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td colspan="4" style="text-align:center;">
                                &nbsp;&nbsp;
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info " OnClick="btnSubmit_Click"  Width="100px" Height="32px" />

                            </td>
                        </tr>
                       
                        
                </table>
            </asp:Panel>
          
    </div>

</asp:Content>
