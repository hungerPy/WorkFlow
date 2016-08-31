Imports System.Data

Partial Class Admin_CompanyType
    Inherits System.Web.UI.Page
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String

    Public db As New general

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        'db.setLayout(Session("userId"))
        'End If
        'btnSubmit.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")
        If Not IsPostBack Then
            showCompanyTypes()
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        db.qry = "insert into companytypes values(" & db.getMaxId("CompanyTypes", "typeId") & ",'" & txtCompanyType.Text & "')"
        db.executeQuery()
        showCompanyTypes()
        txtCompanyType.Text = ""
    End Sub

    Private Sub showCompanyTypes()
        strqry1 = "select * from CompanyTypes order by companyType"
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        gvCompany.DataSource = dr
        gvCompany.DataBind()
        dr.Close()
        dt1.Clear()
        dt1.Dispose()

    End Sub

    Protected Sub gvCompany_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvCompany.RowCancelingEdit
        gvCompany.EditIndex = -1
        txtCompanyType.Enabled = True
        showCompanyTypes()
    End Sub

    Protected Sub gvCompany_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCompany.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub gvCompany_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvCompany.RowEditing
        gvCompany.EditIndex = e.NewEditIndex
        txtCompanyType.Enabled = False
        showCompanyTypes()
    End Sub

    Protected Sub gvCompany_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvCompany.RowUpdating
        Dim s As String
        s = gvCompany.DataKeys(e.RowIndex).Value
        Dim txt As TextBox = CType(gvCompany.Rows(e.RowIndex).FindControl("txtType"), TextBox)
        If txt.Text = "" Then
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "onClick", "<script>alert('Company can not be empty.')</script>")
        Else
            db.qry = "update CompanyTypes set CompanyType='" & txt.Text & "' where typeId=" & s
            db.executeQuery()
        End If
        gvCompany.EditIndex = -1
        txtCompanyType.Enabled = True
        showCompanyTypes()

    End Sub
End Class
