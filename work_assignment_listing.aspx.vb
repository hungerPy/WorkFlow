Imports System.Data
Partial Class Admin_work_assignment_listing
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String


    Protected Sub gvClientPoDetails_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvPlots.PageIndexChanging
        showReport()
        gvPlots.PageIndex = e.NewPageIndex
        gvPlots.DataBind()
        gvPlots.HeaderRow.Cells(0).Text = "S.No."
        gvPlots.HeaderRow.Cells(1).Text = "PO Date"
        gvPlots.HeaderRow.Cells(2).Text = "PO Reciving Date"
        gvPlots.HeaderRow.Cells(3).Text = "Company Name"
        gvPlots.HeaderRow.Cells(4).Text = "Division Name"


        'gvPlots.HeaderRow.Cells(6).Text = "PO Remarks"

        gvPlots.HeaderRow.Cells(5).Text = "Target Date"

        gvPlots.HeaderRow.Cells(6).Text = "Deriables"
        gvPlots.HeaderRow.Cells(7).Text = "Total Amount"
    End Sub


    Protected Sub gvClientPoDetails_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPlots.SelectedIndexChanged

    End Sub

    Private Sub showReport()
        Dim str As String = ""
        Dim tehsilstr As String = ""
        Dim panchayatstr As String = ""
        Dim gramstr As String = ""


        strqry1 = "select * from clientPOdetais "
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()

        Dim tbl As New DataTable
        Dim drw As DataRow
        tbl.Columns.Add("first").ColumnName = "SNo"
        tbl.Columns.Add("second").ColumnName = "poDate"
        tbl.Columns.Add("third").ColumnName = "poRecivingDate"
        tbl.Columns.Add("fourth").ColumnName = "clientid"
        tbl.Columns.Add("fifth").ColumnName = "dcode"
        'tbl.Columns.Add("fifth").ColumnName = "doj"
        'tbl.Columns.Add("fourth").ColumnName = "address"
        tbl.Columns.Add("sixth").ColumnName = "serviceid"
        tbl.Columns.Add("seventh").ColumnName = "wrkCmpletTrgtDate"
        tbl.Columns.Add("eighth").ColumnName = "deriables"
        tbl.Columns.Add("ninth").ColumnName = "totalAmnt"
        tbl.Columns.Add("tenth").ColumnName = "poWorkAssignID"

        While dr.read()

            Dim client As String = db.getFieldValue("clientmaster", "companyid", dr("clientid").ToString(), "Companyname", False)
            Dim dcode As String = db.getFieldValue("Divisions", "DCode", dr("dcode").ToString(), "DName", False)
            drw = tbl.NewRow
            drw(0) = tbl.Rows.Count + 1
            drw(2) = dr("poRecivingDate").ToString()
            drw(3) = client
            drw(1) = dr("poDate").ToString()
            drw(4) = dcode

            drw(5) = dr("wrkCmpletTrgtDate").ToString()


            'Dim citystr As String = db.getFieldValue("city", "cityid", dr("city").ToString(), "cityname", True)
            'Dim statestr As String = db.getFieldValue("state", "stateid", dr("state").ToString(), "stateName", True)

            drw(6) = dr("deriables").ToString()

            drw(7) = dr("totalAmnt").ToString()
            drw(8) = dr("poWorkAssignID").ToString()
            drw(9) = dr("poWorkAssignID")
            tbl.Rows.Add(drw)
        End While
      


        If tbl.Rows.Count > 0 Then
            gvPlots.PagerSettings.Mode = PagerButtons.Numeric
            gvPlots.DataSource = tbl
            gvPlots.DataBind()
            gvPlots.HeaderRow.Cells(0).Text = "S.No."
            gvPlots.HeaderRow.Cells(1).Text = "PO Date"
            gvPlots.HeaderRow.Cells(2).Text = "PO Reciving Date"
            gvPlots.HeaderRow.Cells(3).Text = "Company Name"
            gvPlots.HeaderRow.Cells(4).Text = "Division Name"



            gvPlots.HeaderRow.Cells(5).Text = "Target Date"

            gvPlots.HeaderRow.Cells(6).Text = "Deriables"
            gvPlots.HeaderRow.Cells(8).Text = "Total Amount"
        Else
            gvPlots.DataSource = Nothing
            gvPlots.DataBind()
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        showReport()
    End Sub


    Protected Sub gvPlots_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvPlots.RowUpdating
        Dim pno As String
        pno = gvPlots.DataKeys(e.RowIndex).Value
        Response.Redirect("work_assignment.aspx?poWorkAssignID=" & pno)
    End Sub


End Class
