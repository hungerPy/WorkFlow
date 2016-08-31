<%@ Page Language="VB" AutoEventWireup="false" CodeFile="work_assignment_listing.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_work_assignment_listing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <title>Client PO Detail</title>
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">
   
  <div class="span12">
        <div class="widget" id="trmsg" runat="server" >
         <b>
           
            <asp:Label ID="lblHead" runat="server" style="margin-left:50%"  Text="Client PO  Details"></asp:Label>
       </b>
          <div class="widget-content">
        
            <asp:GridView ID="gvPlots" runat ="server" AllowPaging="true" PagerStyle-CssClass="paging-link" CssClass="table table-striped table-bordered"  AutoGenerateColumns="false" Width="100%" DataKeyNames="poWorkAssignID"  >
        
           <Columns>
        <asp:TemplateField>
        <ItemTemplate>
        <%#Eval("sno")%>
        </ItemTemplate>
        <ItemStyle Width="3%" HorizontalAlign="center" CssClass="gridchk"/>
        </asp:TemplateField>
       
        <asp:TemplateField>
                
                <ItemTemplate>
                       <%#Eval("poDate")%>
                </ItemTemplate>
                <ItemStyle Width="10%" HorizontalAlign="Left"/>
            </asp:TemplateField>
        
        
        <asp:TemplateField>
        <ItemTemplate>
        <%#Eval("poRecivingDate")%>
        </ItemTemplate>
        <ItemStyle Width="10%" HorizontalAlign="left"/>
        </asp:TemplateField>
        <asp:TemplateField>
        <ItemTemplate>
        <%#Eval("Clientid")%>
        </ItemTemplate>
        <ItemStyle Width="25%" HorizontalAlign="left"/>
        </asp:TemplateField>
        <asp:TemplateField>
        <ItemTemplate>
        <%#Eval("dcode")%>
        </ItemTemplate>
        <ItemStyle Width="8%" HorizontalAlign="left" />
        </asp:TemplateField>
      <asp:TemplateField>
        <ItemTemplate>
        <%#Eval("serviceid")%>
        </ItemTemplate>
        <ItemStyle Width="15%" HorizontalAlign="left" />
        </asp:TemplateField>
       

       
       
        <%--<asp:TemplateField>
        <ItemTemplate>
        <%#Eval("poRemarks")%>
        </ItemTemplate>
        <ItemStyle Width="20%" HorizontalAlign="Left" />
        </asp:TemplateField>--%>
        
         <asp:TemplateField>
        <ItemTemplate>
        <%#Eval("wrkCmpletTrgtDate")%>
        </ItemTemplate>
        <ItemStyle Width="7%" HorizontalAlign="Left" />
        </asp:TemplateField>
        
        
        
                <asp:TemplateField Visible="false" > 
        <ItemTemplate>
        <%#Eval("totalAmnt")%>
        </ItemTemplate>
        <ItemStyle Width="0%" HorizontalAlign="center"/>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="" >
        <HeaderStyle HorizontalAlign="Center" />
        <ItemTemplate>
         <%#Eval("deriables")%>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="middle" Width="10%" />
        </asp:TemplateField>
             
        <%--<asp:TemplateField HeaderText="Details" > 
        
        <ItemTemplate>
        <a href='printappdetails1.aspx?dirid=<%#Eval("dirid")%>' target="_blank" >Details</a>  
        <asp:LinkButton ID="lnkUpdate" CommandName="update" runat="server">
        <asp:Image ID="Image1" ImageUrl="~/Images/EDIT.GIF" runat="server"  ToolTip="" />
        </asp:LinkButton>
        <asp:LinkButton ID="lnkDelete" CommandName="Delete" runat="server" OnClientClick="return confirm('Do you really want to delete this row?')">
        <asp:Image ID="Image2" ImageUrl="~/Images/wrong.GIF" runat="server" />
        </asp:LinkButton>  
        </ItemTemplate>
        <ItemStyle Width="6%" HorizontalAlign="center"/>
        </asp:TemplateField>--%>
        <%--<asp:TemplateField HeaderText="Delete">
        <ItemTemplate>
        
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
        </asp:TemplateField>--%>
        
           <asp:TemplateField HeaderText="Details" > 
        <ItemTemplate>
        <a href='printClientPoDetails1.aspx?poWorkAssignID=<%#Eval("poWorkAssignID")%>' target="_blank" >Details</a>  
        <asp:LinkButton ID="lnkUpdate" CommandName="update" runat="server" CssClass ="btn btn-warning ">
       <i class="btn-icon-only icon-cog">Edit</i>
        </asp:LinkButton>
        <asp:LinkButton ID="lnkDelete" CommandName="Delete" runat="server" CssClass ="btn btn-danger " OnClientClick="return confirm('Do you really want to delete this row?')">
       <i class="btn-icon-only icon-trash">Delete</i>
        </asp:LinkButton>  
        </ItemTemplate>
        <ItemStyle Width="10%" HorizontalAlign="center"/>
        </asp:TemplateField>
       
        </Columns>
         <FooterStyle BackColor="#CCCC99" />
         <RowStyle BackColor="#F7F7DE" />
         <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
         <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="center" />
         <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" CssClass="DataGridFixedHeader" />
         <AlternatingRowStyle BackColor="White" />
         <EmptyDataTemplate><center>No Data Available</center></EmptyDataTemplate>    
        
        
        </asp:GridView>
           </div> </div> </div>    
        
      
    </asp:Content>
