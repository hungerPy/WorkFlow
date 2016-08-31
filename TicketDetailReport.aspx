<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="TicketDetailReport.aspx.vb" Inherits="TicketDetailReport" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link rel="icon" href="images/hslogo.jpg" type="image/x-icon" />
    <link href="css/ValidationEngine.css" rel="stylesheet" />
    <link href="css/orange.css" rel="stylesheet" />
    <link href="css/formStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="jvalidation/jquery.validationEngine-en.js"
        charset="utf-8"></script>
    <script type="text/javascript" src="jvalidation/jquery.validationEngine.js"
        charset="utf-8"></script>
    <link href="css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>Untitled Page</title>
    <script type="text/javascript">
        $(function () {
            $('[id*=btnsubmit]').bind("click", function () {
                $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
            });
        });
        function drp1(field, rules, i, options) {
            if ($('[id$=drpemp1] option:selected').val() == "0") {
                return "This Feild Required."
            }
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $("#adminform").validationEngine('attach', { promptPosition: "topRight" });
        });
        function drp(field, rules, i, options) {
            if ($('#dremp option:selected').val() == "0") {
                return "This Feild Required."
            }
        }
    </script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=divprint.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            //printWindow.document.write('<html><head><title>DIV Contents</title>');
            //printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript">
        function checkKey() {
            if (window.event.keyCode != 9)
                return false;
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="span14">
        <div class="widget-box">
            <div class="widget-header">
                <i class="icon-pencil"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>
       Ticket Statement</b>  Date : 
            <asp:Label ID="lbldate" runat="server" Text="Label" Font-Size="10pt" Font-Bold="True"></asp:Label>

                <asp:Label ID="lblday" runat="server" Text="Label" Font-Size="11pt" Font-Bold="True"></asp:Label>
            </div>
            <div class="widget-content">

                <asp:Panel ID="pnlform" runat="server" Width="100%">
                    <table id="tblForm" class="table ">

                        <tr>
                            <td style="height: 24px">&nbsp;Select
                Client</td>
                            <td>
                                <%--  <asp:DropDownCheckBoxes ID="ddchkClient" runat="server"
                                    AddJQueryReference="True" UseButtons="True" UseSelectAllNode="True">
                                    <Style SelectBoxWidth="200" DropDownBoxBoxWidth="200" DropDownBoxBoxHeight="130" />
                                    <Texts SelectBoxCaption="Select Client" />
                                </asp:DropDownCheckBoxes>--%>
                                <asp:DropDownList ID="drpclient" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </td>

                            <td style="width: 20%; height: 24px;">&nbsp;Select Project</td>

                            <td style="width: 30%; height: 24px;">
                                <%-- <asp:DropDownCheckBoxes ID="DropDownCheckBoxes1" runat="server"
                                    AddJQueryReference="True" UseButtons="True" UseSelectAllNode="True">
                                    <Style SelectBoxWidth="200" DropDownBoxBoxWidth="200" DropDownBoxBoxHeight="130" />
                                    <Texts SelectBoxCaption="Select status" />
                                </asp:DropDownCheckBoxes>--%>
                                <asp:DropDownList ID="drpproject" runat="server" AutoPostBack="false"></asp:DropDownList>
                            </td>
                        </tr>

                        <%-- <tr>

                            <td style="height: 26px">&nbsp;Employee Name
                            </td>
                            <td class="auto-style2">
                                <asp:DropDownCheckBoxes ID="DropDownCheckBoxes3" runat="server"
                                    AddJQueryReference="True" UseButtons="True" UseSelectAllNode="True">
                                    <Style SelectBoxWidth="200" DropDownBoxBoxWidth="200" DropDownBoxBoxHeight="130" />
                                    <Texts SelectBoxCaption="Employee Status" />
                                </asp:DropDownCheckBoxes>
                            </td>


                            <td style="width: 20%">&nbsp;Employee Status</td>

                            <td style="width: 30%">
                             <asp:DropDownCheckBoxes ID="DropDownCheckBoxes2" runat="server" CssClass="dropdown1 "
                                    AddJQueryReference="True" UseButtons="True" UseSelectAllNode="True">
                                    <Style SelectBoxWidth="200" DropDownBoxBoxWidth="200" DropDownBoxBoxHeight="130" />
                                    <Texts SelectBoxCaption="Employee Status" />
                                </asp:DropDownCheckBoxes>
                            </td>

                        </tr>--%>

                        <tr>
                            <td style="height: 26px">&nbsp;&nbsp;Select Department &nbsp;&nbsp;&nbsp;
                            </td>
                            <td>

                                <asp:DropDownList ID="drpdivision" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </td>


                            <td style="height: 26px">&nbsp;Employee Name 
                            <br />
                                <br />
                            </td>
                            <td style="height: 26px">
                                <asp:DropDownList ID="dremp" runat="server" ToolTip="Employee Name"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;Date From</td>
                            <td class="auto-style3">
                                <asp:TextBox ID="TextBox1" onpaste="return false;" placeholder="dd-mm-yyyy" onkeydown="return checkKey()" oncut="return false;" runat="server" CssClass="validate[required]"></asp:TextBox>

                                <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" CssClass="orange" Enabled="True" PopupButtonID="Image1" TargetControlID="TextBox1" Format="dd-MMM-yyyy" DaysModeTitleFormat="dd-MMM-yyyy"></asp:CalendarExtender>
                            </td>
                            <td>&nbsp;To</td>
                            <td>
                                <asp:TextBox ID="TextBox2" onpaste="return false;" placeholder="dd-mm-yyyy" onkeydown="return checkKey()" oncut="return false;" runat="server" CssClass="validate[required]"></asp:TextBox>
                                <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" CssClass="orange" Enabled="True" TargetControlID="TextBox2" PopupButtonID="Image2" Format="dd-MMM-yyyy" DaysModeTitleFormat="dd-MMM-yyyy"></asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%; height: 24px;">&nbsp;Ticket Status</td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="drpstatus" runat="server"></asp:DropDownList>
                                <asp:DropDownList ID="drprecords" runat="server" Visible="false" CssClass="dropdown1">
                                </asp:DropDownList></td>

                            <%--<td style="width: 20%; height: 24px;">&nbsp;Order By</td>--%>

                            <%--  <td style="height: 24px">
                                <asp:DropDownList ID="drporderby" runat="server" CssClass="dropdown1">
                                    <asp:ListItem Value="1">Ticket Id</asp:ListItem>
                                    <asp:ListItem Value="2">Employee</asp:ListItem>
                                    <asp:ListItem Value="3">Client</asp:ListItem>
                                    <asp:ListItem Value="4">Assigned Date</asp:ListItem>
                                    <asp:ListItem Value="5">Status</asp:ListItem>
                                </asp:DropDownList></td>--%>
                        </tr>

                        <tr>
                            <td colspan="4" style="text-align: center;">
                                <asp:Button ID="btnsubmit" runat="server" Text="Show" CssClass="btn btn-primary" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
        <div id="divprint" runat="server">
            <div class="widget-content">

                <asp:Panel ID="pnldata" runat="server" Width="100%" Visible="false">
                    <div class="widget-content" style="text-align: center">
                        <h3>Indivisual Employee Wise Work Report</h3>
                    </div>
                    <br />
                    <table class="table">
                        <tr>
                            <td>&nbsp;Client</td>
                            <td style="padding-left: 4px">
                                <asp:Label ID="lblclient" runat="server"></asp:Label>
                            </td>

                            <td style="width: 20%">&nbsp;Status</td>

                            <td style="width: 30%; padding-left: 4px">
                                <asp:Label ID="lblstatus" runat="server"></asp:Label>

                            </td>
                        </tr>

                        <tr>

                            <td style="height: 26px">&nbsp;Employee Name
                            </td>
                            <td style="height: 26px; padding-left: 4px">
                                <asp:Label ID="lblemp" runat="server"></asp:Label>
                            </td>


                            <td style="width: 20%">&nbsp;Employee Status</td>

                            <td style="width: 30%; padding-left: 4px">
                                <asp:Label ID="lblempstatus" runat="server"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td style="width: 20%">&nbsp;Designation:
                            </td>
                            <td style="width: 30%; padding-left: 4px">
                                <asp:Label ID="lbldesignation" runat="server" Font-Size="12px"></asp:Label>
                            </td>

                            <%-- <td style="width: 30%; padding-left: 4px">&nbsp;ID:</td>
                        <td style="width: 30%; padding-left: 4px"> <asp:Label ID="lblcid" runat="server" Font-Size="12px" ></asp:Label></td>--%>
                        </tr>

                        <tr>
                            <td colspan="4">&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>

            </div>
            <div>
                <span style="text-align: right; padding-left: 900px; padding-top: 0em;"><a class="btn blue big hidden-print" onclick="return PrintPanel();">Print <i class="icon-print icon-big"></i></a></span>

            </div>
            <div class="widget-content" id="divstatussummary">
                <asp:GridView ID="Gv1" runat="server" Style="overflow: hidden" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right"
                    CssClass="table table-striped table-bordered lead " ShowFooter="True" Width="100%" DataKeyNames="tsid" AutoGenerateColumns="False"
                    Font-Names="Cambria" PageSize="10" Font-Size="14px" AllowPaging="True">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <HeaderStyle HorizontalAlign="Center" Width="3%" />
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="3.5%" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" Width="7%" />
                            <ItemTemplate>
                                <%#Eval("client")%>, <%#Eval("project") %>
                                <br />
                                Tkt <%#Eval("tid") %>, (<%#Eval("adt1")%>)
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="17%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Assign Date">
                            <HeaderStyle HorizontalAlign="center" Width="5%" />
                            <ItemTemplate>
                                <%#Eval("adt2")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center" Width="10%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Discription">
                            <HeaderStyle HorizontalAlign="Left" Width="15%" />
                            <ItemTemplate>
                                <%#Eval("description") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="25%" />
                        </asp:TemplateField>



                        <asp:TemplateField Visible="false">
                            <HeaderStyle HorizontalAlign="center" Width="10%" />
                            <ItemTemplate>
                                <%#IIf(Eval("status") = "Completed",Eval("assignto"),iif((Eval("empstatus") = "Testing" Or Eval("empstatus") = "Speak"), Eval("assignby"), Eval("assignto")))%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center" Width="10%" />
                        </asp:TemplateField>

                        <asp:TemplateField Visible="false">
                            <HeaderStyle HorizontalAlign="center" Width="5%" />
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" Text='<%#IIf(Eval("status") = "Pending", "Pending", IIf(Eval("status") = "Completed", "Completed", Eval("empstatus")))%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center" Width="5%" />
                        </asp:TemplateField>

                        <asp:TemplateField Visible="false">
                            <HeaderStyle HorizontalAlign="center" Width="5%" />
                            <ItemTemplate>
                                <%#Eval("cdate1")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center" Width="5%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total<br/>Job Time<br/>(In Minutes)">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblduration" runat="server" Text='<%#Eval("timeduration")%>' Width="30%"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="9%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField Visible="true" HeaderText="Target Date">
                            <HeaderStyle HorizontalAlign="center" Width="5%" />
                            <ItemTemplate>
                                <%#Eval("targetdate")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center" Width="10%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Progress Work Time" Visible="false">
                            <ItemTemplate>
                                <b style="color: green; float: left">Start Time:</b>
                                <asp:Label ID="lblstartt" runat="server" Text='<%#Eval("starttime")%>' Style="float: left; color: green" Width="100%"></asp:Label>
                                <br />
                                <b style="color: red; float: left">Stop Time:</b>
                                <asp:Label ID="lblstop" runat="server" Text='<%#Eval("stoptime")%>' Style="color: red; float: left" Width="100%"></asp:Label>
                                <br />
                            </ItemTemplate>
                            <FooterTemplate>Total Time</FooterTemplate>
                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Consumed<br/>working/ Time">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lbltime" runat="server" Text='<%#Eval("totaltime")%>' Style="display: none;"></asp:Label>
                                <asp:Label ID="txtttime" runat="server" Text='<%#Eval("TotalTime")%>' Style="float: left;"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Reason" Visible="false">
                            <HeaderStyle HorizontalAlign="center" Width="30%" />
                            <ItemTemplate>
                                <%#Eval("editremark")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="28%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Progress<br/>working status">
                            <HeaderStyle HorizontalAlign="center" Width="20%" />
                            <ItemTemplate>
                                <%#Eval("eremark")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="28%" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle VerticalAlign="Top" />
                    <PagerStyle />
                    <FooterStyle Font-Bold="True" HorizontalAlign="center" />
                </asp:GridView>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 100%; text-align: right; padding-right: 3px;">
                            <i class="icon-calendar"></i>&nbsp;&nbsp;
                Date :
                            <asp:Label ID="lbldt" runat="server" Text="Label" Font-Size="10pt" Font-Bold="True"></asp:Label>
                            Day :<asp:Label ID="lbld" runat="server" Text="Label" Font-Size="11pt" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>



                <asp:GridView ID="gvstatement" runat="server" Style="overflow: hidden" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right"
                    CssClass="table table-striped table-bordered lead " ShowFooter="True" Width="100%" DataKeyNames="tsid" AutoGenerateColumns="False"
                    Font-Names="Times New Roman" PageSize="10" Font-Size="14px" AllowPaging="false">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tkt No./<br/>Tkt Date">
                            <ItemTemplate>
                                Tkt <%#Eval("tid")%>
                                <br />
                                <%#Eval("adt1")%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Project/Client Name<br>Company Name">
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Assign By" Visible="false">
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Assignment/Activity<br/>Details">
                            <ItemTemplate>
                                <%#Eval("description")%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Duration/ <br/>Target Date">
                            <ItemTemplate>
                                <%#Eval("timeduration") %><br />
                                <%#Eval("targetdate") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Consumed <br/>Working Time">
                            <ItemTemplate>
                                <asp:Label ID="lblduration" runat="server" Text='<%#Eval("totaltime")%>'></asp:Label>
                                <br />
                                <%-- <%#Eval("totaltime") %>--%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Working Status">
                            <ItemTemplate>
                                <%#Eval("empstatus") %>
                            </ItemTemplate>
                        </asp:TemplateField>


                    </Columns>
                    <EmptyDataTemplate>
                        No Data....!!!   
                    </EmptyDataTemplate>
                    <RowStyle VerticalAlign="Top" />
                    <PagerStyle />
                    <FooterStyle Font-Bold="True" HorizontalAlign="center" />
                </asp:GridView>


            </div>
        </div>
    </div>
    <div>
    </div>
</asp:Content>
