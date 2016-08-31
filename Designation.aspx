<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Designation.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Designation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
     <script src="jvalidation/jquery.min.js"></script>
    <script src="jvalidation/jquery.validationEngine-en.js"></script>
    <script src="jvalidation/jquery.validationEngine.js"></script>
    <link href="css/ValidationEngine.css" rel="stylesheet" />
    <script language="javascript" src="functions.js" type="text/javascript"></script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server"> 
    <script type ="text/javascript" >
        $(function () {
            $('[id*=btnSubmit]').bind("click", function () {
                $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
            });
        });



    </script>
     <div class="span12">
         <div class="widget ">
            <div class="widget-header">
                <i class="icon-pencil"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>  Designation Master</b>
                </div>
 
        <asp:TextBox ID="txtId" runat="server" Visible="False"></asp:TextBox>
    <div class="widget-content">
                <div id="formcontrols" class="form-horizontal">
                    <fieldset>
                          <div class="control-group">
                        <label for="Departid" class="control-label">Department *</label> 
                        <div class="controls">
                        <asp:DropDownList ID="DrpDepartment" runat="server" CssClass="validate[required,funcCall[drp2[]]]" ToolTip="Department Name"></asp:DropDownList>
                        </div>
                        </div>  
                        <div class="control-group">
                        <label for="firstname" class="control-label">Designation *</label> 
                        <div class="controls">
                        <asp:TextBox ID="txtDesgination" width="175" runat="server" CssClass ="validate[required]" ToolTip="Designation" ></asp:TextBox>
                        </div>
                        </div> 
                        <div class="control-group">
     <label for="firstname" class="control-label">Description *</label> 
    <div class="controls">
          <asp:TextBox ID="txtDescription" TextMode="multiLine" Rows="3" Columns="50" runat="server" CssClass ="validate[required]"  ToolTip="Service Description" ></asp:TextBox>
        </div> </div> 
                        <div class="form-actions">
            <center>   <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary"/></center> 
                            </div> 
</fieldset> </div> </div> 
             <div class ="widget-content ">
                <asp:GridView ID="GVDesignations" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right" CssClass="table table-striped table-bordered" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center" Width="5%"  CssClass ="gridchk "/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Designation">
                            <ItemTemplate>
                                <%#Eval("DesignationName")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="15%" />
                        </asp:TemplateField>
                          <asp:TemplateField headertext="Description">
                            <ItemTemplate>
                                <%#Eval("Description")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="18%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <center>
                            <asp:LinkButton ID="lnkEdit" runat="server" class="btn btn-warning " tooltip="Edit" CommandName="edit">
                           <i class="btn-icon-only icon-cog"></i>Edit
                            </asp:LinkButton>
                                 
                            <asp:LinkButton ID="lnkDelete" runat="server" class="btn btn-danger " tooltip="Delete" CommandName="delete">
                           <i class="btn-icon-only icon-trash"></i>Delete
                            </asp:LinkButton>
                                </center>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <center>
                            <asp:LinkButton ID="lnkCancel" runat="server" class="btn btn-info "  CommandName="Cancel">
                           <i class="btn-icon-only icon-remove"></i>Cancel</asp:LinkButton>
                             
                            <asp:LinkButton ID="lnkDelete" runat="server" class="btn btn-danger " CommandName="delete"  Visible="false" >
                           <i class="btn-icon-only icon-trash"></i>Delete
                            </asp:LinkButton>
                                </center>
                            </EditItemTemplate>
                            
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15%" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="update">
                                    <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/EDIT.GIF" ToolTip="Edit Details" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                     </Columns>
                     <RowStyle BackColor="#F7F7DE" />
                     <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                     <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                     <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
                     <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
           </div></div> 
   </asp:Content>