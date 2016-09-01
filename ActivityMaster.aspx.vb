Imports System.Data
Partial Class Admin_ActivityMaster
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String
    Public Shared pno As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            db.fillCombo(drpCat, "Division", "Name", "ID", "where ParentID=0 order by ID")
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If btnSubmit.Text = "Update" Then
                db.qry = "Update DIVISION set Name='" & txtCatName.Text & "',IsActive='" & IIf(Convert.ToInt16(drpFlg.SelectedValue) = 0, 1, 0) & "' where ID='" & lblid.Text & "'"
                btnSubmit.Text = "Submit"
                db.executeQuery()
                Dim e1 As New GridViewCancelEditEventArgs(Convert.ToInt32(lblid.Text))
                GVSubCat_RowCancelingEdit(sender, e1)
            Else
                Dim strSCatId As String
                If txtId.Text <> "" Then
                    strSCatId = txtId.Text
                End If
                If db.isExists("DIVISION", "Name", txtCatName.Text, False) = False Then
                    db.qry = "insert into DIVISION (Name,ParentId,IsActive) values('" & txtCatName.Text & "'," & drpCat.SelectedValue & "," & IIf(Convert.ToInt16(drpFlg.SelectedValue) = 0, 1, 0) & ")"


                Else
                    'lblError.Text = "Already Exists!!!"
                End If
            End If

            clear()
            ShowData()

        Catch ex As Exception

        End Try


    End Sub

    Public Sub clear()
        txtId.Text = ""
        txtCatName.Text = ""
        'lblError.Text = ""
        lblid.Text = ""
        drpCat.Enabled = True
        drpCat.SelectedValue = 0

        btnSubmit.Text = "Submit"
    End Sub
    Private Sub ShowData()
        Try
            If drpCat.SelectedValue <> "0" Then

                strqry1 = "select * from division where ParentID=" & drpCat.SelectedValue
                dt1 = db.fillReader1(strqry1)
                dr = dt1.CreateDataReader()


                Dim tbl As New DataTable
                Dim drw As DataRow
                tbl.Columns.Add("first").ColumnName = "ID"
                tbl.Columns.Add("second").ColumnName = "Name"
                tbl.Columns.Add("third").ColumnName = "IsActive"
                tbl.Columns.Add("fourth").ColumnName = "Action"

                While dr.Read()
                    drw = tbl.NewRow
                    drw(0) = tbl.Rows.Count + 1
                    drw(1) = dr("Name")
                    If dr("IsActive").ToString() = "True" Then
                        drw(2) = "Active"
                    Else
                        drw(2) = "InActive"
                    End If

                    tbl.Rows.Add(drw)
                End While
                GVSubCat.DataSource = tbl
                GVSubCat.DataBind()
                dr.Close()
            Else
                GVSubCat.DataSource = New List(Of String)
                GVSubCat.DataBind()

            End If
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub drpCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpCat.SelectedIndexChanged
        Try

            If drpCat.SelectedValue <> "0" Then
                strqry1 = "select * from division where ParentID=" & drpCat.SelectedValue
                dt1 = db.fillReader1(strqry1)
                dr = dt1.CreateDataReader()
                GVSubCat.DataSource = dt1
                GVSubCat.DataBind()
                dr.Close()
                dt1.Clear()
                dt1.Dispose()
            Else
                GVSubCat.DataSource = New List(Of String)
                GVSubCat.DataBind()
            End If
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GVSubCat_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVSubCat.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim flg As String = DataBinder.Eval(e.Row.DataItem, "IsActive").ToString()
            If flg = "True" Then
                e.Row.Cells(2).Text = "Active"
            Else
                e.Row.Cells(2).Text = "InActive"
            End If
        End If
    End Sub

    Protected Sub GVSubCat_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GVSubCat.RowDeleting

    End Sub


    Protected Sub GVSubCat_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GVSubCat.RowEditing
        GVSubCat.EditIndex = e.NewEditIndex
        Dim strId As String = GVSubCat.DataKeys(e.NewEditIndex).Value.ToString()
        strqry1 = "select * from division where id=" & strId
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        If dr.Read() Then
            drpCat.SelectedValue = dr("ParentID").ToString()
            txtCatName.Text = dr("Name")
            drpFlg.SelectedIndex = IIf(Convert.ToInt16(dr("IsActive")) = 0, 1, 0)
            btnSubmit.Text = "Update"
            lblid.Text = strId
            lblid.Visible = False
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
        ShowData()
    End Sub

    Protected Sub GVSubCat_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GVSubCat.RowCancelingEdit
        GVSubCat.EditIndex = -1
        ShowData()
        clear()
        btnSubmit.Text = "Submit"
    End Sub
End Class
