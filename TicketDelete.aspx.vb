Imports System.Data

Partial Class Admin_TicketDelete
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        'db.setLayout(Session("userId"))
        'End If

        If Not IsPostBack Then

            'db.fillCombo(drpclient, "ticket", "client", "client", " group by client")
            db.fillCombo(drpclient, "clientmaster", "companyname", "str(companyid)", " where companyid in(select client from ticket group by client) order by companyname", , "Over All")
            db.fillCombo(drpstatus, "statusmaster", "tktstatus", "tktstatus", " where usr='Admin' and tktstatus<>'Completed' group by tktstatus  order by tktstatus", , "Over All")


        End If

    End Sub

    Private Sub show()


        db.qry = "select (select case when CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)=0 then companyname else SUBSTRING(companyname, 0, CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)) end from clientmaster where companyid=(select client from ticket where tid=t.tid)) as client,* from Ticketmaster as t where 1=1 and t.status<>'Completed'"

        If drpclient.SelectedValue <> "Over All" Then
            strqry1 = db.qry & " and tid in(select tid from ticket where client=" & drpclient.SelectedValue & ")"
        End If

        If drpstatus.SelectedValue <> "Over All" Then
            strqry1 = db.qry & " and status='" & drpstatus.SelectedValue & "'"
        End If


        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()


        If dr.HasRows() Then

            Gv1.DataSource = dr
            Gv1.DataBind()

            Gv1.HeaderRow.Cells(0).Text = "S.No."
            Gv1.HeaderRow.Cells(1).Text = "Tkt._No."
            Gv1.HeaderRow.Cells(2).Text = "Client"
            Gv1.HeaderRow.Cells(3).Text = "Project"
            Gv1.HeaderRow.Cells(4).Text = "Description"
            Gv1.HeaderRow.Cells(5).Text = "Client Remark"
            Gv1.HeaderRow.Cells(6).Text = "Status"

        Else

            Gv1.DataSource = Nothing
            Gv1.DataBind()

        End If

        dr.Close()
        dt1.Clear()
        dt1.Dispose()


    End Sub

    Protected Sub Gv1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gv1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Cells(0).Text = e.Row.RowIndex + 1


        End If
    End Sub


    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click

        show()
    End Sub

    Protected Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click

        Dim i As Integer = 0

        For i = 0 To Gv1.Rows.Count - 1

            Dim ck As CheckBox = CType(Gv1.Rows(i).Cells(7).FindControl("CheckBox1"), CheckBox)
            If ck.Checked = True Then
                Dim hdn As HiddenField = CType(Gv1.Rows(i).Cells(7).FindControl("HiddenField1"), HiddenField)
                If CStr(hdn.Value) <> "" Then

                    db.qry = "delete from ticketmaster where tsid=" & CInt(hdn.Value) & ""
                    db.executeQuery()

                    If db.isExists("ticketmaster", "1", "1", True, " and tid=" & CInt(Gv1.DataKeys(i).Value) & "") = False Then

                        db.qry = "delete from ticket  where tid=" & Gv1.DataKeys(i).Value & ""
                        db.executeQuery()

                    End If


                End If
            End If

        Next
        show()


    End Sub
End Class
