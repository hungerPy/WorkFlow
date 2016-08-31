Imports System.Data
Partial Class Admin_pastemplisiting
    Inherits System.Web.UI.Page
    Public db As New general

    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String

    Dim rcount As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rcount = 0
        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        ' db.setLayout(Session("userId"))
        'End If

        If Not IsPostBack Then
            db.fillCombo(drpcompany, "company", "companyname", "companyAbbr", " order by companyname")
        End If
        showReport()
    End Sub

    Protected Sub gvPlots_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvPlots.PageIndexChanging
        showReport()
        gvPlots.PageIndex = e.NewPageIndex
        gvPlots.DataBind()
        gvPlots.HeaderRow.Cells(0).Text = "S.No."
        gvPlots.HeaderRow.Cells(1).Text = "Photo"
        gvPlots.HeaderRow.Cells(2).Text = "Name"
        gvPlots.HeaderRow.Cells(3).Text = "Designation"

        gvPlots.HeaderRow.Cells(4).Text = "Division"
        'gvPlots.HeaderRow.Cells(4).Text = "Joining Date"
        'gvPlots.HeaderRow.Cells(5).Text = "Address"
        gvPlots.HeaderRow.Cells(5).Text = "Contact Details"
        gvPlots.HeaderRow.Cells(6).Text = "Action"
        gvPlots.HeaderRow.Cells(7).Text = "pappid"
    End Sub

    Protected Sub gvPlots_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPlots.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            'rcount = rcount + 1
            'e.Row.Cells(0).Text = rcount
        End If
    End Sub


    Private Sub showReport()
        Dim str As String = ""
        Dim tehsilstr As String = ""
        Dim panchayatstr As String = ""
        Dim gramstr As String = ""

        If drpcompany.SelectedIndex <> 0 Then
            strqry1 = "select * from director where companyname='" & drpcompany.SelectedItem.Text & "' and empno<>'0' and flg=1"
        Else
            strqry1 = "select * from director where empno<>'0' and flg=1"
        End If
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        Dim tbl As New DataTable
        Dim drw As DataRow
        tbl.Columns.Add("first").ColumnName = "SNo"
        tbl.Columns.Add("second").ColumnName = "photo"
        tbl.Columns.Add("third").ColumnName = "dirname"
        tbl.Columns.Add("fourth").ColumnName = "designation"

        tbl.Columns.Add("fifth").ColumnName = "division"
        'tbl.Columns.Add("fifth").ColumnName = "doj"
        'tbl.Columns.Add("sixth").ColumnName = "address"
        tbl.Columns.Add("sixth").ColumnName = "contact Details"
        tbl.Columns.Add("seventh").ColumnName = "empid"

        While dr.Read()
            drw = tbl.NewRow
            drw(0) = tbl.Rows.Count + 1
            'drw(2) = dr("Aini") + dr("dirName")
            drw(2) = dr("dirName")
            drw(3) = dr("designation").ToString()
            drw(1) = dr("photo").ToString()
            drw(4) = dr("division").ToString()
            'drw(4) = dr("doj").ToString()
            'Dim citystr As String = db.getFieldValue("city", "cityid", dr("city").ToString(), "cityname", True)
            'Dim statestr As String = db.getFieldValue("state", "stateid", dr("state").ToString(), "stateName", True)
            'drw(5) = dr("address") + " , " + citystr + " , " + statestr
            Dim contactdetails = "Mobile No. - " & dr("contactNo") & ",<br/> LandLine No. - " & dr("stdcode") & "-" & dr("landlineno") & "<br/> Office EmailId - " & dr("compmailid") & ",<br/> Personal EmailId - " & dr("emailid")
            drw(5) = contactdetails
            drw(6) = dr("empid")
            tbl.Rows.Add(drw)
        End While



        If tbl.Rows.Count > 0 Then
            gvPlots.PagerSettings.Mode = PagerButtons.Numeric
            gvPlots.DataSource = tbl
            gvPlots.DataBind()
            gvPlots.HeaderRow.Cells(0).Text = "S.No."
            gvPlots.HeaderRow.Cells(1).Text = "Photo"
            gvPlots.HeaderRow.Cells(2).Text = "Name"
            gvPlots.HeaderRow.Cells(3).Text = "Designation"

            gvPlots.HeaderRow.Cells(4).Text = "Division"
            'gvPlots.HeaderRow.Cells(4).Text = "Joining Date"
            'gvPlots.HeaderRow.Cells(5).Text = "Address"
            gvPlots.HeaderRow.Cells(5).Text = "Contact Details"
            gvPlots.HeaderRow.Cells(6).Text = "Action"
            gvPlots.HeaderRow.Cells(7).Text = "pappid"
        Else
            gvPlots.DataSource = Nothing
            gvPlots.DataBind()
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Protected Sub gvPlots_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPlots.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim strId As String = gvPlots.DataKeys(e.Row.RowIndex).Value.ToString()
            Dim dirname As String = db.getFieldValue("director", "empid", strId, "dirname", True)
            Dim appcount As String = db.getFieldValue("director", "dirname", dirname, "count(empid)", False)
            If appcount > 1 Then
                strqry1 = "select * from director where empid=" & strId & ""
                dt1 = db.fillReader1(strqry1)
                dr = dt1.CreateDataReader()
                If dr.Read() Then
                    e.Row.Cells(4).Text = dr("dirName") + " " + dr("FatherfName").ToString()
                End If
            End If

        End If
    End Sub
    Protected Sub drpcompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpcompany.SelectedIndexChanged
        showReport()
    End Sub
End Class
