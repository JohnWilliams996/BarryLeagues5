Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
Imports System.Diagnostics
Imports System.IO
'Imports MySql.Data.MySqlClient

Partial Class Admin_Add_Edit_Result
    Inherits System.Web.UI.Page
    Private fixture_id As Integer
    Private objGlobals As New Globals
    Private home_result As String
    Private away_result As String
    Private HomePointsDeducted As Integer
    Private AwayPointsDeducted As Integer
    Private SelectedHomePlayers As String
    Private SelectedAwayPlayers As String
    Private FixtureWeek As Integer
    Private TeamSelected As String
    Private Moved As Boolean
    Private HomeRollsWon As Single
    Private HomeRollsTotal As Integer
    Private AwayRollsWon As Single
    Private AwayRollsTotal As Integer
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
    Private FixtureStatus As Integer



    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("ladies_skit")
        objGlobals.CurrentUser = "ladies_skit_user" : objGlobals.CurrentSchema = "ladies_skit."
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
            lblLeague.Text = "NOT AUTHORIZED"
            lblDateLiteral.Visible = False
            lblDate.Visible = False
            lblHomeTeamLiteral.Visible = False
            lblHomeTeam.Visible = False
            lblAwayTeamLiteral.Visible = False
            lblAwayTeam.Visible = False
            lblID.Visible = False
            lblResult.Visible = False
            lblHomePointsDeducted.Visible = False
            lblAwayPointsDeducted.Visible = False
            ddHomePointsDeducted.Visible = False
            ddAwayPointsDeducted.Visible = False
            rbResults.Visible = False
            lblAddNewHomePlayer.Visible = False
            lblAddNewAwayPlayer.Visible = False
            btnAdd1.Visible = False
            btnAdd2.Visible = False
            txtAddHomePlayer.Visible = False
            txtAddAwayPlayer.Visible = False
            lblHomePlayersAvailable.Visible = False
            lblAwayPlayersAvailable.Visible = False
            btnUpdate.Visible = False
            lblReset.Visible = False
            btnReset.Visible = False
            lblHomePoints1.Visible = False
            lblAwayPoints1.Visible = False
            lblHomePoints2.Visible = False
            lblAwayPoints2.Visible = False
            lblHomePoints3.Visible = False
            lblAwayPoints3.Visible = False
            lblHomePoints4.Visible = False
            lblAwayPoints4.Visible = False
            lblHomePoints5.Visible = False
            lblAwayPoints5.Visible = False
            lblHomePoints6.Visible = False
            lblAwayPoints6.Visible = False
            lblHomePlayer1.Visible = False
            lblAwayPlayer1.Visible = False
            lblHomePlayer2.Visible = False
            lblAwayPlayer2.Visible = False
            lblHomePlayer3.Visible = False
            lblAwayPlayer3.Visible = False
            lblHomePlayer4.Visible = False
            lblAwayPlayer4.Visible = False
            lblHomePlayer5.Visible = False
            lblAwayPlayer5.Visible = False
            lblHomePlayer6.Visible = False
            lblAwayPlayer6.Visible = False
            lblHomePlayer7.Visible = False
            lblAwayPlayer7.Visible = False
            lblHomePlayer8.Visible = False
            lblAwayPlayer8.Visible = False
            lblHomePlayer9.Visible = False
            lblAwayPlayer9.Visible = False
            lblHomePlayer10.Visible = False
            lblAwayPlayer10.Visible = False
            lblHomePlayer11.Visible = False
            lblAwayPlayer11.Visible = False
            lblHomePlayer12.Visible = False
            lblAwayPlayer12.Visible = False
            lblHomePoints7.Visible = False
            lblAwayPoints7.Visible = False
            lblHomePoints8.Visible = False
            lblAwayPoints8.Visible = False
            lblHomePoints9.Visible = False
            lblAwayPoints9.Visible = False
            lblHomePoints10.Visible = False
            lblAwayPoints10.Visible = False
            lblHomePoints11.Visible = False
            lblAwayPoints11.Visible = False
            lblHomePoints12.Visible = False
            lblAwayPoints12.Visible = False
            btLeft1.Visible = False
            btLeft2.Visible = False
            btLeft3.Visible = False
            btLeft4.Visible = False
            btLeft5.Visible = False
            btLeft6.Visible = False
            btLeft7.Visible = False
            btLeft8.Visible = False
            btLeft9.Visible = False
            btLeft10.Visible = False
            btLeft11.Visible = False
            btLeft12.Visible = False
            btRight1.Visible = False
            btRight2.Visible = False
            btRight3.Visible = False
            btRight4.Visible = False
            btRight5.Visible = False
            btRight6.Visible = False
            btRight7.Visible = False
            btRight8.Visible = False
            btRight9.Visible = False
            btRight10.Visible = False
            btRight11.Visible = False
            btRight12.Visible = False
            lblHomePoints0.Visible = False
            lblAwayPoints0.Visible = False
            Call objGlobals.store_page("ATTACK FROM " & Request.Url.OriginalString, objGlobals.AdminLogin)
            Exit Sub
        End If
        If objGlobals.LiveTestFlag <> 3 Then
            btnRandom.Visible = True
        Else
            btnRandom.Visible = False
        End If
        fixture_id = Request.QueryString("ID")
        FixtureWeek = Request.QueryString("Week")
        TeamSelected = Request.QueryString("Team")
        If Not IsPostBack Then
            Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
            Call load_result()
            Call load_result_deductions()
            Call load_rolls()
            Call load_details()
            Call load_available_players()
            If FixtureStatus = 2 Then
                Call colour_player_rolls()
                Call colour_player_totals()
            End If
            rbResults.Visible = False
        End If
    End Sub

    Protected Sub load_result()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader    'MySqlDataReader
        strSQL = "SELECT * FROM " & objGlobals.CurrentSchema & "vw_fixtures WHERE fixture_id = " & fixture_id
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            lblDate.Text = myDataReader.Item("fixture_date")
            lblLeague.Text = myDataReader.Item("league")
            lblHomeTeam.Text = myDataReader.Item("home_team_name")
            lblAwayTeam.Text = myDataReader.Item("away_team_name")
            lblResult.Text = myDataReader.Item("home_result")
            HomePointsDeducted = myDataReader.Item("home_points_deducted")
            AwayPointsDeducted = myDataReader.Item("away_points_deducted")

            HomeRollsWon = myDataReader.Item("home_rolls_won")
            AwayRollsWon = myDataReader.Item("away_rolls_won")
            HomeRollsTotal = myDataReader.Item("home_rolls_total")
            AwayRollsTotal = myDataReader.Item("away_rolls_total")
            HomeRollTotal1 = myDataReader.Item("home_roll_1")
            HomeRollTotal2 = myDataReader.Item("home_roll_2")
            HomeRollTotal3 = myDataReader.Item("home_roll_3")
            HomeRollTotal4 = myDataReader.Item("home_roll_4")
            HomeRollTotal5 = myDataReader.Item("home_roll_5")
            AwayRollTotal1 = myDataReader.Item("away_roll_1")
            AwayRollTotal2 = myDataReader.Item("away_roll_2")
            AwayRollTotal3 = myDataReader.Item("away_roll_3")
            AwayRollTotal4 = myDataReader.Item("away_roll_4")
            AwayRollTotal5 = myDataReader.Item("away_roll_5")
            FixtureStatus = myDataReader.Item("status")
            lblID.Text = fixture_id
        End While

        If TeamSelected Is Nothing Then
            btnReset.PostBackUrl = "~/Ladies_Skit/Admin/Fixture Result.aspx?ID=" & fixture_id & "&Week=" & FixtureWeek
        Else
            btnReset.PostBackUrl = "~/Ladies_Skit/Admin/Fixture Result.aspx?ID=" & fixture_id & "&Week=" & FixtureWeek & "&League=" & lblLeague.Text & "&Team=" & TeamSelected
        End If
        btnAutoScores.Visible = True
        btnCalc.Visible = True
    End Sub

    Protected Sub btnAutoScores_Click(sender As Object, e As System.EventArgs) Handles btnAutoScores.Click
        lblHomePlayer1.BackColor = Blue
        txtHomeRolls1.BackColor = Blue
        txtHomeRolls1.Focus()
    End Sub

    Sub load_available_players()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader    'MySqlDataReader
        Dim AvailCount As Integer = 0
        Dim EmptyAvail As Integer = 0
        If SelectedHomePlayers = "" Then
            If objGlobals.LiveTestFlag = 3 Then
                strSQL = "SELECT player FROM " & objGlobals.CurrentSchema & "vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblHomeTeam.Text & "' ORDER BY Player"
            Else
                strSQL = "SELECT player FROM " & objGlobals.CurrentSchema & "vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblHomeTeam.Text & "' AND player NOT LIKE 'A N OTHER%' ORDER BY Player"
            End If
        Else
            strSQL = "SELECT player FROM " & objGlobals.CurrentSchema & "vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblHomeTeam.Text & "' AND player NOT IN (" & SelectedHomePlayers & ") ORDER BY Player"
        End If
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            AvailCount = AvailCount + 1
            Select Case AvailCount
                Case 1 : lblHomeAvailPlayer1.Visible = True : lblHomeAvailPlayer1.Text = myDataReader.Item("player") : btAvailRight1.Visible = True
                Case 2 : lblHomeAvailPlayer2.Visible = True : lblHomeAvailPlayer2.Text = myDataReader.Item("player") : btAvailRight2.Visible = True
                Case 3 : lblHomeAvailPlayer3.Visible = True : lblHomeAvailPlayer3.Text = myDataReader.Item("player") : btAvailRight3.Visible = True
                Case 4 : lblHomeAvailPlayer4.Visible = True : lblHomeAvailPlayer4.Text = myDataReader.Item("player") : btAvailRight4.Visible = True
                Case 5 : lblHomeAvailPlayer5.Visible = True : lblHomeAvailPlayer5.Text = myDataReader.Item("player") : btAvailRight5.Visible = True
                Case 6 : lblHomeAvailPlayer6.Visible = True : lblHomeAvailPlayer6.Text = myDataReader.Item("player") : btAvailRight6.Visible = True
                Case 7 : lblHomeAvailPlayer7.Visible = True : lblHomeAvailPlayer7.Text = myDataReader.Item("player") : btAvailRight7.Visible = True
                Case 8 : lblHomeAvailPlayer8.Visible = True : lblHomeAvailPlayer8.Text = myDataReader.Item("player") : btAvailRight8.Visible = True
                Case 9 : lblHomeAvailPlayer9.Visible = True : lblHomeAvailPlayer9.Text = myDataReader.Item("player") : btAvailRight9.Visible = True
                Case 10 : lblHomeAvailPlayer10.Visible = True : lblHomeAvailPlayer10.Text = myDataReader.Item("player") : btAvailRight10.Visible = True
                Case 11 : lblHomeAvailPlayer11.Visible = True : lblHomeAvailPlayer11.Text = myDataReader.Item("player") : btAvailRight11.Visible = True
                Case 12 : lblHomeAvailPlayer12.Visible = True : lblHomeAvailPlayer12.Text = myDataReader.Item("player") : btAvailRight12.Visible = True
                Case 13 : lblHomeAvailPlayer13.Visible = True : lblHomeAvailPlayer13.Text = myDataReader.Item("player") : btAvailRight13.Visible = True
                Case 14 : lblHomeAvailPlayer14.Visible = True : lblHomeAvailPlayer14.Text = myDataReader.Item("player") : btAvailRight14.Visible = True
                Case 15 : lblHomeAvailPlayer15.Visible = True : lblHomeAvailPlayer15.Text = myDataReader.Item("player") : btAvailRight15.Visible = True
                Case 16 : lblHomeAvailPlayer16.Visible = True : lblHomeAvailPlayer16.Text = myDataReader.Item("player") : btAvailRight16.Visible = True
                Case 17 : lblHomeAvailPlayer17.Visible = True : lblHomeAvailPlayer17.Text = myDataReader.Item("player") : btAvailRight17.Visible = True
                Case 18 : lblHomeAvailPlayer18.Visible = True : lblHomeAvailPlayer18.Text = myDataReader.Item("player") : btAvailRight18.Visible = True
                Case 19 : lblHomeAvailPlayer19.Visible = True : lblHomeAvailPlayer19.Text = myDataReader.Item("player") : btAvailRight19.Visible = True
                Case 20 : lblHomeAvailPlayer20.Visible = True : lblHomeAvailPlayer20.Text = myDataReader.Item("player") : btAvailRight20.Visible = True
                Case 21 : lblHomeAvailPlayer21.Visible = True : lblHomeAvailPlayer21.Text = myDataReader.Item("player") : btAvailRight21.Visible = True
                Case 22 : lblHomeAvailPlayer22.Visible = True : lblHomeAvailPlayer22.Text = myDataReader.Item("player") : btAvailRight22.Visible = True
                Case 23 : lblHomeAvailPlayer23.Visible = True : lblHomeAvailPlayer23.Text = myDataReader.Item("player") : btAvailRight23.Visible = True
                Case 24 : lblHomeAvailPlayer24.Visible = True : lblHomeAvailPlayer24.Text = myDataReader.Item("player") : btAvailRight24.Visible = True
                Case 25 : lblHomeAvailPlayer25.Visible = True : lblHomeAvailPlayer25.Text = myDataReader.Item("player") : btAvailRight25.Visible = True
                Case 26 : lblHomeAvailPlayer26.Visible = True : lblHomeAvailPlayer26.Text = myDataReader.Item("player") : btAvailRight26.Visible = True
                Case 27 : lblHomeAvailPlayer27.Visible = True : lblHomeAvailPlayer27.Text = myDataReader.Item("player") : btAvailRight27.Visible = True
                Case 28 : lblHomeAvailPlayer28.Visible = True : lblHomeAvailPlayer28.Text = myDataReader.Item("player") : btAvailRight28.Visible = True
                Case 29 : lblHomeAvailPlayer29.Visible = True : lblHomeAvailPlayer29.Text = myDataReader.Item("player") : btAvailRight29.Visible = True
                Case 30 : lblHomeAvailPlayer30.Visible = True : lblHomeAvailPlayer30.Text = myDataReader.Item("player") : btAvailRight30.Visible = True
                Case 31 : lblHomeAvailPlayer31.Visible = True : lblHomeAvailPlayer31.Text = myDataReader.Item("player") : btAvailRight31.Visible = True
                Case 32 : lblHomeAvailPlayer32.Visible = True : lblHomeAvailPlayer32.Text = myDataReader.Item("player") : btAvailRight32.Visible = True
                Case 33 : lblHomeAvailPlayer33.Visible = True : lblHomeAvailPlayer33.Text = myDataReader.Item("player") : btAvailRight33.Visible = True
                Case 34 : lblHomeAvailPlayer34.Visible = True : lblHomeAvailPlayer34.Text = myDataReader.Item("player") : btAvailRight34.Visible = True
                Case 35 : lblHomeAvailPlayer35.Visible = True : lblHomeAvailPlayer35.Text = myDataReader.Item("player") : btAvailRight35.Visible = True
                Case 36 : lblHomeAvailPlayer36.Visible = True : lblHomeAvailPlayer36.Text = myDataReader.Item("player") : btAvailRight36.Visible = True
                Case 37 : lblHomeAvailPlayer37.Visible = True : lblHomeAvailPlayer37.Text = myDataReader.Item("player") : btAvailRight37.Visible = True
                Case 38 : lblHomeAvailPlayer38.Visible = True : lblHomeAvailPlayer38.Text = myDataReader.Item("player") : btAvailRight38.Visible = True
                Case 39 : lblHomeAvailPlayer39.Visible = True : lblHomeAvailPlayer39.Text = myDataReader.Item("player") : btAvailRight39.Visible = True
            End Select
        End While
        'get the number of players selected and add blanks as available for them
        If SelectedHomePlayers <> "" Then
            strSQL = "SELECT COUNT(Player) FROM " & objGlobals.CurrentSchema & "vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblHomeTeam.Text & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            While myDataReader.Read()
                EmptyAvail = myDataReader.Item(0) - AvailCount
            End While
            For i = 1 To EmptyAvail
                AvailCount = AvailCount + 1
                Select Case AvailCount
                    Case 1 : lblHomeAvailPlayer1.Visible = True : lblHomeAvailPlayer1.Text = "."
                    Case 2 : lblHomeAvailPlayer2.Visible = True : lblHomeAvailPlayer2.Text = "."
                    Case 3 : lblHomeAvailPlayer3.Visible = True : lblHomeAvailPlayer3.Text = "."
                    Case 4 : lblHomeAvailPlayer4.Visible = True : lblHomeAvailPlayer4.Text = "."
                    Case 5 : lblHomeAvailPlayer5.Visible = True : lblHomeAvailPlayer5.Text = "."
                    Case 6 : lblHomeAvailPlayer6.Visible = True : lblHomeAvailPlayer6.Text = "."
                    Case 7 : lblHomeAvailPlayer7.Visible = True : lblHomeAvailPlayer7.Text = "."
                    Case 8 : lblHomeAvailPlayer8.Visible = True : lblHomeAvailPlayer8.Text = "."
                    Case 9 : lblHomeAvailPlayer9.Visible = True : lblHomeAvailPlayer9.Text = "."
                    Case 10 : lblHomeAvailPlayer10.Visible = True : lblHomeAvailPlayer10.Text = "."
                    Case 11 : lblHomeAvailPlayer11.Visible = True : lblHomeAvailPlayer11.Text = "."
                    Case 12 : lblHomeAvailPlayer12.Visible = True : lblHomeAvailPlayer12.Text = "."
                    Case 13 : lblHomeAvailPlayer13.Visible = True : lblHomeAvailPlayer13.Text = "."
                    Case 14 : lblHomeAvailPlayer14.Visible = True : lblHomeAvailPlayer14.Text = "."
                    Case 15 : lblHomeAvailPlayer15.Visible = True : lblHomeAvailPlayer15.Text = "."
                    Case 16 : lblHomeAvailPlayer16.Visible = True : lblHomeAvailPlayer16.Text = "."
                    Case 17 : lblHomeAvailPlayer17.Visible = True : lblHomeAvailPlayer17.Text = "."
                    Case 18 : lblHomeAvailPlayer18.Visible = True : lblHomeAvailPlayer18.Text = "."
                    Case 19 : lblHomeAvailPlayer19.Visible = True : lblHomeAvailPlayer19.Text = "."
                    Case 20 : lblHomeAvailPlayer20.Visible = True : lblHomeAvailPlayer20.Text = "."
                    Case 21 : lblHomeAvailPlayer21.Visible = True : lblHomeAvailPlayer21.Text = "."
                    Case 22 : lblHomeAvailPlayer22.Visible = True : lblHomeAvailPlayer22.Text = "."
                    Case 23 : lblHomeAvailPlayer23.Visible = True : lblHomeAvailPlayer23.Text = "."
                    Case 24 : lblHomeAvailPlayer24.Visible = True : lblHomeAvailPlayer24.Text = "."
                    Case 25 : lblHomeAvailPlayer25.Visible = True : lblHomeAvailPlayer25.Text = "."
                    Case 26 : lblHomeAvailPlayer26.Visible = True : lblHomeAvailPlayer26.Text = "."
                    Case 27 : lblHomeAvailPlayer27.Visible = True : lblHomeAvailPlayer27.Text = "."
                    Case 28 : lblHomeAvailPlayer28.Visible = True : lblHomeAvailPlayer28.Text = "."
                    Case 29 : lblHomeAvailPlayer29.Visible = True : lblHomeAvailPlayer29.Text = "."
                    Case 30 : lblHomeAvailPlayer30.Visible = True : lblHomeAvailPlayer30.Text = "."
                    Case 31 : lblHomeAvailPlayer31.Visible = True : lblHomeAvailPlayer31.Text = "."
                    Case 32 : lblHomeAvailPlayer32.Visible = True : lblHomeAvailPlayer32.Text = "."
                    Case 33 : lblHomeAvailPlayer33.Visible = True : lblHomeAvailPlayer33.Text = "."
                    Case 34 : lblHomeAvailPlayer34.Visible = True : lblHomeAvailPlayer34.Text = "."
                    Case 35 : lblHomeAvailPlayer35.Visible = True : lblHomeAvailPlayer35.Text = "."
                    Case 36 : lblHomeAvailPlayer36.Visible = True : lblHomeAvailPlayer36.Text = "."
                    Case 37 : lblHomeAvailPlayer37.Visible = True : lblHomeAvailPlayer37.Text = "."
                    Case 38 : lblHomeAvailPlayer38.Visible = True : lblHomeAvailPlayer38.Text = "."
                    Case 39 : lblHomeAvailPlayer39.Visible = True : lblHomeAvailPlayer39.Text = "."
                End Select
            Next
        End If

        AvailCount = 0
        If SelectedAwayPlayers = "" Then
            If objGlobals.LiveTestFlag = 3 Then
                strSQL = "SELECT player FROM " & objGlobals.CurrentSchema & "vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblAwayTeam.Text & "' ORDER BY Player"
            Else
                strSQL = "SELECT player FROM " & objGlobals.CurrentSchema & "vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblAwayTeam.Text & "' AND player NOT LIKE 'A N OTHER%' ORDER BY Player"
            End If
        Else
            strSQL = "SELECT player FROM " & objGlobals.CurrentSchema & "vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblAwayTeam.Text & "' AND player NOT IN (" & SelectedAwayPlayers & ") ORDER BY Player"
        End If
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            AvailCount = AvailCount + 1
            Select Case AvailCount
                Case 1 : lblAwayAvailPlayer1.Visible = True : lblAwayAvailPlayer1.Text = myDataReader.Item("player") : btAvailLeft1.Visible = True
                Case 2 : lblAwayAvailPlayer2.Visible = True : lblAwayAvailPlayer2.Text = myDataReader.Item("player") : btAvailLeft2.Visible = True
                Case 3 : lblAwayAvailPlayer3.Visible = True : lblAwayAvailPlayer3.Text = myDataReader.Item("player") : btAvailLeft3.Visible = True
                Case 4 : lblAwayAvailPlayer4.Visible = True : lblAwayAvailPlayer4.Text = myDataReader.Item("player") : btAvailLeft4.Visible = True
                Case 5 : lblAwayAvailPlayer5.Visible = True : lblAwayAvailPlayer5.Text = myDataReader.Item("player") : btAvailLeft5.Visible = True
                Case 6 : lblAwayAvailPlayer6.Visible = True : lblAwayAvailPlayer6.Text = myDataReader.Item("player") : btAvailLeft6.Visible = True
                Case 7 : lblAwayAvailPlayer7.Visible = True : lblAwayAvailPlayer7.Text = myDataReader.Item("player") : btAvailLeft7.Visible = True
                Case 8 : lblAwayAvailPlayer8.Visible = True : lblAwayAvailPlayer8.Text = myDataReader.Item("player") : btAvailLeft8.Visible = True
                Case 9 : lblAwayAvailPlayer9.Visible = True : lblAwayAvailPlayer9.Text = myDataReader.Item("player") : btAvailLeft9.Visible = True
                Case 10 : lblAwayAvailPlayer10.Visible = True : lblAwayAvailPlayer10.Text = myDataReader.Item("player") : btAvailLeft10.Visible = True
                Case 11 : lblAwayAvailPlayer11.Visible = True : lblAwayAvailPlayer11.Text = myDataReader.Item("player") : btAvailLeft11.Visible = True
                Case 12 : lblAwayAvailPlayer12.Visible = True : lblAwayAvailPlayer12.Text = myDataReader.Item("player") : btAvailLeft12.Visible = True
                Case 13 : lblAwayAvailPlayer13.Visible = True : lblAwayAvailPlayer13.Text = myDataReader.Item("player") : btAvailLeft13.Visible = True
                Case 14 : lblAwayAvailPlayer14.Visible = True : lblAwayAvailPlayer14.Text = myDataReader.Item("player") : btAvailLeft14.Visible = True
                Case 15 : lblAwayAvailPlayer15.Visible = True : lblAwayAvailPlayer15.Text = myDataReader.Item("player") : btAvailLeft15.Visible = True
                Case 16 : lblAwayAvailPlayer16.Visible = True : lblAwayAvailPlayer16.Text = myDataReader.Item("player") : btAvailLeft16.Visible = True
                Case 17 : lblAwayAvailPlayer17.Visible = True : lblAwayAvailPlayer17.Text = myDataReader.Item("player") : btAvailLeft17.Visible = True
                Case 18 : lblAwayAvailPlayer18.Visible = True : lblAwayAvailPlayer18.Text = myDataReader.Item("player") : btAvailLeft18.Visible = True
                Case 19 : lblAwayAvailPlayer19.Visible = True : lblAwayAvailPlayer19.Text = myDataReader.Item("player") : btAvailLeft19.Visible = True
                Case 20 : lblAwayAvailPlayer20.Visible = True : lblAwayAvailPlayer20.Text = myDataReader.Item("player") : btAvailLeft20.Visible = True
                Case 21 : lblAwayAvailPlayer21.Visible = True : lblAwayAvailPlayer21.Text = myDataReader.Item("player") : btAvailLeft21.Visible = True
                Case 22 : lblAwayAvailPlayer22.Visible = True : lblAwayAvailPlayer22.Text = myDataReader.Item("player") : btAvailLeft22.Visible = True
                Case 23 : lblAwayAvailPlayer23.Visible = True : lblAwayAvailPlayer23.Text = myDataReader.Item("player") : btAvailLeft23.Visible = True
                Case 24 : lblAwayAvailPlayer24.Visible = True : lblAwayAvailPlayer24.Text = myDataReader.Item("player") : btAvailLeft24.Visible = True
                Case 25 : lblAwayAvailPlayer25.Visible = True : lblAwayAvailPlayer25.Text = myDataReader.Item("player") : btAvailLeft25.Visible = True
                Case 26 : lblAwayAvailPlayer26.Visible = True : lblAwayAvailPlayer26.Text = myDataReader.Item("player") : btAvailLeft26.Visible = True
                Case 27 : lblAwayAvailPlayer27.Visible = True : lblAwayAvailPlayer27.Text = myDataReader.Item("player") : btAvailLeft27.Visible = True
                Case 28 : lblAwayAvailPlayer28.Visible = True : lblAwayAvailPlayer28.Text = myDataReader.Item("player") : btAvailLeft28.Visible = True
                Case 29 : lblAwayAvailPlayer29.Visible = True : lblAwayAvailPlayer29.Text = myDataReader.Item("player") : btAvailLeft29.Visible = True
                Case 30 : lblAwayAvailPlayer30.Visible = True : lblAwayAvailPlayer30.Text = myDataReader.Item("player") : btAvailLeft30.Visible = True
                Case 31 : lblAwayAvailPlayer31.Visible = True : lblAwayAvailPlayer31.Text = myDataReader.Item("player") : btAvailLeft31.Visible = True
                Case 32 : lblAwayAvailPlayer32.Visible = True : lblAwayAvailPlayer32.Text = myDataReader.Item("player") : btAvailLeft32.Visible = True
                Case 33 : lblAwayAvailPlayer33.Visible = True : lblAwayAvailPlayer33.Text = myDataReader.Item("player") : btAvailLeft33.Visible = True
                Case 34 : lblAwayAvailPlayer34.Visible = True : lblAwayAvailPlayer34.Text = myDataReader.Item("player") : btAvailLeft34.Visible = True
                Case 35 : lblAwayAvailPlayer35.Visible = True : lblAwayAvailPlayer35.Text = myDataReader.Item("player") : btAvailLeft35.Visible = True
                Case 36 : lblAwayAvailPlayer36.Visible = True : lblAwayAvailPlayer36.Text = myDataReader.Item("player") : btAvailLeft36.Visible = True
                Case 37 : lblAwayAvailPlayer37.Visible = True : lblAwayAvailPlayer37.Text = myDataReader.Item("player") : btAvailLeft37.Visible = True
                Case 38 : lblAwayAvailPlayer38.Visible = True : lblAwayAvailPlayer38.Text = myDataReader.Item("player") : btAvailLeft38.Visible = True
                Case 39 : lblAwayAvailPlayer39.Visible = True : lblAwayAvailPlayer39.Text = myDataReader.Item("player") : btAvailLeft39.Visible = True
            End Select
        End While
        'get the number of players selected and add blanks as available for them
        If SelectedAwayPlayers <> "" Then
            strSQL = "SELECT COUNT(Player) FROM " & objGlobals.CurrentSchema & "vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblAwayTeam.Text & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            While myDataReader.Read()
                EmptyAvail = myDataReader.Item(0) - AvailCount
            End While
            For i = 1 To EmptyAvail
                AvailCount = AvailCount + 1
                Select Case AvailCount
                    Case 1 : lblAwayAvailPlayer1.Visible = True : lblAwayAvailPlayer1.Text = "."
                    Case 2 : lblAwayAvailPlayer2.Visible = True : lblAwayAvailPlayer2.Text = "."
                    Case 3 : lblAwayAvailPlayer3.Visible = True : lblAwayAvailPlayer3.Text = "."
                    Case 4 : lblAwayAvailPlayer4.Visible = True : lblAwayAvailPlayer4.Text = "."
                    Case 5 : lblAwayAvailPlayer5.Visible = True : lblAwayAvailPlayer5.Text = "."
                    Case 6 : lblAwayAvailPlayer6.Visible = True : lblAwayAvailPlayer6.Text = "."
                    Case 7 : lblAwayAvailPlayer7.Visible = True : lblAwayAvailPlayer7.Text = "."
                    Case 8 : lblAwayAvailPlayer8.Visible = True : lblAwayAvailPlayer8.Text = "."
                    Case 9 : lblAwayAvailPlayer9.Visible = True : lblAwayAvailPlayer9.Text = "."
                    Case 10 : lblAwayAvailPlayer10.Visible = True : lblAwayAvailPlayer10.Text = "."
                    Case 11 : lblAwayAvailPlayer11.Visible = True : lblAwayAvailPlayer11.Text = "."
                    Case 12 : lblAwayAvailPlayer12.Visible = True : lblAwayAvailPlayer12.Text = "."
                    Case 13 : lblAwayAvailPlayer13.Visible = True : lblAwayAvailPlayer13.Text = "."
                    Case 14 : lblAwayAvailPlayer14.Visible = True : lblAwayAvailPlayer14.Text = "."
                    Case 15 : lblAwayAvailPlayer15.Visible = True : lblAwayAvailPlayer15.Text = "."
                    Case 16 : lblAwayAvailPlayer16.Visible = True : lblAwayAvailPlayer16.Text = "."
                    Case 17 : lblAwayAvailPlayer17.Visible = True : lblAwayAvailPlayer17.Text = "."
                    Case 18 : lblAwayAvailPlayer18.Visible = True : lblAwayAvailPlayer18.Text = "."
                    Case 19 : lblAwayAvailPlayer19.Visible = True : lblAwayAvailPlayer19.Text = "."
                    Case 20 : lblAwayAvailPlayer20.Visible = True : lblAwayAvailPlayer20.Text = "."
                    Case 21 : lblAwayAvailPlayer21.Visible = True : lblAwayAvailPlayer21.Text = "."
                    Case 22 : lblAwayAvailPlayer22.Visible = True : lblAwayAvailPlayer22.Text = "."
                    Case 23 : lblAwayAvailPlayer23.Visible = True : lblAwayAvailPlayer23.Text = "."
                    Case 24 : lblAwayAvailPlayer24.Visible = True : lblAwayAvailPlayer24.Text = "."
                    Case 25 : lblAwayAvailPlayer25.Visible = True : lblAwayAvailPlayer25.Text = "."
                    Case 26 : lblAwayAvailPlayer26.Visible = True : lblAwayAvailPlayer26.Text = "."
                    Case 27 : lblAwayAvailPlayer27.Visible = True : lblAwayAvailPlayer27.Text = "."
                    Case 28 : lblAwayAvailPlayer28.Visible = True : lblAwayAvailPlayer28.Text = "."
                    Case 29 : lblAwayAvailPlayer29.Visible = True : lblAwayAvailPlayer29.Text = "."
                    Case 30 : lblAwayAvailPlayer30.Visible = True : lblAwayAvailPlayer30.Text = "."
                    Case 31 : lblAwayAvailPlayer31.Visible = True : lblAwayAvailPlayer31.Text = "."
                    Case 32 : lblAwayAvailPlayer32.Visible = True : lblAwayAvailPlayer32.Text = "."
                    Case 33 : lblAwayAvailPlayer33.Visible = True : lblAwayAvailPlayer33.Text = "."
                    Case 34 : lblAwayAvailPlayer34.Visible = True : lblAwayAvailPlayer34.Text = "."
                    Case 35 : lblAwayAvailPlayer35.Visible = True : lblAwayAvailPlayer35.Text = "."
                    Case 36 : lblAwayAvailPlayer36.Visible = True : lblAwayAvailPlayer36.Text = "."
                    Case 37 : lblAwayAvailPlayer37.Visible = True : lblAwayAvailPlayer37.Text = "."
                    Case 38 : lblAwayAvailPlayer38.Visible = True : lblAwayAvailPlayer38.Text = "."
                    Case 39 : lblAwayAvailPlayer39.Visible = True : lblAwayAvailPlayer39.Text = "."
                End Select
            Next
        End If
    End Sub

    Sub load_details()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader    'MySqlDataReader
        Dim MatchNo As Integer = 0
        SelectedHomePlayers = ""
        SelectedAwayPlayers = ""
        strSQL = "SELECT * FROM " & objGlobals.CurrentSchema & "vw_fixtures_detail WHERE fixture_id = " & fixture_id & " ORDER BY match"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            MatchNo = MatchNo + 1
            Select Case MatchNo
                Case 1
                    lblHomePlayer1.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & "'" & myDataReader.Item("home_player") & "'"
                    lblHomePoints1.Text = myDataReader.Item("home_points")
                    lblAwayPlayer1.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & "'" & myDataReader.Item("away_player") & "'"
                    lblAwayPoints1.Text = myDataReader.Item("away_points")
                    lblHomeRoll1_1.Text = myDataReader.Item("home_roll_1")
                    lblHomeRoll1_2.Text = myDataReader.Item("home_roll_2")
                    lblHomeRoll1_3.Text = myDataReader.Item("home_roll_3")
                    lblHomeRoll1_4.Text = myDataReader.Item("home_roll_4")
                    lblHomeRoll1_5.Text = myDataReader.Item("home_roll_5")
                    lblAwayRoll1_1.Text = myDataReader.Item("away_roll_1")
                    lblAwayRoll1_2.Text = myDataReader.Item("away_roll_2")
                    lblAwayRoll1_3.Text = myDataReader.Item("away_roll_3")
                    lblAwayRoll1_4.Text = myDataReader.Item("away_roll_4")
                    lblAwayRoll1_5.Text = myDataReader.Item("away_roll_5")
                Case 2
                    lblHomePlayer2.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    lblHomePoints2.Text = myDataReader.Item("home_points")
                    lblAwayPlayer2.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    lblAwayPoints2.Text = myDataReader.Item("away_points")
                    lblHomeRoll2_1.Text = myDataReader.Item("home_roll_1")
                    lblHomeRoll2_2.Text = myDataReader.Item("home_roll_2")
                    lblHomeRoll2_3.Text = myDataReader.Item("home_roll_3")
                    lblHomeRoll2_4.Text = myDataReader.Item("home_roll_4")
                    lblHomeRoll2_5.Text = myDataReader.Item("home_roll_5")
                    lblAwayRoll2_1.Text = myDataReader.Item("away_roll_1")
                    lblAwayRoll2_2.Text = myDataReader.Item("away_roll_2")
                    lblAwayRoll2_3.Text = myDataReader.Item("away_roll_3")
                    lblAwayRoll2_4.Text = myDataReader.Item("away_roll_4")
                    lblAwayRoll2_5.Text = myDataReader.Item("away_roll_5")
                Case 3
                    lblHomePlayer3.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    lblHomePoints3.Text = myDataReader.Item("home_points")
                    lblAwayPlayer3.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    lblAwayPoints3.Text = myDataReader.Item("away_points")
                    lblHomeRoll3_1.Text = myDataReader.Item("home_roll_1")
                    lblHomeRoll3_2.Text = myDataReader.Item("home_roll_2")
                    lblHomeRoll3_3.Text = myDataReader.Item("home_roll_3")
                    lblHomeRoll3_4.Text = myDataReader.Item("home_roll_4")
                    lblHomeRoll3_5.Text = myDataReader.Item("home_roll_5")
                    lblAwayRoll3_1.Text = myDataReader.Item("away_roll_1")
                    lblAwayRoll3_2.Text = myDataReader.Item("away_roll_2")
                    lblAwayRoll3_3.Text = myDataReader.Item("away_roll_3")
                    lblAwayRoll3_4.Text = myDataReader.Item("away_roll_4")
                    lblAwayRoll3_5.Text = myDataReader.Item("away_roll_5")
                Case 4
                    lblHomePlayer4.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    lblHomePoints4.Text = myDataReader.Item("home_points")
                    lblAwayPlayer4.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    lblAwayPoints4.Text = myDataReader.Item("away_points")
                    lblHomeRoll4_1.Text = myDataReader.Item("home_roll_1")
                    lblHomeRoll4_2.Text = myDataReader.Item("home_roll_2")
                    lblHomeRoll4_3.Text = myDataReader.Item("home_roll_3")
                    lblHomeRoll4_4.Text = myDataReader.Item("home_roll_4")
                    lblHomeRoll4_5.Text = myDataReader.Item("home_roll_5")
                    lblAwayRoll4_1.Text = myDataReader.Item("away_roll_1")
                    lblAwayRoll4_2.Text = myDataReader.Item("away_roll_2")
                    lblAwayRoll4_3.Text = myDataReader.Item("away_roll_3")
                    lblAwayRoll4_4.Text = myDataReader.Item("away_roll_4")
                    lblAwayRoll4_5.Text = myDataReader.Item("away_roll_5")
                Case 5
                    lblHomePlayer5.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    lblHomePoints5.Text = myDataReader.Item("home_points")
                    lblAwayPlayer5.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    lblAwayPoints5.Text = myDataReader.Item("away_points")
                    lblHomeRoll5_1.Text = myDataReader.Item("home_roll_1")
                    lblHomeRoll5_2.Text = myDataReader.Item("home_roll_2")
                    lblHomeRoll5_3.Text = myDataReader.Item("home_roll_3")
                    lblHomeRoll5_4.Text = myDataReader.Item("home_roll_4")
                    lblHomeRoll5_5.Text = myDataReader.Item("home_roll_5")
                    lblAwayRoll5_1.Text = myDataReader.Item("away_roll_1")
                    lblAwayRoll5_2.Text = myDataReader.Item("away_roll_2")
                    lblAwayRoll5_3.Text = myDataReader.Item("away_roll_3")
                    lblAwayRoll5_4.Text = myDataReader.Item("away_roll_4")
                    lblAwayRoll5_5.Text = myDataReader.Item("away_roll_5")
                Case 6
                    lblHomePlayer6.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    lblHomePoints6.Text = myDataReader.Item("home_points")
                    lblAwayPlayer6.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    lblAwayPoints6.Text = myDataReader.Item("away_points")
                    lblHomeRoll6_1.Text = myDataReader.Item("home_roll_1")
                    lblHomeRoll6_2.Text = myDataReader.Item("home_roll_2")
                    lblHomeRoll6_3.Text = myDataReader.Item("home_roll_3")
                    lblHomeRoll6_4.Text = myDataReader.Item("home_roll_4")
                    lblHomeRoll6_5.Text = myDataReader.Item("home_roll_5")
                    lblAwayRoll6_1.Text = myDataReader.Item("away_roll_1")
                    lblAwayRoll6_2.Text = myDataReader.Item("away_roll_2")
                    lblAwayRoll6_3.Text = myDataReader.Item("away_roll_3")
                    lblAwayRoll6_4.Text = myDataReader.Item("away_roll_4")
                    lblAwayRoll6_5.Text = myDataReader.Item("away_roll_5")
                Case 7
                    lblHomePlayer7.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    lblHomePoints7.Text = myDataReader.Item("home_points")
                    lblAwayPlayer7.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    lblAwayPoints7.Text = myDataReader.Item("away_points")
                    lblHomeRoll7_1.Text = myDataReader.Item("home_roll_1")
                    lblHomeRoll7_2.Text = myDataReader.Item("home_roll_2")
                    lblHomeRoll7_3.Text = myDataReader.Item("home_roll_3")
                    lblHomeRoll7_4.Text = myDataReader.Item("home_roll_4")
                    lblHomeRoll7_5.Text = myDataReader.Item("home_roll_5")
                    lblAwayRoll7_1.Text = myDataReader.Item("away_roll_1")
                    lblAwayRoll7_2.Text = myDataReader.Item("away_roll_2")
                    lblAwayRoll7_3.Text = myDataReader.Item("away_roll_3")
                    lblAwayRoll7_4.Text = myDataReader.Item("away_roll_4")
                    lblAwayRoll7_5.Text = myDataReader.Item("away_roll_5")
                Case 8
                    lblHomePlayer8.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    lblHomePoints8.Text = myDataReader.Item("home_points")
                    lblAwayPlayer8.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    lblAwayPoints8.Text = myDataReader.Item("away_points")
                    lblHomeRoll8_1.Text = myDataReader.Item("home_roll_1")
                    lblHomeRoll8_2.Text = myDataReader.Item("home_roll_2")
                    lblHomeRoll8_3.Text = myDataReader.Item("home_roll_3")
                    lblHomeRoll8_4.Text = myDataReader.Item("home_roll_4")
                    lblHomeRoll8_5.Text = myDataReader.Item("home_roll_5")
                    lblAwayRoll8_1.Text = myDataReader.Item("away_roll_1")
                    lblAwayRoll8_2.Text = myDataReader.Item("away_roll_2")
                    lblAwayRoll8_3.Text = myDataReader.Item("away_roll_3")
                    lblAwayRoll8_4.Text = myDataReader.Item("away_roll_4")
                    lblAwayRoll8_5.Text = myDataReader.Item("away_roll_5")
                Case 9
                    lblHomePlayer9.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    lblHomePoints9.Text = myDataReader.Item("home_points")
                    lblAwayPlayer9.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    lblAwayPoints9.Text = myDataReader.Item("away_points")
                    lblHomeRoll9_1.Text = myDataReader.Item("home_roll_1")
                    lblHomeRoll9_2.Text = myDataReader.Item("home_roll_2")
                    lblHomeRoll9_3.Text = myDataReader.Item("home_roll_3")
                    lblHomeRoll9_4.Text = myDataReader.Item("home_roll_4")
                    lblHomeRoll9_5.Text = myDataReader.Item("home_roll_5")
                    lblAwayRoll9_1.Text = myDataReader.Item("away_roll_1")
                    lblAwayRoll9_2.Text = myDataReader.Item("away_roll_2")
                    lblAwayRoll9_3.Text = myDataReader.Item("away_roll_3")
                    lblAwayRoll9_4.Text = myDataReader.Item("away_roll_4")
                    lblAwayRoll9_5.Text = myDataReader.Item("away_roll_5")
                Case 10
                    lblHomePlayer10.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    lblHomePoints10.Text = myDataReader.Item("home_points")
                    lblAwayPlayer10.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    lblAwayPoints10.Text = myDataReader.Item("away_points")
                    lblHomeRoll10_1.Text = myDataReader.Item("home_roll_1")
                    lblHomeRoll10_2.Text = myDataReader.Item("home_roll_2")
                    lblHomeRoll10_3.Text = myDataReader.Item("home_roll_3")
                    lblHomeRoll10_4.Text = myDataReader.Item("home_roll_4")
                    lblHomeRoll10_5.Text = myDataReader.Item("home_roll_5")
                    lblAwayRoll10_1.Text = myDataReader.Item("away_roll_1")
                    lblAwayRoll10_2.Text = myDataReader.Item("away_roll_2")
                    lblAwayRoll10_3.Text = myDataReader.Item("away_roll_3")
                    lblAwayRoll10_4.Text = myDataReader.Item("away_roll_4")
                    lblAwayRoll10_5.Text = myDataReader.Item("away_roll_5")
                Case 11
                    lblHomePlayer11.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    lblHomePoints11.Text = myDataReader.Item("home_points")
                    lblAwayPlayer11.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    lblAwayPoints11.Text = myDataReader.Item("away_points")
                    lblHomeRoll11_1.Text = myDataReader.Item("home_roll_1")
                    lblHomeRoll11_2.Text = myDataReader.Item("home_roll_2")
                    lblHomeRoll11_3.Text = myDataReader.Item("home_roll_3")
                    lblHomeRoll11_4.Text = myDataReader.Item("home_roll_4")
                    lblHomeRoll11_5.Text = myDataReader.Item("home_roll_5")
                    lblAwayRoll11_1.Text = myDataReader.Item("away_roll_1")
                    lblAwayRoll11_2.Text = myDataReader.Item("away_roll_2")
                    lblAwayRoll11_3.Text = myDataReader.Item("away_roll_3")
                    lblAwayRoll11_4.Text = myDataReader.Item("away_roll_4")
                    lblAwayRoll11_5.Text = myDataReader.Item("away_roll_5")
                Case 12
                    lblHomePlayer12.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    lblHomePoints12.Text = myDataReader.Item("home_points")
                    lblAwayPlayer12.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    lblAwayPoints12.Text = myDataReader.Item("away_points")
                    lblHomeRoll12_1.Text = myDataReader.Item("home_roll_1")
                    lblHomeRoll12_2.Text = myDataReader.Item("home_roll_2")
                    lblHomeRoll12_3.Text = myDataReader.Item("home_roll_3")
                    lblHomeRoll12_4.Text = myDataReader.Item("home_roll_4")
                    lblHomeRoll12_5.Text = myDataReader.Item("home_roll_5")
                    lblAwayRoll12_1.Text = myDataReader.Item("away_roll_1")
                    lblAwayRoll12_2.Text = myDataReader.Item("away_roll_2")
                    lblAwayRoll12_3.Text = myDataReader.Item("away_roll_3")
                    lblAwayRoll12_4.Text = myDataReader.Item("away_roll_4")
                    lblAwayRoll12_5.Text = myDataReader.Item("away_roll_5")
            End Select
        End While
        'no matches ?
        If MatchNo = 0 Then
            btLeft1.Visible = False : btLeft2.Visible = False : btLeft3.Visible = False : btLeft4.Visible = False : btLeft5.Visible = False : btLeft6.Visible = False : btLeft7.Visible = False : btLeft8.Visible = False : btLeft9.Visible = False : btLeft10.Visible = False : btLeft11.Visible = False : btLeft12.Visible = False
            btRight1.Visible = False : btRight2.Visible = False : btRight3.Visible = False : btRight4.Visible = False : btRight5.Visible = False : btRight6.Visible = False : btRight7.Visible = False : btRight8.Visible = False : btRight9.Visible = False : btRight10.Visible = False : btRight11.Visible = False : btRight12.Visible = False
        End If
    End Sub

    Sub load_result_deductions()
        Dim home_result As String
        home_result = lblResult.Text
        With rbResults
            .ClearSelection()
            .Items.Add("0 - 0")
            .Items.Add("2 - 0") : If home_result = "2 - 0" Then .SelectedIndex = 1
            .Items.Add("1 - 1") : If home_result = "1 - 1" Then .SelectedIndex = 2
            .Items.Add("0 - 2") : If home_result = "0 - 2" Then .SelectedIndex = 3
        End With
        With ddResult
            .ClearSelection()
            .Items.Add("0 - 0")
            .Items.Add("2 - 0") : If home_result = "2 - 0" Then .SelectedIndex = 1
            .Items.Add("1 - 1") : If home_result = "1 - 1" Then .SelectedIndex = 2
            .Items.Add("0 - 2") : If home_result = "0 - 2" Then .SelectedIndex = 3
            .Visible = True
        End With
        lblResults.Visible = True
        lblRolls.Visible = True
        Dim i As Single
        For i = 0 To 7 Step 0.5
            ddHomePointsDeducted.Items.Add(i)
            ddAwayPointsDeducted.Items.Add(i)
        Next

        For i = 0 To ddHomePointsDeducted.Items.Count - 1
            If ddHomePointsDeducted.Items(i).Value = HomePointsDeducted Then ddHomePointsDeducted.SelectedIndex = i
        Next
        For i = 0 To ddAwayPointsDeducted.Items.Count - 1
            If ddAwayPointsDeducted.Items(i).Value = AwayPointsDeducted Then ddAwayPointsDeducted.SelectedIndex = i
        Next
    End Sub

    Private Sub load_rolls()
        With ddHomeRolls
            .ClearSelection()
            .Items.Add("0 - 0")
            .Items.Add("5 - 0")
            .Items.Add("4½ - ½")
            .Items.Add("4 - 1")
            .Items.Add("3½ - 1½")
            .Items.Add("3 - 2")
            .Items.Add("2½ - 2½")
            .Items.Add("2 - 3")
            .Items.Add("1½ - 3½")
            .Items.Add("1 - 4")
            .Items.Add("½ - 4½")
            .Items.Add("0 - 5")
            .Visible = True
        End With
        With ddAwayRolls
            .ClearSelection()
            .Items.Add("0 - 0")
            .Items.Add("0 - 5")
            .Items.Add("½ - 4½")
            .Items.Add("1 - 4")
            .Items.Add("1½ - 3½")
            .Items.Add("2 - 3")
            .Items.Add("2½ - 2½")
            .Items.Add("3 - 2")
            .Items.Add("3½ - 1½")
            .Items.Add("4 - 1")
            .Items.Add("4½ - ½")
            .Items.Add("5 - 0")
            .Visible = False
        End With
        lblRolls.Visible = True
        lblHomeRollTotal1.Text = HomeRollTotal1
        lblHomeRollTotal2.Text = HomeRollTotal2
        lblHomeRollTotal3.Text = HomeRollTotal3
        lblHomeRollTotal4.Text = HomeRollTotal4
        lblHomeRollTotal5.Text = HomeRollTotal5
        lblAwayRollTotal1.Text = AwayRollTotal1
        lblAwayRollTotal2.Text = AwayRollTotal2
        lblAwayRollTotal3.Text = AwayRollTotal3
        lblAwayRollTotal4.Text = AwayRollTotal4
        lblAwayRollTotal5.Text = AwayRollTotal5
        lblHomePoints0.Text = HomeRollsTotal
        lblAwayPoints0.Text = AwayRollsTotal

        If HomeRollsTotal = 0 And AwayRollsTotal = 0 Then
            ddHomeRolls.SelectedIndex = 0
        Else
            Select Case HomeRollsWon
                Case 5 : ddHomeRolls.SelectedIndex = 1
                Case 4.5 : ddHomeRolls.SelectedIndex = 2
                Case 4 : ddHomeRolls.SelectedIndex = 3
                Case 3.5 : ddHomeRolls.SelectedIndex = 4
                Case 3 : ddHomeRolls.SelectedIndex = 5
                Case 2.5 : ddHomeRolls.SelectedIndex = 6
                Case 2 : ddHomeRolls.SelectedIndex = 7
                Case 1.5 : ddHomeRolls.SelectedIndex = 8
                Case 1 : ddHomeRolls.SelectedIndex = 9
                Case 0.5 : ddHomeRolls.SelectedIndex = 10
                Case 0 : ddHomeRolls.SelectedIndex = 11
            End Select
        End If
        ddAwayRolls.SelectedIndex = ddHomeRolls.SelectedIndex

        Call colour_totals()
    End Sub

    Protected Sub btLeft1_Click(sender As Object, e As System.EventArgs) Handles btLeft1.Click
        Call find_home_avail(lblHomePlayer1, btLeft1)
    End Sub

    Protected Sub btLeft2_Click(sender As Object, e As System.EventArgs) Handles btLeft2.Click
        Call find_home_avail(lblHomePlayer2, btLeft2)
    End Sub

    Protected Sub btLeft3_Click(sender As Object, e As System.EventArgs) Handles btLeft3.Click
        Call find_home_avail(lblHomePlayer3, btLeft3)
    End Sub

    Protected Sub btLeft4_Click(sender As Object, e As System.EventArgs) Handles btLeft4.Click
        Call find_home_avail(lblHomePlayer4, btLeft4)
    End Sub

    Protected Sub btLeft5_Click(sender As Object, e As System.EventArgs) Handles btLeft5.Click
        Call find_home_avail(lblHomePlayer5, btLeft5)
    End Sub

    Protected Sub btLeft6_Click(sender As Object, e As System.EventArgs) Handles btLeft6.Click
        Call find_home_avail(lblHomePlayer6, btLeft6)
    End Sub

    Protected Sub btLeft7_Click(sender As Object, e As System.EventArgs) Handles btLeft7.Click
        Call find_home_avail(lblHomePlayer7, btLeft7)
    End Sub

    Protected Sub btLeft8_Click(sender As Object, e As System.EventArgs) Handles btLeft8.Click
        Call find_home_avail(lblHomePlayer8, btLeft8)
    End Sub

    Protected Sub btLeft9_Click(sender As Object, e As System.EventArgs) Handles btLeft9.Click
        Call find_home_avail(lblHomePlayer9, btLeft9)
    End Sub

    Protected Sub btLeft10_Click(sender As Object, e As System.EventArgs) Handles btLeft10.Click
        Call find_home_avail(lblHomePlayer10, btLeft10)
    End Sub

    Protected Sub btLeft11_Click(sender As Object, e As System.EventArgs) Handles btLeft11.Click
        Call find_home_avail(lblHomePlayer11, btLeft11)
    End Sub

    Protected Sub btLeft12_Click(sender As Object, e As System.EventArgs) Handles btLeft12.Click
        Call find_home_avail(lblHomePlayer12, btLeft12)
    End Sub



    Protected Sub btRight1_Click(sender As Object, e As System.EventArgs) Handles btRight1.Click
        Call find_away_avail(lblAwayPlayer1, btRight1)
    End Sub

    Protected Sub btRight2_Click(sender As Object, e As System.EventArgs) Handles btRight2.Click
        Call find_away_avail(lblAwayPlayer2, btRight2)
    End Sub

    Protected Sub btRight3_Click(sender As Object, e As System.EventArgs) Handles btRight3.Click
        Call find_away_avail(lblAwayPlayer3, btRight3)
    End Sub

    Protected Sub btRight4_Click(sender As Object, e As System.EventArgs) Handles btRight4.Click
        Call find_away_avail(lblAwayPlayer4, btRight4)
    End Sub

    Protected Sub btRight5_Click(sender As Object, e As System.EventArgs) Handles btRight5.Click
        Call find_away_avail(lblAwayPlayer5, btRight5)
    End Sub

    Protected Sub btRight6_Click(sender As Object, e As System.EventArgs) Handles btRight6.Click
        Call find_away_avail(lblAwayPlayer6, btRight6)
    End Sub

    Protected Sub btRight7_Click(sender As Object, e As System.EventArgs) Handles btRight7.Click
        Call find_away_avail(lblAwayPlayer7, btRight7)
    End Sub

    Protected Sub btRight8_Click(sender As Object, e As System.EventArgs) Handles btRight8.Click
        Call find_away_avail(lblAwayPlayer8, btRight8)
    End Sub

    Protected Sub btRight9_Click(sender As Object, e As System.EventArgs) Handles btRight9.Click
        Call find_away_avail(lblAwayPlayer9, btRight9)
    End Sub

    Protected Sub btRight10_Click(sender As Object, e As System.EventArgs) Handles btRight10.Click
        Call find_away_avail(lblAwayPlayer10, btRight10)
    End Sub

    Protected Sub btRight11_Click(sender As Object, e As System.EventArgs) Handles btRight11.Click
        Call find_away_avail(lblAwayPlayer11, btRight11)
    End Sub

    Protected Sub btRight12_Click(sender As Object, e As System.EventArgs) Handles btRight12.Click
        Call find_away_avail(lblAwayPlayer12, btRight12)
    End Sub

    Sub find_home_avail(inLabel As Label, inButton As Button)
        If lblHomeAvailPlayer1.Text = "." Then
            lblHomeAvailPlayer1.ForeColor = White
            btAvailRight1.Visible = True
            lblHomeAvailPlayer1.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer2.Text = "." Then
            btAvailRight2.Visible = True
            lblHomeAvailPlayer2.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer3.Text = "." Then
            btAvailRight3.Visible = True
            lblHomeAvailPlayer3.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer4.Text = "." Then
            btAvailRight4.Visible = True
            lblHomeAvailPlayer4.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer5.Text = "." Then
            btAvailRight5.Visible = True
            lblHomeAvailPlayer5.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer6.Text = "." Then
            btAvailRight6.Visible = True
            lblHomeAvailPlayer6.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer7.Text = "." Then
            btAvailRight7.Visible = True
            lblHomeAvailPlayer7.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer8.Text = "." Then
            btAvailRight8.Visible = True
            lblHomeAvailPlayer8.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer9.Text = "." Then
            btAvailRight9.Visible = True
            lblHomeAvailPlayer9.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer10.Text = "." Then
            btAvailRight10.Visible = True
            lblHomeAvailPlayer10.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer11.Text = "." Then
            btAvailRight11.Visible = True
            lblHomeAvailPlayer11.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer12.Text = "." Then
            btAvailRight12.Visible = True
            lblHomeAvailPlayer12.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer13.Text = "." Then
            btAvailRight13.Visible = True
            lblHomeAvailPlayer13.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer14.Text = "." Then
            btAvailRight14.Visible = True
            lblHomeAvailPlayer14.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer15.Text = "." Then
            btAvailRight15.Visible = True
            lblHomeAvailPlayer15.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer16.Text = "." Then
            btAvailRight16.Visible = True
            lblHomeAvailPlayer16.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer17.Text = "." Then
            btAvailRight17.Visible = True
            lblHomeAvailPlayer17.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer18.Text = "." Then
            btAvailRight18.Visible = True
            lblHomeAvailPlayer18.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer19.Text = "." Then
            btAvailRight19.Visible = True
            lblHomeAvailPlayer19.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer20.Text = "." Then
            btAvailRight20.Visible = True
            lblHomeAvailPlayer20.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer21.Text = "." Then
            btAvailRight21.Visible = True
            lblHomeAvailPlayer21.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer22.Text = "." Then
            btAvailRight22.Visible = True
            lblHomeAvailPlayer22.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer23.Text = "." Then
            btAvailRight23.Visible = True
            lblHomeAvailPlayer23.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer24.Text = "." Then
            btAvailRight24.Visible = True
            lblHomeAvailPlayer24.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer25.Text = "." Then
            btAvailRight25.Visible = True
            lblHomeAvailPlayer25.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer26.Text = "." Then
            btAvailRight26.Visible = True
            lblHomeAvailPlayer26.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer27.Text = "." Then
            btAvailRight27.Visible = True
            lblHomeAvailPlayer27.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer28.Text = "." Then
            btAvailRight28.Visible = True
            lblHomeAvailPlayer28.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer29.Text = "." Then
            btAvailRight29.Visible = True
            lblHomeAvailPlayer29.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer30.Text = "." Then
            btAvailRight30.Visible = True
            lblHomeAvailPlayer30.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer31.Text = "." Then
            btAvailRight31.Visible = True
            lblHomeAvailPlayer31.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer32.Text = "." Then
            btAvailRight32.Visible = True
            lblHomeAvailPlayer32.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer33.Text = "." Then
            btAvailRight33.Visible = True
            lblHomeAvailPlayer33.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer34.Text = "." Then
            btAvailRight34.Visible = True
            lblHomeAvailPlayer34.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer35.Text = "." Then
            btAvailRight35.Visible = True
            lblHomeAvailPlayer35.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer36.Text = "." Then
            btAvailRight36.Visible = True
            lblHomeAvailPlayer36.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer37.Text = "." Then
            btAvailRight37.Visible = True
            lblHomeAvailPlayer37.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer38.Text = "." Then
            btAvailRight38.Visible = True
            lblHomeAvailPlayer38.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblHomeAvailPlayer39.Text = "." Then
            btAvailRight39.Visible = True
            lblHomeAvailPlayer39.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If

    End Sub

    Sub find_away_avail(inLabel As Label, inButton As Button)
        If lblAwayAvailPlayer1.Text = "." Then
            btAvailLeft1.Visible = True
            lblAwayAvailPlayer1.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer2.Text = "." Then
            btAvailLeft2.Visible = True
            lblAwayAvailPlayer2.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer3.Text = "." Then
            btAvailLeft3.Visible = True
            lblAwayAvailPlayer3.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer4.Text = "." Then
            btAvailLeft4.Visible = True
            lblAwayAvailPlayer4.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer5.Text = "." Then
            btAvailLeft5.Visible = True
            lblAwayAvailPlayer5.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer6.Text = "." Then
            btAvailLeft6.Visible = True
            lblAwayAvailPlayer6.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer7.Text = "." Then
            btAvailLeft7.Visible = True
            lblAwayAvailPlayer7.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer8.Text = "." Then
            btAvailLeft8.Visible = True
            lblAwayAvailPlayer8.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer9.Text = "." Then
            btAvailLeft9.Visible = True
            lblAwayAvailPlayer9.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer10.Text = "." Then
            btAvailLeft10.Visible = True
            lblAwayAvailPlayer10.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer11.Text = "." Then
            btAvailLeft11.Visible = True
            lblAwayAvailPlayer11.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer12.Text = "." Then
            btAvailLeft12.Visible = True
            lblAwayAvailPlayer12.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer13.Text = "." Then
            btAvailLeft13.Visible = True
            lblAwayAvailPlayer13.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer14.Text = "." Then
            btAvailLeft14.Visible = True
            lblAwayAvailPlayer14.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer15.Text = "." Then
            btAvailLeft15.Visible = True
            lblAwayAvailPlayer15.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer16.Text = "." Then
            btAvailLeft16.Visible = True
            lblAwayAvailPlayer16.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer17.Text = "." Then
            btAvailLeft17.Visible = True
            lblAwayAvailPlayer17.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer18.Text = "." Then
            btAvailLeft18.Visible = True
            lblAwayAvailPlayer18.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer19.Text = "." Then
            btAvailLeft19.Visible = True
            lblAwayAvailPlayer19.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer20.Text = "." Then
            btAvailLeft20.Visible = True
            lblAwayAvailPlayer20.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer21.Text = "." Then
            btAvailLeft21.Visible = True
            lblAwayAvailPlayer21.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer22.Text = "." Then
            btAvailLeft22.Visible = True
            lblAwayAvailPlayer22.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer23.Text = "." Then
            btAvailLeft23.Visible = True
            lblAwayAvailPlayer23.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer24.Text = "." Then
            btAvailLeft24.Visible = True
            lblAwayAvailPlayer24.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer25.Text = "." Then
            btAvailLeft25.Visible = True
            lblAwayAvailPlayer25.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer26.Text = "." Then
            btAvailLeft26.Visible = True
            lblAwayAvailPlayer26.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer27.Text = "." Then
            btAvailLeft27.Visible = True
            lblAwayAvailPlayer27.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer28.Text = "." Then
            btAvailLeft28.Visible = True
            lblAwayAvailPlayer28.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer29.Text = "." Then
            btAvailLeft29.Visible = True
            lblAwayAvailPlayer29.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer30.Text = "." Then
            btAvailLeft30.Visible = True
            lblAwayAvailPlayer30.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer31.Text = "." Then
            btAvailLeft31.Visible = True
            lblAwayAvailPlayer31.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer32.Text = "." Then
            btAvailLeft32.Visible = True
            lblAwayAvailPlayer32.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer33.Text = "." Then
            btAvailLeft33.Visible = True
            lblAwayAvailPlayer33.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer34.Text = "." Then
            btAvailLeft34.Visible = True
            lblAwayAvailPlayer34.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer35.Text = "." Then
            btAvailLeft35.Visible = True
            lblAwayAvailPlayer35.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer36.Text = "." Then
            btAvailLeft36.Visible = True
            lblAwayAvailPlayer36.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer37.Text = "." Then
            btAvailLeft37.Visible = True
            lblAwayAvailPlayer37.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer38.Text = "." Then
            btAvailLeft38.Visible = True
            lblAwayAvailPlayer38.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If
        If lblAwayAvailPlayer39.Text = "." Then
            btAvailLeft39.Visible = True
            lblAwayAvailPlayer39.Text = inLabel.Text
            inLabel.Text = "."
            inButton.Visible = False
            Exit Sub
        End If

    End Sub

    Protected Sub btAvailRight1_Click(sender As Object, e As System.EventArgs) Handles btAvailRight1.Click
        Call find_home_selected(lblHomeAvailPlayer1, btAvailRight1)
    End Sub
    Protected Sub btAvailRight2_Click(sender As Object, e As System.EventArgs) Handles btAvailRight2.Click
        Call find_home_selected(lblHomeAvailPlayer2, btAvailRight2)
    End Sub
    Protected Sub btAvailRight3_Click(sender As Object, e As System.EventArgs) Handles btAvailRight3.Click
        Call find_home_selected(lblHomeAvailPlayer3, btAvailRight3)
    End Sub
    Protected Sub btAvailRight4_Click(sender As Object, e As System.EventArgs) Handles btAvailRight4.Click
        Call find_home_selected(lblHomeAvailPlayer4, btAvailRight4)
    End Sub
    Protected Sub btAvailRight5_Click(sender As Object, e As System.EventArgs) Handles btAvailRight5.Click
        Call find_home_selected(lblHomeAvailPlayer5, btAvailRight5)
    End Sub
    Protected Sub btAvailRight6_Click(sender As Object, e As System.EventArgs) Handles btAvailRight6.Click
        Call find_home_selected(lblHomeAvailPlayer6, btAvailRight6)
    End Sub
    Protected Sub btAvailRight7_Click(sender As Object, e As System.EventArgs) Handles btAvailRight7.Click
        Call find_home_selected(lblHomeAvailPlayer7, btAvailRight7)
    End Sub
    Protected Sub btAvailRight8_Click(sender As Object, e As System.EventArgs) Handles btAvailRight8.Click
        Call find_home_selected(lblHomeAvailPlayer8, btAvailRight8)
    End Sub
    Protected Sub btAvailRight9_Click(sender As Object, e As System.EventArgs) Handles btAvailRight9.Click
        Call find_home_selected(lblHomeAvailPlayer9, btAvailRight9)
    End Sub
    Protected Sub btAvailRight10_Click(sender As Object, e As System.EventArgs) Handles btAvailRight10.Click
        Call find_home_selected(lblHomeAvailPlayer10, btAvailRight10)
    End Sub
    Protected Sub btAvailRight11_Click(sender As Object, e As System.EventArgs) Handles btAvailRight11.Click
        Call find_home_selected(lblHomeAvailPlayer11, btAvailRight11)
    End Sub
    Protected Sub btAvailRight12_Click(sender As Object, e As System.EventArgs) Handles btAvailRight12.Click
        Call find_home_selected(lblHomeAvailPlayer12, btAvailRight12)
    End Sub
    Protected Sub btAvailRight13_Click(sender As Object, e As System.EventArgs) Handles btAvailRight13.Click
        Call find_home_selected(lblHomeAvailPlayer13, btAvailRight13)
    End Sub
    Protected Sub btAvailRight14_Click(sender As Object, e As System.EventArgs) Handles btAvailRight14.Click
        Call find_home_selected(lblHomeAvailPlayer14, btAvailRight14)
    End Sub
    Protected Sub btAvailRight15_Click(sender As Object, e As System.EventArgs) Handles btAvailRight15.Click
        Call find_home_selected(lblHomeAvailPlayer15, btAvailRight15)
    End Sub
    Protected Sub btAvailRight16_Click(sender As Object, e As System.EventArgs) Handles btAvailRight16.Click
        Call find_home_selected(lblHomeAvailPlayer16, btAvailRight16)
    End Sub
    Protected Sub btAvailRight17_Click(sender As Object, e As System.EventArgs) Handles btAvailRight17.Click
        Call find_home_selected(lblHomeAvailPlayer17, btAvailRight17)
    End Sub
    Protected Sub btAvailRight18_Click(sender As Object, e As System.EventArgs) Handles btAvailRight18.Click
        Call find_home_selected(lblHomeAvailPlayer18, btAvailRight18)
    End Sub
    Protected Sub btAvailRight19_Click(sender As Object, e As System.EventArgs) Handles btAvailRight19.Click
        Call find_home_selected(lblHomeAvailPlayer19, btAvailRight19)
    End Sub
    Protected Sub btAvailRight20_Click(sender As Object, e As System.EventArgs) Handles btAvailRight20.Click
        Call find_home_selected(lblHomeAvailPlayer20, btAvailRight20)
    End Sub
    Protected Sub btAvailRight21_Click(sender As Object, e As System.EventArgs) Handles btAvailRight21.Click
        Call find_home_selected(lblHomeAvailPlayer21, btAvailRight21)
    End Sub
    Protected Sub btAvailRight22_Click(sender As Object, e As System.EventArgs) Handles btAvailRight22.Click
        Call find_home_selected(lblHomeAvailPlayer22, btAvailRight22)
    End Sub
    Protected Sub btAvailRight23_Click(sender As Object, e As System.EventArgs) Handles btAvailRight23.Click
        Call find_home_selected(lblHomeAvailPlayer23, btAvailRight23)
    End Sub
    Protected Sub btAvailRight24_Click(sender As Object, e As System.EventArgs) Handles btAvailRight24.Click
        Call find_home_selected(lblHomeAvailPlayer24, btAvailRight24)
    End Sub
    Protected Sub btAvailRight25_Click(sender As Object, e As System.EventArgs) Handles btAvailRight25.Click
        Call find_home_selected(lblHomeAvailPlayer25, btAvailRight25)
    End Sub
    Protected Sub btAvailRight26_Click(sender As Object, e As System.EventArgs) Handles btAvailRight26.Click
        Call find_home_selected(lblHomeAvailPlayer26, btAvailRight26)
    End Sub
    Protected Sub btAvailRight27_Click(sender As Object, e As System.EventArgs) Handles btAvailRight27.Click
        Call find_home_selected(lblHomeAvailPlayer27, btAvailRight27)
    End Sub
    Protected Sub btAvailRight28_Click(sender As Object, e As System.EventArgs) Handles btAvailRight28.Click
        Call find_home_selected(lblHomeAvailPlayer28, btAvailRight28)
    End Sub
    Protected Sub btAvailRight29_Click(sender As Object, e As System.EventArgs) Handles btAvailRight29.Click
        Call find_home_selected(lblHomeAvailPlayer29, btAvailRight29)
    End Sub
    Protected Sub btAvailRight30_Click(sender As Object, e As System.EventArgs) Handles btAvailRight30.Click
        Call find_home_selected(lblHomeAvailPlayer30, btAvailRight30)
    End Sub
    Protected Sub btAvailRight31_Click(sender As Object, e As System.EventArgs) Handles btAvailRight31.Click
        Call find_home_selected(lblHomeAvailPlayer31, btAvailRight31)
    End Sub
    Protected Sub btAvailRight32_Click(sender As Object, e As System.EventArgs) Handles btAvailRight32.Click
        Call find_home_selected(lblHomeAvailPlayer32, btAvailRight32)
    End Sub
    Protected Sub btAvailRight33_Click(sender As Object, e As System.EventArgs) Handles btAvailRight33.Click
        Call find_home_selected(lblHomeAvailPlayer33, btAvailRight33)
    End Sub
    Protected Sub btAvailRight34_Click(sender As Object, e As System.EventArgs) Handles btAvailRight34.Click
        Call find_home_selected(lblHomeAvailPlayer34, btAvailRight34)
    End Sub
    Protected Sub btAvailRight35_Click(sender As Object, e As System.EventArgs) Handles btAvailRight35.Click
        Call find_home_selected(lblHomeAvailPlayer35, btAvailRight35)
    End Sub
    Protected Sub btAvailRight36_Click(sender As Object, e As System.EventArgs) Handles btAvailRight36.Click
        Call find_home_selected(lblHomeAvailPlayer36, btAvailRight36)
    End Sub
    Protected Sub btAvailRight37_Click(sender As Object, e As System.EventArgs) Handles btAvailRight37.Click
        Call find_home_selected(lblHomeAvailPlayer37, btAvailRight37)
    End Sub
    Protected Sub btAvailRight38_Click(sender As Object, e As System.EventArgs) Handles btAvailRight38.Click
        Call find_home_selected(lblHomeAvailPlayer38, btAvailRight38)
    End Sub
    Protected Sub btAvailRight39_Click(sender As Object, e As System.EventArgs) Handles btAvailRight39.Click
        Call find_home_selected(lblHomeAvailPlayer39, btAvailRight39)
    End Sub

    Protected Sub btAvailLeft1_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft1.Click
        Call find_away_selected(lblAwayAvailPlayer1, btAvailLeft1)
    End Sub
    Protected Sub btAvailLeft2_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft2.Click
        Call find_away_selected(lblAwayAvailPlayer2, btAvailLeft2)
    End Sub
    Protected Sub btAvailLeft3_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft3.Click
        Call find_away_selected(lblAwayAvailPlayer3, btAvailLeft3)
    End Sub
    Protected Sub btAvailLeft4_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft4.Click
        Call find_away_selected(lblAwayAvailPlayer4, btAvailLeft4)
    End Sub
    Protected Sub btAvailLeft5_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft5.Click
        Call find_away_selected(lblAwayAvailPlayer5, btAvailLeft5)
    End Sub
    Protected Sub btAvailLeft6_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft6.Click
        Call find_away_selected(lblAwayAvailPlayer6, btAvailLeft6)
    End Sub
    Protected Sub btAvailLeft7_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft7.Click
        Call find_away_selected(lblAwayAvailPlayer7, btAvailLeft7)
    End Sub
    Protected Sub btAvailLeft8_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft8.Click
        Call find_away_selected(lblAwayAvailPlayer8, btAvailLeft8)
    End Sub
    Protected Sub btAvailLeft9_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft9.Click
        Call find_away_selected(lblAwayAvailPlayer9, btAvailLeft9)
    End Sub
    Protected Sub btAvailLeft10_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft10.Click
        Call find_away_selected(lblAwayAvailPlayer10, btAvailLeft10)
    End Sub
    Protected Sub btAvailLeft11_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft11.Click
        Call find_away_selected(lblAwayAvailPlayer11, btAvailLeft11)
    End Sub
    Protected Sub btAvailLeft12_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft12.Click
        Call find_away_selected(lblAwayAvailPlayer12, btAvailLeft12)
    End Sub
    Protected Sub btAvailLeft13_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft13.Click
        Call find_away_selected(lblAwayAvailPlayer13, btAvailLeft13)
    End Sub
    Protected Sub btAvailLeft14_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft14.Click
        Call find_away_selected(lblAwayAvailPlayer14, btAvailLeft14)
    End Sub
    Protected Sub btAvailLeft15_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft15.Click
        Call find_away_selected(lblAwayAvailPlayer15, btAvailLeft15)
    End Sub
    Protected Sub btAvailLeft16_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft16.Click
        Call find_away_selected(lblAwayAvailPlayer16, btAvailLeft16)
    End Sub
    Protected Sub btAvailLeft17_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft17.Click
        Call find_away_selected(lblAwayAvailPlayer17, btAvailLeft17)
    End Sub
    Protected Sub btAvailLeft18_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft18.Click
        Call find_away_selected(lblAwayAvailPlayer18, btAvailLeft18)
    End Sub
    Protected Sub btAvailLeft19_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft19.Click
        Call find_away_selected(lblAwayAvailPlayer19, btAvailLeft19)
    End Sub
    Protected Sub btAvailLeft20_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft20.Click
        Call find_away_selected(lblAwayAvailPlayer20, btAvailLeft20)
    End Sub
    Protected Sub btAvailLeft21_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft21.Click
        Call find_away_selected(lblAwayAvailPlayer21, btAvailLeft21)
    End Sub
    Protected Sub btAvailLeft22_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft22.Click
        Call find_away_selected(lblAwayAvailPlayer22, btAvailLeft22)
    End Sub
    Protected Sub btAvailLeft23_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft23.Click
        Call find_away_selected(lblAwayAvailPlayer23, btAvailLeft23)
    End Sub
    Protected Sub btAvailLeft24_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft24.Click
        Call find_away_selected(lblAwayAvailPlayer24, btAvailLeft24)
    End Sub
    Protected Sub btAvailLeft25_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft25.Click
        Call find_away_selected(lblAwayAvailPlayer25, btAvailLeft25)
    End Sub
    Protected Sub btAvailLeft26_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft26.Click
        Call find_away_selected(lblAwayAvailPlayer26, btAvailLeft26)
    End Sub
    Protected Sub btAvailLeft27_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft27.Click
        Call find_away_selected(lblAwayAvailPlayer27, btAvailLeft27)
    End Sub
    Protected Sub btAvailLeft28_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft28.Click
        Call find_away_selected(lblAwayAvailPlayer28, btAvailLeft28)
    End Sub
    Protected Sub btAvailLeft29_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft29.Click
        Call find_away_selected(lblAwayAvailPlayer29, btAvailLeft29)
    End Sub
    Protected Sub btAvailLeft30_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft30.Click
        Call find_away_selected(lblAwayAvailPlayer30, btAvailLeft30)
    End Sub
    Protected Sub btAvailLeft31_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft31.Click
        Call find_away_selected(lblAwayAvailPlayer31, btAvailLeft31)
    End Sub
    Protected Sub btAvailLeft32_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft32.Click
        Call find_away_selected(lblAwayAvailPlayer32, btAvailLeft32)
    End Sub
    Protected Sub btAvailLeft33_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft33.Click
        Call find_away_selected(lblAwayAvailPlayer33, btAvailLeft33)
    End Sub
    Protected Sub btAvailLeft34_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft34.Click
        Call find_away_selected(lblAwayAvailPlayer34, btAvailLeft34)
    End Sub
    Protected Sub btAvailLeft35_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft35.Click
        Call find_away_selected(lblAwayAvailPlayer35, btAvailLeft35)
    End Sub
    Protected Sub btAvailLeft36_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft36.Click
        Call find_away_selected(lblAwayAvailPlayer36, btAvailLeft36)
    End Sub
    Protected Sub btAvailLeft37_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft37.Click
        Call find_away_selected(lblAwayAvailPlayer37, btAvailLeft37)
    End Sub
    Protected Sub btAvailLeft38_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft38.Click
        Call find_away_selected(lblAwayAvailPlayer38, btAvailLeft38)
    End Sub
    Protected Sub btAvailLeft39_Click(sender As Object, e As System.EventArgs) Handles btAvailLeft39.Click
        Call find_away_selected(lblAwayAvailPlayer39, btAvailLeft39)
    End Sub

    Sub find_home_selected(inLabel As Label, inButton As Button)
        If lblHomePlayer1.Text = "." And lblHomePlayer1.Visible Then
            lblHomePlayer1.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btLeft1.Visible = True
            Exit Sub
        End If
        If lblHomePlayer2.Text = "." And lblHomePlayer2.Visible Then
            lblHomePlayer2.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btLeft2.Visible = True
            Exit Sub
        End If
        If lblHomePlayer3.Text = "." And lblHomePlayer3.Visible Then
            lblHomePlayer3.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btLeft3.Visible = True
            Exit Sub
        End If
        If lblHomePlayer4.Text = "." And lblHomePlayer4.Visible Then
            lblHomePlayer4.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btLeft4.Visible = True
            Exit Sub
        End If
        If lblHomePlayer5.Text = "." And lblHomePlayer5.Visible Then
            lblHomePlayer5.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btLeft5.Visible = True
            Exit Sub
        End If
        If lblHomePlayer6.Text = "." And lblHomePlayer6.Visible Then
            lblHomePlayer6.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btLeft6.Visible = True
            Exit Sub
        End If
        If lblHomePlayer7.Text = "." And lblHomePlayer7.Visible Then
            lblHomePlayer7.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btLeft7.Visible = True
            Exit Sub
        End If
        If lblHomePlayer8.Text = "." And lblHomePlayer8.Visible Then
            lblHomePlayer8.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btLeft8.Visible = True
            Exit Sub
        End If
        If lblHomePlayer9.Text = "." And lblHomePlayer9.Visible Then
            lblHomePlayer9.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btLeft9.Visible = True
            Exit Sub
        End If
        If lblHomePlayer10.Text = "." And lblHomePlayer10.Visible Then
            lblHomePlayer10.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btLeft10.Visible = True
            Exit Sub
        End If
        If lblHomePlayer11.Text = "." And lblHomePlayer11.Visible Then
            lblHomePlayer11.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btLeft11.Visible = True
            Exit Sub
        End If
        If lblHomePlayer12.Text = "." And lblHomePlayer12.Visible Then
            lblHomePlayer12.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btLeft12.Visible = True
            Exit Sub
        End If
    End Sub

    Sub find_away_selected(inLabel As Label, inButton As Button)
        If lblAwayPlayer1.Text = "." And lblAwayPlayer1.Visible Then
            lblAwayPlayer1.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btRight1.Visible = True
            Exit Sub
        End If
        If lblAwayPlayer2.Text = "." And lblAwayPlayer2.Visible Then
            lblAwayPlayer2.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btRight2.Visible = True
            Exit Sub
        End If
        If lblAwayPlayer3.Text = "." And lblAwayPlayer3.Visible Then
            lblAwayPlayer3.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btRight3.Visible = True
            Exit Sub
        End If
        If lblAwayPlayer4.Text = "." And lblAwayPlayer4.Visible Then
            lblAwayPlayer4.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btRight4.Visible = True
            Exit Sub
        End If
        If lblAwayPlayer5.Text = "." And lblAwayPlayer5.Visible Then
            lblAwayPlayer5.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btRight5.Visible = True
            Exit Sub
        End If
        If lblAwayPlayer6.Text = "." And lblAwayPlayer6.Visible Then
            lblAwayPlayer6.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btRight6.Visible = True
            Exit Sub
        End If
        If lblAwayPlayer7.Text = "." And lblAwayPlayer7.Visible Then
            lblAwayPlayer7.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btRight7.Visible = True
            Exit Sub
        End If
        If lblAwayPlayer8.Text = "." And lblAwayPlayer8.Visible Then
            lblAwayPlayer8.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btRight8.Visible = True
            Exit Sub
        End If
        If lblAwayPlayer9.Text = "." And lblAwayPlayer9.Visible Then
            lblAwayPlayer9.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btRight9.Visible = True
            Exit Sub
        End If
        If lblAwayPlayer10.Text = "." And lblAwayPlayer10.Visible Then
            lblAwayPlayer10.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btRight10.Visible = True
            Exit Sub
        End If
        If lblAwayPlayer11.Text = "." And lblAwayPlayer11.Visible Then
            lblAwayPlayer11.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btRight11.Visible = True
            Exit Sub
        End If
        If lblAwayPlayer12.Text = "." And lblAwayPlayer12.Visible Then
            lblAwayPlayer12.Text = inLabel.Text
            inLabel.Text = "."
            inLabel.ForeColor = White
            inButton.Visible = False
            btRight12.Visible = True
            Exit Sub
        End If
    End Sub


    Sub calc_totals()

        HomeRollTotal1 = Val(lblHomeRoll1_1.Text) + Val(lblHomeRoll2_1.Text) + Val(lblHomeRoll3_1.Text) + Val(lblHomeRoll4_1.Text) + Val(lblHomeRoll5_1.Text) + Val(lblHomeRoll6_1.Text) + Val(lblHomeRoll7_1.Text) + Val(lblHomeRoll8_1.Text) + Val(lblHomeRoll9_1.Text) + Val(lblHomeRoll10_1.Text) + Val(lblHomeRoll11_1.Text) + Val(lblHomeRoll12_1.Text)
        HomeRollTotal2 = Val(lblHomeRoll1_2.Text) + Val(lblHomeRoll2_2.Text) + Val(lblHomeRoll3_2.Text) + Val(lblHomeRoll4_2.Text) + Val(lblHomeRoll5_2.Text) + Val(lblHomeRoll6_2.Text) + Val(lblHomeRoll7_2.Text) + Val(lblHomeRoll8_2.Text) + Val(lblHomeRoll9_2.Text) + Val(lblHomeRoll10_2.Text) + Val(lblHomeRoll11_2.Text) + Val(lblHomeRoll12_2.Text)
        HomeRollTotal3 = Val(lblHomeRoll1_3.Text) + Val(lblHomeRoll2_3.Text) + Val(lblHomeRoll3_3.Text) + Val(lblHomeRoll4_3.Text) + Val(lblHomeRoll5_3.Text) + Val(lblHomeRoll6_3.Text) + Val(lblHomeRoll7_3.Text) + Val(lblHomeRoll8_3.Text) + Val(lblHomeRoll9_3.Text) + Val(lblHomeRoll10_3.Text) + Val(lblHomeRoll11_3.Text) + Val(lblHomeRoll12_3.Text)
        HomeRollTotal4 = Val(lblHomeRoll1_4.Text) + Val(lblHomeRoll2_4.Text) + Val(lblHomeRoll3_4.Text) + Val(lblHomeRoll4_4.Text) + Val(lblHomeRoll5_4.Text) + Val(lblHomeRoll6_4.Text) + Val(lblHomeRoll7_4.Text) + Val(lblHomeRoll8_4.Text) + Val(lblHomeRoll9_4.Text) + Val(lblHomeRoll10_4.Text) + Val(lblHomeRoll11_4.Text) + Val(lblHomeRoll12_4.Text)
        HomeRollTotal5 = Val(lblHomeRoll1_5.Text) + Val(lblHomeRoll2_5.Text) + Val(lblHomeRoll3_5.Text) + Val(lblHomeRoll4_5.Text) + Val(lblHomeRoll5_5.Text) + Val(lblHomeRoll6_5.Text) + Val(lblHomeRoll7_5.Text) + Val(lblHomeRoll8_5.Text) + Val(lblHomeRoll9_5.Text) + Val(lblHomeRoll10_5.Text) + Val(lblHomeRoll11_5.Text) + Val(lblHomeRoll12_5.Text)
        AwayRollTotal1 = Val(lblAwayRoll1_1.Text) + Val(lblAwayRoll2_1.Text) + Val(lblAwayRoll3_1.Text) + Val(lblAwayRoll4_1.Text) + Val(lblAwayRoll5_1.Text) + Val(lblAwayRoll6_1.Text) + Val(lblAwayRoll7_1.Text) + Val(lblAwayRoll8_1.Text) + Val(lblAwayRoll9_1.Text) + Val(lblAwayRoll10_1.Text) + Val(lblAwayRoll11_1.Text) + Val(lblAwayRoll12_1.Text)
        AwayRollTotal2 = Val(lblAwayRoll1_2.Text) + Val(lblAwayRoll2_2.Text) + Val(lblAwayRoll3_2.Text) + Val(lblAwayRoll4_2.Text) + Val(lblAwayRoll5_2.Text) + Val(lblAwayRoll6_2.Text) + Val(lblAwayRoll7_2.Text) + Val(lblAwayRoll8_2.Text) + Val(lblAwayRoll9_2.Text) + Val(lblAwayRoll10_2.Text) + Val(lblAwayRoll11_2.Text) + Val(lblAwayRoll12_2.Text)
        AwayRollTotal3 = Val(lblAwayRoll1_3.Text) + Val(lblAwayRoll2_3.Text) + Val(lblAwayRoll3_3.Text) + Val(lblAwayRoll4_3.Text) + Val(lblAwayRoll5_3.Text) + Val(lblAwayRoll6_3.Text) + Val(lblAwayRoll7_3.Text) + Val(lblAwayRoll8_3.Text) + Val(lblAwayRoll9_3.Text) + Val(lblAwayRoll10_3.Text) + Val(lblAwayRoll11_3.Text) + Val(lblAwayRoll12_3.Text)
        AwayRollTotal4 = Val(lblAwayRoll1_4.Text) + Val(lblAwayRoll2_4.Text) + Val(lblAwayRoll3_4.Text) + Val(lblAwayRoll4_4.Text) + Val(lblAwayRoll5_4.Text) + Val(lblAwayRoll6_4.Text) + Val(lblAwayRoll7_4.Text) + Val(lblAwayRoll8_4.Text) + Val(lblAwayRoll9_4.Text) + Val(lblAwayRoll10_4.Text) + Val(lblAwayRoll11_4.Text) + Val(lblAwayRoll12_4.Text)
        AwayRollTotal5 = Val(lblAwayRoll1_5.Text) + Val(lblAwayRoll2_5.Text) + Val(lblAwayRoll3_5.Text) + Val(lblAwayRoll4_5.Text) + Val(lblAwayRoll5_5.Text) + Val(lblAwayRoll6_5.Text) + Val(lblAwayRoll7_5.Text) + Val(lblAwayRoll8_5.Text) + Val(lblAwayRoll9_5.Text) + Val(lblAwayRoll10_5.Text) + Val(lblAwayRoll11_5.Text) + Val(lblAwayRoll12_5.Text)

        HomeRollsTotal = HomeRollTotal1 + HomeRollTotal2 + HomeRollTotal3 + HomeRollTotal4 + HomeRollTotal5
        AwayRollsTotal = AwayRollTotal1 + AwayRollTotal2 + AwayRollTotal3 + AwayRollTotal4 + AwayRollTotal5

        lblHomeRollTotal1.Text = HomeRollTotal1
        lblHomeRollTotal2.Text = HomeRollTotal2
        lblHomeRollTotal3.Text = HomeRollTotal3
        lblHomeRollTotal4.Text = HomeRollTotal4
        lblHomeRollTotal5.Text = HomeRollTotal5
        lblAwayRollTotal1.Text = AwayRollTotal1
        lblAwayRollTotal2.Text = AwayRollTotal2
        lblAwayRollTotal3.Text = AwayRollTotal3
        lblAwayRollTotal4.Text = AwayRollTotal4
        lblAwayRollTotal5.Text = AwayRollTotal5

        lblHomePoints0.Text = HomeRollsTotal
        lblAwayPoints0.Text = AwayRollsTotal

        If HomeRollsTotal > 0 And AwayRollsTotal > 0 Then
            If HomeRollsTotal > AwayRollsTotal Then ddResult.Text = "2 - 0"
            If HomeRollsTotal = AwayRollsTotal Then ddResult.Text = "1 - 1"
            If HomeRollsTotal < AwayRollsTotal Then ddResult.Text = "0 - 2"

            HomeRollsWon = 0
            AwayRollsWon = 0

            Select Case True
                Case HomeRollTotal1 > AwayRollTotal1
                    HomeRollsWon = HomeRollsWon + 1
                Case HomeRollTotal1 = AwayRollTotal1
                    HomeRollsWon = HomeRollsWon + 0.5
                    AwayRollsWon = AwayRollsWon + 0.5
                Case HomeRollTotal1 < AwayRollTotal1
                    AwayRollsWon = AwayRollsWon + 1
            End Select

            Select Case True
                Case HomeRollTotal2 > AwayRollTotal2
                    HomeRollsWon = HomeRollsWon + 1
                Case HomeRollTotal2 = AwayRollTotal2
                    HomeRollsWon = HomeRollsWon + 0.5
                    AwayRollsWon = AwayRollsWon + 0.5
                Case HomeRollTotal2 < AwayRollTotal2
                    AwayRollsWon = AwayRollsWon + 1
            End Select

            Select Case True
                Case HomeRollTotal3 > AwayRollTotal3
                    HomeRollsWon = HomeRollsWon + 1
                Case HomeRollTotal3 = AwayRollTotal3
                    HomeRollsWon = HomeRollsWon + 0.5
                    AwayRollsWon = AwayRollsWon + 0.5
                Case HomeRollTotal3 < AwayRollTotal3
                    AwayRollsWon = AwayRollsWon + 1
            End Select

            Select Case True
                Case HomeRollTotal4 > AwayRollTotal4
                    HomeRollsWon = HomeRollsWon + 1
                Case HomeRollTotal4 = AwayRollTotal4
                    HomeRollsWon = HomeRollsWon + 0.5
                    AwayRollsWon = AwayRollsWon + 0.5
                Case HomeRollTotal4 < AwayRollTotal4
                    AwayRollsWon = AwayRollsWon + 1
            End Select

            Select Case True
                Case HomeRollTotal5 > AwayRollTotal5
                    HomeRollsWon = HomeRollsWon + 1
                Case HomeRollTotal5 = AwayRollTotal5
                    HomeRollsWon = HomeRollsWon + 0.5
                    AwayRollsWon = AwayRollsWon + 0.5
                Case HomeRollTotal5 < AwayRollTotal5
                    AwayRollsWon = AwayRollsWon + 1
            End Select

            Select Case HomeRollsWon
                Case 5 : ddHomeRolls.SelectedIndex = 1
                Case 4.5 : ddHomeRolls.SelectedIndex = 2
                Case 4 : ddHomeRolls.SelectedIndex = 3
                Case 3.5 : ddHomeRolls.SelectedIndex = 4
                Case 3 : ddHomeRolls.SelectedIndex = 5
                Case 2.5 : ddHomeRolls.SelectedIndex = 6
                Case 2 : ddHomeRolls.SelectedIndex = 7
                Case 1.5 : ddHomeRolls.SelectedIndex = 8
                Case 1 : ddHomeRolls.SelectedIndex = 9
                Case 0.5 : ddHomeRolls.SelectedIndex = 10
                Case 0 : ddHomeRolls.SelectedIndex = 11
            End Select
            ddAwayRolls.SelectedIndex = ddHomeRolls.SelectedIndex
        End If

        Call colour_totals()

    End Sub

    Sub colour_totals()
        lblHomeRollTotal1.BackColor = Orange
        lblHomeRollTotal2.BackColor = Orange
        lblHomeRollTotal3.BackColor = Orange
        lblHomeRollTotal4.BackColor = Orange
        lblHomeRollTotal5.BackColor = Orange
        lblAwayRollTotal1.BackColor = Orange
        lblAwayRollTotal2.BackColor = Orange
        lblAwayRollTotal3.BackColor = Orange
        lblAwayRollTotal4.BackColor = Orange
        lblAwayRollTotal5.BackColor = Orange

        lblHomeRollTotal1.ForeColor = White
        lblHomeRollTotal2.ForeColor = White
        lblHomeRollTotal3.ForeColor = White
        lblHomeRollTotal4.ForeColor = White
        lblHomeRollTotal5.ForeColor = White
        lblAwayRollTotal1.ForeColor = White
        lblAwayRollTotal2.ForeColor = White
        lblAwayRollTotal3.ForeColor = White
        lblAwayRollTotal4.ForeColor = White
        lblAwayRollTotal5.ForeColor = White

        lblHomePoints0.BackColor = Orange
        lblHomePoints0.ForeColor = White

        lblAwayPoints0.BackColor = Orange
        lblAwayPoints0.ForeColor = White

        Select Case True
            Case HomeRollTotal1 > AwayRollTotal1
                lblHomeRollTotal1.BackColor = Green
                lblAwayRollTotal1.BackColor = Red
            Case HomeRollTotal1 < AwayRollTotal1
                lblHomeRollTotal1.BackColor = Red
                lblAwayRollTotal1.BackColor = Green
            Case HomeRollTotal1 = AwayRollTotal1
                lblHomeRollTotal1.ForeColor = Black
                lblAwayRollTotal1.ForeColor = Black
        End Select

        Select Case True
            Case HomeRollTotal2 > AwayRollTotal2
                lblHomeRollTotal2.BackColor = Green
                lblAwayRollTotal2.BackColor = Red
            Case HomeRollTotal2 < AwayRollTotal2
                lblHomeRollTotal2.BackColor = Red
                lblAwayRollTotal2.BackColor = Green
            Case HomeRollTotal2 = AwayRollTotal2
                lblHomeRollTotal2.ForeColor = Black
                lblAwayRollTotal2.ForeColor = Black
        End Select

        Select Case True
            Case HomeRollTotal3 > AwayRollTotal3
                lblHomeRollTotal3.BackColor = Green
                lblAwayRollTotal3.BackColor = Red
            Case HomeRollTotal3 < AwayRollTotal3
                lblHomeRollTotal3.BackColor = Red
                lblAwayRollTotal3.BackColor = Green
            Case HomeRollTotal3 = AwayRollTotal3
                lblHomeRollTotal3.ForeColor = Black
                lblAwayRollTotal3.ForeColor = Black
        End Select

        Select Case True
            Case HomeRollTotal4 > AwayRollTotal4
                lblHomeRollTotal4.BackColor = Green
                lblAwayRollTotal4.BackColor = Red
            Case HomeRollTotal4 < AwayRollTotal4
                lblHomeRollTotal4.BackColor = Red
                lblAwayRollTotal4.BackColor = Green
            Case HomeRollTotal4 = AwayRollTotal4
                lblHomeRollTotal4.ForeColor = Black
                lblAwayRollTotal4.ForeColor = Black
        End Select

        Select Case True
            Case HomeRollTotal5 > AwayRollTotal5
                lblHomeRollTotal5.BackColor = Green
                lblAwayRollTotal5.BackColor = Red
            Case HomeRollTotal5 < AwayRollTotal5
                lblHomeRollTotal5.BackColor = Red
                lblAwayRollTotal5.BackColor = Green
            Case HomeRollTotal5 = AwayRollTotal5
                lblHomeRollTotal5.ForeColor = Black
                lblAwayRollTotal5.ForeColor = Black
        End Select

        Select Case True
            Case HomeRollsTotal > AwayRollsTotal
                lblHomePoints0.BackColor = Green
                lblAwayPoints0.BackColor = Red
            Case HomeRollsTotal < AwayRollsTotal
                lblHomePoints0.BackColor = Red
                lblAwayPoints0.BackColor = Green
            Case HomeRollsTotal = AwayRollsTotal
                lblHomePoints0.ForeColor = Black
                lblAwayPoints0.ForeColor = Black
        End Select

    End Sub

    Private Sub colour_player_rolls()
        If Val(lblHomeRoll1_1.Text) >= 9 Then lblHomeRoll1_1.BackColor = DarkCyan
        If Val(lblHomeRoll1_2.Text) >= 9 Then lblHomeRoll1_2.BackColor = DarkCyan
        If Val(lblHomeRoll1_3.Text) >= 9 Then lblHomeRoll1_3.BackColor = DarkCyan
        If Val(lblHomeRoll1_4.Text) >= 9 Then lblHomeRoll1_4.BackColor = DarkCyan
        If Val(lblHomeRoll1_5.Text) >= 9 Then lblHomeRoll1_5.BackColor = DarkCyan
        If Val(lblHomeRoll2_1.Text) >= 9 Then lblHomeRoll2_1.BackColor = DarkCyan
        If Val(lblHomeRoll2_2.Text) >= 9 Then lblHomeRoll2_2.BackColor = DarkCyan
        If Val(lblHomeRoll2_3.Text) >= 9 Then lblHomeRoll2_3.BackColor = DarkCyan
        If Val(lblHomeRoll2_4.Text) >= 9 Then lblHomeRoll2_4.BackColor = DarkCyan
        If Val(lblHomeRoll2_5.Text) >= 9 Then lblHomeRoll2_5.BackColor = DarkCyan
        If Val(lblHomeRoll3_1.Text) >= 9 Then lblHomeRoll3_1.BackColor = DarkCyan
        If Val(lblHomeRoll3_2.Text) >= 9 Then lblHomeRoll3_2.BackColor = DarkCyan
        If Val(lblHomeRoll3_3.Text) >= 9 Then lblHomeRoll3_3.BackColor = DarkCyan
        If Val(lblHomeRoll3_4.Text) >= 9 Then lblHomeRoll3_4.BackColor = DarkCyan
        If Val(lblHomeRoll3_5.Text) >= 9 Then lblHomeRoll3_5.BackColor = DarkCyan
        If Val(lblHomeRoll4_1.Text) >= 9 Then lblHomeRoll4_1.BackColor = DarkCyan
        If Val(lblHomeRoll4_2.Text) >= 9 Then lblHomeRoll4_2.BackColor = DarkCyan
        If Val(lblHomeRoll4_3.Text) >= 9 Then lblHomeRoll4_3.BackColor = DarkCyan
        If Val(lblHomeRoll4_4.Text) >= 9 Then lblHomeRoll4_4.BackColor = DarkCyan
        If Val(lblHomeRoll4_5.Text) >= 9 Then lblHomeRoll4_5.BackColor = DarkCyan
        If Val(lblHomeRoll5_1.Text) >= 9 Then lblHomeRoll5_1.BackColor = DarkCyan
        If Val(lblHomeRoll5_2.Text) >= 9 Then lblHomeRoll5_2.BackColor = DarkCyan
        If Val(lblHomeRoll5_3.Text) >= 9 Then lblHomeRoll5_3.BackColor = DarkCyan
        If Val(lblHomeRoll5_4.Text) >= 9 Then lblHomeRoll5_4.BackColor = DarkCyan
        If Val(lblHomeRoll5_5.Text) >= 9 Then lblHomeRoll5_5.BackColor = DarkCyan
        If Val(lblHomeRoll6_1.Text) >= 9 Then lblHomeRoll6_1.BackColor = DarkCyan
        If Val(lblHomeRoll6_2.Text) >= 9 Then lblHomeRoll6_2.BackColor = DarkCyan
        If Val(lblHomeRoll6_3.Text) >= 9 Then lblHomeRoll6_3.BackColor = DarkCyan
        If Val(lblHomeRoll6_4.Text) >= 9 Then lblHomeRoll6_4.BackColor = DarkCyan
        If Val(lblHomeRoll6_5.Text) >= 9 Then lblHomeRoll6_5.BackColor = DarkCyan
        If Val(lblHomeRoll7_1.Text) >= 9 Then lblHomeRoll7_1.BackColor = DarkCyan
        If Val(lblHomeRoll7_2.Text) >= 9 Then lblHomeRoll7_2.BackColor = DarkCyan
        If Val(lblHomeRoll7_3.Text) >= 9 Then lblHomeRoll7_3.BackColor = DarkCyan
        If Val(lblHomeRoll7_4.Text) >= 9 Then lblHomeRoll7_4.BackColor = DarkCyan
        If Val(lblHomeRoll7_5.Text) >= 9 Then lblHomeRoll7_5.BackColor = DarkCyan
        If Val(lblHomeRoll8_1.Text) >= 9 Then lblHomeRoll8_1.BackColor = DarkCyan
        If Val(lblHomeRoll8_2.Text) >= 9 Then lblHomeRoll8_2.BackColor = DarkCyan
        If Val(lblHomeRoll8_3.Text) >= 9 Then lblHomeRoll8_3.BackColor = DarkCyan
        If Val(lblHomeRoll8_4.Text) >= 9 Then lblHomeRoll8_4.BackColor = DarkCyan
        If Val(lblHomeRoll8_5.Text) >= 9 Then lblHomeRoll8_5.BackColor = DarkCyan
        If Val(lblHomeRoll9_1.Text) >= 9 Then lblHomeRoll9_1.BackColor = DarkCyan
        If Val(lblHomeRoll9_2.Text) >= 9 Then lblHomeRoll9_2.BackColor = DarkCyan
        If Val(lblHomeRoll9_3.Text) >= 9 Then lblHomeRoll9_3.BackColor = DarkCyan
        If Val(lblHomeRoll9_4.Text) >= 9 Then lblHomeRoll9_4.BackColor = DarkCyan
        If Val(lblHomeRoll9_5.Text) >= 9 Then lblHomeRoll9_5.BackColor = DarkCyan
        If Val(lblHomeRoll10_1.Text) >= 9 Then lblHomeRoll10_1.BackColor = DarkCyan
        If Val(lblHomeRoll10_2.Text) >= 9 Then lblHomeRoll10_2.BackColor = DarkCyan
        If Val(lblHomeRoll10_3.Text) >= 9 Then lblHomeRoll10_3.BackColor = DarkCyan
        If Val(lblHomeRoll10_4.Text) >= 9 Then lblHomeRoll10_4.BackColor = DarkCyan
        If Val(lblHomeRoll10_5.Text) >= 9 Then lblHomeRoll10_5.BackColor = DarkCyan
        If Val(lblHomeRoll11_1.Text) >= 9 Then lblHomeRoll11_1.BackColor = DarkCyan
        If Val(lblHomeRoll11_2.Text) >= 9 Then lblHomeRoll11_2.BackColor = DarkCyan
        If Val(lblHomeRoll11_3.Text) >= 9 Then lblHomeRoll11_3.BackColor = DarkCyan
        If Val(lblHomeRoll11_4.Text) >= 9 Then lblHomeRoll11_4.BackColor = DarkCyan
        If Val(lblHomeRoll11_5.Text) >= 9 Then lblHomeRoll11_5.BackColor = DarkCyan
        If Val(lblHomeRoll12_1.Text) >= 9 Then lblHomeRoll12_1.BackColor = DarkCyan
        If Val(lblHomeRoll12_2.Text) >= 9 Then lblHomeRoll12_2.BackColor = DarkCyan
        If Val(lblHomeRoll12_3.Text) >= 9 Then lblHomeRoll12_3.BackColor = DarkCyan
        If Val(lblHomeRoll12_4.Text) >= 9 Then lblHomeRoll12_4.BackColor = DarkCyan
        If Val(lblHomeRoll12_5.Text) >= 9 Then lblHomeRoll12_5.BackColor = DarkCyan

        If Val(lblAwayRoll1_1.Text) >= 9 Then lblAwayRoll1_1.BackColor = DarkCyan
        If Val(lblAwayRoll1_2.Text) >= 9 Then lblAwayRoll1_2.BackColor = DarkCyan
        If Val(lblAwayRoll1_3.Text) >= 9 Then lblAwayRoll1_3.BackColor = DarkCyan
        If Val(lblAwayRoll1_4.Text) >= 9 Then lblAwayRoll1_4.BackColor = DarkCyan
        If Val(lblAwayRoll1_5.Text) >= 9 Then lblAwayRoll1_5.BackColor = DarkCyan
        If Val(lblAwayRoll2_1.Text) >= 9 Then lblAwayRoll2_1.BackColor = DarkCyan
        If Val(lblAwayRoll2_2.Text) >= 9 Then lblAwayRoll2_2.BackColor = DarkCyan
        If Val(lblAwayRoll2_3.Text) >= 9 Then lblAwayRoll2_3.BackColor = DarkCyan
        If Val(lblAwayRoll2_4.Text) >= 9 Then lblAwayRoll2_4.BackColor = DarkCyan
        If Val(lblAwayRoll2_5.Text) >= 9 Then lblAwayRoll2_5.BackColor = DarkCyan
        If Val(lblAwayRoll3_1.Text) >= 9 Then lblAwayRoll3_1.BackColor = DarkCyan
        If Val(lblAwayRoll3_2.Text) >= 9 Then lblAwayRoll3_2.BackColor = DarkCyan
        If Val(lblAwayRoll3_3.Text) >= 9 Then lblAwayRoll3_3.BackColor = DarkCyan
        If Val(lblAwayRoll3_4.Text) >= 9 Then lblAwayRoll3_4.BackColor = DarkCyan
        If Val(lblAwayRoll3_5.Text) >= 9 Then lblAwayRoll3_5.BackColor = DarkCyan
        If Val(lblAwayRoll4_1.Text) >= 9 Then lblAwayRoll4_1.BackColor = DarkCyan
        If Val(lblAwayRoll4_2.Text) >= 9 Then lblAwayRoll4_2.BackColor = DarkCyan
        If Val(lblAwayRoll4_3.Text) >= 9 Then lblAwayRoll4_3.BackColor = DarkCyan
        If Val(lblAwayRoll4_4.Text) >= 9 Then lblAwayRoll4_4.BackColor = DarkCyan
        If Val(lblAwayRoll4_5.Text) >= 9 Then lblAwayRoll4_5.BackColor = DarkCyan
        If Val(lblAwayRoll5_1.Text) >= 9 Then lblAwayRoll5_1.BackColor = DarkCyan
        If Val(lblAwayRoll5_2.Text) >= 9 Then lblAwayRoll5_2.BackColor = DarkCyan
        If Val(lblAwayRoll5_3.Text) >= 9 Then lblAwayRoll5_3.BackColor = DarkCyan
        If Val(lblAwayRoll5_4.Text) >= 9 Then lblAwayRoll5_4.BackColor = DarkCyan
        If Val(lblAwayRoll5_5.Text) >= 9 Then lblAwayRoll5_5.BackColor = DarkCyan
        If Val(lblAwayRoll6_1.Text) >= 9 Then lblAwayRoll6_1.BackColor = DarkCyan
        If Val(lblAwayRoll6_2.Text) >= 9 Then lblAwayRoll6_2.BackColor = DarkCyan
        If Val(lblAwayRoll6_3.Text) >= 9 Then lblAwayRoll6_3.BackColor = DarkCyan
        If Val(lblAwayRoll6_4.Text) >= 9 Then lblAwayRoll6_4.BackColor = DarkCyan
        If Val(lblAwayRoll6_5.Text) >= 9 Then lblAwayRoll6_5.BackColor = DarkCyan
        If Val(lblAwayRoll7_1.Text) >= 9 Then lblAwayRoll7_1.BackColor = DarkCyan
        If Val(lblAwayRoll7_2.Text) >= 9 Then lblAwayRoll7_2.BackColor = DarkCyan
        If Val(lblAwayRoll7_3.Text) >= 9 Then lblAwayRoll7_3.BackColor = DarkCyan
        If Val(lblAwayRoll7_4.Text) >= 9 Then lblAwayRoll7_4.BackColor = DarkCyan
        If Val(lblAwayRoll7_5.Text) >= 9 Then lblAwayRoll7_5.BackColor = DarkCyan
        If Val(lblAwayRoll8_1.Text) >= 9 Then lblAwayRoll8_1.BackColor = DarkCyan
        If Val(lblAwayRoll8_2.Text) >= 9 Then lblAwayRoll8_2.BackColor = DarkCyan
        If Val(lblAwayRoll8_3.Text) >= 9 Then lblAwayRoll8_3.BackColor = DarkCyan
        If Val(lblAwayRoll8_4.Text) >= 9 Then lblAwayRoll8_4.BackColor = DarkCyan
        If Val(lblAwayRoll8_5.Text) >= 9 Then lblAwayRoll8_5.BackColor = DarkCyan
        If Val(lblAwayRoll9_1.Text) >= 9 Then lblAwayRoll9_1.BackColor = DarkCyan
        If Val(lblAwayRoll9_2.Text) >= 9 Then lblAwayRoll9_2.BackColor = DarkCyan
        If Val(lblAwayRoll9_3.Text) >= 9 Then lblAwayRoll9_3.BackColor = DarkCyan
        If Val(lblAwayRoll9_4.Text) >= 9 Then lblAwayRoll9_4.BackColor = DarkCyan
        If Val(lblAwayRoll9_5.Text) >= 9 Then lblAwayRoll9_5.BackColor = DarkCyan
        If Val(lblAwayRoll10_1.Text) >= 9 Then lblAwayRoll10_1.BackColor = DarkCyan
        If Val(lblAwayRoll10_2.Text) >= 9 Then lblAwayRoll10_2.BackColor = DarkCyan
        If Val(lblAwayRoll10_3.Text) >= 9 Then lblAwayRoll10_3.BackColor = DarkCyan
        If Val(lblAwayRoll10_4.Text) >= 9 Then lblAwayRoll10_4.BackColor = DarkCyan
        If Val(lblAwayRoll10_5.Text) >= 9 Then lblAwayRoll10_5.BackColor = DarkCyan
        If Val(lblAwayRoll11_1.Text) >= 9 Then lblAwayRoll11_1.BackColor = DarkCyan
        If Val(lblAwayRoll11_2.Text) >= 9 Then lblAwayRoll11_2.BackColor = DarkCyan
        If Val(lblAwayRoll11_3.Text) >= 9 Then lblAwayRoll11_3.BackColor = DarkCyan
        If Val(lblAwayRoll11_4.Text) >= 9 Then lblAwayRoll11_4.BackColor = DarkCyan
        If Val(lblAwayRoll11_5.Text) >= 9 Then lblAwayRoll11_5.BackColor = DarkCyan
        If Val(lblAwayRoll12_1.Text) >= 9 Then lblAwayRoll12_1.BackColor = DarkCyan
        If Val(lblAwayRoll12_2.Text) >= 9 Then lblAwayRoll12_2.BackColor = DarkCyan
        If Val(lblAwayRoll12_3.Text) >= 9 Then lblAwayRoll12_3.BackColor = DarkCyan
        If Val(lblAwayRoll12_4.Text) >= 9 Then lblAwayRoll12_4.BackColor = DarkCyan
        If Val(lblAwayRoll12_5.Text) >= 9 Then lblAwayRoll12_5.BackColor = DarkCyan

    End Sub

    Private Sub colour_player_totals()
        If Val(lblHomePoints1.Text) >= 30 Then lblHomePoints1.BackColor = DarkCyan
        If Val(lblHomePoints2.Text) >= 30 Then lblHomePoints2.BackColor = DarkCyan
        If Val(lblHomePoints3.Text) >= 30 Then lblHomePoints3.BackColor = DarkCyan
        If Val(lblHomePoints4.Text) >= 30 Then lblHomePoints4.BackColor = DarkCyan
        If Val(lblHomePoints5.Text) >= 30 Then lblHomePoints5.BackColor = DarkCyan
        If Val(lblHomePoints6.Text) >= 30 Then lblHomePoints6.BackColor = DarkCyan
        If Val(lblHomePoints7.Text) >= 30 Then lblHomePoints7.BackColor = DarkCyan
        If Val(lblHomePoints8.Text) >= 30 Then lblHomePoints8.BackColor = DarkCyan
        If Val(lblHomePoints9.Text) >= 30 Then lblHomePoints9.BackColor = DarkCyan
        If Val(lblHomePoints10.Text) >= 30 Then lblHomePoints10.BackColor = DarkCyan
        If Val(lblHomePoints11.Text) >= 30 Then lblHomePoints11.BackColor = DarkCyan
        If Val(lblHomePoints12.Text) >= 30 Then lblHomePoints12.BackColor = DarkCyan

        If Val(lblAwayPoints1.Text) >= 30 Then lblAwayPoints1.BackColor = DarkCyan
        If Val(lblAwayPoints2.Text) >= 30 Then lblAwayPoints2.BackColor = DarkCyan
        If Val(lblAwayPoints3.Text) >= 30 Then lblAwayPoints3.BackColor = DarkCyan
        If Val(lblAwayPoints4.Text) >= 30 Then lblAwayPoints4.BackColor = DarkCyan
        If Val(lblAwayPoints5.Text) >= 30 Then lblAwayPoints5.BackColor = DarkCyan
        If Val(lblAwayPoints6.Text) >= 30 Then lblAwayPoints6.BackColor = DarkCyan
        If Val(lblAwayPoints7.Text) >= 30 Then lblAwayPoints7.BackColor = DarkCyan
        If Val(lblAwayPoints8.Text) >= 30 Then lblAwayPoints8.BackColor = DarkCyan
        If Val(lblAwayPoints9.Text) >= 30 Then lblAwayPoints9.BackColor = DarkCyan
        If Val(lblAwayPoints10.Text) >= 30 Then lblAwayPoints10.BackColor = DarkCyan
        If Val(lblAwayPoints11.Text) >= 30 Then lblAwayPoints11.BackColor = DarkCyan
        If Val(lblAwayPoints12.Text) >= 30 Then lblAwayPoints12.BackColor = DarkCyan
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader    'MySqlDataReader
        Dim NewResult As String = ""
        NewResult = ddResult.SelectedValue
        If check_entries() Then
            Call calc_totals()
            Call update_deducted_header()
            Call update_fixture_details()
            Call update_player_stats("sp_update_player_stats")

            'update league AND team positions
            'strSQL = "EXEC AS user = '" + objGlobals.CurrentUser + "'"
            'myDataReader = objGlobals.SQLSelect(strSQL)
            strSQL = "EXEC " & objGlobals.CurrentSchema & "sp_update_league_position '" + objGlobals.CurrentUser + "','" & lblLeague.Text & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)

            'strSQL = "EXEC AS user = '" + objGlobals.CurrentUser + "'"
            'myDataReader = objGlobals.SQLSelect(strSQL)
            strSQL = "EXEC " & objGlobals.CurrentSchema & "sp_update_team_position '" + objGlobals.CurrentUser + "','" & lblLeague.Text & "','" & lblHomeTeam.Text & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)

            'strSQL = "EXEC AS user = '" + objGlobals.CurrentUser + "'"
            'myDataReader = objGlobals.SQLSelect(strSQL)
            strSQL = "EXEC " & objGlobals.CurrentSchema & "sp_update_team_position '" + objGlobals.CurrentUser + "','" & lblLeague.Text & "','" & lblAwayTeam.Text & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            lstErrors.Visible = False
        Else
            Exit Sub
        End If
        If NewResult <> lblResult.Text Then
            lstErrors.Items.Clear()
            lstErrors.Items.Add("Result is different from the Original Result.")
            lstErrors.Items.Add("Click 'Re-Update' to update the Fixture result.")
            lstErrors.Items.Add("Fixture details have been saved")
            lstErrors.Visible = True
            btnReUpdate.Visible = True
            btnReUpdate.PostBackUrl = "~/Ladies_Skit/Admin/Fixture Result.aspx?ID= " & fixture_id & "&Week=" & FixtureWeek
            btnReUpdate.Focus()
            btnUpdate.Visible = False
        Else
            If TeamSelected Is Nothing Then
                Response.Redirect("~/Ladies_Skit/Default.aspx?Week=" & FixtureWeek)
            Else
                Response.Redirect("~/Ladies_Skit/Team Fixtures.aspx?League=" & lblLeague.Text & "&Team=" & TeamSelected)
            End If
        End If
    End Sub

    Sub update_player_stats(inStoredProcedure As String)
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader    'MySqlDataReader
        Dim myDataReader2 As OleDbDataReader    'MySqlDataReader
        Dim tempSeason As String = objGlobals.get_current_season
        inStoredProcedure = objGlobals.CurrentSchema & inStoredProcedure
        'update the home team players
        strSQL = "SELECT player FROM " & objGlobals.CurrentSchema & "vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblHomeTeam.Text & "' AND player NOT LIKE 'A N OTHER%' "
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            'strSQL = "EXEC AS user = '" + objGlobals.CurrentUser + "'"
            'myDataReader = objGlobals.SQLSelect(strSQL)
            strSQL = "EXEC " & inStoredProcedure & " '" + objGlobals.CurrentUser + "','" & tempSeason & "','" & lblLeague.Text & "','" & lblHomeTeam.Text & "','" & myDataReader.Item("player") & "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While

        'update the away team players
        strSQL = "SELECT player FROM " & objGlobals.CurrentSchema & "vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblAwayTeam.Text & "' AND player NOT LIKE 'A N OTHER%' "
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            'strSQL = "EXEC AS user = '" + objGlobals.CurrentUser + "'"
            'myDataReader = objGlobals.SQLSelect(strSQL)
            strSQL = "EXEC " & inStoredProcedure & " '" + objGlobals.CurrentUser + "','" & tempSeason & "','" & lblLeague.Text & "','" & lblAwayTeam.Text & "','" & myDataReader.Item("player") & "'"
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
    End Sub

    Sub update_deducted_header()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader    'MySqlDataReader
        strSQL = "UPDATE " & objGlobals.CurrentSchema & "vw_fixtures SET home_rolls_won = " & HomeRollsWon & ",away_rolls_won = " & AwayRollsWon
        strSQL = strSQL & ",home_rolls_total= " & HomeRollsTotal & ",away_rolls_total = " & AwayRollsTotal
        strSQL = strSQL & ",home_roll_1 = " & HomeRollTotal1 & ",home_roll_2 = " & HomeRollTotal2 & ",home_roll_3 = " & HomeRollTotal3 & ",home_roll_4 = " & HomeRollTotal4 & ",home_roll_5 = " & HomeRollTotal5
        strSQL = strSQL & ",away_roll_1 = " & AwayRollTotal1 & ",away_roll_2 = " & AwayRollTotal2 & ",away_roll_3 = " & AwayRollTotal3 & ",away_roll_4 = " & AwayRollTotal4 & ",away_roll_5 = " & AwayRollTotal5
        strSQL = strSQL & ",home_rolls_result = ' (" & ddHomeRolls.Text & ")',away_rolls_result = ' (" & ddAwayRolls.Text & ")',status = 2,home_points_deducted = " & ddHomePointsDeducted.SelectedValue & ",away_points_deducted = " & ddAwayPointsDeducted.SelectedValue & " WHERE fixture_id = " & fixture_id
        myDataReader = objGlobals.SQLSelect(strSQL)
    End Sub

    Function NumberNines(inRoll1 As Label, inRoll2 As Label, inRoll3 As Label, inRoll4 As Label, inRoll5 As Label) As Integer
        NumberNines = 0
        If Val(inRoll1.Text) >= 9 Then NumberNines = NumberNines + 1
        If Val(inRoll2.Text) >= 9 Then NumberNines = NumberNines + 1
        If Val(inRoll3.Text) >= 9 Then NumberNines = NumberNines + 1
        If Val(inRoll4.Text) >= 9 Then NumberNines = NumberNines + 1
        If Val(inRoll5.Text) >= 9 Then NumberNines = NumberNines + 1
    End Function

    Function NumberThirties(inScore As Label) As Integer
        NumberThirties = 0
        If Val(inScore.Text) >= 30 Then NumberThirties = 1
    End Function

    Sub update_fixture_details()
        Dim strSQL As String
        Dim strSQL1 As String
        Dim myDataReader As OleDbDataReader    'MySqlDataReader
        strSQL = "DELETE FROM " & objGlobals.CurrentSchema & "vw_fixtures_detail WHERE fixture_id = " & fixture_id
        myDataReader = objGlobals.SQLSelect(strSQL)

        strSQL1 = "INSERT INTO " & objGlobals.CurrentSchema & "fixtures_detail VALUES ('" & objGlobals.current_season & "'," & fixture_id & ",'" & lblLeague.Text & "','" & lblDate.Text & "',GETDATE(), " & FixtureWeek & ","
        strSQL = "1,'" & lblHomeTeam.Text & "','" & lblHomePlayer1.Text & "'," & Val(lblHomeRoll1_1.Text) & "," & Val(lblHomeRoll1_2.Text) & "," & Val(lblHomeRoll1_3.Text) & "," & Val(lblHomeRoll1_4.Text) & "," & Val(lblHomeRoll1_5.Text)
        strSQL = strSQL & "," & Val(lblHomePoints1.Text) & "," & NumberNines(lblHomeRoll1_1, lblHomeRoll1_2, lblHomeRoll1_3, lblHomeRoll1_4, lblHomeRoll1_5) & "," & NumberThirties(lblHomePoints1) & ",'"
        strSQL = strSQL & lblAwayTeam.Text & "','" & lblAwayPlayer1.Text & "'," & Val(lblAwayRoll1_1.Text) & "," & Val(lblAwayRoll1_2.Text) & "," & Val(lblAwayRoll1_3.Text) & "," & Val(lblAwayRoll1_4.Text) & "," & Val(lblAwayRoll1_5.Text)
        strSQL = strSQL & "," & Val(lblAwayPoints1.Text) & "," & NumberNines(lblAwayRoll1_1, lblAwayRoll1_2, lblAwayRoll1_3, lblAwayRoll1_4, lblAwayRoll1_5) & "," & NumberThirties(lblAwayPoints1) & ")"
        myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)

        strSQL1 = "INSERT INTO " & objGlobals.CurrentSchema & "fixtures_detail VALUES ('" & objGlobals.current_season & "'," & fixture_id & ",'" & lblLeague.Text & "','" & lblDate.Text & "',GETDATE(), " & FixtureWeek & ","
        strSQL = "2,'" & lblHomeTeam.Text & "','" & lblHomePlayer2.Text & "'," & Val(lblHomeRoll2_1.Text) & "," & Val(lblHomeRoll2_2.Text) & "," & Val(lblHomeRoll2_3.Text) & "," & Val(lblHomeRoll2_4.Text) & "," & Val(lblHomeRoll2_5.Text)
        strSQL = strSQL & "," & Val(lblHomePoints2.Text) & "," & NumberNines(lblHomeRoll2_1, lblHomeRoll2_2, lblHomeRoll2_3, lblHomeRoll2_4, lblHomeRoll2_5) & "," & NumberThirties(lblHomePoints2) & ",'"
        strSQL = strSQL & lblAwayTeam.Text & "','" & lblAwayPlayer2.Text & "'," & Val(lblAwayRoll2_1.Text) & "," & Val(lblAwayRoll2_2.Text) & "," & Val(lblAwayRoll2_3.Text) & "," & Val(lblAwayRoll2_4.Text) & "," & Val(lblAwayRoll2_5.Text)
        strSQL = strSQL & "," & Val(lblAwayPoints2.Text) & "," & NumberNines(lblAwayRoll2_1, lblAwayRoll2_2, lblAwayRoll2_3, lblAwayRoll2_4, lblAwayRoll2_5) & "," & NumberThirties(lblAwayPoints2) & ")"
        myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)

        strSQL1 = "INSERT INTO " & objGlobals.CurrentSchema & "fixtures_detail VALUES ('" & objGlobals.current_season & "'," & fixture_id & ",'" & lblLeague.Text & "','" & lblDate.Text & "',GETDATE(), " & FixtureWeek & ","
        strSQL = "3,'" & lblHomeTeam.Text & "','" & lblHomePlayer3.Text & "'," & Val(lblHomeRoll3_1.Text) & "," & Val(lblHomeRoll3_2.Text) & "," & Val(lblHomeRoll3_3.Text) & "," & Val(lblHomeRoll3_4.Text) & "," & Val(lblHomeRoll3_5.Text)
        strSQL = strSQL & "," & Val(lblHomePoints3.Text) & "," & NumberNines(lblHomeRoll3_1, lblHomeRoll3_2, lblHomeRoll3_3, lblHomeRoll3_4, lblHomeRoll3_5) & "," & NumberThirties(lblHomePoints3) & ",'"
        strSQL = strSQL & lblAwayTeam.Text & "','" & lblAwayPlayer3.Text & "'," & Val(lblAwayRoll3_1.Text) & "," & Val(lblAwayRoll3_2.Text) & "," & Val(lblAwayRoll3_3.Text) & "," & Val(lblAwayRoll3_4.Text) & "," & Val(lblAwayRoll3_5.Text)
        strSQL = strSQL & "," & Val(lblAwayPoints3.Text) & "," & NumberNines(lblAwayRoll3_1, lblAwayRoll3_2, lblAwayRoll3_3, lblAwayRoll3_4, lblAwayRoll3_5) & "," & NumberThirties(lblAwayPoints3) & ")"
        myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)

        strSQL1 = "INSERT INTO " & objGlobals.CurrentSchema & "fixtures_detail VALUES ('" & objGlobals.current_season & "'," & fixture_id & ",'" & lblLeague.Text & "','" & lblDate.Text & "',GETDATE(), " & FixtureWeek & ","
        strSQL = "4,'" & lblHomeTeam.Text & "','" & lblHomePlayer4.Text & "'," & Val(lblHomeRoll4_1.Text) & "," & Val(lblHomeRoll4_2.Text) & "," & Val(lblHomeRoll4_3.Text) & "," & Val(lblHomeRoll4_4.Text) & "," & Val(lblHomeRoll4_5.Text)
        strSQL = strSQL & "," & Val(lblHomePoints4.Text) & "," & NumberNines(lblHomeRoll4_1, lblHomeRoll4_2, lblHomeRoll4_3, lblHomeRoll4_4, lblHomeRoll4_5) & "," & NumberThirties(lblHomePoints4) & ",'"
        strSQL = strSQL & lblAwayTeam.Text & "','" & lblAwayPlayer4.Text & "'," & Val(lblAwayRoll4_1.Text) & "," & Val(lblAwayRoll4_2.Text) & "," & Val(lblAwayRoll4_3.Text) & "," & Val(lblAwayRoll4_4.Text) & "," & Val(lblAwayRoll4_5.Text)
        strSQL = strSQL & "," & Val(lblAwayPoints4.Text) & "," & NumberNines(lblAwayRoll4_1, lblAwayRoll4_4, lblAwayRoll4_3, lblAwayRoll4_4, lblAwayRoll4_5) & "," & NumberThirties(lblAwayPoints4) & ")"
        myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)

        strSQL1 = "INSERT INTO " & objGlobals.CurrentSchema & "fixtures_detail VALUES ('" & objGlobals.current_season & "'," & fixture_id & ",'" & lblLeague.Text & "','" & lblDate.Text & "',GETDATE(), " & FixtureWeek & ","
        strSQL = "5,'" & lblHomeTeam.Text & "','" & lblHomePlayer5.Text & "'," & Val(lblHomeRoll5_1.Text) & "," & Val(lblHomeRoll5_2.Text) & "," & Val(lblHomeRoll5_3.Text) & "," & Val(lblHomeRoll5_4.Text) & "," & Val(lblHomeRoll5_5.Text)
        strSQL = strSQL & "," & Val(lblHomePoints5.Text) & "," & NumberNines(lblHomeRoll5_1, lblHomeRoll5_2, lblHomeRoll5_3, lblHomeRoll5_4, lblHomeRoll5_5) & "," & NumberThirties(lblHomePoints5) & ",'"
        strSQL = strSQL & lblAwayTeam.Text & "','" & lblAwayPlayer5.Text & "'," & Val(lblAwayRoll5_1.Text) & "," & Val(lblAwayRoll5_2.Text) & "," & Val(lblAwayRoll5_3.Text) & "," & Val(lblAwayRoll5_4.Text) & "," & Val(lblAwayRoll5_5.Text)
        strSQL = strSQL & "," & Val(lblAwayPoints5.Text) & "," & NumberNines(lblAwayRoll5_1, lblAwayRoll5_2, lblAwayRoll5_3, lblAwayRoll5_4, lblAwayRoll5_5) & "," & NumberThirties(lblAwayPoints5) & ")"
        myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)

        strSQL1 = "INSERT INTO " & objGlobals.CurrentSchema & "fixtures_detail VALUES ('" & objGlobals.current_season & "'," & fixture_id & ",'" & lblLeague.Text & "','" & lblDate.Text & "',GETDATE(), " & FixtureWeek & ","
        strSQL = "6,'" & lblHomeTeam.Text & "','" & lblHomePlayer6.Text & "'," & Val(lblHomeRoll6_1.Text) & "," & Val(lblHomeRoll6_2.Text) & "," & Val(lblHomeRoll6_3.Text) & "," & Val(lblHomeRoll6_4.Text) & "," & Val(lblHomeRoll6_5.Text)
        strSQL = strSQL & "," & Val(lblHomePoints6.Text) & "," & NumberNines(lblHomeRoll6_1, lblHomeRoll6_2, lblHomeRoll6_3, lblHomeRoll6_4, lblHomeRoll6_5) & "," & NumberThirties(lblHomePoints6) & ",'"
        strSQL = strSQL & lblAwayTeam.Text & "','" & lblAwayPlayer6.Text & "'," & Val(lblAwayRoll6_1.Text) & "," & Val(lblAwayRoll6_2.Text) & "," & Val(lblAwayRoll6_3.Text) & "," & Val(lblAwayRoll6_4.Text) & "," & Val(lblAwayRoll6_5.Text)
        strSQL = strSQL & "," & Val(lblAwayPoints6.Text) & "," & NumberNines(lblAwayRoll6_1, lblAwayRoll6_2, lblAwayRoll6_3, lblAwayRoll6_4, lblAwayRoll6_5) & "," & NumberThirties(lblAwayPoints6) & ")"
        myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)

        strSQL1 = "INSERT INTO " & objGlobals.CurrentSchema & "fixtures_detail VALUES ('" & objGlobals.current_season & "'," & fixture_id & ",'" & lblLeague.Text & "','" & lblDate.Text & "',GETDATE(), " & FixtureWeek & ","
        strSQL = "7,'" & lblHomeTeam.Text & "','" & lblHomePlayer7.Text & "'," & Val(lblHomeRoll7_1.Text) & "," & Val(lblHomeRoll7_2.Text) & "," & Val(lblHomeRoll7_3.Text) & "," & Val(lblHomeRoll7_4.Text) & "," & Val(lblHomeRoll7_5.Text)
        strSQL = strSQL & "," & Val(lblHomePoints7.Text) & "," & NumberNines(lblHomeRoll7_1, lblHomeRoll7_2, lblHomeRoll7_3, lblHomeRoll7_4, lblHomeRoll7_5) & "," & NumberThirties(lblHomePoints7) & ",'"
        strSQL = strSQL & lblAwayTeam.Text & "','" & lblAwayPlayer7.Text & "'," & Val(lblAwayRoll7_1.Text) & "," & Val(lblAwayRoll7_2.Text) & "," & Val(lblAwayRoll7_3.Text) & "," & Val(lblAwayRoll7_4.Text) & "," & Val(lblAwayRoll7_5.Text)
        strSQL = strSQL & "," & Val(lblAwayPoints7.Text) & "," & NumberNines(lblAwayRoll7_1, lblAwayRoll7_2, lblAwayRoll7_3, lblAwayRoll7_4, lblAwayRoll7_5) & "," & NumberThirties(lblAwayPoints7) & ")"
        myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)

        strSQL1 = "INSERT INTO " & objGlobals.CurrentSchema & "fixtures_detail VALUES ('" & objGlobals.current_season & "'," & fixture_id & ",'" & lblLeague.Text & "','" & lblDate.Text & "',GETDATE(), " & FixtureWeek & ","
        strSQL = "8,'" & lblHomeTeam.Text & "','" & lblHomePlayer8.Text & "'," & Val(lblHomeRoll8_1.Text) & "," & Val(lblHomeRoll8_2.Text) & "," & Val(lblHomeRoll8_3.Text) & "," & Val(lblHomeRoll8_4.Text) & "," & Val(lblHomeRoll8_5.Text)
        strSQL = strSQL & "," & Val(lblHomePoints8.Text) & "," & NumberNines(lblHomeRoll8_1, lblHomeRoll8_2, lblHomeRoll8_3, lblHomeRoll8_4, lblHomeRoll8_5) & "," & NumberThirties(lblHomePoints8) & ",'"
        strSQL = strSQL & lblAwayTeam.Text & "','" & lblAwayPlayer8.Text & "'," & Val(lblAwayRoll8_1.Text) & "," & Val(lblAwayRoll8_2.Text) & "," & Val(lblAwayRoll8_3.Text) & "," & Val(lblAwayRoll8_4.Text) & "," & Val(lblAwayRoll8_5.Text)
        strSQL = strSQL & "," & Val(lblAwayPoints8.Text) & "," & NumberNines(lblAwayRoll8_1, lblAwayRoll8_2, lblAwayRoll8_3, lblAwayRoll8_4, lblAwayRoll8_5) & "," & NumberThirties(lblAwayPoints8) & ")"
        myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)

        strSQL1 = "INSERT INTO " & objGlobals.CurrentSchema & "fixtures_detail VALUES ('" & objGlobals.current_season & "'," & fixture_id & ",'" & lblLeague.Text & "','" & lblDate.Text & "',GETDATE(), " & FixtureWeek & ","
        strSQL = "9,'" & lblHomeTeam.Text & "','" & lblHomePlayer9.Text & "'," & Val(lblHomeRoll9_1.Text) & "," & Val(lblHomeRoll9_2.Text) & "," & Val(lblHomeRoll9_3.Text) & "," & Val(lblHomeRoll9_4.Text) & "," & Val(lblHomeRoll9_5.Text)
        strSQL = strSQL & "," & Val(lblHomePoints9.Text) & "," & NumberNines(lblHomeRoll9_1, lblHomeRoll9_2, lblHomeRoll9_3, lblHomeRoll9_4, lblHomeRoll9_5) & "," & NumberThirties(lblHomePoints9) & ",'"
        strSQL = strSQL & lblAwayTeam.Text & "','" & lblAwayPlayer9.Text & "'," & Val(lblAwayRoll9_1.Text) & "," & Val(lblAwayRoll9_2.Text) & "," & Val(lblAwayRoll9_3.Text) & "," & Val(lblAwayRoll9_4.Text) & "," & Val(lblAwayRoll9_5.Text)
        strSQL = strSQL & "," & Val(lblAwayPoints9.Text) & "," & NumberNines(lblAwayRoll9_1, lblAwayRoll9_2, lblAwayRoll9_3, lblAwayRoll9_4, lblAwayRoll9_5) & "," & NumberThirties(lblAwayPoints9) & ")"
        myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)

        strSQL1 = "INSERT INTO " & objGlobals.CurrentSchema & "fixtures_detail VALUES ('" & objGlobals.current_season & "'," & fixture_id & ",'" & lblLeague.Text & "','" & lblDate.Text & "',GETDATE(), " & FixtureWeek & ","
        strSQL = "10,'" & lblHomeTeam.Text & "','" & lblHomePlayer10.Text & "'," & Val(lblHomeRoll10_1.Text) & "," & Val(lblHomeRoll10_2.Text) & "," & Val(lblHomeRoll10_3.Text) & "," & Val(lblHomeRoll10_4.Text) & "," & Val(lblHomeRoll10_5.Text)
        strSQL = strSQL & "," & Val(lblHomePoints10.Text) & "," & NumberNines(lblHomeRoll10_1, lblHomeRoll10_2, lblHomeRoll10_3, lblHomeRoll10_4, lblHomeRoll10_5) & "," & NumberThirties(lblHomePoints10) & ",'"
        strSQL = strSQL & lblAwayTeam.Text & "','" & lblAwayPlayer10.Text & "'," & Val(lblAwayRoll10_1.Text) & "," & Val(lblAwayRoll10_2.Text) & "," & Val(lblAwayRoll10_3.Text) & "," & Val(lblAwayRoll10_4.Text) & "," & Val(lblAwayRoll10_5.Text)
        strSQL = strSQL & "," & Val(lblAwayPoints10.Text) & "," & NumberNines(lblAwayRoll10_1, lblAwayRoll10_2, lblAwayRoll10_3, lblAwayRoll10_4, lblAwayRoll10_5) & "," & NumberThirties(lblAwayPoints10) & ")"
        myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)

        strSQL1 = "INSERT INTO " & objGlobals.CurrentSchema & "fixtures_detail VALUES ('" & objGlobals.current_season & "'," & fixture_id & ",'" & lblLeague.Text & "','" & lblDate.Text & "',GETDATE(), " & FixtureWeek & ","
        strSQL = "11,'" & lblHomeTeam.Text & "','" & lblHomePlayer11.Text & "'," & Val(lblHomeRoll11_1.Text) & "," & Val(lblHomeRoll11_2.Text) & "," & Val(lblHomeRoll11_3.Text) & "," & Val(lblHomeRoll11_4.Text) & "," & Val(lblHomeRoll11_5.Text)
        strSQL = strSQL & "," & Val(lblHomePoints11.Text) & "," & NumberNines(lblHomeRoll11_1, lblHomeRoll11_2, lblHomeRoll11_3, lblHomeRoll11_4, lblHomeRoll11_5) & "," & NumberThirties(lblHomePoints11) & ",'"
        strSQL = strSQL & lblAwayTeam.Text & "','" & lblAwayPlayer11.Text & "'," & Val(lblAwayRoll11_1.Text) & "," & Val(lblAwayRoll11_2.Text) & "," & Val(lblAwayRoll11_3.Text) & "," & Val(lblAwayRoll11_4.Text) & "," & Val(lblAwayRoll11_5.Text)
        strSQL = strSQL & "," & Val(lblAwayPoints11.Text) & "," & NumberNines(lblAwayRoll11_1, lblAwayRoll11_2, lblAwayRoll11_3, lblAwayRoll11_4, lblAwayRoll11_5) & "," & NumberThirties(lblAwayPoints11) & ")"
        myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)

        strSQL1 = "INSERT INTO " & objGlobals.CurrentSchema & "fixtures_detail VALUES ('" & objGlobals.current_season & "'," & fixture_id & ",'" & lblLeague.Text & "','" & lblDate.Text & "',GETDATE(), " & FixtureWeek & ","
        strSQL = "12,'" & lblHomeTeam.Text & "','" & lblHomePlayer12.Text & "'," & Val(lblHomeRoll12_1.Text) & "," & Val(lblHomeRoll12_2.Text) & "," & Val(lblHomeRoll12_3.Text) & "," & Val(lblHomeRoll12_4.Text) & "," & Val(lblHomeRoll12_5.Text)
        strSQL = strSQL & "," & Val(lblHomePoints12.Text) & "," & NumberNines(lblHomeRoll12_1, lblHomeRoll12_2, lblHomeRoll12_3, lblHomeRoll12_4, lblHomeRoll12_5) & "," & NumberThirties(lblHomePoints12) & ",'"
        strSQL = strSQL & lblAwayTeam.Text & "','" & lblAwayPlayer12.Text & "'," & Val(lblAwayRoll12_1.Text) & "," & Val(lblAwayRoll12_2.Text) & "," & Val(lblAwayRoll12_3.Text) & "," & Val(lblAwayRoll12_4.Text) & "," & Val(lblAwayRoll12_5.Text)
        strSQL = strSQL & "," & Val(lblAwayPoints12.Text) & "," & NumberNines(lblAwayRoll12_1, lblAwayRoll12_2, lblAwayRoll12_3, lblAwayRoll12_4, lblAwayRoll12_5) & "," & NumberThirties(lblAwayPoints12) & ")"
        myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)


        'update the fixture_calendar date FROM " & objGlobals.CurrentSchema & "vw_fixtures
        strSQL = "UPDATE " & objGlobals.CurrentSchema & "vw_fixtures_detail SET fixture_calendar = (SELECT fixture_calendar FROM " & objGlobals.CurrentSchema & "vw_fixtures WHERE fixture_id = " & fixture_id & ") WHERE fixture_id = " & fixture_id
        myDataReader = objGlobals.SQLSelect(strSQL)
    End Sub

    Function check_entries() As Boolean
        check_entries = True
        lstErrors.ClearSelection()
        lstErrors.Items.Clear()
        lstErrors.Visible = False
        If lblHomePlayer1.Text = "." Then lstErrors.Items.Add("No Home Player 1 Entered")
        If lblHomePlayer2.Text = "." Then lstErrors.Items.Add("No Home Player 2 Entered")
        If lblHomePlayer3.Text = "." Then lstErrors.Items.Add("No Home Player 3 Entered")
        If lblHomePlayer4.Text = "." Then lstErrors.Items.Add("No Home Player 4 Entered")
        If lblHomePlayer5.Text = "." Then lstErrors.Items.Add("No Home Player 5 Entered")
        If lblHomePlayer6.Text = "." Then lstErrors.Items.Add("No Home Player 6 Entered")
        If lblAwayPlayer1.Text = "." Then lstErrors.Items.Add("No Away Player 1 Entered")
        If lblAwayPlayer2.Text = "." Then lstErrors.Items.Add("No Away Player 2 Entered")
        If lblAwayPlayer3.Text = "." Then lstErrors.Items.Add("No Away Player 3 Entered")
        If lblAwayPlayer4.Text = "." Then lstErrors.Items.Add("No Away Player 4 Entered")
        If lblAwayPlayer5.Text = "." Then lstErrors.Items.Add("No Away Player 5 Entered")
        If lblAwayPlayer6.Text = "." Then lstErrors.Items.Add("No Away Player 6 Entered")
        If lblHomePlayer7.Text = "." Then lstErrors.Items.Add("No Home Player 7 Entered")
        If lblHomePlayer8.Text = "." Then lstErrors.Items.Add("No Home Player 8 Entered")
        If lblHomePlayer9.Text = "." Then lstErrors.Items.Add("No Home Player 9 Entered")
        If lblHomePlayer10.Text = "." Then lstErrors.Items.Add("No Home Player 10 Entered")
        If lblHomePlayer11.Text = "." Then lstErrors.Items.Add("No Home Player 11 Entered")
        If lblHomePlayer12.Text = "." Then lstErrors.Items.Add("No Home Player 12 Entered")
        If lblAwayPlayer7.Text = "." Then lstErrors.Items.Add("No Away Player 7 Entered")
        If lblAwayPlayer8.Text = "." Then lstErrors.Items.Add("No Away Player 8 Entered")
        If lblAwayPlayer9.Text = "." Then lstErrors.Items.Add("No Away Player 9 Entered")
        If lblAwayPlayer10.Text = "." Then lstErrors.Items.Add("No Away Player 10 Entered")
        If lblAwayPlayer11.Text = "." Then lstErrors.Items.Add("No Away Player 11 Entered")
        If lblAwayPlayer12.Text = "." Then lstErrors.Items.Add("No Away Player 12 Entered")

        If lblHomePoints1.Text = "" Then lstErrors.Items.Add("No Home Points for Player 1 Entered")
        If lblHomePoints2.Text = "" Then lstErrors.Items.Add("No Home Points for Player 2 Entered")
        If lblHomePoints3.Text = "" Then lstErrors.Items.Add("No Home Points for Player 3 Entered")
        If lblHomePoints4.Text = "" Then lstErrors.Items.Add("No Home Points for Player 4 Entered")
        If lblHomePoints5.Text = "" Then lstErrors.Items.Add("No Home Points for Player 5 Entered")
        If lblHomePoints6.Text = "" Then lstErrors.Items.Add("No Home Points for Player 6 Entered")
        If lblAwayPoints1.Text = "" Then lstErrors.Items.Add("No Away Points for Player 1 Entered")
        If lblAwayPoints2.Text = "" Then lstErrors.Items.Add("No Away Points for Player 2 Entered")
        If lblAwayPoints3.Text = "" Then lstErrors.Items.Add("No Away Points for Player 3 Entered")
        If lblAwayPoints4.Text = "" Then lstErrors.Items.Add("No Away Points for Player 4 Entered")
        If lblAwayPoints5.Text = "" Then lstErrors.Items.Add("No Away Points for Player 5 Entered")
        If lblAwayPoints6.Text = "" Then lstErrors.Items.Add("No Away Points for Player 6 Entered")
        If lblHomePoints7.Text = "" Then lstErrors.Items.Add("No Home Points for Player 7 Entered")
        If lblHomePoints8.Text = "" Then lstErrors.Items.Add("No Home Points for Player 8 Entered")
        If lblHomePoints9.Text = "" Then lstErrors.Items.Add("No Home Points for Player 9 Entered")
        If lblHomePoints10.Text = "" Then lstErrors.Items.Add("No Home Points for Player 10 Entered")
        If lblHomePoints11.Text = "" Then lstErrors.Items.Add("No Home Points for Player 11 Entered")
        If lblHomePoints12.Text = "" Then lstErrors.Items.Add("No Home Points for Player 12 Entered")
        If lblAwayPoints7.Text = "" Then lstErrors.Items.Add("No Away Points for Player 7 Entered")
        If lblAwayPoints8.Text = "" Then lstErrors.Items.Add("No Away Points for Player 8 Entered")
        If lblAwayPoints9.Text = "" Then lstErrors.Items.Add("No Away Points for Player 9 Entered")
        If lblAwayPoints10.Text = "" Then lstErrors.Items.Add("No Away Points for Player 10 Entered")
        If lblAwayPoints11.Text = "" Then lstErrors.Items.Add("No Away Points for Player 11 Entered")
        If lblAwayPoints12.Text = "" Then lstErrors.Items.Add("No Away Points for Player 12 Entered")

        If ddResult.SelectedValue = "0 - 0" Then
            lstErrors.Items.Add("No Match Result Entered")
        End If

        If lstErrors.Items.Count > 0 Then
            lstErrors.Visible = True
            check_entries = False
        End If
    End Function

    Protected Sub btnAdd1_Click(sender As Object, e As System.EventArgs) Handles btnAdd1.Click
        'add new player to home team
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader    'MySqlDataReader
        Dim Found As Boolean = False
        'see if player already exists first
        lblHomeExists.Visible = False
        strSQL = "SELECT player FROM " & objGlobals.CurrentSchema & "vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblHomeTeam.Text & "' AND player = '" & txtAddHomePlayer.Text & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            Found = True
        End While
        If Found Then
            lblHomeExists.Visible = True
            Exit Sub
        End If
        txtAddHomePlayer.Text = UCase(txtAddHomePlayer.Text)
        strSQL = "INSERT INTO " & objGlobals.CurrentSchema & "players VALUES ('" & objGlobals.current_season & "',"
        strSQL = strSQL & "'" & lblLeague.Text & "',"
        strSQL = strSQL & "'" & lblHomeTeam.Text & "',"
        strSQL = strSQL & "'" & txtAddHomePlayer.Text & "',NULL,0,0,0,0,0,0,0,0,0,0,0,0,0,0)"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If Not lblHomeAvailPlayer1.Visible Or lblHomeAvailPlayer1.Text = "." Then lblHomeAvailPlayer1.Visible = True : lblHomeAvailPlayer1.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer1.ForeColor = Red : btAvailRight1.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer2.Visible Or lblHomeAvailPlayer2.Text = "." Then lblHomeAvailPlayer2.Visible = True : lblHomeAvailPlayer2.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer2.ForeColor = Red : btAvailRight2.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer3.Visible Or lblHomeAvailPlayer3.Text = "." Then lblHomeAvailPlayer3.Visible = True : lblHomeAvailPlayer3.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer3.ForeColor = Red : btAvailRight3.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer4.Visible Or lblHomeAvailPlayer4.Text = "." Then lblHomeAvailPlayer4.Visible = True : lblHomeAvailPlayer4.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer4.ForeColor = Red : btAvailRight4.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer5.Visible Or lblHomeAvailPlayer5.Text = "." Then lblHomeAvailPlayer5.Visible = True : lblHomeAvailPlayer5.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer5.ForeColor = Red : btAvailRight5.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer6.Visible Or lblHomeAvailPlayer6.Text = "." Then lblHomeAvailPlayer6.Visible = True : lblHomeAvailPlayer6.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer6.ForeColor = Red : btAvailRight6.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer7.Visible Or lblHomeAvailPlayer7.Text = "." Then lblHomeAvailPlayer7.Visible = True : lblHomeAvailPlayer7.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer7.ForeColor = Red : btAvailRight7.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer8.Visible Or lblHomeAvailPlayer8.Text = "." Then lblHomeAvailPlayer8.Visible = True : lblHomeAvailPlayer8.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer8.ForeColor = Red : btAvailRight8.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer9.Visible Or lblHomeAvailPlayer9.Text = "." Then lblHomeAvailPlayer9.Visible = True : lblHomeAvailPlayer9.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer9.ForeColor = Red : btAvailRight9.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer10.Visible Or lblHomeAvailPlayer10.Text = "." Then lblHomeAvailPlayer10.Visible = True : lblHomeAvailPlayer10.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer10.ForeColor = Red : btAvailRight10.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer11.Visible Or lblHomeAvailPlayer11.Text = "." Then lblHomeAvailPlayer11.Visible = True : lblHomeAvailPlayer11.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer11.ForeColor = Red : btAvailRight11.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer12.Visible Or lblHomeAvailPlayer12.Text = "." Then lblHomeAvailPlayer12.Visible = True : lblHomeAvailPlayer12.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer12.ForeColor = Red : btAvailRight12.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer13.Visible Or lblHomeAvailPlayer13.Text = "." Then lblHomeAvailPlayer13.Visible = True : lblHomeAvailPlayer13.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer13.ForeColor = Red : btAvailRight13.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer14.Visible Or lblHomeAvailPlayer14.Text = "." Then lblHomeAvailPlayer14.Visible = True : lblHomeAvailPlayer14.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer14.ForeColor = Red : btAvailRight14.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer15.Visible Or lblHomeAvailPlayer15.Text = "." Then lblHomeAvailPlayer15.Visible = True : lblHomeAvailPlayer15.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer15.ForeColor = Red : btAvailRight15.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer16.Visible Or lblHomeAvailPlayer16.Text = "." Then lblHomeAvailPlayer16.Visible = True : lblHomeAvailPlayer16.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer16.ForeColor = Red : btAvailRight16.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer17.Visible Or lblHomeAvailPlayer17.Text = "." Then lblHomeAvailPlayer17.Visible = True : lblHomeAvailPlayer17.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer17.ForeColor = Red : btAvailRight17.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer18.Visible Or lblHomeAvailPlayer18.Text = "." Then lblHomeAvailPlayer18.Visible = True : lblHomeAvailPlayer18.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer18.ForeColor = Red : btAvailRight18.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer19.Visible Or lblHomeAvailPlayer19.Text = "." Then lblHomeAvailPlayer19.Visible = True : lblHomeAvailPlayer19.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer19.ForeColor = Red : btAvailRight19.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer20.Visible Or lblHomeAvailPlayer20.Text = "." Then lblHomeAvailPlayer20.Visible = True : lblHomeAvailPlayer20.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer20.ForeColor = Red : btAvailRight20.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer21.Visible Or lblHomeAvailPlayer21.Text = "." Then lblHomeAvailPlayer21.Visible = True : lblHomeAvailPlayer21.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer21.ForeColor = Red : btAvailRight21.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer22.Visible Or lblHomeAvailPlayer22.Text = "." Then lblHomeAvailPlayer22.Visible = True : lblHomeAvailPlayer22.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer22.ForeColor = Red : btAvailRight22.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer23.Visible Or lblHomeAvailPlayer23.Text = "." Then lblHomeAvailPlayer23.Visible = True : lblHomeAvailPlayer23.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer23.ForeColor = Red : btAvailRight23.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer24.Visible Or lblHomeAvailPlayer24.Text = "." Then lblHomeAvailPlayer24.Visible = True : lblHomeAvailPlayer24.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer24.ForeColor = Red : btAvailRight24.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer25.Visible Or lblHomeAvailPlayer25.Text = "." Then lblHomeAvailPlayer25.Visible = True : lblHomeAvailPlayer25.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer25.ForeColor = Red : btAvailRight25.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer26.Visible Or lblHomeAvailPlayer26.Text = "." Then lblHomeAvailPlayer26.Visible = True : lblHomeAvailPlayer26.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer26.ForeColor = Red : btAvailRight26.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer27.Visible Or lblHomeAvailPlayer27.Text = "." Then lblHomeAvailPlayer27.Visible = True : lblHomeAvailPlayer27.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer27.ForeColor = Red : btAvailRight27.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer28.Visible Or lblHomeAvailPlayer28.Text = "." Then lblHomeAvailPlayer28.Visible = True : lblHomeAvailPlayer28.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer28.ForeColor = Red : btAvailRight28.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer29.Visible Or lblHomeAvailPlayer29.Text = "." Then lblHomeAvailPlayer29.Visible = True : lblHomeAvailPlayer29.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer29.ForeColor = Red : btAvailRight29.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer30.Visible Or lblHomeAvailPlayer30.Text = "." Then lblHomeAvailPlayer30.Visible = True : lblHomeAvailPlayer30.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer30.ForeColor = Red : btAvailRight30.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer31.Visible Or lblHomeAvailPlayer31.Text = "." Then lblHomeAvailPlayer31.Visible = True : lblHomeAvailPlayer31.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer31.ForeColor = Red : btAvailRight31.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer32.Visible Or lblHomeAvailPlayer32.Text = "." Then lblHomeAvailPlayer32.Visible = True : lblHomeAvailPlayer32.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer32.ForeColor = Red : btAvailRight32.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer33.Visible Or lblHomeAvailPlayer33.Text = "." Then lblHomeAvailPlayer33.Visible = True : lblHomeAvailPlayer33.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer33.ForeColor = Red : btAvailRight33.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer34.Visible Or lblHomeAvailPlayer34.Text = "." Then lblHomeAvailPlayer34.Visible = True : lblHomeAvailPlayer34.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer34.ForeColor = Red : btAvailRight34.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer35.Visible Or lblHomeAvailPlayer35.Text = "." Then lblHomeAvailPlayer35.Visible = True : lblHomeAvailPlayer35.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer35.ForeColor = Red : btAvailRight35.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer36.Visible Or lblHomeAvailPlayer36.Text = "." Then lblHomeAvailPlayer36.Visible = True : lblHomeAvailPlayer36.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer36.ForeColor = Red : btAvailRight36.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer37.Visible Or lblHomeAvailPlayer37.Text = "." Then lblHomeAvailPlayer37.Visible = True : lblHomeAvailPlayer37.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer37.ForeColor = Red : btAvailRight37.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer38.Visible Or lblHomeAvailPlayer38.Text = "." Then lblHomeAvailPlayer38.Visible = True : lblHomeAvailPlayer38.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer38.ForeColor = Red : btAvailRight38.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        If Not lblHomeAvailPlayer39.Visible Or lblHomeAvailPlayer39.Text = "." Then lblHomeAvailPlayer39.Visible = True : lblHomeAvailPlayer39.Text = txtAddHomePlayer.Text : lblHomeAvailPlayer39.ForeColor = Red : btAvailRight39.Visible = True : txtAddHomePlayer.Text = "" : Exit Sub
        txtAddHomePlayer.Focus()
    End Sub

    Protected Sub btnAdd2_Click(sender As Object, e As System.EventArgs) Handles btnAdd2.Click
        'add new player to Away team
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader    'MySqlDataReader
        Dim Found As Boolean = False
        lblAwayExists.Visible = False
        'see if player already exists first
        strSQL = "SELECT player FROM " & objGlobals.CurrentSchema & "vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblAwayTeam.Text & "' AND player = '" & txtAddAwayPlayer.Text & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            Found = True
        End While
        If Found Then
            lblAwayExists.Visible = True
            Exit Sub
        End If
        txtAddAwayPlayer.Text = UCase(txtAddAwayPlayer.Text)
        strSQL = "INSERT INTO " & objGlobals.CurrentSchema & "players VALUES ('" & objGlobals.current_season & "',"
        strSQL = strSQL & "'" & lblLeague.Text & "',"
        strSQL = strSQL & "'" & lblAwayTeam.Text & "',"
        strSQL = strSQL & "'" & txtAddAwayPlayer.Text & "',NULL,0,0,0,0,0,0,0,0,0,0,0,0,0,0)"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If Not lblAwayAvailPlayer1.Visible Or lblAwayAvailPlayer1.Text = "." Then lblAwayAvailPlayer1.Visible = True : lblAwayAvailPlayer1.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer1.ForeColor = Red : btAvailLeft1.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer2.Visible Or lblAwayAvailPlayer2.Text = "." Then lblAwayAvailPlayer2.Visible = True : lblAwayAvailPlayer2.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer2.ForeColor = Red : btAvailLeft2.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer3.Visible Or lblAwayAvailPlayer3.Text = "." Then lblAwayAvailPlayer3.Visible = True : lblAwayAvailPlayer3.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer3.ForeColor = Red : btAvailLeft3.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer4.Visible Or lblAwayAvailPlayer4.Text = "." Then lblAwayAvailPlayer4.Visible = True : lblAwayAvailPlayer4.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer4.ForeColor = Red : btAvailLeft4.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer5.Visible Or lblAwayAvailPlayer5.Text = "." Then lblAwayAvailPlayer5.Visible = True : lblAwayAvailPlayer5.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer5.ForeColor = Red : btAvailLeft5.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer6.Visible Or lblAwayAvailPlayer6.Text = "." Then lblAwayAvailPlayer6.Visible = True : lblAwayAvailPlayer6.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer6.ForeColor = Red : btAvailLeft6.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer7.Visible Or lblAwayAvailPlayer7.Text = "." Then lblAwayAvailPlayer7.Visible = True : lblAwayAvailPlayer7.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer7.ForeColor = Red : btAvailLeft7.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer8.Visible Or lblAwayAvailPlayer8.Text = "." Then lblAwayAvailPlayer8.Visible = True : lblAwayAvailPlayer8.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer8.ForeColor = Red : btAvailLeft8.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer9.Visible Or lblAwayAvailPlayer9.Text = "." Then lblAwayAvailPlayer9.Visible = True : lblAwayAvailPlayer9.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer9.ForeColor = Red : btAvailLeft9.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer10.Visible Or lblAwayAvailPlayer10.Text = "." Then lblAwayAvailPlayer10.Visible = True : lblAwayAvailPlayer10.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer10.ForeColor = Red : btAvailLeft10.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer11.Visible Or lblAwayAvailPlayer11.Text = "." Then lblAwayAvailPlayer11.Visible = True : lblAwayAvailPlayer11.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer11.ForeColor = Red : btAvailLeft11.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer12.Visible Or lblAwayAvailPlayer12.Text = "." Then lblAwayAvailPlayer12.Visible = True : lblAwayAvailPlayer12.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer12.ForeColor = Red : btAvailLeft12.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer13.Visible Or lblAwayAvailPlayer13.Text = "." Then lblAwayAvailPlayer13.Visible = True : lblAwayAvailPlayer13.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer13.ForeColor = Red : btAvailLeft13.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer14.Visible Or lblAwayAvailPlayer14.Text = "." Then lblAwayAvailPlayer14.Visible = True : lblAwayAvailPlayer14.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer14.ForeColor = Red : btAvailLeft14.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer15.Visible Or lblAwayAvailPlayer15.Text = "." Then lblAwayAvailPlayer15.Visible = True : lblAwayAvailPlayer15.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer15.ForeColor = Red : btAvailLeft15.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer16.Visible Or lblAwayAvailPlayer16.Text = "." Then lblAwayAvailPlayer16.Visible = True : lblAwayAvailPlayer16.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer16.ForeColor = Red : btAvailLeft16.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer17.Visible Or lblAwayAvailPlayer17.Text = "." Then lblAwayAvailPlayer17.Visible = True : lblAwayAvailPlayer17.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer17.ForeColor = Red : btAvailLeft17.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer18.Visible Or lblAwayAvailPlayer18.Text = "." Then lblAwayAvailPlayer18.Visible = True : lblAwayAvailPlayer18.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer18.ForeColor = Red : btAvailLeft18.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer19.Visible Or lblAwayAvailPlayer19.Text = "." Then lblAwayAvailPlayer19.Visible = True : lblAwayAvailPlayer19.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer19.ForeColor = Red : btAvailLeft19.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer20.Visible Or lblAwayAvailPlayer20.Text = "." Then lblAwayAvailPlayer20.Visible = True : lblAwayAvailPlayer20.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer20.ForeColor = Red : btAvailLeft20.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer21.Visible Or lblAwayAvailPlayer21.Text = "." Then lblAwayAvailPlayer21.Visible = True : lblAwayAvailPlayer21.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer21.ForeColor = Red : btAvailLeft21.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer22.Visible Or lblAwayAvailPlayer22.Text = "." Then lblAwayAvailPlayer22.Visible = True : lblAwayAvailPlayer22.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer22.ForeColor = Red : btAvailLeft22.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer23.Visible Or lblAwayAvailPlayer23.Text = "." Then lblAwayAvailPlayer23.Visible = True : lblAwayAvailPlayer23.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer23.ForeColor = Red : btAvailLeft23.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer24.Visible Or lblAwayAvailPlayer24.Text = "." Then lblAwayAvailPlayer24.Visible = True : lblAwayAvailPlayer24.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer24.ForeColor = Red : btAvailLeft24.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer25.Visible Or lblAwayAvailPlayer25.Text = "." Then lblAwayAvailPlayer25.Visible = True : lblAwayAvailPlayer25.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer25.ForeColor = Red : btAvailLeft25.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer26.Visible Or lblAwayAvailPlayer26.Text = "." Then lblAwayAvailPlayer26.Visible = True : lblAwayAvailPlayer26.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer26.ForeColor = Red : btAvailLeft26.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer27.Visible Or lblAwayAvailPlayer27.Text = "." Then lblAwayAvailPlayer27.Visible = True : lblAwayAvailPlayer27.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer27.ForeColor = Red : btAvailLeft27.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer28.Visible Or lblAwayAvailPlayer28.Text = "." Then lblAwayAvailPlayer28.Visible = True : lblAwayAvailPlayer28.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer28.ForeColor = Red : btAvailLeft28.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer29.Visible Or lblAwayAvailPlayer29.Text = "." Then lblAwayAvailPlayer29.Visible = True : lblAwayAvailPlayer29.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer29.ForeColor = Red : btAvailLeft29.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer30.Visible Or lblAwayAvailPlayer30.Text = "." Then lblAwayAvailPlayer30.Visible = True : lblAwayAvailPlayer30.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer30.ForeColor = Red : btAvailLeft30.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer31.Visible Or lblAwayAvailPlayer31.Text = "." Then lblAwayAvailPlayer31.Visible = True : lblAwayAvailPlayer31.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer31.ForeColor = Red : btAvailLeft31.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer32.Visible Or lblAwayAvailPlayer32.Text = "." Then lblAwayAvailPlayer32.Visible = True : lblAwayAvailPlayer32.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer32.ForeColor = Red : btAvailLeft32.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer33.Visible Or lblAwayAvailPlayer33.Text = "." Then lblAwayAvailPlayer33.Visible = True : lblAwayAvailPlayer33.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer33.ForeColor = Red : btAvailLeft33.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer34.Visible Or lblAwayAvailPlayer34.Text = "." Then lblAwayAvailPlayer34.Visible = True : lblAwayAvailPlayer34.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer34.ForeColor = Red : btAvailLeft34.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer35.Visible Or lblAwayAvailPlayer35.Text = "." Then lblAwayAvailPlayer35.Visible = True : lblAwayAvailPlayer35.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer35.ForeColor = Red : btAvailLeft35.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer36.Visible Or lblAwayAvailPlayer36.Text = "." Then lblAwayAvailPlayer36.Visible = True : lblAwayAvailPlayer36.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer36.ForeColor = Red : btAvailLeft36.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer37.Visible Or lblAwayAvailPlayer37.Text = "." Then lblAwayAvailPlayer37.Visible = True : lblAwayAvailPlayer37.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer37.ForeColor = Red : btAvailLeft37.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer38.Visible Or lblAwayAvailPlayer38.Text = "." Then lblAwayAvailPlayer38.Visible = True : lblAwayAvailPlayer38.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer38.ForeColor = Red : btAvailLeft38.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        If Not lblAwayAvailPlayer39.Visible Or lblAwayAvailPlayer39.Text = "." Then lblAwayAvailPlayer39.Visible = True : lblAwayAvailPlayer39.Text = txtAddAwayPlayer.Text : lblAwayAvailPlayer39.ForeColor = Red : btAvailLeft39.Visible = True : txtAddAwayPlayer.Text = "" : Exit Sub
        txtAddAwayPlayer.Focus()
    End Sub

    Protected Function process_role(inPlayer As Label, inScore As TextBox, inRoll1 As Label, inRoll2 As Label, inRoll3 As Label, inRoll4 As Label, inRoll5 As Label, inTotal As Label, Optional inNextScore As TextBox = Nothing, Optional inNextPlayer As Label = Nothing) As Boolean
        process_role = True
        Dim R1 As String = Mid(UCase(inScore.Text), 1, 1)
        Dim R2 As String = Mid(UCase(inScore.Text), 2, 1)
        Dim R3 As String = Mid(UCase(inScore.Text), 3, 1)
        Dim R4 As String = Mid(UCase(inScore.Text), 4, 1)
        Dim R5 As String = Mid(UCase(inScore.Text), 5, 1)
        inRoll1.Text = R1
        inRoll2.Text = R2
        inRoll3.Text = R3
        inRoll4.Text = R4
        inRoll5.Text = R5
        inRoll1.BackColor = Black
        inRoll2.BackColor = Black
        inRoll3.BackColor = Black
        inRoll4.BackColor = Black
        inRoll5.BackColor = Black

        If InStr("ABCDEFGHIJ", R1) > 0 Then
            inRoll1.Text = "1" + Convert.ToChar(Asc(R1) - 17)
            inRoll1.BackColor = DarkCyan
        ElseIf Not IsNumeric(R1) Then
            inRoll1.BackColor = Red
            process_role = False
        ElseIf R1 = "9" Then
            inRoll1.BackColor = DarkCyan
        End If
        If InStr("ABCDEFGHIJ", R2) > 0 Then
            inRoll2.Text = "1" + Convert.ToChar(Asc(R2) - 17)
            inRoll2.BackColor = DarkCyan
        ElseIf Not IsNumeric(R2) Then
            inRoll2.BackColor = Red
            process_role = False
        ElseIf R2 = "9" Then
            inRoll2.BackColor = DarkCyan
        End If
        If InStr("ABCDEFGHIJ", R3) > 0 Then
            inRoll3.Text = "1" + Convert.ToChar(Asc(R3) - 17)
            inRoll3.BackColor = DarkCyan
        ElseIf Not IsNumeric(R3) Then
            inRoll3.BackColor = Red
            process_role = False
        ElseIf R3 = "9" Then
            inRoll3.BackColor = DarkCyan
        End If
        If InStr("ABCDEFGHIJ", R4) > 0 Then
            inRoll4.Text = "1" + Convert.ToChar(Asc(R4) - 17)
            inRoll4.BackColor = DarkCyan
        ElseIf Not IsNumeric(R4) Then
            inRoll4.BackColor = Red
            process_role = False
        ElseIf R4 = "9" Then
            inRoll4.BackColor = DarkCyan
        End If
        If InStr("ABCDEFGHIJ", R5) > 0 Then
            inRoll5.Text = "1" + Convert.ToChar(Asc(R5) - 17)
            inRoll5.BackColor = DarkCyan
        ElseIf Not IsNumeric(R5) Then
            inRoll5.BackColor = Red
            process_role = False
        ElseIf R5 = "9" Then
            inRoll5.BackColor = DarkCyan
        End If

        If Not process_role Then
            inPlayer.BackColor = Red
            inTotal.Text = ""
            inTotal.BackColor = Red
            inScore.BackColor = Red
            inScore.Focus()
        Else
            inPlayer.BackColor = Black
            inScore.Text = ""
            inScore.BackColor = Black
            If Not (inNextPlayer Is Nothing) Then inNextScore.BackColor = Blue
            inTotal.Text = Val(R1) + Val(R2) + Val(R3) + Val(R4) + Val(R5)
            If Val(inTotal.Text) >= 30 Then
                inTotal.BackColor = DarkCyan
            Else
                inTotal.BackColor = Green
            End If
            If Not (inNextPlayer Is Nothing) Then
                inNextPlayer.BackColor = Blue
                inNextScore.Focus()
            Else
                calc_totals()
                btnUpdate.Focus()
            End If
        End If
    End Function

    Protected Sub txtHomeRolls1_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRolls1.TextChanged
        If Len(txtHomeRolls1.Text) <> 5 Then txtHomeRolls1.BackColor = Red : txtHomeRolls1.Focus() : Exit Sub
        process_role(lblHomePlayer1, txtHomeRolls1, lblHomeRoll1_1, lblHomeRoll1_2, lblHomeRoll1_3, lblHomeRoll1_4, lblHomeRoll1_5, lblHomePoints1, txtHomeRolls2, lblHomePlayer2)
    End Sub

    Protected Sub txtHomeRolls2_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRolls2.TextChanged
        If Len(txtHomeRolls2.Text) <> 5 Then txtHomeRolls2.BackColor = Red : txtHomeRolls2.Focus() : Exit Sub
        process_role(lblHomePlayer2, txtHomeRolls2, lblHomeRoll2_1, lblHomeRoll2_2, lblHomeRoll2_3, lblHomeRoll2_4, lblHomeRoll2_5, lblHomePoints2, txtHomeRolls3, lblHomePlayer3)
    End Sub

    Protected Sub txtHomeRolls3_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRolls3.TextChanged
        If Len(txtHomeRolls3.Text) <> 5 Then txtHomeRolls3.BackColor = Red : txtHomeRolls3.Focus() : Exit Sub
        process_role(lblHomePlayer3, txtHomeRolls3, lblHomeRoll3_1, lblHomeRoll3_2, lblHomeRoll3_3, lblHomeRoll3_4, lblHomeRoll3_5, lblHomePoints3, txtHomeRolls4, lblHomePlayer4)
    End Sub

    Protected Sub txtHomeRolls4_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRolls4.TextChanged
        If Len(txtHomeRolls4.Text) <> 5 Then txtHomeRolls4.BackColor = Red : txtHomeRolls4.Focus() : Exit Sub
        process_role(lblHomePlayer4, txtHomeRolls4, lblHomeRoll4_1, lblHomeRoll4_2, lblHomeRoll4_3, lblHomeRoll4_4, lblHomeRoll4_5, lblHomePoints4, txtHomeRolls5, lblHomePlayer5)
    End Sub

    Protected Sub txtHomeRolls5_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRolls5.TextChanged
        If Len(txtHomeRolls5.Text) <> 5 Then txtHomeRolls5.BackColor = Red : txtHomeRolls5.Focus() : Exit Sub
        process_role(lblHomePlayer5, txtHomeRolls5, lblHomeRoll5_1, lblHomeRoll5_2, lblHomeRoll5_3, lblHomeRoll5_4, lblHomeRoll5_5, lblHomePoints5, txtHomeRolls6, lblHomePlayer6)
    End Sub

    Protected Sub txtHomeRolls6_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRolls6.TextChanged
        If Len(txtHomeRolls6.Text) <> 5 Then txtHomeRolls6.BackColor = Red : txtHomeRolls6.Focus() : Exit Sub
        process_role(lblHomePlayer6, txtHomeRolls6, lblHomeRoll6_1, lblHomeRoll6_2, lblHomeRoll6_3, lblHomeRoll6_4, lblHomeRoll6_5, lblHomePoints6, txtHomeRolls7, lblHomePlayer7)
    End Sub

    Protected Sub txtHomeRolls7_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRolls7.TextChanged
        If Len(txtHomeRolls7.Text) <> 5 Then txtHomeRolls7.BackColor = Red : txtHomeRolls7.Focus() : Exit Sub
        process_role(lblHomePlayer7, txtHomeRolls7, lblHomeRoll7_1, lblHomeRoll7_2, lblHomeRoll7_3, lblHomeRoll7_4, lblHomeRoll7_5, lblHomePoints7, txtHomeRolls8, lblHomePlayer8)
    End Sub

    Protected Sub txtHomeRolls8_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRolls8.TextChanged
        If Len(txtHomeRolls8.Text) <> 5 Then txtHomeRolls8.BackColor = Red : txtHomeRolls8.Focus() : Exit Sub
        process_role(lblHomePlayer8, txtHomeRolls8, lblHomeRoll8_1, lblHomeRoll8_2, lblHomeRoll8_3, lblHomeRoll8_4, lblHomeRoll8_5, lblHomePoints8, txtHomeRolls9, lblHomePlayer9)
    End Sub

    Protected Sub txtHomeRolls9_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRolls9.TextChanged
        If Len(txtHomeRolls9.Text) <> 5 Then txtHomeRolls9.BackColor = Red : txtHomeRolls9.Focus() : Exit Sub
        process_role(lblHomePlayer9, txtHomeRolls9, lblHomeRoll9_1, lblHomeRoll9_2, lblHomeRoll9_3, lblHomeRoll9_4, lblHomeRoll9_5, lblHomePoints9, txtHomeRolls10, lblHomePlayer10)
    End Sub

    Protected Sub txtHomeRolls10_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRolls10.TextChanged
        If Len(txtHomeRolls10.Text) <> 5 Then txtHomeRolls10.BackColor = Red : txtHomeRolls10.Focus() : Exit Sub
        process_role(lblHomePlayer10, txtHomeRolls10, lblHomeRoll10_1, lblHomeRoll10_2, lblHomeRoll10_3, lblHomeRoll10_4, lblHomeRoll10_5, lblHomePoints10, txtHomeRolls11, lblHomePlayer11)
    End Sub

    Protected Sub txtHomeRolls11_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRolls11.TextChanged
        If Len(txtHomeRolls11.Text) <> 5 Then txtHomeRolls11.BackColor = Red : txtHomeRolls11.Focus() : Exit Sub
        process_role(lblHomePlayer11, txtHomeRolls11, lblHomeRoll11_1, lblHomeRoll11_2, lblHomeRoll11_3, lblHomeRoll11_4, lblHomeRoll11_5, lblHomePoints11, txtHomeRolls12, lblHomePlayer12)
    End Sub

    Protected Sub txtHomeRolls12_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomeRolls12.TextChanged
        If Len(txtHomeRolls12.Text) <> 5 Then txtHomeRolls12.BackColor = Red : txtHomeRolls12.Focus() : Exit Sub
        process_role(lblHomePlayer12, txtHomeRolls12, lblHomeRoll12_1, lblHomeRoll12_2, lblHomeRoll12_3, lblHomeRoll12_4, lblHomeRoll12_5, lblHomePoints12, txtAwayRolls1, lblAwayPlayer1)
    End Sub

    Protected Sub txtAwayRolls1_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRolls1.TextChanged
        If Len(txtAwayRolls1.Text) <> 5 Then txtAwayRolls1.BackColor = Red : txtAwayRolls1.Focus() : Exit Sub
        process_role(lblAwayPlayer1, txtAwayRolls1, lblAwayRoll1_1, lblAwayRoll1_2, lblAwayRoll1_3, lblAwayRoll1_4, lblAwayRoll1_5, lblAwayPoints1, txtAwayRolls2, lblAwayPlayer2)
    End Sub

    Protected Sub txtAwayRolls2_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRolls2.TextChanged
        If Len(txtAwayRolls2.Text) <> 5 Then txtAwayRolls2.BackColor = Red : txtAwayRolls2.Focus() : Exit Sub
        process_role(lblAwayPlayer2, txtAwayRolls2, lblAwayRoll2_1, lblAwayRoll2_2, lblAwayRoll2_3, lblAwayRoll2_4, lblAwayRoll2_5, lblAwayPoints2, txtAwayRolls3, lblAwayPlayer3)
    End Sub

    Protected Sub txtAwayRolls3_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRolls3.TextChanged
        If Len(txtAwayRolls3.Text) <> 5 Then txtAwayRolls3.BackColor = Red : txtAwayRolls3.Focus() : Exit Sub
        process_role(lblAwayPlayer3, txtAwayRolls3, lblAwayRoll3_1, lblAwayRoll3_2, lblAwayRoll3_3, lblAwayRoll3_4, lblAwayRoll3_5, lblAwayPoints3, txtAwayRolls4, lblAwayPlayer4)
    End Sub

    Protected Sub txtAwayRolls4_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRolls4.TextChanged
        If Len(txtAwayRolls4.Text) <> 5 Then txtAwayRolls4.BackColor = Red : txtAwayRolls4.Focus() : Exit Sub
        process_role(lblAwayPlayer4, txtAwayRolls4, lblAwayRoll4_1, lblAwayRoll4_2, lblAwayRoll4_3, lblAwayRoll4_4, lblAwayRoll4_5, lblAwayPoints4, txtAwayRolls5, lblAwayPlayer5)
    End Sub

    Protected Sub txtAwayRolls5_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRolls5.TextChanged
        If Len(txtAwayRolls5.Text) <> 5 Then txtAwayRolls5.BackColor = Red : txtAwayRolls5.Focus() : Exit Sub
        process_role(lblAwayPlayer5, txtAwayRolls5, lblAwayRoll5_1, lblAwayRoll5_2, lblAwayRoll5_3, lblAwayRoll5_4, lblAwayRoll5_5, lblAwayPoints5, txtAwayRolls6, lblAwayPlayer6)
    End Sub

    Protected Sub txtAwayRolls6_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRolls6.TextChanged
        If Len(txtAwayRolls6.Text) <> 5 Then txtAwayRolls6.BackColor = Red : txtAwayRolls6.Focus() : Exit Sub
        process_role(lblAwayPlayer6, txtAwayRolls6, lblAwayRoll6_1, lblAwayRoll6_2, lblAwayRoll6_3, lblAwayRoll6_4, lblAwayRoll6_5, lblAwayPoints6, txtAwayRolls7, lblAwayPlayer7)
    End Sub

    Protected Sub txtAwayRolls7_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRolls7.TextChanged
        If Len(txtAwayRolls7.Text) <> 5 Then txtAwayRolls7.BackColor = Red : txtAwayRolls7.Focus() : Exit Sub
        process_role(lblAwayPlayer7, txtAwayRolls7, lblAwayRoll7_1, lblAwayRoll7_2, lblAwayRoll7_3, lblAwayRoll7_4, lblAwayRoll7_5, lblAwayPoints7, txtAwayRolls8, lblAwayPlayer8)
    End Sub

    Protected Sub txtAwayRolls8_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRolls8.TextChanged
        If Len(txtAwayRolls8.Text) <> 5 Then txtAwayRolls8.BackColor = Red : txtAwayRolls8.Focus() : Exit Sub
        process_role(lblAwayPlayer8, txtAwayRolls8, lblAwayRoll8_1, lblAwayRoll8_2, lblAwayRoll8_3, lblAwayRoll8_4, lblAwayRoll8_5, lblAwayPoints8, txtAwayRolls9, lblAwayPlayer9)
    End Sub

    Protected Sub txtAwayRolls9_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRolls9.TextChanged
        If Len(txtAwayRolls9.Text) <> 5 Then txtAwayRolls9.BackColor = Red : txtAwayRolls9.Focus() : Exit Sub
        process_role(lblAwayPlayer9, txtAwayRolls9, lblAwayRoll9_1, lblAwayRoll9_2, lblAwayRoll9_3, lblAwayRoll9_4, lblAwayRoll9_5, lblAwayPoints9, txtAwayRolls10, lblAwayPlayer10)
    End Sub

    Protected Sub txtAwayRolls10_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRolls10.TextChanged
        If Len(txtAwayRolls10.Text) <> 5 Then txtAwayRolls10.BackColor = Red : txtAwayRolls10.Focus() : Exit Sub
        process_role(lblAwayPlayer10, txtAwayRolls10, lblAwayRoll10_1, lblAwayRoll10_2, lblAwayRoll10_3, lblAwayRoll10_4, lblAwayRoll10_5, lblAwayPoints10, txtAwayRolls11, lblAwayPlayer11)
    End Sub

    Protected Sub txtAwayRolls11_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRolls11.TextChanged
        If Len(txtAwayRolls11.Text) <> 5 Then txtAwayRolls11.BackColor = Red : txtAwayRolls11.Focus() : Exit Sub
        process_role(lblAwayPlayer11, txtAwayRolls11, lblAwayRoll11_1, lblAwayRoll11_2, lblAwayRoll11_3, lblAwayRoll11_4, lblAwayRoll11_5, lblAwayPoints11, txtAwayRolls12, lblAwayPlayer12)
    End Sub

    Protected Sub txtAwayRolls12_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayRolls12.TextChanged
        If Len(txtAwayRolls12.Text) <> 5 Then txtAwayRolls12.BackColor = Red : txtAwayRolls12.Focus() : Exit Sub
        process_role(lblAwayPlayer12, txtAwayRolls12, lblAwayRoll12_1, lblAwayRoll12_2, lblAwayRoll12_3, lblAwayRoll12_4, lblAwayRoll12_5, lblAwayPoints12)
    End Sub


    Protected Sub btnCalc_Click(sender As Object, e As System.EventArgs) Handles btnCalc.Click
        calc_totals()
    End Sub

    Protected Sub btnRandom_Click(sender As Object, e As System.EventArgs) Handles btnRandom.Click
        btAvailRight1_Click(Me, e)
        btAvailRight2_Click(Me, e)
        btAvailRight3_Click(Me, e)
        btAvailRight4_Click(Me, e)
        btAvailRight5_Click(Me, e)
        btAvailRight6_Click(Me, e)
        btAvailRight7_Click(Me, e)
        btAvailRight8_Click(Me, e)
        btAvailRight9_Click(Me, e)
        btAvailRight10_Click(Me, e)
        btAvailRight11_Click(Me, e)
        btAvailRight12_Click(Me, e)
        btAvailLeft1_Click(Me, e)
        btAvailLeft2_Click(Me, e)
        btAvailLeft3_Click(Me, e)
        btAvailLeft4_Click(Me, e)
        btAvailLeft5_Click(Me, e)
        btAvailLeft6_Click(Me, e)
        btAvailLeft7_Click(Me, e)
        btAvailLeft8_Click(Me, e)
        btAvailLeft9_Click(Me, e)
        btAvailLeft10_Click(Me, e)
        btAvailLeft11_Click(Me, e)
        btAvailLeft12_Click(Me, e)
        Dim RandomNumber = New Random()
        txtHomeRolls1.BackColor = Blue
        txtHomeRolls1.Text = CStr(RandomNumber.Next(10000, 99999)) : txtHomeRolls1_TextChanged(Me, e)
        txtHomeRolls2.Text = CStr(RandomNumber.Next(10000, 99999)) : txtHomeRolls2_TextChanged(Me, e)
        txtHomeRolls3.Text = CStr(RandomNumber.Next(10000, 99999)) : txtHomeRolls3_TextChanged(Me, e)
        txtHomeRolls4.Text = CStr(RandomNumber.Next(10000, 99999)) : txtHomeRolls4_TextChanged(Me, e)
        txtHomeRolls5.Text = CStr(RandomNumber.Next(10000, 99999)) : txtHomeRolls5_TextChanged(Me, e)
        txtHomeRolls6.Text = CStr(RandomNumber.Next(10000, 99999)) : txtHomeRolls6_TextChanged(Me, e)
        txtHomeRolls7.Text = CStr(RandomNumber.Next(10000, 99999)) : txtHomeRolls7_TextChanged(Me, e)
        txtHomeRolls8.Text = CStr(RandomNumber.Next(10000, 99999)) : txtHomeRolls8_TextChanged(Me, e)
        txtHomeRolls9.Text = CStr(RandomNumber.Next(10000, 99999)) : txtHomeRolls9_TextChanged(Me, e)
        txtHomeRolls10.Text = CStr(RandomNumber.Next(10000, 99999)) : txtHomeRolls10_TextChanged(Me, e)
        txtHomeRolls11.Text = CStr(RandomNumber.Next(10000, 99999)) : txtHomeRolls11_TextChanged(Me, e)
        txtHomeRolls12.Text = CStr(RandomNumber.Next(10000, 99999)) : txtHomeRolls12_TextChanged(Me, e)
        txtAwayRolls1.Text = CStr(RandomNumber.Next(10000, 99999)) : txtAwayRolls1_TextChanged(Me, e)
        txtAwayRolls2.Text = CStr(RandomNumber.Next(10000, 99999)) : txtAwayRolls2_TextChanged(Me, e)
        txtAwayRolls3.Text = CStr(RandomNumber.Next(10000, 99999)) : txtAwayRolls3_TextChanged(Me, e)
        txtAwayRolls4.Text = CStr(RandomNumber.Next(10000, 99999)) : txtAwayRolls4_TextChanged(Me, e)
        txtAwayRolls5.Text = CStr(RandomNumber.Next(10000, 99999)) : txtAwayRolls5_TextChanged(Me, e)
        txtAwayRolls6.Text = CStr(RandomNumber.Next(10000, 99999)) : txtAwayRolls6_TextChanged(Me, e)
        txtAwayRolls7.Text = CStr(RandomNumber.Next(10000, 99999)) : txtAwayRolls7_TextChanged(Me, e)
        txtAwayRolls8.Text = CStr(RandomNumber.Next(10000, 99999)) : txtAwayRolls8_TextChanged(Me, e)
        txtAwayRolls9.Text = CStr(RandomNumber.Next(10000, 99999)) : txtAwayRolls9_TextChanged(Me, e)
        txtAwayRolls10.Text = CStr(RandomNumber.Next(10000, 99999)) : txtAwayRolls10_TextChanged(Me, e)
        txtAwayRolls11.Text = CStr(RandomNumber.Next(10000, 99999)) : txtAwayRolls11_TextChanged(Me, e)
        txtAwayRolls12.Text = CStr(RandomNumber.Next(10000, 99999)) : txtAwayRolls12_TextChanged(Me, e)
    End Sub
End Class

