<%@ Page Language="VB" AutoEventWireup="false" CodeFile="work_assignment.aspx.vb" MasterPageFile="MasterPage.master" Inherits="Admin_work_assignment" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
     <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
      <link href="css/orange.css" rel="stylesheet" />


  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
     <script type  ="text/javascript" >
      var l=0;
      function calDate() {
        var m = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

          var n=document.getElementById ('txt_tim_frame').value;
          var date1 = document.getElementById('txt_po_recvDate').value;
          var dp = date1.split("-");
           var newDt = new Date(dp[1] + " " + dp[0] + ", " + dp[2]);
           newDt.setDate(newDt.getDate()+ parseInt(n));
           dt = newDt.getDate() + "-" + m[newDt.getMonth()] + "-" + newDt.getFullYear();
          document.getElementById("txt_trgt_date").value = dt;
         
}


function cal_payStg_per(targt,i)
{

    var a=parseFloat (targt.value);
    var c=parseFloat(document.getElementById ('txt_total_amnt_inc').value)
    var b=(a*c)/100;
    var d='txt_pmnt_stg_amt'+i;
     document.getElementById (d).value=b;
}

function cal_payStg_amt(targt,i)
{

    var a=parseFloat (targt.value);
    var c=parseFloat(document.getElementById ('txt_total_amnt_inc').value)
    var b=(a*100)/c;
    var d='txt_pmnt_stg_per'+i;

     document.getElementById (d).value=b;
      
}
  
  function is_valid_tot()
  {    
           alert("Total Percentage is not equal to 100 , Please Check");
     
  
  }

     </script>

   <div class="widget container">
            <div class="widget-header ">
                <i class="icon-user"></i>
       
    <asp:Label ID="lbl_head" Text="Clients Purchase Order Details" runat ="server" Font-Size="X-Large" Font-Bold="True"></asp:Label>
    </div> <div class="widget-content ">
                 <table width="100%" class="table "> 
   
    <tr>
        <td align ="Left" style="width: 278px"  >P.O Date </td><td align ="left" >
        <asp:TextBox ID="txt_po_date" onpaste="return false;" placeholder="dd-mm-yyyy" onkeydown="return checkKey()" oncut="return false;" runat="server" CssClass="textbox" ></asp:TextBox>
        <asp:CalendarExtender ID="TextBox1_CalendarExtender"  runat="server" Enabled="True" TargetControlID="txt_po_date" PopupButtonID ="Image1" CssClass= " orange"  format="dd-MMM-yyyy" DaysModeTitleFormat="dd-MMM-yyyy">

        </asp:CalendarExtender>
       </td>
          </tr>
    <tr>
          <td align ="Left" style="width: 278px"  >
              P.O Reciving Date </td><td align ="left" >
              <asp:TextBox ID="txt_po_recvDate" onpaste="return false;" placeholder="dd-mm-yyyy" onkeydown="return checkKey()" oncut="return false;" runat="server" CssClass="textbox" ></asp:TextBox>
              <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" Enabled="True" TargetControlID="txt_po_recvDate" PopupButtonID ="Image1" CssClass="orange "  format="dd-MMM-yyyy" DaysModeTitleFormat="dd-MMM-yyyy">

              </asp:CalendarExtender>
            </td> 
    </tr>
     <tr>
          <td align ="Left" style="height: 24px; width: 278px;"  >
              Client/Company Name </td><td align ="left" style="height: 24px" ><asp:DropDownList ID="drp_clientname" runat="server" ></asp:DropDownList> </td>
    
    </tr>
        <tr>
          <td align ="Left" style="width: 278px"  >
              Assignment Related to (Department Name)
          </td><td align ="left" ><asp:DropDownList ID="drp_dep_name" runat="server" AutoPostBack="True" ></asp:DropDownList> </td>
    
    </tr>
       <tr>
          <td align ="Left" style="width: 278px"  >
              Work Activity </td><td align ="left" ><asp:DropDownList ID="drp_activity" runat="server" ></asp:DropDownList> </td>
    
    </tr>
      <tr>
          <td align ="Left" style="width: 278px"  >Remark /Details </td><td align ="left" ><asp:TextBox ID="txt_remark" TextMode ="multiLine"  runat="server" ></asp:TextBox></td>
    
    </tr>
        <tr>
            <td align="left" style="height: 21px; width: 278px;">
                <strong>Delivery Time</strong></td>
            <td align="left" style="height: 21px">
            </td>
        </tr>
      <tr>
          <td align ="Left" style="width: 278px"  >
              Work Complete in
          </td><td align ="left" >
          <asp:TextBox ID="txt_tim_frame" runat="server" onblur="calDate()"></asp:TextBox>
              in days</td>
    
    </tr>
      <tr>
          <td align ="Left" style="width: 278px"  >Target Date </td><td align ="left" ><asp:TextBox ID="txt_trgt_date" runat="server" ></asp:TextBox></td>
    
    </tr>
        <tr>
            <td align="left" style="width: 278px">
                <strong>Payment Terms</strong></td>
            <td align="left">
            </td>
        </tr>
      <tr>
          <td align ="Left" style="width: 278px"  >Purchase Order value</td><td align ="left" ><asp:TextBox ID="txt_purchase_order_value" runat="server" ></asp:TextBox></td>
    
    </tr>
    
      <tr>
          <td align ="Left" style="width: 278px"  >Tax </td><td align ="left" ><asp:RadioButtonList ID ="rdbtn_tax_incl_excl" runat="server" RepeatDirection="Horizontal" Width="50%" AutoPostBack="True" >
              <asp:ListItem Value="0">Include</asp:ListItem>
              <asp:ListItem Value="1">Exclude</asp:ListItem>
          </asp:RadioButtonList></td>
    
    </tr>
    <tr><td style="width: 278px">
        &nbsp;</td> <td width="70%"><asp:Panel ID="panel1" runat ="server" Width="80%" ><table width="100%" class ="table "><tr><td style="height: 26px"> Applicable Tax
            </td><td style="height: 26px"><asp:DropDownList ID="drp__serv_tax" runat ="server" AutoPostBack="True" ></asp:DropDownList></td><td style="width: 158px; height: 26px;">
            <asp:TextBox ID="txt_ser_tax_val" runat ="server" ></asp:TextBox></td></tr> 
            <tr>
                <td style="height: 26px">
                </td>
                <td style="height: 26px">
                    <asp:DropDownList ID="drpVat" runat ="server" AutoPostBack="True" >
                    </asp:DropDownList></td>
                <td style="width: 158px; height: 26px">
                <asp:TextBox ID="txtVat" runat="server" ></asp:TextBox>
                  </td> 
            </tr>
        </table></asp:Panel> </td>
    </tr> 
     <tr><td style="height: 184px; width: 278px;">
        &nbsp;</td> <td style="height: 184px"><asp:Panel ID="panl_tax_include" Width ="100%" runat="server">
        <table width="100%" class ="table ">
        <tr>
        <td style="height: 26px">
            Base Amount  </td><td style="height: 26px"><asp:TextBox ID="txt_receive_amnt" runat ="server" ></asp:TextBox></td>
       
        
        </tr>  <tr>
        <td>
       Service Tax </td><td>
       <asp:TextBox ID="txt_service_tax" runat ="server" ></asp:TextBox></td>
       
        
        </tr>  
            <tr>
                <td style="height: 21px">
                    Vat</td>
                <td style="height: 21px">
                    <asp:TextBox ID="txtVtaxAmnt" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
        <td>
       Total Amount  </td><td><asp:TextBox ID="txt_total_amnt_inc" runat ="server" ></asp:TextBox></td>
       
        
        </tr>
        
        
        
        
        </table>
        
        
        </asp:Panel></td>
    </tr> 
        <tr>
            <td align="left" style="width: 278px">
                Payment Terms :-</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" rowspan ="7" style="width: 278px">
            </td>
            <td>
                Stage Name &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                Percentage &nbsp; &nbsp;Amount &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" Text="Date" Visible="False"></asp:Label>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Label ID="Label2" runat="server" Text="Status" Visible="False"></asp:Label></td>
        </tr>
         <tr>
             <td style="height: 26px">
                 <asp:TextBox ID="txt_pmnt_stg_nam0" runat="server"></asp:TextBox>
                 <asp:TextBox ID="txt_pmnt_stg_per0" runat="server" onblur="cal_payStg_per(this,'0')" Width="75px" AutoPostBack="True">0</asp:TextBox>
                 <asp:TextBox ID="txt_pmnt_stg_amt0" runat="server" onblur="cal_payStg_amt(this,'0')" AutoPostBack="True">0</asp:TextBox>
                 <asp:TextBox ID="txt_pmnt_stg_date0" visible ="false" runat="server" oncut="return false;" onkeydown="return checkKey()"
                     onpaste="return false;" Width="75px"></asp:TextBox>
                 <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/calander.gif" OnClientClick ="showTBL('txt_po_date')" />--%>
                 <img src="Images/calander.gif" visible ="false"   runat="server" id="Img0" onclick="showTBL('txt_pmnt_stg_date0')" alt=""/>
                 <asp:DropDownList ID="drp_pmnt_stg_chk0"  visible ="false" runat ="server" >
                     <asp:ListItem>Pending</asp:ListItem>
                     <asp:ListItem>Clear</asp:ListItem>
                 </asp:DropDownList>
                 <asp:LinkButton ID="lnk_btn_add0" runat="server" OnClientClick ="tot_per()">Add More</asp:LinkButton></td>
         </tr>
         <tr>
             <td style="height: 26px">
                 <asp:TextBox ID="txt_pmnt_stg_nam1" runat="server" Visible="False"></asp:TextBox>
                 <asp:TextBox ID="txt_pmnt_stg_per1" runat="server" Visible="False"  onblur="cal_payStg_per(this,'1')" Width="75px" AutoPostBack="True">0</asp:TextBox>
                 <asp:TextBox ID="txt_pmnt_stg_amt1" runat="server" Visible="False" onblur="cal_payStg_amt(this,'1')" AutoPostBack="True">0</asp:TextBox>
                 <asp:TextBox ID="txt_pmnt_stg_date1" visible ="false" runat="server" oncut="return false;" onkeydown="return checkKey()"
                     onpaste="return false;" Width="75px"></asp:TextBox>
                 <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/calander.gif" OnClientClick ="showTBL('txt_po_date')" />--%>
                 <img src="Images/calander.gif" runat="server" id="Img1" visible ="false" onclick="showTBL('txt_pmnt_stg_date1')" alt=""/>
                 <asp:DropDownList ID="drp_pmnt_stg_chk1" visible ="false" runat ="server" >
                     <asp:ListItem>Pending</asp:ListItem>
                     <asp:ListItem>Clear</asp:ListItem>
                 </asp:DropDownList>
                 <asp:LinkButton ID="lnk_btn_add1" runat="server" Visible="False">Add More</asp:LinkButton></td>
         </tr>
         <tr>
             <td>
                 <asp:TextBox ID="txt_pmnt_stg_nam2" runat="server" Visible="False"></asp:TextBox>
                 <asp:TextBox ID="txt_pmnt_stg_per2" runat="server" Visible="False"  onblur="cal_payStg_per(this,'2')" Width="75px" AutoPostBack="True">0</asp:TextBox>
                 <asp:TextBox ID="txt_pmnt_stg_amt2" runat="server" Visible="False" onblur="cal_payStg_amt(this,'2')" AutoPostBack="True">0</asp:TextBox>
                <asp:TextBox ID="txt_pmnt_stg_date2" visible ="false" runat="server" oncut="return false;" onkeydown="return checkKey()"
                     onpaste="return false;" Width="75px"></asp:TextBox>
                 <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/calander.gif" OnClientClick ="showTBL('txt_po_date')" />--%>
                 <img src="Images/calander.gif" runat="server" visible ="false" id="Img2" onclick="showTBL('txt_pmnt_stg_date2')" alt=""/>
                 <asp:DropDownList ID="drp_pmnt_stg_chk2" visible ="false" runat ="server" >
                     <asp:ListItem>Pending</asp:ListItem>
                     <asp:ListItem>Clear</asp:ListItem>
                 </asp:DropDownList>
                 <asp:LinkButton ID="lnk_btn_add2" runat="server" Visible="False">Add More</asp:LinkButton></td>
         </tr>
         <tr>
             <td style="height: 26px">
                 <asp:TextBox ID="txt_pmnt_stg_nam3" runat="server" Visible="False"></asp:TextBox>
                 <asp:TextBox ID="txt_pmnt_stg_per3" runat="server" Visible="False"  onblur="cal_payStg_per(this,'3')" Width="75px" AutoPostBack="True">0</asp:TextBox>
                 <asp:TextBox ID="txt_pmnt_stg_amt3" runat="server" Visible="False" onblur="cal_payStg_amt(this,'3')" AutoPostBack="True">0</asp:TextBox>
                 <asp:TextBox ID="txt_pmnt_stg_date3" visible ="false" runat="server" oncut="return false;" onkeydown="return checkKey()"
                     onpaste="return false;" Width="75px"></asp:TextBox>
                 <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/calander.gif" OnClientClick ="showTBL('txt_po_date')" />--%>
                 <img src="Images/calander.gif" visible ="false" runat="server" id="Img3" onclick="showTBL('txt_pmnt_stg_date3')" alt=""  />
                 <asp:DropDownList ID="drp_pmnt_stg_chk3" visible ="false" runat ="server" >
                     <asp:ListItem>Pending</asp:ListItem>
                     <asp:ListItem>Clear</asp:ListItem>
                 </asp:DropDownList>
                 <asp:LinkButton ID="lnk_btn_add3" runat="server" Visible="False">Add More</asp:LinkButton></td>
         </tr>
         <tr>
             <td style="height: 26px">
                 <asp:TextBox ID="txt_pmnt_stg_nam4" runat="server" Visible="False"></asp:TextBox>
                 <asp:TextBox ID="txt_pmnt_stg_per4" runat="server" Visible="False"  onblur="cal_payStg_per(this,'4')" Width="75px" AutoPostBack="True">0</asp:TextBox>
                 <asp:TextBox ID="txt_pmnt_stg_amt4" runat="server" Visible="False" onblur="cal_payStg_amt(this,'4')" AutoPostBack="True">0</asp:TextBox>
                  <asp:TextBox ID="txt_pmnt_stg_date4" visible ="false" runat="server" oncut="return false;" onkeydown="return checkKey()"
                     onpaste="return false;" Width="75px"></asp:TextBox>
                 <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/calander.gif" OnClientClick ="showTBL('txt_po_date')" />--%>
                 <img src="Images/calander.gif" visible ="false" runat="server" id="Img4" onclick="showTBL('txt_pmnt_stg_date4')" alt=""/>
                 <asp:DropDownList ID="drp_pmnt_stg_chk4" visible ="false" runat ="server" >
                     <asp:ListItem>Pending</asp:ListItem>
                     <asp:ListItem>Clear</asp:ListItem>
                 </asp:DropDownList>
                 </td>
                 
         </tr>
         <tr>
             <td>
                 <strong>Total PercenTage &nbsp; &nbsp; :-&nbsp; </strong>&nbsp;<asp:TextBox  ID="txtTotPer" runat="server" Width="60px" ></asp:TextBox><asp:Label
                     ID="lblerr" runat="server" Text="Total percentage Must Be 100" Visible="False"></asp:Label>
                 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:LinkButton ID="lnkper" runat="server">Cal Percentage</asp:LinkButton>
                 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                 &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
             </td>
         </tr>
        <tr>
            <td align="left" style="height: 21px; width: 278px;">
                Deriables :
            </td>
            <td style="height: 21px">
                <asp:TextBox ID="txtDeriables" runat="server" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="left" style="height: 21px; width: 278px;">
            </td>
            <td style="height: 21px" >
                <asp:Button ID="btnPODetailsSave" CssClass ="btn btn-success " runat="server" Text="Save" Enabled="False" />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td> 
        </tr>
    
     </table>
        </div> 
    </div>
    </asp:Content>
    
