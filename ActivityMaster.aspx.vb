Imports System.Data
Partial Class Admin_ActivityMaster
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String
    Public Shared pno As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        '    db.setLayout(Session("userId"), form1)
        'End If

        If Not IsPostBack Then
            db.fillCombo(drpCat, "Division", "Name", "ID", "where ParentID=0 order by ID")
            'lblError.Text = ""
            ShowData()
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If btnSubmit.Text = "Update" Then

                db.qry = "Update DIVISION set Name='" & txtCatName.Text & "',IsActive='" & drpFlg.SelectedValue & "' where ID='" & lblid.Text & "'"

                btnSubmit.Text = "Create Sub Category"

            Else

                Dim strSCatId As String
                If txtId.Text <> "" Then
                    strSCatId = txtId.Text


                End If

                If db.isExists("DIVISION", "Name", txtCatName.Text, False) = False Then
                    db.qry = "insert into DIVISION (Name,ParentId,IsActive) values('" & txtCatName.Text & "'," & drpCat.SelectedValue & "," & drpFlg.SelectedValue & ")"

                Else
                    'lblError.Text = "Already Exists!!!"
                End If
            End If


            db.executeQuery()

            clear()
            ShowData()

        Catch ex As Exception
            'lblError.Text = ex.Message



        End Try


    End Sub

    Public Sub clear()
        txtId.Text = ""
        txtCatName.Text = ""
        'lblError.Text = ""
        lblid.Text = ""
        drpCat.Enabled = True
        drpCat.SelectedValue = 0

        btnSubmit.Text = "Create Sub Category!"
    End Sub
    Private Sub ShowData()
        Try
            strqry1 = "select * from division where ParentID=" & drpCat.SelectedValue
            dt1 = db.fillReader1(strqry1)
            dr = dt1.CreateDataReader()


            Dim tbl As New DataTable
            Dim drw As DataRow
            tbl.Columns.Add("first").ColumnName = "SNo"
            tbl.Columns.Add("second").ColumnName = "Name"
            tbl.Columns.Add("third").ColumnName = "Status"
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
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub drpCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpCat.SelectedIndexChanged
        Try
            strqry1 = "select * from division where ParentID=" & drpCat.SelectedValue
            dt1 = db.fillReader1(strqry1)
            dr = dt1.CreateDataReader()
            GVSubCat.DataSource = dt1
            GVSubCat.DataBind()
            dr.Close()
            dt1.Clear()
            dt1.Dispose()

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
                e.Row.Cells(2).Text = "Inactive"
            End If

            Dim lnk As LinkButton
            lnk = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            Dim Department As String = DataBinder.Eval(e.Row.DataItem, "ID").ToString()
            If db.isExists("Employee", "SubDivisionID", Department, False) = True Then
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
            drpFlg.SelectedIndex = Convert.ToInt16(dr("IsActive"))
            btnSubmit.Text = "Update"
            lblid.Visible = False
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub
End Class
