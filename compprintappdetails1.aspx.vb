Imports System.Data

Partial Class compprintappdetails1
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String


    Private Sub showdata()
        strqry1 = "select * from company where companyid =" & Session("ccid").ToString()
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()

        dlstMain.DataSource = dr
        dlstMain.DataBind()
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("companyid") <> "" Then
            Session("ccid") = Request.QueryString("companyid").ToString()
        End If
        showdata()
    End Sub

    Protected Sub dlstMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlstMain.ItemDataBound
        Dim s As String
        s = dlstMain.DataKeys(e.Item.ItemIndex)
        CType(e.Item.FindControl("lbldate"), Label).Text = Date.Now.ToString("dd-MMM-yyyy")
        Dim companyAbbr = db.getFieldValue("company", "companyid", s, "companyAbbr", True)
        Dim qry = "Select * from bankdetails where compempid='" & companyAbbr & "'"
        dt1 = db.fillReader1(qry)
        dr = dt1.CreateDataReader()
        Dim tb, a, b, c, d, f, g, h, i, j
        tb = "<table>"
        While dr.Read()
            a = dr("accname").ToString()
            b = dr("accno").ToString()
            c = dr("acctype").ToString()
            d = db.getFieldValue("banks", "bankcode", dr("bankcode").ToString(), "bankname", False)
            f = dr("bankaddress").ToString()
            g = db.getFieldValue("bankbranch", "Bbcode", dr("branchcode").ToString(), "BbName", False)
            h = dr("swiftcode")
            i = dr("micrno")
            j = dr("ifscno")
            If h = "" Then h = "N/A"
            If i = "" Then i = "N/A"
            If j = "" Then j = "N/A"
            tb = tb & "<tr><td>Acc Holder:" & a & "</td></tr>" & "<tr><td>Acc No." & b & "</td></tr>" & "<tr><td>Acc Type:" & c & "</td></tr>" & "<tr><td>Bank Name:" & d & "</td></tr>" & "<tr><td>Branch Name:" & g & "</td></tr>" & "<tr><td>Swift Code:" & h & "</td></tr>" & "<tr><td>MICR No.:" & i & "</td></tr>" & "<tr><td>IFSC No.:" & j & "</td></tr>" & "<tr><td>Bank Address:" & f & "</td></tr><tr><td></td></tr>"
        End While
        tb = tb & "</table>"
        If tb = "<table></table>" Then
            CType(e.Item.FindControl("lblbankdetail"), Label).Text = "N/A"
        Else
            CType(e.Item.FindControl("lblbankdetail"), Label).Text = tb
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
        strqry1 = "select * from company where companyId =" & s
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        If dr.Read() Then
            CType(e.Item.FindControl("lblcomptype"), Label).Text = db.getFieldValue("companyTypes", "TypeId", dr("compType").ToString(), "companyType")
            If dr("state").ToString() <> "0" Then
                CType(e.Item.FindControl("lblstate"), Label).Text = ", " & db.getFieldValue("state", "stateid", dr("state").ToString(), "statename")
            End If

            If dr("city").ToString() <> "0" Then
                CType(e.Item.FindControl("lblcity"), Label).Text = ", " & db.getFieldValue("city", "cityid", dr("city").ToString(), "cityname")
            End If

            If dr("CrossState").ToString() <> "0" Then
                CType(e.Item.FindControl("lblsat"), Label).Text = ", " & db.getFieldValue("state", "stateid", dr("CrossState").ToString(), "statename")
            End If

            If dr("CrossCity").ToString() <> "0" Then
                CType(e.Item.FindControl("lblcit"), Label).Text = ", " & db.getFieldValue("city", "cityid", dr("CrossCity").ToString(), "cityname")
            End If
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()

    End Sub

End Class
