<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TicketDelete.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_TicketDelete" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <link href="css/StyleSheet.css" rel="stylesheet" type="text/css"/>
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
  
       <link href="css/ValidationEngine.css" rel="stylesheet" />
    
     <link href="css/formStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="jvalidation/jquery.min.js"></script>
    <script type="text/javascript" src="jvalidation/jquery.validationEngine-en.js"
        charset="utf-8"></script>
    <script type="text/javascript" src="jvalidation/jquery.validationEngine.js"
        charset="utf-8"></script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">
    
     <script type="text/javascript" >
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
   
    
    <div class="span12">
         <div class="widget ">
            <div class="widget-header">
                <i class="icon-pencil"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>   Ticket Update</b>
                </div>
 

 
    <asp:Panel runat="server" ID="pnlform" width="100%">
    <div class="widget-content">
                <div id="formcontrols" class="form-horizontal">
                    <fieldset> <div class="control-group">
     <label for="firstname" class="control-label">Select Client</label> 
    <div class="controls">
                
                <asp:DropDownList ID="drpclient" runat="server">
                </asp:DropDownList>
              </div> </div>
                        <div class="control-group">
     <label for="firstname" class="control-label">  Select Status</label> 
    <div class="controls"> 
  
   
    <asp:DropDownList id="drpstatus" runat="server" >
              
                </asp:DropDownList>
  </div> </div> 
                        <div class="form-actions">
          <asp:Button ID="btnsubmit" runat="server" Text="Show" class="btn btn-primary"  />
                            </div> </fieldset> </div> </div> 
    
    
     <asp:GridView ID="Gv1" width="100%" runat="server" AutoGenerateColumns="False" ForeColor="Black" GridLines="Vertical" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right" CssClass="table table-striped table-bordered" DataKeyNames="tid" Font-Size="Smaller" Height="145px">
        
            <Columns>
             <asp:TemplateField>
                <HeaderStyle HorizontalAlign="Center" /> 
            <ItemTemplate>
         
            </ItemTemplate>
           <ItemStyle Width="5%" HorizontalAlign="Center" CssClass ="gridchk "/>
           </asp:TemplateField>
           
           
            <asp:TemplateField>
    <HeaderStyle HorizontalAlign="center"  />
    <ItemTemplate>
    <%#Eval("tid")%><br />
     (<%#Eval("adt1")%>)
    </ItemTemplate>
    <ItemStyle Width="10%" HorizontalAlign="center" />
    </asp:TemplateField>
           
            <asp:TemplateField>
                <HeaderStyle HorizontalAlign="Left" /> 
            <ItemTemplate>
          <%#Eval("client")%>
            </ItemTemplate>
            <ItemStyle Width="15%" HorizontalAlign="Left" />
           </asp:TemplateField>
           
       <asp:TemplateField>
    <HeaderStyle HorizontalAlign="Left"  />
    <ItemTemplate>
    <%#Eval("project")%>
    </ItemTemplate>
    <ItemStyle Width="10%" HorizontalAlign="Left" />
    </asp:TemplateField>
    
    <asp:TemplateField>
    <HeaderStyle HorizontalAlign="Left"  />
    <ItemTemplate>
    <%#Eval("Description")%>
    </ItemTemplate>
    <ItemStyle Width="30%" HorizontalAlign="Left" />
    </asp:TemplateField>
    
    <asp:TemplateField>
    <HeaderStyle HorizontalAlign = "Left" />
    <ItemTemplate>
    <%#Eval("remark")%>
    </ItemTemplate>
    <ItemStyle Width="20%" HorizontalAlign = "Left"/>
    </asp:TemplateField>
    
     <asp:TemplateField>
    <HeaderStyle HorizontalAlign = "Left" />
    <ItemTemplate>
    <%#Eval("status")%>
    </ItemTemplate>
    <ItemStyle Width="5%" />
    </asp:TemplateField>
    
    <asp:TemplateField>
        <ItemTemplate>
       <asp:CheckBox ID="CheckBox1" runat="server" />
                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("tsid") %>' />
        </ItemTemplate>
            <ItemStyle Width="5%" HorizontalAlign="Center"/>
        </asp:TemplateField> 
       
       
            </Columns>
              
            <FooterStyle BackColor="#CCCC99" />
         <RowStyle BackColor="#F7F7DE" />
         <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
         <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
         <HeaderStyle HorizontalAlign="Left" BackColor="#6B696B" Font-Bold="True" ForeColor="White" CssClass="DataGridFixedHeader" />
         <AlternatingRowStyle BackColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
           
     
            </asp:Panel>
             <div class="form-actions" style="text-align:center;">
          <asp:Button ID="btndelete" runat="server" class="btn btn-primary" Text="Delete" OnClientClick ="javascript:validateCheckBoxes()" />
                 </div>
          </div> </div> 
 
   </asp:Content>
