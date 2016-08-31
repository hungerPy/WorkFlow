Imports System.Data

Partial Class empprintappdetails
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String
    Private Sub showdata()
        strqry1 = "select * from director where empid =" & Session("empid").ToString()
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        dlstMain.DataSource = dr
        dlstMain.DataBind()
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnReconfirm.Attributes.Add("OnClick", "window.close();")
        If Request.QueryString("empid") <> "" Then
            Session("empid") = Request.QueryString("empid").ToString()
        End If
        showdata()
    End Sub

    Protected Sub dlstMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlstMain.ItemDataBound
        Dim s As String
        s = dlstMain.DataKeys(e.Item.ItemIndex)
        Dim empno = db.getFieldValue("director", "empid", s, "empno", True)
        Dim qry = "Select * from bankdetails where compempid='" & empno & "'"

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

        strqry1 = "select * from director where empid =" & s
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        If dr.Read() Then
            If dr("pfix").ToString() = "S/o" Then
                CType(e.Item.FindControl("lblpfix"), Label).Text = "Father's Name"
            ElseIf dr("pfix").ToString() = "D/o" Then
                CType(e.Item.FindControl("lblpfix"), Label).Text = "Father's Name"
            ElseIf dr("pfix").ToString() = "W/o" Then
                CType(e.Item.FindControl("lblpfix"), Label).Text = "Husband's Name"
            End If

            If dr("state").ToString() <> "0" Then
                CType(e.Item.FindControl("lblstate"), Label).Text = ", " & db.getFieldValue("state", "stateid", dr("state").ToString(), "statename")
            End If

            If dr("city").ToString() <> "0" Then
                CType(e.Item.FindControl("lblcity"), Label).Text = ", " & db.getFieldValue("city", "cityid", dr("city").ToString(), "cityname")
            End If

            If dr("contactno").ToString() = "" Then
                CType(e.Item.FindControl("lblcontactno"), Label).Visible = False
            End If

            If dr("landlineno").ToString() = "" Then
                CType(e.Item.FindControl("lblll"), Label).Visible = False
            End If
            If dr("pincode").ToString() = "" Then
                CType(e.Item.FindControl("lblpin"), Label).Visible = False
            End If

            If dr("faxno").ToString() = "" Then
                CType(e.Item.FindControl("lblfax"), Label).Visible = False
            End If

            If dr("sex").ToString() = "M" Then
                CType(e.Item.FindControl("lblsex"), Label).Text = "Male"
            ElseIf dr("sex").ToString() = "F" Then
                CType(e.Item.FindControl("lblsex"), Label).Text = "Female"
            End If

        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Protected Sub dlstMain_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlstMain.ItemCreated

    End Sub

    Protected Sub btnedit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnedit.Click
        Response.Redirect("EmpRegistration.aspx?empid=" & Session("empid").ToString())
    End Sub

    Protected Sub btnReconfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReconfirm.Click
        Response.Redirect("EmpRegistration.aspx")
    End Sub

    Protected Sub btnpreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        Response.Redirect("empprintappdetails1.aspx?empid=" & Session("empid").ToString())
    End Sub
End Class
