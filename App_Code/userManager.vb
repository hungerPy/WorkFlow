Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Web

Public Class userManager
#Region "Constructor"
    Public Sub New()
    End Sub
#End Region
#Region "Global Variables"
    Private StrQuery As [String]
    Private ds As New DataSet()
    Private objcon As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConn"))
#End Region

#Region "Private Members & Public Properties"
    Public Property uid() As Integer
        Get
            Return m_uid
        End Get
        Set(value As Integer)
            m_uid = value
        End Set
    End Property
    Private m_uid As Integer
    Public Property aid() As Integer
        Get
            Return m_aid
        End Get
        Set(value As Integer)
            m_aid = value
        End Set
    End Property
    Private m_aid As Integer
    Public Property session() As String
        Get
            Return m_session
        End Get
        Set(value As String)
            m_session = value
        End Set
    End Property
    Private m_session As String
    Public Property status() As String
        Get
            Return m_status
        End Get
        Set(value As String)
            m_status = value
        End Set
    End Property
    Private m_status As String
    Public Property siteoffice() As Integer
        Get
            Return m_siteoffice
        End Get
        Set(value As Integer)
            m_siteoffice = value
        End Set
    End Property
    Private m_siteoffice As Integer
    Public Property employee() As Integer
        Get
            Return m_employee
        End Get
        Set(value As Integer)
            m_employee = value
        End Set
    End Property
    Private m_employee As Integer
    Public Property adminid() As Integer
        Get
            Return m_adminid
        End Get
        Set(value As Integer)
            m_adminid = value
        End Set
    End Property
    Private m_adminid As Integer
    Public Property firstname() As String
        Get
            Return m_firstname
        End Get
        Set(value As String)
            m_firstname = value
        End Set
    End Property
    Private m_firstname As String
    Public Property lastname() As String
        Get
            Return m_lastname
        End Get
        Set(value As String)
            m_lastname = value
        End Set
    End Property
    Private m_lastname As String
    Public Property emailaddress() As String
        Get
            Return m_emailaddress
        End Get
        Set(value As String)
            m_emailaddress = value
        End Set
    End Property
    Private m_emailaddress As String
    Public Property userid() As String
        Get
            Return m_userid
        End Get
        Set(value As String)
            m_userid = value
        End Set
    End Property
    Private m_userid As String
    Public Property password() As String
        Get
            Return m_password
        End Get
        Set(value As String)
            m_password = value
        End Set
    End Property
    Private m_password As String
    Public Property admintype() As String
        Get
            Return m_admintype
        End Get
        Set(value As String)
            m_admintype = value
        End Set
    End Property
    Private m_admintype As String
    Public Property isactive() As Byte
        Get
            Return m_isactive
        End Get
        Set(value As Byte)
            m_isactive = value
        End Set
    End Property
    Private m_isactive As Byte
    Public Property idmenu() As Integer
        Get
            Return m_idmenu
        End Get
        Set(value As Integer)
            m_idmenu = value
        End Set
    End Property
    Private m_idmenu As Integer
    Public Property title() As String
        Get
            Return m_title
        End Get
        Set(value As String)
            m_title = value
        End Set
    End Property
    Private m_title As String
    Public Property parentid() As Integer
        Get
            Return m_parentid
        End Get
        Set(value As Integer)
            m_parentid = value
        End Set
    End Property
    Private m_parentid As Integer
    Public Property pageurl() As String
        Get
            Return m_pageurl
        End Get
        Set(value As String)
            m_pageurl = value
        End Set
    End Property
    Private m_pageurl As String
    Public Property sortorder() As Integer
        Get
            Return m_sortorder
        End Get
        Set(value As Integer)
            m_sortorder = value
        End Set
    End Property
    Private m_sortorder As Integer
#End Region
#Region "Check Authentication"
    Public Function CheckAuthentication() As DataSet
        StrQuery = "SELECT ISNULL([adminid],0) AS adminid"
        StrQuery += " ,ISNULL([userid],'') AS userid"
        StrQuery += " ,ISNULL([password],'') AS password"
        StrQuery += " ,ISNULL([firstname],'') AS firstname"
        StrQuery += " ,ISNULL([lastname],'') AS lastname"
        StrQuery += " ,ISNULL([emailaddress],'') AS emailaddress"
        StrQuery += " ,ISNULL(admintype,'') AS admintype"
        StrQuery += " FROM administrator"
        StrQuery += " WHERE emailaddress = @emailaddress AND password = @password"
        Try
            ds = New DataSet()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@emailaddress", emailaddress)
            sqlcmd.Parameters.AddWithValue("@password", password)
            Dim sqladp As New SqlDataAdapter(sqlcmd)

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
#Region "Forgot Password Admin"
    Public Function ForgotPasswordAdmin() As DataSet
        StrQuery = " Select isnull([adminid],0) as adminid,isnull([userid],'') as userid,isnull([password],'') as password,isnull([firstname],'') as firstname,isnull([lastname],'') as lastname,isnull([emailaddress],'') as emailaddress from administrator where userid=@userid"
        Try
            ds = New DataSet()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            sqlcmd.Parameters.AddWithValue("@userid", userid)
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
            StrQuery = "select count(adminid) from administrator where emailaddress = @emailid"

            If adminid <> 0 Then
                StrQuery += " and adminid <> @adminid"
            End If

            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            objcon.Open()
            sqlcmd.Parameters.AddWithValue("@emailid", emailaddress)
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
    Public Function UseridExist() As [Boolean]
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

    'search admin
    Public Function SearchItem() As DataSet
        StrQuery = "select * from users"
        'StrQuery = "select isnull([adminid],0) as adminid,isnull([userid],'') as userid,isnull([password],'') as password,isnull([firstname],'') as firstname,isnull([lastname],'') as lastname,isnull([emailaddress],'') as emailaddress,isnull(adminType,'') as adminType,isnull(isActive,0) as isActive from administrator "
        If userid <> "" Then
            StrQuery += " where emailaddress like '%' + @emailaddress + '%'"
        End If

        Try
            ds = New DataSet()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            If userid <> "" Then
                sqlcmd.Parameters.AddWithValue("@emailaddress", emailaddress)
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

    'select all admin items
    Public Function SelectAllItem() As DataSet
        StrQuery = "select isnull([adminid],0) as adminid,isnull([userid],'') as userid,isnull([password],'') as password,isnull([firstname],'') as firstname,isnull([lastname],'') as lastname,isnull([emailaddress],'') as emailaddress,isnull(isActive,0) as isActive  from administrator order by firstname "

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

    'select single admin details
    Public Function SelectSingleItem() As DataSet
        StrQuery = "Select * from users where uid=@adminid"
        'StrQuery = "select isnull([adminid],0) as adminid,isnull([userid],'') as userid,isnull([password],'') as password,isnull([firstname],'') as firstname,isnull([lastname],'') as lastname,isnull([emailaddress],'') as emailaddress,isnull(adminType,'') as adminType,isnull(isActive,0) as isActive  from administrator where adminid=@adminid"

        Try
            ds = New DataSet()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            sqlcmd.Parameters.AddWithValue("@adminid", adminid)
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

    'select admin details by userid
    Public Function SelectAdminByUserId() As DataSet
        StrQuery = "select isnull([adminid],0) as adminid,isnull([userid],'') as userid,isnull([password],'') as password,isnull([firstname],'') as firstname,isnull([lastname],'') as lastname,isnull([emailaddress],'') as emailaddress from administrator where userid=@userid"

        Try
            ds = New DataSet()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            sqlcmd.Parameters.AddWithValue("@userid", userid)
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
    Public Function SelectAdminByEmailAddress() As DataSet
        StrQuery = "select isnull([adminid],0) as adminid,isnull([userid],'') as userid,isnull([password],'') as password,isnull([firstname],'') as firstname,isnull([lastname],'') as lastname,isnull([emailaddress],'') as emailaddress from administrator where emailaddress=@emailaddress"

        Try
            ds = New DataSet()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            sqlcmd.Parameters.AddWithValue("@emailaddress", emailaddress)
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
    'delete admin details
    Public Sub DeleteItem()
        StrQuery = "delete from users where uid=@adminid"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@adminid", adminid)
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try

    End Sub

    'insert admin details
    Public Sub InsertItem()
        StrQuery = "insert into users(uid,empid,userid,pwd,sessionid,status)values(@uid,@employee,@userid,@password,@session,@status)"
        'StrQuery = "insert into administrator(userid,password,firstname,lastname,emailaddress,adminType,isActive,createdate) values(@userid,@password,@firstname,@lastname,@emailaddress,'subadmin',@isactive,getdate())"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@uid", uid)
            sqlcmd.Parameters.AddWithValue("@userid", userid)
            sqlcmd.Parameters.AddWithValue("@password", password)
            sqlcmd.Parameters.AddWithValue("@siteoffice", siteoffice)
            sqlcmd.Parameters.AddWithValue("@employee", employee)
            sqlcmd.Parameters.AddWithValue("@session", session)
            sqlcmd.Parameters.AddWithValue("@status", status)
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try


        StrQuery = "insert into allot(aid,uid,plantid)values(@aid,@uid,@plantid)"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@uid", uid)
            sqlcmd.Parameters.AddWithValue("@aid", aid)
            sqlcmd.Parameters.AddWithValue("@plantid", siteoffice)
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try


    End Sub

    'update admin details
    Public Sub UpdateItem()
        StrQuery = "update users set empid=@employee,userid=@userid,pwd=@password,sessionid=@session,status=@status where uid=@uid"
        'StrQuery = "update administrator set userid=@userid,password=@password,firstname=@firstname,lastname=@lastname,emailaddress=@emailaddress,isactive=@isactive,modifydate=getdate() where adminid=@adminid"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@uid", uid)
            sqlcmd.Parameters.AddWithValue("@userid", userid)
            sqlcmd.Parameters.AddWithValue("@password", password)
            sqlcmd.Parameters.AddWithValue("@siteoffice", siteoffice)
            sqlcmd.Parameters.AddWithValue("@employee", employee)
            sqlcmd.Parameters.AddWithValue("@session", session)
            sqlcmd.Parameters.AddWithValue("@status", status)
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try

        StrQuery = "update allot set uid=@uid,plantid=@plantid where aid=@aid"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@uid", uid)
            sqlcmd.Parameters.AddWithValue("@aid", aid)
            sqlcmd.Parameters.AddWithValue("@plantid", siteoffice)
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try

    End Sub

    'fetch admin name
    Public Function getName() As String
        StrQuery = "select firstname + ' ' + lastname from administrator where adminid =@adminid"
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

#Region "----------------------------public methods---------------------------"
    'select admin menu
    Public Function SelectMenuItem() As DataSet
        StrQuery = "select isnull([idmenu],0) as idmenu,isnull([title],'') as title,isnull([isactive],0) as isactive " & " ,isnull([parentid],0) as parentid,isnull([pageurl],'') as pageurl,isnull([sortorder],0) as sortorder " & " from [menu] where isactive=1"

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

    'select single admin parent menu items
    Public Function selectParentMenus() As DataSet
        StrQuery = "select isnull(menu.[idmenu],0) as idmenu,isnull([title],'') as title,isnull(menu.[isactive],0) as isactive " & " ,isnull([parentid],0) as parentid,isnull([pageurl],'') as pageurl,isnull([sortorder],0) as sortorder " & " from [menu] " & " where parentid=0 and isactive = 1 order by sortorder"

        Try
            ds = New DataSet()
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

    'select single admin sub menu items
    Public Function selectSubMenus() As DataSet
        StrQuery = "select isnull(menu.[idmenu],0) as idmenu,isnull([title],'') as title,isnull(menu.[isactive],0) as isactive " & " ,isnull([parentid],0) as parentid,isnull([pageurl],'') as pageurl,isnull([sortorder],0) as sortorder " & " from [menu] " & " where parentid=@parentid"

        Try
            ds = New DataSet()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            sqlcmd.Parameters.AddWithValue("@parentid", parentid)
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

    'select single admin parent menu items
    Public Function selectAdminParentMenus() As DataTable
        'StrQuery = "select isnull(menu.[idmenu],0) as idmenu,isnull([title],'') as title,isnull(menu.[isactive],0) as isactive " & " ,isnull([parentid],0) as parentid,isnull([pageurl],'') as pageurl,isnull([imagepath],'') as imagepath,isnull([sortorder],0) as sortorder " & " from [menu] " & " inner join adminRights on adminRights.idmenu = menu.idmenu " & " inner join administrator on administrator.adminid = adminRights.adminid " & " where administrator.adminid=@adminid and parentid=0  order by sortorder"
        StrQuery = "select isnull(menu.[idmenu],0) as idmenu,isnull([title],'') as title,isnull(menu.[isactive],0) as isactive " & " ,isnull([parentid],0) as parentid,isnull([pageurl],'') as pageurl,isnull([imagepath],'') as imagepath,isnull([sortorder],0) as sortorder " & " from [menu] " & " inner join adminRights on adminRights.idmenu = menu.idmenu " & " inner join users on users.uid = adminRights.adminid " & " where users.uid=@adminid and parentid=0  order by sortorder"
        Try
            Dim dt As New DataTable()
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
            ds.Dispose()
            objcon.Close()
        End Try
    End Function

    'select single admin sub menu items
    Public Function selectAdminSubMenus() As DataTable
        'StrQuery = "select isnull(menu.[idmenu],0) as idmenu,isnull([title],'') as title,isnull(menu.[isactive],0) as isactive " & " ,isnull([parentid],0) as parentid,isnull([pageurl],'') as pageurl,isnull([sortorder],0) as sortorder " & " from [menu] " & " inner join adminRights on adminRights.idmenu = menu.idmenu " & " inner join administrator on administrator.adminid = adminRights.adminid " & " where administrator.adminid=@adminid and parentid=@parentid  order by sortorder"
        StrQuery = "select isnull(menu.[idmenu],0) as idmenu,isnull([title],'') as title,isnull(menu.[isactive],0) as isactive " & " ,isnull([parentid],0) as parentid,isnull([pageurl],'') as pageurl,isnull([sortorder],0) as sortorder " & " from [menu] " & " inner join adminRights on adminRights.idmenu = menu.idmenu " & " inner join users on users.uid = adminRights.adminid " & " where users.uid=@adminid and parentid=@parentid  order by sortorder"
        Try
            Dim dt As New DataTable()
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            Dim sqladp As New SqlDataAdapter()
            sqlcmd.Parameters.AddWithValue("@adminid", adminid)
            sqlcmd.Parameters.AddWithValue("@parentid", parentid)
            sqladp = New SqlDataAdapter(sqlcmd)
            sqladp.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            ds.Dispose()
            objcon.Close()
        End Try
    End Function

    'delete admin rights
    Public Sub DeleteAdminRightsItem()
        StrQuery = "delete from adminRights where adminid=@adminid"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@adminid", adminid)
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try

    End Sub

    'insert admin details
    Public Sub InsertAdminRolesItem()
        StrQuery = "insert into [adminRights]([idmenu],[adminid]) values (@idmenu,@adminid)"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@idmenu", idmenu)
            sqlcmd.Parameters.AddWithValue("@adminid", adminid)
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Sub

    'parent child count for admin
    Public Function getAdminmenu() As Integer
        StrQuery = " select count(*) from [menu] " & " inner join adminRights on adminRights.idmenu = menu.idmenu " & " inner join users on users.uid = adminRights.adminid " & " where adminRights.idmenu=@idmenu and users.uid=@adminid"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.Add(New SqlParameter("@idmenu", SqlDbType.Int)).Value = idmenu
            sqlcmd.Parameters.Add(New SqlParameter("@adminid", SqlDbType.Int)).Value = adminid
            Return Convert.ToInt32(sqlcmd.ExecuteScalar())
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Function

    'top menu count for admin
    Public Function gettopmenuchild() As Integer
        'StrQuery = " select count(*) from [menu] " & " inner join adminRights on adminRights.idmenu = menu.idmenu " & " inner join administrator on administrator.adminid = adminRights.adminid " & " where administrator.adminid=@adminid" & " and menu.parentid=@idmenu "
        StrQuery = " select count(*) from [menu] " & " inner join adminRights on adminRights.idmenu = menu.idmenu " & " inner join users on users.uid = adminRights.adminid " & " where users.uid=@adminid" & " and menu.parentid=@idmenu "
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.Add(New SqlParameter("@idmenu", SqlDbType.Int)).Value = idmenu
            sqlcmd.Parameters.Add(New SqlParameter("@adminid", SqlDbType.Int)).Value = adminid
            Return Convert.ToInt32(sqlcmd.ExecuteScalar())
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Function

    'make top menu bar
    Public Function getTopMenu() As String
        Dim strmenu As String = String.Empty
        Dim dtmenu As New DataTable()
        dtmenu = selectAdminParentMenus()
        If dtmenu.Rows.Count > 0 Then

            strmenu += vbTab & "<li class='rootmenu'><a href=""home.aspx""><i class=""icon-home""></i><span>Home</span> </a> </li>" & vbLf
            For i As Integer = 0 To dtmenu.Rows.Count - 1
                idmenu = Convert.ToInt32(dtmenu.Rows(i)("idmenu"))
                If gettopmenuchild() > 0 Then
                    strmenu += vbTab & "<li class=""rootmenu dropdown""><a href='javascript:void(0);' class=""dropdown-toggle"" data-toggle=""dropdown""><i class=""icon-long-arrow-down""></i><span>" & Convert.ToString(dtmenu.Rows(i)("title")) & "</span> </a> " & vbLf & vbTab & vbTab & "<ul class=""dropdown-menu"">" & vbLf & getSubMenus(adminid, Convert.ToInt32(dtmenu.Rows(i)("idmenu")), 0) & vbTab & vbTab & "</ul>" & vbLf & vbTab & "</li>" & vbLf
                Else

                    'strmenu += vbTab & "<li class='rootmenu'><a href='" & dtmenu.Rows(i)("pageurl").ToString().ToLower() & "'><i class=""" & Convert.ToString(dtmenu.Rows(i)("imagepath")) & """></i><span>" & Convert.ToString(dtmenu.Rows(i)("title")) & "</span> </a> </li>" & vbLf
                    strmenu += vbTab & "<li class='rootmenu'><a href='" & dtmenu.Rows(i)("pageurl").ToString().ToLower() & "'><img src=images/" & Convert.ToString(dtmenu.Rows(i)("imagepath")) & " /><span>" & Convert.ToString(dtmenu.Rows(i)("title")) & "</span> </a> </li>" & vbLf
                End If

            Next

        Else
        End If
        Return strmenu
    End Function

    'make Top Sub Menus
    Public Function getSubMenus(idadmin As Integer, parentID__1 As Integer, intlevel As Integer) As String
        Dim strmenu As String = String.Empty
        Dim dtmenu As New DataTable()
        adminid = idadmin
        parentid = parentID__1
        dtmenu = selectAdminSubMenus()
        If dtmenu.Rows.Count > 0 Then
            intlevel += 1
            Dim starttab As String = vbTab & vbTab
            For j As Integer = 0 To intlevel - 1
                starttab += vbTab
            Next

            For i As Integer = 0 To dtmenu.Rows.Count - 1
                parentid = Convert.ToInt32(dtmenu.Rows(i)("idmenu"))
                strmenu += starttab & "<li><a href='" & dtmenu.Rows(i)("pageurl").ToString().ToLower() & "' title='" & Convert.ToString(dtmenu.Rows(i)("title")) & "'>" & Convert.ToString(dtmenu.Rows(i)("title")) & "</a></li>" & vbLf
            Next
        End If
        Return strmenu
    End Function

    'home page menu for admin
    Public Function getAdminHomePagemenu() As DataTable
        StrQuery = "select isnull(menu.[idmenu],0) as idmenu,isnull([title],'') as title,isnull(menu.[isactive],0) as isactive " & " ,isnull([parentid],0) as parentid,isnull([pageurl],'') as pageurl,isnull(imagepath,'') as imagepath,isnull(menu.[sortorder],0) as sortorder " & " from [menu] " & " inner join adminRights on adminRights.idmenu = menu.idmenu " & " inner join users on users.uid = adminRights.adminid " & " where users.uid=@adminid and menu.isactive=1 and pageurl<>''"
        Try
            Dim dt As New DataTable()
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
            ds.Dispose()
            objcon.Close()
        End Try
    End Function

#End Region
#Region "Update Status"
    Public Sub UpdateActive()
        StrQuery = " UPDATE administrator SET isactive=@isactive WHERE adminid=@adminid"
        Try
            objcon.Open()
            Dim sqlcmd As New SqlCommand(StrQuery, objcon)
            sqlcmd.Parameters.AddWithValue("@isactive", isactive)
            sqlcmd.Parameters.AddWithValue("@adminid", adminid)
            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Sub
#End Region
End Class
