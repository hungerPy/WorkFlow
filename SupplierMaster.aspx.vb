Imports System.Data

Partial Class Admin_SupplierMaster
    Inherits System.Web.UI.Page
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String

    Public db As New general
    Dim rcount As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rcount = 0
        lblError.Text = ""

        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        'db.setLayout(Session("userId"))
        'End If
        'db.assignEvents(txtTelephoneNo, "fnum")
        'db.assignEvents(txtMobileNo, "fnum")
        'db.assignEvents(txtFaxNo, "fnum")
        db.assignEvents(txtpincode, "fnum")
        If Not IsPostBack Then
            'db.fillCombo(drpCompanyName, "company", "CompanyName", "CompanyAbbr")
            db.fillCombo(drpbankname, "banks", "bankname", "bankcode", " order by bankname")
            db.fillCombo(drpcategory, "categorymaster", "category", "cid", " where flg=0 order by category")
            db.fillCheckList(chkSubCat, "subcategorymaster", "subcategory", "scid", " where 1=2")
            db.fillCombo(drpcomptype, "companyTypes", "CompanyType", "TypeId", " order by companyType")
            db.fillCombo(drpbankbranch, "bankbranch", "BbName", "BbCode", " where 1=2")
            db.fillCombo(drpbank, "banks", "bankname", "bankcode", " order by bankname")
            db.qry = "delete from temperary "
            db.executeQuery()
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
                strqry1 = "select * from supplierMaster where companyid='" & Request.QueryString("companyid") & "'"
                dt1 = db.fillReader1(strqry1)
                dr = dt1.CreateDataReader()

                While dr.Read()
                    lblid.Text = dr("companyId").ToString()
                    txtCompanyName.Text = dr("companyname")
                    txtaccname.Text = dr("companyname")
                    txtCompanyAbbreviation.Text = dr("companyAbbr")
                    drpcomptype.SelectedValue = dr("compType")
                    'If db.isExists("director", "companyname", txtCompanyName.Text) Then
                    '    txtCompanyName.Enabled = False
                    '    txtCompanyAbbreviation.Enabled = False
                    'End If
                    'If db.isExists("director", "companyname", txtCompanyAbbreviation.Text) Then
                    '    txtCompanyName.Enabled = False
                    '    txtCompanyAbbreviation.Enabled = False
                    'End If
                    txtAddress.Text = dr("address")
                    txtpincode.Text = dr("pincode")
                    db.fillCombo(drpCountry, "country", "countryName", "countryid")
                    drpCountry.SelectedValue = dr("country")
                    db.fillCombo(drpState, "state", "statename", "stateid", "where countryid=" & drpCountry.SelectedValue)
                    'db.fillCombo(drpState, "state", "statename", "stateid", "where flag=0")
                    drpState.SelectedValue = dr("state")
                    'db.fillCombo(drpCity, "city", "cityname", "cityid", "where stateid=" & drpState.SelectedValue)
                    db.fillCombo(drpCity, "city", "cityname", "cityid")
                    drpCity.SelectedValue = dr("city")
                    txtTelephoneNo.Text = dr("telephoneno")
                    txtMobileNo.Text = dr("mobileno")
                    txtFaxNo.Text = dr("faxno")
                    txtPanNo.Text = dr("panno")
                    txtEmailId.Text = dr("emailid")
                    txtWebSite.Text = dr("website")
                    txtContactP.Text = dr("contactP")
                    txtdegi.Text = dr("designation")
                    txtRegNo.Text = dr("regNo")
                    txtServiceTaxNo.Text = dr("ServiceTaxNo")
                    txtCSTNo.Text = dr("salestaxNo")
                    txtVatNo.Text = dr("vatNo")
                    txtECCNo.Text = dr("EccNo")
                    txtLSTNo.Text = dr("LstNo")
                    drpcategory.SelectedValue = dr("Category")
                    db.fillCheckList(chkSubCat, "subcategorymaster", "subcategory", "scid", " where cid=" & dr("Category") & " order by subcategory")
                    Dim scatid As String = dr("SubCategory").ToString()
                    lblsubcat.Visible = True
                    chkSubCat.Visible = True
                    Dim scid() As String = scatid.Split(",")
                    Dim tm, item
                    For Each tm In chkSubCat.Items
                        For Each item In scid
                            If tm.value = item Then
                                tm.selected = True
                            End If
                        Next
                    Next

                    Dim contag = dr("ContAggriment")
                    If contag = False Then
                        chkcontagg.Checked = True
                    Else
                        chkcontagg.Checked = False
                    End If
                    If chkcontagg.Checked = True Then
                        lblcontprd.Visible = True
                        lblpaycrd.Visible = True
                        txtcontprd.Visible = True
                        txtpaycrd.Visible = True
                    Else
                        lblcontprd.Visible = False
                        lblpaycrd.Visible = False
                        txtcontprd.Visible = False
                        txtpaycrd.Visible = False
                    End If
                    txtcontprd.Text = dr("contperiod")
                    txtpaycrd.Text = dr("paycreadits")
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
                btnSubmit.Text = "Update"
                dr.Close()
                dt1.Clear()
                dt1.Dispose()

                strqry1 = "select * from bankdetailsupplier where compempid='" & txtCompanyAbbreviation.Text & "'"
                dt1 = db.fillReader1(strqry1)
                dr = dt1.CreateDataReader()
                While dr.Read()
                    db.qry = "insert into temperary values(" & db.getMaxId("temperary", "id") & ",'" & dr("bankcode").ToString() & "','" & dr("compempid").ToString() & "','" & dr("accName").ToString() & "','" & dr("accno").ToString() & "','" & dr("bankaddress").ToString() & "','" & dr("acctype").ToString() & "','" & dr("swiftcode").ToString() & "','" & dr("micrno").ToString() & "','" & dr("ifscno").ToString() & "','" & dr("branchcode").ToString() & "','','','','','','" & Session.SessionID & "')"
                    db.executeQuery()
                End While
                showValues()

                dr.Close()
                dt1.Clear()
                dt1.Dispose()
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
            If Request.QueryString("companyid") = "" Then
                If db.isExists("supplierMaster", "companyabbr", txtCompanyAbbreviation.Text) Then
                    lblError.Text = "* Duplicate Company/Vendor Abbreviation."
                    Exit Sub
                End If
            End If
            Dim dor As String = day.SelectedValue & "-" & month.SelectedValue & "-" & year.SelectedValue
            Dim cstdate As String = drpd1.SelectedValue & "-" & drpm1.SelectedValue & "-" & drpy1.SelectedValue
            Dim lstdate As String = drpd2.SelectedValue & "-" & drpm2.SelectedValue & "-" & drpy2.SelectedValue
            If btnSubmit.Text = "Update" Then
                Dim imgname As String
                imgname = db.getFieldValue("supplierMaster", "companyid", lblid.Text, "logo", True)
                If fupLogo.HasFile Then
                    imgname = "SUPPLIER" & lblid.Text & IO.Path.GetExtension(fupLogo.FileName)
                    fupLogo.SaveAs(Server.MapPath("~\logos\") & imgname)
                End If

                'db.qry = "Delete from company where companyid= '" & lblid.Text & "'"
                'db.executeQuery()

                'Dim compname As String = db.initCap(txtCompanyName.Text)
                'Dim name As String = db.initCap(txtContactP.Text)
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

                Dim tm As Object
                Dim scid As String

                For Each tm In chkSubCat.Items
                    If tm.selected = True Then
                        scid = scid & "," & tm.value
                    End If
                Next

                If scid <> "" Then
                    scid = scid.Substring(1, scid.Length - 1)
                End If
                Dim chkvalue
                If chkcontagg.Checked = True Then
                    chkvalue = 0
                Else
                    chkvalue = 1
                End If
                'db.qry = "insert into company values(" & lblid.Text & ",'" & txtCompanyName.Text & "','" & UCase(txtCompanyAbbreviation.Text) & "','" & txtContactP.Text & "','" & txtdegi.Text & "','" & address & "'," & txtpincode.Text & "," & drpCountry.SelectedValue & "," & drpState.SelectedValue & "," & drpCity.SelectedValue & ",'" & txtTelephoneNo.Text & "','" & txtMobileNo.Text & "','" & txtFaxNo.Text & "','" & UCase(txtPanNo.Text) & "','" & UCase(txtRegNo.Text) & "','" & UCase(txtServiceTaxNo.Text) & "','" & UCase(txtCSTNo.Text) & "','" & UCase(txtVatNo.Text) & "','" & txtEmailId.Text & "','" & txtWebSite.Text & "','" & imgname & "','" & dor & "',0)"
                db.qry = "update SupplierMaster set companyName='" & txtCompanyName.Text & "',companyAbbr='" & UCase(txtCompanyAbbreviation.Text) & "',contactP='" & txtContactP.Text & "',Designation='" & txtdegi.Text & "',address='" & address & "',pinCode=" & txtpincode.Text & ",country=" & drpCountry.SelectedValue & ",state=" & drpState.SelectedValue & ",city=" & drpCity.SelectedValue & ",TelephoneNo='" & txtTelephoneNo.Text & "',MobileNo='" & txtMobileNo.Text & "',FaxNo='" & txtFaxNo.Text & "',PanNo='" & UCase(txtPanNo.Text) & "',RegNo='" & UCase(txtRegNo.Text) & "',ServiceTaxNo='" & UCase(txtServiceTaxNo.Text) & "',salesTaxNo='" & UCase(txtCSTNo.Text) & "',vatNo='" & UCase(txtVatNo.Text) & "',emailId='" & txtEmailId.Text & "',website='" & txtWebSite.Text & "',logo='" & imgname & "',dor='" & dor & "',compType=" & drpcomptype.SelectedValue & ",EccNo='" & txtECCNo.Text & "',LstNo='" & txtLSTNo.Text & "',LstDate='" & lstdate & "',CstDate='" & cstdate & "',category=" & drpcategory.SelectedValue & ",subcategory='" & scid & "',contAggriment=" & chkvalue & ",contperiod='" & txtcontprd.Text & "',paycreadits='" & txtpaycrd.Text & "' where companyId=" & lblid.Text & ""
                db.executeQuery()

                db.qry = "delete from bankdetailsupplier where compempid='" & txtCompanyAbbreviation.Text & "'"
                db.executeQuery()

                Dim Str As String = "select * from temperary where sessionId='" & Session.SessionID & "'"
                dt1 = db.fillReader1(Str)
                dr = dt1.CreateDataReader()
                While dr.Read()
                    db.qry = "insert into bankdetailsupplier values(" & db.getMaxId("bankdetails", "bid") & ",'" & dr("f2").ToString() & "','" & dr("f3").ToString() & "','" & dr("f4").ToString() & "','" & dr("f6").ToString() & "','" & dr("f1").ToString() & "','" & dr("f5").ToString & "','" & dr("f8").ToString() & "','" & dr("f9").ToString() & "','" & dr("f7").ToString() & "',0,'" & Date.Now.ToString("dd-MMM-yyyy") & "','" & dr("f10").ToString() & "')"
                    db.executeQuery()
                End While
                lblError.Text = ""
                btnSubmit.Text = "Preview"
                Session("companyId") = lblid.Text
                clearData()
                dr.Close()
                dt1.Clear()
                dt1.Dispose()
                Response.Redirect("supplierprintappdetails.aspx?companyId=" & Session("companyId").ToString())
            Else
                If db.isExists("supplierMaster", "companyName", txtCompanyName.Text, False, " and panNo='" & txtPanNo.Text & "'") = False Then
                    lblid.Text = db.getMaxId("supplierMaster", "companyid")

                    'Dim compname As String = db.initCap()
                    'Dim name As String = db.initCap(txtContactP.Text)
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

                    Dim tm As Object
                    Dim scid As String

                    For Each tm In chkSubCat.Items
                        If tm.selected = True Then
                            scid = scid & "," & tm.value
                        End If
                    Next

                    If scid <> "" Then
                        scid = scid.Substring(1, scid.Length - 1)
                    End If
                    Dim chkvalue
                    If chkcontagg.Checked = True Then
                        chkvalue = 0
                        If txtcontprd.Text = "" Then txtcontprd.Text = "N/A"
                        If txtpaycrd.Text = "" Then txtpaycrd.Text = "N/A"
                    Else
                        chkvalue = 1
                        If txtcontprd.Text = "" Then txtcontprd.Text = "N/A"
                        If txtpaycrd.Text = "" Then txtpaycrd.Text = "N/A"
                    End If

                    db.qry = "delete from bankdetailsupplier where compempid='" & txtCompanyAbbreviation.Text & "'"
                    db.executeQuery()

                    db.qry = "insert into suppliermaster values(" & lblid.Text & ",'" & txtCompanyName.Text & "','" & UCase(txtCompanyAbbreviation.Text) & "','" & txtContactP.Text & "','" & txtdegi.Text & "','" & address & "'," & txtpincode.Text & "," & drpCountry.SelectedValue & "," & drpState.SelectedValue & "," & drpCity.SelectedValue & ",'" & txtTelephoneNo.Text & "','" & txtMobileNo.Text & "','" & txtFaxNo.Text & "','" & UCase(txtPanNo.Text) & "','" & UCase(txtRegNo.Text) & "','" & UCase(txtServiceTaxNo.Text) & "','" & UCase(txtCSTNo.Text) & "','" & UCase(txtVatNo.Text) & "','" & txtEmailId.Text & "','" & txtWebSite.Text & "','','" & dor & "'," & drpcomptype.SelectedValue & ",'" & txtECCNo.Text & "','" & txtLSTNo.Text & "','" & lstdate & "','" & cstdate & "'," & drpcategory.SelectedValue & ",'" & scid & "'," & chkvalue & ",'" & txtcontprd.Text & "','" & txtpaycrd.Text & "',0)"
                    db.executeQuery()

                    Dim imgname As String
                    If fupLogo.HasFile Then
                        imgname = "SUPPLIER" & lblid.Text & IO.Path.GetExtension(fupLogo.FileName)
                        fupLogo.SaveAs(Server.MapPath("~\logos\") & imgname)
                    Else
                        imgname = "SUPPLIER" & lblid.Text & ".gif"
                        IO.File.Copy(Server.MapPath("~\logos\blank.gif"), Server.MapPath("~\logos\") & imgname)
                    End If

                    db.qry = "update suppliermaster set logo='" & imgname & "' where companyid=" & lblid.Text & " "
                    db.executeQuery()

                    Dim Str As String = "select * from temperary where sessionId='" & Session.SessionID & "'"
                    dt1 = db.fillReader1(Str)
                    dr = dt1.CreateDataReader()
                    While dr.Read()
                        db.qry = "insert into bankdetailsupplier values(" & db.getMaxId("bankdetails", "bid") & ",'" & dr("f2").ToString() & "','" & dr("f3").ToString() & "','" & dr("f4").ToString() & "','" & dr("f6").ToString() & "','" & dr("f1").ToString() & "','" & dr("f5").ToString & "','" & dr("f8").ToString() & "','" & dr("f9").ToString() & "','" & dr("f7").ToString() & "',0,'" & Date.Now.ToString("dd-MMM-yyyy") & "','" & dr("f10").ToString() & "')"
                        db.executeQuery()
                    End While
                    lblError.Text = ""
                    btnSubmit.Text = "Preview"
                    Session("companyId") = lblid.Text
                    clearData()
                    dr.Close()
                    dt1.Clear()
                    dt1.Dispose()

                    Response.Redirect("supplierprintappdetails.aspx?companyId=" & Session("companyId").ToString())
                Else
                    lblError.Text = "This Company are all ready exists"
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
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
        txtWebSite.Text = ""
        txtContactP.Text = ""
        txtdegi.Text = ""
        txtRegNo.Text = ""
        txtServiceTaxNo.Text = ""
        txtCSTNo.Text = ""
        txtVatNo.Text = ""
        txtECCNo.Text = ""
        txtLSTNo.Text = ""
        drpcategory.SelectedValue = "0"
        chkcontagg.Checked = False
        txtcontprd.Text = ""
        txtpaycrd.Text = ""
    End Sub

    Protected Sub drpCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpCountry.SelectedIndexChanged
        db.fillCombo(drpState, "state", "statename", "stateid", "where countryid=" & drpCountry.SelectedValue)
        db.fillCombo(drpCity, "city", "cityname", "cityid", "where 1=2")
    End Sub

    Protected Sub drpState_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpState.SelectedIndexChanged
        db.fillCombo(drpCity, "city", "cityname", "cityid", "where stateid=" & drpState.SelectedValue)
    End Sub

    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Dim name As String = db.initCap(txtaccname.Text)
        Dim address As String = db.initCap(txtbankaddress.Text)

        If btnadd.Text = "Update" Then
            db.qry = "update temperary set f1='" & drpbankname.SelectedValue & "',f3='" & name & "',f4='" & txtaccno.Text & "',f5='" & address & "',f6='" & drpacctype.SelectedValue & "',f7='" & UCase(txtswiftcode.Text) & "',f8='" & UCase(txtmicrno.Text) & "',f9='" & UCase(txtifscno.Text) & "',f10='" & drpbankbranch.SelectedValue & "' where id=" & txtid.Text & ""
            If txtid.Text <> "" Then
                Dim e1 As New GridViewCancelEditEventArgs(Convert.ToInt32(txtid.Text))
                gvtemp_RowCancelingEdit(sender, e1)
            End If
            txtid.Text = ""
        Else
            db.qry = "insert into temperary values(" & db.getMaxId("temperary", "id") & ",'" & drpbankname.SelectedValue & "','" & txtCompanyAbbreviation.Text & "','" & name & "','" & txtaccno.Text & "','" & address & "','" & drpacctype.SelectedValue & "','" & UCase(txtswiftcode.Text) & "','" & UCase(txtmicrno.Text) & "','" & UCase(txtifscno.Text) & "','" & drpbankbranch.SelectedValue & "','','','','','','" & Session.SessionID & "')"
        End If
        db.executeQuery()
        showValues()
        ClearValues()
        btnadd.Text = "Add Bank"

    End Sub

    Protected Sub gvtemp_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvtemp.RowCancelingEdit
        gvtemp.EditIndex = -1
        txtaccname.Text = ""
        txtaccno.Text = ""
        txtbankaddress.Text = ""
        txtswiftcode.Text = ""
        txtmicrno.Text = ""
        txtifscno.Text = ""
        drpacctype.SelectedValue = "Saving"
        drpbankname.SelectedValue = "0"
        drpbankbranch.SelectedValue = "0"
        showValues()
    End Sub

    Protected Sub gvtemp_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvtemp.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim str As String = DataBinder.Eval(e.Row.DataItem, "f1").ToString()
            e.Row.Cells(2).Text = db.getFieldValue("banks", "bankcode", str, "bankname", False)
        End If
    End Sub

    Protected Sub gvtemp_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvtemp.RowDeleting
        Dim Skey As String = gvtemp.DataKeys(e.RowIndex).Value
        db.qry = "delete from temperary where id=" & Skey & " and sessionId='" & Session.SessionID & "'"
        db.executeQuery()
        showValues()
    End Sub

    Private Sub showValues()
        Dim str As String = "select * from temperary where sessionId='" & Session.SessionID & "'"
        dt1 = db.fillReader1(str)
        dr = dt1.CreateDataReader()
        If dr.HasRows Then
            gvtemp.DataSource = dr
            gvtemp.DataBind()
        Else
            gvtemp.DataSource = Nothing
            gvtemp.DataBind()
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Private Sub ClearValues()
        txtaccname.Text = ""
        txtaccno.Text = ""
        txtbankaddress.Text = ""
        txtifscno.Text = ""
        txtmicrno.Text = ""
        txtswiftcode.Text = ""
        drpbankname.SelectedValue = 0
    End Sub

    Protected Sub drpcategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpcategory.SelectedIndexChanged
        If drpcategory.SelectedValue <> "0" Then
            db.fillCheckList(chkSubCat, "subcategorymaster", "subcategory", "scid", " where cid=" & drpcategory.SelectedValue & " order by subcategory")
            lblsubcat.Visible = True
            chkSubCat.Visible = True
            lblmsg.Visible = False
        Else
            lblsubcat.Visible = False
            chkSubCat.Visible = False
            lblmsg.Visible = False
        End If

    End Sub

    Protected Sub btncat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncat.Click
        lblshowcat.Visible = True
        txtCatName.Visible = True
        btnaddcat.Visible = True
        lblshowsubcat.Visible = False
        txtsubcatname.Visible = False
        btnaddsubcat.Visible = False
    End Sub

    Protected Sub btnaddcat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaddcat.Click
        Try
            Dim maxId = db.getMaxId("CategoryMaster", "CId")
            If Not db.isExists("CategoryMaster", "category", txtCatName.Text, False, " and CId<>" & maxId & "") Then
                If txtCatName.Text = "" Then
                    lblmsg.Text = "Please enter category name"
                    lblmsg.Visible = True
                    lblshowcat.Visible = True
                    txtCatName.Visible = True
                    btnaddcat.Visible = True
                    txtCatName.Focus()
                Else
                    db.qry = "delete from CategoryMaster where CId=" & maxId
                    db.executeQuery()

                    db.qry = "insert into CategoryMaster values(" & maxId & ",'" & txtCatName.Text & "',0)"
                    db.executeQuery()
                    txtCatName.Text = ""
                    lblmsg.Text = "Successful Add in Supplier Type"
                    lblmsg.Visible = True
                    lblshowcat.Visible = False
                    txtCatName.Visible = False
                    btnaddcat.Visible = False
                    db.fillCombo(drpcategory, "categorymaster", "category", "cid", " where flg=0 order by category")
                End If
            Else
                lblshowcat.Visible = True
                txtCatName.Visible = True
                lblmsg.Visible = True
                btnaddcat.Visible = True
                txtCatName.Focus()
                lblmsg.Text = "Already Exists!!!"
            End If

        Catch ex As Exception
            lblmsg.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnsubcat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubcat.Click
        lblshowsubcat.Visible = True
        txtsubcatname.Visible = True
        btnaddsubcat.Visible = True
        lblshowcat.Visible = False
        txtCatName.Visible = False
        btnaddcat.Visible = False
    End Sub

    Protected Sub btnaddsubcat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaddsubcat.Click
        Try
            If drpcategory.SelectedValue <> 0 Then
                Dim maxId = db.getMaxId("SubCategoryMaster", "SCId")
                If Not db.isExists("SubCategoryMaster", "subcategory", txtsubcatname.Text, False, " and SCId<> " & maxId) Then
                    If txtsubcatname.Text = "" Then
                        lblmsg.Text = "Please enter sub category name"
                        lblmsg.Visible = True
                        lblshowsubcat.Visible = True
                        txtsubcatname.Visible = True
                        txtsubcatname.Focus()
                    Else
                        db.qry = "delete from SubCategoryMaster where SCId=" & maxId
                        db.executeQuery()
                        db.qry = "insert into SubCategoryMaster values(" & maxId & "," & drpcategory.SelectedValue & ",'" & txtsubcatname.Text & "',0)"
                        db.executeQuery()
                        txtsubcatname.Text = ""
                        lblmsg.Text = "Successful Add in Sub Category"
                        lblmsg.Visible = True
                        lblshowsubcat.Visible = False
                        txtsubcatname.Visible = False
                        btnaddsubcat.Visible = False
                        db.fillCheckList(chkSubCat, "subcategorymaster", "subcategory", "scid", " where cid=" & drpcategory.SelectedValue & " order by subcategory")
                    End If
                Else
                    lblshowsubcat.Visible = True
                    txtsubcatname.Visible = True
                    lblmsg.Visible = True
                    btnaddsubcat.Visible = True
                    txtsubcatname.Focus()
                    lblmsg.Text = "Already Exists!!!"
                End If
            Else
                lblmsg.Text = "Please select supplier type."
                drpcategory.Focus()
                lblshowsubcat.Visible = True
                txtsubcatname.Visible = True
                lblmsg.Visible = True
                btnaddsubcat.Visible = True
            End If
        Catch ex As Exception
            lblmsg.Text = ex.Message
        End Try

    End Sub

    Protected Sub chkcontagg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkcontagg.CheckedChanged
        If chkcontagg.Checked = True Then
            lblcontprd.Visible = True
            lblpaycrd.Visible = True
            txtcontprd.Visible = True
            txtpaycrd.Visible = True
        Else
            lblcontprd.Visible = False
            lblpaycrd.Visible = False
            txtcontprd.Visible = False
            txtpaycrd.Visible = False
        End If
    End Sub

    Protected Sub drpbankname_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpbankname.SelectedIndexChanged
        If drpbankname.SelectedValue <> "0" Then
            db.fillCombo(drpbankbranch, "bankbranch", "BbName", "BbCode", " where bankcode='" & drpbankname.SelectedValue & "'")
        Else
            db.fillCombo(drpbankbranch, "bankbranch", "BbName", "BbCode", " where 1=2")
        End If
    End Sub

    Protected Sub btnbranch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbranch.Click
        pnlmain.Visible = False
        pnlsubcat.Visible = True
        drpbank.SelectedValue = drpbankname.SelectedValue
    End Sub


    Protected Sub btnSSSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSSSubmit.Click
        Try
            If Not db.isExists("BankBranch", "BbCode", txtBbCode.Text, False) Then
                If txtBbCode.Text = "" And txtBbName.Text = "" Then
                    lblSSError.Text = "Please enter Branch Code and Branch Name"
                    pnlmain.Visible = False
                    pnlsubcat.Visible = True
                Else
                    db.qry = "delete from BankBranch where BbCode=" & txtBbCode.Text
                    db.executeQuery()
                    db.qry = "insert into BankBranch values('" & drpbank.SelectedValue & "','" & txtBbCode.Text & "','" & txtBbName.Text & "')"
                    db.executeQuery()
                    txtBbCode.Text = ""
                    txtBbName.Text = ""
                    lblSSError.Text = "Successful Inserted"
                    pnlmain.Visible = False
                    pnlsubcat.Visible = True
                End If
            Else
                pnlmain.Visible = False
                pnlsubcat.Visible = True
                lblSSError.Text = "Already Exists!!!"
            End If

        Catch ex As Exception
            lblSSError.Text = ex.Message
            pnlmain.Visible = False
            pnlsubcat.Visible = True
        End Try
    End Sub

    Protected Sub btnSSback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSSback.Click
        pnlmain.Visible = True
        pnlsubcat.Visible = False
        db.fillCombo(drpbankbranch, "bankbranch", "BbName", "BbCode", " where bankcode='" & drpbankname.SelectedValue & "'")
    End Sub

    Protected Sub gvtemp_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvtemp.RowEditing
        gvtemp.EditIndex = e.NewEditIndex
        Dim strId As String = gvtemp.DataKeys(e.NewEditIndex).Value.ToString()
        showValues()
        Dim str As String = "select * from temperary where sessionId='" & Session.SessionID & "'"
        dt1 = db.fillReader1(str)
        dr = dt1.CreateDataReader()
        If dr.Read Then
            txtid.Text = dr("id")
            txtaccname.Text = dr("f3")
            txtaccno.Text = dr("f4")
            txtbankaddress.Text = dr("f5")
            txtswiftcode.Text = dr("f7")
            txtmicrno.Text = dr("f8")
            txtifscno.Text = dr("f9")
            drpacctype.SelectedValue = dr("f6")
            drpbankname.SelectedValue = dr("f1")
            db.fillCombo(drpbankbranch, "bankbranch", "BbName", "BbCode", " where bankcode='" & drpbankname.SelectedValue & "'")
            drpbankbranch.SelectedValue = dr("f10")
        End If
        btnadd.Text = "Update"


        dr.Close()
        dt1.Clear()
        dt1.Dispose()

    End Sub
End Class
