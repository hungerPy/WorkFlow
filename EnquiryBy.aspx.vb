
Partial Class Admin_EnquiryBy
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dr As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        ' db.setLayout(Session("userId"))
        'End If
        'btnSubmit.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")

        If Not IsPostBack Then
            db.fillCombo(DrpDivision, "Divisions", "DName", "DivisionId", " where flg=0")
            lblerror.Text = ""

            Dim d
            For Each d In db.mName
                month.Items.Add(d)
            Next
            For d = 1 To 31
                day.Items.Add(d)
            Next
            For d = 1997 To Now.Year
                year.Items.Add(d)
            Next
            month.SelectedIndex = Now.Month - 1
            day.SelectedIndex = Now.Day - 1
            year.SelectedIndex = (Now.Year) - 1997
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        pnlmain.Visible = False
        pnlprev.Visible = True
        Dim DOE As String = day.SelectedValue & "-" & month.SelectedValue & "-" & year.SelectedValue
        lblcontactno1.Text = Txtmobno.Text
        lblenqdate1.Text = DOE
        If txtcontname.Text <> "" Then
            lblcontname1.Text = txtcontname.Text
            lblcontname.Visible = True
            lblcontname1.Visible = True
        Else
            lblcontname.Visible = False
            lblcontname1.Visible = False
        End If
        If txtEmailid.Text <> "" Then
            lblmailid1.Text = txtEmailid.Text
            lblmailid.Visible = True
            lblmailid1.Visible = True
        Else
            lblmailid.Visible = False
            lblmailid1.Visible = False
        End If
        If DrpDivision.SelectedValue <> "0" Then
            lbldivision1.Text = DrpDivision.SelectedItem.Text
            lbldivision.Visible = True
            lbldivision1.Visible = True
        Else
            lbldivision.Visible = False
            lbldivision1.Visible = False
        End If
        If txtenqfor.Text <> "" Then
            lblenqfor1.Text = txtenqfor.Text
            lblenqfor.Visible = True
            lblenqfor1.Visible = True
        Else
            lblenqfor.Visible = False
            lblenqfor1.Visible = False
        End If
        If txtcompName.Text <> "" Then
            lblcompname1.Text = txtcompName.Text
            lblcompname.Visible = True
            lblcompname1.Visible = True
        Else
            lblcompname.Visible = False
            lblcompname1.Visible = False
        End If
        If txtcontaddress.Text <> "" Then
            lblcontaddress1.Text = txtcontaddress.Text
            lblcontaddress.Visible = True
            lblcontaddress1.Visible = True
        Else
            lblcontaddress.Visible = False
            lblcontaddress1.Visible = False
        End If
        If txtremarks.Text <> "" Then
            lblremarks1.Text = txtremarks.Text
            lblremarks.Visible = True
            lblremarks1.Visible = True
        Else
            lblremarks.Visible = False
            lblremarks1.Visible = False
        End If
        'db.fillCheckList(chkallmail, "director", "designation+'('+dirName+')'", "compmailId", " where compmailid<>'0' and sendMail='-1' and flg=0")
        db.fillCheckList(chkemail, "director", "designation+'('+dirName+')'", "compmailId", " where compmailid<>'0' and sendMail='1' and flg=0")
        Dim tm
        For Each tm In chkemail.Items
            Dim mdgm = db.getFieldValue("director", "1", 1, "designation", False, " and compmailid='" & tm.value & "'")
            If mdgm = "Managing Director" Then
                tm.selected = True
            ElseIf mdgm = "General Manager" Then
                tm.selected = True
            Else
                tm.selected = False
            End If

        Next
    End Sub

    Protected Sub btnedit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnedit.Click
        pnlmain.Visible = True
        pnlprev.Visible = False
        Txtmobno.Text = lblcontactno1.Text
        Dim DOE As String = lblenqdate1.Text
        If DOE <> "" Then
            day.SelectedIndex = CDate(DOE).Day - 1
            month.SelectedIndex = CDate(DOE).Month - 1
            year.SelectedIndex = (CDate(DOE).Year) - 1997
        Else
            day.SelectedValue = Now.Day - 1
            month.SelectedValue = Now.Month - 1
            year.SelectedValue = (Now.Year) - 2000
        End If
        txtcontname.Text = lblcontname1.Text
        txtEmailid.Text = lblmailid1.Text
        If lbldivision1.Text <> "" Then
            DrpDivision.SelectedItem.Text = lbldivision1.Text
        Else
            DrpDivision.SelectedValue = "0"
        End If
        txtenqfor.Text = lblenqfor1.Text
        txtcompName.Text = lblcompname1.Text
        txtcontaddress.Text = lblcontaddress1.Text
        txtremarks.Text = lblremarks1.Text
        lblcontactno1.Text = ""
        lblenqdate1.Text = ""
        lblcontname1.Text = ""
        lblmailid1.Text = ""
        lbldivision1.Text = ""
        lblenqfor1.Text = ""
        lblcompname1.Text = ""
        lblcontaddress1.Text = ""
        lblremarks1.Text = ""
    End Sub

    Protected Sub btnfSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnfSubmit.Click
        Try
            Dim tm, tm1 As Object
            Dim mailId As String

            'For Each tm1 In chkallmail.Items
            '    If tm1.selected = True Then
            '        mailId = mailId & "," & tm1.value
            '    End If
            'Next

            For Each tm In chkemail.Items
                If tm.selected = True Then
                    mailId = mailId & "," & tm.value
                End If
            Next

            If mailId <> "" Then
                mailId = mailId.Substring(1, mailId.Length - 1)
            End If
            Dim enqid = db.getMaxId("EnquiryBy", "EnqId")
            db.qry = "insert into EnquiryBy values(" & enqid & ",'" & lblcontactno1.Text & "','" & lblmailid1.Text & "','" & lblenqfor1.Text & "','" & DrpDivision.SelectedValue & "','" & lblcompname1.Text & "','" & lblcontname1.Text & "','" & lblcontaddress1.Text & "','" & mailId & "','" & lblenqdate1.Text & "',0,'" & lblremarks1.Text & "')"
            db.executeQuery()
            clearData()
            pnlmain.Visible = True
            pnlprev.Visible = False
            Response.Redirect("email.aspx?enqid=" & enqid & "")
        Catch ex As Exception
            lblerror.Text = ex.Message
        End Try
    End Sub

    Private Sub clearData()
        lblcontactno1.Text = ""
        lblenqdate1.Text = ""
        lblcontname1.Text = ""
        lblmailid1.Text = ""
        lbldivision1.Text = ""
        lblenqfor1.Text = ""
        lblcompname1.Text = ""
        lblcontaddress1.Text = ""
        lblremarks1.Text = ""
        Txtmobno.Text = ""
        txtcontname.Text = ""
        txtcompName.Text = ""
        txtcontaddress.Text = ""
        DrpDivision.SelectedValue = "0"
        txtEmailid.Text = ""
        txtenqfor.Text = ""
        txtremarks.Text = ""
    End Sub
End Class
