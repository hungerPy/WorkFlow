<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TicketEmpWork.aspx.vb" MasterPageFile="MasterPage.master" MaintainScrollPositionOnPostback="true" Inherits="Admin_TicketEmpWork" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/StyleSheet2.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="images/hslogo.jpg" type="image/x-icon" />

    <title>Untitled Page</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
    <script type="text/javascript">
        function checkRange(th, mn, mx) {
            var v;
            //if(window.event.keyCode < 48 || window.event.keyCode > 57 )
            //{
            //window.event.keyCode=0
            //return;
            //}
            v = th.value;
            if (v == "")
                v = "0"
            v = v + String.fromCharCode(window.event.keyCode)
            if (parseFloat(v) < mn || parseFloat(v) > mx)
                window.event.keyCode = 0;
        }

        function checkKey() {
            if (window.event.keyCode != 9)
                return false;
        }

        function RadioCheck(rb) {
            var gv = document.getElementById("<%=Gv1.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            if (rb.checked) {
                //If checked change color to red
                row.style.backgroundColor = "green";
            }
            else {
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "#C2D69B";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }
        function Check_Click(objRef) {
            var gv = document.getElementById("<%=Gv1.ClientID%>");
            var rbs = gv.getElementsByTagName("input");

            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;

            if (objRef.checked) {
                //If checked change color to Aqua
                row.style.backgroundColor = "red";
            }
            else {
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "#C2D69B";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != objRef) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }

    </script>

    <link href="css/ValidationEngine.css" rel="stylesheet" />
    <link href="css/formStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="jvalidation/jquery.min.js"></script>
    <script type="text/javascript" src="jvalidation/jquery.validationEngine-en.js"
        charset="utf-8"></script>
    <script type="text/javascript" src="jvalidation/jquery.validationEngine.js"
        charset="utf-8"></script>
    <script type="text/javascript">
        $(function () {
            $("#form1").validationEngine('attach', { promptPosition: "centerRight" });
        });
    </script>

    <script type="text/javascript">

        $(function () {
            $('[id*=btnsubmit]').bind("click", function () {
                $("#form1").validationEngine('attach', { promptPosition: "centerRight" });
            });
        });

        function drp(field, rules, i, options) {
            if ($('#drpstatus option:selected').val() == "0") {
                return "This Feild Required."
            }
        }
        function drp1(field, rules, i, options) {
            if ($('#drporderby option:selected').val() == "0") {
                return "This Feild Required."
            }
        }
        function drp2(field, rules, i, options) {
            if ($('#drpclient option:selected').val() == "0") {
                return "This Feild Required."
            }
        }
    </script>


    <div class="span14">
        <div class="widget-box">
            <div class="widget-header">
                <i class="icon-pencil"></i>&nbsp;&nbsp;&nbsp;&nbsp;<b>
        Employee Work Status </b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               &nbsp;&nbsp;  <i class="icon-calendar"></i>&nbsp;&nbsp;
                Date : <asp:Label ID="lbldate" runat="server" Text="Label" Font-Size="10pt" Font-Bold="True"></asp:Label>
                Day :<asp:Label ID="lblday" runat="server" Text="Label" Font-Size="11pt" Font-Bold="True"></asp:Label>
            </div>
            <div class="widget-content">
                <table id="Table1" class="table">
                    <tr>
                        <td width="15%" style="height: 24px">&nbsp;Select
                Client</td>
                        <td width="35%">
                            <asp:DropDownList ID="drpclient" runat="server" CssClass="dropdown1 validate[required,funcCall[drp2[]]">
                            </asp:DropDownList></td>
                        <td style="width: 15%">&nbsp;Select Status</td>
                        <td style="width: 35%">
                            <asp:DropDownList ID="drpstatus" runat="server" CssClass="dropdown1 ">
                            </asp:DropDownList>
                            <asp:Label ID="lblstop" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblstart" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                  
                    <tr>

                        <td style="width: 15%;">&nbsp;Order By</td>
                        <td>
                            <asp:DropDownList ID="drporderby" runat="server" CssClass="dropdown1 ">
                                <asp:ListItem Value="0">[Select]</asp:ListItem>
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
                        <td colspan="4" align="center" style="background-color: <%=db.headerBG%>">
                            <center> <asp:Button ID="btnsubmit" runat="server" Text="Show" class="btn btn-primary" /></center>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-lg-12" style="text-align: center; padding: 10px;">

                <span style="font-size: 20px; font: bold">Welcome :</span>
                <asp:Label ID="lblempname" Font-Bold="false" Font-Size="18px" runat="server"></asp:Label>

                <br />
                <span style="font-size: 15px; font: bold">Department :</span>
                <asp:Label ID="lbldepartment" Font-Bold="false" Font-Size="12px" runat="server"></asp:Label>
            </div>
            <div class="col-lg-12" style="text-align: left;">
                <span style="font-size: 15px; font: bold">Designation:</span><asp:Label ID="lbldesignation" runat="server" Font-Size="12px"></asp:Label>
                &nbsp; , <span style="font-size: 15px; font: bold">ID :</span>
                <asp:Label ID="lblempid" runat="server" Font-Size="12px"></asp:Label>
            </div>


            <asp:GridView ID="Gv1" runat="server" Width="100%" PagerStyle-CssClass="paging-link" 
                CssClass="table table-striped table-bordered" ShowFooter="true"  AutoGenerateColumns="False" Font-Size="12px" AllowPaging="True" PageSize="5" DataKeyNames="tsid">
                <Columns>

                    <asp:TemplateField>
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <%#Eval("client")%>,<br />
                            <%#Eval("project")%>
                            <br />
                            Tkt <%#Eval("tid")%>, (<%#Eval("adt1")%>)
                        </ItemTemplate>
                        <ItemStyle Width="18%" HorizontalAlign="Left" />
                    </asp:TemplateField>


                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <%#Eval("adt2")%>
                        </ItemTemplate>
                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                    </asp:TemplateField>


                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <%#Eval("Description")%>
                        </ItemTemplate>
                        <ItemStyle Width="15%" HorizontalAlign="Left" />
                       
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblduration" runat="server" Text='<%#Eval("timeduration")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="8%" HorizontalAlign="Center"/>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lbltargetdate" runat="server" Text='<%#Eval("targetdate")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="10%" HorizontalAlign="Left"   />
                    </asp:TemplateField>



                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:TextBox ID="txteremark" runat="server" TextMode="MultiLine" Height="80%" Width="90%" Text='<%#Eval("eremark")%>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="20%" />
                    </asp:TemplateField>

                    <asp:TemplateField Visible="false">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("empstatus")%>'></asp:Label>

                        </ItemTemplate>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <div id="left">
                                <div class='fx-block'>
                                    <div id="toggle">
                                        <div>
                                            <asp:CheckBox runat="server" ID="chkonoff" AutoPostBack="true" OnCheckedChanged="chkonoff_CheckedChanged"  />
                                            <div data-unchecked="On" data-checked="Off"></div>
                                        </div>
                                    </div>
                                </div>
                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("tsid") %>' />
                        </ItemTemplate>
                        <%--<ItemStyle Width ="15%" HorizontalAlign ="Left" />--%>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Work Time">
                        <ItemTemplate>
                            <%--<b style="float: left; color: darkblue">Est Time:</b>--%>
                            <%--<br />--%>
                            <b style="color: green; float: left">Start Time:</b>
                            <asp:Label ID="lblstartt" runat="server" Text='<%#Eval("starttime")%>' Style="float: left; color: green" Width="100%"></asp:Label>

                            <br />

                            <b style="color: red; float: left">Stop Time:</b>
                            <asp:Label ID="lblsttop" runat="server" Text='<%#Eval("stoptime")%>' Style="color: red; float: left" Width="100%"></asp:Label>
                            <br />

                            <b style="float: left">Work Time:</b>
                            <asp:Label ID="txtttime" runat="server" Text='<%#Eval("TotalTime")%>' Style="float:left"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="25%" HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Delete" Class="btn" Width="80%" ToolTip="Update work process Remark">
                            <i class="icon-comments"></i></asp:LinkButton>
                            <br /><br />
                            <asp:LinkButton ID="btncomplete" runat="server" CommandName="Update" Class="btn" Width="80%"  ToolTip="Work Send for Approval">
                                <i class="icon-check"></i></asp:LinkButton>
                        </ItemTemplate>

                        <ItemStyle Width="20%" HorizontalAlign="Center" />
                    </asp:TemplateField>


                    
                </Columns>
                
                <FooterStyle />
                <RowStyle BackColor="#F7F7DE" VerticalAlign="Top" />
                <SelectedRowStyle Font-Bold="True" />
                <PagerStyle HorizontalAlign="Right" />
                <HeaderStyle Font-Bold="True" />
                <AlternatingRowStyle />
            </asp:GridView>

            <table>

                <tr>

                    <td style="width: 70%; height: 26px;" align="right">
                        <%-- <span style=" font-size: 18px">&nbsp;Update Status</span> --%>
                        <asp:DropDownList ID="Drpsts" runat="server" ToolTip="Status" CssClass="dropdown1" Width="538px" Visible="False">
                        </asp:DropDownList>

                        <asp:Button ID="Updt" Text="Update" runat="server" Height="31px" Visible="False" class="btn btn-primary" /></td>
                </tr>
            </table>
</asp:Content>
