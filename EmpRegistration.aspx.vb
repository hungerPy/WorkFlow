Imports System.Globalization
Imports System.Data

Partial Class EmpRegistration
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("companyname") = "" Then
            Response.Redirect("companyname.aspx")
        End If
        If Not IsPostBack Then

            txtempno.Text = ""
            lblcompnyname.Text = Session("companyname").ToString()


            Dim compcode As String = db.getFieldValue("company", "companyname", lblcompnyname.Text, "companyid")

            db.fillCombo(DrpDivision, "Divisions", "DName", "divisionid", " order by dname ")

            Dim dirname As String = db.isExists("director", "dirname", "dirname", True) = True
            If dirname = False Then
                db.fillCombo(drprepotTo, "company", "contactp", "contactp", " where 1=1 order by contactp")
            Else
                db.fillCombo(drprepotTo, "director", "dirname+' , '+designation", "Ids", " where 1=1 order by dirname")
            End If


            db.fillCombo(drpempdegi, "Designation", "Designation", "Designation", " order by prty")
            db.qry = "delete from temperary "
            db.executeQuery()

            lblerror.Text = ""
            lblid.Text = ""
            If Request.QueryString("empid") <> "" Then
                strqry1 = "select * from Director where empid=" & Request.QueryString("empid") & ""
                dt1 = db.fillReader1(strqry1)
                dr = dt1.CreateDataReader()
                If dr.Read() Then
                    lblid.Text = dr("empid").ToString()
                    txtempno.Text = dr("empno").ToString()
                    txtfirstname.Text = dr("dirname").ToString()
                    rdbsex.SelectedValue = dr("sex")
                    txtqualification.Text = dr("qualification").ToString()
                    txtContactNo.Text = dr("contactNo").ToString()
                    txtEmailid.Text = dr("EmailId").ToString()
                    txtdate.Text = dr("doj").ToString()
                    db.fillCombo(DrpDivision, "Divisions", "DName", "divisionid", " order by dname")
                    DrpDivision.SelectedValue = dr("division")
                    drpType.SelectedValue = dr("emptype")
                    db.fillCombo(drpempdegi, "Designation", "Designation", "Designation", "order by prty")
                    drpempdegi.SelectedValue = dr("Designation")
                    db.fillCombo(drprepotTo, "director", "dirname+' ('+designation+')'", "Ids", " where companyname='" & dr("companyname").ToString() & "' and dinno<>'0' or empno<>'0'")
                    drprepotTo.SelectedItem.Text = dr("reportTo").ToString()
                    txtcompmailid.Text = dr("compmailid").ToString()

                    lblcompnyname.Text = dr("companyname").ToString()
                    Dim compabbr = db.getFieldValue("company", "companyName", dr("companyname").ToString(), "CompanyAbbr", False)


                    drpempdegi.SelectedValue = dr("Designation")
                    drpType.SelectedValue = dr("empType").ToString()


                    txtsalary.Text = dr("salary").ToString()

                    Dim dobstr As String = dr("DOB").ToString()
                    Dim doj As String = dr("DOJ").ToString()

                    If dr("sex").ToString() <> "" Then
                        rdbsex.SelectedValue = dr("sex")
                    End If

                    dr.Close()
                    dt1.Clear()
                    dt1.Dispose()
                End If

                strqry1 = "select * from bankdetails where compempid='" & txtempno.Text & "'"
                dt1 = db.fillReader1(strqry1)
                dr = dt1.CreateDataReader()
                While dr.Read()
                    db.qry = "insert into temperary values(" & db.getMaxId("temperary", "id") & ",'" & dr("bankcode").ToString() & "','" & dr("compempid").ToString() & "','" & dr("accName").ToString() & "','" & dr("accno").ToString() & "','" & dr("bankaddress").ToString() & "','" & dr("acctype").ToString() & "','" & dr("swiftcode").ToString() & "','" & dr("micrno").ToString() & "','" & dr("ifscno").ToString() & "','" & dr("branchcode").ToString() & "','','','','','','" & Session.SessionID & "')"
                    db.executeQuery()
                End While
                dr.Close()
                dt1.Clear()
                dt1.Dispose()
            Else
            End If
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim photoID As Int32
            Dim imgNameupdt As String = ""
            Dim appsignupdt As String = ""
            Dim passimsupdt As String = ""
            Dim empimage As String = ""
            Dim appsign As String = ""
            Dim passimg As String = ""

            Dim doj As String = txtdate.Text

            If btnSubmit.Text = "Update" Then

                Dim ids = db.getFieldValue("director", "empid", lblid.Text, "ids", True)
                Dim empno = db.getFieldValue("director", "empid", lblid.Text, "empno")

                imgNameupdt = db.getFieldValue("director", "empid", lblid.Text, "photo", True)
                If fupphoto.HasFile Then
                    photoID = ids
                    imgNameupdt = "Pphoto" & photoID & IO.Path.GetExtension(fupphoto.FileName)
                    fupphoto.SaveAs(Server.MapPath("~/Photos") & "/" & imgNameupdt)
                End If
                appsignupdt = db.getFieldValue("director", "empid", lblid.Text, "thumbsign", True)
              


                If fupphoto.HasFile Then
                    photoID = ids
                    Dim ext As String = System.IO.Path.GetExtension(fupphoto.FileName)
                    If (ext = ".jpg" Or ext = ".jpeg" Or ext = ".gif" Or ext = ".tif" Or ext = ".png") Then
                        empimage = "Pphoto" & photoID & IO.Path.GetExtension(fupphoto.FileName)
                        fupphoto.SaveAs(Server.MapPath("~/Photos") & "/" & empimage)
                    Else
                        ClientScript.RegisterStartupScript(Me.GetType(), "ButtonClick", "<script>alert('File needs in jpeg, jpg, gif, tif and .png format .')</script>")
                        Exit Sub
                    End If
                Else
                    photoID = ids
                    empimage = "blank.gif"

                End If


                db.qry = "delete from director where empid=" & lblid.Text & ""
                db.executeQuery()

                db.qry = "delete from bankdetails where empid=" & lblid.Text & ""
                db.executeQuery()


                'Dim compid As Integer = drpcompany.SelectedValue
                db.qry = "insert into director values(" & ids & "," & ids & ",'" & txtfirstname.Text & "','" & txtContactNo.Text & "','" & txtEmailid.Text & "','','" & txtqualification.Text & "','" & doj & "','" & rdbsex.SelectedValue & "','" & lblcompnyname.Text & "','" & DrpDivision.SelectedValue & "','" & drprepotTo.SelectedItem.Text & "','" & drpempdegi.SelectedValue & "','" & drpType.SelectedValue & "',0,'" & empno & "','" & txtcompmailid.Text & "',0,'" & empimage & "','" & appsign & "','" & passimg & "'," & db.getMaxId("director", "priority") & ",0,'" & Date.Now.ToString("dd-MMM-yyyy") & "','" & txtsalary.Text & "')"
                db.executeQuery()



                lblerror.Text = ""
                btnSubmit.Text = "Preview"
                Session("empid") = lblid.Text
                clearData()
                Response.Redirect("empprintappdetails.aspx?empid=" & Session("empid").ToString())

            Else


                Dim ids = db.getMaxId("director", "Ids")

                Dim name As String = db.initCap(txtfirstname.Text)


                Dim a() As String = name.Split(" ")

                Dim u As String = ""
                Dim m As String = ""
                Dim fir As String = ""
                Dim mid As String = ""
                For Each u In a
                    fir = fir & u.Substring(0, 1)
                Next

              

                Dim compabbr As String = db.getFieldValue("company", "companyname", lblcompnyname.Text, "companyabbr")

                Dim dojoining As String = txtdate.Text


                Dim dates() As String = dojoining.Split("-")
                Dim day As String = dates(0)
                Dim month As String = dates(1)
                Dim year As String = dates(2)
                Dim yr = year.Substring(2, 2)

                Dim empno As String = fir & "" & mid & "" & day & "" & yr & "" & compabbr & "" & ids

                Dim yr1 As String = ""

                If txtContactNo.Text = "" Then txtContactNo.Text = "N/A"
                If txtcompmailid.Text = "" Then txtcompmailid.Text = "N/A"


               

                If fupphoto.HasFile Then
                    photoID = ids
                    Dim ext As String = System.IO.Path.GetExtension(fupphoto.FileName)
                    If (ext = ".jpg" Or ext = ".jpeg" Or ext = ".gif" Or ext = ".tif" Or ext = ".png") Then
                        empimage = "Pphoto" & photoID & IO.Path.GetExtension(fupphoto.FileName)
                        fupphoto.SaveAs(Server.MapPath("~/Photos") & "/" & empimage)
                    Else
                        ClientScript.RegisterStartupScript(Me.GetType(), "ButtonClick", "<script>alert('File needs in jpeg, jpg, gif, tif and .png format .')</script>")
                        Exit Sub
                    End If
                Else
                    photoID = ids
                    empimage = "blank.gif"

                End If


                db.qry = "insert into director values(" & ids & "," & ids & ",'" & txtfirstname.Text & "','" & txtContactNo.Text & "','" & txtEmailid.Text & "','','" & txtqualification.Text & "','" & doj & "','" & rdbsex.SelectedValue & "','" & lblcompnyname.Text & "','" & DrpDivision.SelectedValue & "','" & drprepotTo.SelectedItem.Text & "','" & drpempdegi.SelectedValue & "','" & drpType.SelectedValue & "',0,'" & empno & "','" & txtcompmailid.Text & "',0,'" & empimage & "','" & appsign & "','" & passimg & "'," & db.getMaxId("director", "priority") & ",0,'" & Date.Now.ToString("dd-MMM-yyyy") & "','" & txtsalary.Text & "')"
                db.executeQuery()

                lblerror.Text = ""
                btnSubmit.Text = "Preview"
                Session("empid") = ids
                clearData()
                Response.Redirect("empprintappdetails.aspx?empid=" & Session("empid").ToString())

            End If
        Catch ex As Exception
            lblerror.Text = ex.Message
        End Try
    End Sub

    Private Sub clearData()
        txtContactNo.Text = ""
        txtEmailid.Text = ""
        DrpDivision.SelectedValue = "0"
        drprepotTo.SelectedValue = "0"
        drpempdegi.SelectedValue = "0"
    End Sub





End Class
