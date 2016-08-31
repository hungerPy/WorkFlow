<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pastemplisiting.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_pastemplisiting" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <title>Past Employee Listing</title>
    <link rel="icon" href="images/hslogo.jpg" type="image/x-icon" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">

    <script language="javascript" src="functions.js" type="text/javascript">function tblForm_onclick() {

}

    </script>
    <style type="text/css">
        .DataGridFixedHeader {
            POSITION: relative;
            ;
            TOP: expression(this.offsetParent.scrollTop-2);
            BACKGROUND-COLOR: white;
        }
    </style>

    <div class="span12">
        <b>

            <asp:Label ID="lblHead" runat="server" Style="margin-left: 50%" Text="Past Employee Listing"></asp:Label>
            <div class="widget widget-table action-table">
                <div class="widget-header">
                    <i class="icon-th-list"></i>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select Company</b>
        <asp:DropDownList ID="drpcompany" ToolTip="Company" runat="server" Style="margin-left: 20px" AutoPostBack="True"></asp:DropDownList>
        <asp:Label ID="lblError" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
    </div>
    </div>
    <div class="widget-content">
        <asp:GridView ID="gvPlots" runat="server" AllowPaging="True" PagerStyle-CssClass="paging-link" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" Width="100%" Font-Size="Smaller" Font-Names="Arial" DataKeyNames="empid">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%--<%#Eval("sno")%>--%>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                    <ItemStyle Width="3%" HorizontalAlign="Center" CssClass="gridchk" />
                </asp:TemplateField>
                <asp:TemplateField>

                    <ItemTemplate>
                        <asp:Image ID="images3" runat="server" ImageUrl='<%#String.Concat("~\Photos\", Eval("photo"))  %> ' BorderWidth="0px" Height="60px" Width="60px" />
                    </ItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%#Eval("dirname")%>
                    </ItemTemplate>
                    <ItemStyle Width="15%" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%#Eval("Designation")%>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <%#Eval("division")%>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <%#Eval("contact Details")%>
                    </ItemTemplate>
                    <ItemStyle Width="20%" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Details">
                    <ItemTemplate>
                        <a href='empprintappdetails1.aspx?empid=<%#Eval("empid")%>' target="_blank">Details</a>
                        <%-- <asp:LinkButton ID="lnkUpdate" CommandName="update" runat="server">
              <asp:Image ID="Image1" ImageUrl="~/Images/EDIT.GIF" runat="server"  ToolTip="" />
              </asp:LinkButton>--%>
                        <%--  <asp:LinkButton ID="lnkDelete" CommandName="Delete" runat="server" OnClientClick="return confirm('Do you really want to delete this row?')">
              <asp:Image ID="Image2" ImageUrl="~/Images/wrong.GIF" runat="server" />
              </asp:LinkButton>--%>
                    </ItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <%#Eval("empid")%>
                    </ItemTemplate>
                    <ItemStyle Width="0%" HorizontalAlign="Center" />
                </asp:TemplateField>

            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" CssClass="DataGridFixedHeader" />
            <AlternatingRowStyle BackColor="White" />
            <EmptyDataTemplate>
                <center>No Data Available</center>
            </EmptyDataTemplate>
        </asp:GridView>

    </div>
    </div>
             
      <span style="display: none">
          <asp:Button ID="Button1" runat="server" Text="Button" />
      </span>
</asp:Content>

