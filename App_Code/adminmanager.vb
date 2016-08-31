Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' Summary description for adminmanager
''' </summary>
Public Class adminmanager

    Private StrQuery As String
    Private objcon As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConn"))
    Private dt As New DataTable()
    '
    ' TODO: Add constructor logic here
    '
    Public Sub New()
    End Sub

    Private _adminid As Integer
    Private _firstname As String
    Private _lastname As String
    Private _emailaddress As String
    Private _userid As String
    Private _password As String
    Private _adminType As String
    Private _sessionid As Integer

    Private _isActive As [Boolean]



    Public Property adminid() As Integer
        Get
            Return _adminid
        End Get
        Set(value As Integer)
            _adminid = value
        End Set
    End Property

    Public Property userid() As String
        Get
            Return _userid
        End Get
        Set(value As String)
            _userid = value
        End Set
    End Property

    Public Property password() As String
        Get
            Return _password
        End Get
        Set(value As String)
            _password = value
        End Set
    End Property

    Public Property firstname() As String
        Get
            Return _firstname
        End Get
        Set(value As String)
            _firstname = value
        End Set
    End Property

    Public Property lastname() As String
        Get
            Return _lastname
        End Get
        Set(value As String)
            _lastname = value
        End Set
    End Property

    Public Property emailaddress() As String
        Get
            Return _emailaddress
        End Get
        Set(value As String)
            _emailaddress = value
        End Set
    End Property

    Public Property adminType() As String
        Get
            Return _adminType
        End Get
        Set(value As String)
            _adminType = value
        End Set
    End Property

    Public Property sessionid() As Integer
        Get
            Return _sessionid
        End Get
        Set(value As Integer)
            _sessionid = value
        End Set
    End Property

    Public Property isActive() As [Boolean]
        Get
            Return _isActive
        End Get
        Set(value As [Boolean])
            _isActive = value
        End Set
    End Property
    'check user authentication
    Public Function CheckAuthentication() As DataTable
        'StrQuery = "select a.*,b.aid,b.plantid from Users as a,Allot as b where a.Uid=b.uid and userid collate latin1_general_cs_as =@userid and pwd collate latin1_general_cs_as =@password and status='Unlock'"
        ''StrQuery = " select isnull([adminid],0) as adminid," & "isnull([userid],'') as userid," & "isnull([password],'') as password," & "isnull([firstname],'') as firstname," & "isnull([lastname],'') as lastname," & "isnull([emailaddress],'') as emailaddress," & "isnull([adminType],'') as adminType ," & "isnull([isActive],'') as isActive  " & " from administrator where emailaddress =@userid and password = @password and isActive=1 "

        'Try
        '    dt = New DataTable()
        '    objcon.Open()
        '    Dim sqlcmd As New SqlCommand(StrQuery, objcon)
        '    sqlcmd.Parameters.AddWithValue("@userid", emailaddress)
        '    sqlcmd.Parameters.AddWithValue("@password", password)
        '    Dim sqladp As New SqlDataAdapter(sqlcmd)
        '    sqladp.Fill(dt)
        '    Return dt
        'Catch ex As Exception
        '    Throw ex
        'Finally
        '    dt.Dispose()
        '    objcon.Close()
        'End Try
        Try
            dt = New DataTable()
            objcon.Open()
            Dim sqlcmd As New SqlCommand("Userlogin", objcon)
            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.Parameters.AddWithValue("@userid", emailaddress)
            sqlcmd.Parameters.AddWithValue("@password", password)
            sqlcmd.Parameters.AddWithValue("@sessionid", sessionid)

            Dim sqladp As New SqlDataAdapter(sqlcmd)
            sqladp.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex

            dt.Dispose()
            objcon.Close()
        End Try
    End Function

    'check user forgot password
    Public Function ForgotPasswordAdmin() As DataTable
        StrQuery = " Select isnull([adminid],0) as adminid,isnull([userid],'') as userid, " & " isnull([password],'') as password,isnull([firstname],'') as firstname,isnull([lastname],'') as lastname, " & " isnull([emailaddress],'') as emailaddress,isnull([adminType],'') as adminType ,isnull([isActive],'') as isActive from administrator where userid=@userid"
        Try
            dt = New DataTable()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            sqlcmd.Parameters.AddWithValue("@userid", userid)
            sqladp = New SqlDataAdapter(sqlcmd)
            sqladp.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            dt.Dispose()
            objcon.Close()
        End Try
    End Function

    'check user loginid exists
    Public Function LoginIdExist() As [Boolean]
        Try
            Dim id As Integer
            StrQuery = "select count(adminid) from administrator where userid = @userid"

            If adminid <> 0 Then
                StrQuery += " and adminid <> @adminid"
            End If

            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            objcon.Open()
            sqlcmd.Parameters.AddWithValue("@userid", userid)
            If adminid <> 0 Then
                sqlcmd.Parameters.AddWithValue("@adminid", adminid)
            End If

            id = Convert.ToInt32(sqlcmd.ExecuteScalar())

            If id = 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try

    End Function

    'check user email id exists
    Public Function EmailIdExist() As [Boolean]
        Try
            Dim id As Integer
            StrQuery = "select count(adminid) from administrator where emailaddress = @emailaddress"

            If adminid <> 0 Then
                StrQuery += " and adminid <> @adminid"
            End If

            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            objcon.Open()
            sqlcmd.Parameters.AddWithValue("@emailaddress", emailaddress)
            If adminid <> 0 Then
                sqlcmd.Parameters.AddWithValue("@adminid", adminid)
            End If

            id = Convert.ToInt32(sqlcmd.ExecuteScalar())

            If id = 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Function

    'search admin
    Public Function SearchItem() As DataTable
        'Boolean flag = false;
        StrQuery = "select isnull([adminid],0) as adminid,isnull([userid],'') as userid, " & " isnull([password],'') as password,isnull([firstname],'') as firstname, " & " isnull([lastname],'') as lastname,isnull([emailaddress],'') as emailaddress " & " from administrator "

        If emailaddress <> "" Then
            StrQuery += " where emailaddress like '%' + @emailaddress + '%' "
        End If
        Try
            dt = New DataTable()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            If emailaddress <> "" Then
                sqlcmd.Parameters.AddWithValue("@emailaddress", emailaddress)
            End If
            sqladp = New SqlDataAdapter(sqlcmd)
            sqladp.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            dt.Dispose()
            objcon.Close()
        End Try
    End Function

    'select all admin items
    Public Function SelectAllItem() As DataTable
        StrQuery = " select isnull([adminid],0) as adminid,isnull([userid],'') as userid, " & " isnull([password],'') as password,isnull([firstname],'') as firstname, " & " isnull([lastname],'') as lastname,isnull([emailaddress],'') as emailaddress " & " from administrator order by firstname "

        Try
            dt = New DataTable()
            Dim sqladp As New SqlDataAdapter(StrQuery, objcon)
            sqladp.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            dt.Dispose()
        End Try
    End Function

    'select admin details by adminid
    Public Function SelectAdminByUserId() As DataTable
        StrQuery = "select isnull([adminid],0) as adminid,isnull([userid],'') as userid, " & " isnull([password],'') as password,isnull([firstname],'') as firstname, " & " isnull([lastname],'') as lastname,isnull([emailaddress],'') as emailaddress, " & " isnull([isActive],0) as isActive " & " from administrator where adminid = @adminid"

        Try
            dt = New DataTable()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            sqlcmd.Parameters.AddWithValue("@adminid", adminid)
            sqladp = New SqlDataAdapter(sqlcmd)
            sqladp.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            dt.Dispose()
            objcon.Close()
        End Try
    End Function

    'select admin details by userid
    Public Function SelectAdminByUserName() As DataTable
        StrQuery = "select isnull([adminid],0) as adminid,isnull([userid],'') as userid, " & " isnull([password],'') as password,isnull([firstname],'') as firstname, " & " isnull([lastname],'') as lastname,isnull([emailaddress],'') as emailaddress, " & " isnull([isActive],0) as isActive " & " from administrator where userid = @userid"

        Try
            dt = New DataTable()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            sqlcmd.Parameters.AddWithValue("@userid", userid)
            sqladp = New SqlDataAdapter(sqlcmd)
            sqladp.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            dt.Dispose()
            objcon.Close()
        End Try
    End Function

    'delete admin details
    Public Function DeleteItem(id As Integer) As Integer
        StrQuery = "delete from administrator where adminid=@adminid and adminid != @id"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@adminid", adminid)
            sqlcmd.Parameters.AddWithValue("@id", id)
            Return Convert.ToInt32(sqlcmd.ExecuteNonQuery())
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try

    End Function

    'insert admin details
    Public Sub InsertItem()
        StrQuery = "insert into administrator(userid,password,firstname,lastname,emailaddress,adminType,isActive,creationdate,modificationdate) " & " values(@userid,@password,@firstname,@lastname,@emailaddress,'subadmin',@isActive,getdate(),getdate())"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@userid", userid)
            sqlcmd.Parameters.AddWithValue("@password", password)
            sqlcmd.Parameters.AddWithValue("@firstname", firstname)
            sqlcmd.Parameters.AddWithValue("@lastname", lastname)
            sqlcmd.Parameters.AddWithValue("@emailaddress", emailaddress)
            sqlcmd.Parameters.AddWithValue("@isActive", isActive)
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Sub

    'update admin details
    Public Sub UpdateItem()
        StrQuery = "update administrator set userid=@userid,password=@password,firstname=@firstname, " & " lastname=@lastname,emailaddress=@emailaddress,isActive=@isActive,modificationdate=getdate() " & " where adminid =@adminid"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@userid", userid)
            sqlcmd.Parameters.AddWithValue("@password", password)
            sqlcmd.Parameters.AddWithValue("@firstname", firstname)
            sqlcmd.Parameters.AddWithValue("@lastname", lastname)
            sqlcmd.Parameters.AddWithValue("@emailaddress", emailaddress)
            sqlcmd.Parameters.AddWithValue("@isActive", isActive)
            sqlcmd.Parameters.AddWithValue("@adminid", adminid)
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Sub

    'fetch admin name
    Public Function getName() As String
        StrQuery = "select firstname + ' ' + lastname from administrator where adminid=@adminid"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@adminid", adminid)
            Return Convert.ToString(sqlcmd.ExecuteScalar())
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Function

End Class
