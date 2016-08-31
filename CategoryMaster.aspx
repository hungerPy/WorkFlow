<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CategoryMaster.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_CategoryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <title>Category Registration</title>
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
     <script src="jvalidation/jquery.min.js"></script>
    <script src="jvalidation/jquery.validationEngine-en.js"></script>
    <script src="jvalidation/jquery.validationEngine.js"></script>
    <link href="css/ValidationEngine.css" rel="stylesheet" />
    <%--<script language="javascript" type="text/javascript" src="../functions.js"></script>--%>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server"> 
       <script type ="text/javascript" >
           $(function () {
               $('[id*=btnSubmit]').bind("click", function () {
                   $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
               });
           });



    </script> 
     <script type ="text/javascript" >
          $(function () {
              $('[id*=btnSSSubmit').bind("click", function () {
                  $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
              });
          });
          function drp(field, rules, i, options) {
              if ($('[id$=drpccat option:selected').val() == "0") {
                  return "This Feild Required."
              }
          }


    </script>
  
   <div class="widget container">
            <div class="widget-header ">
                <i class="icon-user"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Category/Sub Category Masters<asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
     </div> 
                <asp:Panel ID="pnlcategory" runat="server" Width="100%">
        <div class="widget-content"> 
      <table class="table">
            <tr>
                <td width="20%">Category Name</td>
                <td><asp:TextBox ID="txtCatName" runat="server" ToolTip="Name" CssClass ="validate[required]" BorderStyle="Groove" ></asp:TextBox></td>
            </tr>
            <tr>
                <td>Select Status</td>
                <td><asp:RadioButtonList ID="rdbstatus" runat="server" RepeatDirection="Horizontal" >
                <asp:ListItem Value="0" Selected="True">Active</asp:ListItem>
                <asp:ListItem Value="1">Inactive</asp:ListItem>
                </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red" Text="Label"></asp:Label></td>
            </tr>
            <tr style="background-color:<%=db.headerBG%>;color:<%=db.headerColor%>;font-family:<%=db.headerText%>;font-weight:<%=db.headerWeight%>;font-style:<%=db.headerStyle%>;text-decoration:<%=db.headerDecoration%>;font-size:<%=db.headerSize%>px;text-align:<%=db.headerAlignment%>">
                <td colspan="2" align="center"><asp:Button ID="btnSubmit" runat="server" Text="Create Category" CssClass ="btn btn-primary " />
                <asp:Button ID="btnCback" runat="server" Text="Back" CssClass ="btn btn-warning " /></td>
            </tr>
        </table>
            </div> 
                    <div class="widget-content ">
        <asp:GridView ID="gvcategory" runat="server" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" DataKeyNames="CId" >
     <Columns>
     <asp:TemplateField HeaderText="SNo">
     <ItemTemplate>
     
     </ItemTemplate>
     <ItemStyle Width="10%" HorizontalAlign="Center" />
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Category Name" HeaderStyle-HorizontalAlign="Left">
     <ItemTemplate>
     <%#Eval("category")%>
     </ItemTemplate>
         <HeaderStyle HorizontalAlign="Left" />
     <ItemStyle Width="55%" HorizontalAlign="left" />
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Status">
     <ItemTemplate>
                            
     </ItemTemplate>
     <ItemStyle HorizontalAlign="center" VerticalAlign="Top"  Width="12%"/>
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
     <ItemTemplate>
   <center>  <asp:LinkButton ID="lnkEdit" runat="server" CssClass ="btn btn-warning " CommandName="Edit">
         <i class="btn-icon-only icon-cog"></i>Edit
     </asp:LinkButton></center>
     </ItemTemplate>
     <EditItemTemplate>
     <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" CssClass ="btn btn-primary " ToolTip="Cancel">
       <i class="btn-icon-only icon-remove"></i>Cancel
     </asp:LinkButton>
     </EditItemTemplate>
         <HeaderStyle HorizontalAlign="Center" />
     <ItemStyle Width="15%" HorizontalAlign="center" />
     </asp:TemplateField>
     </Columns>
       <EmptyDataTemplate><center>No Data Available</center></EmptyDataTemplate>
  <HeaderStyle HorizontalAlign="center" />
  <HeaderStyle HorizontalAlign="center" />
  <RowStyle  />
     </asp:GridView>
                        </div>
     </asp:Panel>
     
     <asp:Panel ID="pnlsubcat" runat="server" Width="100%">
            <div class="widget-content">
        <table class="table ">
            <tr>
                <td width="20%">Category</td>
                <td><asp:DropDownList ID="drpccat" runat="server" AutoPostBack="true" CssClass="validate[required,funcCall[drp[]]]"  ToolTip="Category"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Sub Category Name</td>
                <td><asp:TextBox ID="txtSCatName" runat="server" CssClass ="validate[required]" ToolTip="Name" BorderStyle="Groove" ></asp:TextBox></td>
            </tr>
            <tr>
                <td>Select Status</td>
                <td><asp:RadioButtonList ID="rbtsubcat" runat="server" RepeatDirection="Horizontal" >
                <asp:ListItem Value="0" Selected="True">Active</asp:ListItem>
                <asp:ListItem Value="1">Inactive</asp:ListItem>
                </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                <asp:Label ID="lblSSError" runat="server" Font-Bold="True" ForeColor="Red" Text="Label"></asp:Label>

                </td>
            </tr>
            <tr style="background-color:<%=db.headerBG%>;color:<%=db.headerColor%>;font-family:<%=db.headerText%>;font-weight:<%=db.headerWeight%>;font-style:<%=db.headerStyle%>;text-decoration:<%=db.headerDecoration%>;font-size:<%=db.headerSize%>px;text-align:<%=db.headerAlignment%>">
                <td colspan="2" align="center"><asp:Button ID="btnSSSubmit" runat="server" CssClass ="btn btn-primary" Text="Create Sub Category" />
                <asp:Button ID="btnSSback" runat="server" Text="Back" CssClass ="btn btn-warning " /></td>
            </tr>
        </table>
                </div> 
         <div class ="widget-content ">
        <asp:GridView ID="gvsubcat" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="SCId" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right" CssClass="table table-striped table-bordered">
     <Columns>
     <asp:TemplateField HeaderText="SNo">
     <ItemTemplate>
     
     </ItemTemplate>
     <ItemStyle Width="10%" HorizontalAlign="Center" />
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Sub Category Name" HeaderStyle-HorizontalAlign="Left">
     <ItemTemplate>
     <%#Eval("subcategory")%>
     </ItemTemplate>
     <ItemStyle Width="70%" HorizontalAlign="left" />
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Status">
     <ItemTemplate>
                            
     </ItemTemplate>
     <ItemStyle HorizontalAlign="center" VerticalAlign="Top"  Width="12%"/>
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
     <ItemTemplate>
    
         <center> <asp:LinkButton ID="lnkedit1" runat="server" CommandName="Edit" CssClass ="btn btn-warning ">
              <i class="btn-icon-only icon-cog"></i>Edit
     </asp:LinkButton></center> 
     </ItemTemplate>
     <EditItemTemplate>
     <asp:LinkButton ID="lnkCancel1" runat="server" CommandName="Cancel" ToolTip="Cancel" CssClass ="btn btn-info">
              <i class="btn-icon-only icon-remove"></i>Cancel
     </asp:LinkButton>
     </EditItemTemplate>
     <ItemStyle Width="10%" HorizontalAlign="center" />
     </asp:TemplateField>
     </Columns>
       <EmptyDataTemplate><center>No Data Available</center></EmptyDataTemplate>
  <HeaderStyle HorizontalAlign="center" />
  <HeaderStyle  HorizontalAlign="center" />
  <RowStyle  />
     </asp:GridView>
             </div>
     </asp:Panel>
     <asp:Panel ID="pnlshow" runat="server" Width="100%">
      <table id="tblParty" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0" class="table">
       <tr>
            <td width="20%">Select Category</td>
            <td width="25%" align="left" ><asp:DropDownList ID="drpcat" runat="server" AutoPostBack="true" ToolTip="Select Head" ></asp:DropDownList></td>
            <td width="50%" align="left" ><asp:Button ID="btnmodify" runat="server" Text="Add Category" CssClass ="btn btn-info " /><asp:Button ID="btnsubcat" CssClass ="btn btn-info "  runat="server" Text="Add Sub Category" /> </td>
       </tr>
     </table>
         <div class ="widget-content ">
     <table width="100%" class ="table " border="0" id="tblForm" >
     <tr>
     <td>
     <asp:GridView ID="GVsubcategory" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="SCId" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right" CssClass="table table-striped table-bordered">
     <Columns>
     <asp:TemplateField HeaderText="SNo">
     <ItemTemplate>
     
     </ItemTemplate>
     <ItemStyle Width="10%" HorizontalAlign="Center" />
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Sub Category Name" HeaderStyle-HorizontalAlign="Left">
     <ItemTemplate>
     <%#Eval("subcategory")%>
     </ItemTemplate>
     <ItemStyle Width="60%" HorizontalAlign="left" />
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Status">
     <ItemTemplate>
                            
     </ItemTemplate>
     <ItemStyle HorizontalAlign="center" VerticalAlign="Top"  Width="12%"/>
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
     <ItemTemplate>
   <center> <asp:LinkButton ID="lnkEdit" runat="server" ToolTip="Add Sub Category" CssClass ="btn btn-warning " CommandName="Edit" >
       <i class="btn-icon-only icon-cog"></i>Edit
     </asp:LinkButton></center> 
     </td></tr>
     </ItemTemplate>
     <ItemStyle Width="20%" HorizontalAlign="center" />
     </asp:TemplateField>
     </Columns>
       <EmptyDataTemplate><center>No Data Available</center></EmptyDataTemplate>
  <HeaderStyle HorizontalAlign="center" />
  <HeaderStyle  HorizontalAlign="center" />
  <RowStyle  />
     </asp:GridView>
     </td>
     </tr>
     <tr>
     <td>
     <asp:Label ID="lblMessage" runat="server" ForeColor="red" Visible="false" Font-Bold="true"></asp:Label>
     </td>
     </tr>
     </table>
             </div>
     </asp:Panel>
    </asp:Content>
