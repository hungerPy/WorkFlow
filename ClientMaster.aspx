<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ClientMaster.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_ClientMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <title>Client Registration</title>
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
     <script src="jvalidation/jquery.min.js"></script>
    <script src="jvalidation/jquery.validationEngine-en.js"></script>
    <script src="jvalidation/jquery.validationEngine.js"></script>
    <link href="css/ValidationEngine.css" rel="stylesheet" />
    <%--<script language="javascript" src="functions.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server"> 

    <script type ="text/javascript" >
        $(function () {
            $('[id*=btnSubmit]').bind("click", function () {
                $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
            });
        });
 </script>
    <div class ="span12 ">
   <div class="widget container">
            <div class="widget-header ">
                <i class="icon-user"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Client Registration<asp:Label ID="lblid" runat="server" Visible="false" Text="" ></asp:Label><asp:TextBox ID="txtid" runat="server" Visible="false" ></asp:TextBox>
        </div>
        
        <asp:Panel ID="pnlmain" runat="server" Width="100%"> 
            <div class ="widget-content ">
       <table class ="table ">
        <tr >
           <td colspan="4"  style="background-color:white">
           <asp:Label ID="lblerror" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red"></asp:Label>
           </td>
       </tr>
        <tr>
            <td width="25%" valign="middle" style="padding-left:10px;">Date of Regt.</td>
            <td style="padding-left :10px;" colspan="3"><asp:DropDownList ID="day"  ToolTip="Day" runat="server">
            </asp:DropDownList><asp:DropDownList ID="month" ToolTip="Month" runat="server">
            </asp:DropDownList><asp:DropDownList ID="year"  ToolTip="Year" runat="server">
            </asp:DropDownList></td>
        </tr>
        
        <tr>
           <td width="25%" style="padding-left :10px;">Company/Client Name*</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtCompanyName" runat="server" ToolTip="Company Name" CssClass ="validate[required]" onkeyup="copy_data(this)"></asp:TextBox></td>
           <td width="25%" style="width: 25%;padding-left :10px;">Company/Client Abbreviation</td>
           <td  style="padding-left :10px;"><asp:TextBox ID="txtCompanyAbbreviation" runat="server" ToolTip="Company Abbreviation"></asp:TextBox></td>
        </tr>
        <tr>
           <td width="25%" style="padding-left :10px;">Contact Person</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtContactP" runat="server" ToolTip="Contact Person"></asp:TextBox></td>
           <td width="25%" style="width: 25%;padding-left :10px;">Designation</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtdegi" ToolTip="Degination" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
        <td width="25%" style="padding-left :10px;">PAN No.</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtPanNo" runat="server" ToolTip="Pan No."></asp:TextBox></td>
        <td width="25%"  valign="middle" style="padding-left:10px;">Company Type</td>
        <td valign="middle" style="padding-left:10px;"><asp:DropDownList ID="drpcomptype" runat="server" ToolTip="Company Type"></asp:DropDownList></td>
        </tr>
        <tr >
           <td width="25%" style="height: 62px;padding-left :10px;" >Address</td>
           <td colspan="3" style="height: 62px;padding-left :10px;"><asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ToolTip="Company Addess" Width="366px" ></asp:TextBox>
           <br />
              Country<asp:DropDownList ID="drpCountry" runat="server" ToolTip="Country" AutoPostBack="true"></asp:DropDownList>
              State<asp:DropDownList ID="drpState" runat="server" ToolTip="State" AutoPostBack="true"></asp:DropDownList>
              City<asp:DropDownList ID="drpCity" runat="server" ToolTip="City" Width="20%" ></asp:DropDownList></td>
        </tr>
        <tr>
            <td width="25%" valign="middle" style="padding-left:10px;">PIN No.</td>
            <td valign="middle" colspan="3" style="padding-left:10px;"><asp:TextBox ID="txtpincode" runat="server" data-rule-number="true"  ToolTip="Pin Code" ></asp:TextBox></td>
        </tr>
        
        <tr>
           <td width="25%" style="padding-left :10px;">Telephone No.</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtTelephoneNo" runat="server" ToolTip="Telephone No."></asp:TextBox></td>
           <td width="25%" style="padding-left :10px;">Mobile No.*</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtMobileNo" runat="server" CssClass ="validate[required,custom[phone]]" ToolTip="Mobile No."></asp:TextBox></td>
        </tr>
        <tr>
           <td width="25%" style="padding-left :10px;">Fax No.</td>
           <td style="padding-left :10px;" colspan="3"><asp:TextBox ID="txtFaxNo" runat="server" ToolTip="Fax No."></asp:TextBox></td>
        </tr>
        <tr>
           <td width="25%" style="padding-left :10px; height: 26px;">Comp. Registration No.</td>
           <td style="padding-left :10px; height: 26px;"><asp:TextBox ID="txtRegNo" runat="server" ToolTip="Registration No" ></asp:TextBox></td>
           <td width="25%" style="padding-left :10px; height: 26px;">ECC No.</td>
           <td style="padding-left :10px; height: 26px;"><asp:TextBox ID="txtECCNo" runat="server" ToolTip="ECC No."></asp:TextBox></td>
        </tr>
        <tr>
           <td width="25%" style="padding-left :10px;">Service Tax No.</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtServiceTaxNo" runat="server" ToolTip="Service Tax No"></asp:TextBox></td>
           <td width="25%" style="padding-left :10px;">LST No.</td>
           <td style="padding-left :10px;"><asp:TextBox ID="txtLSTNo" runat="server" ToolTip="LSTNo"></asp:TextBox></td>
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
           <td width="25%" style="padding-left :10px;">Email ID.*</td>
           <td style="padding-left :10px;" colspan="3"><asp:TextBox ID="txtEmailId" runat="server" ToolTip="Email ID" CssClass ="validate[required,custom[email]]" Width="350px"></asp:TextBox></td>
           
        </tr>
        
          <tr>
           <td width="25%" style="padding-left :10px;">Ticket Email ID.</td>
           <td style="padding-left :10px;" colspan="3"><asp:TextBox ID="txttktEmailId" runat="server" ToolTip="Ticket Support Email ID" Width="350px"></asp:TextBox></td>
           
        </tr>
        
        <tr>
        <td style="width: 25%;padding-left :10px;">Website</td>
           <td style="padding-left :10px;" colspan="3"> <asp:TextBox ID="txtWebSite" runat="server" ToolTip="WebSite Name" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
           <td style="padding-left :10px;">Upload Logo</td>
           <td colspan="3" style="padding-left :10px;"><asp:FileUpload runat="server" ID="fupLogo" /></td>
        </tr>
        <tr style="background-color:<%=db.headerBG%>">
           <td colspan="4" align="center" style="height: 26px"><center><asp:Button ID="btnSubmit" CssClass ="btn btn-success " runat="server" Text="Preview"/></center> <%--<asp:Button ID="btnUpdate" runat="server" Text="Update Company" Enabled="False"/><asp:Button ID="btnReset" runat="server" Text="Reset" /><asp:Button ID="btndirinfo" runat="server" Text="Director Information" />--%></td>
        </tr>
     </table></div>
     </asp:Panel> 
     
     </div>
    </div>
</asp:Content>

