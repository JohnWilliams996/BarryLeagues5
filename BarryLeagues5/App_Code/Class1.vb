Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data
Imports System.Data.Sql
Imports System.Configuration
Imports System.Web
Imports System
Imports System.Data.SqlClient

Public Class Globals
    Inherits System.Web.UI.Page



    Public objDataReader As OleDbDataReader
    Public objConnection As OleDbConnection
    Public CurrentSchema As String
    Public CurrentUser As String
    Public CurrentLogin As String
    Public LeagueSelected As String
    Public TeamSelected As String
    Public PlayerSelected As String
    Public CupSelected As String
    Public FixtureDate As String
    Public FullFixtureDate As String
    Public DateIndex As Integer
    Public AdminLogin As Boolean
    Public CurrentWebPage As String
    Public ClubsRefreshCount As Integer
    Public MensRefreshCount As Integer
    Public dt As DataTable

    Public Sub store_page(ByVal inWebPage As String, inAdmin As Boolean)

        If LiveTestFlag() = 3 Then      'only record details if on Live Website
            Dim strSQL As String
            Dim myDataReader As OleDbDataReader
            Dim UKDateTime As Date = LondonDate(DateTime.UtcNow)
            Dim UKDate As String = Format(UKDateTime.Month, "00") + "/" + Format(UKDateTime.Day, "00") + "/" + Format(UKDateTime.Year, "0000") + " "
            Dim UKTime As String = Format(UKDateTime.Hour, "00") & ":" & Format(UKDateTime.Minute, "00") & "." & Format(UKDateTime.Second, "00")
            Dim ip_address As String

            ip_address = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")

            inWebPage = Replace(inWebPage, ":80", "")
            inWebPage = Replace(inWebPage, "+", " ")
            inWebPage = Replace(inWebPage, "%20", " ")
            inWebPage = Replace(inWebPage, "%22", """")
            inWebPage = Replace(inWebPage, "&amp;", "&")

            CurrentWebPage = inWebPage

            If String.IsNullOrEmpty(ip_address) Then
                ip_address = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
            End If

            If ip_address.StartsWith("66.249") _
                OrElse ip_address.StartsWith("207.46.13") _
                OrElse ip_address.StartsWith("4.77") _
                OrElse ip_address.StartsWith("40.77") _
                OrElse ip_address.StartsWith("52.167") _
                OrElse ip_address.StartsWith("17.241") Then
                inWebPage = "GoogleBot Crawler"
            End If

            If LiveTestFlag() = 3 Then
                If ISmyIPAddress(ip_address) = True Then
                    strSQL = "INSERT INTO " + CurrentSchema + "page_visits VALUES ('" & inWebPage & "','" & ip_address & "','" & UKDate & UKTime & "',1"
                Else
                    strSQL = "INSERT INTO " + CurrentSchema + "page_visits VALUES ('" & inWebPage & "','" & ip_address & "','" & UKDate & UKTime & "',0"
                End If
            Else
                If ISmyIPAddress(ip_address) = True Then
                    strSQL = "INSERT INTO " + CurrentSchema + "page_visits VALUES ('" & inWebPage & "','" & ip_address & "',GETDATE()" & ",1"
                Else
                    strSQL = "INSERT INTO " + CurrentSchema + "page_visits VALUES ('" & inWebPage & "','" & ip_address & "',GETDATE()" & ",0"
                End If
            End If

            If inAdmin Then
                strSQL = strSQL & ",'ADMIN')"
            Else
                strSQL = strSQL & ",null)"
            End If

            myDataReader = SQLSelect(strSQL)
            close_connection()

            If InStr(UCase(inWebPage), "DEFAULT") > 0 And inWebPage <> "GoogleBot Crawler" Then
                store_stat_counter()
            End If
        End If
    End Sub

    Public Function colour_result_background(inResult As String) As System.Drawing.Color
        Select Case Strings.Left(inResult, 1)
            Case "W"
                colour_result_background = Drawing.Color.Green
            Case "L"
                colour_result_background = Drawing.Color.Red
            Case "D"
                colour_result_background = Drawing.Color.Gray
        End Select
    End Function

    Public Function colour_result_foreground(inResult As String) As System.Drawing.Color
        Select Case Strings.Left(inResult, 1)
            Case "W"
                colour_result_foreground = Drawing.Color.White
            Case "L"
                colour_result_foreground = Drawing.Color.Black
            Case "D"
                colour_result_foreground = Drawing.Color.White
        End Select
    End Function

    Private Function ISmyIPAddress(inIPAddress As String) As Boolean
        ISmyIPAddress = False
        'Dim myDataReader As oledbdatareader
        'Dim strsql As String
        'strsql = "SELECT ip_address FROM " + CurrentSchema + "my_ip_addresses WHERE ip_address = '" & inIPAddress & "'"
        'myDataReader = SQLSelect(strsql)
        'If myDataReader.HasRows Then ISmyIPAddress = True
        'close_connection()

    End Function

    Private Sub store_stat_counter()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim UKDateTime As Date = LondonDate(DateTime.UtcNow)
        Dim UKDate As String = UKDateTime.ToShortDateString
        strSQL = "EXEC " + CurrentSchema + "sp_store_stat_counter '" + CurrentUser + "','" + current_season() + "','" + UKDate + "'"
        myDataReader = SQLSelect(strSQL)
        close_connection()
    End Sub

    Public Function current_season() As String
        Return get_current_season()
    End Function

    Public Function last_season() As String
        Return get_last_season()
    End Function

    Public Function get_current_season() As String
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        get_current_season = ""
        strSQL = "SELECT current_season FROM " + CurrentSchema + "seasons"
        myDataReader = SQLSelect(strSQL)
        While myDataReader.Read()
            get_current_season = myDataReader.Item("current_season")
        End While
        close_connection()
    End Function

    Public Function get_last_season() As String
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        get_last_season = ""
        strSQL = "SELECT last_season FROM " + CurrentSchema + "seasons"
        myDataReader = SQLSelect(strSQL)
        While myDataReader.Read()
            get_last_season = myDataReader.Item("last_season")
        End While
        close_connection()

    End Function

    Public Function getNextDate() As Integer
        DateIndex = DateIndex + 1
        Return DateIndex
    End Function

    Public Function AddSuffix(ByVal num As Integer) As String
        Dim suff As String = ""
        If num < 20 Then
            Select Case num
                Case 1 : suff = "st"
                Case 2 : suff = "nd"
                Case 3 : suff = "rd"
                Case 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 : suff = "th"
            End Select
        Else
            Select Case Convert.ToString(num).Chars(Convert.ToString(num).Length - 1)
                Case "1" : suff = "st"
                Case "2" : suff = "nd"
                Case "3" : suff = "rd"
                Case Else : suff = "th"
            End Select
        End If
        AddSuffix = Convert.ToString(num) + suff
    End Function

    Public Function GetCurrentWeek() As Integer
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim tmpCurrentWeek As Integer


        GetCurrentWeek = 1

        'temcode to return week 1 before season starts
        'If LiveTestFlag() = 3 Then
        '    GetCurrentWeek = 1
        '    Return GetCurrentWeek
        'End If
        'end


        tmpCurrentWeek = -1

        strSQL = "SELECT * FROM " + CurrentSchema + "vw_weeks ORDER BY week_number"
        myDataReader = SQLSelect(strSQL)
        If Not myDataReader.HasRows Then
            Return GetCurrentWeek
            Exit Function
        End If
        While myDataReader.Read
            If DateAdd(DateInterval.Day, -6, Now) < myDataReader.Item("week_commences") And tmpCurrentWeek = -1 Then
                GetCurrentWeek = myDataReader.Item("week_number")
                Return GetCurrentWeek
            End If
        End While
        close_connection()

        If tmpCurrentWeek = -1 Then
            strSQL = "SELECT MAX(week_number) FROM " + CurrentSchema + "vw_fixtures"
            myDataReader = SQLSelect(strSQL)
            While myDataReader.Read()
                GetCurrentWeek = myDataReader.Item(0)
                Return GetCurrentWeek
            End While
            close_connection()
        End If
    End Function

    Public Function LondonDate(ByVal utcDate As Date) As Date
        Dim gmtZone As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time")
        Return TimeZoneInfo.ConvertTimeFromUtc(utcDate, gmtZone)
    End Function

    Public Function LiveTestFlag() As Integer
        Dim appSettings As NameValueCollection = ConfigurationManager.AppSettings
        LiveTestFlag = Val(appSettings(2))
    End Function

    Public Function WorkHomeFlag() As String
        Dim appSettings As NameValueCollection = ConfigurationManager.AppSettings
        WorkHomeFlag = appSettings(3)
    End Function

    Public Function SQLSelect(ByRef inSQL As String) As OleDbDataReader
        'Open connection
        objConnection = New OleDbConnection(getSQLConnectionString)
        objConnection.Open()

        'Create a command object
        Dim objCommand As New OleDbCommand(inSQL)
        objCommand.Connection = objConnection

        'Get a datareader
        objDataReader = objCommand.ExecuteReader()

        SQLSelect = objDataReader
    End Function

    Public Sub close_connection()
        'close last connection
        If Not objConnection Is Nothing AndAlso objConnection.State <> Data.ConnectionState.Closed Then
            objConnection.Close()
            objConnection.Dispose() 'This is required to mark object for immediate collection by GC.
        End If
        objConnection = Nothing 'De-instantiating the Connection Object
    End Sub

    Public Function getSQLConnectionString() As String
        Dim SQLConnectionString As String = ""
        Select Case LiveTestFlag()
            Case 3
                'web (live) connection string
                SQLConnectionString = getWebConnectionString()
            Case 1
                'home connection string
                SQLConnectionString = getLocalConnectionString()
        End Select
        'temp to point to LIVE DB
        SQLConnectionString = getWebConnectionString()
        Return SQLConnectionString
    End Function
    Private Function getLocalConnectionString() As String
        'Home (local) connection strings   
        Dim LocalDBConnection As New OleDbConnection(ConfigurationManager.ConnectionStrings("LOCAL_ConnectionString").ConnectionString)
        Return LocalDBConnection.ConnectionString
    End Function
    Private Function getWebConnectionString() As String
        'setup Web (Live) and Home (local) connection strings   
        Dim WEBConnection As New OleDbConnection(ConfigurationManager.ConnectionStrings("WEB_ConnectionString").ConnectionString)
        Return WEBConnection.ConnectionString
    End Function

    Public Sub update_fixtures_combined(ByVal inSchema As String)
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

        strSQL = "EXEC " & inSchema & ".sp_update_fixtures_combined '" & get_current_season() & "'"
        myDataReader = SQLSelect(strSQL)

    End Sub
End Class
