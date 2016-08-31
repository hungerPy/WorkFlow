<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="MasterPage.master" CodeFile="EnquiriesListToday.aspx.vb" Inherits="Admin_EnquiriesListToday" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <title>Today's Enquiry List</title>
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>

    <script language="javascript" type="text/javascript" src="functions.js"></script>
    <script language="javascript" type="text/javascript" src="calander.js"></script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">
    
    <span id="spn1" style="display:none;top:0px;position:absolute"></span>
   <table width="100%" cellpadding="1" cellspacing="0" border="0" style="background-color:<%=db.headerBG%>;color:<%=db.headerColor%>;font-family:<%=db.headerText%>;font-weight:<%=db.headerWeight%>;font-style:<%=db.headerStyle%>;text-decoration:<%=db.headerDecoration%>;font-size:<%=db.headerSize%>px;text-align:<%=db.headerAlignment%>">
    <tr>
    <td align="center">LIST OF TODAY'S ENQUIRIES<asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox></td>
    </tr>
    </table>
    <table id="tblOrders" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0" style="background-color:<%=db.formBG%>;color:<%=db.formColor%>;font-family:<%=db.formText%>;font-weight:<%=db.formWeight%>;font-style:<%=db.formStyle%>;text-decoration:<%=db.formDecoration%>;font-size:<%=db.formSize%>px;text-align:<%=db.formAlignment%>">
        <tr>
            <td align="left"><asp:Label ID="lblmsg" runat="server" Visible="false" Font-Bold="True" Font-Size="Larger" ForeColor="Red"></asp:Label></td>
            <td align="right">Date:<asp:Label ID="lbldate" runat="server" ></asp:Label></td>
        </tr>
    </table>
    
    <asp:Panel ID="pnlmain" runat="server" Width="100%" Visible="false" > 
      <table width="100%"  border="0" cellpadding="1" cellspacing="1" id="tblApp" style="border-color :White ;   background-color:<%=db.formBG%>;color:<%=db.formColor%>;font-family:<%=db.formText%>;font-weight:<%=db.formWeight%>;font-style:<%=db.formStyle%>;text-decoration:<%=db.formDecoration%>;font-size:<%=db.formSize%>px;text-align:<%=db.formAlignment%>">
       <tr> 
            <td valign="middle" width="20%" style="padding-left:10px;">Mobile/Contact No*</td>
            <td valign="middle"><asp:TextBox ID="Txtmobno" ToolTip="Mobile/Contact No" runat="server" Width="250px" ></asp:TextBox></td>
       </tr>
       <tr>
            <td width="20%" valign="middle" style="padding-left:10px;">Email-ID</td>
            <td valign="middle"><asp:TextBox ID="txtEmailid" ToolTip="Personal Email-Id" runat="server" Width="250px"></asp:TextBox></td>
       </tr>
       <tr> <td valign="middle" width="20%"  style="padding-left:10px;">Enquiry for</td>
            <td valign="middle"><asp:TextBox ID="txtenqfor" ToolTip="Enquiry For" runat="server" Width="250px" TextMode="MultiLine"></asp:TextBox></td>
       </tr>
       <tr>
           <td width="20%" valign="middle"  style="padding-left:10px; ">Contact Name</td>
           <td valign="middle"><asp:TextBox  ID="txtcontname" ToolTip="Contact Name" runat="server" Width="250px"></asp:TextBox></td>
       </tr>
       <tr>
           <td width="20%" valign="middle"  style="padding-left:10px; ">Company Name</td>
           <td valign="middle"><asp:TextBox  ID="txtcompName" ToolTip="Company Name" runat="server" Width="250px"></asp:TextBox></td>
       </tr>
       <tr >
            <td width="20%" valign="middle" style="padding-left:10px;">Division Name</td>
            <td valign="middle"><asp:DropDownList ID="DrpDivision" runat="server" ToolTip="Division Name" Width="250px"></asp:DropDownList></td>
       </tr> 
       <tr>
            <td width="20%" valign="middle" style="padding-left:10px; ">Contact Address</td>
            <td valign="middle"><asp:TextBox  ID="txtcontaddress" ToolTip="Contact Address" runat="server" Width="250px" TextMode="MultiLine"></asp:TextBox></td>
       </tr>
       <tr>
            <td width="25%" valign="middle" style="padding-left:10px;">Date of Enquiry</td>
            <td valign="middle"><asp:DropDownList ID="day"  ToolTip="Day" runat="server">
            </asp:DropDownList><asp:DropDownList ID="month" ToolTip="Month" runat="server">
            </asp:DropDownList><asp:DropDownList ID="year"  ToolTip="Year" runat="server">
            </asp:DropDownList></td>
       </tr>
       <tr>
            <td width="20%" valign="middle" style="padding-left:10px; ">Remarks</td>
            <td valign="middle"><asp:TextBox  ID="txtremarks" ToolTip="Remarks" runat="server" Width="250px" TextMode="MultiLine"></asp:TextBox></td>
       </tr>
       <tr style="background-color:<%=db.headerBG%>">
            <td colspan="4" align="center" style="height: 26px"><asp:Button ID="btnSubmit" runat="server" Text="Preview" TabIndex="14" Width="100px" Height="32px" /></td>
       </tr>
        
    </table>
      <asp:Panel ID="pnlprev" runat="server" Width="100%" Visible="false" > 
      <table width="100%"  border="0" cellpadding="1" cellspacing="1" id="tblprv" style="border-color :White ;   background-color:<%=db.formBG%>;color:<%=db.formColor%>;font-family:<%=db.formText%>;font-weight:<%=db.formWeight%>;font-style:<%=db.formStyle%>;text-decoration:<%=db.formDecoration%>;font-size:<%=db.formSize%>px;text-align:<%=db.formAlignment%>">
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
       <tr >
            <td colspan="4"  style="background-color:white"><asp:Label ID="lblerror" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red"></asp:Label></td>
       </tr>
       <tr style="background-color:<%=db.headerBG%>">
            <td colspan="4" align="center" style="height: 26px"><asp:Button ID="btnfSubmit" runat="server" Text="Confirm" TabIndex="14" Width="100px" Height="32px"  /><asp:Button ID="btnedit" runat="server" Text="Edit" TabIndex="14" Width="100px" Height="32px"  /><asp:Button
                    ID="btndel" runat="server" Text="Delete" TabIndex="14" Width="100px" Height="32px"/></td>
       </tr>
        
    </table>
   </asp:Panel>
   </asp:Panel>
    
  
        
     <asp:Panel ID="pnlenq" runat="server" Width="100%" >    
     <table width="100%" border="0" id="tblForm" >
     <tr>
     <td>
     <asp:GridView ID="GVdivision" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="division" RowStyle-BackColor="blanchedAlmond">
     <Columns>
     <asp:TemplateField HeaderText="S.No.">
     <ItemTemplate>
     
     </ItemTemplate>
     <ItemStyle Width="10%" HorizontalAlign="Center" />
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Division Name" HeaderStyle-HorizontalAlign="Left">
     <ItemTemplate>
     <%#Eval("DName")%>
      </td></tr>
        <tr>
        <td colspan="4">
        <table width="100%" id='tbl<%# Eval("division")%>' cellpadding="0" cellspacing="0" style="font-size:8pt;font-family:Verdana;text-align:left;" border="0" bordercolor="black">
             <tr>
             <td>
        <asp:GridView ID="GVenquiry" runat="server" AutoGenerateColumns="false" DataKeyNames="enqid" Width="100%" RowStyle-BackColor="blanchedAlmond" AlternatingRowStyle-BackColor="white">
        <Columns>
            <asp:TemplateField HeaderText="S.No.">
            <ItemTemplate>
                
            </ItemTemplate>
            <ItemStyle Width="3%" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Enquiry for">
            <ItemTemplate>
            <%#Eval("enquiryfor")%>
            </ItemTemplate>
            <ItemStyle Width="15%" HorizontalAlign="left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Details">
            <ItemTemplate>
            <%#Eval("contactname")%><br />
            <%#Eval("companyName") %><br /> 
            <%#Eval("contactaddress")%>
            </ItemTemplate>
            <ItemStyle Width="20%" VerticalAlign="Top" HorizontalAlign="left"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contact Details">
            <ItemTemplate>
            <%#Eval("contactno")%><br />
            <%#Eval("EmailId")%>
            </ItemTemplate>
            <ItemStyle Width="15%" VerticalAlign="Top" HorizontalAlign="left"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remarks">
            <ItemTemplate>
            <%#Eval("remarks")%>
            </ItemTemplate>
            <ItemStyle Width="30%" VerticalAlign="Top" HorizontalAlign="left"/>
            </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
        <ItemTemplate>
        <a href="enquiriesListToday.aspx?id=<%#eval("enqid")%>"><img src="Images/EDIT.gif" alt="Edit" style="border:0;"/></a>
        
        <%--<a href="pnlmain.visible=True">E</a>--%>
        <asp:TextBox ID="txtId" runat="server" Text='<%#Eval("enqid")%>' Visible="false"></asp:TextBox>
        </ItemTemplate>
        <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle"/>
        </asp:TemplateField>
        </Columns>
         <EmptyDataTemplate><center>No Data Available</center></EmptyDataTemplate>
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
     <ItemStyle Width="91%" HorizontalAlign="left" />
     </asp:TemplateField>

    
     </Columns>
      <EmptyDataTemplate><center>No Data Available</center></EmptyDataTemplate>
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
   </asp:Content>

    