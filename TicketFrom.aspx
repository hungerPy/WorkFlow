<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TicketFrom.aspx.vb" MasterPageFile="MasterPage.master" Inherits="TicketFrom" EnableEventValidation="False" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="jvalidation/jquery.min.js"></script>
    <script src="jvalidation/jquery.validationEngine-en.js"></script>
    <script src="jvalidation/jquery.validationEngine.js"></script>
    <link href="css/ValidationEngine.css" rel="stylesheet" />
    <link href="css/red.css" rel="stylesheet" />

    <link rel="icon" href="images/hslogo.jpg" type="image/x-icon" />
    <style type="text/css"> 
        /* popup_box DIV-Styles*/
        #popup_box {
            display: none; /* Hide the DIV */
            position: fixed;
            _position: absolute; /* hack for internet explorer 6 */
            height: 400px;
            width: 600px;
            background: #FFFFFF;
            left: 100px;
            top: 100px;
            z-index: 100; /* Layering ( on-top of others), if you have lots of layers: I just maximized, you can change it yourself */
            margin-left: 0px;
            /* additional features, can be omitted */
            border: 2px solid #ff0000;
            padding: 15px;
            font-size: 15px;
            -moz-box-shadow: 0 0 5px #ff0000;
            -webkit-box-shadow: 0 0 5px #ff0000;
            box-shadow: 0 0 5px #ff0000;
        }

        a {
            cursor: pointer;
            text-decoration: none;
        }

        .h {
            text-transform: capitalize;
        }

        /* This is for the positioning of the Close Link */
        #popupBoxClose {
            font-size: 20px;
            line-height: 15px;
            right: 5px;
            top: 5px;
            position: absolute;
            color: #6fa5e2;
            font-weight: 500;
        }

        #selectedFiles img {
            max-width: 125px;
            max-height: 125px;
            float: left;
            margin-bottom: 10px;
        }

        .auto-style2 {
            height: 25px;
        }
    </style>
    <script>
          
        function checkKey() {
            if (window.event.keyCode != 9)
                return false;
        }

        var selDiv = "";

        document.addEventListener("DOMContentLoaded", init, false);

        function init() {
            document.querySelector('#files').addEventListener('change', handleFileSelect, false);
            selDiv = document.querySelector("#selectedFiles");
        }

        function handleFileSelect(e) {

            if (!e.target.files || !window.FileReader) return;

            selDiv.innerHTML = "";

            var files = e.target.files;
            var filesArr = Array.prototype.slice.call(files);
            filesArr.forEach(function (f) {
                if (!f.type.match("image.*")) {
                    return;
                }

                var reader = new FileReader();
                reader.onload = function (e) {
                    var html = "<img src=\"" + e.target.result + "\">" + f.name + "<br clear=\"left\"/>";
                    selDiv.innerHTML += html;
                }
                reader.readAsDataURL(f);

            });


        }
    </script>


    <script type="text/javascript">
        $(function () {
            $('[id*=btnadd]').bind("click", function () {
                $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
            });
        });
        function drp(field, rules, i, options) {
            if ($('[id$=drpdivision] option:selected').val() == "0") {
                return "This Feild Required."
            }
        }
        function drp1(field, rules, i, options) {
            if ($('[id$=drpclient] option:selected').val() == "0") {
                return "This Feild Required."
            }
        }
        function drp2(field, rules, i, options) {
            if ($('[id$=drpproject] option:selected').val() == "0") {
                return "This Feild Required."
            }
        }

    </script>


    <script language="javascript" type="text/javascript">

        function loadPopupBox() {

            document.getElementById("popup_box").style.display = "Block";

        }

        function unloadPopupBox() {

            document.getElementById("popup_box").style.display = "None";

        }



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">


    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <div class="span12">
        <div class="widget container">
            <div class="widget-header">
                <i class="icon-pencil"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>  Ticket Entry</b>
            </div>
            <div class="widget-content">


                <table class="table">


                    <tr>

                        <td width="20%" align="left" runat="server" visible="false">Select Divison</td>
                        <td align="left" runat="server" visible="false">
                            <asp:DropDownList ID="drpdivision" runat="server" ToolTip="Division" AutoPostBack="True" Width="200px">
                            </asp:DropDownList></td>

                        <td width="20%" align="left">Select Client</td>
                        <td align="left">
                            <asp:DropDownList ID="drpclient" runat="server" ToolTip="Client" Width="200px">
                            </asp:DropDownList></td>


                    </tr>

                    <tr>
                        <td width="20%" align="left">Project Name</td>
                        <td width="30%" align="left">
                            <asp:DropDownList ID="drpproject" runat="server" ToolTip="Project">
                            </asp:DropDownList>

                            <a id="A1" onclick="if(document.getElementById('<%=txtproject.ClientID %>').style.display=='block'){document.getElementById('<%=txtproject.ClientID %>').style.display='none'}else{document.getElementById('<%=txtproject.ClientID %>').style.display='block'}" style="color: Blue">Add Project</a>

                            <asp:TextBox ID="txtproject" runat="server" ToolTip="Project" Style="display: none" CssClass="h "></asp:TextBox></td>


                        <td width="20%" align="left">Date</td>
                        <td style="width: 30%" align="left">
                            <asp:TextBox ID="TextBox1" onpaste="return false;" placeholder="dd-mm-yyyy" onkeydown="return checkKey()" oncut="return false;" OnTextChanged="TextBox1_TextChanged" CssClass="validate[required]" runat="server" AutoPostBack="True"></asp:TextBox>
                            <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" CssClass="red" Enabled="True" TargetControlID="TextBox1" PopupButtonID="Image1" Format="dd-MMM-yyyy" DaysModeTitleFormat="dd-MMM-yyyy">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" align="left" style="height: 40px">Point
                Description*
                        </td>
                        <td width="40%" align="left" style="height: 40px">
                            <asp:TextBox ID="txtdescription" runat="server" ToolTip="Point Description" Style="text-transform: capitalize" CssClass="validate[required]" TextMode="MultiLine"></asp:TextBox></td>

                        <td width="10%" align="left" style="height: 40px">Remark</td>
                        <td align="left" style="width: 30%; height: 40px;">
                            <asp:TextBox ID="txtremark" runat="server" MaxLength="200" TextMode="MultiLine" Style="text-transform: capitalize;" ToolTip="Remark" Width="200px"></asp:TextBox></td>

                    </tr>



                    <tr>
                        <td colspan="4" align="center" style="background-color: <%=db.headerBG%>; height: 26px;">
                            <center> <asp:Button ID="btnadd" runat="server" Text="Add(+)" CssClass ="btn btn-success "/></center>



                        </td>
                    </tr>
                </table>


                <table width="100%" border="0" class="table-bordered" cellpadding="0" cellspacing="0" id="TABLE1">

                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="P2" Width="100%" runat="server">
                                <asp:Panel ID="P4" Width="100%" runat="server">
                                    <table width="100%" border="0">
                                        <tr align="left">
                                            <td class="auto-style2">
                                                <span style="font-size: 14pt">To</span>
                                            </td>
                                        </tr>
                                        <tr align="left">
                                            <td style="height: 11px">

                                                <span style="font-size: 14pt">Technical Support Team (IT)</span></td>
                                        </tr>
                                        <tr align="left">
                                            <td style="height: 21px">

                                                <span style="font-size: 14pt">Kamtech Associates Pvt. Ltd.</span>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="height: 44px">
                                                <span style="font-size: 14pt">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                          &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                          &nbsp; &nbsp;&nbsp;
                          <br />
                                                    <span style="font-size: 18pt"><strong>Ticket No.</strong></span></span><span style="font-size: 16pt"><strong><span style="font-size: 24pt">
                              :</span></strong><span> </span></span>
                                                <asp:Label ID="l5" runat="server" Font-Bold="True" Font-Size="18pt"></asp:Label><span><span
                                                    style="font-size: 18pt">
                      &nbsp; <span><span><strong><span>Date :</span> </strong>
                      </span></span></span></span>
                                                <asp:Label ID="l1" runat="server" Font-Size="18pt" Font-Bold="True"></asp:Label><br />
                                            </td>


                                        </tr>
                                    </table>

                                </asp:Panel>
                                <asp:Panel ID="P1" Width="100%" runat="server" Visible="false">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" id="TABLE2">
                                        <tr>
                                            <td align="left" style="height: 21px">
                                                <span style="font-size: 14pt">Client Name : </span>
                                                <asp:Label ID="l2" runat="server" Font-Size="14pt" Font-Bold="True"></asp:Label>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 21px" align="left">
                                                <span style="font-size: 14pt">Project Name : </span>
                                                <asp:Label ID="l3" runat="server" Font-Size="14pt" Font-Bold="True"></asp:Label>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr style="font-size: 12pt">
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr align="left" style="font-size: 12pt">
                                            <td>
                                                <span>We need following ammendments in our system : </span>
                                            </td>
                                        </tr>

                                    </table>
                                </asp:Panel>
            </div>
        </div>
        <asp:GridView ID="Gv1" Width="100%" runat="server" PagerStyle-CssClass="paging-link" PagerStyle-HorizontalAlign="Right" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" DataKeyNames="id" Font-Size="Smaller" HorizontalAlign="Left" RowHeaderColumn="True">

            <Columns>

                <asp:TemplateField HeaderText="S.No.">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="gridchk " />
                </asp:TemplateField>

                <%--<asp:TemplateField HeaderText="Project Name">
            <ItemTemplate>
            <%#Eval("f1")%>
            </ItemTemplate>
            
                <ItemStyle Width="15%" />
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <%#Eval("f2")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtdes" runat="server" TextMode="MultiLine" Text='<%#eval("f2") %>' Width="250px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="35%" />
                </asp:TemplateField>



                <asp:TemplateField HeaderText="Remarks">
                    <ItemTemplate>
                        <%#Eval("f3")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Text='<%#eval("f3") %>' Width="250px"></asp:TextBox>

                    </EditItemTemplate>
                    <ItemStyle Width="30%" />
                </asp:TemplateField>



                <asp:TemplateField HeaderText="Action">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="edit" CssClass="btn btn-warning " ToolTip="Edit">
                                          <i class="btn-icon-only icon-cog"></i>Edit
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CssClass="btn btn-danger " ToolTip="Delete">
               <i class="btn-icon-only icon-trash"></i>Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" ToolTip="Cancel">
             <i class="btn-icon-only icon-remove"></i>Cancel</asp:LinkButton>

                        <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" ToolTip="Update">
             <i class="btn-icon-only icon-remove"></i>Update</asp:LinkButton>
                    </EditItemTemplate>
                    <ItemStyle Width="15%" HorizontalAlign="Center" />
                </asp:TemplateField>

            </Columns>

            <PagerStyle />
            <HeaderStyle HorizontalAlign="Left" CssClass="DataGridFixedHeader" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" Font-Size="Small" VerticalAlign="Top" />
        </asp:GridView>



        <asp:Panel ID="P3" Width="100%" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0" id="tbl2">
                <%-- <tr><td></td></tr>--%>
                <tr>
                    <td align="left">
                        <br />
                        We request you please do the needful as soon as possible.
                      <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <%-- <tr><td><br /></td></tr>
                      <tr><td><br /></td></tr>
                      <tr><td></td></tr>
                      <tr><td></td></tr>
                       <tr><td></td></tr>--%>
                <tr>
                    <td align="left">Thanking You</td>
                </tr>
                <tr>
                    <td align="left" style="height: 21px">
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label4" runat="server" Text="Label" Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left" style="height: 21px">
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></td>
                </tr>

                <tr>
                    <td align="left">
                        <asp:Label ID="Label3" runat="server" Text="Label" Font-Bold="False"></asp:Label></td>
                </tr>
                <%-- <tr><td></td></tr>
                     <tr><td></td></tr>--%>
                <tr>
                    <td><font face="Franklin Gothic Heavy" color="red" size="3">
                         <br />Kamtech-Saksham MIS generated report</font></td>
                </tr>
            </table>

        </asp:Panel>
        </asp:Panel>
                 
                  
             
            
             
              <tr  >
                  <td width="25%" align="left" >&nbsp;</td>

                  <td width="75%" align="left"  >
                      <input type="file" id="files" style="display:none;" name="fileUploader" multiple accept="image/*"><br />

                      <div id="selectedFiles"></div>
                      <%-- <tr><td></td></tr>--%>
                  </td>
              </tr>



        <tr>
            <td colspan="1">Send e-mail To Client
            </td>
            <td rowspan="1">
                <asp:RadioButton ID="rd1" runat="server" Text="Yes" GroupName="aa" />
                <asp:RadioButton ID="rd2" runat="server" Text="No" GroupName="aa" Checked="True" /></td>

        </tr>

        <tr >
            <td colspan="2" align="center">


                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" />

                <asp:Label ID="lblerror" runat="server" ForeColor="Red"></asp:Label>

            </td>
        </tr>

        <tr>
            <td colspan="2" style="color: red;" align="left">*Note :- 
                 <br />
                <ol>
                    <li>Fill the project name in project name box in which you want to add points.
                    </li>
                    <li>Fill the required points in the point description box one by one.</li>
                    <li>Do remark if necessary.</li>
                    <li>Add each points.</li>
                    <li>If more points add again.</li>
                    <li>These edit points will display on review ticket box if you want delete or edit
                 it can be done by clicking on the respective symbol.</li>
                    <li>If you have any attachement regarding points then please browse selected file.</li>
                    <li>More attachement can be attached by use of rar file format.</li>
                    <li>At last if you want to submit a ticket then click on submit button.</li>
                    <li>Allowed file formats are Jpeg,Gif,Tif ,Zip and Rar.</li>
                </ol>
            </td>
        </tr>

        </table>
          
          
          
          
          
       </center>
       
    
       <div id="popup_box" style="display: none;">
           <!-- OUR PopupBox DIV-->

           <center><h2>Receipt of Technical Ticket</h2></center>
           <hr style="margin-top: -15px" />
           We are confirming the receipt of technical assistance ticket.<br />
           Required points are under consideration.
           <br />
           You can also check ticket status at Ticket Status link available in Ticket Management Module.
    <br />
           <br />
           <br />
           Thank You
           <br />
           <br />
           Technical Support Team (IT)<br />
           Kamtech Associates Pvt Ltd<br />
           (India~Kenya~Tanjania~U.K.)<br />
           G5,Gajraj Apartment, Sarojini Marg, C-scheme,Jaipur-302001.<br />
           Rajasthan, India.<br />
           <br />
           Ph: +91-141-2377559, 2373226, 2371308, Fax +91-141-4041885<br />
           Mobile - +91-9828042461<br />
           Web - www.realcrm.in, www.propertyjunction.in, www.kamtech.in<br />
           E-mail - office@kamtech.in, ajain@kamtech.in, mail@kamtechassociates.com<br />


           <a id="popupBoxClose" onclick="unloadPopupBox()">X</a>
       </div>
</asp:Content>

