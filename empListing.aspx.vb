
Imports System.Data
Partial Class Admin_empListing
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
        'db.setLayout(Session("userId"))
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

        gvPlots.HeaderRow.Cells(4).Text = "Department"
        gvPlots.HeaderRow.Cells(5).Text = "Reporting To"
        'gvPlots.HeaderRow.Cells(4).Text = "Joining Date"
        'gvPlots.HeaderRow.Cells(5).Text = "Address"
        gvPlots.HeaderRow.Cells(6).Text = "Contact Details"
        gvPlots.HeaderRow.Cells(7).Text = "Action"
        gvPlots.HeaderRow.Cells(8).Text = "pappid"
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
            strqry1 = "select * from director where companyname='" & drpcompany.SelectedItem.Text & "' and empno<>'0' and flg=0 order by convert(smalldatetime,DOJ)"
        Else
            strqry1 = "select * from director where empno<>'0' and flg=0 order by convert(smalldatetime,DOJ)"
        End If
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()

        Dim tbl As New DataTable
        Dim drw As DataRow
        tbl.Columns.Add("first").ColumnName = "SNo"
        tbl.Columns.Add("second").ColumnName = "photo"
        'tbl.Columns.Add("add1").ColumnName = "empno"
        tbl.Columns.Add("third").ColumnName = "dirname"
        tbl.Columns.Add("fourth").ColumnName = "designation"

        tbl.Columns.Add("fifth").ColumnName = "division"
        'tbl.Columns.Add("fifth").ColumnName = "doj"
        'tbl.Columns.Add("sixth").ColumnName = "address"
        tbl.Columns.Add("sixth").ColumnName = "reportTo"
        tbl.Columns.Add("seventh").ColumnName = "contact Details"
        tbl.Columns.Add("eighth").ColumnName = "empid"

        While dr.read()
            drw = tbl.NewRow
            drw(0) = tbl.Rows.Count + 1
            drw(2) = dr("dirName")
            drw(3) = dr("designation").ToString()

            drw(1) = dr("photo").ToString()
            drw(4) = dr("division").ToString()
            drw(5) = dr("reportTo").ToString()
            'drw(4) = dr("doj").ToString()
            'Dim citystr As String = db.getFieldValue("city", "cityid", dr("city").ToString(), "cityname", True)
            'Dim statestr As String = db.getFieldValue("state", "stateid", dr("state").ToString(), "stateName", True)
            'drw(5) = dr("address") + " , " + citystr + " , " + statestr
            Dim contactdetails = "Mobile No. - " & dr("contactNo") & ",<br/> LandLine No. - " & dr("stdcode") & "-" & dr("landlineno") & "<br/> Office EmailId - " & dr("compmailid") & ",<br/> Personal EmailId - " & dr("emailid")
            drw(6) = contactdetails
            drw(7) = dr("empid")
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
            gvPlots.HeaderRow.Cells(5).Text = "Reporting To"
            'gvPlots.HeaderRow.Cells(4).Text = "Joining Date"
            'gvPlots.HeaderRow.Cells(5).Text = "Address"
            gvPlots.HeaderRow.Cells(6).Text = "Contact Details"
            gvPlots.HeaderRow.Cells(7).Text = "Action"
            gvPlots.HeaderRow.Cells(8).Text = "pappid"
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
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim strId As String = gvPlots.DataKeys(e.Row.RowIndex).Value.ToString()
            CType(e.Row.FindControl("lblempno"), Label).Text = db.getFieldValue("director", "empid", strId, "empno", True)
            Dim dirname As String = db.getFieldValue("director", "empid", strId, "dirname", True)
            Dim appcount As String = db.getFieldValue("director", "dirname", dirname, "count(empid)", False)
            If appcount > 0 Then
                strqry2 = "select * from director where empid=" & strId & ""
                dt2 = db.fillReader1(strqry2)
                dr1 = dt2.CreateDataReader()
                If dr1.Read() Then
                    e.Row.Cells(4).Text = db.getFieldValue("divisions", "divisionid", dr1("division"), "Dname")
                End If

                dr1.Close()
                dt2.Clear()
                dt2.Dispose()
                'dr("dirName") + " " + dr("pfix") + " " + dr("FatherfName").ToString()
            End If

        End If
    End Sub

    Protected Sub gvPlots_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvPlots.RowUpdating
        Dim pno As String
        pno = gvPlots.DataKeys(e.RowIndex).Value
        Response.Redirect("EmpRegistration.aspx?empid=" & pno)
    End Sub
    Protected Sub gvPlots_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvPlots.RowDeleting
        Dim s As String
        s = gvPlots.DataKeys(e.RowIndex).Value
        Dim empno = db.getFieldValue("director", "empid", s, "empno")
        db.qry = "update director set flg=1 where empid=" & s
        db.executeQuery()
        db.qry = "delete from users where empid=" & s
        db.executeQuery()
        db.qry = "update bankdetails set flg=1 where compempid='" & empno & "'"
        db.executeQuery()
        Dim Ids = db.getFieldValue("director", "empid", s, "ids")
        db.qry = "update login set flg=1 where deno='" & Ids & "'"
        db.executeQuery()
        Dim pg As Integer
        pg = gvPlots.PageIndex
        showReport()
    End Sub

    Protected Sub drpcompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpcompany.SelectedIndexChanged
        showReport()
    End Sub
End Class
