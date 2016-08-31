Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web


Public Class appsettingManager
#Region "Constructor"
    Public Sub New()
    End Sub
#End Region
#Region "Global Variables"
    Private StrQuery As [String]
    Private ds As New DataTable()
    Private objcon As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConn"))
    Private PageSize As Integer = Convert.ToInt32(500)
#End Region

#Region "PRIVATE members & PUBLIC properties"
    Public Property appsettingid() As Integer
        Get
            Return m_appsettingid
        End Get
        Set(value As Integer)
            m_appsettingid = value
        End Set
    End Property
    Private m_appsettingid As Integer
    Public Property keyvalue() As String
        Get
            Return m_keyvalue
        End Get
        Set(value As String)
            m_keyvalue = value
        End Set
    End Property
    Private m_keyvalue As String
    Public Property key() As String
        Get
            Return m_key
        End Get
        Set(value As String)
            m_key = value
        End Set
    End Property
    Private m_key As String
#End Region
#Region "Search appsetting"
    Public Function SearchItem(SortExpression As String) As DataTable
        StrQuery = "SELECT ISNULL([appsettingid],0) AS appsettingid"
        StrQuery += " ,ISNULL([keyvalue],'') AS keyvalue"
        StrQuery += " ,ISNULL([key],'') AS [key]"
        StrQuery += " FROM [appsetting] "
        StrQuery += " WHERE 1 = 1 "
        If key <> "" AndAlso key IsNot Nothing Then
            StrQuery += " AND key LIKE '%' + @key + '%'"
        End If
        If SortExpression <> "" Then
            StrQuery += " ORDER BY " & SortExpression
        Else
            StrQuery += " ORDER BY [key] "
        End If

        Try
            ds = New DataTable()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            If key <> "" AndAlso key IsNot Nothing Then
                sqlcmd.Parameters.Add(New SqlParameter("@key", SqlDbType.NVarChar, 500)).Value = key
            End If

            sqladp = New SqlDataAdapter(sqlcmd)
            sqladp.Fill(ds)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            ds.Dispose()
            objcon.Close()
        End Try
    End Function
#End Region
#Region "Select Active Items"
    Public Function SelectActive() As DataTable
        StrQuery = "SELECT ISNULL([appsettingid],0) AS appsettingid"
        StrQuery += " ,ISNULL([keyvalue],'') AS keyvalue"
        StrQuery += " ,ISNULL([key],'') AS [key]"
        StrQuery += " FROM [appsetting]  "
        StrQuery += "   ORDER BY [key] "

        Try
            ds = New DataTable()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()

            sqladp = New SqlDataAdapter(sqlcmd)
            sqladp.Fill(ds)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            ds.Dispose()
            objcon.Close()
        End Try
    End Function


#End Region
#Region "Select Single Item"
    Public Function SelectSingleItem() As DataTable
        StrQuery = "SELECT ISNULL([appsettingid],0) AS appsettingid"
        StrQuery += " ,ISNULL([keyvalue],'') AS keyvalue"
        StrQuery += " ,ISNULL([key],0) AS [key]"
        StrQuery += " FROM [appsetting] "
        StrQuery += " WHERE appsettingid = @appsettingid   ORDER BY [key]"
        Try
            ds = New DataTable()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            sqlcmd.Parameters.Add(New SqlParameter("@appsettingid", SqlDbType.Int)).Value = appsettingid
            sqladp = New SqlDataAdapter(sqlcmd)
            sqladp.Fill(ds)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            ds.Dispose()
            objcon.Close()
        End Try
    End Function
#End Region
#Region "Delete appsetting"
    Public Sub DeleteItem()
        StrQuery = "DELETE FROM appsetting WHERE appsettingid=@appsettingid"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.Add(New SqlParameter("@appsettingid", SqlDbType.Int)).Value = appsettingid
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Sub
#End Region
#Region "insert appsetting"
    Public Sub InsertItem()
        StrQuery = " INSERT INTO [appsetting]([keyvalue],[key],createdate)"
        StrQuery += " VALUES (@keyvalue,@key,getdate())"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.Add(New SqlParameter("@keyvalue", SqlDbType.NVarChar, 500)).Value = keyvalue
            sqlcmd.Parameters.Add(New SqlParameter("@key", SqlDbType.NVarChar, 500)).Value = key
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Sub
#End Region
#Region "Update appsetting"
    Public Sub UpdateItem()
        StrQuery = " UPDATE appsetting SET"
        StrQuery += " keyvalue=@keyvalue"
        StrQuery += " ,[key]=@key,modifydate=getdate() "
        StrQuery += " WHERE appsettingid = @appsettingid  "
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.Add(New SqlParameter("@keyvalue", SqlDbType.NVarChar, 500)).Value = keyvalue
            sqlcmd.Parameters.Add(New SqlParameter("@appsettingid", SqlDbType.Int)).Value = appsettingid
            sqlcmd.Parameters.Add(New SqlParameter("@key", SqlDbType.NVarChar, 500)).Value = key
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Sub
#End Region
#Region "Check appsettingExists"
    Public Function checkappsettingexists(flag As String) As Boolean
        StrQuery = " SELECT ISNULL(count(*),0) as counter FROM appsetting WHERE [key]=@key "
        If flag = "edit" Then
            StrQuery += " and appsettingid<>@appsettingid "
        End If
        Try
            ds = New DataTable()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.Add(New SqlParameter("@key", SqlDbType.NVarChar, 500)).Value = key
            If flag = "edit" Then
                sqlcmd.Parameters.Add(New SqlParameter("@appsettingid", SqlDbType.Int)).Value = appsettingid
            End If
            If Convert.ToInt32(sqlcmd.ExecuteScalar()) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            ds.Dispose()
            objcon.Close()
        End Try
    End Function
#End Region

End Class
