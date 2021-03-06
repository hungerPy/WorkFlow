Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class general_old
    Public qry As String
    Public headerColor As String
    Public headerBG As String
    Public headerText As String
    Public headerSize As String
    Public headerWeight As String
    Public headerStyle As String
    Public headerDecoration As String
    Public headerAlignment As String
    Public formColor As String
    Public formBG As String
    Public formText As String
    Public formSize As String
    Public formWeight As String
    Public formStyle As String
    Public formDecoration As String
    Public formAlignment As String
    Public strType As String = "SQL"
    Public Shared strConn As String = ConfigurationManager.AppSettings("strConn")
    Public mName() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}
    Public noRecords() As String = {"1", "5", "10", "15", "20", "25", "30", "35", "40", "45", "50", "ALL"}
    Public productType() As String = {"[Select]", "Manufacture", "Purchase"}
    Public orientation() As String = {"[Select]", "Potrait", "Landscape"}
    Public UserType() As String = {"[Select]", "Executive", "SiteOffice"}
    Public enqType() As String = {"[Select]", "Hot", "Cold", "Negative", "Order", "Closed"}
    Public Freight() As String = {"[Select]", "ToPay", "Paid", "Recoverable"}
    Public Currency() As String = {"[Select]", "INR", "USD"}
    Public DiscType() As String = {"INR", "USD", "%"}
    Public eventType() As String = {"Wedding", "Birthday", "Other"}
    Public orderType() As String = {"[Select]", "Direct Purchase", "Through Enquiry", "Works Order"}
    Public pageSize As Integer = 25
    Public cid As Integer = "1000"

    Public Sub assignEvents(ByRef tbox As TextBox, ByVal tp As String)
        tbox.Attributes.Add("onkeypress", "doThis(" & tbox.ClientID & ",'" & tp & "')")
        tbox.Attributes.Add("ondrop", "return false")
        tbox.Attributes.Add("onpaste", "return false")
    End Sub

    Public Sub assignEvents1(ByRef tbox As TextBox, ByVal tp As String)
        tbox.Attributes.Add("onkeypress", "return checkValues(this,'" & tp & "')")
        tbox.Attributes.Add("ondrop", "return false")
        tbox.Attributes.Add("onpaste", "return false")
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

    Public Sub executeQuery(ByVal str As String)
        Dim con As New SqlConnection(ConfigurationManager.AppSettings("strConn"))
        Dim cmd As New SqlCommand(str, con)
        cmd.Connection.Open()
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub
    Public Sub executeQuery()
        Dim c As New SqlConnection(strConn)
        Dim cmd As New SqlCommand(forDbase(qry), c)
        c.Open()

        cmd.ExecuteNonQuery()
        c.Close()

    End Sub

    'Public Sub fillCombo(ByRef drp As DropDownList, ByVal tblName As String, ByVal dispField As String, ByVal valueField As String, Optional ByVal sqlWhere As String = "", Optional ByVal isSelect As Boolean = True, Optional ByVal isSelectOnly As Boolean = False)
    '    drp.Items.Clear()
    '    drp.DataSource = Nothing
    '    drp.DataBind()
    '    If Not isSelectOnly Then
    '        dispField = dispField & " as expr1"
    '        Dim con As New SqlConnection(ConfigurationManager.AppSettings("strConn"))
    '        Dim str As String = "select " & dispField & "," & valueField & " from " & tblName & sqlWhere
    '        Dim cmd As New SqlCommand(str, con)
    '        Dim dr As SqlDataReader
    '        cmd.Connection.Open()
    '        dr = cmd.ExecuteReader()
    '        drp.DataValueField = valueField
    '        drp.DataTextField = "expr1"
    '        drp.DataSource = dr
    '        drp.DataBind()
    '        con.Close()
    '    End If
    '    If isSelect Then drp.Items.Insert(0, New ListItem("[Select]", "0"))
    'End Sub

    Public Sub fillCombo(ByRef cmbBox As DropDownList, ByVal tblName As String, ByVal DisplayField As String, ByVal valueField As String, Optional ByVal sqlWhere As String = "", Optional ByVal spFor As String = "", Optional ByVal extr As String = "")
        Dim c As New SqlConnection(strConn)
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

    Public Function getMaxId(ByVal tblName As String, ByVal fldName As String, Optional ByVal sqlWhere As String = "") As Integer
        Dim c As New SqlConnection(ConfigurationManager.AppSettings("strConn"))
        Dim cmd As New SqlCommand("select max(" & fldName & ") from " & tblName & sqlWhere, c)
        Dim dr As SqlDataReader
        Dim maxId As Integer = 1
        c.Open()
        dr = cmd.ExecuteReader()
        If dr.Read Then If Not IsDBNull(dr(0)) Then maxId = dr(0) + 1
        dr.Close()
        cmd.Dispose()
        c.Close()
        dr = Nothing
        cmd = Nothing
        c = Nothing
        getMaxId = maxId
    End Function

    Public Function getFieldValue(ByVal tblname As String, ByVal fldMatch As String, ByVal fldVal As String, ByVal fldFind As String, Optional ByVal isnumericM As Boolean = False, Optional ByVal isnumericF As Boolean = False, Optional ByVal sqlWhereAttach As String = "") As Object
        Dim sql As String = "select " & fldFind & " from " & tblname & " where " & IIf(isnumericM, fldMatch & "=" & fldVal & "", fldMatch & "='" & fldVal & "'") & " " & sqlWhereAttach
        Dim c As New SqlConnection(ConfigurationManager.AppSettings("strConn"))
        Dim cmd As New SqlCommand(sql, c)
        Dim dr As SqlDataReader
        Dim s As String = ""
        cmd.Connection = c
        cmd.Connection.Open()
        dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
        If dr.Read() Then If Not IsDBNull(dr(0)) Then s = dr(0)
        dr.Close()
        c.Close()
        If s = "" Then If isnumericF = True Then s = "0"
        getFieldValue = s
    End Function


    Public Function isExists(ByVal tblName As String, ByVal fldName As String, ByVal fldValue As String, Optional ByVal isNumeric As Boolean = False, Optional ByVal sqlWhereAttach As String = "") As Boolean
        Dim c As New SqlConnection(strConn)
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

    Public Function fillDataSet() As DataSet
        Dim c As New SqlConnection(strConn)
        qry = forDbase(qry)
        Dim dtp As New SqlDataAdapter(qry, c)
        Dim dts As New DataSet
        c.Open()
        dtp.Fill(dts)
        fillDataSet = dts
        c.Close()
    End Function

    Public Sub fillReader(ByVal sql As String, ByRef dr As Object)
        Dim c As New SqlConnection(ConfigurationManager.AppSettings("strConn"))
        Dim cmd As New SqlCommand(sql, c)
        cmd.Connection = c
        cmd.Connection.Open()
        dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
    End Sub

    Public Function fillReader(Optional ByVal sql As String = "") As SqlDataReader
        Dim con As New SqlConnection(strConn)
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

    Public Sub fillDataSet(ByVal sql As String, ByRef dts As Data.DataSet, Optional ByVal startR As Integer = -1)
        Dim c As New SqlConnection(ConfigurationManager.AppSettings("strConn"))
        Dim da As New SqlDataAdapter(sql, c)
        c.Open()
        If startR <> -1 Then
            da.Fill(dts, startR, pageSize, "table")
        Else
            da.Fill(dts)
        End If
        c.Close()
    End Sub

    Public Sub fillCheckList(ByRef chkList As CheckBoxList, ByVal tblName As String, ByVal DisplayField As String, ByVal valueField As String, Optional ByVal sqlWhere As String = "", Optional ByVal spFor As String = "")
        Dim s As String
        s = "select " & valueField & " as vf," & DisplayField & " as df from " & tblName & " " & sqlWhere
        Dim dr As New Object
        fillReader(s, dr)
        chkList.DataSource = dr
        chkList.DataTextField = "df"
        chkList.DataValueField = "vf"
        chkList.DataBind()
        dr.Close()
    End Sub

    Public Function revertValue(ByVal s As String) As String
        s = Replace(s, "!!", Chr(39))
        s = Replace(s, "~~", Chr(34))
        s = Replace(s, "$$", Chr(38))
        revertValue = s
    End Function

    Function cNum(ByVal n As Double) As String
        Dim nm As String
        nm = n.ToString
        nm = Replace(FormatNumber(nm, 2), ",", "")
        cNum = nm
    End Function

    Public Function empNum(ByVal s As String) As String
        empNum = IIf(s = "", "0", s)
    End Function

    Public Sub setLayout(ByVal userId As Integer)
        Dim dr As New Object
        fillReader("select * from objectstyles where uid=" & userId, dr)
        If dr.Read() Then
            formBG = dr("formBg")
            formColor = dr("formColor")
            formSize = dr("formSize")
            formText = dr("formText")
            formAlignment = dr("formAlignment")
            formWeight = dr("formWeight")
            formStyle = dr("formStyle")
            formDecoration = dr("formDecoration")
            headerBG = dr("headerBG")
            headerColor = dr("headerColor")
            headerSize = dr("headerSize")
            headerText = dr("headerText")
            headerAlignment = dr("headerAlignment")
            headerWeight = dr("headerWeight")
            headerStyle = dr("headerStyle")
            headerDecoration = dr("headerDecoration")
        End If
        dr.Close()
    End Sub
    Public Function getLastDay(ByVal m As Integer, ByVal y As Integer) As Integer
        If m = 2 Then
            If (y Mod 400 = 0 Or (y Mod 4 = 0 And y Mod 100 <> 0)) Then
                getLastDay = 29
            Else
                getLastDay = 28
            End If
        Else
            If (m = 4 Or m = 6 Or m = 9 Or m = 11) Then
                getLastDay = 30
            Else
                getLastDay = 31
            End If
        End If
    End Function

    Public Function amtonwords(ByVal amt As String) As String
        Dim amt1 As String, arb As String, crd As String, lkh As String, thu As String, hnd As String, str As String = ""
        Dim ramt
        amt = Replace(FormatNumber(amt, 2), ",", "")
        ramt = amt.Split(".")
        amt1 = "00000000000" & ramt(0)
        amt = Right(amt1, 11)
        arb = Left(amt, 2)
        crd = Mid(amt, 3, 2)
        lkh = Mid(amt, 5, 2)
        thu = Mid(amt, 7, 2)
        hnd = Right(amt, 3)
        If CInt(arb) <> 0 Then str = chnginwords(arb, " Arab ")
        If CInt(crd) <> 0 Then str = str & chnginwords(crd, " Crore ")
        If CInt(lkh) <> 0 Then str = str & chnginwords(lkh, " Lakh ")
        If CInt(thu) <> 0 Then str = str & chnginwords(thu, " Thousand ")
        If CInt(hnd) <> 0 Then str = str & chnginwords(hnd, " Hundred ")
        If Val(ramt(1)) > 0 Then
            If str <> "" Then
                str = "Rs. " & str & " and " & getPaisa(ramt(1)) & " Paisa Only."
            Else
                str = getPaisa(ramt(1)) & " Paisa Only."
            End If
        Else
            str = "Rs. " & str & " Only."
        End If
        amtonwords = str
    End Function

    Private Function chnginwords(ByVal amt, ByVal fx)
        Dim f As Integer, s As Integer, t As Integer, str As String
        Dim fstr(9) As String, sstr(9) As String, tstr(9) As String
        fstr(0) = "" : fstr(1) = "One" : fstr(2) = "Two" : fstr(3) = "Three" : fstr(4) = "Four" : fstr(5) = "Five" : fstr(6) = "Six"
        fstr(7) = "Seven" : fstr(8) = "Eight" : fstr(9) = "Nine"
        sstr(0) = "Ten" : sstr(1) = "Eleven" : sstr(2) = "Twelve" : sstr(3) = "Thirteen" : sstr(4) = "Fourteen" : sstr(5) = "Fifteen" : sstr(6) = "Sixteen"
        sstr(7) = "Seventeen" : sstr(8) = "Eighteen" : sstr(9) = "Nineteen"
        tstr(0) = "" : tstr(1) = "" : tstr(2) = "Twenty" : tstr(3) = "Thirty" : tstr(4) = "Forty" : tstr(5) = "Fifty" : tstr(6) = "Sixty"
        tstr(7) = "Seventy" : tstr(8) = "Eighty" : tstr(9) = "Ninety"

        If fx <> " Hundred " Then
            f = CInt(Left(amt, 1))
            s = CInt(Right(amt, 1))

            If f = 0 Then
                str = fstr(s) & fx
            ElseIf f = 1 Then
                str = sstr(s) & fx
            Else
                str = tstr(f) & " " & fstr(s) & fx
            End If
        Else
            f = CInt(Left(amt, 1))
            s = CInt(Mid(amt, 2, 1))
            t = CInt(Right(amt, 1))
            If f = 0 Then
                If s = 0 Then
                    str = fstr(t)
                ElseIf s = 1 Then
                    str = sstr(t)
                Else
                    str = tstr(s) & " " & fstr(t)
                End If
            Else
                str = fstr(f) & fx
                If s = 0 Then
                    str = str & fstr(t)
                ElseIf s = 1 Then
                    str = str & sstr(t)
                Else
                    str = str & tstr(s) & " " & fstr(t)
                End If
            End If
        End If
        chnginwords = str
    End Function

    Private Function getPaisa(ByVal a As String) As String
        Dim fstr(9) As String, sstr(9) As String, tstr(9) As String
        fstr(0) = "" : fstr(1) = "One" : fstr(2) = "Two" : fstr(3) = "Three" : fstr(4) = "Four" : fstr(5) = "Five" : fstr(6) = "Six"
        fstr(7) = "Seven" : fstr(8) = "Eight" : fstr(9) = "Nine"
        sstr(0) = "Ten" : sstr(1) = "Eleven" : sstr(2) = "Twelve" : sstr(3) = "Thirteen" : sstr(4) = "Fourteen" : sstr(5) = "Fifteen" : sstr(6) = "Sixteen"
        sstr(7) = "seventeen" : sstr(8) = "Eighteen" : sstr(9) = "Nineteen"
        tstr(0) = "" : tstr(1) = "" : tstr(2) = "Twenty" : tstr(3) = "Thirty" : tstr(4) = "Forty" : tstr(5) = "Fifty" : tstr(6) = "Sixty"
        tstr(7) = "seventy" : tstr(8) = "Eighty" : tstr(9) = "Ninety"
        a = "00" & a
        Dim a1 As Integer, a2 As Integer, str As String = ""
        a = Right(a, 2)
        a1 = CInt(Left(a, 1))
        a2 = CInt(Right(a, 1))
        If a1 = 1 Then
            str = sstr(a2)
        ElseIf a1 = 0 Then
            str = fstr(a2)
        Else
            str = tstr(a1) & " " & fstr(a2)
        End If
        getPaisa = str
    End Function

    Public Function DefaultLocation(ByVal tblname As String, ByVal fieldname As String) As Integer
        Dim ID As Integer = 0
        ID = getFieldValue(tblname, "flag", 1, fieldname, True)
        DefaultLocation = ID
    End Function

    Public Function initCap(ByVal sFullName As String) As String
        Dim fCapitalizeNextLetter As String
        Dim sNewName As String = ""
        Dim sChar, i
        sFullName = Trim(sFullName)
        If sFullName = "" Then initCap = sFullName
        fCapitalizeNextLetter = True
        For i = 1 To Len(sFullName)
            sChar = Mid(sFullName, i, 1)
            If IsLetter(sChar) = True Then
                If fCapitalizeNextLetter = True Then
                    sChar = UCase(sChar)
                    fCapitalizeNextLetter = False
                Else
                    sChar = LCase(sChar)
                End If
            End If
            If sChar = " " Then fCapitalizeNextLetter = True
            sNewName = sNewName & sChar
        Next
        initCap = sNewName
    End Function

    Public Function IsLetter(ByVal sChar) As Boolean
        Dim fRet As Boolean, nASCII As Integer
        fRet = False
        nASCII = Asc(sChar)
        If ((nASCII >= 65) And (nASCII <= 90)) Then fRet = True 'Upper case letters 
        If ((nASCII >= 97) And (nASCII <= 122)) Then fRet = True 'Lower case letters 
        IsLetter = fRet
    End Function

    Public Sub fillDt(ByRef drp As DropDownList, ByVal fillWhat As String)
        Dim t
        If fillWhat = "M" Then
            For Each t In mName
                drp.Items.Add(t)
            Next
            drp.SelectedIndex = Now.Month - 1
        ElseIf fillWhat = "D" Then
            For t = 1 To 31
                drp.Items.Add(t)
            Next
            drp.SelectedIndex = Now.Day - 1
        Else
            For t = 2000 To Now.Year + 15
                drp.Items.Add(t)
            Next
            drp.SelectedIndex = (Now.Year) - 2000
        End If
    End Sub

    Public Sub filldt(ByRef drp() As DropDownList, ByVal str() As String)
        Dim i As Integer = 0
        Dim t As Object
        For i = 0 To UBound(drp)
            If str(i) = "M" Then
                For Each t In mName
                    drp(i).Items.Add(t)
                Next
                drp(i).SelectedIndex = Now.Month - 1
            ElseIf str(i) = "D" Then
                For t = 1 To 31
                    drp(i).Items.Add(t)
                Next
                drp(i).SelectedIndex = Now.Day - 1
            Else
                For t = 2000 To Now.Year + 15
                    drp(i).Items.Add(t)
                Next
                drp(i).SelectedIndex = (Now.Year) - 2000
            End If
        Next
    End Sub

    Public Sub assignDT(ByRef d1 As DropDownList, ByRef m1 As DropDownList, ByRef y1 As DropDownList)
        d1.Attributes.Add("onchange", "getDate(0," & d1.ClientID & "," & m1.ClientID & "," & y1.ClientID & ")")
        m1.Attributes.Add("onchange", "getDate(0," & d1.ClientID & "," & m1.ClientID & "," & y1.ClientID & ")")
        y1.Attributes.Add("onchange", "getDate(0," & d1.ClientID & "," & m1.ClientID & "," & y1.ClientID & ")")
    End Sub

    Public Function checkUser(ByVal app As String, ByVal ss As String, ByVal sid As String) As String
        Dim ob
        ob = Split(app, "/")
        Dim i
        Dim str
        Dim usr
        usr = ss & "-" & sid
        str = "Not Allowed"
        For i = 0 To UBound(ob)
            If LCase(usr) = LCase(ob(i)) Then str = "OK"
        Next
        checkUser = str
    End Function
End Class
