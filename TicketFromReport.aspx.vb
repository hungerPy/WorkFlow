Imports System.Data.SqlClient
Imports System.Data

Partial Class Admin_TicketFromReport
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dr As Object
    Dim ds As New DataSet
    Public strType As String = "SQL"
    Dim qry As String = ""
    'Dim strConnection As String = "Data Source=Kapl-14;Initial Catalog=kamtechmisonline;Integrated Security=True"
    Dim strConnection As String = "Data Source=173.236.21.186;initial catalog=kamtechmis;user id=kamtechmis;password='kamtech#111'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        '    db.setLayout(Session("userId"), form1)
        'End If


        d1.Attributes.Add("onchange", "getDate(0," & d1.ClientID & "," & m1.ClientID & "," & y1.ClientID & ")")
        m1.Attributes.Add("onchange", "getDate(0," & d1.ClientID & "," & m1.ClientID & "," & y1.ClientID & ")")
        y1.Attributes.Add("onchange", "getDate(0," & d1.ClientID & "," & m1.ClientID & "," & y1.ClientID & ")")
        d2.Attributes.Add("onchange", "getDate(0," & d2.ClientID & "," & m2.ClientID & "," & y2.ClientID & ")")
        m2.Attributes.Add("onchange", "getDate(0," & d2.ClientID & "," & m2.ClientID & "," & y2.ClientID & ")")
        y2.Attributes.Add("onchange", "getDate(0," & d2.ClientID & "," & m2.ClientID & "," & y2.ClientID & ")")

        If Not IsPostBack Then


            Dim t
            For Each t In db.mName
                m1.Items.Add(t)
            Next

            For t = 2000 To Now.Year + 15
                y1.Items.Add(t)
            Next
            m1.SelectedIndex = Now.Month - 1

            y1.SelectedIndex = (Now.Year) - 2000


            Dim t2
            For Each t2 In db.mName
                m2.Items.Add(t2)
            Next

            For t2 = 2000 To Now.Year + 15
                y2.Items.Add(t2)
            Next

            m2.SelectedIndex = Now.Month - 1

            y2.SelectedIndex = (Now.Year) - 2000

            For t = 1 To 31
                d1.Items.Add(t)
                d2.Items.Add(t)
            Next

            d1.SelectedValue = Now.Day - 7
            d2.SelectedValue = Now.Day
            m1.SelectedValue = Now.Month
            m2.SelectedValue = Now.Month
            y1.SelectedValue = Now.Year
            y2.SelectedValue = Now.Year

            drprecords.DataSource = db.noRecords
            drprecords.DataBind()

            drprecords.SelectedValue = "10"
            lbluser.Text = getFieldValue("clientmaster", "companyid", db.cid, "companyname", True)

            fillCombo(drpstatus, "statusmaster", "tktstatus", "tktstatus", " where usr='Admin' order by tktstatus", , "Hold By Client|Hold By Kamtech|Over All")

            drpstatus.SelectedValue = "Over All"

        End If

    End Sub

    Private Sub show()

        Dim startdt As String = d1.SelectedValue + "-" + m1.SelectedValue + "-" + y1.SelectedValue
        Dim enddt As String = d2.SelectedValue + "-" + m2.SelectedValue + "-" + y2.SelectedValue


        If drpstatus.SelectedValue <> "Completed" Then
            qry = "select (select dirname from director where ids=t.assignto) as assign,* from Ticketmaster as t where tid in(select tid from ticket where client=" & db.cid & ") and (convert(smalldatetime,adt1) between convert(smalldatetime,'" & startdt & "') and convert(smalldatetime,'" & enddt & "')) and status<>'Completed'"
        Else
            qry = "select (select dirname from director where ids=t.assignto) as assign,* from Ticketmaster as t where tid in(select tid from ticket where client=" & db.cid & ") and (convert(smalldatetime,adt1) between convert(smalldatetime,'" & startdt & "') and convert(smalldatetime,'" & enddt & "'))"
        End If

        If drpstatus.SelectedValue <> "Over All" Then
            If (drpstatus.SelectedValue = "Hold By Client" Or drpstatus.SelectedValue = "Hold By Kamtech") Then
                qry = qry & " and empstatus ='" & drpstatus.SelectedValue & "'"
            Else
                qry = qry & " and status='" & drpstatus.SelectedValue & "'"
            End If
        End If

        qry = qry & " order by tid"

        ds = fillDataSet()

        If ds.Tables(0).Rows.Count > 0 Then
            pnlform.Visible = False

            If drpstatus.SelectedValue <> "0" And drpstatus.SelectedValue <> "Over All" Then
                lblstatus.Text = "(All " & drpstatus.SelectedValue & " Work)"
            ElseIf drpstatus.SelectedValue = "Over All" Then
                lblstatus.Text = "(Over All Work)"
            Else
                lblstatus.Text = ""
            End If

            If drprecords.SelectedValue = "ALL" Then
                Gv1.PageSize = ds.Tables(0).Rows.Count
            Else
                Gv1.PageSize = CInt(drprecords.SelectedValue)
            End If

            Gv1.DataSource = ds
            Gv1.DataBind()


            If drpstatus.SelectedValue = "Completed" Then
                Gv1.Columns(4).Visible = True
            Else
                Gv1.Columns(4).Visible = False
            End If

        Else
            pnlform.Visible = True
            lblstatus.Text = ""
            Gv1.DataSource = Nothing
            Gv1.DataBind()
        End If

        ds.Dispose()

    End Sub

    Protected Sub Gv1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gv1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Cells(0).Text = (Gv1.PageIndex * Gv1.PageSize) + (e.Row.RowIndex + 1)

        End If
    End Sub



    Private Function forDbase(ByVal str As String) As String
        If strType = "SQL" Then
            str = Replace(str, "cdate(", "convert(smalldatetime,")
            str = Replace(str, "lcase", "lower")
            str = Replace(str, "ucase", "upper")
            str = Replace(str, "cint(", "convert(integer,")
            str = Replace(str, "cdbl(", "convert(float,")
        End If
        forDbase = str
    End Function

    Public Sub executeQuery()
        Dim c As New SqlConnection(strConnection)
        Dim cmd As New SqlCommand(forDbase(qry), c)
        c.Open()
        cmd.ExecuteNonQuery()
        c.Close()
    End Sub

    Public Function fillReader(Optional ByVal sql As String = "") As SqlDataReader
        Dim con As New SqlConnection(strConnection)
        Dim cmd As SqlCommand
        Dim dr As SqlDataReader
        If sql <> "" Then
            sql = forDbase(sql)
            cmd = New SqlCommand(sql, con)
        Else
            qry = forDbase(qry)
            cmd = New SqlCommand(qry, con)
        End If
        con.Open()
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        fillReader = dr
    End Function

    Public Function fillDataSet() As DataSet
        Dim c As New SqlConnection(strConnection)
        qry = forDbase(qry)
        Dim dtp As New SqlDataAdapter(qry, c)
        Dim dts As New DataSet
        c.Open()
        dtp.Fill(dts)
        fillDataSet = dts
        c.Close()
    End Function

    Public Function getFieldValue(ByVal tblName As String, ByVal fldName As String, ByVal fldValue As String, ByVal fldFind As String, Optional ByVal isNumeric As Boolean = False, Optional ByVal sqlWhereAttach As String = "") As Object
        Dim c As New SqlConnection(strConnection)
        Dim sql As String
        Dim dr As New Object, v
        If isNumeric = True Then
            sql = "select " & fldFind & " from " & tblName & " where " & fldName & "=" & fldValue & " " & sqlWhereAttach
        Else
            sql = "select " & fldFind & " from " & tblName & " where " & fldName & "='" & fldValue & "' " & sqlWhereAttach
        End If
        sql = forDbase(sql)
        Dim cmd As New SqlCommand(sql, c)
        c.Open()
        dr = cmd.ExecuteReader()
        If dr.read() Then
            v = IIf(IsDBNull(dr(0)), "", dr(0))
        Else
            v = ""
        End If
        dr.close()
        c.Close()
        getFieldValue = v
    End Function

    Public Function isExists(ByVal tblName As String, ByVal fldName As String, ByVal fldValue As String, Optional ByVal isNumeric As Boolean = False, Optional ByVal sqlWhereAttach As String = "") As Boolean
        Dim c As New SqlConnection(strConnection)
        Dim cmd As New SqlCommand()
        Dim bl As Boolean = True
        Dim dr As New Object
        If Not isNumeric Then
            cmd.CommandText = "select * from " & tblName & " where " & fldName & " = '" & fldValue & "'" & " " & sqlWhereAttach
        Else
            cmd.CommandText = "select * from " & tblName & " where " & fldName & " =" & fldValue & " " & sqlWhereAttach
        End If
        cmd.CommandText = forDbase(cmd.CommandText)
        cmd.Connection = c
        c.Open()
        dr = cmd.ExecuteReader()
        bl = dr.read
        dr.close()
        c.Close()
        isExists = bl
    End Function


    Public Sub fillCombo(ByRef cmbBox As DropDownList, ByVal tblName As String, ByVal DisplayField As String, ByVal valueField As String, Optional ByVal sqlWhere As String = "", Optional ByVal spFor As String = "", Optional ByVal extr As String = "")
        Dim c As New SqlConnection(strConnection)
        Dim s As String
        s = "select " & DisplayField & " as df," & valueField & " as vf from " & tblName & " " & sqlWhere
        s = forDbase(s)
        Dim dtp As New SqlDataAdapter(s, c)
        Dim dts As New DataSet
        Dim drw As DataRow
        c.Open()
        dtp.Fill(dts, tblName)
        If spFor <> "config" Then
            drw = dts.Tables(0).NewRow
            drw(0) = "[Select]"
            drw(1) = "0"
            dts.Tables(0).Rows.InsertAt(drw, 0)
        End If
        If extr <> "" Then
            Dim a
            Dim i As Integer
            a = Split(extr, "|")
            For i = 0 To UBound(a)
                drw = dts.Tables(0).NewRow
                drw(0) = a(i)
                drw(1) = a(i)
                dts.Tables(0).Rows.Add(drw)
            Next
        End If
        cmbBox.DataTextField = "df"
        cmbBox.DataValueField = "vf"
        cmbBox.DataSource = dts.Tables(0)
        cmbBox.DataBind()
        dts.Tables.Clear()
        dts.Dispose()
        c.Close()
    End Sub

    Protected Sub btnsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        show()
    End Sub

    Protected Sub Gv1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gv1.PageIndexChanging

        Gv1.PageIndex = e.NewPageIndex
        show()

    End Sub

End Class
