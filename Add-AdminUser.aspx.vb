Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Configuration
Imports System.IO

Partial Public Class Admin_Default2
    Inherits System.Web.UI.Page
    Private objuser As New userManager()
    Dim db As New general

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Page.Title = "Add AdminUser - " & CommonFunctions.GetKeyValue(2).ToString()
        pagename.Text = "Add AdminUser"
        If Request.QueryString("mode") = "edit" Then
            Title = "Edit AdminUser - " & CommonFunctions.GetKeyValue(2).ToString()
            pagename.Text = "Edit AdminUser"
        Else
            'db.fillCombo(drpemp, "employee", "empName", "empId", " where status='' and empId  not in (select empid from Users)")

        End If
        If Not Page.IsPostBack Then
            db.fillCombo(drpsiteoffice, "company", "Companyname", "companyid", " order by companyname")
            drpsiteoffice.SelectedValue = Session("companyid")
            lblcompname.Text = Session("companyname")
            db.fillCombo(drpemp, "director", "dirname", "ids", "where companyname='" & Session("companyname") & "'")

            If Request.QueryString("mode") = "edit" Then
                If Request.QueryString("id") <> "" AndAlso Request.QueryString("id") IsNot Nothing Then
                    If RegExp.IsNumericValue(Request.QueryString("id")) Then
                        If Session("AdminType") IsNot Nothing AndAlso Convert.ToString(Session("AdminType")) = "subadmin" Then
                            If Convert.ToInt32(Session("AdminId")) <> Convert.ToInt32(Request.QueryString("id")) Then
                                Response.Redirect("adminuser.aspx")
                            End If
                        End If
                        db.fillCombo(drpemp, "director", "dirname", "ids", " where ids=" & Request.QueryString("empid") & "")
                        drpsiteoffice.SelectedValue = db.getFieldValue("allot", "UId", Request.QueryString("id"), "plantid")
                        'lblcompname.Text = db.getFieldValue("allot", "UId", Request.QueryString("id"), "plantid")

                        drpemp.SelectedValue = db.getFieldValue("users", "uid", Request.QueryString("id"), "empid")
                        savemenu.Text = "Update"
                        Dim dsadmin As New DataSet()
                        objuser.adminid = Convert.ToInt32(Request.QueryString("id"))
                        dsadmin = objuser.SelectSingleItem()
                        If dsadmin.Tables(0).Rows.Count > 0 Then

                            txtfname.Text = dsadmin.Tables(0).Rows(0)("userid").ToString()
                            'txtlname.Text = dsadmin.Tables(0).Rows(0)("lastname").ToString()
                            'txtemail.Text = dsadmin.Tables(0).Rows(0)("emailaddress").ToString()
                            txtpass.Attributes.Add("value", dsadmin.Tables(0).Rows(0)("pwd").ToString())
                            'txtconfirmpass.Attributes.Add("value", dsadmin.Tables(0).Rows(0)("password").ToString())
                            Dim status As String = dsadmin.Tables(0).Rows(0)("status")
                            If status = "Unlock" Then
                                chkisactive.Checked = True
                            Else
                                chkisactive.Checked = False
                            End If

                        End If
                    Else
                        Response.Redirect("home.aspx")
                    End If
                Else
                    Response.Redirect("home.aspx")
                End If
            Else
                db.fillCombo(drpemp, "director", "dirname", "ids", " where  ids  not in (select empid from Users)")

            End If

            If Convert.ToString(Session("AdminType")) = "superadmin" Then
                BindMenu()
                trmenu.Visible = True
                savemenu.Visible = True
            Else
                BindMenu()
                trmenu.Visible = True
                savemenu.Visible = True
            End If
        End If

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If Page.IsValid Then
            'If validateInput() Then
            Dim imgName As String = String.Empty

            objuser.userid = Server.HtmlEncode(txtfname.Text)
            objuser.password = Server.HtmlEncode(txtpass.Text)
            objuser.siteoffice = Server.HtmlEncode(drpsiteoffice.SelectedValue)
            objuser.employee = Server.HtmlEncode(drpemp.SelectedValue)
            objuser.session = "0"
            'objuser.firstname = CommonFunctions.SanitizeInput(txtfname.Text)
            'objuser.lastname = CommonFunctions.SanitizeInput(txtlname.Text)
            'objuser.emailaddress = CommonFunctions.SanitizeInput(txtemail.Text)
            objuser.isactive = Convert.ToByte(chkisactive.Checked)
            If chkisactive.Checked = True Then
                objuser.status = ""
            Else
                objuser.status = "0"
            End If
            objuser.admintype = "subadmin"

            If Request.QueryString("mode") = "edit" Then
                objuser.adminid = Convert.ToInt32(Request.QueryString("id"))
                objuser.uid = Convert.ToInt32(Request.QueryString("id"))
                objuser.aid = db.getFieldValue("allot", "uid", Convert.ToInt32(Request.QueryString("id")), "aid")
                objuser.UpdateItem()
                Response.Redirect("adminuser.aspx?mode=edit&key=" & Request.QueryString("key"))
            Else
                objuser.uid = db.getMaxId("users", "uid")
                objuser.aid = db.getMaxId("allot", "aid")
                objuser.InsertItem()
                'objuser.adminid = CommonFunctions.GetLastIdentity("administrator")
                GenerateMenuFile(objuser.uid)
                Response.Redirect("adminuser.aspx?mde=add&key=" & Request.QueryString("key"))
            End If
        End If
        'End If
    End Sub
    Protected Function validateInput() As [Boolean]
        Dim flag As [Boolean] = True
        'objuser.emailaddress = Server.HtmlEncode(txtemail.Text)

        If Request.QueryString("mode") = "edit" Then
            objuser.adminid = Convert.ToInt32(Request.QueryString("id"))
        Else
            objuser.adminid = 0
        End If

        If flag = True Then
            If objuser.EmailIdExist() Then
                lblmsgs.Text = "<div class='alert alert-danger'><strong>Alert!</strong> Email address already exists.</div>"
                flag = False

            End If
        End If

        Return flag
    End Function
    Protected Sub BindMenu()
        Dim dtparent As New DataSet()
        dtparent = objuser.selectParentMenus()
        dlrights.DataSource = dtparent
        dlrights.DataBind()
        dtparent.Dispose()
    End Sub

    'menu items databound
    Protected Sub dlrights_ItemDataBound(sender As Object, e As DataListItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim chkmenu As CheckBox = DirectCast(e.Item.FindControl("chkheader"), CheckBox)
            Dim chksubmenulist As CheckBoxList = DirectCast(e.Item.FindControl("chksubmenu"), CheckBoxList)
            Dim idmenu As Integer = Convert.ToInt32(dlrights.DataKeys(e.Item.ItemIndex))
            BindSubMenus(chksubmenulist, idmenu)

            If chksubmenulist.Items.Count > 0 Then
                chkmenu.Visible = False
            End If

            'check admin accessible menu 
            objuser.idmenu = idmenu
            If objuser.getAdminmenu() > 0 Then
                chkmenu.Checked = True
            End If
        End If
    End Sub

    'bind sub menu
    Protected Sub BindSubMenus(chksubmenulist As CheckBoxList, idparent As Integer)
        Dim dtmenu As New DataSet()
        chksubmenulist.Items.Clear()
        chksubmenulist.ClearSelection()
        objuser.parentid = idparent
        dtmenu = objuser.selectSubMenus()
        If dtmenu.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To dtmenu.Tables(0).Rows.Count - 1
                chksubmenulist.Items.Add(dtmenu.Tables(0).Rows(i)("title").ToString())
                chksubmenulist.Items(i).Value = dtmenu.Tables(0).Rows(i)("idmenu").ToString()

                If Request.QueryString("mode") = "edit" Then
                    objuser.adminid = Convert.ToInt32(Request.QueryString("id"))
                End If

                objuser.idmenu = Convert.ToInt32(dtmenu.Tables(0).Rows(i)("idmenu"))
                If objuser.getAdminmenu() > 0 Then
                    chksubmenulist.Items(i).Selected = True
                End If
            Next
        End If
        dtmenu.Dispose()
    End Sub
    Protected Sub GenerateMenuFile(adminid As Integer)
        objuser.adminid = adminid
        Dim strmenu As String = objuser.getTopMenu()
        Dim actualfolder As String = Server.MapPath("menu/")
        Dim actDir As New DirectoryInfo(actualfolder)
        'check if Directory exist if not create it
        If Not actDir.Exists Then
            Directory.CreateDirectory(actualfolder)
        End If

        Dim sw As StreamWriter
        Dim strFilepath As String = String.Empty
        strFilepath = Server.MapPath("menu/" & Convert.ToString(adminid) & ".htm")

        sw = File.CreateText(strFilepath)
        sw.WriteLine(strmenu)
        sw.Close()
        sw.Dispose()
    End Sub

    Protected Sub saverights_Click(sender As Object, e As EventArgs) Handles savemenu.Click
        Dim queryflg As String = String.Empty
        If savemenu.Text <> "Update" Then
            If db.isExists("users", "userid", txtfname.Text, False) = True Then
                Response.Write("<script> alert('Username already exists..!!! please try another username') </script>")
                txtfname.Focus()
                Exit Sub
            Else
                '    End If
                'Else

                If Page.IsValid Then
                    'If validateInput() Then
                    Dim imgName As String = String.Empty

                    objuser.isactive = 1
                    objuser.admintype = "subadmin"

                    objuser.userid = Server.HtmlEncode(txtfname.Text)
                    objuser.password = Server.HtmlEncode(txtpass.Text)
                    objuser.siteoffice = Server.HtmlEncode(drpsiteoffice.SelectedValue)
                    objuser.employee = drpemp.SelectedValue

                    objuser.session = drpsiteoffice.SelectedValue


                    objuser.isactive = Convert.ToByte(chkisactive.Checked)
                    If chkisactive.Checked = True Then
                        objuser.status = "Unlock"
                    Else
                        objuser.status = "Lock"
                    End If

                    If Request.QueryString("mode") = "edit" Then
                        objuser.adminid = Convert.ToInt32(Request.QueryString("id"))
                        objuser.uid = Convert.ToInt32(Request.QueryString("id"))
                        objuser.aid = db.getFieldValue("allot", "uid", Convert.ToInt32(Request.QueryString("id")), "aid")
                        objuser.UpdateItem()
                        queryflg = "edit"
                    Else
                        objuser.uid = db.getMaxId("users", "uid")
                        objuser.aid = db.getMaxId("allot", "aid")
                        objuser.InsertItem()

                        objuser.adminid = objuser.uid
                    End If
                End If
                'End If


                Dim count As Integer = 0
                'delete all previous selections and insert new selections
                If Request.QueryString("mode") = "edit" Then
                    objuser.adminid = Convert.ToInt32(Request.QueryString("id"))
                Else
                    'objuser.adminid = CommonFunctions.GetLastIdentity("administrator")
                    objuser.adminid = objuser.uid
                End If

                objuser.DeleteAdminRightsItem()

                For i As Integer = 0 To dlrights.Items.Count - 1
                    Dim itemcount As Integer = 0
                    Dim chkmenu As CheckBox = DirectCast(dlrights.Items(i).FindControl("chkheader"), CheckBox)
                    If chkmenu.Checked AndAlso chkmenu.Visible = True Then
                        objuser.idmenu = Convert.ToInt32(dlrights.DataKeys(dlrights.Items(i).ItemIndex))
                        objuser.InsertAdminRolesItem()
                        count += 1
                    End If

                    Dim chksubmenulist As CheckBoxList = DirectCast(dlrights.Items(i).FindControl("chksubmenu"), CheckBoxList)
                    For Each cbox As ListItem In chksubmenulist.Items
                        If cbox.Selected Then
                            objuser.idmenu = Convert.ToInt32(cbox.Value)
                            objuser.InsertAdminRolesItem()
                            count += 1
                            itemcount += 1
                        End If
                    Next

                    If itemcount > 0 Then
                        objuser.idmenu = Convert.ToInt32(dlrights.DataKeys(dlrights.Items(i).ItemIndex))
                        objuser.InsertAdminRolesItem()
                    End If
                Next

                If count > 0 Then
                    lblmsgs.Text = "The access granted to the administrator successfully."
                    BindMenu()
                    GenerateMenuFile(objuser.adminid)
                Else
                    'lblmsgs.Text = "Please At Least One Check Admin Rights."
                    ClientScript.RegisterStartupScript(Me.GetType(), "OnLoad", "<script language=javascript>alert('Please At Least One Check Admin Rights.')</script>")
                    Exit Sub
                End If
                savemenu.Text = "Save"
                Response.Redirect("adminuser.aspx?mode=" & queryflg & "&q=" & Request.QueryString("key"))
            End If
        End If


        If savemenu.Text = "Update" Then

                If Page.IsValid Then
                    'If validateInput() Then
                    Dim imgName As String = String.Empty

                    objuser.isactive = 1
                    objuser.admintype = "subadmin"

                    objuser.userid = Server.HtmlEncode(txtfname.Text)
                    objuser.password = Server.HtmlEncode(txtpass.Text)
                    objuser.siteoffice = Server.HtmlEncode(drpsiteoffice.SelectedValue)
                    objuser.employee = drpemp.SelectedValue

                    objuser.session = drpsiteoffice.SelectedValue


                    objuser.isactive = Convert.ToByte(chkisactive.Checked)
                    If chkisactive.Checked = True Then
                        objuser.status = "Unlock"
                    Else
                        objuser.status = "Lock"
                    End If

                    If Request.QueryString("mode") = "edit" Then
                        objuser.adminid = Convert.ToInt32(Request.QueryString("id"))
                        objuser.uid = Convert.ToInt32(Request.QueryString("id"))
                        objuser.aid = db.getFieldValue("allot", "uid", Convert.ToInt32(Request.QueryString("id")), "aid")
                        objuser.UpdateItem()
                        queryflg = "edit"
                    Else
                        objuser.uid = db.getMaxId("users", "uid")
                        objuser.aid = db.getMaxId("allot", "aid")
                        objuser.InsertItem()

                        objuser.adminid = objuser.uid
                    End If
                End If
                'End If


                Dim count As Integer = 0
                'delete all previous selections and insert new selections
                If Request.QueryString("mode") = "edit" Then
                    objuser.adminid = Convert.ToInt32(Request.QueryString("id"))
                Else
                    'objuser.adminid = CommonFunctions.GetLastIdentity("administrator")
                    objuser.adminid = objuser.uid
                End If

                objuser.DeleteAdminRightsItem()

                For i As Integer = 0 To dlrights.Items.Count - 1
                    Dim itemcount As Integer = 0
                    Dim chkmenu As CheckBox = DirectCast(dlrights.Items(i).FindControl("chkheader"), CheckBox)
                    If chkmenu.Checked AndAlso chkmenu.Visible = True Then
                        objuser.idmenu = Convert.ToInt32(dlrights.DataKeys(dlrights.Items(i).ItemIndex))
                        objuser.InsertAdminRolesItem()
                        count += 1
                    End If

                    Dim chksubmenulist As CheckBoxList = DirectCast(dlrights.Items(i).FindControl("chksubmenu"), CheckBoxList)
                    For Each cbox As ListItem In chksubmenulist.Items
                        If cbox.Selected Then
                            objuser.idmenu = Convert.ToInt32(cbox.Value)
                            objuser.InsertAdminRolesItem()
                            count += 1
                            itemcount += 1
                        End If
                    Next

                    If itemcount > 0 Then
                        objuser.idmenu = Convert.ToInt32(dlrights.DataKeys(dlrights.Items(i).ItemIndex))
                        objuser.InsertAdminRolesItem()
                    End If
                Next

                If count > 0 Then
                    lblmsgs.Text = "The access granted to the administrator successfully."
                    BindMenu()
                    GenerateMenuFile(objuser.adminid)
                Else
                    'lblmsgs.Text = "Please At Least One Check Admin Rights."
                    ClientScript.RegisterStartupScript(Me.GetType(), "OnLoad", "<script language=javascript>alert('Please At Least One Check Admin Rights.')</script>")
                    Exit Sub
                End If
                savemenu.Text = "Save"
                Response.Redirect("adminuser.aspx?mode=" & queryflg & "&q=" & Request.QueryString("key"))
            End If

    End Sub

    Protected Sub drpsiteoffice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpsiteoffice.SelectedIndexChanged
        'Dim comcode As String = db.getFieldValue("company", "companyid", drpsiteoffice.SelectedValue, "companyAbbr")
        'db.fillCombo(drpemp, "employee", "empname", "empid", "where companycode='" & comcode & "'")
        'db.fillCombo(drpemp, "director", "dirname", "ids", "where companyname='" & drpsiteoffice.SelectedItem.Text.Trim() & "'")
    End Sub
End Class

