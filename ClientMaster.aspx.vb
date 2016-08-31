Imports System.Data

Partial Class Admin_ClientMaster
    Inherits System.Web.UI.Page
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String

    Public db As New general
    Dim rcount As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rcount = 0
        lblerror.Text = ""

        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        'Response.Redirect("sessionOut.aspx")
        'Else
        'db.setLayout(Session("userId"))
        'End If
        db.assignEvents(txtpincode, "fnum")
        If Not IsPostBack Then
            db.fillCombo(drpcomptype, "companyTypes", "CompanyType", "TypeId", " order by companyType")

            Dim d
            For Each d In db.mName
                month.Items.Add(d)
                drpm1.Items.Add(d)
                drpm2.Items.Add(d)
            Next
            For d = 1 To 31
                day.Items.Add(d)
                drpd1.Items.Add(d)
                drpd2.Items.Add(d)
            Next
            For d = 1960 To Now.Year
                year.Items.Add(d)
                drpy1.Items.Add(d)
                drpy2.Items.Add(d)
            Next
            month.SelectedIndex = Now.Month - 1
            day.SelectedIndex = Now.Day - 1
            year.SelectedIndex = (Now.Year) - 1960
            drpm1.SelectedIndex = Now.Month - 1
            drpd1.SelectedIndex = Now.Day - 1
            drpy1.SelectedIndex = (Now.Year) - 1960
            drpm2.SelectedIndex = Now.Month - 1
            drpd2.SelectedIndex = Now.Day - 1
            drpy2.SelectedIndex = (Now.Year) - 1960
            lblid.Text = ""

            If Request.QueryString("companyid") <> "" Then
                strqry1 = "select * from ClientMaster where companyid='" & Request.QueryString("companyid") & "'"
                dt1 = db.fillReader1(strqry1)
                dr = dt1.CreateDataReader()
                While dr.Read()
                    lblid.Text = dr("companyId").ToString()
                    txtCompanyName.Text = dr("companyname")
                    'txtCompanyName.Enabled = False
                    txtCompanyAbbreviation.Text = dr("companyAbbr")
                    txtCompanyAbbreviation.Enabled = False
                    drpcomptype.SelectedValue = dr("compType")
                    txtAddress.Text = dr("address")
                    txtpincode.Text = dr("pincode")
                    db.fillCombo(drpCountry, "country", "countryName", "countryid")
                    drpCountry.SelectedValue = dr("country")
                    'db.fillCombo(drpState, "state", "statename", "stateid")
                    db.fillCombo(drpState, "state", "statename", "stateid", " where countryid=" & drpCountry.SelectedValue)
                    'db.fillCombo(drpState, "state", "statename", "stateid", "where flag=0")
                    drpState.SelectedValue = dr("state")
                    db.fillCombo(drpCity, "city", "cityname", "cityid", " where stateid=" & drpState.SelectedValue & "")
                    'db.fillCombo(drpCity, "city", "cityname", "cityid")
                    drpCity.SelectedValue = dr("city")
                    txtTelephoneNo.Text = dr("telephoneno")
                    txtMobileNo.Text = dr("mobileno")
                    txtFaxNo.Text = dr("faxno")
                    txtPanNo.Text = dr("panno")
                    txtEmailId.Text = dr("emailid")
                    txttktEmailId.Text = dr("temailid")
                    txtWebSite.Text = dr("website")
                    txtContactP.Text = dr("contactP")
                    txtdegi.Text = dr("designation")
                    txtRegNo.Text = dr("regNo")
                    txtServiceTaxNo.Text = dr("ServiceTaxNo")
                    txtCSTNo.Text = dr("salestaxNo")
                    txtVatNo.Text = dr("vatNo")
                    txtECCNo.Text = dr("EccNo")
                    txtLSTNo.Text = dr("LstNo")
                    Dim cstdate As String = dr("cstDate").ToString()
                    Dim Lstdate As String = dr("LstDate").ToString()
                    Dim dor As String = dr("DOR").ToString()
                    If dor <> "" Then
                        day.SelectedIndex = CDate(dr("DOR")).Day - 1
                        month.SelectedIndex = CDate(dr("DOR")).Month - 1
                        year.SelectedIndex = (CDate(dr("DOR")).Year) - 1960
                    Else
                        day.SelectedValue = Now.Day - 1
                        month.SelectedValue = Now.Month - 1
                        year.SelectedValue = (Now.Year) - 2000
                    End If
                    If cstdate <> "" Then
                        drpd1.SelectedIndex = CDate(dr("cstDate")).Day - 1
                        drpm1.SelectedIndex = CDate(dr("cstDate")).Month - 1
                        drpy1.SelectedIndex = (CDate(dr("cstDate")).Year) - 1960
                    Else
                        drpd1.SelectedValue = Now.Day - 1
                        drpm1.SelectedValue = Now.Month - 1
                        drpy1.SelectedValue = (Now.Year) - 2000
                    End If
                    If Lstdate <> "" Then
                        drpd2.SelectedIndex = CDate(dr("LstDate")).Day - 1
                        drpm2.SelectedIndex = CDate(dr("LstDate")).Month - 1
                        drpy2.SelectedIndex = (CDate(dr("LstDate")).Year) - 1960
                    Else
                        drpd2.SelectedValue = Now.Day - 1
                        drpm2.SelectedValue = Now.Month - 1
                        drpy2.SelectedValue = (Now.Year) - 2000
                    End If
                End While
                dr.Close()
                dt1.Clear()
                dt1.Dispose()
                btnSubmit.Text = "Update"
              

            Else
                db.fillCombo(drpCountry, "country", "countryName", "countryid")
                drpCountry.SelectedValue = db.DefaultLocation("country", "countryid")
                db.fillCombo(drpState, "state", "statename", "stateid", "where countryid=" & drpCountry.SelectedValue)
                drpState.SelectedValue = db.DefaultLocation("state", "stateid")
                db.fillCombo(drpCity, "city", "cityname", "cityid", "where stateid=" & drpState.SelectedValue)
                drpCity.SelectedValue = db.DefaultLocation("city", "cityid")
            End If
        
        End If
        'btnSubmit.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim dor As String = day.SelectedValue & "-" & month.SelectedValue & "-" & year.SelectedValue
            Dim cstdate As String = drpd1.SelectedValue & "-" & drpm1.SelectedValue & "-" & drpy1.SelectedValue
            Dim lstdate As String = drpd2.SelectedValue & "-" & drpm2.SelectedValue & "-" & drpy2.SelectedValue
            If btnSubmit.Text = "Update" Then
                Dim imgname As String
                imgname = db.getFieldValue("clientMaster", "companyid", lblid.Text, "logo", True)
                If fupLogo.HasFile Then
                    imgname = "CLIENT" & lblid.Text & IO.Path.GetExtension(fupLogo.FileName)
                    fupLogo.SaveAs(Server.MapPath("~\logos\") & imgname)
                End If

                Dim address As String = db.initCap(txtAddress.Text)

                If txtPanNo.Text = "" Then txtPanNo.Text = "N/A"
                If txtMobileNo.Text = "" Then txtMobileNo.Text = "N/A"
                If txtFaxNo.Text = "" Then txtFaxNo.Text = "N/A"
                If txtRegNo.Text = "" Then txtRegNo.Text = "N/A"
                If txtServiceTaxNo.Text = "" Then txtServiceTaxNo.Text = "N/A"
                If txtCSTNo.Text = "" Then txtCSTNo.Text = "N/A"
                If txtVatNo.Text = "" Then txtVatNo.Text = "N/A"
                If txtWebSite.Text = "" Then txtWebSite.Text = "N/A"
                If txtCompanyAbbreviation.Text = "" Then txtCompanyAbbreviation.Text = "N/A"
                If txtContactP.Text = "" Then txtContactP.Text = "N/A"
                If txtdegi.Text = "" Then txtdegi.Text = "N/A"


                db.qry = "update ClientMaster set companyname='" & txtCompanyName.Text & "',contactP='" & txtContactP.Text & "',Designation='" & txtdegi.Text & "',address='" & address & "',pinCode=" & txtpincode.Text & ",country=" & drpCountry.SelectedValue & ",state=" & drpState.SelectedValue & ",city=" & drpCity.SelectedValue & ",TelephoneNo='" & txtTelephoneNo.Text & "',MobileNo='" & txtMobileNo.Text & "',FaxNo='" & txtFaxNo.Text & "',PanNo='" & UCase(txtPanNo.Text) & "',RegNo='" & UCase(txtRegNo.Text) & "',ServiceTaxNo='" & UCase(txtServiceTaxNo.Text) & "',salesTaxNo='" & UCase(txtCSTNo.Text) & "',vatNo='" & UCase(txtVatNo.Text) & "',emailId='" & txtEmailId.Text & "',temailId='" & txttktEmailId.Text & "',website='" & txtWebSite.Text & "',logo='" & imgname & "',dor='" & dor & "',compType=" & drpcomptype.SelectedValue & ",EccNo='" & txtECCNo.Text & "',LstNo='" & txtLSTNo.Text & "',LstDate='" & lstdate & "',CstDate='" & cstdate & "' where companyId=" & lblid.Text & ""
                db.executeQuery()

                lblerror.Text = ""
                btnSubmit.Text = "Preview"
                Session("clientcompid") = lblid.Text
                clearData()
                Response.Redirect("clientprintappdetails.aspx?c1=1&companyId=" & Session("clientcompid").ToString())
            Else
                If db.isExists("ClientMaster", "companyName", txtCompanyName.Text, False) = False Then
                    lblid.Text = db.getMaxId("ClientMaster", "companyid")

                    Dim address As String = db.initCap(txtAddress.Text)

                    If txtPanNo.Text = "" Then txtPanNo.Text = "N/A"
                    If txtMobileNo.Text = "" Then txtMobileNo.Text = "N/A"
                    If txtFaxNo.Text = "" Then txtFaxNo.Text = "N/A"
                    If txtRegNo.Text = "" Then txtRegNo.Text = "N/A"
                    If txtServiceTaxNo.Text = "" Then txtServiceTaxNo.Text = "N/A"
                    If txtCSTNo.Text = "" Then txtCSTNo.Text = "N/A"
                    If txtVatNo.Text = "" Then txtVatNo.Text = "N/A"
                    If txtWebSite.Text = "" Then txtWebSite.Text = "N/A"
                    If txtCompanyAbbreviation.Text = "" Then txtCompanyAbbreviation.Text = "N/A"
                    If txtContactP.Text = "" Then txtContactP.Text = "N/A"
                    If txtdegi.Text = "" Then txtdegi.Text = "N/A"

                    If txtpincode.Text = "" Then txtpincode.Text = 0

                    Dim imgname As String
                    If fupLogo.HasFile Then
                        imgname = "CLIENT" & lblid.Text & IO.Path.GetExtension(fupLogo.FileName)
                        fupLogo.SaveAs(Server.MapPath("~\logos\") & imgname)
                    Else
                        imgname = "CLIENT" & lblid.Text & ".gif"
                        IO.File.Copy(Server.MapPath("~\logos\blank.gif"), Server.MapPath("~\logos\") & imgname)
                    End If

                    db.qry = "insert into ClientMaster values(" & lblid.Text & ",'" & txtCompanyName.Text & "','" & UCase(txtCompanyAbbreviation.Text) & "','" & txtContactP.Text & "','" & txtdegi.Text & "','" & address & "'," & txtpincode.Text & "," & drpCountry.SelectedValue & "," & drpState.SelectedValue & "," & drpCity.SelectedValue & ",'" & txtTelephoneNo.Text & "','" & txtMobileNo.Text & "','" & txtFaxNo.Text & "','" & UCase(txtPanNo.Text) & "','" & UCase(txtRegNo.Text) & "','" & UCase(txtServiceTaxNo.Text) & "','" & UCase(txtCSTNo.Text) & "','" & UCase(txtVatNo.Text) & "','" & txtEmailId.Text & "','" & txttktEmailId.Text & "','" & txtWebSite.Text & "','" & imgname & "','" & dor & "'," & drpcomptype.SelectedValue & ",'" & txtECCNo.Text & "','" & txtLSTNo.Text & "','" & lstdate & "','" & cstdate & "',0)"
                    db.executeQuery()

                    'db.qry = "update ClientMaster set logo='" & imgname & "' where companyId=" & lblid.Text & ""
                    ' db.executeQuery()

                    lblerror.Text = ""
                    btnSubmit.Text = "Preview"
                    Session("clientcompid") = lblid.Text
                    clearData()
                    Response.Redirect("clientprintappdetails.aspx?companyId=" & Session("clientcompid").ToString())
                Else
                    lblerror.Text = "This Company are all ready exists"
                End If


            End If
        Catch ex As Exception
            lblerror.Text = ex.Message
        End Try
    End Sub

    Private Sub clearData()
        txtCompanyName.Text = ""
        txtCompanyAbbreviation.Text = ""
        txtAddress.Text = ""
        txtpincode.Text = ""
        db.fillCombo(drpCountry, "country", "countryName", "countryid")
        drpCountry.SelectedValue = db.DefaultLocation("country", "countryid")
        db.fillCombo(drpState, "state", "statename", "stateid", "where countryid=" & drpCountry.SelectedValue)
        drpState.SelectedValue = db.DefaultLocation("state", "stateid")
        db.fillCombo(drpCity, "city", "cityname", "cityid", "where stateid=" & drpState.SelectedValue)
        drpCity.SelectedValue = db.DefaultLocation("city", "cityid")
        txtTelephoneNo.Text = ""
        txtMobileNo.Text = ""
        txtFaxNo.Text = ""
        txtPanNo.Text = ""
        txtEmailId.Text = ""
        txttktEmailId.Text = ""
        txtWebSite.Text = ""
        txtContactP.Text = ""
        txtdegi.Text = ""
        txtRegNo.Text = ""
        txtServiceTaxNo.Text = ""
        txtCSTNo.Text = ""
        txtVatNo.Text = ""
        txtECCNo.Text = ""
        txtLSTNo.Text = ""
    End Sub

    Protected Sub drpCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpCountry.SelectedIndexChanged
        db.fillCombo(drpState, "state", "statename", "stateid", "where countryid=" & drpCountry.SelectedValue)
        db.fillCombo(drpCity, "city", "cityname", "cityid", "where 1=2")
    End Sub

    Protected Sub drpState_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpState.SelectedIndexChanged
        db.fillCombo(drpCity, "city", "cityname", "cityid", "where stateid=" & drpState.SelectedValue)
    End Sub



End Class
