Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.Net
Imports System.IO

Partial Public Class Admin_Default
    Inherits System.Web.UI.Page
    Protected strname As String = ""
    Public db As New general
    Dim dr As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        ' strname = CommonFunctions.GetKeyValue(2).ToString()
        If Not IsPostBack Then
            If Request.QueryString("mode") = "logout" Then
                lblmsg.Visible = True
                lblmsg.Text = "You have successfully Logged out."
            End If
        End If
    End Sub
    Protected Sub btnlogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnlogin.Click


        Dim objadmin As New adminmanager
        'Dim objadmin As adminmanager = New adminmanager()
        Dim dt As DataTable = New DataTable()
       
        objadmin.emailaddress = CommonFunctions.SanitizeInput(txtuser.Text.Trim().Replace("'", ""))
        objadmin.password = CommonFunctions.SanitizeInput(txtpass.Text)
        dt = objadmin.CheckAuthentication()
        If dt.Rows.Count > 0 Then
            'Session("AdminID") = dt.Rows(0)("adminid").ToString()
            'Session("AdminType") = dt.Rows(0)("adminType").ToString()
            'Session("fullname") = dt.Rows(0)("firstname").ToString() + " " + dt.Rows(0)("lastname").ToString()

            'LogUserVisit();
            Session("fullname") = dt.Rows(0)("userId").ToString()
            Session("AdminID") = dt.Rows(0)("uid").ToString()
            Session("uid") = dt.Rows(0)("uid").ToString()
            Session("userid") = dt.Rows(0)("uid").ToString()
            Session("loginName") = dt.Rows(0)("userId").ToString()
            Session("password") = dt.Rows(0)("pwd").ToString()
            Session("Psiteoffice") = dt.Rows(0)("plantid").ToString()
            Session("AdminType") = "superadmin"
            Session("plantId") = "1"
            dr = db.fillReader("select * from login where username='" & Session("loginName") & "' and password='" & Session("password") & "' and flg=0")
            If dr.read Then
                Session("company") = dr("companyCode")
                Dim company As String = Session("company")
            End If
            Response.Redirect("home.aspx")
        Else
            lblmsg.Visible = True
            lblmsg.Text = "Username Or Password is invalid."
        End If

    End Sub
    Private Sub LogUserVisit()

        Dim strIpAddress As String
        strIpAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If strIpAddress Is Nothing Then
            strIpAddress = Request.ServerVariables("REMOTE_ADDR")
        End If

        ' Track Visitors
        Dim ipAddress As String = strIpAddress
        Dim hostName As String = Dns.GetHostByAddress(ipAddress).HostName
        Dim wrtr As StreamWriter = New StreamWriter(Server.MapPath("visitors.log"), True)
        wrtr.WriteLine(DateTime.Now.ToString() + " | " + ipAddress + " | " + hostName + " | " + Request.Url.ToString())
        wrtr.Close()
    End Sub

    'Private Function IpAddress() As String
    '    Dim strIpAddress As String
    '    strIpAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
    '    If strIpAddress Is Nothing Then
    '        strIpAddress = Request.ServerVariables("REMOTE_ADDR")
    '    End If
    '    Return strIpAddress
    'End Function
End Class
