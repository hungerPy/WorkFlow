Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Public Class menumanager
#Region "Constructor"
    Public Sub New()
    End Sub
#End Region
#Region "Global Variables"
    Private StrQuery As String
    Private objcon As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConn"))
    Private dt As New DataTable()
    Private ds As New DataSet()
#End Region

#Region "PRIVATE members & PUBLIC properties"
    Public Property idmenu() As Integer
        Get
            Return m_idmenu
        End Get
        Set(value As Integer)
            m_idmenu = Value
        End Set
    End Property
    Private m_idmenu As Integer
    Public Property title() As String
        Get
            Return m_title
        End Get
        Set(value As String)
            m_title = Value
        End Set
    End Property
    Private m_title As String
    Public Property parentid() As Integer
        Get
            Return m_parentid
        End Get
        Set(value As Integer)
            m_parentid = Value
        End Set
    End Property
    Private m_parentid As Integer
    Public Property pageurl() As String
        Get
            Return m_pageurl
        End Get
        Set(value As String)
            m_pageurl = Value
        End Set
    End Property
    Private m_pageurl As String
    Public Property sortorder() As Integer
        Get
            Return m_sortorder
        End Get
        Set(value As Integer)
            m_sortorder = Value
        End Set
    End Property
    Private m_sortorder As Integer
    Public Property isactive() As Boolean
        Get
            Return m_isactive
        End Get
        Set(value As Boolean)
            m_isactive = Value
        End Set
    End Property
    Private m_isactive As Boolean
    Public Property imagepath() As String
        Get
            Return m_imagepath
        End Get
        Set(value As String)
            m_imagepath = Value
        End Set
    End Property
    Private m_imagepath As String
    Public Property adminid() As String
        Get
            Return m_adminid
        End Get
        Set(value As String)
            m_adminid = Value
        End Set
    End Property
    Private m_adminid As String
#End Region
#Region "Insert item"
    Public Sub InsertItem()
        StrQuery = " DECLARE @count INT "
        StrQuery += " SET @count=0 "
        StrQuery += " SELECT @count = COUNT(*) FROM menu WHERE sortorder=@sortorder"
        StrQuery += " IF(@count>=1) "
        StrQuery += " UPDATE menu SET sortorder = sortorder + 1 WHERE sortorder >= @sortorder "
        StrQuery += " INSERT INTO menu(parentid,title,pageurl,sortorder,isactive,imagepath,createdate)"
        StrQuery += " VALUES (@parentid,@title,@pageurl,@sortorder,@isactive,@imagepath,getdate())"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@parentid", parentid)
            sqlcmd.Parameters.AddWithValue("@title", title)
            sqlcmd.Parameters.AddWithValue("@pageurl", pageurl)
            sqlcmd.Parameters.AddWithValue("@sortorder", sortorder)
            sqlcmd.Parameters.AddWithValue("@isactive", isactive)
            sqlcmd.Parameters.AddWithValue("@imagepath", imagepath)
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Sub
#End Region
#Region "Update Item"
    '    public void UpdateItem(int prevSort, int changeSort) {
    Public Sub UpdateItem()
        'StrQuery = "DECLARE @count INT";
        'StrQuery += " SET @count =0";
        'StrQuery += " SELECT @count = COUNT(*) FROM menu WHERE sortorder = @sortorder";
        'StrQuery += " IF(@count >= 1)";
        'StrQuery += "  BEGIN";
        'StrQuery += "   IF(@prevSort > @changeSort)";
        'StrQuery += "   BEGIN";
        'StrQuery += "    UPDATE menu SET sortorder = sortorder + 1 WHERE sortorder < @prevSort AND sortorder >= @changeSort";
        'StrQuery += "   END";
        'StrQuery += "   IF(@prevSort < @changeSort)";
        'StrQuery += "   BEGIN";
        'StrQuery += "    UPDATE menu SET sortorder = sortorder - 1 WHERE  sortorder <= @changeSort AND sortorder> @prevSort";
        'StrQuery += "   END";
        'StrQuery += "  END";
        StrQuery = " UPDATE menu SET [parentid]=@parentid ,[title]=@title ,pageurl=@pageurl ,sortorder=@sortorder ,isactive=@isactive ,imagepath=@imagepath,modifydate=getdate() WHERE idmenu =@idmenu"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            'sqlcmd.Parameters.AddWithValue("@prevSort", prevSort);
            'sqlcmd.Parameters.AddWithValue("@changeSort", changeSort);
            sqlcmd.Parameters.AddWithValue("@parentid", parentid)
            sqlcmd.Parameters.AddWithValue("@title", title)
            sqlcmd.Parameters.AddWithValue("@pageurl", pageurl)
            sqlcmd.Parameters.AddWithValue("@sortorder", sortorder)
            sqlcmd.Parameters.AddWithValue("@isactive", isactive)
            sqlcmd.Parameters.AddWithValue("@imagepath", imagepath)
            sqlcmd.Parameters.AddWithValue("@idmenu", idmenu)

            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Sub
#End Region
#Region "Update Status"
    Public Sub UpdateActive()
        StrQuery = " UPDATE menu SET isactive=@isactive WHERE idmenu=@idmenu"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@isactive", isactive)
            sqlcmd.Parameters.AddWithValue("@idmenu", idmenu)
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Sub
#End Region
#Region "Delete Item"
    Public Sub DeleteItem()
        StrQuery = " DELETE FROM menu WHERE idmenu=@idmenu"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@idmenu", idmenu)
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Sub
#End Region
#Region "Select Active Items"
    Public Function SelectMenuItem() As DataSet
        StrQuery = "SELECT idmenu,title,parentid,pageurl,sortorder,isactive FROM [menu] WHERE isactive=1"
        Try
            ds = New DataSet()
            Dim sqladp As New SqlDataAdapter(StrQuery, objcon)
            sqladp.Fill(ds)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            ds.Dispose()
        End Try
    End Function
#End Region
#Region "Search Item"
    Public Function SearchItem(SortExpression As String) As DataTable
        StrQuery = "SELECT idmenu,parentid,title,pageurl,sortorder,isactive,imagepath FROM [menu] "
        If title <> "" AndAlso title IsNot Nothing Then
            StrQuery += " WHERE title LIKE '%' + @title + '%'"
        End If
        If SortExpression <> "" AndAlso SortExpression IsNot Nothing Then
            StrQuery += " ORDER BY " & SortExpression
        Else
            StrQuery += " ORDER BY sortorder"
        End If
        Try
            dt = New DataTable()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            If title <> "" AndAlso title IsNot Nothing Then
                sqlcmd.Parameters.AddWithValue("@title", title)
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
#End Region
#Region "Select Single Item"
    Public Function SelectSingleItem() As DataTable
        StrQuery = "SELECT idmenu,parentid,title,pageurl,sortorder,isactive,imagepath FROM menu WHERE idmenu =@idmenu "

        Try
            dt = New DataTable()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            sqlcmd.Parameters.AddWithValue("@idmenu", idmenu)
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
#End Region
#Region "Select Parent Menu"
    Public Function SelectParentMenu() As DataTable
        StrQuery = "SELECT idmenu,parentid,title,pageurl,sortorder,isactive,imagepath FROM menu WHERE parentid = 0"
        Try
            dt = New DataTable()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            sqlcmd.Parameters.AddWithValue("@parentid", parentid)
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
    Public Function SelectParentMenuForHome() As DataTable
        'StrQuery = "SELECT idmenu,parentid,title,pageurl,sortorder,isactive,imagepath FROM menu WHERE parentid=0 and isactive=1";
        StrQuery = " select * from menu inner join adminRights on menu.idmenu=adminRights.idmenu WHERE parentid=0 and isactive=1 and adminid=@adminid "
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
    Public Function SelectSubMenuForHome() As DataTable
        StrQuery = "SELECT idmenu,parentid,title,pageurl,sortorder,isactive,imagepath FROM menu WHERE parentid=@parentid and  isactive=1"
        Try
            dt = New DataTable()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@parentid", parentid)
            Dim sqladp As New SqlDataAdapter()
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
    Public Function SelectSubMenuForHomeMenu() As DataTable
        StrQuery = " select * from menu inner join adminRights a on menu.idmenu=a.idmenu WHERE parentid=@parentid and isactive=1 and adminid=@adminid "
        Try
            dt = New DataTable()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@adminid", adminid)
            sqlcmd.Parameters.AddWithValue("@parentid", parentid)
            Dim sqladp As New SqlDataAdapter()
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
#End Region
#Region "Get Parent Menu Name"
    Public Function getparentmenname() As String
        StrQuery = "SELECT title FROM menu WHERE idmenu=@parentid"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@parentid", parentid)
            Dim str As Object = DirectCast(sqlcmd.ExecuteScalar(), Object)
            Return Convert.ToString(str)
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Function
#End Region

End Class
