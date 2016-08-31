<%@ Page Language="VB" AutoEventWireup="false" CodeFile="printappdetails1.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_printappdetails1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">

    <title>Director Details</title>
 <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server"> 

   <%--<div style="padding-left:15px;width:auto ;">--%>
        <asp:DataList ID="dlstMain" runat="server" Width="97%" DataKeyField="ids" Visible="True" RepeatColumns ="1">
        <ItemTemplate >
        <div class="widget-content">
        <table class="table " >
            <div class ="widget-header " >   <i class="icon-pencil "></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <%--   <table width="100%" border="0" style="background-color :#EEEEEE ; border-color :White ;" >
         <tr style="background-color :#EEEEEE ;line-height:20px;" ><td colspan="4"  width="100%"   align ="center" >
        <b><span style=" font-size :large ; ">--%><%--</span> </b>--%>
              <b>Director Profile & Details Report</b>
     
            </td></div>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
            <td align="left"  colspan="3" style=" padding-left :10px;">Record No. :<b> <%#DataBinder.Eval(Container.DataItem, "ids")%></b></td>
            <td align="left" style=" padding-left :10px;">Date:<asp:Label ID="lbldate" runat="server"></asp:Label></td>
            <%--<td align="right"  colspan="2" style=" padding-right :10px;" >
            Registration Date : <asp:Label ID="lbldate" runat="server"  Font-Names="Verdana" Font-Bold="true" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "dt")%></asp:Label>
            </td>--%>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">1.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Director Name  </td>
               <td align="left"  style=" padding-left :10px;">
             <asp:Label ID="lblname" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "dirName")%></asp:Label>
            </td>
            <td rowspan="5" valign="top" align="left" ><asp:Image ID="Image3" runat="server" ImageUrl='<%#"~/Photos/" & DataBinder.Eval(Container.DataItem, "photo")%>' style="height:150px;width:150px;border:1px solid gray;"></asp:Image></td></tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">2.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Division Name  </td>
               <td align="left" style=" padding-left :10px;">
             <asp:Label ID="lbldivision" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "division")%></asp:Label>
            </td><td></td></tr>
            
             <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">3.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Company Name  </td>
               <td align="left" style=" padding-left :10px;">
             <asp:Label ID="lblcompname" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "companyName")%></asp:Label>
            </td></tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">4.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">DIN No.</td>
               <td align="left" style=" padding-left :10px;"  width="40%">
             <asp:Label ID="lbldinNo" runat="server" Width="50%"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "dinNo")%></asp:Label>
            </td>
            </tr>
            
             <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">5.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Date of Joining</td>
               <td align="left" style=" padding-left :10px;"  width="40%">
             <asp:Label ID="lbldoj" runat="server" Width="50%"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "doj")%></asp:Label>
            </td>
            </tr>
            
             <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">6.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Qualification </td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
             <asp:Label ID="lblqualification" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "Qualification")%></asp:Label>
            </td></tr>
            
            <tr style="background-color :#E1E1E1 ;line-height:25px;">
               <td Width="5%" align="center"  valign="top">7.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%"  valign="top">Contact No.  </td>
               <td align="left" valign="middle" style=" padding-left :10px; width :40%" >
            <asp:Label ID="lblcontactno" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#"Mobile : "  & DataBinder.Eval(Container.DataItem, "contactno")%></asp:Label><br /> <asp:Label ID="lblll" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#"Landline : " & DataBinder.Eval(Container.DataItem, "stdcode") & " - " & DataBinder.Eval(Container.DataItem, "landlineno")%></asp:Label><br /> <asp:Label ID="lblfax" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#"Fax : " & DataBinder.Eval(Container.DataItem, "faxno")%></asp:Label>
            </td>
            <td rowspan="2" valign="Top"> <asp:Image ID="Image2" runat="server" style="height:55px;width:150px;border:1px solid gray;" ImageUrl='<%#"~/Photos/" & DataBinder.Eval(Container.DataItem, "thumbsign")%>'></asp:Image></td></tr>
            
             <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">8.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Company Email-Id </td>
               <td align="left" style=" padding-left :10px;width :40%;">
             <asp:Label ID="lblcompmailid" runat="server" Width="50%"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "compmailid")%></asp:Label>
            </td></tr>
            
             <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">9.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Personal Email-Id </td>
               <td align="left" style=" padding-left :10px;width :40%;"  colspan="2">
             <asp:Label ID="lblemailid" runat="server" Width="50%"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "Emailid")%></asp:Label>
            </td></tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%"  align="center">10.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">PAN No. </td>
               <td align="left" style=" padding-left :10px;width :40%" >
             <asp:Label ID="lblpaano" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "panno")%></asp:Label>
            </td>
            <td rowspan="5" valign="top" align="left" ><a href='image.aspx?imageid=<%#Eval("passportImg")%>' target="_blank" ><asp:Image ID="Image1" runat="server" ImageUrl='<%#"~/Photos/" & DataBinder.Eval(Container.DataItem, "passportImg")%>' style="height:150px;width:150px;border:1px solid gray;"></asp:Image></a></td>
            </tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%"  align="center">11.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">PF No. </td>
               <td align="left" style=" padding-left :10px;width :40%">
             <asp:Label ID="lblpfno" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "pfno")%></asp:Label>
            </td></tr>
            
                <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">12.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">DL No. </td>
               <td align="left" style=" padding-left :10px;width :40%">
             <asp:Label ID="lbldlno" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "dlno")%></asp:Label>
            </td></tr>
            
                <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">13.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Passport No. </td>
               <td align="left" style=" padding-left :10px;width :40%">
             <asp:Label ID="lblpassportno" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "passno")%></asp:Label>
            </td></tr>
            
             <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">14.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Date of Birth </td>
               <td align="left" style=" padding-left :10px;"  width="40%" colspan="2">
             <asp:Label ID="lbldob" runat="server" Width="50%"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "dob")%></asp:Label>
            </td></tr>
            
             <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">15.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Age </td>
               <td align="left" style=" padding-left :10px;"  width="40%" colspan="2">
             <asp:Label ID="lblage" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "age")%></asp:Label> &nbsp;&nbsp;Years
            </td>
            </tr>
            
             <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">16.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Sex </td>
               <td align="left" style=" padding-left :10px;" colspan="2" >
             <asp:Label ID="lblsex" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "sex")%></asp:Label>
            </td>
            </tr>
             
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center">17.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Nationality </td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
             <asp:Label ID="lblnationality" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "Nationality")%></asp:Label>
            </td></tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center"  valign="top">18.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%"  valign="top">Present/Correspondence Address </td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
               <asp:Label ID="lbldirname1" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%# DataBinder.Eval(Container.DataItem, "dirName")%></asp:Label><br />
               <asp:Label ID="lblpfix" runat="server" Font-Bold="true"  ><%#DataBinder.Eval(Container.DataItem, "pfix")%> </asp:Label>-<asp:Label ID="lbldirfname" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "FatherfName")%></asp:Label><br />
             <asp:Label ID="lblpresentaddress" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "Address")%></asp:Label> <asp:Label ID="lblgram" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label> <asp:Label ID="lblpanchayat" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label><asp:Label ID="lbltehsil" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label>    <asp:Label ID="lblcity" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label> <asp:Label ID="lblstate" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label><asp:Label ID="lblpin" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#", Pincode : " & DataBinder.Eval(Container.DataItem, "pincode")%></asp:Label>
            </td></tr>
            
                   <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td Width="5%" align="center" valign="top">19.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%"  valign="top">Permanent Address </td>
               <td align="left" style=" padding-left :10px;width :40%" colspan="2">
               <asp:Label ID="lbldirname2" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%# DataBinder.Eval(Container.DataItem, "dirName")%></asp:Label><br />
               <asp:Label ID="lblpfix1" runat="server" Font-Bold="true"  ><%#DataBinder.Eval(Container.DataItem, "pfix")%> </asp:Label>-<asp:Label ID="lbldirfname1" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "FatherfName")%></asp:Label><br />
             <asp:Label ID="lblppaddress" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "PPaddress")%></asp:Label>
            </td></tr>
          
          <tr style="background-color :#E1E1E1 ;line-height:25px;">
               <td Width="5%" align="center" valign="top" >20.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%" valign="top">Bank Details </td>
               <td align="left" valign="middle" style=" padding-left :10px; width :40%" colspan="2">
            <asp:Label ID="lblbankdetail" runat="server"  Font-Names="Verdana" Font-Size="9pt" ></asp:Label>
            </td></tr>
            
   
    <tr style="background-color :#EEEEEE ;" ><td colspan="4"  width="100%" align ="center" >
     
             
            </td></tr>
            
            
            </table>
            </div> 
            </ItemTemplate> 
        
        </asp:DataList>
        
    </div>
  </asp:Content>

