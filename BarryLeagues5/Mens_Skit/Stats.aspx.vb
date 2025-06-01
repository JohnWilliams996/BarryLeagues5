Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing.Color
Imports System.Web.UI.DataVisualization.Charting
Imports System.Drawing

'Imports MySql.Data
'Imports MySql.Data.MySqlClient


Partial Class Stats
    Inherits System.Web.UI.Page
    Private dt As DataTable = New DataTable
    Private dr As DataRow = Nothing
    Private gRow As Integer
    Private objGlobals As New Globals
    Private SplitRow As Integer
    Private MatchResult As String
    Private RollsResult As String
    Private LeagueCount As Integer = 0
    Private TeamCount As Integer = 0
    Private PlayerCount As Integer = 0
    Private ChartLeague_League As String
    Private ChartLeague_Team(99) As String
    Private ChartLeague_Player(99) As String
    Private ChartTeam_League As String
    Private ChartTeam_Team(99) As String
    Private ChartTeam_Player(99) As String
    Private HomePoints As Double
    Private AwayPoints As Double
    Private HomeRollsWon As Double
    Private AwayRollsWon As Double
    Private HomePlayersTotal As Integer
    Private AwayPlayersTotal As Integer
    Private HomeRollTotal1 As Integer
    Private HomeRollTotal2 As Integer
    Private HomeRollTotal3 As Integer
    Private HomeRollTotal4 As Integer
    Private HomeRollTotal5 As Integer
    Private AwayRollTotal1 As Integer
    Private AwayRollTotal2 As Integer
    Private AwayRollTotal3 As Integer
    Private AwayRollTotal4 As Integer
    Private AwayRollTotal5 As Integer
    Private home_roll_1 As Integer
    Private home_roll_2 As Integer
    Private home_roll_3 As Integer
    Private home_roll_4 As Integer
    Private home_roll_5 As Integer
    Private home_roll_total As Integer
    Private away_roll_1 As Integer
    Private away_roll_2 As Integer
    Private away_roll_3 As Integer
    Private away_roll_4 As Integer
    Private away_roll_5 As Integer
    Private away_roll_total As Integer
    Private FixtureDate As String
    Private FixtureFullDate As String
    Private FixtureWeek As Integer
    Private FixtureLeague As String
    Private FixtureDetail As Boolean
    Private FixtureHomeTeam As String
    Private FixtureAwayTeam As String
    Private Top10 As String
    Private Result As String = ""
    Private home_result As String = ""
    Private ShowResults As String
    Private HighAverage As Integer
    Private LowAverage As Integer

    Private FixtureStatus As Integer
    Private CompID As Integer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("mens_skit")
        objGlobals.CurrentUser = "mens_skit_user"
        objGlobals.CurrentSchema = "mens_skit."
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
        hlTop10.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=Top10&Team=" & objGlobals.TeamSelected & "&ShowResults=" & ShowResults & "&Player=" & objGlobals.PlayerSelected & "&CompID=" & CompID
        hlAll.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=All&Team=" & objGlobals.TeamSelected & "&ShowResults=" & ShowResults & "&Player=" & objGlobals.PlayerSelected & "&CompID=" & CompID
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
                lblTop10.Text = "HIGHEST AVERAGE FOR ALL TEAMS (MIN 50% OF RESULT CARDS RETURNED TO COUNT)"
                lblTop10.Visible = True
                Call load_LeagueStatsTopPlayer()
                If objGlobals.PlayerSelected = Nothing Then highlight_top10_player(gridLeagueStats, objGlobals.TeamSelected)
                If Top10 = "Top10" Then Call show_league_graph(Left(objGlobals.LeagueSelected, 4))
                Call load_teams()
                If objGlobals.TeamSelected <> Nothing Then
                    lblTeamRule.Visible = True
                    If ShowResults = "True" Then
                        chtTeam1.Visible = False
                        chtTeam2.Visible = True
                        hlShowResults.Visible = False
                        hlHideResults.Visible = True
                        hlHideResults.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&ShowResults=False" & "&Player=" & objGlobals.PlayerSelected
                        Call load_results()
                    Else
                        chtTeam1.Visible = True
                        chtTeam2.Visible = False
                        hlShowResults.Visible = True
                        hlHideResults.Visible = False
                        hlShowResults.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&ShowResults=True" & "&Player=" & objGlobals.PlayerSelected
                        gridResults.Visible = False
                    End If
                    Call load_TeamStats()
                    If objGlobals.PlayerSelected <> Nothing Then highlight_top10_player(gridLeagueStats, objGlobals.PlayerSelected)
                    highlight_top10_team(gridLeagueStats, objGlobals.TeamSelected)
                    highlight_team_player(gridTeamStats)
                    Call show_team_graph(Left(objGlobals.LeagueSelected, 4))
                    Call load_players()
                End If
                If objGlobals.PlayerSelected <> Nothing And InStr(objGlobals.PlayerSelected, "A N OTHER") = 0 Then
                    If CompID = 0 Then
                        lblPlayerStats.Visible = True
                        Call load_PlayerStats()
                        Call load_PositionStats()
                        Call show_player_graph(Left(objGlobals.LeagueSelected, 4))
                    End If
                End If
            End If
        End If
    End Sub


    Sub load_leagues()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        strSQL = "SELECT league FROM mens_skit.vw_leagues ORDER BY 1"
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
                Case 1 : dr("League1") = myDataReader.Item(0)
                Case 2 : dr("League2") = myDataReader.Item(0)
                Case 3 : dr("League3") = myDataReader.Item(0)
                Case 4 : dr("League4") = myDataReader.Item(0)
                Case 5 : dr("League5") = myDataReader.Item(0)
                Case 6 : dr("League6") = myDataReader.Item(0)
            End Select
        End While
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
        strSQL = "SELECT long_name FROM mens_skit.vw_teams WHERE league = '" & objGlobals.LeagueSelected & "' AND long_name <> 'BYE' ORDER BY 1"
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
        dt.Columns.Add(New DataColumn("Team9", GetType(System.String)))
        dr = dt.NewRow
        While myDataReader.Read()
            TeamCount = TeamCount + 1
            Select Case TeamCount
                Case 1 : dr("Team1") = myDataReader.Item("long_name")
                Case 2, 11 : dr("Team2") = myDataReader.Item("long_name")
                Case 3, 12 : dr("Team3") = myDataReader.Item("long_name")
                Case 4, 13 : dr("Team4") = myDataReader.Item("long_name")
                Case 5, 14 : dr("Team5") = myDataReader.Item("long_name")
                Case 6, 15 : dr("Team6") = myDataReader.Item("long_name")
                Case 7, 16 : dr("Team7") = myDataReader.Item("long_name")
                Case 8, 17 : dr("Team8") = myDataReader.Item("long_name")
                Case 9, 18 : dr("Team9") = myDataReader.Item("long_name")
                Case 10
                    dt.Rows.Add(dr)
                    dr = dt.NewRow
                    dr("Team1") = myDataReader.Item("long_name")
            End Select
        End While
        dt.Rows.Add(dr)

        gridTeams.DataSource = dt
        gridTeams.DataBind()
    End Sub

    Sub load_players()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        strSQL = "SELECT player FROM mens_skit.vw_players WHERE league = '" & objGlobals.LeagueSelected & "' AND team = '" & objGlobals.TeamSelected & "' AND player NOT LIKE 'A N OTHER%' ORDER BY 1"
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
        dt.Rows.Add(dr)

        gridPlayers.DataSource = dt
        gridPlayers.DataBind()
    End Sub


    Sub load_LeagueStatsTop10()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        strSQL = "SELECT last_six,league_pos,player,team,total_played,total_score,average "
        strSQL = strSQL & "FROM mens_skit.vw_player_stats "
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

        gridLeagueStats.DataSource = dt
        gridLeagueStats.DataBind()

        'check for Tied positions
        With gridLeagueStats
            For i = .Rows.Count - 1 To 3 Step -1
                If .Rows(i).Cells(6).Text = .Rows(i - 1).Cells(6).Text Then
                    If Left(.Rows(i - 1).Cells(1).Text, 1) <> "T" Then .Rows(i - 1).Cells(1).Text = "T" + .Rows(i - 1).Cells(1).Text
                    If Left(.Rows(i).Cells(1).Text, 1) <> "T" Then .Rows(i).Cells(1).Text = "T" + .Rows(i).Cells(1).Text
                End If
            Next
        End With

        gridTeams.Focus()
    End Sub

    Sub load_LeagueStatsTopPlayer()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        strSQL = "SELECT last_six,league_pos,team,player,total_played,total_score,average "
        strSQL = strSQL & "FROM mens_skit.vw_player_stats "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "'"
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

        gridLeagueStats.DataSource = dt
        gridLeagueStats.DataBind()

        gridTeams.Focus()
    End Sub

    Sub load_LeagueStatsAll()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        strSQL = "SELECT last_six,league_pos,player,team,total_played,total_score,Average "
        strSQL = strSQL & "FROM mens_skit.vw_player_stats "
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

        strSQL = "SELECT last_six,league_pos,player,team,total_played,total_score,Average "
        strSQL = strSQL & "FROM mens_skit.vw_player_stats "
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

        gridLeagueStats.DataSource = dt
        gridLeagueStats.DataBind()

        If SplitRow > 0 Then
            For i = 1 To gridLeagueStats.Columns.Count - 1
                gridLeagueStats.Rows(SplitRow).Cells(i).Visible = False
            Next
            With gridLeagueStats.Rows(SplitRow).Cells(0)
                .HorizontalAlign = HorizontalAlign.Left
                .ForeColor = lblTeamRule.ForeColor
                .Font.Size = 12
                .ColumnSpan = gridLeagueStats.Columns.Count
                .BackColor = gridLeagueStats.BackColor
            End With
        End If

        'check for Tied positions
        With gridLeagueStats
            For i = .Rows.Count - 1 To 3 Step -1
                If .Rows(i).Cells(6).Text = .Rows(i - 1).Cells(6).Text Then
                    If Left(.Rows(i - 1).Cells(1).Text, 1) <> "T" Then .Rows(i - 1).Cells(1).Text = "T" + .Rows(i - 1).Cells(1).Text
                    If Left(.Rows(i).Cells(1).Text, 1) <> "T" Then .Rows(i).Cells(1).Text = "T" + .Rows(i).Cells(1).Text
                End If
            Next
        End With

        gridTeams.Focus()
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
            .Height = 25 * gridLeagueStats.Rows.Count
            .Titles.Add("Highest Average for All Teams in Team Order")
            .ChartAreas(0).AxisY.Title = "Average"
            Dim j As Integer = 2
            For i = gridLeagueStats.Rows.Count - 1 To 2 Step -1
                If i = gridLeagueStats.Rows.Count - 1 Then .ChartAreas(0).AxisY.Minimum = gridLeagueStats.Rows(i).Cells(6).Text
                If i = 2 Then .ChartAreas(0).AxisY.Maximum = gridLeagueStats.Rows(i).Cells(6).Text
                ChartLeague_League = objGlobals.LeagueSelected
                ChartLeague_Team(i - 1) = Replace(gridLeagueStats.Rows(i).Cells(3).Text, "&quot;", Chr(34))
                ChartLeague_Player(i - 1) = gridLeagueStats.Rows(i).Cells(2).Text
                .ChartAreas(0).AxisX.CustomLabels.Add(i - 1, i - 1, gridLeagueStats.Rows(j).Cells(2).Text)
                LeagueSeries.Points.AddXY(gridLeagueStats.Rows(i).Cells(2).Text, gridLeagueStats.Rows(i).Cells(6).Text)
                j = j + 1
            Next
            Dim myFont As New System.Drawing.Font("Arial", 8)
            For Each pt As DataPoint In LeagueSeries.Points
                pt.Font = myFont
            Next
            .ChartAreas(0).AxisY.Minimum = (LowAverage / 100) - 0.4
            .ChartAreas(0).AxisY.Maximum = (HighAverage / 100) + 0.4
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
            .Height = 20 * gridTeamStats.Rows.Count
            .Titles.Add("Total Average - " & objGlobals.TeamSelected)
            .ChartAreas(0).AxisY.Title = "Average"
            Dim j As Integer = 3
            Dim MinPoints As Double = 99.9
            For i = gridTeamStats.Rows.Count - 1 To 3 Step -1
                If IsNumeric(gridTeamStats.Rows(i).Cells(6).Text) Then
                    If gridTeamStats.Rows(i).Cells(6).Text < MinPoints Then MinPoints = gridTeamStats.Rows(i).Cells(6).Text
                End If
                If i = 3 Then
                    .ChartAreas(0).AxisY.Maximum = gridTeamStats.Rows(i).Cells(6).Text
                End If
                ChartTeam_League = objGlobals.LeagueSelected
                ChartTeam_Team(i - 1) = objGlobals.TeamSelected
                ChartTeam_Player(i - 1) = gridTeamStats.Rows(i).Cells(3).Text
                .ChartAreas(0).AxisX.CustomLabels.Add(i - 2, i - 2, Replace(gridTeamStats.Rows(j).Cells(3).Text, "&nbsp;", ""))
                TeamSeries.Points.AddXY(gridTeamStats.Rows(i).Cells(3).Text, gridTeamStats.Rows(i).Cells(6).Text)
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
            .Height = 20 * gridTeamStats.Rows.Count
            .Titles.Add("Total Average - " & objGlobals.TeamSelected)
            .ChartAreas(0).AxisY.Title = "Average"
            Dim j As Integer = 3
            Dim MinPoints As Double = 99.9
            For i = gridTeamStats.Rows.Count - 1 To 3 Step -1
                If IsNumeric(gridTeamStats.Rows(i).Cells(6).Text) Then
                    If gridTeamStats.Rows(i).Cells(6).Text < MinPoints Then MinPoints = gridTeamStats.Rows(i).Cells(6).Text
                End If
                If i = 3 Then
                    .ChartAreas(0).AxisY.Maximum = gridTeamStats.Rows(i).Cells(6).Text
                End If
                ChartTeam_League = objGlobals.LeagueSelected
                ChartTeam_Team(i - 1) = objGlobals.TeamSelected
                ChartTeam_Player(i - 1) = gridTeamStats.Rows(i).Cells(3).Text
                .ChartAreas(0).AxisX.CustomLabels.Add(i - 2, i - 2, Replace(gridTeamStats.Rows(j).Cells(3).Text, "&nbsp;", ""))
                TeamSeries.Points.AddXY(gridTeamStats.Rows(i).Cells(3).Text, gridTeamStats.Rows(i).Cells(6).Text)
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
            .Titles.Item(0).ForeColor = LightGreen
            .Titles.Item(0).Font = New System.Drawing.Font("Arial", 12)
        End With

    End Sub

    Sub show_player_graph(inLeague As String)
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

            .Height = 400
            .ChartAreas(0).AxisX.Title = "Week"
            .ChartAreas(0).AxisY.Title = "Pins"
            .Titles.Add(objGlobals.PlayerSelected & " - Pins per Game")
            Dim MinPoints As Integer = 99
            Dim MaxPoints As Integer = 0
            For i = 1 To gridPlayerStats.Rows.Count - 2
                If gridPlayerStats.Rows(i).Cells(4).Text < MinPoints Then MinPoints = gridPlayerStats.Rows(i).Cells(4).Text
                If gridPlayerStats.Rows(i).Cells(4).Text > MaxPoints Then MaxPoints = gridPlayerStats.Rows(i).Cells(4).Text
                PlayerSeries.Points.AddXY(gridPlayerStats.Rows(i).Cells(0).Text, gridPlayerStats.Rows(i).Cells(4).Text)
                'PlayerSeries.Points(i - 1)("LabelStyle=Bottom") = "True"
            Next
            .ChartAreas(0).AxisY.Minimum = MinPoints - 1
            .ChartAreas(0).AxisY.Maximum = MaxPoints + 1
            If MaxPoints - MinPoints >= 11 Then
                .ChartAreas(0).AxisY.Interval = 2
            Else
                .ChartAreas(0).AxisY.Interval = 1
            End If
            'get the average from the team grid
            For i = 3 To gridTeamStats.Rows.Count - 1
                If gridTeamStats.Rows(i).Cells(3).Text = objGlobals.PlayerSelected Then
                    .Titles.Add("Average : " & gridTeamStats.Rows(i).Cells(6).Text & " Pins per Game (" & gridTeamStats.Rows(i).Cells(4).Text & " Games)")
                    .Titles.Item(1).ForeColor = Orange
                    .Titles.Item(1).Font = New System.Drawing.Font("Arial", 10)
                    hla.AnchorY = gridTeamStats.Rows(i).Cells(6).Text
                    ta.AnchorY = hla.AnchorY
                    ta.Text = "Ave : " & gridTeamStats.Rows(i).Cells(6).Text
                    Exit For
                End If
            Next
            Dim myFont As New System.Drawing.Font("Arial", 8)
            For Each pt As DataPoint In PlayerSeries.Points
                pt.Font = myFont
            Next
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

    Sub load_TeamStats()
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
        dt.Columns.Add(New DataColumn("Total High Score", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total High Roll", GetType(System.String)))
        ' dt.Columns.Add(New DataColumn("Total Nines", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Thirties", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Pins", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Average", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home High Score", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home High Roll", GetType(System.String)))
        ' dt.Columns.Add(New DataColumn("Home Nines", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Thirties", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Pins", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Average", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away High Score", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away High Roll", GetType(System.String)))
        'dt.Columns.Add(New DataColumn("Away Nines", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Thirties", GetType(System.String)))

        dr = dt.NewRow
        dr("Last 6") = "Last 6"
        dr("League Pos") = "League"
        dr("Team Pos") = "Team"
        dr("Player") = "Player"
        dr("Total Played") = "Total"
        dr("Total Pins") = "Total"
        dr("Total Average") = "Total"
        dr("Total High Score") = "Total"
        dr("Total High Roll") = "Total"
        ' dr("Total Nines") = "Total"
        dr("Total Thirties") = "Total"
        dr("Home Played") = "Home"
        dr("Home Pins") = "Home"
        dr("Home Average") = "Home"
        dr("Home High Score") = "Home"
        dr("Home High Roll") = "Home"
        'dr("Home Nines") = "Home"
        dr("Home Thirties") = "Home"
        dr("Away Played") = "Away"
        dr("Away Pins") = "Away"
        dr("Away Average") = "Away"
        dr("Away High Score") = "Away"
        dr("Away High Roll") = "Away"
        'dr("Away Nines") = "Away"
        dr("Away Thirties") = "Away"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Last 6") = "Scores"
        dr("League Pos") = "Pos"
        dr("Team Pos") = "Pos"
        dr("Player") = "(Click Name for"
        dr("Total Played") = "Pld"
        dr("Total Pins") = "Pins"
        dr("Total Average") = "Average"
        dr("Total High Score") = "High"
        dr("Total High Roll") = "High"
        'dr("Total Nines") = "9+"
        'dr("Total Thirties") = "30+"
        dr("Home Played") = "Pld"
        dr("Home Pins") = "Pins"
        dr("Home Average") = "Average"
        dr("Home High Score") = "High"
        dr("Home High Roll") = "High"
        'dr("Home Nines") = "9+"
        'dr("Home Thirties") = "30+"
        dr("Away Played") = "Pld"
        dr("Away Pins") = "Pins"
        dr("Away Average") = "Average"
        dr("Away High Score") = "High"
        dr("Away High Roll") = "High"
        'dr("Away Nines") = "9+"
        dr("Away Thirties") = "30+"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Last 6") = ""
        dr("League Pos") = ""
        dr("Team Pos") = ""
        dr("Player") = "Player Stats)"
        dr("Total Played") = ""
        dr("Total Pins") = ""
        dr("Total Average") = ""
        dr("Total High Score") = "Score"
        dr("Total High Roll") = "Roll"
        'dr("Total Nines") = ""
        'dr("Total Thirties") = ""
        dr("Home Played") = ""
        dr("Home Pins") = ""
        dr("Home Average") = ""
        dr("Home High Score") = "Score"
        dr("Home High Roll") = "Roll"
        'dr("Home Nines") = ""
        'dr("Home Thirties") = ""
        dr("Away Played") = ""
        dr("Away Pins") = ""
        dr("Away Average") = ""
        dr("Away High Score") = "Score"
        dr("Away High Roll") = "Roll"
        'dr("Away Nines") = ""
        dr("Away Thirties") = ""
        dt.Rows.Add(dr)

        SplitRow = 2
        gRow = 0
        strSQL = "SELECT last_six,league_pos,team_pos,total_team_played,player,"
        strSQL = strSQL & "total_played,total_score,average,highest_score,"
        strSQL = strSQL & "home_played,home_score,home_average,home_highest_score,"
        strSQL = strSQL & "away_played,away_score,away_average,away_highest_score,away_thirties "
        strSQL = strSQL & "FROM mens_skit.vw_player_stats "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "' "
        strSQL = strSQL & "AND team = '" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & "AND player NOT LIKE 'A N OTHER%' "
        strSQL = strSQL & "AND team_pos > 0 "
        strSQL = strSQL & "ORDER BY team_pos,total_played DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            If dt.Rows.Count = 3 Then
                SplitGames = Int((myDataReader.Item("total_team_played") / 2) + 0.5)
                lblTeamRule.Text = objGlobals.TeamSelected & " STATS (" & CStr(myDataReader.Item("total_team_played")) & " CARDS RETURNED, " & CStr(SplitGames) & "+ (50%) GAMES PLAYED TO COUNT)"
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
            dr("Total High Score") = myDataReader.Item("highest_score")

            dr("Home Played") = myDataReader.Item("home_played")
            dr("Home Pins") = myDataReader.Item("home_score")
            dr("Home Average") = myDataReader.Item("home_average")
            dr("Home High Score") = myDataReader.Item("home_highest_score")

            dr("Away Played") = myDataReader.Item("away_played")
            dr("Away Pins") = myDataReader.Item("away_score")
            dr("Away Average") = myDataReader.Item("away_average")
            dr("Away High Score") = myDataReader.Item("away_highest_score")
            If myDataReader.Item("away_thirties") > 0 Then dr("Away Thirties") = myDataReader.Item("away_thirties")

            dt.Rows.Add(dr)
            SplitRow = SplitRow + 1
        End While

        strSQL = "SELECT last_six,league_pos,team_pos,total_team_played,player,"
        strSQL = strSQL & "total_played,total_score,average,highest_score,"
        strSQL = strSQL & "home_played,home_score,home_average,home_highest_score,"
        strSQL = strSQL & "away_played,away_score,away_average,away_highest_score,away_thirties "
        strSQL = strSQL & "FROM mens_skit.vw_player_stats "
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
            dr("Player") = myDataReader.Item("player")
            dr("Total Played") = myDataReader.Item("total_played")
            dr("Total Pins") = myDataReader.Item("total_score")
            dr("Total Average") = myDataReader.Item("average")
            dr("Total High Score") = myDataReader.Item("highest_score")

            dr("Home Played") = myDataReader.Item("home_played")
            dr("Home Pins") = myDataReader.Item("home_score")
            dr("Home Average") = myDataReader.Item("home_average")
            dr("Home High Score") = myDataReader.Item("home_highest_score")

            dr("Away Played") = myDataReader.Item("away_played")
            dr("Away Pins") = myDataReader.Item("away_score")
            dr("Away Average") = myDataReader.Item("away_average")
            dr("Away High Score") = myDataReader.Item("away_highest_score")
            If myDataReader.Item("away_thirties") > 0 Then dr("Away Thirties") = myDataReader.Item("away_thirties")

            dt.Rows.Add(dr)
        End While

        gridTeamStats.DataSource = dt
        gridTeamStats.DataBind()

        If SplitRow > 0 Then
            For i = 1 To gridTeamStats.Columns.Count - 1
                gridTeamStats.Rows(SplitRow).Cells(i).Visible = False
            Next
            With gridTeamStats.Rows(SplitRow).Cells(0)
                .HorizontalAlign = HorizontalAlign.Left
                .ForeColor = lblTeamRule.ForeColor
                .Font.Size = 12
                .ColumnSpan = gridTeamStats.Columns.Count
                .BackColor = gridTeamStats.BackColor
            End With
        End If

        'check for Tied team positions
        With gridTeamStats
            For i = .Rows.Count - 1 To 3 Step -1
                If .Rows(i).Cells(6).Text = .Rows(i - 1).Cells(6).Text Then
                    If Left(.Rows(i - 1).Cells(2).Text, 1) <> "T" Then .Rows(i - 1).Cells(2).Text = "T" + .Rows(i - 1).Cells(2).Text
                    If Left(.Rows(i).Cells(2).Text, 1) <> "T" Then .Rows(i).Cells(2).Text = "T" + .Rows(i).Cells(2).Text
                End If
            Next
        End With

        gridPlayers.Focus()
    End Sub

    Sub load_PositionStats()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        dt = New DataTable
        dt.Columns.Add(New DataColumn("Position", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Played", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Average", GetType(System.String)))
        dr = dt.NewRow
        dr("Position") = "Position"
        dr("Played") = "Played"
        dr("Average") = "Average"
        dt.Rows.Add(dr)
        gRow = 0

        strSQL = "EXEC mens_skit.sp_get_player_stats_by_position '" & objGlobals.current_season & "','" & objGlobals.TeamSelected & "','" & objGlobals.PlayerSelected & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            dr = dt.NewRow
            dr("Position") = myDataReader.Item("Position")
            dr("Played") = myDataReader.Item("Played")
            dr("Average") = myDataReader.Item("Average")
            dt.Rows.Add(dr)
        End While
        gridPositionStats.DataSource = dt
        gridPositionStats.DataBind()

        lblPositionStats.Visible = True
        gridPositionStats.Visible = True

    End Sub

    Sub load_PlayerStats()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        dt = New DataTable
        dt.Columns.Add(New DataColumn("Week", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Date", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Opponents", GetType(System.String)))
        dt.Columns.Add(New DataColumn("H/A", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Score", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Number Thirties", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Rolls Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture ID", GetType(System.String)))

        dr = dt.NewRow
        dr("Week") = "Week"
        dr("Date") = "Date"
        dr("Opponents") = "Opponents"
        dr("H/A") = "H/A"
        dr("Score") = "Total"
        dr("Number Thirties") = "Away 30+"
        dr("Result") = "Result"
        dr("Rolls Result") = "Rolls"
        dt.Rows.Add(dr)
        gRow = 0
        strSQL = "SELECT week_number,fixture_calendar,fixture_short_date,fixture_id,"
        strSQL = strSQL & "home_team,home_player,home_points,"
        strSQL = strSQL & "away_team,away_player,away_points,away_thirties "
        strSQL = strSQL & "FROM mens_skit.vw_fixtures_detail "
        strSQL = strSQL & "WHERE league = '" & objGlobals.LeagueSelected & "'"
            strSQL = strSQL & " AND (home_team='" & objGlobals.TeamSelected & "' AND home_player='" & objGlobals.PlayerSelected & "') "
        strSQL = strSQL & " OR (League = '" & objGlobals.LeagueSelected & "' AND (away_team='" & objGlobals.TeamSelected & "' AND away_player='" & objGlobals.PlayerSelected & "'))"
        strSQL = strSQL & " ORDER BY week_number "
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            dr = dt.NewRow
            dr("Week") = myDataReader.Item("week_number")
            dr("Date") = myDataReader.Item("fixture_short_date")
            'dr("Date") = Format(myDataReader.Item("fixture_calendar"), "ddd dd MMM")
            If myDataReader.Item("home_team") = objGlobals.TeamSelected Then
                dr("Opponents") = myDataReader.Item("away_team")
                dr("H/A") = "HOME"
                dr("Score") = myDataReader.Item("home_points")
                Call get_match_result(myDataReader.Item("fixture_id"), "Home")
            Else
                dr("Opponents") = myDataReader.Item("home_team")
                dr("H/A") = "AWAY"
                dr("Score") = myDataReader.Item("away_points")
                If myDataReader.Item("away_thirties") > 0 Then
                    dr("Number Thirties") = myDataReader.Item("away_thirties")
                End If
                Call get_match_result(myDataReader.Item("fixture_id"), "Away")
            End If
            dr("Result") = MatchResult
            dr("Rolls Result") = RollsResult
            dr("Fixture ID") = myDataReader.Item("fixture_id")
            dt.Rows.Add(dr)
        End While

        dr = dt.NewRow
        dr("Opponents") = "Click on the Match Result to View the Match Card"
        dt.Rows.Add(dr)

        gridPlayerStats.DataSource = dt
        gridPlayerStats.DataBind()
        gridPlayerStats.Focus()

        lblPlayerStats.Text = objGlobals.PlayerSelected & " - SCORES THIS SEASON (" & gridPlayerStats.Rows.Count - 2 & " GAMES PLAYED)"

        lblPositionStats.Text = objGlobals.PlayerSelected & " - AVERAGE BY POSITION PLAYED"

    End Sub

    Sub highlight_top10_player(inGrid As GridView, inComparison As String)
        'highlight player if in league top 10
        inComparison = Replace(inComparison, Chr(34), "&quot;")
        With inGrid
            For irow = 2 To .Rows.Count - 1
                If .Rows(irow).Cells(2).Text = inComparison Then
                    Dim hLink As New HyperLink
                    hLink.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&Player=" & objGlobals.PlayerSelected
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
                    hLink.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&Player=" & objGlobals.PlayerSelected
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
                    hLink.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&Player=" & objGlobals.PlayerSelected
                    hLink.Text = .Rows(irow).Cells(3).Text
                    .Rows(irow).Cells(3).Text = objGlobals.PlayerSelected
                    .Rows(irow).Cells(3).Controls.Add(hLink)
                    .Rows(irow).Cells(3).ForeColor = Black
                    .Rows(irow).Cells(3).BackColor = White
                End If
            Next
        End With
    End Sub

    Sub get_match_result(inID As Integer, inHA As String)
        Dim strSQL As String
        Dim myDataReader2 As OleDbDataReader
        If inHA = "Home" Then
            strSQL = "SELECT REPLACE(home_result,' ',''),home_points,away_points,REPLACE(home_rolls_result,' ',''),REPLACE(away_rolls_result,' ',''),home_rolls_won,away_rolls_won FROM mens_skit.vw_fixtures WHERE fixture_id = " & inID
        Else
            strSQL = "SELECT REPLACE(away_result,' ',''),home_points,away_points,REPLACE(home_rolls_result,' ',''),REPLACE(away_rolls_result,' ',''),home_rolls_won,away_rolls_won FROM mens_skit.vw_fixtures WHERE fixture_id = " & inID
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
                Select Case myDataReader2.Item("home_rolls_won") - myDataReader2.Item("away_rolls_won")
                    Case Is > 0
                        RollsResult = "W " & myDataReader2.Item(3)
                    Case 0
                        RollsResult = "D " & myDataReader2.Item(3)
                    Case Is < 0
                        RollsResult = "L " & myDataReader2.Item(3)
                End Select
                'RollsResult = myDataReader2.Item(3)
            Else
                Select Case myDataReader2.Item("home_points") - myDataReader2.Item("away_points")
                    Case Is < 0
                        MatchResult = "W " & myDataReader2.Item(0)
                    Case 0
                        MatchResult = "D " & myDataReader2.Item(0)
                    Case Is > 0
                        MatchResult = "L " & myDataReader2.Item(0)
                End Select
                Select Case myDataReader2.Item("away_rolls_won") - myDataReader2.Item("home_rolls_won")
                    Case Is > 0
                        RollsResult = "W " & myDataReader2.Item(4)
                    Case 0
                        RollsResult = "D " & myDataReader2.Item(4)
                    Case Is < 0
                        RollsResult = "L " & myDataReader2.Item(4)
                End Select
                'RollsResult = myDataReader2.Item(4)
            End If
        End While
    End Sub


    Function position_count(ByVal inLeague As String, ByVal inPosition As Integer) As Integer
        position_count = 1
        Dim strSQL As String = ""
        Dim myDataReader As OleDbDataReader

        strSQL = "SELECT COUNT(*) FROM mens_skit.vw_player_stats "
        strSQL = strSQL + "WHERE league = '" & inLeague & "' AND league_pos = " & inPosition
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            position_count = myDataReader.Item(0)
        End While
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
        dt.Columns.Add(New DataColumn("Rolls Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture ID", GetType(System.String)))

        strSQL = "SELECT * FROM mens_skit.vw_fixtures"
        strSQL = strSQL & " WHERE (home_team_name = '" & objGlobals.TeamSelected & "'"
        strSQL = strSQL & " OR away_team_name = '" & objGlobals.TeamSelected & "')"
        strSQL = strSQL & " AND league = '" & objGlobals.LeagueSelected & "'"
        strSQL = strSQL & " ORDER BY week_number"
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            dr = dt.NewRow
            Wk = myDataReader.Item("week_number")
            dr("Week Number") = Wk
            dr("Fixture Calendar") = myDataReader.Item("fixture_short_date")
            'dr("Fixture Calendar") = objGlobals.AddSuffix(Right(Format(myDataReader.Item("fixture_calendar"), "ddd d"), 2))
            'dr("Fixture Calendar") = dr("Fixture Calendar") + Format(myDataReader.Item("fixture_calendar"), " MMM")
            dr("Home Team Name") = ""
            dr("Away Team Name") = ""
            dr("Home Result") = ""
            dr("Rolls Result") = ""
            dr("Fixture ID") = ""
            Select Case myDataReader.Item("home_team_name")
                Case objGlobals.TeamSelected
                    dr("Home Team Name") = myDataReader.Item("away_team_name")
                    dr("Away Team Name") = "H"
                    If myDataReader.Item("home_result") <> "0 - 0" Then
                        get_match_result(myDataReader.Item("fixture_id"), "Home")
                        dr("Home Result") = MatchResult
                        If myDataReader.Item("status") >= 1 Then dr("Rolls Result") = RollsResult
                        dr("Fixture ID") = myDataReader.Item("fixture_id")
                    End If
                    If myDataReader.Item("status") = -1 Then dr("Home Result") = "Postponed"
                Case Else
                    If IsNumeric(myDataReader.Item("home_team")) Then
                        'Away match
                        dr("Home Team Name") = myDataReader.Item("home_team_name")
                        dr("Away Team Name") = "A"
                        If myDataReader.Item("home_result") <> "0 - 0" Then
                            get_match_result(myDataReader.Item("fixture_id"), "Away")
                            dr("Home Result") = MatchResult
                            If myDataReader.Item("status") >= 1 Then dr("Rolls Result") = RollsResult
                            dr("Fixture ID") = myDataReader.Item("fixture_id")
                        End If
                        If myDataReader.Item("status") = -1 Then dr("Home Result") = "Postponed"
                    Else
                        dr("Home Team Name") = "OPEN WEEK"
                    End If
            End Select
            dt.Rows.Add(dr)
        End While
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
                    hLink.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & dt.Rows(gRow)(i).ToString & "&Top10=Top10"
                    hLink.Text = e.Row.Cells(i).Text
                    hLink.ForeColor = LightBlue
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
            For i = 0 To 8
                If e.Row.Cells(i).Text <> "&nbsp;" Then
                    Dim hLink As New HyperLink
                    hLink.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & dt.Rows(gRow)(i).ToString & "&ShowResults=" & ShowResults
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
                    hLink.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&ShowResults=" & ShowResults & "&Player=" & dt.Rows(gRow)(i).ToString
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

    Private Sub gridLeagueStats_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridLeagueStats.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If gRow > 1 Then
                'row is a data row
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & dt.Rows(gRow)(3).ToString & "&ShowResults=" & ShowResults & "&Player=" & e.Row.Cells(2).Text
                hLink1.Text = e.Row.Cells(2).Text
                hLink1.ForeColor = White
                e.Row.Cells(2).Controls.Add(hLink1)
                e.Row.CssClass = "cell"

                Dim hLink2 As New HyperLink
                hLink2.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & dt.Rows(gRow)(3).ToString & "&ShowResults=" & ShowResults
                hLink2.Text = e.Row.Cells(3).Text
                hLink2.ForeColor = Cyan
                e.Row.Cells(3).Controls.Add(hLink2)
                e.Row.CssClass = "cell"
            Else
                Dim iCol As Integer
                For iCol = 0 To gridLeagueStats.Columns.Count - 1
                    e.Row.Cells(iCol).ForeColor = DarkKhaki
                    e.Row.Cells(iCol).BackColor = gridLeagueStats.BackColor
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

    Private Sub gridTeamStats_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTeamStats.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If gRow > 2 Then
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&ShowResults=" & ShowResults & "&Player=" & e.Row.Cells(3).Text
                hLink1.Text = e.Row.Cells(3).Text
                hLink1.ForeColor = White
                e.Row.Cells(3).Controls.Add(hLink1)
                e.Row.CssClass = "cell"
            Else
                Dim iCol As Integer
                For iCol = 0 To gridTeamStats.Columns.Count - 1
                    e.Row.Cells(iCol).ForeColor = DarkKhaki
                    e.Row.Cells(iCol).BackColor = gridTeamStats.BackColor
                Next
                If gRow = 1 Or gRow = 2 Then
                    e.Row.Cells(3).Font.Size = 8
                End If
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridPlayerStats_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridPlayerStats.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If gRow > 0 Then
                If Left(e.Row.Cells(2).Text, 5) = "Click" Then 'Add Click to view card etc
                    e.Row.Cells(2).ColumnSpan = 6
                    e.Row.Cells(2).ForeColor = Red
                    e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(0).BackColor = gridPlayerStats.BackColor
                    e.Row.Cells(2).BackColor = gridPlayerStats.BackColor
                    e.Row.Cells(3).Visible = False
                    e.Row.Cells(4).Visible = False
                    e.Row.Cells(5).Visible = False
                    e.Row.Cells(6).Visible = False
                Else    'row is a data row
                    Dim hLink1 As New HyperLink
                    hLink1.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & dt.Rows(gRow)(2) & "&ShowResults=" & ShowResults
                    hLink1.Text = e.Row.Cells(2).Text
                    hLink1.ForeColor = Cyan
                    e.Row.Cells(2).Controls.Add(hLink1)
                    e.Row.CssClass = "cell"

                    Dim hLink2 As New HyperLink
                    'hLink2.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Top10=" & Top10 & "&Team=" & objGlobals.TeamSelected & "&ShowResults=" & ShowResults & "&Player=" & objGlobals.PlayerSelected & "&CompID=" & Val(dt.Rows(gRow)(8))
                    hLink2.NavigateUrl = "~/Mens_Skit/Result Card.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&Player=" & objGlobals.PlayerSelected & "&CompID=" & Val(dt.Rows(gRow)(8))
                    hLink2.Text = e.Row.Cells(6).Text
                    hLink2.ForeColor = objGlobals.colour_result_foreground(e.Row.Cells(6).Text)
                    e.Row.Cells(6).Controls.Add(hLink2)
                    e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
                    e.Row.CssClass = "cell"
                    e.Row.Cells(6).ToolTip = "Click to view card"
                    e.Row.Cells(6).BackColor = objGlobals.colour_result_background(e.Row.Cells(6).Text)
                    'e.Row.Cells(6).ForeColor = objGlobals.colour_result_foreground(e.Row.Cells(6).Text)
                End If
            Else
                Dim iCol As Integer
                For iCol = 0 To gridPlayerStats.Columns.Count - 1
                    e.Row.Cells(iCol).ForeColor = DarkKhaki
                    e.Row.Cells(iCol).BackColor = gridPlayerStats.BackColor
                Next
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridPositionStats_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridPositionStats.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If gRow = 0 Then
                For iCol As Integer = 0 To gridPositionStats.Columns.Count - 1
                    e.Row.Cells(iCol).ForeColor = DarkKhaki
                    e.Row.Cells(iCol).BackColor = gridPositionStats.BackColor
                Next
            Else
                e.Row.Cells(0).ForeColor = White : e.Row.Cells(0).BackColor = DarkGreen
                e.Row.Cells(1).ForeColor = LightGreen : e.Row.Cells(1).BackColor = gridPositionStats.BackColor
                e.Row.Cells(2).ForeColor = Cyan : e.Row.Cells(2).BackColor = gridPositionStats.BackColor
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridResults_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResults.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Left(dt.Rows(gRow)(2).ToString, 4) <> "OPEN" And Left(dt.Rows(gRow)(2).ToString, 3) <> "BYE" Then
                Dim hLink As New HyperLink
                hLink.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(2).ToString & "&ShowResults=" & ShowResults
                hLink.Text = e.Row.Cells(2).Text
                hLink.ForeColor = Cyan
                e.Row.Cells(2).Controls.Add(hLink)
                'e.Row.CssClass = "row"

                If IsNumeric(e.Row.Cells(6).Text) Then
                    Dim hLink1 As New HyperLink
                    'hLink1.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&ShowResults=True&Player=" & objGlobals.PlayerSelected & "&CompID=" & e.Row.Cells(6).Text
                    hLink1.NavigateUrl = "~/Mens_Skit/Result Card.aspx?CompID=" & Val(dt.Rows(gRow)(6))
                    hLink1.Text = e.Row.Cells(4).Text
                    hLink1.ForeColor = objGlobals.colour_result_foreground(e.Row.Cells(4).Text)
                    e.Row.Cells(4).Controls.Add(hLink1)
                    e.Row.Cells(4).BackColor = objGlobals.colour_result_background(e.Row.Cells(4).Text)
                    'e.Row.Cells(4).ForeColor = objGlobals.colour_result_foreground(e.Row.Cells(4).Text)
                    'e.Row.CssClass = "row"
                    e.Row.Cells(6).Visible = False
                Else
                    e.Row.Cells(1).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                    e.Row.Cells(2).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                    e.Row.Cells(3).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                    e.Row.Cells(4).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                    e.Row.Cells(5).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                    e.Row.Cells(6).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
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
            End If
            gRow = gRow + 1
        End If
    End Sub



End Class
