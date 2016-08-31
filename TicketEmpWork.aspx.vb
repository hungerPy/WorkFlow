Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Configuration
Imports System.IO
Imports System.Data.SqlClient
Partial Class Admin_TicketEmpWork
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String

    Dim ds As New DataSet
    Dim i As Integer
    Dim col1() As Integer = {}
    Dim myDate2, myDate1 As DateTime
    Dim ttime, flag As Integer
    Dim total As Double = 0
    Dim hours As Double = 0
    Dim day As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        If Session("empid") = "" Then
            Response.Redirect("companyname.aspx")
        End If

        ' db.setLayout(Session("userId"))
        'End If
        lblempname.Text = db.getFieldValue("director", "ids", Session("empid"), "dirname")

        Dim deptid As String = db.getFieldValue("director", "ids", Session("empid"), "division")

        lbldepartment.Text = db.getFieldValue("divisions", "divisionid", deptid, "dname")

        lbldesignation.Text = db.getFieldValue("director", "ids", Session("empid"), "designation")
        lblempid.Text = db.getFieldValue("director", "ids", Session("empid"), "empno")
        If Not IsPostBack() Then
            lbldate.Text = DateTime.Today.ToString("dd-MMM-yyyy")
            lblday.Text = DateTime.Now.DayOfWeek.ToString()
            db.fillCombo(drpclient, "clientmaster", "companyname", "str(companyid)", " where companyid in(select client from ticket group by client) order by companyname", , "Over All")
            db.fillCombo(drpstatus, "statusmaster", "tktstatus", "tktstatus", " where usr='Employee' and tktstatus<>'Testing' and tktstatus<>'Speak' and tktstatus<>'Completed' order by tktstatus", , "Over All")
            db.fillCombo(Drpsts, "statusmaster", "tktstatus", "tktstatus", " where usr='Employee' and tktstatus<>'Completed'")



            lbldate.Text = DateTime.Today.ToString("dd-MMM-yyyy")
            lblday.Text = DateTime.Now.DayOfWeek.ToString()
            drprecords.DataSource = db.noRecords
            drprecords.DataBind()
            drprecords.SelectedValue = "10"
            drpclient.SelectedValue = "Over All"
            drpstatus.SelectedValue = "[ Select ]"
            drporderby.SelectedValue = "[Select]"
            Show()
        End If
        ' Updt.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")
    End Sub

    Private Sub Show()
        db.qry = "select (select case when CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)=0 then companyname else SUBSTRING(companyname, 0, CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)) end from clientmaster where companyid=(select client from ticket where tid=t.tid)) as client,t.* from ticketmaster as t,ticket as tkt where t.tid=tkt.tid and t.assignto=" & Session("empid") & " and t.status='Assigned' and t.empstatus<>'Testing' and t.empstatus<>'Speak' and t.empstatus<>'Completed'"

        If drpclient.SelectedValue <> "Over All" Then
            db.qry = db.qry & " and t.tid in(select tid from ticket where client=" & drpclient.SelectedValue & ")"
        End If
        'If drpstatus.SelectedValue <> "Over All" Then
        '    db.qry = db.qry & " and t.empstatus='" & drpstatus.SelectedValue & "'"
        'End If
        If drpstatus.SelectedValue = "0" And drpstatus.SelectedValue <> "Over All" Then
            db.qry = db.qry & "and t.empstatus in ('In progress','Pending')"
        Else
            db.qry = db.qry & " and t.empstatus='" & drpstatus.SelectedValue & "'"
        End If

        If drporderby.SelectedValue = "0" Then

            db.qry = db.qry & "order by t.empstatus"
        ElseIf drporderby.SelectedValue = "1" Then

            db.qry = db.qry & " order by t.tid"

        ElseIf drporderby.SelectedValue = "2" Then

            db.qry = db.qry & " order by tkt.client"

        ElseIf drporderby.SelectedValue = "3" Then

            db.qry = db.qry & " order by Convert(smalldatetime, t.adt1)"


        End If


        ds = db.fillDataSet()

        If ds.Tables(0).Rows.Count > 0 Then

            If drprecords.SelectedValue = "ALL" Then
                Gv1.PageSize = ds.Tables(0).Rows.Count
            Else
                Gv1.PageSize = CInt(drprecords.SelectedValue)
            End If

            Gv1.DataSource() = ds
            Gv1.DataBind()

            Gv1.HeaderRow.Cells(0).Text = "#"
            Gv1.HeaderRow.Cells(0).Font.Size = 9

            Gv1.HeaderRow.Cells(1).Text = "Client & Project <br/> TicketNo. , Date"
            Gv1.HeaderRow.Cells(1).Font.Size = 9

            Gv1.HeaderRow.Cells(2).Text = "Assign Date"
            Gv1.HeaderRow.Cells(2).Font.Size = 9

            'Gv1.HeaderRow.Cells(3).Text = "Client"
            'Gv1.HeaderRow.Cells(3).Font.Size = 9

            'Gv1.HeaderRow.Cells(4).Text = "Project"
            'Gv1.HeaderRow.Cells(4).Font.Size = 9

            Gv1.HeaderRow.Cells(3).Text = "Job/ Point Description"
            Gv1.HeaderRow.Cells(3).Font.Size = 9

            Gv1.HeaderRow.Cells(4).Text = "Total<br/>Job Time <br/>(In Days)"
            Gv1.HeaderRow.Cells(4).Font.Size = 9

            Gv1.HeaderRow.Cells(5).Text = "Target<br/>Date "
            Gv1.HeaderRow.Cells(5).Font.Size = 9

            Gv1.HeaderRow.Cells(6).Text = "Progress Remark"
            Gv1.HeaderRow.Cells(6).Font.Size = 9

            'Gv1.HeaderRow.Cells(7).Text = "Status"
            'Gv1.HeaderRow.Cells(7).Font.Size = 9

            Gv1.HeaderRow.Cells(8).Text = "Start Time"
            Gv1.HeaderRow.Cells(8).Font.Size = 9

            Gv1.HeaderRow.Cells(9).Text = "Day Consumed<br/>Working/ Time <br/>"
            Gv1.HeaderRow.Cells(9).Font.Size = 9

            Gv1.HeaderRow.Cells(10).Text = "Action"
            Gv1.HeaderRow.Cells(10).Font.Size = 9

            If Drpsts.SelectedValue <> "Testing" And Drpsts.SelectedValue <> "Speak" And drpstatus.SelectedValue = "Over All" Then
                If col1.Length() > 0 Then
                    For i = 0 To UBound(col1)
                        Gv1.Rows(col1(i)).BackColor = Drawing.Color.Red
                    Next
                End If
            End If
            Dim j As Integer = -1
            Dim flag, l As Integer

            For l = 0 To Gv1.Rows.Count - 1
                Dim ck1 As CheckBox = CType(Gv1.Rows(l).Cells(8).FindControl("chkonoff"), CheckBox)
                Dim hdn As HiddenField = CType(Gv1.Rows(l).Cells(8).FindControl("HiddenField1"), HiddenField)

                strqry1 = "select flag from ticketmaster  where tsid=" & CInt(hdn.Value) & ""
                dt1 = db.fillReader1(strqry1)
                dr = dt1.CreateDataReader()
                If dr.read Then
                    flag = dr("flag")
                End If
                dr.Close()
                dt1.Clear()
                dt1.Dispose()
                If flag = 1 Then
                    ck1.Checked = True
                End If
                j = j + 1
                ReDim Preserve col1(j)
                col1(j) = l
            Next

        Else
            Gv1.DataSource = Nothing
            Gv1.DataBind()

        End If

        ds.Dispose()
        'dr.close()

    End Sub

    Protected Sub chkonoff_CheckedChanged(sender As Object, e As EventArgs)

        Dim f As Integer
        Dim j As Integer = -1
        Dim i As Integer
        lblstart.Text = System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm")
        For i = 0 To Gv1.Rows.Count - 1
            Dim status As Label = CType(Gv1.Rows(i).Cells(5).FindControl("lblstatus"), Label)
            Dim ck1 As CheckBox = CType(Gv1.Rows(i).Cells(6).FindControl("chkonoff"), CheckBox)
            Dim hdn As HiddenField = CType(Gv1.Rows(i).Cells(7).FindControl("HiddenField1"), HiddenField)

            Dim tstop As Label = CType(Gv1.Rows(i).Cells(8).FindControl("lblsttop"), Label)

            strqry1 = "select flag from TicketMaster  where tsid=" & CInt(hdn.Value) & ""
            dt1 = db.fillReader1(strqry1)
            dr = dt1.CreateDataReader()

            If dr.read Then
                f = dr("flag")
            End If
            dr.Close()
            dt1.Clear()
            dt1.Dispose()

            If ck1.Checked = True And f = 0 Then
                tstop.Text = ""
                'If tstatus <> "Pending" Then
                If CStr(hdn.Value) <> "" And status.Text = "Pending" Or status.Text = "pending" Then
                    db.qry = "update TicketMaster set starttime = '" & lblstart.Text & "' , stoptime='" & tstop.Text & "', empstatus='In Progress',flag=1 where tsid=" & CInt(hdn.Value) & ""
                    db.executeQuery()

                    j = j + 1
                    ReDim Preserve col1(j)
                    col1(j) = i

                ElseIf CStr(hdn.Value) <> "" And status.Text = "Bug" Or status.Text = "bug" Then
                    db.qry = "update TicketMaster set starttime = '" & lblstart.Text & "' , stoptime='" & tstop.Text & "', empstatus='In Progress',flag=1 where tsid=" & CInt(hdn.Value) & ""
                    db.executeQuery()

                    j = j + 1
                    ReDim Preserve col1(j)
                    col1(j) = i
                    Show()
                    Exit Sub
                ElseIf CStr(hdn.Value) <> "" And status.Text = "In Progress" Then

                    db.qry = "update TicketMaster set starttime = '" & lblstart.Text & "' , stoptime='" & tstop.Text & "',flag=1 where tsid=" & CInt(hdn.Value) & ""
                    db.executeQuery()

                    j = j + 1
                    ReDim Preserve col1(j)
                    col1(j) = i
                End If

                Show()
                Dim rd As CheckBox = CType(Gv1.Rows(i).Cells(7).FindControl("chkonoff"), CheckBox)
                rd.Checked = True
                'rd.BackColor = Drawing.Color.Green


            ElseIf ck1.Checked = False And f = 1 Then

                lblstop.Text = System.DateTime.Now.ToString("dd MMMM yyyy HH:mm")
                If CStr(hdn.Value) <> "" And status.Text = "In Progress" Then
                    strqry1 = "select totaltime,starttime from TicketMaster  where tsid=" & CInt(hdn.Value) & ""
                    dt1 = db.fillReader1(strqry1)
                    dr = dt1.CreateDataReader()
                    If dr.read Then
                        'lblstart.Text = dr("totaltime").ToString()
                        ttime = dr("totaltime").ToString()
                        myDate1 = dr("starttime").ToString()
                    End If
                    dr.Close()
                    dt1.Clear()
                    dt1.Dispose()

                    myDate2 = lblstop.Text
                    Dim difference As TimeSpan = myDate2.Subtract(myDate1)

                    Dim totalMinutes As Integer = difference.TotalMinutes
                    ttime += totalMinutes
                    'lblstart.Text = ttime.ToString()


                    db.qry = "update TicketMaster set StopTime = '" & lblstop.Text & "' , totaltime='" & ttime & "',flag=0 where tsid=" & CInt(hdn.Value) & ""
                    db.executeQuery()
                    j = j + 1
                    ReDim Preserve col1(j)
                    col1(j) = i
                End If
            End If
        Next
        Show()
    End Sub

    Protected Sub Gv1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gv1.RowDataBound
        Dim j As Integer = -1
        Dim t As Double

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = (Gv1.PageIndex * Gv1.PageSize) + (e.Row.RowIndex + 1)
            Dim time As Label = CType(e.Row.FindControl("lblduration"), Label)
            t += CDbl(time.Text)
            't += dr("timeduration")
            total += t

            Dim cminutes As Label = CType(e.Row.FindControl("txtttime"), Label)
            Dim minutes As Double = CDbl(cminutes.Text)

            If minutes <> 0 Then
                Dim cm As String = minutes / 480
                Dim cc() As String = cm.Split(".")
                If cc(0) = "0" Then
                    day = cc(0)
                    Dim min As Double = day * 480
                    Dim h As String = minutes - min
                    Dim hh() As String = h.Split(".")
                    Dim hourss As Double = hh(0)
                    If hourss <> "0" Then
                        hourss = hh(0)
                        Dim hm As Integer = CInt(hh(0))
                        If hm > 60 Then
                            Dim hs As String = hourss / 60
                            Dim hfs() As String = hs.Split(".")
                            hours = hfs(0)
                            Dim hf As Integer = hours * 60

                            Dim fm As Integer = hm - hf
                            minutes = fm
                        End If
                    Else
                        hours = 0
                    End If

                ElseIf cc(0) <> "0" Then
                    day = cc(0)
                    Dim min As Double = day * 480
                    Dim h As String = minutes - min
                    Dim hh() As String = h.Split(".")
                    Dim hourss As Double = hh(0)
                    If hourss <> "0" Then
                        hourss = hh(0)
                        Dim hm As Integer = CInt(hh(0))
                        If hm > 60 Then
                            Dim hs As String = hourss / 60
                            Dim hfs() As String = hs.Split(".")
                            hours = hfs(0)
                            Dim hf As Integer = hours * 60

                            Dim fm As Integer = hm - hf
                            minutes = fm
                        End If
                    Else
                        hours = 0
                    End If


                    'day = 0
                    'Dim hh As String = cc(1)
                    'Dim hfh As String = hh.Substring(0, 2)
                    'If hfh > "60" Then
                    '    hours = CInt(hfh / 60)
                    'Else
                    '    hours = 0
                    'End If
                End If
                cminutes.Text = "Days:" & day & " ;Hours:" & hours & "; Minutes:" & minutes & ""

            End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).Text = "Total time "
            e.Row.Cells(4).Text = total

        End If

    End Sub

    Protected Sub Gv1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gv1.PageIndexChanging
        Gv1.PageIndex = e.NewPageIndex
        Show()
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Show()
    End Sub

    'Protected Sub Gv1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles Gv1.RowUpdating
    '    Dim t1 As TextBox
    '    t1 = CType(Gv1.Rows(e.RowIndex).FindControl("txteremark"), TextBox)
    '    db.qry = "update Ticketmaster set eremark='" & t1.Text & "'  where tsid=" & CInt(Gv1.DataKeys(e.RowIndex).Value)
    '    db.executeQuery()
    '    Show()
    'End Sub
    
    Protected Sub Gv1_RowUpdating(sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles Gv1.RowUpdating
        Dim l1, l2 As Label
        l1 = CType(Gv1.Rows(e.RowIndex).FindControl("lblstatus"), Label)
        l2 = CType(Gv1.Rows(e.RowIndex).FindControl("lblsttop"), Label)
        If l1.Text = "In Progress" And l2.Text <> "" Then
            db.qry = "update ticketmaster set empstatus='Testing'  where tsid=" & CInt(Gv1.DataKeys(e.RowIndex).Value)
            db.executeQuery()

        ElseIf l1.Text = "Pending" And l2.Text = "" Then
            db.qry = "update ticketmaster set empstatus='Testing'  where tsid=" & CInt(Gv1.DataKeys(e.RowIndex).Value)
            db.executeQuery()
        End If
        Show()
    End Sub
    Protected Sub Gv1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles Gv1.RowDeleting
        Dim t1 As TextBox
        t1 = CType(Gv1.Rows(e.RowIndex).FindControl("txteremark"), TextBox)
        db.qry = "update Ticketmaster set eremark='" & t1.Text & "'  where tsid=" & CInt(Gv1.DataKeys(e.RowIndex).Value)
        db.executeQuery()
        Show()
    End Sub

End Class

