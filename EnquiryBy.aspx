<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EnquiryBy.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_EnquiryBy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <title>Enquiry by</title>
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>

     <script language="javascript" src="functions.js" type="text/javascript"></script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">
  
     <style type="text/css">
     		form li#send button {
			background:#003366 url(images/css-form-send.gif) no-repeat 8px 50%;
			border:none;align:center;
			padding:4px 8px 4px 28px;
			border-radius:15%; /* Don't expect this to work on IE6 or 7 */
			-moz-border-radius:15%;
			-webkit-border-radius:15%;
			color:#fff;
			margin-left:77px; /* Total width of the labels + their right margin */
			cursor:pointer;
			}
            form li#send button:hover {
			background-color:#006633;
			}
	</style>
	<script type="text/javascript">
function NewWindow() {
document.forms[0].target = "_blank";
}
</script>
      <script type ="text/javascript" >
          $(function () {
              $('[id*=btnSubmit').bind("click", function () {
                  $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
              });
          });
         
          </script>

	

    <div>
    <div class="span12">
         <div class="widget ">
            <div class="widget-header">
                <i class="icon-pencil"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Enquiry By</b>
                </div><asp:TextBox ID="txtid" runat="server" Visible="false" ></asp:TextBox>
    
    <asp:Panel ID="pnlmain" runat="server" Width="100%"> 
        <div class="widget-content">
                <div id="formcontrols" class="form-horizontal">
                    <fieldset>
                         <div class="control-group">
     <label for="firstname" class="control-label">Mobile/Contact No*</label> 
    <div class="controls">
<asp:TextBox ID="Txtmobno" ToolTip="Mobile/Contact No" runat="server" Width="250px" CssClass ="validate[required,custom[phone]]"  Font-Names="Times New Roman"></asp:TextBox>
        </div> </div> 
                        <div class="control-group">
     <label for="firstname" class="control-label">Email-ID*</label> 
    <div class="controls">
<asp:TextBox ID="txtEmailid" ToolTip="Personal Email-Id" runat="server" Width="250px" CssClass ="validate[required,custom[email]]" Font-Names="Times New Roman"></asp:TextBox>
        </div> </div> 
                       <div class="control-group">
     <label for="firstname" class="control-label">Enquiry for*</label> 
    <div class="controls">  
            <asp:TextBox ID="txtenqfor" ToolTip="Enquiry For" runat="server" Width="250px" CssClass ="validate[required]" TextMode="MultiLine" Font-Names="Times New Roman" Height="109px"></asp:TextBox>
        </div> </div> 
                        <div class="control-group">
     <label for="firstname" class="control-label"> Contact Name</label> 
    <div class="controls"> 
     <asp:TextBox  ID="txtcontname" ToolTip="Contact Name" runat="server" Width="250px" Font-Names="Times New Roman"></asp:TextBox>
        </div> </div> 
                         <div class="control-group">
     <label for="firstname" class="control-label">   Company Name</label> 
    <div class="controls">
    <asp:TextBox  ID="txtcompName" ToolTip="Company Name" runat="server" Width="250px" Font-Names="Times New Roman"></asp:TextBox>
        </div></div> 
                        <div class="control-group">
     <label for="firstname" class="control-label">    Division Name</label> 
    <div class="controls"><asp:DropDownList ID="DrpDivision" runat="server" ToolTip="Division Name" Width="250px" Font-Names="Times New Roman"></asp:DropDownList>
      </div></div> 
                   <div class="control-group">
     <label for="firstname" class="control-label">   Contact Address</label> 
    <div class="controls">        
         <asp:TextBox  ID="txtcontaddress" ToolTip="Contact Address" runat="server" Width="250px" TextMode="MultiLine" Font-Names="Times New Roman" Height="75px"></asp:TextBox>
       </div> </div> 
      <div class="control-group">
     <label for="firstname" class="control-label">   Date of Enquiry</label> 
    <div class="controls">
          <asp:DropDownList ID="day"  ToolTip="Day" runat="server">
            </asp:DropDownList><asp:DropDownList ID="month" ToolTip="Month" runat="server">
            </asp:DropDownList><asp:DropDownList ID="year"  ToolTip="Year" runat="server">
            </asp:DropDownList></div> </div> 
                         <div class="control-group">
     <label for="firstname" class="control-label">  Remarks</label> 
    <div class="controls">
           <asp:TextBox  ID="txtremarks" ToolTip="Remarks" runat="server" Width="250px" TextMode="MultiLine" Font-Names="Times New Roman" Height="75px"></asp:TextBox>
     </div> </div> 
                        <div class="form-actions">
        <asp:Button ID="btnSubmit" runat="server" Text="Preview" class="btn btn-success  " TabIndex="14" Width="100px" Height="32px" />
  </fieldset> </div> </div> 
                             </asp:Panel> 
    
   <asp:Panel ID="pnlprev" runat="server" Width="100%" Visible="false" > 
      <table width="100%"  border="1" cellpadding="1" cellspacing="1" id="tblprv" style="border-color :White ;   background-color:<%=db.formBG%>;color:<%=db.formColor%>;font-family:<%=db.formText%>;font-weight:<%=db.formWeight%>;font-style:<%=db.formStyle%>;text-decoration:<%=db.formDecoration%>;font-size:<%=db.formSize%>px;text-align:<%=db.formAlignment%>">
        <tr style="line-height:30px;">
           <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%"><asp:Label ID="lblcontname" runat="Server" Text="Contact Name"></asp:Label></td>
           <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="75%"><asp:Label  ID="lblcontname1" runat="server" Width="250px"></asp:Label></td>
        </tr>
        <tr style="line-height:30px;"> 
            <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%"><asp:Label ID="lblcontactno" runat="Server" Text="Mobile/Contact No"></asp:Label></td>
            <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="75%"><asp:Label ID="lblcontactno1" runat="server" Width="250px"></asp:Label></td>
        </tr>
        <tr style="line-height:30px;">
            <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%"><asp:Label ID="lblmailid" runat="Server" Text="Email-ID"></asp:Label></td>
            <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="75%"><asp:Label ID="lblmailid1" runat="server" Width="250px"></asp:Label></td>
       </tr>
       <tr style="line-height:30px;">
            <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%"><asp:Label ID="lbldivision" runat="Server" Text="Division Name"></asp:Label></td>
            <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="75%"><asp:Label ID="lbldivision1" runat="server" Width="250px"></asp:Label></td>
       </tr> 
       <tr style="line-height:30px;"> 
            <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%"><asp:Label ID="lblenqfor" runat="Server" Text="Enquiry for"></asp:Label></td>
            <td style=" padding-left :20px; font-size:11pt;font-weight :bold ;" width="75%"><asp:Label ID="lblenqfor1" runat="server" Width="250px"></asp:Label></td>
       </tr>
       <tr style="line-height:30px;">
           <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%"><asp:Label ID="lblcompname" runat="Server" Text="Company Name"></asp:Label></td>
           <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="75%"><asp:Label  ID="lblcompname1" runat="server" Width="250px"></asp:Label></td>
       </tr>
       <tr style="line-height:30px;">
            <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%"><asp:Label ID="lblcontaddress" runat="Server" Text="Contact Address"></asp:Label></td>
            <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="75%"><asp:Label  ID="lblcontaddress1" runat="server" Width="250px" ></asp:Label></td>
       </tr>
       <tr style="line-height:30px;">
            <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%"><asp:Label ID="lblenqdate" runat="Server" Text="Date of Enquiry"></asp:Label></td>
            <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="75%"><asp:Label  ID="lblenqdate1" runat="server" Width="250px" ></asp:Label></td>
       </tr>
       <tr style="line-height:30px;">
            <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%"><asp:Label ID="lblremarks" runat="Server" Text="Remarks"></asp:Label></td>
            <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="75%"><asp:Label  ID="lblremarks1" runat="server" Width="250px" ></asp:Label></td>
       </tr>
       <tr style="line-height:30px;">
            <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%" valign="top" ><asp:Label ID="lblemailName" runat="Server" Text="Enquiry inform by e-mail Email"></asp:Label></td>
            <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="75%">
           <%-- <asp:CheckBoxList ID="chkallmail" RepeatColumns="2" runat="server"></asp:CheckBoxList>--%>
            <asp:CheckBoxList ID="chkemail" RepeatColumns="2" runat="server"></asp:CheckBoxList></td>
       </tr>
       <tr >
            <td colspan="4"  style="background-color:white"><asp:Label ID="lblerror" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red"></asp:Label></td>
       </tr>
       <tr style="background-color:<%=db.headerBG%>">
            <td colspan="4" align="center" style="height: 26px"><asp:Button ID="btnfSubmit" runat="server" Text="Confirm" TabIndex="14" Width="100px" Height="32px"  /><asp:Button ID="btnedit" runat="server" Text="Edit" TabIndex="14" Width="100px" Height="32px"  /></td>
       </tr>
        
    </table>
   </asp:Panel>
   
    </div>
   </asp:Content>
