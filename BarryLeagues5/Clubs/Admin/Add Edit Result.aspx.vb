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


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("clubs")
        objGlobals.CurrentUser = "clubs_user"
        objGlobals.CurrentSchema = "clubs."

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
            lblHomePlayersSelected.Visible = False
            lblAwayPlayersSelected.Visible = False
            lblHomeScore.Visible = False
            lblAwayScore.Visible = False
            lblNines.Visible = False
            lblHomeTotal.Visible = False
            lblAwayTotal.Visible = False
            btnUpdate.Visible = False
            lblReset.Visible = False
            btnReset.Visible = False
            txtHomePoints1.Visible = False
            txtAwayPoints1.Visible = False
            txtHomePoints2.Visible = False
            txtAwayPoints2.Visible = False
            txtHomePoints3.Visible = False
            txtAwayPoints3.Visible = False
            txtHomePoints4.Visible = False
            txtAwayPoints4.Visible = False
            txtHomePoints5.Visible = False
            txtAwayPoints5.Visible = False
            txtHomePoints6.Visible = False
            txtAwayPoints6.Visible = False
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
            txtHomePoints7.Visible = False
            txtAwayPoints7.Visible = False
            txtHomePoints8.Visible = False
            txtAwayPoints8.Visible = False
            txtHomePoints9.Visible = False
            txtAwayPoints9.Visible = False
            txtHomePoints10.Visible = False
            txtAwayPoints10.Visible = False
            txtHomePoints11.Visible = False
            txtAwayPoints11.Visible = False
            txtHomePoints12.Visible = False
            txtAwayPoints12.Visible = False
            txtAwayNines1.Visible = False
            txtAwayNines2.Visible = False
            txtAwayNines3.Visible = False
            txtAwayNines4.Visible = False
            txtAwayNines5.Visible = False
            txtAwayNines6.Visible = False
            txtAwayNines7.Visible = False
            txtAwayNines8.Visible = False
            txtAwayNines9.Visible = False
            txtAwayNines10.Visible = False
            txtAwayNines11.Visible = False
            txtAwayNines12.Visible = False
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
            txtHomePoints0.Visible = False
            txtAwayPoints0.Visible = False
            Call objGlobals.store_page("ATTACK FROM " & Request.Url.OriginalString, objGlobals.AdminLogin)
            Exit Sub
        End If
        fixture_id = Request.QueryString("ID")
        FixtureWeek = Request.QueryString("Week")
        TeamSelected = Request.QueryString("Team")
        If Not IsPostBack Then
            Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
            Call load_result()
            Call load_result_deductions()
            Call load_details()
            Call load_available_players()
            Call calc_totals()
            rbResults.Visible = False
        End If
    End Sub

    Protected Sub load_result()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        strSQL = "SELECT * FROM clubs.vw_fixtures WHERE fixture_id = " & fixture_id
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            lblDate.Text = myDataReader.Item("fixture_date")
            lblLeague.Text = myDataReader.Item("league")
            lblHomeTeam.Text = myDataReader.Item("home_team_name")
            lblAwayTeam.Text = myDataReader.Item("away_team_name")
            lblResult.Text = Replace(myDataReader.Item("home_result"), " ", "")
            HomePointsDeducted = myDataReader.Item("home_points_deducted")
            AwayPointsDeducted = myDataReader.Item("away_points_deducted")
            lblID.Text = fixture_id
        End While
        objGlobals.close_connection()

        If TeamSelected Is Nothing Then
            btnReset.PostBackUrl = "~/Clubs/Admin/Fixture Result.aspx?ID=" & fixture_id & "&Week=" & FixtureWeek
        Else
            btnReset.PostBackUrl = "~/Clubs/Admin/Fixture Result.aspx?ID=" & fixture_id & "&Week=" & FixtureWeek & "&League=" & lblLeague.Text & "&Team=" & TeamSelected
        End If
        Select Case Left(lblLeague.Text, 4)
            Case "CRIB" : btnCribAutoScores.Visible = True
            Case "SKIT" : btnSkittlesAutoScores.Visible = True
            Case "SNOO" : btnSnookerAutoScores.Visible = True
        End Select
    End Sub

    Protected Sub btnCribAutoScores_Click(sender As Object, e As System.EventArgs) Handles btnCribAutoScores.Click
        Call enable_crib_scores()
    End Sub

    Protected Sub btnSkittlesAutoScores_Click(sender As Object, e As System.EventArgs) Handles btnSkittlesAutoScores.Click
        Call enable_skittles_scores()
    End Sub

    Protected Sub btnSnookerAutoScores_Click(sender As Object, e As System.EventArgs) Handles btnSnookerAutoScores.Click
        Call enable_snooker_scores()
    End Sub

    Sub load_available_players()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim AvailCount As Integer = 0
        Dim EmptyAvail As Integer = 0
        If SelectedHomePlayers = "" Then
            strSQL = "SELECT player FROM clubs.vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblHomeTeam.Text & "' ORDER BY Player"
        Else
            strSQL = "SELECT player FROM clubs.vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblHomeTeam.Text & "' AND player NOT IN (" & SelectedHomePlayers & ") ORDER BY Player"
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
        objGlobals.close_connection()

        'get the number of players selected and add blanks as available for them
        If SelectedHomePlayers <> "" Then
            strSQL = "SELECT COUNT(Player) FROM clubs.vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblHomeTeam.Text & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            While myDataReader.Read()
                EmptyAvail = myDataReader.Item(0) - AvailCount
            End While
            objGlobals.close_connection()

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
            strSQL = "SELECT player FROM clubs.vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblAwayTeam.Text & "' ORDER BY Player"
        Else
            strSQL = "SELECT player FROM clubs.vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblAwayTeam.Text & "' AND player NOT IN (" & SelectedAwayPlayers & ") ORDER BY Player"
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
        objGlobals.close_connection()

        'get the number of players selected and add blanks as available for them
        If SelectedAwayPlayers <> "" Then
            strSQL = "SELECT COUNT(Player) FROM clubs.vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblAwayTeam.Text & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            While myDataReader.Read()
                EmptyAvail = myDataReader.Item(0) - AvailCount
            End While
            objGlobals.close_connection()

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
        Dim myDataReader As oledbdatareader
        Dim MatchNo As Integer = 0
        If Left(lblLeague.Text, 4) = "CRIB" Then ' hide points 2,4,6
            txtHomePoints2.Text = "" : txtHomePoints2.Enabled = False : txtHomePoints2.BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33) : txtHomePoints2.BorderColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
            txtHomePoints4.Text = "" : txtHomePoints4.Enabled = False : txtHomePoints4.BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33) : txtHomePoints4.BorderColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
            txtHomePoints6.Text = "" : txtHomePoints6.Enabled = False : txtHomePoints6.BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33) : txtHomePoints6.BorderColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
            txtAwayPoints2.Text = "" : txtAwayPoints2.Enabled = False : txtAwayPoints2.BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33) : txtAwayPoints2.BorderColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
            txtAwayPoints4.Text = "" : txtAwayPoints4.Enabled = False : txtAwayPoints4.BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33) : txtAwayPoints4.BorderColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
            txtAwayPoints6.Text = "" : txtAwayPoints6.Enabled = False : txtAwayPoints6.BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33) : txtAwayPoints6.BorderColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
        End If
        If Left(lblLeague.Text, 4) <> "SKIT" Then
            lblNines.Visible = False
        End If
        SelectedHomePlayers = ""
        SelectedAwayPlayers = ""
        strSQL = "SELECT * FROM clubs.vw_fixtures_detail WHERE fixture_id = " & fixture_id & " ORDER BY match"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            MatchNo = MatchNo + 1
            Select Case MatchNo
                Case 1
                    lblHomePlayer1.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & "'" & myDataReader.Item("home_player") & "'"
                    txtHomePoints1.Text = myDataReader.Item("home_points")
                    lblAwayPlayer1.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & "'" & myDataReader.Item("away_player") & "'"
                    txtAwayPoints1.Text = myDataReader.Item("away_points")
                    If Not IsDBNull(myDataReader.Item("away_nines")) Then
                        txtAwayNines1.Text = myDataReader.Item("away_nines")
                    End If
                    If Left(lblLeague.Text, 4) = "CRIB" Then
                        lblHomePlayer2.Text = myDataReader.Item("home_partner") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_partner") & "'"
                        lblAwayPlayer2.Text = myDataReader.Item("away_partner") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_partner") & "'"
                    End If
                Case 2
                    If Left(lblLeague.Text, 4) = "CRIB" Then
                        lblHomePlayer3.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                        txtHomePoints3.Text = myDataReader.Item("home_points")
                        lblAwayPlayer3.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                        txtAwayPoints3.Text = myDataReader.Item("away_points")
                        lblHomePlayer4.Text = myDataReader.Item("home_partner") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_partner") & "'"
                        lblAwayPlayer4.Text = myDataReader.Item("away_partner") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_partner") & "'"
                    Else
                        lblHomePlayer2.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                        txtHomePoints2.Text = myDataReader.Item("home_points")
                        lblAwayPlayer2.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                        txtAwayPoints2.Text = myDataReader.Item("away_points")
                        If Not IsDBNull(myDataReader.Item("away_nines")) Then
                            txtAwayNines2.Text = myDataReader.Item("away_nines")
                        End If
                    End If
                Case 3
                    If Left(lblLeague.Text, 4) = "CRIB" Then
                        lblHomePlayer5.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                        txtHomePoints5.Text = myDataReader.Item("home_points")
                        lblAwayPlayer5.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                        txtAwayPoints5.Text = myDataReader.Item("away_points")
                        lblHomePlayer6.Text = myDataReader.Item("home_partner") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_partner") & "'"
                        lblAwayPlayer6.Text = myDataReader.Item("away_partner") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_partner") & "'"
                    Else
                        lblHomePlayer3.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                        txtHomePoints3.Text = myDataReader.Item("home_points")
                        lblAwayPlayer3.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                        txtAwayPoints3.Text = myDataReader.Item("away_points")
                        If Not IsDBNull(myDataReader.Item("away_nines")) Then
                            txtAwayNines3.Text = myDataReader.Item("away_nines")
                        End If
                    End If
                Case 4
                    lblHomePlayer4.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    txtHomePoints4.Text = myDataReader.Item("home_points")
                    lblAwayPlayer4.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    txtAwayPoints4.Text = myDataReader.Item("away_points")
                    If Not IsDBNull(myDataReader.Item("away_nines")) Then
                        txtAwayNines4.Text = myDataReader.Item("away_nines")
                    End If
                Case 5
                    lblHomePlayer5.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    txtHomePoints5.Text = myDataReader.Item("home_points")
                    lblAwayPlayer5.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    txtAwayPoints5.Text = myDataReader.Item("away_points")
                    If Not IsDBNull(myDataReader.Item("away_nines")) Then
                        txtAwayNines5.Text = myDataReader.Item("away_nines")
                    End If
                Case 6
                    lblHomePlayer6.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    txtHomePoints6.Text = myDataReader.Item("home_points")
                    lblAwayPlayer6.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    txtAwayPoints6.Text = myDataReader.Item("away_points")
                    If Not IsDBNull(myDataReader.Item("away_nines")) Then
                        txtAwayNines6.Text = myDataReader.Item("away_nines")
                    End If
                Case 7
                    lblHomePlayer7.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    txtHomePoints7.Text = myDataReader.Item("home_points")
                    lblAwayPlayer7.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    txtAwayPoints7.Text = myDataReader.Item("away_points")
                    If Not IsDBNull(myDataReader.Item("away_nines")) Then
                        txtAwayNines7.Text = myDataReader.Item("away_nines")
                    End If
                Case 8
                    lblHomePlayer8.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    txtHomePoints8.Text = myDataReader.Item("home_points")
                    lblAwayPlayer8.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    txtAwayPoints8.Text = myDataReader.Item("away_points")
                    If Not IsDBNull(myDataReader.Item("away_nines")) Then
                        txtAwayNines8.Text = myDataReader.Item("away_nines")
                    End If
                Case 9
                    lblHomePlayer9.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    txtHomePoints9.Text = myDataReader.Item("home_points")
                    lblAwayPlayer9.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    txtAwayPoints9.Text = myDataReader.Item("away_points")
                    If Not IsDBNull(myDataReader.Item("away_nines")) Then
                        txtAwayNines9.Text = myDataReader.Item("away_nines")
                    End If
                Case 10
                    lblHomePlayer10.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    txtHomePoints10.Text = myDataReader.Item("home_points")
                    lblAwayPlayer10.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    txtAwayPoints10.Text = myDataReader.Item("away_points")
                    If Not IsDBNull(myDataReader.Item("away_nines")) Then
                        txtAwayNines10.Text = myDataReader.Item("away_nines")
                    End If
                Case 11
                    lblHomePlayer11.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    txtHomePoints11.Text = myDataReader.Item("home_points")
                    lblAwayPlayer11.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    txtAwayPoints11.Text = myDataReader.Item("away_points")
                    If Not IsDBNull(myDataReader.Item("away_nines")) Then
                        txtAwayNines11.Text = myDataReader.Item("away_nines")
                    End If
                Case 12
                    lblHomePlayer12.Text = myDataReader.Item("home_player") : SelectedHomePlayers = SelectedHomePlayers & ",'" & myDataReader.Item("home_player") & "'"
                    txtHomePoints12.Text = myDataReader.Item("home_points")
                    lblAwayPlayer12.Text = myDataReader.Item("away_player") : SelectedAwayPlayers = SelectedAwayPlayers & ",'" & myDataReader.Item("away_player") & "'"
                    txtAwayPoints12.Text = myDataReader.Item("away_points")
                    If Not IsDBNull(myDataReader.Item("away_nines")) Then
                        txtAwayNines12.Text = myDataReader.Item("away_nines")
                    End If
            End Select
        End While
        objGlobals.close_connection()

        'no matches ?
        If MatchNo = 0 Then
            btLeft1.Visible = False : btLeft2.Visible = False : btLeft3.Visible = False : btLeft4.Visible = False : btLeft5.Visible = False : btLeft6.Visible = False : btLeft7.Visible = False : btLeft8.Visible = False : btLeft9.Visible = False : btLeft10.Visible = False : btLeft11.Visible = False : btLeft12.Visible = False
            btRight1.Visible = False : btRight2.Visible = False : btRight3.Visible = False : btRight4.Visible = False : btRight5.Visible = False : btRight6.Visible = False : btRight7.Visible = False : btRight8.Visible = False : btRight9.Visible = False : btRight10.Visible = False : btRight11.Visible = False : btRight12.Visible = False
        End If
    End Sub
    Sub load_result_deductions()
        Dim home_result As String
        home_result = Replace(lblResult.Text, " ", "")
        Select Case Left(lblLeague.Text, 4)
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


                End With
                With ddSkittlesResult
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
                    .Visible = True
                End With
                lblSkittlesResults.Visible = True
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
                End With
                Dim i As Integer
                For i = 0 To 15
                    ddHomePointsDeducted.Items.Add(i)
                    ddAwayPointsDeducted.Items.Add(i)
                Next
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
                txtHomePoints7.Visible = False
                txtAwayPoints7.Visible = False
                txtHomePoints8.Visible = False
                txtAwayPoints8.Visible = False
                txtHomePoints9.Visible = False
                txtAwayPoints9.Visible = False
                txtHomePoints10.Visible = False
                txtAwayPoints10.Visible = False
                txtHomePoints11.Visible = False
                txtAwayPoints11.Visible = False
                txtHomePoints12.Visible = False
                txtAwayPoints12.Visible = False
                txtAwayNines1.Visible = False
                txtAwayNines2.Visible = False
                txtAwayNines3.Visible = False
                txtAwayNines4.Visible = False
                txtAwayNines5.Visible = False
                txtAwayNines6.Visible = False
                txtAwayNines7.Visible = False
                txtAwayNines8.Visible = False
                txtAwayNines9.Visible = False
                txtAwayNines10.Visible = False
                txtAwayNines11.Visible = False
                txtAwayNines12.Visible = False
                btLeft7.Visible = False
                btLeft8.Visible = False
                btLeft9.Visible = False
                btLeft10.Visible = False
                btLeft11.Visible = False
                btLeft12.Visible = False
                btRight7.Visible = False
                btRight8.Visible = False
                btRight9.Visible = False
                btRight10.Visible = False
                btRight11.Visible = False
                btRight12.Visible = False
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
                End With
                Dim i As Integer
                For i = 0 To 5
                    ddHomePointsDeducted.Items.Add(i)
                    ddAwayPointsDeducted.Items.Add(i)
                Next
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
                txtHomePoints6.Visible = False
                txtAwayPoints6.Visible = False
                txtHomePoints7.Visible = False
                txtAwayPoints7.Visible = False
                txtHomePoints8.Visible = False
                txtAwayPoints8.Visible = False
                txtHomePoints9.Visible = False
                txtAwayPoints9.Visible = False
                txtHomePoints10.Visible = False
                txtAwayPoints10.Visible = False
                txtHomePoints11.Visible = False
                txtAwayPoints11.Visible = False
                txtHomePoints12.Visible = False
                txtAwayPoints12.Visible = False
                txtAwayNines1.Visible = False
                txtAwayNines2.Visible = False
                txtAwayNines3.Visible = False
                txtAwayNines4.Visible = False
                txtAwayNines5.Visible = False
                txtAwayNines6.Visible = False
                txtAwayNines7.Visible = False
                txtAwayNines8.Visible = False
                txtAwayNines9.Visible = False
                txtAwayNines10.Visible = False
                txtAwayNines11.Visible = False
                txtAwayNines12.Visible = False
                btLeft6.Visible = False
                btLeft7.Visible = False
                btLeft8.Visible = False
                btLeft9.Visible = False
                btLeft10.Visible = False
                btLeft11.Visible = False
                btLeft12.Visible = False
                btRight6.Visible = False
                btRight7.Visible = False
                btRight8.Visible = False
                btRight9.Visible = False
                btRight10.Visible = False
                btRight11.Visible = False
                btRight12.Visible = False
        End Select
        For i = 0 To ddHomePointsDeducted.Items.Count - 1
            If ddHomePointsDeducted.Items(i).Value = HomePointsDeducted Then ddHomePointsDeducted.SelectedIndex = i
        Next
        For i = 0 To ddAwayPointsDeducted.Items.Count - 1
            If ddAwayPointsDeducted.Items(i).Value = AwayPointsDeducted Then ddAwayPointsDeducted.SelectedIndex = i
        Next
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




    Protected Sub txtHomePoints1_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomePoints1.TextChanged
        Select Case Left(lblLeague.Text, 4)
            Case "SNOO"
                If txtHomePoints1.Text <> "1" And txtHomePoints1.Text <> "0" Then
                    txtHomePoints1.Text = ""
                    txtHomePoints1.Focus()
                Else
                    If txtHomePoints1.Text = "1" Then
                        txtAwayPoints1.Text = "0"
                    Else
                        txtAwayPoints1.Text = "1"
                    End If
                    calc_totals()
                    txtHomePoints2.Focus()
                End If
            Case "CRIB"
                If Val(txtHomePoints1.Text) < 0 Or Val(txtHomePoints1.Text) > 5 Then
                    txtHomePoints1.Text = ""
                    txtHomePoints1.Focus()
                Else
                    txtAwayPoints1.Text = CStr(5 - Val(txtHomePoints1.Text))
                    calc_totals()
                    txtHomePoints3.Focus()
                End If
            Case "SKIT"
                calc_totals()
                txtAwayPoints1.Focus()
        End Select
    End Sub

    Protected Sub txtHomePoints2_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomePoints2.TextChanged
        Select Case Left(lblLeague.Text, 4)
            Case "SNOO"
                If txtHomePoints2.Text <> "1" And txtHomePoints2.Text <> "0" Then
                    txtHomePoints2.Text = ""
                    txtHomePoints2.Focus()
                Else
                    If txtHomePoints2.Text = "1" Then
                        txtAwayPoints2.Text = "0"
                    Else
                        txtAwayPoints2.Text = "1"
                    End If
                    calc_totals()
                    txtHomePoints3.Focus()
                End If
            Case "SKIT"
                calc_totals()
                txtAwayPoints2.Focus()
        End Select
    End Sub

    Protected Sub txtHomePoints3_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomePoints3.TextChanged
        Select Case Left(lblLeague.Text, 4)
            Case "SNOO"
                If txtHomePoints3.Text <> "1" And txtHomePoints3.Text <> "0" Then
                    txtHomePoints3.Text = ""
                    txtHomePoints3.Focus()
                Else
                    If txtHomePoints3.Text = "1" Then
                        txtAwayPoints3.Text = "0"
                    Else
                        txtAwayPoints3.Text = "1"
                    End If
                    calc_totals()
                    txtHomePoints4.Focus()
                End If
            Case "CRIB"
                If Val(txtHomePoints3.Text) < 0 Or Val(txtHomePoints3.Text) > 5 Then
                    txtHomePoints3.Text = ""
                    txtHomePoints3.Focus()
                Else
                    txtAwayPoints3.Text = CStr(5 - Val(txtHomePoints3.Text))
                    calc_totals()
                    txtHomePoints5.Focus()
                End If
            Case "SKIT"
                calc_totals()
                txtAwayPoints3.Focus()
        End Select
    End Sub

    Protected Sub txtHomePoints4_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomePoints4.TextChanged
        Select Case Left(lblLeague.Text, 4)
            Case "SNOO"
                If txtHomePoints4.Text <> "1" And txtHomePoints4.Text <> "0" Then
                    txtHomePoints4.Text = ""
                    txtHomePoints4.Focus()
                Else
                    If txtHomePoints4.Text = "1" Then
                        txtAwayPoints4.Text = "0"
                    Else
                        txtAwayPoints4.Text = "1"
                    End If
                    calc_totals()
                    txtHomePoints5.Focus()
                End If
            Case "SKIT"
                calc_totals()
                txtAwayPoints4.Focus()
        End Select
    End Sub

    Protected Sub txtHomePoints5_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomePoints5.TextChanged
        Select Case Left(lblLeague.Text, 4)
            Case "SNOO"
                If txtHomePoints5.Text <> "1" And txtHomePoints5.Text <> "0" Then
                    txtHomePoints5.Text = ""
                    txtHomePoints5.Focus()
                Else
                    If txtHomePoints5.Text = "1" Then
                        txtAwayPoints5.Text = "0"
                    Else
                        txtAwayPoints5.Text = "1"
                    End If
                    calc_totals()
                End If
            Case "CRIB"
                If Val(txtHomePoints5.Text) < 0 Or Val(txtHomePoints5.Text) > 5 Then
                    txtHomePoints5.Text = ""
                    txtHomePoints5.Focus()
                Else
                    txtAwayPoints5.Text = CStr(5 - Val(txtHomePoints5.Text))
                    calc_totals()
                End If
            Case "SKIT"
                calc_totals()
                txtAwayPoints5.Focus()
        End Select
    End Sub


    Protected Sub txtHomePoints6_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomePoints6.TextChanged
        calc_totals()
        txtAwayPoints6.Focus()
    End Sub
    Protected Sub txtHomePoints7_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomePoints7.TextChanged
        calc_totals()
        txtAwayPoints7.Focus()
    End Sub
    Protected Sub txtHomePoints8_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomePoints8.TextChanged
        calc_totals()
        txtAwayPoints8.Focus()
    End Sub
    Protected Sub txtHomePoints9_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomePoints9.TextChanged
        calc_totals()
        txtAwayPoints9.Focus()
    End Sub
    Protected Sub txtHomePoints10_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomePoints10.TextChanged
        calc_totals()
        txtAwayPoints10.Focus()
    End Sub
    Protected Sub txtHomePoints11_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomePoints11.TextChanged
        calc_totals()
        txtAwayPoints11.Focus()
    End Sub
    Protected Sub txtHomePoints12_TextChanged(sender As Object, e As System.EventArgs) Handles txtHomePoints12.TextChanged
        calc_totals()
        txtAwayPoints12.Focus()
    End Sub


    Protected Sub txtAwayPoints1_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayPoints1.TextChanged
        Select Case Left(lblLeague.Text, 4)
            Case "SNOO"
                If txtAwayPoints1.Text <> "1" And txtAwayPoints1.Text <> "0" Then
                    txtAwayPoints1.Text = ""
                    txtAwayPoints1.Focus()
                Else
                    If txtAwayPoints1.Text = "1" Then
                        txtHomePoints1.Text = "0"
                    Else
                        txtHomePoints1.Text = "1"
                    End If
                    calc_totals()
                    txtHomePoints2.Focus()
                End If
            Case "CRIB"
                If Val(txtAwayPoints1.Text) < 0 Or Val(txtAwayPoints1.Text) > 5 Then
                    txtAwayPoints1.Text = ""
                    txtHomePoints3.Focus()
                Else
                    txtHomePoints1.Text = CStr(5 - Val(txtAwayPoints1.Text))
                    calc_totals()
                    txtHomePoints3.Focus()
                End If
            Case "SKIT"
                calc_totals()
                txtHomePoints2.Focus()
        End Select
    End Sub

    Protected Sub txtAwayPoints2_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayPoints2.TextChanged
        Select Case Left(lblLeague.Text, 4)
            Case "SNOO"
                If txtAwayPoints2.Text <> "1" And txtAwayPoints2.Text <> "0" Then
                    txtAwayPoints2.Text = ""
                    txtAwayPoints2.Focus()
                Else
                    If txtAwayPoints2.Text = "1" Then
                        txtHomePoints2.Text = "0"
                    Else
                        txtHomePoints2.Text = "1"
                    End If
                    calc_totals()
                    txtHomePoints3.Focus()
                End If
            Case "SKIT"
                calc_totals()
                txtHomePoints3.Focus()
        End Select
    End Sub

    Protected Sub txtAwayPoints3_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayPoints3.TextChanged
        Select Case Left(lblLeague.Text, 4)
            Case "SNOO"
                If txtAwayPoints3.Text <> "1" And txtAwayPoints3.Text <> "0" Then
                    txtAwayPoints3.Text = ""
                    txtAwayPoints3.Focus()
                Else
                    If txtAwayPoints3.Text = "1" Then
                        txtHomePoints3.Text = "0"
                    Else
                        txtHomePoints3.Text = "1"
                    End If
                    calc_totals()
                    txtHomePoints4.Focus()
                End If
            Case "CRIB"
                If Val(txtAwayPoints3.Text) < 0 Or Val(txtAwayPoints3.Text) > 5 Then
                    txtAwayPoints3.Text = ""
                    txtAwayPoints3.Focus()
                Else
                    txtHomePoints3.Text = CStr(5 - Val(txtAwayPoints3.Text))
                    calc_totals()
                    txtHomePoints5.Focus()
                End If
            Case "SKIT"
                calc_totals()
                txtHomePoints4.Focus()
        End Select
    End Sub

    Protected Sub txtAwayPoints4_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayPoints4.TextChanged
        Select Case Left(lblLeague.Text, 4)
            Case "SNOO"
                If txtAwayPoints4.Text <> "1" And txtAwayPoints4.Text <> "0" Then
                    txtAwayPoints4.Text = ""
                    txtAwayPoints4.Focus()
                Else
                    If txtAwayPoints4.Text = "1" Then
                        txtHomePoints4.Text = "0"
                    Else
                        txtHomePoints4.Text = "1"
                    End If
                    calc_totals()
                    txtHomePoints5.Focus()
                End If
            Case "SKIT"
                calc_totals()
                txtHomePoints5.Focus()
        End Select
    End Sub

    Protected Sub txtAwayPoints5_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayPoints5.TextChanged
        Select Case Left(lblLeague.Text, 4)
            Case "SNOO"
                If txtAwayPoints5.Text <> "1" And txtAwayPoints5.Text <> "0" Then
                    txtAwayPoints5.Text = ""
                    txtAwayPoints5.Focus()
                Else
                    If txtAwayPoints5.Text = "1" Then
                        txtHomePoints5.Text = "0"
                    Else
                        txtHomePoints5.Text = "1"
                    End If
                    calc_totals()
                End If
            Case "CRIB"
                If Val(txtAwayPoints5.Text) < 0 Or Val(txtAwayPoints5.Text) > 5 Then
                    txtAwayPoints5.Text = ""
                    txtAwayPoints5.Focus()
                Else
                    txtHomePoints5.Text = CStr(5 - Val(txtAwayPoints5.Text))
                    calc_totals()
                End If
            Case "SKIT"
                calc_totals()
                txtHomePoints6.Focus()
        End Select
    End Sub


    Protected Sub txtAwayPoints6_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayPoints6.TextChanged
        calc_totals()
        txtHomePoints7.Focus()
    End Sub
    Protected Sub txtAwayPoints7_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayPoints7.TextChanged
        calc_totals()
        txtHomePoints8.Focus()
    End Sub
    Protected Sub txtAwayPoints8_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayPoints8.TextChanged
        calc_totals()
        txtHomePoints9.Focus()
    End Sub
    Protected Sub txtAwayPoints9_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayPoints9.TextChanged
        calc_totals()
        txtHomePoints10.Focus()
    End Sub
    Protected Sub txtAwayPoints10_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayPoints10.TextChanged
        calc_totals()
        txtHomePoints11.Focus()
    End Sub
    Protected Sub txtAwayPoints11_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayPoints11.TextChanged
        calc_totals()
        txtHomePoints12.Focus()
    End Sub
    Protected Sub txtAwayPoints12_TextChanged(sender As Object, e As System.EventArgs) Handles txtAwayPoints12.TextChanged
        calc_totals()
    End Sub

    Sub calc_totals()
        Dim HomeTotal As Integer = 0
        Dim AwayTotal As Integer = 0
        HomeTotal = Val(txtHomePoints1.Text) + Val(txtHomePoints2.Text) + Val(txtHomePoints3.Text) + Val(txtHomePoints4.Text) + Val(txtHomePoints5.Text) + Val(txtHomePoints6.Text) + Val(txtHomePoints7.Text) + Val(txtHomePoints8.Text) + Val(txtHomePoints9.Text) + Val(txtHomePoints10.Text) + Val(txtHomePoints11.Text) + Val(txtHomePoints12.Text)
        AwayTotal = Val(txtAwayPoints1.Text) + Val(txtAwayPoints2.Text) + Val(txtAwayPoints3.Text) + Val(txtAwayPoints4.Text) + Val(txtAwayPoints5.Text) + Val(txtAwayPoints6.Text) + Val(txtAwayPoints7.Text) + Val(txtAwayPoints8.Text) + Val(txtAwayPoints9.Text) + Val(txtAwayPoints10.Text) + Val(txtAwayPoints11.Text) + Val(txtAwayPoints12.Text)
        txtHomePoints0.Text = CStr(HomeTotal)
        txtAwayPoints0.Text = CStr(AwayTotal)
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim NewResult As String = ""
        If Left(lblLeague.Text, 4) = "SKIT" Then
            NewResult = ddSkittlesResult.SelectedValue
        Else
            NewResult = txtHomePoints0.Text & "-" & txtAwayPoints0.Text
        End If
        If check_entries() Then
            Call update_deducted_header()
            Call update_fixture_details()
            Select Case Left(lblLeague.Text, 4)
                Case "CRIB"
                    Call update_player_stats("sp_update_player_stats_crib")
                Case "SNOO"
                    Call update_player_stats("sp_update_player_stats_snooker")
                Case "SKIT"
                    Call update_player_stats("sp_update_player_stats_skittles")
            End Select
            'update league AND team positions
            strSQL = "EXEC clubs.sp_update_league_position '" & lblLeague.Text & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            strSQL = "EXEC clubs.sp_update_team_position '" & lblLeague.Text & "','" & lblHomeTeam.Text & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            strSQL = "EXEC clubs.sp_update_team_position '" & lblLeague.Text & "','" & lblAwayTeam.Text & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

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
            btnReUpdate.PostBackUrl = "~/Clubs/Admin/Fixture Result.aspx?ID= " & fixture_id & "&Week=" & FixtureWeek
            btnReUpdate.Focus()
            btnUpdate.Visible = False
        Else
            '19.11.18 - update fixture_combined table
            Call objGlobals.update_fixtures_combined("clubs")

            If TeamSelected Is Nothing Then
                Response.Redirect("~/Clubs/Default.aspx?Week=" & FixtureWeek)
            Else
                Response.Redirect("~/Clubs/Team Fixtures.aspx?League=" & lblLeague.Text & "&Team=" & TeamSelected)
            End If
        End If
    End Sub

    Sub update_player_stats(inStoredProcedure As String)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim myDataReader2 As oledbdatareader
        Dim tempSeason As String
        tempSeason = objGlobals.get_current_season
        inStoredProcedure = "clubs." & inStoredProcedure
        'update the home team players
        strSQL = "SELECT player FROM clubs.vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblHomeTeam.Text & "' AND player NOT LIKE 'A N OTHER%' "
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            strSQL = "EXEC " & inStoredProcedure + "'" & tempSeason & "','" & lblLeague.Text & "','" & lblHomeTeam.Text & "','" & myDataReader.Item("player") & "'," & fixture_id
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        objGlobals.close_connection()

        'update the away team players
        strSQL = "SELECT player FROM clubs.vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblAwayTeam.Text & "' AND player NOT LIKE 'A N OTHER%' "
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            strSQL = "EXEC " & inStoredProcedure & "'" & tempSeason & "','" & lblLeague.Text & "','" & lblAwayTeam.Text & "','" & myDataReader.Item("player") & "'," & fixture_id
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        objGlobals.close_connection()

    End Sub

    Sub update_deducted_header()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        strSQL = "UPDATE clubs.vw_fixtures SET Status = 2,home_points_deducted = " & ddHomePointsDeducted.SelectedValue & ",away_points_deducted = " & ddAwayPointsDeducted.SelectedValue & " WHERE fixture_id = " & fixture_id
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

    End Sub

    Sub update_fixture_details()
        Dim strSQL As String
        Dim strSQL1 As String
        Dim myDataReader As oledbdatareader
        Dim neutral_fixture As Integer

        strSQL = "DELETE FROM clubs.vw_fixtures_detail WHERE fixture_id = " & fixture_id
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

        strSQL = "SELECT fixture_neutral FROM clubs.vw_fixtures WHERE fixture_id = " & fixture_id
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            neutral_fixture = myDataReader.Item("fixture_neutral")
        End While
        objGlobals.close_connection()

        strSQL1 = "INSERT INTO clubs.fixtures_detail VALUES ('" & objGlobals.current_season & "'," & fixture_id & ",'" & lblLeague.Text & "','" & lblDate.Text & "',GETDATE(), " & FixtureWeek & ","
        Select Case Left(lblLeague.Text, 4)
            Case "SNOO"
                strSQL = "1,'" & lblHomeTeam.Text & "','" & lblHomePlayer1.Text & "',null, " & Val(txtHomePoints1.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer1.Text & "',null," & Val(txtAwayPoints1.Text) & ",null,null," & neutral_fixture & ")"
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "2,'" & lblHomeTeam.Text & "','" & lblHomePlayer2.Text & "',null, " & Val(txtHomePoints2.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer2.Text & "',null," & Val(txtAwayPoints2.Text) & ",null,null," & neutral_fixture & ")"
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "3,'" & lblHomeTeam.Text & "','" & lblHomePlayer3.Text & "',null, " & Val(txtHomePoints3.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer3.Text & "',null," & Val(txtAwayPoints3.Text) & ",null,null," & neutral_fixture & ")"
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "4,'" & lblHomeTeam.Text & "','" & lblHomePlayer4.Text & "',null, " & Val(txtHomePoints4.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer4.Text & "',null," & Val(txtAwayPoints4.Text) & ",null,null," & neutral_fixture & ")"
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "5,'" & lblHomeTeam.Text & "','" & lblHomePlayer5.Text & "',null, " & Val(txtHomePoints5.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer5.Text & "',null," & Val(txtAwayPoints5.Text) & ",null,null," & neutral_fixture & ")"
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
            Case "SKIT"
                strSQL = "1,'" & lblHomeTeam.Text & "','" & lblHomePlayer1.Text & "',null, " & Val(txtHomePoints1.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer1.Text & "',null," & Val(txtAwayPoints1.Text) & ","
                If txtAwayNines1.Text <> "" Then
                    strSQL = strSQL & Val(txtAwayNines1.Text) & ",null," & neutral_fixture & ")"
                Else
                    strSQL = strSQL & "null,null," & neutral_fixture & ")"
                End If
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "2,'" & lblHomeTeam.Text & "','" & lblHomePlayer2.Text & "',null, " & Val(txtHomePoints2.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer2.Text & "',null," & Val(txtAwayPoints2.Text) & ","
                If txtAwayNines2.Text <> "" Then
                    strSQL = strSQL & Val(txtAwayNines2.Text) & ",null," & neutral_fixture & ")"
                Else
                    strSQL = strSQL & "null,null," & neutral_fixture & ")"
                End If
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "3,'" & lblHomeTeam.Text & "','" & lblHomePlayer3.Text & "',null, " & Val(txtHomePoints3.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer3.Text & "',null," & Val(txtAwayPoints3.Text) & ","
                If txtAwayNines3.Text <> "" Then
                    strSQL = strSQL & Val(txtAwayNines3.Text) & ",null," & neutral_fixture & ")"
                Else
                    strSQL = strSQL & "null,null," & neutral_fixture & ")"
                End If
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "4,'" & lblHomeTeam.Text & "','" & lblHomePlayer4.Text & "',null, " & Val(txtHomePoints4.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer4.Text & "',null," & Val(txtAwayPoints4.Text) & ","
                If txtAwayNines4.Text <> "" Then
                    strSQL = strSQL & Val(txtAwayNines4.Text) & ",null," & neutral_fixture & ")"
                Else
                    strSQL = strSQL & "null,null," & neutral_fixture & ")"
                End If
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "5,'" & lblHomeTeam.Text & "','" & lblHomePlayer5.Text & "',null, " & Val(txtHomePoints5.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer5.Text & "',null," & Val(txtAwayPoints5.Text) & ","
                If txtAwayNines5.Text <> "" Then
                    strSQL = strSQL & Val(txtAwayNines5.Text) & ",null," & neutral_fixture & ")"
                Else
                    strSQL = strSQL & "null,null," & neutral_fixture & ")"
                End If
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "6,'" & lblHomeTeam.Text & "','" & lblHomePlayer6.Text & "',null, " & Val(txtHomePoints6.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer6.Text & "',null," & Val(txtAwayPoints6.Text) & ","
                If txtAwayNines6.Text <> "" Then
                    strSQL = strSQL & Val(txtAwayNines6.Text) & ",null," & neutral_fixture & ")"
                Else
                    strSQL = strSQL & "null,null," & neutral_fixture & ")"
                End If
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "7,'" & lblHomeTeam.Text & "','" & lblHomePlayer7.Text & "',null, " & Val(txtHomePoints7.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer7.Text & "',null," & Val(txtAwayPoints7.Text) & ","
                If txtAwayNines7.Text <> "" Then
                    strSQL = strSQL & Val(txtAwayNines7.Text) & ",null," & neutral_fixture & ")"
                Else
                    strSQL = strSQL & "null,null," & neutral_fixture & ")"
                End If
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "8,'" & lblHomeTeam.Text & "','" & lblHomePlayer8.Text & "',null, " & Val(txtHomePoints8.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer8.Text & "',null," & Val(txtAwayPoints8.Text) & ","
                If txtAwayNines8.Text <> "" Then
                    strSQL = strSQL & Val(txtAwayNines8.Text) & ",null," & neutral_fixture & ")"
                Else
                    strSQL = strSQL & "null,null," & neutral_fixture & ")"
                End If
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "9,'" & lblHomeTeam.Text & "','" & lblHomePlayer9.Text & "',null, " & Val(txtHomePoints9.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer9.Text & "',null," & Val(txtAwayPoints9.Text) & ","
                If txtAwayNines9.Text <> "" Then
                    strSQL = strSQL & Val(txtAwayNines9.Text) & ",null," & neutral_fixture & ")"
                Else
                    strSQL = strSQL & "null,null," & neutral_fixture & ")"
                End If
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "10,'" & lblHomeTeam.Text & "','" & lblHomePlayer10.Text & "',null, " & Val(txtHomePoints10.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer10.Text & "',null," & Val(txtAwayPoints10.Text) & ","
                If txtAwayNines10.Text <> "" Then
                    strSQL = strSQL & Val(txtAwayNines10.Text) & ",null," & neutral_fixture & ")"
                Else
                    strSQL = strSQL & "null,null," & neutral_fixture & ")"
                End If
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "11,'" & lblHomeTeam.Text & "','" & lblHomePlayer11.Text & "',null, " & Val(txtHomePoints11.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer11.Text & "',null," & Val(txtAwayPoints11.Text) & ","
                If txtAwayNines11.Text <> "" Then
                    strSQL = strSQL & Val(txtAwayNines11.Text) & ",null," & neutral_fixture & ")"
                Else
                    strSQL = strSQL & "null,null," & neutral_fixture & ")"
                End If
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "12,'" & lblHomeTeam.Text & "','" & lblHomePlayer12.Text & "',null, " & Val(txtHomePoints12.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer12.Text & "',null," & Val(txtAwayPoints12.Text) & ","
                If txtAwayNines12.Text <> "" Then
                    strSQL = strSQL & Val(txtAwayNines12.Text) & ",null," & neutral_fixture & ")"
                Else
                    strSQL = strSQL & "null,null," & neutral_fixture & ")"
                End If
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
            Case "CRIB"
                strSQL = "1,'" & lblHomeTeam.Text & "','" & lblHomePlayer1.Text & "','" & lblHomePlayer2.Text & "'," & Val(txtHomePoints1.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer1.Text & "','" & lblAwayPlayer2.Text & "'," & Val(txtAwayPoints1.Text) & ",null,null," & neutral_fixture & ")"
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "2,'" & lblHomeTeam.Text & "','" & lblHomePlayer3.Text & "','" & lblHomePlayer4.Text & "'," & Val(txtHomePoints3.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer3.Text & "','" & lblAwayPlayer4.Text & "'," & Val(txtAwayPoints3.Text) & ",null,null," & neutral_fixture & ")"
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
                strSQL = "3,'" & lblHomeTeam.Text & "','" & lblHomePlayer5.Text & "','" & lblHomePlayer6.Text & "'," & Val(txtHomePoints5.Text) & ",'" & lblAwayTeam.Text & "','" & lblAwayPlayer5.Text & "','" & lblAwayPlayer6.Text & "'," & Val(txtAwayPoints5.Text) & ",null,null," & neutral_fixture & ")"
                myDataReader = objGlobals.SQLSelect(strSQL1 + strSQL)
                objGlobals.close_connection()
        End Select

        'update the fixture_calendar date FROM clubs.vw_fixtures
        strSQL = "UPDATE clubs.vw_fixtures_detail SET fixture_calendar = (SELECT fixture_calendar FROM clubs.vw_fixtures WHERE fixture_id = " & fixture_id & ") WHERE fixture_id = " & fixture_id
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

        'update the fixture_calendar short date FROM mens_skit.vw_fixtures
        strSQL = "UPDATE clubs.vw_fixtures_detail SET fixture_short_date = (SELECT fixture_short_date FROM clubs.vw_fixtures WHERE fixture_id = " & fixture_id & ") WHERE fixture_id = " & fixture_id
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()
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
        If Left(lblLeague.Text, 4) <> "SNOO" And lblHomePlayer6.Text = "." Then lstErrors.Items.Add("No Home Player 6 Entered")
        If lblAwayPlayer1.Text = "." Then lstErrors.Items.Add("No Away Player 1 Entered")
        If lblAwayPlayer2.Text = "." Then lstErrors.Items.Add("No Away Player 2 Entered")
        If lblAwayPlayer3.Text = "." Then lstErrors.Items.Add("No Away Player 3 Entered")
        If lblAwayPlayer4.Text = "." Then lstErrors.Items.Add("No Away Player 4 Entered")
        If lblAwayPlayer5.Text = "." Then lstErrors.Items.Add("No Away Player 5 Entered")
        If Left(lblLeague.Text, 4) <> "SNOO" And lblAwayPlayer6.Text = "." Then lstErrors.Items.Add("No Away Player 6 Entered")
        If Left(lblLeague.Text, 4) = "SKIT" Then
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
        End If
        If txtHomePoints1.Text = "" Then lstErrors.Items.Add("No Home Points for Player 1 Entered")
        If Left(lblLeague.Text, 4) <> "CRIB" And txtHomePoints2.Text = "" Then lstErrors.Items.Add("No Home Points for Player 2 Entered")
        If txtHomePoints3.Text = "" Then lstErrors.Items.Add("No Home Points for Player 3 Entered")
        If Left(lblLeague.Text, 4) <> "CRIB" And txtHomePoints4.Text = "" Then lstErrors.Items.Add("No Home Points for Player 4 Entered")
        If txtHomePoints5.Text = "" Then lstErrors.Items.Add("No Home Points for Player 5 Entered")
        If Left(lblLeague.Text, 4) = "SKIT" And txtHomePoints6.Text = "" Then lstErrors.Items.Add("No Home Points for Player 6 Entered")
        If txtAwayPoints1.Text = "" Then lstErrors.Items.Add("No Away Points for Player 1 Entered")
        If Left(lblLeague.Text, 4) <> "CRIB" And txtAwayPoints2.Text = "" Then lstErrors.Items.Add("No Away Points for Player 2 Entered")
        If txtAwayPoints3.Text = "" Then lstErrors.Items.Add("No Away Points for Player 3 Entered")
        If Left(lblLeague.Text, 4) <> "CRIB" And txtAwayPoints4.Text = "" Then lstErrors.Items.Add("No Away Points for Player 4 Entered")
        If txtAwayPoints5.Text = "" Then lstErrors.Items.Add("No Away Points for Player 5 Entered")
        If Left(lblLeague.Text, 4) = "SKIT" Then
            If txtAwayPoints6.Text = "" Then lstErrors.Items.Add("No Away Points for Player 6 Entered")
            If txtHomePoints7.Text = "" Then lstErrors.Items.Add("No Home Points for Player 7 Entered")
            If txtHomePoints8.Text = "" Then lstErrors.Items.Add("No Home Points for Player 8 Entered")
            If txtHomePoints9.Text = "" Then lstErrors.Items.Add("No Home Points for Player 9 Entered")
            If txtHomePoints10.Text = "" Then lstErrors.Items.Add("No Home Points for Player 10 Entered")
            If txtHomePoints11.Text = "" Then lstErrors.Items.Add("No Home Points for Player 11 Entered")
            If txtHomePoints12.Text = "" Then lstErrors.Items.Add("No Home Points for Player 12 Entered")
            If txtAwayPoints7.Text = "" Then lstErrors.Items.Add("No Away Points for Player 7 Entered")
            If txtAwayPoints8.Text = "" Then lstErrors.Items.Add("No Away Points for Player 8 Entered")
            If txtAwayPoints9.Text = "" Then lstErrors.Items.Add("No Away Points for Player 9 Entered")
            If txtAwayPoints10.Text = "" Then lstErrors.Items.Add("No Away Points for Player 10 Entered")
            If txtAwayPoints11.Text = "" Then lstErrors.Items.Add("No Away Points for Player 11 Entered")
            If txtAwayPoints12.Text = "" Then lstErrors.Items.Add("No Away Points for Player 12 Entered")

            If ddSkittlesResult.SelectedValue = "0-0" Then
                lstErrors.Items.Add("No Match Result Entered")
            End If
        End If

        If lstErrors.Items.Count > 0 Then
            lstErrors.Visible = True
            check_entries = False
        End If
    End Function

    Protected Sub btnAdd1_Click(sender As Object, e As System.EventArgs) Handles btnAdd1.Click
        'add new player to home team
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim Found As Boolean = False
        'see if player already exists first
        lblHomeExists.Visible = False
        strSQL = "SELECT player FROM clubs.vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblHomeTeam.Text & "' AND player = '" & txtAddHomePlayer.Text & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            Found = True
        End While
        objGlobals.close_connection()

        If Found Then
            lblHomeExists.Visible = True
            Exit Sub
        End If
        txtAddHomePlayer.Text = UCase(txtAddHomePlayer.Text)
        strSQL = "INSERT INTO clubs.players VALUES ('" & objGlobals.current_season & "',"
        strSQL = strSQL & "'" & lblLeague.Text & "',"
        strSQL = strSQL & "'" & lblHomeTeam.Text & "',"
        strSQL = strSQL & "'" & txtAddHomePlayer.Text & "',NULL,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0)"
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

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
        Dim myDataReader As oledbdatareader
        Dim Found As Boolean = False
        lblAwayExists.Visible = False
        'see if player already exists first
        strSQL = "SELECT player FROM clubs.vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblAwayTeam.Text & "' AND player = '" & txtAddAwayPlayer.Text & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            Found = True
        End While
        objGlobals.close_connection()

        If Found Then
            lblAwayExists.Visible = True
            Exit Sub
        End If
        txtAddAwayPlayer.Text = UCase(txtAddAwayPlayer.Text)
        strSQL = "INSERT INTO clubs.players VALUES ('" & objGlobals.current_season & "',"
        strSQL = strSQL & "'" & lblLeague.Text & "',"
        strSQL = strSQL & "'" & lblAwayTeam.Text & "',"
        strSQL = strSQL & "'" & txtAddAwayPlayer.Text & "',NULL,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0)"
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

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

    Protected Sub enable_crib_scores()
        lblCurrentHomePlayer.Visible = True
        lblCurrentAwayPlayer.Visible = True
        btn0.Visible = False : btn1.Visible = False : btn2.Visible = False : btn3.Visible = False : btn4.Visible = False : btn5.Visible = False
        btn6.Visible = False : btn7.Visible = False : btn8.Visible = False : btn9.Visible = False
        btn10.Visible = False : btn11.Visible = False : btn12.Visible = False : btn13.Visible = False : btn14.Visible = False : btn15.Visible = False
        btn16.Visible = False : btn17.Visible = False : btn18.Visible = False : btn19.Visible = False
        btn20.Visible = False : btn21.Visible = False : btn22.Visible = False : btn23.Visible = False : btn24.Visible = False : btn25.Visible = False
        btn26.Visible = False : btn27.Visible = False : btn28.Visible = False : btn29.Visible = False
        btn30.Visible = False : btn31.Visible = False : btn32.Visible = False : btn33.Visible = False : btn34.Visible = False : btn35.Visible = False
        btn36.Visible = False : btn37.Visible = False : btn38.Visible = False : btn39.Visible = False : btn40.Visible = False
        btn5_0.Visible = True : btn4_1.Visible = True : btn3_2.Visible = True : btn2_3.Visible = True : btn1_4.Visible = True : btn0_5.Visible = True
        btn1_0.Visible = False : btn0_1.Visible = False

        lblCurrentHomePlayer.Text = lblHomePlayer1.Text & " & " & lblHomePlayer2.Text
        lblHomePlayer1.ForeColor = LightBlue
        txtHomePoints1.BackColor = Blue
        lblHomePlayer2.ForeColor = LightBlue

        lblCurrentAwayPlayer.Text = lblAwayPlayer1.Text & " & " & lblAwayPlayer2.Text
        lblAwayPlayer1.ForeColor = LightBlue
        txtAwayPoints1.BackColor = Blue
        lblAwayPlayer2.ForeColor = LightBlue
    End Sub

    Protected Sub enable_skittles_scores()
        lblCurrentHomePlayer.Visible = True
        lblCurrentAwayPlayer.Visible = True
        btn0.Visible = True : btn1.Visible = True : btn2.Visible = True : btn3.Visible = True : btn4.Visible = True : btn5.Visible = True
        btn6.Visible = True : btn7.Visible = True : btn8.Visible = True : btn9.Visible = True
        btn10.Visible = True : btn11.Visible = True : btn12.Visible = True : btn13.Visible = True : btn14.Visible = True : btn15.Visible = True
        btn16.Visible = True : btn17.Visible = True : btn18.Visible = True : btn19.Visible = True
        btn20.Visible = True : btn21.Visible = True : btn22.Visible = True : btn23.Visible = True : btn24.Visible = True : btn25.Visible = True
        btn26.Visible = True : btn27.Visible = True : btn28.Visible = True : btn29.Visible = True
        btn30.Visible = True : btn31.Visible = True : btn32.Visible = True : btn33.Visible = True : btn34.Visible = True : btn35.Visible = True
        btn36.Visible = True : btn37.Visible = True : btn38.Visible = True : btn39.Visible = True : btn40.Visible = True
        lblCurrentHomePlayer.Text = lblHomePlayer1.Text : lblHomePlayer1.ForeColor = LightBlue : txtHomePoints1.BackColor = Blue
        btn5_0.Visible = False : btn4_1.Visible = False : btn3_2.Visible = False : btn2_3.Visible = False : btn1_4.Visible = False : btn0_5.Visible = False
        btn1_0.Visible = False : btn0_1.Visible = False
    End Sub

    Protected Sub enable_snooker_scores()
        lblCurrentHomePlayer.Visible = True
        lblCurrentAwayPlayer.Visible = True
        btn0.Visible = False : btn1.Visible = False : btn2.Visible = False : btn3.Visible = False : btn4.Visible = False : btn5.Visible = False
        btn6.Visible = False : btn7.Visible = False : btn8.Visible = False : btn9.Visible = False
        btn10.Visible = False : btn11.Visible = False : btn12.Visible = False : btn13.Visible = False : btn14.Visible = False : btn15.Visible = False
        btn16.Visible = False : btn17.Visible = False : btn18.Visible = False : btn19.Visible = False
        btn20.Visible = False : btn21.Visible = False : btn22.Visible = False : btn23.Visible = False : btn24.Visible = False : btn25.Visible = False
        btn26.Visible = False : btn27.Visible = False : btn28.Visible = False : btn29.Visible = False
        btn30.Visible = False : btn31.Visible = False : btn32.Visible = False : btn33.Visible = False : btn34.Visible = False : btn35.Visible = False
        btn36.Visible = False : btn37.Visible = False : btn38.Visible = False : btn39.Visible = False : btn40.Visible = False
        lblCurrentHomePlayer.Text = lblHomePlayer1.Text : lblHomePlayer1.ForeColor = LightBlue : txtHomePoints1.BackColor = Blue
        btn5_0.Visible = False : btn4_1.Visible = False : btn3_2.Visible = False : btn2_3.Visible = False : btn1_4.Visible = False : btn0_5.Visible = False
        btn1_0.Visible = True : btn0_1.Visible = True

        lblCurrentHomePlayer.Text = lblHomePlayer1.Text
        lblHomePlayer1.ForeColor = LightBlue
        txtHomePoints1.BackColor = Blue

        lblCurrentAwayPlayer.Text = lblAwayPlayer1.Text
        lblAwayPlayer1.ForeColor = LightBlue
        txtAwayPoints1.BackColor = Blue
    End Sub

    Protected Sub btn0_Click(sender As Object, e As System.EventArgs) Handles btn0.Click
        Call enter_skittles_score_player(btn0.Text)
    End Sub
    Protected Sub btn1_Click(sender As Object, e As System.EventArgs) Handles btn1.Click
        Call enter_skittles_score_player(btn1.Text)
    End Sub
    Protected Sub btn2_Click(sender As Object, e As System.EventArgs) Handles btn2.Click
        Call enter_skittles_score_player(btn2.Text)
    End Sub
    Protected Sub btn3_Click(sender As Object, e As System.EventArgs) Handles btn3.Click
        Call enter_skittles_score_player(btn3.Text)
    End Sub
    Protected Sub btn4_Click(sender As Object, e As System.EventArgs) Handles btn4.Click
        Call enter_skittles_score_player(btn4.Text)
    End Sub
    Protected Sub btn5_Click(sender As Object, e As System.EventArgs) Handles btn5.Click
        Call enter_skittles_score_player(btn5.Text)
    End Sub
    Protected Sub btn6_Click(sender As Object, e As System.EventArgs) Handles btn6.Click
        Call enter_skittles_score_player(btn6.Text)
    End Sub
    Protected Sub btn7_Click(sender As Object, e As System.EventArgs) Handles btn7.Click
        Call enter_skittles_score_player(btn7.Text)
    End Sub
    Protected Sub btn8_Click(sender As Object, e As System.EventArgs) Handles btn8.Click
        Call enter_skittles_score_player(btn8.Text)
    End Sub
    Protected Sub btn9_Click(sender As Object, e As System.EventArgs) Handles btn9.Click
        Call enter_skittles_score_player(btn9.Text)
    End Sub
    Protected Sub btn10_Click(sender As Object, e As System.EventArgs) Handles btn10.Click
        Call enter_skittles_score_player(btn10.Text)
    End Sub
    Protected Sub btn11_Click(sender As Object, e As System.EventArgs) Handles btn11.Click
        Call enter_skittles_score_player(btn11.Text)
    End Sub
    Protected Sub btn12_Click(sender As Object, e As System.EventArgs) Handles btn12.Click
        Call enter_skittles_score_player(btn12.Text)
    End Sub
    Protected Sub btn13_Click(sender As Object, e As System.EventArgs) Handles btn13.Click
        Call enter_skittles_score_player(btn13.Text)
    End Sub
    Protected Sub btn14_Click(sender As Object, e As System.EventArgs) Handles btn14.Click
        Call enter_skittles_score_player(btn14.Text)
    End Sub
    Protected Sub btn15_Click(sender As Object, e As System.EventArgs) Handles btn15.Click
        Call enter_skittles_score_player(btn15.Text)
    End Sub
    Protected Sub btn16_Click(sender As Object, e As System.EventArgs) Handles btn16.Click
        Call enter_skittles_score_player(btn16.Text)
    End Sub
    Protected Sub btn17_Click(sender As Object, e As System.EventArgs) Handles btn17.Click
        Call enter_skittles_score_player(btn17.Text)
    End Sub
    Protected Sub btn18_Click(sender As Object, e As System.EventArgs) Handles btn18.Click
        Call enter_skittles_score_player(btn18.Text)
    End Sub
    Protected Sub btn19_Click(sender As Object, e As System.EventArgs) Handles btn19.Click
        Call enter_skittles_score_player(btn19.Text)
    End Sub
    Protected Sub btn20_Click(sender As Object, e As System.EventArgs) Handles btn20.Click
        Call enter_skittles_score_player(btn20.Text)
    End Sub
    Protected Sub btn21_Click(sender As Object, e As System.EventArgs) Handles btn21.Click
        Call enter_skittles_score_player(btn21.Text)
    End Sub
    Protected Sub btn22_Click(sender As Object, e As System.EventArgs) Handles btn22.Click
        Call enter_skittles_score_player(btn22.Text)
    End Sub
    Protected Sub btn23_Click(sender As Object, e As System.EventArgs) Handles btn23.Click
        Call enter_skittles_score_player(btn23.Text)
    End Sub
    Protected Sub btn24_Click(sender As Object, e As System.EventArgs) Handles btn24.Click
        Call enter_skittles_score_player(btn24.Text)
    End Sub
    Protected Sub btn25_Click(sender As Object, e As System.EventArgs) Handles btn25.Click
        Call enter_skittles_score_player(btn25.Text)
    End Sub
    Protected Sub btn26_Click(sender As Object, e As System.EventArgs) Handles btn26.Click
        Call enter_skittles_score_player(btn26.Text)
    End Sub
    Protected Sub btn27_Click(sender As Object, e As System.EventArgs) Handles btn27.Click
        Call enter_skittles_score_player(btn27.Text)
    End Sub
    Protected Sub btn28_Click(sender As Object, e As System.EventArgs) Handles btn28.Click
        Call enter_skittles_score_player(btn28.Text)
    End Sub
    Protected Sub btn29_Click(sender As Object, e As System.EventArgs) Handles btn29.Click
        Call enter_skittles_score_player(btn29.Text)
    End Sub
    Protected Sub btn30_Click(sender As Object, e As System.EventArgs) Handles btn30.Click
        Call enter_skittles_score_player(btn30.Text)
    End Sub
    Protected Sub btn31_Click(sender As Object, e As System.EventArgs) Handles btn31.Click
        Call enter_skittles_score_player(btn31.Text)
    End Sub
    Protected Sub btn32_Click(sender As Object, e As System.EventArgs) Handles btn32.Click
        Call enter_skittles_score_player(btn32.Text)
    End Sub
    Protected Sub btn33_Click(sender As Object, e As System.EventArgs) Handles btn33.Click
        Call enter_skittles_score_player(btn33.Text)
    End Sub
    Protected Sub btn34_Click(sender As Object, e As System.EventArgs) Handles btn34.Click
        Call enter_skittles_score_player(btn34.Text)
    End Sub
    Protected Sub btn35_Click(sender As Object, e As System.EventArgs) Handles btn35.Click
        Call enter_skittles_score_player(btn35.Text)
    End Sub
    Protected Sub btn36_Click(sender As Object, e As System.EventArgs) Handles btn36.Click
        Call enter_skittles_score_player(btn36.Text)
    End Sub
    Protected Sub btn37_Click(sender As Object, e As System.EventArgs) Handles btn37.Click
        Call enter_skittles_score_player(btn37.Text)
    End Sub
    Protected Sub btn38_Click(sender As Object, e As System.EventArgs) Handles btn38.Click
        Call enter_skittles_score_player(btn38.Text)
    End Sub
    Protected Sub btn39_Click(sender As Object, e As System.EventArgs) Handles btn39.Click
        Call enter_skittles_score_player(btn39.Text)
    End Sub
    Protected Sub btn40_Click(sender As Object, e As System.EventArgs) Handles btn40.Click
        Call enter_skittles_score_player(btn40.Text)
    End Sub

    Protected Sub btn5_0_Click(sender As Object, e As System.EventArgs) Handles btn5_0.Click
        Call enter_crib_score_player(btn5_0.Text)
    End Sub
    Protected Sub btn4_1_Click(sender As Object, e As System.EventArgs) Handles btn4_1.Click
        Call enter_crib_score_player(btn4_1.Text)
    End Sub
    Protected Sub btn3_2_Click(sender As Object, e As System.EventArgs) Handles btn3_2.Click
        Call enter_crib_score_player(btn3_2.Text)
    End Sub
    Protected Sub btn2_3_Click(sender As Object, e As System.EventArgs) Handles btn2_3.Click
        Call enter_crib_score_player(btn2_3.Text)
    End Sub
    Protected Sub btn1_4_Click(sender As Object, e As System.EventArgs) Handles btn1_4.Click
        Call enter_crib_score_player(btn1_4.Text)
    End Sub
    Protected Sub btn0_5_Click(sender As Object, e As System.EventArgs) Handles btn0_5.Click
        Call enter_crib_score_player(btn0_5.Text)
    End Sub

    Protected Sub btn1_0_Click(sender As Object, e As System.EventArgs) Handles btn1_0.Click
        Call enter_snooker_score_player(btn1_0.Text)
    End Sub
    Protected Sub btn0_1_Click(sender As Object, e As System.EventArgs) Handles btn0_1.Click
        Call enter_snooker_score_player(btn0_1.Text)
    End Sub

    Sub enter_snooker_score_player(inScore As String)
        If txtHomePoints1.Text = "" Then
            txtHomePoints1.Text = Left(inScore, 1)
            lblCurrentHomePlayer.Text = lblHomePlayer2.Text
            txtAwayPoints1.Text = Right(inScore, 1)
            lblCurrentAwayPlayer.Text = lblAwayPlayer2.Text
            lblHomePlayer2.ForeColor = LightBlue
            txtHomePoints2.BackColor = Blue
            lblAwayPlayer2.ForeColor = LightBlue
            txtAwayPoints2.BackColor = Blue
            Exit Sub
        End If

        If txtHomePoints2.Text = "" Then
            txtHomePoints2.Text = Left(inScore, 1)
            lblCurrentHomePlayer.Text = lblHomePlayer3.Text
            txtAwayPoints2.Text = Right(inScore, 1)
            lblCurrentAwayPlayer.Text = lblAwayPlayer3.Text
            lblHomePlayer3.ForeColor = LightBlue
            txtHomePoints3.BackColor = Blue
            lblAwayPlayer3.ForeColor = LightBlue
            txtAwayPoints3.BackColor = Blue
            Exit Sub
        End If

        If txtHomePoints3.Text = "" Then
            txtHomePoints3.Text = Left(inScore, 1)
            lblCurrentHomePlayer.Text = lblHomePlayer4.Text
            txtAwayPoints3.Text = Right(inScore, 1)
            lblCurrentAwayPlayer.Text = lblAwayPlayer4.Text
            lblHomePlayer4.ForeColor = LightBlue
            txtHomePoints4.BackColor = Blue
            lblAwayPlayer4.ForeColor = LightBlue
            txtAwayPoints4.BackColor = Blue
            Exit Sub
        End If

        If txtHomePoints4.Text = "" Then
            txtHomePoints4.Text = Left(inScore, 1)
            lblCurrentHomePlayer.Text = lblHomePlayer5.Text
            txtAwayPoints4.Text = Right(inScore, 1)
            lblCurrentAwayPlayer.Text = lblAwayPlayer5.Text
            lblHomePlayer5.ForeColor = LightBlue
            txtHomePoints5.BackColor = Blue
            lblAwayPlayer5.ForeColor = LightBlue
            txtAwayPoints5.BackColor = Blue
            Exit Sub
        End If

        If txtHomePoints5.Text = "" Then
            txtHomePoints5.Text = Left(inScore, 1)
            txtAwayPoints5.Text = Right(inScore, 1)

            calc_totals()
            btnUpdate.Focus()
        End If

    End Sub

    Sub enter_crib_score_player(inScore As String)
        If txtHomePoints1.Text = "" Then
            txtHomePoints1.Text = Left(inScore, 1)
            lblCurrentHomePlayer.Text = lblHomePlayer3.Text & " & " & lblHomePlayer4.Text
            lblHomePlayer3.ForeColor = LightBlue
            lblHomePlayer4.ForeColor = LightBlue
            txtHomePoints3.BackColor = Blue

            txtAwayPoints1.Text = Right(inScore, 1)
            lblCurrentAwayPlayer.Text = lblAwayPlayer3.Text & " & " & lblAwayPlayer4.Text
            lblAwayPlayer3.ForeColor = LightBlue
            lblAwayPlayer4.ForeColor = LightBlue
            txtAwayPoints3.BackColor = Blue

            Exit Sub
        End If

        If txtHomePoints3.Text = "" Then
            txtHomePoints3.Text = Left(inScore, 1)
            lblCurrentHomePlayer.Text = lblHomePlayer5.Text & " & " & lblHomePlayer6.Text
            lblHomePlayer5.ForeColor = LightBlue
            lblHomePlayer6.ForeColor = LightBlue
            txtHomePoints5.BackColor = Blue

            txtAwayPoints3.Text = Right(inScore, 1)
            lblCurrentAwayPlayer.Text = lblAwayPlayer5.Text & " & " & lblAwayPlayer6.Text
            lblAwayPlayer5.ForeColor = LightBlue
            lblAwayPlayer6.ForeColor = LightBlue
            txtAwayPoints5.BackColor = Blue

            Exit Sub
        End If

        If txtHomePoints5.Text = "" Then
            txtHomePoints5.Text = Left(inScore, 1)
            txtAwayPoints5.Text = Right(inScore, 1)

            calc_totals()
            btnUpdate.Focus()
        End If


    End Sub

    Sub enter_skittles_score_player(inScore As String)
        If txtHomePoints1.Text = "" Then txtHomePoints1.Text = inScore : lblCurrentHomePlayer.Text = lblHomePlayer2.Text : lblHomePlayer2.ForeColor = LightBlue : txtHomePoints2.BackColor = Blue : Exit Sub
        If txtHomePoints2.Text = "" Then txtHomePoints2.Text = inScore : lblCurrentHomePlayer.Text = lblHomePlayer3.Text : lblHomePlayer3.ForeColor = LightBlue : txtHomePoints3.BackColor = Blue : Exit Sub
        If txtHomePoints3.Text = "" Then txtHomePoints3.Text = inScore : lblCurrentHomePlayer.Text = lblHomePlayer4.Text : lblHomePlayer4.ForeColor = LightBlue : txtHomePoints4.BackColor = Blue : Exit Sub
        If txtHomePoints4.Text = "" Then txtHomePoints4.Text = inScore : lblCurrentHomePlayer.Text = lblHomePlayer5.Text : lblHomePlayer5.ForeColor = LightBlue : txtHomePoints5.BackColor = Blue : Exit Sub
        If txtHomePoints5.Text = "" Then txtHomePoints5.Text = inScore : lblCurrentHomePlayer.Text = lblHomePlayer6.Text : lblHomePlayer6.ForeColor = LightBlue : txtHomePoints6.BackColor = Blue : Exit Sub
        If txtHomePoints6.Text = "" Then txtHomePoints6.Text = inScore : lblCurrentHomePlayer.Text = lblHomePlayer7.Text : lblHomePlayer7.ForeColor = LightBlue : txtHomePoints7.BackColor = Blue : Exit Sub
        If txtHomePoints7.Text = "" Then txtHomePoints7.Text = inScore : lblCurrentHomePlayer.Text = lblHomePlayer8.Text : lblHomePlayer8.ForeColor = LightBlue : txtHomePoints8.BackColor = Blue : Exit Sub
        If txtHomePoints8.Text = "" Then txtHomePoints8.Text = inScore : lblCurrentHomePlayer.Text = lblHomePlayer9.Text : lblHomePlayer9.ForeColor = LightBlue : txtHomePoints9.BackColor = Blue : Exit Sub
        If txtHomePoints9.Text = "" Then txtHomePoints9.Text = inScore : lblCurrentHomePlayer.Text = lblHomePlayer10.Text : lblHomePlayer10.ForeColor = LightBlue : txtHomePoints10.BackColor = Blue : Exit Sub
        If txtHomePoints10.Text = "" Then txtHomePoints10.Text = inScore : lblCurrentHomePlayer.Text = lblHomePlayer11.Text : lblHomePlayer11.ForeColor = LightBlue : txtHomePoints11.BackColor = Blue : Exit Sub
        If txtHomePoints11.Text = "" Then txtHomePoints11.Text = inScore : lblCurrentHomePlayer.Text = lblHomePlayer12.Text : lblHomePlayer12.ForeColor = LightBlue : txtHomePoints12.BackColor = Blue : Exit Sub
        If txtHomePoints12.Text = "" Then txtHomePoints12.Text = inScore : lblCurrentHomePlayer.Text = "" : lblCurrentAwayPlayer.Text = lblAwayPlayer1.Text : lblAwayPlayer1.ForeColor = LightBlue : txtAwayPoints1.BackColor = Blue : calc_totals() : Exit Sub

        If txtAwayPoints1.Text = "" Then txtAwayPoints1.Text = inScore : lblCurrentAwayPlayer.Text = lblAwayPlayer2.Text : lblAwayPlayer2.ForeColor = LightBlue : txtAwayPoints2.BackColor = Blue : Exit Sub
        If txtAwayPoints2.Text = "" Then txtAwayPoints2.Text = inScore : lblCurrentAwayPlayer.Text = lblAwayPlayer3.Text : lblAwayPlayer3.ForeColor = LightBlue : txtAwayPoints3.BackColor = Blue : Exit Sub
        If txtAwayPoints3.Text = "" Then txtAwayPoints3.Text = inScore : lblCurrentAwayPlayer.Text = lblAwayPlayer4.Text : lblAwayPlayer4.ForeColor = LightBlue : txtAwayPoints4.BackColor = Blue : Exit Sub
        If txtAwayPoints4.Text = "" Then txtAwayPoints4.Text = inScore : lblCurrentAwayPlayer.Text = lblAwayPlayer5.Text : lblAwayPlayer5.ForeColor = LightBlue : txtAwayPoints5.BackColor = Blue : Exit Sub
        If txtAwayPoints5.Text = "" Then txtAwayPoints5.Text = inScore : lblCurrentAwayPlayer.Text = lblAwayPlayer6.Text : lblAwayPlayer6.ForeColor = LightBlue : txtAwayPoints6.BackColor = Blue : Exit Sub
        If txtAwayPoints6.Text = "" Then txtAwayPoints6.Text = inScore : lblCurrentAwayPlayer.Text = lblAwayPlayer7.Text : lblAwayPlayer7.ForeColor = LightBlue : txtAwayPoints7.BackColor = Blue : Exit Sub
        If txtAwayPoints7.Text = "" Then txtAwayPoints7.Text = inScore : lblCurrentAwayPlayer.Text = lblAwayPlayer8.Text : lblAwayPlayer8.ForeColor = LightBlue : txtAwayPoints8.BackColor = Blue : Exit Sub
        If txtAwayPoints8.Text = "" Then txtAwayPoints8.Text = inScore : lblCurrentAwayPlayer.Text = lblAwayPlayer9.Text : lblAwayPlayer9.ForeColor = LightBlue : txtAwayPoints9.BackColor = Blue : Exit Sub
        If txtAwayPoints9.Text = "" Then txtAwayPoints9.Text = inScore : lblCurrentAwayPlayer.Text = lblAwayPlayer10.Text : lblAwayPlayer10.ForeColor = LightBlue : txtAwayPoints10.BackColor = Blue : Exit Sub
        If txtAwayPoints10.Text = "" Then txtAwayPoints10.Text = inScore : lblCurrentAwayPlayer.Text = lblAwayPlayer11.Text : lblAwayPlayer11.ForeColor = LightBlue : txtAwayPoints11.BackColor = Blue : Exit Sub
        If txtAwayPoints11.Text = "" Then txtAwayPoints11.Text = inScore : lblCurrentAwayPlayer.Text = lblAwayPlayer12.Text : lblAwayPlayer12.ForeColor = LightBlue : txtAwayPoints12.BackColor = Blue : Exit Sub
        If txtAwayPoints12.Text = "" Then
            lblCurrentAwayPlayer.Text = ""
            txtAwayPoints12.Text = inScore
            calc_totals()
            btnUpdate.Focus()
        End If


    End Sub

    Protected Sub btnReset_Click(sender As Object, e As System.EventArgs) Handles btnReset.Click

    End Sub
End Class

