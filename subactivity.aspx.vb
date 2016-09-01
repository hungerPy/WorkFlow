Imports System.Data
Partial Class Admin_subactivity
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            db.fillCombo(drp_depart, "Division", "Name", "ID", "where ParentId=0")
        End If
    End Sub
    Private Sub ShowData()
        Try
            If drp_sub.SelectedValue <> "0" And drp_depart.SelectedValue <> "0" Then

                strqry1 = "select * from division where ParentID=" & drp_sub.SelectedValue
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
    Private Sub clear()
        drp_sub.SelectedValue = 0
        drp_depart.SelectedValue = 0
        txt_activity.Text = ""
        txtId.Text = ""
        lblid.Text = ""
    End Sub


    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim IsActive As Integer = IIf(drpFlg.SelectedValue = 0, 1, 0)

            If btnSubmit.Text = "Update" Then

                db.qry = "update division set Name='" & txt_activity.Text & "',ParentId=" & drp_sub.SelectedValue & ",IsActive=" & IsActive & " where ID=" & lblid.Text
                btnSubmit.Text = "Submit"
                db.executeQuery()
                Dim e1 As New GridViewCancelEditEventArgs(Convert.ToInt32(lblid.Text))
                GVSubCat_RowCancelingEdit(sender, e1)
            Else

                db.qry = "insert into division(name,parentid,isactive) values('" & txt_activity.Text & "'," & drp_sub.SelectedValue & "," & IsActive & ")"
                db.executeQuery()
            End If


            clear()
            btnSubmit.Text = "Submit"

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub drp_depart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_depart.SelectedIndexChanged
        If drp_depart.SelectedValue <> "0" Then
            db.fillCombo(drp_sub, "Division", "Name", "Id", "where ParentId='" & drp_depart.SelectedValue & "'")
        Else
            db.fillCombo(drp_sub, "Division", "Name", "Id", "where 1=2")
            GVSubCat.DataSource = New List(Of String)
            GVSubCat.DataBind()
        End If

    End Sub

    Protected Sub drp_sub_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_sub.SelectedIndexChanged
        Try

            If drp_depart.SelectedValue <> "0" And drp_sub.SelectedValue <> "0" Then
                strqry1 = "select * from division where ParentID=" & drp_sub.SelectedValue
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


    Protected Sub GVSubCat_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GVSubCat.RowEditing

        GVSubCat.EditIndex = e.NewEditIndex
        Dim strId As String = GVSubCat.DataKeys(e.NewEditIndex).Value.ToString()
        strqry1 = "select * from division where id=" & strId
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        If dr.Read() Then
            drp_sub.SelectedValue = dr("ParentID").ToString()

            Dim GrandParent As String = db.getFieldValue("division", "ID", Convert.ToInt32(dr("ParentID")), "ParentID")

            drp_depart.SelectedValue = GrandParent

            txt_activity.Text = dr("Name")
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

    Protected Sub GVSubCat_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GVSubCat.RowDataBound
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
End Class



