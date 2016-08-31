<%@ Page Language="VB" AutoEventWireup="false" CodeFile="loginRegistration.aspx.vb" MasterPageFile="MasterPage.master"  Inherits="Admin_loginRegistration" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <title>Login Registration</title>
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
      <script src="jvalidation/jquery.min.js"></script>
    <script src="jvalidation/jquery.validationEngine-en.js"></script>
    <script src="jvalidation/jquery.validationEngine.js"></script>
    <link href="css/ValidationEngine.css" rel="stylesheet" />
    <%--<script language="javascript" src="functions.js" type="text/javascript"/>--%>
       
     </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server"> 
     <script type ="text/javascript" >
         $(function () {
             $('[id*=Btnsubmit').bind("click", function () {
                 $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
             });
         });
         function drp(field, rules, i, options) {
             if ($('[id$=drpcompany] option:selected').val() == "0") {
                 return "This Feild Required."
             }
         }
        
         function drp1(field, rules, i, options) {
             if ($('[id$= drpemployee] option:selected').val() == "0") {
                 return "This Feild Required."
             }
         }
    </script>

            <div class="widget ">
            <div class="widget-header">
                <i class="icon-pencil"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>User Registration</b>
                </div> 
                
      <div class="widget-content">
                <div id="formcontrols" class="form-horizontal">
                    <fieldset>
                      <div class="control-group">
                          <label for="firstname" runat="server" visible="false" class="control-label">Add/Edit User's </label> 
    <div class="controls">  
    
                  <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
    </div></div>      
                        <div class="control-group">
                          <label for="firstname" class="control-label">Select Company : </label> 
    <div class="controls">  
    <asp:DropDownList ID="drpcompany" runat="server" CssClass="validate[required,funcCall[drp[]]]"  AutoPostBack="True"  ToolTip="Company Name"> </asp:DropDownList>  </div></div>      

       <div class="control-group">
                          <label for="firstname" class="control-label">Select Employee : </label> 
    <div class="controls">  
    <asp:DropDownList ID="drpemployee" CssClass="validate[required,funcCall[drp1[]]]"  runat="server" ToolTip="Employee Name"> </asp:DropDownList></div></div>      
    <div class="control-group">
                          <label for="firstname" class="control-label">User Name</label> 
    <div class="controls">  
    
    <asp:TextBox ID="txtusername" runat="server" ToolTip="User Name" CssClass ="validate[required]"  Width="250px" ></asp:TextBox></div></div>
    <div class="control-group">
                          <label for="firstname" class="control-label">Password</label> 
    <div class="controls">  
    
    <asp:TextBox ID="txtpassword" runat="server" ToolTip="Password"  Width="250px" CssClass ="validate[required]" ></asp:TextBox></div></div>
    <div class="control-group">
                          <label for="firstname" class="control-label">Select Status</label> 
    <div class="controls">  
    
    <asp:RadioButtonList ID="rdbstatus" runat="server" RepeatDirection="Horizontal" Width="147px" >
        <asp:ListItem Value="0" Selected="True">Active</asp:ListItem>
        <asp:ListItem Value="1">Inactive</asp:ListItem>
        </asp:RadioButtonList>
    </div></div>
    <center><asp:Button ID="Btnsubmit" runat="server" Text="Submit" class="btn btn-success " /></center>
</fieldset>    
    </div> 
 </div> 
    <div class="widget-content">
    <asp:GridView ID="GVUsers" runat="server" AutoGenerateColumns="false" PagerStyle-CssClass="paging-link" CssClass="table table-striped table-bordered" DataKeyNames="lid" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                              
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Top" Width="7%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Employee Name">
                            <ItemTemplate>
                            
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"  Width="25%"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Name">
                            <ItemTemplate>
                            <%#Eval("username")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"  Width="18%"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Password">
                            <ItemTemplate>
                            <%#Eval("password")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"  Width="12%"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                            
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Top"  Width="12%"/>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="edit" CssClass ="btn btn-warning " ToolTip="Edit Details">
                                   <i class="btn-icon-only icon-cog"></i>Edit
                    
                            </asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" CssClass ="btn btn-danger " ToolTip="Delete Details">
                           <i class="btn-icon-only icon-trash"></i>Delete
                            </asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" ToolTip="Cancel" CssClass ="btn btn-info ">
                           <i class="btn-icon-only icon-remove"></i>Cancel
                                </asp:LinkButton>
                            </EditItemTemplate>
                            
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="10%" />
                        </asp:TemplateField>
                        
                    </Columns>
                   <FooterStyle BackColor="#CCCC99" />
         <RowStyle BackColor="#F7F7DE" />
         <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
         <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
         <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
         <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
    </div> 
    
    </div>
     </asp:Content>
