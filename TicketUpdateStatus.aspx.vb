Imports System.Web.Mail
Imports System.Net
Imports System.Net.Mail
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.IO

Partial Class Admin_TicketUpdateStatus
    Inherits System.Web.UI.Page
    Public db As New general
    Public dr As Object
    Dim ds As New DataSet
    Dim str As String
    Dim myDate2, myDate1 As DateTime
    Dim ttime, flag As Integer
    Dim total As Double = 0
    Dim hours As Double = 0
    Dim day As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        'db.setLayout(Session("userId"))
        'End If

        If Not IsPostBack Then

            db.fillCombo(drpstatus, "statusmaster", "tktstatus", "tktstatus", " where tktstatus in('Completed','Bug') group by tktstatus order by tktstatus")

            db.fillCombo(drpstatus1, "statusmaster", "tktstatus", "tktstatus", " where tktstatus in('Testing','Speak') order by tktstatus", , "Over All")

            db.fillCombo(drpemp1, "director", "dirname +' '+ empmname +' '+ emplname ", "str(ids)", " where ids in (select assignto from ticketmaster group by assignto) order by dirname", , "Over All")

            db.fillCombo(drpclient, "clientmaster", "companyname", "str(companyid)", " where companyid in(select client from ticket group by client) order by companyname", , "Over All")

            ' db.fillCombo(dremp, "director", "dirname", "ids", " where empno<>'0' and flg=0 order by dirname")
            db.fillCombo(dremp, "director", "dirname", "Ids", " where 1=2")
            db.fillCombo(drpdivision, "divisions", "dname", "divisionid", " where 1=1 order by dname")

            drprecords.DataSource = db.noRecords
            drprecords.DataBind()
            drprecords.SelectedValue = "10"
            lbldate.Text = DateTime.Today.ToString("dd-MMM-yyyy")
            lblday.Text = DateTime.Now.DayOfWeek.ToString()
            drpclient.SelectedValue = "Over All"
            drpemp1.SelectedValue = "Over All"
            drpstatus1.SelectedValue = "Testing"
            show()

        End If

        'btnassigned.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")

    End Sub


    Private Sub show()

        db.qry = "select (select case when CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)=0 then companyname else SUBSTRING(companyname, 0, CHARINDEX(' ', companyname,CHARINDEX(' ', companyname, 0)+1)) end from clientmaster where companyid=(select client from ticket where tid=t.tid)) as client,(select dirname from director where ids=t.assignto) as assignto,t.* from ticketmaster as t,ticket as tkt where t.tid=tkt.tid and t.status='Assigned' and (t.empstatus='Testing' or t.empstatus='Speak') "

        If drpclient.SelectedValue <> "Over All" Then
            db.qry = db.qry & " and t.tid in(select tid from ticket where client=" & drpclient.SelectedValue & ")"
        End If

        If drpstatus1.SelectedValue <> "Over All" Then

            db.qry = db.qry & " and t.empstatus='" & drpstatus1.SelectedValue & "'"

        End If

        If drpemp1.SelectedValue <> "Over All" Then
            db.qry = db.qry & " and t.assignto=" & drpemp1.SelectedValue & ""
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

        'dr = db.fillReader
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
            'Gv1.HeaderRow.Cells(1).Text = "Tkt._No."
            'Gv1.HeaderRow.Cells(2).Text = "Client"
            'Gv1.HeaderRow.Cells(3).Text = "Project"
            'Gv1.HeaderRow.Cells(4).Text = "Description"
            'Gv1.HeaderRow.Cells(5).Text = "Client_Remark"
            'Gv1.HeaderRow.Cells(6).Text = "Technical_Remark"
            'Gv1.HeaderRow.Cells(7).Text = "Assign_Date"
            'Gv1.HeaderRow.Cells(8).Text = "AssignTo"
            'Gv1.HeaderRow.Cells(9).Text = "Allotted Time <br/>(in minutes)"
            'Gv1.HeaderRow.Cells(10).Text = "Total Work_Time <br/>(in minutes)"
            'Gv1.HeaderRow.Cells(11).Text = "Give_Reason"
            'Gv1.HeaderRow.Cells(12).Text = "Action"
            'Gv1.HeaderRow.Cells(13).Text = "Update"





        Else
            Gv1.DataSource = Nothing
            Gv1.DataBind()

        End If


        ds.Dispose()

    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        show()
    End Sub

    Protected Sub Gv1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gv1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = (Gv1.PageIndex * Gv1.PageSize) + (e.Row.RowIndex + 1)


            Dim j As Integer = -1
            Dim t As Double

            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(0).Text = (Gv1.PageIndex * Gv1.PageSize) + (e.Row.RowIndex + 1)
                Dim time As Label = CType(e.Row.FindControl("lblduration"), Label)
                t += CDbl(time.Text)
                't += dr("timeduration")
                total += t

                Dim cminutes As Label = CType(e.Row.FindControl("lbltime"), Label)
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
        End If


    End Sub

    Protected Sub btnassigned_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnassigned.Click


        Dim i As Integer
        If drpstatus.SelectedValue <> "0" Then

            For i = 0 To Gv1.Rows.Count - 1

                Dim ck As CheckBox = CType(Gv1.Rows(i).Cells(12).FindControl("CheckBox1"), CheckBox)
                If ck.Checked = True Then
                    Dim hdn As HiddenField = CType(Gv1.Rows(i).Cells(12).FindControl("HiddenField1"), HiddenField)
                    If CStr(hdn.Value) <> "" Then
                        If drpstatus.SelectedValue = "Completed" Then

                            db.qry = "update ticketmaster set status = '" & drpstatus.SelectedValue & "',empstatus = '" & drpstatus.SelectedValue & "',cdate1='" & Now.Date.ToString("dd-MMM-yyyy") & "' where tsid=" & CInt(hdn.Value) & ""
                            db.executeQuery()

                            Dim tid As Integer = db.getFieldValue("ticketmaster", "tsid", CInt(hdn.Value), "tid", True)

                            If db.isExists("ticketmaster", "1", "1", True, " and status<>'Completed' and tid=" & tid & "") = False Then

                                db.qry = "update ticket set cdt='" & Now.Date.ToString("dd-MMM-yyyy") & "',status='Completed' where tid=" & tid & ""
                                db.executeQuery()

                            End If

                            If str = "" Then
                                str = i
                            Else
                                str = str & "," & i
                            End If

                        ElseIf drpstatus.SelectedValue = "Bug" Then
                            db.qry = "update ticketmaster set empstatus = '" & drpstatus.SelectedValue & "' where tsid=" & CInt(hdn.Value) & ""
                            db.executeQuery()
                        End If
                    End If
                End If

            Next

            If drpclient.SelectedValue <> "0" And drpclient.SelectedValue <> "Over All" And str <> "" Then
                sendmailclient()
            End If

            show()

        End If


    End Sub

    Protected Sub Gv1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gv1.PageIndexChanging

        Gv1.PageIndex = e.NewPageIndex
        show()

    End Sub



    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim i As Integer
        If dremp.SelectedValue <> "0" Then

            For i = 0 To Gv1.Rows.Count - 1

                Dim ck As CheckBox = CType(Gv1.Rows(i).Cells(12).FindControl("CheckBox1"), CheckBox)
                If ck.Checked = True Then
                    Dim hdn As HiddenField = CType(Gv1.Rows(i).Cells(12).FindControl("HiddenField1"), HiddenField)
                    If CStr(hdn.Value) <> "" Then
                        db.qry = "update ticketmaster set empstatus = 'Pending',assignby=" & Session("empid") & " ,assignto=" & dremp.SelectedValue & " ,adt2='" & Now.Date.ToString("dd-MMM-yyyy") & "' where tsid=" & CInt(hdn.Value) & ""
                        db.executeQuery()
                    End If
                End If

            Next

            show()

        End If

    End Sub

    Private Sub sendmailclient()

        Dim emailto As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "emailid", True)
        Dim temailto As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "temailid", True)
        Dim client As New System.Net.Mail.SmtpClient("69.175.99.245", "25")

        'Dim cn As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "country", True)
        'Dim st As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "state", True)
        'Dim ct As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "city", True)
        'Dim cnty As String = db.getFieldValue("country", "countryid", cn, "countryname", True)
        'Dim stat As String = db.getFieldValue("state", "stateid", st, "statename", True)
        'Dim cty As String = db.getFieldValue("city", "cityid", ct, "cityname", True)


        client.Credentials = New NetworkCredential("mail@kamtechassociates.com", "kapl#100")



        If emailto <> "" Or temailto <> "" Then

            If emailto <> "" Then
                emailto = emailto & "," & temailto
            Else
                emailto = temailto
            End If

            Dim message As New System.Net.Mail.MailMessage("mail@kamtechassociates.com", emailto)

            Dim k As Integer = 0

            For k = 0 To Gv1.Rows.Count - 1
                Gv1.Rows(k).Visible = False
            Next

            If str.Contains(",") Then

                Dim str1() As String = str.Split(",")

                For Each part As String In str1
                    Gv1.Rows(CInt(part)).Visible = True
                Next

            Else

                Gv1.Rows(CInt(str)).Visible = True

            End If

            Gv1.Columns(0).Visible = False
            Gv1.Columns(2).Visible = False
            Gv1.Columns(5).Visible = False
            Gv1.Columns(6).Visible = False
            Gv1.Columns(7).Visible = False
            Gv1.Columns(8).Visible = False
            Gv1.Columns(9).Visible = False
            Gv1.Columns(10).Visible = False

            Dim SB As New StringBuilder()
            Dim SW As New StringWriter(SB)
            Dim htmlTW As New HtmlTextWriter(SW)

            Gv1.RenderControl(htmlTW)

            Dim dataGridHTML As String = SB.ToString()

            Dim addr As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "address", True)

            Dim body As String = "<font size='3'>Dear Sir, <br/><p align='justify'>Following Points are managed in realcrm software.Please check any revert back to us.</font> "

            body = body & "<br/>" & dataGridHTML
            body = body & "&nbsp;<br/><br/><br/> Regards! <br/><br/>Technical Support Team (IT)<br/>Kamtech Associates Pvt. Ltd.<br/>(India~Kenya~Tanjania~U.K.)<br/><br/> G5, Gajraj Apartment, Sarojini Marg, C-Scheme, Jaipur-302001.<br/>Rajasthan, India.<br/><br/>Ph:  +91-141-2377559, 2373226, 2371308, Fax +91-141-4041885<br/>Mobile - +91-9828042461<br/>Web - www.realcrm.in, www.propertyjunction.in, www.kamtech.in<br/>E-mail - office@kamtech.in, ajain@kamtech.in, mail@kamtechassociates.com<br/><br/>* Recipient of MSME-IT Excellence Gold Award 2011<br/>* Recipient of National Award 2009 (First Prize)by President of India <br/>* National Award 2008 (Special Recognition Prize) in Service Sector for Outstanding Performance by  Government of India.<br/><br/><b>Strategic support to your business.</b><br/><br/><font face='Franklin Gothic Heavy' size='3'> Kamtech - Saksham MIS generated report.</font>"

            message.Body = body

            message.BodyEncoding = System.Text.Encoding.UTF8
            message.IsBodyHtml = True
            Dim subj As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "companyname", True) & " Ticket Status"

            message.Subject = subj

            Try
                client.Send(message)

            Catch ex As SmtpException
                Response.Write("<pre>" & ex.ToString() & "</pre>")
            Finally
                ' Clean up.
                message.Dispose()
            End Try

            Gv1.Columns(0).Visible = True
            Gv1.Columns(2).Visible = True
            Gv1.Columns(5).Visible = True
            Gv1.Columns(6).Visible = True
            Gv1.Columns(7).Visible = True
            Gv1.Columns(8).Visible = True
            Gv1.Columns(9).Visible = True
            Gv1.Columns(10).Visible = True

        End If


    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub

    Protected Sub btnremark_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnremark.Click

        Dim i As Integer

        For i = 0 To Gv1.Rows.Count - 1

            Dim ck As CheckBox = CType(Gv1.Rows(i).Cells(12).FindControl("CheckBox1"), CheckBox)
            Dim t1 As TextBox = CType(Gv1.Rows(i).Cells(5).FindControl("txtremark"), TextBox)

            If ck.Checked = True Then
                Dim hdn As HiddenField = CType(Gv1.Rows(i).Cells(12).FindControl("HiddenField1"), HiddenField)
                If CStr(hdn.Value) <> "" And t1.Text <> "" Then
                    db.qry = "update ticketmaster set premark='" & t1.Text & "' where tsid=" & CInt(hdn.Value) & ""
                    db.executeQuery()
                End If
            End If

        Next
        show()


    End Sub

    Protected Sub Gv1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Gv1.SelectedIndexChanged

    End Sub
    Protected Sub Gv1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles Gv1.RowUpdating

        Dim s As Integer
        Dim diff As Double
        s = Gv1.DataKeys(e.RowIndex).Value
        Dim t2, t3 As TextBox
        Dim l1 As Label
        l1 = CType(Gv1.Rows(e.RowIndex).FindControl("lbltime"), Label)
        t2 = CType(Gv1.Rows(e.RowIndex).FindControl("txtedit"), TextBox)
        t3 = CType(Gv1.Rows(e.RowIndex).FindControl("txteremark"), TextBox)

        'h1 = t2.Text.ToString()
        'h2 = l1.Text.ToString()

        'Dim d1, d2, d3,h1,h2 As String
        'Dim arrDate() As String = h1.Split("*"c)

        'd2 = arrDate(0).ToString()
        'd3 = arrDate(1).ToString()
        'Dim arrDate1() As String = h2.Split("*"c)

        'd1 = arrDate1(0).ToString()

        'diff = d1 - d2
        diff = Convert.ToDouble(l1.Text) - Convert.ToDouble(t2.Text)
        If diff <> 0 And t3.Text <> "" Then
            db.qry = "update ticketmaster set totaltime=" & diff & ",editremark=' Date:  " & Now.Date.ToString("dd-MMM-yyyy") & " <br/> Reason: " & t3.Text & "<br/> Previous Work_Time: " & l1.Text & "<br/> Edit time: " & t2.Text & "' where tsid=" & s
            db.executeQuery()

        End If
        Gv1.EditIndex = -1


        show()
    End Sub
    Protected Sub Gv1_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles Gv1.RowCancelingEdit
        Gv1.EditIndex = -1
        show()
    End Sub

    Protected Sub Gv1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles Gv1.RowEditing
        Gv1.EditIndex = e.NewEditIndex
        show()
    End Sub

    Protected Sub drpstatus_Load(sender As Object, e As EventArgs) Handles drpstatus.Load

    End Sub

    Protected Sub drpdivision_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpdivision.SelectedIndexChanged
        db.fillCombo(dremp, "director", "dirname", "Ids", " where division=" & drpdivision.SelectedValue & " order by dirname")
    End Sub
End Class
