<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="adminuser.aspx.vb" Inherits="Admin_adminuser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server"> 

    <asp:HiddenField ID="hdSortdirection" runat="server" />
    <!--  start page-heading -->
    <div class="span12">
        <div class="widget" id="trmsg" runat="server" style="display:none;">
            <div class="widget-header" >
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
                    <asp:Button ID="imgbtnSearch" class="btn btn-primary" Text="Search" runat="server"
                        OnClick="imgbtnSearch_Click" />
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
                <a href="add-adminuser.aspx" class="btn btn-primary">Add User</a>
                <a href="EmpRegistration.aspx" class="btn btn-primary">Add Employee</a>
                
                
                <div class="span2 pull-right gotodropdown" style="display:none;">
                    <label for="category" class="control-label">
                        Goto Page
                    </label>
                    <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <!-- /widget-header -->
            <div class="widget-content">
                <asp:GridView ID="GVMenu" runat="server" CssClass="table table-striped table-bordered"
                    GridLines="None" DataKeyNames="uid" PagerStyle-CssClass="paging-link" AutoGenerateColumns="false"
                    ShowFooter="false" AllowPaging="True" PagerStyle-HorizontalAlign="Right" Width="100%"
                    AllowSorting="true" OnSorting="GVMenu_Sorting" OnPageIndexChanging="GVMenu_PageIndexChanging"
                    OnRowDataBound="GVMenu_RowDataBound">
                    <AlternatingRowStyle CssClass="alternate-row" />
                    <EmptyDataRowStyle CssClass="repeat Required" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        No record found for Admin User.
                    </EmptyDataTemplate>
                    <Columns>
                            <asp:TemplateField>
                            <HeaderStyle CssClass="gridchk" /><ItemStyle CssClass="gridchk" />
                            <HeaderTemplate>S.No.
                                <%--<asp:CheckBox ID="chkHead" CssClass="headercheckbox" onclick="javascript:SelectAllChk();" runat="server"></asp:CheckBox>--%></HeaderTemplate>
                            <ItemTemplate>
                                <%--<asp:CheckBox ID="chkDel" CssClass="innercheckbox" onclick="javascript:UnSelectMainChk();" runat="server"></asp:CheckBox>--%>

                            </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>

                        <asp:TemplateField HeaderText="Employee Name">
                            <ItemTemplate>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="UserName">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate><%#Eval("userid")%></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <HeaderStyle CssClass="gridstatus" /><ItemStyle CssClass="gridstatus" />
                            <ItemTemplate>
                                <%#Eval("status")%>
                                <%--<asp:LinkButton ID="lnkstatus" OnClick="lnkStatus_click" runat="server" Text='<%# Convert.ToInt32(Eval("isActive"))%>'
                                    CommandArgument='<%# Convert.ToInt32(Eval("isActive"))%>'></asp:LinkButton>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <%--  <asp:TemplateField HeaderText="User Type">
                            <ItemTemplate>
                                <asp:Literal ID="ltruserid" runat="server" Text='<%#Eval("uid")%>' Visible="false" />
                                <%--<asp:Literal ID="ltradmintype" runat="server" Text='<%# Eval("adminType") %>' />--%>
                        <%--    </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="70">
                            <HeaderStyle CssClass="td-actions" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <a href="add-adminuser.aspx?mode=edit&id=<%# Eval("uid")%>&empid=<%#Eval("empid") %>" class="btn" title="Edit">
                                        <i class="btn-icon-only icon-cog"></i>Edit</a></a>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Visible="true" Position="Bottom" Mode="NextPreviousFirstLast" FirstPageText="First"
                        LastPageText="Last" NextPageText="Next" PreviousPageText="Prev" />
                </asp:GridView>
            </div>
            <div class="form-actions">
               <center> <asp:Button ID="btndel" Style="display:none;" Text="Delete" CssClass="btn" Visible="false"  runat="server" OnClientClick="javascript:return ItemSelect();"
                    OnClick="btndel_Click" />&nbsp;&nbsp;&nbsp; <%--<a href="add-adminuser.aspx" class="btn btn-primary">
                        Add User</a></center>--%>
            </div>
            <!-- /widget-content -->
        </div>
        <!-- /widget -->
        <!-- /widget -->
    </div>
</asp:Content>

