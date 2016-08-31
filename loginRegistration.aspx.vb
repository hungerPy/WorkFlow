Imports System.Data

Partial Class Admin_loginRegistration
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1, dr2 As DataTableReader
    Dim strqry1, strqry2 As String
    Protected Sub Btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnsubmit.Click
        Dim tid = txtid.Text
        Dim str As String = ""
        If tid = "" Then tid = db.getMaxId("login", "lid")

        If Not db.isExists("login", "username", txtusername.Text, False, " and lid <> " & tid) Then
            db.qry = "delete from login where lid=" & tid
            db.executeQuery()

            db.qry = "insert into login values(" & tid & ",'" & txtusername.Text & "','" & txtpassword.Text & "','" & drpcompany.SelectedValue & "'," & drpemployee.SelectedValue & "," & rdbstatus.SelectedValue & ",'" & Format(Date.Now, "dd-MMM-yyyy") & "')"
            db.executeQuery()
            strqry1 = "select * from rights"
            dt1 = db.fillReader1(strqry1)
            dr = dt1.CreateDataReader()
            Dim i As Integer = 0
            For i = 1 To dr.FieldCount - 1
                If str <> "" Then str = str & ","
                str = str & "'False'"
            Next
            dr.Close()
            If Not db.isExists("rights", "uid", drpemployee.SelectedValue, False) Then
                db.qry = "insert into rights values (" & drpemployee.SelectedValue & "," & str & ")"
                db.executeQuery()
                db.qry = "insert into objectstyles(headercolor,headerBG,headerText,headerSize,headerWeight,headerStyle,headerDecoration,headerAlignment,formColor,formBG,formText,formSize,formWeight,formStyle,formDecoration,formalignment)" & _
                                            " select headercolor,headerBG,headerText,headerSize,headerWeight,headerStyle,headerDecoration,headerAlignment,formColor,formBG,formText,formSize,formWeight,formStyle,formDecoration,formalignment from objectstyles where uid=0"
                db.executeQuery()
                db.qry = "update objectstyles set objectid=" & db.getMaxId("objectstyles", "objectid") & " , uid=" & drpemployee.SelectedValue & " where uid is null"
                db.executeQuery()
            End If
            If txtid.Text <> "" Then
                Dim e1 As New GridViewCancelEditEventArgs(Convert.ToInt32(txtid.Text))
                GVUsers_RowCancelingEdit(sender, e1)
            End If
        Else
            Response.Write("<Script>alert('Duplicate User name.')</script>")
            txtusername.Text = ""
            txtpassword.Text = ""
        End If
        txtid.Text = ""
        txtusername.Text = ""
        txtpassword.Text = ""
        drpemployee.SelectedValue = 0
        rdbstatus.SelectedValue = 0
        showusers()
        Btnsubmit.Text = "Submit"
    End Sub

    Protected Sub GVUsers_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GVUsers.RowDeleting
        Dim s As String
        s = GVUsers.DataKeys(e.RowIndex).Value
        db.qry = "delete from login where lid=" & s
        db.executeQuery()
        showusers()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        'db.setLayout(Session("userId"))
        'End If

        If Not IsPostBack Then
            db.fillCombo(drpcompany, "company", "companyname", "companyabbr", "order by companyid")
            db.fillCombo(drpemployee, "director", "dirname+' ('+designation+')'", "Ids", " where 1=2")
        End If
        'Btnsubmit.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")
    End Sub
    Protected Sub showusers()

        strqry1 = "select * from login where companycode='" & drpcompany.SelectedValue & "' order by lid"
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        If dr.HasRows Then
            'lblcompname.Text = drpcompany.SelectedItem.Text
            GVUsers.DataSource = dr
            GVUsers.DataBind()
        Else
            GVUsers.DataSource = Nothing
            GVUsers.DataBind()
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Protected Sub GVUsers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVUsers.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim s As String = GVUsers.DataKeys(e.Row.RowIndex).Value
            Dim flg As Boolean = DataBinder.Eval(e.Row.DataItem, "flg").ToString()
            Dim Deno = DataBinder.Eval(e.Row.DataItem, "Deno").ToString()
            e.Row.Cells(1).Text = db.getFieldValue("director", "ids", Deno, "dirname")
            If flg = False Then
                e.Row.Cells(4).Text = "Active"
            Else
                e.Row.Cells(4).Text = "Inactive"
            End If
            If GVUsers.EditIndex = -1 Then
                Dim lnk11 As LinkButton
                lnk11 = CType(e.Row.FindControl("lnkDelete"), LinkButton)
                lnk11.Attributes.Add("onclick", "return confirm('Do you want to delete?')")
            End If
        End If
    End Sub

    Protected Sub GVUsers_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GVUsers.RowEditing
        GVUsers.EditIndex = e.NewEditIndex
        Dim strId As String = GVUsers.DataKeys(e.NewEditIndex).Value.ToString()
        Dim qry = "select * from login where lid=" & strId
        dt1 = db.fillReader1(qry)
        dr = dt1.CreateDataReader()

        While dr.Read()
            txtid.Text = dr("lid").ToString()
            drpcompany.SelectedValue = dr("companycode").ToString()
            drpemployee.SelectedValue = dr("deno")
            txtusername.Text = dr("username").ToString()
            txtpassword.Text = dr("password").ToString()
            Dim flg As Boolean = dr("flg")
            If flg = False Then
                rdbstatus.SelectedValue = 0
            Else
                rdbstatus.SelectedValue = 1
            End If
        End While
        Btnsubmit.Text = "Update"
        dr.Close()
        dt1.Clear()
        dt1.Dispose()

        showusers()
    End Sub
    Protected Sub GVUsers_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GVUsers.RowCancelingEdit
        GVUsers.EditIndex = -1
        txtusername.Text = ""
        txtpassword.Text = ""
        drpemployee.SelectedValue = 0
        rdbstatus.SelectedValue = 0
        showusers()
        Btnsubmit.Text = "Submit"
    End Sub

    Protected Sub drpcompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpcompany.SelectedIndexChanged
        db.fillCombo(drpemployee, "director", "dirname+' ('+designation+')'", "Ids", " where companyname='" & drpcompany.SelectedItem.Text & "' and flg=0 and (dinno<>'0' or empno<>'0')")
        showusers()
    End Sub
End Class
