Imports System.Data

Partial Class Admin_printClientPoDetails1
    Inherits System.Web.UI.Page
    Public db, db1 As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.QueryString("poWorkAssignID") <> "" Then
            Session("poWorkAssignID") = Request.QueryString("poWorkAssignID").ToString()
        End If
        showdata()
    End Sub
    Private Sub showdata()
        strqry1 = "select * from clientPOdetais  where poWorkAssignID=" & Session("poWorkAssignID").ToString()
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()

        strqry2 = "select * from clientPOpaymntStage where poWorkAssignID=" & Session("poWorkAssignID").ToString()
        dt2 = db.fillReader1(strqry2)
        dr1 = dt2.CreateDataReader()

        dlstMain.DataSource = dr
        dlstMain.DataBind()
        dlstPmntStg.DataSource = dr1
        dlstPmntStg.DataBind()
        dr.Close()
        dt1.Clear()
        dt1.Dispose()

        dr1.Close()
        dt2.Clear()
        dt2.Dispose()
    End Sub
    Protected Sub btnpreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        Response.Redirect("printClientPoDetails1.aspx?poWorkAssignID=" & Session("poWorkAssignID").ToString())
    End Sub
    Protected Sub dlstMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlstMain.ItemDataBound
        Dim s As String
        s = Session("poWorkAssignID").ToString()
        Dim clientid = db.getFieldValue("clientPOdetais", "poWorkAssignID", s, "clientid", False)
        Dim cmpnyname = db.getFieldValue("clientmaster", "companyid", clientid, "companyname", False)
        Dim wrkactivity = db.getFieldValue("clientPOdetais", "poWorkAssignID", s, "serviceid", False)
        Dim wrkname = db.getFieldValue("services", "serviceid", wrkactivity, "serviceHead", False)
        Dim Dcode = db.getFieldValue("clientPOdetais", "poWorkAssignID", s, "dcode", False)
        Dim dname = db.getFieldValue("divisions", "dcode", Dcode, "Dname", False)
        strqry1 = "select * from  clientPOdetais where poWorkAssignID =" & s
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        While dr.Read()
            CType(e.Item.FindControl("lblcompname"), Label).Text = cmpnyname
            CType(e.Item.FindControl("lbldoj"), Label).Text = wrkname
            CType(e.Item.FindControl("lbldinNo"), Label).Text = dname

        End While

        'If dr("clientid").ToString() <> "0" Then
        '    'CType(e.Item.FindControl("lblcompname"), Label).Text = ", " & cmpnyname
        'End If

        'If dr("city").ToString() <> "0" Then
        '    CType(e.Item.FindControl("lblcity"), Label).Text = ", " & db.getFieldValue("city", "cityid", dr("city").ToString(), "cityname")
        'End If

        'If dr("contactno").ToString() = "" Then
        '    CType(e.Item.FindControl("lblcontactno"), Label).Visible = False
        'End If

        'If dr("landlineno").ToString() = "" Then
        '    CType(e.Item.FindControl("lblll"), Label).Visible = False
        'End If
        'If dr("pincode").ToString() = "" Then
        '    CType(e.Item.FindControl("lblpin"), Label).Visible = False
        'End If

        'If dr("faxno").ToString() = "" Then
        '    CType(e.Item.FindControl("lblfax"), Label).Visible = False
        'End If

        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Protected Sub btnReconfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReconfirm.Click
        Response.Redirect("work_assignment.aspx")
    End Sub
    Protected Sub btnedit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnedit.Click
        Response.Redirect("work_assignment.aspx?poWorkAssignID=" & Session("poWorkAssignID").ToString())
    End Sub


End Class
