<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DivisionMaster.aspx.vb" MasterPageFile="MasterPage.master"  Inherits="Admin_DivisionMaster" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
    <script src="jvalidation/jquery.min.js"></script>
    <script src="jvalidation/jquery.validationEngine-en.js"></script>
    <script src="jvalidation/jquery.validationEngine.js"></script>
    <link href="css/ValidationEngine.css" rel="stylesheet" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">
        <%--<script src="functions.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(function () {
            $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
        });
        function drp(field, rules, i, options) {
            if ($('[id$=drpemployee] option:selected').val() == "0") {
                return "This Feild Required."
            }
        }

    </script>
    <asp:TextBox ID="txtId" runat="server" Visible="False"></asp:TextBox>
     <div class="span12"></div>

    <div class="widget container ">
            <div class="widget-header">
                <i class="icon-file"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Add Department</b>
                </div>   
        
      
   <div class="widget-content">
         <table class="table">
    <%-- <tr>
        <td>Department Code*</td>
        <td><asp:TextBox ID="txtDCode" ToolTip="Division Code" CssClass="validate[required]"  runat="server"></asp:TextBox></td>
     </tr>--%>
     <tr>
        <td>Department Name*</td>
        <td><asp:TextBox ID="txtDName" runat="server" ToolTip="Division Name" CssClass="validate[required]" ></asp:TextBox></td>
     </tr>
<%--     <tr>
        <td>Department Head</td>
        <td><asp:DropDownList ID="drpemployee" CssClass="" runat="server" ToolTip="Employee"></asp:DropDownList></td>
     </tr>--%>
    <tr>
    <td>Select Status</td>
    <td><asp:RadioButtonList ID="rdbstatus" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Value="0" Selected="True">Active</asp:ListItem>
        <asp:ListItem Value="1">Inactive</asp:ListItem>
        </asp:RadioButtonList></td>
    </tr>
    
     <tr style="background-color:white">
            <td colspan="4" align="center">
                 <asp:Label runat="server" id="lblError" ForeColor="red" Font-Bold="true"></asp:Label>
            </td>
     </tr>
     <tr style="background-color:<%=db.headerBG%>">
            <td colspan="4" style="text-align: center"><asp:Button ID="btnSubmit" CssClass="btn btn-primary " runat="server" Text="Add" /></td>
     </tr>
             </table>
        </div>
    <div class="widget-content">
        <table class="table">
     <tr>

         
            <td colspan="4" style="text-align: center">
             <asp:GridView ID="GVDivision" runat="server" PagerStyle-CssClass="paging-link" CssClass="table table-striped table-bordered" AutoGenerateColumns="false" Width="100%" DataKeyNames="ID">
               <Columns>
               <asp:TemplateField HeaderText="S.No.">
               <ItemTemplate>
               
               </ItemTemplate>
               <ItemStyle Width="5%" HorizontalAlign="center" />
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Division Name">
               <ItemTemplate>
               <%#Eval("Name")%>
               </ItemTemplate>
               <ItemStyle HorizontalAlign="Left" Width="20%" />
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Status">
               <ItemTemplate>
                            
               </ItemTemplate>
               <ItemStyle HorizontalAlign="center" Width="12%"/>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Action">
               <ItemTemplate>
               <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-warning" CommandName="edit">
              <i class="btn-icon-only icon-cog">Edit</i>
               
               </asp:LinkButton>&nbsp;&nbsp;
               <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger" CommandName="delete">
              <i class="btn-icon-only icon-trash">Delete</i>
               
               </asp:LinkButton>
               </ItemTemplate>
               <EditItemTemplate>
               <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" CssClass="btn btn-info" ToolTip="Cancel">
            <i class="btn-icon-only icon-remove">Cancel</i>
               </asp:LinkButton>
               <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" Visible="false" >
              <i class="btn-icon-only icon-trash">Delete</i>
               </asp:LinkButton>
               </EditItemTemplate>
               <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="10%" />
               </asp:TemplateField>
               
               </Columns>
               <RowStyle BackColor="#F7F7DE" />
               <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
               <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
               <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
               <AlternatingRowStyle BackColor="White" />
               
               </asp:GridView>
            </td>
        </tr>
   </table>
        </div>


        </div>
   </asp:Content>

