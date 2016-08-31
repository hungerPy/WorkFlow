Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class Admin_companyMaster
    Inherits System.Web.UI.Page
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String
    Public db As New general
    Dim rcount As Integer
    Dim compid As Integer

    Dim conne As SqlConnection = New SqlConnection(ConfigurationManager.AppSettings("strConn").ToString())

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        rcount = 0
        If Not IsPostBack Then
            Session("cid") = "1"
                strqry1 = "select top 1 * from company"
                dt1 = db.fillReader1(strqry1)
            dr = dt1.CreateDataReader()
            db.fillCombo(drpcomptype, "companyTypes", "CompanyType", "TypeId", " order by companyType")
                While dr.Read()
                    txtCompanyName.Text = dr("companyname").ToString()
                    txtCompanyAbbreviation.Text = dr("companyabbr").ToString()
                    txtContactP.Text = dr("contactp").ToString()
                    fupLogoimg.Visible = True
                    fupLogoimg.ImageUrl = "~\logos\" & dr("logo").ToString()
                    txtimage.Text = dr("logo")
                    drpcomptype.SelectedValue = dr("CompType")
                    txtRegNo.Text = dr("regno").ToString()
                    txtTelephoneNo.Text = dr("telephoneno").ToString()
                    txtMobileNo.Text = dr("mobileno").ToString()
                    txtFaxNo.Text = dr("faxno").ToString()
                    txtEmailId.Text = dr("emailid").ToString()
                    txtWebSite.Text = dr("website").ToString()
                    txtContactP.Text = dr("contactP").ToString()
                    txtdate.Text = dr("dor").ToString()
                    txtRegNo.Text = dr("regNo").ToString()
                    lblid.Text = dr("companyid")
                    compid = dr("companyid")
                    'txtbankid.Text = dr("bid")
                End While
                dr.Close()
                dt1.Clear()
                dt1.Dispose()


        Else
            Session("cid") = "1"
                strqry1 = "select top 1 * from company"
                dt1 = db.fillReader1(strqry1)
                dr = dt1.CreateDataReader()
                While dr.Read()
                    txtCompanyName.Text = dr("companyname").ToString()
                    txtCompanyAbbreviation.Text = dr("companyabbr").ToString()
                    txtContactP.Text = dr("contactp").ToString()
                    fupLogoimg.Visible = True
                    fupLogoimg.ImageUrl = "~\logos\" & dr("logo").ToString()
                    txtimage.Text = dr("logo")
                    drpcomptype.SelectedValue = dr("CompType")
                    txtRegNo.Text = dr("regno").ToString()
                    txtTelephoneNo.Text = dr("telephoneno").ToString()
                    txtMobileNo.Text = dr("mobileno").ToString()
                    txtFaxNo.Text = dr("faxno").ToString()
                    txtEmailId.Text = dr("emailid").ToString()
                    txtWebSite.Text = dr("website").ToString()
                    txtContactP.Text = dr("contactP").ToString()
                    txtdate.Text = dr("dor").ToString()
                    txtRegNo.Text = dr("regNo").ToString()
                    lblid.Text = dr("companyid")
                    compid = dr("companyid")
                    'txtbankid.Text = dr("bid")

                End While
                dr.Close()
                dt1.Clear()
                dt1.Dispose()
        End If



    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim dor As String = txtdate.Text
            If "Update" = "Update" Then

                Dim imgname As String
                imgname = db.getFieldValue("company", "companyid", lblid.Text, "logo", True)
                If fupLogo.HasFile Then
                    imgname = "CMP" & lblid.Text & IO.Path.GetExtension(fupLogo.FileName)
                    fupLogo.SaveAs(Server.MapPath("~\logos\") & imgname)
                End If
                If txtTelephoneNo.Text = "" Then txtTelephoneNo.Text = "N/A"
                If txtMobileNo.Text = "" Then txtMobileNo.Text = "N/A"
                If txtFaxNo.Text = "" Then txtFaxNo.Text = "N/A"
                If txtRegNo.Text = "" Then txtRegNo.Text = "N/A"
                If txtEmailId.Text = "" Then txtEmailId.Text = "N/A"
                If txtWebSite.Text = "" Then txtWebSite.Text = "N/A"

                db.qry = "update company set companyname='" & txtCompanyName.Text & "',companyabbr='" & txtCompanyAbbreviation.Text & "',contactp='" & txtContactP.Text & "'" &
                "telephoneno='" & txtTelephoneNo.Text & "',mobileno='" & txtMobileNo.Text & "',faxno='" & txtFaxNo.Text & "',regno='" & txtRegNo.Text & "',emailid='" & txtEmailId.Text & "',website='" & txtWebSite.Text & "',logo='" & imgname & "',dor='" & dor & "',comptype=" & drpcomptype.SelectedValue & ", where companyid=" & lblid.Text & ""
                db.executeQuery()

                clearData()
                If Session("cid") <> "" Then
                    Response.Redirect("compprintappdetails.aspx?companyId=" & Session("cid").ToString())

                Else
                    Response.Redirect("compprintappdetails.aspx?companyId=" & Session("companyId").ToString())
                End If
               
            End If
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub clearData()
        txtCompanyName.Text = ""
        txtCompanyAbbreviation.Text = ""
        txtTelephoneNo.Text = ""
        txtMobileNo.Text = ""
        txtFaxNo.Text = ""
        txtEmailId.Text = ""
        txtWebSite.Text = ""
        txtContactP.Text = ""
        txtRegNo.Text = ""
    End Sub

End Class
