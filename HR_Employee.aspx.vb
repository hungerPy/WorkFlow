Imports System.Data

Partial Class Admin_HR_Employee
    Inherits System.Web.UI.Page
    Public db As New general
    Dim dt1, dt2 As New DataTable()
    Dim dr, dr1 As DataTableReader
    Dim strqry1, strqry2 As String
    Dim rcount As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If db.checkUser(LCase(Application("users")), LCase(Session("loginName")), LCase(Session.SessionID)) <> "OK" Then
        '    ClientScript.RegisterClientScriptBlock(Me.GetType(), "ddd", "<script>window.parent.location='../timeout.htm';</script>")
        'End If
        'db.setLayout(Session("uId"))


        If Not IsPostBack Then
            pagename.Text = "Employee Master"

            Page.Title = "Employee Master - " + CommonFunctions.GetKeyValue(2)
            db.fillCombo(drpCompanyName, "company", "CompanyName", "CompanyAbbr", " order by CompanyId")
            db.fillCombo(DrpDivision, "Divisions", "DivisionHead", "DivisionId", " order by Priority,DivisionHead")
            'drpType.Items.Add("On Salary")
            'drpType.Items.Add("Contractor")
            db.fillCombo(drpemps, "Employee", "empName", "empId", " order by empName")
            txtDOB.Text = Date.Now.Date.ToString("dd-MMM-yyyy")
            txtDOJ.Text = Date.Now.Date.ToString("dd-MMM-yyyy")
            db.assignEvents(txtEmpName, "alpha")
            Dim i As Integer
            For i = 1 To 12
                drpOH.Items.Add(Right("00" & i, 2))
                drpIH.Items.Add(Right("00" & i, 2))
            Next
            drpIH.SelectedValue = "10"
            drpOH.SelectedValue = "07"
            For i = 0 To 45 Step 15
                drpIM.Items.Add(Right("00" & i, 2))
                drpOM.Items.Add(Right("00" & i, 2))
            Next

            If Request.QueryString("empid") <> "" Then
                showEmp()
                btnSubmit.Text = "Update Employee"
            End If
        End If
        'showEmp()
        'db.assignEvents(txtremarks, "fnum")

        'btnSubmit.Attributes.Add("onclick", "return validate(" & form1.ClientID & ",'','')")
        'btnRemove.Attributes.Add("onclick", "return confirm('Are you sure want to remove the data of selected employee.')")
    End Sub

    Private Sub showEmp()
        Dim qry = "select * from employee where empid=" & Request.QueryString("empid") & ""
        dt1 = db.fillReader1(qry)
        dr = dt1.CreateDataReader()
        While dr.Read()
            txtId.Text = dr("empid")
            drpCompanyName.SelectedValue = dr("CompId")
            DrpDivision.SelectedValue = dr("DivisionId")
            txtEmpName.Text = dr("empname")
            txtDesignation.Text = dr("Designation")
            drpType.SelectedValue = dr("empType")
            txtAdd.Text = dr("address")
            txtPhone.Text = dr("phone")
            txtMobile.Text = dr("mobile")
            txtDOB.Text = dr("DOB")
            rblSex.SelectedValue = dr("sex")
            txtDOJ.Text = dr("DOJ")
            txtEmail.Text = dr("email")
            txtEmpNo.Text = dr("empno")
            txtremarks.Text = dr("Remarks")
            Dim strIn As String = dr("INtime")
            Dim a() As String = strIn.Split(":")
            Dim a1() As String = a(1).ToString().Split(" ")
            drpIH.SelectedValue = a(0).ToString()
            drpIM.SelectedValue = a1(0).ToString()
            drpIAP.SelectedItem.Text = a1(1).ToString()
            Dim strOut As String = dr("OutTime")
            Dim b() As String = strOut.Split(":")
            Dim b1() As String = b(1).ToString().Split(" ")
            drpOH.SelectedValue = b(0).ToString()
            drpOM.SelectedValue = b1(0).ToString()
            drpOAP.SelectedItem.Text = b1(1).ToString()
            drpemps.SelectedValue = dr("ReportingTo")
            lblImage.Text = dr("Emp_photo")
            If lblImage.Text <> "" Then
                EmpImg.Src = "~/EmployeePhotos/" & lblImage.Text
            End If
        End While
        dr.Close()
        dt1.Clear()
        dt1.Dispose()

    End Sub

    'Protected Sub GrdEmp_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdEmp.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Cells(0).Text = e.Row.RowIndex + 1
    '    End If
    'End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim d, dj, it, ot As String
        Dim str As String = ""
        d = txtDOB.Text ' d1.SelectedValue & "-" & m1.SelectedValue & "-" & y1.SelectedValue
        dj = txtDOJ.Text ' d2.SelectedValue & "-" & m2.SelectedValue & "-" & y2.SelectedValue
        it = drpIH.SelectedValue & ":" & drpIM.SelectedValue & " " & drpIAP.SelectedValue
        ot = drpOH.SelectedValue & ":" & drpOM.SelectedValue & " " & drpOAP.SelectedValue
        Dim eid As Integer = db.empNum(txtId.Text)
        If eid = 0 Then eid = db.getMaxId("Employee", "empid")
        Dim imgname As String
        imgname = lblImage.Text

        If (fup1.HasFile) Then
            If ((System.IO.Path.GetExtension(fup1.FileName).ToLower() <> ".jpg") And (System.IO.Path.GetExtension(fup1.FileName).ToLower <> ".gif")) Then
                lblError.Text = "* file format nor supported."
                Exit Sub
            Else
                If imgname = "" Then imgname = "Img" & eid & System.IO.Path.GetExtension(fup1.FileName)
                fup1.SaveAs(Server.MapPath("~/EmployeePhotos/") & imgname)
                lblError.Text = ""
            End If
        End If
        Dim qry = "delete from employee where empid=" & eid
        db.executeQuery()

        qry = "insert into Employee values(" & eid & ",'" & drpCompanyName.SelectedValue & "'," & DrpDivision.SelectedValue & ",'" & txtEmpNo.Text & "','" & drpType.SelectedValue & "','" & txtEmpName.Text & "','" & txtDesignation.Text & "','" & _
        txtAdd.Text & "','" & txtMobile.Text & "','" & txtPhone.Text & "','" & d & "','" & rblSex.SelectedValue & "','" & dj & "','" & txtEmail.Text & "','" & _
        txtremarks.Text & "','','" & it & "','" & ot & "','" & drpemps.SelectedValue & "','" & imgname & "',0,0,0,0,0,0)"
        db.executeQuery()

        qry = "update attn set inTime='" & it & "',outTime='" & ot & "' where empid=" & eid & " and status='Pending'"
        db.executeQuery()

        If Not db.isExists("SalaryStruct", "empId", eid, True) Then
            qry = "insert into SalaryStruct values(" & db.getMaxId("SalaryStruct", "ssid") & ",'" & drpCompanyName.SelectedValue & "'," & DrpDivision.SelectedValue & "," & eid & ",'0','0','0','0','0','0','0','0',0)"
            db.executeQuery()
        Else
            qry = "update SalaryStruct set divisionId=" & DrpDivision.SelectedValue & " where empId=" & eid & ""
            db.executeQuery()
        End If

        'If txtId.Text <> "" Then
        '    If txtUserId.Text <> "" And txtPwd.Text <> "" Then
        '        db.qry = "select * from rights"
        '        dr = db.fillReader()
        '        Dim i As Integer = 0
        '        For i = 1 To dr.FieldCount - 1
        '            If str <> "" Then str = str & ","
        '            str = str & "'False'"
        '        Next
        '        dr.close()
        '        

        '        db.qry = "insert into rights values (" & eid & "," & str & ")"
        '        db.executeQuery()
        '        db.qry = "insert into objectstyles(headercolor,headerBG,headerText,headerSize,headerWeight,headerStyle,headerDecoration,headerAlignment,formColor,formBG,formText,formSize,formWeight,formStyle,formDecoration,formalignment)" & _
        '                                    " select headercolor,headerBG,headerText,headerSize,headerWeight,headerStyle,headerDecoration,headerAlignment,formColor,formBG,formText,formSize,formWeight,formStyle,formDecoration,formalignment from objectstyles where uid=0"
        '        db.executeQuery()
        '        db.qry = "update objectstyles set objectid=" & db.getMaxId("objectstyles", "objectid") & " , uid=" & eid & " where uid is null"
        '        db.executeQuery()
        '    End If
        'End If

        'End If
        'Else
        '    If Not db.isExists("Employee", "userid", txtUserId.Text, False, " and empid<>" & drpEmployee.SelectedValue) Then
        '        db.qry = "update Employee set empname='" & txtEmpName.Text & "',address='" & txtAdd.Text & "',mobile='" & txtMobile.Text & "',phone='" & txtPhone.Text & "',dob='" & d & "',sex='" & rblSex.SelectedValue & "',inTime='" & it & "',outTime='" & ot & "',userId='" & txtUserId.Text & "',pwd='" & txtPwd.Text & "' where empid=" & drpEmployee.SelectedValue
        '        db.executeQuery()
        '        btnSubmit.Text = "Add Employee"
        '    Else
        '        lblError.Text = "*Duplicate UserId"
        '        Exit Sub
        '    End If
        'End If
        'showEmp()
        'clearData()
        'db.fillCombo(drpEmployee, "Employee", "EMpName", "empId")
        If Request.QueryString("empid") = "" Then
            Response.Redirect("HR_Employee.aspx")
        Else
            Response.Redirect("HR_employeelist.aspx?comp=" & Request.QueryString("comp") & "&dept=" & Request.QueryString("dept") & "")
        End If
    End Sub

    'Protected Sub drpEmployee_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpEmployee.SelectedIndexChanged
    '    If drpEmployee.SelectedIndex = 0 Then
    '        btnSubmit.Text = "Add Employee"
    '        clearData()
    '    Else
    '        btnSubmit.Text = "Edit Employee"
    '        showEmpData()
    '    End If


    'End Sub
    'Private Sub showEmpData()
    '    Dim it, ot As Object
    '    db.qry = "Select * from Employee where empId=" & drpEmployee.SelectedValue
    '    dr = db.fillReader
    '    If dr.read Then
    '        txtEmpName.Text = dr("EmpName")
    '        txtAdd.Text = dr("address")
    '        txtPhone.Text = dr("phone")
    '        txtMobile.Text = dr("mobile")
    '        d1.SelectedValue = Right("00" & CDate(dr("DOB")).Day, 2)
    '        m1.SelectedIndex = Right("00" & CDate(dr("DOB")).Month, 2)
    '        y1.SelectedValue = CDate(dr("DOB")).Year
    '        rblSex.SelectedValue = dr("sex")
    '        it = Split(Replace(dr("InTime"), " ", ":"), ":")
    '        ot = Split(Replace(dr("OutTime"), " ", ":"), ":")
    '        drpIAP.SelectedValue = it(2)
    '        drpIH.SelectedValue = it(0)
    '        drpIM.SelectedValue = it(1)
    '        drpOAP.SelectedValue = ot(2)
    '        drpOH.SelectedValue = ot(0)
    '        drpOM.SelectedValue = ot(1)
    '        txtUserId.Text = dr("userId")
    '        txtPwd.Text = dr("pwd")
    '        btnRemove.enabled = True
    '    Else
    '        clearData()
    '    End If
    '    dr.close()
    '    

    'End Sub

    'Private Sub clearData()
    '    txtEmpName.Text = ""
    '    txtAdd.Text = ""
    '    txtMobile.Text = ""
    '    txtPhone.Text = ""
    '    d1.SelectedIndex = 0
    '    m1.SelectedIndex = 0
    '    y1.SelectedIndex = 0
    '    rblSex.SelectedValue = "M"
    '    drpIAP.SelectedValue = "AM"
    '    drpIH.SelectedValue = "10"
    '    drpIM.SelectedValue = "00"
    '    drpOAP.SelectedValue = "PM"
    '    drpOH.SelectedValue = "07"
    '    drpOM.SelectedValue = "00"
    '    txtUserId.Text = ""
    '    txtPwd.Text = ""
    '    btnRemove.enabled = False
    'End Sub

    'Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
    '    db.qry = "insert into xEmployee select * from Employee where empid=" & txtId.Text
    '    db.executeQuery()
    '    db.qry = "delete from Employee where empid=" & txtId.Text
    '    db.executeQuery()
    '    Response.Redirect("AddEmp.aspx")
    'End Sub

    'Protected Sub DrpDivision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrpDivision.SelectedIndexChanged
    '    db.fillCombo(drpShift, "shifts", "Shifttitle + '(' + intime + ' - ' + str(whours) + ' Hours)'", "shiftId", " where companyCode='" & drpCompanyName.SelectedValue & "' and dCode='" & DrpDivision.SelectedValue & "'")
    'End Sub
End Class
