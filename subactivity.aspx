<%@ Page Language="VB" AutoEventWireup="false" CodeFile="subactivity.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_subactivity" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
       <script src="jvalidation/jquery.min.js"></script>
    <script src="jvalidation/jquery.validationEngine-en.js"></script>
    <script src="jvalidation/jquery.validationEngine.js"></script>
    <link href="css/ValidationEngine.css" rel="stylesheet" />
    <title>Sub Activity</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server"> 
     <script type ="text/javascript" >
         $(function () {
             $('[id*=btnSubmit').bind("click", function () {
                 $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
             });
         });
         function drp(field, rules, i, options) {
             if ($('[id$=drpCat] option:selected').val() == "0") {
                 return "This Feild Required."
             }
         }
         function drp1(field, rules, i, options) {
             if ($('[id$=drpact] option:selected').val() == "0") {
                 return "This Feild Required."
             }
         }

    </script>
    <div class="span12">
         <div class="widget ">
            <div class="widget-header">
                <i class="icon-pencil"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Sub Activity Master</b>
                </div>
     <asp:TextBox ID="txtId" runat="server" Visible="false"></asp:TextBox>
     <div class="widget-content">
                <div id="formcontrols" class="form-horizontal">
                    <fieldset>
                        <div class="control-group">
     <label for="firstname" class="control-label">Department Name</label> 
    <div class="controls">
    <asp:DropDownList ID="drp_depart"  runat="server" ToolTip="Department" CssClass="validate[required,funcCall[drp[]]]" AutoPostBack="True"></asp:DropDownList>
        <asp:Label ID="Label1" runat="server"></asp:Label>
        </div> </div>
                         <div class="control-group">
     <label for="firstname" class="control-label">Sub Division Name</label> 
    <div class="controls">
    <asp:DropDownList ID="drpCat"  runat="server" ToolTip="Category" CssClass="validate[required,funcCall[drp[]]]" AutoPostBack="True"></asp:DropDownList>
        <asp:Label ID="lblid" runat="server"></asp:Label>
        </div> </div> 
                         <div class="control-group">
     <label for="firstname" class="control-label">Activity Name</label> 
    <div class="controls">
      <asp:TextBox ID="txt_activity" runat="server" CssClass="validate[required]" ></asp:TextBox>
</div>  </div> 
                          <div class="control-group">
     <label for="firstname" class="control-label">Sub Activity Status</label> 
    <div class="controls">
             
    <asp:DropDownList ID="drpFlg" runat="server" ToolTip="Flag" BorderStyle="Groove">
<asp:ListItem Value="0">Active</asp:ListItem>
<asp:ListItem Value="1">Inactive</asp:ListItem>
</asp:DropDownList>
       
            
                <%--<asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red" Text="Label"></asp:Label>--%>
       </div> </div> 
                        <div class="form-actions">
<asp:Button ID="btnSubmit" runat="server" Text="Create Sub Activity" class="btn btn-primary"  />
   </div> </fieldset> </div> </div> </div> 
<%--<asp:GridView ID="GVSubCat" runat="server" AutoGenerateColumns="false" DataKeyNames="sdcode" Width="100%" Font-Names="Segoe UI">
<Columns>
<asp:TemplateField HeaderText="SNo">
<ItemTemplate>
<%#Eval("SNo")%>
</ItemTemplate>
<ItemStyle Width="5%" HorizontalAlign="Center" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Left">
<ItemTemplate>
<%#Eval("dcode")%>
</ItemTemplate>
<ItemStyle Width="25%" HorizontalAlign="left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Sub-Category Name" HeaderStyle-HorizontalAlign="Left">
<ItemTemplate>
<%#Eval("SDname")%>
</ItemTemplate>
<ItemStyle Width="35%" HorizontalAlign="left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Action">
<ItemTemplate>
<asp:LinkButton ID="lnkEdit" runat="server" ToolTip="Edit" CommandName="Update">
<img src="Images/EDIT.GIF" alt="Edit" style="border:0;"/>
</asp:LinkButton>

</ItemTemplate>
<EditItemTemplate>
<asp:LinkButton ID="lnkCancel" runat="server" ToolTip="Cancel" CommandName="Cancel">
<img src="Images/CANCEL.GIF" alt="Cancel" style="border:0;" />
</asp:LinkButton>
</EditItemTemplate>
<ItemStyle Width="10%" HorizontalAlign="Center" />
</asp:TemplateField>
</Columns>
<RowStyle Font-Size="Small" />
<HeaderStyle BackColor="Gainsboro" Font-Size="Small"/>
<EditRowStyle BackColor="Moccasin" />
</asp:GridView>--%>
    </asp:Content>

