<%@ Page Language="VB" AutoEventWireup="false" CodeFile="printClientPoDetails1.aspx.vb"  MasterPageFile="MasterPage.master" Inherits="Admin_printClientPoDetails1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
   <title>Client PO Detail</title>
     <script src="jvalidation/jquery.min.js"></script>
    <script src="jvalidation/jquery.validationEngine-en.js"></script>
    <script src="jvalidation/jquery.validationEngine.js"></script>
    <link href="css/ValidationEngine.css" rel="stylesheet" />
     <%--<script  src="functions.js" type="text/javascript"></script>--%>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server"> 
   
<div class="widget-container" >
  
    <asp:DataList id="dlstMain" runat="server" RepeatColumns="1" Visible="True" DataKeyField="poWorkAssignID" Width="100%"><ItemTemplate>
        <div class="widget-content ">
<TABLE class="table "><TBODY><TR ><TD style="HEIGHT: 22px" align="center" width="100%" colSpan="4"><B><SPAN style="FONT-SIZE: large">Client PO Details</SPAN></B></TD></TR><TR ><TD style="PADDING-LEFT: 10px; HEIGHT: 22px" align="left" colSpan="4">Record No. :<B> <asp:Label id="Label1" runat="server" Text='<%# Eval("poWorkAssignID") %>'></asp:Label> </b></TD></TR><TR "><TD style="WIDTH: 5%; HEIGHT: 22px" align=center>1.</TD><TD style="PADDING-LEFT: 20px; FONT-WEIGHT: bold; FONT-SIZE: 10pt; HEIGHT: 22px; width: 11%;">PO Date&nbsp;</TD><TD style="PADDING-LEFT: 10px; HEIGHT: 22px; width: 136px;" align=left><asp:Label id="lblname" runat="server" Text='<%# Eval("poDate") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label> </TD><TD vAlign=top align=left rowSpan=5></TD></TR><TR ><TD style="WIDTH: 5%" align=center>2.</TD><TD style="PADDING-LEFT: 20px; FONT-WEIGHT: bold; FONT-SIZE: 10pt; width: 11%;">PO Reciving Date&nbsp;</TD><TD style="PADDING-LEFT: 10px; width: 136px;" align=left><asp:Label id="lbldivision" runat="server" Text='<%# Eval("poRecivingDate") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label> </TD><TD></TD></TR><TR "><TD style="WIDTH: 5%" align=center>3.</TD><TD style="PADDING-LEFT: 20px; FONT-WEIGHT: bold; FONT-SIZE: 10pt; width: 11%;">Clint Name&nbsp;</TD><TD style="PADDING-LEFT: 10px; width: 136px;" align=left><asp:Label id="lblcompname" runat="server" Text='<%# Eval("clientID") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label> </TD></TR><TR ><TD style="WIDTH: 5%" align=center>4.</TD><TD style="PADDING-LEFT: 20px; FONT-WEIGHT: bold; FONT-SIZE: 10pt; width: 11%;">Assignment related to </TD><TD style="PADDING-LEFT: 10px; width: 136px;" align=left><asp:Label id="lbldinNo" runat="server" Width="50%" Text='<%# Eval("dcode") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label> </TD></TR><TR ><TD style="WIDTH: 5%; HEIGHT: 22px" align=center>5.</TD><TD style="PADDING-LEFT: 20px; FONT-WEIGHT: bold; FONT-SIZE: 10pt; HEIGHT: 22px; width: 11%;">Work Activity</TD><TD style="PADDING-LEFT: 10px; HEIGHT: 22px; width: 136px;" align=left><asp:Label id="lbldoj" runat="server" Width="50%" Text='<%# Eval("serviceid") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label> </TD></TR><TR ><TD style="WIDTH: 5%; HEIGHT: 22px" align=center>6.</TD><TD style="PADDING-LEFT: 20px; FONT-WEIGHT: bold; FONT-SIZE: 10pt; HEIGHT: 22px; width: 11%;">Remarks</TD><TD style="PADDING-LEFT: 10px; WIDTH: 40%; HEIGHT: 22px" align=left colSpan=2><asp:Label id="lblqualification" runat="server" Text='<%# Eval("poRemarks") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label> </TD></TR><TR style="LINE-HEIGHT: 25px; BACKGROUND-COLOR: #e1e1e1"><TD style="WIDTH: 5%" vAlign=top align=center>7.</TD><TD style="PADDING-LEFT: 20px; FONT-WEIGHT: bold; FONT-SIZE: 10pt; width: 11%;" vAlign=top>Work Complete in DAys</TD><TD style="PADDING-LEFT: 10px; WIDTH: 136px" vAlign=middle align=left><asp:Label id="lblcontactno" runat="server" Text='<%# Eval("wrkCmpleteinDays") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label>&nbsp;</TD><TD vAlign=top rowSpan=2></TD></TR><TR ><TD style="WIDTH: 5%" align=center>8.</TD><TD style="PADDING-LEFT: 20px; FONT-WEIGHT: bold; FONT-SIZE: 10pt; width: 11%;">Target Date</TD><TD style="PADDING-LEFT: 10px; WIDTH: 136px" align=left><asp:Label id="lblcompmailid" runat="server" Width="50%" Text='<%# Eval("wrkCmpletTrgtDate") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label> </TD></TR><TR ><TD style="WIDTH: 5%" align=center>9.</TD><TD style="PADDING-LEFT: 20px; FONT-WEIGHT: bold; FONT-SIZE: 10pt; width: 11%;">Purchase Order Value</TD><TD style="PADDING-LEFT: 10px; WIDTH: 40%" align=left colSpan=2><asp:Label id="lblemailid" runat="server" Width="50%" Text='<%# Eval("basePoValue") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label> </TD></TR><TR ><TD style="WIDTH: 5%" align=center>10.</TD><TD style="PADDING-LEFT: 20px; FONT-WEIGHT: bold; FONT-SIZE: 10pt; width: 11%;">Service Tax</TD><TD style="PADDING-LEFT: 10px; WIDTH: 136px" align=left><asp:Label id="lblpaano" runat="server" Text='<%# Eval("serTax") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label> </TD></TR><TR ><TD style="WIDTH: 5%" align=center>11.</TD><TD style="PADDING-LEFT: 20px; FONT-WEIGHT: bold; FONT-SIZE: 10pt; width: 11%;">Vat</TD><TD style="PADDING-LEFT: 10px; WIDTH: 136px" align=left><asp:Label id="lblpfno" runat="server" Text='<%# Eval("vat") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label> </TD></TR><TR ><TD style="WIDTH: 5%" align=center>12.</TD><TD style="PADDING-LEFT: 20px; FONT-WEIGHT: bold; FONT-SIZE: 10pt; width: 11%;">Total Amount</TD><TD style="PADDING-LEFT: 10px; WIDTH: 136px" align=left><asp:Label id="lbldlno" runat="server" Text='<%# Eval("totalAmnt") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label> </TD></TR><TR style="BACKGROUND-COLOR: #eeeeee"><TD align=center width="100%" colSpan=4></TD></TR></TBODY></TABLE>
</div>
</ItemTemplate>
</asp:DataList>
    <div class="widget-content ">
<TABLE  width="100%" ><TBODY><TR ><td align="center" colspan =4><B><div class="widget container "></div><div class="widget-header "><SPAN style="FONT-SIZE: large">Payment Stage</SPAN></div></div></B></TD></TR> 

<tr ><td align ="left"  width="20%">Name</td><td align ="left" width="20%">Percentage</td><td align ="left" width="20%">Amount</td><td align ="left" width="20%">Status</td><td align ="left" width="20%">Date</td></TR>





</TBODY> </TABLE> 
    </div>
    <asp:DataList id="dlstPmntStg" runat="server" RepeatColumns="1" Visible="True" DataKeyField="poWorkAssignID" Width="100%"><ItemTemplate>
<div class="widget-content ">
<TABLE width="100%" class="table ">
<TBODY>


<%--<TR style="LINE-HEIGHT: 20px; BACKGROUND-COLOR: #e1e1e1" width="100%" >--%>
<%--<tr ><th align ="left" >Name</th><th align ="left">Percentage</th><th align ="left" >Amount</th><th align ="left">Status</th><th align ="left">Date</th></TR>--%><tr>
<TD  style="PADDING-LEFT: 10px; HEIGHT: 25px" width="20%" align="left" ><asp:Label id="lblname" runat="server" Text='<%# Eval("poStageName") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label> </TD><TD width="20%"  style="PADDING-LEFT: 10px; HEIGHT: 25px" align="left" ><asp:Label id="Label2" runat="server" Text='<%# Eval("poStagePercntg") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label> </TD><TD width="20%" style="PADDING-LEFT: 10px; HEIGHT: 25px" align="left" ><asp:Label id="Label3" runat="server" Text='<%# Eval("poStageAmnt") %>' Font-Size="9pt" Font-Names="Verdana" ></asp:Label> </TD><TD width="20%" style="PADDING-LEFT: 10px; HEIGHT: 25px" align="left"><asp:Label id="Label4" runat="server" Text='<%# Eval("poStageStatus") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label> </TD><TD width="20%" style="PADDING-LEFT: 10px; HEIGHT: 25px" align="left"><asp:Label id="Label5" runat="server" Text='<%# Eval("poStagePaymentDate") %>' Font-Size="9pt" Font-Names="Verdana"></asp:Label> </TD></TR>
</TBODY> </TABLE> </div></ItemTemplate> </asp:DataList> 




    <div class="widget-content ">

<table  width="100%" class="table"><tr "><td align="center" ><asp:Button ID="btnReconfirm" runat="server" CssClass ="btn btn-success " Text="Submit" Width="100px" Height="30px"></asp:Button>&nbsp;&nbsp;<asp:Button ID="btnedit" CssClass ="btn btn-warning " Width="70px" runat="server" Text="Edit" Height="30px"></asp:Button>&nbsp;&nbsp;<asp:Button ID="btnpreview" CssClass ="btn btn-primary " Width="70px" runat="server" Text="Print" Height="30px"></asp:Button></td></tr></table>
   </div> </div>
   </asp:Content>