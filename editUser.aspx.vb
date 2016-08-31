Imports System.Data

Partial Class Admin_editUser
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String
    Public mndYes As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        ' db.setLayout(Session("userId"))
        'End If
        mndYes = db.isExists("menu", "nodecode", "MND")

        If Not IsPostBack Then
            BindGridView()
            db.fillCombo(drpCompanyName, "company", "CompanyName", "CompanyAbbr", " where flg=0")
            db.fillCombo(DrpDivision, "Divisions", "DName", "DCode", " where 1=2")
            db.fillCombo(drpempdegi, "Designation", "Designation", "Designation", " where 1=2")
            db.fillCombo(drpUserName, "director", "dirname+' ('+designation+')'", "Ids", " where 1=2")
            'Dim companyName = db.getFieldValue("company", "companyAbbr", Session("company"), "companyName")
            'db.fillCombo(drpUserName, "director", "dirname+' ('+designation+')'", "Ids", " where companyname='" & companyName & "' and flg=0 and (dinno<>'0' or empno<>'0')")
            'db.fillCombo(drpUserName, "employee", "Empname", "empid", " order by cdate(DOJ),empname")
        End If
        'btnSubmit.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")
    End Sub

    Protected Sub drpUserName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpUserName.SelectedIndexChanged
        Dim tm, tm1
        Dim f As String
        Dim chkl As New CheckBoxList
        For Each tm1 In gvRights.Rows
            If tm1.RowType = DataControlRowType.DataRow Then
                CType(tm1.FindControl("chkNP"), CheckBox).Checked = False
                chkl = CType(tm1.FindControl("ChkList"), CheckBoxList)
                For Each tm In chkl.Items
                    f = db.getFieldValue("rights", "uid", drpUserName.SelectedValue, tm.value, True)
                    tm.selected = CBool(IIf(f = "", 0, f))
                    db.qry = db.qry & tm.value & "='" & tm.selected & "',"
                Next
            End If
        Next


        Dim sendmail = db.getFieldValue("director", "IDS", drpUserName.SelectedValue, "sendmail", False)
        If sendmail = "1" Then
            chkAllmail.Checked = True
        Else
            chkAllmail.Checked = False
        End If

    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim tm, tm1 As Object, str As String = ""
        Dim sid As String = ""
        'db.qry = "delete from rights where uid=" & drpUserName.SelectedValue
        'db.executeQuery()
        'db.qry = "select * from rights"
        'dr = db.fillReader()
        'Dim i As Integer = 0
        'For i = 1 To dr.FieldCount - 1
        '    If str <> "" Then str = str & ","
        '    str = str & "'False'"
        'Next
        'dr.close()
        '


        'db.qry = "insert into rights values (" & drpUserName.SelectedValue & "," & str & ")"
        'db.executeQuery()
        db.qry = ""
        Dim chkl As New CheckBoxList
        For Each tm1 In gvRights.Rows
            If tm1.RowType = DataControlRowType.DataRow Then
                chkl = CType(tm1.FindControl("ChkList"), CheckBoxList)
                For Each tm In chkl.Items
                    db.qry = db.qry & tm.value & "='" & tm.selected & "',"
                Next
            End If
        Next
        db.qry = db.qry.TrimEnd(",")
        db.qry = "update rights set " & db.qry & " where uid=" & drpUserName.SelectedValue
        db.executeQuery()
        If chkAllmail.Checked = True Then
            db.qry = "update director set sendmail='1' where IDS=" & drpUserName.SelectedValue
            db.executeQuery()
        End If
    End Sub
    Protected Sub BindGridView()
        Dim a As Integer
        strqry1 = "select nodetext,nodecode,nodeparent from menu where nodeparent='Root' order by nodeid"

        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        gvRights.DataSource = dr
        gvRights.DataBind()
        dr.Close()
        dt1.Clear()
        dt1.Dispose()

    End Sub

    Protected Sub gvRights_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvRights.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim NCode As String
            NCode = gvRights.DataKeys(e.Row.RowIndex).Value
            CType(e.Row.FindControl("chkNP"), CheckBox).Attributes.Add("onclick", "CheckAll(this)")

            Dim ChkListRight As New CheckBoxList
            ChkListRight = CType(e.Row.FindControl("ChkList"), CheckBoxList)
            db.fillCheckList(ChkListRight, "menu", "nodetext", "nodecode", "where nodeparent='" & NCode & "' order by nodeid")
            If ChkListRight.Items.Count = 0 Then e.Row.Visible = False
        End If

    End Sub

    Protected Sub drpCompanyName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpCompanyName.SelectedIndexChanged
        db.fillCombo(DrpDivision, "Divisions", "DName", "DCode", " where companycode='" & drpCompanyName.SelectedValue & "'")
        DrpDivision.Items.Add(drpCompanyName.SelectedValue)
    End Sub

    Protected Sub DrpDivision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrpDivision.SelectedIndexChanged
        db.fillCombo(drpempdegi, "Designation", "Designation", "Designation", " order by prty")
    End Sub

    Protected Sub drpempdegi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpempdegi.SelectedIndexChanged
        Dim companyName = db.getFieldValue("company", "companyAbbr", Session("company"), "companyName")
        db.fillCombo(drpUserName, "director", "dirname", "Ids", " where companyname='" & companyName & "' and division='" & DrpDivision.SelectedItem.Text & "' and designation='" & drpempdegi.SelectedItem.Text & "' and flg=0 and (dinno<>'0' or empno<>'0')")
    End Sub
End Class
