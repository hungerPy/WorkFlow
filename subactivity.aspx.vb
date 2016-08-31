Imports System.Data
Partial Class Admin_subactivity
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            db.fillCombo(drp_depart, "Division", "Name", "ID", "")
            ShowData()
        End If
    End Sub
    Private Sub ShowData()
        strqry1 = "select * from subactivity where dcode='" & drpCat.SelectedValue & "' and ACode='" & drpact.SelectedValue & "'"
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        dr.Close()
        dt1.Clear()
        dt1.Dispose()

    End Sub
    Private Sub clear()
        drpCat.SelectedValue = 0
        drp_depart.SelectedValue = 0       
    End Sub


    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim IsActive As Integer = IIf(drpFlg.SelectedValue = 0, 1, 0)


        If btnSubmit.Text = "Update" Then

            db.qry = "update division set Name='" & txt_activity.Text & "',ParentId=" & drp_depart.SelectedValue & ",IsActive=" & IsActive & " where ID=" & txtId.Text

        Else
            db.qry = "insert into division(name,parentid,isactive) values('" & txt_activity.Text & "'," & drp_depart.SelectedValue & "," & IsActive & ")"
        End If
        db.executeQuery()

        clear()
        btnSubmit.Text = "Create Sub Activity"

    End Sub

    Protected Sub drp_depart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_depart.SelectedIndexChanged
        db.fillCombo(drpCat, "Division", "Name", "Id", "where ParentId='" & drp_depart.SelectedValue & "'")
    End Sub
End Class
