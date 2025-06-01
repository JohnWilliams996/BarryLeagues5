Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
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

        grid6aside.Visible = False
        Call load_players()
        txtInfo.Visible = False
        Select Case Left(objGlobals.LeagueSelected, 4)
            Case "SNOO"
                txtInfo.Text = "Snooker Teams:" + vbCrLf + "Deadline for registering new players is 31st December 2022"
                Call load_singles()
                Call load_pairs()
                Call load_3aSide()
            Case "CRIB"
                Call load_pairs()
            Case "SKIT"
                txtInfo.Text = "Skittles Teams:" + vbCrLf + "All games to now start at 8pm." + vbCrLf + "King William XII are now playing on Thursday's at the Football Club."
                grid6aside.Visible = True
        End Select
        gRow = 0
        gridPlayers.ShowHeader = False
        gridPlayers.DataSource = dt
        gridPlayers.DataBind()
        If Left(objGlobals.LeagueSelected, 4) = "SKIT" Then Call load_6aSide()


        '        If objGlobals.LeagueSelected = "SNOOKER DIVISION 1" Then
        ' lblLibs.Visible = True
        'Else
        'End If

    End Sub

    Function get_league_from_team(ByVal inLeague As String) As String
        Dim strSQL As String = ""
        get_league_from_team = ""
        Dim myDataReader As oledbdatareader
        If InStr(inLeague, "CRIB", CompareMethod.Text) > 0 Then strSQL = "SELECT league FROM clubs.vw_teams WHERE league LIKE 'CRIB%' AND long_name = '" & objGlobals.TeamSelected & "'"
        If InStr(inLeague, "SKITTLES", CompareMethod.Text) > 0 Then strSQL = "SELECT league FROM clubs.vw_teams WHERE league LIKE 'SKITTLES%' AND long_name = '" & objGlobals.TeamSelected & "'"
        If InStr(inLeague, "SNOOKER", CompareMethod.Text) > 0 Then strSQL = "SELECT league FROM clubs.vw_teams WHERE league LIKE 'SNOOKER%' AND long_name = '" & objGlobals.TeamSelected & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            get_league_from_team = myDataReader.Item("league")
        End While
    End Function

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

    End Sub


    Sub load_singles()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        dt.Rows.Add("")
        dt.Rows.Add("Singles Entries")
        strSQL = "SELECT player,still_in_singles FROM clubs.vw_players WHERE league='" & objGlobals.LeagueSelected & "' AND team='" & objGlobals.TeamSelected & "' AND Singles = 1 ORDER BY LEFT(Player,3)"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If Not myDataReader.HasRows Then Exit Sub
        While myDataReader.Read()
            dt.Rows.Add(myDataReader.Item("player"), myDataReader.Item("still_in_singles"))
        End While

    End Sub

    Sub load_pairs()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim ThisPair As String = ""
        Dim StillInPairs As Integer

        dt.Rows.Add("")
        If Left(objGlobals.LeagueSelected, 4) = "SNOO" Then
            dt.Rows.Add("Doubles Entries")
        Else
            dt.Rows.Add("Pairs Entries")
        End If

        strSQL = "SELECT player,still_in_pairs FROM clubs.vw_players WHERE league='" & objGlobals.LeagueSelected & "' AND team='" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & " AND pairs1 = 1"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If myDataReader.HasRows Then
            ThisPair = ""
            While myDataReader.Read()
                StillInPairs = myDataReader.Item("still_in_pairs")
                ThisPair = ThisPair & myDataReader.Item("player") & " & "
            End While
            ThisPair = Left(ThisPair, Len(ThisPair) - 2)
            dt.Rows.Add(ThisPair, StillInPairs)
        End If

        strSQL = "SELECT player,still_in_pairs FROM clubs.vw_players WHERE league='" & objGlobals.LeagueSelected & "' AND team='" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & " AND pairs2 = 1"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If myDataReader.HasRows Then
            ThisPair = ""
            While myDataReader.Read()
                StillInPairs = myDataReader.Item("still_in_pairs")
                ThisPair = ThisPair & myDataReader.Item("player") & " & "
            End While
            ThisPair = Left(ThisPair, Len(ThisPair) - 2)
            dt.Rows.Add(ThisPair, StillInPairs)
        End If

        strSQL = "SELECT player,still_in_pairs FROM clubs.vw_players WHERE league='" & objGlobals.LeagueSelected & "' AND team='" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & " AND pairs3 = 1"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If myDataReader.HasRows Then
            ThisPair = ""
            While myDataReader.Read()
                StillInPairs = myDataReader.Item("still_in_pairs")
                ThisPair = ThisPair & myDataReader.Item("player") & " & "
            End While
            ThisPair = Left(ThisPair, Len(ThisPair) - 2)
            dt.Rows.Add(ThisPair, StillInPairs)
        End If

        strSQL = "SELECT player,still_in_pairs FROM clubs.vw_players WHERE league='" & objGlobals.LeagueSelected & "' AND team='" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & " AND pairs4 = 1"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If myDataReader.HasRows Then
            ThisPair = ""
            While myDataReader.Read()
                StillInPairs = myDataReader.Item("still_in_pairs")
                ThisPair = ThisPair & myDataReader.Item("player") & " & "
            End While
            ThisPair = Left(ThisPair, Len(ThisPair) - 2)
            dt.Rows.Add(ThisPair, StillInPairs)
        End If

        strSQL = "SELECT player,still_in_pairs FROM clubs.vw_players WHERE league='" & objGlobals.LeagueSelected & "' AND team='" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & " AND pairs5 = 1"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If myDataReader.HasRows Then
            ThisPair = ""
            While myDataReader.Read()
                StillInPairs = myDataReader.Item("still_in_pairs")
                ThisPair = ThisPair & myDataReader.Item("player") & " & "
            End While
            ThisPair = Left(ThisPair, Len(ThisPair) - 2)
            dt.Rows.Add(ThisPair, StillInPairs)
        End If

        strSQL = "SELECT player,still_in_pairs FROM clubs.vw_players WHERE league='" & objGlobals.LeagueSelected & "' AND team='" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & " AND pairs6 = 1"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If myDataReader.HasRows Then
            ThisPair = ""
            While myDataReader.Read()
                StillInPairs = myDataReader.Item("still_in_pairs")
                ThisPair = ThisPair & myDataReader.Item("player") & " & "
            End While
            ThisPair = Left(ThisPair, Len(ThisPair) - 2)
            dt.Rows.Add(ThisPair, StillInPairs)
        End If

    End Sub

    Sub load_3aSide()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim This3aSide As String = ""
        Dim StillIn3aSide As Integer

        dt.Rows.Add("")
        dt.Rows.Add("3-a-Side Entries")

        strSQL = "SELECT player,still_in_triples FROM clubs.vw_players WHERE league='" & objGlobals.LeagueSelected & "' AND team='" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & " AND triples1 = 1"
        myDataReader = objGlobals.SQLSelect(strSQL)

        If myDataReader.HasRows Then
            This3aSide = ""
            While myDataReader.Read()
                StillIn3aSide = myDataReader.Item("still_in_triples")
                This3aSide = This3aSide & myDataReader.Item("player") & " / "
            End While
            This3aSide = Left(This3aSide, Len(This3aSide) - 2)
            dt.Rows.Add(This3aSide, StillIn3aSide)
        End If

        strSQL = "SELECT player,still_in_triples FROM clubs.vw_players WHERE league='" & objGlobals.LeagueSelected & "' AND team='" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & " AND triples2 = 1"
        myDataReader = objGlobals.SQLSelect(strSQL)

        If myDataReader.HasRows Then
            This3aSide = ""
            While myDataReader.Read()
                StillIn3aSide = myDataReader.Item("still_in_triples")
                This3aSide = This3aSide & myDataReader.Item("player") & " / "
            End While
            This3aSide = Left(This3aSide, Len(This3aSide) - 2)
            dt.Rows.Add(This3aSide, StillIn3aSide)
        End If

        strSQL = "SELECT player,still_in_triples FROM clubs.vw_players WHERE league='" & objGlobals.LeagueSelected & "' AND team='" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & " AND triples3 = 1"
        myDataReader = objGlobals.SQLSelect(strSQL)

        If myDataReader.HasRows Then
            This3aSide = ""
            While myDataReader.Read()
                StillIn3aSide = myDataReader.Item("still_in_triples")
                This3aSide = This3aSide & myDataReader.Item("player") & " / "
            End While
            This3aSide = Left(This3aSide, Len(This3aSide) - 2)
            dt.Rows.Add(This3aSide, StillIn3aSide)
        End If

        strSQL = "SELECT player,still_in_triples FROM clubs.vw_players WHERE league='" & objGlobals.LeagueSelected & "' AND team='" & objGlobals.TeamSelected & "' "
        strSQL = strSQL & " AND triples4 = 1"
        myDataReader = objGlobals.SQLSelect(strSQL)

        If myDataReader.HasRows Then
            This3aSide = ""
            While myDataReader.Read()
                StillIn3aSide = myDataReader.Item("still_in_triples")
                This3aSide = This3aSide & myDataReader.Item("player") & " / "
            End While
            This3aSide = Left(This3aSide, Len(This3aSide) - 2)
            dt.Rows.Add(This3aSide, StillIn3aSide)
        End If

    End Sub

    Sub load_6aSide()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        dt = New DataTable
        dt.Columns.Add(New DataColumn("player1", GetType(System.String)))
        dt.Columns.Add(New DataColumn("player2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("player3", GetType(System.String)))


        strSQL = "EXEC clubs.sp_get_6aside_teams '" & objGlobals.current_season & "','" & objGlobals.LeagueSelected & "','" & objGlobals.TeamSelected & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            dt.Rows.Add(myDataReader.Item("player1"), myDataReader.Item("player2"), myDataReader.Item("player3"))
        End While

        gRow = 0
        grid6aside.ShowHeader = False
        grid6aside.DataSource = dt
        grid6aside.DataBind()

    End Sub

    Sub load_title()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        lblTeam.Text = objGlobals.TeamSelected
        lblLeague.Text = objGlobals.LeagueSelected
        strSQL = "SELECT * FROM clubs.vw_tables WHERE league='" & objGlobals.LeagueSelected & "' AND team='" & objGlobals.TeamSelected & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            lblStatus.Text = "Current Pos : " & myDataReader.Item("pos") & vbTab & "Pld : " & myDataReader.Item("pld") & vbTab & "Pts : " & myDataReader.Item("pts")
        End While
        Select Case Left(objGlobals.LeagueSelected, 4)
            Case "CRIB"
                lblTeam.ForeColor = Yellow
                lblLeague.ForeColor = Yellow
            Case "SNOO"
                lblTeam.ForeColor = LightGreen
                lblLeague.ForeColor = LightGreen
            Case "SKIT"
                lblTeam.ForeColor = LightBlue
                lblLeague.ForeColor = LightBlue
        End Select
        hlTeamStats.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected
        hlLeagueStats.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected
    End Sub

    Sub get_week_pts_pld()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim Wk As Integer

        strSQL = "SELECT week,pts,pos FROM clubs.vw_tables_week"
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
        dt.Columns.Add(New DataColumn("Home Points Deducted", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Points Deducted", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total Points", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League Position", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture ID", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture ID2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Status", GetType(System.String)))

        strSQL = "SELECT venue FROM clubs.vw_teams WHERE league = '" & objGlobals.LeagueSelected & "' AND long_name = '" & objGlobals.TeamSelected & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            home_venue = myDataReader.Item("venue")
        End While
        objGlobals.close_connection()


        strSQL = "EXEC clubs.sp_get_team_fixtures '" & objGlobals.get_current_season & "','" & objGlobals.LeagueSelected & "','" & objGlobals.TeamSelected & "'"
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
                            dr("Venue") = IIf(myDataReader.Item("fixture_neutral") = 0, "HOME", myDataReader.Item("venue") + " (N)")
                        Else
                            dr("Venue") = IIf(myDataReader.Item("fixture_neutral") = 0, myDataReader.Item("venue"), myDataReader.Item("venue") + " (N)")
                        End If
                        If myDataReader.Item("home_result") = "WON" Or myDataReader.Item("home_result") = "LOST" Then
                            dr("Home Result") = myDataReader.Item("home_result")
                        ElseIf myDataReader.Item("home_result") = "versus" Then
                            dr("Home Result") = ""
                        Else
                            Select Case myDataReader.Item("home_points") - myDataReader.Item("away_points")
                                Case Is > 0 : dr("Home Result") = "W " & Replace(myDataReader.Item("home_result"), " ", "")
                                Case Is < 0 : dr("Home Result") = "L " & Replace(myDataReader.Item("home_result"), " ", "")
                                Case 0 : dr("Home Result") = "D " & Replace(myDataReader.Item("home_result"), " ", "")
                            End Select
                        End If
                        If myDataReader.Item("home_points_deducted") > 0 Then dr("Home Points Deducted") = myDataReader.Item("home_points_deducted") * -1
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
                            Case 0 : dr("Home Result") = "D " & Replace(myDataReader.Item("away_result"), " ", "")
                        End Select
                    End If
                    If myDataReader.Item("away_result") = "0 - 0" Then dr("Home Result") = ""
                    If myDataReader.Item("status") = -1 Then dr("Home Result") = "Postponed"
                    If myDataReader.Item("away_points_deducted") > 0 Then dr("Home Points Deducted") = myDataReader.Item("away_points_deducted") * -1
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
                If Left(CStr(objGlobals.LeagueSelected), 4) <> "SKIT" Then
                    dr("Total Points") = Pts(Wk)
                Else
                    dr("Total Points") = Format(Pts(Wk), "##0.0")
                End If
                dr("League Position") = Pos(Wk)
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

        strSQL = "Select long_name, home_night, venue FROM clubs.vw_teams WHERE league = '" & objGlobals.LeagueSelected & "' AND long_name <> 'BYE' ORDER BY long_name"
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
        dr = dt.NewRow
        dr("Long Name") = "Competitions"
        dt.Rows.Add(dr)
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

        strSQL = "SELECT player FROM clubs.vw_players WHERE league = '" & objGlobals.LeagueSelected & "' AND team = '" & objGlobals.TeamSelected & "' AND CHARINDEX( 'A N OTHER',Player) = 0  ORDER BY LEFT(Player,3)"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If myDataReader.HasRows Then
            While myDataReader.Read()
                PlayerCount = PlayerCount + 1
                dt.Rows.Add(myDataReader.Item("player"), 1)
            End While
        Else
            dt.Rows.Add("NO PLAYERS REGISTERED")
        End If

        If Left(objGlobals.LeagueSelected, 4) = "SKIT" Then
            dt.Rows.Add("")
            dt.Rows.Add("6-a-Side Cup Teams")
            dt.Rows.Add("")
        End If
    End Sub

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


    Protected Sub gridPlayers_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridPlayers.RowDataBound
        Dim SinglesEntry As Boolean = False
        Dim DoublesEntry As Boolean = False
        Dim TriplesEntry As Boolean = False
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If dt.Rows(gRow)(0) <> "" Then
                If InStr(dt.Rows(gRow)(0), "Players", CompareMethod.Text) > 0 Or InStr(dt.Rows(gRow)(0), "Singles", CompareMethod.Text) > 0 _
                    Or InStr(dt.Rows(gRow)(0), "Doubles", CompareMethod.Text) > 0 Or InStr(dt.Rows(gRow)(0), "Pairs", CompareMethod.Text) > 0 _
                    Or InStr(dt.Rows(gRow)(0), "3-a-Side", CompareMethod.Text) > 0 Or InStr(dt.Rows(gRow)(0), "(Click", CompareMethod.Text) > 0 _
                    Or InStr(dt.Rows(gRow)(0), "6-a-Side", CompareMethod.Text) > 0 Or InStr(dt.Rows(gRow)(0), "(Click", CompareMethod.Text) > 0 _
                    Or InStr(dt.Rows(gRow)(0), "Team 1", CompareMethod.Text) > 0 Or InStr(dt.Rows(gRow)(0), "(Click", CompareMethod.Text) > 0 _
                    Or InStr(dt.Rows(gRow)(0), "Team 2", CompareMethod.Text) > 0 Or InStr(dt.Rows(gRow)(0), "(Click", CompareMethod.Text) > 0 _
                    Or InStr(dt.Rows(gRow)(0), "Team 3", CompareMethod.Text) > 0 Or InStr(dt.Rows(gRow)(0), "(Click", CompareMethod.Text) > 0 Then
                    e.Row.Cells(0).ForeColor = System.Drawing.Color.FromArgb(&HE4, &HBB, &H18)
                Else
                    If InStr(dt.Rows(gRow)(0), "&", CompareMethod.Text) > 0 Then
                        DoublesEntry = True
                    ElseIf InStr(dt.Rows(gRow)(0), "/", CompareMethod.Text) > 0 Then
                        TriplesEntry = True
                    ElseIf Left(objGlobals.LeagueSelected, 4) <> "SKIT" Then
                        SinglesEntry = True
                    End If
                    Dim hLink As New HyperLink
                    Select Case True
                        Case SinglesEntry
                            hLink.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & lblTeam.Text & "&Player=" & dt.Rows(gRow)(0)
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
                        Case Else
                            hLink.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & lblTeam.Text & "&Player=" & dt.Rows(gRow)(0)
                            hLink.Text = e.Row.Cells(0).Text
                            hLink.ForeColor = White
                            e.Row.Cells(0).Controls.Add(hLink)
                    End Select
                End If
            End If
            gRow = gRow + 1
        End If
    End Sub
    Protected Sub grid6aside_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grid6aside.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If dt.Rows(gRow)(0) <> "" Then
                If InStr(dt.Rows(gRow)(0), "Team") > 0 Then
                    e.Row.Cells(0).ForeColor = System.Drawing.Color.FromArgb(&HE4, &HBB, &H18)
                    e.Row.Cells(1).ForeColor = System.Drawing.Color.FromArgb(&HE4, &HBB, &H18)
                    e.Row.Cells(2).ForeColor = System.Drawing.Color.FromArgb(&HE4, &HBB, &H18)
                Else
                    Dim hLink0 As New HyperLink
                    hLink0.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & lblTeam.Text & "&Player=" & dt.Rows(gRow)(0)
                    hLink0.Text = e.Row.Cells(0).Text
                    hLink0.ForeColor = White
                    e.Row.Cells(0).Controls.Add(hLink0)

                    Dim hLink1 As New HyperLink
                    hLink1.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & lblTeam.Text & "&Player=" & dt.Rows(gRow)(1)
                    hLink1.Text = e.Row.Cells(1).Text
                    hLink1.ForeColor = White
                    e.Row.Cells(1).Controls.Add(hLink1)

                    Dim hLink2 As New HyperLink
                    hLink2.NavigateUrl = "~/Clubs/Stats.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & lblTeam.Text & "&Player=" & dt.Rows(gRow)(2)
                    hLink2.Text = e.Row.Cells(2).Text
                    hLink2.ForeColor = White
                    e.Row.Cells(2).Controls.Add(hLink2)
                End If
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridFixtures1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridFixtures1.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Left(dt.Rows(gRow)(1).ToString, 3) <> "W/C" And Left(dt.Rows(gRow)(3).ToString, 3) <> "BYE" Then
                Dim hLink As New HyperLink
                hLink.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(3).ToString
                hLink.Text = e.Row.Cells(3).Text
                hLink.ForeColor = Cyan
                e.Row.Cells(3).Controls.Add(hLink)

                Dim Status As Integer = Val(dt.Rows(gRow)(14))
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
                                    hLink3.NavigateUrl = "~/Clubs/Admin/Add Edit Result.aspx?ID=" & Val(dt.Rows(gRow)(13)).ToString & "&Week=" & Val(dt.Rows(gRow)(0)).ToString & "&League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected
                                Else
                                    'hLink3.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&CompID=" & Val(dt.Rows(gRow)(13)).ToString
                                    hLink3.NavigateUrl = "~/Clubs/Result Card.aspx?CompID=" & Val(dt.Rows(gRow)(13)).ToString
                                End If
                                hLink3.ForeColor = LightGreen
                            Case 1
                                If objGlobals.AdminLogin Then
                                    hLink3.NavigateUrl = "~/Clubs/Admin/Add Edit Result.aspx?ID=" & Val(dt.Rows(gRow)(13)).ToString & "&Week=" & Val(dt.Rows(gRow)(0)).ToString & "&League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected
                                Else
                                    'hLink3.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&CompID=" & Val(dt.Rows(gRow)(13)).ToString
                                    hLink3.NavigateUrl = "~/Clubs/Result Card.aspx?CompID=" & Val(dt.Rows(gRow)(13)).ToString
                                End If
                                hLink3.ForeColor = Orange
                            Case 0
                                hLink3.NavigateUrl = "~/Clubs/Admin/Fixture Result.aspx?ID=" & Val(dt.Rows(gRow)(13)).ToString & "&Week=" & Val(dt.Rows(gRow)(0)).ToString & "&League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected
                                hLink3.ForeColor = White
                            Case -1
                                If objGlobals.AdminLogin Then
                                    hLink3.NavigateUrl = "~/Clubs/Admin/Add Edit Result.aspx?ID=" & Val(dt.Rows(gRow)(13)).ToString & "&Week=" & Val(dt.Rows(gRow)(0)).ToString & "&League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected
                                Else
                                    hLink3.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected & "&CompID=" & Val(dt.Rows(gRow)(13)).ToString
                                End If
                                hLink3.ForeColor = White
                        End Select

                        hLink3.Text = e.Row.Cells(11).Text
                        e.Row.Cells(11).Controls.Add(hLink3)
                    End If
                End If
            Else
                If Left(dt.Rows(gRow)(3).ToString, 3) = "BYE" Then
                    e.Row.Cells(0).ForeColor = Gray
                    e.Row.Cells(2).ForeColor = Gray
                End If
                e.Row.Cells(1).ForeColor = Gray
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

        strSQL = "EXEC [clubs].[sp_write_download_PDF] '" & l_param_in_values(0) & "','" & l_param_in_values(1) & "','" & l_param_in_values(2) & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        objGlobals.close_connection()
    End Sub

    Protected Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Dim FilePath As String = Server.MapPath("TeamFixtures") & "\"
        Dim filename As String = Replace(objGlobals.TeamSelected, """", "'") & " " & objGlobals.LeagueSelected & ".pdf"
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
        Dim filename As String = ""
        Select Case Left(objGlobals.LeagueSelected, 4)
            Case "CRIB"
                filename = "CRIB RESULT CARD A4.pdf"
            Case "SKIT"
                filename = "SKITTLES RESULT CARD A4.pdf"
            Case "SNOO"
                filename = "SNOOKER RESULT CARD A4.pdf"
        End Select
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
