Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data

Partial Class CategoryReport
    Inherits System.Web.UI.Page
    Dim db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String

    Dim s12 As Integer
    Dim gv As GridView
    Private objuser As New userManager()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Page.Title = "Admin User - " & CommonFunctions.GetKeyValue(2)
        pagename.Text = "Category Report"


        Page.Title = "Category Report - " + CommonFunctions.GetKeyValue(2)
        'pagesearch.Text = "Search Admin User"
        'If db.checkUser(LCase(Application("users")), LCase(Session("loginName")), LCase(Session.SessionID)) <> "OK" Then
        '    ClientScript.RegisterClientScriptBlock(Me.GetType(), "ddd", "<script>window.parent.location='../timeout.htm';</script>")
        'End If
        'db.setLayout(Session("uId"))
        ' btnSubmit.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")
        'btnSSSubmit.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")

        If Not IsPostBack Then
            db.fillCombo(drpcat, "productCategory", "CatName", "catId", " where flg=0 and catid=1 order by catId")
            db.fillCombo(drpccat, "productCategory", "catCode + ' - ' + catName", "catId", " where flg=0 and catid=1 order by catcode")
            db.fillCombo(drpSSCat, "productCategory", "catCode + ' - ' + catName", "catId", " where flg=0 and catid=1 order by catcode")
            lblSSError.Text = ""
            lblError.Text = ""
        End If
        pnlshow.Visible = True
        pnlcategory.Visible = False
        pnlsubcat.Visible = False
        pagename.Visible = True
        If pnlcategory.Visible = True Then
            pagename.Text = "Add Category"
        End If
        If pnlsubcat.Visible = True Then
            pagename.Text = "Add Sub-Category"
        End If
        If pnlcategory.Visible = True Then
            pagename.Text = "Category Report"
        End If
    End Sub

    Protected Sub GVsubcategory_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVsubcategory.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim strId As String = GVsubcategory.DataKeys(e.Row.RowIndex).Value.ToString()
            'Dim gv As GridView = CType(e.Row.FindControl("GVsubsubcategory"), GridView)
            gv = CType(e.Row.FindControl("GVsubsubcategory"), GridView)
            AddHandler gv.RowDataBound, AddressOf gv_RowDataBound
            'AddHandler gv.RowEditing, AddressOf gv_RowEditing
            strqry1 = "select a.*,c.SCatCode + ' - ' + c.SCatName as scatname from productSubSubCategory as a,productsubcategory as c,productCategory as b where a.catID=b.catId and a.catId=" & drpcat.SelectedValue & " and a.SCatID=" & strId & " and c.ScatId=a.SCatID and a.flg=0 and b.flg=0 order by a.flg,a.SScatcode"
            dt1 = db.fillReader1(strqry1)
            dr = dt1.CreateDataReader()
            If dr.HasRows Then
                gv.DataSource = dr
                gv.DataBind()
            Else
                gv.DataSource = Nothing
                gv.DataBind()
            End If
            dr.Close()
            dt1.Clear()
            dt1.Dispose()
        End If
    End Sub

    Protected Sub gv_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Cells(1).Text = e.Row.RowIndex + 1
            s12 = DataBinder.Eval(e.Row.DataItem, "sscatid").ToString()
            Dim lkb As LinkButton = CType(e.Row.FindControl("lnkAdd"), LinkButton)

            'AddHandler lkb.Click, AddressOf lkb_clck
            'lkb.CommandName = "show"
            'If lkb.CommandName = "show" Then
            '    AddHandler gv.RowEditing, AddressOf gv_RowEditing
            'End If

        End If
    End Sub

    Protected Sub gv_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
        gv.EditIndex = e.NewEditIndex
        Dim strId As String = gv.DataKeys(e.NewEditIndex).Value.ToString()
        'pnlcategory.Visible = False
        'pnlshow.Visible = False
        'pnlsubcat.Visible = True
        'drpSSCat.SelectedValue = drpcat.SelectedValue
        'db.fillCombo(drpSubCat, "productSubCategory", "SCatCode + ' - ' + SCatName", "SCatId", " where SCatId=" & strId & "")
        'drpSubCat.SelectedValue = strId
    End Sub

    'Protected Sub lkb_clck(ByVal sender As Object, ByVal e As System.EventArgs)
    '    'Dim shift As String = DataList3.DataKeys.Item(shCount)
    '    'Dim sh As Integer = GVsubcategory.DataKeys.Item(s12)
    '    Response.Redirect("12.aspx")
    'End Sub

    Protected Sub drpcat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpcat.SelectedIndexChanged
        Dim ds As New Data.DataSet
        If drpcat.SelectedValue <> "0" Then
            GVsubcategory.DataSource = Nothing
            GVsubcategory.DataBind()
            Dim strqry1 = "select a.*,b.catCode + ' - ' + b.catname as catname from productSubCategory as a,productCategory as b where a.catID=b.catId and a.catID=" & drpcat.SelectedValue & " and a.flg=0 and b.flg=0 order by a.flg,a.ScatId,a.scatcode "

            dt1 = db.fillReader1(strqry1)
            dr = dt1.CreateDataReader()
            If dr.Read() Then
                GVsubcategory.DataSource = dr
                GVsubcategory.DataBind()
            End If
            btnmodify.Visible = True
        Else
            GVsubcategory.DataSource = Nothing
            GVsubcategory.DataBind()
            btnmodify.Visible = False

        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()

    End Sub

    Public Sub show()
        Dim ds As New Data.DataSet
        If drpcat.SelectedValue <> "0" Then
            Dim strqry1 = "select a.*,b.catCode + ' - ' + b.catname as catname from productSubCategory as a,productCategory as b where a.catID=b.catId and a.catID=" & drpcat.SelectedValue & " and a.flg=0 and b.flg=0 order by a.flg,a.ScatId,a.scatcode "
            dt1 = db.fillReader1(strqry1)
            dr = dt1.CreateDataReader()
            If dr.Read() Then
                GVsubcategory.DataSource = dr
                GVsubcategory.DataBind()
            End If
        Else
            GVsubcategory.DataSource = Nothing
            GVsubcategory.DataBind()
        End If
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
        btnmodify.Visible = True
    End Sub

    Protected Sub btnmodify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmodify.Click
        'Dim a = drpcat.SelectedValue
        'Page.RegisterStartupScript(a, "<script language=javascript>window.open('../ADM/CategoryMaster.aspx?GHCatId=" & a & "','Add_Member','width=815,height=500,top=170,left=200,scrollbars=yes');Add_Member.focus();</script>")
        pnlcategory.Visible = True
        pnlshow.Visible = False
        lblheader.Text = "Add Sub Category"
        drpccat.SelectedValue = drpcat.SelectedValue
        pagename.Text = "Add Sub Category"
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try

            Dim strSCatId As String
            strSCatId = db.getMaxId("productSubCategory", "SCatId")
            Dim qry = "delete from productSubCategory where SCatId=" & strSCatId
            db.executeQuery()
            If db.isExists("productSubCategory", "scatCode", txtCatCode.Text, False, " and SCatId<>" & strSCatId & "") = False Then
                qry = "insert into productSubCategory values(" & strSCatId & "," & drpccat.SelectedValue & ",'" & txtCatCode.Text & "','" & txtCatName.Text & "'," & drpFlg.SelectedValue & ")"
                db.executeQuery()
                txtCatCode.Text = ""
                txtCatName.Text = ""
                lblError.Text = ""
                pnlcategory.Visible = False
                pnlshow.Visible = True
                drpcat.SelectedValue = drpccat.SelectedValue
                show()
                lblheader.Text = "Product Category"
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
        lblheader.Text = "Product Category"
        drpcat.SelectedValue = drpccat.SelectedValue
        show()
    End Sub

    Protected Sub btnSSSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSSSubmit.Click
        Try
            Dim strSSCatID As String
            strSSCatID = db.getMaxId("productSubSubCategory", "SScatId")
            If db.isExists("productSubSubCategory", "SScatCode", txtCatCode.Text, False, " and SScatId<> " & strSSCatID) = False Then
                Dim qry = "delete from productSubSubCategory where SScatId=" & strSSCatID
                db.executeQuery()
                qry = "insert into productSubSubCategory values(" & strSSCatID & "," & drpSubCat.SelectedValue & "," & drpSSCat.SelectedValue & ",'" & txtSSCatCode.Text & "','" & txtSSCatName.Text & "'," & drpFlg.SelectedValue & ")"
                db.executeQuery()
                txtSSCatCode.Text = ""
                txtSSCatName.Text = ""
                lblSSError.Text = ""
                pnlshow.Visible = True
                pnlcategory.Visible = False
                pnlsubcat.Visible = False
                drpcat.SelectedValue = drpSSCat.SelectedValue
                show()
                lblheader.Text = "Product Category"
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

    Protected Sub drpSSCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpSSCat.SelectedIndexChanged
        db.fillCombo(drpSubCat, "productSubCategory", "SCatCode + ' - ' + SCatName", "SCatId", " where catId=" & drpcat.SelectedValue & " order by scatcode")
        pnlcategory.Visible = False
        pnlshow.Visible = False
        pnlsubcat.Visible = True
        txtSSCatCode.Text = ""
        txtSSCatName.Text = ""
        lblSSError.Text = ""
    End Sub

    Protected Sub GVsubcategory_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GVsubcategory.RowEditing
        GVsubcategory.EditIndex = e.NewEditIndex
        Dim strId As String = GVsubcategory.DataKeys(e.NewEditIndex).Value.ToString()
        pnlcategory.Visible = False
        pnlshow.Visible = False
        pnlsubcat.Visible = True
        drpSSCat.SelectedValue = drpcat.SelectedValue
        db.fillCombo(drpSubCat, "productSubCategory", "SCatCode + ' - ' + SCatName", "SCatId", " where SCatId=" & strId & "")
        drpSubCat.SelectedValue = strId
        lblheader.Text = "Add Sub Sub Category"
        pagename.Text = "Add Sub Sub Category"
    End Sub

    Protected Sub btnSSback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSSback.Click



        pnlcategory.Visible = False
        pnlsubcat.Visible = False
        pnlshow.Visible = True
        lblheader.Text = "Product Category"
        drpcat.SelectedValue = drpSSCat.SelectedValue
    End Sub

End Class
