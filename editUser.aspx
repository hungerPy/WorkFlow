<%@ Page Language="VB" AutoEventWireup="false" CodeFile="editUser.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_editUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <title>User Rights</title>
    <script language="javascript" src="functions.js" type="text/javascript"></script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">
    <script language="javascript" type="text/javascript">
   function checkIt()
{
var a=document.getElementById("txtUId").value.toLowerCase();
if (a=="available" || a=="booked")
{
alert("Invalid User Name.")
document.getElementById("txtUId").focus()
return false;
}
return true; 
}
function CheckAll(th)
{
var clst,clt;
clst = th.name.replace("$chkNP","")
var elm;
var i=0;
clt = clst + "$ChkList$" + i
elm=document.getElementById(clt)
while(elm != null)
{
elm.checked=th.checked
i=i+1
clt = clst + "$ChkList$" + i
elm=document.getElementById(clt)
}
}
    
    </script>
    
<style type="text/css">
    .DataGridFixedHeader { POSITION: relative;; TOP: expression(this.offsetParent.scrollTop-2); BACKGROUND-COLOR: white }

</style>    

      <div>
     <table id="tblHeader" class="DataGridFixedHeader" width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:<%=db.headerBG%>;color:<%=db.headerColor%>;font-family:<%=db.headerText%>;font-weight:<%=db.headerWeight%>;font-style:<%=db.headerStyle%>;text-decoration:<%=db.headerDecoration%>;font-size:<%=db.headerSize%>px;text-align:<%=db.headerAlignment%>;">
   <tr>
    <td>Employee Rights</td>
    </tr>
    </table>
    
    
     <table width="100%" border="0" cellpadding="0" class="DataGridFixedHeader" cellspacing="0" id="tblForm" style="background-color:<%=db.formBG%>;color:<%=db.formColor%>;font-family:<%=db.formText%>;font-weight:<%=db.formWeight%>;font-style:<%=db.formStyle%>;text-decoration:<%=db.formDecoration%>;font-size:<%=db.formSize%>px;text-align:<%=db.formAlignment%>">
        <tr>
            <td width="25%"  valign="middle" style="padding-left:10px;">Select Company Name</td>
            <td width="75%"><asp:DropDownList ID="drpCompanyName" runat="server" ToolTip="Company Name" AutoPostBack="True" Height="20" Font-Size="11pt" Font-Bold="true" Width="350px"></asp:DropDownList></td>
        </tr>
        <tr >
            <td width="25%" valign="middle" style="padding-left:10px;">Division Name</td>
            <td ><asp:DropDownList ID="DrpDivision" runat="server" ToolTip="Division Name" Width="350px"  AutoPostBack="true"></asp:DropDownList></td>
        </tr> 
        <tr>
            <td width="25%"  valign="middle" style="padding-left:10px;">Employee Designation</td>
            <td ><asp:DropDownList ID="drpempdegi" ToolTip="Employee Degination" Width="350px"  runat="server" AutoPostBack="true" ></asp:DropDownList></td>
          </tr>
        <tr >
            <td width="25%" valign="middle" style="padding-left:10px;">Employee Name</td>
            <td width="75%" colspan="4"><asp:DropDownList ID="drpUserName" runat="server" Width="350px"  ToolTip="Employee Name" AutoPostBack="True"></asp:DropDownList></td>
            <!-- <td width="25%">&nbsp;
                 </td>
                 <td width="25%">&nbsp;
                     &nbsp;</td>-->
        </tr>
        <%-- <tr>
             <td width="25%">
                 User Name*</td>
             <td width="25%">
                 <asp:TextBox ID="txtUName" runat="server" ToolTip="User Name"></asp:TextBox>
             </td>
             <td width="25%">
                 Designation*</td>
             <td width="25%">
                     <asp:DropDownList ID="drpDesignation" runat="server" ToolTip="Designation" >
                         <asp:ListItem Value="Administrative">Administrative</asp:ListItem>
                         <asp:ListItem Value="Director">Director</asp:ListItem>
                         <asp:ListItem Value="GM">General Manager</asp:ListItem>
                         <asp:ListItem Value="VP">V.P.</asp:ListItem>
                         <asp:ListItem Value="AVP">A.V.P.</asp:ListItem>
                         <asp:ListItem Value="Manager">Manager</asp:ListItem>
                         <asp:ListItem Value="AM">Assistant Manager</asp:ListItem>
                         <asp:ListItem Value="Executive">Executive</asp:ListItem>
                         <asp:ListItem Value="DSA">DSA</asp:ListItem>
                         <asp:ListItem>Broker</asp:ListItem>
                         </asp:DropDownList>
                         <asp:TextBox ID="txtbid" runat="server" Visible="false"  AccessKey="r" ToolTip="Broker Id" Width="50px"></asp:TextBox>
             </td>
         </tr>
        
        <tr >
            <td width="25%">
                Contact No.*</td>
            <td width="25%">
                <asp:TextBox ID="txtContactNo" runat="server" AccessKey="r" ToolTip="Contact No."></asp:TextBox></td>
             <td width="25%">
                 Email Id</td>
                 <td width="25%">
                <asp:TextBox ID="txtEmail" runat="server" ToolTip="Email Id"></asp:TextBox></td>
        </tr>
        <tr>
            <td >
                User Id*</td>
            <td >
                <asp:TextBox ID="txtUId" runat="server" AccessKey="r" ToolTip="User Id"></asp:TextBox>
                
            </td>
            <td >
                Password*</td>
            <td ><asp:TextBox ID="txtPWD" runat="server" AccessKey="r" ToolTip="Password"></asp:TextBox>
            </td>
        </tr>--%>
    </table> 
        
     <table id="Table1" width="100%" class="DataGridFixedHeader" cellpadding="0" cellspacing="0" border="0" style="background-color:<%=db.headerBG%>;color:<%=db.headerColor%>;font-family:<%=db.headerText%>;font-weight:<%=db.headerWeight%>;font-style:<%=db.headerStyle%>;text-decoration:<%=db.headerDecoration%>;font-size:<%=db.headerSize%>px;text-align:<%=db.headerAlignment%>">
   <tr>
    <td>Access Rights( Modules Wise )</td>
    </tr>
    <tr>
        <td>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table3" style="background-color:<%=db.formBG%>;color:<%=db.formColor%>;font-family:<%=db.formText%>;font-weight:<%=db.formWeight%>;font-style:<%=db.formStyle%>;text-decoration:<%=db.formDecoration%>;font-size:<%=db.formSize%>px;text-align:<%=db.formAlignment%>">
                <tr>
                <td valign="middle" style="font-weight:bold;font-size:larger;">Access Rights for Information about enquiry:&nbsp;&nbsp;<asp:CheckBox ID="chkAllmail" runat="server" /></td>
                </tr>
                
            </table>
        </td>
    </tr>
   </table>
       <asp:GridView ID="gvRights" ShowHeader="False" runat="server" AutoGenerateColumns="false" Width="100%" Font-Size="Smaller" Font-Names="Arial" DataKeyNames="NodeCode" >
        <Columns>
        <asp:TemplateField>
         <ItemTemplate>
        <table width="100%" cellpadding="0" cellspacing="0">
        <tr style="background-color:<%=db.formBG%>;color:<%=db.formColor%>;font-family:<%=db.formText%>;font-weight:<%=db.formWeight%>;font-style:<%=db.formStyle%>;text-decoration:<%=db.formDecoration%>;font-size:<%=db.formSize%>px;text-align:<%=db.formAlignment%>"><td width="3%">
        <asp:CheckBox ID="chkNP" runat="server"/></td>
        <td width="97%"><asp:Label ID="lblNode" runat="server" Text='<%#Eval("nodetext")%>' Width="90%" ></asp:Label>
            </td></tr>        
            <tr style="background-color:<%=db.headerBG%>;color:<%=db.headerColor%>;font-family:<%=db.formText%>;font-weight:<%=db.formWeight%>;font-style:<%=db.formStyle%>;text-decoration:<%=db.formDecoration%>;font-size:<%=db.formSize%>px;text-align:<%=db.formAlignment%>">
            <td colspan="2">
            <asp:CheckBoxList ID="ChkList" runat="server" CellSpacing="5" RepeatColumns="5" RepeatDirection="Horizontal">
            </asp:CheckBoxList>
                          </td></tr>
           </table>
       </ItemTemplate>
        </asp:TemplateField>
        </Columns>
                  
         <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
     <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table2" style="background-color:<%=db.headerBG%>;color:<%=db.headerColor%>;font-family:<%=db.formText%>;font-weight:<%=db.formWeight%>;font-style:<%=db.formStyle%>;text-decoration:<%=db.formDecoration%>;font-size:<%=db.formSize%>px;text-align:<%=db.formAlignment%>;border-color:<%=db.headerBG%>">
         <tr><td align="center"><asp:Button ID="btnSubmit" runat="server" Text="Edit User" /></td></tr>
       </table>
    </div>
</asp:Content> 