Imports System.Data

Partial Class clientprintappdetails
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("companyid") <> "" Then
            Session("companyid") = Request.QueryString("companyid").ToString()
        End If
        showdata()
    End Sub
    Private Sub showdata()
        strqry1 = "select * from clientmaster where companyid =" & Session("companyid").ToString()
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        dlstMain.DataSource = dr
        dlstMain.DataBind()
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub


    Protected Sub dlstMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlstMain.ItemDataBound
        Dim s As String
        s = dlstMain.DataKeys(e.Item.ItemIndex)
        CType(e.Item.FindControl("lbldate"), Label).Text = Date.Now.ToString("dd-MMM-yyyy")

        strqry1 = "select * from clientMaster where companyId =" & s
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        If dr.Read() Then
            txtCompanyName.Text = dr("companyName").ToString()
            txtCompanyAbbreviation.Text = dr("companyabbr").ToString()
            If dr("state").ToString() <> "0" Then
                CType(e.Item.FindControl("lblstate"), Label).Text = ", " & db.getFieldValue("state", "stateid", dr("state").ToString(), "statename")
            End If
            If dr("city").ToString() <> "0" Then
                CType(e.Item.FindControl("lblcity"), Label).Text = ", " & db.getFieldValue("city", "cityid", dr("city").ToString(), "cityname")
            End If
            If dr("compType").ToString() <> "0" Then
                CType(e.Item.FindControl("lblcompType"), Label).Text = db.getFieldValue("companyTypes", "typeId", dr("compType").ToString(), "companyType")
            End If
            Dim dor As String = (CDate(dr("DOR")).Year)
            CType(e.Item.FindControl("lblrecno"), Label).Text = "Company/Clint No. :" & DataBinder.Eval(e.Item.DataItem, "companyId").ToString() & "/" & dor
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Protected Sub dlstMain_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlstMain.ItemCreated

    End Sub
    Protected Sub btnReconfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReconfirm.Click
        If Request.QueryString("companyid") <> "" Then
            Response.Redirect("clientlisting.aspx")
        Else
            Response.Redirect("clientmaster.aspx")

        End If
    End Sub

    'Protected Sub btnedit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnedit.Click
    '    Response.Redirect("clientmaster.aspx?empid=" & Session("companyId").ToString())
    'End Sub

End Class
