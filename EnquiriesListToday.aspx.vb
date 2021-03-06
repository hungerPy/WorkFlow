Imports System.Data
Partial Class Admin_EnquiriesListToday
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
            lbldate.Text = Format(Now.Date, "dd-MMM-yyyy")
            ShowEnquiries()
            If Request.QueryString("id") <> "" Then
                lblmsg.Visible = False
                pnlmain.Visible = True
                pnlenq.Visible = False
                pnlprev.Visible = False
                db.fillCombo(DrpDivision, "Divisions", "DName", "DivisionId", " where flg=0")
                lblerror.Text = ""

                Dim d
                For Each d In db.mName
                    Month.Items.Add(d)
                Next
                For d = 1 To 31
                    Day.Items.Add(d)
                Next
                For d = 1997 To Now.Year
                    Year.Items.Add(d)
                Next
                Month.SelectedIndex = Now.Month - 1
                Day.SelectedIndex = Now.Day - 1
                Year.SelectedIndex = (Now.Year) - 1997

                Dim str = "select * from enquiryby where enqid=" & Request.QueryString("id") & ""
                dt1 = db.fillReader1(str)
                dr = dt1.CreateDataReader()
                If dr.Read() Then
                    Txtmobno.Text = dr("contactno").ToString()
                    txtcontname.Text = dr("contactname").ToString()
                    txtcompName.Text = dr("companyname").ToString()
                    txtcontaddress.Text = dr("contactaddress").ToString()
                    DrpDivision.SelectedValue = dr("division").ToString()
                    txtEmailid.Text = dr("emailid").ToString()
                    txtenqfor.Text = dr("enquiryfor").ToString()
                    txtremarks.Text = dr("remarks").ToString()
                    Dim enqdt = dr("enqdate").ToString()
                    If enqdt <> "" Then
                        day.SelectedIndex = CDate(enqdt).Day - 1
                        month.SelectedIndex = CDate(enqdt).Month - 1
                        year.SelectedIndex = (CDate(enqdt).Year) - 1997
                    Else
                        day.SelectedValue = Now.Day - 1
                        month.SelectedValue = Now.Month - 1
                        year.SelectedValue = (Now.Year) - 2000
                    End If
                End If
                dr.Close()
                dt1.Clear()
                dt1.Dispose()
            End If
        End If
    End Sub

    Public Sub ShowEnquiries()
        Dim ds As New Data.DataSet
        Dim dt = Format(Now.Date, "dd-MMM-yyyy")
        db.qry = "select e.division,d.dname as dname from divisions as d,enquiryby as e where d.divisionid=e.division and convert(smalldatetime,enqdate)=convert(smalldatetime,'" & dt & "') and e.flg=0 and d.flg=0 group by division,dname"
        ds = db.fillDataSet()
        If ds.Tables(0).Rows.Count > 0 Then
            GVdivision.DataSource = ds
            GVdivision.DataBind()
        Else
            GVdivision.DataSource = Nothing
            GVdivision.DataBind()
        End If
        ds.Dispose()
    End Sub

    Protected Sub GVdivision_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVdivision.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim dt = Format(Now.Date, "dd-MMM-yyyy")
            Dim strId As String = GVdivision.DataKeys(e.Row.RowIndex).Value.ToString()
            Dim gv As GridView = CType(e.Row.FindControl("GVenquiry"), GridView)
            AddHandler gv.RowDataBound, AddressOf gv_RowDataBound
            Dim qry = "select * from enquiryby where division=" & strId & " and convert(smalldatetime,enqdate)=convert(smalldatetime,'" & dt & "')"
            dt1 = db.fillReader1(qry)
            dr1 = dt1.CreateDataReader()

            If dr1.HasRows Then
                gv.DataSource = dr1
                gv.DataBind()
            Else
                gv.DataSource = Nothing
                gv.DataBind()
            End If
            dr1.Close()
            dt1.Clear()
            dt1.Dispose()
        End If
    End Sub

    Protected Sub gv_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        pnlmain.Visible = True
        pnlprev.Visible = True
        pnlenq.Visible = False
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
    End Sub

    Protected Sub btnedit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnedit.Click
        pnlmain.Visible = True
        pnlprev.Visible = False
        pnlenq.Visible = False
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
            db.qry = "update enquiryby set contactno='" & lblcontactno1.Text & "',EmailId='" & lblmailid1.Text & "',EnquiryFor='" & lblenqfor1.Text & "',Division='" & DrpDivision.SelectedValue & "',companyname='" & lblcompname1.Text & "',contactname='" & lblcontname1.Text & "',contactAddress='" & lblcontaddress1.Text & "',Enqdate='" & lblenqdate1.Text & "',Remarks='" & lblremarks1.Text & "' where enqid=" & Request.QueryString("id") & ""
            db.executeQuery()
            clearData()
            pnlmain.Visible = False
            pnlprev.Visible = False
            pnlenq.Visible = True
            ShowEnquiries()
            lblmsg.Text = "Successful Update"
            lblmsg.Visible = True
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

    Protected Sub btndel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndel.Click
        db.qry = "delete from enquiryby where enqid=" & Request.QueryString("id") & ""
        db.executeQuery()

        pnlmain.Visible = False
        pnlprev.Visible = False
        pnlenq.Visible = True
        ShowEnquiries()
        lblmsg.Text = "Successful Delete"
        lblmsg.Visible = True
    End Sub
End Class