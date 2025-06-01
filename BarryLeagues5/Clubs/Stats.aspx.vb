Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing.Color
Imports System.Web.UI.DataVisualization.Charting
Imports System.Drawing


Partial Class Stats
    Inherits System.Web.UI.Page
    Private dt As DataTable = New DataTable
    Private dr As DataRow = Nothing
    Private gRow As Integer
    Private objGlobals As New Globals
    Private SplitRow As Integer
    Private MatchResult As String
    Private LeagueCount As Integer = 0
    Private TeamCount As Integer = 0
    Private PlayerCount As Integer = 0
    Private ChartLeague_League As String
    Private ChartLeague_Team(99) As String
    Private ChartLeague_Player(99) As String
    Private ChartTeam_League As String
    Private ChartTeam_Team(99) As String
    Private ChartTeam_Player(99) As String
    Private TotalHome As Integer = 0
    Private TotalAway As Integer = 0
    Private HomePoints As Double
    Private AwayPoints As Double
    Private HomePointsDeducted As Integer
    Private AwayPointsDeducted As Integer
    Private FixtureDate As String
    Private FixtureFullDate As String
    Private FixtureWeek As Integer
    Private FixtureLeague As String
    Private FixtureDetail As Boolean
    Private FixtureHomeTeam As String
    Private FixtureAwayTeam As String
    Private Top10 As String
    Private Result As String = ""
    Private HomeResult As String = ""
    Private ShowResults As String
    Private HighAverage As Integer
    Private LowAverage As Integer


    Private CompID As Integer


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("clubs")
        objGlobals.CurrentUser = "clubs_user"
        objGlobals.CurrentSchema = "clubs."

        objGlobals.LeagueSelected = Request.QueryString("League")
        objGlobals.TeamSelected = Request.QueryString("Team")
        objGlobals.PlayerSelected = Request.QueryString("Player")
        Top10 = Request.QueryString("Top10")
        If Top10 Is Nothing Then Top10 = "Top10"
        ShowResults = Request.QueryString("ShowResults")
        lblClickResult.Visible = False
        If ShowResults Is Nothing Then ShowResults = "False"
        If ShowResults = "True" Then lblClickResult.Visible = True
        CompID = Request.QueryString("CompID")
        If Left(objGlobals.LeagueSelected, 4) <> "SKIT" Then
            hlTop10.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=Top10&Team=" & objGlobals.TeamSelected & "&ShowResults=" & ShowResults & "&Player=" & objGlobals.PlayerSelected & "&CompID=" & CompID
            hlAll.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=All&Team=" & objGlobals.TeamSelected & "&ShowResults=" & ShowResults & "&Player=" & objGlobals.PlayerSelected & "&CompID=" & CompID
        End If
        hlTop10.Visible = False
        hlAll.Visible = False

        btnBack1.Attributes.Add("onClick", "javascript:history.back(); return false;")
        btnBack2.Attributes.Add("onClick", "javascript:history.back(); return false;")
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        If Not IsPostBack Then
            Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
            Call load_leagues()
            If objGlobals.LeagueSelected <> Nothing Then
                lblTop10.Visible = True
                If Top10 = "Top10" Then
                    If Left(objGlobals.LeagueSelected, 4) <> "SKIT" Then
                        lblTop10.Text = "TOP 10 LEAGUE PLAYERS (MIN 50% OF RESULT CARDS RETURNED TO COUNT)"
                        hlAll.Visible = True
                        hlTop10.Visible = False
                    Else
                        lblTop10.Text = "HIGHEST AVERAGE FOR ALL TEAMS (MIN 50% OF RESULT CARDS RETURNED TO COUNT)"
                    End If
                Else
                    lblTop10.Text = "ALL LEAGUE PLAYERS (MIN 50% OF RESULT CARDS RETURNED TO COUNT)"
                    hlAll.Visible = False
                    hlTop10.Visible = True
                End If
                Select Case Left(objGlobals.LeagueSelected, 4)
                    Case "CRIB"
                        If Top10 = "Top10" Then
                            Call load_CribLeagueStatsTop10()
                        Else
                            Call load_CribLeagueStatsAll()
                        End If
                        If objGlobals.PlayerSelected = Nothing Then highlight_top10_player(gridCribLeagueStats, objGlobals.TeamSelected)
                    Case "SKIT"
                        If Top10 = "Top10" Then
                            Call load_SkittlesLeagueStatsTopPlayer()
                        Else
                            Call load_SkittlesLeagueStatsAll()
                        End If
                        If objGlobals.PlayerSelected = Nothing Then highlight_top10_player(gridSkittlesLeagueStats, objGlobals.TeamSelected)
                    Case "SNOO"
                        If Top10 = "Top10" Then
                            Call load_SnookerLeagueStatsTop10()
                        Else
                            Call load_SnookerLeagueStatsAll()
                        End If
                        If objGlobals.PlayerSelected = Nothing Then highlight_top10_player(gridSnookerLeagueStats, objGlobals.TeamSelected)
                End Select
                If Top10 = "Top10" Then Call show_league_graph(Left(objGlobals.LeagueSelected, 4))
                Call load_teams()
                If objGlobals.TeamSelected <> Nothing Then
                    lblTeamRule.Visible = True
                    If ShowResults = "True" Then
                        chtTeam1.Visible = False
                        chtTeam2.Visible = True
                        hlShowResults.Visible = False
                        hlHideResults.Visible = True
                        hlHideResults.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&ShowResults=False" & "&Player=" & objGlobals.PlayerSelected
                        Call load_results()
                    Else
                        chtTeam1.Visible = True
                        chtTeam2.Visible = False
                        hlShowResults.Visible = True
                        hlHideResults.Visible = False
                        hlShowResults.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&ShowResults=True" & "&Player=" & objGlobals.PlayerSelected
                        gridResults.Visible = False
                    End If
                    Select Case Left(objGlobals.LeagueSelected, 4)
                        Case "CRIB"
                            Call load_CribTeamStats()
                            If objGlobals.PlayerSelected <> Nothing Then highlight_top10_player(gridCribLeagueStats, objGlobals.PlayerSelected)
                            highlight_top10_team(gridCribLeagueStats, objGlobals.TeamSelected)
                            highlight_team_player(gridCribTeamStats)
                        Case "SKIT"
                            Call load_SkittlesTeamStats()
                            If objGlobals.PlayerSelected <> Nothing Then highlight_top10_player(gridSkittlesLeagueStats, objGlobals.PlayerSelected)
                            highlight_top10_team(gridSkittlesLeagueStats, objGlobals.TeamSelected)
                            highlight_team_player(gridSkittlesTeamStats)
                        Case "SNOO"
                            Call load_SnookerTeamStats()
                            If objGlobals.PlayerSelected <> Nothing Then highlight_top10_player(gridSnookerLeagueStats, objGlobals.PlayerSelected)
                            highlight_top10_team(gridSnookerLeagueStats, objGlobals.TeamSelected)
                            highlight_team_player(gridSnookerTeamStats)
                    End Select
                    Call show_team_graph(Left(objGlobals.LeagueSelected, 4))
                    Call load_players()
                End If
                If objGlobals.PlayerSelected <> Nothing And InStr(objGlobals.PlayerSelected, "A N OTHER") = 0 Then
                    If CompID = 0 Then
                        lblPlayerStats.Visible = True
                        Select Case Left(objGlobals.LeagueSelected, 4)
                            Case "CRIB"
                                Call load_CribPlayerStats()
                                lblPartnerStats.Visible = True
                                Call load_CribPartnerStats()
                            Case "SKIT"
                                Call load_SkittlesPlayerStats()
                            Case "SNOO"
                                Call load_SnookerPlayerStats()
                        End Select
                        Call show_player_graph(Left(objGlobals.LeagueSelected, 4))
                    End If
                Else
                    gridCribPlayerStats.Visible = False
                End If
            End If
        End If
    End Sub

    Sub load_leagues()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        strSQL = "SELECT League FROM clubs.vw_Leagues ORDER BY 1"
        myDataReader = objGlobals.SQLSelect(strSQL)
        gRow = 0
        dt = New DataTable

        dt.Columns.Add(New DataColumn("League1", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League3", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League4", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League5", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League6", GetType(System.String)))
        dr = dt.NewRow
        While myDataReader.Read()
            LeagueCount = LeagueCount + 1
            Select Case LeagueCount
                Case 1 : dr("League1") = myDataReader.Item("league")
                Case 2 : dr("League2") = myDataReader.Item("league")
                Case 3 : dr("League3") = myDataReader.Item("league")
                Case 4 : dr("League4") = myDataReader.Item("league")
                Case 5 : dr("League5") = myDataReader.Item("league")
                Case 6 : dr("League6") = myDataReader.Item("league")
            End Select
        End While
        objGlobals.close_connection()

        dt.Rows.Add(dr)

        gridLeagues.DataSource = dt
        gridLeagues.DataBind()
        For i = LeagueCount To 5
            gridLeagues.Rows(0).Cells(i).Visible = False
        Next
    End Sub

    Sub load_teams()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        strSQL = "SELECT long_name FROM clubs.vw_teams WHERE league = '" & objGlobals.LeagueSelected & "' AND long_name <> 'BYE' ORDER BY 1"
        myDataReader = objGlobals.SQLSelect(strSQL)
        gRow = 0
        dt = New DataTable

        dt.Columns.Add(New DataColumn("Team1", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team3", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team4", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team5", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team6", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team7", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team8", GetType(System.String)))
        dr = dt.NewRow
        While myDataReader.Read()
            TeamCount = TeamCount + 1
            Select Case TeamCount
                Case 1 : dr("Team1") = myDataReader.Item("long_name")
                Case 2, 10 : dr("Team2") = myDataReader.Item("long_name")
                Case 3, 11 : dr("Team3") = myDataReader.Item("long_name")
                Case 4, 12 : dr("Team4") = myDataReader.Item("long_name")
                Case 5, 13 : dr("Team5") = myDataReader.Item("long_name")
                Case 6, 14 : dr("Team6") = myDataReader.Item("long_name")
                Case 7, 15 : dr("Team7") = myDataReader.Item("long_name")
                Case 8, 16 : dr("Team8") = myDataReader.Item("long_name")
                Case 1, 9
                    dt.Rows.Add(dr)
                    dr = dt.NewRow
                    dr("Team1") = myDataReader.Item("long_name")
            End Select
        End While
        objGlobals.close_connection()

        dt.Rows.Add(dr)

        gridTeams.DataSource = dt
        gridTeams.DataBind()
    End Sub

    Sub load_players()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        strSQL = "SELECT player FROM clubs."
        Select Case Left(objGlobals.LeagueSelected, 4)
            Case "SKIT" : strSQL = strSQL & "vw_player_stats_skittles"
            Case "CRIB" : strSQL = strSQL & "vw_player_stats_crib"
            Case "SNOO" : strSQL = strSQL & "vw_player_stats_snooker"
        End Select
        strSQL = strSQL & " WHERE league = '" & objGlobals.LeagueSelected & "' AND team = '" & objGlobals.TeamSelected & "' AND player NOT LIKE 'A N OTHER%' ORDER BY 1"
        myDataReader = objGlobals.SQLSelect(strSQL)
        gRow = 0
        dt = New DataTable

        dt.Columns.Add(New DataColumn("Player1", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player3", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player4", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player5", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player6", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player7", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player8", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player9", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player10", GetType(System.String)))
        dr = dt.NewRow
        While myDataReader.Read()
            PlayerCount = PlayerCount + 1
            Select Case PlayerCount
                Case 1 : dr("Player1") = myDataReader.Item("player")
                Case 2, 12, 22, 32 : dr("Player2") = myDataReader.Item("player")
                Case 3, 13, 23, 33 : dr("Player3") = myDataReader.Item("player")
                Case 4, 14, 24, 34 : dr("Player4") = myDataReader.Item("player")
                Case 5, 15, 25, 35 : dr("Player5") = myDataReader.Item("player")
                Case 6, 16, 26, 36 : dr("Player6") = myDataReader.Item("player")
                Case 7, 17, 27, 37 : dr("Player7") = myDataReader.Item("player")
                Case 8, 18, 28, 38 : dr("Player8") = myDataReader.Item("player")
                Case 9, 19, 29, 39 : dr("Player9") = myDataReader.Item("player")
                Case 10, 20, 30, 40 : dr("Player10") = myDataReader.Item("player")
                Case 11, 21, 31
                    dt.Rows.Add(dr)
                    dr = dt.NewRow
                    dr("Player1") = myDataReader.Item("player")
            End Select
        End While
        objGlobals.close_connection()

        dt.Rows.Add(dr)

        gridPlayers.DataSource = dt
        gridPlayers.DataBind()
    End Sub


    Sub load_CribLeagueStatsTop10()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

        strSQL = "SELECT last_six,league_pos,player,team,total_played,total_won,total_won_percent,points_won,points_per_game "
        strSQL = strSQL & "FROM clubs.vw_player_stats_crib "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "'"
        strSQL = strSQL & "AND league_pos > 0 "
        strSQL = strSQL & "AND league_pos <= 10 "
        strSQL = strSQL & "ORDER BY league_pos,total_played DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)
        dt = New DataTable

        dt.Columns.Add(New DataColumn("Last 6", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Won", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Won PC", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Points Won", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Points Per Game", GetType(System.String)))

        gRow = 0
        dr = dt.NewRow
        dr("Last 6") = "Last 6"
        dr("League Pos") = "League"
        dr("Player") = "Player"
        dr("Team") = "Team"
        dr("Total Played") = "Games"
        dr("Total Won") = "Games"
        dr("Total Won PC") = "Won"
        dr("Points Won") = "Pts"
        dr("Points Per Game") = "Pts Per"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Last 6") = "Scores"
        dr("League Pos") = "Pos"
        dr("Player") = "(Click Name for Player Stats)"
        dr("Team") = "(Click Team Name for Team Stats)"
        dr("Total Played") = "Pld"
        dr("Total Won") = "Won"
        dr("Total Won PC") = "  %"
        dr("Points Won") = "Won"
        dr("Points Per Game") = "Game"
        dt.Rows.Add(dr)

        While myDataReader.Read()
            dr = dt.NewRow
            dr("Last 6") = myDataReader.Item("last_six")
            dr("League Pos") = myDataReader.Item("league_pos")
            dr("Player") = myDataReader.Item("player")
            dr("Team") = myDataReader.Item("team")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Won") = myDataReader.Item("total_won")
            dr("Total Won PC") = myDataReader.Item("total_won_percent")
            dr("Points Won") = myDataReader.Item("points_won")
            dr("Points Per Game") = myDataReader.Item("points_per_game")
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

        gridCribLeagueStats.DataSource = dt
        gridCribLeagueStats.DataBind()

        'check for Tied positions
        With gridCribLeagueStats
            For i = .Rows.Count - 1 To 3 Step -1
                If .Rows(i).Cells(8).Text = .Rows(i - 1).Cells(8).Text Then
                    If Left(.Rows(i - 1).Cells(1).Text, 1) <> "T" Then .Rows(i - 1).Cells(1).Text = "T" + .Rows(i - 1).Cells(1).Text
                    If Left(.Rows(i).Cells(1).Text, 1) <> "T" Then .Rows(i).Cells(1).Text = "T" + .Rows(i).Cells(1).Text
                End If
            Next
        End With

        gridTeams.Focus()
    End Sub

    Sub load_CribLeagueStatsAll()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

        strSQL = "SELECT last_six,league_pos,player,team,total_played,total_won,total_won_percent,points_won,points_per_game "
        strSQL = strSQL & "FROM clubs.vw_player_stats_crib "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "'"
        strSQL = strSQL & "AND league_pos > 0 "
        strSQL = strSQL & "ORDER BY league_pos,total_played DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)
        dt = New DataTable

        dt.Columns.Add(New DataColumn("Last 6", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Won", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Won PC", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Points Won", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Points Per Game", GetType(System.String)))

        gRow = 0
        dr = dt.NewRow
        dr("Last 6") = "Last 6"
        dr("League Pos") = "League"
        dr("Player") = "Player"
        dr("Team") = "Team"
        dr("Total Played") = "Games"
        dr("Total Won") = "Games"
        dr("Total Won PC") = "Won"
        dr("Points Won") = "Pts"
        dr("Points Per Game") = "Pts Per"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Last 6") = "Scores"
        dr("League Pos") = "Pos"
        dr("Player") = "(Click Name for Player Stats)"
        dr("Team") = "(Click Team Name for Team Stats)"
        dr("Total Played") = "Pld"
        dr("Total Won") = "Won"
        dr("Total Won PC") = "  %"
        dr("Points Won") = "Won"
        dr("Points Per Game") = "Game"
        dt.Rows.Add(dr)

        SplitRow = 1
        While myDataReader.Read()
            dr = dt.NewRow
            dr("Last 6") = myDataReader.Item("last_six")
            dr("League Pos") = myDataReader.Item("league_pos")
            dr("Player") = myDataReader.Item("player")
            dr("Team") = myDataReader.Item("team")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Won") = myDataReader.Item("total_won")
            dr("Total Won PC") = myDataReader.Item("total_won_percent")
            dr("Points Won") = myDataReader.Item("points_won")
            dr("Points Per Game") = myDataReader.Item("points_per_game")
            SplitRow = SplitRow + 1
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

        strSQL = "SELECT last_six,league_pos,player,team,total_played,total_won,total_won_percent,points_won,points_per_game "
        strSQL = strSQL & "FROM clubs.vw_player_stats_crib "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "'"
        strSQL = strSQL & "AND league_pos < 0 "
        strSQL = strSQL & "ORDER BY league_pos DESC,total_played DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)

        If myDataReader.HasRows Then
            SplitRow = SplitRow + 1
            dr = dt.NewRow
            dr("Last 6") = "OTHER PLAYERS (PLAYED LESS THAN 50% OF CARDS RETURNED)"
            dt.Rows.Add(dr)
        Else
            SplitRow = 0
        End If

        While myDataReader.Read()
            dr = dt.NewRow
            dr("Last 6") = myDataReader.Item("last_six")
            dr("League Pos") = myDataReader.Item("league_pos") * -1
            dr("Player") = myDataReader.Item("player")
            dr("Team") = myDataReader.Item("team")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Won") = myDataReader.Item("total_won")
            dr("Total Won PC") = myDataReader.Item("total_won_percent")
            dr("Points Won") = myDataReader.Item("points_won")
            dr("Points Per Game") = myDataReader.Item("points_per_game")
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

        gridCribLeagueStats.DataSource = dt
        gridCribLeagueStats.DataBind()

        If SplitRow > 0 Then
            For i = 1 To gridCribLeagueStats.Columns.Count - 1
                gridCribLeagueStats.Rows(SplitRow).Cells(i).Visible = False
            Next
            With gridCribLeagueStats.Rows(SplitRow).Cells(0)
                .HorizontalAlign = HorizontalAlign.Left
                .ForeColor = lblTeamRule.ForeColor
                .Font.Size = 12
                .ColumnSpan = gridCribLeagueStats.Columns.Count
                .BackColor = gridCribLeagueStats.BackColor
            End With
        End If

        'check for Tied positions
        With gridCribLeagueStats
            For i = .Rows.Count - 1 To 3 Step -1
                If .Rows(i).Cells(8).Text = .Rows(i - 1).Cells(8).Text Then
                    If Left(.Rows(i - 1).Cells(1).Text, 1) <> "T" Then .Rows(i - 1).Cells(1).Text = "T" + .Rows(i - 1).Cells(1).Text
                    If Left(.Rows(i).Cells(1).Text, 1) <> "T" Then .Rows(i).Cells(1).Text = "T" + .Rows(i).Cells(1).Text
                End If
            Next
        End With

        gridTeams.Focus()
    End Sub

    Sub load_SkittlesLeagueStatsTopPlayer()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        strSQL = "SELECT last_six,league_pos,team,player,total_played,total_score,average "
        strSQL = strSQL & "FROM clubs.vw_player_stats_skittles "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "'"
        'strSQL = strSQL & "AND league_pos > 0 "
        'strSQL = strSQL & "AND league_pos <= 10 "
        'strSQL = strSQL & "ORDER BY league_pos,total_played DESC"
        strSQL = strSQL & "AND team_pos = 1 "
        strSQL = strSQL & "ORDER BY team,player"
        myDataReader = objGlobals.SQLSelect(strSQL)
        gRow = 0
        dt = New DataTable

        dt.Columns.Add(New DataColumn("Last 6", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Pins", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Average", GetType(System.String)))

        dr = dt.NewRow
        dr("Last 6") = "Last 6"
        dr("League Pos") = "League"
        dr("Player") = "Player"
        dr("Team") = "Team"
        dr("Total Played") = "Total"
        dr("Total Pins") = "Total"
        dr("Average") = "Average"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Last 6") = "Scores"
        dr("League Pos") = "Pos"
        dr("Player") = "(Click Name for Player Stats)"
        dr("Team") = "(Click Team Name for Team Stats)"
        dr("Total Played") = "Pld"
        dr("Total Pins") = "Pins"
        dt.Rows.Add(dr)

        SplitRow = 1
        HighAverage = 0
        LowAverage = 99999
        While myDataReader.Read()
            dr = dt.NewRow
            dr("Last 6") = myDataReader.Item("last_six")
            dr("League Pos") = myDataReader.Item("league_pos")
            If position_count(objGlobals.LeagueSelected, myDataReader.Item("league_pos")) > 1 Then dr("League Pos") = "T" & myDataReader.Item("league_pos")
            dr("Player") = myDataReader.Item("player")
            dr("Team") = myDataReader.Item("team")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Pins") = myDataReader.Item("total_score")
            dr("Average") = myDataReader.Item("average")
            SplitRow = SplitRow + 1
            dt.Rows.Add(dr)

            If myDataReader.Item("average") * 100 < LowAverage Then LowAverage = myDataReader.Item("average") * 100
            If myDataReader.Item("average") * 100 > HighAverage Then HighAverage = myDataReader.Item("average") * 100
        End While
        objGlobals.close_connection()

        gridSkittlesLeagueStats.DataSource = dt
        gridSkittlesLeagueStats.DataBind()

        gridTeams.Focus()
    End Sub

    Sub load_SkittlesLeagueStatsAll()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        strSQL = "SELECT last_six,league_pos,player,team,total_played,total_score,average "
        strSQL = strSQL & "FROM clubs.vw_player_stats_skittles "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "'"
        strSQL = strSQL & "AND league_pos > 0 "
        strSQL = strSQL & "ORDER BY league_pos,total_played DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)
        gRow = 0
        dt = New DataTable

        dt.Columns.Add(New DataColumn("Last 6", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Pins", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Average", GetType(System.String)))

        dr = dt.NewRow
        dr("Last 6") = "Last 6"
        dr("League Pos") = "League"
        dr("Player") = "Player"
        dr("Team") = "Team"
        dr("Total Played") = "Total"
        dr("Total Pins") = "Total"
        dr("Average") = "Average"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Last 6") = "Scores"
        dr("League Pos") = "Pos"
        dr("Player") = "(Click Name for Player Stats)"
        dr("Team") = "(Click Team Name for Team Stats)"
        dr("Total Played") = "Pld"
        dr("Total Pins") = "Pins"
        dt.Rows.Add(dr)

        SplitRow = 1
        While myDataReader.Read()
            dr = dt.NewRow
            dr("Last 6") = myDataReader.Item("last_six")
            dr("League Pos") = myDataReader.Item("league_pos")
            dr("Player") = myDataReader.Item("player")
            dr("Team") = myDataReader.Item("team")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Pins") = myDataReader.Item("total_score")
            dr("Average") = myDataReader.Item("average")
            SplitRow = SplitRow + 1
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

        strSQL = "SELECT last_six,league_pos,player,team,total_played,total_score,average "
        strSQL = strSQL & "FROM clubs.vw_player_stats_skittles "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "'"
        strSQL = strSQL & "AND league_pos < 0 "
        strSQL = strSQL & "ORDER BY league_pos DESC,total_played DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)

        If myDataReader.HasRows Then
            SplitRow = SplitRow + 1
            dr = dt.NewRow
            dr("Last 6") = "OTHER PLAYERS (PLAYED LESS THAN 50% OF CARDS RETURNED)"
            dt.Rows.Add(dr)
        Else
            SplitRow = 0
        End If

        While myDataReader.Read()
            dr = dt.NewRow
            dr("Last 6") = myDataReader.Item("last_six")
            dr("League Pos") = myDataReader.Item("league_pos") * -1
            dr("Player") = myDataReader.Item("player")
            dr("Team") = myDataReader.Item("team")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Pins") = myDataReader.Item("total_score")
            dr("Average") = myDataReader.Item("average")
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

        gridSkittlesLeagueStats.DataSource = dt
        gridSkittlesLeagueStats.DataBind()

        If SplitRow > 0 Then
            For i = 1 To gridSkittlesLeagueStats.Columns.Count - 1
                gridSkittlesLeagueStats.Rows(SplitRow).Cells(i).Visible = False
            Next
            With gridSkittlesLeagueStats.Rows(SplitRow).Cells(0)
                .HorizontalAlign = HorizontalAlign.Left
                .ForeColor = lblTeamRule.ForeColor
                .Font.Size = 12
                .ColumnSpan = gridSkittlesLeagueStats.Columns.Count
                .BackColor = gridSkittlesLeagueStats.BackColor
            End With
        End If

        'check for Tied positions
        With gridSkittlesLeagueStats
            For i = .Rows.Count - 1 To 3 Step -1
                If .Rows(i).Cells(6).Text = .Rows(i - 1).Cells(6).Text Then
                    If Left(.Rows(i - 1).Cells(1).Text, 1) <> "T" Then .Rows(i - 1).Cells(1).Text = "T" + .Rows(i - 1).Cells(1).Text
                    If Left(.Rows(i).Cells(1).Text, 1) <> "T" Then .Rows(i).Cells(1).Text = "T" + .Rows(i).Cells(1).Text
                End If
            Next
        End With

        gridTeams.Focus()
    End Sub

    Sub load_SnookerLeagueStatsTop10()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        strSQL = "SELECT last_six,league_pos,player,team,total_played,total_won,total_won_percent "
        strSQL = strSQL & "FROM clubs.vw_player_stats_snooker "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "'"
        strSQL = strSQL & "AND league_pos > 0 "
        strSQL = strSQL & "AND league_pos <= 10 "
        strSQL = strSQL & "ORDER BY league_pos,total_played DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)
        gRow = 0
        dt = New DataTable

        dt.Columns.Add(New DataColumn("Last 6", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Won", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Won PC", GetType(System.String)))

        dr = dt.NewRow
        dr("Last 6") = "Last 6"
        dr("League Pos") = "League"
        dr("Player") = "Player"
        dr("Team") = "Team"
        dr("Total Played") = "Total"
        dr("Total Won") = "Total"
        dr("Total Won PC") = "Won"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Last 6") = "Results"
        dr("League Pos") = "Pos"
        dr("Player") = "(Click Name for Player Stats)"
        dr("Team") = "(Click Team Name for Team Stats)"
        dr("Total Played") = "Pld"
        dr("Total Won") = "Won"
        dr("Total Won PC") = "%"
        dt.Rows.Add(dr)


        While myDataReader.Read()
            dr = dt.NewRow
            dr("Last 6") = myDataReader.Item("last_six")
            dr("League Pos") = myDataReader.Item("league_pos")
            dr("Player") = myDataReader.Item("player")
            dr("Team") = myDataReader.Item("team")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Won") = myDataReader.Item("total_won")
            dr("Total Won PC") = myDataReader.Item("total_won_percent")
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

        gridSnookerLeagueStats.DataSource = dt
        gridSnookerLeagueStats.DataBind()

        'check for Tied positions
        With gridSnookerLeagueStats
            For i = .Rows.Count - 1 To 3 Step -1
                If .Rows(i).Cells(6).Text = .Rows(i - 1).Cells(6).Text Then
                    If Left(.Rows(i - 1).Cells(1).Text, 1) <> "T" Then .Rows(i - 1).Cells(1).Text = "T" + .Rows(i - 1).Cells(1).Text
                    If Left(.Rows(i).Cells(1).Text, 1) <> "T" Then .Rows(i).Cells(1).Text = "T" + .Rows(i).Cells(1).Text
                End If
            Next
        End With

        gridTeams.Focus()
    End Sub

    Sub load_SnookerLeagueStatsAll()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        strSQL = "SELECT last_six,league_pos,player,team,total_played,total_won,total_won_percent "
        strSQL = strSQL & "FROM clubs.vw_player_stats_snooker "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "'"
        strSQL = strSQL & "AND league_pos > 0 "
        strSQL = strSQL & "ORDER BY league_pos,total_played DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)
        gRow = 0
        dt = New DataTable

        dt.Columns.Add(New DataColumn("Last 6", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Won", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Won PC", GetType(System.String)))

        dr = dt.NewRow
        dr("Last 6") = "Last 6"
        dr("League Pos") = "League"
        dr("Player") = "Player"
        dr("Team") = "Team"
        dr("Total Played") = "Total"
        dr("Total Won") = "Total"
        dr("Total Won PC") = "Won"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Last 6") = "Results"
        dr("League Pos") = "Pos"
        dr("Player") = "(Click Name for Player Stats)"
        dr("Team") = "(Click Team Name for Team Stats)"
        dr("Total Played") = "Pld"
        dr("Total Won") = "Won"
        dr("Total Won PC") = "%"
        dt.Rows.Add(dr)

        SplitRow = 1
        While myDataReader.Read()
            dr = dt.NewRow
            dr("Last 6") = myDataReader.Item("last_six")
            dr("League Pos") = myDataReader.Item("league_pos")
            dr("Player") = myDataReader.Item("player")
            dr("Team") = myDataReader.Item("team")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Won") = myDataReader.Item("total_won")
            dr("Total Won PC") = myDataReader.Item("total_won_percent")
            SplitRow = SplitRow + 1
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

        strSQL = "SELECT last_six,league_pos,player,team,total_played,total_won,total_won_percent "
        strSQL = strSQL & "FROM clubs.vw_player_stats_snooker "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "'"
        strSQL = strSQL & "AND league_pos < 0 "
        strSQL = strSQL & "ORDER BY league_pos DESC,total_played DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)

        If myDataReader.HasRows Then
            SplitRow = SplitRow + 1
            dr = dt.NewRow
            dr("Last 6") = "OTHER PLAYERS (PLAYED LESS THAN 50% OF CARDS RETURNED)"
            dt.Rows.Add(dr)
        Else
            SplitRow = 0
        End If

        While myDataReader.Read()
            dr = dt.NewRow
            dr("Last 6") = myDataReader.Item("last_six")
            dr("League Pos") = myDataReader.Item("league_pos") * -1
            dr("Player") = myDataReader.Item("player")
            dr("Team") = myDataReader.Item("team")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Won") = myDataReader.Item("total_won")
            dr("Total Won PC") = myDataReader.Item("total_won_percent")
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

        gridSnookerLeagueStats.DataSource = dt
        gridSnookerLeagueStats.DataBind()

        If SplitRow > 0 Then
            For i = 1 To gridSnookerLeagueStats.Columns.Count - 1
                gridSnookerLeagueStats.Rows(SplitRow).Cells(i).Visible = False
            Next
            With gridSnookerLeagueStats.Rows(SplitRow).Cells(0)
                .HorizontalAlign = HorizontalAlign.Left
                .ForeColor = lblTeamRule.ForeColor
                .Font.Size = 12
                .ColumnSpan = gridSnookerLeagueStats.Columns.Count
                .BackColor = gridSnookerLeagueStats.BackColor
            End With
        End If

        'check for Tied positions
        With gridSnookerLeagueStats
            For i = .Rows.Count - 1 To 3 Step -1
                If .Rows(i).Cells(6).Text = .Rows(i - 1).Cells(6).Text Then
                    If Left(.Rows(i - 1).Cells(1).Text, 1) <> "T" Then .Rows(i - 1).Cells(1).Text = "T" + .Rows(i - 1).Cells(1).Text
                    If Left(.Rows(i).Cells(1).Text, 1) <> "T" Then .Rows(i).Cells(1).Text = "T" + .Rows(i).Cells(1).Text
                End If
            Next
        End With

        gridTeams.Focus()
    End Sub

    Sub load_CribTeamStats()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim SplitGames As Integer

        dt = New DataTable

        dt.Columns.Add(New DataColumn("Last 6", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Won", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Won PC", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Points Won", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Points Per Game", GetType(System.String)))


        dr = dt.NewRow
        dr("Last 6") = "Last 6"
        dr("League Pos") = "League"
        dr("Team Pos") = "Team"
        dr("Player") = "Player"
        dr("Total Played") = "Games"
        dr("Total Won") = "Games"
        dr("Total Won PC") = "Won"
        dr("Points Won") = "Pts"
        dr("Points Per Game") = "Pts Per"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Last 6") = "Scores"
        dr("League Pos") = "Pos"
        dr("Team Pos") = "Pos"
        dr("Player") = "(Click Name for Player Stats)"
        dr("Total Played") = "Pld"
        dr("Total Won") = "Won"
        dr("Total Won PC") = "%"
        dr("Points Won") = "Won"
        dr("Points Per Game") = "Game"
        dt.Rows.Add(dr)

        SplitRow = 1
        gRow = 0
        strSQL = "SELECT last_six,league_pos,team_pos,total_team_played,player,total_played,total_won,total_won_percent,points_won,points_per_game "
        strSQL = strSQL & "FROM clubs.vw_player_stats_crib "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "' "
        strSQL = strSQL & "AND team = '" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & "AND league_pos > 0 "
        strSQL = strSQL & "AND player NOT LIKE 'A N OTHER%' "
        strSQL = strSQL & "ORDER BY league_pos,total_played DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            If dt.Rows.Count = 2 Then
                SplitGames = Int((myDataReader.Item("total_team_played") / 2) + 0.5)
                lblTeamRule.Text = objGlobals.TeamSelected & " STATS (" & CStr(myDataReader.Item("total_team_played")) & " CARDS RETURNED, " & CStr(SplitGames) & " + (50%) GAMES PLAYED TO COUNT)"
            End If
            dr = dt.NewRow
            dr("Last 6") = myDataReader.Item("last_six")
            dr("League Pos") = myDataReader.Item("league_pos")
            If position_count(objGlobals.LeagueSelected, myDataReader.Item("league_pos")) > 1 Then dr("League Pos") = "T" & myDataReader.Item("league_pos")
            dr("Team Pos") = myDataReader.Item("team_pos")
            dr("Player") = myDataReader.Item("player")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Won") = myDataReader.Item("total_won")
            dr("Total Won PC") = myDataReader.Item("total_won_percent")
            dr("Points Won") = myDataReader.Item("points_won")
            dr("Points Per Game") = myDataReader.Item("points_per_game")
            dt.Rows.Add(dr)
            SplitRow = SplitRow + 1
        End While
        objGlobals.close_connection()

        strSQL = "SELECT last_six,league_pos,team_pos,total_team_played,player,total_played,total_won,total_won_percent,points_won,points_per_game "
        strSQL = strSQL & "FROM clubs.vw_player_stats_crib "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "' "
        strSQL = strSQL & "AND team = '" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & "AND team_pos < 0 "
        strSQL = strSQL & "AND player NOT LIKE 'A N OTHER%' "
        strSQL = strSQL & "ORDER BY team_pos DESC,total_played DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If myDataReader.HasRows Then
            SplitRow = SplitRow + 1
            dr = dt.NewRow
            dr("Last 6") = "OTHER PLAYERS (PLAYED LESS THAN " & SplitGames & " GAMES)"
            dt.Rows.Add(dr)
        Else
            SplitRow = 0
        End If

        While myDataReader.Read()
            dr = dt.NewRow
            dr("Last 6") = myDataReader.Item("last_six")
            dr("League Pos") = myDataReader.Item("league_pos") * -1
            If position_count(objGlobals.LeagueSelected, myDataReader.Item("league_pos")) > 1 Then dr("League Pos") = "T" & (myDataReader.Item("league_pos") * -1)
            dr("Team Pos") = myDataReader.Item("team_pos") * -1
            dr("Player") = myDataReader.Item("player")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Won") = myDataReader.Item("total_won")
            dr("Total Won PC") = myDataReader.Item("total_won_percent")
            dr("Points Won") = myDataReader.Item("points_won")
            dr("Points Per Game") = myDataReader.Item("points_per_game")
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

        gridCribTeamStats.DataSource = dt
        gridCribTeamStats.DataBind()

        If SplitRow > 0 Then
            For i = 1 To gridCribTeamStats.Columns.Count - 1
                gridCribTeamStats.Rows(SplitRow).Cells(i).Visible = False
            Next
            With gridCribTeamStats.Rows(SplitRow).Cells(0)
                .HorizontalAlign = HorizontalAlign.Left
                .ForeColor = lblTeamRule.ForeColor
                .Font.Size = 12
                .ColumnSpan = gridCribTeamStats.Columns.Count
                .BackColor = gridCribTeamStats.BackColor
            End With
        End If

        'check for Tied team positions
        With gridCribTeamStats
            For i = .Rows.Count - 1 To 3 Step -1
                If .Rows(i).Cells(8).Text = .Rows(i - 1).Cells(8).Text Then
                    If Left(.Rows(i - 1).Cells(2).Text, 1) <> "T" Then .Rows(i - 1).Cells(2).Text = "T" + .Rows(i - 1).Cells(2).Text
                    If Left(.Rows(i).Cells(2).Text, 1) <> "T" Then .Rows(i).Cells(2).Text = "T" + .Rows(i).Cells(2).Text
                End If
            Next
        End With

        gridPlayers.Focus()
    End Sub

    Sub show_league_graph(inLeague As String)
        Dim LeagueSeries = chtLeague.Series("League")
        LeagueSeries.IsValueShownAsLabel = True
        LeagueSeries.LabelForeColor = White
        'LeagueSeries.Font.Size = New System.Drawing.Font.size("8")
        LeagueSeries.CustomProperties = "BarLabelStyle=Right"
        With chtLeague
            .Visible = True
            .ChartAreas(0).AxisX.LabelStyle.ForeColor = White
            .ChartAreas(0).AxisY.LabelStyle.ForeColor = White
            '.ChartAreas(0).AxisX.Title = "Player"
            .ChartAreas(0).AxisX.TitleForeColor = LightGreen
            .ChartAreas(0).AxisY.TitleForeColor = LightGreen
            .ChartAreas(0).AxisX.Interval = 1
            .ChartAreas(0).AxisX.MajorGrid.Enabled = False
            .ChartAreas(0).AxisY.MajorGrid.Enabled = False
            .ChartAreas(0).AxisX.LabelStyle.Font = New System.Drawing.Font("Arial", 9)
            .ChartAreas(0).AxisY.LabelStyle.Font = New System.Drawing.Font("Arial", 9)
            .ChartAreas(0).BackColor = DarkGray
            '.ChartAreas(0).Area3DStyle.Inclination = 5
            '.ChartAreas(0).Area3DStyle.Perspective = 5
            Select Case inLeague
                Case "CRIB"
                    .Height = 25 * gridCribLeagueStats.Rows.Count
                    .Titles.Add("League Top 10 - Average Points Per Game")
                    .ChartAreas(0).AxisY.Title = "Average Points"
                    Dim j As Integer = 2
                    For i = gridCribLeagueStats.Rows.Count - 1 To 2 Step -1
                        If i = gridCribLeagueStats.Rows.Count - 1 Then .ChartAreas(0).AxisY.Minimum = gridCribLeagueStats.Rows(i).Cells(8).Text - 0.2
                        If i = 2 Then .ChartAreas(0).AxisY.Maximum = gridCribLeagueStats.Rows(i).Cells(8).Text
                        ChartLeague_League = objGlobals.LeagueSelected
                        ChartLeague_Team(i - 1) = Replace(gridCribLeagueStats.Rows(i).Cells(3).Text, "&quot;", Chr(34))
                        ChartLeague_Player(i - 1) = gridCribLeagueStats.Rows(i).Cells(2).Text
                        .ChartAreas(0).AxisX.CustomLabels.Add(i - 1, i - 1, gridCribLeagueStats.Rows(j).Cells(2).Text)
                        LeagueSeries.Points.AddXY(gridCribLeagueStats.Rows(i).Cells(2).Text, gridCribLeagueStats.Rows(i).Cells(8).Text)
                        j = j + 1
                    Next
                Case "SKIT"
                    .Height = 28 * gridSkittlesLeagueStats.Rows.Count
                    .Titles.Add("Highest Average for All Teams in Team Order")
                    .ChartAreas(0).AxisY.Title = "Average"
                    Dim j As Integer = 2
                    For i = gridSkittlesLeagueStats.Rows.Count - 1 To 2 Step -1
                        If i = gridSkittlesLeagueStats.Rows.Count - 1 Then .ChartAreas(0).AxisY.Minimum = gridSkittlesLeagueStats.Rows(i).Cells(6).Text
                        If i = 2 Then .ChartAreas(0).AxisY.Maximum = gridSkittlesLeagueStats.Rows(i).Cells(6).Text
                        ChartLeague_League = objGlobals.LeagueSelected
                        ChartLeague_Team(i - 1) = Replace(gridSkittlesLeagueStats.Rows(i).Cells(3).Text, "&quot;", Chr(34))
                        ChartLeague_Player(i - 1) = gridSkittlesLeagueStats.Rows(i).Cells(2).Text
                        .ChartAreas(0).AxisX.CustomLabels.Add(i - 1, i - 1, gridSkittlesLeagueStats.Rows(j).Cells(2).Text)
                        LeagueSeries.Points.AddXY(gridSkittlesLeagueStats.Rows(i).Cells(2).Text, gridSkittlesLeagueStats.Rows(i).Cells(6).Text)
                        j = j + 1
                    Next
                    Dim myFont As New System.Drawing.Font("Arial", 8)
                    For Each pt As DataPoint In LeagueSeries.Points
                        pt.Font = myFont
                    Next
                    .ChartAreas(0).AxisY.Minimum = (LowAverage / 100) - 0.4
                    .ChartAreas(0).AxisY.Maximum = (HighAverage / 100) + 0.4
                Case "SNOO"
                    .Height = 25 * gridSnookerLeagueStats.Rows.Count
                    .Titles.Add("League Top 10 - Total Win %")
                    .ChartAreas(0).AxisY.Title = "Total Win %"
                    Dim j As Integer = 2
                    For i = gridSnookerLeagueStats.Rows.Count - 1 To 2 Step -1
                        If i = gridSnookerLeagueStats.Rows.Count - 1 Then .ChartAreas(0).AxisY.Minimum = gridSnookerLeagueStats.Rows(i).Cells(6).Text - 5
                        If i = 2 Then .ChartAreas(0).AxisY.Maximum = gridSnookerLeagueStats.Rows(i).Cells(6).Text
                        ChartLeague_League = objGlobals.LeagueSelected
                        ChartLeague_Team(i - 1) = Replace(gridSnookerLeagueStats.Rows(i).Cells(3).Text, "&quot;", Chr(34))
                        ChartLeague_Player(i - 1) = gridSnookerLeagueStats.Rows(i).Cells(2).Text
                        .ChartAreas(0).AxisX.CustomLabels.Add(i - 1, i - 1, gridSnookerLeagueStats.Rows(j).Cells(2).Text)
                        LeagueSeries.Points.AddXY(gridSnookerLeagueStats.Rows(i).Cells(1).Text, gridSnookerLeagueStats.Rows(i).Cells(6).Text)
                        j = j + 1
                    Next
            End Select
            '.Series("League").Sort(pointSortOrder:=0)       ' descending sort
            .Titles.Item(0).ForeColor = LightGreen
            .Titles.Item(0).Font = New System.Drawing.Font("Arial", 12)
        End With
        gridPlayers.Focus()
    End Sub

    Sub show_team_graph(inLeague As String)
        Dim TeamSeries = chtTeam1.Series("Team")
        TeamSeries.IsValueShownAsLabel = True
        TeamSeries.LabelForeColor = White
        TeamSeries.CustomProperties = "BarLabelStyle=Right"
        With chtTeam1
            .ChartAreas(0).AxisX.LabelStyle.ForeColor = White
            .ChartAreas(0).AxisY.LabelStyle.ForeColor = White
            '.ChartAreas(0).AxisX.Title = "Player"
            .ChartAreas(0).AxisX.TitleForeColor = LightGreen
            .ChartAreas(0).AxisY.TitleForeColor = LightGreen
            .ChartAreas(0).AxisX.Interval = 1
            .ChartAreas(0).AxisX.MajorGrid.Enabled = False
            .ChartAreas(0).AxisY.MajorGrid.Enabled = False
            .ChartAreas(0).AxisX.LabelStyle.Font = New System.Drawing.Font("Arial", 9)
            .ChartAreas(0).AxisY.LabelStyle.Font = New System.Drawing.Font("Arial", 9)
            .ChartAreas(0).BackColor = DarkGray
            Select Case inLeague
                Case "CRIB"
                    .Height = 25 * gridCribTeamStats.Rows.Count
                    .Titles.Add("Average Points Per Game - " & objGlobals.TeamSelected)
                    .ChartAreas(0).AxisY.Title = "Average Points"
                    Dim j As Integer = 2
                    Dim MinPoints As Double = 99.9
                    For i = gridCribTeamStats.Rows.Count - 1 To 2 Step -1
                        If IsNumeric(gridCribTeamStats.Rows(i).Cells(8).Text) Then
                            If gridCribTeamStats.Rows(i).Cells(8).Text < MinPoints Then MinPoints = gridCribTeamStats.Rows(i).Cells(8).Text
                        End If
                        If i = 2 Then .ChartAreas(0).AxisY.Maximum = gridCribTeamStats.Rows(i).Cells(8).Text
                        ChartTeam_League = objGlobals.LeagueSelected
                        ChartTeam_Team(i - 1) = objGlobals.TeamSelected
                        ChartTeam_Player(i - 1) = gridCribTeamStats.Rows(i).Cells(3).Text
                        .ChartAreas(0).AxisX.CustomLabels.Add(i - 1, i - 1, Replace(gridCribTeamStats.Rows(j).Cells(3).Text, "&nbsp;", ""))
                        TeamSeries.Points.AddXY(gridCribTeamStats.Rows(i).Cells(3).Text, gridCribTeamStats.Rows(i).Cells(8).Text)
                        j = j + 1
                    Next
                    .ChartAreas(0).AxisY.Minimum = MinPoints - 0.2
                    If .ChartAreas(0).AxisY.Minimum < 0 Then .ChartAreas(0).AxisY.Minimum = 0
                Case "SKIT"
                    .Height = 20 * gridSkittlesTeamStats.Rows.Count
                    .Titles.Add("Total Average - " & objGlobals.TeamSelected)
                    .ChartAreas(0).AxisY.Title = "Average"
                    Dim j As Integer = 2
                    Dim MinPoints As Double = 99.9
                    For i = gridSkittlesTeamStats.Rows.Count - 1 To 2 Step -1
                        If IsNumeric(gridSkittlesTeamStats.Rows(i).Cells(6).Text) Then
                            If gridSkittlesTeamStats.Rows(i).Cells(6).Text < MinPoints Then MinPoints = gridSkittlesTeamStats.Rows(i).Cells(6).Text
                        End If
                        If i = 2 Then .ChartAreas(0).AxisY.Maximum = gridSkittlesTeamStats.Rows(i).Cells(6).Text
                        ChartTeam_League = objGlobals.LeagueSelected
                        ChartTeam_Team(i - 1) = objGlobals.TeamSelected
                        ChartTeam_Player(i - 1) = gridSkittlesTeamStats.Rows(i).Cells(3).Text
                        .ChartAreas(0).AxisX.CustomLabels.Add(i - 1, i - 1, Replace(gridSkittlesTeamStats.Rows(j).Cells(3).Text, "&nbsp;", ""))
                        TeamSeries.Points.AddXY(gridSkittlesTeamStats.Rows(i).Cells(3).Text, gridSkittlesTeamStats.Rows(i).Cells(6).Text)
                        j = j + 1
                    Next
                    Dim myFont As New System.Drawing.Font("Arial", 8)
                    For Each pt As DataPoint In TeamSeries.Points
                        pt.Font = myFont
                    Next
                    .ChartAreas(0).AxisY.Minimum = MinPoints - 2
                    If .ChartAreas(0).AxisY.Minimum < 0 Then .ChartAreas(0).AxisY.Minimum = 0
                    .ChartAreas(0).AxisY.Minimum = Int(.ChartAreas(0).AxisY.Minimum)
                    .ChartAreas(0).AxisY.Maximum = Int(.ChartAreas(0).AxisY.Maximum)
                Case "SNOO"
                    .Height = 24 * gridSnookerTeamStats.Rows.Count
                    .Titles.Add("Total Win % - " & objGlobals.TeamSelected)
                    .ChartAreas(0).AxisY.Title = "Total Win %"
                    .ChartAreas(0).AxisY.Maximum = 100
                    Dim j As Integer = 2
                    For i = gridSnookerTeamStats.Rows.Count - 1 To 2 Step -1
                        ChartTeam_League = objGlobals.LeagueSelected
                        ChartTeam_Team(i - 1) = objGlobals.TeamSelected
                        ChartTeam_Player(i - 1) = gridSnookerTeamStats.Rows(i).Cells(2).Text
                        .ChartAreas(0).AxisX.CustomLabels.Add(i - 1, i - 1, Replace(gridSnookerTeamStats.Rows(j).Cells(3).Text, "&nbsp;", ""))
                        TeamSeries.Points.AddXY(gridSnookerTeamStats.Rows(i).Cells(3).Text, gridSnookerTeamStats.Rows(i).Cells(6).Text)
                        j = j + 1
                    Next
            End Select
            .Titles.Item(0).ForeColor = LightGreen
            .Titles.Item(0).Font = New System.Drawing.Font("Arial", 12)
        End With
        gridPlayers.Focus()

        TeamSeries = chtTeam2.Series("Team")
        TeamSeries.IsValueShownAsLabel = True
        TeamSeries.LabelForeColor = White
        TeamSeries.CustomProperties = "BarLabelStyle=Right"
        With chtTeam2
            .ChartAreas(0).AxisX.LabelStyle.ForeColor = White
            .ChartAreas(0).AxisY.LabelStyle.ForeColor = White
            '.ChartAreas(0).AxisX.Title = "Player"
            .ChartAreas(0).AxisX.TitleForeColor = LightGreen
            .ChartAreas(0).AxisY.TitleForeColor = LightGreen
            .ChartAreas(0).AxisX.Interval = 1
            .ChartAreas(0).AxisX.MajorGrid.Enabled = False
            .ChartAreas(0).AxisY.MajorGrid.Enabled = False
            .ChartAreas(0).AxisX.LabelStyle.Font = New System.Drawing.Font("Arial", 9)
            .ChartAreas(0).AxisY.LabelStyle.Font = New System.Drawing.Font("Arial", 9)
            .ChartAreas(0).BackColor = DarkGray
            Select Case inLeague
                Case "CRIB"
                    .Height = 25 * gridCribTeamStats.Rows.Count
                    .Titles.Add("Average Points Per Game - " & objGlobals.TeamSelected)
                    .ChartAreas(0).AxisY.Title = "Average Points"
                    Dim j As Integer = 2
                    Dim MinPoints As Double = 99.9
                    For i = gridCribTeamStats.Rows.Count - 1 To 2 Step -1
                        If IsNumeric(gridCribTeamStats.Rows(i).Cells(8).Text) Then
                            If gridCribTeamStats.Rows(i).Cells(8).Text < MinPoints Then MinPoints = gridCribTeamStats.Rows(i).Cells(8).Text
                        End If
                        If i = 2 Then .ChartAreas(0).AxisY.Maximum = gridCribTeamStats.Rows(i).Cells(8).Text
                        ChartTeam_League = objGlobals.LeagueSelected
                        ChartTeam_Team(i - 1) = objGlobals.TeamSelected
                        ChartTeam_Player(i - 1) = gridCribTeamStats.Rows(i).Cells(3).Text
                        .ChartAreas(0).AxisX.CustomLabels.Add(i - 1, i - 1, Replace(gridCribTeamStats.Rows(j).Cells(3).Text, "&nbsp;", ""))
                        TeamSeries.Points.AddXY(gridCribTeamStats.Rows(i).Cells(2).Text, gridCribTeamStats.Rows(i).Cells(8).Text)
                        j = j + 1
                    Next
                    .ChartAreas(0).AxisY.Minimum = MinPoints - 0.2
                    If .ChartAreas(0).AxisY.Minimum < 0 Then .ChartAreas(0).AxisY.Minimum = 0
                Case "SKIT"
                    .Height = 20 * gridSkittlesTeamStats.Rows.Count
                    .Titles.Add("Total Average - " & objGlobals.TeamSelected)
                    .ChartAreas(0).AxisY.Title = "Average"
                    Dim j As Integer = 2
                    Dim MinPoints As Double = 99.9
                    For i = gridSkittlesTeamStats.Rows.Count - 1 To 2 Step -1
                        If IsNumeric(gridSkittlesTeamStats.Rows(i).Cells(6).Text) Then
                            If gridSkittlesTeamStats.Rows(i).Cells(6).Text < MinPoints Then MinPoints = gridSkittlesTeamStats.Rows(i).Cells(6).Text
                        End If
                        If i = 2 Then .ChartAreas(0).AxisY.Maximum = gridSkittlesTeamStats.Rows(i).Cells(6).Text
                        ChartTeam_League = objGlobals.LeagueSelected
                        ChartTeam_Team(i - 1) = objGlobals.TeamSelected
                        ChartTeam_Player(i - 1) = gridSkittlesTeamStats.Rows(i).Cells(3).Text
                        .ChartAreas(0).AxisX.CustomLabels.Add(i - 1, i - 1, Replace(gridSkittlesTeamStats.Rows(j).Cells(3).Text, "&nbsp;", ""))
                        TeamSeries.Points.AddXY(gridSkittlesTeamStats.Rows(i).Cells(3).Text, gridSkittlesTeamStats.Rows(i).Cells(6).Text)
                        j = j + 1
                    Next
                    Dim myFont As New System.Drawing.Font("Arial", 8)
                    For Each pt As DataPoint In TeamSeries.Points
                        pt.Font = myFont
                    Next
                    .ChartAreas(0).AxisY.Minimum = MinPoints - 2
                    If .ChartAreas(0).AxisY.Minimum < 0 Then .ChartAreas(0).AxisY.Minimum = 0
                    .ChartAreas(0).AxisY.Minimum = Int(.ChartAreas(0).AxisY.Minimum)
                    .ChartAreas(0).AxisY.Maximum = Int(.ChartAreas(0).AxisY.Maximum)
                Case "SNOO"
                    .Height = 24 * gridSnookerTeamStats.Rows.Count
                    .Titles.Add("Total Win % - " & objGlobals.TeamSelected)
                    .ChartAreas(0).AxisY.Title = "Total Win %"
                    .ChartAreas(0).AxisY.Maximum = 100
                    Dim j As Integer = 2
                    For i = gridSnookerTeamStats.Rows.Count - 1 To 2 Step -1
                        ChartTeam_League = objGlobals.LeagueSelected
                        ChartTeam_Team(i - 1) = objGlobals.TeamSelected
                        ChartTeam_Player(i - 1) = gridSnookerTeamStats.Rows(i).Cells(2).Text
                        .ChartAreas(0).AxisX.CustomLabels.Add(i - 1, i - 1, Replace(gridSnookerTeamStats.Rows(j).Cells(3).Text, "&nbsp;", ""))
                        TeamSeries.Points.AddXY(gridSnookerTeamStats.Rows(i).Cells(3).Text, gridSnookerTeamStats.Rows(i).Cells(6).Text)
                        j = j + 1
                    Next
            End Select
            .Titles.Item(0).ForeColor = LightGreen
            .Titles.Item(0).Font = New System.Drawing.Font("Arial", 12)
        End With

    End Sub

    Sub show_player_graph(inLeague As String)
        Dim ThisResult As String
        Dim TotalWins As Integer = 0
        Dim PlayerSeries = chtPlayer.Series("Player")
        PlayerSeries.LabelForeColor = White
        With chtPlayer
            .Visible = True
            .Width = 500
            .ChartAreas(0).AxisX.LabelStyle.ForeColor = White
            .ChartAreas(0).AxisY.LabelStyle.ForeColor = White
            .ChartAreas(0).AxisX.TitleForeColor = LightGreen
            .ChartAreas(0).AxisY.TitleForeColor = LightGreen
            .ChartAreas(0).AxisX.Interval = 1
            .ChartAreas(0).Area3DStyle.Inclination = 1
            .ChartAreas(0).Area3DStyle.Perspective = 1
            .ChartAreas(0).AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet
            .ChartAreas(0).AxisX.LabelStyle.Font = New System.Drawing.Font("Arial", 9)
            .ChartAreas(0).AxisY.LabelStyle.Font = New System.Drawing.Font("Arial", 9)
            .ChartAreas(0).BackColor = DarkGray

            ' Use a horizontal annotation and a text annotation to draw the mean average (this always appears on top of other content) '
            Dim hla As New HorizontalLineAnnotation
            hla.AxisX = .ChartAreas(0).AxisX
            hla.AxisY = .ChartAreas(0).AxisY
            hla.AnchorX = 0
            hla.IsInfinitive = True
            hla.LineColor = Orange
            hla.LineWidth = 3
            hla.LineDashStyle = ChartDashStyle.Dot
            hla.ClipToChartArea = .ChartAreas(0).Name


            ' Add a label to the HorizontalLineAnnotation using a TextAnnotation '
            Dim ta As New TextAnnotation
            ta.AxisX = .ChartAreas(0).AxisX
            ta.AxisY = .ChartAreas(0).AxisY
            ta.AnchorX = 0 ' Place to far left of chart '
            ta.AnchorAlignment = Drawing.ContentAlignment.BottomLeft
            ta.ForeColor = Yellow
            ta.BackColor = Black
            ta.ClipToChartArea = .ChartAreas(0).Name

            Select Case inLeague
                Case "CRIB"
                    .Height = 300
                    .ChartAreas(0).AxisY.Interval = 1
                    .ChartAreas(0).AxisX.Title = "Week"
                    .ChartAreas(0).AxisY.Title = "Points"
                    .ChartAreas(0).AxisY.Maximum = 5
                    .Titles.Add(objGlobals.PlayerSelected & " -  Points per Game")
                    For i = 2 To gridCribPlayerStats.Rows.Count - 2
                        ThisResult = gridCribPlayerStats.Rows(i).Cells(8).Text
                        PlayerSeries.Points.AddXY(gridCribPlayerStats.Rows(i).Cells(0).Text, Mid(ThisResult, 3, 1))
                    Next
                    'get the average FROM clubs.the team grid
                    For i = 2 To gridCribTeamStats.Rows.Count - 1
                        If gridCribTeamStats.Rows(i).Cells(3).Text = objGlobals.PlayerSelected Then
                            .Titles.Add("Average : " & gridCribTeamStats.Rows(i).Cells(8).Text & " Points per Game (" & gridCribTeamStats.Rows(i).Cells(4).Text & " Games)")
                            .Titles.Item(1).ForeColor = Orange
                            .Titles.Item(1).Font = New System.Drawing.Font("Arial", 10)
                            hla.AnchorY = gridCribTeamStats.Rows(i).Cells(8).Text
                            ta.AnchorY = hla.AnchorY
                            ta.Text = "Ave : " & gridCribTeamStats.Rows(i).Cells(8).Text
                            Exit For
                        End If
                    Next
                Case "SKIT"
                    .Height = 400
                    .ChartAreas(0).AxisX.Title = "Week"
                    .ChartAreas(0).AxisY.Title = "Pins"
                    .Titles.Add(objGlobals.PlayerSelected & " - Pins per Game")
                    Dim MinPoints As Integer = 99
                    Dim MaxPoints As Integer = 0
                    For i = 1 To gridSkittlesPlayerStats.Rows.Count - 2
                        If gridSkittlesPlayerStats.Rows(i).Cells(4).Text < MinPoints Then MinPoints = gridSkittlesPlayerStats.Rows(i).Cells(4).Text
                        If gridSkittlesPlayerStats.Rows(i).Cells(4).Text > MaxPoints Then MaxPoints = gridSkittlesPlayerStats.Rows(i).Cells(4).Text
                        PlayerSeries.Points.AddXY(gridSkittlesPlayerStats.Rows(i).Cells(0).Text, gridSkittlesPlayerStats.Rows(i).Cells(4).Text)
                        'PlayerSeries.Points(i - 1)("LabelStyle=Bottom") = "True"
                    Next
                    .ChartAreas(0).AxisY.Minimum = MinPoints - 1
                    .ChartAreas(0).AxisY.Maximum = MaxPoints + 1
                    If MaxPoints - MinPoints >= 11 Then
                        .ChartAreas(0).AxisY.Interval = 2
                    Else
                        .ChartAreas(0).AxisY.Interval = 1
                    End If
                    'get the average FROM clubs.the team grid
                    For i = 2 To gridSkittlesTeamStats.Rows.Count - 1
                        If gridSkittlesTeamStats.Rows(i).Cells(3).Text = objGlobals.PlayerSelected Then
                            .Titles.Add("Average : " & gridSkittlesTeamStats.Rows(i).Cells(6).Text & " Pins per Game (" & gridSkittlesTeamStats.Rows(i).Cells(4).Text & " Games)")
                            .Titles.Item(1).ForeColor = Orange
                            .Titles.Item(1).Font = New System.Drawing.Font("Arial", 10)
                            hla.AnchorY = gridSkittlesTeamStats.Rows(i).Cells(6).Text
                            ta.AnchorY = hla.AnchorY
                            ta.Text = "Ave : " & gridSkittlesTeamStats.Rows(i).Cells(6).Text
                            Exit For
                        End If
                    Next
                    Dim myFont As New System.Drawing.Font("Arial", 8)
                    For Each pt As DataPoint In PlayerSeries.Points
                        pt.Font = myFont
                    Next
                Case "SNOO"
                    .Height = 400
                    .ChartAreas(0).AxisX.Title = "Week"
                    .ChartAreas(0).AxisY.Title = "Games Played"
                    .ChartAreas(0).AxisY.Interval = 1
                    .Titles.Add(objGlobals.PlayerSelected & " - Total Wins %")
                    For i = 1 To gridSnookerPlayerStats.Rows.Count - 2
                        If gridSnookerPlayerStats.Rows(i).Cells(5).Text = "WON" Then TotalWins = TotalWins + 1
                        PlayerSeries.Points.AddXY(gridSnookerPlayerStats.Rows(i).Cells(0).Text, TotalWins)
                    Next
                    For i = 2 To gridSnookerTeamStats.Rows.Count - 1
                        If gridSnookerTeamStats.Rows(i).Cells(3).Text = objGlobals.PlayerSelected Then
                            If gridSnookerTeamStats.Rows(i).Cells(5).Text <> "1" Then
                                .Titles.Add(gridSnookerTeamStats.Rows(i).Cells(6).Text & " % (" & gridSnookerTeamStats.Rows(i).Cells(5).Text & " wins from " & gridSnookerTeamStats.Rows(i).Cells(4).Text & ")")
                            Else
                                .Titles.Add(gridSnookerTeamStats.Rows(i).Cells(6).Text & " % (" & gridSnookerTeamStats.Rows(i).Cells(5).Text & " win from " & gridSnookerTeamStats.Rows(i).Cells(4).Text & ")")
                            End If
                            .Titles.Item(1).ForeColor = Orange
                            .Titles.Item(1).Font = New System.Drawing.Font("Arial", 10)
                            .ChartAreas(0).AxisY.Maximum = Val(gridSnookerTeamStats.Rows(i).Cells(4).Text)
                            Exit For
                        End If
                    Next i
            End Select
            chtPlayer.Annotations.Add(hla)
            chtPlayer.Annotations.Add(ta)
            .Titles.Item(0).ForeColor = LightGreen
            .Titles.Item(0).Font = New System.Drawing.Font("Arial", 12)
        End With
        'highlight player in team
        With chtTeam1
            Dim j As Integer = .ChartAreas(0).AxisX.CustomLabels.Count - 1
            For i = 0 To .ChartAreas(0).AxisX.CustomLabels.Count - 1
                If .ChartAreas(0).AxisX.CustomLabels.Item(i).Text = objGlobals.PlayerSelected Then
                    chtTeam1.ChartAreas(0).AxisX.CustomLabels.Item(i).ForeColor = Red
                    chtTeam2.ChartAreas(0).AxisX.CustomLabels.Item(i).ForeColor = Red
                    chtTeam1.Series("Team").Points.Item(j).BorderColor = DarkRed
                    chtTeam1.Series("Team").Points.Item(j).BorderWidth = 2
                    chtTeam2.Series("Team").Points.Item(j).BorderColor = DarkRed
                    chtTeam2.Series("Team").Points.Item(j).BorderWidth = 2
                    Exit For
                Else
                    j = j - 1
                End If
            Next
        End With
        'highlight player in league
        With chtLeague
            Dim j As Integer = .ChartAreas(0).AxisX.CustomLabels.Count - 1
            For i = 0 To .ChartAreas(0).AxisX.CustomLabels.Count - 1
                If .ChartAreas(0).AxisX.CustomLabels.Item(i).Text = objGlobals.PlayerSelected Then
                    chtLeague.ChartAreas(0).AxisX.CustomLabels.Item(i).ForeColor = Red
                    chtLeague.Series("League").Points.Item(j).BorderColor = DarkRed
                    chtLeague.Series("League").Points.Item(j).BorderWidth = 2
                    Exit For
                Else
                    j = j - 1
                End If
            Next
        End With
        btnBack2.Focus()
    End Sub


    Sub load_SkittlesTeamStats()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim SplitGames As Integer

        dt = New DataTable

        dt.Columns.Add(New DataColumn("Last 6", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Pins", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Average", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total HS", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Pins", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Average", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home HS", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Pins", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Average", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away HS", GetType(System.String)))
        'dt.Columns.Add(New DataColumn("Number Nines", GetType(System.String)))

        dr = dt.NewRow
        dr("Last 6") = "Last 6"
        dr("League Pos") = "League"
        dr("Team Pos") = "Team"
        dr("Player") = "Player"
        dr("Total Played") = "Total"
        dr("Total Pins") = "Total"
        dr("Total Average") = "Total"
        dr("Total HS") = "Total"
        dr("Home Played") = "Home"
        dr("Home Pins") = "Home"
        dr("Home Average") = "Home"
        dr("Home HS") = "Home"
        dr("Away Played") = "Away"
        dr("Away Pins") = "Away"
        dr("Away Average") = "Away"
        dr("Away HS") = "Away"
        'dr("Number Nines") = "No."
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Last 6") = "Scores"
        dr("League Pos") = "Pos"
        dr("Team Pos") = "Pos"
        dr("Player") = "(Click Name for Player Stats)"
        dr("Total Played") = "Played"
        dr("Total Pins") = "Pins"
        dr("Total Average") = "Average"
        dr("Total HS") = "High"
        dr("Home Played") = "Played"
        dr("Home Pins") = "Pins"
        dr("Home Average") = "Average"
        dr("Home HS") = "High"
        dr("Away Played") = "Played"
        dr("Away Pins") = "Pins"
        dr("Away Average") = "Average"
        dr("Away HS") = "High"
        'dr("Number Nines") = "9+'s"
        dt.Rows.Add(dr)

        SplitRow = 1
        gRow = 0
        strSQL = "SELECT last_six,league_pos,team_pos,total_team_played,player,total_played,total_score,average,highest_score,"
        strSQL = strSQL & "home_played,home_score,home_average,home_highest_score,"
        strSQL = strSQL & "away_played,away_score,away_average,away_highest_score,number_nines "
        strSQL = strSQL & "FROM clubs.vw_player_stats_skittles "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "' "
        strSQL = strSQL & "AND team = '" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & "AND player NOT LIKE 'A N OTHER%' "
        strSQL = strSQL & "AND team_pos > 0 "
        strSQL = strSQL & "ORDER BY team_pos,total_played DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            If dt.Rows.Count = 2 Then
                SplitGames = Int((myDataReader.Item("total_team_played") / 2) + 0.5)
                lblTeamRule.Text = objGlobals.TeamSelected & " STATS (" & CStr(myDataReader.Item("total_team_played")) & " CARDS RETURNED, " & CStr(SplitGames) & "+ GAMES PLAYED TO COUNT)"
            End If
            dr = dt.NewRow
            dr("Last 6") = myDataReader.Item("last_six")
            dr("League Pos") = myDataReader.Item("league_pos")
            If position_count(objGlobals.LeagueSelected, myDataReader.Item("league_pos")) > 1 Then dr("League Pos") = "T" & myDataReader.Item("league_pos")
            dr("Team Pos") = myDataReader.Item("team_pos")
            dr("Player") = myDataReader.Item("player")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Pins") = myDataReader.Item("total_score")
            dr("Total Average") = myDataReader.Item("average")
            dr("Total HS") = myDataReader.Item("highest_score")
            dr("Home Played") = myDataReader.Item("home_played")
            dr("Home Pins") = myDataReader.Item("home_score")
            dr("Home Average") = myDataReader.Item("home_average")
            dr("Home HS") = myDataReader.Item("home_highest_score")
            dr("Away Played") = myDataReader.Item("away_played")
            dr("Away Pins") = myDataReader.Item("away_score")
            dr("Away Average") = myDataReader.Item("away_average")
            dr("Away HS") = myDataReader.Item("away_highest_score")
            'If myDataReader.Item("number_nines") > 0 Then
            '    dr("Number Nines") = myDataReader.Item("number_nines")
            'Else
            '    dr("Number Nines") = ""
            'End If
            dt.Rows.Add(dr)
            SplitRow = SplitRow + 1
        End While
        objGlobals.close_connection()

        strSQL = "SELECT last_six,league_pos,team_pos,total_team_played,player,total_played,total_score,average,highest_score,"
        strSQL = strSQL & "home_played,home_score,home_average,home_highest_score,"
        strSQL = strSQL & "away_played,away_score,away_average,away_highest_score,number_nines "
        strSQL = strSQL & "FROM clubs.vw_player_stats_skittles "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "' "
        strSQL = strSQL & "AND team = '" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & "AND team_pos < 0 "
        strSQL = strSQL & "AND player NOT LIKE 'A N OTHER%' "
        strSQL = strSQL & "ORDER BY team_pos DESC,total_played DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If myDataReader.HasRows Then
            SplitRow = SplitRow + 1
            dr = dt.NewRow
            dr("Last 6") = "OTHER PLAYERS (PLAYED LESS THAN " & SplitGames & " GAMES)"
            dt.Rows.Add(dr)
        Else
            SplitRow = 0
        End If

        While myDataReader.Read()
            dr = dt.NewRow
            dr("Last 6") = myDataReader.Item("last_six")
            dr("League Pos") = myDataReader.Item("league_pos") * -1
            If position_count(objGlobals.LeagueSelected, myDataReader.Item("league_pos")) > 1 Then dr("League Pos") = "T" & (myDataReader.Item("league_pos") * -1)
            dr("Team Pos") = myDataReader.Item("team_pos") * -1
            dr("Player") = myDataReader.Item("player")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Pins") = myDataReader.Item("total_score")
            dr("Total Average") = myDataReader.Item("average")
            dr("Total HS") = myDataReader.Item("highest_score")
            dr("Home Played") = myDataReader.Item("home_played")
            dr("Home Pins") = myDataReader.Item("home_score")
            dr("Home Average") = myDataReader.Item("home_average")
            dr("Home HS") = myDataReader.Item("home_highest_score")
            dr("Away Played") = myDataReader.Item("away_played")
            dr("Away Pins") = myDataReader.Item("away_score")
            dr("Away Average") = myDataReader.Item("away_average")
            dr("Away HS") = myDataReader.Item("away_highest_score")
            'If myDataReader.Item("number_nines") > 0 Then
            '    dr("Number Nines") = myDataReader.Item("number_nines")
            'Else
            '    dr("Number Nines") = ""
            'End If
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

        gridSkittlesTeamStats.DataSource = dt
        gridSkittlesTeamStats.DataBind()

        If SplitRow > 0 Then
            For i = 1 To gridSkittlesTeamStats.Columns.Count - 1
                gridSkittlesTeamStats.Rows(SplitRow).Cells(i).Visible = False
            Next
            With gridSkittlesTeamStats.Rows(SplitRow).Cells(0)
                .HorizontalAlign = HorizontalAlign.Left
                .ForeColor = lblTeamRule.ForeColor
                .Font.Size = 12
                .ColumnSpan = gridSkittlesTeamStats.Columns.Count
                .BackColor = gridSkittlesTeamStats.BackColor
            End With
        End If

        'check for Tied team positions
        With gridSkittlesTeamStats
            For i = .Rows.Count - 1 To 3 Step -1
                If .Rows(i).Cells(6).Text = .Rows(i - 1).Cells(6).Text Then
                    If Left(.Rows(i - 1).Cells(2).Text, 1) <> "T" Then .Rows(i - 1).Cells(2).Text = "T" + .Rows(i - 1).Cells(2).Text
                    If Left(.Rows(i).Cells(2).Text, 1) <> "T" Then .Rows(i).Cells(2).Text = "T" + .Rows(i).Cells(2).Text
                End If
            Next
        End With

        gridPlayers.Focus()
    End Sub

    Sub load_SnookerTeamStats()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim SplitGames As Integer

        dt = New DataTable

        dt.Columns.Add(New DataColumn("Last 6", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Won", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Won PC", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Won", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Won PC", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Won", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Won PC", GetType(System.String)))

        dr = dt.NewRow
        dr("Last 6") = "Last 6"
        dr("League Pos") = "League"
        dr("Team Pos") = "Team"
        dr("Player") = "Player"
        dr("Total Played") = "Total"
        dr("Total Won") = "Total"
        dr("Total Won PC") = "Total"
        dr("Home Played") = "Home"
        dr("Home Won") = "Home"
        dr("Home Won PC") = "Home"
        dr("Away Played") = "Away"
        dr("Away Won") = "Away"
        dr("Away Won PC") = "Away"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Last 6") = "Results"
        dr("League Pos") = "Pos"
        dr("Team Pos") = "Pos"
        dr("Player") = "(Click Name for Player Stats)"
        dr("Total Played") = "Played"
        dr("Total Won") = "Won"
        dr("Total Won PC") = "Won %"
        dr("Home Played") = "Played"
        dr("Home Won") = "Won"
        dr("Home Won PC") = "Won %"
        dr("Away Played") = "Played"
        dr("Away Won") = "Won"
        dr("Away Won PC") = "Won %"
        dt.Rows.Add(dr)

        gRow = 0
        SplitRow = 1
        strSQL = "SELECT last_six,league_pos, team_pos,total_team_played,player,total_played,total_won,total_won_percent,"
        strSQL = strSQL & "home_played,home_won,home_won_percent,"
        strSQL = strSQL & "away_played,away_won,away_won_percent "
        strSQL = strSQL & "FROM clubs.vw_player_stats_snooker "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "' "
        strSQL = strSQL & "AND team = '" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & "AND team_pos > 0 "
        strSQL = strSQL & "AND player NOT LIKE 'A N OTHER%' "
        strSQL = strSQL & "ORDER BY team_pos,total_played DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            If dt.Rows.Count = 2 Then
                SplitGames = Int((myDataReader.Item("total_team_played") / 2) + 0.5)
                lblTeamRule.Text = objGlobals.TeamSelected & " STATS (" & CStr(myDataReader.Item("total_team_played")) & " CARDS RETURNED, " & CStr(SplitGames) & "+ GAMES PLAYED TO COUNT)"
            End If
            dr = dt.NewRow
            dr("Last 6") = myDataReader.Item("last_six")
            dr("League Pos") = myDataReader.Item("league_pos")
            If position_count(objGlobals.LeagueSelected, myDataReader.Item("league_pos")) > 1 Then dr("League Pos") = "T" & myDataReader.Item("league_pos")
            dr("Team Pos") = myDataReader.Item("team_pos")
            dr("Player") = myDataReader.Item("player")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Won") = myDataReader.Item("total_won")
            dr("Total Won PC") = myDataReader.Item("total_won_percent")
            dr("Home Played") = myDataReader.Item("home_played")
            dr("Home Won") = myDataReader.Item("home_won")
            dr("Home Won PC") = myDataReader.Item("home_won_percent")
            dr("Away Played") = myDataReader.Item("away_played")
            dr("Away Won") = myDataReader.Item("away_won")
            dr("Away Won PC") = myDataReader.Item("away_won_percent")
            dt.Rows.Add(dr)

            SplitRow = SplitRow + 1
        End While
        objGlobals.close_connection()

        strSQL = "SELECT last_six,league_pos,team_pos,total_team_played,player,total_played,total_won,total_won_percent,"
        strSQL = strSQL & "home_played,home_won,home_won_percent,"
        strSQL = strSQL & "away_played,away_won,away_won_percent "
        strSQL = strSQL & "FROM clubs.vw_player_stats_snooker "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "' "
        strSQL = strSQL & "AND team = '" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & "AND team_pos < 0 "
        strSQL = strSQL & "AND player NOT LIKE 'A N OTHER%' "
        strSQL = strSQL & "ORDER BY team_pos DESC,total_played DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If myDataReader.HasRows Then
            SplitRow = SplitRow + 1
            dr = dt.NewRow
            dr("Last 6") = "OTHER PLAYERS (PLAYED LESS THAN " & SplitGames & " GAMES)"
            dt.Rows.Add(dr)
        Else
            SplitRow = 0
        End If

        While myDataReader.Read()
            dr = dt.NewRow
            dr("Last 6") = myDataReader.Item("last_six")
            dr("League Pos") = myDataReader.Item("league_pos") * -1
            If position_count(objGlobals.LeagueSelected, myDataReader.Item("league_pos")) > 1 Then dr("League Pos") = "T" & (myDataReader.Item("league_pos") * -1)
            dr("Team Pos") = myDataReader.Item("team_pos") * -1
            dr("Player") = myDataReader.Item("player")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Won") = myDataReader.Item("total_won")
            dr("Total Won PC") = myDataReader.Item("total_won_percent")
            dr("Home Played") = myDataReader.Item("home_played")
            dr("Home Won") = myDataReader.Item("home_won")
            dr("Home Won PC") = myDataReader.Item("home_won_percent")
            dr("Away Played") = myDataReader.Item("away_played")
            dr("Away Won") = myDataReader.Item("away_won")
            dr("Away Won PC") = myDataReader.Item("away_won_percent")
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

        gridSnookerTeamStats.DataSource = dt
        gridSnookerTeamStats.DataBind()

        If SplitRow > 0 Then
            For i = 1 To gridSnookerTeamStats.Columns.Count - 1
                gridSnookerTeamStats.Rows(SplitRow).Cells(i).Visible = False
            Next
            With gridSnookerTeamStats.Rows(SplitRow).Cells(0)
                .HorizontalAlign = HorizontalAlign.Left
                .ForeColor = lblTeamRule.ForeColor
                .Font.Size = 12
                .ColumnSpan = gridSnookerTeamStats.Columns.Count
                .BackColor = gridSnookerTeamStats.BackColor
            End With
        End If

        'check for Tied with the team positions
        With gridSnookerTeamStats
            For i = .Rows.Count - 1 To 3 Step -1
                If .Rows(i).Cells(6).Text = .Rows(i - 1).Cells(6).Text Then
                    If Left(.Rows(i - 1).Cells(2).Text, 1) <> "T" Then .Rows(i - 1).Cells(2).Text = "T" + .Rows(i - 1).Cells(2).Text
                    If Left(.Rows(i).Cells(2).Text, 1) <> "T" Then .Rows(i).Cells(2).Text = "T" + .Rows(i).Cells(2).Text
                End If
            Next
        End With

        gridPlayers.Focus()
    End Sub

    Sub load_CribPlayerStats()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

        dt = New DataTable

        dt.Columns.Add(New DataColumn("Week", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Date", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Partner", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Opponent Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Opponent Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Opponent Partner", GetType(System.String)))
        dt.Columns.Add(New DataColumn("H/A", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Match", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture ID", GetType(System.String)))

        dr = dt.NewRow
        dr("Week") = "Week"
        dr("Date") = "Date"
        dr("Player") = "Players"
        dr("Opponent Team") = "Opponent"
        dr("Opponent Player") = "Opponent 1"
        dr("Opponent Partner") = "Opponent 2"
        dr("H/A") = "H/A"
        dr("Result") = "Result"
        dr("Match") = "Match"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Opponent Team") = "Team"
        dr("Result") = ""
        dr("Match") = ""
        dt.Rows.Add(dr)

        gRow = 0
        strSQL = "SELECT fixture_id,week_number,fixture_calendar,fixture_short_date,home_team,home_player,home_partner,home_points,"
        strSQL = strSQL & "away_team,away_player,away_partner,away_points "
        strSQL = strSQL & "FROM clubs.vw_fixtures_detail "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "' "
        strSQL = strSQL & "AND (home_team='" & objGlobals.TeamSelected & "' AND (home_player='" & objGlobals.PlayerSelected & "' OR home_partner = '" & objGlobals.PlayerSelected & "')) "
        strSQL = strSQL & "OR (League = '" & objGlobals.LeagueSelected & "' AND away_team='" & objGlobals.TeamSelected & "' AND (away_player='" & objGlobals.PlayerSelected & "' OR away_partner = '" & objGlobals.PlayerSelected & "')) "
        strSQL = strSQL & "ORDER BY week_number "
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            dr = dt.NewRow
            dr("Week") = myDataReader.Item("week_number")
            dr("Date") = myDataReader.Item("fixture_short_date")
            'dr("Date") = Format(myDataReader.Item("fixture_calendar"), "ddd dd MMM")
            dr("Player") = objGlobals.PlayerSelected
            If myDataReader.Item("home_team") = objGlobals.TeamSelected Then
                If objGlobals.PlayerSelected = myDataReader.Item("home_player") Then
                    dr("Partner") = myDataReader.Item("home_partner")
                Else
                    dr("Partner") = myDataReader.Item("home_player")
                End If
                dr("Opponent Team") = myDataReader.Item("away_team")
                dr("Opponent Player") = myDataReader.Item("away_player")
                dr("Opponent Partner") = myDataReader.Item("away_partner")
                dr("H/A") = "HOME"
                If myDataReader.Item("home_points") > myDataReader.Item("away_points") Then
                    dr("Result") = "W " & myDataReader.Item("home_points") & "-" & myDataReader.Item("away_points")
                Else
                    dr("Result") = "L " & myDataReader.Item("home_points") & "-" & myDataReader.Item("away_points")
                End If
                Call get_match_result(myDataReader.Item("fixture_id"), "Home")
            Else
                If objGlobals.PlayerSelected = myDataReader.Item("away_player") Then
                    dr("Partner") = myDataReader.Item("away_partner")
                Else
                    dr("Partner") = myDataReader.Item("away_player")
                End If
                dr("Opponent Team") = myDataReader.Item("home_team")
                dr("Opponent Player") = myDataReader.Item("home_player")
                dr("Opponent Partner") = myDataReader.Item("home_partner")
                dr("H/A") = "AWAY"
                If myDataReader.Item("home_points") > myDataReader.Item("away_points") Then
                    dr("Result") = "L " & myDataReader.Item("away_points") & "-" & myDataReader.Item("home_points")
                Else
                    dr("Result") = "W " & myDataReader.Item("away_points") & "-" & myDataReader.Item("home_points")
                End If
                Call get_match_result(myDataReader.Item("fixture_id"), "Away")
            End If

            dr("Match") = MatchResult
            dr("Fixture ID") = myDataReader.Item("fixture_id")
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

        dr = dt.NewRow
        dr("Opponent Player") = "Click on the Match Result to View the Match Card"
        dt.Rows.Add(dr)

        gridCribPlayerStats.DataSource = dt
        gridCribPlayerStats.DataBind()
        gridCribPlayerStats.Focus()

        lblPlayerStats.Text = objGlobals.PlayerSelected & " - RESULTS THIS SEASON (" & gridCribPlayerStats.Rows.Count - 3 & " GAMES PLAYED)"

    End Sub

    Sub load_CribPartnerStats()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

        Dim ThisPartner As String = ""
        Dim ThisGamesWon As Integer
        Dim ThisPointsWon As Integer
        Dim PartnerFound As Boolean
        Dim PartnerName(20) As String
        Dim PartnerCount As Integer = 0
        Dim PartnerPlayed(20) As Integer
        Dim PartnerGamesWon(20) As Integer
        Dim PartnerPointsWon(20) As Integer
        strSQL = "SELECT home_player,home_partner,home_points,away_team,away_player,away_partner,away_points "
        strSQL = strSQL & "FROM clubs.vw_fixtures_detail "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "' AND home_team='" & objGlobals.TeamSelected & "' AND (home_player='" & objGlobals.PlayerSelected & "' OR home_partner = '" & objGlobals.PlayerSelected & "') "
        strSQL = strSQL & " OR  (league = '" & objGlobals.LeagueSelected & "' AND away_team ='" & objGlobals.TeamSelected & "' AND (away_player='" & objGlobals.PlayerSelected & "' OR away_partner = '" & objGlobals.PlayerSelected & "')) "
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            ThisGamesWon = 0
            If objGlobals.PlayerSelected = myDataReader.Item("home_player") Then
                ThisPartner = myDataReader.Item("home_partner")
                If myDataReader.Item("home_points") > myDataReader.Item("away_points") Then
                    ThisGamesWon = 1
                End If
                ThisPointsWon = myDataReader.Item("home_points")
            End If

            If objGlobals.PlayerSelected = myDataReader.Item("home_partner") Then
                ThisPartner = myDataReader.Item("home_player")
                If myDataReader.Item("home_points") > myDataReader.Item("away_points") Then
                    ThisGamesWon = 1
                End If
                ThisPointsWon = myDataReader.Item("home_points")
            End If

            If objGlobals.PlayerSelected = myDataReader.Item("away_player") Then
                ThisPartner = myDataReader.Item("away_partner")
                If myDataReader.Item("away_points") > myDataReader.Item("home_points") Then
                    ThisGamesWon = 1
                End If
                ThisPointsWon = myDataReader.Item("away_points")
            End If

            If objGlobals.PlayerSelected = myDataReader.Item("away_partner") Then
                ThisPartner = myDataReader.Item("away_player")
                If myDataReader.Item("away_points") > myDataReader.Item("home_points") Then
                    ThisGamesWon = 1
                End If
                ThisPointsWon = myDataReader.Item("away_points")
            End If
            PartnerFound = False
            For i = 1 To PartnerCount
                If ThisPartner = PartnerName(i) Then
                    PartnerFound = True
                    PartnerPlayed(i) = PartnerPlayed(i) + 1
                    PartnerGamesWon(i) = PartnerGamesWon(i) + ThisGamesWon
                    PartnerPointsWon(i) = PartnerPointsWon(i) + ThisPointsWon
                End If
            Next
            If Not PartnerFound Then
                PartnerCount = PartnerCount + 1
                PartnerPlayed(PartnerCount) = 1
                PartnerName(PartnerCount) = ThisPartner
                PartnerGamesWon(PartnerCount) = ThisGamesWon
                PartnerPointsWon(PartnerCount) = ThisPointsWon
            End If
        End While
        objGlobals.close_connection()

        dt = New DataTable

        dt.Columns.Add(New DataColumn("Players", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Won", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Won PC", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Points Won", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Points Per Game", GetType(System.String)))


        dr = dt.NewRow
        dr("Players") = "Players"
        dr("Total Played") = "Games"
        dr("Total Won") = "Games"
        dr("Total Won PC") = "Won"
        dr("Points Won") = "Pts"
        dr("Points Per Game") = "Pts Per"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Total Played") = "Pld"
        dr("Total Won") = "Won"
        dr("Total Won PC") = "%"
        dr("Points Won") = "Won"
        dr("Points Per Game") = "Game"
        dt.Rows.Add(dr)

        gRow = 0
        lblPartnerStats.Text = objGlobals.PlayerSelected & " - PARTNER STATS"
        For i = 1 To PartnerCount
            dr = dt.NewRow
            dr("Players") = objGlobals.PlayerSelected + " & " + PartnerName(i)
            dr("Total Played") = PartnerPlayed(i)
            dr("Total Won") = PartnerGamesWon(i)
            dr("Total Won PC") = Format(((PartnerGamesWon(i) * 100) / PartnerPlayed(i)), "##0.0")
            dr("Points Won") = PartnerPointsWon(i)
            dr("Points Per Game") = Format((((PartnerPointsWon(i) * 100) / PartnerPlayed(i))) / 100, "##0.00")
            dt.Rows.Add(dr)
        Next

        gridCribPartnerStats.DataSource = dt
        gridCribPartnerStats.DataBind()
    End Sub

    Sub load_SkittlesPlayerStats()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        dt = New DataTable
        dt.Columns.Add(New DataColumn("Week", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Date", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Opponents", GetType(System.String)))
        dt.Columns.Add(New DataColumn("H/A", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Score", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Result", GetType(System.String)))
        'dt.Columns.Add(New DataColumn("Number Nines", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture ID", GetType(System.String)))

        dr = dt.NewRow
        dr("Week") = "Week"
        dr("Date") = "Date"
        dr("Opponents") = "Opponents"
        dr("H/A") = "H/A/N"
        dr("Score") = "Pins"
        'dr("Number Nines") = "9+'s"
        dr("Result") = "Match"
        dt.Rows.Add(dr)
        gRow = 0
        strSQL = "SELECT week_number,fixture_calendar,fixture_short_date,fixture_id,fixture_neutral,"
        strSQL = strSQL & "home_team,home_player,home_points,"
        strSQL = strSQL & "away_team,away_player,away_points,away_nines "
        strSQL = strSQL & "FROM clubs.vw_fixtures_detail "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "'"
        strSQL = strSQL & "AND (home_team='" & objGlobals.TeamSelected & "' AND home_player='" & objGlobals.PlayerSelected & "') "
        strSQL = strSQL & "OR (League = '" & objGlobals.LeagueSelected & "' AND away_team='" & objGlobals.TeamSelected & "' AND away_player='" & objGlobals.PlayerSelected & "')"
        strSQL = strSQL & "ORDER BY week_number "
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            dr = dt.NewRow
            dr("Week") = myDataReader.Item("week_number")
            dr("Date") = myDataReader.Item("fixture_short_date")
            'dr("Date") = Format(myDataReader.Item("fixture_calendar"), "ddd dd MMM")
            If myDataReader.Item("home_team") = objGlobals.TeamSelected Then
                If Not myDataReader.Item("fixture_neutral") Then
                    dr("Opponents") = myDataReader.Item("away_team")
                    dr("H/A") = "HOME"
                Else
                    dr("Opponents") = myDataReader.Item("away_team") + " (N)"
                    dr("H/A") = "HOME (N)"
                End If
                dr("Score") = myDataReader.Item("home_points")
                Call get_match_result(myDataReader.Item("fixture_id"), "Home")
            Else
                If Not myDataReader.Item("fixture_neutral") Then
                    dr("Opponents") = myDataReader.Item("home_team")
                    dr("H/A") = "AWAY"
                Else
                    dr("Opponents") = myDataReader.Item("home_team") + " (N)"
                    dr("H/A") = "AWAY (N)"
                End If
                dr("Score") = myDataReader.Item("away_points")
                Call get_match_result(myDataReader.Item("fixture_id"), "Away")
                'If Not IsDBNull(myDataReader.Item("away_nines")) Then
                '    dr("Number Nines") = myDataReader.Item("away_nines")
                'End If
                Call get_match_result(myDataReader.Item("fixture_id"), "Away")
            End If
            dr("Result") = MatchResult
            dr("Fixture ID") = myDataReader.Item("fixture_id")
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

        dr = dt.NewRow
        dr("Opponents") = "Click on the Match Result to View the Match Card"
        dt.Rows.Add(dr)

        gridSkittlesPlayerStats.DataSource = dt
        gridSkittlesPlayerStats.DataBind()
        gridSkittlesPlayerStats.Focus()

        lblPlayerStats.Text = objGlobals.PlayerSelected & " - SCORES THIS SEASON (" & gridSkittlesPlayerStats.Rows.Count - 2 & " GAMES PLAYED)"
    End Sub

    Sub highlight_top10_player(inGrid As GridView, inComparison As String)
        'highlight player if in league top 10
        inComparison = Replace(inComparison, Chr(34), "&quot;")
        With inGrid
            For irow = 2 To .Rows.Count - 1
                If .Rows(irow).Cells(2).Text = inComparison Then
                    Dim hLink As New HyperLink
                    hLink.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&Player=" & objGlobals.PlayerSelected
                    hLink.Text = inComparison
                    .Rows(irow).Cells(2).Text = inComparison
                    .Rows(irow).Cells(2).Controls.Add(hLink)
                    .Rows(irow).Cells(2).ForeColor = Black
                    .Rows(irow).Cells(2).BackColor = White
                Else
                    .Rows(irow).Cells(2).ForeColor = White
                    .Rows(irow).Cells(2).BackColor = inGrid.BackColor
                End If
            Next
        End With
    End Sub

    Sub highlight_top10_team(inGrid As GridView, inComparison As String)
        'highlight team if in league top 10
        inComparison = Replace(inComparison, Chr(34), "&quot;")
        With inGrid
            For irow = 2 To .Rows.Count - 1
                If .Rows(irow).Cells(3).Text = inComparison Then
                    Dim hLink As New HyperLink
                    hLink.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&Player=" & objGlobals.PlayerSelected
                    hLink.Text = inComparison
                    .Rows(irow).Cells(3).Text = inComparison
                    .Rows(irow).Cells(3).Controls.Add(hLink)
                    .Rows(irow).Cells(3).ForeColor = Black
                    .Rows(irow).Cells(3).BackColor = White
                    Dim TeamSeries = chtTeam1.Series("Team")
                Else
                    .Rows(irow).Cells(3).ForeColor = Cyan
                    .Rows(irow).Cells(3).BackColor = inGrid.BackColor
                End If
            Next
        End With
    End Sub

    Sub highlight_team_player(inGrid As GridView)
        'highlight player in team stats
        With inGrid
            For irow = 1 To .Rows.Count - 1
                If .Rows(irow).Cells(3).Text = objGlobals.PlayerSelected Then
                    Dim hLink As New HyperLink
                    hLink.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&Player=" & objGlobals.PlayerSelected
                    hLink.Text = .Rows(irow).Cells(3).Text
                    .Rows(irow).Cells(3).Text = objGlobals.PlayerSelected
                    .Rows(irow).Cells(3).Controls.Add(hLink)
                    .Rows(irow).Cells(3).ForeColor = Black
                    .Rows(irow).Cells(3).BackColor = White
                End If
            Next
        End With
    End Sub

    Sub load_SnookerPlayerStats()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        dt = New DataTable
        dt.Columns.Add(New DataColumn("Week", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Date", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Opponent Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Opponent Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("H/A", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Match", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture ID", GetType(System.String)))

        dr = dt.NewRow
        dr("Week") = "Week"
        dr("Date") = "Date"
        dr("Opponent Team") = "Opponent Team"
        dr("Opponent Player") = "Opponent"
        dr("H/A") = "H/A"
        dr("Result") = "Result"
        dr("Match") = "Match"
        dt.Rows.Add(dr)
        gRow = 0
        strSQL = "SELECT week_number,fixture_calendar,fixture_short_date,fixture_id, "
        strSQL = strSQL & "home_team,home_player,home_points,"
        strSQL = strSQL & "away_team,away_player,away_points "
        strSQL = strSQL & "FROM clubs.vw_fixtures_detail "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "'"
        strSQL = strSQL & "AND (home_team='" & objGlobals.TeamSelected & "' AND home_player='" & objGlobals.PlayerSelected & "') "
        strSQL = strSQL & "OR (League = '" & objGlobals.LeagueSelected & "' AND away_team='" & objGlobals.TeamSelected & "' AND away_player='" & objGlobals.PlayerSelected & "')"
        strSQL = strSQL & "ORDER BY week_number "
        myDataReader = objGlobals.SQLSelect(strSQL)
        lblPlayerStats.Text = objGlobals.PlayerSelected & " - RESULTS THIS SEASON"
        While myDataReader.Read()
            dr = dt.NewRow
            dr("Week") = myDataReader.Item("week_number")
            dr("Date") = myDataReader.Item("fixture_short_date")
            'dr("Date") = Format(myDataReader.Item("fixture_calendar"), "ddd dd MMM")
            If myDataReader.Item("home_team") = objGlobals.TeamSelected Then
                dr("Opponent Team") = myDataReader.Item("away_team")
                dr("Opponent Player") = myDataReader.Item("away_player")
                dr("H/A") = "HOME"
                If myDataReader.Item("home_points") = 1 Then
                    dr("Result") = "WON"
                Else
                    dr("Result") = "LOST"
                End If
                Call get_match_result(myDataReader.Item("fixture_id"), "Home")
            Else
                dr("Opponent Team") = myDataReader.Item("home_team")
                dr("Opponent Player") = myDataReader.Item("home_player")
                dr("H/A") = "AWAY"
                If myDataReader.Item("away_points") = 1 Then
                    dr("Result") = "WON"
                Else
                    dr("Result") = "LOST"
                End If
                Call get_match_result(myDataReader.Item("fixture_id"), "Away")
            End If
            dr("Match") = MatchResult
            dr("Fixture ID") = myDataReader.Item("fixture_id")
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

        dr = dt.NewRow
        dr("Opponent Team") = "Click on the Match Result to View the Match Card"
        dt.Rows.Add(dr)

        gridSnookerPlayerStats.DataSource = dt
        gridSnookerPlayerStats.DataBind()
        gridSnookerPlayerStats.Focus()

        lblPlayerStats.Text = objGlobals.PlayerSelected & " - RESULTS THIS SEASON (" & gridSnookerPlayerStats.Rows.Count - 2 & " GAMES PLAYED)"

    End Sub

    Sub get_match_result(inID As Integer, inHA As String)
        Dim strSQL As String
        Dim myDataReader2 As OleDbDataReader
        If inHA = "Home" Then
            strSQL = "SELECT REPLACE(home_result,' ',''),home_points,away_points FROM clubs.vw_fixtures WHERE fixture_id = " & inID
        Else
            strSQL = "SELECT REPLACE(away_result,' ',''),home_points,away_points FROM clubs.vw_fixtures WHERE fixture_id = " & inID
        End If
        myDataReader2 = objGlobals.SQLSelect(strSQL)
        While myDataReader2.Read()
            If inHA = "Home" Then
                Select Case myDataReader2.Item("home_points") - myDataReader2.Item("away_points")
                    Case Is > 0
                        MatchResult = "W " & myDataReader2.Item(0)
                    Case 0
                        MatchResult = "D " & myDataReader2.Item(0)
                    Case Is < 0
                        MatchResult = "L " & myDataReader2.Item(0)
                End Select
            Else
                Select Case myDataReader2.Item("home_points") - myDataReader2.Item("away_points")
                    Case Is < 0
                        MatchResult = "W " & myDataReader2.Item(0)
                    Case 0
                        MatchResult = "D " & myDataReader2.Item(0)
                    Case Is > 0
                        MatchResult = "L " & myDataReader2.Item(0)
                End Select
            End If

        End While
    End Sub


    Function position_count(ByVal inLeague As String, ByVal inPosition As Integer) As Integer
        position_count = 1
        Dim strSQL As String = ""
        Dim myDataReader As OleDbDataReader

        Select Case Left(inLeague, 4)
            Case "CRIB" : strSQL = "SELECT COUNT(*) FROM clubs.vw_player_stats_crib "
            Case "SKIT" : strSQL = "SELECT COUNT(*) FROM clubs.vw_player_stats_skittles "
            Case "SNOO" : strSQL = "SELECT COUNT(*) FROM clubs.vw_player_stats_snooker "
        End Select
        strSQL = strSQL + "WHERE league = '" & inLeague & "' AND league_pos = " & inPosition
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            position_count = myDataReader.Item(0)
        End While
        objGlobals.close_connection()

    End Function

    Sub load_results()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim Wk As Integer
        Dim CurrentWeek As Integer = objGlobals.GetCurrentWeek

        dt = New DataTable

        dt.Columns.Add(New DataColumn("Week Number", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture Calendar", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Team Name", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Team Name", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Points Deducted", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Points Deducted", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture ID", GetType(System.String)))

        strSQL = "SELECT * FROM clubs.vw_fixtures"
        strSQL = strSQL & " WHERE (home_team_name = '" & objGlobals.TeamSelected & "'"
        strSQL = strSQL & " OR away_team_name = '" & objGlobals.TeamSelected & "')"
        strSQL = strSQL & " AND League = '" & objGlobals.LeagueSelected & "'"
        strSQL = strSQL & " ORDER BY week_number"
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            dr = dt.NewRow
            Wk = myDataReader.Item("week_number")
            dr("Week Number") = Wk
            dr("Fixture Calendar") = myDataReader.Item("fixture_short_date")
            dr("Home Team Name") = ""
            dr("Away Team Name") = ""
            dr("Home Result") = ""
            dr("Home Points Deducted") = ""
            dr("Away Points Deducted") = ""
            dr("Fixture ID") = ""
            Select Case myDataReader.Item("home_team_name")
                Case objGlobals.TeamSelected
                    If Not myDataReader.Item("fixture_neutral") Then
                        dr("Home Team Name") = myDataReader.Item("away_team_name")
                        dr("Away Team Name") = "H"
                    Else
                        dr("Home Team Name") = myDataReader.Item("away_team_name") + " (N)"
                        dr("Away Team Name") = "N"
                    End If
                    If myDataReader.Item("home_result") <> "0 - 0" Then
                        get_match_result(myDataReader.Item("fixture_id"), "Home")
                        dr("Home Result") = MatchResult
                        dr("Fixture ID") = myDataReader.Item("fixture_id")
                    End If
                    If myDataReader.Item("status") = -1 Then dr("Home Result") = "Postponed"
                    If myDataReader.Item("home_points_deducted") > 0 Then dr("Home Points Deducted") = myDataReader.Item("home_points_deducted") * -1
                Case Else
                    If IsNumeric(myDataReader.Item("home_team")) Then
                        'Away match
                        If Not myDataReader.Item("fixture_neutral") Then
                            dr("Home Team Name") = myDataReader.Item("home_team_name")
                            dr("Away Team Name") = "A"
                        Else
                            dr("Home Team Name") = myDataReader.Item("home_team_name") + " (N)"
                            dr("Away Team Name") = "N"
                        End If
                        If myDataReader.Item("home_result") <> "0 - 0" Then
                            get_match_result(myDataReader.Item("fixture_id"), "Away")
                            dr("Home Result") = MatchResult
                            dr("Fixture ID") = myDataReader.Item("fixture_id")
                        End If
                        If myDataReader.Item("status") = -1 Then dr("Home Result") = "Postponed"
                        If myDataReader.Item("away_points_deducted") > 0 Then dr("Away Points Deducted") = myDataReader.Item("away_points_deducted") * -1
                    Else
                        dr("Home Team Name") = "OPEN WEEK"
                    End If
            End Select
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

        gRow = 0
        gridResults.DataSource = dt
        gridResults.DataBind()
        gridResults.Visible = True
        gridResults.Focus()

    End Sub

    Private Sub gridLeagues_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridLeagues.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            For i = 0 To 5
                If e.Row.Cells(i).Text <> "&nbsp;" Then
                    Dim hLink As New HyperLink
                    hLink.NavigateUrl = "~/Clubs/Stats.aspx?League=" & dt.Rows(gRow)(i).ToString & "&Top10=Top10"
                    hLink.Text = e.Row.Cells(i).Text
                    Select Case Left(e.Row.Cells(i).Text, 4)
                        Case "CRIB"
                            hLink.ForeColor = Yellow
                        Case "SNOO"
                            hLink.ForeColor = LightGreen
                        Case "SKIT"
                            hLink.ForeColor = LightBlue
                    End Select
                    If dt.Rows(gRow)(i).ToString = objGlobals.LeagueSelected Then
                        hLink.BackColor = White
                        hLink.ForeColor = Black
                    End If
                    e.Row.Cells(i).Controls.Add(hLink)
                    e.Row.CssClass = "cell"
                Else
                    e.Row.Cells(i).Visible = False
                End If
            Next i
        End If
    End Sub

    Private Sub gridTeam_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTeams.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            For i = 0 To 7
                If e.Row.Cells(i).Text <> "&nbsp;" Then
                    Dim hLink As New HyperLink
                    hLink.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & dt.Rows(gRow)(i).ToString & "&ShowResults=" & ShowResults
                    hLink.Text = e.Row.Cells(i).Text
                    hLink.ForeColor = Cyan
                    If dt.Rows(gRow)(i).ToString = objGlobals.TeamSelected Then
                        hLink.BackColor = White
                        hLink.ForeColor = Black
                    End If
                    e.Row.Cells(i).Controls.Add(hLink)
                    e.Row.CssClass = "cell"
                Else
                    e.Row.Cells(i).Visible = False
                End If
            Next
            gRow = gRow + 1
        End If
    End Sub

    Private Sub gridPlayers_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridPlayers.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            For i = 0 To 9
                If e.Row.Cells(i).Text <> "&nbsp;" Then
                    Dim hLink As New HyperLink
                    hLink.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&ShowResults=" & ShowResults & "&Player=" & dt.Rows(gRow)(i).ToString
                    hLink.Text = e.Row.Cells(i).Text
                    hLink.ForeColor = White
                    If dt.Rows(gRow)(i).ToString = objGlobals.PlayerSelected Then
                        hLink.BackColor = White
                        hLink.ForeColor = Black
                    End If
                    e.Row.Cells(i).Controls.Add(hLink)
                    e.Row.CssClass = "cell"
                Else
                    e.Row.Cells(i).Visible = False
                End If
            Next
            gRow = gRow + 1
        End If
    End Sub

    Private Sub gridCribLeagueStats_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridCribLeagueStats.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If gRow > 1 Then
                'row is a data row
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & dt.Rows(gRow)(3).ToString & "&ShowResults=" & ShowResults & "&Player=" & e.Row.Cells(2).Text
                hLink1.Text = e.Row.Cells(2).Text
                hLink1.ForeColor = White
                e.Row.Cells(2).Controls.Add(hLink1)
                e.Row.CssClass = "cell"

                Dim hLink2 As New HyperLink
                hLink2.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & dt.Rows(gRow)(3).ToString & "&ShowResults=" & ShowResults
                hLink2.Text = e.Row.Cells(3).Text
                hLink2.ForeColor = Cyan
                e.Row.Cells(3).Controls.Add(hLink2)
                e.Row.CssClass = "cell"
            Else
                Dim iCol As Integer
                For iCol = 0 To gridCribLeagueStats.Columns.Count - 1
                    e.Row.Cells(iCol).ForeColor = DarkKhaki
                    e.Row.Cells(iCol).BackColor = gridCribLeagueStats.BackColor
                Next
                If gRow = 1 Then
                    e.Row.Cells(2).Font.Size = 8
                    e.Row.Cells(3).Font.Size = 8
                End If
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            End If
            gRow = gRow + 1
        End If
    End Sub

    Private Sub gridSkittlesLeagueStats_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSkittlesLeagueStats.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If gRow > 1 Then
                'row is a data row
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & dt.Rows(gRow)(3).ToString & "&ShowResults=" & ShowResults & "&Player=" & e.Row.Cells(2).Text
                hLink1.Text = e.Row.Cells(2).Text
                hLink1.ForeColor = White
                e.Row.Cells(2).Controls.Add(hLink1)
                e.Row.CssClass = "cell"

                Dim hLink2 As New HyperLink
                hLink2.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & dt.Rows(gRow)(3).ToString & "&ShowResults=" & ShowResults
                hLink2.Text = e.Row.Cells(3).Text
                hLink2.ForeColor = Cyan
                e.Row.Cells(3).Controls.Add(hLink2)
                e.Row.CssClass = "cell"
            Else
                Dim iCol As Integer
                For iCol = 0 To gridSkittlesLeagueStats.Columns.Count - 1
                    e.Row.Cells(iCol).ForeColor = DarkKhaki
                    e.Row.Cells(iCol).BackColor = gridSkittlesLeagueStats.BackColor
                Next
                If gRow = 1 Then
                    e.Row.Cells(2).Font.Size = 8
                    e.Row.Cells(3).Font.Size = 8
                End If
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            End If
            gRow = gRow + 1
        End If
    End Sub

    Private Sub gridSnookerLeagueStats_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSnookerLeagueStats.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If gRow > 1 Then
                'row is a data row
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & dt.Rows(gRow)(3).ToString & "&ShowResults=" & ShowResults & "&Player=" & e.Row.Cells(2).Text
                hLink1.Text = e.Row.Cells(2).Text
                hLink1.ForeColor = White
                e.Row.Cells(2).Controls.Add(hLink1)
                e.Row.CssClass = "cell"

                Dim hLink2 As New HyperLink
                hLink2.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & dt.Rows(gRow)(3).ToString & "&ShowResults=" & ShowResults
                hLink2.Text = e.Row.Cells(3).Text
                hLink2.ForeColor = Cyan
                e.Row.Cells(3).Controls.Add(hLink2)
                e.Row.CssClass = "cell"
            Else
                Dim iCol As Integer
                For iCol = 0 To gridSnookerLeagueStats.Columns.Count - 1
                    e.Row.Cells(iCol).ForeColor = DarkKhaki
                    e.Row.Cells(iCol).BackColor = gridSnookerLeagueStats.BackColor
                Next
                If gRow = 1 Then
                    e.Row.Cells(2).Font.Size = 8
                    e.Row.Cells(3).Font.Size = 8
                End If
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            End If
            gRow = gRow + 1
        End If
    End Sub

    Private Sub gridCribTeamStats_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridCribTeamStats.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If gRow > 1 Then
                'row is a data row
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&ShowResults=" & ShowResults & "&Player=" & e.Row.Cells(3).Text
                hLink1.Text = e.Row.Cells(3).Text
                hLink1.ForeColor = White
                e.Row.Cells(3).Controls.Add(hLink1)
                e.Row.CssClass = "cell"
            Else
                Dim iCol As Integer
                For iCol = 0 To gridCribTeamStats.Columns.Count - 1
                    e.Row.Cells(iCol).ForeColor = DarkKhaki
                    e.Row.Cells(iCol).BackColor = gridCribTeamStats.BackColor
                Next
                If gRow = 1 Then
                    e.Row.Cells(3).Font.Size = 8
                End If
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            End If
            gRow = gRow + 1
        End If

    End Sub

    Private Sub gridSkittlesTeamStats_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSkittlesTeamStats.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If gRow > 1 Then
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&ShowResults=" & ShowResults & "&Player=" & e.Row.Cells(3).Text
                hLink1.Text = e.Row.Cells(3).Text
                hLink1.ForeColor = White
                e.Row.Cells(3).Controls.Add(hLink1)
                e.Row.CssClass = "cell"
            Else
                Dim iCol As Integer
                For iCol = 0 To gridSkittlesTeamStats.Columns.Count - 1
                    e.Row.Cells(iCol).ForeColor = DarkKhaki
                    e.Row.Cells(iCol).BackColor = gridSkittlesTeamStats.BackColor
                Next
                If gRow = 1 Then
                    e.Row.Cells(3).Font.Size = 8
                End If
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            End If
            gRow = gRow + 1
        End If
    End Sub

    Private Sub gridSnookerTeamStats_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSnookerTeamStats.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If gRow > 1 Then
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&ShowResults=" & ShowResults & "&Player=" & e.Row.Cells(3).Text
                hLink1.Text = e.Row.Cells(3).Text
                hLink1.ForeColor = White
                e.Row.Cells(3).Controls.Add(hLink1)
                e.Row.CssClass = "cell"
            Else
                Dim iCol As Integer
                For iCol = 0 To gridSnookerTeamStats.Columns.Count - 1
                    e.Row.Cells(iCol).ForeColor = DarkKhaki
                    e.Row.Cells(iCol).BackColor = gridSnookerTeamStats.BackColor
                Next
                If gRow = 1 Then
                    e.Row.Cells(3).Font.Size = 8
                End If
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridCribPlayerStats_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridCribPlayerStats.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If gRow > 1 Then
                If Left(e.Row.Cells(5).Text, 5) = "Click" Then 'Add Click to view card etc
                    e.Row.Cells(5).ColumnSpan = 5
                    e.Row.Cells(5).ForeColor = Red
                    e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(0).BackColor = gridCribPlayerStats.BackColor
                    e.Row.Cells(5).BackColor = gridCribPlayerStats.BackColor
                    e.Row.Cells(6).Visible = False
                    e.Row.Cells(7).Visible = False
                    e.Row.Cells(8).Visible = False
                    e.Row.Cells(9).Visible = False
                Else
                    Dim hLink1 As New HyperLink
                    Dim hLink2 As New HyperLink
                    Dim hLink3 As New HyperLink
                    Dim hLink4 As New HyperLink
                    Dim hLink5 As New HyperLink
                    Dim hLink6 As New HyperLink

                    'add SELECTED PLAYER link
                    hLink1.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&Player=" & e.Row.Cells(2).Text & "&ShowResults=" & ShowResults
                    hLink1.Text = e.Row.Cells(2).Text
                    hLink1.ForeColor = White
                    e.Row.Cells(2).Controls.Add(hLink1)
                    e.Row.CssClass = "cell"
                    hLink1.Text = e.Row.Cells(2).Text & " &"

                    'add SELECTED PARTNER link
                    hLink2.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&ShowResults=" & ShowResults & "&Player=" & e.Row.Cells(3).Text
                    hLink2.Text = e.Row.Cells(3).Text
                    hLink2.ForeColor = White
                    e.Row.Cells(3).Controls.Add(hLink2)
                    e.Row.CssClass = "cell"

                    'add OPPONENT TEAM link
                    hLink3.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & e.Row.Cells(4).Text & "&ShowResults=" & ShowResults
                    hLink3.Text = e.Row.Cells(4).Text
                    hLink3.ForeColor = Cyan
                    e.Row.Cells(4).Controls.Add(hLink3)
                    e.Row.CssClass = "cell"

                    'add OPPONENT #1 link
                    hLink4.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & e.Row.Cells(4).Text & "&ShowResults=" & ShowResults & "&Player=" & e.Row.Cells(5).Text
                    hLink4.Text = e.Row.Cells(5).Text
                    hLink4.ForeColor = White
                    e.Row.Cells(5).Controls.Add(hLink4)
                    e.Row.CssClass = "cell"

                    'add OPPONENT #2 link
                    hLink5.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & e.Row.Cells(4).Text & "&ShowResults=" & ShowResults & "&Player=" & e.Row.Cells(6).Text
                    hLink5.Text = e.Row.Cells(6).Text
                    hLink5.ForeColor = White
                    e.Row.Cells(6).Controls.Add(hLink5)
                    e.Row.CssClass = "cell"

                    'colours for indiviual result
                    e.Row.Cells(8).BackColor = objGlobals.colour_result_background(e.Row.Cells(8).Text)
                    e.Row.Cells(8).ForeColor = objGlobals.colour_result_foreground(e.Row.Cells(8).Text)

                    'add MATCH RESULT link
                    'hLink6.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&ShowResults=" & ShowResults & "&Player=" & objGlobals.PlayerSelected & "&CompID=" & Val(dt.Rows(gRow)(10))
                    hLink6.NavigateUrl = "~/Clubs/Result Card.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&Player=" & objGlobals.PlayerSelected & "&CompID=" & Val(dt.Rows(gRow)(10))
                    hLink6.Text = e.Row.Cells(9).Text
                    hLink6.ForeColor = objGlobals.colour_result_foreground(e.Row.Cells(9).Text)
                    e.Row.Cells(9).Controls.Add(hLink6)
                    e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(9).ToolTip = "Click to view card"
                    e.Row.Cells(9).BackColor = objGlobals.colour_result_background(e.Row.Cells(9).Text)
                    'e.Row.Cells(9).ForeColor = objGlobals.colour_result_foreground(e.Row.Cells(9).Text)
                    e.Row.CssClass = "cell"
                End If

            Else
                Dim iCol As Integer
                For iCol = 0 To gridCribPlayerStats.Columns.Count - 1
                    e.Row.Cells(iCol).ForeColor = DarkKhaki
                    e.Row.Cells(iCol).BackColor = gridCribPlayerStats.BackColor
                Next
                e.Row.Cells(2).ColumnSpan = 2
                e.Row.Cells(3).Visible = False
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Center
            End If
            gRow = gRow + 1
        End If

    End Sub

    Protected Sub gridCribPartnerStats_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridCribPartnerStats.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If gRow > 1 Then
                'row is a data row
            Else
                Dim iCol As Integer
                For iCol = 0 To gridCribPartnerStats.Columns.Count - 1
                    e.Row.Cells(iCol).ForeColor = DarkKhaki
                Next
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridSkittlesPlayerStats_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSkittlesPlayerStats.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If gRow > 0 Then
                If Left(e.Row.Cells(2).Text, 5) = "Click" Then 'Add Click to view card etc
                    e.Row.Cells(2).ColumnSpan = 5
                    e.Row.Cells(2).ForeColor = Red
                    e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(0).BackColor = gridSkittlesPlayerStats.BackColor
                    e.Row.Cells(2).BackColor = gridSkittlesPlayerStats.BackColor
                    e.Row.Cells(3).Visible = False
                    e.Row.Cells(4).Visible = False
                    e.Row.Cells(5).Visible = False
                    e.Row.Cells(6).Visible = False
                Else    'row is a data row
                    Dim hLink1 As New HyperLink
                    hLink1.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & dt.Rows(gRow)(2) & "&ShowResults=" & ShowResults
                    hLink1.Text = e.Row.Cells(2).Text
                    hLink1.ForeColor = Cyan
                    e.Row.Cells(2).Controls.Add(hLink1)
                    e.Row.CssClass = "cell"

                    Dim hLink2 As New HyperLink
                    'hLink2.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&ShowResults=" & ShowResults & "&Player=" & objGlobals.PlayerSelected & "&CompID=" & Val(dt.Rows(gRow)(6))
                    hLink2.NavigateUrl = "~/Clubs/Result Card.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&Player=" & objGlobals.PlayerSelected & "&CompID=" & Val(dt.Rows(gRow)(6))
                    hLink2.Text = e.Row.Cells(5).Text
                    hLink2.ForeColor = objGlobals.colour_result_foreground(e.Row.Cells(5).Text)
                    e.Row.Cells(5).Controls.Add(hLink2)
                    e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Center
                    e.Row.CssClass = "cell"
                    e.Row.Cells(5).ToolTip = "Click to view card"
                    e.Row.Cells(5).BackColor = objGlobals.colour_result_background(e.Row.Cells(5).Text)
                    'e.Row.Cells(5).ForeColor = objGlobals.colour_result_foreground(e.Row.Cells(5).Text)
                End If
            Else
                Dim iCol As Integer
                For iCol = 0 To gridSkittlesPlayerStats.Columns.Count - 1
                    e.Row.Cells(iCol).ForeColor = DarkKhaki
                    e.Row.Cells(iCol).BackColor = gridSkittlesPlayerStats.BackColor
                Next
                e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridSnookerPlayerStats_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSnookerPlayerStats.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If gRow > 0 Then
                'row is a data row
                If Left(e.Row.Cells(2).Text, 5) = "Click" Then 'Add Click to view card etc
                    e.Row.Cells(2).ColumnSpan = 5
                    e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(2).ForeColor = Red
                    e.Row.Cells(0).BackColor = gridSnookerPlayerStats.BackColor
                    e.Row.Cells(2).BackColor = gridSnookerPlayerStats.BackColor
                    e.Row.Cells(3).Visible = False
                    e.Row.Cells(4).Visible = False
                    e.Row.Cells(5).Visible = False
                    e.Row.Cells(6).Visible = False
                Else
                    Dim hLink1 As New HyperLink
                    hLink1.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & dt.Rows(gRow)(2) & "&ShowResults=" & ShowResults
                    hLink1.Text = e.Row.Cells(2).Text
                    hLink1.ForeColor = Cyan
                    e.Row.Cells(2).Controls.Add(hLink1)
                    e.Row.CssClass = "cell"

                    Dim hLink2 As New HyperLink
                    hLink2.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & dt.Rows(gRow)(2) & "&ShowResults=" & ShowResults & "&Player=" & dt.Rows(gRow)(3)
                    hLink2.Text = e.Row.Cells(3).Text
                    hLink2.ForeColor = White
                    e.Row.Cells(3).Controls.Add(hLink2)
                    e.Row.CssClass = "cell"

                    e.Row.Cells(5).BackColor = objGlobals.colour_result_background(e.Row.Cells(5).Text)
                    e.Row.Cells(5).ForeColor = objGlobals.colour_result_foreground(e.Row.Cells(5).Text)

                    Dim hLink3 As New HyperLink
                    'hLink3.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&ShowResults=" & ShowResults & "&Player=" & objGlobals.PlayerSelected & "&CompID=" & Val(dt.Rows(gRow)(7))
                    hLink3.NavigateUrl = "~/Clubs/Result Card.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&Player=" & objGlobals.PlayerSelected & "&CompID=" & Val(dt.Rows(gRow)(7))
                    hLink3.Text = e.Row.Cells(6).Text
                    hLink3.ForeColor = objGlobals.colour_result_foreground(e.Row.Cells(6).Text)
                    e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(6).Controls.Add(hLink3)
                    e.Row.CssClass = "cell"
                    e.Row.Cells(6).ToolTip = "Click to view card"
                    e.Row.Cells(6).BackColor = objGlobals.colour_result_background(e.Row.Cells(6).Text)
                    'e.Row.Cells(6).ForeColor = objGlobals.colour_result_foreground(e.Row.Cells(6).Text)
                End If
            Else
                Dim iCol As Integer
                For iCol = 0 To gridSnookerPlayerStats.Columns.Count - 1
                    e.Row.Cells(iCol).ForeColor = DarkKhaki
                    e.Row.Cells(iCol).BackColor = gridSnookerPlayerStats.BackColor
                Next
                e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            End If
            gRow = gRow + 1
        End If

    End Sub

    Protected Sub gridResults_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResults.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Left(dt.Rows(gRow)(2).ToString, 4) <> "OPEN" And Left(dt.Rows(gRow)(2).ToString, 3) <> "BYE" Then
                Dim hLink As New HyperLink
                hLink.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & Replace(dt.Rows(gRow)(2).ToString, " (N)", "") & "&ShowResults=" & ShowResults
                hLink.Text = e.Row.Cells(2).Text
                hLink.ForeColor = Cyan
                e.Row.Cells(2).Controls.Add(hLink)
                ''e.Row.CssClass = "row"

                If IsNumeric(e.Row.Cells(7).Text) Then
                    Dim hLink1 As New HyperLink
                    'hLink1.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&ShowResults=True&Player=" & objGlobals.PlayerSelected & "&CompID=" & e.Row.Cells(7).Text
                    hLink1.NavigateUrl = "~/Clubs/Result Card.aspx?CompID=" & e.Row.Cells(7).Text
                    hLink1.Text = e.Row.Cells(4).Text
                    hLink1.ForeColor = objGlobals.colour_result_foreground(e.Row.Cells(4).Text)
                    e.Row.Cells(4).Controls.Add(hLink1)
                    e.Row.Cells(4).BackColor = objGlobals.colour_result_background(e.Row.Cells(4).Text)
                    'e.Row.Cells(4).ForeColor = objGlobals.colour_result_foreground(e.Row.Cells(4).Text)
                    ''e.Row.CssClass = "row"
                    e.Row.Cells(7).Visible = False
                Else
                    e.Row.Cells(1).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                    e.Row.Cells(2).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                    e.Row.Cells(3).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                    e.Row.Cells(4).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                    e.Row.Cells(5).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                    e.Row.Cells(6).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                    e.Row.Cells(7).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                End If
            Else
                e.Row.Cells(1).ForeColor = Gray
                e.Row.Cells(2).ForeColor = Gray
                e.Row.Cells(1).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                e.Row.Cells(2).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                e.Row.Cells(3).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                e.Row.Cells(4).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                e.Row.Cells(5).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                e.Row.Cells(6).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                e.Row.Cells(7).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
            End If
            gRow = gRow + 1
        Else
            If objGlobals.LeagueSelected.Contains("SKITTLES") Then e.Row.Cells(3).Text = "H/A/N"
        End If
    End Sub

End Class
