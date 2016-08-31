Imports System.Data
Imports System.Web.Script
Partial Class Admin_TicketWorkReAssign
    Inherits System.Web.UI.Page
    Public db As New general

    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String
    Dim ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        'db.setLayout(Session("userId"))
        'End If

        If Not IsPostBack Then

            db.fillCombo(dremp, "director", "dirname", "ids", " where 1=2")
            'db.fillCombo(drpemp1, "director", "dirname", "ids", " where empno<>'0' and flg=0 and ids in (select assignto from ticketmaster) order by dirname ")
            db.fillCombo(drpemp1, "director", "dirname", "Ids", " where 1=2")
            'db.fillCombo(drpemp1, "director", "dirname", "str(ids)", " where empno<>'0' and flg=0 and ids in (select assignto from ticketmaster where assignby=" & Session("empid") & " group by assignto) order by dirname", , "Over All")
            'db.fillCombo(drpclient, "ticket", "client", "client", " group by client order by client")
            db.fillCombo(drppr, "ticketmaster", "str(priority)", "priority", " where 1=2")
            db.fillCombo(drpempstatus, "statusmaster", "tktstatus", "tktstatus", " where usr='Employee' and tktstatus<>'Testing' and tktstatus<>'Completed' order by tktstatus", , "Over All")
            db.fillCombo(drpdivision, "divisions", "dname", "divisionid", " where 1=1 order by dname ")
            db.fillCombo(drpdivisionall, "divisions", "dname", "divisionid", " where 1=1 order by dname")
            drprecords.DataSource = db.noRecords
            drprecords.DataBind()
            drprecords.SelectedValue = "10"
            db.fillCombo(drpclient, "clientmaster", "companyname", "str(companyid)", " where companyid in(select client from ticket group by client) order by companyname", , "Over All")

            lbldate.Text = DateTime.Today.ToString("dd-MMM-yyyy")
            lblday.Text = DateTime.Now.DayOfWeek.ToString()
            drpclient.SelectedValue = "Over All"
            drpemp1.SelectedValue = "Over All"
            drpempstatus.SelectedValue = "Over All"
            show()

        End If

        'btnassigned.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")
        'btnsubmit.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")

    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        show()
    End Sub

    Private Sub show()
        If Session("empid") = "" Then
            Response.Redirect("companyname.aspx")
        End If

        'db.qry = "select (select case when CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)=0 then companyname else SUBSTRING(companyname, 0, CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)) end from clientmaster where companyid=(select client from ticket where tid=t.tid)) as client,(select dirname from director where ids=t.assignto) as assignto,t.* from ticketmaster as t,ticket as tkt where t.tid=tkt.tid and t.status='Assigned' and t.assignby=" & Session("empid") & " and t.empstatus<>'Testing'"
        db.qry = "select (select case when CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)=0 then companyname else SUBSTRING(companyname, 0, CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)) end from clientmaster where companyid=(select client from ticket where tid=t.tid)) as client,(select dirname from director where ids=t.assignto) as assignto,t.* from ticketmaster as t,ticket as tkt where t.tid=tkt.tid and t.status='Assigned' and t.empstatus<>'Testing'"

        If drpclient.SelectedValue <> "Over All" Then
            db.qry = db.qry & " and t.tid in(select tid from ticket where client=" & drpclient.SelectedValue & ")"
        End If

        If drpemp1.SelectedValue <> "Over All" Then
            db.qry = db.qry & " and t.assignto=" & drpemp1.SelectedValue & ""
        End If

        If drpempstatus.SelectedValue <> "Over All" Then
            db.qry = db.qry & " and t.empstatus='" & drpempstatus.SelectedValue & "'"
        End If

        If drporderby.SelectedValue = "1" Then
            db.qry = db.qry & " order by t.tid"
        ElseIf drporderby.SelectedValue = "2" Then
            db.qry = db.qry & " order by t.assignto"
        ElseIf drporderby.SelectedValue = "3" Then
            db.qry = db.qry & " order by tkt.client"
        ElseIf drporderby.SelectedValue = "4" Then
            db.qry = db.qry & " order by Convert(smalldatetime, t.adt1)"
        End If

        ds = db.fillDataSet()

        If ds.Tables(0).Rows.Count > 0 Then

            If drprecords.SelectedValue = "ALL" Then
                Gv1.PageSize = ds.Tables(0).Rows.Count
            Else
                Gv1.PageSize = CInt(drprecords.SelectedValue)
            End If

            Gv1.DataSource = ds
            Gv1.DataBind()
            Gv1.HeaderRow.Cells(0).Text = "#"
            Gv1.HeaderRow.Cells(0).Font.Size = 9
            Gv1.HeaderRow.Cells(1).Text = "Tkt._No. & <br/>Client &<br/>Project"
            Gv1.HeaderRow.Cells(1).Font.Size = 9

            Gv1.HeaderRow.Cells(2).Text = "Description"
            Gv1.HeaderRow.Cells(2).Font.Size = 9

            Gv1.HeaderRow.Cells(3).Text = "Client_Remark"
            Gv1.HeaderRow.Cells(3).Font.Size = 9

            Gv1.HeaderRow.Cells(4).Text = "Priority/<br/>Remark "
            Gv1.HeaderRow.Cells(4).Font.Size = 9

            Gv1.HeaderRow.Cells(5).Text = "Assigned_Date & <br/>Assigned to"
            Gv1.HeaderRow.Cells(5).Font.Size = 9

            Gv1.HeaderRow.Cells(6).Text = "Time <br/>(in Days)"
            Gv1.HeaderRow.Cells(6).Font.Size = 9

            Gv1.HeaderRow.Cells(7).Text = "New Assign Time"
            Gv1.HeaderRow.Cells(7).Font.Size = 9

            Gv1.HeaderRow.Cells(9).Text = "Work Report"
            Gv1.HeaderRow.Cells(9).Font.Size = 9



        Else
            Gv1.DataSource = Nothing
            Gv1.DataBind()

        End If


        ds.Dispose()

    End Sub



    Protected Sub Gv1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gv1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = (Gv1.PageIndex * Gv1.PageSize) + (e.Row.RowIndex + 1)
        End If
    End Sub

    Protected Sub btnassigned_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnassigned.Click

        Dim i, tassign As Integer
        Dim status As String
        If dremp.SelectedValue <> "0" Then

            For i = 0 To Gv1.Rows.Count - 1
                Dim assign As Label = CType(Gv1.Rows(i).Cells(5).FindControl("lblassign"), Label)
                Dim time As TextBox = CType(Gv1.Rows(i).Cells(7).FindControl("txtduration"), TextBox)
                Dim wtime As Label = CType(Gv1.Rows(i).Cells(6).FindControl("lbltime"), Label)
                Dim ck As CheckBox = CType(Gv1.Rows(i).Cells(10).FindControl("CheckBox1"), CheckBox)
                Dim id1 As Label = CType(Gv1.Rows(i).Cells(1).FindControl("lblid"), Label)
                Dim targetdate As TextBox = CType(Gv1.Rows(i).Cells(1).FindControl("txttargetdate"), TextBox)


                If ck.Checked = True Then

                    Dim hdn As HiddenField = CType(Gv1.Rows(i).Cells(10).FindControl("HiddenField1"), HiddenField)
                    'db.qry = "select tid,assignto from ticketmaster where assignto =" & drpemp1.SelectedValue & " and tsid=" & CInt(hdn.Value) & " "
                    'dr = db.fillReader
                    'If dr.read Then

                    '    idt = dr("tid").ToString()
                    '    tassign = dr("assignto").ToString()
                    'End If
                    'dr.close()
                    strqry1 = "select empstatus from ticketmaster where  tsid=" & CInt(hdn.Value) & " "
                    dt1 = db.fillReader1(strqry1)
                    dr = dt1.CreateDataReader()
                    If dr.Read Then

                        status = dr("empstatus").ToString()

                    End If
                    dr.Close()
                    dt1.Clear()
                    dt1.Dispose()
                    'If CStr(hdn.Value) <> "" And time.Text <> "" And wtime.Text = 0 And tassign <> dremp.SelectedValue And idt <> id1.Text Then
                    If CStr(hdn.Value) <> "" And time.Text <> "" And wtime.Text = 0 And status <> "In Progress" Then
                        db.qry = "update ticketmaster set empstatus = 'Pending',assignby=" & Session("empid") & " ,timeduration=" & time.Text & ",targetdate='" & targetdate.Text & "',assignto=" & dremp.SelectedValue & " ,starttime='',stoptime='',adt2='" & Now.Date.ToString("dd-MMM-yyyy") & "' where tsid=" & CInt(hdn.Value) & ""
                        db.executeQuery()

                        'ElseIf CStr(hdn.Value) <> "" And wtime.Text <> 0 And tassign <> dremp.SelectedValue And idt <> id1.Text Then
                    ElseIf CStr(hdn.Value) <> "" And wtime.Text <> 0 And status = "In Progress" Then
                        strqry1 = "select * from ticketMaster where tsid=" & CInt(hdn.Value) & ""
                        dt1 = db.fillReader1(strqry1)
                        dr = dt1.CreateDataReader()
                        While dr.Read
                            '        'db.isExists("ticketmaster", "tid", " & id.text &  ", False)

                            db.qry = "insert into ticketmaster values(" & db.getMaxId("ticketmaster", "tsid") & "," & dr(1) & ",'" & dr(2) & "','" & dr(3) & "','" & dr(4) & "','Assigned','" & Session("empid") & "','" & dremp.SelectedValue & "','" & Now.Date.ToString("dd-MMM-yyyy") & "','" & dr(9) & "','" & dr(10) & "','Pending','" & dr(12) & "','" & dr(13) & "','" & dr(14) & "','','','0','" & time.Text & "','','','" & targetdate.Text & "','0'," & dremp.SelectedValue & ")"
                            db.executeQuery()

                            db.qry = "update ticketmaster set message= 'This work is also assign to:" & dremp.SelectedItem.Text & "' where tsid=" & CInt(hdn.Value) & ""
                            db.executeQuery()
                        End While
                        dr.Close()
                        dt1.Clear()
                        dt1.Dispose()
                        'Else
                        '    Label1.Text = "error"
                        '<script language=javascript>alert('this ticket is already assigned.')</script>
                    ElseIf CStr(hdn.Value) <> "" And wtime.Text = 0 And status = "In Progress" Then
                        strqry1 = "select * from ticketMaster where tsid=" & CInt(hdn.Value) & ""

                        dt1 = db.fillReader1(strqry1)
                        dr = dt1.CreateDataReader()
                        While dr.Read
                            '        'db.isExists("ticketmaster", "tid", " & id.text &  ", False)

                            db.qry = "insert into ticketmaster values(" & db.getMaxId("ticketmaster", "tsid") & "," & dr(1) & ",'" & dr(2) & "','" & dr(3) & "','" & dr(4) & "','Assigned','" & Session("userid") & "','" & dremp.SelectedValue & "','" & Now.Date.ToString("dd-MMM-yyyy") & "','" & dr(9) & "','" & dr(10) & "','Pending','" & dr(12) & "','" & dr(13) & "','" & dr(14) & "','','','0','" & time.Text & "','','','')"
                            db.executeQuery()



                            db.qry = "update ticketmaster set message= 'This work is also assign to:" & dremp.SelectedItem.Text & "' where tsid=" & CInt(hdn.Value) & ""
                            db.executeQuery()
                        End While
                        dr.Close()
                        dt1.Clear()
                        dt1.Dispose()
                    End If


                End If

            Next

            show()

        End If


    End Sub

    Protected Sub drpemp1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpemp1.SelectedIndexChanged

        If drpemp1.SelectedValue <> "0" And drpemp1.SelectedValue <> "Over All" Then
            db.fillCombo(drppr, "ticketmaster", "str(priority)", "priority", " where assignto=" & drpemp1.SelectedValue & " group by priority order by priority")
            Dim cn As Integer = db.getFieldValue("ticketmaster", "assignto", drpemp1.SelectedValue, "max(priority)", True)
            drppr.Items.Add(cn + 1)
        Else
            db.fillCombo(drppr, "ticketmaster", "str(priority)", "priority", " where 1=2")

        End If

        Gv1.DataSource = Nothing
        Gv1.DataBind()

    End Sub

    Protected Sub btnpr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpr.Click

        Dim i As Integer

        If drpemp1.SelectedValue <> "0" And drppr.SelectedValue <> "0" Then

            For i = 0 To Gv1.Rows.Count - 1

                Dim ck As CheckBox = CType(Gv1.Rows(i).Cells(10).FindControl("CheckBox1"), CheckBox)
                If ck.Checked = True Then
                    Dim hdn As HiddenField = CType(Gv1.Rows(i).Cells(10).FindControl("HiddenField1"), HiddenField)
                    If CStr(hdn.Value) <> "" Then
                        db.qry = "update ticketmaster set priority=" & drppr.SelectedValue & " where tsid=" & CInt(hdn.Value) & ""
                        db.executeQuery()
                    End If
                End If

            Next

            db.fillCombo(drppr, "ticketmaster", "str(priority)", "priority", " where assignto=" & drpemp1.SelectedValue & " group by priority order by priority")
            Dim cn As Integer = db.getFieldValue("ticketmaster", "assignto", drpemp1.SelectedValue, "max(priority)", True)
            drppr.Items.Add(cn + 1)

            show()

        End If

    End Sub


    Protected Sub Gv1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gv1.PageIndexChanging

        Gv1.PageIndex = e.NewPageIndex
        show()

    End Sub

    Protected Sub btnremark_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnremark.Click

        Dim i As Integer


        For i = 0 To Gv1.Rows.Count - 1

            Dim ck As CheckBox = CType(Gv1.Rows(i).Cells(10).FindControl("CheckBox1"), CheckBox)
            Dim t1 As TextBox = CType(Gv1.Rows(i).Cells(6).FindControl("txtremark"), TextBox)
            If ck.Checked = True Then
                Dim hdn As HiddenField = CType(Gv1.Rows(i).Cells(10).FindControl("HiddenField1"), HiddenField)
                If CStr(hdn.Value) <> "" And t1.Text <> "" Then
                    db.qry = "update ticketmaster set premark='" & t1.Text & "' where tsid=" & CInt(hdn.Value) & ""
                    db.executeQuery()
                End If
            End If

        Next
        show()


    End Sub

    Protected Sub drpdivision_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpdivision.SelectedIndexChanged
        'db.fillCombo(drpemp1, "director", "dirname", "Ids", " where division=" & drpdivision.SelectedValue & " order by dirname")
        ' db.fillCombo(drpemp1, "director", "dirname", "ids", " where empno<>'0' and flg=0 and division=" & drpdivision.SelectedValue & "  and ids in (select assignto from ticketmaster) order by dirname ")
        db.fillCombo(drpemp1, "director", "dirname +' '+ empmname + ' '+ emplname", "ids", " where empno<>'0' and flg=0 and division=" & drpdivision.SelectedValue & "  and ids in (select assignto from ticketmaster) order by dirname ")

    End Sub

    Protected Sub txttargetdate_TextChanged(sender As Object, e As EventArgs)
        Dim i As Integer

        For i = 0 To Gv1.Rows.Count - 1
            Dim targetdate As TextBox = CType(Gv1.Rows(i).Cells(7).FindControl("txttargetdate"), TextBox)
            If targetdate.Text <> "" Then
                Dim targetdays As DateTime = targetdate.Text
                Dim currentdate As DateTime = DateTime.Today.ToString("dd-MMM-yyyy")
                Dim objTimeSpan As TimeSpan = targetdays - currentdate
                Dim Days As Double = Convert.ToDouble(objTimeSpan.TotalDays)
                Dim td As String = Days
                'Dim td() As String = d.Split()
                If td(0) = "-" Then
                    Response.Write("<script>alert('Please Do not select back date')</script>")
                    targetdate.Text = ""
                    targetdate.Focus()
                    Exit Sub
                End If

                Dim tdays As TextBox = CType(Gv1.Rows(i).Cells(7).FindControl("txtduration"), TextBox)
                tdays.Text = Days
            End If
        Next
    End Sub

    Protected Sub drpdivisionall_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpdivisionall.SelectedIndexChanged

        db.fillCombo(dremp, "director", "dirname +' '+ empmname + ' '+ emplname ", "Ids", " where division=" & drpdivisionall.SelectedValue & " order by dirname")

    End Sub
End Class
