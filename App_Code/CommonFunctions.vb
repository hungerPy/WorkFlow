Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Net
Imports System.Data
Imports System.Text
Imports System.Drawing
Imports System.Diagnostics
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Collections.Generic
Imports System.Drawing.Imaging
'using CDO;
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.Web


Public Class appsetting
    Public Shared ReadOnly pagesize As Integer = 20
End Class
''' <summary>
''' Summary description for CommonFunctions
''' </summary>
Public Class CommonFunctions
    Private objCon As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConn"))

    '
    ' TODO: Add constructor logic here
    '
    Public Sub New()
    End Sub
    Public Shared Function checkloginsession() As Boolean
        If HttpContext.Current.Session("Loginid") Is Nothing OrElse Convert.ToInt32(HttpContext.Current.Session("Loginid")) = 0 OrElse Convert.ToString(HttpContext.Current.Session("Loginid")) = "" OrElse HttpContext.Current.Session("Loginname") Is Nothing OrElse Convert.ToString(HttpContext.Current.Session("Loginname")) = "" Then
            Return False
        End If
        Return True
    End Function

    Public Shared Function IsSessionExists(strSessionID As String, isINTEGER As Boolean) As Boolean
        Dim flag As Boolean = False
        If isINTEGER Then
            If System.Web.HttpContext.Current.Session(strSessionID) IsNot Nothing Then
                If System.Web.HttpContext.Current.Session(strSessionID).ToString() <> "" Then
                    If RegExp.IsNumericValue(System.Web.HttpContext.Current.Session(strSessionID).ToString()) Then
                        flag = True
                    End If
                End If
            End If
        Else
            If System.Web.HttpContext.Current.Session(strSessionID) IsNot Nothing Then
                If System.Web.HttpContext.Current.Session(strSessionID).ToString() <> "" Then
                    flag = True
                End If
            End If
        End If
        Return flag
    End Function

    Public Shared Function IsRequestDotFormValueExist(strName As String, isINTEGER As Boolean) As Boolean
        Dim flag As Boolean = False
        If isINTEGER Then
            If System.Web.HttpContext.Current.Request.Form(strName) IsNot Nothing Then
                If System.Web.HttpContext.Current.Request.Form(strName).ToString() <> "" Then
                    If RegExp.IsNumericValue(System.Web.HttpContext.Current.Request.Form(strName).ToString()) Then
                        flag = True
                    End If
                End If
            End If
        Else
            If System.Web.HttpContext.Current.Request.Form(strName) IsNot Nothing Then
                If System.Web.HttpContext.Current.Request.Form(strName).ToString() <> "" Then
                    flag = True
                End If
            End If
        End If
        Return flag
    End Function

    ' CHECK COOKIE EXISTS OR NOT
    Public Shared Function IsCookieExists(strCookieName As String, isINTEGER As Boolean) As Boolean
        Dim flag As Boolean = False
        If isINTEGER Then
            If System.Web.HttpContext.Current.Request.Cookies(strCookieName) IsNot Nothing Then
                If System.Web.HttpContext.Current.Request.Cookies(strCookieName).ToString() <> "" Then
                    If RegExp.IsNumericValue(System.Web.HttpContext.Current.Request.Cookies(strCookieName).ToString()) Then
                        flag = True
                    End If
                End If
            End If
        Else
            If System.Web.HttpContext.Current.Request.Cookies(strCookieName) IsNot Nothing Then
                If System.Web.HttpContext.Current.Request.Cookies(strCookieName).ToString() <> "" Then
                    flag = True
                End If
            End If
        End If
        Return flag
    End Function

    Public Shared Function IsQueryString(strQueryString As String, isINTEGER As Boolean) As Boolean
        Dim flag As Boolean = False
        If isINTEGER Then
            If System.Web.HttpContext.Current.Request.QueryString(strQueryString) IsNot Nothing Then
                If System.Web.HttpContext.Current.Request.QueryString(strQueryString).ToString() <> "" Then
                    If RegExp.IsNumericValue(System.Web.HttpContext.Current.Request.QueryString(strQueryString).ToString()) Then
                        flag = True
                    End If
                End If
            End If
        Else
            If System.Web.HttpContext.Current.Request.QueryString(strQueryString) IsNot Nothing Then
                If System.Web.HttpContext.Current.Request.QueryString(strQueryString).ToString() <> "" Then
                    flag = True
                End If
            End If
        End If
        Return flag
    End Function
    Public Shared Function GetKeyValue(KeyID As Integer) As String
        Dim strpage As String = ""
        Dim obj As New appsettingManager()
        Dim dt As New DataTable()
        obj.appsettingid = KeyID
        dt = obj.SelectSingleItem()
        If dt.Rows.Count > 0 Then
            strpage = dt.Rows(0)("keyvalue").ToString()
        End If
        Return strpage
    End Function
    Public Shared Function PaginationForFront(totalpage As Integer, pageno As Integer, pagesize As Integer, pagename As String, parameters As String) As String
        Dim strpage As String = ""
        Dim left As String = "", right As String = "", mid As String = ""
        If totalpage > 1 Then
            Dim min As Integer, max As Integer = 0
            If totalpage <= 11 Then
                min = 1
                max = totalpage
            Else
                If pageno > 5 AndAlso pageno < (totalpage - 5) Then
                    min = pageno - 5
                    max = pageno + 5
                ElseIf pageno < (totalpage - 5) Then
                    min = 1
                    max = pageno + 10
                Else
                    min = totalpage - 10
                    max = totalpage

                End If
            End If
            left = "<ul>"
            If pageno = 1 Then
            Else
                left += "<li><a class='first' href='" & pagename.ToString() & "_1" & parameters.ToString() & "'>First</a></li>"
                left += "<li><a class='first' href='" & pagename.ToString() & "_" & (pageno - 1).ToString() & parameters.ToString() & "'>Previous</a></li>"
            End If
            For i As Integer = min To max
                If i = pageno Then
                    mid += "<li><a class='select' >" & i.ToString() & "</a></li>"
                Else
                    mid += "<li><a  href='" & pagename.ToString() & "_" & i.ToString() & parameters.ToString() & "'>" & i.ToString() & "</a></li>"

                End If
            Next
            If pageno = max Then
            Else
                right += "<li><a class='first' href='" & pagename.ToString() & "_" & (pageno + 1).ToString() & parameters.ToString() & "'>Next</a></li>"
                right += "<li><a  class='first'  href='" & pagename.ToString() & "_" & totalpage.ToString() & parameters.ToString() & "'>Last</a></li>"
            End If
            right += "</ul>"
        End If
        strpage = left & mid & right
        Return strpage
    End Function

    Public Shared Function Pagination(totalpage As Integer, pageno As Integer, pagesize As Integer, pagename As String, parameters As String) As String
        Dim strpage As String = ""
        Dim left As String = "", right As String = "", mid As String = ""
        If totalpage > 1 Then
            Dim min As Integer, max As Integer = 0
            If totalpage <= 11 Then
                min = 1
                max = totalpage
            Else
                If pageno > 5 AndAlso pageno < (totalpage - 5) Then
                    min = pageno - 5
                    max = pageno + 5
                ElseIf pageno < (totalpage - 5) Then
                    min = 1
                    max = pageno + 10
                Else
                    min = totalpage - 10
                    max = totalpage

                End If
            End If
            left = "<ul class='paginate'>"
            If pageno = 1 Then
            Else
                left += "<li><a class='first' href='" & pagename.ToString() & "?p=1" & parameters.ToString() & "'>First</a></li>"
                left += "<li><a class='first' href='" & pagename.ToString() & "?p=" & (pageno - 1).ToString() & parameters.ToString() & "'>Previous</a></li>"
            End If
            For i As Integer = min To max
                If i = pageno Then
                    mid += "<li><a class='select' >" & i.ToString() & "</a></li>"
                Else

                    mid += "<li><a  href='" & pagename.ToString() & "?p=" & i.ToString() & parameters.ToString() & "'>" & i.ToString() & "</a></li>"

                End If
            Next
            If pageno = max Then
            Else
                right += "<li><a class='first' href='" & pagename.ToString() & "?p=" & (pageno + 1).ToString() & parameters.ToString() & "'>Next</a></li>"
                right += "<li><a  class='first'  href='" & pagename.ToString() & "?p=" & totalpage.ToString() & parameters.ToString() & "'>Last</a></li>"
            End If
            right += "</ul>"
        End If
        strpage = left & mid & right
        Return strpage
    End Function
    Public Shared Function GetUrl(input As String) As String
        Return Regex.Replace(input, "[^0-9a-zA-Z]+", "-")
    End Function
    'image operations
#Region "-----------------------------Image Operations------------------------------"
    'fixed size image
    Public Shared Function FixedSizeImage(imgPhoto As Image, Width As Integer, Height As Integer) As Image
        Dim bmpOut As Bitmap = Nothing
        Try
            Dim oBMP As New Bitmap(imgPhoto)
            Dim oFormat As System.Drawing.Imaging.ImageFormat = oBMP.RawFormat
            Dim NewWidth As Integer = 0
            Dim NewHeight As Integer = 0

            '*** If the image is smaller than a thumbnail just return it

            If oBMP.Width <= Width AndAlso oBMP.Height <= Height Then
                Return oBMP
            End If

            Dim per As Double = 0
            If oBMP.Width > Width Or oBMP.Height > Height Then
                If oBMP.Width > oBMP.Height Then
                    per = (100 * Width) \ oBMP.Width
                    NewWidth = Convert.ToInt32((oBMP.Width * per) / 100)
                    NewHeight = Convert.ToInt32((oBMP.Height * per) / 100)
                Else
                    per = (100 * Height) \ oBMP.Height
                    NewWidth = Convert.ToInt32((oBMP.Width * per) / 100)
                    NewHeight = Convert.ToInt32((oBMP.Height * per) / 100)
                End If

                If NewHeight > Height Or NewWidth > Width Then
                    If NewWidth > NewHeight Then
                        per = (100 * Width) \ NewWidth
                    Else
                        per = (100 * Height) \ NewHeight
                    End If
                    NewWidth = Convert.ToInt32((NewWidth * per) / 100)

                    NewHeight = Convert.ToInt32((NewHeight * per) / 100)
                End If
            End If

            bmpOut = New Bitmap(NewWidth, NewHeight)

            Dim g As Graphics = Graphics.FromImage(bmpOut)

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic

            g.FillRectangle(Brushes.White, 0, 0, NewWidth, NewHeight)

            g.DrawImage(oBMP, 0, 0, NewWidth, NewHeight)

            oBMP.Dispose()
        Catch
            Return Nothing
        End Try
        Return bmpOut
    End Function

    'getNewBounds (ratio/propration)
    Public Shared Function getNewBounds(origWidth As Integer, origHeight As Integer, targWidth As Integer, targHeight As Integer) As String

        Dim width2HtRatio As [Decimal] = Convert.ToDecimal(origWidth) / Convert.ToDecimal(origHeight)
        Dim Ht2widthRatio As [Decimal] = Convert.ToDecimal(origHeight) / Convert.ToDecimal(origWidth)
        Dim useWidth As Integer = 0
        Dim useHeight As Integer = 0

        If origWidth < targWidth And origHeight < targHeight Then
            useWidth = origWidth
            useHeight = origHeight
        ElseIf origWidth > targWidth Then
            useWidth = targWidth
            useHeight = Convert.ToInt32(useWidth * Ht2widthRatio)
        ElseIf origHeight > targHeight Then
            useHeight = targHeight
            useWidth = Convert.ToInt32(useHeight * width2HtRatio)
        ElseIf origWidth < targWidth Then
            useWidth = targWidth
            useHeight = Convert.ToInt32(useWidth * Ht2widthRatio)
        ElseIf origHeight < targHeight Then
            useHeight = targHeight
            useWidth = Convert.ToInt32(useHeight * width2HtRatio)
        Else
            useWidth = targWidth
            useHeight = targHeight
        End If

        'Return newDimensions
        Dim NewSize As String = useWidth & "," & useHeight
        Return NewSize
    End Function

    'create Thmbimages
    Public Shared Function Thmbimages(MainPath As String, ThmbPath As String, Filename As String, Passwidth As Integer, Passheight As Integer, FixFlag As Integer) As String
        Dim width As Integer = 0
        Dim height As Integer = 0
        Dim inp As New IntPtr()
        Dim orginalimg As System.Drawing.Image = Nothing

        orginalimg = Image.FromFile(MainPath)
        width = orginalimg.Width
        height = orginalimg.Height

        If width < Passwidth And height < Passheight Then
            Passwidth = width
            Passheight = height
        End If

        If FixFlag <> 0 Then
            width = Passwidth
            height = Passheight
        End If
        'else
        '{
        Dim Size As String = Nothing

        Size = getNewBounds(width, height, Passwidth, Passheight)
        Dim array As String() = Size.Split(Convert.ToChar(","))
        width = Convert.ToInt16(array(0))
        height = Convert.ToInt16(array(1))

        Dim NewSize As String = ""

        If width > Passwidth Or height > Passheight Then
            NewSize = getNewBounds(width, height, Passwidth, Passheight)
        End If

        If NewSize.Length > 0 Then
            Dim array1 As String() = NewSize.Split(Convert.ToChar(","))
            width = Convert.ToInt16(array1(0))
            height = Convert.ToInt16(array1(1))
        End If

        If width <= 0 And height > 0 Then
            width = Passwidth
        ElseIf height <= 0 And width > 0 Then
            height = Passheight
        End If

        Dim memoryStream As New System.IO.MemoryStream()
        Dim thumbnail As New Bitmap(width, height)

        Dim graphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(thumbnail)
        graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear
        graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
        graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
        graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
        'graphic.FillRectangle(Brushes.White, 0, 0, width, height);
        graphic.FillRectangle(Brushes.Transparent, 0, 0, width, height)

        graphic.DrawImage(orginalimg, 0, 0, width, height)

        'if (Passwidth > 300) {
        'graphic.DrawString("opera web", new Font("Verdana", 8, FontStyle.Italic), new SolidBrush(Color.Silver), 0, 0);
        'graphic.DrawString("opera web", new Font("Verdana", 8, FontStyle.Italic), new SolidBrush(Color.Silver), 0, height - 15);
        '}

        If File.Exists(ThmbPath & Filename) Then
            File.Delete(ThmbPath & Filename)
        End If

        Dim strext As String = Path.GetExtension(MainPath)
        If strext.IndexOf(".") <> -1 Then
            strext = strext.TrimStart(Convert.ToChar("."))
        End If


        'save thumb image
        'SaveJPGWithCompressionSetting(thumbnail, ThmbPath + Filename, 90L);]
        If strext.ToLower() = "png" Then
            SaveJPGWithCompressionSetting(thumbnail, ThmbPath & Filename, 90L, strext)
        Else
            SaveJPGWithCompressionSetting(thumbnail, ThmbPath & Filename, 90L)
        End If

        memoryStream.Dispose()
        graphic.Dispose()
        orginalimg.Dispose()

        thumbnail.Dispose()
        orginalimg.Dispose()
        Return Filename
        '}

        Return Filename
    End Function
    Public Shared Function SanitizeInput(input As String) As String
        Dim badCharReplace As New System.Text.RegularExpressions.Regex("([<>""'%;()&])")
        Dim goodChars As String = badCharReplace.Replace(input, "")
        Return goodChars
    End Function
    'Save JPGWith Compression Setting
    Public Shared Sub SaveJPGWithCompressionSetting(image As System.Drawing.Image, szFileName As String, lCompression As Long)
        'On Error GoTo chkErr
        'Dim errOcr As Boolean
        'GC.Collect()
        Dim eps As New EncoderParameters(1)
        eps.Param(0) = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, lCompression)
        Dim ici As ImageCodecInfo = GetEncoderInfo("image/jpeg")
        image.Save(szFileName, ici, eps)
        Return
        'chkErr: MsgBox("Error: " & Err.Number & " " & Err.Description & vbCrLf & "Choose a different name for file.")
        'errOcr = True
        ' ERROR: Not supported in C#: ResumeStatement

    End Sub
    Public Shared Sub SaveJPGWithCompressionSetting(image As System.Drawing.Image, szFileName As String, lCompression As Long, fileext As String)
        'On Error GoTo chkErr
        'Dim errOcr As Boolean
        'GC.Collect()
        Dim eps As New EncoderParameters(1)
        eps.Param(0) = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, lCompression)
        Dim ici As ImageCodecInfo = GetEncoderInfo("image/" & fileext & "")
        image.Save(szFileName, ici, eps)
        Return
        'chkErr: MsgBox("Error: " & Err.Number & " " & Err.Description & vbCrLf & "Choose a different name for file.")
        'errOcr = True
        ' ERROR: Not supported in C#: ResumeStatement

    End Sub
    'GetEncoderInfo
    Public Shared Function GetEncoderInfo(mimeType As String) As ImageCodecInfo
        Dim j As Integer = 0
        Dim encoders As ImageCodecInfo() = Nothing
        encoders = ImageCodecInfo.GetImageEncoders()
        For j = 0 To encoders.Length
            If encoders(j).MimeType = mimeType Then
                Return encoders(j)
            End If
        Next
        Return Nothing
    End Function

#End Region
    Public Shared Function GetFileContents(filename As String) As String
        Dim filecontent As String = String.Empty
        Dim objStreamReader As System.IO.StreamReader
        objStreamReader = System.IO.File.OpenText(filename)
        filecontent = objStreamReader.ReadToEnd()
        objStreamReader.Close()
        Return filecontent
    End Function
    'encoding & decoding
#Region "----------------------Encoding/Decoding------------------------------------"
    Public Shared strAdminCode As String = "perfect"
    '
    '**************************************************************************************************
    ' Global Definitions for this site
    '**************************************************************************************************
    Public Shared strProtocol As String = "2.23"
    Public Shared arrBase64EncMap As String() = New String(64) {}
    Public Shared arrBase64DecMap As Integer() = New Integer(127) {}
    Public Shared arrProducts As String(,) = New String(3, 2) {}
    Public Shared strNewLine As String = "<P>" & "/n"
    Const BASE_64_MAP_INIT As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"

    '** Base 64 encoding routine.  Used to ensure the encrypted "crypt" field **
    '** can be safely transmitted over HTTP **

    Private Function base64Encode(sData As String) As String
        Try
            Dim encData_byte As Byte() = New Byte(sData.Length - 1) {}

            encData_byte = System.Text.Encoding.UTF8.GetBytes(sData)

            Dim encodedData As String = Convert.ToBase64String(encData_byte)


            Return encodedData
        Catch ex As Exception
            Throw New Exception("Error in base64Encode" & ex.Message)
        End Try
    End Function

    Private Shared Function Encrypt(strText As String, strEncrypt As String) As String
        Dim byKey As Byte() = New Byte(19) {}
        Dim dv As Byte() = {&H12, &H34, &H56, &H78, &H90, &HAB, _
            &HCD, &HEF}
        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8))
            Dim des As New DESCryptoServiceProvider()
            Dim inputArray As Byte() = System.Text.Encoding.UTF8.GetBytes(strText)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, dv), CryptoStreamMode.Write)
            cs.Write(inputArray, 0, inputArray.Length)
            cs.FlushFinalBlock()
            Return Convert.ToBase64String(ms.ToArray())
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Shared Function Decrypt(strText As String, strEncrypt As String) As String
        Dim bKey As Byte() = New Byte(19) {}
        Dim IV As Byte() = {&H12, &H34, &H56, &H78, &H90, &HAB, _
            &HCD, &HEF}
        Try
            bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8))
            Dim des As New DESCryptoServiceProvider()
            Dim inputByteArray As [Byte]() = InlineAssignHelper(inputByteArray, Convert.FromBase64String(strText))
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Return encoding.GetString(ms.ToArray())
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Shared Function GetLastIdentity(tablename As String) As Integer
        Dim objCon As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConn"))
        Dim StrQuery As String = ""
        StrQuery = " select IDENT_CURRENT(@tablename) "
        Try
            Dim sqlcmd As New SqlCommand()
            objCon.Open()
            sqlcmd = New SqlCommand(StrQuery, objCon)
            sqlcmd.Parameters.AddWithValue("@tablename", tablename)
            Return Convert.ToInt32(sqlcmd.ExecuteScalar())
        Catch ex As Exception
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    ' get last identity pluse one
    Public Shared Function GetLastIdentityPlusOne(tablename As String) As Integer
        Dim objCon As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConn"))
        Dim strquery As String = ""
        strquery = " declare @numbercount int select @numbercount = count(*) from " & tablename & " if @numbercount = 0 begin select IDENT_CURRENT(@tablename) end else begin" & vbTab & "select (IDENT_CURRENT(@tablename) +1)" & vbTab & "end "
        Try
            Dim sqlcmd As New SqlCommand()
            objCon.Open()
            sqlcmd = New SqlCommand(strquery, objCon)
            sqlcmd.Parameters.AddWithValue("@tablename", tablename)
            Return Convert.ToInt16(sqlcmd.ExecuteScalar())
        Catch ex As Exception
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    'get last sort count
    Public Shared Function GetLastSortCount(tablename As String, columnname As String) As Integer
        Dim objcon As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConn"))
        Dim strquery As String = ""
        strquery = " declare @numbercount int " & " select @numbercount = count(*) from " & tablename & " if @numbercount = 0 begin select 1 end " & " if((select len(max(" & columnname & ")) as sortorder from " & tablename & " ) < 10) " & " begin" & vbTab & "select (MAX(" & columnname & " )+ 1) as Current_Rank from " & tablename & " end " & " else begin select 1 end "

        Try
            Dim sqlcmd As New SqlCommand()
            objcon.Open()
            sqlcmd = New SqlCommand(strquery, objcon)
            sqlcmd.Parameters.AddWithValue("@columnname", columnname)
            sqlcmd.Parameters.AddWithValue("@tablename", tablename)
            Return Convert.ToInt16(sqlcmd.ExecuteScalar())
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Function
    Public Shared Function GetLastSortparentidCount(tablename As String, columnname As String, parentid As Integer) As Integer
        Dim objcon As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConn"))
        Dim strquery As String = ""
        strquery = " declare @numbercount int " & " select @numbercount = count(*) from " & tablename & " where parentid=@parentid" & " if @numbercount = 0 begin select 1 end " & " if((select len(max(" & columnname & ")) as sortorder from " & tablename & " where parentid=@parentid ) < 10) " & " begin" & vbTab & "select (MAX(" & columnname & " )+ 1) as Current_Rank from " & tablename & " where parentid=@parentid end " & " else begin select 1 end "

        Try
            Dim sqlcmd As New SqlCommand()
            objcon.Open()
            sqlcmd = New SqlCommand(strquery, objcon)
            sqlcmd.Parameters.AddWithValue("@columnname", columnname)
            sqlcmd.Parameters.AddWithValue("@tablename", tablename)
            sqlcmd.Parameters.AddWithValue("@parentid", parentid)
            Return Convert.ToInt16(sqlcmd.ExecuteScalar())
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Function
    'Check directory exist
    Public Shared Sub Checkdirectoryexist(directorypath As String)
        If Not Directory.Exists(directorypath) Then
            Directory.CreateDirectory(directorypath)
        End If
    End Sub

    'Delete file
    Public Shared Sub DeleteFile(strPath As String)
        If File.Exists(strPath) Then
            File.Delete(strPath)
        End If
    End Sub

#End Region

    'mail functions
    '#Region "-----------------------------Mail Functions--------------------------------"
    Public Shared Function SendMail(from As String, Recipients As String, RecipientsCC As String, mailbody As String, Subject As String, user As String, _
        password As String, smtpServer As String) As Boolean
        Try
            smtpServer = "smtpout.asia.secureserver.net"
            user = "sendmail@ifoxsolutions.com"
            password = "SendMail#007"
            Dim msg As New MailMessage()
            msg.[To].Add(Recipients)
            If RecipientsCC <> "" Then
                msg.CC.Add(RecipientsCC)
            End If
            msg.Subject = Subject
            msg.Body = mailbody
            msg.From = New MailAddress("sendmail@ifoxsolutions.com", from)
            msg.IsBodyHtml = True
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess
            Dim smtp As New System.Net.Mail.SmtpClient(smtpServer, 25)
            smtp.Credentials = New NetworkCredential(user, password)



            smtp.Send(msg)

        Catch
        End Try
        Return True
    End Function


    Public Shared Function SendMail_old(from As String, Recipients As String, RecipientsCC As String, mailbody As String, Subject As String, user As String, _
        password As String, smtpServer As String) As Boolean
        Try

            Dim message As New System.Net.Mail.MailMessage()
            message.[To].Add(Recipients)
            If RecipientsCC <> "" Then
                message.CC.Add(RecipientsCC)
            End If

            message.Subject = Subject
            message.From = New System.Net.Mail.MailAddress("sendmail@ifoxsolutions.com", from)
            message.Body = mailbody
            message.IsBodyHtml = True
            Dim smtp As New System.Net.Mail.SmtpClient("mail.ifoxsolutions.in", 25)
            smtp.Credentials = New NetworkCredential("sendmail@ifoxsolutions.com", "SendMail#007")

            smtp.Send(message)

        Catch
        End Try
        Return True
    End Function
    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        target = value
        Return value
    End Function

End Class

'regular expressions and messages


Public Class RegExp
    Public Shared ReadOnly Url As String = "[a-zA-Z0-9-_\$]+(//.[a-za-z0-9-_//$]+)?\??" & "[a-zA-Z0-9-_\$]+=?[a-zA-Z0-9-_\$]+(&[a-zA-Z0-9-_\$]+=" & "[a-zA-Z0-9-_\$]+)*"

    Public Shared ReadOnly [Date] As String = "(0[1-9]|[12][0-9]|3[01])[-]" & "(0[1-9]|1[012])[-]((175[7-9])|(17[6-9][0-9])|(1[8-9][0-9][0-9])|" & "([2-9][0-9][0-9][0-9]))"
    ' supports dates from 1-1-1757 to 31-12-9999 for SQL Server 2000 Date Range 

    Public Shared ReadOnly Time As String = "(0[1-9]|[1][0-2])[:]" & "(0[0-9]|[1-5][0-9])[:](0[0-9]|[1-5][0-9])[ ][A|a|P|p][M|m]"

    Public Shared ReadOnly Number As String = "[-+]?[0-9]*\.?[0-9]*"
    Public Shared ReadOnly Digit As String = "[0-9]*"
    Public Shared ReadOnly NonNegative As String = "[+]?[0-9]*\.?[0-9]*"
    Public Shared Function MaxLength(len As Integer) As String
        Return "[\s\S]{0," & len.ToString() & "}"
    End Function

    Public Shared ReadOnly SoundFilesUpload As String = "^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))"
    Public Shared ReadOnly ImageFilesUpload As String = "^.+\.((jpg)|(JPG)|(gif)|(GIF)|(jpeg)|(JPEG)|(png)|(PNG)|(bmp)|(BMP))$"
    Public Shared ReadOnly Emailid As String = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"

    Public Shared Function IsNumericValue(strTextEntry As String) As Boolean
        Dim objNotWholePattern As New System.Text.RegularExpressions.Regex("[^0-9]")
        Return Not objNotWholePattern.IsMatch(strTextEntry) AndAlso (strTextEntry <> "")
    End Function
End Class

'#End Region
Public Class ApplicationSetting

    Public Shared CategoryLargePath As String = "data\Category\large\"
    Public Shared CategoryMidiumPath As String = "data\Category\midium\"
    Public Shared CategoryThumbPath As String = "data\Category\thumb\"
    Public Shared CategorySmallPath As String = "data\Category\small\"
    Public Shared CategorySmallURL As String = "data/Category/small/"
    Public Shared CategoryLargeURL As String = "data/Category/large/"
    Public Shared CategoryMidiumURL As String = "data/Category/midium/"
    Public Shared CategoryThumbURL As String = "data/Category/thumb/"
    Public Shared CategoryMidiumWidth As String = "485"
    Public Shared CategoryMidiumHeight As String = "320"
    Public Shared CategorySmallWidth As String = "275"
    Public Shared CategorySmallHeight As String = "275"
    Public Shared CategoryThumbWidth As String = "100"
    Public Shared CategoryThumbHeight As String = "70"



    Public Shared ContentLargePath As String = "data\Content\large\"
    Public Shared ContentMidiumPath As String = "data\Content\midium\"
    Public Shared ContentThumbPath As String = "data\Content\thumb\"
    Public Shared ContentSmallPath As String = "data\Content\small\"
    Public Shared ContentSmallURL As String = "data/Content/small/"
    Public Shared ContentLargeURL As String = "data/Content/large/"
    Public Shared ContentMidiumURL As String = "data/Content/midium/"
    Public Shared ContentThumbURL As String = "data/Content/thumb/"
    Public Shared ContentMidiumWidth As String = "485"
    Public Shared ContentMidiumHeight As String = "320"
    Public Shared ContentSmallWidth As String = "275"
    Public Shared ContentSmallHeight As String = "275"
    Public Shared ContentThumbWidth As String = "100"
    Public Shared ContentThumbHeight As String = "70"

    Public Shared NewsLargePath As String = "data\News\large\"
    Public Shared NewsMidiumPath As String = "data\News\midium\"
    Public Shared NewsThumbPath As String = "data\News\thumb\"
    Public Shared NewsSmallPath As String = "data\News\small\"
    Public Shared NewsSmallURL As String = "data/News/small/"
    Public Shared NewsLargeURL As String = "data/News/large/"
    Public Shared NewsMidiumURL As String = "data/News/midium/"
    Public Shared NewsThumbURL As String = "data/News/thumb/"
    Public Shared NewsMidiumWidth As String = "485"
    Public Shared NewsMidiumHeight As String = "320"
    Public Shared NewsSmallWidth As String = "275"
    Public Shared NewsSmallHeight As String = "275"
    Public Shared NewsThumbWidth As String = "100"
    Public Shared NewsThumbHeight As String = "70"


    Public Shared ProductLargePath As String = "data\Product\large\"
    Public Shared ProductMidiumPath As String = "data\Product\midium\"
    Public Shared ProductThumbPath As String = "data\Product\thumb\"
    Public Shared ProductSmallPath As String = "data\Product\small\"
    Public Shared ProductSmallURL As String = "data/Product/small/"
    Public Shared ProductLargeURL As String = "data/Product/large/"
    Public Shared ProductMidiumURL As String = "data/Product/midium/"
    Public Shared ProductThumbURL As String = "data/Product/thumb/"
    Public Shared ProductMidiumWidth As String = "485"
    Public Shared ProductMidiumHeight As String = "320"
    Public Shared ProductSmallWidth As String = "275"
    Public Shared ProductSmallHeight As String = "275"
    Public Shared ProductThumbWidth As String = "100"
    Public Shared ProductThumbHeight As String = "70"


    Public Shared BannerLargePath As String = "data\Banner\large\"
    Public Shared BannerMidiumPath As String = "data\Banner\midium\"
    Public Shared BannerThumbPath As String = "data\Banner\thumb\"
    Public Shared BannerSmallPath As String = "data\Banner\small\"
    Public Shared BannerSmallURL As String = "data/Banner/small/"
    Public Shared BannerLargeURL As String = "data/Banner/large/"
    Public Shared BannerMidiumURL As String = "data/Banner/midium/"
    Public Shared BannerThumbURL As String = "data/Banner/thumb/"
    Public Shared BannerMidiumWidth As String = "485"
    Public Shared BannerMidiumHeight As String = "320"
    Public Shared BannerSmallWidth As String = "275"
    Public Shared BannerSmallHeight As String = "275"
    Public Shared BannerThumbWidth As String = "100"
    Public Shared BannerThumbHeight As String = "70"

    Public Shared PhotoGalleryLargePath As String = "data\PhotoGallery\large\"
    Public Shared PhotoGalleryMidiumPath As String = "data\PhotoGallery\midium\"
    Public Shared PhotoGalleryThumbPath As String = "data\PhotoGallery\thumb\"
    Public Shared PhotoGallerySmallPath As String = "data\PhotoGallery\small\"
    Public Shared PhotoGallerySmallURL As String = "data/PhotoGallery/small/"
    Public Shared PhotoGalleryLargeURL As String = "data/PhotoGallery/large/"
    Public Shared PhotoGalleryMidiumURL As String = "data/PhotoGallery/midium/"
    Public Shared PhotoGalleryThumbURL As String = "data/PhotoGallery/thumb/"
    Public Shared PhotoGalleryMidiumWidth As String = "485"
    Public Shared PhotoGalleryMidiumHeight As String = "320"
    Public Shared PhotoGallerySmallWidth As String = "275"
    Public Shared PhotoGallerySmallHeight As String = "275"
    Public Shared PhotoGalleryThumbWidth As String = "100"
    Public Shared PhotoGalleryThumbHeight As String = "70"



    Public Shared PhotoGalleryDetailLargePath As String = "data\PhotoGalleryDetail\large\"
    Public Shared PhotoGalleryDetailMidiumPath As String = "data\PhotoGalleryDetail\midium\"
    Public Shared PhotoGalleryDetailThumbPath As String = "data\PhotoGalleryDetail\thumb\"
    Public Shared PhotoGalleryDetailSmallPath As String = "data\PhotoGalleryDetail\small\"
    Public Shared PhotoGalleryDetailSmallURL As String = "data/PhotoGalleryDetail/small/"
    Public Shared PhotoGalleryDetailLargeURL As String = "data/PhotoGalleryDetail/large/"
    Public Shared PhotoGalleryDetailMidiumURL As String = "data/PhotoGalleryDetail/midium/"
    Public Shared PhotoGalleryDetailThumbURL As String = "data/PhotoGalleryDetail/thumb/"
    Public Shared PhotoGalleryDetailMidiumWidth As String = "485"
    Public Shared PhotoGalleryDetailMidiumHeight As String = "320"
    Public Shared PhotoGalleryDetailSmallWidth As String = "275"
    Public Shared PhotoGalleryDetailSmallHeight As String = "275"
    Public Shared PhotoGalleryDetailThumbWidth As String = "100"
    Public Shared PhotoGalleryDetailThumbHeight As String = "70"



End Class
#Region "BABY NAMES CLASS"
Public Class Religion
    Public Shared ReadOnly All As Integer = 0
    Public Shared ReadOnly Hindu As Integer = 1
    Public Shared ReadOnly Muslim As Integer = 2
    Public Shared ReadOnly Sikh As Integer = 3
    Public Shared ReadOnly Christian As Integer = 4
End Class

Public Class Gender
    Public Shared ReadOnly All As Integer = 0
    Public Shared ReadOnly Boy As Integer = 1
    Public Shared ReadOnly Girl As Integer = 2

End Class

Public Class Categories
    Public Shared ReadOnly All As Integer = 0
    Public Shared ReadOnly Celebrity_Kids_Names As Integer = 1
    Public Shared ReadOnly Celebrity_Names As Integer = 2
    Public Shared ReadOnly Modern_Names As Integer = 3
    Public Shared ReadOnly Rare_Names As Integer = 4
    Public Shared ReadOnly God_Goddess As Integer = 5
    Public Shared ReadOnly Historical As Integer = 6
    Public Shared ReadOnly Sanskrit As Integer = 7



End Class

Public Class Rashi
    Public Shared ReadOnly All As Integer = 0
    Public Shared ReadOnly Dhan As Integer = 1
    Public Shared ReadOnly Kanya As Integer = 2
    Public Shared ReadOnly Kark As Integer = 3
    Public Shared ReadOnly Kumbh As Integer = 4
    Public Shared ReadOnly Makar As Integer = 5
    Public Shared ReadOnly Meen As Integer = 6
    Public Shared ReadOnly Mesh As Integer = 7
    Public Shared ReadOnly Mithun As Integer = 8
    Public Shared ReadOnly Sinh As Integer = 9
    Public Shared ReadOnly Tula As Integer = 10
    Public Shared ReadOnly Vrushabh As Integer = 11
    Public Shared ReadOnly Vrushik As Integer = 12
End Class
#End Region

