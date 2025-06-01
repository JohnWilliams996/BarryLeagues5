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
    Private Rolls(20) As Double
    Private Pins(20) As Integer
    Private Points_Rolls_Pins(20) As Double
    Private HomePoints As Double
    Private AwayPoints As Double
    Private FixtureDate As String
    Private FixtureFullDate As String
    Private FixtureWeek As Integer
    Private FixtureLeague As String
    Private FixtureDetail As Boolean
    Private FixtureHomeTeam As String
    Private FixtureAwayTeam As String
    Private Result As String = ""
    Private RollsResult As String = ""
    Private home_result As String = ""
    Private home_rolls_result As String = ""
    Private home_roll_1 As Integer
    Private home_roll_2 As Integer
    Private home_roll_3 As Integer
    Private home_roll_4 As Integer
    Private home_roll_5 As Integer
    Private home_roll_total As Integer
    Private home_rolls_won As Double
    Private away_rolls_won As Double
    Private away_roll_1 As Integer
    Private away_roll_2 As Integer
    Private away_roll_3 As Integer
    Private away_roll_4 As Integer
    Private away_roll_5 As Integer
    Private away_roll_total As Integer
    Private home_rolls_total As Integer
    Private away_rolls_total As Integer
    Private gRow As Integer = 0
    Private TotalHome As Integer = 0
    Private TotalAway As Integer = 0
    Private strSQL As String
    Private fixture_status As Integer
    Private myDataReader As OleDbDataReader


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("mens_skit")
        objGlobals.CurrentUser = "mens_skit_user"
        objGlobals.CurrentSchema = "mens_skit."
        objGlobals.TeamSelected = Request.QueryString("Team")
        objGlobals.LeagueSelected = Request.QueryString("League")
        objGlobals.PlayerSelected = Request.QueryString("Player")
        FixtureWeek = Request.QueryString("Week")

        btnUpdateHeader.Visible = True
        btnAddEditResult.Visible = False

        CompID = Request.QueryString("ID")
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
            Call objGlobals.store_page(Request.Url.OriginalString, True)
            If objGlobals.TeamSelected Is Nothing Then
                btnAddEditResult.PostBackUrl = "~/Mens_Skit/Admin/Add Edit Result 2.aspx?ID=" & CompID & "&Week=" & FixtureWeek
            Else
                btnAddEditResult.PostBackUrl = "~/Mens_Skit/Admin/Add Edit Result 2.aspx?ID=" & CompID & "&Week=" & FixtureWeek & "&Team=" & objGlobals.TeamSelected
            End If
        Else
            lblVenue.Text = "NOT AUTHORIZED"
            btnBack.Visible = False
            btnUpdateHeader.Visible = False
            lblResult.Visible = False
            lblRolls.Visible = False

            btn20.Visible = False
            btn21.Visible = False
            btn22.Visible = False
            btn23.Visible = False
            btn24.Visible = False
            btn25.Visible = False
            btn26.Visible = False
            btn27.Visible = False
            btn28.Visible = False
            btn29.Visible = False

            btn30.Visible = False
            btn31.Visible = False
            btn32.Visible = False
            btn33.Visible = False
            btn34.Visible = False
            btn35.Visible = False
            btn36.Visible = False
            btn37.Visible = False
            btn38.Visible = False
            btn39.Visible = False

            btn40.Visible = False
            btn41.Visible = False
            btn42.Visible = False
            btn43.Visible = False
            btn44.Visible = False
            btn45.Visible = False
            btn46.Visible = False
            btn47.Visible = False
            btn48.Visible = False
            btn49.Visible = False

            btn50.Visible = False
            btn51.Visible = False
            btn52.Visible = False
            btn53.Visible = False
            btn54.Visible = False
            btn55.Visible = False
            btn56.Visible = False
            btn57.Visible = False
            btn58.Visible = False
            btn59.Visible = False

            btn60.Visible = False
            btn61.Visible = False
            btn62.Visible = False
            btn63.Visible = False
            btn64.Visible = False
            btn65.Visible = False
            btn66.Visible = False
            btn67.Visible = False
            btn68.Visible = False
            btn69.Visible = False

            btn70.Visible = False
            btn71.Visible = False
            btn72.Visible = False
            btn73.Visible = False
            btn74.Visible = False
            btn75.Visible = False
            btn76.Visible = False
            btn77.Visible = False
            btn78.Visible = False
            btn79.Visible = False

            lblRollNo.Visible = False
            lblRoll1.Visible = False
            lblRoll2.Visible = False
            lblRoll3.Visible = False
            lblRoll4.Visible = False
            lblRoll5.Visible = False
            lblPoints.Visible = False
            lblTotal.Visible = False

            lblHome.Visible = False
            txtHomeRoll1.Visible = False
            txtHomeRoll2.Visible = False
            txtHomeRoll3.Visible = False
            txtHomeRoll4.Visible = False
            txtHomeRoll5.Visible = False
            txtHomeRollsPoints.Visible = False
            txtHomeTotal.Visible = False

            lblAway.Visible = False
            txtAwayRoll1.Visible = False
            txtAwayRoll2.Visible = False
            txtAwayRoll3.Visible = False
            txtAwayRoll4.Visible = False
            txtAwayRoll5.Visible = False
            txtAwayRollsPoints.Visible = False
            txtAwayTotal.Visible = False

            btnCancel.Visible = False
            btnOK.Visible = False
            lblTableBefore.Visible = False
            lblTableAfter.Visible = False
            lblNewDate.Visible = False
            lblVenueLit.Visible = False
            Calendar1.Visible = False
            lblNewStatus.Visible = False
            txtNewStatus.Visible = False
            lblError.Visible = False
            btnUpdateDateStatus.Visible = False

            objGlobals.AdminLogin = False
            Call objGlobals.store_page("ATTACK FROM " & Request.Url.OriginalString, objGlobals.AdminLogin)
            '16.9.14 - only allow ADMIN in this screen
            Exit Sub
        End If
        btnBack.Visible = True
        btnBack.Attributes.Add("onClick", "javascript:history.back(); return false;")

        If objGlobals.PlayerSelected <> Nothing Then
            btnBack.Visible = False
            btnOK.Attributes.Add("onClick", "javascript:history.back(); return false;")
        End If
        lblTableBefore.Visible = True
        lblTableAfter.Visible = True

        strSQL = "SELECT *,CONVERT(VARCHAR(10),fixture_calendar,112) AS Fixture_YMD FROM mens_skit.vw_fixtures WHERE fixture_id=" & CompID
        myDataReader = objGlobals.SQLSelect(strSQL)
        If Not IsPostBack Then
            While myDataReader.Read()
                HomePoints = myDataReader.Item("home_points")
                AwayPoints = myDataReader.Item("away_points")
                home_result = Replace(myDataReader.Item("home_result"), " ", "")
                home_rolls_result = Replace(myDataReader.Item("home_rolls_result"), " ", "")
                home_rolls_won = myDataReader.Item("home_rolls_won")
                away_rolls_won = myDataReader.Item("away_rolls_won")
                home_rolls_total = myDataReader.Item("home_rolls_total")
                away_rolls_total = myDataReader.Item("away_rolls_total")
                FixtureLeague = myDataReader.Item("league")
                FixtureDate = myDataReader.Item("fixture_ymd")
                FixtureHomeTeam = myDataReader.Item("home_team_name")
                FixtureAwayTeam = myDataReader.Item("away_team_name")
                FixtureFullDate = "Date : " & myDataReader.Item("fixture_date")
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

                '8/12/22 Allow change of fixture date and status
                Calendar1.SelectedDate = myDataReader.Item("fixture_calendar")
                lblVenue.Text = myDataReader.Item("venue")
                txtNewStatus.Text = myDataReader.Item("status")
            End While

            fixture_status = FixtureStatus()

            If home_roll_1 = 0 Then txtHomeRoll1.BackColor = LightBlue
            rbResults.Enabled = True
            btnOK.Visible = False
            btnCancel.Visible = True

            With rbResults
                .ClearSelection()
                .Items.Add("0-0")
                .Items.Add("2-0") : If home_result = "2-0" Then .SelectedIndex = 1
                .Items.Add("1-1") : If home_result = "1-1" Then .SelectedIndex = 2
                .Items.Add("0-2") : If home_result = "0-2" Then .SelectedIndex = 3
                .Items.Add("Postponed")
            End With
            txtHomeRoll1.Text = home_roll_1
            txtHomeRoll2.Text = home_roll_2
            txtHomeRoll3.Text = home_roll_3
            txtHomeRoll4.Text = home_roll_4
            txtHomeRoll5.Text = home_roll_5
            txtAwayRoll1.Text = away_roll_1
            txtAwayRoll2.Text = away_roll_2
            txtAwayRoll3.Text = away_roll_3
            txtAwayRoll4.Text = away_roll_4
            txtAwayRoll5.Text = away_roll_5
            txtHomeRollsPoints.Text = home_rolls_won
            txtAwayRollsPoints.Text = away_rolls_won
            txtHomeTotal.Text = home_rolls_total
            txtAwayTotal.Text = away_rolls_total
        Else
            While myDataReader.Read()
                FixtureLeague = myDataReader.Item("league")
                FixtureDate = myDataReader.Item("fixture_ymd")
                FixtureHomeTeam = myDataReader.Item("home_team_name")
                FixtureAwayTeam = myDataReader.Item("away_team_name")
                FixtureFullDate = "Date : " & myDataReader.Item("fixture_date")
            End While
        End If

        If objGlobals.AdminLogin Then
            Call load_table(gridTable, "Tables")
            lblTableAfter.Visible = False
        Else
            Call create_tables_before()
            Call load_table(gridTable, "before_tables")
            Call create_tables_after(FixtureDate)
            Call load_table(gridTable2, "after_tables")
        End If
        Call load_header()

        If fixture_status >= 1 Then
            Call load_result()
            rbResults.Visible = False
            Call load_totals()
            If fixture_status >= 1 Then
                Call load_rolls()
            End If
            If fixture_status = 2 Then
                Call colour_rolls()
                Call colour_thirties()
                Call colour_high_scores()
                Call colour_totals()
            End If
        Else
            gRow = 0
            gridResult.Visible = True
            gridResult.DataSource = dt
            gridResult.DataBind()
        End If

        If objGlobals.AdminLogin Then
            rbResults.Visible = True
        End If
    End Sub

    Protected Sub create_tables_before()
        Dim myDataReader2 As oledbdatareader
        Dim ThisTeam As String = ""

        Dim BeforePlayed As Integer = 0
        Dim BeforeWon As Integer = 0
        Dim BeforeDrawn As Integer = 0
        Dim BeforeLost As Integer = 0
        Dim BeforePoints As Double = 0
        Dim BeforeRolls As Double = 0
        Dim BeforePins As Integer = 0
        Dim BeforePoints_Rolls_Pins As Double = 0
        Dim tempSeason As String
        tempSeason = objGlobals.get_current_season

        strSQL = "TRUNCATE TABLE mens_skit.before_tables"
        myDataReader = objGlobals.SQLSelect(strSQL)

        strSQL = "SELECT DISTINCT(team) FROM mens_skit.vw_tables WHERE league = '" & FixtureLeague & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ThisTeam = myDataReader.Item(0)
            strSQL = "INSERT INTO mens_skit.before_tables VALUES ('" & tempSeason & "','" & FixtureLeague & "',1,'" & ThisTeam & "',0,0,0,0,0,0)"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While

        strSQL = "SELECT t.long_name as Team,"
        strSQL = strSQL + " COUNT(a.home_team_name) - COUNT(a1.home_team_name) AS Pld, "
        strSQL = strSQL + " COUNT(b.home_points) AS W, "
        strSQL = strSQL + " COUNT(c.home_points) AS D,"
        strSQL = strSQL + " COUNT(d.home_points) AS L,"
        strSQL = strSQL + " SUM(ISNULL(e.home_points,0)) AS Pts,"
        strSQL = strSQL + " SUM(ISNULL(e.home_rolls,0)) AS Rolls,"
        strSQL = strSQL + " SUM(ISNULL(e.home_pins,0)) AS Pins"
        strSQL = strSQL + " FROM mens_skit.vw_teams t"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures AS a ON a.League = t.League AND a.home_team_name = t.long_name AND a.home_result <> '0 - 0'"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures b ON b.fixture_id = a.fixture_id AND b.home_points > b.away_points"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures c ON c.fixture_id = a.fixture_id AND c.home_points = c.away_points AND c.home_result <> '0 - 0'"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures d ON d.fixture_id = a.fixture_id AND d.home_points < d.away_points"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures e ON e.fixture_id = a.fixture_id"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures f ON f.fixture_id = a.fixture_id "
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
            BeforeRolls = myDataReader.Item("rolls")
            BeforePins = myDataReader.Item("pins")
            BeforePoints_Rolls_Pins = (BeforePoints * 1000000) + (BeforeRolls * 100000) + BeforePins
            strSQL = "UPDATE mens_skit.before_tables SET Pld=" & BeforePlayed & ",W=" & BeforeWon & ",D=" & BeforeDrawn & ",L=" & BeforeLost & ",pts=" & BeforePoints & ",rolls=" & BeforeRolls & ",pins=" & BeforePins & ",Pts_Rolls_Pins = " & BeforePoints_Rolls_Pins
            strSQL = strSQL & " WHERE team = '" & ThisTeam & "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While

        strSQL = "SELECT t.long_name as Team,"
        strSQL = strSQL + " COUNT(a.away_team_name) - COUNT(a1.away_team_name) AS Pld, "
        strSQL = strSQL + " COUNT(b.away_points) AS W, "
        strSQL = strSQL + " COUNT(c.away_points) AS D,"
        strSQL = strSQL + " COUNT(d.away_points) AS L,"
        strSQL = strSQL + " SUM(ISNULL(e.away_points,0)) AS Pts,"
        strSQL = strSQL + " SUM(ISNULL(e.away_rolls,0)) AS Rolls,"
        strSQL = strSQL + " SUM(ISNULL(e.away_pins,0)) AS Pins"
        strSQL = strSQL + " FROM mens_skit.vw_teams t"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures AS a ON a.League = t.League AND a.away_team_name = t.long_name AND a.away_result <> '0 - 0'"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures b ON b.fixture_id = a.fixture_id AND b.away_points > b.home_points"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures c ON c.fixture_id = a.fixture_id AND c.away_points = c.home_points AND c.away_result <> '0 - 0'"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures d ON d.fixture_id = a.fixture_id AND d.away_points < d.home_points"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures e ON e.fixture_id = a.fixture_id"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures f ON f.fixture_id = a.fixture_id "
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
            BeforeRolls = myDataReader.Item("rolls")
            BeforePins = myDataReader.Item("pins")
            BeforePoints_Rolls_Pins = (BeforePoints * 1000000) + (BeforeRolls * 100000) + BeforePins
            strSQL = "UPDATE mens_skit.before_tables SET Pld=Pld+" & BeforePlayed & ",W=W+" & BeforeWon & ",D=D+" & BeforeDrawn & ",L=L+" & BeforeLost & ",pts=pts+" & BeforePoints & ",rolls=rolls+" & BeforeRolls & ",pins=pins+" & BeforePins & ",Pts_Rolls_Pins=Pts_Rolls_Pins+" & BeforePoints_Rolls_Pins
            strSQL = strSQL & " WHERE team = '" & ThisTeam & "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        strSQL = "SELECT * FROM mens_skit.before_tables ORDER BY Pts_Rolls_Pins DESC,pld,W"
        myDataReader = objGlobals.SQLSelect(strSQL)

        Dim LastPoints As Double = 0
        Dim Pos1 As Integer = 0
        Dim Pos2 As Integer = 0
        While myDataReader.Read()
            If LastPoints = 0 And Pos1 = 0 Then
                Pos1 = 1
                LastPoints = myDataReader.Item("Pts_Rolls_Pins")
            End If
            Pos2 = Pos2 + 1
            If LastPoints <> myDataReader.Item("Pts_Rolls_Pins") Then
                Pos1 = Pos2
                LastPoints = myDataReader.Item("Pts_Rolls_Pins")
            End If
            strSQL = "UPDATE mens_skit.before_tables SET Pos = " + CStr(Pos1)
            strSQL = strSQL + " WHERE team = '" + myDataReader.Item("team") + "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
    End Sub

    Protected Sub create_tables_after(ByVal inDate As String)
        Dim myDataReader2 As OleDbDataReader
        Dim ThisTeam As String = ""

        Dim AfterPlayed As Integer = 0
        Dim AfterWon As Integer = 0
        Dim AfterDrawn As Integer = 0
        Dim AfterLost As Integer = 0
        Dim AfterPoints As Double = 0
        Dim AfterRolls As Integer = 0
        Dim AfterPins As Integer = 0
        Dim AfterPoints_Rolls_Pins As Integer = 0
        Dim tempSeason As String
        tempSeason = objGlobals.get_current_season

        strSQL = "TRUNCATE TABLE mens_skit.after_tables"
        myDataReader = objGlobals.SQLSelect(strSQL)

        strSQL = "SELECT DISTINCT(team) FROM mens_skit.vw_tables WHERE league = '" & FixtureLeague & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ThisTeam = myDataReader.Item(0)
            strSQL = "INSERT INTO mens_skit.after_tables VALUES ('" & tempSeason & "','" & FixtureLeague & "',1,'" & ThisTeam & "',0,0,0,0,0,0,0,0)"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While

        strSQL = "SELECT t.long_name as Team,"
        strSQL = strSQL + " COUNT(a.home_team_name) - COUNT(a1.home_team_name) AS Pld, "
        strSQL = strSQL + " COUNT(b.home_points) AS W, "
        strSQL = strSQL + " COUNT(c.home_points) AS D,"
        strSQL = strSQL + " COUNT(d.home_points) AS L,"
        strSQL = strSQL + " SUM(ISNULL(e.home_points,0)) AS Pts,"
        strSQL = strSQL + " SUM(ISNULL(e.home_rolls_won,0)) AS Rolls,"
        strSQL = strSQL + " SUM(ISNULL(e.home_rolls_total,0)) AS Pins"
        strSQL = strSQL + " FROM mens_skit.vw_teams t"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures a ON a.League = t.League AND a.home_team_name = t.long_name AND a.home_result <> '0 - 0'"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures a1 ON a1.fixture_id = a.fixture_id AND a1.home_result = '0 - 0' "
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures b ON b.fixture_id = a.fixture_id AND b.home_points > b.away_points"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures c ON c.fixture_id = a.fixture_id AND c.home_points = c.away_points AND c.home_result <> '0 - 0'"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures d ON d.fixture_id = a.fixture_id AND d.home_points < d.away_points"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures e ON e.fixture_id = a.fixture_id"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures f ON f.fixture_id = a.fixture_id "
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
            AfterRolls = myDataReader.Item("rolls")
            AfterPins = myDataReader.Item("pins")
            AfterPoints_Rolls_Pins = (AfterPoints * 1000000) + (AfterRolls * 100000) + AfterPins
            strSQL = "UPDATE mens_skit.after_tables SET Pld=" & AfterPlayed & ",W=" & AfterWon & ",D=" & AfterDrawn & ",L=" & AfterLost & ",pts=" & AfterPoints & ",rolls=" & AfterRolls & ",pins=" & AfterPins & ",Pts_Rolls_Pins=" & AfterPoints_Rolls_Pins
            strSQL = strSQL & " WHERE team = '" & ThisTeam & "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While

        strSQL = "SELECT t.long_name as Team,"
        strSQL = strSQL + " COUNT(a.away_team_name) - COUNT(a1.away_team_name) AS Pld, "
        strSQL = strSQL + " COUNT(b.away_points) AS W, "
        strSQL = strSQL + " COUNT(c.away_points) AS D,"
        strSQL = strSQL + " COUNT(d.away_points) AS L,"
        strSQL = strSQL + " SUM(ISNULL(e.away_points,0)) AS Pts,"
        strSQL = strSQL + " SUM(ISNULL(e.away_rolls_won,0)) AS Rolls,"
        strSQL = strSQL + " SUM(ISNULL(e.away_rolls_total,0)) AS Pins"
        strSQL = strSQL + " FROM mens_skit.vw_teams t"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures a ON a.League = t.League AND a.away_team_name = t.long_name AND a.away_result <> '0 - 0'"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures a1 ON a1.fixture_id = a.fixture_id AND a1.away_result = '0 - 0' "
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures b ON b.fixture_id = a.fixture_id AND b.away_points > b.home_points"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures c ON c.fixture_id = a.fixture_id AND c.away_points = c.home_points AND c.away_result <> '0 - 0'"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures d ON d.fixture_id = a.fixture_id AND d.away_points < d.home_points"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures e ON e.fixture_id = a.fixture_id"
        strSQL = strSQL + " LEFT OUTER JOIN mens_skit.vw_fixtures f ON f.fixture_id = a.fixture_id "
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
            AfterRolls = myDataReader.Item("rolls")
            AfterPins = myDataReader.Item("pins")
            AfterPoints_Rolls_Pins = (AfterPoints * 1000000) + (AfterRolls * 100000) + AfterPins
            strSQL = "UPDATE mens_skit.after_tables SET Pld=Pld+" & AfterPlayed & ",W=W+" & AfterWon & ",D=D+" & AfterDrawn & ",L=L+" & AfterLost & ",pts=pts+" & AfterPoints & ",Rolls=Rolls+" & AfterRolls & ",Pins=Pins+" & AfterPins & ",Pts_Rolls_Pins=Pts_Rolls_Pins+" & AfterPoints_Rolls_Pins
            strSQL = strSQL & " WHERE team = '" & ThisTeam & "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        strSQL = "SELECT * FROM mens_skit.after_tables ORDER BY Pts_Rolls_Pins DESC,pld,W"
        myDataReader = objGlobals.SQLSelect(strSQL)

        Dim LastPoints As Double = 0
        Dim Pos1 As Integer = 0
        Dim Pos2 As Integer = 0
        While myDataReader.Read()
            If LastPoints = 0 And Pos1 = 0 Then
                Pos1 = 1
                LastPoints = myDataReader.Item("Pts_Rolls_Pins")
            End If
            Pos2 = Pos2 + 1
            If LastPoints <> myDataReader.Item("Pts_Rolls_Pins") Then
                Pos1 = Pos2
                LastPoints = myDataReader.Item("Pts_Rolls_Pins")
            End If
            strSQL = "UPDATE mens_skit.after_tables SET Pos = " + CStr(Pos1)
            strSQL = strSQL + " WHERE team = '" + myDataReader.Item("team") + "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
    End Sub

    Protected Sub update_result(ByVal inResult As String)
        Dim HomeScore As String = ""
        Dim AwayScore As String = ""
        Dim HomeRolls As String = ""
        Dim AwayRolls As String = ""
        Dim AwayString As String = ""
        Dim HomeRollsScore As String = ""
        Dim AwayRollsScore As String = ""
        Dim AwayRollsString As String = ""
        Dim DashPos As Integer
        Dim home_result1 As String = ""
        Dim home_result2 As String = ""
        Dim away_result1 As String = ""
        Dim away_result2 As String = ""
        Dim home_rolls_result1 As String = ""
        Dim home_rolls_result2 As String = ""
        Dim away_rolls_result1 As String = ""
        Dim away_rolls_result2 As String = ""
        Dim i As Integer
        Dim NewStatus As Integer

        If inResult = "Postponed" Then
            home_result1 = "0 - 0"
            HomeScore = "0"
            HomePoints = 0
            away_result1 = "0 - 0"
            AwayScore = "0"
            AwayPoints = 0
            NewStatus = -1
        Else
            DashPos = InStr(inResult, "-")
            home_result1 = Replace(inResult, "-", " - ") + "*"
            home_result1 = Replace(home_result1, "0½", "½")
            home_result2 = Replace(home_result1, "½", ".5")
            DashPos = InStr(home_result1, "-")
            HomeScore = Left(home_result2, DashPos - 1)
            HomePoints = HomeScore

            For i = DashPos + 2 To Len(home_result1)
                If Mid(home_result1, i, 1) <> "*" Then
                    AwayString = AwayString + Mid(home_result1, i, 1)
                Else
                    Exit For
                End If
            Next i

            away_result1 = AwayString & " - " & Left(home_result1, DashPos - 2)
            away_result2 = Replace(AwayString, "0½", "½")
            AwayScore = Replace(away_result2, "½", ".5")
            AwayPoints = AwayScore

            home_result1 = Left(home_result1, Len(home_result1) - 1)        ' remove *
            home_result = home_result1

            HomeRolls = txtHomeRollsPoints.Text
            AwayRolls = txtAwayRollsPoints.Text
            home_rolls_result2 = HomeRolls + " - " + AwayRolls
            home_rolls_result1 = Replace(home_rolls_result2, ".5", "½")
            home_rolls_result1 = Replace(home_rolls_result1, "0½", "½")

            away_rolls_result2 = AwayRolls + " - " + HomeRolls
            away_rolls_result1 = Replace(away_rolls_result2, ".5", "½")
            away_rolls_result1 = Replace(away_rolls_result1, "0½", "½")
        End If

        If Val(HomeScore) + Val(AwayScore) = 0 Then
            strSQL = "UPDATE mens_skit.vw_fixtures SET"
            strSQL = strSQL & " home_points=0"
            strSQL = strSQL & ",away_points=0"
            strSQL = strSQL & ",home_result='0 - 0'"
            strSQL = strSQL & ",away_result='0 - 0'"
            strSQL = strSQL & ",home_rolls_won=0"
            strSQL = strSQL & ",away_rolls_won=0"
            strSQL = strSQL & ",home_rolls_total=0"
            strSQL = strSQL & ",away_rolls_total=0"
            strSQL = strSQL & ",home_rolls_result='0 - 0'"
            strSQL = strSQL & ",away_rolls_result='0 - 0'"
            strSQL = strSQL & ",home_roll_1=0"
            strSQL = strSQL & ",home_roll_2=0"
            strSQL = strSQL & ",home_roll_3=0"
            strSQL = strSQL & ",home_roll_4=0"
            strSQL = strSQL & ",home_roll_5=0"
            strSQL = strSQL & ",away_roll_1=0"
            strSQL = strSQL & ",away_roll_2=0"
            strSQL = strSQL & ",away_roll_3=0"
            strSQL = strSQL & ",away_roll_4=0"
            strSQL = strSQL & ",away_roll_5=0"
            strSQL = strSQL & " WHERE fixture_id=" & CompID
        Else
            strSQL = "UPDATE mens_skit.vw_fixtures SET"
            strSQL = strSQL & " home_points = " & Convert.ToDouble(HomeScore)
            strSQL = strSQL & ",away_points=" & Convert.ToDouble(AwayScore)
            strSQL = strSQL & ",home_result='" & home_result1 & "'"
            strSQL = strSQL & ",away_result='" & away_result1 & "'"
            strSQL = strSQL & ",home_rolls_result='" & home_rolls_result1 & "'"
            strSQL = strSQL & ",home_rolls_won=" & Convert.ToDouble(HomeRolls)
            strSQL = strSQL & ",away_rolls_won=" & Convert.ToDouble(AwayRolls)
            strSQL = strSQL & ",home_rolls_total=" & txtHomeTotal.Text
            strSQL = strSQL & ",away_rolls_total=" & txtAwayTotal.Text
            strSQL = strSQL & ",away_rolls_result='" & away_rolls_result1 & "'"
            strSQL = strSQL & ",home_roll_1=" & txtHomeRoll1.Text
            strSQL = strSQL & ",home_roll_2=" & txtHomeRoll2.Text
            strSQL = strSQL & ",home_roll_3=" & txtHomeRoll3.Text
            strSQL = strSQL & ",home_roll_4=" & txtHomeRoll4.Text
            strSQL = strSQL & ",home_roll_5=" & txtHomeRoll5.Text
            strSQL = strSQL & ",away_roll_1=" & txtAwayRoll1.Text
            strSQL = strSQL & ",away_roll_2=" & txtAwayRoll2.Text
            strSQL = strSQL & ",away_roll_3=" & txtAwayRoll3.Text
            strSQL = strSQL & ",away_roll_4=" & txtAwayRoll4.Text
            strSQL = strSQL & ",away_roll_5=" & txtAwayRoll5.Text
            strSQL = strSQL & " WHERE fixture_id=" & CompID
        End If

        myDataReader = objGlobals.SQLSelect(strSQL)

        strSQL = "SELECT"
        strSQL = strSQL & " t.long_name,"
        strSQL = strSQL & " SUM(h.Pld+a.Pld) AS Pld,"
        strSQL = strSQL & " SUM(h.W+a.W) AS W,"
        strSQL = strSQL & " SUM(h.D+a.D) AS D,"
        strSQL = strSQL & " SUM(h.L+a.L) AS L,"
        strSQL = strSQL & " SUM(h.Pts+a.Pts) AS Pts,"
        strSQL = strSQL & " SUM(h.Rolls+a.Rolls) AS Rolls,"
        strSQL = strSQL & " SUM(h.Pins+a.Pins) AS Pins,"
        strSQL = strSQL & " SUM(h.Pts_Rolls_Pins+a.Pts_Rolls_Pins) AS Pts_Rolls_Pins"
        strSQL = strSQL & " FROM mens_skit.vw_teams t"
        strSQL = strSQL & " JOIN mens_skit.vw_home_tables h ON (h.League = t.League AND h.Team = t.long_name)"
        strSQL = strSQL & " JOIN mens_skit.vw_away_tables a ON (a.League = t.League AND a.Team = t.long_name)"
        strSQL = strSQL & " WHERE t.League = '" & FixtureLeague & "'"
        strSQL = strSQL & " AND t.long_name <> 'BYE'"
        strSQL = strSQL & " GROUP BY t.long_name"
        strSQL = strSQL & " ORDER BY Pts_Rolls_Pins DESC"
        i = 0
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            Pos(i) = 0
            Team(i) = myDataReader.Item(0)
            Played(i) = myDataReader.Item("pld")
            Won(i) = myDataReader.Item("w")
            Drawn(i) = myDataReader.Item("d")
            Lost(i) = myDataReader.Item("l")
            Points(i) = myDataReader.Item("Pts")
            Rolls(i) = myDataReader.Item("Rolls")
            Pins(i) = myDataReader.Item("Pins")
            Points_Rolls_Pins(i) = myDataReader.Item("Pts_Rolls_Pins")
            i = i + 1
        End While

        'end 20/9/13


        Call sort_table()
        Call re_update_table("Tables")
        Call load_table(gridTable2, "Tables")

        '17/9/13 update the tables by week in Tables_Week
        If FixtureWeek <> objGlobals.GetCurrentWeek Then 'And objGlobals.LiveTestFlag = 3 Then
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
            strSQL = "UPDATE mens_skit.vw_fixtures SET Status = " & NewStatus & " WHERE fixture_id = " & CompID
            myDataReader = objGlobals.SQLSelect(strSQL)
            strSQL = "DELETE FROM mens_skit.vw_fixtures_detail WHERE fixture_id = " & CompID
            myDataReader = objGlobals.SQLSelect(strSQL)
            Call update_player_stats("sp_update_player_stats")

            're-update league AND team positions
            strSQL = "EXEC mens_skit.sp_update_league_position '" & FixtureLeague & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)

            strSQL = "EXEC mens_skit.sp_update_team_position '" & FixtureLeague & "','" & FixtureHomeTeam & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)

            strSQL = "EXEC mens_skit.sp_update_team_position '" & FixtureLeague & "','" & FixtureAwayTeam & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
        Else
            fixture_status = FixtureStatus()
            If fixture_status = 0 Then fixture_status = 1
            strSQL = "UPDATE mens_skit.vw_fixtures SET Status = " & fixture_status & " WHERE fixture_id = " & CompID
            myDataReader = objGlobals.SQLSelect(strSQL)
        End If
        '28.7.14 - end

        '19.11.18 - update fixture_combined table
        Call objGlobals.update_fixtures_combined("mens_skit")

        strSQL = "UPDATE mens_skit.last_changed SET date_time_changed = '" & UKDate & UKTime & "'"

        myDataReader = objGlobals.SQLSelect(strSQL)

        btnCancel.Visible = False
        btnOK.Visible = True

    End Sub

    Sub update_player_stats(inStoredProcedure As String)
        Dim myDataReader2 As OleDbDataReader
        Dim tempSeason As String
        tempSeason = objGlobals.get_current_season
        inStoredProcedure = "mens_skit." & inStoredProcedure
        'update the home team stats
        strSQL = "SELECT player FROM mens_skit.vw_players WHERE league = '" & FixtureLeague & "' AND team = '" & FixtureHomeTeam & "' AND player NOT LIKE 'A N OTHER%' "
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            strSQL = "EXEC " & inStoredProcedure & " '" & tempSeason & "','" & FixtureLeague & "','" & FixtureHomeTeam & "','" & myDataReader.Item("player") & "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        'update the away team stats
        strSQL = "SELECT player FROM mens_skit.vw_players WHERE league = '" & FixtureLeague & "' AND team = '" & FixtureAwayTeam & "' AND player NOT LIKE 'A N OTHER%' "
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            strSQL = "EXEC " & inStoredProcedure & " '" & tempSeason & "','" & FixtureLeague & "','" & FixtureAwayTeam & "','" & myDataReader.Item("player") & "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
    End Sub

    Protected Function FixtureStatus() As Integer
        FixtureStatus = 1
        strSQL = "SELECT COUNT(*) FROM mens_skit.vw_fixtures_detail WHERE fixture_id = " & CompID
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            If myDataReader.Item(0) = 0 Then FixtureStatus = 0
            If myDataReader.Item(0) > 0 Then FixtureStatus = 2
        End While
    End Function

    Protected Function get_end_week_date(ByVal inWeek As Integer) As String
        get_end_week_date = ""
        strSQL = "SELECT week_commences FROM mens_skit.vw_weeks WHERE week_number = " & inWeek
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            get_end_week_date = Format(DateAdd(DateInterval.Day, 6, myDataReader.Item(0)), "yyyyMMdd")
        End While
    End Function

    Protected Sub update_weekly_tables(ByVal inTable As String, ByVal inWeek As Integer)
        strSQL = "DELETE FROM mens_skit.vw_tables_week WHERE league = '" & FixtureLeague & "' AND week = " & inWeek
        myDataReader = objGlobals.SQLSelect(strSQL)
        strSQL = "INSERT INTO mens_skit.tables_week SELECT season,league," & inWeek & ",pos,team,pts,rolls,pins,Pts_Rolls_Pins FROM mens_skit." & inTable & " WHERE league = '" & FixtureLeague & "' AND season = '" & objGlobals.get_current_season & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
    End Sub

    Protected Sub sort_table()
        'sort array (by descending points)
        Dim tempTeam As String
        Dim tempPlayed As Integer
        Dim tempWon As Integer
        Dim tempDrawn As Integer
        Dim tempLost As Integer
        Dim tempPoints As Double
        Dim tempRolls As Double
        Dim tempPins As Integer
        Dim tempPoints_Rolls_Pins As Double
        Dim i As Integer
        For i = 0 To gridTable.Rows.Count - 1
            For j = i + 1 To gridTable.Rows.Count - 1
                If Points_Rolls_Pins(i) < Points_Rolls_Pins(j) Then

                    tempPoints_Rolls_Pins = Points_Rolls_Pins(i)
                    Points_Rolls_Pins(i) = Points_Rolls_Pins(j)
                    Points_Rolls_Pins(j) = tempPoints_Rolls_Pins

                    tempPoints = Points(i)
                    Points(i) = Points(j)
                    Points(j) = tempPoints

                    tempRolls = Rolls(i)
                    Rolls(i) = Rolls(j)
                    Rolls(j) = tempRolls

                    tempPins = Pins(i)
                    Pins(i) = Pins(j)
                    Pins(j) = tempPins

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
                LastMatch = Points_Rolls_Pins(i)
            End If
            Pos2 = Pos2 + 1
            If LastMatch <> Points_Rolls_Pins(i) Then
                Pos1 = Pos2
                LastMatch = Points_Rolls_Pins(i)
            End If
            Pos(i) = Pos1

        Next

    End Sub

    Protected Sub re_update_table(inTable As String)
        Dim tempSeason As String
        tempSeason = objGlobals.get_current_season
        're-update the TABLES table
        strSQL = "DELETE FROM mens_skit.vw_" & inTable & " WHERE league ='" & FixtureLeague & "'"

        myDataReader = objGlobals.SQLSelect(strSQL)

        For i = 0 To gridTable.Rows.Count - 1
            strSQL = "INSERT INTO mens_skit." & inTable & " VALUES ('"
            strSQL = strSQL & tempSeason & "','"
            strSQL = strSQL & FixtureLeague & "',"
            strSQL = strSQL & Pos(i) & ",'"
            strSQL = strSQL & Team(i) & "',"
            strSQL = strSQL & Played(i) & ","
            strSQL = strSQL & Won(i) & ","
            strSQL = strSQL & Drawn(i) & ","
            strSQL = strSQL & Lost(i) & ","
            strSQL = strSQL & Points(i) & ","
            strSQL = strSQL & Rolls(i) & ","
            strSQL = strSQL & Pins(i) & ","
            strSQL = strSQL & Points_Rolls_Pins(i) & ")"

            myDataReader = objGlobals.SQLSelect(strSQL)
        Next

    End Sub

    Sub update_log(inSQL As String)
        inSQL = Replace(inSQL, "'", "''")
        Dim logSQL As String
        Dim strNow As String = Format(DateTime.Now, "dd/MM/yyyy HH:mm:ss.ffff")
        logSQL = "INSERT INTO mens_skit.update_log VALUES ('" & strNow & "','" & inSQL & "')"
        myDataReader = objGlobals.SQLSelect(logSQL)
    End Sub

    Sub load_table(inGrid As GridView, inTable As String)

        If inTable = "Tables" Then lblTableAfter.Visible = True

        dt = New DataTable

        'add header row
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Pld", GetType(System.String)))
        dt.Columns.Add(New DataColumn("w", GetType(System.String)))
        dt.Columns.Add(New DataColumn("d", GetType(System.String)))
        dt.Columns.Add(New DataColumn("l", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Pts", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Rolls", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Pins", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Move", GetType(System.String)))

        strSQL = "SELECT * FROM mens_skit." & inTable & " WHERE season = '" & objGlobals.current_season & "' AND league = '" & FixtureLeague & "' ORDER BY pos"
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
                dr("Pts") = myDataReader.Item("pts")
                dr("Rolls") = myDataReader.Item("rolls")
                dr("Pins") = myDataReader.Item("pins")
                dr("Move") = ""
                dt.Rows.Add(dr)
            End With
        End While

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
                inGrid.Rows(i).Cells(6).BackColor = inGrid.BackColor
                inGrid.Rows(i).Cells(7).ForeColor = Gray
                inGrid.Rows(i).Cells(8).ForeColor = Gray
            End If
            Pos(i) = inGrid.Rows(i).Cells(0).Text
            Team(i) = inGrid.Rows(i).Cells(1).Text
            Played(i) = inGrid.Rows(i).Cells(2).Text
            Won(i) = inGrid.Rows(i).Cells(3).Text
            Drawn(i) = inGrid.Rows(i).Cells(4).Text
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
                        gridTable2.Rows(i).Cells(9).Text = "-"
                    Case Is > 0
                        gridTable2.Rows(i).Cells(9).Text = "Down " & PosDiff
                    Case Is < 0
                        gridTable2.Rows(i).Cells(9).Text = "Up " & Math.Abs(PosDiff)
                End Select
            End If
            If gridTable2.Rows(i).Cells(1).Text = FixtureAwayTeam Then
                NewPosAway = Val(gridTable2.Rows(i).Cells(0).Text)
                PosDiff = NewPosAway - OldPosAway
                Select Case PosDiff
                    Case 0
                        gridTable2.Rows(i).Cells(9).Text = "-"
                    Case Is > 0
                        gridTable2.Rows(i).Cells(9).Text = "Down " & PosDiff
                    Case Is < 0
                        gridTable2.Rows(i).Cells(9).Text = "Up " & Math.Abs(PosDiff)
                End Select
            End If
        Next
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        If objGlobals.TeamSelected Is Nothing Then
            Response.Redirect("~/Mens_Skit/Default.aspx?Week=" & FixtureWeek)
        Else
            Response.Redirect("~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected)
        End If
    End Sub

    Protected Sub btnOK_Click(sender As Object, e As System.EventArgs) Handles btnOK.Click
        If objGlobals.TeamSelected Is Nothing Then
            Response.Redirect("~/Mens_Skit/Default.aspx?Week=" & FixtureWeek)
        Else
            Response.Redirect("~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected)
        End If
    End Sub

    Sub load_header()
        gRow = 0
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
        dr("Match") = "RESULT CARD"
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


    End Sub

    Sub load_result()
        dr = dt.NewRow
        dr("Home Player") = "Player"
        dr("Home Points") = "Score"
        dr("Away Player") = "Player"
        dr("Away Points") = "Score"
        dt.Rows.Add(dr)
        TotalHome = 0
        TotalAway = 0
        strSQL = "SELECT * FROM mens_skit.vw_fixtures_detail WHERE fixture_id = " & CompID & " ORDER BY match"
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
            dt.Rows.Add(dr)
        End While

    End Sub

    Sub load_totals()
        If FixtureStatus = 2 Then
            dr = dt.NewRow
            dr("Home Player") = "PLAYERS TOTAL :"
            dr("Home Points") = home_rolls_total
            dr("Away Player") = "PLAYERS TOTAL :"
            dr("Away Points") = away_rolls_total
            dt.Rows.Add(dr)
        End If
        dr = dt.NewRow
        dr("Home Player") = "POINTS :"
        dr("Home Points") = HomePoints
        dr("Away Player") = "POINTS :"
        dr("Away Points") = AwayPoints
        dt.Rows.Add(dr)
    End Sub

    Sub load_rolls()
        dr = dt.NewRow
        dr("Home Player") = "ROLL 1 :"
        dr("Home Points") = home_roll_1
        dr("Away Player") = "ROLL 1 :"
        dr("Away Points") = away_roll_1
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Home Player") = "ROLL 2 :"
        dr("Home Points") = home_roll_2
        dr("Away Player") = "ROLL 2 :"
        dr("Away Points") = away_roll_2
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Home Player") = "ROLL 3 :"
        dr("Home Points") = home_roll_3
        dr("Away Player") = "ROLL 3 :"
        dr("Away Points") = away_roll_3
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Home Player") = "ROLL 4 :"
        dr("Home Points") = home_roll_4
        dr("Away Player") = "ROLL 4 :"
        dr("Away Points") = away_roll_4
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Home Player") = "ROLL 5 :"
        dr("Home Points") = home_roll_5
        dr("Away Player") = "ROLL 5 :"
        dr("Away Points") = away_roll_5
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Home Player") = "ROLLS TOTAL :"
        dr("Home Points") = home_roll_total
        dr("Away Player") = "ROLLS TOTAL :"
        dr("Away Points") = away_roll_total
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Home Player") = "ROLLS :"
        dr("Away Player") = "ROLLS :"
        dr("Home Points") = home_rolls_won
        dr("Away Points") = away_rolls_won
        dt.Rows.Add(dr)

        gRow = 0
        gridResult.Visible = True
        gridResult.DataSource = dt
        gridResult.DataBind()
    End Sub


    Private Sub gridResult_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResult.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Select Case e.Row.Cells(1).Text
                Case "PLAYERS TOTAL :", "ROLLS TOTAL :"
                    e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Font.Bold = True
                Case "POINTS :", "ROLLS :"
                    e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Font.Bold = True
                Case Else
                    If gRow > 4 Then
                        If gRow <= 16 Then
                            Dim hLink1 As New HyperLink
                            hLink1.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & FixtureLeague & "&Team=" & FixtureHomeTeam & "&Player=" & dt.Rows(gRow - 1)(1).ToString
                            hLink1.ForeColor = Black
                            hLink1.Text = e.Row.Cells(1).Text
                            hLink1.BackColor = gridResult.BackColor
                            e.Row.Cells(1).Controls.Add(hLink1)

                            Dim hLink2 As New HyperLink
                            hLink2.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & FixtureLeague & "&Team=" & FixtureAwayTeam & "&Player=" & dt.Rows(gRow - 1)(3).ToString
                            hLink2.ForeColor = Black
                            hLink2.Text = e.Row.Cells(3).Text
                            hLink2.BackColor = gridResult.BackColor
                            e.Row.Cells(3).Controls.Add(hLink2)
                        End If
                    Else
                        Select Case gRow
                            Case 1
                                e.Row.Cells(0).Font.Bold = True
                                e.Row.Cells(0).Font.Size = 14
                                e.Row.Cells(0).ColumnSpan = 4
                                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
                                For iCell As Integer = 1 To 3
                                    e.Row.Cells(iCell).Visible = False
                                Next
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
                                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
                        End Select
                    End If
            End Select
            ''e.Row.CssClass = "row"
        Else
            e.Row.Visible = False
        End If
        gRow = gRow + 1

    End Sub

    Sub calc_rolls_result()
        Dim home_rolls_points As Double = 0
        Dim away_rolls_points As Double = 0

        Dim home_total As Integer = 0
        Dim away_total As Integer = 0

        home_total = Val(txtHomeRoll1.Text) + Val(txtHomeRoll2.Text) + Val(txtHomeRoll3.Text) + Val(txtHomeRoll4.Text) + Val(txtHomeRoll5.Text)
        away_total = Val(txtAwayRoll1.Text) + Val(txtAwayRoll2.Text) + Val(txtAwayRoll3.Text) + Val(txtAwayRoll4.Text) + Val(txtAwayRoll5.Text)

        If Val(txtHomeRoll1.Text) > Val(txtAwayRoll1.Text) Then home_rolls_points = home_rolls_points + 1
        If Val(txtHomeRoll1.Text) = Val(txtAwayRoll1.Text) Then home_rolls_points = home_rolls_points + 0.5 : away_rolls_points = away_rolls_points + 0.5
        If Val(txtHomeRoll1.Text) < Val(txtAwayRoll1.Text) Then away_rolls_points = away_rolls_points + 1

        If Val(txtHomeRoll2.Text) > Val(txtAwayRoll2.Text) Then home_rolls_points = home_rolls_points + 1
        If Val(txtHomeRoll2.Text) = Val(txtAwayRoll2.Text) Then home_rolls_points = home_rolls_points + 0.5 : away_rolls_points = away_rolls_points + 0.5
        If Val(txtHomeRoll2.Text) < Val(txtAwayRoll2.Text) Then away_rolls_points = away_rolls_points + 1

        If Val(txtHomeRoll3.Text) > Val(txtAwayRoll3.Text) Then home_rolls_points = home_rolls_points + 1
        If Val(txtHomeRoll3.Text) = Val(txtAwayRoll3.Text) Then home_rolls_points = home_rolls_points + 0.5 : away_rolls_points = away_rolls_points + 0.5
        If Val(txtHomeRoll3.Text) < Val(txtAwayRoll3.Text) Then away_rolls_points = away_rolls_points + 1

        If Val(txtHomeRoll4.Text) > Val(txtAwayRoll4.Text) Then home_rolls_points = home_rolls_points + 1
        If Val(txtHomeRoll4.Text) = Val(txtAwayRoll4.Text) Then home_rolls_points = home_rolls_points + 0.5 : away_rolls_points = away_rolls_points + 0.5
        If Val(txtHomeRoll4.Text) < Val(txtAwayRoll4.Text) Then away_rolls_points = away_rolls_points + 1

        If Val(txtHomeRoll5.Text) > Val(txtAwayRoll5.Text) Then home_rolls_points = home_rolls_points + 1
        If Val(txtHomeRoll5.Text) = Val(txtAwayRoll5.Text) Then home_rolls_points = home_rolls_points + 0.5 : away_rolls_points = away_rolls_points + 0.5
        If Val(txtHomeRoll5.Text) < Val(txtAwayRoll5.Text) Then away_rolls_points = away_rolls_points + 1

        txtHomeRollsPoints.Text = home_rolls_points
        txtAwayRollsPoints.Text = away_rolls_points

        txtHomeTotal.Text = home_total
        txtAwayTotal.Text = away_total

        If home_total > 0 And away_total > 0 Then
            If home_total > away_total Then rbResults.SelectedIndex = 1
            If home_total = away_total Then rbResults.SelectedIndex = 2
            If home_total < away_total Then rbResults.SelectedIndex = 3
        End If

    End Sub

    Sub enter_roll_total(inScore As String)
        If txtHomeRoll1.Text = "0" Then txtHomeRoll1.Text = Val(inScore) : txtHomeRoll2.BackColor = LightBlue : calc_rolls_result() : Exit Sub
        If txtHomeRoll2.Text = "0" Then txtHomeRoll2.Text = Val(inScore) : txtHomeRoll3.BackColor = LightBlue : calc_rolls_result() : Exit Sub
        If txtHomeRoll3.Text = "0" Then txtHomeRoll3.Text = Val(inScore) : txtHomeRoll4.BackColor = LightBlue : calc_rolls_result() : Exit Sub
        If txtHomeRoll4.Text = "0" Then txtHomeRoll4.Text = Val(inScore) : txtHomeRoll5.BackColor = LightBlue : calc_rolls_result() : Exit Sub
        If txtHomeRoll5.Text = "0" Then txtHomeRoll5.Text = Val(inScore) : txtAwayRoll1.BackColor = LightBlue : calc_rolls_result() : Exit Sub

        If txtAwayRoll1.Text = "0" Then txtAwayRoll1.Text = Val(inScore) : txtAwayRoll2.BackColor = LightBlue : calc_rolls_result() : Exit Sub
        If txtAwayRoll2.Text = "0" Then txtAwayRoll2.Text = Val(inScore) : txtAwayRoll3.BackColor = LightBlue : calc_rolls_result() : Exit Sub
        If txtAwayRoll3.Text = "0" Then txtAwayRoll3.Text = Val(inScore) : txtAwayRoll4.BackColor = LightBlue : calc_rolls_result() : Exit Sub
        If txtAwayRoll4.Text = "0" Then txtAwayRoll4.Text = Val(inScore) : txtAwayRoll5.BackColor = LightBlue : calc_rolls_result() : Exit Sub
        If txtAwayRoll5.Text = "0" Then txtAwayRoll5.Text = Val(inScore) : calc_rolls_result() : Exit Sub
    End Sub

    Protected Sub btn20_Click(sender As Object, e As System.EventArgs) Handles btn20.Click
        Call enter_roll_total(btn20.Text)
    End Sub
    Protected Sub btn21_Click(sender As Object, e As System.EventArgs) Handles btn21.Click
        Call enter_roll_total(btn21.Text)
    End Sub
    Protected Sub btn22_Click(sender As Object, e As System.EventArgs) Handles btn22.Click
        Call enter_roll_total(btn22.Text)
    End Sub
    Protected Sub btn23_Click(sender As Object, e As System.EventArgs) Handles btn23.Click
        Call enter_roll_total(btn23.Text)
    End Sub
    Protected Sub btn24_Click(sender As Object, e As System.EventArgs) Handles btn24.Click
        Call enter_roll_total(btn24.Text)
    End Sub
    Protected Sub btn25_Click(sender As Object, e As System.EventArgs) Handles btn25.Click
        Call enter_roll_total(btn25.Text)
    End Sub
    Protected Sub btn26_Click(sender As Object, e As System.EventArgs) Handles btn26.Click
        Call enter_roll_total(btn26.Text)
    End Sub
    Protected Sub btn27_Click(sender As Object, e As System.EventArgs) Handles btn27.Click
        Call enter_roll_total(btn27.Text)
    End Sub
    Protected Sub btn28_Click(sender As Object, e As System.EventArgs) Handles btn28.Click
        Call enter_roll_total(btn28.Text)
    End Sub
    Protected Sub btn29_Click(sender As Object, e As System.EventArgs) Handles btn29.Click
        Call enter_roll_total(btn29.Text)
    End Sub
    Protected Sub btn30_Click(sender As Object, e As System.EventArgs) Handles btn30.Click
        Call enter_roll_total(btn30.Text)
    End Sub
    Protected Sub btn31_Click(sender As Object, e As System.EventArgs) Handles btn31.Click
        Call enter_roll_total(btn31.Text)
    End Sub
    Protected Sub btn32_Click(sender As Object, e As System.EventArgs) Handles btn32.Click
        Call enter_roll_total(btn32.Text)
    End Sub
    Protected Sub btn33_Click(sender As Object, e As System.EventArgs) Handles btn33.Click
        Call enter_roll_total(btn33.Text)
    End Sub
    Protected Sub btn34_Click(sender As Object, e As System.EventArgs) Handles btn34.Click
        Call enter_roll_total(btn34.Text)
    End Sub
    Protected Sub btn35_Click(sender As Object, e As System.EventArgs) Handles btn35.Click
        Call enter_roll_total(btn35.Text)
    End Sub
    Protected Sub btn36_Click(sender As Object, e As System.EventArgs) Handles btn36.Click
        Call enter_roll_total(btn36.Text)
    End Sub
    Protected Sub btn37_Click(sender As Object, e As System.EventArgs) Handles btn37.Click
        Call enter_roll_total(btn37.Text)
    End Sub
    Protected Sub btn38_Click(sender As Object, e As System.EventArgs) Handles btn38.Click
        Call enter_roll_total(btn38.Text)
    End Sub
    Protected Sub btn39_Click(sender As Object, e As System.EventArgs) Handles btn39.Click
        Call enter_roll_total(btn39.Text)
    End Sub
    Protected Sub btn40_Click(sender As Object, e As System.EventArgs) Handles btn40.Click
        Call enter_roll_total(btn40.Text)
    End Sub
    Protected Sub btn41_Click(sender As Object, e As System.EventArgs) Handles btn41.Click
        Call enter_roll_total(btn41.Text)
    End Sub
    Protected Sub btn42_Click(sender As Object, e As System.EventArgs) Handles btn42.Click
        Call enter_roll_total(btn42.Text)
    End Sub
    Protected Sub btn43_Click(sender As Object, e As System.EventArgs) Handles btn43.Click
        Call enter_roll_total(btn43.Text)
    End Sub
    Protected Sub btn44_Click(sender As Object, e As System.EventArgs) Handles btn44.Click
        Call enter_roll_total(btn44.Text)
    End Sub
    Protected Sub btn45_Click(sender As Object, e As System.EventArgs) Handles btn45.Click
        Call enter_roll_total(btn45.Text)
    End Sub
    Protected Sub btn46_Click(sender As Object, e As System.EventArgs) Handles btn46.Click
        Call enter_roll_total(btn46.Text)
    End Sub
    Protected Sub btn47_Click(sender As Object, e As System.EventArgs) Handles btn47.Click
        Call enter_roll_total(btn47.Text)
    End Sub
    Protected Sub btn48_Click(sender As Object, e As System.EventArgs) Handles btn48.Click
        Call enter_roll_total(btn48.Text)
    End Sub
    Protected Sub btn49_Click(sender As Object, e As System.EventArgs) Handles btn49.Click
        Call enter_roll_total(btn49.Text)
    End Sub
    Protected Sub btn50_Click(sender As Object, e As System.EventArgs) Handles btn50.Click
        Call enter_roll_total(btn50.Text)
    End Sub
    Protected Sub btn51_Click(sender As Object, e As System.EventArgs) Handles btn51.Click
        Call enter_roll_total(btn51.Text)
    End Sub
    Protected Sub btn52_Click(sender As Object, e As System.EventArgs) Handles btn52.Click
        Call enter_roll_total(btn52.Text)
    End Sub
    Protected Sub btn53_Click(sender As Object, e As System.EventArgs) Handles btn53.Click
        Call enter_roll_total(btn53.Text)
    End Sub
    Protected Sub btn54_Click(sender As Object, e As System.EventArgs) Handles btn54.Click
        Call enter_roll_total(btn54.Text)
    End Sub
    Protected Sub btn55_Click(sender As Object, e As System.EventArgs) Handles btn55.Click
        Call enter_roll_total(btn55.Text)
    End Sub
    Protected Sub btn56_Click(sender As Object, e As System.EventArgs) Handles btn56.Click
        Call enter_roll_total(btn56.Text)
    End Sub
    Protected Sub btn57_Click(sender As Object, e As System.EventArgs) Handles btn57.Click
        Call enter_roll_total(btn57.Text)
    End Sub
    Protected Sub btn58_Click(sender As Object, e As System.EventArgs) Handles btn58.Click
        Call enter_roll_total(btn58.Text)
    End Sub
    Protected Sub btn59_Click(sender As Object, e As System.EventArgs) Handles btn59.Click
        Call enter_roll_total(btn59.Text)
    End Sub
    Protected Sub btn60_Click(sender As Object, e As System.EventArgs) Handles btn60.Click
        Call enter_roll_total(btn60.Text)
    End Sub
    Protected Sub btn61_Click(sender As Object, e As System.EventArgs) Handles btn61.Click
        Call enter_roll_total(btn61.Text)
    End Sub
    Protected Sub btn62_Click(sender As Object, e As System.EventArgs) Handles btn62.Click
        Call enter_roll_total(btn62.Text)
    End Sub
    Protected Sub btn63_Click(sender As Object, e As System.EventArgs) Handles btn63.Click
        Call enter_roll_total(btn63.Text)
    End Sub
    Protected Sub btn64_Click(sender As Object, e As System.EventArgs) Handles btn64.Click
        Call enter_roll_total(btn64.Text)
    End Sub
    Protected Sub btn65_Click(sender As Object, e As System.EventArgs) Handles btn65.Click
        Call enter_roll_total(btn65.Text)
    End Sub
    Protected Sub btn66_Click(sender As Object, e As System.EventArgs) Handles btn66.Click
        Call enter_roll_total(btn66.Text)
    End Sub
    Protected Sub btn67_Click(sender As Object, e As System.EventArgs) Handles btn67.Click
        Call enter_roll_total(btn67.Text)
    End Sub
    Protected Sub btn68_Click(sender As Object, e As System.EventArgs) Handles btn68.Click
        Call enter_roll_total(btn68.Text)
    End Sub
    Protected Sub btn69_Click(sender As Object, e As System.EventArgs) Handles btn69.Click
        Call enter_roll_total(btn69.Text)
    End Sub
    Protected Sub btn70_Click(sender As Object, e As System.EventArgs) Handles btn70.Click
        Call enter_roll_total(btn70.Text)
    End Sub
    Protected Sub btn71_Click(sender As Object, e As System.EventArgs) Handles btn71.Click
        Call enter_roll_total(btn71.Text)
    End Sub
    Protected Sub btn72_Click(sender As Object, e As System.EventArgs) Handles btn72.Click
        Call enter_roll_total(btn72.Text)
    End Sub
    Protected Sub btn73_Click(sender As Object, e As System.EventArgs) Handles btn73.Click
        Call enter_roll_total(btn73.Text)
    End Sub
    Protected Sub btn74_Click(sender As Object, e As System.EventArgs) Handles btn74.Click
        Call enter_roll_total(btn74.Text)
    End Sub
    Protected Sub btn75_Click(sender As Object, e As System.EventArgs) Handles btn75.Click
        Call enter_roll_total(btn75.Text)
    End Sub
    Protected Sub btn76_Click(sender As Object, e As System.EventArgs) Handles btn76.Click
        Call enter_roll_total(btn76.Text)
    End Sub
    Protected Sub btn77_Click(sender As Object, e As System.EventArgs) Handles btn77.Click
        Call enter_roll_total(btn77.Text)
    End Sub
    Protected Sub btn78_Click(sender As Object, e As System.EventArgs) Handles btn78.Click
        Call enter_roll_total(btn78.Text)
    End Sub
    Protected Sub btn79_Click(sender As Object, e As System.EventArgs) Handles btn79.Click
        Call enter_roll_total(btn79.Text)
    End Sub

    Protected Sub btnUpdateHeader_Click(sender As Object, e As System.EventArgs) Handles btnUpdateHeader.Click
        btnUpdateHeader.Text = "Updating ..." : System.Windows.Forms.Application.DoEvents()
        Dim ok_update As Boolean = True

        If rbResults.SelectedIndex = -1 Then
            rbResults.BackColor = LightSalmon
            Exit Sub
        Else
            rbResults.BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
        End If

        Call update_result(rbResults.SelectedItem.Text)

        fixture_status = FixtureStatus()
        Call load_header()

        If rbResults.SelectedItem.Text <> "0-0" And rbResults.SelectedItem.Text <> "Postponed" Then
            'check it's okay to update
            If txtHomeRoll1.Text = "0" Then txtHomeRoll1.BackColor = Red : ok_update = False
            If txtHomeRoll2.Text = "0" Then txtHomeRoll2.BackColor = Red : ok_update = False
            If txtHomeRoll3.Text = "0" Then txtHomeRoll3.BackColor = Red : ok_update = False
            If txtHomeRoll4.Text = "0" Then txtHomeRoll4.BackColor = Red : ok_update = False
            If txtHomeRoll5.Text = "0" Then txtHomeRoll5.BackColor = Red : ok_update = False

            If txtAwayRoll1.Text = "0" Then txtAwayRoll1.BackColor = Red : ok_update = False
            If txtAwayRoll2.Text = "0" Then txtAwayRoll2.BackColor = Red : ok_update = False
            If txtAwayRoll3.Text = "0" Then txtAwayRoll3.BackColor = Red : ok_update = False
            If txtAwayRoll4.Text = "0" Then txtAwayRoll4.BackColor = Red : ok_update = False
            If txtAwayRoll5.Text = "0" Then txtAwayRoll5.BackColor = Red : ok_update = False

            If Not ok_update Then Exit Sub

            If fixture_status = 2 Then Call load_result()
            Call load_totals()

            If fixture_status >= 1 Then
                Call load_rolls()
            End If
            If fixture_status = 2 Then
                Call colour_rolls()
                Call colour_thirties()
                Call colour_high_scores()
                Call colour_totals()
            End If
            lblTableAfter.Visible = True
            gridTable2.Visible = True
            btnUpdateHeader.Visible = False
            btnAddEditResult.Visible = True
            btnAddEditResult.Focus()
        Else
            txtHomeRoll1.Text = "0"
            txtHomeRoll2.Text = "0"
            txtHomeRoll3.Text = "0"
            txtHomeRoll4.Text = "0"
            txtHomeRoll5.Text = "0"
            txtHomeRollsPoints.Text = "0"
            txtHomeTotal.Text = "0"
            txtAwayRoll1.Text = "0"
            txtAwayRoll2.Text = "0"
            txtAwayRoll3.Text = "0"
            txtAwayRoll4.Text = "0"
            txtAwayRoll5.Text = "0"
            txtAwayRollsPoints.Text = "0"
            txtAwayTotal.Text = "0"
            gRow = 0
            gridResult.Visible = True
            gridResult.DataSource = dt
            gridResult.DataBind()

            btnOK.Focus()
        End If

        write_fixtures_FTP()

        'If objGlobals.TeamSelected Is Nothing Then
        'Response.Redirect("~/Mens_Skit/Default.aspx?Week=" & FixtureWeek)
        'Else
        'Response.Redirect("~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected)
        'End If
    End Sub
    Sub write_fixtures_FTP()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim l_param_in_names(0) As String
        Dim l_param_in_values(0) As String

        l_param_in_names(0) = "@inFixtureID"
        l_param_in_values(0) = CompID

        strSQL = "EXEC [mens_skit].[sp_write_fixtures_FTP] '" & objGlobals.current_season & "'," & l_param_in_values(0)
        myDataReader = objGlobals.SQLSelect(strSQL)

        objGlobals.close_connection()
    End Sub
    Sub colour_high_scores()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim home_high_score As Integer
        Dim away_high_score As Integer

        strSQL = "SELECT MAX(home_points),MAX(away_points) FROM mens_skit.vw_fixtures_detail WHERE fixture_id = " & CompID
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            home_high_score = myDataReader.Item(0)
            away_high_score = myDataReader.Item(1)
        End While
        For gRow As Integer = 4 To 15
            If Val(gridResult.Rows(gRow).Cells(2).Text) = home_high_score Then
                gridResult.Rows(gRow).Cells(2).BackColor = Blue
                gridResult.Rows(gRow).Cells(2).ForeColor = White
            End If
            If Val(gridResult.Rows(gRow).Cells(4).Text) = away_high_score Then
                gridResult.Rows(gRow).Cells(4).BackColor = Blue
                gridResult.Rows(gRow).Cells(4).ForeColor = White
            End If
        Next
    End Sub

    Sub colour_thirties()
        For gRow As Integer = 4 To 15
            If Val(gridResult.Rows(gRow).Cells(4).Text) >= 30 Then
                gridResult.Rows(gRow).Cells(4).BackColor = LightGreen
            End If
        Next
    End Sub

    Sub colour_rolls()
        For gRow As Integer = 18 To 22
            If Val(gridResult.Rows(gRow).Cells(2).Text) > Val(gridResult.Rows(gRow).Cells(4).Text) Then
                gridResult.Rows(gRow).Cells(2).BackColor = LightGreen
            End If
            If Val(gridResult.Rows(gRow).Cells(2).Text) < Val(gridResult.Rows(gRow).Cells(4).Text) Then
                gridResult.Rows(gRow).Cells(4).BackColor = LightGreen
            End If
        Next
    End Sub

    Sub colour_totals()
        If HomePoints > AwayPoints Then
            gridResult.Rows(16).Cells(2).BackColor = Green
            gridResult.Rows(17).Cells(2).BackColor = Green
            gridResult.Rows(16).Cells(2).ForeColor = White
            gridResult.Rows(17).Cells(2).ForeColor = White
        End If
        If HomePoints < AwayPoints Then
            gridResult.Rows(16).Cells(4).BackColor = Green
            gridResult.Rows(17).Cells(4).BackColor = Green
            gridResult.Rows(16).Cells(4).ForeColor = White
            gridResult.Rows(17).Cells(4).ForeColor = White
        End If
        If home_roll_total > away_roll_total Then
            gridResult.Rows(23).Cells(2).BackColor = Green
            gridResult.Rows(23).Cells(2).ForeColor = White
        End If
        If home_roll_total < away_roll_total Then
            gridResult.Rows(23).Cells(4).BackColor = Green
            gridResult.Rows(23).Cells(4).ForeColor = White
        End If
        If home_rolls_won > away_rolls_won Then
            gridResult.Rows(24).Cells(2).BackColor = Green
            gridResult.Rows(24).Cells(2).ForeColor = White
        End If
        If home_rolls_won < away_rolls_won Then
            gridResult.Rows(24).Cells(4).BackColor = Green
            gridResult.Rows(24).Cells(4).ForeColor = White
        End If
    End Sub

    Protected Sub txtHomeRoll1_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRoll1.TextChanged
        txtHomeRoll1.Text = Val(txtHomeRoll1.Text)
        calc_rolls_result()
    End Sub
    Protected Sub txtHomeRoll2_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRoll2.TextChanged
        txtHomeRoll2.Text = Val(txtHomeRoll2.Text)
        calc_rolls_result()
    End Sub
    Protected Sub txtHomeRoll3_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRoll3.TextChanged
        txtHomeRoll3.Text = Val(txtHomeRoll3.Text)
        calc_rolls_result()
    End Sub
    Protected Sub txtHomeRoll4_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRoll4.TextChanged
        txtHomeRoll4.Text = Val(txtHomeRoll4.Text)
        calc_rolls_result()
    End Sub
    Protected Sub txtHomeRoll5_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRoll5.TextChanged
        txtHomeRoll5.Text = Val(txtHomeRoll5.Text)
        calc_rolls_result()
    End Sub

    Protected Sub txtAwayRoll1_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRoll1.TextChanged
        txtAwayRoll1.Text = Val(txtAwayRoll1.Text)
        calc_rolls_result()
    End Sub
    Protected Sub txtAwayRoll2_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRoll2.TextChanged
        txtAwayRoll2.Text = Val(txtAwayRoll2.Text)
        calc_rolls_result()
    End Sub
    Protected Sub txtAwayRoll3_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRoll3.TextChanged
        txtAwayRoll3.Text = Val(txtAwayRoll3.Text)
        calc_rolls_result()
    End Sub
    Protected Sub txtAwayRoll4_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRoll4.TextChanged
        txtAwayRoll4.Text = Val(txtAwayRoll4.Text)
        calc_rolls_result()
    End Sub
    Protected Sub txtAwayRoll5_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRoll5.TextChanged
        txtAwayRoll5.Text = Val(txtAwayRoll5.Text)
        calc_rolls_result()
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
        l_param_in_values(2) = "" 'not required
        l_param_in_names(3) = "@inVenue"
        l_param_in_values(3) = lblVenue.Text
        l_param_in_names(4) = "@inFixtureCalendar"
        l_param_in_values(4) = l_new_date
        l_param_in_names(5) = "@inStatus"
        l_param_in_values(5) = 0

        strSQL = "EXEC [mens_skit.tools_16].[sp_check_fixture_exists] '" & l_param_in_values(0) & "'," & l_param_in_values(1) & ",'" & l_param_in_values(2) & "','" & l_param_in_values(3) & "','" & l_param_in_values(4) & "','" & l_param_in_values(5) & "'"
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
        l_param_in_values(4) = Calendar1.SelectedDate
        l_param_in_names(5) = "@inStatus"
        l_param_in_values(5) = Val(txtNewStatus.Text)

        strSQL = "EXEC [mens_skit.tools_16].[sp_update_fixture_date_status] '" & l_param_in_values(0) & "'," & l_param_in_values(1) & ",'" & l_param_in_values(2) & "','" & l_param_in_values(3) & "','" & l_param_in_values(4) & "','" & l_param_in_values(5) & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        write_fixtures_FTP()

        If objGlobals.TeamSelected Is Nothing Then
            Response.Redirect("~/Mens_Skit/Default.aspx?Week=" & FixtureWeek)
        Else
            Response.Redirect("~/Mens_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & objGlobals.TeamSelected)
        End If
    End Sub
End Class
