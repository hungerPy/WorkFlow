<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="false" CodeFile="TicketFromReport.aspx.vb" Inherits="Admin_TicketFromReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">
    <div class="span12">
        <div class="widget" id="trmsg" runat="server" style="display: none;">
            <div class="widget-header">
                <i class="icon-search"></i>
                <h3>
                    <asp:Literal ID="pagesearch" runat="server"></asp:Literal>
                </h3>
            </div>
            <!-- /widget-header -->

            <div class="widget-content">
                <div class="control-group-search">
                    <label for="firstname" class="control-label">
                        Search By Email Address :</label>
                    <asp:TextBox ID="txtsearch" CssClass="span2" runat="server"></asp:TextBox>
                    <asp:Button ID="imgbtnSearch" class="btn btn-primary" Text="Search" runat="server" />
                    <!-- /controls -->
                </div>
                <!-- /shortcuts -->
            </div>
            <!-- /widget-content -->
        </div>

        <asp:Literal ID="lblmsgs" runat="server" Text=""></asp:Literal>
        <div class="widget widget-table action-table">
            <div class="widget-header">
                <i class="icon-th-list"></i>
                <h3>
                    <asp:Literal ID="pagename" runat="server"></asp:Literal></h3>
             

                <div class="span2 pull-right gotodropdown" style="display: none;">
                    <label for="category" class="control-label">
                    </label>

                </div>
            </div>
            <!-- /widget-header -->
            <div class="widget-content">

<%--**************************************************Paste Data******************************************--%>
                  <table id="tblHeader" width="100%" cellpadding="0" cellspacing="0" border="0" style="display:none;">

                <tr>
                    <td align="center" colspan="2">Ticket Report of 
        <asp:Label ID="lbluser" runat="server"></asp:Label><br />
                        <asp:Label ID="lblstatus" runat="server"></asp:Label>
                    </td>

                </tr>

            </table>

            <asp:Panel ID="pnlform" runat="server" Width="100%">

                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblForm" style="margin-top:10px;">

                    <tr>
                        <td style="width: 20%">&nbsp;Select Status</td>

                        <td width="30%">
                            <asp:DropDownList ID="drpstatus" runat="server">
                            </asp:DropDownList>
                        </td>

                        <td style="width: 20%">&nbsp;Record Per Page</td>

                        <td width="30%">
                            <asp:DropDownList ID="drprecords" runat="server">
                            </asp:DropDownList></td>

                    </tr>

                    <tr>
                        <td width="20%" style="height: 24px">&nbsp;Duration From</td>
                        <td style="width: 30%; height: 24px;">
                            <asp:DropDownList ID="m1" Width="80px" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="d1" Width="50px" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="y1" Width="80px" runat="server">
                            </asp:DropDownList></td>
                        <td width="5%" style="height: 24px">&nbsp;To</td>
                        <td width="45%" style="height: 24px">
                            <asp:DropDownList ID="m2" Width="80px" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="d2" Width="50px" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="y2" Width="80px" runat="server">
                            </asp:DropDownList></td>
                    </tr>


                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnsearch" runat="server" Text="Search" /></td>
                    </tr>

                </table>
            </asp:Panel>

        </div>

        <asp:GridView ID="Gv1" Width="100%" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" DataKeyNames="tsid" Font-Size="Smaller" AllowPaging="True">

            <Columns>

                <asp:TemplateField HeaderText="S.No.">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                    </ItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="TicketNo">
                    <ItemTemplate>
                        <%#Eval("tid")%><br />
                        (<%#Eval("adt1")%>)

                    </ItemTemplate>

                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Project">
                    <ItemTemplate>
                        <%#Eval("project")%>
                    </ItemTemplate>

                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <%#Eval("Description")%>
                    </ItemTemplate>

                    <ItemStyle Width="35%" HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>



                <asp:TemplateField HeaderText="Completion_Date">
                    <ItemTemplate>
                        <%#Eval("cdate1")%>
                    </ItemTemplate>

                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Remark">
                    <ItemTemplate>
                        <%#Eval("remark")%>
                    </ItemTemplate>

                    <ItemStyle Width="25%" HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <%#Eval("status")%>
                    </ItemTemplate>

                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>



            </Columns>

            <FooterStyle BackColor="#CCCC99" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <HeaderStyle HorizontalAlign="Left" BackColor="#6B696B" Font-Bold="True" ForeColor="White" CssClass="DataGridFixedHeader" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>

        <span style="text-decoration: underline"></span>
             
            </div>
            <div class="form-actions" style="display: none;">
                <asp:Button ID="btndel" Text="Delete" CssClass="btn" runat="server" OnClientClick="javascript:return ItemSelect();" />&nbsp;&nbsp;&nbsp; <a href="add-adminuser.aspx" class="btn btn-primary">Add New</a>
            </div>
            <!-- /widget-content -->
        </div>
        <!-- /widget -->
        <!-- /widget -->
   
</asp:Content>

