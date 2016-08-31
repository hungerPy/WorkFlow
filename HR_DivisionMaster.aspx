<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="HR_DivisionMaster.aspx.vb" Inherits="Admin_HR_DivisionMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">

    <link rel="icon" href="Images/hslogo.jpg" type="image/x-icon"/>
            <link href="jvalidation/ValidationEngine.css" rel="stylesheet" type="text/css"  />
    <link href="css/formStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="jvalidation/jquery.min.js"></script>
<script type="text/javascript" src="jvalidation/jquery.validationEngine-en.js" charset="utf-8"></script>
<script type="text/javascript" src="jvalidation/jquery.validationEngine.js" charset="utf-8"></script>

    <script type="text/javascript">
        function Div(txtDiv, str) {
            var str1 = str.split(" ").map(function (i) { return i[0].toUpperCase() + i.substring(1) }).join(" ");
            document.getElementById(txtDiv).value = str1;
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return true;


            return false;
            
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">

    <script type="text/javascript">

        $(function () {
            $('[id*=btnSubmit]').bind("click", function () {
                $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
            });
        });


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
                <a href="HR_Employee.aspx" class="btn btn-primary">Add Employee</a>

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
    <td align="center">Division Master
    <asp:TextBox ID="txtId" runat="server" Visible="false"></asp:TextBox>
    </td>
    </tr>
    </table> 
    <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblForm" style="margin-top:20px;">
    <tr>
    <td width="20%">Division</td>
    <td><asp:TextBox ID="txtDiv" CssClass="validate[required,custom[onlyLetterSp]]" onkeyup="javascript:Div(this.id, this.value)"  runat="server" AccessKey="r" ToolTip="Division Name" BorderStyle="Groove" MaxLength="20" Width="80%"></asp:TextBox></td>
    </tr>
   
    <tr>
    <td colspan="2" style="text-align: center"><asp:Label ID="lblError" runat="server" Text="Label" Font-Bold="True" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr >
    <td colspan="2" align="center">
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
    </td>
    </tr>
    </table>
    <br />
    <asp:GridView ID="GVDivisions" runat="server" AutoGenerateColumns="false" DataKeyNames="DivisionId" Width="100%" Font-Names="Segoe UI">
<Columns>
<asp:TemplateField HeaderText="SNo">
<ItemTemplate>

</ItemTemplate>
<ItemStyle Width="5%" HorizontalAlign="Center" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Division" HeaderStyle-HorizontalAlign="Left">
<ItemTemplate>
<%#Eval("DivisionHead")%>
</ItemTemplate>
<EditItemTemplate>
<asp:TextBox ID="txtDivision" onkeypress="return isNumberKey(event)" runat="server" Text='<%#Eval("DivisionHead")%>' Width="80%"></asp:TextBox>
</EditItemTemplate>
<ItemStyle Width="70%"/>
</asp:TemplateField>

<asp:TemplateField HeaderText="Priority" HeaderStyle-HorizontalAlign="Left">
<ItemTemplate>
<%#Eval("Priority")%>
</ItemTemplate>
<EditItemTemplate>
<asp:DropDownList ID="drp" runat="server"></asp:DropDownList>
</EditItemTemplate>
<ItemStyle Width="10%"/>
</asp:TemplateField>
<asp:TemplateField HeaderText="Action">
<ItemTemplate>
<asp:LinkButton ID="lnkEdit" runat="server" ToolTip="Edit" CommandName="Edit">
<img src="Images/edit.jpg" alt="Edit" style="border:0;"/>
</asp:LinkButton>
</ItemTemplate>
<EditItemTemplate>
<asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Update" CommandName="Update">
<img src="Images/update.gif" alt="Cancel" style="border:0;" />
</asp:LinkButton>&nbsp;&nbsp;&nbsp;
<asp:LinkButton ID="lnkCancel" runat="server" ToolTip="Cancel" CommandName="Cancel">
<img src="Images/CANCEL.GIF" alt="Cancel" style="border:0;" />
</asp:LinkButton>
</EditItemTemplate>
<ItemStyle Width="10%" HorizontalAlign="Center" />
</asp:TemplateField>

</Columns>
<RowStyle Font-Size="Small" />
<HeaderStyle BackColor="Gainsboro" Font-Size="Medium"/>
<SelectedRowStyle BackColor ="burlywood" />
<EditRowStyle BackColor="red" />
</asp:GridView>
             
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

