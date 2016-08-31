<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="HR_Employee.aspx.vb" Inherits="Admin_HR_Employee" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
  



        <link href="jvalidation/ValidationEngine.css" rel="stylesheet" type="text/css"  />
    <link href="css/formStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="jvalidation/jquery.min.js"></script>
<script type="text/javascript" src="jvalidation/jquery.validationEngine-en.js" charset="utf-8"></script>
<script type="text/javascript" src="jvalidation/jquery.validationEngine.js" charset="utf-8"></script>
 

    <script type="text/javascript">
        function EName(txtEmpName, str) {
            var str1 = str.toUpperCase();
            document.getElementById(txtEmpName).value = str1;
        }

        function ENo(txtEmpNo, str) {
            var str1 = str.toUpperCase();
            document.getElementById(txtEmpNo).value = str1;
        }

        

        function DName(txtDesignation, str) {
            var str1 = str.split(" ").map(function (i) { return i[0].toUpperCase() + i.substring(1) }).join(" ");
            document.getElementById(txtDesignation).value = str1;
        }

        function Add(txtAdd, str) {
            var str1 = str.split(" ").map(function (i) { return i[0].toUpperCase() + i.substring(1) }).join(" ");
            document.getElementById(txtAdd).value = str1;
        }

        function Remark(txtremarks, str) {
            var str1 = str.split(" ").map(function (i) { return i[0].toUpperCase() + i.substring(1) }).join(" ");
            document.getElementById(txtremarks).value = str1;
        }
        

           </script>

      <script language="javascript" type="text/javascript">
        function Show() {
            document.getElementById("Ing").style.display = ""
        }
        function DispHide(tbl) {
            if (document.getElementById(tbl).style.display == "none")
                document.getElementById(tbl).style.display = "block";
            else
                document.getElementById(tbl).style.display = "none";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">


    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <script type="text/javascript">

        $(function () {
            $('[id*=btnSubmit]').bind("click", function () {
                $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
            });
        });


        function drpCompanyName(field, rules, i, options) {
            if ($('[id$=drpCompanyName] option:selected').val() == "0") {
                return "This Feild Required."
            }
        }

        function DrpDivision(field, rules, i, options) {
            if ($('[id$=DrpDivision] option:selected').val() == "0") {
                return "This Feild Required."
            }
        }


    </script>

    <div class="span12">
        <div class="widget" id="trmsg" runat="server" style="display: none;">
            <div class="widget-header">
                <i class="icon-search"></i>
                <h3>
                    <asp:Literal ID="pagesearch" runat="server"></asp:Literal>
                </h3>
            </div>
            <!-- /widget-header -->

            <div class="widget-content">
                <div class="control-group-search">
                    <label for="firstname" class="control-label">
                        Search By Email Address :</label>
                    <asp:TextBox ID="txtsearch" CssClass="span2" runat="server"></asp:TextBox>
                    <asp:Button ID="imgbtnSearch" class="btn btn-primary" Text="Search" runat="server" />
                    <!-- /controls -->
                </div>
                <!-- /shortcuts -->
            </div>
            <!-- /widget-content -->
        </div>

        <asp:Literal ID="lblmsgs" runat="server" Text=""></asp:Literal>
        <div class="widget widget-table action-table">
            <div class="widget-header">
                <i class="icon-th-list"></i>
                <h3>
                    <asp:Literal ID="pagename" runat="server"></asp:Literal></h3>
                <a href="adminuser.aspx" class="btn btn-primary">User Listing</a>
                <a href="Add-AdminUser.aspx" class="btn btn-primary">Add User</a>
                <a href="HR_DivisionMaster.aspx" class="btn btn-primary">Add Division</a>
                

                <div class="span2 pull-right gotodropdown" style="display: none;">
                    <label for="category" class="control-label">
                    </label>

                </div>
            </div>
            <!-- /widget-header -->
            <div class="widget-content">
<%--**************************************************Paste Data******************************************--%>
           <table id="tblHeader" width="100%" cellpadding="0" cellspacing="0" border="0" style="display:none;">
                <tr>
                    <td align="center">Employee Master<br />
                        <asp:TextBox ID="txtId" runat="server" Visible="False"></asp:TextBox></td>
                </tr>

               </table> 

                <asp:Calendar runat="server" ></asp:Calendar>

            <table width="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="gray" style="margin-top:10px;">
                <tr>
                    <td>
                    
                                    <!--Contents-->

                                    <table width="100%" border="0" cellpadding="2" cellspacing="0" id="tblForm" >
                                        <tr>
                                            <td width="20%">Company Name*:</td>
                                            <td width="30%">
                                                <asp:DropDownList ID="drpCompanyName" CssClass="validate[required,funcCall[drpCompanyName[]]" runat="server" AccessKey="r" ToolTip="Company Name">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="20%">Employee No.*:</td>
                                            <td width="30%">
                                                <asp:TextBox runat="server" placeholder="Employee No." CssClass="validate[required]" ID="txtEmpNo" onkeyup="javascript:ENo(this.id, this.value)" ToolTip="Employee No."></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Department Name*:</td>
                                            <td>
                                                <asp:DropDownList CssClass="validate[required,funcCall[DrpDivision[]]" ID="DrpDivision" runat="server" ToolTip="Division Name">
                                                </asp:DropDownList>
                                            </td>
                                            <td>Employee Type*:</td>
                                            <td>
                                                <asp:DropDownList ID="drpType" runat="server">
                                                    <asp:ListItem Value="On Salary">On Salary</asp:ListItem>
                                                    <asp:ListItem Value="Contractor">Contractor</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>Employee Name*:</td>
                                            <td>
                                                <asp:TextBox ID="txtEmpName"  Placeholder="Employee Name" CssClass="validate[required,custom[onlyLetterSp]]" onkeyup="javascript:EName(this.id, this.value)"  runat="server" AccessKey="r" ToolTip="Employee Name"></asp:TextBox>
                                            </td>
                                            <td>Designation*:</td>
                                            <td>
                                                <asp:TextBox runat="server" Placeholder="Designation" CssClass="validate[required,custom[onlyLetterSp]]" onkeyup="javascript:DName(this.id, this.value)" ID="txtDesignation"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Time (IN)*:</td>
                                            <td>
                                                <asp:DropDownList runat="server" Width="65px" ID="drpIH" ToolTip="Hour"></asp:DropDownList>
                                                <asp:DropDownList runat="server" Width="65px" ID="drpIM" ToolTip="Minutes"></asp:DropDownList>
                                                <asp:DropDownList runat="server" Width="80px" ID="drpIAP" ToolTip="AM/PM"><asp:ListItem Value="AM" Selected="true">AM</asp:ListItem><asp:ListItem Value="PM">PM</asp:ListItem></asp:DropDownList>
                                            </td>
                                            <td>Time (OUT)*:</td>
                                            <td>

                                                <asp:DropDownList runat="server" Width="65px" ID="drpOH" ToolTip="Hour"></asp:DropDownList>
                                                <asp:DropDownList runat="server" Width="65px" ID="drpOM" ToolTip="Minutes"></asp:DropDownList>
                                                <asp:DropDownList runat="server" Width="82px" ID="drpOAP" ToolTip="AM/PM">
                                                    <asp:ListItem Value="AM">AM</asp:ListItem>
                                                    <asp:ListItem Value="PM" Selected="true">PM</asp:ListItem>

                                                </asp:DropDownList>
                                            </td>
                                        </tr>




                                        <tr>
                                            <td colspan="4" bgcolor="Gray">
                                                <img src="Images/Add.gif" alt="" width="30px" height="20px" onclick="DispHide('tblAddMore')" style="cursor: hand;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblAddMore" style="display: none  ; margin-top:10px;">

                                                    <tr>
                                                        <td>Email-ID:</td>
                                                        <td><asp:TextBox ID="txtEmail" Placeholder="Email-ID" CssClass="validate[custom[email]]" runat="server" ToolTip="E-mail"></asp:TextBox></td>
                                                        
                                                        <td>Gender:</td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rblSex"  runat="server" RepeatDirection="Horizontal" Width="117px">
                                                                <asp:ListItem Value="M" Selected="True">Male</asp:ListItem>
                                                                <asp:ListItem Value="F">Female</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        </tr>

                                                    <tr>
                                                        <td>Phone No.:</td>
                                                        <td><asp:TextBox ID="txtPhone" Placeholder="Phone No." CssClass="validate[custom[phone]]" runat="server" ToolTip="Phone No"></asp:TextBox></td>
                                                        <td>Mobile No.:</td>
                                                        <td><asp:TextBox ID="txtMobile" Placeholder="Mobile No." CssClass="validate[custom[phone]]" runat="server" ToolTip="Mobile No"></asp:TextBox></td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>Date Of Birth:</td>
                                                        <td><asp:TextBox runat="server" ID="txtDOB" onpaste="return false;" Width="180px" onkeydown="return false;" oncut="return false;"></asp:TextBox>
                                                            <img src="Images/calander.gif" id="img1"  alt="" />
                                                           <%-- <ajaxtoolkit:calendarextender ID="Calendarextender12" CssClass="cal_Theme1" runat="server" TargetControlID="txtDOB" Format="dd-MMM-yyyy" PopupButtonID="img1" />--%>
                                                            <ajaxToolkit:CalendarExtender ID="Calendarextender12" runat="server" CssClass="cal_Theme1" TargetControlID="txtDOB" Format="dd-MMM-yyyy" PopupButtonID="img1"></ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td>Date Of Joining</td>
                                                        <td><asp:TextBox runat="server" ID="txtDOJ" onpaste="return false;" Width="180px" onkeydown="return false;" oncut="return false;"></asp:TextBox>
                                                            <img src="Images/calander.gif" id="img2" alt=""  onclick="return false;" />
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDOJ" PopupButtonID="img2" runat="server" Format="dd-MMM-yyyy" />
                                                        </td>
                                                    </tr>



                                                    <tr>
                                                        <td width="20%">Contact Address:</td>
                                                        <td width="30%"> <asp:TextBox placeholder="Contact Address" TextMode="MultiLine" ID="txtAdd" runat="server" onkeyup="javascript:Add(this.id, this.value)" ToolTip="Address"></asp:TextBox></td>
                                                        
                                                        <td>Remarks</td>
                                                        <td><asp:TextBox runat="server" Placeholder="Remark" onkeyup="javascript:Remark(this.id, this.value)" ID="txtremarks" ToolTip="Remarks" TextMode="MultiLine"></asp:TextBox></td>
                                                    </tr>



                                                    <tr>
                                                         <td style="display: none">Working Shift:</td>
                                                        <td style="display: none;">
                                                            <asp:DropDownList runat="server" ID="drpShift" ToolTip="Working Shift"></asp:DropDownList>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblImage" runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                        
                                                    </tr>


                                                    <tr>
                                                        <td>Photo:</td>
                                                        <td ><asp:FileUpload ID="fup1" runat="server" /><img src="" alt="" id="EmpImg" runat="server" width="150" height="150" /></td>
                                                        <td width="20%">Reporting To:</td>
                                                        <td width="30%"><asp:DropDownList runat="server" ID="drpemps" ToolTip="Supervisor"></asp:DropDownList></td>
                                                    
                                                    </tr>


                                                </table>
                                            </td>
                                        </tr>

                                        
                         
                                                    <tr style="background-color: white">
                                            <td colspan="4" align="center">
                                                <asp:Label ID="lblError" runat="server" Text="" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>


                                                    <tr>
                                            <td colspan="4" align="center">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Add Employee" />
                                               
   
                                            </td>
                                        </tr>
                                        
                                    </table>
                                    <!--Content Ends-->
                             

                    </td>
                </tr>
            </table>
             
            </div>
            <div class="form-actions" style="display: none;">
                <asp:Button ID="btndel" Text="Delete" CssClass="btn" runat="server" OnClientClick="javascript:return ItemSelect();" />&nbsp;&nbsp;&nbsp; <a href="add-adminuser.aspx" class="btn btn-primary">Add New</a>
            </div>
            <!-- /widget-content -->
        </div>
        <!-- /widget -->
        <!-- /widget -->
    </div>
</asp:Content>

