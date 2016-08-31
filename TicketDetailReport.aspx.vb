Imports System.Data
Imports System.IO

Partial Class TicketDetailReport
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String
    Dim ds As New DataSet
    Dim quantity As Integer
    Dim total, total1 As Integer
    Dim m As Integer
    Dim i As Integer
    Dim col1() As Integer = {}
    Dim myDate2, myDate1 As DateTime
    Dim ttime, flag As Integer
    Dim totals As Double = 0
    Dim hours As Double = 0
    Dim day As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        'db.setLayout(Session("userId"))
        'lblcid.Text = db.getFieldValue("director", "ids", Request.QueryString("ids"), "empno")
        lbldesignation.Text = db.getFieldValue("director", "ids", Request.QueryString("ids"), "designation")
        lbldt.Text = DateTime.Today.ToString("dd-MMM-yyyy")
        lbld.Text = DateTime.Now.DayOfWeek.ToString()
        If Not IsPostBack Then
            'db.fillListBox(listemp1, "director", "dirname", "str(Ids)", " where ids in (select assignto from ticketmaster group by assignto) order by dirname")
            'db.fillListBox(liststatus, "statusmaster", "tktstatus", "tktstatus", " where usr='Admin' order by tktstatus")
            'db.fillListBox(listempstatus, "statusmaster", "tktstatus", "tktstatus", " where usr='Employee' order by tktstatus")
            'db.fillListBox(listclient, "clientmaster", "companyname", "str(companyid)", " where companyid in(select client from ticket group by client) order by companyname")
            'db.fillCheckList(DropDownCheckBoxes2, "statusmaster", "tktstatus", "tktstatus", " where usr='Employee' order by tktstatus")
            'db.fillCheckList(DropDownCheckBoxes3, "director", "dirname", "str(Ids)", " where ids in (select assignto from ticketmaster group by assignto) order by dirname")
            'db.fillCheckList(ddchkClient, "clientmaster", "companyname", "str(companyid)", " where companyid in(select client from ticket group by client) order by companyname")
            'db.fillCheckList(DropDownCheckBoxes1, "statusmaster", "tktstatus", "tktstatus", " where usr='Admin' order by tktstatus")

            'db.fillCombo(drpProjectName, "projects", "ProjectName", "Projectcode", , , "OverAll")


            db.fillCombo(drpclient, "clientmaster", "companyname", "str(companyid)", " order by companyname", , "OverAll")
            ''db.fillCombo(drpclient, "clientmaster", "companyname", "companyid", , , "OverAll")

            'drpclient.Attributes.Add("OverAll", "OverAll")
            db.fillCombo(drpproject, "ticketmaster", "project", "project", " group by project order by project", , "OverAll")
            db.fillCombo(drpstatus, "statusmaster", "tktstatus", "tktstatus", " where usr='Employee' and tktstatus<>'SPEAK' and tktstatus<>'Hold By Client' and tktstatus<>'Hold By Kamtech' and tktstatus<>'Assigned' order by tktstatus", , "Over All")

            db.fillCombo(drpdivision, "divisions", "dname", "divisionid", " where 1=1 order by dname")
            db.fillCombo(dremp, "director", "dirname", "Ids", " where 1=2")

            drprecords.DataSource = db.noRecords
            drprecords.DataBind()
            drprecords.SelectedValue = "ALL"
            lbldate.Text = DateTime.Today.ToString("dd-MMM-yyyy")
            lblday.Text = DateTime.Now.DayOfWeek.ToString()

            If Request.QueryString("emp") = "" And Request.QueryString("s1") <> "" Then
                show1()
            ElseIf Request.QueryString("emp") <> "" Then
                show2()
            ElseIf Request.QueryString("com") <> "" And Request.QueryString("s2") <> "" Then
                show3()
            Else
                ' show(1)
            End If
        End If

    End Sub


    Private Sub show(ByVal t As Integer)

        Dim client As String = ""
        Dim emp1 As String = ""
        Dim status As String = ""
        Dim empstatus As String = ""

        'Dim startdt As String = d1.SelectedValue + "-" + m1.SelectedValue + "-" + y1.SelectedValue
        'Dim enddt As String = d2.SelectedValue + "-" + m2.SelectedValue + "-" + y2.SelectedValue
        Dim startdt As String = TextBox1.Text
        Dim enddt As String = TextBox2.Text

        If t = 1 Then
            db.qry = "select (select case when CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)=0 then companyname else SUBSTRING(companyname, 0, CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)) end from clientmaster where companyid=(select client from ticket where tid=t.tid)) as client,(select dirname from director where ids=t.assignto) as assignto,(select dirname from director where ids=t.assignby) as assignby,t.* from ticketmaster as t,ticket as tkt where t.tid=tkt.tid and t.status<>'Completed' and (convert(smalldatetime,t.adt1) between convert(smalldatetime,'" & Now.Date.AddDays(-2).ToString("dd-MMM-yyyy") & "') and convert(smalldatetime,'" & Now.Date.ToString("dd-MMM-yyyy") & "'))"
        Else
            db.qry = "select (select case when CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)=0 then companyname else SUBSTRING(companyname, 0, CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)) end from clientmaster where companyid=(select client from ticket where tid=t.tid)) as client,(select dirname from director where ids=t.assignto) as assignto,(select dirname from director where ids=t.assignby) as assignby,t.* from ticketmaster as t,ticket as tkt where t.tid=tkt.tid and (convert(smalldatetime,t.adt1) between convert(smalldatetime,'" & startdt & "') and convert(smalldatetime,'" & enddt & "'))"
        End If


        Dim s1 As String = ""
        Dim k As Integer = 0
        Dim t1 As Integer = 0



        If s1 <> "" Then
            db.qry = db.qry & " and t.tid in(select tid from ticket where client in(" & s1 & "))"
        End If


        s1 = ""
        k = 0
        t1 = 0


        If s1 <> "" Then
            db.qry = db.qry & " and (t.tsid in(select tsid from ticketmaster where assignto in(" & s1 & ") and empstatus<>'Testing' and empstatus<>'Speak') or tsid in(select tsid from ticketmaster where assignby in(" & s1 & ") and status='Assigned' and (empstatus='Testing' or empstatus='Speak')))"
        End If

        s1 = ""
        k = 0
        t1 = 0

        If s1 <> "" Then

            db.qry = db.qry & " and t.status in(" & s1 & ")"

        End If
        s1 = ""
        k = 0
        t1 = 0


        If s1 <> "" Then

            db.qry = db.qry & " and t.empstatus in(" & s1 & ")"

        End If

        If status = "Completed" Then
            db.qry = db.qry & " order by convert(smalldatetime,t.cdate1) desc"
        Else


        End If

        ds = db.fillDataSet()
        'dr = db.fillReader
        If ds.Tables(0).Rows.Count > 0 Then
            If drprecords.SelectedValue = "ALL" Then
                Gv1.PageSize = ds.Tables(0).Rows.Count
            Else
                Gv1.PageSize = CInt(drprecords.SelectedValue)
            End If

            Gv1.DataSource = ds
            Gv1.DataBind()
            Gv1.HeaderRow.Cells(0).Text = "S.No."


            If status = "Completed" Then
                Gv1.Columns(8).Visible = True
            ElseIf status = "Pending" Then
                Gv1.Columns(5).Visible = False
                Gv1.Columns(6).Visible = False
                Gv1.Columns(7).Visible = False
                Gv1.Columns(8).Visible = False
            Else
                '  Gv1.Columns(5).Visible = True
                Gv1.Columns(6).Visible = True
                Gv1.Columns(7).Visible = True
                Gv1.Columns(8).Visible = False

            End If

            If t = 0 Then

                pnlform.Visible = False
                pnldata.Visible = True

                If client <> "" Then
                    lblclient.Text = client
                Else
                    lblclient.Text = "All"
                End If

                If emp1 <> "" Then
                    lblemp.Text = emp1
                Else
                    lblemp.Text = "All"
                End If

                If status <> "" Then
                    lblstatus.Text = status
                Else
                    lblstatus.Text = "All"
                End If

                If empstatus <> "" Then
                    lblempstatus.Text = empstatus
                Else
                    lblempstatus.Text = "All"
                End If

            End If

        Else

            Gv1.DataSource = Nothing
            Gv1.DataBind()

            pnlform.Visible = True
            pnldata.Visible = False

        End If

        ds.Dispose()


        t = 0

    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        ' show(0)
        Gv1.Visible = False
        display()
    End Sub

    Public Sub display()

        Dim status As String
        Dim employee As String

        If drpclient.SelectedItem.Text = "OverAll" And drpproject.SelectedItem.Text = "OverAll" And drpstatus.SelectedItem.Text = "Over All" And dremp.SelectedItem.Text = "OverAll" Then
            db.qry = "select * from ticketmaster as tm,ticket as t, clientmaster as cm where tm.tid=t.tid and tm.adt1 between convert(smalldatetime,'" & TextBox1.Text & "') and convert(smalldatetime,'" & TextBox2.Text & "') and tm.empstatus in ('Pending','Completed','Bug','Completed','In Progress')  and  t.client=cm.companyid order by t.tid"
            'ElseIf drpclient.SelectedValue <> "" And drpproject.SelectedValue <> "" And drpstatus.SelectedValue <> "" Then
            'db.qry = " and tm.project='" & drpproject.SelectedItem.Text & "' and cm.companyid=" & drpclient.SelectedValue & "  and tm.empid=" & dremp.SelectedValue & ""

        Else

            If drpclient.SelectedItem.Text <> "OverAll" And drpclient.SelectedItem.Text <> "" Then
                db.qry = " and cm.companyid=" & drpclient.SelectedValue & " " & db.qry
            End If

            If drpstatus.SelectedValue <> "Over All" And drpstatus.SelectedValue <> "" Then
                status = drpstatus.SelectedValue
                db.qry = " and tm.empstatus='" & status & "'" & db.qry
            End If

            If dremp.SelectedValue <> "OverAll" And dremp.SelectedValue <> "" Then
                employee = dremp.SelectedValue
                db.qry = "and tm.empid=" & dremp.SelectedValue & "" & db.qry
            End If

            If drpproject.SelectedItem.Text <> "OverAll" And drpproject.SelectedItem.Text <> "" Then
                db.qry = " and tm.project='" & drpproject.SelectedValue & "'" & db.qry
            End If

            db.qry = "select * from ticketmaster as tm,ticket as t, clientmaster as cm where tm.tid=t.tid and tm.adt1 between convert(smalldatetime,'" & TextBox1.Text & "') and convert(smalldatetime,'" & TextBox2.Text & "') and t.client=cm.companyid " & db.qry

            'ElseIf drpclient.SelectedItem.Text <> "" And drpproject.SelectedValue <> "" And drpstatus.SelectedItem.Text <> "" Then
            '    db.qry = "select * from ticketmaster as tm,ticket as t, clientmaster as cm where tm.tid=t.tid and tm.project='" & drpproject.SelectedItem.Text & "'  and tm.adt1 between convert(smalldatetime,'" & TextBox1.Text & "') and convert(smalldatetime,'" & TextBox2.Text & "') and tm.empstatus in ('Pending','Completed','Bug','Completed','In Progress')  and  t.client=cm.companyid and tm.empid=" & dremp.SelectedValue & " order by t.tid"
            'ElseIf drpclient.SelectedValue <> "" And drpproject.SelectedValue <> "" And drpstatus.SelectedItem.Text = "Over All" Then
            '    'db.qry = "select * from ticketmaster as tm,ticket as t, clientmaster as cm where tm.tid=t.tid and tm.project='" & drpproject.SelectedItem.Text & "' and cm.companyid=" & drpclient.SelectedValue & " and tm.adt1 between convert(smalldatetime,'" & TextBox1.Text & "') and convert(smalldatetime,'" & TextBox2.Text & "') and tm.empstatus in ('Pending','Completed','Bug','Completed','In Progress')  and  t.client=cm.companyid and tm.empid=" & Session("empid") & " order by t.tid"
            '    db.qry = "select * from ticketmaster as tm,ticket as t, clientmaster as cm where tm.tid=t.tid and tm.project='" & drpproject.SelectedItem.Text & "' and cm.companyid=" & drpclient.SelectedValue & " and tm.adt1 between convert(smalldatetime,'" & TextBox1.Text & "') and convert(smalldatetime,'" & TextBox2.Text & "') and tm.empstatus in ('Pending','Completed','Bug','Completed','In Progress')  and  t.client=cm.companyid and tm.empid=" & dremp.SelectedValue & " order by t.tid"
            'ElseIf drpclient.SelectedValue <> "" Or drpproject.SelectedValue <> "" And drpstatus.SelectedValue <> "" Then
            '    'db.qry = "select * from ticketmaster as tm,ticket as t, clientmaster as cm where tm.tid=t.tid and tm.project='" & drpproject.SelectedItem.Text & "' and cm.companyid=" & drpclient.SelectedValue & " and tm.adt1 between convert(smalldatetime,'" & TextBox1.Text & "') and convert(smalldatetime,'" & TextBox2.Text & "') and tm.empstatus='" & drpstatus.SelectedItem.Text & "'  and  t.client=cm.companyid and tm.empid=" & Session("empid") & " order by t.tid"
            '    db.qry = "select * from ticketmaster as tm,ticket as t, clientmaster as cm where tm.tid=t.tid and tm.project='" & drpproject.SelectedItem.Text & "' and cm.companyid=" & drpclient.SelectedValue & " and tm.adt1 between convert(smalldatetime,'" & TextBox1.Text & "') and convert(smalldatetime,'" & TextBox2.Text & "') and tm.empstatus='" & drpstatus.SelectedItem.Text & "'  and  t.client=cm.companyid and tm.empid=" & dremp.SelectedValue & " order by t.tid"
            'db.qry = "select * from ticketmaster as tm,ticket as t, clientmaster as cm where tm.tid=t.tid and tm.adt1 between convert(smalldatetime,'" & TextBox1.Text & "') and convert(smalldatetime,'" & TextBox2.Text & "') and t.client=cm.companyid and tm.empid=" & Session("empid") & ""

        End If

        dt1 = db.fillReader1(db.qry)
        dr = dt1.CreateDataReader()
        gvstatement.DataSource = dr
        gvstatement.DataBind()

        dr.Close()
        dt1.Clear()
        dt1.Dispose()

    End Sub

    Protected Sub gvstatement_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvstatement.RowDataBound
        Dim t As Double
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim clientname As String = db.getFieldValue("clientmaster", "companyid", dr("client"), "companyname")

            e.Row.Cells(2).Text = dr("Project") & "<br/>" & clientname
            e.Row.Cells(3).Text = db.getFieldValue("director", "empid", dr("assignby"), "dirname")
            Dim time As Label = CType(e.Row.FindControl("lblduration"), Label)
            t += CDbl(time.Text)
            't += dr("timeduration")
            total += t

            Dim cminutes As Label = CType(e.Row.FindControl("lblduration"), Label)
            If cminutes.Text <> "" Then

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
                    cminutes.Text = "Days:" & day & " <br/>Hours:" & hours & "<br/> Minutes:" & minutes & ""

                End If
            End If
        End If
    End Sub

    Protected Sub Gv1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gv1.RowDataBound
        Dim hours As Integer
        'Dim minutes As Integer = 0
        Dim i As Integer
        If e.Row.RowType = DataControlRowType.DataRow Then
            total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "timeduration"))
            total1 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "totaltime"))
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            'hours = total / 60
            'minutes = total Mod 60
            e.Row.Cells(3).Text = "Total Time"
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(7).Text = total & "<br/>" & "<br/>"
            'e.Row.Cells(11).Text = total & "=" & hours & "hr" & minutes & "min"
            'hours = total1 / 60
            'minutes = total1 Mod 60
            'e.Row.Cells(12).Text = total1 & "<br/>" & "<br/>"
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 0 To Gv1.Rows.Count
                Dim stoptime As Label = DirectCast(e.Row.FindControl("lblstop"), Label)
                Dim estatus As Label = DirectCast(e.Row.FindControl("lblstatus"), Label)
                Dim img As Image = DirectCast(e.Row.FindControl("image1"), Image)


                If stoptime.Text = "" And estatus.Text = "In Progress" Then
                    img.Height = 20
                    img.Width = 20
                    img.ImageUrl = "~/Images/on.png"

                ElseIf stoptime.Text <> "" And estatus.Text = "In Progress" Then
                    img.Height = 20
                    img.Width = 20
                    img.ImageUrl = "~/Images/off.png"

                ElseIf estatus.Text = "Completed" Then
                    img.ImageUrl = "~/icons/completed.png"
                    img.Height = 30
                    img.Width = 30
                Else
                    img.ImageUrl = "~/Images/off.png"
                    img.Height = 20
                    img.Width = 20
                End If
            Next

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
                cminutes.Text = "Days:" & day & "<br/>Hours:" & hours & "<br/> Minutes:" & minutes & ""

            End If

        End If

    End Sub


    Protected Sub Gv1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gv1.PageIndexChanging
        Gv1.PageIndex = e.NewPageIndex
        show(0)
    End Sub

    Private Sub show1()

        Dim client As String = ""
        Dim emp1 As String = ""
        Dim status As String = ""
        Dim empstatus As String = ""


        db.qry = "select (select case when CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)=0 then companyname else SUBSTRING(companyname, 0, CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)) end from clientmaster where companyid=(select client from ticket where tid=t.tid)) as client,(select dirname from director where ids=t.assignto) as assignto,(select dirname from director where ids=t.assignby) as assignby,t.* from ticketmaster as t,ticket as tkt where t.tid=tkt.tid"


        If Request.QueryString("s1") = "NotCompleted" Then

            db.qry = db.qry & " and t.status<>'Completed'"

            status = "Pending"

        ElseIf Request.QueryString("s1") = "NotAssigned" Then

            db.qry = db.qry & " and t.status='Pending'"

            status = "Not Assigned"

        Else

            db.qry = db.qry & " and t.empstatus='" & Request.QueryString("s1") & "'"

            empstatus = Request.QueryString("s1")

        End If


        'db.qry = db.qry & " order by tkt.client"
        ' db.qry = db.qry & " order by flag desc"
        db.qry = db.qry & "order by t.flag desc"


        ds = db.fillDataSet()

        'dr = db.fillReader

        If ds.Tables(0).Rows.Count > 0 Then

            If drprecords.SelectedValue = "ALL" Then
                Gv1.PageSize = ds.Tables(0).Rows.Count
            Else
                Gv1.PageSize = CInt(drprecords.SelectedValue)
            End If

            Gv1.DataSource = ds
            Gv1.DataBind()
            'Gv1.HeaderRow.Cells(0).Text = "S.No."
            Gv1.HeaderRow.Cells(1).Text = "Client & Project <br/>TicketNo. ,  Date"
            'Gv1.HeaderRow.Cells(2).Text = "Client"
            'Gv1.HeaderRow.Cells(3).Text = "Project"
            Gv1.HeaderRow.Cells(4).Text = "Employee Name"
            Gv1.HeaderRow.Cells(5).Text = "Status"
            'Gv1.HeaderRow.Cells(6).Text = "AssignTo"
            'Gv1.HeaderRow.Cells(7).Text = "Status"
            'Gv1.HeaderRow.Cells(8).Text = "Com._Date"


            If status = "Completed" Then
                Gv1.Columns(8).Visible = True
            ElseIf status = "Pending" Then
                Gv1.Columns(4).Visible = True
                Gv1.Columns(5).Visible = True
                Gv1.Columns(6).Visible = False
                Gv1.Columns(7).Visible = True
                Gv1.Columns(8).Visible = True
            Else
                Gv1.Columns(4).Visible = True
                Gv1.Columns(5).Visible = True
                Gv1.Columns(6).Visible = False
                Gv1.Columns(7).Visible = True
                Gv1.Columns(8).Visible = True

            End If


            pnlform.Visible = False
            pnldata.Visible = True

            If client <> "" Then
                lblclient.Text = client
            Else
                lblclient.Text = "All"
            End If

            If emp1 <> "" Then
                lblemp.Text = emp1
            Else
                lblemp.Text = "All"
            End If

            If status <> "" Then
                lblstatus.Text = status
            Else
                lblstatus.Text = "All"
            End If

            If empstatus <> "" Then
                lblempstatus.Text = empstatus
            Else
                lblempstatus.Text = "All"
            End If



        Else

            Gv1.DataSource = Nothing
            Gv1.DataBind()

            pnlform.Visible = True
            pnldata.Visible = False

        End If

        ds.Dispose()


    End Sub

    Private Sub show2()

        Dim client As String = ""
        Dim emp1 As String = ""
        Dim status As String = ""
        Dim empstatus As String = ""
        db.qry = "select (select case when CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)=0 then companyname else SUBSTRING(companyname, 0, CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)) end from clientmaster where companyid=(select client from ticket where tid=t.tid)) as client,(select dirname from director where ids=t.assignto) as assignto,(select dirname from director where ids=t.assignby) as assignby,t.* from ticketmaster as t,ticket as tkt where t.tid=tkt.tid"
        If Request.QueryString("s1") = "" Then

            db.qry = db.qry & " and t.empstatus<>'Completed'"

        Else

            db.qry = db.qry & " and t.empstatus in ('" & Request.QueryString("s1") & "' , '" & Request.QueryString("s2") & "') "


            empstatus = Request.QueryString("s1")

        End If



        db.qry = db.qry & " and t.assignto=" & Request.QueryString("emp") & ""

        emp1 = db.getFieldValue("director", "ids", CInt(Request.QueryString("emp")), "dirname", True)

        If Request.QueryString("s1") = "" Then
            db.qry = db.qry & " order by tkt.client"
        Else
            'db.qry = db.qry & " order by t.empstatus "
            db.qry = db.qry & " order by t.flag desc "

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
            'Gv1.HeaderRow.Cells(0).Text = "S.No."
            Gv1.HeaderRow.Cells(1).Text = "Client & Project <br/>TicketNo. ,  Date"
            'Gv1.HeaderRow.Cells(2).Text = "Client"
            'Gv1.HeaderRow.Cells(3).Text = "Project"
            'Gv1.HeaderRow.Cells(4).Text = "Description"
            'Gv1.HeaderRow.Cells(5).Text = "Assign_Date"
            'Gv1.HeaderRow.Cells(6).Text = "AssignTo"
            'Gv1.HeaderRow.Cells(7).Text = "Status"
            'Gv1.HeaderRow.Cells(8).Text = "Com._Date"



            If empstatus = "Pending" Then
                'Gv1.Columns(5).Visible = False
                'Gv1.Columns(6).Visible = False
                'Gv1.Columns(7).Visible = False
                ' Gv1.Columns(8).Visible = False
                Gv1.Columns(9).Visible = False
                Gv1.Columns(10).Visible = False

            Else
                ' Gv1.Columns(5).Visible = True
                ' Gv1.Columns(6).Visible = True
                Gv1.Columns(7).Visible = True
                'Gv1.Columns(8).Visible = False
                'Gv1.Rows(lblempstatus.Text = "in progress").BackColor = Drawing.Color.Green



            End If


            pnlform.Visible = False
            pnldata.Visible = True

            If client <> "" Then
                lblclient.Text = client
            Else
                lblclient.Text = "All"
            End If

            If emp1 <> "" Then
                lblemp.Text = emp1
            Else
                lblemp.Text = "All"
            End If

            If status <> "" Then
                lblstatus.Text = status
            Else
                lblstatus.Text = "All"
            End If

            If empstatus <> "" Then
                lblempstatus.Text = empstatus
            Else
                lblempstatus.Text = "All"
            End If



        Else

            Gv1.DataSource = Nothing
            Gv1.DataBind()

            pnlform.Visible = True
            pnldata.Visible = False

        End If

        ds.Dispose()


    End Sub

    Private Sub show3()
        Dim client As String = ""
        Dim emp1 As String = ""
        Dim status As String = ""
        Dim empstatus As String = ""
        db.qry = "select (select case when CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)=0 then companyname else SUBSTRING(companyname, 0, CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)) end from clientmaster where companyid=(select client from ticket where tid=t.tid)) as client,(select dirname from director where ids=t.assignto) as assignto,(select dirname from director where ids=t.assignby) as assignby,t.* from ticketmaster as t,ticket as tkt where t.tid=tkt.tid"
        If Request.QueryString("s2") = "" Then

            db.qry = db.qry & " and t.empstatus<>'Completed'"
        Else
            db.qry = db.qry & " and t.empstatus='" & Request.QueryString("s2") & "'"
            empstatus = Request.QueryString("s2")
        End If
        db.qry = db.qry & " and tkt.client=" & Request.QueryString("com") & ""
        If Request.QueryString("s2") = "" Then
            ' db.qry = db.qry & " order by t.empstatus"
            db.qry = db.qry & "order by t.flag desc"


        Else
            'db.qry = db.qry & " order by tkt.client"
            db.qry = db.qry & "order by t.flag desc"

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
            'Gv1.HeaderRow.Cells(0).Text = "S.No."
            Gv1.HeaderRow.Cells(1).Text = "Client & Project <br/>TicketNo. ,  Date"
            'Gv1.HeaderRow.Cells(2).Text = "Client"
            'Gv1.HeaderRow.Cells(3).Text = "Project"
            'Gv1.HeaderRow.Cells(4).Text = "Description"
            'Gv1.HeaderRow.Cells(5).Text = "Assign_Date"
            'Gv1.HeaderRow.Cells(6).Text = "AssignTo"
            'Gv1.HeaderRow.Cells(7).Text = "Status"
            'Gv1.HeaderRow.Cells(8).Text = "Com._Date"



            If status = "Pending" Then
                Gv1.Columns(5).Visible = False
                Gv1.Columns(6).Visible = False
                Gv1.Columns(7).Visible = False
                'Gv1.Columns(8).Visible = False
            Else
                ' Gv1.Columns(5).Visible = True
                ' Gv1.Columns(6).Visible = True
                Gv1.Columns(7).Visible = True
                'Gv1.Columns(8).Visible = False

            End If


            pnlform.Visible = False
            pnldata.Visible = True

            If client <> "" Then
                lblclient.Text = client
            Else

                lblclient.Text = "All"


            End If

            If emp1 <> "" Then
                lblemp.Text = emp1
            Else
                lblemp.Text = "All"
            End If

            If status <> "" Then
                lblstatus.Text = status
            Else
                lblstatus.Text = "Assigned"
            End If

            If empstatus <> "" Then
                lblempstatus.Text = empstatus
            Else
                lblempstatus.Text = "All"
            End If



        Else

            Gv1.DataSource = Nothing
            Gv1.DataBind()

            pnlform.Visible = True
            pnldata.Visible = False

        End If

        ds.Dispose()
    End Sub


    Protected Sub drpdivision_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpdivision.SelectedIndexChanged
        db.fillCombo(dremp, "director", "dirname +' '+ empmname + ' '+ emplname ", "str(Ids)", " where division=" & drpdivision.SelectedValue & " order by dirname", , "OverAll")

    End Sub



End Class


