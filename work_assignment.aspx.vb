Imports System.Web.UI.HtmlControls.HtmlImage
Imports System.Data

Partial Class Admin_work_assignment
    Inherits System.Web.UI.Page
    Public db, db1 As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String
    Public Shared i As Int16 = 0

    Protected Sub rdbtn_tax_incl_excl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbtn_tax_incl_excl.SelectedIndexChanged
        drp__serv_tax.SelectedValue = 0
        drpVat.SelectedValue = 0
        txt_ser_tax_val.Text = ""
        txt_receive_amnt.Text = ""
        txt_service_tax.Text = ""
        txt_total_amnt_inc.Text = ""
        txtVtaxAmnt.Text = ""
        txtVat.Text = ""

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        btnPODetailsSave.Enabled = False
        If Not IsPostBack Then
            If Request.QueryString("poWorkAssignID") <> "" Then
                strqry1 = "select * from clientPOdetais where poWorkAssignID='" & Request.QueryString("poWorkAssignID") & "'"

                dt1 = db.fillReader1(strqry1)
                dr = dt1.CreateDataReader()
                Label1.Visible = True
                Label2.Visible = True
                If dr.Read() Then
                    txt_po_date.Text = dr("poDate").ToString()

                    txt_po_recvDate.Text = dr("poRecivingDate").ToString()
                    db.fillCombo(drp_dep_name, "Divisions", "DName", "DCode", "where flg=0")


                    db.fillCombo(drp_clientname, "clientmaster", "Companyname", "companyid", " where flg=0")

                    db.fillCombo(drp__serv_tax, "taxes", "TaxType", "tax", " where taxid=3")
                    db.fillCombo(drpVat, "taxes", "TaxType", "tax", " where taxid=2")
                    db.fillCombo(drp_activity, "Services", "Servicehead", "serviceid", "where dcode='" & drp_dep_name.SelectedValue & "'")

                    drp_clientname.SelectedValue = dr("clientid")
                    drp_dep_name.SelectedValue = dr("DCode")

                    drp__serv_tax.SelectedValue = dr("serTax")

                    drpVat.SelectedValue = dr("vat")
                    drp_activity.SelectedValue = dr("serviceid")



                    txt_remark.Text = dr("poRemarks").ToString()

                    txt_tim_frame.Text = dr("wrkCmpleteinDays").ToString()
                    txt_trgt_date.Text = dr("wrkCmpletTrgtDate").ToString()
                    txt_purchase_order_value.Text = dr("basePovalue").ToString()


                    txt_ser_tax_val.Text = drp__serv_tax.SelectedValue.ToString()
                    txt_service_tax.Text = txt_ser_tax_val.Text

                    txtVat.Text = drpVat.SelectedValue.ToString()
                    txtVtaxAmnt.Text = txtVat.Text
                    txt_receive_amnt.Text = dr("basepovalue").ToString()
                    txt_total_amnt_inc.Text = dr("totalAmnt").ToString()

                    txtDeriables.Text = dr("deriables").ToString()
                End If

                dr.Close()
                dt1.Clear()
                dt1.Dispose()


                strqry1 = "select * from clientPOpaymntStage where poworkAssignId='" & Request.QueryString("poWorkAssignID") & "'"
                dt1 = db.fillReader1(strqry1)
                dr = dt1.CreateDataReader()
                Dim j As Int16 = 0
                Dim str1, str2, str3, str4, str5, str As String
                Dim t1, t2, t3, t4 As TextBox
                Dim d1 As DropDownList
                Dim i As HtmlImage
                Dim lnk As LinkButton
                While dr.Read()

                    str = "Img" + Convert.ToString(j)
                    i = FindControl(str)
                    i.Visible = True

                    str1 = "txt_pmnt_stg_nam" + Convert.ToString(j)
                    t1 = FindControl(str1)
                    t1.Visible = True
                    str2 = "txt_pmnt_stg_per" + Convert.ToString(j)
                    t2 = FindControl(str2)
                    t2.Visible = True
                    str3 = "txt_pmnt_stg_amt" + Convert.ToString(j)
                    t3 = FindControl(str3)
                    t3.Visible = True

                    str4 = "txt_pmnt_stg_date" + Convert.ToString(j)
                    t4 = FindControl(str4)
                    t4.Visible = True

                    str5 = "drp_pmnt_stg_chk" + Convert.ToString(j)
                    d1 = FindControl(str5)
                    d1.Visible = True

                    t1.Text = dr("poStageName").ToString()
                    t2.Text = dr("poStagePercntg").ToString()
                    t3.Text = dr("poStageAmnt").ToString()
                    t4.Text = dr("poStagePaymentDate").ToString()
                    d1.SelectedValue = dr("poStageStatus").ToString()
                    If j > 0 Then
                        str5 = "lnk_btn_add" + Convert.ToString(j)

                        lnk = FindControl(str5)
                        lnk.Visible = False
                    End If


                    j = j + 1



                End While


                btnPODetailsSave.Text = "Update"

            Else
                Label1.Visible = False
                Label2.Visible = False

                db.fillCombo(drp_dep_name, "Divisions", "DName", "DCode", "where flg=0")
                db.fillCombo(drp_clientname, "clientmaster", "Companyname", "companyid", " where flg=0")
                db.fillCombo(drp__serv_tax, "taxes", "TaxType", "tax", " where taxid=3")
                db.fillCombo(drpVat, "taxes", "TaxType", "tax", " where taxid=2")
                btnPODetailsSave.Text = "Save"

            End If
            dr.Close()
            dt1.Clear()
            dt1.Dispose()


        End If

    End Sub
    Private Sub calc_tax()
        Dim stax, vtax, tot As Double
        stax = Convert.ToDouble(drp__serv_tax.SelectedValue)
        vtax = Convert.ToDouble(drpVat.SelectedValue)
        txt_receive_amnt.Text = txt_purchase_order_value.Text
        tot = Convert.ToDouble(txt_receive_amnt.Text)
        stax = (stax * tot) / 100
        vtax = (vtax * tot) / 100
        stax = Math.Round(stax, 2)
        vtax = Math.Round(vtax, 2)
        tot = Math.Round(tot, 2)
        tot = tot + stax + vtax
        txt_service_tax.Text = Convert.ToString(stax)
        txtVtaxAmnt.Text = Convert.ToString(vtax)
        txt_total_amnt_inc.Text = Convert.ToString(tot)

    End Sub
    Private Sub rev_calc_tax()
        Dim tot_tax, vtax, stax As Double
        stax = Convert.ToDouble(drp__serv_tax.SelectedValue)
        vtax = Convert.ToDouble(drpVat.SelectedValue)
        tot_tax = stax + vtax
        Dim pov_tot, pov As Double
        pov = Convert.ToDouble(txt_purchase_order_value.Text)
        pov_tot = (pov * 100) / (100 + tot_tax)
        pov_tot = Math.Round(pov_tot, 2)
        txt_receive_amnt.Text = Convert.ToString(pov_tot)

        stax = (pov_tot * stax) / 100
        stax = Math.Round(stax, 2)
        vtax = (pov_tot * vtax) / 100
        vtax = Math.Round(vtax, 2)
        txtVtaxAmnt.Text = Convert.ToString(vtax)
        txt_service_tax.Text = Convert.ToString(stax)

        txt_total_amnt_inc.Text = Convert.ToString(pov_tot + stax + vtax)
    End Sub


    Protected Sub lnk_btn_add0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_btn_add0.Click
        i = i + 1
        txt_pmnt_stg_amt1.Visible = True
        txt_pmnt_stg_nam1.Visible = True
        txt_pmnt_stg_per1.Visible = True
        lnk_btn_add1.Visible = True

    End Sub

    Protected Sub lnk_btn_add1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_btn_add1.Click
        i = i + 1
        txt_pmnt_stg_amt2.Visible = True
        txt_pmnt_stg_nam2.Visible = True
        txt_pmnt_stg_per2.Visible = True
        lnk_btn_add2.Visible = True
    End Sub

    Protected Sub lnk_btn_add2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_btn_add2.Click
        i = i + 1
        txt_pmnt_stg_amt3.Visible = True
        txt_pmnt_stg_nam3.Visible = True
        txt_pmnt_stg_per3.Visible = True
        lnk_btn_add3.Visible = True
    End Sub

    Protected Sub lnk_btn_add3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_btn_add3.Click
        i = i + 1
        txt_pmnt_stg_amt4.Visible = True
        txt_pmnt_stg_nam4.Visible = True
        txt_pmnt_stg_per4.Visible = True
    End Sub

    Protected Sub drp_dep_name_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drp_dep_name.SelectedIndexChanged
        db.fillCombo(drp_activity, "Services", "Servicehead", "serviceid", "where dcode='" & drp_dep_name.SelectedValue & "'")
    End Sub

    Protected Sub drp__serv_tax_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drp__serv_tax.SelectedIndexChanged
        txt_ser_tax_val.Text = drp__serv_tax.SelectedValue.ToString()
        If rdbtn_tax_incl_excl.SelectedItem.ToString = "Include" Then

            rev_calc_tax()
        Else
            calc_tax()
        End If
    End Sub

    Protected Sub drpVat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpVat.SelectedIndexChanged
        txtVat.Text = drpVat.SelectedValue.ToString()
        If rdbtn_tax_incl_excl.SelectedItem.ToString = "Include" Then

            rev_calc_tax()
        Else
            calc_tax()
        End If
    End Sub


    Protected Sub btnPODetailsSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPODetailsSave.Click
        Dim j, k, l As Int16
        j = 0
        l = 0
        Dim str1, str2, str3, str4, str5 As String
        Dim t1, t2, t3, t4 As TextBox
        Dim d1 As DropDownList

        If btnPODetailsSave.Text = "Update" Then
            db.qry = "update clientPOdetais set poDate='" & txt_po_date.Text & "',poRecivingDate='" & txt_po_recvDate.Text & "',clientid='" & drp_clientname.SelectedValue & "',dcode='" & drp_dep_name.SelectedValue & "',serviceid='" & drp_activity.SelectedValue & "',poRemarks='" & txt_remark.Text & "',wrkcmpleteinDays='" & txt_tim_frame.Text & "',wrkcmpletTrgtdate='" & txt_trgt_date.Text & "',basepovalue='" & txt_receive_amnt.Text & "',serTax='" & drp__serv_tax.SelectedValue & "',vat='" & drpVat.SelectedValue & "',totalAmnt='" & txt_total_amnt_inc.Text & "',deriables='" & txtDeriables.Text & "' where poworkAssignId='" & Request.QueryString("poWorkAssignID") & "'"
            db.executeQuery()

            strqry1 = "SELECT * FROM clientPOpaymntStage where poWorkAssignId='" & Request.QueryString("poWorkAssignID") & "'"
            dt1 = db.fillReader1(strqry1)
            dr = dt1.CreateDataReader()
            While dr.Read()
                k = db.getFieldValue("clientPOpaymntStage", "poWorkAssignId", Request.QueryString("poWorkAssignID"), "poPmntStgId", True)
                str1 = "txt_pmnt_stg_nam" + Convert.ToString(l)
                t1 = FindControl(str1)

                str2 = "txt_pmnt_stg_per" + Convert.ToString(l)
                t2 = FindControl(str2)
                str3 = "txt_pmnt_stg_amt" + Convert.ToString(l)
                t3 = FindControl(str3)

                str4 = "txt_pmnt_stg_date" + Convert.ToString(j)
                t4 = FindControl(str4)


                str5 = "drp_pmnt_stg_chk" + Convert.ToString(j)
                d1 = FindControl(str5)
                db.qry = "update clientPoPaymntStage set clientid='" & drp_clientname.SelectedValue & "',poWorkAssignId='" & dr("poWorkAssignId") & "',poStagename='" & t1.Text & "',poStagePercntg='" & t2.Text & "',poStageAmnt='" & t3.Text & "',poStageStatus='" & d1.SelectedValue & "',poStagePaymentDate='" & t4.Text & "'  where poPmntStgId='" & k & "'"
                db.executeQuery()
                l = l + 1
            End While

            txt_po_date.Text = ""
            txt_po_recvDate.Text = ""

            drp_clientname.SelectedValue = 0
            drp_dep_name.SelectedValue = 0
            txt_remark.Text = ""
            drp_activity.SelectedValue = 0
            txt_tim_frame.Text = ""
            txt_trgt_date.Text = ""
            txt_total_amnt_inc.Text = ""
            txtDeriables.Text = ""
            txt_purchase_order_value.Text = ""
            drp__serv_tax.SelectedValue = 0
            drpVat.SelectedValue = 0
            txt_po_recvDate.Text = ""
            txt_ser_tax_val.Text = ""
            txt_service_tax.Text = ""
            txtVat.Text = ""
            txtVtaxAmnt.Text = ""
            txt_receive_amnt.Text = ""
            dr.Close()
            dt1.Clear()
            dt1.Dispose()



        Else
            db.qry = "insert into clientPOdetais(poDate,poRecivingDate,clientid,dcode,serviceid,poRemarks,wrkcmpleteinDays,wrkcmpletTrgtdate,basepovalue,serTax,vat,totalAmnt,deriables) values('" & txt_po_date.Text & "','" & txt_po_recvDate.Text & "'," & drp_clientname.SelectedValue & ",'" & drp_dep_name.SelectedValue & "'," & drp_activity.SelectedValue & ",'" & txt_remark.Text & "'," & txt_tim_frame.Text & ",'" & txt_trgt_date.Text & "'," & txt_receive_amnt.Text & "," & drp__serv_tax.SelectedValue & "," & drpVat.SelectedValue & "," & txt_total_amnt_inc.Text & ",'" & txtDeriables.Text & "')"
            db.executeQuery()

            strqry1 = "SELECT TOP 1 * FROM clientPOdetais ORDER BY poWorkAssignId DESC"
            dt1 = db.fillReader1(strqry1)
            dr = dt1.CreateDataReader()
            While dr.Read()



                While (j < i + 1)
                    str1 = "txt_pmnt_stg_nam" + Convert.ToString(j)
                    t1 = FindControl(str1)

                    str2 = "txt_pmnt_stg_per" + Convert.ToString(j)
                    t2 = FindControl(str2)
                    str3 = "txt_pmnt_stg_amt" + Convert.ToString(j)
                    t3 = FindControl(str3)
                    db.qry = "insert into clientPoPaymntStage (clientid,poWorkAssignId,poStagename,poStagePercntg,poStageAmnt,poStageStatus)values(" & drp_clientname.SelectedValue & "," & dr("poWorkAssignId") & ",'" & t1.Text & "'," & t2.Text & "," & t3.Text & ",'Pending') "

                    db.executeQuery()
                    j = j + 1
                End While

            End While

            dr.Close()
            dt1.Clear()
            dt1.Dispose()

            txt_po_date.Text = ""
            txt_po_recvDate.Text = ""

            drp_clientname.SelectedValue = 0
            drp_dep_name.SelectedValue = 0
            txt_remark.Text = ""
            drp_activity.SelectedValue = 0
            txt_tim_frame.Text = ""
            txt_trgt_date.Text = ""
            txt_total_amnt_inc.Text = ""
            txtDeriables.Text = ""
            txt_purchase_order_value.Text = ""
            drp__serv_tax.SelectedValue = 0
            drpVat.SelectedValue = 0
            txt_po_recvDate.Text = ""
            txt_ser_tax_val.Text = ""
            txt_service_tax.Text = ""
            txtVat.Text = ""
            txtVtaxAmnt.Text = ""
            txt_receive_amnt.Text = ""


            j = 0

            While (j < 5)
                str1 = "txt_pmnt_stg_nam" + Convert.ToString(j)
                t1 = FindControl(str1)
                t1.Text = ""
                str2 = "txt_pmnt_stg_per" + Convert.ToString(j)
                t2 = FindControl(str2)
                t2.Text = ""
                str3 = "txt_pmnt_stg_amt" + Convert.ToString(j)
                t3 = FindControl(str3)
                t3.Text = ""
                If (j > 0) Then
                    t1.Visible = False
                    t2.Visible = False
                    t3.Visible = False
                End If
                j = j + 1
            End While
            lnk_btn_add1.Visible = False
            lnk_btn_add2.Visible = False
            lnk_btn_add3.Visible = False
            i = 0
        End If

        txtTotPer.Text = ""
        btnPODetailsSave.Enabled = True
    End Sub

    Protected Sub lnkper_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkper.Click
        Dim j As Integer = 0
        Dim str1, t1
        While (j < 5)
            str1 = "txt_pmnt_stg_per" + Convert.ToString(j)
            t1 = FindControl(str1)
            If txtTotPer.Text = "" Then
                txtTotPer.Text = CInt(t1.Text)
            Else
                txtTotPer.Text = CInt(txtTotPer.Text) + CInt(t1.Text)
            End If


            If (j > 0) Then
                If t1.Text = 0 Then
                    t1.Visible = False
                Else
                    t1.Visible = True
                End If

            End If
            j = j + 1

        End While
        If txtTotPer.Text <> 100 Then
            txtTotPer.BackColor = Drawing.Color.Yellow
            lblerr.Visible = True
            Exit Sub
        Else
            lblerr.Visible = False
            txtTotPer.BackColor = Drawing.Color.White
            btnPODetailsSave.Enabled = True
        End If
    End Sub

    'Protected Sub txt_pmnt_stg_amt0_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_pmnt_stg_amt0.TextChanged
    '    blank()
    'End Sub
    Private Sub blank()
        txtTotPer.Text = ""
    End Sub

    'Protected Sub txt_pmnt_stg_amt1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_pmnt_stg_amt1.TextChanged
    '    blank()
    'End Sub

    'Protected Sub txt_pmnt_stg_amt2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_pmnt_stg_amt2.TextChanged
    '    blank()
    'End Sub

    'Protected Sub txt_pmnt_stg_amt3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_pmnt_stg_amt3.TextChanged
    '    blank()
    'End Sub

    'Protected Sub txt_pmnt_stg_amt4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_pmnt_stg_amt4.TextChanged
    '    blank()
    'End Sub

    Protected Sub txt_pmnt_stg_per0_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_pmnt_stg_per0.TextChanged
        blank()
    End Sub

    Protected Sub txt_pmnt_stg_per1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_pmnt_stg_per1.TextChanged
        blank()
    End Sub

    Protected Sub txt_pmnt_stg_per2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_pmnt_stg_per2.TextChanged
        blank()
    End Sub

    Protected Sub txt_pmnt_stg_per3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_pmnt_stg_per3.TextChanged
        blank()
    End Sub

    Protected Sub txt_pmnt_stg_per4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_pmnt_stg_per4.TextChanged
        blank()
    End Sub

    Protected Sub drp_clientname_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drp_clientname.SelectedIndexChanged

    End Sub
End Class
