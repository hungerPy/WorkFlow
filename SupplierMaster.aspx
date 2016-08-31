<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SupplierMaster.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_SupplierMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <title>Supplier Registration</title>
    <script language="javascript" src="functions.js" type="text/javascript"></script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server"> 

   <script language="javascript" type="text/javascript">
   function copy_data(val){
     var a = document.getElementById(val.id).value
     document.getElementById("txtaccname").value=a
    }
    </script>
 
   
    <div>
     <table id="tblHeader" width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:<%=db.headerBG%>;color:<%=db.headerColor%>;font-family:<%=db.headerText%>;font-weight:<%=db.headerWeight%>;font-style:<%=db.headerStyle%>;text-decoration:<%=db.headerDecoration%>;font-size:<%=db.headerSize%>px;text-align:<%=db.headerAlignment%>">
        <tr >
         <td>Supplier/Vendor Registration<asp:Label ID="lblid" runat="server" Visible="false" Text="" ></asp:Label><asp:TextBox ID="txtid" runat="server" Visible="false" ></asp:TextBox></td>
        </tr>
        </table>
        
        <asp:Panel ID="pnlmain" runat="server" Width="100%"> 
        <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblForm" style="background-color:<%=db.formBG%>;color:<%=db.formColor%>;font-family:<%=db.formText%>;font-weight:<%=db.formWeight%>;font-style:<%=db.formStyle%>;text-decoration:<%=db.formDecoration%>;font-size:<%=db.formSize%>px;text-align:<%=db.formAlignment%>">
       <%-- <tr> 
            <td style="height:25pt;font-size:11pt;padding-left :10px;">Select Company Name </td><td colspan="3" style="height:25pt;font-size:11pt;padding-left :10px;"><asp:DropDownList ID="drpCompanyName" runat="server" ToolTip="Company Name" AccessKey="" AutoPostBack="True" Height="20" Font-Size="11pt" Font-Bold="true"></asp:DropDownList></td>
        </tr>--%>
        <tr>
            <td width="25%" valign="middle" style="padding-left:10px;">Date of Regt.</td>
            <td style="padding-left :10px;" colspan="3"><asp:DropDownList ID="day"  ToolTip="Day" runat="server">
            </asp:DropDownList><asp:DropDownList ID="month" ToolTip="Month" runat="server">
            </asp:DropDownList><asp:DropDownList ID="year"  ToolTip="Year" runat="server">
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 25%;padding-left :10px;">Type of Supplier*</td>
            <td colspan="3" style="width: 25%;padding-left :10px;"><asp:DropDownList ID="drpcategory" runat="server" ToolTip="Supplier Type" AutoPostBack="true" ></asp:DropDownList>
                 <asp:Button ID="btncat" runat="server" Text="Add Category" /><asp:Button ID="btnsubcat" runat="server" Text="Add Sub Category" />
                 <asp:Label runat="server" id="lblmsg" Visible="false" Font-Bold="true" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 25%;padding-left :10px;"><asp:Label runat="server" id="lblshowcat" Text="Category Name" Visible="false" ></asp:Label></td>
            <td colspan="3" style="padding-left :10px;"><asp:TextBox ID="txtCatName" runat="server" ToolTip="Name" BorderStyle="Groove" Visible="false"></asp:TextBox>
            <asp:Button ID="btnaddcat" runat="server" Text="Submit" Visible="false"/>
            </td>
        </tr>
        <tr>
            <td style="width: 25%;padding-left :10px;"><asp:Label runat="server" id="lblshowsubcat" Text="Sub-Category Name" Visible="false" ></asp:Label></td>
            <td colspan="3" style="padding-left :10px;"><asp:TextBox ID="txtsubcatname" runat="server" ToolTip="Name" BorderStyle="Groove" Visible="false"></asp:TextBox>
            <asp:Button ID="btnaddsubcat" runat="server" Text="Submit" Visible="false"/>
            </td>
        </tr>
        <tr>
            <td style="width: 25%;padding-left :10px;" valign="top"><asp:Label runat="server" id="lblsubcat" Text="Select Category" Visible="false" ></asp:Label></td>
            <td colspan="3" style="padding-left :10px;"><asp:CheckBoxList ID="chkSubCat" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" Width="100%" Visible="false"></asp:CheckBoxList>
             </td>
        </tr>
        <tr>
           <td width="25%" style="padding-left :10px;">Company/Vendor Name*</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtCompanyName" runat="server"  ToolTip="Company Name" onkeyup="copy_data(this)"></asp:TextBox></td>
           <td width="25%" style="width: 25%;padding-left :10px;">Company/Vendor Abbreviation</td>
           <td  style="padding-left :10px;"><asp:TextBox ID="txtCompanyAbbreviation" runat="server" ToolTip="Company Abbreviation"></asp:TextBox></td>
        </tr>
        <tr>
           <td width="25%" style="padding-left :10px;">Contact Person</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtContactP" runat="server" ToolTip="Contact Person" ></asp:TextBox></td>
           <td width="25%" style="width: 25%;padding-left :10px;">Designation</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtdegi" ToolTip="Degination" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
        <td width="25%" style="padding-left :10px;">PAN No.*</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtPanNo" runat="server" ToolTip="Pan No." ></asp:TextBox></td>
        <td width="25%"  valign="middle" style="padding-left:10px;">Company Type</td>
        <td valign="middle" style="padding-left:10px;"><asp:DropDownList ID="drpcomptype" runat="server" ToolTip="Company Type"></asp:DropDownList></td>
        </tr>
        <tr >
           <td width="25%" style="height: 62px;padding-left :10px;" >Address</td>
           <td colspan="3" style="height: 62px;padding-left :10px;"><asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ToolTip="Company Addess" Width="366px" ></asp:TextBox>
           <br />
              Country<asp:DropDownList ID="drpCountry" runat="server" ToolTip="Country" AutoPostBack="true"></asp:DropDownList>
              State<asp:DropDownList ID="drpState" runat="server" ToolTip="State" AutoPostBack="true"></asp:DropDownList>
              City<asp:DropDownList ID="drpCity" runat="server" ToolTip="City" ></asp:DropDownList></td>
        </tr>
        <tr>
            <td width="25%" valign="middle" style="padding-left:10px;">PIN No.</td>
            <td valign="middle" colspan="3" style="padding-left:10px;"><asp:TextBox ID="txtpincode" runat="server" ToolTip="Pin Code" ></asp:TextBox></td>
        </tr>
        
        <tr>
           <td width="25%" style="padding-left :10px;">Telephone No.</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtTelephoneNo" runat="server" ToolTip="Telephone No."></asp:TextBox></td>
           <td width="25%" style="padding-left :10px;">Mobile No.</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtMobileNo" runat="server" ToolTip="Mobile No."></asp:TextBox></td>
        </tr>
        <tr>
           <td width="25%" style="padding-left :10px;">Fax No.</td>
           <td style="padding-left :10px;" colspan="3"><asp:TextBox ID="txtFaxNo" runat="server" ToolTip="Fax No."></asp:TextBox></td>
        </tr>
        <tr>
           <td width="25%" style="padding-left :10px;">Comp. Registration No.</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtRegNo" runat="server" ToolTip="Registration No" ></asp:TextBox></td>
           <td width="25%" style="padding-left :10px;">ECC No.</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtECCNo" runat="server" ToolTip="ECC No."></asp:TextBox></td>
        </tr>
        <tr>
           <td width="25%" style="padding-left :10px;">Service Tax No.</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtServiceTaxNo" runat="server" ToolTip="Service Tax No"></asp:TextBox></td>
           <td width="25%" style="padding-left :10px;">LST No.</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtLSTNo" runat="server" ToolTip="LSTNo" ></asp:TextBox></td>
        </tr>
        <tr>
            <td width="25%" style="padding-left :10px;">TIN/VAT No.</td>
            <td style="padding-left :10px;"><asp:TextBox ID="txtVatNo" runat="server" ToolTip="Vat No"></asp:TextBox></td>
            <td width="25%" style="padding-left :10px;">LST Date</td>
            <td style="padding-left :10px;"><asp:DropDownList ID="drpd2"  ToolTip="Day" runat="server">
            </asp:DropDownList><asp:DropDownList ID="drpm2" ToolTip="Month" runat="server">
            </asp:DropDownList><asp:DropDownList ID="drpy2"  ToolTip="Year" runat="server">
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="25%" style="padding-left :10px;">CST No.</td>
            <td style="padding-left :10px;"><asp:TextBox ID="txtCSTNo" runat="server" ToolTip="CSTNo" ></asp:TextBox></td>
            <td width="25%" style="padding-left :10px;">CST Date</td>
            <td style="padding-left :10px;"><asp:DropDownList ID="drpd1"  ToolTip="Day" runat="server">
            </asp:DropDownList><asp:DropDownList ID="drpm1" ToolTip="Month" runat="server">
            </asp:DropDownList><asp:DropDownList ID="drpy1"  ToolTip="Year" runat="server">
            </asp:DropDownList>
           </td>
        </tr>
        
        <tr>
           <td width="25%" style="padding-left :10px;">Email ID.</td>
           <td style="padding-left :10px;" colspan="3"><asp:TextBox ID="txtEmailId" runat="server" ToolTip="Email ID" Width="350px"></asp:TextBox></td>
           
        </tr>
        <tr>
        <td style="width: 25%;padding-left :10px;">Website</td>
           <td style="padding-left :10px;" colspan="3"> <asp:TextBox ID="txtWebSite" runat="server" ToolTip="WebSite Name" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
           <td style="padding-left :10px;">Upload Logo</td>
           <td colspan="3" style="padding-left :10px;"><asp:FileUpload runat="server" ID="fupLogo" /></td>
        </tr>
        
        <tr>
        <td colspan="4" style="font-weight:bold;height:22pt; font-size:12pt ">Contract Agreement &nbsp;&nbsp;<asp:CheckBox ID="chkcontagg" runat="server" AutoPostBack="true" Height="22px" Width="10px"/></td>
        </tr>
        <tr>
            <td style="width: 25%;padding-left :10px;"><asp:Label runat="server" id="lblcontprd" Text="Contract Period" Visible="false" ></asp:Label></td>
            <td colspan="3" style="padding-left :10px;"><asp:TextBox ID="txtcontprd" runat="server" ToolTip="Contract Period" Visible="false"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 25%;padding-left :10px;"><asp:Label runat="server" id="lblpaycrd" Text="Payment Creadits" Visible="false" ></asp:Label></td>
            <td colspan="3" style="padding-left :10px;"><asp:TextBox ID="txtpaycrd" runat="server" ToolTip="Payment Creadits" Visible="false"></asp:TextBox></td>
        </tr>
        <tr>
        <td colspan="4" style="font-weight:bold;height:22pt; font-size:12pt ">Bank Details :</td>
        </tr>
        <tr>
            <td style="padding-left :10px;">Acc. Holder Name</td>
            <td style="padding-left :10px;"><asp:TextBox ID="txtaccname" runat="server" ToolTip="Acc. Name"></asp:TextBox></td>
            <td style="padding-left :10px;">Acc. No.</td>
            <td style="padding-left :10px;"><asp:TextBox ID="txtaccno" runat="server" ToolTip="Acc. No."></asp:TextBox></td>
        </tr> 
        <tr>
            <td style="padding-left :10px;">Bank Name</td>
            <td style="padding-left :10px;"><asp:DropDownList ID="drpbankname" runat="server" ToolTip="Bank Name" Width="100%" AutoPostBack="true" ></asp:DropDownList></td>
            <td style="padding-left :10px;">Bank Address</td>
            <td style="padding-left :10px;"><asp:TextBox ID="txtbankaddress" runat="server" ToolTip="Bank Address" TextMode="MultiLine" Width="150px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="padding-left :10px;">Branch Name</td>
            <td style="padding-left :10px;" colspan="3"><asp:DropDownList ID="drpbankbranch" runat="server" ToolTip="Branch Name" ></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnbranch" runat="server" Text="Add Branch"/></td>
        </tr>
        <tr>
            <td style="padding-left :10px;">Acc. Type</td>
            <td style="padding-left :10px;"><asp:DropDownList ID="drpacctype" runat="server" ToolTip="Acc Type">
            <asp:ListItem Value="Saving">Saving</asp:ListItem>
            <asp:ListItem Value="Current">Current</asp:ListItem>
            <asp:ListItem Value="ODCC">OD/CC</asp:ListItem>
            </asp:DropDownList></td>
            <td style="padding-left :10px;">SWIFT Code</td>
            <td style="padding-left :10px;"><asp:TextBox ID="txtswiftcode" runat="server" ToolTip="SWIFT Code"></asp:TextBox></td>
        </tr> 
        <tr>
            <td style="padding-left :10px;">MICR No.</td>
            <td style="padding-left :10px;"><asp:TextBox ID="txtmicrno" runat="server" ToolTip="MICR No."></asp:TextBox></td>
            <td style="padding-left :10px;">IFSC No.</td>
            <td style="padding-left :10px;"><asp:TextBox ID="txtifscno" runat="server" ToolTip="IFSC No."></asp:TextBox></td>
        </tr> 
        <tr>
        <td colspan="4" align="center"><asp:Button ID="btnadd" runat="server" Text="Add Bank" /></td>
        </tr>
        <tr bgcolor="white">
           <td colspan="4"><asp:Label ID="lblError" runat="server" Font-Bold="true" ForeColor="red"></asp:Label></td>
        </tr>
        <tr>
        <td colspan="4" align="center">
        <asp:GridView ID="gvtemp" runat="server" AutoGenerateColumns="false" DataKeyNames="id" Width="100%"  BackColor="#E0E0E0" Font-Bold="False" Font-Size="Smaller">
       <Columns>
       <asp:TemplateField HeaderText="#">
            <ItemTemplate>

            </ItemTemplate>
            <ItemStyle Width="5%" HorizontalAlign="Center" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="ACC No.">
            <ItemTemplate>
            <%#Eval("f4")%>
            </ItemTemplate>
            <ItemStyle Width="15%" HorizontalAlign="Left"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bank Name">
            <ItemTemplate>
            <%#Eval("f1")%>
            </ItemTemplate>
            <ItemStyle Width="35%" HorizontalAlign="Left"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bank Address">
            <ItemTemplate>
            <%#Eval("f5")%>
            </ItemTemplate>
            <ItemStyle Width="35%" HorizontalAlign="left"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
            <asp:LinkButton ID="lnkUpdate" CommandName="Edit" runat="server">
            <asp:Image ID="Image1" ImageUrl="~/Images/EDIT.GIF" runat="server"  ToolTip="Edit" />
            </asp:LinkButton>
            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("id") %>'>
            <img src="Images/delete.gif" alt="" />
            </asp:LinkButton>
            </ItemTemplate>
            <EditItemTemplate>
            <asp:LinkButton ID="lnkCancel1" runat="server" CommandName="Cancel">
            <asp:Image ID="imgcancle" runat="server" ImageUrl="~/Images/cancel.GIF" ToolTip="Cancel" />
            </asp:LinkButton>
            </EditItemTemplate>
            </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
            <center>No Data Available</center>
            </EmptyDataTemplate>
            <RowStyle BackColor="#cccccc" Font-Size="8pt"/>
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" Font-Size="8pt" ForeColor="White" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" Font-Size="10pt" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" Font-Size="8pt" />
            </asp:GridView>
        </td>        
        </tr>
        <tr style="background-color:<%=db.headerBG%>">
           <td colspan="4" align="center" style="height: 26px"><asp:Button ID="btnSubmit" runat="server" Text="Preview"/> <%--<asp:Button ID="btnUpdate" runat="server" Text="Update Company" Enabled="False"/><asp:Button ID="btnReset" runat="server" Text="Reset" /><asp:Button ID="btndirinfo" runat="server" Text="Director Information" />--%></td>
        </tr>
     </table>
     </asp:Panel> 
     
     <asp:Panel ID="pnlsubcat" runat="server" Width="100%" Visible="false" >
        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="Table2" style="background-color:<%=db.formBG%>;color:<%=db.formColor%>;font-family:<%=db.formText%>;font-weight:<%=db.formWeight%>;font-style:<%=db.formStyle%>;text-decoration:<%=db.formDecoration%>;font-size:<%=db.formSize%>px;text-align:<%=db.formAlignment%>" align="center">
            <tr>
                <td width="20%">Bank Name</td>
                <td><asp:DropDownList ID="drpbank" runat="server" AutoPostBack="true"  ToolTip="Bank Name"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Branch Code</td>
                <td><asp:TextBox ID="txtBbCode" runat="server" ToolTip="Branch Code" BorderStyle="Groove" ></asp:TextBox></td>
            </tr>
            <tr>
                <td>Branch Name</td>
                <td><asp:TextBox ID="txtBbName" runat="server" ToolTip="Branch Name" BorderStyle="Groove" ></asp:TextBox></td>
            </tr>
            
            <tr>
                <td colspan="2" style="text-align: center">
                <asp:Label ID="lblSSError" runat="server" Font-Bold="True" ForeColor="Red" ></asp:Label></td>
            </tr>
            <tr style="background-color:<%=db.headerBG%>;color:<%=db.headerColor%>;font-family:<%=db.headerText%>;font-weight:<%=db.headerWeight%>;font-style:<%=db.headerStyle%>;text-decoration:<%=db.headerDecoration%>;font-size:<%=db.headerSize%>px;text-align:<%=db.headerAlignment%>">
                <td colspan="2" align="center"><asp:Button ID="btnSSSubmit" runat="server" Text="Create Bank Branch" /> <asp:Button ID="btnSSback" runat="server" Text="Back" /></td>
            </tr>
        </table>
      </asp:Panel>
    </div>
</asp:Content>
