Imports System.Data

Partial Class CompanyName
    Inherits System.Web.UI.Page
    Public db As New general

    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1, dr2 As DataTableReader
    Dim strqry1, strqry2 As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'db.setLayout(Session("userId"))
        If Not IsPostBack Then
            showcompany()
        End If
    End Sub
    Public Sub showcompany()
        strqry1 = "select * from company order by companyname"
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        dlcompanyname.DataSource = dr
        dlcompanyname.DataBind()
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Protected Sub dlcompanyname_ItemCommand(source As Object, e As DataListCommandEventArgs) Handles dlcompanyname.ItemCommand
        Dim compid As String = dlcompanyname.DataKeys(e.Item.ItemIndex).ToString()
        Session("imagelogo") = db.getFieldValue("company", "companyid", compid, "logo", True)
        Session("companyname") = db.getFieldValue("company", "companyid", compid, "companyname", True)
        Session("companyid") = compid.ToString()
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub lnkaddcompany_Click(sender As Object, e As EventArgs)
        divdlcompanyname.Visible = False
        divselectcompany.Visible = False
        pnladdcompany.Visible = True
    End Sub

    Protected Sub btnregister_Click(sender As Object, e As EventArgs) Handles btnregister.Click
        If db.isExists("company", "companyname", txtcompany.Text, False, " and companyabbr='" & txtAbbreviation.Text & "' and contactp='" & txtdirector.Text & "' ") = True Then
            Response.Write("<script>alert('Sorry...!!! Company is already registered')</script>")
            Exit Sub
        End If

        Dim name As String = ""
        Dim flag As Integer
        Dim cid As Integer = db.getMaxId("company", "companyid")
        If (clogo.HasFile = True) Then
            Dim ext As String = System.IO.Path.GetExtension(clogo.FileName)
            If (ext = ".jpg" Or ext = ".jpeg" Or ext = ".gif" Or ext = ".tif" Or ext = ".png") Then
                Dim filename As String = clogo.FileName
                If clogo.PostedFile.ContentLength > 100000000000 Then
                    ClientScript.RegisterStartupScript(Me.GetType(), "ButtonClick", "<script>alert('File needs to be > 0 bytes and less than 10 MB.')</script>")
                    Exit Sub
                Else
                    name = "company" & cid & IO.Path.GetExtension(clogo.FileName)
                    clogo.SaveAs(Server.MapPath("~\logos\") & name)
                End If
            Else

                ClientScript.RegisterStartupScript(Me.GetType(), "ButtonClick", "<script>alert('File needs to be in proper format .')</script>")
                Exit Sub
            End If

        Else
            name = "~/Category/No-Image-Available.gif"
        End If

        db.qry = "insert into company values(" & cid & ",'" & txtcompany.Text & "','" & txtAbbreviation.Text & "','" & txtdirector.Text & "','','','','','','','','','','','','','','','','','" & name & "','','','','','','','','','')"
        db.executeQuery()
        Response.Redirect("companyname.aspx")

    End Sub
End Class


