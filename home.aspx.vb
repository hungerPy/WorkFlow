Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Partial Class Admin_Default2
    Inherits System.Web.UI.Page
    Dim db As New general
    Dim counter As Integer
    Dim counter1 As Integer
    Dim objmenu As New menumanager
    'Dim objmenu As MenuManager = New MenuManager()
    Dim dt As DataTable = New DataTable()


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            counter = 0
            counter1 = 0
        End If
        Page.Title = "Home - " + CommonFunctions.GetKeyValue(2)

        BindHomeMenu()
    End Sub

    Private Sub BindHomeMenu()
        ltrhomemenu.Text = ""
        Dim submenu As String = ""
        Dim mainmenu = ""
        If Session("AdminID") Is Nothing OrElse Convert.ToInt32(Session("AdminID")) = 0 OrElse Convert.ToString(Session("AdminID")) = "" Then
            Response.Redirect("companyname.aspx")
        End If

        objmenu.adminid = Session("AdminID").ToString()
        dt = objmenu.SelectParentMenuForHome()


        If dt.Rows.Count > 0 Then
            mainmenu += "<div class=""widget""><div class=""widget-header""><i class=""icon-bookmark""></i> <h3> Important Shortcuts</h3></div><div class=""widget-content"" ><div class=""shortcuts"">"

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim tempsub As String = ""
                'If Request.Cookies("counter") Is Nothing Then
                'counter = 0
                'Else
                'counter = Int32.Parse(Request.Cookies("counter").Value)
                'End If
                counter = counter + 1
                If counter > 10 Then counter = 1
                Response.Cookies("counter").Value = counter.ToString

                If counter = 1 Then

                    tempsub = BindSubmenuMenu(Convert.ToInt32(dt.Rows(i)("idmenu")), objmenu.adminid)
                    If tempsub <> "" Then
                        mainmenu += tempsub
                    Else
                        'mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><i class='shortcut-icon ") + dt.Rows(i)("imagepath") & "'></i><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                        mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><img src='images/") + dt.Rows(i)("imagepath") & "' /><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                    End If

                ElseIf counter = 2 Then

                    tempsub = BindSubmenuMenu(Convert.ToInt32(dt.Rows(i)("idmenu")), objmenu.adminid)
                    If tempsub <> "" Then
                        mainmenu += tempsub
                    Else
                        'mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><i class='shortcut-icon ") + dt.Rows(i)("imagepath") & "'></i><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                        mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><img src='images/") + dt.Rows(i)("imagepath") & "' /><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                    End If

                ElseIf counter = 3 Then

                    tempsub = BindSubmenuMenu(Convert.ToInt32(dt.Rows(i)("idmenu")), objmenu.adminid)
                    If tempsub <> "" Then
                        mainmenu += tempsub
                    Else
                        'mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><i class='shortcut-icon ") + dt.Rows(i)("imagepath") & "'></i><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                        mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><img src='images/") + dt.Rows(i)("imagepath") & "' /><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                    End If

                ElseIf counter = 4 Then

                    tempsub = BindSubmenuMenu(Convert.ToInt32(dt.Rows(i)("idmenu")), objmenu.adminid)
                    If tempsub <> "" Then
                        mainmenu += tempsub
                    Else
                        'mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><i class='shortcut-icon ") + dt.Rows(i)("imagepath") & "'></i><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                        mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><img src='images/") + dt.Rows(i)("imagepath") & "' /><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                    End If

                ElseIf counter = 5 Then

                    tempsub = BindSubmenuMenu(Convert.ToInt32(dt.Rows(i)("idmenu")), objmenu.adminid)
                    If tempsub <> "" Then
                        mainmenu += tempsub
                    Else
                        'mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><i class='shortcut-icon ") + dt.Rows(i)("imagepath") & "'></i><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                        mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><img src='images/") + dt.Rows(i)("imagepath") & "' /><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                    End If

                ElseIf counter = 6 Then

                    tempsub = BindSubmenuMenu(Convert.ToInt32(dt.Rows(i)("idmenu")), objmenu.adminid)
                    If tempsub <> "" Then
                        mainmenu += tempsub
                    Else
                        'mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><i class='shortcut-icon ") + dt.Rows(i)("imagepath") & "'></i><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                        mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><img src='images/") + dt.Rows(i)("imagepath") & "' /><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                    End If
                ElseIf counter = 7 Then

                    tempsub = BindSubmenuMenu(Convert.ToInt32(dt.Rows(i)("idmenu")), objmenu.adminid)
                    If tempsub <> "" Then
                        mainmenu += tempsub
                    Else
                        'mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><i class='shortcut-icon ") + dt.Rows(i)("imagepath") & "'></i><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                        mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><img src='images/") + dt.Rows(i)("imagepath") & "' /><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                    End If
                ElseIf counter = 8 Then

                    tempsub = BindSubmenuMenu(Convert.ToInt32(dt.Rows(i)("idmenu")), objmenu.adminid)
                    If tempsub <> "" Then
                        mainmenu += tempsub
                    Else
                        'mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><i class='shortcut-icon ") + dt.Rows(i)("imagepath") & "'></i><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                        mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><img src='images/") + dt.Rows(i)("imagepath") & "' /><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                    End If
                ElseIf counter = 9 Then

                    tempsub = BindSubmenuMenu(Convert.ToInt32(dt.Rows(i)("idmenu")), objmenu.adminid)
                    If tempsub <> "" Then
                        mainmenu += tempsub
                    Else
                        'mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><i class='shortcut-icon ") + dt.Rows(i)("imagepath") & "'></i><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                        mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><img src='images/") + dt.Rows(i)("imagepath") & "' /><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                    End If
                ElseIf counter = 10 Then

                    tempsub = BindSubmenuMenu(Convert.ToInt32(dt.Rows(i)("idmenu")), objmenu.adminid)
                    If tempsub <> "" Then
                        mainmenu += tempsub
                    Else
                        'mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><i class='shortcut-icon ") + dt.Rows(i)("imagepath") & "'></i><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                        mainmenu += (("<a href='" + dt.Rows(i)("pageurl") & "' class='shortcut'><img src='images/") + dt.Rows(i)("imagepath") & "' /><span class='shortcut-label'>") + dt.Rows(i)("title") & "</span> </a>"
                    End If
                End If



            Next
            mainmenu += "</div></div></div>"
            ltrhomemenu.Text = mainmenu
        End If

    End Sub
    Public Function BindSubmenuMenu(ByVal id As Integer, ByVal adminid As String) As String
        Dim menu As String = ""
        objmenu.parentid = id
        objmenu.adminid = adminid
        Dim dtsub As DataTable = New DataTable()
        dtsub = objmenu.SelectSubMenuForHomeMenu()
        If dtsub.Rows.Count > 0 Then
            Dim i As Integer

            For i = 0 To dtsub.Rows.Count - 1 Step i + 1
                counter1 = counter1 + 1
                If counter1 > 10 Then counter1 = 1
                Response.Cookies("counter1").Value = counter1.ToString

                If counter1 = 1 Then

                    menu += "<a href='" + dtsub.Rows(i)("pageurl") + "' class='shortcut'><img src='images/" + dtsub.Rows(i)("imagepath") + "' /><span class='shortcut-label'>" + dtsub.Rows(i)("title") + "</span> </a>"
                ElseIf counter1 = 2 Then
                    menu += "<a href='" + dtsub.Rows(i)("pageurl") + "' class='shortcut'><img src='images/" + dtsub.Rows(i)("imagepath") + "' /><span class='shortcut-label'>" + dtsub.Rows(i)("title") + "</span> </a>"

                ElseIf counter1 = 3 Then
                    menu += "<a href='" + dtsub.Rows(i)("pageurl") + "' class='shortcut'><img src='images/" + dtsub.Rows(i)("imagepath") + "' /><span class='shortcut-label'>" + dtsub.Rows(i)("title") + "</span> </a>"

                ElseIf counter1 = 4 Then
                    menu += "<a href='" + dtsub.Rows(i)("pageurl") + "' class='shortcut'><img src='images/" + dtsub.Rows(i)("imagepath") + "' /><span class='shortcut-label'>" + dtsub.Rows(i)("title") + "</span> </a>"

                ElseIf counter1 = 5 Then
                    menu += "<a href='" + dtsub.Rows(i)("pageurl") + "' class='shortcut'><img src='images/" + dtsub.Rows(i)("imagepath") + "' /><span class='shortcut-label'>" + dtsub.Rows(i)("title") + "</span> </a>"

                ElseIf counter1 = 6 Then
                    menu += "<a href='" + dtsub.Rows(i)("pageurl") + "' class='shortcut'><img src='images/" + dtsub.Rows(i)("imagepath") + "' /><span class='shortcut-label'>" + dtsub.Rows(i)("title") + "</span> </a>"
                ElseIf counter1 = 7 Then
                    menu += "<a href='" + dtsub.Rows(i)("pageurl") + "' class='shortcut'><img src='images/" + dtsub.Rows(i)("imagepath") + "' /><span class='shortcut-label'>" + dtsub.Rows(i)("title") + "</span> </a>"

                ElseIf counter1 = 8 Then
                    menu += "<a href='" + dtsub.Rows(i)("pageurl") + "' class='shortcut'><img src='images/" + dtsub.Rows(i)("imagepath") + "' /><span class='shortcut-label'>" + dtsub.Rows(i)("title") + "</span> </a>"

                ElseIf counter1 = 9 Then
                    menu += "<a href='" + dtsub.Rows(i)("pageurl") + "' class='shortcut'><img src='images/" + dtsub.Rows(i)("imagepath") + "' /><span class='shortcut-label'>" + dtsub.Rows(i)("title") + "</span> </a>"

                ElseIf counter1 = 10 Then
                    menu += "<a href='" + dtsub.Rows(i)("pageurl") + "' class='shortcut'><img src='images/" + dtsub.Rows(i)("imagepath") + "' /><span class='shortcut-label'>" + dtsub.Rows(i)("title") + "</span> </a>"

                End If

                ''menu += "<a href='" + dtsub.Rows(i)("pageurl") + "' class='shortcut'><i class='shortcut-icon " + dtsub.Rows(i)("imagepath") + "'></i><span class='shortcut-label'>" + dtsub.Rows(i)("title") + "</span> </a>"
                'menu += "<a href='" + dtsub.Rows(i)("pageurl") + "' class='shortcut'><img src='images/" + dtsub.Rows(i)("imagepath") + "' /><span class='shortcut-label'>" + dtsub.Rows(i)("title") + "</span> </a>"
            Next

        End If
        Return menu
    End Function





End Class



