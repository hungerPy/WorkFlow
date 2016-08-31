<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="false" CodeFile="MasterReport.aspx.vb" Inherits="Admin_MasterReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link rel="icon" href="Images/hslogo.jpg" type="image/x-icon"/>
     <link href="jvalidation/ValidationEngine.css" rel="stylesheet" type="text/css"  />
    <link href="css/formStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="jvalidation/jquery.min.js"></script>
<script type="text/javascript" src="jvalidation/jquery.validationEngine-en.js" charset="utf-8"></script>
<script type="text/javascript" src="jvalidation/jquery.validationEngine.js" charset="utf-8"></script>
 

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

    <script type="text/javascript">
        function SName(txtSiteName, str) {
            var str1 = str.split(" ").map(function (i) { return i[0].toUpperCase() + i.substring(1) }).join(" ");
            document.getElementById(txtSiteName).value = str1;
        }

        function CName(txtContPerson, str) {
            var str1 = str.split(" ").map(function (i) { return i[0].toUpperCase() + i.substring(1) }).join(" ");
            document.getElementById(txtContPerson).value = str1;
        }

        function Desig(txtDesg, str) {
            var str1 = str.split(" ").map(function (i) { return i[0].toUpperCase() + i.substring(1) }).join(" ");
            document.getElementById(txtDesg).value = str1;
        }

        function Add1(txtAdd1, str) {
            var str1 = str.split(" ").map(function (i) { return i[0].toUpperCase() + i.substring(1) }).join(" ");
            document.getElementById(txtAdd1).value = str1;
        }

        function Add2(txtAdd2, str) {
            var str1 = str.split(" ").map(function (i) { return i[0].toUpperCase() + i.substring(1) }).join(" ");
            document.getElementById(txtAdd2).value = str1;
        }

        function Add3(txtAdd3, str) {
            var str1 = str.split(" ").map(function (i) { return i[0].toUpperCase() + i.substring(1) }).join(" ");
            document.getElementById(txtAdd3).value = str1;
        }

        function WName(txtWHName, str) {
            var str1 = str.split(" ").map(function (i) { return i[0].toUpperCase() + i.substring(1) }).join(" ");
            document.getElementById(txtWHName).value = str1;
        }

        function Scode(txtSiteCode, str) {
            var str1 = str.toUpperCase();
            document.getElementById(txtSiteCode).value = str1;
        }
        
        function Wcode(txtWHCode, str) {
            var str1 = str.toUpperCase();
            document.getElementById(txtWHCode).value = str1;
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">

    <script type="text/javascript">

        $(function () {
            $('[id*=btnSubmit]').bind("click", function () {
                $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
            });
            $('[id*=btnback]').bind("click", function () {
                $("#adminform").validationEngine('detach');
            });
            $('[id*=btnWSubmit]').bind("click", function () {
                $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
            });
            $('[id*=btnwback]').bind("click", function () {
                $("#adminform").validationEngine('detach');
            });
        });


        function DateFormat(field, rules, i, options) {
            var regex = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
            if (!regex.test(field.val())) {
                return "Please enter date in dd/MM/yyyy format."
            }
        }

        function drp(field, rules, i, options) {
            if ($('[id$=drpSPlant] option:selected').val() == "0") {
                return "This Feild Required."
            }
        }

        function drpWp(field, rules, i, options) {
            if ($('[id$=drpWPlant] option:selected').val() == "0") {
                return "This Feild Required."
            }
        }

        function drpS(field, rules, i, options) {
            if ($('[id$=drpSiteOffice] option:selected').val() == "0") {
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
                <a href="ProductMaster.aspx" class="btn btn-primary">Add Product</a>
                <a href="CategoryReport.aspx" class="btn btn-primary">Category Report</a>
                <a href="TaxMaster.aspx" class="btn btn-primary">Add Tax</a>
                <a href="UomMaster.aspx" class="btn btn-primary">Add UOM</a>



                <div class="span2 pull-right gotodropdown" style="display: none;">
                    <label for="category" class="control-label">
                    </label>

                </div>
            </div>
            <!-- /widget-header -->
            <div class="widget-content">
                <%--**************************************************Paste Data******************************************--%>

                <table id="tblHeader" width="100%" border="0" style="display:none;">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblheader" runat="server" Text="SiteOffice & Warehouse Master"></asp:Label><asp:TextBox
                                ID="txtId" runat="server" Visible="false"></asp:TextBox></td>
                    </tr>
                </table>
                <asp:Panel ID="pnlsiteoff" runat="server" Width="100%" Style="margin-top:10px;">
                    <table width="100%" cellpadding="0" cellspacing="0" border="0" id="Table1" >
                        <tr>
                            <td width="20%" class="fontsize">Plant Name</td>
                            <td>
                                <asp:DropDownList ID="drpSPlant" CssClass="design_drpbig validate[required,funcCall[drp[]]" Width="220px" runat="server" ToolTip="Plant Name">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="fontsize">Site Office Code</td>
                            <td>
                                <asp:TextBox ID="txtSiteCode" CssClass="design_form validate[required]" onkeyup="javascript:Scode(this.id, this.value)" runat="server" Placeholder="Site Office Code" ToolTip="Site Code"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fontsize">Site Office Name</td>
                            <td>
                                <asp:TextBox ID="txtSiteName" runat="server" Placeholder="Site Office Name" CssClass="design_form validate[required,custom[onlyLetterSp]]" onkeyup="javascript:SName(this.id, this.value)" ToolTip="Site Name"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fontsize">Contact Person</td>
                            <td>
                                <asp:TextBox CssClass="design_form validate[required,custom[onlyLetterSp]]" Placeholder="Contact Person" onkeyup="javascript:CName(this.id, this.value)" ID="txtContPerson" runat="server" ToolTip="Contact Person"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fontsize ">Designation</td>
                            <td>
                                <asp:TextBox CssClass="design_form validate[required]" onkeyup="javascript:Desig(this.id, this.value)" ID="txtDesg" Placeholder="Designation" runat="server" ToolTip="Designation"></asp:TextBox></td>
                        </tr>
                        <tr style="display: none">
                            <td>Add On</td>
                            <td>
                                <asp:TextBox ID="txtAddon" Text="0" runat="server" ToolTip="Add On"></asp:TextBox></td>
                        </tr>
                        <tr style="display: none;">
                            <td>Max. Discount</td>
                            <td>
                                <asp:TextBox ID="txtMaxDisc" runat="server" Text="0" ToolTip="Max. Allowed Discount"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fontsize ">Status</td>
                            <td>
                                <asp:DropDownList CssClass="design_drpbig" ID="drpFlg" runat="Server">
                                    <asp:ListItem Value="0">Active</asp:ListItem>
                                    <asp:ListItem Value="1">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" bgcolor="Gray">
                                <img src="Images/Add.gif" alt="" width="30px" height="20px" onclick="DispHide('tblAddMore')"
                                    style="cursor: hand;" />
                            </td>
                        </tr>
                    </table>
                    <table id="tblAddMore" width="100%" border="0" style="display:none;">
                        <tr>
                            <td colspan="2">Contact Details</td>
                        </tr>
                        <tr>
                            <td class="fontsize" width="20%">Address Line 1</td>
                            <td>
                                <asp:TextBox ID="txtAdd1" runat="server" CssClass="design_form" placeholder="Address1" onkeyup="javascript:Add1(this.id, this.value)"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fontsize">Address Line 2</td>
                            <td>
                                <asp:TextBox ID="txtAdd2" runat="server" CssClass="design_form" placeholder="Address2" onkeyup="javascript:Add2(this.id, this.value)"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fontsize">Address Line 3</td>
                            <td>
                                <asp:TextBox ID="txtAdd3" runat="server" CssClass="design_form" placeholder="Address3" onkeyup="javascript:Add3(this.id, this.value)"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fontsize">Location</td>
                            <td>Country :
                        <asp:DropDownList ID="drpCountry" runat="server" AutoPostBack="true" Width="120px" CssClass="design_drpbig">
                        </asp:DropDownList>
                                State :
                        <asp:DropDownList ID="drpState" runat="server" AutoPostBack="true" Width="120px" CssClass="design_drpbig">
                        </asp:DropDownList>
                                City:
                        <asp:DropDownList ID="drpCity" runat="server" CssClass="design_drpbig" Width="120px">
                        </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="fontsize">Pincode</td>
                            <td>
                                <asp:TextBox ID="txtPincode" runat="server" placeholder="Pincode" CssClass="design_form"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fontsize">Office No.</td>
                            <td>
                                <asp:TextBox ID="txtOffNo" runat="server" placeholder="Office No." CssClass="design_form validate[custom[phone]"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fontsize">Mobile No.</td>
                            <td>
                                <asp:TextBox ID="txtMob" runat="server" placeholder="Mobile No." CssClass="design_form validate[custom[phone]"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fontsize">Email-Id</td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" placeholder="Email-Id" CssClass="design_form validate[custom[email]]"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fontsize">Website</td>
                            <td>
                                <asp:TextBox ID="txtWebsite" runat="server" placeholder="Website" CssClass="design_form"></asp:TextBox></td>
                        </tr>
                    </table>
                    <table id="tblSubmit" width="100%" border="0" >
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="btnSubmit" CssClass="buttonstyle" Width="150px" runat="server" Text="Create SiteOffice" />
                                <asp:Button ID="btnback" CssClass="buttonstyle" runat="server" Text="Back" /></td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlwarehouse" runat="server" Width="100%" Style="margin-top:10px;">
                    <table width="100%" cellpadding="0" cellspacing="0" border="0" id="Table2" >
                        <tr>
                            <td width="20%" class="fontsize">Plant Name</td>
                            <td>
                                <asp:DropDownList ID="drpWPlant" runat="server" AutoPostBack="True" ToolTip="Plant Name" CssClass="design_drpbig validate[required,funcCall[drpWp[]]">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="fontsize">Site Office</td>
                            <td>
                                <asp:DropDownList ID="drpSiteOffice" runat="server" ToolTip="Site Office" CssClass="design_drpbig validate[required,funcCall[drpS[]]">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="fontsize">Warehouse Code</td>
                            <td>
                                <asp:TextBox ID="txtWHCode" runat="server" ToolTip="Ware House Code" CssClass="design_form validate[required]" Placeholder="Site Office Code" onkeyup="javascript:Wcode(this.id, this.value)"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fontsize">Warehouse Name</td>
                            <td>
                                <asp:TextBox ID="txtWHName" runat="server" ToolTip="Ware House Name" CssClass="design_form validate[required]" Placeholder="Site Office Code" onkeyup="javascript:WName(this.id, this.value)"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fontsize">Status</td>
                            <td>
                                <asp:DropDownList ID="drpwFlag" CssClass="design_drpbig" runat="server">
                                    <asp:ListItem Value="0">Active</asp:ListItem>
                                    <asp:ListItem Value="1">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Label ID="lblWError" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" >
                                <asp:Button ID="btnWSubmit" CssClass="buttonstyle" Width="170px" runat="server" Text="Create Warehouse" />
                                <asp:Button ID="btnwback" CssClass="buttonstyle" runat="server" Text="Back" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlshow" runat="server" Width="100%" Style="margin-top:10px;">
                    <table id="tblParty" width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="15%" class="fontsize">Select Plant</td>
                            <td width="35%" align="left">
                                <asp:DropDownList ID="drpplant" Width="200px" runat="server" CssClass="design_drpbig  validate[required]" AutoPostBack="true" ToolTip="Select Plant"></asp:DropDownList></td>
                            <td width="40%" align="left">
                                <asp:Button ID="btnsiteoff" runat="server" Width="150px" Text="Add SiteOffice" Visible="false" CssClass="buttonstyle" />
                                <asp:Button ID="btneditsiteoffice" runat="server" Width="130px" Text="Edit Siteoffice" CssClass="buttonstyle"  />
                                <asp:Button  CssClass="buttonstyle" Width="150px" ID="btneditwarehouse" runat="server" Text="Edit Warehouse" /></td>
                        </tr>
                    </table>
                    <table width="100%" border="0" id="tblForm">
                        <tr>
                            <td>
                                <asp:GridView ID="GVsiteoff" runat="server" Width="100%" AutoGenerateColumns="false"
                                    DataKeyNames="siteOffId" RowStyle-BackColor="PapayaWhip">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Site Name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%#Eval("siteLText")%>
                                            </ItemTemplate>
                                            <ItemStyle Width="40%" HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact Person" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%#Eval("contactPerson")%>
                                        (<%#Eval("designation")%>)
                                            </ItemTemplate>
                                            <ItemStyle Width="25%" HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" ToolTip="Add Ledger" CommandName="Edit"
                                                    Text="Add Warehouse">
     
                                                </asp:LinkButton>
                                                <img src="Images/ddown.jpg" alt="View Ledger" style="border: 0; cursor: hand;"
                                                    onload="openClose('tbl<%# Eval("siteOffId")%>');" onclick="openClose('tbl<%# Eval("siteOffId")%>');" />
                                        </td></tr>
                                        <tr>
                                            <td colspan="4">
                                                <table width="100%" id='tbl<%# Eval("siteOffId")%>' cellpadding="0" cellspacing="0"
                                                    style="font-size: 8pt; font-family: Verdana; text-align: justify;" border="1"
                                                    bordercolor="black">
                                                    <tr>
                                                        <td width="100%">
                                                            <asp:GridView ID="GVwarehouse" runat="server" AutoGenerateColumns="false" DataKeyNames="warehouseId"
                                                                Width="100%" RowStyle-BackColor="blanchedAlmond" AlternatingRowStyle-BackColor="white">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SNo">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="Warehouse Name" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <%#Eval("wareHouseLText")%>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="30%" HorizontalAlign="left" />
                                                                    </asp:TemplateField>
                                                                  
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <center>
                                                                        No Data Available</center>
                                                                </EmptyDataTemplate>
                                                                <HeaderStyle HorizontalAlign="center" />
                                                                <HeaderStyle BackColor="BURLYWOOD" HorizontalAlign="center" />
                                                                <RowStyle Font-Names="Segoe UI" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <center>
                                    No Data Available</center>
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <HeaderStyle BackColor="DarkSalmon" HorizontalAlign="center" />
                                    <RowStyle Font-Names="Segoe UI" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMessage" runat="server" ForeColor="red" Visible="false" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>


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

