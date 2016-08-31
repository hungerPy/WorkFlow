<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TicketUpdateStatus.aspx.vb" MaintainScrollPositionOnPostback="true" MasterPageFile="MasterPage.master" Inherits="Admin_TicketUpdateStatus" EnableEventValidation="False" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/StyleSheet.css" rel="stylesheet" type="text/css" />

    <link rel="icon" href="images/hslogo.jpg" type="image/x-icon" />

    <link href="css/ValidationEngine.css" rel="stylesheet" />

    <link href="css/formStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="jvalidation/jquery.min.js"></script>
    <script type="text/javascript" src="jvalidation/jquery.validationEngine-en.js"
        charset="utf-8"></script>
    <script type="text/javascript" src="jvalidation/jquery.validationEngine.js"
        charset="utf-8"></script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">


    <script type="text/javascript">
        $(function () {
            $('[id*=btnassigned]').bind("click", function () {
                $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
            });
        });
        function drp(field, rules, i, options) {
            if ($('[id$=rpstatus] option:selected').val() == "0") {
                return "This Feild Required."
            }
        }

    </script>
    <script type="text/javascript">

        $(function () {
            $('[id*=Button1]').bind("click", function () {
                $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
            });
        });

        function drp1(field, rules, i, options) {
            if ($('#dremp option:selected').val() == "0") {
                return "This Feild Required."
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
                <i class="icon-pencil"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>
         Work Testing And Upload </b>Date : 
            <asp:Label ID="lbldate" runat="server" Text="Label" Font-Size="10pt" Font-Bold="True"></asp:Label>

                <asp:Label ID="lblday" runat="server" Text="Label" Font-Size="11pt" Font-Bold="True"></asp:Label>
            </div>
            <div class="widget-content">
                <table width="100%" id="tblForm" class="table ">


                    <tr>
                        <td width="20%">&nbsp;Select
                Client</td>
                        <td colspan="3">
                            <asp:DropDownList ID="drpclient" runat="server" CssClass="dropdown1">
                            </asp:DropDownList></td>

                    </tr>

                    <tr>
                        <td width="20%" style="height: 26px">&nbsp;Employee Name
                        </td>
                        <td width="30%" style="height: 26px">
                            <asp:DropDownList ID="drpemp1" runat="server" ToolTip="Employee Name" CssClass="dropdown1">
                            </asp:DropDownList></td>


                        <td style="width: 20%">&nbsp;Select Status</td>

                        <td style="width: 30%">
                            <asp:DropDownList ID="drpstatus1" runat="server" CssClass="dropdown1">
                            </asp:DropDownList></td>
                    </tr>

                    <tr>

                        <td style="width: 20%">&nbsp;Order By</td>

                        <td style="width: 30%">
                            <asp:DropDownList ID="drporderby" runat="server" CssClass="dropdown1">
                                <asp:ListItem Value="1">Ticket Id</asp:ListItem>
                                <asp:ListItem Value="2">Employee</asp:ListItem>
                                <asp:ListItem Value="3">Client</asp:ListItem>
                                <asp:ListItem Value="4">Assigned Date</asp:ListItem>
                            </asp:DropDownList>

                        </td>

                        <td style="width: 20%">&nbsp;Record Per Page</td>

                        <td style="width: 30%">
                            <asp:DropDownList ID="drprecords" runat="server" CssClass="dropdown1">
                            </asp:DropDownList>
                        </td>


                    </tr>

                    <tr>
                        <td colspan="4" align="center">
                            <center>
          <asp:Button ID="btnsubmit" runat="server" Text="Show" class="btn btn-primary" /></center>
                        </td>
                    </tr>
            </div>
        </div>
    </div>

    <tr>

        <td colspan="4">

            <asp:GridView ID="Gv1" runat="server" Width="100%" DataKeyNames="tsid" PagerStyle-CssClass="paging-link"
                PagerStyle-HorizontalAlign="Right" CssClass="table table-striped table-bordered" AutoGenerateColumns="False"
                Font-Names="Times New Roman" Font-Size="15px" PageSize="10" AllowPaging="True">
                <FooterStyle />
                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="gridchk " />
                        <ItemTemplate>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tkt._No.">
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <%#Eval("tid") %><br />
                            (<%#Eval("adt1")%>)
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Client">
                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        <ItemTemplate>
                            <%#Eval("client")%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Project">
                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        <ItemTemplate>
                            <%#Eval("project") %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description">
                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        <ItemTemplate>
                            <%#Eval("description") %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <%-- <asp:TemplateField>
   <HeaderStyle HorizontalAlign="Left" Width="5%" />
       <ItemTemplate>
       <%#Eval("remark")%>
       </ItemTemplate>
       </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Technical_Remark">
                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Text='<%#Eval("premark")%>' Width="80%"></asp:TextBox>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Assign_Date">
                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                        <ItemTemplate>
                            <%#Eval("adt2")%>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="AssignTo">
                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        <ItemTemplate>
                            <%#Eval("Assignto")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Allotted Time <br/>(in Days)">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblduration" runat="server" Text='<%#Eval("timeduration")%>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Working Time <br/>">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lbltime" runat="server" Text='<%#Eval("totaltime")%>'></asp:Label><br />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lbltime" runat="server" Text='<%#Eval("totaltime")%>'></asp:Label><br />
                            <asp:TextBox ID="txtedit" runat="server" Width="80%" onkeypress="return isNumberKey(event)" placeholder="Time"></asp:TextBox>
                        </EditItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Give_Reason">
                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                        <ItemTemplate>

                            <%#Eval("editremark")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txteremark" runat="server" Width="60%"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="edit" CssClass="btn btn-warning" ToolTip="Edit">
               <i class="btn-icon-only icon-cog"></i>Edit
                            </asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" CssClass="btn btn-danger " ToolTip="Cancel">
                <i class="btn-icon-only icon-remove"></i>Cancle

                            </asp:LinkButton>
                            &nbsp;&nbsp;|&nbsp;&nbsp; 
           <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" CssClass="btn btn-primary" ToolTip="Update">
           <i class="btn-icon-only icon-update"></i>Update
           </asp:LinkButton>
                        </EditItemTemplate>
                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Update">
                        <HeaderStyle HorizontalAlign="Center" Width="3%" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("tsid") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                    </asp:TemplateField>

                </Columns>
                <RowStyle />
                <PagerStyle />
            </asp:GridView>

        </td>
    </tr>


    <tr>

        <td style="height: 26px">&nbsp;&nbsp;Select Department &nbsp;&nbsp;&nbsp;</td>
        <td>
            <asp:DropDownList ID="drpdivision" runat="server" AutoPostBack="true"></asp:DropDownList>
            <br />
            <br />
        </td>
        <td width="20%" style="height: 26px">&nbsp;Employee Name
        </td>
        <td>
            <asp:DropDownList ID="dremp" runat="server" ToolTip="Employee Name" CssClass="dropdown1  ,validate[required,funcCall[drp1[]]]">
            </asp:DropDownList></td>


    </tr>


    <tr>

        <td>&nbsp;Select Status
        </td>
        <td>
            <asp:DropDownList ID="drpstatus" runat="server" ToolTip="status" CssClass="dropdown1, validate[required,funcCall[drp[]]]">
            </asp:DropDownList>
        </td>

    </tr>
    <tr>

        <td colspan="3" style="text-align: center;">
            <asp:Button ID="btnassigned" runat="server" Text="Update Status" class="btn btn-primary" OnClientClick="javascript:validateCheckBoxes()" />
            &nbsp;&nbsp;
            <asp:Button ID="btnremark" runat="server" Text="Update Remark" class="btn btn-primary" OnClientClick="javascript:validateCheckBoxes()" />
        </td>

        <td colspan="2">
            <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Re-Assign Ticket" Width="197px" />

        </td>
    </tr>

    </table>
   
</asp:Content>

