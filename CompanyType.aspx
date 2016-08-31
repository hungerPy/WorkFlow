<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CompanyType.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_CompanyType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
    <title>Untitled Page</title>
     <script src="jvalidation/jquery.min.js"></script>
    <script src="jvalidation/jquery.validationEngine-en.js"></script>
    <script src="jvalidation/jquery.validationEngine.js"></script>
    <link href="css/ValidationEngine.css" rel="stylesheet" />
        <script language="javascript" src="functions.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">
     <script type="text/javascript">
         $(function () {
             $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
         });
         </script>

      <div class="span12">
           
                <div class="widget-content">
                <div id="formcontrols" class="form-horizontal">
                    <fieldset>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>   Company Type * </b>
            <asp:TextBox ID="txtCompanyType" runat="server"  ToolTip="Company Type" CssClass ="validate[required]" Width="250px"></asp:TextBox>
            </div></div>

    
              
                <div class="form-actions">
             <center>  <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary" /></center>
                            </div> 
</fieldset> </div> </div> 
            
       <div class="widget-content">
       
          <asp:GridView ID="gvCompany" runat="server" AutoGenerateColumns="false" Width="100%"   PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right" CssClass="table table-striped table-bordered"  Font-Bold="False" Font-Size="Smaller" DataKeyNames="typeId">
       <Columns>
       <asp:TemplateField HeaderText="S.No.">
       <ItemTemplate>
       
       </ItemTemplate>
       <ItemStyle Width="10%" HorizontalAlign="Center" CssClass="gridchk " />
       </asp:TemplateField>
        <asp:TemplateField HeaderText="Company Type" HeaderStyle-HorizontalAlign="Left">
       <ItemTemplate>
       <%#Eval("CompanyType")%>
       </ItemTemplate>
       <EditItemTemplate>
       <asp:TextBox runat="server" ID="txtType" Text='<%#Eval("CompanyType")%>'></asp:TextBox>
       </EditItemTemplate>
       
       
       <ItemStyle Width="60%" />
       </asp:TemplateField>
        
        
       <asp:TemplateField HeaderText="Edit">
               <ItemTemplate>
                   <center>
                <asp:LinkButton ID="lnkEdit" CommandName="edit" class="btn btn-warning " ToolTip ="Edit" runat="server">
                    <i class="btn-icon-only icon-cog"></i>Edit
                   </asp:LinkButton>
                       </center>
                   </ItemTemplate>
                   
                   <EditItemTemplate>
                    <asp:LinkButton ID="lnkCancel" CommandName="cancel" ToolTip ="Cancel" class="btn btn-info " runat="server">
                    <i class="btn-icon-only icon-remove"></i>Cancel
                   </asp:LinkButton>&nbsp;&nbsp;
                   <asp:LinkButton ID="lnkUpdate" CommandName="Update" class="btn" ToolTip ="Update" runat="server">
                     <i class="btn-icon-only icon-cog"></i>Update
                   </asp:LinkButton>

                   </EditItemTemplate>
                   
                   <ItemStyle Width="30%"  HorizontalAlign="Center" />
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
