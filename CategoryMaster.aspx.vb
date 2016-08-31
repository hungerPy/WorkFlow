Imports System.Data

Partial Class Admin_CategoryMaster
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String

    Dim s12 As Integer
    Dim gv As GridView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If db.checkUser(Application("users"), Session("loginName"), Session.SessionID) <> "OK" Then
        '    Response.Redirect("sessionOut.aspx")
        'Else
        ' db.setLayout(Session("userId"))
        'End If
        'btnSubmit.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")
        'btnSSSubmit.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")

        If Not IsPostBack Then
            db.fillCombo(drpcat, "CategoryMaster", "category", "cId", " where flg=0 order by category")
            db.fillCombo(drpccat, "CategoryMaster", "category", "cId", " where flg=0 order by category")
            'db.fillCombo(drpSSCat, "productCategory", "catCode + ' - ' + catName", "catId", " order by catcode")
            lblSSError.Text = ""
            lblError.Text = ""
        End If
        pnlshow.Visible = True
        pnlcategory.Visible = False
        pnlsubcat.Visible = False
    End Sub

    Protected Sub GVsubcategory_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVsubcategory.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim strId As String = GVsubcategory.DataKeys(e.Row.RowIndex).Value.ToString()
            Dim flg As Boolean = DataBinder.Eval(e.Row.DataItem, "flg").ToString()
            If flg = False Then
                e.Row.Cells(2).Text = "Active"
            Else
                e.Row.Cells(2).Text = "Inactive"
            End If
        End If
    End Sub

    Protected Sub drpcat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpcat.SelectedIndexChanged
        Dim ds As New Data.DataSet
        If drpcat.SelectedValue <> "0" Then
            db.qry = "select a.*, b.category as catname from SubCategoryMaster as a,CategoryMaster as b where a.cID=b.cId and a.cID=" & drpcat.SelectedValue & " and a.flg=0 and b.flg=0 order by a.flg,a.subcategory "
            ds = db.fillDataSet()
            If ds.Tables(0).Rows.Count > 0 Then
                GVsubcategory.DataSource = ds
                GVsubcategory.DataBind()
            Else
                GVsubcategory.DataSource = Nothing
                GVsubcategory.DataBind()
            End If
        Else
            GVsubcategory.DataSource = Nothing
            GVsubcategory.DataBind()
        End If
        ds.Dispose()
        btnmodify.Visible = True
    End Sub

    Public Sub show()
        Dim ds As New Data.DataSet
        If drpcat.SelectedValue <> "0" Then
            GVsubcategory.DataBind()
            db.qry = "select a.*, b.category as catname from SubCategoryMaster as a,CategoryMaster as b where a.cID=b.cId and a.cID=" & drpcat.SelectedValue & " and a.flg=0 and b.flg=0 order by a.flg,a.subcategory "
            ds = db.fillDataSet()
            If ds.Tables(0).Rows.Count > 0 Then
                GVsubcategory.DataSource = ds
                GVsubcategory.DataBind()
            Else
                GVsubcategory.DataSource = Nothing
                GVsubcategory.DataBind()
            End If
        Else
            GVsubcategory.DataSource = Nothing
            GVsubcategory.DataBind()
        End If
        ds.Dispose()
        btnmodify.Visible = True
    End Sub

    Protected Sub btnmodify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmodify.Click
        Dim ds As New Data.DataSet
        db.qry = "select * from CategoryMaster where flg=0 order by category"
        ds = db.fillDataSet()
        If ds.Tables(0).Rows.Count > 0 Then
            gvcategory.DataSource = ds
            gvcategory.DataBind()
        Else
            gvcategory.DataSource = Nothing
            gvcategory.DataBind()
        End If
        ds.Dispose()
        pnlcategory.Visible = True
        pnlshow.Visible = False
        drpccat.SelectedValue = drpcat.SelectedValue
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If txtid.Text = "" Then txtid.Text = db.getMaxId("CategoryMaster", "CId")
            If Not db.isExists("CategoryMaster", "category", txtCatName.Text, False, " and CId<>" & txtid.Text & "") Then
                If txtCatName.Text = "" Then
                    'lblError.Text = "Please enter category name"
                    pnlcategory.Visible = True
                    pnlshow.Visible = False
                Else
                    db.qry = "delete from CategoryMaster where CId=" & txtid.Text
                    db.executeQuery()

                    db.qry = "insert into CategoryMaster values(" & txtid.Text & ",'" & txtCatName.Text & "'," & rdbstatus.SelectedValue & ")"
                    db.executeQuery()
                    If txtid.Text <> "" Then
                        Dim e1 As New GridViewCancelEditEventArgs(Convert.ToInt32(txtid.Text))
                        gvcategory_RowCancelingEdit(sender, e1)
                    End If
                    txtid.Text = ""
                    txtCatName.Text = ""
                    lblError.Text = "Successful Inserted"
                    'pnlcategory.Visible = False
                    'pnlshow.Visible = True
                    pnlcategory.Visible = True
                    pnlshow.Visible = False
                    'drpcat.SelectedValue = drpccat.SelectedValue
                    showcategory()
                    btnSubmit.Text = "Submit"
                End If
            Else
                pnlcategory.Visible = True
                pnlshow.Visible = False
                lblError.Text = "Already Exists!!!"
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCback.Click
        pnlcategory.Visible = False
        pnlshow.Visible = True
        show()
    End Sub

    Protected Sub btnSSSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSSSubmit.Click
        Try
            If txtid.Text = "" Then txtid.Text = db.getMaxId("SubCategoryMaster", "SCId")
            If Not db.isExists("SubCategoryMaster", "subcategory", txtSCatName.Text, False, " and SCId<> " & txtid.Text) Then
                If txtSCatName.Text = "" Then
                    'lblSSError.Text = "Please enter sub category name"
                    pnlshow.Visible = False
                    pnlcategory.Visible = False
                    pnlsubcat.Visible = True
                Else
                    db.qry = "delete from SubCategoryMaster where SCId=" & txtid.Text
                    db.executeQuery()
                    db.qry = "insert into SubCategoryMaster values(" & txtid.Text & "," & drpccat.SelectedValue & ",'" & txtSCatName.Text & "'," & rbtsubcat.SelectedValue & ")"
                    db.executeQuery()
                    If txtid.Text <> "" Then
                        Dim e1 As New GridViewCancelEditEventArgs(Convert.ToInt32(txtid.Text))
                        gvsubcat_RowCancelingEdit(sender, e1)
                    End If
                    txtid.Text = ""
                    txtSCatName.Text = ""
                    lblSSError.Text = "Successful Inserted"
                    'pnlshow.Visible = True
                    'pnlcategory.Visible = False
                    'pnlsubcat.Visible = False
                    pnlshow.Visible = False
                    pnlcategory.Visible = False
                    pnlsubcat.Visible = True
                    btnSSSubmit.Text = "Submit"
                    'drpcat.SelectedValue = drpccat.SelectedValue
                    showsubcategory()
                End If
            Else
                pnlshow.Visible = False
                pnlcategory.Visible = False
                pnlsubcat.Visible = True
                lblError.Text = "Already Exists!!!"
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GVsubcategory_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GVsubcategory.RowEditing
        GVsubcategory.EditIndex = e.NewEditIndex
        Dim strId As String = GVsubcategory.DataKeys(e.NewEditIndex).Value.ToString()
        showsubcategory()
        pnlcategory.Visible = False
        pnlshow.Visible = False
        pnlsubcat.Visible = True
        drpccat.SelectedValue = drpcat.SelectedValue
    End Sub

    Protected Sub btnSSback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSSback.Click
        pnlcategory.Visible = False
        pnlsubcat.Visible = False
        pnlshow.Visible = True
        drpcat.SelectedValue = drpccat.SelectedValue
    End Sub

    Protected Sub gvcategory_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvcategory.RowCancelingEdit
        gvcategory.EditIndex = -1
        txtCatName.Text = ""
        rdbstatus.SelectedValue = 0
        showcategory()
        btnSubmit.Text = "Submit"
        pnlcategory.Visible = True
        pnlshow.Visible = False
    End Sub

    Protected Sub gvcategory_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvcategory.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim strId As String = gvcategory.DataKeys(e.Row.RowIndex).Value.ToString()
            Dim flg As Boolean = DataBinder.Eval(e.Row.DataItem, "flg").ToString()
            If flg = False Then
                e.Row.Cells(2).Text = "Active"
            Else
                e.Row.Cells(2).Text = "Inactive"
            End If
        End If
    End Sub

    Public Sub showcategory()
        Dim ds As New Data.DataSet
        db.qry = "select * from CategoryMaster order by category"
        ds = db.fillDataSet()
        If ds.Tables(0).Rows.Count > 0 Then
            gvcategory.DataSource = ds
            gvcategory.DataBind()
        Else
            gvcategory.DataSource = Nothing
            gvcategory.DataBind()
        End If
        ds.Dispose()
    End Sub

    Protected Sub gvcategory_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvcategory.RowEditing
        gvcategory.EditIndex = e.NewEditIndex
        Dim strId As String = gvcategory.DataKeys(e.NewEditIndex).Value.ToString()
        strqry1 = "select * from CategoryMaster where cid=" & strId
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        While dr.Read()
            txtid.Text = dr("cid").ToString()
            txtCatName.Text = dr("category").ToString()
            Dim flg As Boolean = dr("flg")
            If flg = False Then
                rdbstatus.SelectedValue = 0
            Else
                rdbstatus.SelectedValue = 1
            End If
        End While
        btnSubmit.Text = "Update"
        lblError.Text = ""
        dr.Close()
        dt1.Clear()
        dt1.Dispose()

        showcategory()
        pnlcategory.Visible = True
        pnlshow.Visible = False
    End Sub

    Protected Sub gvsubcat_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvsubcat.RowCancelingEdit
        gvsubcat.EditIndex = -1
        txtSCatName.Text = ""
        rbtsubcat.SelectedValue = 0
        showsubcategory()
        btnSSSubmit.Text = "Submit"
        pnlshow.Visible = False
        pnlcategory.Visible = False
        pnlsubcat.Visible = True
    End Sub

    Protected Sub gvsubcat_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvsubcat.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim strId As String = gvsubcat.DataKeys(e.Row.RowIndex).Value.ToString()
            Dim flg As Boolean = DataBinder.Eval(e.Row.DataItem, "flg").ToString()
            If flg = False Then
                e.Row.Cells(2).Text = "Active"
            Else
                e.Row.Cells(2).Text = "Inactive"
            End If
        End If
    End Sub

    Protected Sub gvsubcat_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvsubcat.RowEditing
        gvsubcat.EditIndex = e.NewEditIndex
        Dim strId As String = gvsubcat.DataKeys(e.NewEditIndex).Value.ToString()
        strqry1 = "select * from subCategoryMaster where scid=" & strId
        dt1 = db.fillReader1(strqry1)
        dr = dt1.CreateDataReader()
        While dr.Read()
            txtid.Text = dr("scid").ToString()
            db.fillCombo(drpccat, "CategoryMaster", "category", "cId", " where flg=0 order by category")
            drpccat.SelectedValue = dr("cid").ToString()
            txtSCatName.Text = dr("subcategory").ToString()
            Dim flg As Boolean = dr("flg")
            If flg = False Then
                rbtsubcat.SelectedValue = 0
            Else
                rbtsubcat.SelectedValue = 1
            End If
        End While
        btnSSSubmit.Text = "Update"
        lblSSError.Text = ""
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
        showsubcategory()
        pnlshow.Visible = False
        pnlcategory.Visible = False
        pnlsubcat.Visible = True
    End Sub

    Protected Sub drpccat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpccat.SelectedIndexChanged
        Dim ds As New Data.DataSet
        db.qry = "select * from subCategoryMaster where cid=" & drpccat.SelectedValue & " order by subcategory"
        ds = db.fillDataSet()
        If ds.Tables(0).Rows.Count > 0 Then
            gvsubcat.DataSource = ds
            gvsubcat.DataBind()
        Else
            gvsubcat.DataSource = Nothing
            gvsubcat.DataBind()
        End If
        ds.Dispose()
        pnlshow.Visible = False
        pnlcategory.Visible = False
        pnlsubcat.Visible = True
    End Sub

    Public Sub showsubcategory()
        Dim ds As New Data.DataSet
        db.qry = "select * from subCategoryMaster where cid=" & drpcat.SelectedValue & " order by subcategory"
        ds = db.fillDataSet()
        If ds.Tables(0).Rows.Count > 0 Then
            gvsubcat.DataSource = ds
            gvsubcat.DataBind()
        Else
            gvsubcat.DataSource = Nothing
            gvsubcat.DataBind()
        End If
        ds.Dispose()
        pnlshow.Visible = False
        pnlcategory.Visible = False
        pnlsubcat.Visible = True
    End Sub

    Protected Sub btnsubcat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubcat.Click
        showsubcategory()
        pnlcategory.Visible = False
        pnlshow.Visible = False
        pnlsubcat.Visible = True
        drpccat.SelectedValue = drpcat.SelectedValue
    End Sub
End Class
