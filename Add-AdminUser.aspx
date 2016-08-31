<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="Add-AdminUser.aspx.vb" Inherits="Admin_Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <link rel="icon" href="images/hslogo.jpg" type="image/x-icon"/>
    <link href="jvalidation/ValidationEngine.css" rel="stylesheet" type="text/css"  />
    <link href="css/formStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="jvalidation/jquery.min.js"></script>
<script type="text/javascript" src="jvalidation/jquery.validationEngine-en.js" charset="utf-8"></script>
<script type="text/javascript" src="jvalidation/jquery.validationEngine.js" charset="utf-8"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">
    
    <script type="text/javascript">
       
       $(function () {
           $('[id*=savemenu]').bind("click", function () {
               $("#adminform").validationEngine('attach', { promptPosition: "centerRight" });
           });
          
       });

       function DateFormat(field, rules, i, options) {
           var regex = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
           if (!regex.test(field.val())) {
               return "Please enter date in dd/MM/yyyy format."
           }
       }

       function drpsiteoffice(field, rules, i, options) {
           if ($('[id$=drpsiteoffice] option:selected').val() == "0") {
               return "This Feild Required."
           }
       }


       function drpemp(field, rules, i, options) {
           if ($('[id$=drpemp] option:selected').val() == "0") {
               return "This Feild Required."
           }
       }

            </script>
    <div class="span12">
    <asp:Literal ID="lblmsgs" runat="server" Text=""></asp:Literal>
        <div class="widget ">
            <div class="widget-header">
                <i class="icon-pencil"></i>
                <h3>
                    <asp:Literal ID="pagename" runat="server"></asp:Literal></h3>
            </div>
            
            <!-- /widget-header -->
            <div class="widget-content">
                <div id="formcontrols" class="form-horizontal">
                    <fieldset>
                        <div class="control-group">
                            <label for="firstname" class="control-label">  
                                User Name</label>
                            <div class="controls">
                                <asp:TextBox ID="txtfname" MaxLength="25" runat="server" class="span4 validate[required]"></asp:TextBox>
                                <span class="Required">*</span>
                                <%--<asp:RequiredFieldValidator CssClass="Required" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfname" ErrorMessage="Please enter Name" Display="Dynamic" SetFocusOnError="true" ValidationGroup="btnsave"></asp:RequiredFieldValidator>--%>
                            </div>
                            <!-- /controls -->
                        </div>

                        <div class="control-group">
                            <label for="firstname" class="control-label">
                                Password</label>
                            <div class="controls">
                                <asp:TextBox ID="txtpass" MaxLength="25" TextMode="Password" runat="server" class="span4 validate[required]"></asp:TextBox><span
                                    class="Required">*</span><%--<asp:RequiredFieldValidator CssClass="Required" ID="rfvpass"
                                        runat="server" ControlToValidate="txtpass" ErrorMessage="Please enter password"
                                        Display="Dynamic" SetFocusOnError="true" ValidationGroup="btnsave"></asp:RequiredFieldValidator>--%>
                            </div>
                            <!-- /controls -->
                        </div>

                       <%-- <div class="control-group">--%>
                            <%--<label for="firstname" class="control-label" >&nbsp;Company Name</label>--%>
                            <div class="">
                                
                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  Companyname:&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:DropDownList runat="server" ID="drpsiteoffice" Visible="true"   ToolTip="Siteoffice" AutoPostBack="true" CssClass="" Height="28px" Width="218px"></asp:DropDownList>
                                <asp:Label ID="lblcompname" runat="server" Visible="false"  ></asp:Label>
                            </div>
                           <br />


                         <div class="control-group">
                            <label for="firstname" class="control-label">
                                Employee Name</label>
                            <div class="controls">
                                <asp:DropDownList runat="server" ID="drpemp" ToolTip="Employee" ></asp:DropDownList>
                               <%-- <span
                                    class="Required">*</span><asp:RequiredFieldValidator CssClass="Required" ID="RequiredFieldValidator3"
                                        runat="server" ControlToValidate="drpemp" ErrorMessage="Please enter last name"
                                        Display="Dynamic" SetFocusOnError="true" ValidationGroup="btnsave"></asp:RequiredFieldValidator>--%>
                            </div>
                            <!-- /controls -->
                        </div>

   <%--                     <div class="control-group">
                            <label for="firstname" class="control-label">
                                Email Address</label>
                            <div class="controls">
                                <asp:TextBox ID="txtemail" MaxLength="100" runat="server" class="span4"></asp:TextBox><span
                                    class="Required">*</span>
                                <asp:RequiredFieldValidator ID="rfvemailid" runat="server" ControlToValidate="txtemail"
                                    CssClass="Required" ErrorMessage="Please enter email address" Display="Dynamic"
                                    EnableViewState="false" SetFocusOnError="true" ValidationGroup="btnsave"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revemail" runat="server" ControlToValidate="txtemail"
                                    CssClass="Required" Display="Dynamic" ErrorMessage="The email address you entered appears to be incorrect. (Ex: test@example.com)"
                                    SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ValidationGroup="btnsave" EnableViewState="False"></asp:RegularExpressionValidator>
                            </div>
                            <!-- /controls -->
                        </div>--%>
                        
                       <%-- <div class="control-group">
                            <label for="firstname" class="control-label">
                                Confirm Password</label>
                            <div class="controls">
                                <asp:TextBox ID="txtconfirmpass" MaxLength="25" TextMode="Password" runat="server"
                                    class="span4"></asp:TextBox><span class="Required">*</span><asp:RequiredFieldValidator
                                        CssClass="Required" ID="rfvconfirmpass" runat="server" ControlToValidate="txtconfirmpass"
                                        ErrorMessage="Please enter confirm password" Display="Dynamic" SetFocusOnError="true"
                                        ValidationGroup="btnsave"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ControlToCompare="txtpass" ControlToValidate="txtconfirmpass"
                                    CssClass="Required" ID="rfvconfirmpass1" runat="server" ErrorMessage="passwords did not match each other. Please try again."
                                    Display="Dynamic" ValidationGroup="btnsave"></asp:CompareValidator>
                            </div>
                            <!-- /controls -->
                        </div>--%>
                        <div class="control-group">
                            <label for="firstname" class="control-label"></label>
                                Is Active    <div class="controls">
                                <label class="checkbox inline">
                                    <asp:CheckBox ID="chkisactive"   Checked="true"  runat="server" />
                                    Lock/Unlock
                                </label>
                            </div>
                            <!-- /controls -->
                        </div>
                        <div class="form-actions" style="display:none;">
                            <asp:Button ID="btnSave" runat="server" ValidationGroup="x" class="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                            <a href="adminuser.aspx" class="btn">Back</a>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
        <!-- /widget-content -->
        <div class="widget " runat="server" id="trmenu">
            <div class="widget-header">
                <i class="icon-pencil"></i>
                <h3>
                    Admin Rights</h3>
            </div>
            <!-- /widget-header -->
            <div class="widget-content">
                <asp:DataList ID="dlrights" runat="server" GridLines="None" ShowFooter="false" DataKeyField="idmenu"
                    CssClass="table table-striped table-bordered" OnItemDataBound="dlrights_ItemDataBound">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkheader" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("title")%><asp:CheckBoxList ID="chksubmenu" CssClass="table  AdminRightsMenu" runat="server"
                            RepeatColumns="4" RepeatDirection="Horizontal" >
                        </asp:CheckBoxList>
                    </ItemTemplate>
                </asp:DataList>
                <div class="form-actions">
                 <center> <asp:Button ID="savemenu" runat="server" ValidationGroup="x" class="btn btn-primary"
                        Text="Save"  /></center>  
                </div>
            </div>
        </div>
        <!-- /widget-content -->
    </div>
</asp:Content>

