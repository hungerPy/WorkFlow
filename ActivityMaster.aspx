<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ActivityMaster.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_ActivityMaster" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
    
    
     <script src="jvalidation/jquery.min.js"></script>
    <script src="jvalidation/jquery.validationEngine-en.js"></script>
    <script src="jvalidation/jquery.validationEngine.js"></script>
    <link href="css/ValidationEngine.css" rel="stylesheet" />
    <title>Activity Master</title>
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


    </script>
     <div class="span12">
         <div class="widget ">
            <div class="widget-header">
                <i class="icon-pencil"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Add Sub-Work Division</b>
                </div> 
    <div class="widget-content">
                <div id="formcontrols" class="form-horizontal">
                    <fieldset>
                      <div class="control-group">
     <label for="firstname" class="control-label">Department * </label> 
    <div class="controls">  
 <asp:DropDownList ID="drpCat" CssClass ="validate[required,funcCall[drp[]]]"  runat="server" ToolTip="Category"  AutoPostBack="True"></asp:DropDownList>
         <asp:TextBox ID="txtId" runat="server" Visible="false"></asp:TextBox>
              <asp:Label ID="lblid" runat="server"></asp:Label>  </div>
                      </div>
                        <div class="control-group">
                       <label for="firstname" class="control-label">Sub-Work Division * </label> 
    <div class="controls">    
       <asp:TextBox ID="txtCatName"  runat="server" ToolTip="Name" CssClass ="validate[required]" BorderStyle="Groove" MaxLength="20"></asp:TextBox>
      </div>  </div>   
      <div class="control-group">
     <label for="firstname" class="control-label">Status </label><div class="controls"> <asp:DropDownList ID="drpFlg" runat="server" ToolTip="Flag" BorderStyle="Groove" AutoPostBack="True">
<asp:ListItem Value="0">Active</asp:ListItem>
<asp:ListItem Value="1">Inactive</asp:ListItem>
</asp:DropDownList>
        </div></div> 
        <div class="form-actions">   
<asp:Button ID="btnSubmit" runat="server" Text="Create Sub Division" class="btn btn-primary" />
            </div> 
</fieldset> </div> </div> 
  <div class="widget-content">
<asp:GridView ID="GVSubCat" runat="server" PagerStyle-CssClass="paging-link" CssClass="table table-striped table-bordered" AutoGenerateColumns="false" DataKeyNames="ID" Width="100%" Font-Names="Segoe UI">
<Columns>
<asp:TemplateField HeaderText="SNo">
<ItemTemplate>

</ItemTemplate>
<ItemStyle Width="5%" HorizontalAlign="Center" CssClass="gridchk" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Sub Division" HeaderStyle-HorizontalAlign="Left">
<ItemTemplate>
<%#Eval("Name")%>
</ItemTemplate>
<ItemStyle Width="25%" HorizontalAlign="left" />
</asp:TemplateField>

 <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
<ItemTemplate>
    <%#  IIf( Eval("IsActive")="True","Active","InActive") %>
</ItemTemplate>
<ItemStyle Width="25%" HorizontalAlign="left" />
</asp:TemplateField>


<asp:TemplateField HeaderText="Action">
<ItemTemplate>
<asp:LinkButton ID="lnkEdit" runat="server" ToolTip="Edit" CssClass ="btn btn-warning " CommandName="edit">
    <i class="btn-icon-only icon-cog "></i>Edit
</asp:LinkButton>

</ItemTemplate>
<EditItemTemplate>
<asp:LinkButton ID="lnkCancel" runat="server" ToolTip="Cancel" CssClass ="btn btn-info " CommandName="Cancel">
    <i class ="btn-icon-only icon-trash "></i>Cancel
</asp:LinkButton>
</EditItemTemplate>
<ItemStyle Width="10%" HorizontalAlign="Center" />
</asp:TemplateField>
</Columns>
<RowStyle Font-Size="Small" />
<HeaderStyle BackColor="Gainsboro" Font-Size="Small"/>
<EditRowStyle BackColor="Moccasin" />
</asp:GridView>
             </div> </div> 
   </asp:Content>

