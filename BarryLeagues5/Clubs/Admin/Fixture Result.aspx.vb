Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
Imports System.Diagnostics
Imports System.IO
'Imports MySql.Data.MySqlClient

Partial Class Admin_Fixture_Result
    Inherits System.Web.UI.Page
    Private dt As DataTable = New DataTable
    Private dr As DataRow = Nothing
    Private CompID As Integer
    Private objGlobals As New Globals
    Private Pos(20) As Integer
    Private Team(20) As String
    Private Played(20) As Integer
    Private Won(20) As Integer
    Private Drawn(20) As Integer
    Private Lost(20) As Integer
    Private Points(20) As Double
    Private Deducted(20) As Double
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
    Private gRow As Integer = 0
    Private TotalHome As Integer = 0
    Private TotalAway As Integer = 0
    Private strSQL As String
    Private myDataReader As oledbdatareader


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("clubs")
        objGlobals.CurrentUser = "clubs_user"
        objGlobals.CurrentSchema = "clubs."

        objGlobals.TeamSelected = Request.QueryString("Team")
        objGlobals.LeagueSelected = Request.QueryString("League")
        objGlobals.PlayerSelected = Request.QueryString("Player")
        FixtureWeek = Request.QueryString("Week")
        CompID = Request.QueryString("ID")
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
            Call objGlobals.store_page(Request.Url.OriginalString, True)
            If objGlobals.TeamSelected Is Nothing Then
                btnAddEditResult.PostBackUrl = "~/Clubs/Admin/Add Edit Result.aspx?ID=" & CompID & "&Week=" & FixtureWeek
            Else
                btnAddEditResult.PostBackUrl = "~/Clubs/Admin/Add Edit Result.aspx?ID=" & CompID & "&Week=" & FixtureWeek & "&Team=" & objGlobals.TeamSelected
            End If
        Else
            lblPlayerStats.Text = "NOT AUTHORIZED"
            lblNoCard.Visible = False
            btnBacktoStats.Visible = False
            btnBack.Visible = False
            lblHomePointsDeducted.Visible = False
            ddHomePointsDeducted.Visible = False
            lblAwayPointsDeducted.Visible = False
            ddAwayPointsDeducted.Visible = False
            btnCancel.Visible = False
            btnOK.Visible = False
            lblTableBefore.Visible = False
            lblTableAfter.Visible = False
            btnAddEditResult.Visible = False
            objGlobals.AdminLogin = False

            lblNewDate.Visible = False
            lblVenueLit.Visible = False
            Calendar1.Visible = False
            lblNewStatus.Visible = False
            txtNewStatus.Visible = False
            lblError.Visible = False
            btnUpdateDateStatus.Visible = False

            Call objGlobals.store_page("ATTACK FROM " & Request.Url.OriginalString, objGlobals.AdminLogin)
            '16.9.14 - only allow ADMIN in this screen
            Exit Sub
        End If
        btnBack.Visible = True
        btnBack.Attributes.Add("onClick", "javascript:history.back(); return false;")

        If objGlobals.PlayerSelected <> Nothing Then
            btnBack.Visible = False
            btnBacktoStats.Visible = True
            btnBacktoStats.Attributes.Add("onClick", "javascript:history.back(); return false;")
            btnOK.Attributes.Add("onClick", "javascript:history.back(); return false;")
        Else
            btnBacktoStats.Visible = False
        End If
        lblTableBefore.Visible = True
        lblTableAfter.Visible = True



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

            If myDataReader.Item("status") < 2 Then
                lblNoCard.Visible = True
                lblPlayerStats.Visible = False
            Else
                lblNoCard.Visible = False
                lblPlayerStats.Visible = True
            End If

            '8/12/22 Allow change of fixture date and status
            If Calendar1.SelectedDate = "#12:00:00 AM#" Then
                Calendar1.SelectedDate = myDataReader.Item("fixture_calendar")
                lblVenue.Text = myDataReader.Item("venue")
                txtNewStatus.Text = myDataReader.Item("status")
            End If
        End While
        objGlobals.close_connection()


        If Not IsPostBack Then
            If objGlobals.AdminLogin Then
                rbResults.Enabled = True
                btnOK.Visible = False
                btnCancel.Visible = True
                ddHomePointsDeducted.Enabled = True
                ddAwayPointsDeducted.Enabled = True
                btnAddEditResult.Visible = True
            Else
                rbResults.Enabled = False
                rbResults.Visible = False
                btnOK.Visible = True
                btnCancel.Visible = False
                ddHomePointsDeducted.Visible = False
                ddAwayPointsDeducted.Visible = False
                lblHomePointsDeducted.Visible = False
                lblAwayPointsDeducted.Visible = False
                btnAddEditResult.Visible = False
            End If

            Select Case Left(FixtureLeague, 4)
                Case "SKIT"
                    With rbResults
                        .ClearSelection()
                        .Items.Add("0-0")
                        .Items.Add("7-0") : If home_result = "7-0" Then .SelectedIndex = 1
                        .Items.Add("6½-½") : If home_result = "6½-½" Then .SelectedIndex = 2
                        .Items.Add("6-1") : If home_result = "6-1" Then .SelectedIndex = 3
                        .Items.Add("5½-1½") : If home_result = "5½-1½" Then .SelectedIndex = 4
                        .Items.Add("5-2") : If home_result = "5-2" Then .SelectedIndex = 5
                        .Items.Add("4½-2½") : If home_result = "4½-2½" Then .SelectedIndex = 6
                        .Items.Add("4-3") : If home_result = "4-3" Then .SelectedIndex = 7
                        .Items.Add("3½-3½") : If home_result = "3½-3½" Then .SelectedIndex = 8
                        .Items.Add("3-4") : If home_result = "3-4" Then .SelectedIndex = 9
                        .Items.Add("2½-4½") : If home_result = "2½-4½" Then .SelectedIndex = 10
                        .Items.Add("2-5") : If home_result = "2-5" Then .SelectedIndex = 11
                        .Items.Add("1½-5½") : If home_result = "1½-5½" Then .SelectedIndex = 12
                        .Items.Add("1-6") : If home_result = "1-6" Then .SelectedIndex = 13
                        .Items.Add("½-6½") : If home_result = "½-6½" Then .SelectedIndex = 14
                        .Items.Add("0-7") : If home_result = "0-7" Then .SelectedIndex = 15
                        .Items.Add("Postponed")
                    End With
                    Dim i As Single
                    For i = 0 To 7 Step 0.5
                        ddHomePointsDeducted.Items.Add(i)
                        ddAwayPointsDeducted.Items.Add(i)
                    Next
                Case "CRIB"
                    With rbResults
                        .ClearSelection()
                        .Items.Add("0-0")
                        .Items.Add("15-0") : If home_result = "15-0" Then .SelectedIndex = 1
                        .Items.Add("14-1") : If home_result = "14-1" Then .SelectedIndex = 2
                        .Items.Add("13-2") : If home_result = "13-2" Then .SelectedIndex = 3
                        .Items.Add("12-3") : If home_result = "12-3" Then .SelectedIndex = 4
                        .Items.Add("11-4") : If home_result = "11-4" Then .SelectedIndex = 5
                        .Items.Add("10-5") : If home_result = "10-5" Then .SelectedIndex = 6
                        .Items.Add("9-6") : If home_result = "9-6" Then .SelectedIndex = 7
                        .Items.Add("8-7") : If home_result = "8-7" Then .SelectedIndex = 8
                        .Items.Add("7-8") : If home_result = "7-8" Then .SelectedIndex = 9
                        .Items.Add("6-9") : If home_result = "6-9" Then .SelectedIndex = 10
                        .Items.Add("5-10") : If home_result = "5-10" Then .SelectedIndex = 11
                        .Items.Add("4-11") : If home_result = "4-11" Then .SelectedIndex = 12
                        .Items.Add("3-12") : If home_result = "3-12" Then .SelectedIndex = 13
                        .Items.Add("2-13") : If home_result = "2-13" Then .SelectedIndex = 14
                        .Items.Add("1-14") : If home_result = "1-14" Then .SelectedIndex = 15
                        .Items.Add("0-15") : If home_result = "0-15" Then .SelectedIndex = 16
                        .Items.Add("Postponed")
                    End With
                    Dim i As Integer
                    For i = 0 To 15
                        ddHomePointsDeducted.Items.Add(i)
                        ddAwayPointsDeducted.Items.Add(i)
                    Next
                Case "SNOO"
                    With rbResults
                        .ClearSelection()
                        .Items.Add("0-0")
                        .Items.Add("5-0") : If home_result = "5-0" Then .SelectedIndex = 1
                        .Items.Add("4-1") : If home_result = "4-1" Then .SelectedIndex = 2
                        .Items.Add("3-2") : If home_result = "3-2" Then .SelectedIndex = 3
                        .Items.Add("2-3") : If home_result = "2-3" Then .SelectedIndex = 4
                        .Items.Add("1-4") : If home_result = "1-4" Then .SelectedIndex = 5
                        .Items.Add("0-5") : If home_result = "0-5" Then .SelectedIndex = 6
                        .Items.Add("Postponed")
                    End With
                    Dim i As Integer
                    For i = 0 To 5
                        ddHomePointsDeducted.Items.Add(i)
                        ddAwayPointsDeducted.Items.Add(i)
                    Next
            End Select
            For i = 0 To ddHomePointsDeducted.Items.Count - 1
                If i = HomePointsDeducted Then ddHomePointsDeducted.SelectedIndex = i
            Next
            For i = 0 To ddAwayPointsDeducted.Items.Count - 1
                If i = AwayPointsDeducted Then ddAwayPointsDeducted.SelectedIndex = i
            Next
            If objGlobals.AdminLogin Then
                Call load_table(gridTable, "Tables")
                lblTableAfter.Visible = False
            Else
                Call create_tables_before()
                Call load_table(gridTable, "before_tables")
                Call create_tables_after(FixtureDate)
                Call load_table(gridTable2, "after_tables")
            End If
            Select Case Left(FixtureLeague, 4)
                Case "SKIT"
                    Call load_skittles_header()
                Case "CRIB"
                    Call load_crib_header()
                Case "SNOO"
                    Call load_snooker_header()
            End Select
            If Not lblNoCard.Visible Then
                Select Case Left(FixtureLeague, 4)
                    Case "SKIT"
                        Call load_skittles_result()
                    Case "CRIB"
                        Call load_crib_result()
                    Case "SNOO"
                        Call load_snooker_result()
                End Select
                ddHomePointsDeducted.Visible = False
                ddAwayPointsDeducted.Visible = False
                rbResults.Visible = False
                lblHomePointsDeducted.Visible = False
                lblAwayPointsDeducted.Visible = False
            End If
            Select Case Left(FixtureLeague, 4)
                Case "SKIT"
                    Call load_skittles_totals()
                Case "CRIB"
                    Call load_crib_totals()
                Case "SNOO"
                    Call load_snooker_totals()
            End Select
            If objGlobals.AdminLogin Then
                ddHomePointsDeducted.Visible = True
                ddAwayPointsDeducted.Visible = True
                rbResults.Visible = True
                lblHomePointsDeducted.Visible = True
                lblAwayPointsDeducted.Visible = True
            End If
        End If
    End Sub

    Protected Sub create_tables_before()
        Dim myDataReader2 As OleDbDataReader
        Dim ThisTeam As String = ""

        Dim BeforePlayed As Integer = 0
        Dim BeforeWon As Integer = 0
        Dim BeforeDrawn As Integer = 0
        Dim BeforeLost As Integer = 0
        Dim BeforePoints As Double = 0
        Dim BeforeDeducted As Integer = 0
        Dim tempSeason As String
        tempSeason = objGlobals.get_current_season

        strSQL = "TRUNCATE TABLE clubs.before_tables"
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

        strSQL = "SELECT DISTINCT(Team) FROM clubs.vw_tables WHERE league = '" & FixtureLeague & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ThisTeam = myDataReader.Item(0)
            strSQL = "INSERT INTO clubs.before_tables VALUES ('" & tempSeason & "','" & FixtureLeague & "',1,'" & ThisTeam & "',0,0,0,0,0,0)"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        objGlobals.close_connection()

        strSQL = "SELECT t.long_name as Team,"
        strSQL = strSQL + " COUNT(a.home_team_name) - COUNT(a1.home_team_name) AS Pld, "
        strSQL = strSQL + " COUNT(b.home_points) AS W, "
        strSQL = strSQL + " COUNT(c.home_points) AS D,"
        strSQL = strSQL + " COUNT(d.home_points) AS L,"
        strSQL = strSQL + " SUM(CASE WHEN e.home_points IS NULL THEN 0 ELSE e.home_points END) AS Pts,"
        strSQL = strSQL + " SUM(CASE WHEN f.home_points_deducted IS NULL THEN 0 ELSE f.home_points_deducted END) AS Deducted"
        strSQL = strSQL + " FROM clubs.vw_teams t"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures AS a ON a.League = t.League AND a.home_team_name = t.long_name AND (a.home_result <> '0 - 0' OR (a.home_result = '0 - 0' AND a.home_points_deducted > 0))"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures AS a1 ON a1.fixture_id = a.fixture_id AND a1.home_result = '0 - 0' AND a1.home_points_deducted > 0"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures b ON b.fixture_id = a.fixture_id AND b.home_points > b.away_points"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures c ON c.fixture_id = a.fixture_id AND c.home_points = c.away_points AND c.home_result <> '0 - 0'"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures d ON d.fixture_id = a.fixture_id AND d.home_points < d.away_points"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures e ON e.fixture_id = a.fixture_id"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures f ON f.fixture_id = a.fixture_id "
        strSQL = strSQL + " WHERE t.League = '" & FixtureLeague & "'"
        strSQL = strSQL + " AND t.long_name <> 'BYE'"
        strSQL = strSQL + " AND CONVERT(VARCHAR(10),a.fixture_calendar,112) <= '" + FixtureDate + "'"
        strSQL = strSQL + " AND a.fixture_id <> " & CompID
        strSQL = strSQL + " GROUP BY t.long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            ThisTeam = myDataReader.Item("team")
            BeforePlayed = myDataReader.Item("pld")
            BeforeWon = myDataReader.Item("w")
            BeforeDrawn = myDataReader.Item("d")
            BeforeLost = myDataReader.Item("l")
            BeforePoints = myDataReader.Item("pts")
            BeforeDeducted = myDataReader.Item("Deducted")
            strSQL = "UPDATE clubs.before_tables SET Pld=" & BeforePlayed & ",W=" & BeforeWon & ",D=" & BeforeDrawn & ",L=" & BeforeLost & ",pts=" & (BeforePoints - BeforeDeducted) & ",Deducted=" & BeforeDeducted
            strSQL = strSQL & " WHERE team = '" & ThisTeam & "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        objGlobals.close_connection()

        strSQL = "SELECT t.long_name as Team,"
        strSQL = strSQL + " COUNT(a.away_team_name) - COUNT(a1.away_team_name) AS Pld, "
        strSQL = strSQL + " COUNT(b.away_points) AS W, "
        strSQL = strSQL + " COUNT(c.away_points) AS D,"
        strSQL = strSQL + " COUNT(d.away_points) AS L,"
        strSQL = strSQL + " SUM(CASE WHEN e.away_points IS NULL THEN 0 ELSE e.away_points END) AS Pts,"
        strSQL = strSQL + " SUM(CASE WHEN f.away_points_deducted IS NULL THEN 0 ELSE f.away_points_deducted END) AS Deducted"
        strSQL = strSQL + " FROM clubs.vw_teams t"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures AS a ON a.League = t.League AND a.away_team_name = t.long_name AND (a.away_result <> '0 - 0' OR (a.away_result = '0 - 0' AND a.away_points_deducted > 0))"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures AS a1 ON a1.fixture_id = a.fixture_id AND a1.away_result = '0 - 0' AND a1.away_points_deducted > 0"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures b ON b.fixture_id = a.fixture_id AND b.away_points > b.home_points"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures c ON c.fixture_id = a.fixture_id AND c.away_points = c.home_points AND c.away_result <> '0 - 0'"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures d ON d.fixture_id = a.fixture_id AND d.away_points < d.home_points"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures e ON e.fixture_id = a.fixture_id"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures f ON f.fixture_id = a.fixture_id "
        strSQL = strSQL + " WHERE a.League = '" & FixtureLeague & "'"
        strSQL = strSQL + " AND t.long_name <> 'BYE'"
        strSQL = strSQL + " AND CONVERT(VARCHAR(10),a.fixture_calendar,112) <= '" + FixtureDate + "'"
        strSQL = strSQL + " AND a.fixture_id <> " & CompID
        strSQL = strSQL + " GROUP BY t.long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            ThisTeam = myDataReader.Item("team")
            BeforePlayed = myDataReader.Item("pld")
            BeforeWon = myDataReader.Item("w")
            BeforeDrawn = myDataReader.Item("d")
            BeforeLost = myDataReader.Item("l")
            BeforePoints = myDataReader.Item("pts")
            BeforeDeducted = myDataReader.Item("Deducted")
            strSQL = "UPDATE clubs.before_tables SET Pld=Pld+" & BeforePlayed & ",W=W+" & BeforeWon & ",D=D+" & BeforeDrawn & ",L=L+" & BeforeLost & ",pts=Pts+" & (BeforePoints - BeforeDeducted) & ",Deducted=Deducted+" & BeforeDeducted
            strSQL = strSQL & " WHERE team = '" & ThisTeam & "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        objGlobals.close_connection()

        strSQL = "SELECT * FROM clubs.before_tables ORDER BY Pts DESC,pld,W"
        myDataReader = objGlobals.SQLSelect(strSQL)

        Dim LastPoints As Double = 0
        Dim Pos1 As Integer = 0
        Dim Pos2 As Integer = 0
        While myDataReader.Read()
            If LastPoints = 0 And Pos1 = 0 Then
                Pos1 = 1
                LastPoints = myDataReader.Item("pts")
            End If
            Pos2 = Pos2 + 1
            If LastPoints <> myDataReader.Item("pts") Then
                Pos1 = Pos2
                LastPoints = myDataReader.Item("pts")
            End If
            strSQL = "UPDATE clubs.before_tables SET Pos = " + CStr(Pos1)
            strSQL = strSQL + " WHERE team = '" + myDataReader.Item("team") + "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        objGlobals.close_connection()

    End Sub

    Protected Sub create_tables_after(ByVal inDate As String)
        Dim myDataReader2 As OleDbDataReader
        Dim ThisTeam As String = ""

        Dim AfterPlayed As Integer = 0
        Dim AfterWon As Integer = 0
        Dim AfterDrawn As Integer = 0
        Dim AfterLost As Integer = 0
        Dim AfterPoints As Double = 0
        Dim AfterDeducted As Integer = 0
        Dim tempSeason As String
        tempSeason = objGlobals.get_current_season

        strSQL = "TRUNCATE TABLE clubs.after_tables"
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

        strSQL = "SELECT DISTINCT(Team) FROM clubs.vw_tables WHERE league = '" & FixtureLeague & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ThisTeam = myDataReader.Item(0)
            strSQL = "INSERT INTO clubs.after_tables VALUES ('" & tempSeason & "','" & FixtureLeague & "',1,'" & ThisTeam & "',0,0,0,0,0,0)"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        objGlobals.close_connection()

        strSQL = "SELECT t.long_name as Team,"
        strSQL = strSQL + " COUNT(a.home_team_name) - COUNT(a1.home_team_name) AS Pld, "
        strSQL = strSQL + " COUNT(b.home_points) AS W, "
        strSQL = strSQL + " COUNT(c.home_points) AS D,"
        strSQL = strSQL + " COUNT(d.home_points) AS L,"
        strSQL = strSQL + " SUM(CASE WHEN e.home_points IS NULL THEN 0 ELSE e.home_points END) AS Pts,"
        strSQL = strSQL + " SUM(CASE WHEN f.home_points_deducted IS NULL THEN 0 ELSE f.home_points_deducted END) AS Deducted"
        strSQL = strSQL + " FROM clubs.vw_teams t"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures AS a ON a.League = t.League AND a.home_team_name = t.long_name AND (a.home_result <> '0 - 0' OR (a.home_result = '0 - 0' AND a.home_points_deducted > 0))"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures AS a1 ON a1.fixture_id = a.fixture_id AND a1.home_result = '0 - 0' AND a1.home_points_deducted > 0"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures b ON b.fixture_id = a.fixture_id AND b.home_points > b.away_points"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures c ON c.fixture_id = a.fixture_id AND c.home_points = c.away_points AND c.home_result <> '0 - 0'"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures d ON d.fixture_id = a.fixture_id AND d.home_points < d.away_points"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures e ON e.fixture_id = a.fixture_id"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures f ON f.fixture_id = a.fixture_id "
        strSQL = strSQL + " WHERE t.League = '" & FixtureLeague & "'"
        strSQL = strSQL + " AND t.long_name <> 'BYE'"
        strSQL = strSQL + " AND CONVERT(VARCHAR(10),a.fixture_calendar,112) <= '" + inDate + "'"
        strSQL = strSQL + " GROUP BY t.long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            ThisTeam = myDataReader.Item("team")
            AfterPlayed = myDataReader.Item("pld")
            AfterWon = myDataReader.Item("w")
            AfterDrawn = myDataReader.Item("d")
            AfterLost = myDataReader.Item("l")
            AfterPoints = myDataReader.Item("pts")
            AfterDeducted = myDataReader.Item("Deducted")
            strSQL = "UPDATE clubs.after_tables SET Pld=" & AfterPlayed & ",W=" & AfterWon & ",D=" & AfterDrawn & ",L=" & AfterLost & ",pts=" & (AfterPoints - AfterDeducted) & ",Deducted=" & AfterDeducted
            strSQL = strSQL & " WHERE team = '" & ThisTeam & "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        objGlobals.close_connection()

        strSQL = "SELECT t.long_name as Team,"
        strSQL = strSQL + " COUNT(a.away_team_name) - COUNT(a1.away_team_name) AS Pld, "
        strSQL = strSQL + " COUNT(b.away_points) AS W, "
        strSQL = strSQL + " COUNT(c.away_points) AS D,"
        strSQL = strSQL + " COUNT(d.away_points) AS L,"
        strSQL = strSQL + " SUM(CASE WHEN e.away_points IS NULL THEN 0 ELSE e.away_points END) AS Pts,"
        strSQL = strSQL + " SUM(CASE WHEN f.away_points_deducted IS NULL THEN 0 ELSE f.away_points_deducted END) AS Deducted"
        strSQL = strSQL + " FROM clubs.vw_teams t"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures AS a ON a.League = t.League AND a.away_team_name = t.long_name AND (a.away_result <> '0 - 0' OR (a.away_result = '0 - 0' AND a.away_points_deducted > 0))"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures AS a1 ON a1.fixture_id = a.fixture_id AND a1.away_result = '0 - 0' AND a1.away_points_deducted > 0"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures b ON b.fixture_id = a.fixture_id AND b.away_points > b.home_points"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures c ON c.fixture_id = a.fixture_id AND c.away_points = c.home_points AND c.away_result <> '0 - 0'"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures d ON d.fixture_id = a.fixture_id AND d.away_points < d.home_points"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures e ON e.fixture_id = a.fixture_id"
        strSQL = strSQL + " LEFT OUTER JOIN clubs.vw_fixtures f ON f.fixture_id = a.fixture_id "
        strSQL = strSQL + " WHERE a.League = '" & FixtureLeague & "'"
        strSQL = strSQL + " AND t.long_name <> 'BYE'"
        strSQL = strSQL + " AND CONVERT(VARCHAR(10),a.fixture_calendar,112) <= '" + inDate + "'"
        strSQL = strSQL + " GROUP BY t.long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            ThisTeam = myDataReader.Item("team")
            AfterPlayed = myDataReader.Item("pld")
            AfterWon = myDataReader.Item("w")
            AfterDrawn = myDataReader.Item("d")
            AfterLost = myDataReader.Item("l")
            AfterPoints = myDataReader.Item("pts")
            AfterDeducted = myDataReader.Item("Deducted")
            strSQL = "UPDATE clubs.after_tables SET Pld=Pld+" & AfterPlayed & ",W=W+" & AfterWon & ",D=D+" & AfterDrawn & ",L=L+" & AfterLost & ",pts=Pts+" & (AfterPoints - AfterDeducted) & ",Deducted=Deducted+" & AfterDeducted
            strSQL = strSQL & " WHERE team = '" & ThisTeam & "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        objGlobals.close_connection()

        strSQL = "SELECT * FROM clubs.after_tables ORDER BY Pts DESC,pld,W"
        myDataReader = objGlobals.SQLSelect(strSQL)

        Dim LastPoints As Double = 0
        Dim Pos1 As Integer = 0
        Dim Pos2 As Integer = 0
        While myDataReader.Read()
            If LastPoints = 0 And Pos1 = 0 Then
                Pos1 = 1
                LastPoints = myDataReader.Item("pts")
            End If
            Pos2 = Pos2 + 1
            If LastPoints <> myDataReader.Item("pts") Then
                Pos1 = Pos2
                LastPoints = myDataReader.Item("pts")
            End If
            strSQL = "UPDATE clubs.after_tables SET Pos = " + CStr(Pos1)
            strSQL = strSQL + " WHERE team = '" + myDataReader.Item("team") + "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        objGlobals.close_connection()

    End Sub

    Protected Sub rbResults_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rbResults.SelectedIndexChanged
        Call update_result(rbResults.SelectedItem.Text)
        Select Case Left(FixtureLeague, 4)
            Case "SKIT"
                Call load_skittles_header()
                Call load_skittles_result()
                Call load_skittles_totals()
            Case "CRIB"
                Call load_crib_header()
                Call load_crib_result()
                Call load_crib_totals()
            Case "SNOO"
                Call load_snooker_header()
                Call load_snooker_result()
                Call load_snooker_totals()
        End Select
    End Sub

    Protected Sub update_result(ByVal inResult As String)
        Dim HomeScore As String = ""
        Dim AwayScore As String = ""
        Dim AwayString As String = ""
        Dim DashPos As Integer
        Dim HomeResult1 As String = ""
        Dim HomeResult2 As String = ""
        Dim AwayResult1 As String = ""
        Dim AwayResult2 As String = ""
        Dim i As Integer
        Dim NewStatus As Integer

        If inResult = "Postponed" Then
            HomeResult1 = "0 - 0"
            HomeScore = "0"
            HomePoints = 0
            AwayResult1 = "0 - 0"
            AwayScore = "0"
            AwayPoints = 0
            NewStatus = -1
        Else
            DashPos = InStr(inResult, "-")
            HomeResult1 = Replace(inResult, "-", " - ") + "*"
            HomeResult1 = Replace(HomeResult1, "0½", "½")
            HomeResult2 = Replace(HomeResult1, "½", ".5")
            DashPos = InStr(HomeResult1, "-")
            HomeScore = Left(HomeResult2, DashPos - 1)
            HomePoints = HomeScore

            For i = DashPos + 2 To Len(HomeResult1)
                If Mid(HomeResult1, i, 1) <> "*" Then
                    AwayString = AwayString + Mid(HomeResult1, i, 1)
                Else
                    Exit For
                End If
            Next i

            AwayResult1 = AwayString & " - " & Left(HomeResult1, DashPos - 2)
            AwayResult2 = Replace(AwayString, "0½", "½")
            AwayScore = Replace(AwayResult2, "½", ".5")
            AwayPoints = AwayScore

            HomeResult1 = Left(HomeResult1, Len(HomeResult1) - 1)        ' remove *
            home_result = HomeResult1
        End If

        HomePointsDeducted = Val(ddHomePointsDeducted.SelectedValue)
        AwayPointsDeducted = Val(ddAwayPointsDeducted.SelectedValue)

        strSQL = "UPDATE clubs.vw_fixtures SET home_points=" & Convert.ToDouble(HomeScore)
        strSQL = strSQL & ",away_points=" & Convert.ToDouble(AwayScore)
        strSQL = strSQL & ",home_result='" & HomeResult1 & "'"
        strSQL = strSQL & ",away_result='" & AwayResult1 & "'"
        strSQL = strSQL & ",home_points_deducted=" & ddHomePointsDeducted.SelectedValue
        strSQL = strSQL & ",away_points_deducted=" & ddAwayPointsDeducted.SelectedValue
        strSQL = strSQL & " WHERE fixture_id=" & CompID
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()



        strSQL = "SELECT"
        strSQL = strSQL & " t.long_name,"
        strSQL = strSQL & " SUM(h.Pld+a.Pld) AS Pld,"
        strSQL = strSQL & " SUM(h.W+a.W) AS W,"
        strSQL = strSQL & " SUM(h.D+a.D) AS D,"
        strSQL = strSQL & " SUM(h.L+a.L) AS L,"
        strSQL = strSQL & " SUM(h.Pts+a.Pts) AS Pts,"
        strSQL = strSQL & " SUM(h.Pts+a.Pts)-SUM(h.Deducted+a.Deducted) AS Reduced_Pts,"
        strSQL = strSQL & " SUM(h.Deducted+a.Deducted) AS Deducted"
        strSQL = strSQL & " FROM clubs.vw_teams t"
        'strSQL = strSQL & " LEFT OUTER JOIN clubs.vw_home_tables h ON (h.League = t.League AND h.Team = t.long_name)" change LOJ to JOIN 30.11.17
        strSQL = strSQL & " JOIN clubs.vw_home_tables h ON (h.League = t.League AND h.Team = t.long_name)"
        'strSQL = strSQL & " LEFT OUTER JOIN clubs.vw_away_tables a ON (a.League = t.League AND a.Team = t.long_name)" change LOJ to JOIN 30.11.17
        strSQL = strSQL & " JOIN clubs.vw_away_tables a ON (a.League = t.League AND a.Team = t.long_name)"
        strSQL = strSQL & " WHERE t.League = '" & FixtureLeague & "'"
        strSQL = strSQL & " AND t.long_name <> 'BYE'"
        strSQL = strSQL & " GROUP BY t.long_name"
        strSQL = strSQL & " ORDER BY Reduced_Pts DESC"
        i = 0
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            Pos(i) = 0
            Team(i) = myDataReader.Item(0)
            Played(i) = myDataReader.Item("pld")
            Won(i) = myDataReader.Item("w")
            Drawn(i) = myDataReader.Item("d")
            Lost(i) = myDataReader.Item("l")
            Deducted(i) = myDataReader.Item("Deducted")
            Points(i) = myDataReader.Item("Reduced_Pts")
            i = i + 1
        End While
        objGlobals.close_connection()

        'end 20/9/13

        Call sort_table()
        Call re_update_table("Tables")
        Call load_table(gridTable2, "Tables")

        '17/9/13 update the tables by week in Tables_Week
        If FixtureWeek <> objGlobals.GetCurrentWeek Then
            're-update all weeks from the fixture week to the current week + 2 (in case any games are played up tp 2 weeks early)
            Dim wk As Integer
            'For wk = FixtureWeek To objGlobals.GetCurrentWeek
            For wk = 1 To objGlobals.GetCurrentWeek + 2
                Call create_tables_after(get_end_week_date(wk))
                Call update_weekly_tables("after_tables", wk)
            Next wk
        Else
            Call update_weekly_tables("Tables", FixtureWeek)
        End If
        '17/9/13 end

        Dim UKDateTime As Date = objGlobals.LondonDate(DateTime.UtcNow)
        Dim UKDate As String = Format(UKDateTime.Year, "0000") + Format(UKDateTime.Month, "00") + Format(UKDateTime.Day, "00")
        Dim UKTime As String = Format(UKDateTime.Hour, "00") & Format(UKDateTime.Minute, "00")

        If InStr(inResult, "0-0") > 0 Then NewStatus = 0

        '28.7.14 - add Status
        If InStr(inResult, "0-0") > 0 Or inResult = "Postponed" Then
            'reset status if no result
            strSQL = "UPDATE clubs.vw_fixtures SET Status = " & NewStatus & " WHERE fixture_id = " & CompID
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            strSQL = "DELETE FROM clubs.vw_fixtures_detail WHERE fixture_id = " & CompID
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            Select Case Left(FixtureLeague, 4)
                Case "CRIB"
                    Call update_player_stats("sp_update_player_stats_Crib")
                Case "SNOO"
                    Call update_player_stats("sp_update_player_stats_Snooker")
                Case "SKIT"
                    Call update_player_stats("sp_update_player_stats_Skittles")
            End Select
            're-update league AND team positions
            strSQL = "EXEC clubs.sp_update_league_position '" & FixtureLeague & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            strSQL = "EXEC clubs.sp_update_team_position '" & FixtureLeague & "','" & FixtureHomeTeam & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            strSQL = "EXEC clubs.sp_update_team_position '" & FixtureLeague & "','" & FixtureAwayTeam & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()
        Else
            strSQL = "UPDATE clubs.vw_fixtures SET Status = " & FixtureStatus() & " WHERE fixture_id = " & CompID
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()
        End If
        '28.7.14 - end

        '19.11.18 - update fixture_combined table
        Call objGlobals.update_fixtures_combined("clubs")

        write_fixtures_FTP()

        strSQL = "UPDATE clubs.last_changed SET date_time_changed = '" & UKDate & UKTime & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()


        btnCancel.Visible = False
        btnOK.Visible = True

    End Sub

    Sub write_fixtures_FTP()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim l_param_in_names(0) As String
        Dim l_param_in_values(0) As String

        l_param_in_names(0) = "@inFixtureID"
        l_param_in_values(0) = CompID

        strSQL = "EXEC [clubs].[sp_write_fixtures_FTP] '" & objGlobals.current_season & "'," & l_param_in_values(0)
        myDataReader = objGlobals.SQLSelect(strSQL)

        objGlobals.close_connection()
    End Sub

    Sub update_player_stats(inStoredProcedure As String)
        Dim myDataReader2 As OleDbDataReader
        Dim tempSeason As String
        tempSeason = objGlobals.get_current_season
        inStoredProcedure = "clubs." & inStoredProcedure
        'update the home team stats
        strSQL = "SELECT player FROM clubs.vw_players WHERE league = '" & FixtureLeague & "' AND team = '" & FixtureHomeTeam & "' AND player NOT LIKE 'A N OTHER%' "
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            strSQL = "EXEC " & inStoredProcedure & " '" & tempSeason & "','" & FixtureLeague & "','" & FixtureHomeTeam & "','" & myDataReader.Item("player") & "'"
            If inStoredProcedure.ToUpper.Contains("SKITTLES") Then strSQL &= "," & CompID
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        objGlobals.close_connection()
        'update the away team stats
        strSQL = "SELECT player FROM clubs.vw_players WHERE league = '" & FixtureLeague & "' AND team = '" & FixtureAwayTeam & "' AND player NOT LIKE 'A N OTHER%' "
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            strSQL = "EXEC " & inStoredProcedure & " '" & tempSeason & "','" & FixtureLeague & "','" & FixtureAwayTeam & "','" & myDataReader.Item("player") & "'"
            If inStoredProcedure.ToUpper.Contains("SKITTLES") Then strSQL &= "," & CompID
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        objGlobals.close_connection()
    End Sub

    Protected Function FixtureStatus() As Integer
        FixtureStatus = 1
        strSQL = "SELECT COUNT(*) FROM clubs.vw_fixtures_detail WHERE fixture_id = " & CompID
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            If myDataReader.Item(0) > 0 Then FixtureStatus = 2
        End While
        objGlobals.close_connection()
    End Function

    Protected Function get_end_week_date(ByVal inWeek As Integer) As String
        get_end_week_date = ""
        strSQL = "SELECT week_commences FROM clubs.vw_weeks WHERE week_number = " & inWeek
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            get_end_week_date = Format(DateAdd(DateInterval.Day, 6, myDataReader.Item(0)), "yyyyMMdd")
        End While
        objGlobals.close_connection()
    End Function

    Protected Sub update_weekly_tables(ByVal inTable As String, ByVal inWeek As Integer)
        strSQL = "DELETE FROM clubs.vw_tables_week WHERE league = '" & FixtureLeague & "' AND Week = " & inWeek
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()
        strSQL = "INSERT INTO clubs.tables_week SELECT Season,league," & inWeek & ",pos,team,pts FROM clubs." & inTable & " WHERE league = '" & FixtureLeague & "' AND season = '" & objGlobals.get_current_season & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()
    End Sub

    Protected Sub sort_table()
        'sort array (by descending points)
        Dim tempTeam As String
        Dim tempPlayed As Integer
        Dim tempWon As Integer
        Dim tempDrawn As Integer
        Dim tempLost As Integer
        Dim tempPoints As Double
        Dim i As Integer
        For i = 0 To gridTable.Rows.Count - 1
            For j = i + 1 To gridTable.Rows.Count - 1
                If Points(i) < Points(j) Then
                    tempPoints = Points(i)
                    Points(i) = Points(j)
                    Points(j) = tempPoints

                    tempPlayed = Played(i)
                    Played(i) = Played(j)
                    Played(j) = tempPlayed

                    tempTeam = Team(i)
                    Team(i) = Team(j)
                    Team(j) = tempTeam

                    tempWon = Won(i)
                    Won(i) = Won(j)
                    Won(j) = tempWon

                    tempDrawn = Drawn(i)
                    Drawn(i) = Drawn(j)
                    Drawn(j) = tempDrawn

                    tempLost = Lost(i)
                    Lost(i) = Lost(j)
                    Lost(j) = tempLost
                End If
            Next j
        Next i

        Dim LastMatch As Double = 0
        Dim Pos1 As Integer = 0
        Dim Pos2 As Integer = 0
        For i = 0 To gridTable.Rows.Count - 1
            If LastMatch = 0 And Pos1 = 0 Then
                Pos1 = 1
                LastMatch = Points(i)
            End If
            Pos2 = Pos2 + 1
            If LastMatch <> Points(i) Then
                Pos1 = Pos2
                LastMatch = Points(i)
            End If
            Pos(i) = Pos1

        Next

    End Sub

    Protected Sub re_update_table(inTable As String)
        Dim tempSeason As String
        tempSeason = objGlobals.get_current_season
        're-update the TABLES table
        strSQL = "DELETE FROM clubs.vw_" & inTable & " WHERE league ='" & FixtureLeague & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()


        For i = 0 To gridTable.Rows.Count - 1
            strSQL = "INSERT INTO clubs." & inTable & " VALUES" & " ('"
            strSQL = strSQL & tempSeason & "','"
            strSQL = strSQL & FixtureLeague & "',"
            strSQL = strSQL & Pos(i) & ",'"
            strSQL = strSQL & Team(i) & "',"
            strSQL = strSQL & Played(i) & ","
            strSQL = strSQL & Won(i) & ","
            strSQL = strSQL & Drawn(i) & ","
            strSQL = strSQL & Lost(i) & ","
            strSQL = strSQL & Points(i) & ","
            strSQL = strSQL & Deducted(i) & ")"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

        Next

    End Sub

    Function get_deducted(inSeason As String, inLeague As String, inTeam As String) As Integer
        get_deducted = 0
        strSQL = "SELECT SUM(home_points_deducted) FROM clubs.vw_fixtures WHERE league = '" & inLeague & "' AND home_team_name = '" & inTeam & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            get_deducted = get_deducted + myDataReader.Item(0)
        End While
        objGlobals.close_connection()

        strSQL = "SELECT SUM(away_points_deducted) FROM clubs.vw_fixtures WHERE league = '" & inLeague & "' AND away_team_name = '" & inTeam & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            get_deducted = get_deducted + myDataReader.Item(0)
        End While
        objGlobals.close_connection()

    End Function


    Sub load_table(inGrid As GridView, inTable As String)

        If inTable = "Tables" Then lblTableAfter.Visible = True

        dt = New DataTable

        'add header row
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Pld", GetType(System.String)))
        dt.Columns.Add(New DataColumn("W", GetType(System.String)))
        dt.Columns.Add(New DataColumn("D", GetType(System.String)))
        dt.Columns.Add(New DataColumn("L", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Deducted", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Pts", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Move", GetType(System.String)))

        strSQL = "SELECT * FROM clubs." & inTable & " WHERE season = '" & objGlobals.current_season & "' AND league = '" & FixtureLeague & "' ORDER BY pos"
        myDataReader = objGlobals.SQLSelect(strSQL)

        Dim iRow As Integer = 0
        While myDataReader.Read()
            With inGrid
                iRow = iRow + 1
                dr = dt.NewRow
                dr("Pos") = myDataReader.Item("pos")
                dr("Team") = myDataReader.Item("team")
                dr("Pld") = myDataReader.Item("pld")
                dr("W") = myDataReader.Item("w")
                dr("D") = myDataReader.Item("d")
                dr("L") = myDataReader.Item("l")
                If myDataReader.Item("deducted") > 0 Then
                    dr("Deducted") = myDataReader.Item("deducted") * -1
                End If
                dr("Pts") = myDataReader.Item("pts")
                If myDataReader.Item("deducted") > 0 Then
                    dr("Pts") = "*" & dr("Pts")
                End If
                dr("Move") = ""
                dt.Rows.Add(dr)
            End With
        End While
        objGlobals.close_connection()

        inGrid.DataSource = dt
        inGrid.DataBind()

        For i = 0 To inGrid.Rows.Count - 1
            Pos(i) = 0
            Team(i) = ""
            Played(i) = 0
            Won(i) = 0
            Drawn(i) = 0
            Lost(i) = 0
            Points(i) = 0
            If Not (inGrid.Rows(i).Cells(1).Text = FixtureHomeTeam _
                Or inGrid.Rows(i).Cells(1).Text = FixtureAwayTeam) Then
                inGrid.Rows(i).Cells(0).ForeColor = Gray
                inGrid.Rows(i).Cells(1).ForeColor = Gray
                inGrid.Rows(i).Cells(2).ForeColor = Gray
                inGrid.Rows(i).Cells(3).ForeColor = Gray
                inGrid.Rows(i).Cells(4).ForeColor = Gray
                inGrid.Rows(i).Cells(5).ForeColor = Gray
                inGrid.Rows(i).Cells(6).ForeColor = Gray
                inGrid.Rows(i).Cells(7).ForeColor = Gray
            End If
            Pos(i) = inGrid.Rows(i).Cells(0).Text
            Team(i) = inGrid.Rows(i).Cells(1).Text
            Played(i) = inGrid.Rows(i).Cells(2).Text
            Won(i) = inGrid.Rows(i).Cells(3).Text
            If Left(FixtureLeague, 4) <> "CRIB" Then
                Drawn(i) = inGrid.Rows(i).Cells(4).Text
            Else
                Drawn(i) = 0
            End If
            Lost(i) = inGrid.Rows(i).Cells(5).Text
            Points(i) = Val(inGrid.Rows(i).Cells(7).Text)
        Next i

        Dim OldPosHome As Integer = 0
        Dim OldPosAway As Integer = 0
        Dim NewPosHome As Integer = 0
        Dim NewPosAway As Integer = 0
        Dim PosDiff As Integer = 0
        For i = 0 To gridTable.Rows.Count - 1
            If gridTable.Rows(i).Cells(1).Text = FixtureHomeTeam Then
                OldPosHome = Val(gridTable.Rows(i).Cells(0).Text)
            End If
            If gridTable.Rows(i).Cells(1).Text = FixtureAwayTeam Then
                OldPosAway = Val(gridTable.Rows(i).Cells(0).Text)
            End If
        Next
        For i = 0 To gridTable2.Rows.Count - 1
            If gridTable2.Rows(i).Cells(1).Text = FixtureHomeTeam Then
                NewPosHome = Val(gridTable2.Rows(i).Cells(0).Text)
                PosDiff = NewPosHome - OldPosHome
                Select Case PosDiff
                    Case 0
                        gridTable2.Rows(i).Cells(8).Text = "-"
                    Case Is > 0
                        gridTable2.Rows(i).Cells(8).Text = "Down " & PosDiff
                    Case Is < 0
                        gridTable2.Rows(i).Cells(8).Text = "Up " & Math.Abs(PosDiff)
                End Select
            End If
            If gridTable2.Rows(i).Cells(1).Text = FixtureAwayTeam Then
                NewPosAway = Val(gridTable2.Rows(i).Cells(0).Text)
                PosDiff = NewPosAway - OldPosAway
                Select Case PosDiff
                    Case 0
                        gridTable2.Rows(i).Cells(8).Text = "-"
                    Case Is > 0
                        gridTable2.Rows(i).Cells(8).Text = "Down " & PosDiff
                    Case Is < 0
                        gridTable2.Rows(i).Cells(8).Text = "Up " & Math.Abs(PosDiff)
                End Select
            End If
        Next
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        If objGlobals.TeamSelected Is Nothing Then
            Response.Redirect("~/Clubs/Default.aspx?Week=" & FixtureWeek)
        Else
            Response.Redirect("~/Clubs/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected)
        End If
    End Sub

    Protected Sub btnOK_Click(sender As Object, e As System.EventArgs) Handles btnOK.Click
        If objGlobals.TeamSelected Is Nothing Then
            Response.Redirect("~/Clubs/Default.aspx?Week=" & FixtureWeek)
        Else
            Response.Redirect("~/Clubs/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected)
        End If
    End Sub

    Sub load_skittles_header()
        dt = New DataTable

        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Match", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Points", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Points", GetType(System.String)))
        'dt.Columns.Add(New DataColumn("Number Nines", GetType(System.String)))

        'add header rows
        dr = dt.NewRow
        dr("Match") = "SKITTLES RESULT CARD"
        dr("Away Points") = CompID
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Home Player") = "Division : " & FixtureLeague
        dr("Away Player") = FixtureFullDate
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Home Player") = "Home Team : " & FixtureHomeTeam
        dr("Away Player") = "Away Team : " & FixtureAwayTeam
        dt.Rows.Add(dr)


        If Not lblNoCard.Visible Then
            dr = dt.NewRow
            'dr("Number Nines") = "AwayTeams"
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Home Player") = "Player"
            dr("Home Points") = "Score"
            dr("Away Player") = "Player"
            dr("Away Points") = "Score"
            'dr("Number Nines") = "No. of 9+'s"
            dt.Rows.Add(dr)
        End If

    End Sub

    Sub load_skittles_result()
        TotalHome = 0
        TotalAway = 0
        strSQL = "SELECT * FROM clubs.vw_fixtures_detail WHERE fixture_id = " & CompID & " ORDER BY match"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            dr = dt.NewRow
            TotalHome = TotalHome + myDataReader.Item("home_points")
            TotalAway = TotalAway + myDataReader.Item("away_points")
            dr("Match") = myDataReader.Item("match")
            dr("Home Player") = myDataReader.Item("home_player")
            dr("Home Points") = myDataReader.Item("home_points")
            dr("Away Player") = myDataReader.Item("away_player")
            dr("Away Points") = myDataReader.Item("away_points")
            'If Not IsDBNull(myDataReader.Item("away_nines")) Then
            '    dr("Number Nines") = myDataReader.Item("away_nines")
            'Else
            '    dr("Number Nines") = Nothing
            'End If
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()

    End Sub

    Sub load_skittles_totals()
        dr = dt.NewRow
        dr("Home Player") = "PLAYERS TOTAL :"
        dr("Home Points") = TotalHome
        dr("Away Player") = "PLAYERS TOTAL :"
        dr("Away Points") = TotalAway
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Home Player") = "POINTS :"
        dr("Home Points") = HomePoints
        dr("Away Player") = "POINTS :"
        dr("Away Points") = AwayPoints
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Home Player") = "POINTS DEDUCTED :"
        dr("Home Points") = HomePointsDeducted
        dr("Away Player") = "POINTS DEDUCTED :"
        dr("Away Points") = AwayPointsDeducted
        dt.Rows.Add(dr)

        gridSkittlesResult.Visible = True
        gridSkittlesResult.DataSource = dt
        gridSkittlesResult.DataBind()
    End Sub

    Sub load_crib_header()
        dt = New DataTable

        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Match", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Points", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Points", GetType(System.String)))

        'add header rows
        dr = dt.NewRow
        dr("Match") = "CRIB RESULT CARD"
        dr("Away Points") = CompID
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Away Player") = FixtureFullDate
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Home Player") = "Home Team : " & FixtureHomeTeam
        dr("Away Player") = "Away Team : " & FixtureAwayTeam
        dt.Rows.Add(dr)


        If Not lblNoCard.Visible Then
            dr = dt.NewRow
            dt.Rows.Add(dr)
            dr = dt.NewRow
            dr("Home Player") = "Pair"
            dr("Home Points") = "Score"
            dr("Away Player") = "Pair"
            dr("Away Points") = "Score"
            dt.Rows.Add(dr)
        End If
    End Sub

    Sub load_crib_result()
        TotalHome = 0
        TotalAway = 0
        strSQL = "SELECT * FROM clubs.vw_fixtures_detail WHERE fixture_id = " & CompID & " ORDER BY match"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            TotalHome = TotalHome + myDataReader.Item("home_points")
            TotalAway = TotalAway + myDataReader.Item("away_points")
            dr = dt.NewRow
            dr("Match") = myDataReader.Item("match")
            dr("Home Player") = myDataReader.Item("home_player")
            dr("Home Points") = myDataReader.Item("home_points")
            dr("Away Player") = myDataReader.Item("away_player")
            dr("Away Points") = myDataReader.Item("away_points")
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Home Player") = myDataReader.Item("home_partner")
            dr("Away Player") = myDataReader.Item("away_partner")
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()
    End Sub

    Sub load_crib_totals()
        dr = dt.NewRow
        dr("Home Player") = "POINTS :"
        If lblNoCard.Visible Then
            dr("Home Points") = HomePoints
            dr("Away Points") = AwayPoints
        Else
            dr("Home Points") = TotalHome
            dr("Away Points") = TotalAway
        End If
        dr("Away Player") = "POINTS :"
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Home Player") = "POINTS DEDUCTED :"
        dr("Home Points") = HomePointsDeducted
        dr("Away Player") = "POINTS DEDUCTED :"
        dr("Away Points") = AwayPointsDeducted
        dt.Rows.Add(dr)

        gridCribResult.Visible = True
        gridCribResult.DataSource = dt
        gridCribResult.DataBind()
    End Sub

    Sub load_snooker_header()
        dt = New DataTable

        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Match", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Points", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Points", GetType(System.String)))

        'add header rows
        dr = dt.NewRow
        dr("Match") = "SNOOKER RESULT CARD"
        dr("Away Points") = CompID
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Home Player") = "Division : " & FixtureLeague
        dr("Away Player") = FixtureFullDate
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Home Player") = "Home Team : " & FixtureHomeTeam
        dr("Away Player") = "Away Team : " & FixtureAwayTeam
        dt.Rows.Add(dr)


        If Not lblNoCard.Visible Then
            dr = dt.NewRow
            dt.Rows.Add(dr)
            dr = dt.NewRow
            dr("Home Player") = "Player"
            dr("Home Points") = "Score"
            dr("Away Player") = "Player"
            dr("Away Points") = "Score"
            dt.Rows.Add(dr)
        End If
    End Sub

    Sub load_snooker_result()
        TotalHome = 0
        TotalAway = 0
        strSQL = "SELECT * FROM clubs.vw_fixtures_detail WHERE fixture_id = " & CompID & " ORDER BY match"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            TotalHome = TotalHome + myDataReader.Item("home_points")
            TotalAway = TotalAway + myDataReader.Item("away_points")
            dr = dt.NewRow
            dr("Match") = myDataReader.Item("match")
            dr("Home Player") = myDataReader.Item("home_player")
            dr("Home Points") = myDataReader.Item("home_points")
            dr("Away Player") = myDataReader.Item("away_player")
            dr("Away Points") = myDataReader.Item("away_points")
            dt.Rows.Add(dr)
        End While
        objGlobals.close_connection()
    End Sub

    Sub load_snooker_totals()
        dr = dt.NewRow
        dr("Home Player") = "POINTS :"
        If lblNoCard.Visible Then
            dr("Home Points") = HomePoints
            dr("Away Points") = AwayPoints
        Else
            dr("Home Points") = TotalHome
            dr("Away Points") = TotalAway
        End If
        dr("Away Player") = "POINTS :"
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Home Player") = "POINTS DEDUCTED :"
        dr("Home Points") = HomePointsDeducted
        dr("Away Player") = "POINTS DEDUCTED :"
        dr("Away Points") = AwayPointsDeducted
        dt.Rows.Add(dr)

        gridSnookerResult.Visible = True
        gridSnookerResult.DataSource = dt
        gridSnookerResult.DataBind()
    End Sub


    'Protected Sub Process_SQL(inSQL As String)
    '    Dim Deducted As String = ""
    '    FixtureDetail = False
    '    Select Case (Left(inSQL, 6))
    '        Case "RESULT"
    '            Result = RTrim(Mid(inSQL, 24, 10))
    '            For i = 0 To rbResults.Items.Count - 1
    '                If Result = rbResults.Items(i).Text Then rbResults.SelectedIndex = i
    '            Next
    '        Case "HOME P"
    '            Deducted = RTrim(Mid(inSQL, 24, 4))
    '            ddHomePointsDeducted.SelectedValue = Deducted
    '        Case "AWAY P"
    '            Deducted = RTrim(Mid(inSQL, 24, 4))
    '            ddAwayPointsDeducted.SelectedValue = Deducted
    '            Call update_result(Result)
    '        Case Else
    '            FixtureDetail = True
    '            myDataReader = objGlobals.SQLSelect(inSQL)      'execute the SQL statement
    '            objGlobals.close_connection()

    '            Call update_log(inSQL)
    '    End Select
    'End Sub

    Private Sub gridSkittlesResult_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSkittlesResult.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Select Case e.Row.Cells(1).Text
                Case "PLAYERS TOTAL :"
                    e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
                    'e.Row.Cells(5).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                    'e.Row.Cells(5).BorderColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                Case "POINTS :"
                    e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(1).Font.Bold = True
                    e.Row.Cells(2).Font.Bold = True
                    e.Row.Cells(3).Font.Bold = True
                    e.Row.Cells(4).Font.Bold = True
                    'e.Row.Cells(5).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                    'e.Row.Cells(5).BorderColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                Case "POINTS DEDUCTED :"
                    e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
                    'e.Row.Cells(5).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                    'e.Row.Cells(5).BorderColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                Case Else
                    If gRow > 5 Then
                        Dim hLink1 As New HyperLink
                        hLink1.NavigateUrl = "~/Clubs/Stats.aspx?League=" & FixtureLeague & "&Team=" & FixtureHomeTeam & "&Player=" & dt.Rows(gRow - 1)(1).ToString
                        hLink1.ForeColor = Black
                        hLink1.Text = e.Row.Cells(1).Text
                        If objGlobals.PlayerSelected = e.Row.Cells(1).Text Then
                            hLink1.BackColor = Red
                        End If
                        e.Row.Cells(1).Controls.Add(hLink1)

                        Dim hLink2 As New HyperLink
                        hLink2.NavigateUrl = "~/Clubs/Stats.aspx?League=" & FixtureLeague & "&Team=" & FixtureAwayTeam & "&Player=" & dt.Rows(gRow - 1)(3).ToString
                        hLink2.ForeColor = Black
                        hLink2.Text = e.Row.Cells(3).Text
                        If objGlobals.PlayerSelected = e.Row.Cells(3).Text Then
                            hLink2.BackColor = Red
                        End If
                        e.Row.Cells(3).Controls.Add(hLink2)
                    Else
                        Select Case gRow
                            Case 1
                                e.Row.Cells(0).Font.Bold = True
                                e.Row.Cells(0).Font.Size = 14
                                e.Row.Cells(0).ColumnSpan = 4
                                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
                                e.Row.Cells(1).Visible = False
                                e.Row.Cells(2).Visible = False
                                e.Row.Cells(3).Visible = False
                                'e.Row.Cells(5).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                                'e.Row.Cells(5).BorderColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                            Case 2, 3
                                e.Row.Cells(0).BorderStyle = BorderStyle.None
                                e.Row.Cells(1).Font.Bold = True
                                e.Row.Cells(1).ColumnSpan = 2
                                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Left
                                e.Row.Cells(2).Visible = False

                                e.Row.Cells(3).Font.Bold = True
                                e.Row.Cells(3).ColumnSpan = 2
                                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Left
                                e.Row.Cells(4).Visible = False
                                'e.Row.Cells(5).BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                                'e.Row.Cells(5).BorderColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                            Case 4
                                e.Row.Cells(0).ColumnSpan = 5
                                e.Row.Cells(1).Visible = False
                                e.Row.Cells(2).Visible = False
                                e.Row.Cells(3).Visible = False
                                e.Row.Cells(4).Visible = False
                                'e.Row.Cells(5).BorderStyle = BorderStyle.None
                            Case 5
                                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
                        End Select
                    End If
            End Select
            'e.Row.CssClass = "row"
        Else
            e.Row.Visible = False
        End If
        gRow = gRow + 1

    End Sub

    Private Sub gridCribResult_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridCribResult.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Select Case e.Row.Cells(1).Text
                Case "TOTAL :"
                    e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
                Case "POINTS :"
                    e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(1).Font.Bold = True
                    e.Row.Cells(2).Font.Bold = True
                    e.Row.Cells(3).Font.Bold = True
                    e.Row.Cells(4).Font.Bold = True
                Case "POINTS DEDUCTED :"
                    e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
                Case Else
                    If gRow > 5 Then
                        Dim hLink1 As New HyperLink
                        hLink1.NavigateUrl = "~/Clubs/Stats.aspx?League=" & FixtureLeague & "&Team=" & FixtureHomeTeam & "&Player=" & dt.Rows(gRow - 1)(1).ToString
                        hLink1.ForeColor = Black
                        hLink1.Text = e.Row.Cells(1).Text
                        If gRow Mod 2 = 0 Then
                            hLink1.Text = e.Row.Cells(1).Text & " & "
                        End If
                        If objGlobals.PlayerSelected = e.Row.Cells(1).Text Then
                            hLink1.BackColor = Red
                        End If
                        e.Row.Cells(1).Controls.Add(hLink1)

                        Dim hLink2 As New HyperLink
                        hLink2.NavigateUrl = "~/Clubs/Stats.aspx?League=" & FixtureLeague & "&Team=" & FixtureAwayTeam & "&Player=" & dt.Rows(gRow - 1)(3).ToString
                        hLink2.ForeColor = Black
                        hLink2.Text = e.Row.Cells(3).Text
                        If gRow Mod 2 = 0 Then
                            hLink2.Text = e.Row.Cells(3).Text & " & "
                        End If
                        If objGlobals.PlayerSelected = e.Row.Cells(3).Text Then
                            hLink2.BackColor = Red
                        End If
                        e.Row.Cells(3).Controls.Add(hLink2)

                        Select Case gRow
                            Case 6, 8, 10
                                e.Row.Cells(0).RowSpan = 2
                                e.Row.Cells(2).RowSpan = 2
                                e.Row.Cells(4).RowSpan = 2
                            Case 7, 9, 11
                                e.Row.Cells(0).Visible = False
                                e.Row.Cells(2).Visible = False
                                e.Row.Cells(4).Visible = False


                        End Select
                    Else
                        Select Case gRow
                            Case 1
                                e.Row.Cells(0).Font.Bold = True
                                e.Row.Cells(0).Font.Size = 14
                                e.Row.Cells(0).ColumnSpan = 4
                                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
                                e.Row.Cells(1).Visible = False
                                e.Row.Cells(2).Visible = False
                                e.Row.Cells(3).Visible = False
                            Case 2, 3
                                e.Row.Cells(0).BorderStyle = BorderStyle.None
                                e.Row.Cells(1).Font.Bold = True
                                e.Row.Cells(1).ColumnSpan = 2
                                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Left
                                e.Row.Cells(2).Visible = False

                                e.Row.Cells(3).Font.Bold = True
                                e.Row.Cells(3).ColumnSpan = 2
                                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Left
                                e.Row.Cells(4).Visible = False
                            Case 4
                                e.Row.Cells(0).ColumnSpan = 5
                                e.Row.Cells(1).Visible = False
                                e.Row.Cells(2).Visible = False
                                e.Row.Cells(3).Visible = False
                                e.Row.Cells(4).Visible = False
                            Case 5
                                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
                        End Select
                    End If
            End Select
            'e.Row.CssClass = "row"
        Else
            e.Row.Visible = False
        End If
        gRow = gRow + 1

    End Sub


    Private Sub gridSnookerResult_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSnookerResult.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Select Case e.Row.Cells(1).Text
                Case "TOTAL :"
                    e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
                Case "POINTS :"
                    e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(1).Font.Bold = True
                    e.Row.Cells(2).Font.Bold = True
                    e.Row.Cells(3).Font.Bold = True
                    e.Row.Cells(4).Font.Bold = True
                Case "POINTS DEDUCTED :"
                    e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
                Case Else
                    If gRow > 5 Then
                        Dim hLink1 As New HyperLink
                        hLink1.NavigateUrl = "~/Clubs/Stats.aspx?League=" & FixtureLeague & "&Team=" & FixtureHomeTeam & "&Player=" & dt.Rows(gRow - 1)(1).ToString
                        hLink1.ForeColor = Black
                        hLink1.Text = e.Row.Cells(1).Text
                        If objGlobals.PlayerSelected = e.Row.Cells(1).Text Then
                            hLink1.BackColor = Red
                        End If
                        e.Row.Cells(1).Controls.Add(hLink1)

                        Dim hLink2 As New HyperLink
                        hLink2.NavigateUrl = "~/Clubs/Stats.aspx?League=" & FixtureLeague & "&Team=" & FixtureAwayTeam & "&Player=" & dt.Rows(gRow - 1)(3).ToString
                        hLink2.ForeColor = Black
                        hLink2.Text = e.Row.Cells(3).Text
                        If objGlobals.PlayerSelected = e.Row.Cells(3).Text Then
                            hLink2.BackColor = Red
                        End If
                        e.Row.Cells(3).Controls.Add(hLink2)
                    Else
                        Select Case gRow
                            Case 1
                                e.Row.Cells(0).Font.Bold = True
                                e.Row.Cells(0).Font.Size = 14
                                e.Row.Cells(0).ColumnSpan = 4
                                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
                                e.Row.Cells(1).Visible = False
                                e.Row.Cells(2).Visible = False
                                e.Row.Cells(3).Visible = False
                            Case 2, 3
                                e.Row.Cells(0).BorderStyle = BorderStyle.None
                                e.Row.Cells(1).Font.Bold = True
                                e.Row.Cells(1).ColumnSpan = 2
                                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Left
                                e.Row.Cells(2).Visible = False

                                e.Row.Cells(3).Font.Bold = True
                                e.Row.Cells(3).ColumnSpan = 2
                                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Left
                                e.Row.Cells(4).Visible = False
                            Case 4
                                e.Row.Cells(0).ColumnSpan = 5
                                e.Row.Cells(1).Visible = False
                                e.Row.Cells(2).Visible = False
                                e.Row.Cells(3).Visible = False
                                e.Row.Cells(4).Visible = False
                            Case 5
                                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
                        End Select
                    End If
            End Select
            'e.Row.CssClass = "row"
        Else
            e.Row.Visible = False
        End If
        gRow = gRow + 1

    End Sub

    Protected Sub Calendar1_SelectionChanged(sender As Object, e As System.EventArgs) Handles Calendar1.SelectionChanged
        'check to see if date & venue are available
        Dim tempSeason As String
        tempSeason = objGlobals.get_current_season
        Dim l_new_date As String = Calendar1.SelectedDate.ToString("yyyy-MM-dd")
        Dim l_param_in_names(5) As String
        Dim l_param_in_values(5) As String
        l_param_in_names(0) = "@inSeason"
        l_param_in_values(0) = tempSeason
        l_param_in_names(1) = "@inFixtureID"
        l_param_in_values(1) = CompID
        l_param_in_names(2) = "@inSport"
        l_param_in_values(2) = Left(FixtureLeague, 4)
        l_param_in_names(3) = "@inVenue"
        l_param_in_values(3) = lblVenue.Text
        l_param_in_names(4) = "@inFixtureCalendar"
        l_param_in_values(4) = l_new_date
        l_param_in_names(5) = "@inStatus"
        l_param_in_values(5) = 0

        strSQL = "EXEC [clubs.tools_16].[sp_check_fixture_exists] '" & l_param_in_values(0) & "'," & l_param_in_values(1) & ",'" & l_param_in_values(2) & "','" & l_param_in_values(3) & "','" & l_param_in_values(4) & "','" & l_param_in_values(5) & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            If myDataReader.Item("Error") = "OK" Then
                lblError.Text = "Date OK. Click 'Update Date & Status'"
                lblError.BackColor = Gold
                lblError.ForeColor = Black
                btnUpdateDateStatus.Visible = True
            Else
                lblError.Text = myDataReader.Item("Error") & vbCrLf & ". CAN'T UPDATE"
                lblError.BackColor = Red
                lblError.ForeColor = White
                btnUpdateDateStatus.Visible = False
            End If
        End While

    End Sub

    Protected Sub btnUpdateDateStatus_Click(sender As Object, e As System.EventArgs) Handles btnUpdateDateStatus.Click
        Dim tempSeason As String
        tempSeason = objGlobals.get_current_season
        Dim l_new_date As String = Calendar1.SelectedDate.ToString("yyyy-MM-dd")
        Dim l_param_in_names(5) As String
        Dim l_param_in_values(5) As String
        l_param_in_names(0) = "@inSeason"
        l_param_in_values(0) = tempSeason
        l_param_in_names(1) = "@inFixtureID"
        l_param_in_values(1) = CompID
        l_param_in_names(2) = "@inSport"
        l_param_in_values(2) = "" 'not required
        l_param_in_names(3) = "@inVenue"
        l_param_in_values(3) = "" 'not required
        l_param_in_names(4) = "@inFixtureCalendar"
        l_param_in_values(4) = l_new_date
        l_param_in_names(5) = "@inStatus"
        l_param_in_values(5) = Val(txtNewStatus.Text)

        strSQL = "EXEC [clubs.tools_16].[sp_update_fixture_date_status] '" & l_param_in_values(0) & "'," & l_param_in_values(1) & ",'" & l_param_in_values(2) & "','" & l_param_in_values(3) & "','" & l_param_in_values(4) & "','" & l_param_in_values(5) & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        write_fixtures_FTP()

        If objGlobals.TeamSelected Is Nothing Then
            Response.Redirect("~/clubs/Default.aspx?Week=" & FixtureWeek)
        Else
            Response.Redirect("~/clubs/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected)
        End If
    End Sub


End Class
