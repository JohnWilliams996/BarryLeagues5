Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
'Imports MySql.Data.MySqlClient

Partial Class League_Tables
    Inherits System.Web.UI.Page
    Private dt As DataTable = New DataTable
    Private dr As DataRow = Nothing
    Private gRow As Integer
    Private objGlobals As New Globals
    Private ThisURL As String
    Private HomePlayersTotal As Integer
    Private AwayPlayersTotal As Integer
    Private HomePoints As Integer
    Private AwayPoints As Integer
    Private HomeRollsWon As Double
    Private AwayRollsWon As Double
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
    Private Result As String = ""
    Private home_result As String = ""

    Private FixtureStatus As Integer
    Private CompID As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("mens_skit")
        objGlobals.CurrentUser = "mens_skit_user"
        objGlobals.CurrentSchema = "mens_skit."
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
        objGlobals.LeagueSelected = Request.QueryString("League")
        CompID = Request.QueryString("CompID")
        Call load_options(gridOptions)
        If Not IsPostBack Then
            Call btnPoints_Click(Me, e)
            Call load_high_scores(gridHS)
            Call load_recent_results(gridRecentResults)
            Call load_late_results(gridLateResults)
        End If
    End Sub

    Protected Sub btnPoints_Click(sender As Object, e As System.EventArgs) Handles btnPoints.Click
        btnPoints.BackColor = Red
        btnRolls.BackColor = LightGray
        btn30s.BackColor = LightGray
        btnPins.BackColor = LightGray
        Call load_table(gridTable, "Points")
    End Sub

    Protected Sub btnRolls_Click(sender As Object, e As System.EventArgs) Handles btnRolls.Click
        btnPoints.BackColor = LightGray
        btnRolls.BackColor = Red
        btn30s.BackColor = LightGray
        btnPins.BackColor = LightGray
        Call load_table(gridTable, "Rolls")
    End Sub

    Protected Sub btn30s_Click(sender As Object, e As System.EventArgs) Handles btn30s.Click
        btnPoints.BackColor = LightGray
        btnRolls.BackColor = LightGray
        btn30s.BackColor = Red
        btnPins.BackColor = LightGray
        Call load_table(gridTable, "Away 30+")
    End Sub

    Protected Sub btnPins_Click(sender As Object, e As System.EventArgs) Handles btnPins.Click
        btnPoints.BackColor = LightGray
        btnRolls.BackColor = LightGray
        btn30s.BackColor = LightGray
        btnPins.BackColor = Red
        Call load_table(gridTable, "Pins")
    End Sub

    Sub load_options(ByRef inGrid As GridView)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        strSQL = "SELECT long_name,home_night,venue FROM mens_skit.vw_teams WHERE league = '" & objGlobals.LeagueSelected & "' AND long_name <> 'BYE' ORDER BY long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)

        dt = New DataTable

        dt.Columns.Add(New DataColumn("Long Name", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Night", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Venue", GetType(System.String)))
        dt.Rows.Add("LEAGUE TABLE")
        dt.Rows.Add("FULL LEAGUE FIXTURES")
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

        dt.Rows.Add("Competitions")
        'dt.Rows.Add("ALAN ROSSER CUP")
        dt.Rows.Add("ALLFORM CUP")
        dt.Rows.Add("HOLME TOWERS CUP (4 PIN)")
        'dt.Rows.Add("GARY MITCHELL CUP")

        gRow = 0
        inGrid.DataSource = dt
        inGrid.DataBind()
    End Sub

    Sub load_late_results(ByRef inGrid As GridView)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        dt = New DataTable

        dt.Columns.Add(New DataColumn("Fixture Calendar", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Team Name", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Team Name", GetType(System.String)))

        strSQL = "SELECT fixture_short_date,fixture_calendar,home_team_name,away_team_name FROM mens_skit.vw_fixtures WHERE league = '" & objGlobals.LeagueSelected & "'"
        strSQL = strSQL & " AND week_number <= " & objGlobals.GetCurrentWeek - 1
        strSQL = strSQL & " AND (home_points+away_points=0)"
        strSQL = strSQL & " AND home_team_name <> 'BYE'"
        strSQL = strSQL & " AND away_team_name <> 'BYE'"
        strSQL = strSQL & " AND ISNUMERIC(home_team) = 1"
        strSQL = strSQL & " ORDER BY fixture_calendar"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If Not myDataReader.HasRows Then Exit Sub
        gRow = 0
        While myDataReader.Read()
            With inGrid
                dr = dt.NewRow
                dr("Fixture Calendar") = myDataReader.Item("fixture_short_date")
                'dr("Fixture Calendar") = Format(myDataReader.Item("fixture_calendar"), "ddd ")
                'dr("Fixture Calendar") = dr("Fixture Calendar") + objGlobals.AddSuffix(Right(Format(myDataReader.Item("fixture_calendar"), "ddd d"), 2))
                'dr("Fixture Calendar") = dr("Fixture Calendar") + Format(myDataReader.Item("fixture_calendar"), " MMM")
                dr("Home Team Name") = myDataReader.Item("home_team_name")
                dr("Home Result") = "versus"
                dr("Away Team Name") = myDataReader.Item("away_team_name")
                dt.Rows.Add(dr)
            End With
        End While
        inGrid.DataSource = dt
        inGrid.DataBind()
    End Sub

    Sub load_recent_results(ByRef inGrid As GridView)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        dt = New DataTable

        dt.Columns.Add(New DataColumn("Fixture Calendar", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Team Name", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Team Name", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Rolls Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("More", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture ID", GetType(System.String)))

        strSQL = "SELECT TOP 14 fixture_short_date,fixture_calendar,home_team_name,home_points,away_points, away_team_name, home_result,fixture_id,home_rolls_result "
        strSQL = strSQL & "FROM mens_skit.vw_fixtures WHERE league = '" & objGlobals.LeagueSelected & "' "
        strSQL = strSQL & "AND (home_points + away_points > 0) "
        strSQL = strSQL & "ORDER BY fixture_calendar DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)

        gRow = 0
        Dim iRow As Integer = 0
        While myDataReader.Read()
            With inGrid
                iRow = iRow + 1
                dr = dt.NewRow
                dr("Fixture Calendar") = myDataReader.Item("fixture_short_date")
                dr("Home Team Name") = myDataReader.Item("home_team_name")
                dr("Home Result") = myDataReader.Item("home_result")
                dr("Away Team Name") = myDataReader.Item("away_team_name")
                dr("Home Rolls Result") = myDataReader.Item("home_rolls_result")
                dr("More") = "View Card"
                dr("Fixture ID") = myDataReader.Item("fixture_id")
                dt.Rows.Add(dr)
            End With
        End While


        inGrid.DataSource = dt
        inGrid.DataBind()
    End Sub

    Sub load_high_scores(ByRef inGrid As GridView)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        dt = New DataTable
        gRow = 0

        'add header row
        dr = dt.NewRow
        'dt.Columns.Add(New DataColumn("League", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Score", GetType(System.String)))

        dr = dt.NewRow
        dr("Team") = "Highest Score on Home Alley (All Divisions)"
        dt.Rows.Add(dr)

        strSQL = "SELECT * FROM mens_skit.vw_best_high_score_home"
        myDataReader = objGlobals.SQLSelect(strSQL)

        If myDataReader.HasRows Then
            While myDataReader.Read()
                With inGrid
                    dr = dt.NewRow
                    'dr("League") = myDataReader.Item("league")
                    dr("Team") = myDataReader.Item("team")
                    dr("Player") = myDataReader.Item("player")
                    dr("Score") = myDataReader.Item("Score")
                    dt.Rows.Add(dr)
                End With
            End While
        End If

        dr = dt.NewRow
        dr("Team") = "Highest Score on Away Alley (All Divisions)"
        dt.Rows.Add(dr)

        strSQL = "SELECT * FROM mens_skit.vw_best_high_score_away"
        myDataReader = objGlobals.SQLSelect(strSQL)

        If myDataReader.HasRows Then
            While myDataReader.Read()
                With inGrid
                    dr = dt.NewRow
                    'dr("League") = myDataReader.Item("league")
                    dr("Team") = myDataReader.Item("team")
                    dr("Player") = myDataReader.Item("player")
                    dr("Score") = myDataReader.Item("Score")
                    dt.Rows.Add(dr)
                End With
            End While
        End If

        dr = dt.NewRow
        dr("Team") = "Most Rolls (Division " + Right(objGlobals.LeagueSelected, 1) + ")"
        dt.Rows.Add(dr)

        strSQL = "SELECT * FROM mens_skit.vw_best_number_rolls WHERE league = '" & objGlobals.LeagueSelected & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        If myDataReader.HasRows Then
            While myDataReader.Read()
                With inGrid
                    dr = dt.NewRow
                    'dr("League") = myDataReader.Item("league")
                    dr("Team") = myDataReader.Item("team")
                    dr("Player") = ""
                    dr("Score") = myDataReader.Item("Score")
                    dt.Rows.Add(dr)
                End With
            End While
        End If

        dr = dt.NewRow
        dr("Team") = "Most Away 30+  (Division " + Right(objGlobals.LeagueSelected, 1) + ")"
        dt.Rows.Add(dr)

        strSQL = "SELECT * FROM mens_skit.vw_best_number_thirties WHERE league = '" & objGlobals.LeagueSelected & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        If myDataReader.HasRows Then
            While myDataReader.Read()
                With inGrid
                    dr = dt.NewRow
                    dr("Team") = myDataReader.Item("team")
                    dr("Player") = ""
                    dr("Score") = myDataReader.Item("Score")
                    dt.Rows.Add(dr)
                End With
            End While
        End If

        inGrid.DataSource = dt
        inGrid.DataBind()

    End Sub

    Sub load_table(ByRef inGrid As GridView, ByRef inSortBy As String)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim SortColumn As Integer
        dt = New DataTable

        'add header row
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Last 6", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Stats", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Pld", GetType(System.String)))
        dt.Columns.Add(New DataColumn("W", GetType(System.String)))
        dt.Columns.Add(New DataColumn("D", GetType(System.String)))
        dt.Columns.Add(New DataColumn("L", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Pts", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Show Champions", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Rolls", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Pins", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Number_Thirties", GetType(System.String)))

        strSQL = "SELECT a.Team as Team ,a.Pld as Pld, a.W as W, a.D as D, a.L as L ,a.Pts as Pts, a.Rolls as Rolls,a.Pts_Rolls_Pins as Pts_Rolls_Pins, b.show_champions as ShowChampions,"
        strSQL = strSQL & "a.pins as Pins,"
        strSQL = strSQL & "ISNULL(d.Number_Thirties,0) as Number_Thirties "
        strSQL = strSQL & "FROM mens_skit.vw_tables a "
        strSQL = strSQL & "INNER JOIN mens_skit.vw_leagues b ON b.League = '" & objGlobals.LeagueSelected & "' "
        strSQL = strSQL & "LEFT OUTER JOIN mens_skit.vw_Number_Thirties d ON d.Team = a.Team" & " "
        strSQL = strSQL & "WHERE a.League = '" & objGlobals.LeagueSelected & "' AND a.Team <> 'BYE'"
        Select Case inSortBy
            Case "Points" : strSQL = strSQL & " ORDER BY a.Pts_Rolls_Pins DESC" : SortColumn = 9
            Case "Rolls" : strSQL = strSQL & " ORDER BY a.Rolls DESC" : SortColumn = 10
            Case "Pins" : strSQL = strSQL & " ORDER BY a.Pins DESC" : SortColumn = 11
            Case "Away 30+" : strSQL = strSQL & " ORDER BY d.Number_Thirties DESC" : SortColumn = 12
        End Select
        myDataReader = objGlobals.SQLSelect(strSQL)
        gRow = 0
        Dim iRow As Integer = 0
        Dim CurrentPos As Integer = 0
        Dim SortValue As Double = 999999999
        Dim GridValue As Double
        While myDataReader.Read()
            Select Case inSortBy
                Case "Points" : GridValue = myDataReader.Item("Pts_Rolls_Pins")
                Case "Rolls" : GridValue = myDataReader.Item("Rolls")
                Case "Pins" : GridValue = myDataReader.Item("Pins")
                Case "Away 30+" : GridValue = myDataReader.Item("Number_Thirties")
            End Select
            With inGrid
                iRow = iRow + 1
                If GridValue < SortValue Then
                    CurrentPos = iRow
                    SortValue = GridValue
                End If
                dr = dt.NewRow
                dr("Last 6") = get_team_last_6(objGlobals.LeagueSelected, myDataReader.Item("team"))
                dr("Stats") = "Stats"
                dr("Pos") = CurrentPos
                dr("Team") = myDataReader.Item("team")
                dr("Pld") = myDataReader.Item("pld")
                dr("W") = myDataReader.Item("w")
                If myDataReader.Item("d") > 0 Then
                    dr("D") = myDataReader.Item("d")
                Else
                    dr("D") = ""
                End If


                dr("L") = myDataReader.Item("l")
                dr("Pts") = myDataReader.Item("pts")
                If CurrentPos = 1 And myDataReader.Item("ShowChampions") = dr("Team") And inSortBy = "Points" Then
                    dr("Show Champions") = "Y"
                Else
                    dr("Show Champions") = "N"
                End If
                dr("Rolls") = myDataReader.Item("Rolls")
                dr("Pins") = myDataReader.Item("Pins")
                If myDataReader.Item("Number_Thirties") > 0 Then
                    dr("Number_Thirties") = myDataReader.Item("Number_Thirties")
                End If
                dt.Rows.Add(dr)
            End With
        End While

        gRow = 0
        inGrid.DataSource = dt
        inGrid.DataBind()

        For iRow = 0 To inGrid.Rows.Count - 1
            Select Case inSortBy
                Case "Points"
                    inGrid.Rows(iRow).Cells(8).ForeColor = White
                    inGrid.Rows(iRow).Cells(8).BackColor = DarkGreen
                    inGrid.Rows(iRow).Cells(8).Font.Size = 10
                    inGrid.Rows(iRow).Cells(8).Font.Bold = True

                    inGrid.Rows(iRow).Cells(9).ForeColor = White
                    inGrid.Rows(iRow).Cells(9).BackColor = System.Drawing.Color.FromArgb(&H1B, &H1B, &H1B)
                    inGrid.Rows(iRow).Cells(9).Font.Size = 8

                    inGrid.Rows(iRow).Cells(10).ForeColor = White
                    inGrid.Rows(iRow).Cells(10).BackColor = System.Drawing.Color.FromArgb(&H1B, &H1B, &H1B)
                    inGrid.Rows(iRow).Cells(10).Font.Size = 8

                    inGrid.Rows(iRow).Cells(11).ForeColor = White
                    inGrid.Rows(iRow).Cells(11).BackColor = System.Drawing.Color.FromArgb(&H1B, &H1B, &H1B)
                    inGrid.Rows(iRow).Cells(11).Font.Size = 8
                Case "Rolls"
                    inGrid.Rows(iRow).Cells(8).ForeColor = White
                    inGrid.Rows(iRow).Cells(8).BackColor = System.Drawing.Color.FromArgb(&H1B, &H1B, &H1B)
                    inGrid.Rows(iRow).Cells(8).Font.Size = 8

                    inGrid.Rows(iRow).Cells(9).ForeColor = White
                    inGrid.Rows(iRow).Cells(9).BackColor = DarkGreen
                    inGrid.Rows(iRow).Cells(9).Font.Size = 10
                    inGrid.Rows(iRow).Cells(9).Font.Bold = True

                    inGrid.Rows(iRow).Cells(10).ForeColor = White
                    inGrid.Rows(iRow).Cells(10).BackColor = System.Drawing.Color.FromArgb(&H1B, &H1B, &H1B)
                    inGrid.Rows(iRow).Cells(10).Font.Size = 8

                    inGrid.Rows(iRow).Cells(11).ForeColor = White
                    inGrid.Rows(iRow).Cells(11).BackColor = System.Drawing.Color.FromArgb(&H1B, &H1B, &H1B)
                    inGrid.Rows(iRow).Cells(11).Font.Size = 8
                Case "Pins"
                    inGrid.Rows(iRow).Cells(8).ForeColor = White
                    inGrid.Rows(iRow).Cells(8).BackColor = System.Drawing.Color.FromArgb(&H1B, &H1B, &H1B)
                    inGrid.Rows(iRow).Cells(8).Font.Size = 8

                    inGrid.Rows(iRow).Cells(9).ForeColor = White
                    inGrid.Rows(iRow).Cells(9).BackColor = System.Drawing.Color.FromArgb(&H1B, &H1B, &H1B)
                    inGrid.Rows(iRow).Cells(9).Font.Size = 8

                    inGrid.Rows(iRow).Cells(10).ForeColor = White
                    inGrid.Rows(iRow).Cells(10).BackColor = DarkGreen
                    inGrid.Rows(iRow).Cells(10).Font.Size = 10
                    inGrid.Rows(iRow).Cells(10).Font.Bold = True

                    inGrid.Rows(iRow).Cells(11).ForeColor = White
                    inGrid.Rows(iRow).Cells(11).BackColor = System.Drawing.Color.FromArgb(&H1B, &H1B, &H1B)
                    inGrid.Rows(iRow).Cells(11).Font.Size = 8

                Case "Away 30+"
                    inGrid.Rows(iRow).Cells(8).ForeColor = White
                    inGrid.Rows(iRow).Cells(8).BackColor = System.Drawing.Color.FromArgb(&H1B, &H1B, &H1B)
                    inGrid.Rows(iRow).Cells(8).Font.Size = 8

                    inGrid.Rows(iRow).Cells(9).ForeColor = White
                    inGrid.Rows(iRow).Cells(9).BackColor = System.Drawing.Color.FromArgb(&H1B, &H1B, &H1B)
                    inGrid.Rows(iRow).Cells(9).Font.Size = 8

                    inGrid.Rows(iRow).Cells(10).ForeColor = White
                    inGrid.Rows(iRow).Cells(10).BackColor = System.Drawing.Color.FromArgb(&H1B, &H1B, &H1B)
                    inGrid.Rows(iRow).Cells(10).Font.Size = 8

                    inGrid.Rows(iRow).Cells(11).ForeColor = White
                    inGrid.Rows(iRow).Cells(11).BackColor = DarkGreen
                    inGrid.Rows(iRow).Cells(11).Font.Size = 10
                    inGrid.Rows(iRow).Cells(11).Font.Bold = True

            End Select
        Next


        Dim labelColour As System.Drawing.Color = LightBlue

        lblLeague.ForeColor = labelColour
        lblHighScores.ForeColor = labelColour
        lblRecentResults.ForeColor = labelColour
        lblLateResults.ForeColor = labelColour
        lblLeague.Text = objGlobals.LeagueSelected & " TABLE"

        hlLeagueStats.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected

    End Sub

    Function get_team_last_6(ByVal inLeague As String, inTeam As String) As String
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim ResultCount As Integer = 6
        Dim Result(6) As String
        get_team_last_6 = ""
        strSQL = "SELECT TOP 6 a.Week,a.Result FROM "
        strSQL = strSQL & "("
        strSQL = strSQL & "(SELECT week_number AS Week, CASE WHEN home_points<away_points THEN 'L' WHEN home_points>away_points THEN 'W' ELSE 'D' END AS Result FROM mens_skit.vw_fixtures "
        strSQL = strSQL & "WHERE league='" & inLeague & "' AND home_team_name='" & inTeam & "' AND home_result <> '0 - 0') "
        strSQL = strSQL & "UNION ALL "
        strSQL = strSQL & "(SELECT week_number AS Week, CASE WHEN home_points>away_points THEN 'L' WHEN home_points<away_points THEN 'W' ELSE 'D' END AS Result FROM mens_skit.vw_fixtures "
        strSQL = strSQL & "WHERE league='" & inLeague & "' AND away_team_name='" & inTeam & "' AND away_result <> '0 - 0') "
        strSQL = strSQL & ") AS a "
        strSQL = strSQL & "ORDER BY a.Week DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            Result(ResultCount) = myDataReader.Item("result")
            ResultCount = ResultCount - 1
        End While
        get_team_last_6 = Result(1) + Result(2) + Result(3) + Result(4) + Result(5) + Result(6)

    End Function

    Private Sub gridOptions_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridOptions.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hLink As New HyperLink
            Dim hLink2 As New HyperLink

            If dt.Rows(gRow)(0).ToString = "LEAGUE TABLE" Then
                hLink.NavigateUrl = "~/Mens_Skit/League Tables.aspx?League=" & objGlobals.LeagueSelected
                e.Row.Cells(0).ColumnSpan = 2
                e.Row.Cells(1).Visible = False
            ElseIf dt.Rows(gRow)(0).ToString = "ALLFORM CUP" Or dt.Rows(gRow)(0).ToString = "HOLME TOWERS CUP (4 PIN)" Or dt.Rows(gRow)(0).ToString = "GARY MITCHELL CUP" Then
                hLink.NavigateUrl = "~/Mens_Skit/Cup Fixtures List.aspx?Comp=" & dt.Rows(gRow)(0).ToString
                e.Row.Cells(0).ColumnSpan = 2
                e.Row.Cells(1).Visible = False
            ElseIf dt.Rows(gRow)(0).ToString = "ALAN ROSSER CUP" Then
                hLink.NavigateUrl = "~/Mens_Skit/Alan Rosser Cup.aspx?Group=PLAYOFFS"
                e.Row.Cells(0).ColumnSpan = 2
                e.Row.Cells(1).Visible = False
            ElseIf Left(dt.Rows(gRow)(0).ToString, 4) = "FULL" Then
                hLink.NavigateUrl = "~/Mens_Skit/League Fixtures.aspx?League=" & objGlobals.LeagueSelected
                e.Row.Cells(0).ColumnSpan = 2
                e.Row.Cells(1).Visible = False
            Else
                If dt.Rows(gRow)(0) = "Team" Or dt.Rows(gRow)(0) = "Competitions" Then
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
                hLink.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(0).ToString
                hLink2.NavigateUrl = "~/Mens_Skit/Club Fixtures.aspx?Club=" & dt.Rows(gRow)(2)
            End If
            If e.Row.Cells(0).Text <> "Team" And dt.Rows(gRow)(0) <> "Competitions" Then
                hLink.Text = e.Row.Cells(0).Text
                hLink.ForeColor = Cyan
                e.Row.Cells(0).Controls.Add(hLink)

                hLink2.Text = e.Row.Cells(2).Text
                hLink2.ForeColor = White
                e.Row.Cells(2).Controls.Add(hLink2)

                e.Row.CssClass = "cell"
            End If
            gRow = gRow + 1
        End If
    End Sub


    Private Sub gridTable_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTable.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hLink0 As New HyperLink
            hLink0.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(3).ToString
            hLink0.Text = e.Row.Cells(0).Text
            hLink0.ForeColor = Black
            e.Row.Cells(0).Controls.Add(hLink0)
            e.Row.CssClass = "cell"

            Dim hLink As New HyperLink
            hLink.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(3).ToString
            hLink.Text = e.Row.Cells(3).Text
            hLink.ForeColor = Cyan
            e.Row.Cells(3).Controls.Add(hLink)
            e.Row.CssClass = "cell"

            If dt.Rows(gRow)(9).ToString = "Y" Then
                hLink.ForeColor = White
                e.Row.CssClass = "cell"
                e.Row.Cells(2).Text = "C"
                'e.Row.ForeColor = White
                'e.Row.BackColor = DarkRed
                e.Row.Cells(2).ForeColor = White : e.Row.Cells(2).BackColor = DarkRed
                e.Row.Cells(3).ForeColor = White : e.Row.Cells(3).BackColor = DarkRed
                e.Row.Cells(4).ForeColor = White : e.Row.Cells(4).BackColor = DarkRed
                e.Row.Cells(5).ForeColor = White : e.Row.Cells(5).BackColor = DarkRed
                e.Row.Cells(6).ForeColor = White : e.Row.Cells(6).BackColor = DarkRed
                e.Row.Cells(7).ForeColor = White : e.Row.Cells(7).BackColor = DarkRed
                e.Row.Cells(8).ForeColor = White : e.Row.Cells(8).BackColor = DarkRed
                e.Row.Cells(9).ForeColor = White : e.Row.Cells(9).BackColor = DarkRed
                e.Row.Cells(10).ForeColor = White : e.Row.Cells(10).BackColor = DarkRed
                e.Row.Cells(11).ForeColor = White : e.Row.Cells(11).BackColor = DarkRed
                'e.Row.Cells(12).ForeColor = White : e.Row.Cells(12).BackColor = DarkRed
                'e.Row.Cells(13).ForeColor = White : e.Row.Cells(13).BackColor = DarkRed
            End If
            gRow = gRow + 1
        Else
            e.Row.Cells(6).Text = "D"
        End If
    End Sub


    Protected Sub gridRecentResults_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRecentResults.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If e.Row.Cells(0).Text <> "" Then
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(1).ToString
                hLink1.Text = dt.Rows(gRow)(1).ToString
                hLink1.ForeColor = Cyan
                e.Row.Cells(1).Controls.Add(hLink1)
                e.Row.CssClass = "cell"
                'hLink1.Attributes.Add("onmouseover", "this.ClassName='cell'")

                Dim hLink2 As New HyperLink
                hLink2.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(3).ToString
                hLink2.Text = dt.Rows(gRow)(3).ToString
                hLink2.ForeColor = Cyan
                e.Row.Cells(3).Controls.Add(hLink2)
                e.Row.CssClass = "cell"

                Dim hLink3 As New HyperLink
                'hLink3.NavigateUrl = "~/Mens_Skit/League Tables.aspx?League=" & objGlobals.LeagueSelected & "&CompID=" & dt.Rows(gRow)(6).ToString
                hLink3.NavigateUrl = "~/Mens_Skit/Result Card.aspx?CompID=" & dt.Rows(gRow)(6).ToString
                hLink3.ForeColor = LightGreen
                hLink3.Text = "View Card"
                e.Row.Cells(5).Controls.Add(hLink3)
                e.Row.CssClass = "cell"

                e.Row.Cells(6).Visible = False

                gRow = gRow + 1
            End If

        End If
    End Sub

    Protected Sub gridHS_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridHS.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Left(dt.Rows(gRow)(0).ToString, 7) = "Highest" Or Left(dt.Rows(gRow)(0).ToString, 4) = "Most" Then
                e.Row.Cells(0).ColumnSpan = 2
                e.Row.Cells(0).ForeColor = System.Drawing.Color.FromArgb(&HE4, &HBB, &H18)
                e.Row.Cells(2).BackColor = System.Drawing.Color.FromArgb(&H1B, &H1B, &H1B)
                'End If
            Else
                'If Left(dt.Rows(gRow)(0).ToString, 3) = "All" Or Left(dt.Rows(gRow)(0).ToString, 4) = "DIVI" Then
                Dim hLink1 As New HyperLink
                Dim hLink2 As New HyperLink
                hLink1.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & fnGetLeague(dt.Rows(gRow)(0).ToString) & "&Team=" & dt.Rows(gRow)(0).ToString
                hLink2.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & fnGetLeague(dt.Rows(gRow)(0).ToString) & "&Team=" & dt.Rows(gRow)(0).ToString & "&Player=" & dt.Rows(gRow)(1).ToString
                hLink1.Text = e.Row.Cells(0).Text
                hLink1.ForeColor = Cyan
                e.Row.Cells(0).Controls.Add(hLink1)

                hLink2.Text = e.Row.Cells(1).Text
                hLink2.ForeColor = White
                e.Row.Cells(1).Controls.Add(hLink2)
            End If
            'e.Row.CssClass = "row"
            gRow = gRow + 1
        End If
    End Sub

    Private Function fnGetLeague(inTeam As String) As String
        fnGetLeague = ""
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        strSQL = "SELECT league  FROM mens_skit.vw_teams WHERE long_name = '" & inTeam & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            fnGetLeague = myDataReader.Item("league")
        End While
    End Function

    Protected Sub gridLateResults_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridLateResults.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hLink1 As New HyperLink
            Dim hLink2 As New HyperLink
            hLink1.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(1).ToString
            hLink2.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(3).ToString

            hLink1.Text = e.Row.Cells(1).Text
            hLink1.ForeColor = Cyan
            e.Row.Cells(1).Controls.Add(hLink1)

            hLink2.Text = e.Row.Cells(3).Text
            hLink2.ForeColor = Cyan
            e.Row.Cells(3).Controls.Add(hLink2)

            'e.Row.CssClass = "row"
            gRow = gRow + 1
        End If
    End Sub



End Class
