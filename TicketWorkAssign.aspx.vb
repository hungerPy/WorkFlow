Imports System.Data
Imports System.Data.SqlClient
Imports System.Net
Imports System.IO
Imports System.Net.Mail

Partial Class TicketWorkAssign
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String
    Dim ds As New DataSet
    Dim t1, t2, t3, t4, t5, t6, t7, t8 As Double
    Dim aa As Integer
    Dim tktid As String
    Dim tktida As String
    Dim tktidb As String
    Dim tsid As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        'db.setLayout(Session("userId"))
        'End If
        If Not IsPostBack Then
            'db.fillCombo(drpclient, "ticket", "client", "client", " group by client order by client ")
            db.fillCombo(drpclient, "clientmaster", "companyname", "str(companyid)", " where companyid in(select client from ticket group by client) order by companyname", , "Over All")
            'db.fillCombo(dremp, "director", "dirname", "Ids", " where empno<>'0' and flg=0 order by dirname")
            db.fillCombo(dremp, "director", "dirname", "Ids", " where 1=2")
            db.fillCombo(drpdivision, "divisions", "dname", "divisionid", " where 1=1 order by dname ")
            drprecords.DataSource = db.noRecords
            drprecords.DataBind()
            drprecords.SelectedValue = "10"
            lbldate.Text = DateTime.Today.ToString("dd-MMM-yyyy")
            lblemaildate.Text = DateTime.Today.ToString("dd-MMM-yyyy")
            lblday.Text = DateTime.Now.DayOfWeek.ToString()
            drpclient.SelectedValue = "Over All"
            show()
            show1()
            show2()
        End If
        'btnassigned.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")
    End Sub
    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        show()
        show1()
        show2()
    End Sub

    Protected Sub Gv1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gv1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = (Gv1.PageIndex * Gv1.PageSize) + (e.Row.RowIndex + 1)

            'If ds.Tables(0).Rows.Count <> 0 Then
            '    aa += Convert.ToInt32(ds.Tables(0).Rows(0)("empid"))
            'End If

            'Dim name As String = db.getFieldValue("director", "empid", aa, "dirname")
            'Dim divisionid As String = db.getFieldValue("director", "empid", aa, "division")
            'Dim deptname As String = db.getFieldValue("divisions", "divisionid", divisionid, "dname")

            'e.Row.Cells(9).Text = name & "<br/>" & deptname
        End If
    End Sub

    Protected Sub btnassigned_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnassigned.Click
        Dim time As TextBox
        Dim targetdate As TextBox
        Dim priority As TextBox
        Dim i As Integer
        If dremp.SelectedValue <> "0" Then
            For i = 0 To Gv1.Rows.Count - 1
                time = CType(Gv1.Rows(i).Cells(6).FindControl("txtduration"), TextBox)
                targetdate = CType(Gv1.Rows(i).Cells(7).FindControl("txttargetdate"), TextBox)
                priority = CType(Gv1.Rows(i).Cells(7).FindControl("txtremark"), TextBox)

                Dim ck As CheckBox = CType(Gv1.Rows(i).Cells(10).FindControl("CheckBox1"), CheckBox)
                If ck.Checked = True Then
                    Dim hdn As HiddenField = CType(Gv1.Rows(i).Cells(10).FindControl("HiddenField1"), HiddenField)

                    If CStr(hdn.Value) <> "" And time.Text <> "" Then

                        tktida = hdn.Value
                        tktid = tktid & "," & tktida
                        db.qry = "update ticketmaster set empstatus = 'Pending',status = 'Assigned' ,premark='" & priority.Text & "' ,assignby=" & Session("empid") & " ,assignto=" & dremp.SelectedValue & " ,timeduration=" & time.Text & ",adt2='" & Now.Date.ToString("dd-MMM-yyyy") & "',empid=" & dremp.SelectedValue & ",targetdate='" & targetdate.Text & "' where tsid=" & CInt(hdn.Value) & ""
                        db.executeQuery()
                    End If
                End If
            Next

            'Next
            Dim ticketids As String = tktid.TrimStart(",")
            strqry1 = "select (select case when CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)=0 then companyname else SUBSTRING(companyname, 0, CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)) end from clientmaster where companyid=(select client from ticket where tid=t.tid)) client,t.*,dr.*,divi.* from ticketmaster as t,ticket as tkt,Director as dr,divisions as divi where t.tid=tkt.tid and dr.division =divi.DivisionId  and t.tsid in(" & ticketids & ") and  t.empid=dr.empId and t.empstatus='Pending' and t.status='Assigned' order by t.tid"
            'db.qry = "select * from ticketmaster as tm, ticket as t where t.tid=tm.tid tsid in(" & ticketids & ")"

            dt1 = db.fillReader1(strqry1)
            dr1 = dt1.CreateDataReader()
            'If dr1.read() Then
            gvdummy.DataSource = dr1
            gvdummy.DataBind()
            'End If

            sendmail()

            show()
            show1()
            show2()
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
        show()
    End Sub

    Private Sub sendmail()

        pnlhead.Visible = True
        pnlfooter.Visible = True
        'P4.Visible = True
        Dim empfname As String = db.getFieldValue("director", "ids", dremp.SelectedValue, "dirname")
        Dim empmname As String = db.getFieldValue("director", "ids", dremp.SelectedValue, "empmname")
        lblempname.Text = empfname & "&nbsp;" & empmname
        Dim divisionid As Integer = db.getFieldValue("director", "ids", dremp.SelectedValue, "division")
        lblempdesignation.Text = db.getFieldValue("divisions", "divisionid", divisionid, "dname")

        lblcompanyname.Text = Session("companyname")

        lblauthorityname.Text = db.getFieldValue("director", "ids", Session("empid"), "dirname") & "&nbsp;" & db.getFieldValue("director", "ids", Session("empid"), "empmname")

        Dim autherdivid As String = db.getFieldValue("director", "ids", Session("empid"), "division")
        lblauthoritydesignation.Text = db.getFieldValue("divisions", "divisionid", autherdivid, "dname")
        lblcompname.Text = Session("companyname")
        Gv1.AllowPaging = False
        Gv1.AllowSorting = False
        Dim i As Integer

        ' Dim emailto As String = "ajain@kamtechassociates.com,office@kamtechassociates.com"
        Dim mailid As String = db.getFieldValue("director", "ids", dremp.SelectedValue, "compmailid")

        Dim emailto As String = mailid

        Dim client As New System.Net.Mail.SmtpClient("69.175.99.245", "25")

        client.Credentials = New NetworkCredential("ta@kamtech.in", "kapl#100")

        If emailto <> "" And emailto <> "N/A" Then

            'Dim emailto As String = txtemail.Text //info@ashapurna.com
            'Dim message As New System.Net.Mail.MailMessage(txtEmail.Text, emailto)
            Dim mailfrom As String = db.getFieldValue("director", "ids", Session("empid"), "compmailid")

            Dim message As New System.Net.Mail.MailMessage(mailfrom, emailto)

            'Dim message As New System.Net.Mail.MailMessage("mail@kamtechassociates.com", emailto)

            'gvdummy.Columns(5).Visible = False
            'gvdummy.Columns(6).Visible = False
            'gvdummy.Columns(7).Visible = False
            'gvdummy.Columns(9).Visible = False



            Dim SB As New StringBuilder()
            Dim SW As New StringWriter(SB)
            Dim htmlTW As New HtmlTextWriter(SW)

            pnlhead.RenderControl(htmlTW)
            gvdummy.RenderControl(htmlTW)
            pnlfooter.RenderControl(htmlTW)
            'rows.RenderControl(htmlTW)
            'P2.RenderControl(htmlTW)

            ' Get the HTML into a string.
            ' This will be used in the body of the email report.
            '---------------------------------------------------
            Dim dataGridHTML As String = SB.ToString()
            Dim body As String = dataGridHTML

            message.Body = body

            ' Gv1.Columns(3).Visible = True

            message.BodyEncoding = System.Text.Encoding.UTF8
            message.IsBodyHtml = True

            'message.Subject = getFieldValue("clientmaster", "companyid", db.cid, "companyname", True) & " Ticket"


            If i = 1 Then

                message.SubjectEncoding = System.Text.Encoding.UTF8

                Try
                    client.Send(message)

                Catch ex As SmtpException
                    Response.Write("<pre>" & ex.ToString() & "</pre>")
                Finally
                    ' Clean up.
                    message.Dispose()
                End Try


            Else
                message.SubjectEncoding = System.Text.Encoding.UTF8
                ' send message

                Try
                    client.Send(message)

                Catch ex As SmtpException
                    Response.Write("<pre>" & ex.ToString() & "</pre>")
                Finally
                    ' Clean up.
                    message.Dispose()
                End Try
            End If
        End If
        pnlhead.Visible = False
        pnlfooter.Visible = False

        'gvdummy.Columns(5).Visible = True
        'gvdummy.Columns(6).Visible = True
        'gvdummy.Columns(7).Visible = True
        'gvdummy.Columns(9).Visible = True


    End Sub
    'Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    '    ' Verifies that the control is rendered
    'End Sub
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)

    End Sub
    Protected Sub Gv1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gv1.PageIndexChanging
        Gv1.PageIndex = e.NewPageIndex
        show()
    End Sub

    Protected Sub btnremark_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnremark.Click
        Dim i As Integer
        For i = 0 To Gv1.Rows.Count - 1
            Dim ck As CheckBox = CType(Gv1.Rows(i).Cells(7).FindControl("CheckBox1"), CheckBox)
            Dim t1 As TextBox = CType(Gv1.Rows(i).Cells(7).FindControl("txtremark"), TextBox)
            If ck.Checked = True Then
                Dim hdn As HiddenField = CType(Gv1.Rows(i).Cells(7).FindControl("HiddenField1"), HiddenField)
                If CStr(hdn.Value) <> "" And t1.Text <> "" Then
                    db.qry = "update ticketmaster set premark='" & t1.Text & "' where tsid=" & CInt(hdn.Value) & ""
                    db.executeQuery()
                End If
            End If
        Next
        show()
    End Sub

    Private Sub show()
        db.qry = "select (select case when CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)=0 then companyname else SUBSTRING(companyname, 0, CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)) end from clientmaster where companyid=(select client from ticket where tid=t.tid)) client,t.*,dr.*,divi.* from ticketmaster as t,ticket as tkt,Director as dr,divisions as divi where t.tid=tkt.tid and dr.division =divi.DivisionId  and  t.empid=dr.Ids and t.status='Pending'"
        If drpclient.SelectedValue <> "Over All" Then
            db.qry = db.qry & " and t.tid in(select tid from ticket where client=" & drpclient.SelectedValue & ")"
        End If
        If drporderby.SelectedValue = "1" Then
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

            Gv1.DataSource = ds
            Gv1.DataBind()
            Gv1.HeaderRow.Cells(0).Text = "S.No."
            Gv1.HeaderRow.Cells(0).Font.Size = 10

            Gv1.HeaderRow.Cells(1).Text = "Ticket No."
            Gv1.HeaderRow.Cells(1).Font.Size = 10

            Gv1.HeaderRow.Cells(2).Text = "Client"
            Gv1.HeaderRow.Cells(2).Font.Size = 10

            Gv1.HeaderRow.Cells(3).Text = "Project"
            Gv1.HeaderRow.Cells(3).Font.Size = 10

            Gv1.HeaderRow.Cells(4).Text = "Description"
            Gv1.HeaderRow.Cells(4).Font.Size = 10

            Gv1.HeaderRow.Cells(5).Text = "Client_Remark"
            Gv1.HeaderRow.Cells(5).Font.Size = 10

            Gv1.HeaderRow.Cells(6).Text = "Assign Time <br/>(In Days)"
            Gv1.HeaderRow.Cells(6).Font.Size = 10

            Gv1.HeaderRow.Cells(7).Text = "Target <br/>Date"
            Gv1.HeaderRow.Cells(7).Font.Size = 10

            Gv1.HeaderRow.Cells(8).Text = "Priority"
            Gv1.HeaderRow.Cells(8).Font.Size = 10
            Gv1.HeaderRow.Cells(9).Font.Size = 9

            'Gv1.HeaderRow.Cells(8).Text = "Assign/Remark"
        Else
            Gv1.DataSource = Nothing
            Gv1.DataBind()
        End If
        ds.Dispose()

    End Sub

    Private Sub show1()
        strqry1 = "select dirname,ids,(select count(*) from ticketmaster where (empstatus='Pending' or empstatus='')  and assignto=d.ids) as pending,(select count(*) from ticketmaster where empstatus='Speak' and assignto=d.ids) as speak,(select count(*) from ticketmaster where empstatus='Bug' and assignto=d.ids) as Bug,(select count(*) from ticketmaster where empstatus='In Progress' and assignto=d.ids) as InProgress,(select count(*) from ticketmaster where empstatus='Testing'  and assignto=d.ids) as testing,(select count(*) from ticketmaster where empstatus='Hold by Client' and assignto=d.ids) as hold1,(select count(*) from ticketmaster where empstatus='Hold by Kamtech'  and assignto=d.ids) as hold2,(select count(*) from ticketmaster where empstatus='Completed' and assignto=d.ids) as completed from director as d where ids in(select assignto from ticketmaster group by assignto) and flg<>1 order by completed desc,dirname"
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        If dr.HasRows() Then
            gvemp.DataSource = dr
            gvemp.DataBind()

            gvemp.HeaderRow.Cells(0).Font.Size = 10
            gvemp.HeaderRow.Cells(1).Font.Size = 10
            gvemp.HeaderRow.Cells(2).Font.Size = 10
            gvemp.HeaderRow.Cells(3).Font.Size = 10
            gvemp.HeaderRow.Cells(4).Font.Size = 10
            gvemp.HeaderRow.Cells(5).Font.Size = 10
            gvemp.HeaderRow.Cells(6).Font.Size = 10
            gvemp.HeaderRow.Cells(7).Font.Size = 10
            gvemp.HeaderRow.Cells(8).Font.Size = 10
        Else
            gvemp.DataSource = Nothing
            gvemp.DataBind()

        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Private Sub show2()
        strqry1 = "select Companyname,companyId,(select count(*) from ticketmaster where (empstatus='Pending' or empstatus='')  and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as pending,(select count(*) from ticketmaster where empstatus='Speak' and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as speak,(select count(*) from ticketmaster where empstatus='Bug' and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as Bug,(select count(*) from ticketmaster where empstatus='In Progress' and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as InProgress,(select count(*) from ticketmaster where empstatus='Testing' and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as testing,(select count(*) from ticketmaster where empstatus='Hold by Client' and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as hold1,(select count(*) from ticketmaster where empstatus='Hold by Kamtech' and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as hold2,(select count(*) from ticketmaster where empstatus='Completed' and tsid in (select tsid from ticketmaster where tid in (select tid from ticket where client=d.companyid))) as completed from clientmaster as d where companyId in(select client from ticket group by client) order by Pending desc,InProgress desc,Bug desc,Speak desc,Testing desc,hold1 desc,hold2 desc,completed desc,companyname"
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        If dr.HasRows() Then
            gvcmp.DataSource = dr
            gvcmp.DataBind()
            gvcmp.HeaderRow.Cells(0).Font.Size = 10
            gvcmp.HeaderRow.Cells(1).Font.Size = 10
            gvcmp.HeaderRow.Cells(2).Font.Size = 10
            gvcmp.HeaderRow.Cells(3).Font.Size = 10
            gvcmp.HeaderRow.Cells(4).Font.Size = 10
            gvcmp.HeaderRow.Cells(5).Font.Size = 10
            gvcmp.HeaderRow.Cells(6).Font.Size = 10
            gvcmp.HeaderRow.Cells(7).Font.Size = 10
            gvcmp.HeaderRow.Cells(8).Font.Size = 10
        Else
            gvcmp.DataSource = Nothing
            gvcmp.DataBind()
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Protected Sub gvemp_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvemp.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & "&s1=Pending'>" & dr("Pending") & "</a>"
            e.Row.Cells(2).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & "&s1=In Progress'>" & dr("inprogress") & "</a>"
            e.Row.Cells(3).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & "&s1=Bug'>" & dr("bug") & "</a>"
            e.Row.Cells(4).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & "&s1=Speak'>" & dr("speak") & "</a>"
            e.Row.Cells(5).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & "&s1=Testing'>" & dr("testing") & "</a>"
            e.Row.Cells(6).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & "&s1=Hold by Client'>" & dr("hold1") & "</a>"
            ' e.Row.Cells(7).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & "&s1=Hold by Kamtech'>" & dr("hold2") & "</a>"
            e.Row.Cells(8).Text = "<a href='ticketdetailreport.aspx?emp=" & dr("ids") & "&s1=Completed'>" & dr("completed") & "</a>"

            t1 = t1 + CInt(dr("Pending"))
            t2 = t2 + CInt(dr("inprogress"))
            t3 = t3 + CInt(dr("bug"))
            t4 = t4 + CInt(dr("speak"))
            t5 = t5 + CInt(dr("testing"))
            t6 = t6 + CInt(dr("hold1"))
            ' t7 = t7 + CInt(dr("hold2"))
            t8 = t8 + CInt(dr("completed"))
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "Total"
            e.Row.Cells(1).Text = t1
            e.Row.Cells(2).Text = t2
            e.Row.Cells(3).Text = t3
            e.Row.Cells(4).Text = t4
            e.Row.Cells(5).Text = t5
            e.Row.Cells(6).Text = t6
            '  e.Row.Cells(7).Text = t7
            e.Row.Cells(8).Text = t8
            ' t1 = 0 : t2 = 0 : t3 = 0 : t4 = 0 : t5 = 0 : t6 = 0 : t7 = 0 : t8 = 0
            t1 = 0 : t2 = 0 : t3 = 0 : t4 = 0 : t5 = 0 : t6 = 0 : t8 = 0

        End If
    End Sub

    Protected Sub gvcmp_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvcmp.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            t1 = t1 + CInt(dr("Pending"))
            t2 = t2 + CInt(dr("inprogress"))
            t3 = t3 + CInt(dr("bug"))
            t4 = t4 + CInt(dr("speak"))
            t5 = t5 + CInt(dr("testing"))
            t6 = t6 + CInt(dr("hold1"))
            t7 = t7 + CInt(dr("hold2"))
            t8 = t8 + CInt(dr("completed"))
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "Total"
            e.Row.Cells(1).Text = t1
            e.Row.Cells(2).Text = t2
            e.Row.Cells(3).Text = t3
            e.Row.Cells(4).Text = t4
            e.Row.Cells(5).Text = t5
            e.Row.Cells(6).Text = t6
            e.Row.Cells(7).Text = t7
            e.Row.Cells(8).Text = t8
        End If
    End Sub

    Protected Sub drpdivision_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpdivision.SelectedIndexChanged
        db.fillCombo(dremp, "director", "dirname +' '+ empmname + ' '+ emplname ", "Ids", " where division=" & drpdivision.SelectedValue & " order by dirname")
        show1()
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
        show1()
    End Sub

    'Public Sub export()


    '    gvdummy.AllowPaging = False
    '    gvdummy.DataBind()
    '    Response.ClearContent()
    '    Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "Customers.doc"))
    '    Response.Charset = ""
    '    Response.ContentType = "application/ms-word"
    '    Dim SB As New StringBuilder()
    '    Dim sw As New StringWriter(SB)
    '    Dim htw As New HtmlTextWriter(sw)
    '    pnlhead.RenderControl(htw)
    '    gvdummy.RenderControl(htw)
    '    pnlfooter.RenderControl(htw)
    '    Response.Write(sw.ToString())
    '    Response.[End]()


    'pnlfooter.RenderControl(hw)


    ' End Sub
End Class

