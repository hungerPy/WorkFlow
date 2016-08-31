<%@ WebHandler Language="VB" Class="Logout" %>

Imports System
Imports System.Web
Imports System.Web.SessionState
Public Class Logout : Implements IHttpHandler, IRequiresSessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Session.Clear()
        context.Session.Abandon()
        context.Response.Redirect("companyname.aspx?mode=logout")
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class