Imports System.Data

Partial Class Admin_MasterReport
    Inherits System.Web.UI.Page
    Public db As New general

    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If db.checkUser(LCase(Application("users")), LCase(Session("loginName")), LCase(Session.SessionID)) <> "OK" Then
        '    ClientScript.RegisterClientScriptBlock(Me.GetType(), "ddd", "<script>window.parent.location='../timeout.htm';</script>")
        'End If
        'db.setLayout(Session("uId"))
        'btnSubmit.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")
        'btnWSubmit.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")

        If Not IsPostBack Then
            pagename.Text = "Counter Master"
            Page.Title = "Counter Master - " + CommonFunctions.GetKeyValue(2)
            db.fillCombo(drpplant, "plantMaster", "PlantName", "PlantId")
            db.fillCombo(drpSPlant, "plantMaster", "plantCode + ' - ' + plantName", "plantId")
            db.fillCombo(drpCountry, "country", "countryName", "countryid")
            drpCountry.SelectedValue = db.DefaultLocation("country", "countryid")
            db.fillCombo(drpState, "state", "statename", "stateid", " where countryid=" & drpCountry.SelectedValue)
            drpState.SelectedValue = db.DefaultLocation("state", "stateid")
            db.fillCombo(drpCity, "city", "cityname", "cityid", " where stateid=" & drpState.SelectedValue)
            drpCity.SelectedValue = db.DefaultLocation("city", "cityid")
            db.fillCombo(drpWPlant, "plantMaster", "plantCode + ' - ' + plantName", "plantId")
            db.fillCombo(drpSiteOffice, "siteOfficeMaster", "siteCode + ' - ' + siteLText", "siteOffId", " where 1=2")
        End If
        pnlsiteoff.Visible = False
        pnlwarehouse.Visible = False
        lblError.Text = ""
        lblWError.Text = ""
    End Sub

    Protected Sub GVsiteoff_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVsiteoff.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim strId As String = GVsiteoff.DataKeys(e.Row.RowIndex).Value.ToString()
            Dim gv As GridView = CType(e.Row.FindControl("GVwarehouse"), GridView)
            AddHandler gv.RowDataBound, AddressOf gv_RowDataBound
            Dim qry = "select a.*,b.PlantName,c.siteLText from wareHouseMaster as a, plantMaster as b, siteOfficeMaster as c where a.plantId=b.plantId and a.siteOffId=c.siteOffId and a.plantId=c.PlantId and a.siteoffId=" & strId & " and a.plantID=" & drpplant.SelectedValue & "  order by a.flg"
            dt1 = db.fillReader1(qry)
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
            e.Row.Cells(1).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub drpplant_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpplant.SelectedIndexChanged
        Dim ds As New Data.DataSet


        If drpplant.SelectedValue <> "0" Then
            btneditsiteoffice.Visible = False
            btneditwarehouse.Visible = False
            btnsiteoff.Visible = True
            Dim strqry = "Select * from siteofficeMaster as s,plantmaster as p where s.plantId=p.PlantId and p.plantid=" & drpplant.SelectedValue & " order by flg"
            db.fillDataSet()
            If ds.Tables(0).Rows.Count > 0 Then
                GVsiteoff.DataSource = ds
                GVsiteoff.DataBind()
            Else
                GVsiteoff.DataSource = Nothing
                GVsiteoff.DataBind()
            End If
        Else
            GVsiteoff.DataSource = Nothing
            GVsiteoff.DataBind()
            btneditsiteoffice.Visible = True
            btneditwarehouse.Visible = True
            btnsiteoff.Visible = False

        End If
        ds.Dispose()

    End Sub

    Protected Sub btnsiteoff_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsiteoff.Click
        drpSPlant.SelectedValue = drpplant.SelectedValue
        lblheader.Text = "Site Office Master"
        pagename.Text = "Site Office Master"
        pnlshow.Visible = False
        pnlwarehouse.Visible = False
        pnlsiteoff.Visible = True
    End Sub

    Public Sub show()
        Dim ds As New Data.DataSet
        If drpplant.SelectedValue <> "0" Then
            Dim strqry = "Select * from siteofficeMaster as s,plantmaster as p where s.plantId=p.PlantId and p.plantid=" & drpplant.SelectedValue & " order by flg"
            db.fillDataSet()
            If ds.Tables(0).Rows.Count > 0 Then
                GVsiteoff.DataSource = ds
                GVsiteoff.DataBind()
                pnlshow.Visible = True

            End If
            ds.Dispose()
            btnsiteoff.Visible = True
        Else
            GVsiteoff.DataSource = Nothing
            GVsiteoff.DataBind()
        End If
    End Sub

    Protected Sub GVsiteoff_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GVsiteoff.RowEditing
        GVsiteoff.EditIndex = e.NewEditIndex
        Dim strId As String = GVsiteoff.DataKeys(e.NewEditIndex).Value.ToString()
        pnlshow.Visible = False
        pnlsiteoff.Visible = False
        pnlwarehouse.Visible = True
        lblheader.Text = "Warehouse Master"
        pagename.Text = "Warehouse Master"
        drpWPlant.SelectedValue = drpplant.SelectedValue
        db.fillCombo(drpSiteOffice, "siteOfficeMaster", "siteCode + ' - ' + siteLText", "siteOffId", " where siteOffId=" & strId & "")
        drpSiteOffice.SelectedValue = strId
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim strSOId As String
            strSOId = db.getMaxId("siteOfficeMaster", "siteOffId")
            Dim qry = "delete from siteOfficeMaster where siteOffId=" & strSOId
            db.executeQuery()
            If db.isExists("siteOfficeMaster", "siteCode", txtSiteCode.Text, False, " and siteOffId <>" & strSOId) = False Then
                qry = "insert into siteOfficeMaster values(" & strSOId & "," & drpSPlant.SelectedValue & ",'" & _
                txtSiteCode.Text & "','" & txtSiteName.Text & "','" & txtContPerson.Text & "','" & _
                txtDesg.Text & "','" & txtAdd1.Text & "','" & txtAdd2.Text & "','" & txtAdd3.Text & "'," & _
                drpCountry.SelectedValue & "," & drpState.SelectedValue & "," & drpCity.SelectedValue & ",'" & _
                txtOffNo.Text & "','" & txtMob.Text & "'," & drpFlg.SelectedValue & "," & _
                Session("uid") & ",'" & Format(Date.Now.Date, "dd-MMM-yyyy") & "'," & txtAddon.Text & "," & txtMaxDisc.Text & ")"
                db.executeQuery()
                clearControls()
                lblError.Text = ""
                pnlsiteoff.Visible = False
                pnlwarehouse.Visible = False
                pnlshow.Visible = True
                lblheader.Text = "SiteOffice & Warehouse Master"
                drpplant.SelectedValue = drpSPlant.SelectedValue
                show()
                Response.Redirect("MasterReport.aspx")
            Else
                pnlsiteoff.Visible = True
                pnlshow.Visible = False
                lblError.Text = "Already Exists!!!"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub drpCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpCountry.SelectedIndexChanged
        db.fillCombo(drpState, "state", "statename", "stateid", " where countryid=" & drpCountry.SelectedValue)
        db.fillCombo(drpCity, "city", "cityname", "cityid", " where 1=2")
        pnlsiteoff.Visible = True
    End Sub

    Protected Sub drpState_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpState.SelectedIndexChanged
        db.fillCombo(drpCity, "city", "cityname", "cityid", " where stateid=" & drpState.SelectedValue)
        pnlsiteoff.Visible = True
    End Sub

    Private Sub clearControls()
        txtId.Text = ""
        txtSiteCode.Text = ""
        txtSiteName.Text = ""
        txtContPerson.Text = ""
        txtDesg.Text = ""
        txtAdd1.Text = ""
        txtAdd2.Text = ""
        txtAdd3.Text = ""
        drpCountry.SelectedValue = db.DefaultLocation("country", "countryid")
        db.fillCombo(drpState, "state", "statename", "stateid", " where countryid=" & drpCountry.SelectedValue)
        drpState.SelectedValue = db.DefaultLocation("state", "stateid")
        db.fillCombo(drpCity, "city", "cityname", "cityid", " where stateid=" & drpState.SelectedValue)
        drpCity.SelectedValue = db.DefaultLocation("city", "cityid")
        txtOffNo.Text = ""
        txtMob.Text = ""
        txtEmail.Text = ""
        txtWebsite.Text = ""
        txtMaxDisc.Text = ""
        txtAddon.Text = ""
    End Sub

    Protected Sub btnWSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWSubmit.Click

        If drpWPlant.SelectedValue = 2 Then
            If db.isExists("warehousemaster", "siteoffid", drpSiteOffice.SelectedValue, False, " and flg=0 ") = True Then
                ClientScript.RegisterStartupScript(Me.GetType(), "OnLoad", "<script language=javascript>alert('Only One Warehouse Add in FSA Plant.')</script>")
                Exit Sub
            Else
                Try
                    Dim strWhId As String
                    strWhId = db.getMaxId("wareHouseMaster", "wareHouseId")
                    If db.isExists("wareHouseMaster", "wareHouseCode", txtWHCode.Text, False, " and wareHouseId<>" & strWhId) = False Then
                        Dim qry = "delete from wareHouseMaster where wareHouseId=" & strWhId
                        db.executeQuery()
                        qry = "insert into wareHouseMaster values(" & strWhId & "," & drpWPlant.SelectedValue & "," & _
                        drpSiteOffice.SelectedValue & ",'" & txtWHCode.Text & "','" & txtWHName.Text & "'," & _
                        drpwFlag.SelectedValue & "," & Session("uid") & ",'" & Format(Date.Now.Date, "dd-MMM-yyyy") & "')"
                        db.executeQuery()
                        pnlshow.Visible = True
                        pnlsiteoff.Visible = False
                        pnlwarehouse.Visible = False
                        lblWError.Text = ""
                        txtWHCode.Text = ""
                        txtWHName.Text = ""
                        drpplant.SelectedIndex = drpWPlant.SelectedValue
                        lblheader.Text = "SiteOffice & Warehouse Master"
                        show()
                    Else
                        pnlshow.Visible = False
                        pnlsiteoff.Visible = False
                        pnlwarehouse.Visible = True
                        lblWError.Text = "Already Exists!!!"
                    End If
                Catch ex As Exception
                    lblWError.Text = ex.Message
                End Try
            End If


        Else
            Try
                Dim strWhId As String
                strWhId = db.getMaxId("wareHouseMaster", "wareHouseId")
                If db.isExists("wareHouseMaster", "wareHouseCode", txtWHCode.Text, False, " and wareHouseId<>" & strWhId) = False Then
                    Dim qry = "delete from wareHouseMaster where wareHouseId=" & strWhId
                    db.executeQuery()
                    qry = "insert into wareHouseMaster values(" & strWhId & "," & drpWPlant.SelectedValue & "," & _
                    drpSiteOffice.SelectedValue & ",'" & txtWHCode.Text & "','" & txtWHName.Text & "'," & _
                    drpwFlag.SelectedValue & "," & Session("uid") & ",'" & Format(Date.Now.Date, "dd-MMM-yyyy") & "')"
                    db.executeQuery()
                    pnlshow.Visible = True
                    pnlsiteoff.Visible = False
                    pnlwarehouse.Visible = False
                    lblWError.Text = ""
                    txtWHCode.Text = ""
                    txtWHName.Text = ""
                    drpplant.SelectedIndex = drpWPlant.SelectedValue
                    lblheader.Text = "SiteOffice & Warehouse Master"
                    show()
                Else
                    pnlshow.Visible = False
                    pnlsiteoff.Visible = False
                    pnlwarehouse.Visible = True
                    lblWError.Text = "Already Exists!!!"
                End If
            Catch ex As Exception
                lblWError.Text = ex.Message
            End Try
        End If


        'Try
        '    Dim strWhId As String
        '    strWhId = db.getMaxId("wareHouseMaster", "wareHouseId")
        '    If db.isExists("wareHouseMaster", "wareHouseCode", txtWHCode.Text, False, " and wareHouseId<>" & strWhId) = False Then
        '        Dim qry = "delete from wareHouseMaster where wareHouseId=" & strWhId
        '        db.executeQuery(qry)
        '        qry = "insert into wareHouseMaster values(" & strWhId & "," & drpWPlant.SelectedValue & "," & _
        '        drpSiteOffice.SelectedValue & ",'" & txtWHCode.Text & "','" & txtWHName.Text & "'," & _
        '        drpwFlag.SelectedValue & "," & Session("uid") & ",'" & Format(Date.Now.Date, "dd-MMM-yyyy") & "')"
        '        db.executeQuery(qry)
        '        pnlshow.Visible = True
        '        pnlsiteoff.Visible = False
        '        pnlwarehouse.Visible = False
        '        lblWError.Text = ""
        '        txtWHCode.Text = ""
        '        txtWHName.Text = ""
        '        drpplant.SelectedIndex = drpWPlant.SelectedValue
        '        lblheader.Text = "SiteOffice & Warehouse Master"
        '        show()
        '    Else
        '        pnlshow.Visible = False
        '        pnlsiteoff.Visible = False
        '        pnlwarehouse.Visible = True
        '        lblWError.Text = "Already Exists!!!"
        '    End If
        'Catch ex As Exception
        '    lblWError.Text = ex.Message
        'End Try
    End Sub

    Protected Sub drpWPlant_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpWPlant.SelectedIndexChanged
        db.fillCombo(drpSiteOffice, "siteOfficeMaster", "siteCode + ' - ' + siteLText", "siteOffId", " where plantID=" & drpWPlant.SelectedValue & "")
        pnlshow.Visible = False
        pnlsiteoff.Visible = False
        pnlwarehouse.Visible = True
        lblWError.Text = ""
        txtWHCode.Text = ""
        txtWHName.Text = ""
    End Sub

    Protected Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
        pnlsiteoff.Visible = False
        pnlwarehouse.Visible = False
        pnlshow.Visible = True
        drpplant.SelectedValue = drpSPlant.SelectedValue
        lblheader.Text = "SiteOffice & Warehouse Master"
        pagename.Text = "Counter Master"
        show()
    End Sub

    Protected Sub btnwback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnwback.Click
        pnlsiteoff.Visible = False
        pnlwarehouse.Visible = False
        pnlshow.Visible = True
        drpplant.SelectedValue = drpWPlant.SelectedValue
        lblheader.Text = "SiteOffice & Warehouse Master"
        pagename.Text = "Counter Master"
        show()
    End Sub

    Protected Sub btneditsiteoffice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btneditsiteoffice.Click
        Response.Redirect("SiteOfficeMaster.aspx")
    End Sub

    Protected Sub btneditwarehouse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btneditwarehouse.Click
        Response.Redirect("WarehouseMaster.aspx")
    End Sub

End Class
