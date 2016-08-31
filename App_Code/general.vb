Imports Microsoft.VisualBasic
'Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data

Public Class general

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
    Public qry As String
    Public Shared strConn As String = ConfigurationManager.AppSettings("strConn")
    Public strType As String = "SQL"
    Public mName() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}
    Public plans() As String = {"First", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eighth", "Ninth", "Tenth"}
    'Public floors() As String = {"[Select]", "BS-1", "BS-2", "GF", "Ist", "IInd", "IIIrd", "IVth", "Vth", "VIth", "VIIth", "VIIIth", "IXth", "Xth", "XIth", "XIIth", "XIIIth", "XIVth", "XVth", "XVIth", "XVIIth", "XVIIIth", "XIXth", "XXth", "XXIst", "XXIInd", "XXIIIrd", "XXIVth", "XXVth"}
    'change in Floors for RSG Group on 2-Feb-2012
    Public floors() As String = {"[Select]", "BS-1", "BS-2", "BS-3", "LG", "GF", "UG", "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th", "11th", "12th", "13th", "14th", "15th", "16th", "17th", "18th", "19th", "20th", "21st", "22nd", "23rd", "24th", "25th", "26th", "27th", "28th", "29th", "30th"}
    Public towers() As String = {"[Select]", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
    'Public flatType() As String = {"[Select]", "1BHK", "2BHK", "3BHK", "4BHK", "5BHK", "6BHK", "PENTHOUSE"}
    'change in Flat Type for RSG Group on 2-Feb-2012
    Public flatType() As String = {"[Select]", "1BHK", "2BHK", "2BHK-Servant", "2BHK-Study", "2BHK-PH", "3BHK", "3BHK-Servant", "3BHK-Duplex-Servant", "3BHK-PH", "4BHK", "4BHK-Servant", "4BHK-PH", "5BHK", "6BHK", "PENTHOUSE"}
    Public OfficeType() As String = {"[Select]", "Shop", "Office", "Kiosk", "Showroom", "Food Court", "Restaurant", "Cafe - Atrium", "ATM", "Family ENT. CENT.", "Anchor SH.", "Theatre"}
    Public noRecords() As String = {"1", "5", "10", "15", "20", "25", "30", "35", "40", "45", "50", "ALL"}
    Public enqTh() As String = {"[Select]", "CALL RECD", "CALLING", "SITE VISIT", "CLIENT VISIT"}
    Public enqTh1() As String = {"[Select]", "CALL", "EMAIL", "SMS", "VISIT", "LETTER"}
    Public Fo_Status() As String = {"[Select]", "Booking", "Due_R1", "Due_R2", "Due_R3", "Due_R4", "Registry_Sent", "Registry Received", "Completion_H1", "Completion_H2", "Completion_H3", "Handover Certificate", "Cancellation"}
    Public cid As Integer = 68

    Dim dtp As New SqlDataAdapter
    Dim dts As New DataSet
    Dim drw As DataRow
    Dim c As SqlConnection

    Public Sub assignEvents(ByRef tbox As TextBox, ByVal tp As String)
        tbox.Attributes.Add("onkeypress", "doThis(" & tbox.ClientID & ",'" & tp & "')")
        tbox.Attributes.Add("ondrop", "return doThis(" & tbox.ClientID & ",'')")
        tbox.Attributes.Add("onpaste", "return doThis(" & tbox.ClientID & ",'')")
    End Sub

    Public Sub executeQuery()
        Dim cs As New SqlConnection
        Try
            Dim ConexecuteQuery As String = ConfigurationManager.AppSettings("strConn")
            cs = New SqlConnection(ConexecuteQuery)
            Dim cmd As New SqlCommand(forDbase(qry), cs)
            cs.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            cs.Close()
            cs.Dispose()
        End Try
    End Sub
    
    Public Function getMaxId(ByVal tblName As String, ByVal fldName As String, Optional ByVal tblName1 As String = "", Optional ByVal fldName1 As String = "") As Integer
        Dim dr As SqlDataReader
        Try
            Dim CongetMaxId As String = ConfigurationManager.AppSettings("strConn")
            c = New SqlConnection(CongetMaxId)
            Dim cmd As New SqlCommand()
            cmd.Connection = c
            If tblName1 = "" Then
                cmd.CommandText = "select max(" & fldName & ") from " & tblName
            Else
                cmd.CommandText = "select max(a) from (select max(" & fldName & ") as a from " & tblName & " union all select max(" & fldName1 & ") as a from " & tblName1 & ") as b"
            End If
            Dim maxId As Integer = 1
            c.Open()
            dr = cmd.ExecuteReader()
            If dr.Read Then If Not IsDBNull(dr(0)) Then maxId = dr(0) + 1
            getMaxId = maxId
            dr.Close()
        Catch ex As Exception
            Throw ex
        Finally
            dr.Close()
            c.Close()
            c.Dispose()
        End Try
    End Function

    Public Function fillReader(Optional ByVal sql As String = "") As SqlDataReader
        Try
            Dim ConfillReader As String = ConfigurationManager.AppSettings("strConn")
            Dim con As New SqlConnection(ConfillReader)
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
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fillReader1(Optional ByVal sql As String = "") As DataTable
        Dim objcon As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConn"))
        Dim dt As New DataTable()
        Try
            If sql <> "" Then
                dt = New DataTable()
                objcon.Open()
                qry = forDbase(sql)
                Dim sqlcmd As New SqlCommand(qry, objcon)
                Dim sqladp As New SqlDataAdapter(sqlcmd)
                sqladp.Fill(dt)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            dt.Dispose()
            objcon.Close()
            objcon.Dispose()
        End Try
    End Function


    Public Sub fillCombo(ByRef cmbBox As DropDownList, ByVal tblName As String, ByVal DisplayField As String, ByVal valueField As String, Optional ByVal sqlWhere As String = "", Optional ByVal spFor As String = "", Optional ByVal extr As String = "")
        Dim fillcomboc As New SqlConnection
        Try
            Dim Connfillcombo As String = ConfigurationManager.AppSettings("strConn")
            fillcomboc = New SqlConnection(Connfillcombo)
            Dim s As String
            s = "select " & DisplayField & " as df," & valueField & " as vf from " & tblName & " " & sqlWhere
            dtp = New SqlDataAdapter(s, fillcomboc)
            dts = New DataSet
            s = forDbase(s)
            fillcomboc.Open()
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
        Catch ex As Exception
            Throw ex
        Finally
            dts.Tables.Clear()
            dts.Dispose()
            fillcomboc.Close()
            fillcomboc.Dispose()
        End Try
    End Sub

    Public Sub fillCheckList(ByRef chkList As CheckBoxList, ByVal tblName As String, ByVal DisplayField As String, ByVal valueField As String, Optional ByVal sqlWhere As String = "", Optional ByVal spFor As String = "")
        Dim dr As New Object
        Try
            Dim ConConnfillcombo As String = ConfigurationManager.AppSettings("strConn")
            c = New SqlConnection(ConConnfillcombo)
            Dim s As String
            s = "select " & valueField & " as vf," & DisplayField & " as df from " & tblName & " " & sqlWhere
            s = forDbase(s)
            Dim cmd As New SqlCommand(s, c)
            c.Open()
            dr = cmd.ExecuteReader()
            chkList.DataSource = dr
            chkList.DataTextField = "df"
            chkList.DataValueField = "vf"
            chkList.DataBind()

        Catch ex As Exception
            Throw ex
        Finally
            dr.Close()
            c.Close()
            c.Dispose()
        End Try
    End Sub

    Public Sub fillListBox(ByRef lBox As ListBox, ByVal tblName As String, ByVal DisplayField As String, ByVal valueField As String, Optional ByVal sqlWhere As String = "", Optional ByVal spFor As String = "")
        Dim dr As New Object
        Try
            Dim ConfillListBox As String = ConfigurationManager.AppSettings("strConn")
            c = New SqlConnection(ConfillListBox)
            Dim s As String
            s = "select " & valueField & " as vf," & DisplayField & " as df from " & tblName & " " & sqlWhere
            s = forDbase(s)
            Dim cmd As New SqlCommand(s, c)
            c.Open()
            dr = cmd.ExecuteReader
            lBox.DataSource = dr
            lBox.DataTextField = "df"
            lBox.DataValueField = "vf"
            lBox.DataBind()

        Catch ex As Exception
            Throw ex
        Finally
            dr.Close()
            c.Close()
            c.Dispose()
        End Try
    End Sub

    Public Function isExists(ByVal tblName As String, ByVal fldName As String, ByVal fldValue As String, Optional ByVal isNumeric As Boolean = False, Optional ByVal sqlWhereAttach As String = "") As Boolean
        Try
            Dim ConisExists As String = ConfigurationManager.AppSettings("strConn")
            c = New SqlConnection(ConisExists)
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
        Catch ex As Exception
            Throw ex
        Finally
            c.Close()
            c.Dispose()
        End Try
    End Function

    Public Function fillDataSet() As DataSet
        Try
            Dim ConfillDataSet As String = ConfigurationManager.AppSettings("strConn")
            c = New SqlConnection(ConfillDataSet)
            qry = forDbase(qry)
            Dim dtp As New SqlDataAdapter(qry, c)
            Dim dts As New DataSet
            c.Open()
            dtp.Fill(dts)
            fillDataSet = dts
        Catch ex As Exception
            Throw ex
        Finally
            c.Close()
            c.Dispose()
        End Try
    End Function

    Public Function fillDataTable() As DataTable
        Try
            Dim ConfillDataTable As String = ConfigurationManager.AppSettings("strConn")
            c = New SqlConnection(ConfillDataTable)
            Dim dtp As New SqlDataAdapter(qry, c)
            Dim dt As New DataTable
            Using dt
                dt.Columns.Add("Sno", System.Type.GetType("System.Int32")).AutoIncrement = True
                dt.Columns("Sno").AutoIncrementSeed = 1
                dt.Columns("Sno").AutoIncrementStep = 1
                dtp.Fill(dt)
            End Using
            c.Open()
            fillDataTable = dt
        Catch ex As Exception
            Throw ex
        Finally
            c.Close()
            c.Dispose()
        End Try
    End Function

    Public Function getFieldValue(ByVal tblName As String, ByVal fldName As String, ByVal fldValue As String, ByVal fldFind As String, Optional ByVal isNumeric As Boolean = False, Optional ByVal sqlWhereAttach As String = "") As Object
        Dim dr As New Object, v
        Dim getFieldValuec As New SqlConnection
        Try
            Dim CongetFieldValue As String = ConfigurationManager.AppSettings("strConn")
            getFieldValuec = New SqlConnection(CongetFieldValue)
            Dim sql As String
            If isNumeric = True Then
                sql = "select " & fldFind & " from " & tblName & " where " & fldName & "=" & fldValue & " " & sqlWhereAttach
            Else
                sql = "select " & fldFind & " from " & tblName & " where " & fldName & "='" & fldValue & "' " & sqlWhereAttach
            End If
            sql = forDbase(sql)
            Dim cmd As New SqlCommand(sql, getFieldValuec)
            getFieldValuec.Open()
            dr = cmd.ExecuteReader()
            If dr.read() Then
                v = IIf(IsDBNull(dr(0)), "", dr(0))
            Else
                v = ""
            End If
            getFieldValue = v
        Catch ex As Exception
            Throw ex
        Finally
            dr.close()
            getFieldValuec.Close()
            getFieldValuec.Dispose()
        End Try
    End Function

    Public Sub clearStory(ByVal userid As String)
        qry = "update plots set status='Available' where status='" & userid & "'"
        executeQuery()
        qry = "update flats set status='Available' where status='" & userid & "'"
        executeQuery()
        qry = "update CommercialUnits set status='Available' where status='" & userid & "'"
        executeQuery()
    End Sub

    Public Sub setLayout(ByVal userId As Integer, ByVal frm As Object)
        Dim dr As New Object
        qry = "select * from objectstyles where uid=" & userId
        dr = fillReader()
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
        setTabIndex(frm)
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

    Public Function amtonwords1(ByVal amt As String) As String
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
                str = "Rs. " & str & " Point " & getPaisa(ramt(1))
            Else
                str = getPaisa(ramt(1))
            End If
        Else
            str = "Rs. " & str
        End If
        amtonwords1 = str
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
        sstr(7) = "Seventeen" : sstr(8) = "Eighteen" : sstr(9) = "Nineteen"
        tstr(0) = "" : tstr(1) = "" : tstr(2) = "Twenty" : tstr(3) = "Thirty" : tstr(4) = "Forty" : tstr(5) = "Fifty" : tstr(6) = "Sixty"
        tstr(7) = "Seventy" : tstr(8) = "Eighty" : tstr(9) = "Ninety"
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

    Function cNum(ByVal n As Double) As String
        Dim nm As String
        nm = n.ToString
        nm = Replace(FormatNumber(nm, 0), ",", "")
        cNum = nm
    End Function

    Function empNum(ByVal s As String) As String
        If s = "" Then s = "0"
        empNum = s
    End Function

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
    Public Function getRAmount(ByVal unqCode As String, Optional ByVal s As String = "") As Double
        Dim str As String
        Dim rAmt As Double = 0
        str = getFieldValue("paymentdetails", "uniquecode", unqCode, "sum(chequeAmount+(chequeAmount*stax/100))", False, s)
        If str <> "" Then rAmt = CDbl(str)
        str = getFieldValue("installments", "uniquecode", unqCode, "sum(chequeAmount+(chequeAmount*stax/100))", False, s)
        If str <> "" Then rAmt = rAmt + CDbl(str)
        getRAmount = rAmt
    End Function
    Public Sub insertBroker(ByVal unitCost As Double, ByVal unqCode As String, ByVal brkId As Integer, ByVal Exeid As Integer, ByVal amtr As Double, ByVal bdate As String, ByVal CommT As String, ByVal CommA As Double, ByVal CommP As Double, ByVal Area As Double)
        Dim cb As New SqlConnection
        Try
            Dim CongetFieldValue As String = ConfigurationManager.AppSettings("strConn")
            cb = New SqlConnection(CongetFieldValue)
            Dim sql As String
            sql = "insert into BrokerP(rid,brokerid,ExecutiveId,UniqueCode,unitcost,amountr,dor,area,CommType,CommAmt,commP,commA) values "
            sql = sql & "(" & getMaxId("brokerP", "rid") & "," & brkId & "," & Exeid & ",'" & unqCode & "'," & cNum(unitCost) & "," & cNum(amtr) & ",'" & bdate & "'," & Area & ",'" & CommT & "'," & CommA & "," & CommP & ",0)"
            Dim c As New SqlConnection(strConn)
            Dim cmd As New SqlCommand(sql, cb)
            cb.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            cb.Close()
            cb.Dispose()
        End Try
        ' updateCommission(brkId)
    End Sub
    Public Sub updateCommission(ByVal brkid As Integer)
        Dim dt1, dt2 As New DataTable()
        Dim dr, dr1 As DataTableReader
        Dim cntb As String
        cntb = empNum(getFieldValue("brokerP", "brokerid", brkid, "count(*)", True, ""))
        qry = "update brokerp set commP=0,commA=0 where brokerid=" & brkid
        executeQuery()
        If cntb = "0" Then Exit Sub
        Dim commp As Double

        qry = "select year(bdate),month(bdate),count(*) from booking where brokerid=" & brkid & " and appid>0 group by year(bdate),month(bdate) order by year(bdate),month(bdate)"
        dt1 = fillReader1(qry)
        dr = dt1.CreateDataReader()

        While dr.Read
            If dr(2) <= 5 Then
                commp = 2
            ElseIf dr(2) <= 10 Then
                commp = 3
            Else
                commp = 3.5
            End If
            qry = "update brokerP set commp = " & commp & ",commA=round((amountr * " & commp & ")/100,2) where uniquecode in (select uniqueCode from booking where year(bdate)=" & dr(0) & " and month(bdate)=" & dr(1) & " and brokerid=" & brkid & " and appid>0)"
            executeQuery()
        End While
        dr.Close()
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    Public Sub updateBooking(ByVal Unitno As String, ByVal prj As String)
        Dim tcost, bcost As Double, unqCode As String
        Dim apid As Integer
        tcost = getFieldValue("booking", "unitno", Unitno, "totalAmount", False, " and projectcode='" & prj & "' and appid>0")
        bcost = getFieldValue("booking", "unitno", Unitno, "unitCost", False, " and projectcode='" & prj & "' and appid>0")
        unqCode = getFieldValue("booking", "unitno", Unitno, "uniqueCode", False, " and projectcode='" & prj & "' and appid>0")
        apid = getFieldValue("booking", "unitno", Unitno, "appid", False, " and projectcode='" & prj & "' and appid>0")
        'qry = "update paymentSchedule set stageA=round((" & bcost & " * stageP)/100,2) where uniqueCode='" & unqCode & "' and stagep<>0"
        'executeQuery()
        'updated by shweta as on 29.01.2011 against update brokerP
        Dim commt As Integer = CInt(getFieldValue("BrokerP", "Uniquecode", unqCode, "Count(*)", False, " and CommType='P'"))
        If commt > 0 Then
            qry = "update brokerP set unitcost=" & tcost & ",CommAmt=(CommP/100) * " & tcost & " where uniqueCode='" & unqCode & "' and CommType='P'"
        Else
            qry = "update brokerP set unitcost=" & tcost & " where uniqueCode='" & unqCode & "'"
        End If
        executeQuery()
        'end here
        updatePaymentSchedule(apid, unqCode)

    End Sub

    Public Sub updatePaymentSchedule(ByVal apid As Integer, ByVal uniqueCode As String)
        Dim dt1, dt2 As New DataTable()
        Dim dr, dr1 As DataTableReader
        qry = "update paymentschedule set status='Pending',stageR=0,rAmount=0 where uniqueCode='" & uniqueCode & "'"
        executeQuery()
        'getRAmount(uniquecode," and right(tp,1)='1'")
        Dim rAmount As Double, pamt As Double, iamt As Double, namt As String
        rAmount = 0 : pamt = 0 : iamt = 0
        namt = getFieldValue("paymentdetails", "uniqueCode", uniqueCode, "sum(chequeamount)", , " and right(tp,1)='1'")
        If namt <> "" Then pamt = CDbl(namt)
        namt = getFieldValue("installments", "uniqueCode", uniqueCode, "sum(chequeamount)", , " and right(tp,1)='1'")
        If namt <> "" Then iamt = CDbl(namt)
        rAmount = pamt + iamt
        ''Updating Broker Commission
        'updateBroker(apid, uniqueCode, rAmount)

        '' inserting rAmount for first stage as Reg + Booking Amount
        qry = "update paymentschedule set rAmount=" & pamt & " where uniquecode='" & uniqueCode & "' and pid=(select min(pid) from paymentschedule where uniqueCode='" & uniqueCode & "')"
        executeQuery()
        '' inserting rAmount
        qry = "select sum(chequeAmount),chfor from installments where uniqueCode='" & uniqueCode & "'  and right(tp,1)='1' group by chfor"
        dt1 = fillReader1(qry)
        dr = dt1.CreateDataReader()

        While dr.Read
            qry = "update paymentschedule set rAmount=rAmount + " & dr(0) & " where uniqueCode='" & uniqueCode & "' and stage='" & dr(1) & "'"
            executeQuery()
        End While
        dr.Close()
        dt1.Clear()
        dt1.Dispose()

        qry = "select * from paymentschedule where uniqueCode='" & uniqueCode & "' and status='Pending' order by pid"
        dt1 = fillReader1(qry)
        dr = dt1.CreateDataReader()
        Dim stus As String
        Dim stageR As Double
        While dr.Read
            stus = "Pending"
            stageR = 0
            'rAmount = rAmount + dr("stageR")
            If rAmount > 0 Then
                If dr("stageA") > rAmount Then
                    stageR = rAmount
                    rAmount = 0
                Else
                    stageR = dr("stageA")
                    rAmount = rAmount - dr("stageA")
                    stus = "Received"
                End If
            End If
            qry = "update paymentschedule set stageR=" & cNum(stageR) & ", status='" & stus & "' where uniqueCode='" & dr("uniqueCode") & "' and pid=" & dr("pid")
            executeQuery()
        End While
        dr.Close()
        dt1.Clear()
        dt1.Dispose()

    End Sub

    Public Sub updatePS(ByVal apid As Integer, ByVal uniqueCode As String)
        Dim dt1, dt2 As New DataTable()
        Dim dr, dr1 As DataTableReader
        qry = "update ps set status='Pending',stageR=0,rAmount=0 where uniqueCode='" & uniqueCode & "'"
        executeQuery()

        Dim rAmount As Double, pamt As Double, iamt As Double, namt As String
        rAmount = 0 : pamt = 0 : iamt = 0
        namt = getFieldValue("paymentdetails", "uniqueCode", uniqueCode, "sum(chequeamount)")
        If namt <> "" Then pamt = CDbl(namt)
        namt = getFieldValue("installments", "uniqueCode", uniqueCode, "sum(chequeamount)")
        If namt <> "" Then iamt = CDbl(namt)
        rAmount = pamt + iamt
        ''Updating Broker Commission
        'updateBroker(apid, uniqueCode, rAmount)

        '' inserting rAmount for first stage as Reg + Booking Amount
        qry = "update ps set rAmount=" & pamt & " where uniquecode='" & uniqueCode & "' and pid=(select min(pid) from ps where uniqueCode='" & uniqueCode & "')"
        executeQuery()
        '' inserting rAmount
        qry = "select sum(chequeAmount),chfor from installments where uniqueCode='" & uniqueCode & "' group by chfor"
        dt1 = fillReader1(qry)
        dr = dt1.CreateDataReader()
        While dr.Read
            qry = "update ps set rAmount=rAmount + " & dr(0) & " where uniqueCode='" & uniqueCode & "' and stage='" & dr(1) & "'"
            executeQuery()
        End While
        dr.Close()
        dt1.Clear()
        dt1.Dispose()

        qry = "select * from ps where uniqueCode='" & uniqueCode & "' and status='Pending' order by pid"
        dt1 = fillReader1(qry)
        dr = dt1.CreateDataReader()
        Dim stus As String
        Dim stageR As Double
        While dr.Read
            stus = "Pending"
            stageR = 0
            If rAmount > 0 Then
                If dr("stageA") > rAmount Then
                    stageR = rAmount
                    rAmount = 0
                Else
                    stageR = dr("stageA")
                    rAmount = rAmount - dr("stageA")
                    stus = "Received"
                End If
            End If
            qry = "update ps set stageR=" & cNum(stageR) & ", status='" & stus & "' where uniqueCode='" & dr("uniqueCode") & "' and pid=" & dr("pid")
            executeQuery()
        End While
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
    End Sub

    'Public Sub updateIt(ByVal uq As String)
    '    Dim dr As New Object
    '    qry = "delete from bk where uniqueCode='" & uq & "'"
    '    executeQuery()
    '    qry = "insert into bk select * from booking where uniqueCode='" & uq & "'"
    '    executeQuery()
    '    qry = "select * from bk where uniqueCode='" & uq & "'"
    '    dr = fillReader(qry)
    '    Dim uc As String = ""
    '    Dim bmt As Double = 0
    '    Dim prj As String
    '    While dr.Read
    '        prj = getFieldValue("projects", "projectCode", dr("projectcode"), "ProjectType")
    '        If prj = "Commercial" Then
    '            uc = getFieldValue("CommercialUnits", "unitno", dr("unitno"), "((brate+r2) * SBA) + ((((brate+r2) * SBA) * (plcp))/100) + (totalCharge + Security + Rfield1 + Rfield2 + Rfield3)", False, " and projectcode='" & dr("projectcode") & "'")
    '        ElseIf prj = "Residential Plots" Then
    '            uc = getFieldValue("plots", "plotno", dr("unitno"), "((brate+r2) * area) + ((((brate+r2) * area) * (plcp))/100) + (totalCharge + Security + Rfield1 + Rfield2 + Rfield3)", False, " and projectcode='" & dr("projectcode") & "'")
    '        ElseIf prj = "Residential Flats" Then
    '            uc = getFieldValue("Flats", "flatno", dr("unitno"), "((brate+r2) * SBA) + ((((brate+r2) * SBA) * (plcp))/100) + (totalCharge + Security + Rfield1 + Rfield2 + Rfield3)", False, " and projectcode='" & dr("projectcode") & "'")
    '        End If
    '        If uc = "" Then uc = "0"
    '        If getFieldValue("booking", "uniquecode", uq, "dtype", False) = "Rs" Then
    '            uc = uc - getFieldValue("booking", "uniquecode", uq, "dc", False)
    '        Else
    '            uc = uc - (uc * getFieldValue("booking", "uniquecode", uq, "dc", False) / 100)
    '        End If

    '        uc = uc + (getFieldValue("booking", "uniquecode", uq, "bc", False) * getFieldValue("booking", "uniquecode", uq, "servicetax", False) / 100)

    '        bmt = getFieldValue("paymentdetails", "uniqueCode", uq, "sum(chequeAmount)")
    '        qry = "update bk set unitcost=" & uc & ",bamount=" & bmt & ",totalAmount=ParkingAmt + RoomAmt + " & uc & " where bookingid=" & dr("bookingid")
    '        executeQuery()
    '    End While
    '    dr.Close()
    '    'con.Close()

    '    Dim tCost As Double = 0
    '    tCost = getFieldValue("bk", "uniqueCode", uq, "totalAmount")
    '    qry = "delete from ps where uniqueCode='" & uq & "'"
    '    executeQuery()
    '    qry = "insert into PS select * from paymentSchedule where uniqueCode='" & uq & "'"
    '    executeQuery()
    '    qry = "update ps set status='Pending',stageR=0,rAmount=0 where uniqueCode='" & uq & "'"
    '    executeQuery()
    '    Dim rAmount As Double, pamt As Double, iamt As Double, namt As String
    '    rAmount = 0 : pamt = 0 : iamt = 0
    '    namt = getFieldValue("paymentdetails", "uniqueCode", uq, "sum(chequeamount)")
    '    If namt <> "" Then pamt = CDbl(namt)
    '    namt = getFieldValue("installments", "uniqueCode", uq, "sum(chequeamount)")
    '    If namt <> "" Then iamt = CDbl(namt)
    '    rAmount = pamt + iamt
    '    qry = "select * from PS where uniqueCode='" & uq & "' order by pid"
    '    dr = fillReader(qry)
    '    Dim stgA, stgR As Double
    '    stgA = 0 : stgR = 0
    '    Dim sts As String
    '    While dr.Read
    '        sts = "Pending"
    '        stgA = (tCost * dr("stageP")) / 100
    '        If stgA <= rAmount Then
    '            stgR = stgA
    '            sts = "Received"
    '        Else
    '            stgR = rAmount
    '        End If
    '        qry = "update PS set stageA=" & stgA & ",stageR=" & stgR & ",status='" & sts & "' where pid=" & dr("pid")
    '        executeQuery()
    '        rAmount = rAmount - stgR
    '    End While
    '    dr.Close()
    '    qry = "update PS set rAmount=" & pamt & " where uniquecode='" & uq & "' and pid=(select min(pid) from ps where uniqueCode='" & uq & "')"
    '    executeQuery()
    'End Sub

    Public Sub updateIt(ByVal uq As String)

        Dim dt1, dt2 As New DataTable()
        Dim dr, dr1 As DataTableReader
        qry = "delete from bk where uniqueCode='" & uq & "'"
        executeQuery()
        qry = "insert into bk select * from booking where uniqueCode='" & uq & "'"
        executeQuery()
        qry = "select * from bk where uniqueCode='" & uq & "'"

        dt1 = fillReader1(qry)
        dr = dt1.CreateDataReader()

        Dim uc As String = ""
        Dim villa As String = ""
        Dim plotnum As String = ""
        Dim plcP As Double = 0
        Dim projectcode As String = ""
        Dim txttower As String = ""
        Dim planno As String = ""
        Dim bmt As Double = 0
        Dim uc1 As Double = 0
        Dim uc2 As Double = 0
        Dim prj As String = ""
        While dr.Read
            prj = getFieldValue("projects", "projectCode", dr("projectcode"), "ProjectType")
            If prj = "Commercial" Then
                uc = getFieldValue("CommercialUnits", "unitno", dr("unitno"), "((brate+r2) * SBA) + ((((brate+r2) * SBA) * (plcp))/100) + (totalCharge + Security)", False, " and projectcode='" & dr("projectcode") & "'")
            ElseIf prj = "Residential Plots" Then
                uc = getFieldValue("plots", "plotno", dr("unitno"), "((brate+r2) * area) + ((((brate+r2) * area) * (plcp))/100) + (totalCharge + Security + Rfield1 + Rfield2 + Rfield3)", False, " and projectcode='" & dr("projectcode") & "'")
                If uc = "" Then
                    plotnum = getFieldValue("villalist", "plotno", dr("unitno"), "plotid", True, " and projectcode='" & dr("projectcode") & "'")
                    plcP = getFieldValue("villalist", "plotno", dr("unitno"), "plcP", True, " and projectcode='" & dr("projectcode") & "'")
                    villa = "true"
                    uc1 = getFieldValue("villalist", "plotno", dr("unitno"), "(brate+r2) * area", False, " and projectcode='" & dr("projectcode") & "'")
                    uc2 = getFieldValue("villas", "plotid", plotnum, "(builduprate+r2c) * builduparea", False, " and projectcode='" & dr("projectcode") & "'")
                    uc = CDbl(uc1) + CDbl(uc2) + ((CDbl(uc1) + CDbl(uc2)) * CDbl(plcP) / 100)
                    uc = uc + getFieldValue("villas", "plotid", plotnum, "EC", False, " and projectcode='" & dr("projectcode") & "'")
                End If
            ElseIf prj = "Residential Flats" Then
                uc = getFieldValue("Flats", "flatno", dr("unitno"), "((brate+r2) * SBA) + ((((brate+r2) * SBA) * (plcp))/100) + (totalCharge + Security + Rfield1 + Rfield2 + Rfield3)", False, " and projectcode='" & dr("projectcode") & "'")
            End If
            If uc = "" Then uc = "0"
            If getFieldValue("booking", "uniquecode", uq, "dtype", False) = "Rs" Then
                uc = uc - getFieldValue("booking", "uniquecode", uq, "dc", False)
            Else
                uc = uc - (uc * getFieldValue("booking", "uniquecode", uq, "dc", False) / 100)
            End If
            If villa = "true" Then
                uc = uc + ((getFieldValue("villalist", "plotno", dr("unitno"), "(brate) * area", False, " and projectcode='" & dr("projectcode") & "'")) + (getFieldValue("villas", "plotid", plotnum, "(builduprate) * builduparea", False, " and projectcode='" & dr("projectcode") & "'"))) * (getFieldValue("booking", "uniquecode", uq, "servicetax", False) / 100)
            Else
                uc = uc + (getFieldValue("booking", "uniquecode", uq, "bc", False) * getFieldValue("booking", "uniquecode", uq, "servicetax", False) / 100)
            End If

            bmt = getFieldValue("paymentdetails", "uniqueCode", uq, "sum(chequeAmount)")
            qry = "update bk set unitcost=" & uc & ",bamount=" & bmt & ",totalAmount=ParkingAmt + RoomAmt + " & uc & " where bookingid=" & dr("bookingid")
            executeQuery()
        End While
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
        'con.Close()
        If villa = "true" Then
            Dim sumcharge As Double = 0
            Dim ChrgTyp() As String = {"Parking", "clubHouse", "mShip", "htLine", "maintenance", "security"}
            Dim item
            Dim c1 As Integer = 0
            Dim c2 As Integer = 0
            Dim c3 As Integer = 0
            Dim c4 As Integer = 0
            Dim c5 As Integer = 0
            Dim c6 As Integer = 0
            Dim c7 As Integer = 0


            Dim txtparking As Double = getFieldValue("villalist", "plotid", plotnum, "parking", True)
            Dim txtclubhouse As Double = getFieldValue("villalist", "plotid", plotnum, "clubhouse", True)
            Dim txtsecurity As Double = getFieldValue("villalist", "plotid", plotnum, "security", True)
            Dim txtmaintenance As Double = getFieldValue("villalist", "plotid", plotnum, "maintenance", True)
            Dim txtmembership As Double = getFieldValue("villalist", "plotid", plotnum, "mship", True)
            Dim txthtline As Double = getFieldValue("villalist", "plotid", plotnum, "htline", True)
            txttower = getFieldValue("villalist", "plotid", plotnum, "tower", True)
            Dim txtec As Double = getFieldValue("villas", "plotid", plotnum, "EC", True)
            Dim a As String = "select projectcode,planno from booking where uniquecode='" & uq & "'"
            Dim dr2 As Object = fillReader(a)
            While dr2.read
                projectcode = dr2("projectcode")
                planno = dr2("planno")
            End While
            dr2.close()
            qry = "select stage from add_vcharge where projectcode='" & projectcode & "' and planno='" & planno & "'"
            dt1 = fillReader1(qry)
            dr = dt1.CreateDataReader()
            While dr.Read()
                For Each item In ChrgTyp
                    If (dr("stage").ToString() <> item.ToString()) Then
                        If dr("stage") = "parking" Then
                            c1 = c1 + 1
                            If (c1 = 1) Then sumcharge = sumcharge + CDbl(txtparking)
                        ElseIf dr("stage") = "clubHouse" Then
                            c2 = c2 + 1
                            If (c2 = 1) Then sumcharge = sumcharge + CDbl(txtclubhouse)
                        ElseIf dr("stage") = "security" Then
                            c3 = c3 + 1
                            If (c3 = 1) Then sumcharge = sumcharge + CDbl(txtsecurity)
                        ElseIf dr("stage") = "maintenance" Then
                            c4 = c4 + 1
                            If (c4 = 1) Then sumcharge = sumcharge + CDbl(txtmaintenance)
                        ElseIf dr("stage") = "mShip" Then
                            c5 = c5 + 1
                            If (c5 = 1) Then sumcharge = sumcharge + CDbl(txtmembership)
                        ElseIf dr("stage") = "htLine" Then
                            c6 = c6 + 1
                            If (c6 = 1) Then sumcharge = sumcharge + CDbl(txthtline)
                        ElseIf dr("stage") = "EC" Then
                            c7 = c7 + 1
                            If (c7 = 1) Then sumcharge = sumcharge + CDbl(txtec)
                        End If
                    End If
                Next
            End While
            dr.Close()
            dt1.Clear()
            dt1.Dispose()
        End If

        Dim tCost As Double = 0
        tCost = getFieldValue("bk", "uniqueCode", uq, "totalAmount")
        qry = "delete from ps where uniqueCode='" & uq & "'"
        executeQuery()
        qry = "insert into PS select * from paymentSchedule where uniqueCode='" & uq & "'"
        executeQuery()
        qry = "update ps set status='Pending',stageR=0,rAmount=0 where uniqueCode='" & uq & "'"
        executeQuery()
        Dim rAmount As Double, pamt As Double, iamt As Double, namt As String
        rAmount = 0 : pamt = 0 : iamt = 0
        namt = getFieldValue("paymentdetails", "uniqueCode", uq, "sum(chequeamount)")
        If namt <> "" Then pamt = CDbl(namt)
        namt = getFieldValue("installments", "uniqueCode", uq, "sum(chequeamount)")
        If namt <> "" Then iamt = CDbl(namt)
        rAmount = pamt + iamt

        qry = "select * from PS where uniqueCode='" & uq & "' order by pid"
        dt1 = fillReader1(qry)
        dr = dt1.CreateDataReader()

        Dim stgA, stgR As Double
        stgA = 0 : stgR = 0
        Dim sts As String
        Dim minpspid As String = getFieldValue("ps", "uniqueCode", uq, "min(pid)", False)
        Dim minVCpid As String = getFieldValue("VillaPaymentPlan", "projectcode", projectcode, "min(pid)", False, " and planno='" & planno & "'")

        While dr.Read
            Dim SChrgTot As Double = 0
            Dim SChrg As Double = 0
            Dim ACC As Double = 0
            sts = "Pending"
            If villa = "true" Then
                Dim txtadd As Double = getFieldValue("villas", "plotid", plotnum, "addarea", True)
                Dim crt0 As Double = getFieldValue("villas", "plotid", plotnum, "builduprate", True)
                Dim crt1 As Double = getFieldValue("villas", "plotid", plotnum, "r2c", True)

                If dr("pid").ToString() = getFieldValue("ps", "uniquecode", uq, "max(pid)", False) Then
                    ACC = empNum((CDbl(txtadd)) * (CDbl(crt0) + CDbl(crt1)))
                End If
                Dim FPid As String = CInt(dr("pid")) - CInt(minpspid) + CInt(minVCpid)

                If isExists("Add_VCharge", "Pid", FPid, True) Then
                    qry = "select * from Add_VCharge where pid=" & FPid & " and projectcode='" & projectcode & "' and planno='" & planno & "'"
                    dt2 = fillReader1(qry)
                    dr1 = dt2.CreateDataReader()

                    While dr1.Read()
                        If dr1("stage") <> "addarea" And dr1("stage") <> "EC" Then
                            Dim a = getFieldValue("Villalist", "projectCode", projectcode, dr1("stage"), False, " and tower='" & txttower & "' and plotid='" & plotnum & "'")
                            SChrg = CDbl(a * CDbl(dr1("stageP")) / 100)
                            SChrgTot = SChrgTot + SChrg
                        ElseIf dr1("stage") = "EC" Then
                            SChrg = CDbl(getFieldValue("Villas", "projectCode", projectcode, dr1("stage"), False, " and plotid='" & plotnum & "'") * CDbl(dr1("stageP")) / 100)
                            SChrgTot = SChrgTot + SChrg
                        End If
                    End While
                    dr1.Close()
                    dt2.Clear()
                    dt2.Dispose()
                End If
            End If
            stgA = (tCost * dr("stageP")) / 100 + SChrgTot + ACC
            If stgA <= rAmount Then
                stgR = stgA
                sts = "Received"
            Else
                stgR = rAmount
            End If
            qry = "update PS set stageA=" & stgA & ",stageR=" & stgR & ",status='" & sts & "' where pid=" & dr("pid")
            executeQuery()
            rAmount = rAmount - stgR
        End While
        dr.Close()
        dt1.Clear()
        dt1.Dispose()
        qry = "update PS set rAmount=" & pamt & " where uniquecode='" & uq & "' and pid=(select min(pid) from ps where uniqueCode='" & uq & "')"
        executeQuery()
    End Sub
    Private Sub updateBroker(ByVal apid As Integer, ByVal uniqueCode As String, ByVal ramt As Double)
        'added by shweta on 16.10.2008 against broker montly commison
        Dim brkId As Integer
        brkId = getFieldValue("booking", "uniqueCode", uniqueCode, "Brokerid")
        If brkId = -1 Then Exit Sub
        qry = "update brokerp set amountr=" & ramt & " where brokerid=" & brkId & " and uniqueCode='" & uniqueCode & "'"
        executeQuery()
        updateCommission(brkId)
    End Sub


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

    Public Sub fillInterest(ByVal unqCode As String, ByVal ssid As String, ByVal IR As Double, ByRef iAmount As Double)
        Dim dtII, dtI As New DataTable()
        'replace 0.18 by iAmount by shweta as on 03.02.2011
        Dim drd, drd1 As DataTableReader
        Dim tblName As String = "tempII"
        Dim u() As String = {"", "", ""}
        u = unqCode.Split("/")
        'Try
        Try
            Dim cdt As String = Format(Now.Date, "dd-MMM-yyyy")
            iAmount = 0
            qry = "delete from tempPS where sessionid='" & ssid & "'"
            executeQuery()
            'qry = "insert into tempI select * from (select sum(chequeAmount/(1+stax/100)) as cr ,dt as ddt from installments where uniqueCode='" & unqCode & "' and right(tp,1) ='1' and status='Received' and rno>1 group by dt" & _
            '                           " union All" & _
            '                           " select sum(stageR) as cr,dt as ddt from otherD where uniqueCode='" & unqCode & "' and stageR <> 0 and sFor in ('Charges','Discount') group by dt) AS B"
            'executeQuery()
            If u(0) = "TFC" Then
                qry = "insert into tempI select * from (select sum(chequeAmount/(1+stax/100)) as cr ,dt as ddt from installments where uniqueCode='" & unqCode & "' and right(tp,1) ='1' and status='Received' and rno>1 group by dt" & _
                                            " union All" & _
                                            " select sum(stageR) as cr,dt as ddt from otherD where uniqueCode='" & unqCode & "' and stageR <> 0 and sFor in ('Charges','Discount') group by dt) AS B"
            Else
                qry = "insert into tempI select * from (select sum(chequeAmount/(1+stax/100)) as cr ,dt as ddt from installments where uniqueCode='" & unqCode & "' and right(tp,1) ='1' and rno>1 group by dt" & _
                                            " union All" & _
                                            " select sum(stageR) as cr,dt as ddt from otherD where uniqueCode='" & unqCode & "' and stageR <> 0 and sFor in ('Charges','Discount') group by dt) AS B"
            End If

            'On Error GoTo solv
            'un commented commented for Trimurty on 03.04.2013
            qry = "insert into TempII select * from (select projectcode,stageA as dr,stage,stagedate as ddt from paymentschedule,booking where booking.uniqueCode='" & unqCode & "' and booking.uniquecode=paymentschedule.uniquecode and stagedate<>'Inactive' and pid <> (select max(pid) from paymentschedule where uniquecode='" & unqCode & "')) as b"
            qry = "insert into TempII select * from (select projectcode,stageA as dr,stage,stagedate as ddt from paymentschedule,booking where booking.uniqueCode='" & unqCode & "' and booking.uniquecode=paymentschedule.uniquecode and stagedate<>'Inactive' and pid not in (select top 2 pid from paymentschedule where uniquecode='" & unqCode & "' order by pid desc)) as b"
            'un commented commented for Trimurty on 03.04.2013
            qry = "insert into TempII select * from (select projectcode,stageA as dr,stage,stagedate as ddt from paymentschedule,booking where booking.uniqueCode='" & unqCode & "' and booking.uniquecode=paymentschedule.uniquecode and stagedate<>'Inactive') as b"
            executeQuery()
            'On Error GoTo 0


            qry = "select projectcode,sum(dr) as dr1,stage,ddt from tempII group by projectcode,stage,ddt order by cdate(ddt)"
            dtII = fillReader1(qry)
            drd = dtII.CreateDataReader()

            qry = "select * from tempI where cr <> 0 order by cdate(ddt)"
            dtI = fillReader1(qry)
            drd1 = dtI.CreateDataReader()

            Dim bdate, rdate As String
            bdate = getFieldValue("Booking", "UniqueCode", unqCode, "bdate", False)
            Dim bamt, bramt As String
            'bamt = getFieldValue("paymentdetails", "UniqueCode", unqCode, "sum(chequeamount)", False, " and status='Received' and rno>1") 'getFieldValue("Booking", "UniqueCode", unqCode, "bamount", False)
            If u(0) = "TFC" Then
                bamt = getFieldValue("paymentdetails", "UniqueCode", unqCode, "sum(chequeamount)", False, " and status='Received' and rno>1") 'getFieldValue("Booking", "UniqueCode", unqCode, "bamount", False)
            Else
                bamt = getFieldValue("paymentdetails", "UniqueCode", unqCode, "sum(chequeamount)", False, " and rno>1") 'getFieldValue("Booking", "UniqueCode", unqCode, "bamount", False)
            End If
            qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & bdate & "','0','" & bamt & "','0','" & bamt & "','0.00','" & ssid & "')"
            executeQuery()
            'rdate = getFieldValue("paymentdetails", "uniqueCode", unqCode, "dt", False, "group by dt")
            rdate = bdate
            'bramt = getFieldValue("paymentdetails", "uniqueCode", unqCode, "sum(chequeAmount)", False, "group by uniqueCode")
            bramt = bamt
            qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & rdate & "','" & DateDiff(DateInterval.Day, CDate(bdate), CDate(rdate)) & "','0','" & bramt & "','" & bamt - bramt & "','0.00','" & ssid & "')"
            executeQuery()

            Dim dueD As String = ""
            Dim stageA As Double = 0
            Dim dueA As Double = 0
            Dim extA As Double = 0
            Dim s As String = ""
            Dim ramt As Double = 0
            Dim ddif As Double = 0
            Dim iAmt As Double = 0
            Dim bl As String = ""
            Dim cr, cr1 As Double
            Dim bal As Double = 0
            Dim cnt As Integer = 500 ' drd1.RecordsAffected + drd.RecordsAffected 'drd1.GetSchemaTable.Rows.Count + drd.GetSchemaTable.Rows.Count
            Dim i As Integer
            If drd.hasRows And drd1.hasRows Then
                bl = "both"
                drd1.read()
                drd.read()
            ElseIf drd.hasRows Then
                bl = "drd"
                drd.read()
            ElseIf drd1.hasRows Then
                bl = "drd1"
                drd1.read()
            End If
            If bl = "both" Then

                For i = 0 To cnt

                    If CDate(drd("ddt")) < CDate(drd1("ddt")) Then
                        If CDate(drd("ddt")) > CDate(cdt) Then Exit For
                        ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd("ddt")))
                        bdate = drd("ddt")
                        iAmt = 0
                        'Changes made on 3.04.2013 for Trimurty for non calculating negative value interest
                        If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        'If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        iAmount = iAmount + iAmt
                        'bal = bal + (drd("dr1") - bamt)
                        bal = bal + (drd("dr1") - bamt) + iAmt
                        qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd("ddt") & "','" & ddif & "','" & drd("dr1") - bamt & "','0','" & bal & "','" & cNum(iAmt) & "','" & ssid & "')"
                        executeQuery()
                        bamt = 0
                        If Not drd.read Then
                            bl = "drd1"
                            Exit For
                        End If

                    ElseIf CDate(drd("ddt")) > CDate(drd1("ddt")) Then
                        If CDate(drd1("ddt")) > CDate(cdt) Then Exit For
                        ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd1("ddt")))
                        bdate = drd1("ddt")
                        iAmt = 0
                        'Changes made on 3.04.2013 for Trimurty for non calculating negative value interest
                        If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        'If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        iAmount = iAmount + iAmt
                        'bal = bal - drd1("cr")
                        bal = bal - drd1("cr") + iAmt
                        qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd1("ddt") & "','" & ddif & "','0','" & drd1("cr") & "','" & bal & "','" & cNum(iAmt) & "','" & ssid & "')"
                        executeQuery()
                        'bamt = 0
                        If Not drd1.read Then
                            bl = "drd"
                            Exit For
                        End If
                    Else

                        ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd("ddt")))
                        bdate = drd("ddt")
                        iAmt = 0
                        'Changes made on 3.04.2013 for Trimurty for non calculating negative value interest
                        If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        'If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        iAmount = iAmount + iAmt
                        'bal = bal + (drd("dr1") - bamt)
                        bal = bal + (drd("dr1") - bamt) + iAmt
                        qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd("ddt") & "','" & ddif & "','" & drd("dr1") - bamt & "','0','" & bal & "','" & cNum(iAmt) & "','" & ssid & "')"
                        executeQuery()
                        bamt = 0
                        If Not drd.read Then
                            bl = "drd1"
                            Exit For
                        End If


                        ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd1("ddt")))
                        bdate = drd1("ddt")
                        iAmt = 0
                        'Changes made on 3.04.2013 for Trimurty for non calculating negative value interest
                        If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        'If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        iAmount = iAmount + iAmt
                        'bal = bal - drd1("cr")
                        bal = bal - drd1("cr") + iAmt
                        qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd1("ddt") & "','" & ddif & "','0','" & drd1("cr") & "','" & bal & "','" & cNum(iAmt) & "','" & ssid & "')"
                        executeQuery()
                        bamt = 0
                        If Not drd1.read Then
                            bl = "drd"
                            Exit For
                        End If


                    End If
                Next
            End If

            If bl = "drd" Then
                'drd.read()
                For i = 0 To cnt
                    If CDate(drd("ddt")) > CDate(cdt) Then Exit For
                    ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd("ddt")))
                    bdate = drd("ddt")
                    iAmt = 0
                    'Changes made on 3.04.2013 for Trimurty for non calculating negative value interest
                    If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                    'If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                    iAmount = iAmount + iAmt
                    'bal = bal + (drd("dr1") - bamt)
                    bal = bal + (drd("dr1") - bamt) + iAmt
                    qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd("ddt") & "','" & ddif & "','" & drd("dr1") - bamt & "','0','" & bal & "','" & cNum(iAmt) & "','" & ssid & "')"
                    executeQuery()
                    bamt = 0
                    If Not drd.read Then Exit For

                Next
            End If


            If bl = "drd1" Then
                'drd1.read()
                For i = 0 To cnt
                    If CDate(drd1("ddt")) > CDate(cdt) Then Exit For
                    ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd1("ddt")))
                    bdate = drd1("ddt")
                    iAmt = 0
                    'Changes made on 3.04.2013 for Trimurty for non calculating negative value interest
                    If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                    'If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                    iAmount = iAmount + iAmt
                    'bal = bal - drd1("cr")
                    bal = bal - drd1("cr") + iAmt
                    qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd1("ddt") & "','" & ddif & "','0','" & drd1("cr") & "','" & bal & "','" & cNum(iAmt) & "','" & ssid & "')"
                    executeQuery()
                    'bamt = 0
                    If Not drd1.read Then Exit For

                Next
            End If


            drd.Close()
            dtII.Clear()
            dtII.Dispose()

            drd1.Close()
            dtI.Clear()
            dtI.Dispose()
            If bal <> 0 And (CDate(bdate) <> Now.Date) Then
                ddif = DateDiff(DateInterval.Day, CDate(bdate), Now.Date)
                bdate = Format(Now.Date, "dd-MMM-yyyy")
                iAmt = 0
                'Changes made on 3.04.2013 for Trimurty for non calculating negative value interest
                If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                'If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                iAmount = iAmount + iAmt
                qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & bdate & "','" & ddif & "','0','0','" & bal & "','" & cNum(iAmt) & "','" & ssid & "')"
                executeQuery()
            End If
            Exit Sub
        Catch ex As Exception

        Finally
            qry = "delete from tempI"
            executeQuery()
            'qry = "drop table tempII"
            qry = "delete from tempII"
            executeQuery()
        End Try
    End Sub

    Public Sub fillInterest1(ByVal unqCode As String, ByVal ssid As String, ByVal IR As Double, ByRef iAmount As Double)
        'replace 0.18 by iAmount by shweta as on 03.02.2011
        Dim drd, drd1 As DataTableReader
        Dim dt1, dt2 As New DataTable()
        Dim tblName As String = "tempII"
        'Try
        Try
            Dim cdt As String = Format(Now.Date, "dd-MMM-yyyy")
            iAmount = 0
            qry = "delete from tempPS where sessionid='" & ssid & "'"
            executeQuery()
            qry = "insert into tempI select * from (select sum(chequeAmount) as cr ,dt as ddt from installments where uniqueCode='" & unqCode & "' group by dt" & _
                                        " union All" & _
                                        " select sum(stageR + stageR2) as cr,dt as ddt from otherD where uniqueCode='" & unqCode & "' and stageR <> 0 and sFor in ('Charges','Discount') group by dt) AS B"
            executeQuery()

            'On Error GoTo solv

            '        qry = "insert into TempII select * from (select projectcode,stageA as dr,stage,stagedate as ddt from paymentschedule,booking where booking.uniqueCode='" & unqCode & "' and booking.uniquecode=paymentschedule.uniquecode and stagedate<>'Inactive' and pid <> (select max(pid) from paymentschedule where uniquecode='" & unqCode & "')) as b"
            'qry = "insert into TempII select * from (select projectcode,stageA as dr,stage,stagedate as ddt from ps,bk where bk.uniqueCode='" & unqCode & "' and bk.uniquecode=ps.uniquecode and stagedate<>'Inactive' and pid not in (select top 2 pid from ps where uniquecode='" & unqCode & "' order by pid desc)) as b"
            'commented for SNG Group on 25 Aug 2011
            qry = "insert into TempII select * from (select projectcode,stageA as dr,stage,stagedate as ddt from ps,bk where bk.uniqueCode='" & unqCode & "' and bk.uniquecode=ps.uniquecode and stagedate<>'Inactive') as b"
            executeQuery()
            'On Error GoTo 0


            qry = "select projectcode,sum(dr) as dr1,stage,ddt from tempII group by projectcode,stage,ddt order by cdate(ddt)"

            dt1 = fillReader1(qry)
            drd = dt1.CreateDataReader()


            qry = "select * from tempI where cr <> 0 order by cdate(ddt)"
            dt2 = fillReader1(qry)
            drd1 = dt2.CreateDataReader()



            Dim bdate, rdate As String
            bdate = getFieldValue("bk", "UniqueCode", unqCode, "bdate", False)
            Dim bamt, bramt As String
            bamt = getFieldValue("Bk", "UniqueCode", unqCode, "bamount", False)
            qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & bdate & "','0','" & bamt & "','0','" & bamt & "','0.00','" & ssid & "')"
            executeQuery()
            'rdate = getFieldValue("paymentdetails", "uniqueCode", unqCode, "dt", False, "group by dt")
            rdate = bdate
            'bramt = getFieldValue("paymentdetails", "uniqueCode", unqCode, "sum(chequeAmount)", False, "group by uniqueCode")
            bramt = bamt
            qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & rdate & "','" & DateDiff(DateInterval.Day, CDate(bdate), CDate(rdate)) & "','0','" & bramt & "','" & bamt - bramt & "','0.00','" & ssid & "')"
            executeQuery()

            Dim dueD As String = ""
            Dim stageA As Double = 0
            Dim dueA As Double = 0
            Dim extA As Double = 0
            Dim s As String = ""
            Dim ramt As Double = 0
            Dim ddif As Double = 0
            Dim iAmt As Double = 0
            Dim bl As String = ""
            Dim cr, cr1 As Double
            Dim bal As Double = 0
            Dim cnt As Integer = 500 ' drd1.RecordsAffected + drd.RecordsAffected 'drd1.GetSchemaTable.Rows.Count + drd.GetSchemaTable.Rows.Count
            Dim i As Integer
            If drd.HasRows And drd1.HasRows Then
                bl = "both"
                drd1.Read()
                drd.Read()
            ElseIf drd.HasRows Then
                bl = "drd"
                drd.Read()
            ElseIf drd1.HasRows Then
                bl = "drd1"
                drd1.Read()
            End If
            If bl = "both" Then

                For i = 0 To cnt

                    If CDate(drd("ddt")) < CDate(drd1("ddt")) Then
                        If CDate(drd("ddt")) > CDate(cdt) Then Exit For
                        ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd("ddt")))
                        bdate = drd("ddt")
                        iAmt = 0
                        'Changes made on 29.08.2011 for SNG Group for calculating negative value interest
                        'If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        iAmount = iAmount + iAmt
                        bal = bal + (drd("dr1") - bamt)
                        qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd("ddt") & "','" & ddif & "','" & drd("dr1") - bamt & "','0','" & bal & "','" & cNum(iAmt) & "','" & ssid & "')"
                        executeQuery()
                        bamt = 0
                        If Not drd.read Then
                            bl = "drd1"
                            Exit For
                        End If

                    ElseIf CDate(drd("ddt")) > CDate(drd1("ddt")) Then
                        If CDate(drd1("ddt")) > CDate(cdt) Then Exit For
                        ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd1("ddt")))
                        bdate = drd1("ddt")
                        iAmt = 0
                        'Changes made on 29.08.2011 for SNG Group for calculating negative value interest
                        'If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        iAmount = iAmount + iAmt
                        bal = bal - drd1("cr")
                        qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd1("ddt") & "','" & ddif & "','0','" & drd1("cr") & "','" & bal & "','" & cNum(iAmt) & "','" & ssid & "')"
                        executeQuery()
                        'bamt = 0
                        If Not drd1.read Then
                            bl = "drd"
                            Exit For
                        End If
                    Else

                        ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd("ddt")))
                        bdate = drd("ddt")
                        iAmt = 0
                        'Changes made on 29.08.2011 for SNG Group for calculating negative value interest
                        'If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        iAmount = iAmount + iAmt
                        bal = bal + (drd("dr1") - bamt)
                        qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd("ddt") & "','" & ddif & "','" & drd("dr1") - bamt & "','0','" & bal & "','" & cNum(iAmt) & "','" & ssid & "')"
                        executeQuery()
                        bamt = 0
                        If Not drd.read Then
                            bl = "drd1"
                            Exit For
                        End If


                        ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd1("ddt")))
                        bdate = drd1("ddt")
                        iAmt = 0
                        'Changes made on 29.08.2011 for SNG Group for calculating negative value interest
                        'If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        iAmount = iAmount + iAmt
                        bal = bal - drd1("cr")
                        qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd1("ddt") & "','" & ddif & "','0','" & drd1("cr") & "','" & bal & "','" & cNum(iAmt) & "','" & ssid & "')"
                        executeQuery()
                        bamt = 0
                        If Not drd1.read Then
                            bl = "drd"
                            Exit For
                        End If


                    End If
                Next
            End If

            If bl = "drd" Then
                'drd.read()
                For i = 0 To cnt
                    If CDate(drd("ddt")) > CDate(cdt) Then Exit For
                    ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd("ddt")))
                    bdate = drd("ddt")
                    iAmt = 0
                    'Changes made on 29.08.2011 for SNG Group for calculating negative value interest
                    'If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                    If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                    iAmount = iAmount + iAmt
                    bal = bal + (drd("dr1") - bamt)
                    qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd("ddt") & "','" & ddif & "','" & drd("dr1") - bamt & "','0','" & bal & "','" & cNum(iAmt) & "','" & ssid & "')"
                    executeQuery()
                    bamt = 0
                    If Not drd.read Then Exit For

                Next
            End If


            If bl = "drd1" Then
                'drd1.read()
                For i = 0 To cnt
                    If CDate(drd1("ddt")) > CDate(cdt) Then Exit For
                    ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd1("ddt")))
                    bdate = drd1("ddt")
                    iAmt = 0
                    'Changes made on 29.08.2011 for SNG Group for calculating negative value interest
                    'If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                    If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                    iAmount = iAmount + iAmt
                    bal = bal - drd1("cr")
                    qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd1("ddt") & "','" & ddif & "','0','" & drd1("cr") & "','" & bal & "','" & cNum(iAmt) & "','" & ssid & "')"
                    executeQuery()
                    'bamt = 0
                    If Not drd1.read Then Exit For

                Next
            End If


            drd.Close()
            dt1.Clear()
            dt1.Dispose()
            drd1.Close()
            dt2.Clear()
            dt2.Dispose()
            If bal <> 0 And (CDate(bdate) <> Now.Date) Then
                ddif = DateDiff(DateInterval.Day, CDate(bdate), Now.Date)
                bdate = Format(Now.Date, "dd-MMM-yyyy")
                iAmt = 0
                'Changes made on 29.08.2011 for SNG Group for calculating negative value interest
                'If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                iAmount = iAmount + iAmt
                qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & bdate & "','" & ddif & "','0','0','" & bal & "','" & cNum(iAmt) & "','" & ssid & "')"
                executeQuery()
            End If
            Exit Sub
        Catch ex As Exception

        Finally
            qry = "delete from tempI"
            executeQuery()
            'qry = "drop table tempII"
            qry = "delete from tempII"
            executeQuery()
        End Try
    End Sub
    Public Sub replaceUniqueCode(ByVal findWhat As String, ByVal replaceWith As String)
        Dim unq As Object
        unq = Split(replaceWith, "/")
        qry = "update paymentdetails set appid=" & unq(2) & ",  uniqueCode='" & replaceWith & "' where uniqueCode='" & findWhat & "'"
        executeQuery()
        qry = "update installments set appid=" & unq(2) & ", uniqueCode='" & replaceWith & "' where uniqueCode='" & findWhat & "'"
        executeQuery()
        qry = "update installmentpdc set appid=" & unq(2) & ", uniqueCode='" & replaceWith & "' where uniqueCode='" & findWhat & "'"
        executeQuery()
        qry = "update booking set appid=" & unq(2) & ", uniqueCode='" & replaceWith & "',unitno='" & unq(1) & "' where uniqueCode='" & findWhat & "'"
        executeQuery()
        qry = "update PaymentSchedule set uniqueCode='" & replaceWith & "' where uniqueCode='" & findWhat & "'"
        executeQuery()
        qry = "update BrokerP set uniqueCode='" & replaceWith & "' where uniqueCode='" & findWhat & "'"
        executeQuery()
        qry = "update ConfirmEnq set uniqueCode='" & replaceWith & "' where uniqueCode='" & findWhat & "'"
        executeQuery()
        qry = "update OtherD set uniqueCode='" & replaceWith & "' where uniqueCode='" & findWhat & "'"
        executeQuery()
        qry = "update Parkings set status='" & replaceWith & "' where status='" & findWhat & "'"
        executeQuery()
        qry = "update serventRooms set status='" & replaceWith & "' where status='" & findWhat & "'"
        executeQuery()
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
            If usr = ob(i) Then str = "OK"
        Next
        checkUser = str
    End Function

    Public Sub setTabIndex(ByVal frm As Object)
        On Error GoTo chk
        Dim i As Integer = 0
        Dim cnt As Object
        For Each cnt In frm.Controls
            If cnt.Controls.Count > 0 Then setTabIndex(cnt)
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ff", "<script>alert('" & cnt.TabIndex & "')</script>")
            cnt.tabIndex = i
            'Response.Write("Control Name : " & cnt.id & "----Tab Index : " & cnt.tabIndex & "<br>")
            i = i + 1
        Next

        Exit Sub
chk:
        'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ff", "<script>alert('" & Err.Description & "')</script>")

        Resume Next
    End Sub



    Public Sub fillCombo1(ByRef drp As DropDownList, ByVal tblName As String, ByVal dispField As String, ByVal valueField As String, Optional ByVal sqlWhere As String = "", Optional ByVal isSelect As Boolean = True, Optional ByVal isSelectOnly As Boolean = False)
        Dim con As New SqlConnection
        Dim dr As SqlDataReader
        Try
            drp.Items.Clear()
            drp.DataSource = Nothing
            drp.DataBind()
            If Not isSelectOnly Then
                dispField = dispField & " as expr1"
                con = New SqlConnection(ConfigurationManager.AppSettings("strConn"))
                Dim str As String = "select " & dispField & "," & valueField & " from " & tblName & sqlWhere
                Dim cmd As New SqlCommand(str, con)
                cmd.Connection.Open()
                dr = cmd.ExecuteReader()
                drp.DataValueField = valueField
                drp.DataTextField = "expr1"
                drp.DataSource = dr
                drp.DataBind()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            con.Dispose()
        End Try
        If isSelect Then drp.Items.Insert(0, New ListItem("[Select]", "0"))
    End Sub

    Public Function getFieldValue1(ByVal tblname As String, ByVal fldMatch As String, ByVal fldVal As String, ByVal fldFind As String, Optional ByVal isnumericM As Boolean = False, Optional ByVal isnumericF As Boolean = False, Optional ByVal sqlWhereAttach As String = "") As Object
        Dim dr As SqlDataReader
        Try
            Dim sql As String = "select " & fldFind & " from " & tblname & " where " & IIf(isnumericM, fldMatch & "=" & fldVal & "", fldMatch & "='" & fldVal & "'") & " " & sqlWhereAttach
            Dim c As New SqlConnection(ConfigurationManager.AppSettings("strConn"))
            Dim cmd As New SqlCommand(sql, c)
            Dim s As String = ""
            cmd.Connection = c
            cmd.Connection.Open()
            dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
            If dr.Read() Then If Not IsDBNull(dr(0)) Then s = dr(0)
            If s = "" Then If isnumericF = True Then s = "0"
            getFieldValue1 = s
        Catch ex As Exception
            Throw ex
        Finally
            dr.Close()
            c.Close()
        End Try
    End Function

    Public Sub fillAging(ByVal unqCode As String, ByVal ssid As String, ByVal IR As Double, ByRef iAmount As Double)
        'replace 0.18 by iAmount by shweta as on 03.02.2011
        Dim dt As New DataTable()
        Dim dt1 As New DataTable()
        Dim drd, drd1 As DataTableReader
        Dim tblName As String = "tempII"
        'Try
        Try
            Dim cdt As String = Format(Now.Date, "dd-MMM-yyyy")
            iAmount = 0
            'qry = "delete from tempPS where sessionid='" & ssid & "'"
            'executeQuery()
            qry = "insert into tempI select * from (select sum(chequeAmount) as cr ,dt as ddt from installments where uniqueCode='" & unqCode & "' and right(tp,1) ='1' group by dt" & _
                                        " union All" & _
                                        " select sum(stageR) as cr,dt as ddt from otherD where uniqueCode='" & unqCode & "' and stageR <> 0 and sFor in ('Charges','Discount') group by dt) AS B"
            executeQuery()

            'On Error GoTo solv

            '        qry = "insert into TempII select * from (select projectcode,stageA as dr,stage,stagedate as ddt from paymentschedule,booking where booking.uniqueCode='" & unqCode & "' and booking.uniquecode=paymentschedule.uniquecode and stagedate<>'Inactive' and pid <> (select max(pid) from paymentschedule where uniquecode='" & unqCode & "')) as b"
            'qry = "insert into TempII select * from (select projectcode,stageA as dr,stage,stagedate as ddt from paymentschedule,booking where booking.uniqueCode='" & unqCode & "' and booking.uniquecode=paymentschedule.uniquecode and stagedate<>'Inactive' and pid not in (select top 2 pid from paymentschedule where uniquecode='" & unqCode & "' order by pid desc)) as b"
            'commented for SNG Group on 25 Aug 2011
            qry = "insert into TempII select * from (select projectcode,stageA as dr,stage,stagedate as ddt from paymentschedule,booking where booking.uniqueCode='" & unqCode & "' and booking.uniquecode=paymentschedule.uniquecode and stagedate<>'Inactive') as b"
            executeQuery()
            'On Error GoTo 0


            qry = "select projectcode,sum(dr) as dr1,stage,ddt from tempII group by projectcode,stage,ddt order by cdate(ddt)"
            dt = fillReader1(qry)
            drd = dt.CreateDataReader()

            qry = "select * from tempI where cr <> 0 order by cdate(ddt)"
            dt1 = fillReader1(qry)
            drd1 = dt1.CreateDataReader()

            Dim bdate, rdate As String
            bdate = getFieldValue("Booking", "UniqueCode", unqCode, "bdate", False)
            Dim bamt, bramt As String
            bamt = getFieldValue("Booking", "UniqueCode", unqCode, "bamount", False)
            qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,f15,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & bdate & "','0','" & bamt & "','0','" & bamt & "','0.00','" & unqCode & "','" & ssid & "')"
            executeQuery()
            'rdate = getFieldValue("paymentdetails", "uniqueCode", unqCode, "dt", False, "group by dt")
            rdate = bdate
            'bramt = getFieldValue("paymentdetails", "uniqueCode", unqCode, "sum(chequeAmount)", False, "group by uniqueCode")
            bramt = bamt
            qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,f15,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & rdate & "','" & DateDiff(DateInterval.Day, CDate(bdate), CDate(rdate)) & "','0','" & bramt & "','" & bamt - bramt & "','0.00','" & unqCode & "','" & ssid & "')"
            executeQuery()

            Dim dueD As String = ""
            Dim stageA As Double = 0
            Dim dueA As Double = 0
            Dim extA As Double = 0
            Dim s As String = ""
            Dim ramt As Double = 0
            Dim ddif As Double = 0
            Dim iAmt As Double = 0
            Dim bl As String = ""
            Dim cr, cr1 As Double
            Dim bal As Double = 0
            Dim cnt As Integer = 500 ' drd1.RecordsAffected + drd.RecordsAffected 'drd1.GetSchemaTable.Rows.Count + drd.GetSchemaTable.Rows.Count
            Dim i As Integer
            If drd.hasRows And drd1.hasRows Then
                bl = "both"
                drd1.read()
                drd.read()
            ElseIf drd.hasRows Then
                bl = "drd"
                drd.read()
            ElseIf drd1.hasRows Then
                bl = "drd1"
                drd1.read()
            End If
            If bl = "both" Then

                For i = 0 To cnt

                    If CDate(drd("ddt")) < CDate(drd1("ddt")) Then
                        If CDate(drd("ddt")) > CDate(cdt) Then Exit For
                        ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd("ddt")))
                        bdate = drd("ddt")
                        iAmt = 0
                        'Changes made on 29.08.2011 for SNG Group for calculating negative value interest
                        'If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        iAmount = iAmount + iAmt
                        bal = bal + (drd("dr1") - bamt)
                        qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,f15,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd("ddt") & "','" & ddif & "','" & drd("dr1") - bamt & "','0','" & bal & "','" & cNum(iAmt) & "','" & unqCode & "','" & ssid & "')"
                        executeQuery()
                        bamt = 0
                        If Not drd.read Then
                            bl = "drd1"
                            Exit For
                        End If

                    ElseIf CDate(drd("ddt")) > CDate(drd1("ddt")) Then
                        If CDate(drd1("ddt")) > CDate(cdt) Then Exit For
                        ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd1("ddt")))
                        bdate = drd1("ddt")
                        iAmt = 0
                        'Changes made on 29.08.2011 for SNG Group for calculating negative value interest
                        'If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        iAmount = iAmount + iAmt
                        bal = bal - drd1("cr")
                        qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,f15,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd1("ddt") & "','" & ddif & "','0','" & drd1("cr") & "','" & bal & "','" & cNum(iAmt) & "','" & unqCode & "','" & ssid & "')"
                        executeQuery()
                        'bamt = 0
                        If Not drd1.read Then
                            bl = "drd"
                            Exit For
                        End If
                    Else

                        ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd("ddt")))
                        bdate = drd("ddt")
                        iAmt = 0
                        'Changes made on 29.08.2011 for SNG Group for calculating negative value interest
                        'If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        iAmount = iAmount + iAmt
                        bal = bal + (drd("dr1") - bamt)
                        qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,f15,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd("ddt") & "','" & ddif & "','" & drd("dr1") - bamt & "','0','" & bal & "','" & cNum(iAmt) & "','" & unqCode & "','" & ssid & "')"
                        executeQuery()
                        bamt = 0
                        If Not drd.read Then
                            bl = "drd1"
                            Exit For
                        End If


                        ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd1("ddt")))
                        bdate = drd1("ddt")
                        iAmt = 0
                        'Changes made on 29.08.2011 for SNG Group for calculating negative value interest
                        'If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                        iAmount = iAmount + iAmt
                        bal = bal - drd1("cr")
                        qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,f15,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd1("ddt") & "','" & ddif & "','0','" & drd1("cr") & "','" & bal & "','" & cNum(iAmt) & "','" & unqCode & "','" & ssid & "')"
                        executeQuery()
                        bamt = 0
                        If Not drd1.read Then
                            bl = "drd"
                            Exit For
                        End If


                    End If
                Next
            End If

            If bl = "drd" Then
                'drd.read()
                For i = 0 To cnt
                    If CDate(drd("ddt")) > CDate(cdt) Then Exit For
                    ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd("ddt")))
                    bdate = drd("ddt")
                    iAmt = 0
                    'Changes made on 29.08.2011 for SNG Group for calculating negative value interest
                    'If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                    If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                    iAmount = iAmount + iAmt
                    bal = bal + (drd("dr1") - bamt)
                    qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,f15,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd("ddt") & "','" & ddif & "','" & drd("dr1") - bamt & "','0','" & bal & "','" & cNum(iAmt) & "','" & unqCode & "','" & ssid & "')"
                    executeQuery()
                    bamt = 0
                    If Not drd.read Then Exit For

                Next
            End If


            If bl = "drd1" Then
                'drd1.read()
                For i = 0 To cnt
                    If CDate(drd1("ddt")) > CDate(cdt) Then Exit For
                    ddif = DateDiff(DateInterval.Day, CDate(bdate), CDate(drd1("ddt")))
                    bdate = drd1("ddt")
                    iAmt = 0
                    'Changes made on 29.08.2011 for SNG Group for calculating negative value interest
                    'If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                    If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                    iAmount = iAmount + iAmt
                    bal = bal - drd1("cr")
                    qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,f15,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & drd1("ddt") & "','" & ddif & "','0','" & drd1("cr") & "','" & bal & "','" & cNum(iAmt) & "','" & unqCode & "','" & ssid & "')"
                    executeQuery()
                    'bamt = 0
                    If Not drd1.read Then Exit For

                Next
            End If


            drd.Close()
            dt.Clear()
            dt.Dispose()

            drd1.Close()
            dt1.Clear()
            dt1.Dispose()

            If bal <> 0 And (CDate(bdate) <> Now.Date) Then
                ddif = DateDiff(DateInterval.Day, CDate(bdate), Now.Date)
                bdate = Format(Now.Date, "dd-MMM-yyyy")
                iAmt = 0
                'Changes made on 29.08.2011 for SNG Group for calculating negative value interest
                'If ddif > 0 And bal > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                If ddif > 0 Then iAmt = iAmt + ((bal * IR) / 365) * ddif
                iAmount = iAmount + iAmt
                qry = "insert into tempPS(id,f1,f2,f3,f4,f5,f6,f15,sessionid) values(" & getMaxId("tempPS", "id") & ",'" & bdate & "','" & ddif & "','0','0','" & bal & "','" & cNum(iAmt) & "','" & unqCode & "','" & ssid & "')"
                executeQuery()
            End If
            Exit Sub
        Catch ex As Exception

        Finally
            qry = "delete from tempI"
            executeQuery()
            'qry = "drop table tempII"
            qry = "delete from tempII"
            executeQuery()
        End Try
    End Sub

    Public Function getDateTime() As Date
        getDateTime = DateAdd(DateInterval.Minute, 630, Now)
    End Function
End Class