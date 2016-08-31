
Imports System.Data
Partial Class Admin_supplierListing
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
            showReport()
        End If

    End Sub

    Protected Sub gvPlots_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvPlots.PageIndexChanging
        showReport()
        gvPlots.PageIndex = e.NewPageIndex
        gvPlots.DataBind()
        gvPlots.HeaderRow.Cells(0).Text = "S.No."
        'gvPlots.HeaderRow.Cells(1).Text = "Logo"
        gvPlots.HeaderRow.Cells(1).Text = "SubCategory"
        gvPlots.HeaderRow.Cells(2).Text = "Company Name"
        gvPlots.HeaderRow.Cells(3).Text = "Contact Name"
        gvPlots.HeaderRow.Cells(4).Text = "Contact Details"
        'gvPlots.HeaderRow.Cells(5).Text = "Address"
        gvPlots.HeaderRow.Cells(5).Text = "Details"
        gvPlots.HeaderRow.Cells(6).Text = "companyid"
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

        strqry1 = "select * from suppliermaster where flg=0"
        dt1 = db.fillReader1(strqry1)
        dr1 = dt1.CreateDataReader()

        Dim tbl As New DataTable
        Dim drw As DataRow
        tbl.Columns.Add("first").ColumnName = "SNo"
        tbl.Columns.Add("second").ColumnName = "subcategory"
        tbl.Columns.Add("third").ColumnName = "compname"
        tbl.Columns.Add("fourth").ColumnName = "contname"
        tbl.Columns.Add("fifth").ColumnName = "telephoneNo"
        'tbl.Columns.Add("sixth").ColumnName = "address"
        tbl.Columns.Add("sixth").ColumnName = "companyid"

        While dr1.Read()
            drw = tbl.NewRow
            drw(0) = tbl.Rows.Count + 1
            'drw(1) = dr1("logo")

            'Dim itemids As String = ""
            'Dim sitemid As Integer = 0

            'Dim s As String = db.getFieldValue("suppliermaster", "1", "1", "subcategory", True, " and companyID=" & dr1("companyID") & "")

            'If s.IndexOf(",") <> -1 Then

            '    Dim parts As String() = s.Split(New Char() {","c})

            '    Dim part As String
            '    For Each part In parts
            '        If sitemid = 0 Then
            '            itemids = part
            '        Else
            '            itemids = itemids & "," & part
            '         sitemid = sitemid + 1
            '    Next

            'Else
            '    itemids = db.getFieldValue("suppliermaster", "1", "1", "subcategory", True, " and companyID=" & dr1("companyID") & "")
            'End If

            'db.qry = "select distinct y.* from SubCategoryMaster as y where y.scid in (" & itemids & ")"
            'dr3 = db.fillReader
            'drw(1) = dr3("SubCategory")
            'End If
            '   
            'dr3.close()
            Dim itemids As String = ""
            Dim sitemid As Integer = 0

            If dr1("SubCategory").IndexOf(",") <> -1 Then

                Dim parts As String() = dr1("SubCategory").Split(New Char() {","c})

                Dim part As String
                For Each part In parts
                    If sitemid = 0 Then
                        itemids = db.getFieldValue("SubCategoryMaster", "scid", part, "SubCategory", True)
                    Else
                        itemids = itemids & "," & db.getFieldValue("SubCategoryMaster", "scid", part, "SubCategory", True)

                    End If
                    sitemid = sitemid + 1
                Next

            Else
                itemids = db.getFieldValue("SubCategoryMaster", "scid", dr1("SubCategory"), "SubCategory", True)
            End If

            'Dim subcat As String = db.getFieldValue("SubCategoryMaster", "1", "1", "SubCategory", False, " and scid in (" & dr1("SubCategory") & ")")
            drw(1) = itemids
            drw(2) = dr1("companyName").ToString()
            drw(3) = dr1("contactP").ToString()
            Dim contactdetails = "Mobile No. - " & dr1("mobileNo") & ",<br/> LandLine No. - " & dr1("telephoneno") & "<br/> EmailId - " & dr1("EmailId")
            drw(4) = contactdetails
            'Dim citystr As String = db.getFieldValue("city", "cityid", dr1("city").ToString(), "cityname", True)
            'Dim statestr As String = db.getFieldValue("state", "stateid", dr1("state").ToString(), "stateName", True)
            'Dim countrystr As String = db.getFieldValue("country", "countryid", dr1("country"), "countryname", True)
            'drw(5) = dr1("address").ToString() + ", Pin Code- " + dr1("pincode").ToString() + ", " + citystr + ", " + statestr + ", " + countrystr
            drw(5) = dr1("companyid")
            tbl.Rows.Add(drw)
        End While
     

        If tbl.Rows.Count > 0 Then
            gvPlots.PagerSettings.Mode = PagerButtons.Numeric
            gvPlots.DataSource = tbl
            gvPlots.DataBind()
            gvPlots.HeaderRow.Cells(0).Text = "S.No."
            gvPlots.HeaderRow.Cells(1).Text = "SubCategory"
            'gvPlots.HeaderRow.Cells(1).Text = "Logo"
            gvPlots.HeaderRow.Cells(2).Text = "Company Name"
            gvPlots.HeaderRow.Cells(3).Text = "Contact Name"
            gvPlots.HeaderRow.Cells(4).Text = "Contact Details"
            'gvPlots.HeaderRow.Cells(5).Text = "Address"
            gvPlots.HeaderRow.Cells(5).Text = "Details"
            gvPlots.HeaderRow.Cells(6).Text = "companyid"
        Else
            gvPlots.DataSource = Nothing
            gvPlots.DataBind()
        End If
        dr1.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Protected Sub gvPlots_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPlots.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim strId As String = gvPlots.DataKeys(e.Row.RowIndex).Value.ToString()
        End If
    End Sub

    Protected Sub gvPlots_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvPlots.RowUpdating
        Dim pno As String
        pno = gvPlots.DataKeys(e.RowIndex).Value
        Response.Redirect("SupplierMaster.aspx?companyid=" & pno)
    End Sub
    Protected Sub gvPlots_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvPlots.RowDeleting
        Dim s As String
        s = gvPlots.DataKeys(e.RowIndex).Value
        db.qry = "update SupplierMaster set flg=1 where companyid=" & s
        db.executeQuery()
        db.qry = "update bankdetails set flg=1 where compempid='" & s & "'"
        db.executeQuery()
        Dim pg As Integer
        pg = gvPlots.PageIndex
        showReport()
    End Sub
End Class
