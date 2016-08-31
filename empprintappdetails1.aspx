<%@ Page Language="VB" AutoEventWireup="false" CodeFile="empprintappdetails1.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_empprintappdetails1" EnableEventValidation ="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <title>Employee Details</title>
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server"> 
   <div style="padding-left:15px;width:auto ;">
       <asp:Panel ID="Panel1" runat="server">
      
        <asp:DataList ID="dlstMain" runat="server" width="97%" DataKeyField="empid" Visible="True" RepeatColumns ="1">
        <ItemTemplate >
              <div class="widget-content">
        <table class="table " >
            <div class ="widget-header " >   <i class="icon-pencil "></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <b>Employee Profile & Details Report</b>
     
            </td></div>
       <%-- 
        <table width="100%" border="0" style="background-color :#EEEEEE ; border-color :White ;" >
         <tr style="background-color :#EEEEEE ;line-height:20px;" ><td colspan="4"  width="100%"   align ="center" >
        <b><span style=" font-size :large ; ">Employee Profile & Details Report</span> </b>
              
            </td></tr>--%>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
            <td align="left"  colspan="3" style=" padding-left :10px;">Employee No. :<b> <%#DataBinder.Eval(Container.DataItem, "empno")%></b></td>
            <td align="left" style=" padding-left :10px;">Date:<asp:Label ID="lbldate" runat="server"></asp:Label></td>
            <%--<td align="right"  colspan="2" style=" padding-right :10px;" >
            Registration Date : <asp:Label ID="lbldate" runat="server"  Font-Names="Verdana" Font-Bold="true" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "dt")%></asp:Label>
            </td>--%>
            </tr>
<tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td width="5%" align="center">1.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Employee Name</td>
               <td align="left" width="60%">
             <asp:Label ID="lblname" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "dirName")%></asp:Label>
            </td>
            <td rowspan="5" valign="top" align="left" ><asp:Image ID="Image3" runat="server" ImageUrl='<%#"~/Photos/" & DataBinder.Eval(Container.DataItem, "photo")%>' style="height:150px;width:150px;border:1px solid gray;"></asp:Image></td></tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td width="5%" align="center">2.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Designation Name</td>
                <td align="left"  width="60%"><asp:Label ID="lbldesignation" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "designation")%></asp:Label>
                </td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td width="5%" align="center">3.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Division Name  </td>
                <td align="left" width="60%"><asp:Label ID="lbldivision" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "division")%></asp:Label>
                </td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td width="5%" align="center">4.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Company Name  </td>
                <td align="left" width="60%"><asp:Label ID="lblcompname" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "companyName")%></asp:Label>
                </td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td width="5%" align="center">5.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Date of Joining</td>
                <td align="left" width="60%" ><asp:Label ID="lbldoj" runat="server" width="50%"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "doj")%> (<%#DataBinder.Eval(Container.DataItem, "emptype")%>)</asp:Label>
                </td>
                
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td width="5%" align="center">6.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Reporting To</td>
                <td align="left" width="60%" colspan="2"><asp:Label ID="lbldinNo" runat="server" width="50%"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "reportto")%></asp:Label>
                </td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td width="5%" align="center">7.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Qualification </td>
                <td align="left" width="60%"><asp:Label ID="lblqualification" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "Qualification")%></asp:Label>
</td>
                <td rowspan="2" valign="Top"> <asp:Image ID="Image2" runat="server" style="height:55px;width:150px;border:1px solid gray;" ImageUrl='<%#"~/Photos/" & DataBinder.Eval(Container.DataItem, "thumbsign")%>'></asp:Image></td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td width="5%" align="center" valign="top">8.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%" valign="top">Contact No.  </td>
               <td align="left" valign="middle" width="60%">
            <asp:Label ID="lblcontactno" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#"Mobile : "  & DataBinder.Eval(Container.DataItem, "contactno")%></asp:Label><br /> <asp:Label ID="lblll" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#"Landline : " & DataBinder.Eval(Container.DataItem, "stdcode") & " - " & DataBinder.Eval(Container.DataItem, "landlineno")%></asp:Label><br /> <asp:Label ID="lblfax" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#"Fax : " & DataBinder.Eval(Container.DataItem, "faxno")%></asp:Label></td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td width="5%" align="center">9.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Company Email-Id </td>
               <td align="left" width="60%" colspan="2">
             <asp:Label ID="lblcompmailId" runat="server" width="50%"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "compmailId")%></asp:Label>
            </td></tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td width="5%" align="center">10.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Personal Email-Id </td>
               <td align="left" width="60%">
             <asp:Label ID="lblemailid" runat="server" width="50%"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "Emailid")%></asp:Label></td>
            <td rowspan="5" valign="top" align="left" ><a href='image.aspx?imageid=<%#Eval("passportImg")%>' target="_blank" ><asp:Image ID="Image1" runat="server" ImageUrl='<%#"~/Photos/" & DataBinder.Eval(Container.DataItem, "passportImg")%>' style="height:150px;width:150px;border:1px solid gray;"></asp:Image></a></td>
                </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td width="5%"  align="center">11.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">PAN No. </td>
                <td align="left" width="60%"><asp:Label ID="lblpaano" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "panno")%></asp:Label></td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td width="5%"  align="center">12.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">PF No. </td>
                <td align="left" width="60%"><asp:Label ID="lblpfno" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "pfno")%></asp:Label></td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td width="5%" align="center">13.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">DL No. </td>
                <td align="left" width="60%"><asp:Label ID="lbldlno" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "dlno")%></asp:Label>
                </td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td width="5%" align="center">14.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Passport No. </td>
                <td align="left" width="60%"><asp:Label ID="lblpassportno" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "passno")%></asp:Label>
                </td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td width="5%" align="center">15.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Sex </td>
                <td align="left" width="60%" colspan="2" ><asp:Label ID="lblsex" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "sex")%></asp:Label></td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td width="5%" align="center">16.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Date of Birth </td>
                <td align="left" width="60%" colspan="2"><asp:Label ID="lbldob" runat="server" width="50%"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "dob")%></asp:Label></td>
                
            </tr>
            <tr style="background-color :#E1E1E1 ;line-height:25px;">
                <td width="5%" align="center" valign="top" >17.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Age </td>
                <td align="left" width="60%" colspan="2" ><asp:Label ID="lblage" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "age")%></asp:Label> &nbsp;&nbsp;Years</td>
               </tr>
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td width="5%" align="center">18.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%">Nationality </td>
               <td align="left" width="60%" colspan="2">
             <asp:Label ID="lblnationality" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "Nationality")%></asp:Label>
            </td></tr>
            
            
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
                <td width="5%" align="center">19.</td>
                <td style=" padding-left :20px; font-size:10pt;" width="25%"> <asp:Label ID="lblpfix" runat="server" Font-Bold="true"  ><%#DataBinder.Eval(Container.DataItem, "pfix")%> </asp:Label> </td>
                <td align="left" width="60%" colspan="2"><asp:Label ID="Label1" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "FatherfName")%></asp:Label>
                </td>
            </tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td width="5%" align="center" valign="top">20.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%" valign="top">Present/Correspondence Address </td>
               <td align="left" width="60%" colspan="2">
             <asp:Label ID="lblpresentaddress" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "Address")%></asp:Label> <asp:Label ID="lblgram" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label> <asp:Label ID="lblpanchayat" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label><asp:Label ID="lbltehsil" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label>    <asp:Label ID="lblcity" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label> <asp:Label ID="lblstate" runat="server"   Font-Names="Verdana" Font-Size="9pt" ></asp:Label><asp:Label ID="lblpin" runat="server"   Font-Names="Verdana" Font-Size="9pt" ><%#", Pincode : " & DataBinder.Eval(Container.DataItem, "pincode")%></asp:Label>
            </td></tr>
            <tr style="background-color :#E1E1E1 ; line-height:20px;">
               <td width="5%" align="center" valign="top">21.</td>
                <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%" valign="top">Permanent Address </td>
               <td align="left" width="60%" colspan="2">
             <asp:Label ID="lblppaddress" runat="server"  Font-Names="Verdana" Font-Size="9pt" ><%#DataBinder.Eval(Container.DataItem, "PPaddress")%></asp:Label>
            </td></tr>
            <tr style="background-color :#E1E1E1 ;line-height:25px;">
               <td width="5%" align="center" valign="top" >22.</td>
               <td style=" padding-left :20px; font-size:10pt;font-weight :bold ;" width="25%" valign="top">Bank Details </td>
               <td align="left" valign="middle" width="60%" colspan="2">
            <asp:Label ID="lblbankdetail" runat="server"  Font-Names="Verdana" Font-Size="9pt" ></asp:Label>
            </td></tr>
   
    <tr style="background-color :#EEEEEE ;" ><td colspan="4"  width="100%" align ="center" >
     
             
            </td></tr>
            
            
            </table>
            </div> 
            </ItemTemplate> 
        
        </asp:DataList>
         </asp:Panel>
         <table width="100%"><tr><td align="center"><asp:Button ID="btnprint" runat="server" CssClass ="btn btn-success " Text="Print" Height="30px" Width="58px" /></td></tr></table>
    </div>
    </asp:Content> 