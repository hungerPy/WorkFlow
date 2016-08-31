<%@ Page Language="VB" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="MasterPage.master" MaintainScrollPositionOnPostback="true" CodeFile="TicketWorkAssign.aspx.vb" Inherits="TicketWorkAssign" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>Untitled Page</title>
    <script src="jvalidation/jquery.min.js"></script>
    <script src="jvalidation/jquery.validationEngine-en.js"></script>
    <script src="jvalidation/jquery.validationEngine.js"></script>
    <link href="css/ValidationEngine.css" rel="stylesheet" />
    <link href="css/red.css" rel="stylesheet" />
    <%-- <link href="css/style.css" rel="stylesheet" />--%>
    <%--<script language="javascript" src="functions.js" type="text/javascript"></script>--%>
    <link rel="icon" href="images/hslogo.jpg" type="image/x-icon" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">


    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <script type="text/javascript">
        function validateCheckBoxes() {
            var isValid = false;
            var gridView = document.getElementById('<%= Gv1.ClientID %>');
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            alert("Please select atleast one checkbox");
            return false;
        }
    </script>
    <script>
        function checkKey() {
            if (window.event.keyCode != 9)
                return false;
        }
        var selDiv = "";
        document.addEventListener("DOMContentLoaded", init, false);
        function init() {
            document.querySelector('#files').addEventListener('change', handleFileSelect, false);
            selDiv = document.querySelector("#selectedFiles");
        }
        function handleFileSelect(e) {
            if (!e.target.files || !window.FileReader) return;
            selDiv.innerHTML = "";
            var files = e.target.files;
            var filesArr = Array.prototype.slice.call(files);
            filesArr.forEach(function (f) {
                if (!f.type.match("image.*")) {
                    return;
                }

                var reader = new FileReader();
                reader.onload = function (e) {
                    var html = "<img src=\"" + e.target.result + "\">" + f.name + "<br clear=\"left\"/>";
                    selDiv.innerHTML += html;
                }
                reader.readAsDataURL(f);
            });
        }
    </script>
    <script type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode != 45 && (charCode != 46 || $(this).val().indexOf('.') != -1) && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>


    <script type="text/javascript">
        $(function () {
            $('[id*=btnassigned]').bind("click", function () {
                $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
            });
        });
        function drp1(field, rules, i, options) {
            if ($('[id$=dremp] option:selected').val() == "0") {
                return "This Feild Required."
            }
        }
    </script>

    <div class="span14">
        <div class="widget-content container">
            <div class="widget-box">
                <div class="widget-header">
                    <i class="icon-pencil"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b> 
 Ticket Work Assignment</b>  Date :
            <asp:Label ID="lbldate" runat="server" Text="Label" Font-Size="10pt" Font-Bold="True"></asp:Label>
                    |Day :
            <asp:Label ID="lblday" runat="server" Text="Label" Font-Size="11pt" Font-Bold="True"></asp:Label>
                </div>


                <table class="table" style="width: 100%">
                    <tr>
                        <td>&nbsp;Select
                Client</td>
                        <td colspan="3">
                            <asp:DropDownList ID="drpclient" runat="server" CssClass="btn btn-default dropdown-toggle" Width="60%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;Order By</td>
                        <td>
                            <asp:DropDownList ID="drporderby" runat="server" CssClass="dropdown1">
                                <asp:ListItem Value="1">Ticket Id</asp:ListItem>
                                <asp:ListItem Value="2">Client</asp:ListItem>
                                <asp:ListItem Value="3">Assigned Date</asp:ListItem>
                            </asp:DropDownList></td>


                        <td>&nbsp;Record Per Page</td>

                        <td>
                            <asp:DropDownList ID="drprecords" runat="server" CssClass="dropdown1">
                            </asp:DropDownList></td>

                    </tr>

                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnsubmit" runat="server" Text="Show" class="btn btn-primary" />
                        </td>

                    </tr>
                </table>

                <table style="width: 100%; text-transform: none;">
                    <tr>
                        <td colspan="4">

                            <asp:GridView ID="Gv1" runat="server" Width="100%" DataKeyNames="tsid" AutoGenerateColumns="False" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right" CssClass="table table-striped table-bordered" Font-Names="Times New Roman" Font-Size="15px" AllowPaging="True">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="gridchk " />
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("tid") %><br />
                                            (<%#Eval("adt1")%>)
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                        <ItemTemplate>
                                            <%#Eval("client")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>



                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                        <ItemTemplate>
                                            <%#Eval("project") %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                        <ItemTemplate>
                                            <%#Eval("description") %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("remark")%>
                                        </ItemTemplate>
                                        <%--<ItemStyle Width="15%" />--%>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtduration" runat="server" onkeypress="return isNumberKey(event)" onkeydown="return checkKey()" Width="50" placeholder="days"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txttargetdate" onpaste="return false;" Width="100px" placeholder="dd-mm-yyyy" OnTextChanged="txttargetdate_TextChanged" AutoPostBack="true" onkeydown="return checkKey()" oncut="return false;" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" CssClass="red" Enabled="True" TargetControlID="txttargetdate" PopupButtonID="Image1" Format="dd-MMM-yyyy" DaysModeTitleFormat="dd-MMM-yyyy">
                                            </asp:CalendarExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Width="140" Style="text-transform: capitalize" Text='<%#Eval("premark")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tkt Generated By & <br/> Department">
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("dirname")%><br />
                                            <%#Eval("dname")%>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("tsid") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                                <RowStyle />
                                <PagerStyle />
                                <AlternatingRowStyle CssClass="alternate-row" />
                            </asp:GridView>

                        </td>
                    </tr>



                    <tr>
                        <td style="height: 26px">&nbsp;&nbsp;Select Department &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="drpdivision" runat="server" AutoPostBack="true"></asp:DropDownList>
                            <br />
                            <br />
                        </td>

                        <td style="height: 26px">&nbsp;Employee Name 
                            <br />
                            <br />
                        </td>
                        <td style="height: 26px">
                            <asp:DropDownList ID="dremp" runat="server" ToolTip="Employee Name" CssClass="dropdown1 , validate[required,funcCall[drp1[]]]">
                            </asp:DropDownList>


                        </td>

                        <td>
                            <asp:Button ID="btnassigned" runat="server" Text="Assign Ticket" class="btn btn-primary" OnClientClick="javascript:validateCheckBoxes()" />
                            <asp:Button ID="btnremark" runat="server" Text="Update Remark" class="btn btn-primary" Width="150px" OnClientClick="javascript:validateCheckBoxes()" />
                            <br />
                            <br />
                        </td>

                    </tr>




                    <tr>


                        <td colspan="4">
                            <div class="widget-header">
                                <i class="icon-pencil"></i>&nbsp;&nbsp;<b>Employee Work Status</b>
                            </div>

                            <asp:GridView ID="gvemp" runat="server" Width="100%" AutoGenerateColumns="False" Font-Names="Times New Roman" Font-Size="15px" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right" CssClass="table table-striped table-bordered" ShowFooter="True">
                                <FooterStyle HorizontalAlign="Center" />
                                <Columns>



                                    <asp:TemplateField HeaderText="Employee Name">
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                        <ItemTemplate>
                                            <%#Eval("dirname")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Pending">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("pending") %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="In Progress">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("inprogress")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bug">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("bug")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Speak">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("speak")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Testing">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("testing")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Hold by Client">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("hold1")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Hold by Kamtech" Visible="false">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("hold2")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Completed">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("completed")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>


                                </Columns>
                                <AlternatingRowStyle CssClass="alternate-row" />
                                <RowStyle />
                                <HeaderStyle HorizontalAlign="center" />
                                <EmptyDataTemplate>
                                    No Data...!!!
                                </EmptyDataTemplate>
                            </asp:GridView>

                            <div class="widget-header">
                                <i class="icon-pencil">&nbsp;&nbsp;</i><b>Company Work Status</b>
                            </div>
                            <asp:GridView ID="gvcmp" runat="server" Width="100%" AutoGenerateColumns="False" Font-Names="Times New Roman" Font-Size="15px" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right" CssClass="table table-striped table-bordered" ShowFooter="True">
                                <FooterStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                        <ItemTemplate>
                                            <%#Eval("companyname")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Pending">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("pending") %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="In Progress">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("inprogress")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bug">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("bug")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Speak">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("speak")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Testing">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("testing")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Hold by Client">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("hold1")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Hold by Kamtech" Visible="false">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("hold2")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Completed">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemTemplate>
                                            <%#Eval("completed")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>


                                </Columns>
                                <RowStyle />
                                <AlternatingRowStyle CssClass="alternate-row" />
                                <HeaderStyle HorizontalAlign="center" />

                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                </table>
                <div>
                    <asp:Panel ID="pnldummytkt" runat="server" Visible="false" Style="width: 100%;">

                        <asp:Panel ID="pnlhead" runat="server" Visible="false">
                            <div style="text-align: right; padding-right:3px">
                                Date:
                                <asp:Label ID="lblemaildate" runat="server"></asp:Label>

                            </div>
                            <table style="width: 100%">
                                <tr style="font: bold;">
                                    <td style="text-align: left; width: 83%">To,
                                        <br />

                                        <asp:Label ID="lblempname" Style="font-size: 14px; font-family: Cambria;" runat="server" Font-Size="14px"></asp:Label><br />
                                        <asp:Label ID="lblempdesignation" Style="font-size: 14px; font-family: Cambria;" runat="server" Font-Size="14px"></asp:Label>
                                       <br />
                                        <asp:Label ID="lblcompanyname" Style="font-size: 14px; font-family: Cambria;" runat="server" Font-Size="14px"></asp:Label><br />
                                        <br />
                                        <asp:Label ID="Label1" runat="server" Style="font-size: 14px; font-family: Cambria;" Font-Size="14px" Text="Dear Sir /Madam,"></asp:Label><br />
                                        <br />
                                    </td>
                                    <td style="padding-left:5px;">
                                        <asp:Label ID="lblalt" Style="font-size: 14px; font-family: Cambria;" runat="server" Text="Work Allotment" Font-Bold="true" Font-Size="18px"></asp:Label>
                                    </td>
                                </tr>

                            </table>
                            <table style="width:100%">
                                <tr>
                                    <td style="width:100%">
                                         <span style="font-size: 14px; font-family: Cambria;">Company Authority has assigned following responsibility to you through following ticket/s :</span>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:GridView ID="gvdummy" runat="server" Width="100%" DataKeyNames="tsid" font-family="Cambria"
                            AutoGenerateColumns="False" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right" Font-Size="14px">
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="4%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Ticket No. & <br/>Date">
                                    <ItemTemplate>
                                        &nbsp;Tkt <%#Eval("tid")%>,<br />
                                        &nbsp;<%#Eval("adt1")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"   />
                                    <HeaderStyle Font-Names="Cambria" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project/<br/>Client/Company Name">
                                    <ItemTemplate>
                                        &nbsp;<%#Eval("Project")%><br />&nbsp;<%#Eval("client")%>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Names="Cambria" />

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Assignment/<br>Activity<br/>Details">
                                    <ItemTemplate>
                                        &nbsp;<%#Eval("description")%>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Names="Cambria" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate>
                                        &nbsp;<%#Eval("remark")%>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Names="Cambria" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Duration">
                                    <ItemTemplate>
                                        &nbsp;<%#Eval("timeduration")%>&nbsp;days    
                                    </ItemTemplate>
                                    <HeaderStyle Font-Names="Cambria" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Priority">
                                    <ItemTemplate>
                                        &nbsp;<%#Eval("premark")%>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Names="Cambria" />

                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>



                        <asp:Panel ID="pnlfooter" runat="server" Visible="false">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 90%; text-align: left; padding-left: 2px;">
                                        <span style="font-size: 14px; font-family: Cambria;">You are requested to complete the assignments or responsibilities as above and contact us for any further clarification.<br />
                                        </span>
                                        <br />
                                        <span style="font-size: 14px; font-family: Cambria;">Please note that status of the work progress will be regularly update in the Workflow Management System by yourself.
                                        </span>
                                        <br />
                                        <br />
                                        <br />
                                     <span style="font-size: 14px; font-family: Cambria;">  Thanking You!</span>
                                                 <br />
                                        <br />
                                        <br />
                                        <br />
                                        <asp:Label ID="lblauthorityname" Style="font-size: 14px; font-family: Cambria;" runat="server" Font-Size="14px"></asp:Label><br />
                                        <asp:Label ID="lblauthoritydesignation" Style="font-size: 14px; font-family: Cambria;" runat="server" Font-Size="14px"></asp:Label><br />
                                        <asp:Label ID="lblcompname" Style="font-size: 14px; font-family: Cambria;" runat="server" Font-Size="14px"></asp:Label><br />

                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        Note: It is an authentic system generated information.
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

