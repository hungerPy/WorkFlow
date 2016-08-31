<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="CategoryReport.aspx.vb" Inherits="CategoryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <link rel="icon" href="Images/hslogo.jpg" type="image/x-icon"/>
    <link href="jvalidation/ValidationEngine.css" rel="stylesheet" type="text/css"  />
    <link href="css/formStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="jvalidation/jquery.min.js"></script>
<script type="text/javascript" src="jvalidation/jquery.validationEngine-en.js" charset="utf-8"></script>
<script type="text/javascript" src="jvalidation/jquery.validationEngine.js" charset="utf-8"></script>
        

      <script type="text/javascript">
          function CName(txtCatName, str) {
              var str1 = str.split(" ").map(function (i) { return i[0].toUpperCase() + i.substring(1) }).join(" ");
              document.getElementById(txtCatName).value = str1;
          }

          function SSCName(txtSSCatName, str) {
              var str1 = str.split(" ").map(function (i) { return i[0].toUpperCase() + i.substring(1) }).join(" ");
              document.getElementById(txtSSCatName).value = str1;
          }

          </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">

   <script type="text/javascript">
       
       $(function () {
           $('[id*=btnSubmit]').bind("click", function () {
               $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
           });
           $('[id*=btnCback]').bind("click", function () {
               $("#adminform").validationEngine('detach');
           });

           $('[id*=btnSSSubmit]').bind("click", function () {
               $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
           });
           $('[id*=btnSSback]').bind("click", function () {
               $("#adminform").validationEngine('detach');
           });
       });

       function DateFormat(field, rules, i, options) {
           var regex = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
           if (!regex.test(field.val())) {
               return "Please enter date in dd/MM/yyyy format."
           }
       }


       function drpCat(field, rules, i, options) {
           if ($('[id$=drpccat] option:selected').val() == "0") {
               return "This Feild Required."
           }
       }


       function drpCat1(field, rules, i, options) {
           if ($('[id$=drpSSCat] option:selected').val() == "0") {
               return "This Feild Required."
           }
       }

       function drpSCat(field, rules, i, options) {
           if ($('[id$=drpSubCat] option:selected').val() == "0") {
               return "This Feild Required."
           }
       }
            </script>


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
                    <asp:Button ID="imgbtnSearch" class="btn btn-primary" Text="Search" runat="server"/>
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
                <a href="ProductMaster.aspx" class="btn btn-primary">Add Product</a>
                <a href="MasterReport.aspx" class="btn btn-primary">Add Section/Counter</a>
                <a href="TaxMaster.aspx" class="btn btn-primary">Add Tax</a>
                <a href="UomMaster.aspx" class="btn btn-primary">Add UOM</a>
                
                <div class="span2 pull-right gotodropdown" style="display:none;">
                    <label for="category" class="control-label">
                       
                    </label>
                  
                </div>
            </div>
            <!-- /widget-header -->
            <div class="widget-content">
                   <table id="tblHeader" width="100%" border="0" style="display:none;">
     <tr>
     <td align="center"><asp:Label ID="lblheader" runat="server" Text="Product Category"></asp:Label></td>
     </tr>
     </table>

                 <asp:Panel ID="pnlcategory" runat="server" Width="100%" Style="margin-top:10px;">
        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="Table1" >
            <tr>
                <td width="20%" class="fontsize">Category Name</td>
                <td><asp:DropDownList ID="drpccat"  runat="server" ToolTip="Category" CssClass="design_drpbig validate[required,funcCall[drpCat[]]"></asp:DropDownList> <br /><br /></td>
            </tr>
            <tr>
                <td class="fontsize">Sub Category Code</td>
                <td><asp:TextBox ID="txtCatCode" CssClass="design_form validate[required]"  placeholder="Sub Category Code" runat="server" ToolTip="Code"  MaxLength="20"></asp:TextBox><br /><br /></td>
            </tr>
            <tr>
                <td class="fontsize">Sub Category Name</td>
                <td><asp:TextBox ID="txtCatName" CssClass="design_form validate[required]" runat="server" placeholder="Sub Category Name" ToolTip="Name"  MaxLength="40" onkeyup="javascript:CName(this.id, this.value)"></asp:TextBox><br /><br /></td>
            </tr>
            <tr>
                <td class="fontsize">Sub Category Status</td>
                <td><asp:DropDownList ID="drpFlg" runat="server" ToolTip="Flag" CssClass="design_drpsm" >
                <asp:ListItem Value="0">Active</asp:ListItem>
                <asp:ListItem Value="1">Inactive</asp:ListItem>
                </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red" Text="Label"></asp:Label></td>
            </tr>
            <tr>
         
                <td></td>
                <td >
                         
                <asp:Button ID="btnSubmit" runat="server"  Text="Create Sub Category"/>
                <asp:Button ID="btnCback" runat="server" Text="Back"  />
                
                </td>
                
            </tr>
        </table>
     </asp:Panel>
     <asp:Panel ID="pnlsubcat" runat="server" Width="100%" Style="margin-top:10px;">
        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="Table2" >
            <tr>
                <td width="20%" class="fontsize">Category</td><td><asp:DropDownList ID="drpSSCat" CssClass="design_drpbig validate[required,funcCall[drpCat1[]]" runat="server" AutoPostBack="true"  ToolTip="Category"></asp:DropDownList><br /><br /></td>
            </tr>
            <tr>
                <td class="fontsize">Sub Category</td><td><asp:DropDownList ID="drpSubCat" runat="server" CssClass="design_drpbig validate[required,funcCall[drpSCat[]]" ToolTip="Sub Category" ></asp:DropDownList><br /><br /></td>
            </tr>
            <tr>
                <td class="fontsize">Sub Sub Category Code</td><td><asp:TextBox ID="txtSSCatCode"  runat="server" ToolTip="Code"  MaxLength="25" CssClass="design_form validate[required]"  placeholder="Sub Sub Category code"></asp:TextBox><br /><br /></td>
            </tr>
            <tr>
                <td class="fontsize">Sub Sub Category Name</td><td><asp:TextBox ID="txtSSCatName" runat="server" ToolTip="Name" BorderStyle="Groove" MaxLength="40" placeholder="Sub Sub category Name"  CssClass="design_form validate[required]" onkeyup="javascript:SSCName(this.id, this.value)"></asp:TextBox><br /><br /></td>
            </tr>
            <tr>
                <td class="fontsize">Sub Sub Category Status</td>
                <td><asp:DropDownList ID="drpSSFlg" runat="server" ToolTip="Flag" BorderStyle="Groove" CssClass="design_drpsm">
                <asp:ListItem Value="0">Active</asp:ListItem>
                <asp:ListItem Value="1">Inactive</asp:ListItem>
                </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                <asp:Label ID="lblSSError" runat="server" Font-Bold="True" ForeColor="Red" Text="Label"></asp:Label></td>
            </tr>
            <tr>
            
                <td></td>
                <td><asp:Button ID="btnSSSubmit" CssClass="buttonstyle"  runat="server"  Text="Create Sub-Sub Category"/>
                <asp:Button ID="btnSSback" runat="server" CssClass="buttonstyle" Text="Back" /></td>
            
            </tr>
        </table>
     </asp:Panel>
     <asp:Panel ID="pnlshow" runat="server" Width="100%" Style="margin-top:10px;">
      <table id="tblParty"  width="100%" border="0" cellpadding="0" cellspacing="0"  >
          <tr>
            
                  <td class="fontsize" width="20%">Select Category</td>
                  <td align="left" width="30%">
                      <asp:DropDownList ID="drpcat" runat="server" AutoPostBack="true" CssClass="design_drpbig" ToolTip="Select Head" Width="150px">
                      </asp:DropDownList>
                  </td>
                  <td align="left" width="50%">
                      <asp:Button ID="btnmodify" runat="server" CssClass="buttonstyle" Text="Add Sub Category" Visible="false" Width="180px" />
                  </td>
            
          </tr>
     </table>
     <table width="100%" border="0" id="tblForm" >
     <tr>
     <td>
     <asp:GridView ID="GVsubcategory" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="SCatId" RowStyle-BackColor="blanchedAlmond">
     <Columns>
     <asp:TemplateField HeaderText="SNo">
     <ItemTemplate>
     <%#Eval("SCatCode")%>
     </ItemTemplate>
     <ItemStyle Width="10%" HorizontalAlign="Center" />
     </asp:TemplateField>
    <asp:TemplateField HeaderText="Sub Category Name" HeaderStyle-HorizontalAlign="Left">
     <ItemTemplate>
      <%#Eval("SCatName")%>
     </ItemTemplate>
     <ItemStyle Width="55%" HorizontalAlign="left" />
     </asp:TemplateField>

     <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
     <ItemTemplate>
     <img src="icons/ball_blueS.gif" />
     <asp:LinkButton ID="lnkEdit" runat="server" ToolTip="Add Sub Sub Category" CommandName="Edit" Text="Add Sub Sub Category">
     <%--<img src="../Images/add1.gif" alt="Add Sub Sub Category" style="border:0;"/>--%>
     </asp:LinkButton>
    <%-- <a href="#" onclick="showsubcat()">
     <img src="../Images/Add1.gif" alt="Add Sub Category" style="border:0;"/></a>--%>
     <img src="Images/ddown.jpg" alt="View Ledger" style="display:none; border:0;cursor:hand;" onload="openClose('tbl<%# Eval("SCatId")%>');" onclick="openClose('tbl<%# Eval("SCatId")%>');"/>
     <%--<asp:LinkButton ID="lnkAdd" runat="server" ToolTip="Add Category" >
     <img src="../Images/AddPartner.png" alt="Add Category" style="border:0;"/>
     </asp:LinkButton>--%>
     
     </td></tr>
        <tr>
        <td colspan="4">
        <table width="100%" id='tbl<%# Eval("SCatId")%>' cellpadding="0" cellspacing="0" style="font-size:10pt;font-family:Verdana;text-align:justify;" border="1" bordercolor="black">
             <tr>
             <td>
        <asp:GridView ID="GVsubsubcategory" runat="server" AutoGenerateColumns="false" DataKeyNames="SScatId" Width="100%"  RowStyle-BackColor="blanchedAlmond" AlternatingRowStyle-BackColor="white">
        <Columns>
        <asp:TemplateField HeaderText="">
        <ItemTemplate>
        
        </ItemTemplate>
        <ItemStyle Width="10%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="SNo">
        <ItemTemplate>
        <%#Eval("SSCatCode")%>
        </ItemTemplate>
        <ItemStyle Width="5%" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Sub Sub Category Name" HeaderStyle-HorizontalAlign="Left">
        <ItemTemplate>
         <%#Eval("SSCatName")%>
        </ItemTemplate>
        <ItemStyle Width="30%" HorizontalAlign="left" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
        <ItemTemplate>
        <img src="icons/ball_redS.gif" alt="" />
        <%--<a href="javascript:Add_Member=window.open('productmaster.aspx?id=<%#eval("SScatId") %>','Add_Member','width=800,height=500,top=170,left=210,scrollbars=yes');Add_Member.focus();">Add</a>--%>
            <a href="productmaster.aspx?id=<%#eval("SScatId") %>">Add</a>
        <a href="Traverse.aspx?id=<%#eval("SScatId") %>">View</a>
        <asp:TextBox ID="txtId" runat="server" Text='<%#Eval("SScatId")%>' Visible="false"></asp:TextBox>
        </ItemTemplate>
        <ItemStyle Width="10%" />
        </asp:TemplateField>
        </Columns>
          <EmptyDataTemplate><center><span style="color:Red; font-weight:bold">No Data Available</span></center>
          </EmptyDataTemplate>
  <HeaderStyle HorizontalAlign="center" />
  <HeaderStyle backcolor="BURLYWOOD" HorizontalAlign="center" />
  <RowStyle Font-Names="Segoe UI" />
        </asp:GridView>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        
     </ItemTemplate>
     <ItemStyle Width="35%" HorizontalAlign="Right" />
     </asp:TemplateField>
     </Columns>
       <EmptyDataTemplate><center><span style="color:Red; font-weight:bold">No Data Available</span></center></EmptyDataTemplate>
  <HeaderStyle HorizontalAlign="center" />
  <HeaderStyle backcolor="BURLYWOOD" HorizontalAlign="center" />
  <RowStyle Font-Bold="true" Font-Names="Segoe UI" />
     </asp:GridView>
     </td>
     </tr>
     <tr>
     <td>
     <asp:Label ID="lblMessage" runat="server" ForeColor="red" Visible="false" Font-Bold="true"></asp:Label>
     </td>
     </tr>
     </table>
     </asp:Panel>
            </div>
            <div class="form-actions" style="display:none;">
                <asp:Button ID="btndel" Text="Delete" CssClass="btn" runat="server" OnClientClick="javascript:return ItemSelect();"/>&nbsp;&nbsp;&nbsp; <a href="add-adminuser.aspx" class="btn btn-primary">
                        Add New</a>
            </div>
            <!-- /widget-content -->
        </div>
        <!-- /widget -->
        <!-- /widget -->
    </div>
</asp:Content>

