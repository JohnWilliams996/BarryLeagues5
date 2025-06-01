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
    Private Result As String = ""
    Private home_result As String = ""

    Private CompID As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("clubs")
        objGlobals.CurrentUser = "clubs_user"
        objGlobals.CurrentSchema = "clubs."

        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
        objGlobals.LeagueSelected = Request.QueryString("League")
        CompID = Request.QueryString("CompID")

        Call load_options(gridOptions)
        Call load_table(gridTable)
        Call load_high_scores(gridHS)
        Call load_recent_results(gridRecentResults)
        Call load_late_results(gridLateResults)

    End Sub


    Sub load_fixture_result()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        strSQL = "SELECT *,CONVERT(VARCHAR(10),fixture_calendar,112) AS Fixture_YMD FROM clubs.vw_fixtures WHERE fixture_id=" & CompID
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            HomePoints = myDataReader.Item("home_points")
            AwayPoints = myDataReader.Item("away_points")
            home_result = Replace(myDataReader.Item("home_result"), " ", "")
            FixtureLeague = myDataReader.Item("league")
            FixtureDate = myDataReader.Item("fixture_ymd")
            FixtureHomeTeam = myDataReader.Item("home_team_name")
            FixtureAwayTeam = myDataReader.Item("away_team_name")
            HomePointsDeducted = myDataReader.Item("home_points_deducted")
            AwayPointsDeducted = myDataReader.Item("away_points_deducted")
            FixtureFullDate = "Date : " & myDataReader.Item("fixture_date")
        End While
        objGlobals.close_connection()

    End Sub

    Sub load_options(ByRef inGrid As GridView)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        strSQL = "SELECT long_name,home_night,venue FROM clubs.vw_teams WHERE league = '" & objGlobals.LeagueSelected & "' AND long_name <> 'BYE' ORDER BY long_name"
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
        objGlobals.close_connection()

        dt.Rows.Add("Competitions")
        Select Case Left(objGlobals.LeagueSelected, 4)
            Case "CRIB"
                dt.Rows.Add("CUP - TEAM KO")
                dt.Rows.Add("CUP - PAIRS")
            Case "SKIT"
                dt.Rows.Add("CUP - 12-A-SIDE TEAM KO")
                dt.Rows.Add("CUP - 6-A-SIDE TEAM KO")
            Case "SNOO"
                dt.Rows.Add("CUP - TEAM KO")
                dt.Rows.Add("CUP - SINGLES")
                dt.Rows.Add("CUP - DOUBLES")
                dt.Rows.Add("CUP - 3-A-SIDE")
        End Select
        gRow = 0
        inGrid.DataSource = dt
        inGrid.DataBind()

        If Left(objGlobals.LeagueSelected, 4) <> "SKIT" Then inGrid.Columns(2).Visible = False

    End Sub


    Sub load_late_results(ByRef inGrid As GridView)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        dt = New DataTable

        dt.Columns.Add(New DataColumn("Fixture Calendar", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Team Name", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Team Name", GetType(System.String)))

        strSQL = "SELECT fixture_short_date,fixture_calendar,home_team_name,away_team_name FROM clubs.vw_fixtures WHERE league = '" & objGlobals.LeagueSelected & "'"
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
        objGlobals.close_connection()

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
        dt.Columns.Add(New DataColumn("More", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture ID", GetType(System.String)))

        strSQL = "SELECT TOP 14 fixture_short_date,fixture_calendar,home_team_name,home_points,away_points, away_team_name, home_result,home_points_deducted,away_points_deducted,fixture_id "
        strSQL = strSQL & "FROM clubs.vw_fixtures WHERE league = '" & objGlobals.LeagueSelected & "' "
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
                If myDataReader.Item("home_points_deducted") > 0 And myDataReader.Item("away_points_deducted") > 0 Then
                    dr("Home Result") = "*" & myDataReader.Item("home_result") & "*"
                End If
                If myDataReader.Item("home_points_deducted") > 0 And myDataReader.Item("away_points_deducted") = 0 Then
                    dr("Home Result") = "*" & myDataReader.Item("home_result")
                End If
                If myDataReader.Item("home_points_deducted") = 0 And myDataReader.Item("away_points_deducted") > 0 Then
                    dr("Home Result") = myDataReader.Item("home_result") & "*"
                End If
                dr("Away Team Name") = myDataReader.Item("away_team_name")
                dr("More") = "View Card"
                dr("Fixture ID") = myDataReader.Item("fixture_id")
                dt.Rows.Add(dr)
            End With
        End While
        objGlobals.close_connection()


        inGrid.DataSource = dt
        inGrid.DataBind()
    End Sub

    Sub load_high_scores(ByRef inGrid As GridView)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        dt = New DataTable

        'add header row
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("HomeAway", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Score", GetType(System.String)))

        If Left(objGlobals.LeagueSelected, 4) = "CRIB" Then
            strSQL = "EXEC clubs.sp_Crib_High_Scores '" & objGlobals.current_season & "'"
        Else
            strSQL = "SELECT home_away,player,team,score FROM clubs.vw_high_scores WHERE league = '" & objGlobals.LeagueSelected & "'"
        End If
        myDataReader = objGlobals.SQLSelect(strSQL)

        If myDataReader.HasRows Then
            gRow = 0
            While myDataReader.Read()
                With inGrid
                    dr = dt.NewRow
                    If Left(objGlobals.LeagueSelected, 4) = "SNOO" Then
                        dr("HomeAway") = myDataReader.Item("home_away")
                    Else
                        dr("HomeAway") = ""
                    End If
                    dr("Player") = myDataReader.Item("player")
                    dr("Team") = myDataReader.Item("team")
                    If Left(objGlobals.LeagueSelected, 4) = "CRIB" And myDataReader.Item("Score") > 1 Then
                        dr("Score") = myDataReader.Item("Score").ToString + " ( x" + myDataReader.Item("Count").ToString + ")"
                    Else
                        dr("Score") = myDataReader.Item("Score")
                    End If
                    dt.Rows.Add(dr)
                End With
            End While
            inGrid.DataSource = dt
            inGrid.DataBind()
            If Left(objGlobals.LeagueSelected, 4) <> "SNOO" Then inGrid.HeaderRow.Cells(0).Text = ""
        End If
        objGlobals.close_connection()
    End Sub


    Sub load_table(ByRef inGrid As GridView)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

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
        dt.Columns.Add(New DataColumn("Deducted", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Pts", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Show Champions", GetType(System.String)))
        'dt.Columns.Add(New DataColumn("Number Nines", GetType(System.String)))

        strSQL = "SELECT a.Pos as Pos,a.Team as Team ,a.Pld as Pld, a.W as W, a.D as D, a.L as L ,a.Pts as Pts,a.Deducted as Deducted,b.show_champions as ShowChampions,''" ',c.Number_Nines as Number_Nines "
        strSQL = strSQL & "FROM clubs.vw_tables a "
        strSQL = strSQL & "INNER JOIN clubs.vw_leagues b ON b.League = '" & objGlobals.LeagueSelected & "' "
        'strSQL = strSQL & "LEFT OUTER JOIN clubs.vw_Skittles_Number_Nines c ON c.Team = a.Team" & " "
        strSQL = strSQL & "WHERE a.League = '" & objGlobals.LeagueSelected & "' ORDER BY a.Pos"
        myDataReader = objGlobals.SQLSelect(strSQL)
        gRow = 0
        Dim iRow As Integer = 0
        While myDataReader.Read()
            With inGrid
                iRow = iRow + 1
                dr = dt.NewRow
                dr("Last 6") = get_team_last_6(objGlobals.LeagueSelected, myDataReader.Item("team"))
                dr("Stats") = "Stats"
                dr("Pos") = myDataReader.Item("pos")
                dr("Team") = myDataReader.Item("team")
                dr("Pld") = myDataReader.Item("pld")
                dr("W") = myDataReader.Item("w")
                If Left(objGlobals.LeagueSelected, 4) <> "CRIB" Then
                    If myDataReader.Item("d") > 0 Then
                        dr("D") = myDataReader.Item("d")
                    Else
                        dr("D") = ""
                    End If
                Else
                    dr("D") = ""
                End If
                dr("L") = myDataReader.Item("l")
                If myDataReader.Item("deducted") > 0 Then
                    dr("Deducted") = myDataReader.Item("deducted") * -1
                End If
                dr("Pts") = myDataReader.Item("pts")
                If Left(objGlobals.LeagueSelected, 4) = "SKIT" Then
                    dr("Pts") = Format(Val(myDataReader.Item("pts")), "##0.0")
                End If
                If myDataReader.Item("pos") = 1 And myDataReader.Item("ShowChampions") = dr("Team") Then
                    dr("Show Champions") = "Y"
                Else
                    dr("Show Champions") = "N"
                End If
                'If Left(objGlobals.LeagueSelected, 4) = "SKIT" Then
                '    If Not IsDBNull(myDataReader.Item("number_nines")) Then
                '        If myDataReader.Item("number_nines") > 0 Then
                '            dr("Number Nines") = myDataReader.Item("number_nines")
                '        End If
                '    End If
                'End If
                dt.Rows.Add(dr)
            End With
        End While
        objGlobals.close_connection()

        gRow = 0
        inGrid.DataSource = dt
        inGrid.DataBind()

        txtInfo.Visible = False
        Dim labelColour As System.Drawing.Color
        Select Case Left(objGlobals.LeagueSelected, 4)
            Case "CRIB"
                labelColour = Yellow
            Case "SNOO"
                txtInfo.Text = "Snooker Teams:" + vbCrLf + "Deadline for registering new players is 31st December 2022"
                labelColour = LightGreen
            Case "SKIT"
                txtInfo.Text = "Skittles Teams:" + vbCrLf + "All games to now start at 8pm." + vbCrLf + "King William XII are now playing on Thursday's at the Football Club."
                labelColour = LightBlue
        End Select

        lblLeague.ForeColor = labelColour
        lblHighScores.ForeColor = labelColour
        lblRecentResults.ForeColor = labelColour
        lblLateResults.ForeColor = labelColour
        lblLeague.Text = objGlobals.LeagueSelected & " TABLE"

        hlLeagueStats.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected

    End Sub
    Function get_team_last_6(ByVal inLeague As String, inTeam As String) As String
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim ResultCount As Integer = 6
        Dim Result(6) As String
        get_team_last_6 = ""
        strSQL = "SELECT TOP 6 a.Week,a.Result FROM "
        strSQL = strSQL & "("
        strSQL = strSQL & "(SELECT week_number AS Week, CASE WHEN home_points<away_points THEN 'L' WHEN home_points>away_points THEN 'W' ELSE 'D' END AS Result FROM clubs.vw_fixtures "
        strSQL = strSQL & "WHERE league='" & inLeague & "' AND home_team_name='" & inTeam & "' AND home_result <> '0 - 0') "
        strSQL = strSQL & "UNION ALL "
        strSQL = strSQL & "(SELECT week_number AS Week, CASE WHEN home_points>away_points THEN 'L' WHEN home_points<away_points THEN 'W' ELSE 'D' END AS Result FROM clubs.vw_fixtures "
        strSQL = strSQL & "WHERE league='" & inLeague & "' AND away_team_name='" & inTeam & "' AND away_result <> '0 - 0') "
        strSQL = strSQL & ") AS a "
        strSQL = strSQL & "ORDER BY a.Week DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            Result(ResultCount) = myDataReader.Item("result")
            ResultCount = ResultCount - 1
        End While
        objGlobals.close_connection()

        get_team_last_6 = Result(1) + Result(2) + Result(3) + Result(4) + Result(5) + Result(6)

    End Function


    Private Sub gridOptions_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridOptions.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hLink As New HyperLink
            Dim hLink2 As New HyperLink
            If dt.Rows(gRow)(0).ToString = "LEAGUE TABLE" Then
                hLink.NavigateUrl = "~/Clubs/League Tables.aspx?League=" & objGlobals.LeagueSelected
                e.Row.Cells(0).ColumnSpan = 2
                e.Row.Cells(1).Visible = False
            ElseIf Left(dt.Rows(gRow)(0).ToString, 3) = "CUP" Then
                hLink.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=" & objGlobals.LeagueSelected & "&Comp=" & dt.Rows(gRow)(0).ToString
                e.Row.Cells(0).ColumnSpan = 2
                e.Row.Cells(1).Visible = False
            ElseIf Left(dt.Rows(gRow)(0).ToString, 4) = "FULL" Then
                hLink.NavigateUrl = "~/Clubs/League Fixtures.aspx?League=" & objGlobals.LeagueSelected
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
                hLink.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(0).ToString
                hLink2.NavigateUrl = "~/Clubs/Club Fixtures.aspx?Club=" & dt.Rows(gRow)(2)

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
            hLink0.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(3).ToString
            hLink0.Text = e.Row.Cells(0).Text
            hLink0.ForeColor = Black
            e.Row.Cells(0).Controls.Add(hLink0)
            e.Row.CssClass = "cell"

            Dim hLink As New HyperLink
            hLink.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(3).ToString
            hLink.Text = e.Row.Cells(3).Text
            hLink.ForeColor = Cyan
            e.Row.Cells(3).Controls.Add(hLink)
            e.Row.CssClass = "cell"

            If dt.Rows(gRow)(10).ToString = "Y" Then
                hLink.ForeColor = White
                e.Row.Cells(2).Text = "C"
                e.Row.Cells(2).ForeColor = White : e.Row.Cells(2).BackColor = DarkRed
                e.Row.Cells(3).ForeColor = White : e.Row.Cells(3).BackColor = DarkRed
                e.Row.Cells(4).ForeColor = White : e.Row.Cells(4).BackColor = DarkRed
                e.Row.Cells(5).ForeColor = White : e.Row.Cells(5).BackColor = DarkRed
                e.Row.Cells(6).ForeColor = White : e.Row.Cells(6).BackColor = DarkRed
                e.Row.Cells(7).ForeColor = White : e.Row.Cells(7).BackColor = DarkRed
                e.Row.Cells(8).ForeColor = White : e.Row.Cells(8).BackColor = DarkRed
                e.Row.Cells(9).ForeColor = White : e.Row.Cells(9).BackColor = DarkRed
            End If
            gRow = gRow + 1
        Else
            If Left(objGlobals.LeagueSelected, 4) = "CRIB" Then
                e.Row.Cells(6).Text = ""
            Else
                e.Row.Cells(6).Text = "D"
            End If
        End If
    End Sub

    Protected Sub gridRecentResults_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRecentResults.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If e.Row.Cells(0).Text <> "" Then
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(1).ToString
                hLink1.Text = dt.Rows(gRow)(1).ToString
                hLink1.ForeColor = Cyan
                e.Row.Cells(1).Controls.Add(hLink1)
                e.Row.CssClass = "cell"

                Dim hLink2 As New HyperLink
                hLink2.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(3).ToString
                hLink2.Text = dt.Rows(gRow)(3).ToString
                hLink2.ForeColor = Cyan
                e.Row.Cells(3).Controls.Add(hLink2)
                e.Row.CssClass = "cell"

                Dim hLink3 As New HyperLink
                'hLink3.NavigateUrl = "~/Clubs/League Tables.aspx?League=" & objGlobals.LeagueSelected & "&CompID=" & dt.Rows(gRow)(5).ToString
                hLink3.NavigateUrl = "~/Clubs/Result Card.aspx?CompID=" & dt.Rows(gRow)(5).ToString
                hLink3.ForeColor = LightGreen
                hLink3.Text = "View Card"
                e.Row.Cells(4).Controls.Add(hLink3)
                e.Row.CssClass = "cell"

                e.Row.Cells(5).Visible = False

                gRow = gRow + 1

            End If
        End If
    End Sub

    Protected Sub gridHS_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridHS.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hLink0 As New HyperLink
            hLink0.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(2).ToString & "&Player=" & dt.Rows(gRow)(1).ToString
            hLink0.Text = dt.Rows(gRow)(1).ToString
            hLink0.ForeColor = White
            e.Row.Cells(1).Controls.Add(hLink0)
            e.Row.CssClass = "cell"

            Dim hLink1 As New HyperLink
            hLink1.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(2).ToString
            hLink1.Text = dt.Rows(gRow)(2).ToString
            hLink1.ForeColor = Cyan
            e.Row.Cells(2).Controls.Add(hLink1)
            e.Row.CssClass = "cell"

            'e.Row.CssClass = "row"
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridLateResults_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridLateResults.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hLink1 As New HyperLink
            Dim hLink2 As New HyperLink
            hLink1.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(1).ToString
            hLink2.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(3).ToString

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

    Private Function CupPage(inComp As String) As String
        inComp = Replace(inComp, "CUP", "")
        inComp = Replace(inComp, "KO", "")
        CupPage = ""
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        If Left(objGlobals.LeagueSelected, 4) <> "SKIT" Then
            strSQL = "SELECT cup_page_on_web FROM clubs.vw_comps_web WHERE Competition = '" & objGlobals.LeagueSelected & inComp & "'"
        Else
            strSQL = "SELECT cup_page_on_web FROM clubs.vw_comps_web WHERE Competition = 'SKITTLES" & inComp & "'"
        End If
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            Return CStr(myDataReader.Item("cup_page_on_web"))
        End While
        objGlobals.close_connection()

    End Function

End Class
