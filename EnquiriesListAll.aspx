<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EnquiriesListAll.aspx.vb" MasterPageFile="MasterPage.master" Inherits="EnquiriesListAll" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">

    <title>Enquiry List</title>
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
     <script src="jvalidation/jquery.min.js"></script>
    <script src="jvalidation/jquery.validationEngine-en.js"></script>
    <script src="jvalidation/jquery.validationEngine.js"></script>
    <link href="css/ValidationEngine.css" rel="stylesheet" />
       <link href="css/orange.css" rel="stylesheet" />
    <%--<script language="javascript" type="text/javascript" src="functions.js"></script>--%>
    <%--<script language="javascript" type="text/javascript" src="calander.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">
     <script type ="text/javascript" >
         $(function () {
             $('[id*=btnSubmit').bind("click", function () {
                 $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
             });
         });
        
         
    </script>
     <script type ="text/javascript" >
         $(function () {
             $('[id*=btnSearch').bind("click", function () {
                 $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
             });
         });
         function drp(field, rules, i, options) {
             if ($('[id$=drpmaindvision] option:selected').val() == "0") {
                 return "This Feild Required."
             }
         }

         btnSearch
    </script>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
     <div class="widget container">
            <div class="widget-header ">
                <i class="icon-user"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;LIST OF ALL ENQUIRIES<asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>
   </div> 
             <div class="widget-content"> 
     <table id="tblOrders" runat="server" width="100%" class ="table " ><tr>
             <td>Duration From</td><td>
             <asp:TextBox ID="txtfrmDt" runat="server" onpaste="return false;" onkeydown="return checkKey()" oncut="return false;" placeholder="dd-mm-yyyy" ToolTip="From Date" CssClass="textbox"></asp:TextBox>
             <asp:CalendarExtender ID="TextBox1_CalendarExtender2" runat="server" Enabled="True"  TargetControlID="txtfrmDt" PopupButtonID ="Image1" format="dd-MMM-yyyy" DaysModeTitleFormat="dd-MMM-yyyy" CssClass= " orange" >
             </asp:CalendarExtender>
          <%--<asp:TextBox ID="txtfrmDt" onpaste="return false;" placeholder="dd-mm-yyyy" onkeydown="return checkKey()" oncut="return false;" runat="server" ToolTip="From Date" CssClass="textbox"></asp:TextBox>--%>

                 </td>
      
           <td>To</td><td> <asp:TextBox ID="txtToDt" placeholder="dd-mm-yyyy" ToolTip="From Date" CssClass="textbox" runat="server"></asp:TextBox>
             <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" Enabled="True"  TargetControlID="txtToDt" PopupButtonID ="Image1" format="dd-MMM-yyyy" DaysModeTitleFormat="dd-MMM-yyyy" CssClass= " orange" >
             </asp:CalendarExtender>
       </td>
         </tr>
        <tr>
            <td>Select Division</td>
            <td colspan="3">
            <asp:DropDownList ID="drpmaindvision" runat="server" ToolTip="Divisions" CssClass="validate[required,funcCall[drp[]]]"  ></asp:DropDownList>
            </td>
        </tr>
       <tr>
       <td colspan="4" style="background-color:White">
           <asp:Label ID="lblError" Font-Bold="true" ForeColor="red" runat="server"></asp:Label>
       </td>
       </tr>
       </table>
                 </div> 
         <div class="widget-content ">
       <table width="100%" class="table ">
        <tr>
           <td colspan="4"  align="center"> 
               <center>             
           <asp:Button ID="btnSearch" runat="server" CssClass ="btn btn-info " Text="Search" TabIndex="25" />      </center>             
           </td>
        </tr>
       </table>
               </div>      
     <asp:Panel ID="pnlmain" runat="server" Width="100%" Visible="false" > 
         <div class="widget-content ">
      <table width="100%"  class="table ">
       <tr> 
            <td valign="middle" width="20%" style="padding-left:10px;">Mobile/Contact No*</td>
            <td valign="middle"><asp:TextBox ID="Txtmobno" ToolTip="Mobile/Contact No" runat="server" CssClass ="validate[required,custom[phone]]" Width="250px" ></asp:TextBox></td>
       </tr>
       <tr>
            <td width="20%" valign="middle" style="padding-left:10px;">Email-ID*</td>
            <td valign="middle"><asp:TextBox ID="txtEmailid" ToolTip="Personal Email-Id" runat="server" CssClass ="validate[required,custom[email]]" Width="250px"></asp:TextBox></td>
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
            <td colspan="4" align="center" style="height: 26px"><center><asp:Button ID="btnSubmit" runat="server" Text="Preview" TabIndex="14" Width="100px" Height="32px" cssClass="btn btn-success " /></center></td>
       </tr>
        
    </table>
             </div> 
       <asp:Panel ID="pnlprev" runat="server" Width="100%" Visible="false" > 
           <div class ="widget-content ">
      <table width="100%" class="table ">
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
            <td colspan="4"  style="background-color:white"><asp:Label ID="lblenqerror" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red"></asp:Label></td>
       </tr>
       <tr style="background-color:<%=db.headerBG%>">
            <td colspan="4" align="center" style="height: 26px"><asp:Button ID="btnfSubmit" CssClass ="btn btn-success " runat="server" Text="Conform" TabIndex="14" Width="100px" Height="32px"  /><asp:Button ID="btnedit" CssClass ="btn btn-warning " runat="server" Text="Edit" TabIndex="14" Width="100px" Height="32px"  />
                <asp:Button ID="btndel" runat="server" Text="Delete" CssClass ="btn btn-danger " TabIndex="14" Width="100px" Height="32px"/></td>
       </tr>
        
    </table>
               </div> 
   </asp:Panel>
         
   </asp:Panel>
    
 
        
     <asp:Panel ID="pnlenq" runat="server" Width="100%" >
         <div class ="widget-content ">   
     <table width="100%" class ="table " id="tblForm" >
     <tr>
     <td>
     <asp:GridView ID="GVdivision" runat="server" Width="100%" AutoGenerateColumns="false" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right" CssClass="table table-striped table-bordered" DataKeyNames="division" >
     <Columns>
     <asp:TemplateField HeaderText="S.No.">
     <ItemTemplate>
     
     </ItemTemplate>
     <ItemStyle Width="7%" HorizontalAlign="Center" />
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Division Name" HeaderStyle-HorizontalAlign="Left">
     <ItemTemplate>
     <%#Eval("DName")%>
      </td></tr>
        <tr>
        <td colspan="4">
        <table width="100%" id='tbl<%# Eval("division")%>' cellpadding="0" cellspacing="0" style="font-size:8pt;font-family:Verdana;text-align:justify;" border="0" bordercolor="black">
             <tr>
             <td>
        <asp:GridView ID="GVenquiry" runat="server" AutoGenerateColumns="false" DataKeyNames="enqid" Width="100%" RowStyle-BackColor="blanchedAlmond" AlternatingRowStyle-BackColor="white">
        <Columns>
        <%--<asp:TemplateField>
        <ItemTemplate>
        
        </ItemTemplate>
        <ItemStyle Width="12%" HorizontalAlign="Center" />
        </asp:TemplateField>--%>
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
            <asp:TemplateField HeaderText="Enquiry Date">
            <ItemTemplate>
            <%#Eval("enqdate")%>
            </ItemTemplate>
            <ItemStyle Width="10%" HorizontalAlign="left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Details">
            <ItemTemplate>
            <%#Eval("contactname")%><br />
            <%#Eval("companyName") %><br /> 
            <%#Eval("contactaddress")%>
            </ItemTemplate>
            <ItemStyle Width="20%" VerticalAlign="Top" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contact Details">
            <ItemTemplate>
            <%#Eval("contactno")%><br />
            <%#Eval("EmailId")%>
            </ItemTemplate>
            <ItemStyle Width="15%" VerticalAlign="Top"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remarks">
            <ItemTemplate>
            <%#Eval("remarks")%>
            </ItemTemplate>
            <ItemStyle Width="30%" VerticalAlign="Top"/>
            </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
        <ItemTemplate>
        <a href="EnquiriesListAll.aspx?id=<%#eval("enqid")%>"><i class="btn-icon-only icon-cog" ></i>Edit<%--<img src="Images/EDIT.gif" alt="Edit" style="border:0;"/>--%></a><%--<a href="pnlmain.visible=True">E</a>--%><asp:TextBox ID="txtId" runat="server" Text='<%#Eval("enqid")%>' Visible="false"></asp:TextBox>
        </ItemTemplate>
        <ItemStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle"/>
        </asp:TemplateField>
        </Columns>
         <EmptyDataTemplate><center>No Data Available</center></EmptyDataTemplate>
  <HeaderStyle HorizontalAlign="center" />
  <HeaderStyle  HorizontalAlign="center" />
  <RowStyle  />
        </asp:GridView>
        </td>
        </tr>
        </table>
        </td>
        </tr>
     </ItemTemplate>
     <ItemStyle Width="94%" HorizontalAlign="left" />
     </asp:TemplateField>

    
     </Columns>
      <EmptyDataTemplate><center>No Data Available</center></EmptyDataTemplate>
  <HeaderStyle HorizontalAlign="center" />
  <HeaderStyle  HorizontalAlign="center" />
  <RowStyle />
     </asp:GridView>
     </td>
     </tr>
     <tr>
     <td>
     <asp:Label ID="lblMessage" runat="server" ForeColor="red" Visible="false" Font-Bold="true"></asp:Label>
     </td>
     </tr>
     </table>
</div> 
            </asp:Panel>    
   </asp:Content>
    