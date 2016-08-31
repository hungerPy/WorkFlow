Imports System.Web.Mail
Imports System.Net
Imports System.Net.Mail
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.IO
Imports System.Threading
Imports System.Web.Configuration
Imports System.Net.WebClient
Imports System.Data.SqlClient

Partial Class TicketFrom
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String
    Dim tid As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        'db.setLayout(Session("userId"))
        'End If

        If Not IsPostBack Then

            db.qry = "delete from temperary where sessionid='" & Session.SessionID & "'"
            db.executeQuery()

            db.fillCombo(drpdivision, "Divisions", "DName", "DCode", "where flg=0 ")
            db.fillCombo(drpproject, "ticketmaster", "project", "project", " group by project order by project")
            db.fillCombo(drpclient, "clientmaster", "companyname", "companyid", " where flg=0 order by companyname")

            'Dim t
            'For Each t In db.mName
            '    m1.Items.Add(t)
            'Next

            'For t = Now.Year - 1 To Now.Year + 1
            '    y1.Items.Add(t)
            'Next


            'For t = 1 To 31
            '    d1.Items.Add(t)
            'Next

            'm1.SelectedIndex = Now.Month - 1

            'y1.SelectedIndex = (Now.Year)

            'd1.SelectedValue = Now.Day
            'm1.SelectedValue = Now.Month
            'y1.SelectedValue = Now.Year



        End If

        'd1.Attributes.Add("onchange", "getDate(0," & d1.ClientID & "," & m1.ClientID & "," & y1.ClientID & ")")
        'm1.Attributes.Add("onchange", "getDate(0," & d1.ClientID & "," & m1.ClientID & "," & y1.ClientID & ")")
        'y1.Attributes.Add("onchange", "getDate(0," & d1.ClientID & "," & m1.ClientID & "," & y1.ClientID & ")")

        'btnadd.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")

        P1.Visible = False
        P3.Visible = False
        P4.Visible = False
        'l4.Visible = False


    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try

            l1.Text = DateTime.Today.ToString("dd-MMM-yyyy")
            tid = db.getMaxId("ticket", "tid")

            'Dim city As String = db.getFieldValue("state", "state", drpclient.SelectedValue, "statename", True)
            'Dim company As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "companyname", True)
            Dim comemail As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "temailid", True)
            Dim contactno As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "telephoneno", True)
            Dim add As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "address", True)
            'Dim mail As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "EmailId", True)


            'Dim tktdt As String = d1.SelectedValue + "-" + m1.SelectedValue + "-" + y1.SelectedValue
            Dim tktdt As String = TextBox1.Text

            If Gv1.Rows.Count > 0 Then
                'If company <> "" Then
                db.qry = "insert into ticket values(" & tid & "," & drpclient.SelectedValue & ",'" & comemail & "','" & tktdt & "','','Pending')"
                db.executeQuery()
                'End If
            End If

            strqry1 = "select id,f1,f2,f3,f4 from temperary where sessionid='" & Session.SessionID & "'"
            dt1 = db.fillReader1(strqry1)
            dr = dt1.CreateDataReader()

            While dr.read
                db.qry = "insert into ticketmaster values(" & db.getMaxId("ticketmaster", "tsid") & "," & tid & ",'" & dr("f1") & "','" & dr("f2") & "','" & tktdt & "','Pending',0,0,'','','" & dr("f3") & "','',0,'','','','','','','','','',''," & dr("f4") & ")"
                db.executeQuery()
            End While
            dr.Close()
            dt1.Clear()
            dt1.Dispose()

            Dim cn As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "country", True)
            Dim st As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "state", True)
            Dim ct As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "city", True)
            Dim cnty As String = db.getFieldValue("country", "countryid", cn, "countryname", True)
            Dim stat As String = db.getFieldValue("state", "stateid", st, "statename", True)
            Dim cty As String = db.getFieldValue("city", "cityid", ct, "cityname", True)

            l2.Text = drpclient.SelectedItem.Text

            If drpproject.SelectedValue <> "0" Then
                l3.Text = drpproject.SelectedValue
            Else
                l3.Text = txtproject.Text
            End If

            l5.Text = tid
            'l4.Visible = False
            Label1.Text = drpclient.SelectedItem.Text
            Label4.Text = add
            Label2.Text = comemail
            Label3.Text = contactno
            Label5.Text = cty
            Label6.Text = stat
            Label7.Text = cnty

            'Dim f As String
            'f = ""
            'f = System.IO.Path.GetFileName(FileUpload1.FileName)
            '' f = FileUpload1.FileName;
            ''f = Path.GetFileName(FileUpload1.FileName);


            'If f <> "" Then

            '    FileUpload1.SaveAs(Server.MapPath("~") + "/Data/" + f)
            '    f = "~/Data/" + f
            'End If
            'Dim ext1 As String = System.IO.Path.GetExtension(FileUpload2.FileName)
            'If (ext1 = ".jpg" Or ext1 = ".jpeg" Or ext1 = ".JPG" Or ext1 = ".JPEG" Or ext1 = ".gif" Or ext1 = ".GIF" Or ext1 = ".tif" Or ext1 = ".TIF") Then
            '    Image4.Visible = True
            '    Image4.ImageUrl = f
            'Else
            '    HyperLink1.Visible = True
            '    HyperLink1.Text = f
            'End If
            'Dim f1 As String
            'f1 = ""
            'f1 = System.IO.Path.GetFileName(FileUpload2.FileName)
            ' f = FileUpload1.FileName;
            'f = Path.GetFileName(FileUpload1.FileName);


            'If f1 <> "" Then

            '    FileUpload2.SaveAs(Server.MapPath("~") + "/Data/" + f1)
            '    f1 = "~/Data/" + f1
            'End If
            'Dim ext2 As String = System.IO.Path.GetExtension(FileUpload2.FileName)
            'If (ext2 = ".jpg" Or ext2 = ".jpeg" Or ext2 = ".JPG" Or ext2 = ".JPEG" Or ext2 = ".gif" Or ext2 = ".GIF" Or ext2 = ".tif" Or ext2 = ".TIF") Then
            '    Image3.Visible = True
            '    Image3.ImageUrl = f1
            'Else
            '    HyperLink1.Visible = True
            '    HyperLink2.Text = f1
            'End If

            sendmail()

            If rd1.Checked Then
                sendmailclient()
            End If

            db.qry = "delete from temperary where sessionid='" & Session.SessionID & "'"
            db.executeQuery()

            txtproject.Enabled = True
            drpproject.Enabled = True
            drpclient.SelectedValue = "0"
            drpproject.SelectedValue = "0"
            txtproject.Text = ""
            txtdescription.Text = ""
            txtremark.Text = ""

            show()
            'l4.Visible = True

            ClientScript.RegisterStartupScript(Me.GetType(), "Call Me", "loadPopupBox()", True)

        Catch ex As Exception
            lblerror.Text = ex.Message

        End Try


    End Sub


    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnadd.Click

        Dim project As String

        If drpproject.SelectedValue <> "0" Then
            project = drpproject.SelectedValue
        Else
            project = txtproject.Text
        End If
        Dim empid As Integer = CInt(Session("empid"))
        Dim departmentid As Integer = db.getFieldValue("director", "empid", empid, "division")


        db.qry = "insert into temperary(id,f1,f2,f3,f4,sessionid) values(" & db.getMaxId("temperary", "id") & ",'" & project & "','" & txtdescription.Text & "','" & txtremark.Text & "','" & empid & "','" & Session.SessionID & "')"
        db.executeQuery()

        txtproject.Enabled = False
        drpproject.Enabled = False
        txtdescription.Text = ""
        txtremark.Text = ""

        show()

    End Sub


    Private Sub show()

        strqry1 = "select id,f1,f2,f3 from temperary where sessionid='" & Session.SessionID & "'"
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        If dr.hasRows Then
            Gv1.DataSource = dr
            Gv1.DataBind()
        Else
            Gv1.DataSource = Nothing
            Gv1.DataBind()
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()

    End Sub

    Private Sub sendmail()

        P1.Visible = True
        P3.Visible = True
        P4.Visible = True
        'Dim i As Integer

        Dim emailto As String = "ajain@kamtechassociates.com,office@kamtechassociates.com"

        Dim client As New System.Net.Mail.SmtpClient("69.175.99.245", "25")

        client.Credentials = New NetworkCredential("mail@kamtechassociates.com", "kapl#100")


        If emailto <> "" Then

            'Dim emailto As String = txtemail.Text //info@ashapurna.com
            'Dim message As New System.Net.Mail.MailMessage(txtEmail.Text, emailto)
            Dim message As New System.Net.Mail.MailMessage("mail@kamtechassociates.com", emailto)

            Gv1.Columns(3).Visible = False

            Dim SB As New StringBuilder()
            Dim SW As New StringWriter(SB)
            Dim htmlTW As New HtmlTextWriter(SW)

            'Gv1.RenderControl(htmlTW)
            P2.RenderControl(htmlTW)



            ' Get the HTML into a string.
            ' This will be used in the body of the email report.
            '---------------------------------------------------
            Dim dataGridHTML As String = SB.ToString()
            Dim body As String = dataGridHTML

            message.Body = body

            Gv1.Columns(3).Visible = True

            message.BodyEncoding = System.Text.Encoding.UTF8
            message.IsBodyHtml = True

            message.Subject = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "companyname", True) & " Ticket"

            '   If Request.Files.Count > 0 Then

            'dim file = Request.Files[0]

            '       If FileLe > 0 Then

            '           Dim fileName = Path.GetFileName(File.FileName)
            '           Dim Path = Path.Combine(Server.MapPath("~/Images/"), fileName)
            '           File.SaveAs(Path)
            '       End If
            '   End If

            'If FileUpload1.HasFile Then

            '    If FileUpload1.PostedFile.ContentLength > 0 Then

            '        Dim ext As String = System.IO.Path.GetExtension(FileUpload1.FileName)
            '        Dim size As Integer = FileUpload1.FileBytes.Length


            '        If (ext = ".pdf" Or ext = ".PDF" Or ext = ".jpg" Or ext = ".jpeg" Or ext = ".JPG" Or ext = ".JPEG" Or ext = ".gif" Or ext = ".GIF" Or ext = ".tif" Or ext = ".TIF" Or ext = ".rar" Or ext = ".RAR" Or ext = ".zip" Or ext = ".ZIP") Then


            '            Dim attachment As New Attachment(FileUpload1.PostedFile.InputStream, FileUpload1.PostedFile.FileName)
            '            message.Attachments.Add(attachment)

            '            i = 1
            '        End If
            '    End If
            'End If

            'If FileUpload2.HasFile Then

            '    If FileUpload2.PostedFile.ContentLength > 0 Then

            'Dim ext1 As String = System.IO.Path.GetExtension(FileUpload2.FileName)
            'Dim size1 As Integer = FileUpload2.FileBytes.Length

            'If (ext1 = ".pdf" Or ext1 = ".PDF" Or ext1 = ".jpg" Or ext1 = ".jpeg" Or ext1 = ".JPG" Or ext1 = ".JPEG" Or ext1 = ".gif" Or ext1 = ".GIF" Or ext1 = ".tif" Or ext1 = ".TIF" Or ext1 = ".rar" Or ext1 = ".RAR" Or ext1 = ".zip" Or ext1 = ".ZIP") Then


            '        Dim attachment As New Attachment(FileUpload2.PostedFile.InputStream, FileUpload2.PostedFile.FileName)
            '        message.Attachments.Add(attachment)

            '        i = 1


            '    End If
            'End If
            '    End If

            'If i = 1 Then

            '    message.SubjectEncoding = System.Text.Encoding.UTF8

            '    Try
            '        client.Send(message)

            '    Catch ex As SmtpException
            '        Response.Write("<pre>" & ex.ToString() & "</pre>")
            '    Finally
            ' Clean up.
            '        message.Dispose()
            '    End Try


            'Else
            '    message.SubjectEncoding = System.Text.Encoding.UTF8
            '    ' send message

            '    Try
            '        client.Send(message)

            '    Catch ex As SmtpException
            '        Response.Write("<pre>" & ex.ToString() & "</pre>")
            '    Finally
            '        ' Clean up.
            '        message.Dispose()
            '    End Try
            'End If  

            'Dim message As New System.Net.Mail.MailMessage("mail@kamtechassociates.com", emailto)

            'If fileUploader IsNot Nothing Then
            '    Dim fileName As String = Path.GetFileName(fileUploader.FileName)
            '    'mail.Attachments.Add(New Attachment(fileUploader.InputStream, fileName))
            '    Dim attachment As New Attachment(FileUploader.filename, FileUploader..FileName)
            '    message.Attachments.Add(attachment)
            '    Dim ext1 As String = System.IO.Path.GetExtension(FileUploader.FileName)
            'End If


        End If


        P1.Visible = False
        P3.Visible = False
        P4.Visible = False


    End Sub

    Private Sub sendmailclient()

        Dim emailto As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "emailid", True)
        Dim temailto As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "temailid", True)
        Dim client As New System.Net.Mail.SmtpClient("69.175.99.245", "25")

        Dim cn As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "country", True)
        Dim st As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "state", True)
        Dim ct As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "city", True)
        Dim cnty As String = db.getFieldValue("country", "countryid", cn, "countryname", True)
        Dim stat As String = db.getFieldValue("state", "stateid", st, "statename", True)
        Dim cty As String = db.getFieldValue("city", "cityid", ct, "cityname", True)


        client.Credentials = New NetworkCredential("mail@kamtechassociates.com", "kapl#100")

        If emailto <> "" Then

            'If emailto <> "" Or temailto <> "" Then

            'If emailto <> "" Then
            '    emailto = emailto & "," & temailto
            'Else
            '    emailto = temailto
            'End If

            Dim message As New System.Net.Mail.MailMessage("mail@kamtechassociates.com", emailto)

            Gv1.Columns(3).Visible = False

            Dim SB As New StringBuilder()
            Dim SW As New StringWriter(SB)
            Dim htmlTW As New HtmlTextWriter(SW)

            Gv1.RenderControl(htmlTW)

            Dim dataGridHTML As String = SB.ToString()

            Dim addr As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "address", True)

            Dim body As String = "To,<br/>" & db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "companyname", True) & "<br/>" & addr & "<br/>" & cty & "<br/>" & stat & "," & cnty & "<br/><br/><br/><font size='3'>Dear Sir, <br/><p align='justify'>Thank you for contacting us. We are confirming the receipt of your technical assistance ticket.<br/><br/> Required points are under consideration. Our executive will contact you as soon as possible with in next working day.<br/><br/> For records, the details of the ticket are listed below : <p><b>Ticket ID :</b>" & tid & "<br/><b> Dated : </b> " & DateTime.Today.ToString("dd-MMM-yyyy") & "</font> "

            body = body & "<br/>" & dataGridHTML
            body = body & "&nbsp;<br/><br/><br/> Regards! <br/><br/>Technical Support Team (IT)<br/>Kamtech Associates Pvt. Ltd.<br/>(India~Kenya~Tanjania~U.K.)<br/><br/> G5, Gajraj Apartment, Sarojini Marg, C-Scheme, Jaipur-302001.<br/>Rajasthan, India.<br/><br/>Ph:  +91-141-2377559, 2373226, 2371308, Fax +91-141-4041885<br/>Mobile - +91-9828042461<br/>Web - www.realcrm.in, www.propertyjunction.in, www.kamtech.in<br/>E-mail - office@kamtech.in, ajain@kamtech.in, mail@kamtechassociates.com<br/><br/>* Recipient of MSME-IT Excellence Gold Award 2011<br/>* Recipient of National Award 2009 (First Prize)by President of India <br/>* National Award 2008 (Special Recognition Prize) in Service Sector for Outstanding Performance by  Government of India.<br/><br/><b>Strategic support to your business.</b><br/><br/><font face='Franklin Gothic Heavy' size='3'> Kamtech - Saksham MIS generated report.</font>"

            message.Body = body

            message.BodyEncoding = System.Text.Encoding.UTF8
            message.IsBodyHtml = True
            Dim subj As String = db.getFieldValue("clientmaster", "companyid", drpclient.SelectedValue, "companyname", True) & " Ticket No. : " & tid

            message.Subject = subj

            Try
                client.Send(message)

            Catch ex As SmtpException
                Response.Write("<pre>" & ex.ToString() & "</pre>")
            Finally
                ' Clean up.
                message.Dispose()
            End Try

            Gv1.Columns(3).Visible = True

        End If


    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub

    Protected Sub Gv1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gv1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            Dim l As LinkButton
            l = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            If Not l Is Nothing Then
                l.Attributes.Add("onclick", "return confirm('Do you really want to delete')")
            End If

        End If

    End Sub

    Protected Sub Gv1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles Gv1.RowDeleting

        Dim s As Integer
        s = Gv1.DataKeys(e.RowIndex).Value
        db.qry = "delete from temperary where id=" & s
        db.executeQuery()
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

    Protected Sub Gv1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles Gv1.RowUpdating

        Dim s As Integer
        s = Gv1.DataKeys(e.RowIndex).Value
        Dim t As TextBox
        Dim t2 As TextBox
        t = CType(Gv1.Rows(e.RowIndex).FindControl("txtdes"), TextBox)
        t2 = CType(Gv1.Rows(e.RowIndex).FindControl("txtremark"), TextBox)

        If t.Text = "" Then
            Exit Sub
        End If

        db.qry = "update temperary set f2='" & t.Text & "',f3='" & t2.Text & "' where id=" & s
        db.executeQuery()
        Gv1.EditIndex = -1
        show()
    End Sub



    Protected Sub drpdivision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdivision.SelectedIndexChanged

        If drpdivision.SelectedValue <> "0" Then
            db.fillCombo(drpclient, "clientmaster", "companyname", "companyid", " where companyid in(select clientid from clientPOdetais where dcode='" & drpdivision.SelectedValue & "') order by companyname")
        Else
            db.fillCombo(drpclient, "clientmaster", "companyname", "companyid", " order by companyname")
        End If

    End Sub

    Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        Dim targetdays As DateTime = TextBox1.Text
        Dim currentdate As DateTime = DateTime.Today.ToString("dd-MMM-yyyy")
        Dim objTimeSpan As TimeSpan = targetdays - currentdate
        Dim Days As Double = Convert.ToDouble(objTimeSpan.TotalDays)
        Dim td As String = Days
        'Dim td() As String = d.Split()
        If td(0) = "-" Then
            Response.Write("<script>alert('Please Do not select back date')</script>")
            TextBox1.Text = ""
            TextBox1.Focus()
            Exit Sub
        End If
    End Sub
End Class
