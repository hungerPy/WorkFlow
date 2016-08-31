Imports System.Web.Mail
Imports System.Net
Imports System.Net.Mail
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.IO
Imports System.Net.WebClient
Imports System.Data.SqlClient

Partial Class TicketSummary
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String
    Dim ds As New DataSet
    Dim p1, p2, p3, p4, p5, p6, p7 As String
    Dim t1, t2, t3, t4, t5, t6, t7, t8, t9, n1 As Double

    Dim tt1 As Double = 0
    Dim tt2 As Double = 0
    Dim tt3 As Double = 0
    Dim tt4 As Double = 0
    Dim tt5 As Double = 0
    Dim tt6 As Double = 0
    Dim tt7 As Double = 0
    Dim tt8 As Double = 0
    Dim tt9 As Double = 0
    Dim nn1 As Double = 0


    Dim hours As Double = 0
    Dim day As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        'db.setLayout(Session("userId"))
        'End If
        If Not IsPostBack Then
            lbldate.Text = DateTime.Today.ToString("dd-MMM-yyyy")
            lblday.Text = DateTime.Now.DayOfWeek.ToString()
            lblprintdate.Text = DateTime.Today.ToString("dd-MMM-yyyy") & " " & DateTime.Now.DayOfWeek.ToString()
            'Label2.Text = DateTime.Today.ToString("dd-MMM-yyyy")
            'Label1.Text = DateTime.Now.DayOfWeek.ToString()
            show()
            show1()
            show3()
            show4()
        End If

    End Sub

    Private Sub show4()
        db.qry = "select (select case when CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)=0 then companyname else SUBSTRING(companyname, 0, CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)) end from clientmaster where companyid=(select client from ticket where tid=t.tid)) as Client,t.tid ,t.adt1,t.project ,t.description ,t.empstatus ,t.adt2,t.starttime,t.totaltime ,t.timeduration  ,(select dirname from director where ids=t.assignto) as Developer   from ticketmaster as t,ticket as tkt where t.tid=tkt.tid and t.empstatus='In Progress'  and t.starttime <>'' and stoptime='' order by adt2 desc"
        ds = db.fillDataSet()
        If ds.Tables(0).Rows.Count > 0 Then


            gv1process.DataSource() = ds
            gv1process.DataBind()
        Else
            gv1process.DataSource() = Nothing
            gv1process.DataBind()
        End If

        'gv1process.HeaderRow.Cells(0).Text = "S.No."
        'gv1process.HeaderRow.Cells(1).Text = "Tkt._No."
        'gv1process.HeaderRow.Cells(2).Text = "Client"
        'gv1process.HeaderRow.Cells(3).Text = "Project"
        'gv1process.HeaderRow.Cells(4).Text = "Description"
        'gv1process.HeaderRow.Cells(5).Text = "Assign_Date"
        'gv1process.HeaderRow.Cells(6).Text = "Status"
        'gv1process.HeaderRow.Cells(7).Text = "Time in Minutes"
        'gv1process.HeaderRow.Cells(8).Text = "Developer"

        'dr = db.fillReader()
        'If dr.hasrows() Then
        '    gv1process.DataSource = dr

        '    gv1process.DataBind()
        'Else
        '    gv1process.DataSource = Nothing
        '    gv1process.DataBind()
        'End If



        dr.Close()
    End Sub

    Private Sub show()

        Dim client As String = ""
        Dim emp1 As String = ""
        Dim status As String = ""
        Dim empstatus As String = ""
        Dim q1 As String = ""
        Dim q2 As String = ""
        lblcompname.Text = Session("companyname")
        lblcompanyname.Text = Session("companyname")


        Dim s5 As String = "1=1"

        strqry1 = "select top 1 (select count(*) from ticket where status<>'Completed' and tid in(select tid from ticketmaster where " & s5 & " " & q1 & " " & q2 & " group by tid)) as ticpending,(select count(*) from ticketmaster where " & s5 & " " & q1 & " " & q2 & " and empstatus='Pending') as pending,(select count(*) from ticketmaster where " & s5 & " " & q1 & " " & q2 & " and status='Pending') as Notassigned,(select count(*) from ticketmaster where " & s5 & " " & q1 & " " & q2 & " and empstatus='Speak') as speak,(select count(*) from ticketmaster where " & s5 & " " & q1 & " " & q2 & " and empstatus='Bug') as Bug,(select count(*) from ticketmaster where " & s5 & " " & q1 & " " & q2 & " and empstatus='In Progress') as InProgress,(select count(*) from ticketmaster where " & s5 & " " & q1 & " " & q2 & " and empstatus='Testing') as testing,(select count(*) from ticketmaster where " & s5 & " " & q1 & " " & q2 & " and empstatus='Hold by Client') as hold1,(select count(*) from ticketmaster where " & s5 & " " & q1 & " " & q2 & " and empstatus='Hold by Kamtech') as hold2 from ticketmaster"

        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()


        If dr.HasRows Then
            gvcompanystatus.DataSource = dr
            gvcompanystatus.DataBind()
            'While dr.read()

            '    Lblticket.Text = "<a href='ticketdetailreport.aspx?s1=NotCompleted'>" & dr("ticpending") & "</a>"
            '    lblpending.Text = "<a href='ticketdetailreport.aspx?s1=Pending'>" & dr("pending") & "</a>"
            '    lblnotassigned.Text = "<a href='ticketdetailreport.aspx?s1=NotAssigned'>" & dr("notassigned") & "</a>"
            '    n1 = dr("notassigned")
            '    lblspeak.Text = "<a href='ticketdetailreport.aspx?s1=Speak'>" & dr("speak") & "</a>"
            '    lblbug.Text = "<a href='ticketdetailreport.aspx?s1=Bug'>" & dr("bug") & "</a>"
            '    lblprogress.Text = "<a href='ticketdetailreport.aspx?s1=In Progress'>" & dr("inprogress") & "</a>"
            '    lbltesting.Text = "<a href='ticketdetailreport.aspx?s1=Testing'>" & dr("testing") & "</a>"
            '    lblhold.Text = "<a href='ticketdetailreport.aspx?s1=Hold by Client'>" & dr("hold1") & "</a>"
            '    lblhold1.Text = "<a href='ticketdetailreport.aspx?s1=Hold by Kamtech'>" & dr("hold2") & "</a>"
            '    lbltotal.Text = "<a href='ticketdetailreport.aspx?s1=NotCompleted'>" & CInt(CInt(dr("pending")) + CInt(dr("notassigned")) + CInt(dr("speak")) + CInt(dr("bug")) + CInt(dr("inprogress")) + CInt(dr("testing")) + CInt(dr("hold1")) + CInt(dr("hold2"))) & "</a>"

            '    btnmail.Visible = True

            'End While
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()

    End Sub

    Private Sub show3()
        strqry1 = "select Companyname,companyId,(select count(*) from ticketmaster where (empstatus='Pending' or empstatus='')  and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as pending,(select count(*) from ticketmaster where empstatus='Speak' and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as speak,(select count(*) from ticketmaster where empstatus='Bug' and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as Bug,(select count(*) from ticketmaster where empstatus='In Progress' and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as InProgress,(select count(*) from ticketmaster where empstatus='Testing' and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as testing,(select count(*) from ticketmaster where empstatus='Hold by Client' and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as hold1,(select count(*) from ticketmaster where empstatus='Hold by Kamtech' and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as hold2,(select count(*) from ticketmaster where empstatus='Completed' and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as completed from clientmaster as d where companyId in(select client from ticket group by client) order by Pending desc,InProgress desc,Bug desc,Speak desc,Testing desc,hold1 desc,hold2 desc,completed desc,companyname"


        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()

        If dr.HasRows() Then
            Gvcmp.DataSource = dr
            Gvcmp.DataBind()
        Else
            Gvcmp.DataSource = Nothing
            Gvcmp.DataBind()
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Private Sub show1()
        strqry1 = "select Dirname,ids,(select count(*) from ticketmaster where (empstatus in('Pending','in progress') or empstatus='')  and assignto=d.ids) as Pending_InProgress,(select count(*) from ticketmaster where empstatus='Speak' and assignto=d.ids) as Speak,(select count(*) from ticketmaster where empstatus='Bug' and assignto=d.ids) as Bug,(select count(*) from ticketmaster where empstatus='Testing'  and assignto=d.ids) as Testing,(select count(*) from ticketmaster where empstatus='Hold by Client' and assignto=d.ids) as Hold1,(select count(*) from ticketmaster where empstatus='Hold by Kamtech'  and assignto=d.ids) as Hold2,(select count(*) from ticketmaster where empstatus='Completed' and assignto=d.ids) as Completed from director as d where ids in(select assignto from ticketmaster group by assignto) and flg<>1 order by dirname"

        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()

        If dr.HasRows() Then
            gvemp.DataSource = dr
            gvemp.DataBind()
        Else
            gvemp.DataSource = Nothing
            gvemp.DataBind()

        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Protected Sub btnmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmail.Click

        sendmail()

    End Sub


    Protected Sub gvemp_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvemp.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Cells(0).Text = dr("dirname")

            e.Row.Cells(1).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & " &s1=Pending &s2=in progress'>" & dr("Pending_InProgress") & "</a>"

            e.Row.Cells(2).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & "&s1=Bug'>" & dr("bug") & "</a>"

            e.Row.Cells(3).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & "&s1=Speak'>" & dr("speak") & "</a>"

            e.Row.Cells(4).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & "&s1=Testing'>" & dr("testing") & "</a>"

            e.Row.Cells(5).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & "&s1=Hold by Client'>" & dr("hold1") & "</a>"

            ' e.Row.Cells(6).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & "&s1=Hold by Kamtech'>" & dr("hold2") & "</a>"


            e.Row.Cells(7).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & "&s1=completed'>" & dr("completed") & "</a>"

            e.Row.Cells(8).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & "'>" & CInt(CInt(dr("Pending_InProgress")) + CInt(dr("bug")) + CInt(dr("speak")) + CInt(dr("testing")) + CInt(dr("hold1")) + CInt(dr("hold2"))) + CInt(dr("completed")) & "</a>"

            t1 = t1 + CInt(dr("Pending_InProgress"))

            t2 = t2 + CInt(dr("bug"))

            t3 = t3 + CInt(dr("speak"))

            t4 = t4 + CInt(dr("testing"))

            t5 = t5 + CInt(dr("hold1"))

            't6 = t6 + CInt(dr("hold2"))

            t7 = t7 + CInt(dr("completed"))

            't8 = t8 + CInt(CInt(dr("Pending_InProgress")) + CInt(dr("bug")) + CInt(dr("speak")) + CInt(dr("testing")) + CInt(dr("hold1")) + CInt(dr("hold2")) + CInt(dr("completed")))
            t8 = t8 + CInt(CInt(dr("Pending_InProgress")) + CInt(dr("bug")) + CInt(dr("speak")) + CInt(dr("testing")) + CInt(dr("hold1")) + CInt(dr("completed")))



        End If

        If e.Row.RowType = DataControlRowType.Footer Then

            e.Row.Cells(0).Text = "Total" & "<br/>" & "<br/>"

            e.Row.Cells(1).Text = t1 & "<br/>" & "<br/>"

            e.Row.Cells(2).Text = t2 & "<br/>" & "<br/>"

            e.Row.Cells(3).Text = t3 & "<br/>" & "<br/>"

            e.Row.Cells(4).Text = t4 & "<br/>" & "<br/>"

            e.Row.Cells(5).Text = t5 & "<br/>" & "<br/>"

            e.Row.Cells(6).Text = t6 & "<br/>" & "<br/>"

            e.Row.Cells(7).Text = t7
            'e.Row.Cells(7).Text = e.Row.Cells(8).Text & "<br/><hr>Not Assigned</hr><br/><hr>Total Points</hr>"

            e.Row.Cells(8).Text = t8

            'e.Row.Cells(8).Text = e.Row.Cells(8).Text & "<br/><hr>" & n1 & "</hr><br/><hr>" & t8 + n1 & "</hr>"
        End If
    End Sub

    Protected Sub gvemp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvemp.SelectedIndexChanged

    End Sub

    Private Sub sendmail()


        Dim emailto As String = "office@kamtechassociates.com"

        Dim client As New System.Net.Mail.SmtpClient("69.175.99.245", "25")

        client.Credentials = New NetworkCredential("mail@kamtechassociates.com", "kapl#100")


        If emailto <> "" Then

            Dim message As New System.Net.Mail.MailMessage("mail@kamtechassociates.com", emailto)


            Dim SB As New StringBuilder()
            Dim SW As New StringWriter(SB)
            Dim htmlTW As New HtmlTextWriter(SW)

            pnldata.RenderControl(htmlTW)



            ' Get the HTML into a string.
            ' This will be used in the body of the email report.
            '---------------------------------------------------
            Dim dataGridHTML As String = SB.ToString()

            Dim body As String = ""

            body = dataGridHTML

            body = body & "<br/>For More Details Click Here : " & "http://www.sakshamerp.com/mis/default.aspx"

            body = body & "&nbsp;<br/><br/><br/> Regards! <br/><br/>Technical Support Team (IT)<br/>Kamtech Associates Pvt. Ltd.<br/>(India~Kenya~Tanjania~U.K.)<br/><br/> G5, Gajraj Apartment, Sarojini Marg, C-Scheme, Jaipur-302001.<br/>Rajasthan, India.<br/><br/>Ph:  +91-141-2377559, 2373226, 2371308, Fax +91-141-4041885<br/>Mobile - +91-9828042461<br/>Web - www.realcrm.in, www.propertyjunction.in, www.kamtech.in<br/>E-mail - office@kamtech.in, ajain@kamtech.in, mail@kamtechassociates.com<br/><br/>* Recipient of MSME-IT Excellence Gold Award 2011<br/>* Recipient of National Award 2009 (First Prize)by President of India <br/>* National Award 2008 (Special Recognition Prize) in Service Sector for Outstanding Performance by  Government of India.<br/><br/><b>Strategic support to your business.</b><br/><br/><font face='Franklin Gothic Heavy' size='3'> Kamtech - Saksham MIS generated report.</font>"

            message.Body = body

            message.BodyEncoding = System.Text.Encoding.UTF8
            message.IsBodyHtml = True

            message.Subject = "Ticket Work Status"

            message.SubjectEncoding = System.Text.Encoding.UTF8

            Try
                client.Send(message)

            Catch ex As SmtpException
                Response.Write("<pre>" & ex.ToString() & "</pre>")
            Finally
                ' Clean up.
                message.Dispose()
            End Try

        End If

        show()
        show1()

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub


    Protected Sub Gvcmp_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gvcmp.RowDataBound
        Dim p As String = "0"
        't1 = 0
        't2 = 0
        't3 = 0
        't4 = 0
        't5 = 0
        't6 = 0
        't7 = 0
        't8 = 0
        't9 = 0

        If e.Row.RowType = DataControlRowType.DataRow Then
            p1 = dr("Pending")
            p2 = dr("inprogress")
            p3 = dr("bug")
            p4 = dr("Speak")
            p5 = dr("testing")
            p6 = dr("hold1")
            p7 = dr("hold2")
            e.Row.Cells(2).Text = p1
            e.Row.Cells(3).Text = p2
            e.Row.Cells(4).Text = p3
            e.Row.Cells(5).Text = p4
            e.Row.Cells(6).Text = p5
            e.Row.Cells(7).Text = p6
            ' e.Row.Cells(8).Text = p7
            If e.Row.Cells(2).Text <> p And e.Row.Cells(3).Text <> p And e.Row.Cells(4).Text <> p And e.Row.Cells(5).Text <> p And e.Row.Cells(6).Text <> p And e.Row.Cells(7).Text <> p And e.Row.Cells(8).Text <> p Then
                e.Row.Visible = True
            ElseIf e.Row.Cells(2).Text = p And e.Row.Cells(3).Text = p And e.Row.Cells(4).Text = p And e.Row.Cells(5).Text = p And e.Row.Cells(6).Text = p And e.Row.Cells(7).Text = p And e.Row.Cells(8).Text = p Then
                e.Row.Visible = False
            End If
            'e.Row.Cells(0).Text = (Gvcmp.PageIndex * Gvcmp.PageSize) + (e.Row.RowIndex + 1)
            e.Row.Cells(1).Text = dr("Companyname")
            e.Row.Cells(2).Text = "<a href='ticketdetailreport.aspx?com=" & dr("companyId") & "&s2=Pending'>" & dr("Pending") & "</a>"

            e.Row.Cells(3).Text = "<a href='ticketdetailreport.aspx?com=" & dr("companyId") & "&s2=In Progress'>" & dr("inprogress") & "</a>"


            e.Row.Cells(4).Text = "<a href='ticketdetailreport.aspx?com=" & dr("companyId") & "&s2=bug'>" & dr("bug") & "</a>"

            e.Row.Cells(5).Text = "<a href='ticketdetailreport.aspx?com=" & dr("companyId") & "&s2=Speak'>" & dr("Speak") & "</a>"

            e.Row.Cells(6).Text = "<a href='ticketdetailreport.aspx?com=" & dr("companyId") & "&s2=testing'>" & dr("testing") & "</a>"


            e.Row.Cells(7).Text = "<a href='ticketdetailreport.aspx?com=" & dr("companyId") & "&s2=hold1'>" & dr("hold1") & "</a>"


            e.Row.Cells(8).Text = "<a href='ticketdetailreport.aspx?com=" & dr("companyId") & "&s2=hold2'>" & dr("hold2") & "</a>"


            e.Row.Cells(9).Text = "<a href='ticketdetailreport.aspx?com=" & dr("companyId") & "&s2=completed'>" & dr("completed") & "</a>"


            'e.Row.Cells(10).Text = CInt(CInt(dr("Pending")) + CInt(dr("InProgress")) + CInt(dr("bug")) + CInt(dr("speak")) + CInt(dr("testing")) + CInt(dr("Hold1")) + CInt(dr("Hold2")) + CInt(dr("completed")))
            e.Row.Cells(10).Text = CInt(CInt(dr("Pending")) + CInt(dr("InProgress")) + CInt(dr("bug")) + CInt(dr("speak")) + CInt(dr("testing")) + CInt(dr("Hold1")) + CInt(dr("completed")))



            'If e.Row.Cells(2).Text <> p And e.Row.Cells(3).Text <> p And e.Row.Cells(4).Text <> p And e.Row.Cells(5).Text <> p And e.Row.Cells(6).Text <> p And e.Row.Cells(7).Text <> p And e.Row.Cells(8).Text <> p Then
            '    e.Row.Visible = True
            'Else
            '    e.Row.Visible = False
            'End If


            tt1 = tt1 + CInt(dr("Pending"))
            tt2 = tt2 + CInt(dr("InProgress"))
            tt3 = tt3 + CInt(dr("bug"))
            tt4 = tt4 + CInt(dr("speak"))
            tt5 = tt5 + CInt(dr("testing"))
            tt6 = tt6 + CInt(dr("hold1"))
            tt7 = tt7 + CInt(dr("hold2"))
            tt8 = tt8 + CInt(dr("completed"))

            t9 = t9 + CInt(CInt(dr("Pending")) + CInt(dr("InProgress")) + CInt(dr("bug")) + CInt(dr("speak")) + CInt(dr("testing")) + CInt(dr("hold1")) + CInt(dr("hold2")) + CInt(dr("completed")))


        End If



        If e.Row.RowType = DataControlRowType.Footer Then

            e.Row.Cells(1).Text = "Total" & "<br/>" & "<br/>"

            e.Row.Cells(2).Text = tt1 & "<br/>" & "<br/>"
            e.Row.Cells(3).Text = tt2 & "<br/>" & "<br/>"
            e.Row.Cells(4).Text = tt3 & "<br/>" & "<br/>"
            e.Row.Cells(5).Text = tt4 & "<br/>" & "<br/>"
            e.Row.Cells(6).Text = tt5 & "<br/>" & "<br/>"
            e.Row.Cells(7).Text = tt6 & "<br/>" & "<br/>"
            e.Row.Cells(8).Text = tt7
            e.Row.Cells(9).Text = tt8

            e.Row.Cells(10).Text = tt1 + tt2 + tt3 + tt4 + tt5 + tt6 + tt7 + tt8


        End If

    End Sub

    Protected Sub gv1process_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv1process.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = (gv1process.PageIndex * gv1process.PageSize) + (e.Row.RowIndex + 1)
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

    End Sub

    Protected Sub gv1process_SelectedIndexChanged(sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gv1process.PageIndexChanging
        gv1process.PageIndex = e.NewPageIndex
        show4()
    End Sub

    Protected Sub gvcompanystatus_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvcompanystatus.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim Lblticket As Label = CType(e.Row.FindControl("Lblticket"), Label)
            Dim lblnotassigned As Label = CType(e.Row.FindControl("lblnotassigned"), Label)
            Dim lblpending As Label = CType(e.Row.FindControl("lblpending"), Label)
            Dim lblprogress As Label = CType(e.Row.FindControl("lblprogress"), Label)
            Dim lblbug As Label = CType(e.Row.FindControl("lblbug"), Label)
            Dim lblspeak As Label = CType(e.Row.FindControl("lblspeak"), Label)
            Dim lbltesting As Label = CType(e.Row.FindControl("lbltesting"), Label)
            Dim lblhold As Label = CType(e.Row.FindControl("lblhold"), Label)
            Dim lblhold1 As Label = CType(e.Row.FindControl("lblhold1"), Label)
            Dim lbltotal As Label = CType(e.Row.FindControl("lbltotal"), Label)

            Lblticket.Text = "<a href='ticketdetailreport.aspx?s1=NotCompleted'>" & dr("ticpending") & "</a>"
            lblpending.Text = "<a href='ticketdetailreport.aspx?s1=Pending'>" & dr("pending") & "</a>"
            lblnotassigned.Text = "<a href='ticketdetailreport.aspx?s1=NotAssigned'>" & dr("notassigned") & "</a>"
            n1 = dr("notassigned")
            lblspeak.Text = "<a href='ticketdetailreport.aspx?s1=Speak'>" & dr("speak") & "</a>"
            lblbug.Text = "<a href='ticketdetailreport.aspx?s1=Bug'>" & dr("bug") & "</a>"
            lblprogress.Text = "<a href='ticketdetailreport.aspx?s1=In Progress'>" & dr("inprogress") & "</a>"
            lbltesting.Text = "<a href='ticketdetailreport.aspx?s1=Testing'>" & dr("testing") & "</a>"
            lblhold.Text = "<a href='ticketdetailreport.aspx?s1=Hold by Client'>" & dr("hold1") & "</a>"
            lblhold1.Text = "<a href='ticketdetailreport.aspx?s1=Hold by Kamtech'>" & dr("hold2") & "</a>"
            ' lbltotal.Text = "<a href='ticketdetailreport.aspx?s1=NotCompleted'>" & CInt(CInt(dr("pending")) + CInt(dr("notassigned")) + CInt(dr("speak")) + CInt(dr("bug")) + CInt(dr("inprogress")) + CInt(dr("testing")) + CInt(dr("hold1")) + CInt(dr("hold2"))) & "</a>"
            lbltotal.Text = "<a href='ticketdetailreport.aspx?s1=NotCompleted'>" & CInt(CInt(dr("pending")) + CInt(dr("notassigned")) + CInt(dr("speak")) + CInt(dr("bug")) + CInt(dr("inprogress")) + CInt(dr("testing")) + CInt(dr("hold2"))) & "</a>"


            btnmail.Visible = True


        End If
    End Sub

    'Protected Sub imgbtninprocess_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtninprocess.Click

    '    If divgvcompanystatus.Visible = False Then
    '        divgvcompanystatus.Visible = True
    '    Else
    '        divgvcompanystatus.Visible = False

    '    End If

    'End Sub

    Protected Sub lnkdailyinprogresswork_Click(sender As Object, e As EventArgs)
        DailyInprogressWork.Visible = True
        WorkStatusSummary.Visible = False
        WorkStatusEmployeeWise.Visible = False
        CompanyWorkStatus.Visible = False
    End Sub

    Protected Sub lnkworkstatus_Click(sender As Object, e As EventArgs)
        WorkStatusSummary.Visible = True
        DailyInprogressWork.Visible = False
        WorkStatusEmployeeWise.Visible = False
        CompanyWorkStatus.Visible = False
    End Sub

    Protected Sub lnkworkstatusempwise_Click(sender As Object, e As EventArgs)
        WorkStatusEmployeeWise.Visible = True
        DailyInprogressWork.Visible = False
        WorkStatusSummary.Visible = False
        CompanyWorkStatus.Visible = False
    End Sub

    Protected Sub lnkcompanyworkstatus_Click(sender As Object, e As EventArgs)
        CompanyWorkStatus.Visible = True
        WorkStatusEmployeeWise.Visible = False
        DailyInprogressWork.Visible = False
        WorkStatusSummary.Visible = False
    End Sub

    Protected Sub inkshowallwork_Click(sender As Object, e As EventArgs)
        CompanyWorkStatus.Visible = True
        WorkStatusEmployeeWise.Visible = True
        DailyInprogressWork.Visible = True
        WorkStatusSummary.Visible = True
    End Sub

End Class
