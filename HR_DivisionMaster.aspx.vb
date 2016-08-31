Imports System.Data

Partial Class Admin_HR_DivisionMaster
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String
    Dim cnt As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("uId") = 1
        'If db.checkUser(LCase(Application("users")), LCase(Session("loginName")), LCase(Session.SessionID)) <> "OK" Then
        '    ClientScript.RegisterClientScriptBlock(Me.GetType(), "ddd", "<script>window.parent.location='../timeout.htm';</script>")
        'End If
        cnt = db.getFieldValue("divisions", "1", "1", "count(*)", True, True)
        'btnSubmit.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")
        'db.setLayout(Session("uId"))
        If Not IsPostBack Then
            lblError.Text = ""
            pagename.Text = "Division Master"
            Page.Title = "Division Master - " + CommonFunctions.GetKeyValue(2)
            ShowData()
        End If
    End Sub
    Private Sub ShowData()
        Dim qry = "select * from Divisions order by Priority,DivisionHead"
        dt1 = db.fillReader1(qry)
        dr = dt1.CreateDataReader()
        GVDivisions.DataSource = dr
        GVDivisions.DataBind()
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Protected Sub GVDivisions_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GVDivisions.RowCancelingEdit
        GVDivisions.EditIndex = -1
        txtDiv.Enabled = True
        btnSubmit.Enabled = True
        ShowData()
    End Sub

    Protected Sub GVDivisions_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVDivisions.RowDataBound
        Dim i As Integer
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            Dim drp As DropDownList = CType(e.Row.FindControl("drp"), DropDownList)
            If Not drp Is Nothing Then
                For i = 1 To cnt
                    drp.Items.Add(i)
                Next
                drp.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Priority").ToString()
            End If
        End If
    End Sub

    Protected Sub GVDivisions_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GVDivisions.RowEditing
        GVDivisions.EditIndex = e.NewEditIndex
        txtDiv.Enabled = False
        btnSubmit.Enabled = False
        ShowData()
    End Sub

    Protected Sub GVDivisions_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GVDivisions.RowUpdating
        Dim strId As String = GVDivisions.DataKeys(e.RowIndex).Value.ToString()
        Dim txtDiv As TextBox = CType(GVDivisions.Rows(e.RowIndex).FindControl("txtDivision"), TextBox)
        Dim drp As DropDownList = CType(GVDivisions.Rows(e.RowIndex).FindControl("drp"), DropDownList)
        Dim qry = "update Divisions set DivisionHead='" & txtDiv.Text & "',Priority=" & drp.SelectedValue & " where DivisionId=" & strId
        db.executeQuery()
        txtDiv.Enabled = True
        btnSubmit.Enabled = True
        GVDivisions.EditIndex = -1
        ShowData()
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim strDivId As String = db.getMaxId("Divisions", "DivisionId")
        If db.isExists("Divisions", "DivisionHead", txtDiv.Text, False, " and DivisionId<> " & strDivId) = False Then
            Dim qry = "insert into Divisions values(" & strDivId & ",'" & txtDiv.Text & "'," & Convert.ToInt32(strDivId) & ")"
            db.executeQuery()
            txtDiv.Text = ""
        Else
            lblError.Text = "Already Exists!!!"
        End If
        ShowData()
    End Sub

End Class
