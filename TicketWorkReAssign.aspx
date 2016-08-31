<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="MasterPage.master" CodeFile="TicketWorkReAssign.aspx.vb" Inherits="Admin_TicketWorkReAssign" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>Untitled Page</title>
    <link rel="icon" href="images/hslogo.jpg" type="image/x-icon" />

    <script src="functions.js" type="text/javascript"></script>
    <link href="css/ValidationEngine.css" rel="stylesheet" />
    <link href="css/red.css" rel="stylesheet" />
    <link href="css/formStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="jvalidation/jquery.min.js"></script>
    <script type="text/javascript" src="jvalidation/jquery.validationEngine-en.js"
        charset="utf-8"></script>
    <script type="text/javascript" src="jvalidation/jquery.validationEngine.js"
        charset="utf-8"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

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

        function checkKey() {
            if (window.event.keyCode != 9)
                return false;
        }
    </script>

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
        function DateFormat(field, rules, i, options) {
            var regex = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
            if (!regex.test(field.val())) {
                return "Please enter date in dd/MM/yyyy format."
            }
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
    <div class="span14">
        <div class="widget-box">
            <div class="widget-header">
                <b>&nbsp;&nbsp;&nbsp;

           
Ticket Work Re-Assignment </b>

                <asp:Label ID="lbldate" runat="server" Text="Label" Font-Size="10pt" Font-Bold="True"></asp:Label>

                <asp:Label ID="lblday" runat="server" Text="Label" Font-Size="11pt" Font-Bold="True"></asp:Label>
            </div>
            <div class="widget-content">

                <table width="100%" id="tblForm" class="table">


                    <tr>
                        <td width="20%">&nbsp;Select
                Client</td>
                        <td width="30%">
                            <asp:DropDownList ID="drpclient" runat="server" CssClass="dropdown1">
                            </asp:DropDownList>
                        </td>
                        <td width="20%" style="height: 26px">&nbsp;Employee Status
                        </td>
                        <td style="height: 26px">
                            <asp:DropDownList ID="drpempstatus" runat="server" CssClass="dropdown1">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>

                        <td>Select Department</td>
                        <td> <asp:DropDownList ID="drpdivision" runat="server" AutoPostBack="true" ></asp:DropDownList> </td>

                        <td style="height: 26px">&nbsp;Employee Name
                        </td>
                        <td style="height: 26px">
                            <asp:DropDownList ID="drpemp1" runat="server" ToolTip="Employee Name" AutoPostBack="True" CssClass="dropdown1 ,validate[required,funcCall[drp1[]]]">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 20%">Order By</td>
                        <td>
                            <asp:DropDownList ID="drporderby" runat="server" CssClass="dropdown1">
                                <asp:ListItem Value="1">Ticket Id</asp:ListItem>
                                <asp:ListItem Value="2">Employee</asp:ListItem>
                                <asp:ListItem Value="3">Client</asp:ListItem>
                                <asp:ListItem Value="4">Assigned Date</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%">&nbsp;Record Per Page</td>
                        <td>
                            <asp:DropDownList ID="drprecords" runat="server" CssClass="dropdown1">
                            </asp:DropDownList>
                        </td>
                    </tr>


                    <tr>
                        <td colspan="4" style="text-align: center;">

                            <asp:Button ID="btnsubmit" runat="server" Text="Show" class="btn btn-primary" />
                        </td>
                    </tr>
            </div>
        </div>
    </div>
    <tr>

        <td colspan="4">

            <asp:GridView ID="Gv1" runat="server" Width="100%" DataKeyNames="tsid" AutoGenerateColumns="False" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right"
                CssClass="table table-striped table-bordered" Font-Names="Times New Roman" Font-Size="15px" AllowPaging="True">
                <Columns>
                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="gridchk " />
                        <ItemTemplate>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Left" Width="6%" />
                        <ItemTemplate>
                            <asp:Label ID="lblid" runat="server" Text='<%#Eval("tid")%>' Width="50%" Style="display: none;"></asp:Label>
                            <br />
                            (<%#Eval("adt1")%>)
                            <br />
                            <%#Eval("client")%><br />
                            <%#Eval("project") %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemTemplate>
                            <%#Eval("description") %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Left" Width="8%" />
                        <ItemTemplate>
                            <%#Eval("remark")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Width="90%" Text='<%#Eval("premark")%>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="This Feild Required." ControlToValidate="txtremark" ForeColor="Red" ValidationGroup="update"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                        <ItemStyle Width="15%" />
                    </asp:TemplateField>


                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                        <ItemTemplate>
                            <%#Eval("adt2")%><br />
                            <asp:Label ID="lblassign" runat="server" Text='<%#Eval("Assignto")%>' Width="40%"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="12%" />
                    </asp:TemplateField>


                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                        <ItemTemplate>
                            <b>Prev. allot time:</b>
                            <asp:Label ID="lblduration" runat="server" Text='<%#Eval("timeduration")%>' Width="40%"></asp:Label><br />

                            <b>Working time: </b>
                            <asp:Label ID="lbltime" runat="server" Text='<%#Eval("totaltime")%>' Width="70%"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="12%" />
                    </asp:TemplateField>


                    <asp:TemplateField> 
                        <HeaderStyle HorizontalAlign="Left" />    
                        <ItemTemplate>
                            <asp:TextBox ID="txtduration" runat="server" Width="80%" onkeypress="return isNumberKey(event)" placeholder="day"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="8%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Targetdate">
                        <ItemTemplate>
                            <asp:TextBox ID="txttargetdate" placeholder="dd-mm-yyyy" Width="90%" onkeydown="return checkKey()" AutoPostBack="true" OnTextChanged="txttargetdate_TextChanged"  runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" CssClass="red" Enabled="True" TargetControlID="txttargetdate" PopupButtonID="Image1" Format="dd-MMM-yyyy" DaysModeTitleFormat="dd-MMM-yyyy">
                            </asp:CalendarExtender>
                        </ItemTemplate>
                        <ItemStyle Width="15%" />
                    </asp:TemplateField>


                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                        <ItemTemplate>
                            <asp:Label ID="lblreport" runat="server" Text=' <%#Eval("Message")%>'></asp:Label>
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
                <RowStyle VerticalAlign="Top" />
                <PagerStyle />
                <AlternatingRowStyle CssClass="alternate-row" />
            </asp:GridView>



            <%--</div>--%> 
        </td>
    </tr>



    <tr>
        <td width="20%" style="height: 26px">
          Select Department 
           <asp:DropDownList ID="drpdivisionall" runat="server" AutoPostBack="true"></asp:DropDownList>
                            
        </td>
        <td width="30%" style="height: 26px">
        &nbsp;Employee Name
            <asp:DropDownList ID="dremp" runat="server" ToolTip="Employee Name" CssClass="dropdown1 ">
            </asp:DropDownList>

        </td>

        <td colspan="2" style="height: 26px">
            <asp:Button ID="btnassigned" runat="server" Text="Re-Assign Ticket" Width="182px" class="btn btn-primary" OnClientClick="javascript:validateCheckBoxes()" />
            <asp:Button ID="btnremark" runat="server" Text="Update Remark" class="btn btn-primary" Width="215px" OnClientClick="javascript:validateCheckBoxes()" />
        </td>

    </tr>
    <tr >

        <td width="20%" style="height: 26px">&nbsp;</td>
        <td width="30%" style="height: 26px"></td>
    </tr>

    <tr >
        <td width="20%" style="height: 26px">&nbsp;<%--Set Priority--%>
        </td>
        <td width="30%" style="height: 26px">
            <asp:DropDownList ID="drppr" runat="server" ToolTip="Priority" Visible="false" CssClass="dropdown1">
            </asp:DropDownList>

        </td>

        <td colspan="2" style="height: 26px">
            <asp:Button ID="btnpr" runat="server" Text="Set Priority" Width="126px" Visible="false" class="btn btn-primary" />

        </td>

    </tr>
    </table>
    
             
</asp:Content>
