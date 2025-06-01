Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
'Imports MySql.Data.MySqlClient

Partial Class Team_Fixtures
    Inherits System.Web.UI.Page

    Private dt As DataTable
    Private dr As DataRow
    Private gRow As Integer
    Private objGlobals As New Globals


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("all_skit")
        objGlobals.CurrentUser = "all_skit_user"
        objGlobals.CurrentSchema = "all_skit."
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
        objGlobals.TeamSelected = Request.QueryString("Team")
        objGlobals.LeagueSelected = Request.QueryString("League")

        lblTeam.Text = "TEAM: " & objGlobals.TeamSelected
        lblLeague.Text = "LEAGUE: " & objGlobals.LeagueSelected
        Call load_options(gridOptions)
        Call load_fixtures1(gridFixtures1)

        dt = New DataTable
        dt.Columns.Add(New DataColumn("Players", GetType(System.String)))
        dt.Columns.Add(New DataColumn("In/Out", GetType(System.Int32)))

        gRow = 0

        'added 12.9.14 - show result on this page

    End Sub

    Function get_league_type(ByVal inLeague As String) As String
        get_league_type = ""
        If Left(inLeague, 4) = "MENS" Or Left(inLeague, 8) = "A ROSSER" Or Left(inLeague, 9) = "G MITCHELL" Or Left(inLeague, 7) = "ALLFORM" Or Left(inLeague, 7) = "H TOWERS" Then
            get_league_type = "M"
        End If
        If Left(inLeague, 5) = "CLUBS" Then
            get_league_type = "C"
        End If
        If Left(inLeague, 6) = "LADIES" Then
            get_league_type = "L"
        End If
    End Function

    Sub load_fixtures1(inGrid As GridView)
        Dim strSQL As String 
        Dim myDataReader As OleDbDataReader
        Dim Wk As Integer
        Dim CurrentWeek As Integer = objGlobals.GetCurrentWeek
        Dim home_venue As String = ""

        dt = New DataTable

        dt.Columns.Add(New DataColumn("Week Number", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture Calendar", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League Cup", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Team Name", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Team Name", GetType(System.String)))

        strSQL = "SELECT venue FROM all_skit.vw_teams WHERE league = '" & objGlobals.LeagueSelected & "' AND long_name = '" & objGlobals.TeamSelected & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            home_venue = myDataReader.Item("venue")
        End While
        objGlobals.close_connection()


        strSQL = "EXEC all_skit.sp_get_team_fixtures '" & objGlobals.get_current_season & "','" & get_league_type(objGlobals.LeagueSelected) & "','" & objGlobals.TeamSelected & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            dr = dt.NewRow
            Wk = myDataReader.Item("week_number")
            dr("Week Number") = Wk
            dr("Fixture Calendar") = myDataReader.Item("fixture_short_date")
            dr("League Cup") = myDataReader.Item("league")
            Select Case myDataReader.Item("home_team_name")
                Case objGlobals.TeamSelected
                    dr("Home Team Name") = myDataReader.Item("away_team_name")
                    If myDataReader.Item("venue") = home_venue Then
                        dr("Away Team Name") = "HOME"
                    Else
                        dr("Away Team Name") = myDataReader.Item("venue")
                    End If
                Case "BYE"
                    dr("Home Team Name") = "BYE WEEK"
                Case Else
                    If Left(myDataReader.Item("fixture_short_date"), 3) <> "W/C" Then
                        dr("Home Team Name") = myDataReader.Item("home_team_name")
                        dr("Away Team Name") = myDataReader.Item("venue")
                    Else
                        'open week
                        dr("Home Team Name") = myDataReader.Item("home_team_name")
                    End If
            End Select
            If myDataReader.Item("away_team_name") = "BYE" Then
                dr("Home Team Name") = "BYE WEEK"
                dr("Away Team Name") = ""
            End If
            dt.Rows.Add(dr)

        End While
        gRow = 0
        inGrid.DataSource = dt
        inGrid.DataBind()

    End Sub


    Sub load_options(ByRef inGrid As GridView)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        strSQL = "SELECT long_name,home_night,venue FROM all_skit.vw_teams WHERE league = '" & objGlobals.LeagueSelected & "' AND long_name <> 'BYE' ORDER BY long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)

        dt = New DataTable

        dt.Columns.Add(New DataColumn("Long Name", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Night", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Venue", GetType(System.String)))
        dt.Rows.Add("Team", "Home Night", "Venue")
        While myDataReader.Read()
            With inGrid
                dr = dt.NewRow
                dr("Long Name") = myDataReader.Item("long_name")
                dr("Home Night") = myDataReader.Item("home_night")
                dr("Venue") = myDataReader.Item("venue")
                dt.Rows.Add(dr)
            End With
        End While

        gRow = 0
        inGrid.DataSource = dt
        inGrid.DataBind()


    End Sub

    Sub load_players()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim PlayerCount As Integer = 0
        If Left(objGlobals.LeagueSelected, 4) <> "SNOO" Then
            dt.Rows.Add("Players")
        Else
            dt.Rows.Add("Registered Players")
        End If
        dt.Rows.Add("(Click on a Player to see his Stats)")
        dt.Rows.Add("")

        strSQL = "SELECT player FROM all_skit.vw_players WHERE league = '" & objGlobals.LeagueSelected & "' AND team = '" & objGlobals.TeamSelected & "' AND CHARINDEX( 'A N OTHER',Player) = 0  ORDER BY LEFT(Player,3)"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If myDataReader.HasRows Then
            While myDataReader.Read()
                PlayerCount = PlayerCount + 1
                dt.Rows.Add(myDataReader.Item("player"), 1)
            End While
        Else
            dt.Rows.Add("NO PLAYERS REGISTERED")
        End If
    End Sub

    Private Sub gridOptions_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridOptions.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hLink As New HyperLink
            Dim hLink2 As New HyperLink
            If dt.Rows(gRow)(0) = "Team" Then
                e.Row.Cells(0).Font.Size = 8
                e.Row.Cells(0).ForeColor = Gray
                e.Row.Cells(1).Font.Size = 8
                e.Row.Cells(1).ForeColor = Gray
                e.Row.Cells(2).Font.Size = 8
                e.Row.Cells(2).ForeColor = Gray
            Else
                e.Row.Cells(1).Font.Size = 8
                e.Row.Cells(2).Font.Size = 8
            End If
            hLink.NavigateUrl = "~/All_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(0).ToString
            If e.Row.Cells(0).Text <> "Team" Then
                hLink.Text = e.Row.Cells(0).Text
                hLink.ForeColor = Cyan
                e.Row.Cells(0).Controls.Add(hLink)


                e.Row.CssClass = "cell"
            End If
            gRow = gRow + 1
        End If
    End Sub


    Protected Sub gridFixtures1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridFixtures1.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            'If Left(dt.Rows(gRow)(2).ToString, 4) <> "OPEN" And Left(dt.Rows(gRow)(2).ToString, 3) <> "BYE" Then
            If Left(dt.Rows(gRow)(1).ToString, 3) <> "W/C" And Left(dt.Rows(gRow)(3).ToString, 3) <> "BYE" Then
                Dim hLink As New HyperLink
                hLink.NavigateUrl = "~/All_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(3).ToString
                hLink.Text = e.Row.Cells(3).Text
                hLink.ForeColor = Cyan
                e.Row.Cells(3).Controls.Add(hLink)
                'e.Row.CssClass = "row"
            Else
                If Left(dt.Rows(gRow)(3).ToString, 3) = "BYE" Then
                    e.Row.Cells(0).ForeColor = Gray
                    e.Row.Cells(2).ForeColor = Gray
                End If
                e.Row.Cells(1).ForeColor = Gray
                e.Row.Cells(2).ForeColor = Gray
                e.Row.Cells(3).ForeColor = Gray
            End If
            gRow = gRow + 1
        End If
    End Sub




End Class
