Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Diagnostics
Imports System.Net

'Imports MySql.Data.MySqlClient

Partial Class Team_Fixtures
    Inherits System.Web.UI.Page

    Private dt As DataTable
    Private dr As DataRow
    Private gRow As Integer
    Private objGlobals As New Globals
    Private Pts(52) As Double
    Private Pos(52) As Double
    Private ThisURL As String
    Private HomePlayersTotal As Integer
    Private AwayPlayersTotal As Integer
    Private HomePoints As Integer
    Private AwayPoints As Integer
    Private HomeRollsWon As Double
    Private AwayRollsWon As Double
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
    Private FixtureType As String


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
        ThisURL = Request.RawUrl
        objGlobals.TeamSelected = Request.QueryString("Team")
        objGlobals.LeagueSelected = get_league_from_team(Request.QueryString("League"))
        CompID = Request.QueryString("CompID")

        Call load_options(gridOptions)
        Call load_title()
        If CompID = 0 Then
            Call load_fixtures1(gridFixtures1)
        End If

        dt = New DataTable
        dt.Columns.Add(New DataColumn("Players", GetType(System.String)))
        dt.Columns.Add(New DataColumn("In/Out", GetType(System.Int32)))

        Call load_players()
        gRow = 0
        gridPlayers.ShowHeader = False
        gridPlayers.DataSource = dt
        gridPlayers.DataBind()

    End Sub

    Function get_league_from_team(ByVal inLeague As String) As String
        get_league_from_team = ""
        Dim strSQL As String = ""
        Dim myDataReader As oledbdatareader
        strSQL = "SELECT league FROM mens_skit.vw_teams WHERE long_name = '" & objGlobals.TeamSelected & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            get_league_from_team = myDataReader.Item("league")
        End While
    End Function

    Sub load_fixture_result()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        If FixtureType = "League" Then
            strSQL = "SELECT *,CONVERT(VARCHAR(10),fixture_calendar,112) AS Fixture_YMD FROM mens_skit.vw_fixtures WHERE fixture_id=" & CompID
        Else
            strSQL = "SELECT *,CONVERT(VARCHAR(10),fixture_calendar,112) AS Fixture_YMD FROM mens_skit.vw_fixtures_AR WHERE fixture_id=" & CompID
        End If
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            HomePoints = myDataReader.Item("home_points")
            AwayPoints = myDataReader.Item("away_points")
            HomeRollsWon = myDataReader.Item("home_rolls_won")
            AwayRollsWon = myDataReader.Item("away_rolls_won")
            home_roll_1 = myDataReader.Item("home_roll_1")
            home_roll_2 = myDataReader.Item("home_roll_2")
            home_roll_3 = myDataReader.Item("home_roll_3")
            home_roll_4 = myDataReader.Item("home_roll_4")
            home_roll_5 = myDataReader.Item("home_roll_5")
            home_roll_total = home_roll_1 + home_roll_2 + home_roll_3 + home_roll_4 + home_roll_5
            away_roll_1 = myDataReader.Item("away_roll_1")
            away_roll_2 = myDataReader.Item("away_roll_2")
            away_roll_3 = myDataReader.Item("away_roll_3")
            away_roll_4 = myDataReader.Item("away_roll_4")
            away_roll_5 = myDataReader.Item("away_roll_5")
            away_roll_total = away_roll_1 + away_roll_2 + away_roll_3 + away_roll_4 + away_roll_5

            home_result = Replace(myDataReader.Item("home_result"), " ", "")
            FixtureLeague = myDataReader.Item("league")
            FixtureDate = myDataReader.Item("fixture_ymd")
            FixtureHomeTeam = myDataReader.Item("home_team_name")
            FixtureAwayTeam = myDataReader.Item("away_team_name")
            FixtureFullDate = "Date : " & myDataReader.Item("fixture_date")
            FixtureStatus = myDataReader.Item("status")
        End While
    End Sub

    Sub load_title()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        lblTeam.Text = objGlobals.TeamSelected
        lblLeague.Text = objGlobals.LeagueSelected
        strSQL = "SELECT * FROM mens_skit.vw_tables WHERE league='" & objGlobals.LeagueSelected & "' AND team='" & objGlobals.TeamSelected & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            lblStatus.Text = "Current Pos : " & myDataReader.Item("pos") & vbTab & "Pld : " & myDataReader.Item("pld") & vbTab & "Pts : " & myDataReader.Item("pts")
        End While
        lblTeam.ForeColor = LightBlue
        lblLeague.ForeColor = LightBlue
        hlTeamStats.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected
        hlLeagueStats.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected
    End Sub

    Sub get_week_pts_pld()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim Wk As Integer

        strSQL = "SELECT Week,pts,pos FROM mens_skit.vw_tables_week"
        strSQL = strSQL & " WHERE league = '" & objGlobals.LeagueSelected & "'"
        strSQL = strSQL & " AND team = '" & objGlobals.TeamSelected & "'"
        strSQL = strSQL & " ORDER BY week"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            Wk = myDataReader.Item(0)
            Pts(Wk) = myDataReader.Item(1)
            Pos(Wk) = myDataReader.Item(2)
        End While
    End Sub

    Sub load_fixtures1(inGrid As GridView)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim Wk As Integer
        Dim CurrentWeek As Integer = objGlobals.GetCurrentWeek
        Dim home_venue As String = ""

        'store the total points and position at then end of each week
        Call get_week_pts_pld()

        dt = New DataTable

        dt.Columns.Add(New DataColumn("Week Number", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture Calendar", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League Cup", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Team Name", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Venue", GetType(System.String)))
        dt.Columns.Add(New DataColumn("", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Rolls Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Points", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League Position", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture ID", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture ID2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Status", GetType(System.String)))

        strSQL = "SELECT venue FROM mens_skit.vw_teams WHERE league = '" & objGlobals.LeagueSelected & "' AND long_name = '" & objGlobals.TeamSelected & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            home_venue = myDataReader.Item("venue")
        End While
        objGlobals.close_connection()

        strSQL = "EXEC mens_skit.sp_get_team_fixtures '" & objGlobals.get_current_season & "','" & objGlobals.LeagueSelected & "','" & objGlobals.TeamSelected & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            dr = dt.NewRow
            Wk = myDataReader.Item("week_number")
            dr("Week Number") = Wk
            dr("Fixture Calendar") = myDataReader.Item("fixture_short_date")
            dr("League Cup") = myDataReader.Item("league")
            Select Case myDataReader.Item("home_team_name")
                Case objGlobals.TeamSelected
                    If myDataReader.Item("away_team_name") = "BYE" Then
                        dr("Home Result") = ""
                    Else
                        dr("Home Team Name") = myDataReader.Item("away_team_name")
                        If myDataReader.Item("venue") = home_venue Then
                            dr("Venue") = "HOME"
                        Else
                            dr("Venue") = myDataReader.Item("venue")
                        End If
                        If myDataReader.Item("home_result") = "WON" Or myDataReader.Item("home_result") = "LOST" Then
                            dr("Home Result") = myDataReader.Item("home_result")
                        ElseIf myDataReader.Item("home_result") = "versus" Then
                            dr("Home Result") = ""
                        Else
                            Select Case myDataReader.Item("home_points") - myDataReader.Item("away_points")
                                Case Is > 0 : dr("Home Result") = "W " & Replace(myDataReader.Item("home_result"), " ", "")
                                Case Is < 0 : dr("Home Result") = "L " & Replace(myDataReader.Item("home_result"), " ", "")
                                Case 0
                                    If myDataReader.Item("fixture_id") > 0 Then
                                        dr("Home Result") = "D " & Replace(myDataReader.Item("home_result"), " ", "")
                                    Else
                                        dr("Home Result") = ""
                                    End If
                            End Select
                        End If
                        If myDataReader.Item("status") >= 1 Then
                            Select Case myDataReader.Item("home_rolls_won") - myDataReader.Item("away_rolls_won")
                                Case Is > 0 : dr("Rolls Result") = "W " & Replace(myDataReader.Item("home_rolls_result"), " ", "")
                                Case Is < 0 : dr("Rolls Result") = "L " & Replace(myDataReader.Item("home_rolls_result"), " ", "")
                                Case 0
                                    If myDataReader.Item("fixture_id") > 0 Then
                                        dr("Rolls Result") = "D " & Replace(myDataReader.Item("home_rolls_result"), " ", "")
                                    Else
                                        dr("Rolls Result") = ""
                                    End If
                            End Select
                        End If
                        If myDataReader.Item("home_result") = "0 - 0" Then dr("Home Result") = ""
                        If myDataReader.Item("status") = -1 Then dr("Home Result") = "Postponed"
                        If objGlobals.AdminLogin And myDataReader.Item("fixture_id") <> 0 Then
                            dr("Fixture ID") = myDataReader.Item("fixture_id")
                            dr("Fixture ID2") = myDataReader.Item("fixture_id")
                        Else
                            dr("Fixture ID") = ""
                            If myDataReader.Item("status") > 0 And myDataReader.Item("fixture_id") <> 0 Then
                                dr("Fixture ID") = "View Card"
                                dr("Fixture ID2") = myDataReader.Item("fixture_id")
                            End If
                        End If
                        dr("Status") = myDataReader.Item("status")
                    End If
                Case "BYE"
                    dr("Home Team Name") = "BYE WEEK"
                    dr("Home Result") = ""
                Case Else
                    dr("Home Team Name") = myDataReader.Item("home_team_name")
                    dr("Venue") = myDataReader.Item("venue")
                    If myDataReader.Item("home_result") = "WON" Or myDataReader.Item("home_result") = "LOST" Then
                        dr("Home Result") = myDataReader.Item("home_result")
                    ElseIf myDataReader.Item("home_result") = "versus" Then
                        dr("Home Result") = ""
                    Else
                        Select Case myDataReader.Item("away_points") - myDataReader.Item("home_points")
                            Case Is > 0 : dr("Home Result") = "W " & Replace(myDataReader.Item("away_result"), " ", "")
                            Case Is < 0 : dr("Home Result") = "L " & Replace(myDataReader.Item("away_result"), " ", "")
                            Case 0
                                If myDataReader.Item("fixture_id") > 0 Then
                                    dr("Home Result") = "D " & Replace(myDataReader.Item("away_result"), " ", "")
                                Else
                                    dr("Home Result") = ""
                                End If
                        End Select
                    End If
                    If myDataReader.Item("away_result") = "0 - 0" Then dr("Home Result") = ""
                    If myDataReader.Item("status") = -1 Then dr("Home Result") = "Postponed"
                    If myDataReader.Item("status") >= 1 Then
                        Select Case myDataReader.Item("away_rolls_won") - myDataReader.Item("home_rolls_won")
                            Case Is > 0 : dr("Rolls Result") = "W " & Replace(myDataReader.Item("away_rolls_result"), " ", "")
                            Case Is < 0 : dr("Rolls Result") = "L " & Replace(myDataReader.Item("away_rolls_result"), " ", "")
                            Case 0
                                If myDataReader.Item("fixture_id") > 0 Then
                                    dr("Rolls Result") = "D " & Replace(myDataReader.Item("away_rolls_result"), " ", "")
                                Else
                                    dr("Rolls Result") = ""
                                End If
                        End Select
                    End If
                    If objGlobals.AdminLogin And myDataReader.Item("fixture_id") <> 0 Then
                        dr("Fixture ID") = myDataReader.Item("fixture_id")
                        dr("Fixture ID2") = myDataReader.Item("fixture_id")
                    Else
                        dr("Fixture ID") = ""
                        If myDataReader.Item("status") > 0 And myDataReader.Item("fixture_id") <> 0 Then
                            dr("Fixture ID") = "View Card"
                            dr("Fixture ID2") = myDataReader.Item("fixture_id")
                        End If
                    End If
                    dr("Status") = myDataReader.Item("status")
            End Select
            If myDataReader.Item("away_team_name") = "BYE" Then
                dr("Home Team Name") = "BYE WEEK"
                dr("Venue") = ""
            End If
            If myDataReader.Item("fixture_type") = "L" And dr("Home Result") <> "" Then
                dr("Total Points") = Pts(Wk)
                dr("League Position") = Pos(Wk)
            End If
            dt.Rows.Add(dr)

        End While
        gRow = 0
        inGrid.DataSource = dt
        inGrid.DataBind()
        objGlobals.close_connection()

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

        strSQL = "SELECT player FROM mens_skit.vw_players WHERE league = '" & objGlobals.LeagueSelected & "' AND team = '" & objGlobals.TeamSelected & "' AND CHARINDEX( 'A N OTHER',Player) = 0  ORDER BY LEFT(Player,3)"
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


    Protected Sub gridPlayers_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridPlayers.RowDataBound
        Dim SinglesEntry As Boolean = False
        Dim DoublesEntry As Boolean = False
        Dim TriplesEntry As Boolean = False
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If dt.Rows(gRow)(0) <> "" Then
                If InStr(dt.Rows(gRow)(0), "Players", CompareMethod.Text) > 0 Or InStr(dt.Rows(gRow)(0), "Singles", CompareMethod.Text) > 0 _
                    Or InStr(dt.Rows(gRow)(0), "Doubles", CompareMethod.Text) > 0 Or InStr(dt.Rows(gRow)(0), "Pairs", CompareMethod.Text) > 0 _
                    Or InStr(dt.Rows(gRow)(0), "3-a-Side", CompareMethod.Text) > 0 Or InStr(dt.Rows(gRow)(0), "(Click", CompareMethod.Text) > 0 Then
                    e.Row.Cells(0).ForeColor = System.Drawing.Color.FromArgb(&HE4, &HBB, &H18)
                Else
                    If InStr(dt.Rows(gRow)(0), "&", CompareMethod.Text) > 0 Then
                        DoublesEntry = True
                    ElseIf InStr(dt.Rows(gRow)(0), "/", CompareMethod.Text) > 0 Then
                        TriplesEntry = True
                    Else
                        SinglesEntry = True
                    End If
                    Select Case True
                        Case SinglesEntry
                            Dim hLink As New HyperLink
                            hLink.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & lblTeam.Text & "&Player=" & dt.Rows(gRow)(0)
                            hLink.Text = e.Row.Cells(0).Text
                            hLink.ForeColor = White
                            e.Row.Cells(0).Controls.Add(hLink)
                            If dt.Rows(gRow)(1) = "0" Then
                                hLink.ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                            End If
                        Case DoublesEntry, TriplesEntry
                            If dt.Rows(gRow)(1) = "0" Then
                                e.Row.Cells(0).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                            End If
                    End Select
                End If
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridFixtures1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridFixtures1.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Left(dt.Rows(gRow)(1).ToString, 3) <> "W/C" And Left(dt.Rows(gRow)(3).ToString, 3) <> "BYE" Then
                Dim hLink As New HyperLink
                hLink.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(3).ToString
                hLink.Text = e.Row.Cells(3).Text
                hLink.ForeColor = Cyan
                e.Row.Cells(3).Controls.Add(hLink)

                Dim Status As Integer = Val(dt.Rows(gRow)(13))
                If Status > 0 Or e.Row.Cells(5).Text = "WON" Or e.Row.Cells(5).Text = "LOST" Then
                    e.Row.Cells(5).BackColor = objGlobals.colour_result_background(e.Row.Cells(5).Text)
                    e.Row.Cells(5).ForeColor = objGlobals.colour_result_foreground(e.Row.Cells(5).Text)
                End If
                If Status = -1 Then
                    e.Row.Cells(5).ForeColor = Red
                    e.Row.Cells(5).BackColor = Black
                End If

                If Not IsDBNull(dt.Rows(gRow)(12)) Then
                    If dt.Rows(gRow)(12) <> "" Then
                        Dim hLink3 As New HyperLink
                        Select Case Status
                            Case 2
                                If objGlobals.AdminLogin Then
                                    hLink3.NavigateUrl = "~/Mens_Skit/Admin/Add Edit Result 2.aspx?ID=" & Val(dt.Rows(gRow)(12)).ToString & "&Week=" & Val(dt.Rows(gRow)(0)).ToString & "&League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected
                                Else
                                    If InStr(dt.Rows(gRow)(2), "ROSSER", CompareMethod.Text) = 0 Then
                                        'hLink3.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&CompID=" & Val(dt.Rows(gRow)(12)).ToString & "&FixtureType=League"
                                        hLink3.NavigateUrl = "~/Mens_Skit/Result Card.aspx?CompID=" & Val(dt.Rows(gRow)(12)).ToString & "&FixtureType=League"
                                    Else
                                        hLink3.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&CompID=" & Val(dt.Rows(gRow)(12)).ToString & "&FixtureType=ARosser"
                                    End If
                                End If
                                hLink3.ForeColor = LightGreen
                            Case 1
                                If objGlobals.AdminLogin Then
                                    hLink3.NavigateUrl = "~/Mens_Skit/Admin/Add Edit Result 2.aspx?ID=" & Val(dt.Rows(gRow)(12)).ToString & "&Week=" & Val(dt.Rows(gRow)(0)).ToString & "&League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected
                                Else
                                    If InStr(dt.Rows(gRow)(2), "ROSSER", CompareMethod.Text) = 0 Then
                                        'hLink3.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&CompID=" & Val(dt.Rows(gRow)(12)).ToString & "&FixtureType=League"
                                        hLink3.NavigateUrl = "~/Mens_Skit/Result Card.aspx?CompID=" & Val(dt.Rows(gRow)(12)).ToString & "&FixtureType=League"
                                    Else
                                        hLink3.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&CompID=" & Val(dt.Rows(gRow)(12)).ToString & "&FixtureType=ARosser"
                                    End If
                                End If
                                hLink3.ForeColor = Orange
                            Case 0
                                hLink3.NavigateUrl = "~/Mens_Skit/Admin/Fixture Result.aspx?ID=" & Val(dt.Rows(gRow)(12)).ToString & "&Week=" & Val(dt.Rows(gRow)(0)).ToString & "&League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected
                                hLink3.ForeColor = White
                            Case -1
                                If InStr(dt.Rows(gRow)(2), "ROSSER", CompareMethod.Text) = 0 Then
                                    hLink3.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&CompID=" & Val(dt.Rows(gRow)(12)).ToString & "&FixtureType=League"
                                Else
                                    hLink3.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&CompID=" & Val(dt.Rows(gRow)(12)).ToString & "&FixtureType=ARosser"
                                End If
                                hLink3.ForeColor = White
                        End Select

                        hLink3.Text = e.Row.Cells(10).Text
                        e.Row.Cells(10).Controls.Add(hLink3)
                    End If
                End If
            Else
                If Left(dt.Rows(gRow)(3).ToString, 3) = "BYE" Then
                    e.Row.Cells(0).ForeColor = Gray
                    e.Row.Cells(2).ForeColor = Gray
                End If
                e.Row.Cells(1).ForeColor = Gray
                e.Row.Cells(3).ForeColor = Gray
                e.Row.Cells(3).ForeColor = Gray
                e.Row.Cells(4).Text = ""
                e.Row.Cells(5).Text = ""
                e.Row.Cells(11).Text = ""
            End If
            gRow = gRow + 1
        End If
    End Sub

    Sub write_PDF_download(ByVal inFilepath As String)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim l_param_in_names(2) As String
        Dim l_param_in_values(2) As String

        l_param_in_names(0) = "@inLeague"
        l_param_in_values(0) = objGlobals.LeagueSelected
        l_param_in_names(1) = "@inTeam"
        l_param_in_values(1) = objGlobals.TeamSelected
        l_param_in_names(2) = "@inFilepath"
        l_param_in_values(2) = Replace(inFilepath, "'", """")

        strSQL = "EXEC [mens_skit].[sp_write_download_PDF] '" & l_param_in_values(0) & "','" & l_param_in_values(1) & "','" & l_param_in_values(2) & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        objGlobals.close_connection()
    End Sub

    Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Dim FilePath As String = Server.MapPath("TeamFixtures") & "\"
        Dim filename As String = Replace(objGlobals.TeamSelected, "'", """") & " " & objGlobals.LeagueSelected & ".pdf"
        Dim PDFfile As String = FilePath + filename

        'write details for PDF_downloads
        write_PDF_download(PDFfile)

        Dim client As New WebClient
        Dim ByteArray As Byte() = client.DownloadData(PDFfile)

        Response.Clear()
        'Send the file to the output stream
        Response.Buffer = True
        'Try and ensure the browser always opens the file and doesn’t just prompt to "open/save”.
        Response.AddHeader("Content-Length", ByteArray.Length.ToString())
        Response.AddHeader("Content-Disposition", "inline filename=" + PDFfile)
        Response.AddHeader("Expires", "0")
        Response.AddHeader("Pragma", "cache")
        Response.AddHeader("Cache-Control", "private")

        'Set the output stream to the correct content type (PDF).
        Response.ContentType = "application/pdf"

        'Output the file
        Response.BinaryWrite(ByteArray)

        'Flushing the Response to display the serialized data
        'to the client browser.
        Response.Flush()
    End Sub
    Protected Sub btnCard_Click(sender As Object, e As EventArgs) Handles btnCard.Click
        Dim FilePath As String = Server.MapPath("ResultsCards") & "\"
        Dim filename As String = "MENS SKITTLES RESULT CARD A4.pdf"
        Dim PDFfile As String = FilePath + filename

        'write details for PDF_downloads
        write_PDF_download(PDFfile)

        Dim client As New WebClient
        Dim ByteArray As Byte() = client.DownloadData(PDFfile)

        Response.Clear()
        'Send the file to the output stream
        Response.Buffer = True
        'Try and ensure the browser always opens the file and doesn’t just prompt to "open/save”.
        Response.AddHeader("Content-Length", ByteArray.Length.ToString())
        Response.AddHeader("Content-Disposition", "inline filename=" + PDFfile)
        Response.AddHeader("Expires", "0")
        Response.AddHeader("Pragma", "cache")
        Response.AddHeader("Cache-Control", "private")

        'Set the output stream to the correct content type (PDF).
        Response.ContentType = "application/pdf"

        'Output the file
        Response.BinaryWrite(ByteArray)

        'Flushing the Response to display the serialized data
        'to the client browser.
        Response.Flush()
    End Sub
End Class
