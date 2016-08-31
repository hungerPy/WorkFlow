Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data

Partial Class Admin_adminuser
    Inherits System.Web.UI.Page
    Private objuser As New userManager()
    Dim dsadmin As New DataSet()
    Dim db As New general

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Page.Title = "Admin User - " & CommonFunctions.GetKeyValue(2)
        pagename.Text = "Admin User Listing"
        pagesearch.Text = "Search Admin User"
        GVMenu.PageSize = Convert.ToInt32(CommonFunctions.GetKeyValue(1))
        txtsearch.Focus()
        If Not IsPostBack Then
            If Request.QueryString("mode") = "add" Then

                lblmsgs.Visible = True
                lblmsgs.Text = "<div class='alert alert-success'><strong>Success!</strong> Admin User details are added successfully.</div>"
            ElseIf Request.QueryString("mode") = "edit" Then
                lblmsgs.Visible = True
                lblmsgs.Text = "<div class='alert'><strong>Update!</strong> Admin User details are updated successfully.</div>"
            ElseIf Request.QueryString("mode") = "delete" Then
                lblmsgs.Visible = True
                lblmsgs.Text = "<div class='alert alert-danger'><strong>Delete!</strong> Admin User details are deleted successfully.</div>"
            End If

            '----------------------Maintain Search------------------------------------
            If Request.QueryString("q") <> "" Then
                txtsearch.Text = Request.QueryString("q")
            End If

            BindMenu()
        End If
    End Sub
    Public Sub BindMenu()

        Try
            objuser.emailaddress = txtsearch.Text.Trim()
            If Session("AdminType") IsNot Nothing AndAlso Convert.ToString(Session("AdminType")) = "subadmin" Then
                trmsg.Visible = False
                objuser.adminid = Convert.ToInt32(Session("AdminID"))
                dsadmin = objuser.SelectSingleItem()
            Else
                dsadmin = objuser.SearchItem()
            End If

            GVMenu.DataSource = dsadmin
            GVMenu.DataBind()
            If dsadmin.Tables(0).Rows.Count > 0 Then

                If GVMenu.PageCount < 2 Then
                    GVMenu.PageIndex = 0
                End If
            Else
                btndel.Visible = False
            End If
            'LoadDropDownList()
        Catch ex As Exception
            Throw ex
        Finally
            dsadmin.Dispose()
            dsadmin.Clear()
        End Try
    End Sub


    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs)

        GVMenu.PageIndex = 0
        BindMenu()
    End Sub
    Protected Sub LoadDropDownList()

        Dim addstr As [String] = String.Empty
        ddlpage.ClearSelection()
        ddlpage.Items.Clear()

        For i As Integer = 0 To GVMenu.PageCount - 1
            addstr = (Convert.ToSingle(i + 1) & " of ") + GVMenu.PageCount
            ddlpage.Items.Add(addstr)
            ddlpage.Items(ddlpage.Items.Count - 1).Value = Convert.ToString(i)
        Next
        ddlpage.SelectedValue = Convert.ToString(GVMenu.PageIndex)

    End Sub
    Protected Sub ddlpage_SelectedIndexChanged(sender As Object, e As EventArgs)
        GVMenu.PageIndex = Convert.ToInt32(ddlpage.SelectedItem.Value)
        BindMenu()
    End Sub
    Protected Sub GVMenu_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GVMenu.PageIndex = e.NewPageIndex
        BindMenu()
    End Sub

    Protected Sub lnkStatus_click(sender As Object, e As EventArgs)
        Dim row As GridViewRow = TryCast(DirectCast(sender, LinkButton).Parent.Parent, GridViewRow)
        objuser.isactive = Convert.ToByte(If(Convert.ToInt32(DirectCast(sender, LinkButton).CommandArgument) = 0, 1, 0))
        objuser.adminid = Convert.ToInt32(GVMenu.DataKeys(GVMenu.Rows(row.RowIndex).RowIndex).Value.ToString())
        objuser.UpdateActive()
        BindMenu()
    End Sub
    Protected Sub GVMenu_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If Session("AdminType") IsNot Nothing AndAlso Convert.ToString(Session("AdminType")) = "subadmin" Then
            e.Row.Cells(0).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim uid As Integer = Convert.ToInt32(GVMenu.DataKeys(e.Row.RowIndex).Values(0))
            Dim emp As Integer = db.getFieldValue("users", "uid", uid, "empid")
            e.Row.Cells(1).Text = db.getFieldValue("director", "ids", emp, "dirname")

            'Dim emp As Integer = dsadmin.Tables(0).Rows(0)(1)("empid").ToString()
            'Dim ltradmintype As Literal = DirectCast(e.Row.FindControl("ltradmintype"), Literal)
            'Dim ltruserid As Literal = DirectCast(e.Row.FindControl("ltruserid"), Literal)
            ''If ltradmintype.Text = "superadmin" Then
            ''    Dim chk As CheckBox = DirectCast(e.Row.FindControl("chkDel"), CheckBox)
            ''    chk.Enabled = False
            ''End If

            'If Session("AdminType") IsNot Nothing AndAlso ltradmintype.Text = "superadmin" AndAlso Convert.ToString(Session("AdminType")) = "subadmin" Then

            '    e.Row.Cells(4).Visible = False

            'End If
        End If

    End Sub

    'Gridview sorting 
    Protected Sub GVMenu_Sorting(sender As Object, e As GridViewSortEventArgs)
        If e.SortExpression.ToString() <> String.Empty Then
            If SortDirection.ToLower() = "asc" Then
                SortDirection = "desc"
            Else
                SortDirection = "asc"
            End If
            SortExpression = e.SortExpression & " " & SortDirection
            BindMenu()
        End If
    End Sub

    'handle sort direction
    Private Property SortDirection() As String
        Get
            Return hdSortdirection.Value.ToString()
        End Get
        Set(value As String)
            hdSortdirection.Value = value
        End Set
    End Property

    'handle sortexpression
    Private Property SortExpression() As String
        Get
            If ViewState("SortExpression") Is Nothing Then
                ViewState("SortExpression") = [String].Empty
            End If
            Return ViewState("SortExpression").ToString()
        End Get
        Set(value As String)
            ViewState("SortExpression") = value
        End Set
    End Property


    Protected Sub btndel_Click(sender As Object, e As EventArgs)
        Dim con As Integer = 0
        Dim flag As Integer = 0
        Dim chk As New CheckBox()
        Dim dtmenu As New DataTable()

        For i As Integer = 0 To GVMenu.Rows.Count - 1
            chk = DirectCast(GVMenu.Rows(i).FindControl("chkDel"), CheckBox)
            If chk.Checked = True Then
                con += 1
                objuser.adminid = Convert.ToInt32(GVMenu.DataKeys(GVMenu.Rows(i).RowIndex).Value.ToString())
                objuser.DeleteItem()
            End If
        Next
        Response.Redirect(("adminuser.aspx?mode=delete&q=" + txtsearch.Text & "&page=") + ddlpage.SelectedValue)
    End Sub

    Protected Sub GVMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GVMenu.SelectedIndexChanged

    End Sub
End Class
