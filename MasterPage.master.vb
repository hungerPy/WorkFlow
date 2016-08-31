Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Net
Imports System.IO
Partial Class Admin_MasterPage
    Inherits System.Web.UI.MasterPage
    Protected strmenu As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        Session("adminId") = 61
        Session("fullname") = "mukesh"
        
        strmenu = (CommonFunctions.GetFileContents(Server.MapPath("menu/") + Convert.ToString(Session("adminId")) + ".htm"))

    End Sub

End Class


