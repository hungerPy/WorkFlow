Imports System.Data

Partial Class Designation
    Inherits System.Web.UI.Page
    Public db As New general
    Public abcd As String
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            db.fillCombo(DrpDepartment, "Department", "DepartmentName", "ID", " order by DepartmentName")
            ShowDesignation()
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        if

            If Not db.isExists("Designation", "DesignationName", txtDesgination.Text, False, " and ID <> " & txtId.Text) Then

                db.qry = "insert into Designation(DepartmentID,DesignationName,Description,IsActive) values(" & DrpDepartment.SelectedItem.Value & ",'" & txtDesgination.Text & "','" & txtDescription.Text & "')"
                db.executeQuery()
                If txtId.Text <> "" Then
                    Dim e1 As New GridViewCancelEditEventArgs(Convert.ToInt32(txtId.Text))
                    GVDesignations_RowCancelingEdit(sender, e1)
                End If
            Else
                Response.Write("<Script>alert('Duplicate Designation name.')</script>")
                txtDescription.Text = ""
            End If

            ShowDesignation()
            txtId.Text = ""
            txtDescription.Text = ""
            txtDesgination.Text = ""
            btnSubmit.Text = "Submit"
    End Sub

    Private Sub ShowDesignation()
        strqry1 = "select * from Designation order by DesignationName"
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        If dr.HasRows Then
            GVDesignations.DataSource = dr
            GVDesignations.DataBind()
        Else
            GVDesignations.DataSource = Nothing
            GVDesignations.DataBind()
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Protected Sub GVDesignations_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GVDesignations.RowCancelingEdit
        GVDesignations.EditIndex = -1
        ShowDesignation()
        txtDescription.Text = ""
        txtDesgination.Text = ""
        btnSubmit.Text = "Submit"
    End Sub

    Protected Sub GVDesignations_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVDesignations.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim strId As String = GVDesignations.DataKeys(e.Row.RowIndex).Value.ToString()
            Dim lnk As LinkButton
            lnk = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            Dim designation As String = DataBinder.Eval(e.Row.DataItem, "ID").ToString()
            If db.isExists("Employee", "EmployeeDesignation", designation, False) = True Then
                lnk.Visible = False
            End If
            If GVDesignations.EditIndex = -1 Then
                Dim lnk11 As LinkButton
                lnk11 = CType(e.Row.FindControl("lnkDelete"), LinkButton)
                lnk11.Attributes.Add("onclick", "return confirm('Do you want to delete?')")
            End If
        End If
    End Sub

    Protected Sub GVDesignations_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GVDesignations.RowDeleting
        Dim s As String
        s = GVDesignations.DataKeys(e.RowIndex).Value
        db.qry = "delete from designation where designationid=" & s
        db.executeQuery()
        ShowDesignation()
    End Sub

    Protected Sub GVDesignations_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GVDesignations.RowEditing
        GVDesignations.EditIndex = e.NewEditIndex
        Dim strId As String = GVDesignations.DataKeys(e.NewEditIndex).Value.ToString()
        Dim qry = "select * from Designation where ID=" & strId
        dt1 = db.fillReader1(qry)
        dr = dt1.CreateDataReader()
        While dr.Read()
            txtId.Text = dr("ID").ToString()
            txtDesgination.Text = dr("DesignationName").ToString()
            txtDescription.Text = dr("description").ToString()
        End While
        btnSubmit.Text = "Update"
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
        ShowDesignation()
    End Sub
End Class
