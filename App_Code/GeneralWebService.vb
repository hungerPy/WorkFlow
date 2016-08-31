Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports System.Net
Imports System.Text
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class GeneralWebService
    Inherits System.Web.Services.WebService
    Public db As New general
    '--------------------------Session Use in this function "GeneralIssueProductName"---------------------
    <WebMethod()> _
      Public Function ProductMasterGetSCat(ByVal username As String) As List(Of String)
        Dim result As New List(Of String)()
        Dim usersName As String = username
        Dim Productcatid() As String = {"#"}
        Productcatid = usersName.ToString.Split("#")
        Dim name = Productcatid(0)
        Dim catid = Productcatid(1)
        Using con As New SqlConnection()
            con.ConnectionString = general.strConn
            Dim sql As String = "select scatname,scatid from productsubcategory where flg=0 and catid=" & catid & " and scatname LIKE '%'+@SearchText+'%'"
            Using cmd As New SqlCommand(sql, con)
                con.Open()
                cmd.Parameters.AddWithValue("@SearchText", name)
cmd.CommandTimeout = 90000
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    result.Add(String.Format("{0}/{1}", dr("scatname"), dr("scatid")))
                End While
                Return result
                dr.Close()
                con.Close()
            End Using
        End Using
    End Function

    <WebMethod()> _
       Public Function ProductMasterGetSSCat(ByVal username As String) As List(Of String)
        Dim result As New List(Of String)()
        Dim sscatid As Integer = 0
        Dim usersName As String = username
        Dim Productsubcatid() As String = {"#"}
        Productsubcatid = usersName.ToString.Split("#")
        Dim name = Productsubcatid(0)
        Dim subcatid = Productsubcatid(1)
        Dim catid = Productsubcatid(2)
        Using con As New SqlConnection()
            con.ConnectionString = general.strConn
            Dim sql As String = "select sscatname,sscatid from productsubsubcategory where flg=0 and catid=" & catid & "  and scatid=" & subcatid & "  and sscatname  LIKE '%'+@SearchText+'%'"
            Using cmd As New SqlCommand(sql, con)
                con.Open()
                cmd.Parameters.AddWithValue("@SearchText", name)
cmd.CommandTimeout = 90000
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    result.Add(String.Format("{0}/{1}", dr("sscatname"), dr("sscatid")))
                End While
                Return result
                dr.Close()
                con.Close()
            End Using
        End Using
    End Function


    <WebMethod()> _
Public Function InternalissueGetScat(ByVal username As String) As List(Of String)
        Dim result As New List(Of String)()
        Dim usersName As String = username
        Dim Productcatid() As String = {"#"}
        Productcatid = usersName.ToString.Split("#")
        Dim name = Productcatid(0)
        Dim catid = Productcatid(1)
        Using con As New SqlConnection()
            con.ConnectionString = general.strConn
            Dim sql As String = "select scatname,scatid from productsubcategory where flg=0 and catid=" & catid & " and scatname LIKE '%'+@SearchText+'%'"
            Using cmd As New SqlCommand(sql, con)
                con.Open()
                cmd.Parameters.AddWithValue("@SearchText", name)
cmd.CommandTimeout = 90000
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    result.Add(String.Format("{0}/{1}", dr("scatname"), dr("scatid")))
                End While
                Return result
                dr.Close()
                con.Close()
            End Using
        End Using
    End Function

    <WebMethod()> _
   Public Function IssueTransferGetProduct(ByVal username As String) As List(Of String)
        Dim result As New List(Of String)()
        Dim sscatid As Integer = 0
        Dim usersName As String = username
        Dim Productsubcatid() As String = {"#"}
        Productsubcatid = usersName.ToString.Split("#")
        Dim name = Productsubcatid(0)
        Dim subcatid = Productsubcatid(1)
        Dim catid = Productsubcatid(2)
        Dim siteoff = Productsubcatid(3)
        Dim warehouse = Productsubcatid(4)
        Using con As New SqlConnection()
            con.ConnectionString = general.strConn
            'Dim sql As String = "select sscatname,sscatid from productsubsubcategory where flg=0 and catid=" & catid & "  and scatid=" & subcatid & "  and sscatname  LIKE '%'+@SearchText+'%'"
            Dim sql As String = "SELECT EXPR1,PRODUCTID FROM (select (select productcode+'-'+productname from productmaster where productid=p.productid) as expr1,productid from productstock as p where siteoffid=" & siteoff & " and warehouseid=" & warehouse & " and  plantid=2 and productid in(select productid from productmaster where catid<>2) and catid=" & catid & "  and scatid=" & subcatid & " ) AS TBL WHERE expr1  LIKE '%'+@SearchText+'%'"

            Using cmd As New SqlCommand(sql, con)
                con.Open()
                cmd.Parameters.AddWithValue("@SearchText", name)
cmd.CommandTimeout = 90000
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    result.Add(String.Format("{0}/{1}", dr("expr1"), dr("productid")))
                End While
                Return result
                dr.Close()
                con.Close()
            End Using
        End Using
    End Function

    <WebMethod()> _
 Public Function IssueByEmp(ByVal username As String) As List(Of String)
        Dim result As New List(Of String)()
        Using con As New SqlConnection()
            con.ConnectionString = general.strConn
            Dim sql As String = "select empName+'-'+Designation as empName,empid from employee where status<>'Deactive' and empName  LIKE '%'+@SearchText+'%'"
            Using cmd As New SqlCommand(sql, con)
                con.Open()
                cmd.Parameters.AddWithValue("@SearchText", username)
cmd.CommandTimeout = 90000
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    result.Add(String.Format("{0}/{1}", dr("empName"), dr("empid")))
                End While
                Return result
                dr.Close()
                con.Close()
            End Using
        End Using
    End Function

    <WebMethod()> _
Public Function PurchaseChallanGetScat(ByVal username As String) As List(Of String)
        Dim result As New List(Of String)()
        Dim usersName As String = username
        Dim Productcatid() As String = {"#"}
        Productcatid = usersName.ToString.Split("#")
        Dim name = Productcatid(0)
        Dim catid = Productcatid(1)
        Using con As New SqlConnection()
            con.ConnectionString = general.strConn
            Dim sql As String = "select scatname,scatid from productsubcategory where flg=0 and catid=" & catid & " and scatname LIKE '%'+@SearchText+'%'"
            Using cmd As New SqlCommand(sql, con)
                con.Open()
                cmd.Parameters.AddWithValue("@SearchText", name)
cmd.CommandTimeout = 90000
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    result.Add(String.Format("{0}/{1}", dr("scatname"), dr("scatid")))
                End While
                Return result
                dr.Close()
                con.Close()
            End Using
        End Using
    End Function

    <WebMethod()> _
Public Function PurchasechallanGetPurchaseProduct(ByVal username As String) As List(Of String)
        Dim result As New List(Of String)()
        Dim sscatid As Integer = 0
        Dim usersName As String = username
        Dim Productsubcatid() As String = {"#"}
        Productsubcatid = usersName.ToString.Split("#")
        Dim name = Productsubcatid(0)
        Dim subcatid = Productsubcatid(1)
        Dim catid = Productsubcatid(2)
        Using con As New SqlConnection()
            con.ConnectionString = general.strConn
            Dim sql As String = "select productcode+'-'+productname as productcode ,productid from productmaster where flg=0 and catid=" & catid & "  and scatid=" & subcatid & "  and productcode  LIKE '%'+@SearchText+'%'"
            Using cmd As New SqlCommand(sql, con)
                con.Open()
                cmd.Parameters.AddWithValue("@SearchText", name)
cmd.CommandTimeout = 90000
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    result.Add(String.Format("{0}/{1}", dr("productcode"), dr("productid")))
                End While
                Return result
                dr.Close()
                con.Close()
            End Using
        End Using
    End Function



    <WebMethod()> _
    Public Function GeneralProductSSCat(ByVal username As String) As List(Of String)
        Dim result As New List(Of String)()
        Dim sscatid As Integer = 0
        Dim usersName As String = username
        Dim Productsubcatid() As String = {"#"}
        Productsubcatid = usersName.ToString.Split("#")
        Dim name = Productsubcatid(0)
        Dim catid = Productsubcatid(1)
        Using con As New SqlConnection()
            con.ConnectionString = general.strConn
            Dim sql As String = "select sscatname,sscatid from productsubsubcategory where flg=0 and catid=" & catid & " and sscatname  LIKE '%'+@SearchText+'%'"
            Using cmd As New SqlCommand(sql, con)
                con.Open()
                cmd.Parameters.AddWithValue("@SearchText", name)
cmd.CommandTimeout = 90000
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    result.Add(String.Format("{0}/{1}", dr("sscatname"), dr("sscatid")))
                End While
                Return result
                dr.Close()
                con.Close()
            End Using
        End Using
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function GeneralIssueProductName(ByVal username As String) As List(Of String)
        Dim result As New List(Of String)()
        Dim sscatid As Integer = 0
        Dim usersName As String = username
        Dim Productsubcatid() As String = {"#"}
        Productsubcatid = usersName.ToString.Split("#")
        Dim name = Productsubcatid(0)
        Dim subsubcatid = Productsubcatid(1)
        Dim catid = Productsubcatid(2)
        Dim plantid = Context.Session("plantid")
        Using con As New SqlConnection()
            con.ConnectionString = general.strConn
            Dim sql As String = "select productcode+'-'+productname as productcode ,productid from productmaster where plantid=" & plantid & " and flg=0 and catid=" & catid & "  and sscatid=" & subsubcatid & "  and productcode  LIKE '%'+@SearchText+'%'"
            Using cmd As New SqlCommand(sql, con)
                con.Open()
                cmd.Parameters.AddWithValue("@SearchText", name)
cmd.CommandTimeout = 90000
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    result.Add(String.Format("{0}/{1}", dr("productcode"), dr("productid")))
                End While
                Return result
                dr.Close()
                con.Close()
            End Using
        End Using
    End Function

    <WebMethod()> _
        Public Function GeneralEmpName(ByVal username As String) As List(Of String)
        Dim result As New List(Of String)()
        Dim usersName As String = username
        Dim Productsubcatid() As String = {"#"}
        Productsubcatid = usersName.ToString.Split("#")
        Dim name = Productsubcatid(0)
        Dim departid = Productsubcatid(1)
        Dim divisionHead As String = db.getFieldValue("divisions", "divisionId", departid, "divisionHead")
        Using con As New SqlConnection()
            con.ConnectionString = general.strConn
            Dim sql As String = "select empName+'-'+Designation as empName,empid from employee where status<>'Deactive' and designation='" & divisionHead & "' and empName  LIKE '%'+@SearchText+'%'"
            Using cmd As New SqlCommand(sql, con)
                con.Open()
                cmd.Parameters.AddWithValue("@SearchText", name)
cmd.CommandTimeout = 90000
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    result.Add(String.Format("{0}/{1}", dr("empName"), dr("empid")))
                End While
                Return result
                dr.Close()
                con.Close()
            End Using
        End Using
    End Function


    <WebMethod(EnableSession:=True)> _
    Public Function SalesOrderProduct(ByVal username As String) As List(Of String)
        Dim result As New List(Of String)()
        Dim usersName As String = username
        Dim Productcatid() As String = {"#"}
        Productcatid = usersName.ToString.Split("#")
        Dim name = Productcatid(0)
        Dim catid = Productcatid(1)
        Dim plantid = Context.Session("plantid")
        Using con As New SqlConnection()
            con.ConnectionString = general.strConn
            Dim sql As String = "select productcode+'-'+productName as productcode,productid from productmaster where plantid=" & plantid & " and flg=0 and catid=1 and scatid=" & catid & " and productcode LIKE '%'+@SearchText+'%'"
            Using cmd As New SqlCommand(sql, con)
                con.Open()
                cmd.Parameters.AddWithValue("@SearchText", name)
                cmd.CommandTimeout = 90000
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    result.Add(String.Format("{0}/{1}", dr("productcode"), dr("productid")))
                End While
                Return result
                dr.Close()
                con.Close()
            End Using
        End Using
    End Function

End Class
