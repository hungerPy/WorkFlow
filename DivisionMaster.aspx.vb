Imports System.Data.SqlClient

Imports System.Data

Partial Class Admin_DivisionMaster
    Inherits System.Web.UI.Page
    Public db As New general
    Public abcd As String
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Text = ""

        If Not IsPostBack Then
            Dim companyid As String = Session("companyid")
            ShowDivision()
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If btnSubmit.Text = "Add" Then
            If db.isExists("Division", "NAME", txtDName.Text, False, "") = True Then
                lblError.Text = "* Duplicate Department Name."

                Exit Sub
            End If
            'db.qry = "delete from Department where ID=" & txtId.Text
            'db.executeQuery()
            Dim isactive As Integer = If(rdbstatus.SelectedValue = "0", 1, 0)

            db.qry = "insert into Division(Name,ParentId,IsActive) values('" & txtDName.Text & "',0," & isactive & ")"
            db.executeQuery()
            If txtId.Text <> "" Then
                Dim e1 As New GridViewCancelEditEventArgs(Convert.ToInt32(txtId.Text))
                GVDivision_RowCancelingEdit(sender, e1)
            End If
        Else
            Dim isactive As Integer = If(rdbstatus.SelectedValue = "0", 1, 0)
            db.qry = "update Division set Name='" & txtDName.Text & "',IsActive=" & isactive & " where ID=" & Convert.ToInt32(txtId.Text) & " "
            db.executeQuery()
            If txtId.Text <> "" Then
                Dim e1 As New GridViewCancelEditEventArgs(Convert.ToInt32(txtId.Text))
                GVDivision_RowCancelingEdit(sender, e1)
            End If
        End If
        ClearFields()
        txtId.Text = ""
        ShowDivision()
        btnSubmit.Text = "Add"
    End Sub

    Private Sub ShowDivision()
        Dim companyid As String = Session("companyid")
        strqry1 = "select * from Division where parentID=0 order by Name"
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        If dr.hasrows Then
            GVDivision.DataSource = dr
            GVDivision.DataBind()
        Else
            GVDivision.DataSource = Nothing
            GVDivision.DataBind()
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Protected Sub GVDivision_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GVDivision.RowCancelingEdit
        GVDivision.EditIndex = -1
        ShowDivision()
        ClearFields()
        btnSubmit.Text = "Submit"
    End Sub

    Protected Sub GVDivision_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVDivision.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim flg As String = DataBinder.Eval(e.Row.DataItem, "IsActive").ToString()
            If flg = "True" Then
                e.Row.Cells(2).Text = "Active"
            Else
                e.Row.Cells(2).Text = "Inactive"
            End If

                Dim lnk As LinkButton
                lnk = CType(e.Row.FindControl("lnkDelete"), LinkButton)
                Dim Department As String = DataBinder.Eval(e.Row.DataItem, "ID").ToString()
            If db.isExists("Employee", "DepartmentID", Department, False) = True Then
                lnk.Visible = False
                'End If
                'If GVDivision.EditIndex = -1 
            Else
                Dim lnk11 As LinkButton
                lnk11 = CType(e.Row.FindControl("lnkDelete"), LinkButton)
                lnk11.Attributes.Add("onclick", "return confirm('Do you want to delete?')")
            End If
            End If
    End Sub


    Private Sub ClearFields()
        Dim companyid As String = Session("companyid")
        txtDName.Text = ""
    End Sub

    Protected Sub GVDivision_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GVDivision.RowDeleting
        Dim s As String
        s = GVDivision.DataKeys(e.RowIndex).Value
        db.qry = "delete from division where ID=" & s
        db.executeQuery()
        ShowDivision()
    End Sub

    Protected Sub GVDivision_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GVDivision.RowEditing
        GVDivision.EditIndex = e.NewEditIndex
        Dim strId As String = GVDivision.DataKeys(e.NewEditIndex).Value.ToString()
        strqry1 = "select * from division where ID=" & strId
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        While dr.read()
            txtId.Text = dr("ID").ToString()
            txtDName.Text = dr("Name").ToString()
            Dim flg As Boolean = dr("IsActive")
            If flg = True Then
                rdbstatus.SelectedValue = 0
            Else
                rdbstatus.SelectedValue = 1
            End If
        End While
        btnSubmit.Text = "Update"
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
        ShowDivision()
    End Sub

End Class
