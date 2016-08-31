<%@ Page Language="VB" AutoEventWireup="false" CodeFile="supplierListing.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_supplierListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">

    <title>Supplier Listing</title>
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server"> 

      <script language="javascript" src="functions.js" type="text/javascript">function tblForm_onclick() {

}

</script>
      <style type="text/css">
    .DataGridFixedHeader { POSITION: relative;; TOP: expression(this.offsetParent.scrollTop-2); BACKGROUND-COLOR: white }

</style>
 <div class="span12">
        <div class="widget" id="trmsg" runat="server" >
         <b>
           

        <asp:Label ID="lblHead" runat="server" style="margin-left:50%"   Text="Supplier/Vendor Listing"></asp:Label>
      
         </b> 
          
      <div class="widget-content">
        <asp:GridView ID="gvPlots" runat="server" AllowPaging="true" PagerStyle-CssClass="paging-link" CssClass="table table-striped table-bordered" AutoGenerateColumns="false" Width="100%" Font-Size="Smaller" Font-Names="Arial" DataKeyNames="companyid">
        <Columns>
        <asp:TemplateField>
        <ItemTemplate>
        <%#Eval("sno")%>
        </ItemTemplate>
        <ItemStyle Width="3%" HorizontalAlign="center" CssClass="gridchk"/>
        </asp:TemplateField>
        <asp:TemplateField>
        <ItemTemplate>
        <%#Eval("SubCategory")%>
        </ItemTemplate>
        <ItemStyle Width="15%" HorizontalAlign="left"/>
        </asp:TemplateField>
         <%--<asp:TemplateField>
                <ItemTemplate>
        <%#Eval("recno")%>
        </ItemTemplate>
            <ItemStyle Width="7%" HorizontalAlign="center"/>
        </asp:TemplateField>--%>
        <%--<asp:TemplateField>
        <ItemTemplate>
        <asp:Image ID="images" runat="server" ImageUrl='<%# Eval("logo","~/logos/{0}") %>' BorderWidth="0px" Height="60px" Width="60px" />
        </ItemTemplate>
        <ItemStyle Width="5%" HorizontalAlign="left"/>
        </asp:TemplateField>--%>
        <asp:TemplateField>
        <ItemTemplate>
        <%#Eval("compname")%>
        </ItemTemplate>
        <ItemStyle Width="15%" HorizontalAlign="left"/>
        </asp:TemplateField>
         <asp:TemplateField>
        <ItemTemplate>
        <%#Eval("contname")%>
        </ItemTemplate>
        <ItemStyle Width="15%" HorizontalAlign="left"/>
        </asp:TemplateField>
        <asp:TemplateField>
        <ItemTemplate>
        <%#Eval("telephoneNo")%>
        </ItemTemplate>
        <ItemStyle Width="20%" HorizontalAlign="left" />
        </asp:TemplateField>
       <%-- <asp:TemplateField>
        <ItemTemplate>
        <%#Eval("Address")%>
        </ItemTemplate>
        <ItemStyle Width="20%" HorizontalAlign="left" />
        </asp:TemplateField>--%>
        
        <asp:TemplateField HeaderText="Details" > 
        <ItemTemplate>
        <a href='Supplierprintappdetails1.aspx?companyid=<%#Eval("companyid")%>' target="_blank" >Details</a>  
            <br />
        <asp:LinkButton ID="lnkUpdate" CommandName="update" CssClass ="btn btn-warning " runat="server">
        <i class="btn-icon-only icon-cog"></i>Edit
        </asp:LinkButton>
        <asp:LinkButton ID="lnkDelete" CommandName="Delete" runat="server" CssClass ="btn btn-danger " OnClientClick="return confirm('Do you really want to delete this row?')">
    <i class="btn-icon-only  icon-trash "></i>Delete
        </asp:LinkButton>  
        </ItemTemplate>
        <ItemStyle Width="10%" HorizontalAlign="center"/>
        </asp:TemplateField>
        <asp:TemplateField Visible="false" > 
        <ItemTemplate>
        <%#Eval("companyid")%>
        </ItemTemplate>
        <ItemStyle Width="0%" HorizontalAlign="center"/>
        </asp:TemplateField>
        <%--<asp:TemplateField HeaderText="Edit" >
        <HeaderStyle HorizontalAlign="Center" />
        <ItemTemplate>
        
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="middle" Width="5%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Delete">
        <ItemTemplate>
        
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
        </asp:TemplateField>--%>
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="center" />
               <PagerSettings Visible="true" Position="Bottom" Mode="NextPreviousFirstLast" FirstPageText="First"
                        LastPageText="Last" NextPageText="Next" PreviousPageText="Prev" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" CssClass="DataGridFixedHeader" />
        <AlternatingRowStyle BackColor="White" />
        <EmptyDataTemplate><center>No Data Available</center></EmptyDataTemplate>  
        </asp:GridView>
              </div>   </div> 
    </div>
      <span style="display:none">
 <asp:Button ID="Button1" runat="server" Text="Button" />
 </span>
   </asp:Content>
