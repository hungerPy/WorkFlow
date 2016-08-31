<%@ Page Language="VB" AutoEventWireup="false" CodeFile="clientprintappdetails.aspx.vb" Inherits="clientprintappdetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Client Details</title>
</head>
<body style="background-color :#E1E1E1 ;">
    <form id="form1" runat="server">
   <div style="padding-left:15px;width:auto ;">
         <asp:TextBox ID="txtCompanyName" runat="server" Visible="false" ></asp:TextBox>
         <asp:TextBox ID="txtCompanyAbbreviation" runat="server" Visible="false" ></asp:TextBox>
        <asp:DataList ID="dlstMain" runat="server" Width="97%" DataKeyField="companyId" Visible="True" RepeatColumns ="1">
        <ItemTemplate >
        
        <table width="100%" border="0" style="background-color :#EEEEEE ; border-color :White ;" >
         <tr style="background-color :#EEEEEE ;line-height:20px;" ><td colspan="4"  width="100%"   align ="center" >
        <b><span style=" font-size :large ; ">Client Profile & Details Report</span> </b>
            </td></tr>
            <tr>
            <td colspan="4">
            <table width="100%">
            <tr style="background-color :#E1E1E1 ; line-height:20px;"><br />
            <td align="left"  colspan="2" style=" padding-left :10px; font-size:10pt;font-weight :bold ;" width="40%"><asp:Label ID="lblrecno" runat="server" Font-Names="Verdana" Font-Size="9pt" ></asp:Label></td>
             <td align="left" style=" padding-left :10px;"  width="30%">Date of Regt.:&nbsp;
             <asp:Label ID="lblregdate" runat="server" Width="50%"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "DOR")%></asp:Label>
            </td>
            <td align="right" style=" padding-left :10px;">Date:<asp:Label ID="lbldate" runat="server"></asp:Label></td>
            
            </tr>
            </table> 
            </td> 
            </tr> 
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">1.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Company/Client Name</td>
                <td align="left"  style=" padding-left :10px;" width="50%"><asp:Label ID="lblname" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "companyName")%></asp:Label></td> 
            <td rowspan="5" valign="top" align="center"><asp:Image ID="Image3" runat="server" ImageUrl='<%#"~/logos/" & DataBinder.Eval(Container.DataItem, "logo")%>' style="height:150px;width:150px;border:1px solid gray;"></asp:Image><asp:Label ID="lblimpcompname" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "companyName")%></asp:Label></td>
            </tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td Width="5%" align="center" valign="top">2.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Company/Client Abbreviation</td>
               <td align="left" style=" padding-left :10px;" >
             <asp:Label ID="Label1" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "companyabbr")%></asp:Label>
            </td>
            </tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td Width="5%" align="center" valign="top">3.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Company Type</td>
                <td align="left" style=" padding-left :10px;"><asp:Label ID="lblcompType" runat="server"  Font-Names="Verdana" Font-Size="9pt" ></asp:Label></td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center" valign="top">4.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Contact Person</td>
               <td align="left" style=" padding-left :10px;" ><asp:Label ID="lblcompname" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "contactP")%></asp:Label> </td>
            </tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">5.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Designation</td>
               <td align="left" style=" padding-left :10px;"><asp:Label ID="lbldivision" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "designation")%></asp:Label></td>
            </tr>
            
            <tr style="background-color :#E1E1E1 ;line-height:25px;">
               <td Width="5%" align="center" valign="top" >6.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%" valign="top">Contact No.</td>
               <td align="left" valign="middle" style=" padding-left :10px; width :40%" colspan="2">
            <asp:Label ID="lblcontactno" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#"Mobile : "  & DataBinder.Eval(Container.DataItem, "mobileno")%></asp:Label><br /> <asp:Label ID="lblll" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#"Landline : " & DataBinder.Eval(Container.DataItem, "telephoneno")%></asp:Label><br /> <asp:Label ID="lblfax" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#"Fax : " & DataBinder.Eval(Container.DataItem, "faxno")%></asp:Label>
            </td>
            </tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">7.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Email-Id </td>
               <td align="left" style=" padding-left :10px;width :40%;"  colspan="2">
               <asp:Label ID="lblemailid" runat="server" Width="50%"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "Emailid")%></asp:Label>
            </td>
            </tr>
            
             <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">8.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Ticket Email-Id </td>
               <td align="left" style=" padding-left :10px;width :40%;"  colspan="2">
               <asp:Label ID="lbltemailid" runat="server" Width="50%"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "tEmailid")%></asp:Label>
            </td>
            </tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">9.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Website</td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
               <asp:Label ID="lblwebsite" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "website")%></asp:Label>
            </td>
            </tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%"  align="center">10.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">PAN No. </td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
             <asp:Label ID="lblpaano" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "panno")%></asp:Label>
            </td>
            </tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%"  align="center">11.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Registration No.</td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2" >
             <asp:Label ID="lblpfno" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "regno")%></asp:Label>
            </td>
            </tr>
           
           <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">12.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">TIN/VAT No.</td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
               <asp:Label ID="lblnationality" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "vatno")%></asp:Label>
            </td>
            </tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">13.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Service Tax No. </td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2" >
             <asp:Label ID="lbldlno" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "serviceTaxNo")%></asp:Label>
            </td>
            </tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">14.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">ECC No. </td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2" >
             <asp:Label ID="lbleccno" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "ECCNo")%></asp:Label>
            </td>
            </tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">15.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">CST No.</td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
             <asp:Label ID="lblpassportno" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "salesTaxNo")%></asp:Label>
            </td>
            </tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">16.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">CST Date</td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
             <asp:Label ID="lbicstDate" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "cstdate")%></asp:Label>
            </td>
            </tr>
             
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">17.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">LST No.</td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
             <asp:Label ID="lblLstNo" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "LstNo")%></asp:Label>
            </td>
            </tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">18.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">LST Date</td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
             <asp:Label ID="lblLstDate" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "lstdate")%></asp:Label>
            </td>
            </tr>
                       
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center" valign="top">19.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%" valign="top">Address </td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
               <asp:Label ID="lblpresentaddress" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "Address")%></asp:Label><asp:Label ID="lblcity" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label> <asp:Label ID="lblstate" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label><asp:Label ID="lblpin" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#", Pincode : " & DataBinder.Eval(Container.DataItem, "pincode")%></asp:Label>
            </td>
            </tr>
            
            <tr style="background-color :#EEEEEE ;" >
                <td colspan="4"  width="100%" align ="center" ></td>
            </tr>
          </table>

            </ItemTemplate> 
        
        </asp:DataList>
        <asp:Button ID="btnReconfirm" runat="server" Text="Submit" Visible="false" width="100px" Height="20px"></asp:Button>
       <%--<asp:Button ID="btnedit" width="70px" runat="server" Text="Edit" Height="20px"></asp:Button>--%>
    </div>
    </form>
</body>
</html>
