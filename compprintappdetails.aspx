<%@ Page Language="VB" AutoEventWireup="false" CodeFile="compprintappdetails.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_compprintappdetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">

    <title>Company Edit Preview</title>
<link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server"> 

    <div><asp:TextBox ID="txtCompanyName" runat="server" Visible="false" ></asp:TextBox>
         <asp:TextBox ID="txtCompanyAbbreviation" runat="server" Visible="false" ></asp:TextBox>
         <asp:DataList ID="dlstMain" runat="server" Width="97%" DataKeyField="companyId" Visible="True" RepeatColumns ="1">
        <ItemTemplate >
        
        <table width="100%" border="0" style="background-color :#EEEEEE ; border-color :White ;" >
         <tr style="background-color :#EEEEEE ;line-height:20px;" ><td colspan="4"  width="100%"   align ="center" >
        <b><span style=" font-size :large ; ">Company Profile & Details</span> </b>
              
            </td></tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
            <td align="left"  colspan="4" style=" padding-left :10px;">Company No. :<b> <%#DataBinder.Eval(Container.DataItem, "companyid")%></b></td>
            <%--<td align="right"  colspan="2" style=" padding-right :10px;" >
            Registration Date : <asp:Label ID="lbldate" runat="server"  Font-Names="Verdana" Font-Bold="true" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "dt")%></asp:Label>
            </td>--%>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">1.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Company Name</td>
               <td align="left"  style=" padding-left :10px;">
             <asp:Label ID="lblname" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "companyName")%></asp:Label>
            </td>
            <td rowspan="5" valign="top" align="left" ><asp:Image ID="Image3" runat="server" ImageUrl='<%#"~/logos/" & DataBinder.Eval(Container.DataItem, "logo")%>' style="height:150px;width:150px;border:1px solid gray;"></asp:Image></td></tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">2.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Company Abbreviation</td>
               <td align="left" style=" padding-left :10px;" >
             <asp:Label ID="Label1" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "companyabbr")%></asp:Label>
            </td></tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">3.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Company Type</td>
               <td align="left" style=" padding-left :10px;" >
             <asp:Label ID="lblcomptype" runat="server"  Font-Names="Verdana" Font-Size="9pt" ></asp:Label>
            </td></tr>
            
             <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">4.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Contact Person</td>
               <td align="left" style=" padding-left :10px;">
             <asp:Label ID="lblcompname" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "contactP")%></asp:Label>
            </td></tr>
            
  <%--          <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">5.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Designation</td>
               <td align="left" style=" padding-left :10px;">
             <asp:Label ID="lbldivision" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "designation")%></asp:Label>
            </td></tr>--%>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">6.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Date of Regt.</td>
               <td align="left" style=" padding-left :10px;"  width="40%" colspan="2">
             <asp:Label ID="lbldinNo" runat="server" Width="50%"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "DOR")%></asp:Label>
            </td>
            </tr>
            
        <%--    <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%"  align="center">7.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">PAN No. </td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
             <asp:Label ID="lblpaano" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "panno")%></asp:Label>
            </td>
            </tr>--%>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%"  align="center">8.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Registration No.</td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2" >
             <asp:Label ID="lblpfno" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "regno")%></asp:Label>
            </td>
            </tr>
        <%--    <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">9.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Service Tax No. </td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2" >
             <asp:Label ID="lbldlno" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "serviceTaxNo")%></asp:Label>
            </td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">10.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Sales Tax No.</td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
             <asp:Label ID="lblpassportno" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "salesTaxNo")%></asp:Label>
            </td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">11.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">VAT No.</td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
               <asp:Label ID="lblnationality" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "vatno")%></asp:Label>
            </td>
            </tr>--%>
            <tr style="background-color :#E1E1E1 ;line-height:25px;">
               <td Width="5%" align="center" valign="top" >12.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%" valign="top">Contact No.</td>
               <td align="left" valign="middle" style=" padding-left :10px; width :40%" colspan="2">
            <asp:Label ID="lblcontactno" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#"Mobile : "  & DataBinder.Eval(Container.DataItem, "mobileno")%></asp:Label><br /> <asp:Label ID="lblll" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#"Landline : " & DataBinder.Eval(Container.DataItem, "telephoneno")%></asp:Label><br /> <asp:Label ID="lblfax" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#"Fax : " & DataBinder.Eval(Container.DataItem, "faxno")%></asp:Label>
            </td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">13.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Email-Id </td>
               <td align="left" style=" padding-left :10px;width :40%;"  colspan="2">
               <asp:Label ID="lblemailid" runat="server" Width="50%"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "Emailid")%></asp:Label>
            </td>
            </tr>
<tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">14.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Website</td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
               <asp:Label ID="lblwebsite" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "website")%></asp:Label>
            </td>
            </tr>
           <%-- <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center" valign="top">15.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%" valign="top">Regt. Address </td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
               <asp:Label ID="lblpresentaddress" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "Address")%></asp:Label><asp:Label ID="lblcity" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label> <asp:Label ID="lblstate" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label><asp:Label ID="lblpin" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#", Pincode : " & DataBinder.Eval(Container.DataItem, "pincode")%></asp:Label>
            </td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center" valign="top">16.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%" valign="top">Correspondence Address </td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
               <asp:Label ID="lblcrossaddress" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "CorrsAddress")%></asp:Label><asp:Label ID="lblcit" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label> <asp:Label ID="lblsat" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label><asp:Label ID="lblcrosspin" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#", Pincode : " & DataBinder.Eval(Container.DataItem, "CrossPinCode")%></asp:Label>
            </td>
            </tr>
             <tr style="background-color :#E1E1E1 ;line-height:25px;">
               <td Width="5%" align="center" valign="top" >17.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%" valign="top">Bank Details </td>
               <td align="left" valign="middle" style=" padding-left :10px; width :40%" colspan="2">
            <asp:Label ID="lblbankdetail" runat="server"  Font-Names="Verdana" Font-Size="9pt" ></asp:Label>
            </td></tr>
   
    <tr style="background-color :#EEEEEE ;" ><td colspan="4"  width="100%" align ="center" >
     
             
            </td></tr>--%>
            
            
            </table>
            
            </ItemTemplate> 
        
        </asp:DataList>
        <table  width="100%"><tr style="background-color :#E1E1E1 ;"><td align="center" ><asp:Button ID="btnReconfirm" runat="server" Text="Save" CssClass ="btn btn-success " Width="70px" Height="37px"></asp:Button>&nbsp;&nbsp;<asp:Button ID="btnedit" Width="70px" CssClass ="btn btn-warning " runat="server" Text="Edit" Height="37px"></asp:Button>&nbsp;&nbsp;<asp:Button ID="btnpreview" Width="70px" runat="server" CssClass ="btn btn-success " Text="Print" Height="37px"></asp:Button>&nbsp;&nbsp;<asp:Button ID="btndirinfo" runat="server" CssClass ="btn btn-info " Text="Director/Proprietor Info." Height="37px" Width="186px" /></td></tr></table>
    </div>
   
        </asp:Content>