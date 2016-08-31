<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TicketSummary.aspx.vb" MasterPageFile="MasterPage.master" Inherits="TicketSummary" EnableEventValidation="False" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link rel="icon" href="images/hslogo.jpg" type="image/x-icon" />
    <%--<link href="css/component.css" rel="stylesheet" />--%>
    <%--<link href="css/bootstrap-min2.css" rel="stylesheet" />--%>
    <%--<link href="css/font-awsom.css" rel="stylesheet" />--%>
    <link href="css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="css/ractangle.css" rel="stylesheet" />

    <title>Untitled Page</title>
    <style>
        .rect {
            height: 80px;
            border: 2px solid #000000;
            padding: 20px;
            background: #91cddd;
            min-width: 150px;
            max-width: 180px;
            margin: 50px 20px;
            text-align: center;
        }
    </style>

    <script type="text/javascript">
        function PrintPanel() {
            var header = document.getElementById("<%=divheader.ClientID %>");
            var panel = document.getElementById("<%=WorkStatusSummary.ClientID %>");
            var footer = document.getElementById("<%=divfooter.ClientID %>");
            $("#btnsummary").hide();
          
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');

            printWindow.document.write(header.innerHTML);
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write(footer.innerHTML);

            printWindow.document.write('</body></html>');
            printWindow.document.close();
            $("#btnsummary").show();
            setTimeout(function () {
                printWindow.print();
            }, 300);
            return false;

            
        }



        function PrintPanel2() {
            var header = document.getElementById("<%=divheader.ClientID %>");
            var panel = document.getElementById("<%=DailyInprogressWork.ClientID %>");
            var footer = document.getElementById("<%=divfooter.ClientID %>");
            var reportpdate = document.getElementById("<%=lblprintdate.ClientID %>");
            $("#btninprocess").hide();
            
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(header.innerHTML);
          
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write(footer.innerHTML);
            
            printWindow.document.write('</body></html>');

            printWindow.document.close();
            $("#btninprocess").show();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        } 

        function PrintPanel3() {
            var header = document.getElementById("<%=divheader.ClientID %>");
            var panel = document.getElementById("<%=WorkStatusEmployeeWise.ClientID%>");
            var footer = document.getElementById("<%=divfooter.ClientID %>");
            $("#btnempwise").hide();
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            
            printWindow.document.write(header.innerHTML);
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write(footer.innerHTML);

            printWindow.document.write('</body></html>');
            printWindow.document.close();
            $("#btnempwise").show();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }

        function PrintPanel4() {
            var header = document.getElementById("<%=divheader.ClientID %>");
            var panel = document.getElementById("<%=CompanyWorkStatus.ClientID%>");
            var footer = document.getElementById("<%=divfooter.ClientID %>");
            $("#btnwstatus").hide();
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');


            printWindow.document.write(header.innerHTML);
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write(footer.innerHTML);

            printWindow.document.write('</body></html>');
            printWindow.document.close();
            $("#btnwstatus").show();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }

        function PrintPanelprintall() {
            var header = document.getElementById("<%=divheader.ClientID %>");
            var panel = document.getElementById("<%=printall.ClientID%>");
            var footer = document.getElementById("<%=divfooter.ClientID %>");
            $("#btnallreport").hide();
            $("#btnsummary").hide();
            $("#btninprocess").hide();
            $("#btnempwise").hide();
            $("#btnwstatus").hide();
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');

            printWindow.document.write(header.innerHTML);
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write(footer.innerHTML);

            printWindow.document.write('</body></html>');
            printWindow.document.close();
            $("#btnallreport").show();
            $("#btnsummary").show();
            $("#btninprocess").show();
            $("#btnempwise").show();
            $("#btnwstatus").show();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>

    
    


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">

    <asp:Panel ID="pnldata" runat="server" Width="100%" Visible="true">
        <div class="span14">
            <div class="widget-box">
                <div class="widget widget-header" style="text-align: right;">
                    <span style="text-align: center; font-size: 14px;">
                        <h3>Work Status Dash Board</h3>
                    </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <i class="icon-calendar"></i>&nbsp;&nbsp;
                  Date:&nbsp;<asp:Label ID="lbldate" runat="server" Text="Label" Font-Size="10pt" Font-Bold="True"></asp:Label>&nbsp;&nbsp;
                    Day:&nbsp;<asp:Label ID="lblday" runat="server" Text="Label" Font-Size="11pt" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </div>


                <div class="widget " style="text-align: left; height: 150px;">
                    <div class="shortcuts">
                        <asp:LinkButton CssClass="shortcut" runat="server" ID="lnkdailyinprogresswork" OnClick="lnkdailyinprogresswork_Click">
                         <img src="icons/dailyinprogress.png" /><span class="shortcut-label"><b> Daily In <br />Progress Work </b></span> </asp:LinkButton>

                        <asp:LinkButton CssClass="shortcut" runat="server" ID="lnkworkstatus" OnClick="lnkworkstatus_Click">
                         <img src="icons/workstatussummary.png" /><span class="shortcut-label"><b> Work Status<br />Summary</b></span> </asp:LinkButton>

                        <asp:LinkButton CssClass="shortcut" runat="server" ID="lnkworkstatusempwise" OnClick="lnkworkstatusempwise_Click">
                         <img src="icons/workstatusempwise.png" /><span class="shortcut-label"><b> Work Status<br />Employee Wise</b></span> </asp:LinkButton>


                        <asp:LinkButton CssClass="shortcut" runat="server" ID="lnkcompanyworkstatus" OnClick="lnkcompanyworkstatus_Click">
                         <img src="icons/companywork.png" /><span class="shortcut-label"><b>Company wise Work <br />Status</b></span> </asp:LinkButton>
                        <br />

                    </div>
                </div>
                <div>
                    <table style="width: 96%">
                        <tr>
                            <td style="width: 60%; text-align: right;">
                                <asp:LinkButton ID="inkshowallwork" runat="server" Text="Show All work Reports" OnClick="inkshowallwork_Click" Font-Bold="false" Font-Underline="false" Font-Size="Large"></asp:LinkButton>
                            </td>
                            <td style="width: 40%; text-align: right;">
                                <a class="btn blue big hidden-print" id="btnallreport" onclick="return PrintPanelprintall();">Print All Reports<i class="icon-print icon-big"></i></a>
                            </td>
                        </tr>
                    </table>
                </div>

                <div id="divheader" runat="server" style="display: none;">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 100%; text-align: center;">
                                <asp:Label ID="lblcompanyname" runat="server" Font-Bold="true" Font-Names="Cambria" Font-Size="18px"></asp:Label><br />
                                <asp:Label ID="lbljaipur" runat="server" Text="Jaipur" Font-Names="Cambria" Font-Size="18px"></asp:Label><br />
                                <br />
                                <asp:Label ID="Label1" runat="server" Text="Head Office" Font-Names="Cambria" Font-Size="18px"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;"></td>
                        </tr>
                    </table>
                </div>

                <div id="printall" runat="server">
                    <div class="widget container" id="WorkStatusSummary" runat="server" visible="false">
                        <table id="tbllblcompname" class="table" style="padding-top: 0em;" width="100%">
                            <tr>
                                <td style="padding-top: 0em;">
                                    <asp:Label ID="lblcompname" runat="server" Visible="false" Font-Bold="true" Font-Names="Cambria" Font-Size="13px"></asp:Label>
                                    &nbsp;<b style="font-family:Cambria;"> Work Status Summary</b>

                                    <span style="text-align: right; padding-left: 650px; padding-top: 0em;"><a class="btn blue big hidden-print" id="btnsummary" onclick="return PrintPanel();">Print <i class="icon-print icon-big"></i></a>
                                     
                                    </span>
                                </td>

                            </tr>

                            <tr>

                                <td style="vertical-align: top; padding-top: 0em;" width="80%">
                                    <div id="divgvcompanystatus" runat="server" class="widget-content" style="text-align: center;" visible="true">

                                        <asp:GridView ID="gvcompanystatus" runat="server" AllowPaging="false" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" Font-Names="Cambria" Font-Size="14px" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Center" PageSize="5" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Total&lt;br/&gt; Pending&lt;br/&gt; Tickets" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lblticket" runat="server" Font-Bold="true"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Not&lt;br/&gt;Assigned">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnotassigned" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Pending&lt;br/&gt; Points">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpending" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="In&lt;br/&gt;Pogress&lt;br/&gt; Points">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblprogress" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bug">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbug" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Speak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblspeak" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Testing&lt;br/&gt;Points">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltesting" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hold&lt;br/&gt;By Clinet&lt;br/&gt;Points">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblhold" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hold&lt;br/&gt;By Kamtech&lt;br/&gt;Points" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblhold1" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total&lt;br/&gt;Points">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltotal" runat="server" Font-Bold="true"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; padding-top: 0em;" width="80%">&nbsp;</td>
                            </tr>
                        </table>
                    </div>


                    <div class="widget-content" style="text-align: left;" runat="server" id="DailyInprogressWork" visible="true">
                        <span style="font-size: 14px; font: bold;"><b style="font-family:Cambria;">Daily In process Work</b></span>&nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton ID="imaginprocess" runat="server" Height="27px" ImageUrl="~/Images/on.png" Width="30px" />
                        &nbsp;<b> On</b>
                        <span style="text-align: right; padding-left: 700px;" id="btninprocess">
                           <span style="display:none;" id="reportprintdate">Report Print Date <asp:Label ID="lblprintdate" runat="server" Font-Size="12px" Font-Names="Cambria" ></asp:Label></span>
                            <a class="btn blue big" onclick="return PrintPanel2();">Print <i class="icon-print icon-big"></i></a></span>

                        <table id="Table4" class="table" style="padding-top: 0em;" width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gv1process" runat="server" AllowPaging="false" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" Font-Names="Cambria" Font-Size="14px" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right" PageSize="5" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <HeaderStyle CssClass="gridchk " HorizontalAlign="center" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" Width="3%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="&nbsp;Assigned to &lt;br&gt;&nbsp;and Date">
                                                <HeaderStyle Font-Bold="true" Font-Size="12px" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    &nbsp;<asp:Label ID="lblname1" runat="server" Text='<%#Eval("Developer")%>'></asp:Label>
                                                    <br />
                                                    &nbsp;<%#Eval("adt2")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="&nbsp;Client &amp; Project &lt;br/&gt;&nbsp;TicketNo., Date">
                                                <HeaderStyle Font-Bold="true" Font-Size="12px" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    &nbsp;<%#Eval("client")%>,<br />
                                                    &nbsp;<%#Eval("project")%><br />&nbsp;Tkt&nbsp;<%#Eval("tid")%>, <%#Eval("adt1")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="12%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="&nbsp;Job / Description">
                                                <HeaderStyle Font-Bold="true" Font-Size="12px" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    &nbsp;<%#Eval("Description")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="17%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                <HeaderStyle Font-Bold="true" Font-Size="12px" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Image ID="imgstatus" runat="server" Height="30px" ImageUrl="~/Images/on.png" Width="30px" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="&nbsp;Time Details">
                                                <HeaderStyle Font-Bold="true" Font-Size="12px" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <b style="float: left">&nbsp;Job Time:
                                                &nbsp;<asp:Label ID="lblduration" runat="server" Text='<%#Eval("timeduration")%>'></asp:Label>
                                                        <br />
                                                        <b style="float: left">&nbsp;Consumed Time:<br />
                                                            &nbsp;<asp:Label ID="txtttime" runat="server" Text='<%#Eval("TotalTime")%>'></asp:Label>
                                                            <%-- <b style="color: green; float: left">Start Time:</b>--%>
                                                      &nbsp;<asp:Label ID="lblstartt" runat="server" Visible="false" Style="float: left; color: green" Text='<%#Eval("starttime")%>' Width="100%"></asp:Label>
                                                        </b></b>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                                            </asp:TemplateField>
                                           
                                                  
                                           
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <center>
                                            No Data Available</center>
                                        </EmptyDataTemplate>
                                        
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; padding-top: 0em;" width="80%">&nbsp;</td>
                            </tr>
                        </table>
                    </div>


                    <div class="widget-content" runat="server" id="WorkStatusEmployeeWise" visible="false">
                        <div class="widget-content" style="text-align: left;">
                            <b style="font-family:Cambria;">Work Status: Employee Wise</b>
                            <span style="text-align: right; padding-left: 700px; padding-top: 0em;"><a class="btn blue big hidden-print" id="btnempwise" onclick="return PrintPanel3();">Print <i class="icon-print icon-big"></i></a></span>

                        </div>
                        <table id="Table2" class="table" width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvemp" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" Font-Names="Cambria" Font-Size="14px" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right" ShowFooter="True" Width="100%">
                                        <FooterStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField ControlStyle-Font-Size="13px" HeaderText="Employee Name">
                                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Font-Size="13px" HeaderText="Pending/ In progress">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bug">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Speak">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Testing">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hold by Client">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hold by Kamtech" Visible="false">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="completed">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle VerticalAlign="Top" />
                                    </asp:GridView>
                                </td>
                            </tr>

                        </table>
                    </div>


                    <div class="widget-content" id="CompanyWorkStatus" runat="server" visible="false">
                        <div class="widget-content" style="text-align: left;">
                            <b style="font-family:Cambria;">Company Work Status</b>
                            <span style="text-align: right; padding-left: 750px; padding-top: 0em;"><a class="btn blue big hidden-print" id="btnwstatus" onclick="return PrintPanel4();">Print <i class="icon-print icon-big"></i></a></span>

                        </div>
                        <table id="Table2" class="table" width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="Gvcmp" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" Font-Names="Cambria" Font-Size="14px" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right" ShowFooter="True" Width="100%">
                                        <FooterStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Client Name">
                                                <HeaderStyle HorizontalAlign="Left" Width="35%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pending">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="In Progress">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bug">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="speak">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Testing">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hold by Client">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hold by Kamtech" Visible="false">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Completed">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total">
                                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle />
                                        <HeaderStyle HorizontalAlign="center" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                  
                    <div id="divfooter" runat="server" style="display: none;">
                        
                        <span style="font-family: Cambria;font-size:9px;">Generated By: Work Flow Management System</span>

                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <div class="widget-content ">
        <table id="td1" width="100%">
            <tr>
                <td colspan="2" align="center" style="display: none;">
                    <asp:Button ID="btnmail" runat="server" Text="Send Mail" Visible="false" CssClass="btn btn-success " />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
