Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
Imports System.Diagnostics
Imports System.IO
Imports System.Windows.Forms

Partial Class Admin_Add_Edit_Result_2
    Inherits System.Web.UI.Page
    Private dt As DataTable '= New DataTable
    Private dr As DataRow = Nothing
    Private gRow As Integer
    Private fixture_id As Integer
    Private objGlobals As New Globals
    Private home_result As String
    Private away_result As String
    Private SelectedHomePlayers As String
    Private SelectedAwayPlayers As String
    Private FixtureWeek As Integer
    Private TeamSelected As String
    Private HomePoints As Integer
    Private HomeRollsWon As Single
    Private HomeRollsTotal As Integer
    Private AwayPoints As Integer
    Private AwayRollsWon As Single
    Private AwayRollsTotal As Integer
    Private FixtureStatus As Integer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("mens_skit")
        objGlobals.CurrentUser = "mens_skit_user"
        objGlobals.CurrentSchema = "mens_skit."
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
            rbResults.Visible = False
            lblAddNewHomePlayer.Visible = False
            lblAddNewAwayPlayer.Visible = False
            btnAdd1.Visible = False
            btnAdd2.Visible = False
            txtAddHomePlayer.Visible = False
            txtAddAwayPlayer.Visible = False
            btnUpdate.Visible = False
            btnReset.Visible = False
            Call objGlobals.store_page("ATTACK FROM " & Request.Url.OriginalString, objGlobals.AdminLogin)
            Exit Sub
        End If
        ' If objGlobals.LiveTestFlag <> 3 Then
        'btnRandom.Visible = True
        'Else
        btnRandom.Visible = False
        'End If
        fixture_id = Request.QueryString("ID")
        FixtureWeek = Request.QueryString("Week")
        TeamSelected = Request.QueryString("Team")
        Call load_result()
        If Not IsPostBack Then
            Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
            Call load_details()
            rbResults.Visible = False
        End If
    End Sub

    Protected Sub load_result()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        strSQL = "SELECT * FROM mens_skit.vw_fixtures WHERE fixture_id = " & fixture_id
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            lblDate.Text = myDataReader.Item("fixture_date")
            lblLeague.Text = myDataReader.Item("league")
            lblHomeTeam.Text = myDataReader.Item("home_team_name")
            lblAwayTeam.Text = myDataReader.Item("away_team_name")
            lblResult.Text = myDataReader.Item("home_result")
            lblRolls.Text = myDataReader.Item("home_rolls_result")
            HomePoints = myDataReader.Item("home_points")
            AwayPoints = myDataReader.Item("away_points")
            HomeRollsWon = myDataReader.Item("home_rolls_won")
            AwayRollsWon = myDataReader.Item("away_rolls_won")
            HomeRollsTotal = myDataReader.Item("home_rolls_total")
            AwayRollsTotal = myDataReader.Item("away_rolls_total")
            FixtureStatus = myDataReader.Item("status")
            lblID.Text = fixture_id
        End While

        colour_totals()

        If TeamSelected Is Nothing Then
            btnReset.PostBackUrl = "~/Mens_Skit/Admin/Fixture Result.aspx?ID=" & fixture_id & "&Week=" & FixtureWeek
        Else
            btnReset.PostBackUrl = "~/Mens_Skit/Admin/Fixture Result.aspx?ID=" & fixture_id & "&Week=" & FixtureWeek & "&League=" & lblLeague.Text & "&Team=" & TeamSelected
        End If
        btnAutoScores.Visible = True
    End Sub


    Sub load_details()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        gRow = 0

        dt = New DataTable

        'add header row
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("HomePlayerAvailable", GetType(System.String)))
        dt.Columns.Add(New DataColumn("HomePlayerSelected", GetType(System.String)))
        dt.Columns.Add(New DataColumn("HomeRollTotal", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Match", GetType(System.String)))
        dt.Columns.Add(New DataColumn("AwayRollTotal", GetType(System.String)))
        dt.Columns.Add(New DataColumn("AwayPlayerSelected", GetType(System.String)))
        dt.Columns.Add(New DataColumn("AwayPlayerAvailable", GetType(System.String)))
        dr = dt.NewRow
        dt.Rows.Add(dr)

        If Not IsPostBack Then
            strSQL = "EXEC mens_skit.sp_get_result " + fixture_id.ToString + "," + FixtureStatus.ToString
            myDataReader = objGlobals.SQLSelect(strSQL)
        End If
        strSQL = "SELECT * FROM mens_skit.fixtures_detail_temp WHERE fixture_id = " & fixture_id & " ORDER BY 1"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            With gridResult
                dr = dt.NewRow
                dr("HomePlayerAvailable") = myDataReader.Item("home_player_available")
                dr("HomePlayerSelected") = myDataReader.Item("home_player_selected")
                dr("HomeRollTotal") = myDataReader.Item("home_roll_total")
                dr("Match") = ""
                If Not IsDBNull(myDataReader.Item("match")) Then
                    If myDataReader.Item("match") <= 12 Then
                        dr("Match") = myDataReader.Item("match")
                    End If
                End If
                dr("AwayRollTotal") = myDataReader.Item("away_roll_total")
                dr("AwayPlayerSelected") = myDataReader.Item("away_player_selected")
                dr("AwayPlayerAvailable") = myDataReader.Item("away_player_available")
                dt.Rows.Add(dr)
                gRow = gRow + 1
            End With
        End While

        gridResult.DataSource = dt
        gridResult.DataBind()


        remove_scores()

        If gridResult.Rows.Count > 1 Then calc_roll_totals()

        colour_totals()

    End Sub

    Sub remove_scores()
        'remove scores for matches 13+
        For iRow As Integer = 13 To gridResult.Rows.Count - 1
            Dim TXT1 As System.Web.UI.WebControls.TextBox = gridResult.Rows(iRow).Cells(2).FindControl("txtHomeScore")
            gridResult.Rows(iRow).Cells(2).Controls.Remove(TXT1)
            Dim TXT2 As System.Web.UI.WebControls.TextBox = gridResult.Rows(iRow).Cells(4).FindControl("txtAwayScore")
            gridResult.Rows(iRow).Cells(4).Controls.Remove(TXT2)
        Next
    End Sub

    Sub calc_roll_totals()
        HomeRollsTotal = 0
        AwayRollsTotal = 0
        Dim MaxRow As Integer = 12
        Dim txt1 As New System.Web.UI.WebControls.TextBox
        Dim txt2 As New System.Web.UI.WebControls.TextBox

        If gridResult.Rows.Count < MaxRow Then MaxRow = gridResult.Rows.Count - 1
        For iRow As Integer = 1 To MaxRow
            txt1 = gridResult.Rows(iRow).FindControl("txtHomeRollTotal")
            txt2 = gridResult.Rows(iRow).FindControl("txtAwayRollTotal")
            HomeRollsTotal = HomeRollsTotal + Val(txt1.Text)
            AwayRollsTotal = AwayRollsTotal + Val(txt2.Text)
        Next
        lblHomeTotal.Text = HomeRollsTotal
        lblAwayTotal.Text = AwayRollsTotal
    End Sub

    Sub colour_totals()

        lblHomePoints.Text = HomePoints
        lblAwayPoints.Text = AwayPoints

        lblHomeRolls.Text = HomeRollsWon
        lblAwayRolls.Text = AwayRollsWon

        lblHomeTotal.Text = HomeRollsTotal
        lblAwayTotal.Text = AwayRollsTotal

        lblHomeTotal.BackColor = Orange
        lblHomeTotal.ForeColor = Black

        lblAwayTotal.BackColor = Orange
        lblAwayTotal.ForeColor = Black

        lblHomePoints.BackColor = Orange
        lblHomePoints.ForeColor = Black

        lblAwayPoints.BackColor = Orange
        lblAwayPoints.ForeColor = Black

        lblHomeRolls.BackColor = Orange
        lblHomeRolls.ForeColor = Black

        lblAwayRolls.BackColor = Orange
        lblAwayRolls.ForeColor = Black

        Select Case True
            Case HomePoints > AwayPoints
                lblHomePoints.BackColor = Green
                lblHomePoints.ForeColor = White
                lblAwayPoints.BackColor = Red
            Case HomePoints < AwayPoints
                lblHomePoints.BackColor = Red
                lblAwayPoints.BackColor = Green
                lblAwayPoints.ForeColor = White
        End Select

        Select Case True
            Case HomeRollsWon > AwayRollsWon
                lblHomeRolls.BackColor = Green
                lblHomeRolls.ForeColor = White
                lblAwayRolls.BackColor = Red
            Case HomeRollsWon < AwayRollsWon
                lblHomeRolls.BackColor = Red
                lblAwayRolls.BackColor = Green
                lblAwayRolls.ForeColor = White
        End Select

        Select Case True
            Case HomeRollsTotal > AwayRollsTotal
                lblHomeTotal.BackColor = Green
                lblHomeTotal.ForeColor = White
                lblAwayTotal.BackColor = Red
            Case HomeRollsTotal < AwayRollsTotal
                lblHomeTotal.BackColor = Red
                lblAwayTotal.BackColor = Green
                lblAwayTotal.ForeColor = White
        End Select

    End Sub


    Protected Sub btnRandom_Click(sender As Object, e As System.EventArgs) Handles btnRandom.Click
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim Button1 As New System.Web.UI.WebControls.Button
        Dim Button2 As New System.Web.UI.WebControls.Button
        Dim TXT1 As New System.Web.UI.WebControls.TextBox
        Dim TXT2 As New System.Web.UI.WebControls.TextBox
        Dim HomeScore As String
        Dim AwayScore As String
        Dim SelectedCount As Integer
        Dim PlayerNo As Integer
        Dim RandomNumber = New Random()
        strSQL = "DELETE FROM mens_skit.vw_fixtures_detail WHERE fixture_id = " + fixture_id.ToString
        myDataReader = objGlobals.SQLSelect(strSQL)
        strSQL = "EXEC mens_skit.sp_get_result " + fixture_id.ToString + "," + FixtureStatus.ToString
        myDataReader = objGlobals.SQLSelect(strSQL)
        load_result()
        load_details()
        SelectedCount = 0
        While SelectedCount < 12
            PlayerNo = RandomNumber.Next(1, gridResult.Rows.Count)
            Button1 = gridResult.Rows(PlayerNo).FindControl("HomePlayerAvailable")
            While Button1.Text = ""
                PlayerNo = RandomNumber.Next(1, gridResult.Rows.Count)
                Button1 = gridResult.Rows(PlayerNo).FindControl("HomePlayerAvailable")
            End While
            SelectedCount = SelectedCount + 1
            Button2 = gridResult.Rows(SelectedCount).FindControl("HomePlayerSelected")
            Button2.Text = Button1.Text
            Button1.Text = ""
        End While

        SelectedCount = 0
        While SelectedCount < 12
            PlayerNo = RandomNumber.Next(1, gridResult.Rows.Count)
            Button1 = gridResult.Rows(PlayerNo).FindControl("AwayPlayerAvailable")
            While Button1.Text = ""
                PlayerNo = RandomNumber.Next(1, gridResult.Rows.Count)
                Button1 = gridResult.Rows(PlayerNo).FindControl("AwayPlayerAvailable")
            End While
            SelectedCount = SelectedCount + 1
            Button2 = gridResult.Rows(SelectedCount).FindControl("AwayPlayerSelected")
            Button2.Text = Button1.Text
            Button1.Text = ""
        End While

        For iRow As Integer = 1 To 12
            TXT1 = gridResult.Rows(iRow).FindControl("txtHomeRollTotal")
            TXT1.Text = ""
            HomeScore = CStr(RandomNumber.Next(12, 34))
            enter_skittles_score_player(HomeScore)
        Next

        For iRow As Integer = 1 To 12
            TXT2 = gridResult.Rows(iRow).FindControl("txtAwayRollTotal")
            TXT2.Text = ""
            AwayScore = CStr(RandomNumber.Next(12, 34))
            enter_skittles_score_player(AwayScore)
        Next
        calc_roll_totals()
        colour_totals()
        btnUpdate.Focus()
    End Sub

    Protected Sub gridResult_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridResult.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim selectedRow As GridViewRow = gridResult.Rows(index)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        Dim Button1 As New System.Web.UI.WebControls.Button
        Dim Button2 As New System.Web.UI.WebControls.Button
        Dim Button3 As New System.Web.UI.WebControls.Button
        Dim Button4 As New System.Web.UI.WebControls.Button

        Select Case e.CommandName
            Case "HomePlayerAvailable"      'player is available - find slot to be selected
                Button1 = gridResult.Rows(index).FindControl("HomePlayerAvailable")
                For iRow As Integer = 1 To 12
                    Button2 = gridResult.Rows(iRow).FindControl("HomePlayerSelected")
                    If Button2.Text = "" Then
                        Button2.Text = Button1.Text
                        Button1.Text = ""
                        strSQL = "UPDATE mens_skit.fixtures_detail_temp SET home_player_available = '' WHERE fixture_id = " & fixture_id & " AND home_player_available =  '" + Button2.Text + "'"
                        myDataReader = objGlobals.SQLSelect(strSQL)
                        strSQL = "UPDATE mens_skit.fixtures_detail_temp SET home_player_selected = '" + Button2.Text + "' WHERE fixture_id = " & fixture_id & " AND match =  " + iRow.ToString
                        myDataReader = objGlobals.SQLSelect(strSQL)
                        Exit For
                    End If
                Next
            Case "HomePlayerSelected"      'player is selected - find slot to be available
                Button2 = gridResult.Rows(index).FindControl("HomePlayerSelected")
                For iRow As Integer = 1 To gridResult.Rows.Count - 1
                    Button1 = gridResult.Rows(iRow).FindControl("HomePlayerAvailable")
                    If Button1.Text = "" Then
                        Button1.Text = Button2.Text
                        Button2.Text = ""
                        strSQL = "UPDATE mens_skit.fixtures_detail_temp SET home_player_available = '" + Button1.Text + "' WHERE fixture_id = " & fixture_id & " AND match =  " + iRow.ToString
                        myDataReader = objGlobals.SQLSelect(strSQL)
                        strSQL = "UPDATE mens_skit.fixtures_detail_temp SET home_player_selected = '' WHERE fixture_id = " & fixture_id & " AND match =  " + index.ToString
                        myDataReader = objGlobals.SQLSelect(strSQL)
                        Exit For
                    End If
                Next
            Case "AwayPlayerAvailable"      'player is available - find slot to be selected
                Button3 = gridResult.Rows(index).FindControl("AwayPlayerAvailable")
                For iRow As Integer = 1 To 12
                    Button4 = gridResult.Rows(iRow).FindControl("AwayPlayerSelected")
                    If Button4.Text = "" Then
                        Button4.Text = Button3.Text
                        Button3.Text = ""
                        strSQL = "UPDATE mens_skit.fixtures_detail_temp SET away_player_available = '' WHERE fixture_id = " & fixture_id & " AND away_player_available =  '" + Button4.Text + "'"
                        myDataReader = objGlobals.SQLSelect(strSQL)
                        strSQL = "UPDATE mens_skit.fixtures_detail_temp SET away_player_selected = '" + Button4.Text + "' WHERE fixture_id = " & fixture_id & " AND match =  " + iRow.ToString
                        myDataReader = objGlobals.SQLSelect(strSQL)
                        Exit For
                    End If
                Next
            Case "AwayPlayerSelected"      'player is selected - find slot to be available
                Button4 = gridResult.Rows(index).FindControl("AwayPlayerSelected")
                For iRow As Integer = 1 To gridResult.Rows.Count - 1
                    Button3 = gridResult.Rows(iRow).FindControl("AwayPlayerAvailable")
                    If Button3.Text = "" Then
                        Button3.Text = Button4.Text
                        Button4.Text = ""
                        strSQL = "UPDATE mens_skit.fixtures_detail_temp SET away_player_available = '" + Button3.Text + "' WHERE fixture_id = " & fixture_id & " AND match =  " + iRow.ToString
                        myDataReader = objGlobals.SQLSelect(strSQL)
                        strSQL = "UPDATE mens_skit.fixtures_detail_temp SET away_player_selected = '' WHERE fixture_id = " & fixture_id & " AND match =  " + index.ToString
                        myDataReader = objGlobals.SQLSelect(strSQL)
                        Exit For
                    End If
                Next
        End Select
        load_details()
    End Sub

    Protected Sub gridResult_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResult.RowDataBound
        Dim Button1 As New System.Web.UI.WebControls.Button
        Dim Button2 As New System.Web.UI.WebControls.Button
        Dim Button3 As New System.Web.UI.WebControls.Button
        Dim Button4 As New System.Web.UI.WebControls.Button
        Dim txt1 As New System.Web.UI.WebControls.TextBox
        Dim txt2 As New System.Web.UI.WebControls.TextBox
        If e.Row.RowType = DataControlRowType.DataRow And e.Row.RowIndex > 0 Then
            Dim iRow As Integer = e.Row.RowIndex
            Button1 = e.Row.FindControl("HomePlayerAvailable")
            Button2 = e.Row.FindControl("HomePlayerSelected")
            Button3 = e.Row.FindControl("AwayPlayerSelected")
            Button4 = e.Row.FindControl("AwayPlayerAvailable")
            txt1 = e.Row.FindControl("txtHomeRollTotal")
            txt2 = e.Row.FindControl("txtAwayRollTotal")

            'home player avail
            If dt.Rows(iRow)(0) <> "" Then
                Button1.Text = dt.Rows(iRow)(0)
            Else
                Button1.BackColor = Black
                Button1.BorderStyle = WebControls.BorderStyle.None
            End If

            'home player selected
            If dt.Rows(iRow)(1) <> "" Then
                Button2.Text = dt.Rows(iRow)(1)
            Else
                Button2.BackColor = Black
                Button2.BorderStyle = WebControls.BorderStyle.None
            End If

            'home score
            txt1.Text = ""
            If Not IsDBNull(dt.Rows(iRow)(2)) Then
                If dt.Rows(iRow)(2) > 0 Then
                    txt1.Text = dt.Rows(iRow)(2)
                End If
            End If

            'away score
            txt2.Text = ""
            If Not IsDBNull(dt.Rows(iRow)(4)) Then
                If dt.Rows(iRow)(4) > 0 Then
                    txt2.Text = dt.Rows(iRow)(4)
                End If
            End If

            'away player selected
            If dt.Rows(iRow)(5) <> "" Then
                Button3.Text = dt.Rows(iRow)(5)
            Else
                Button3.BackColor = Black
                Button3.BorderStyle = WebControls.BorderStyle.None
            End If

            'away player avail
            If dt.Rows(iRow)(6) <> "" Then
                Button4.Text = dt.Rows(iRow)(6)
            Else
                Button4.BackColor = Black
                Button4.BorderStyle = WebControls.BorderStyle.None
            End If

            If e.Row.RowIndex > 12 Then
                Button2.BackColor = Red
                txt1.BackColor = Red
                txt1.BorderStyle = WebControls.BorderStyle.None
                txt2.BackColor = Red
                txt2.BorderStyle = WebControls.BorderStyle.None
                Button3.BackColor = Red
            End If
        Else
            If e.Row.RowIndex = 0 Then e.Row.Visible = False
        End If
    End Sub

    Protected Sub update_grid_home(inMatch As Integer, inHomeScore As Double)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim Button1 As New System.Web.UI.WebControls.Button
        Dim Button2 As New System.Web.UI.WebControls.Button

        Button1 = gridResult.Rows(inMatch).FindControl("HomePlayerAvailable")
        Button2 = gridResult.Rows(inMatch).FindControl("HomePlayerSelected")

        strSQL = "UPDATE mens_skit.fixtures_detail_temp SET "
        strSQL = strSQL + "home_player_available = '" + Button1.Text + "',"
        strSQL = strSQL + "home_player_selected = '" + Button2.Text + "',"
        strSQL = strSQL + "home_roll_total = " + inHomeScore.ToString
        strSQL = strSQL + " WHERE fixture_id = " & fixture_id & " AND match =  " + inMatch.ToString
        myDataReader = objGlobals.SQLSelect(strSQL)

    End Sub

    Protected Sub update_grid_away(inMatch As Integer, inAwayScore As Double)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim Button3 As New System.Web.UI.WebControls.Button
        Dim Button4 As New System.Web.UI.WebControls.Button

        Button3 = gridResult.Rows(inMatch).FindControl("AwayPlayerSelected")
        Button4 = gridResult.Rows(inMatch).FindControl("AwayPlayerAvailable")

        strSQL = "UPDATE mens_skit.fixtures_detail_temp SET "
        strSQL = strSQL + "away_roll_total = " + inAwayScore.ToString + ","
        strSQL = strSQL + "away_player_selected =  '" + Button3.Text + "',"
        strSQL = strSQL + "away_player_available = '" + Button4.Text + "'"
        strSQL = strSQL + " WHERE fixture_id = " & fixture_id & " AND match =  " + inMatch.ToString
        myDataReader = objGlobals.SQLSelect(strSQL)

    End Sub

    Protected Sub btnAutoScores_Click(sender As Object, e As System.EventArgs) Handles btnAutoScores.Click
        Dim Button1 As New System.Web.UI.WebControls.Button
        Dim Button2 As New System.Web.UI.WebControls.Button

        Dim OKproceed As Boolean = True
        For iRow As Integer = 1 To 12
            Button1 = gridResult.Rows(iRow).FindControl("HomePlayerSelected")
            If Button1.Text = "" Then
                gridResult.Rows(iRow).Cells(1).BackColor = Red
                OKproceed = False
            End If
            Button2 = gridResult.Rows(iRow).FindControl("AwayPlayerSelected")
            If Button2.Text = "" Then
                gridResult.Rows(iRow).Cells(5).BackColor = Red
                OKproceed = False
            End If
        Next

        If OKproceed Then
            btn0.Visible = True : btn1.Visible = True : btn2.Visible = True : btn3.Visible = True : btn4.Visible = True : btn5.Visible = True
            btn6.Visible = True : btn7.Visible = True : btn8.Visible = True : btn9.Visible = True
            btn10.Visible = True : btn11.Visible = True : btn12.Visible = True : btn13.Visible = True : btn14.Visible = True : btn15.Visible = True
            btn16.Visible = True : btn17.Visible = True : btn18.Visible = True : btn19.Visible = True
            btn20.Visible = True : btn21.Visible = True : btn22.Visible = True : btn23.Visible = True : btn24.Visible = True : btn25.Visible = True
            btn26.Visible = True : btn27.Visible = True : btn28.Visible = True : btn29.Visible = True
            btn30.Visible = True : btn31.Visible = True : btn32.Visible = True : btn33.Visible = True : btn34.Visible = True : btn35.Visible = True
            btn36.Visible = True : btn37.Visible = True : btn38.Visible = True : btn39.Visible = True : btn40.Visible = True

            Dim txt1 As New System.Web.UI.WebControls.TextBox
            Dim txt2 As New System.Web.UI.WebControls.TextBox

            For iRow As Integer = 1 To 12
                txt1 = gridResult.Rows(iRow).FindControl("txtHomeRollTotal")
                If txt1.Text = "" Then
                    gridResult.Rows(iRow).Cells(2).Focus()
                    gridResult.Rows(iRow).Cells(2).BackColor = Blue
                    Exit For
                End If
                txt2 = gridResult.Rows(iRow).FindControl("txtAwayRollTotal")
                If txt2.Text = "" Then
                    gridResult.Rows(iRow).Cells(4).Focus()
                    gridResult.Rows(iRow).Cells(4).BackColor = Blue
                    Exit For
                End If
            Next
        Else
            remove_scores()
        End If
    End Sub

    Protected Sub btnAdd1_Click(sender As Object, e As System.EventArgs) Handles btnAdd1.Click
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim Button1 As New System.Web.UI.WebControls.Button
        Dim Button2 As New System.Web.UI.WebControls.Button
        txtAddHomePlayer.Text = txtAddHomePlayer.Text.ToUpper
        For iRow As Integer = 1 To gridResult.Rows.Count - 1
            Button1 = gridResult.Rows(iRow).FindControl("HomePlayerSelected")
            Button2 = gridResult.Rows(iRow).FindControl("HomePlayerAvailable")
            If Button1.Text = txtAddHomePlayer.Text Or Button2.Text = txtAddHomePlayer.Text Then
                lblHomeExists.Visible = True
                Exit Sub
            End If
        Next
        lblHomeExists.Visible = False
        'find an available slot to add new player
        Dim Added As Boolean = False
        Dim NewRow As Integer = 0
        For iRow As Integer = 1 To gridResult.Rows.Count - 1
            Button1 = gridResult.Rows(iRow).FindControl("HomePlayerAvailable")
            If Button1.Text = "" Then
                gridResult.Rows(iRow).Cells(0).BorderColor = Red
                Button1.Text = txtAddHomePlayer.Text
                Button1.BorderColor = Red
                Button1.BorderStyle = WebControls.BorderStyle.Solid
                Added = True
                strSQL = "UPDATE mens_skit.fixtures_detail_temp SET home_player_available = '" + txtAddHomePlayer.Text + "' WHERE fixture_id = " & fixture_id & " AND ID =  " + iRow.ToString
                myDataReader = objGlobals.SQLSelect(strSQL)
                Exit For
            Else
                NewRow = NewRow + 1
            End If
        Next
        If Not Added Then
            NewRow = NewRow + 1
            If NewRow <= 12 Then
                strSQL = "INSERT INTO mens_skit.fixtures_detail_temp VALUES (" & fixture_id & ",'" & txtAddHomePlayer.Text & "','',null," & NewRow & ",null,'','')"
            Else
                strSQL = "INSERT INTO mens_skit.fixtures_detail_temp VALUES (" & fixture_id & ",'" & txtAddHomePlayer.Text & "','',null,null,null,'','')"
            End If
            myDataReader = objGlobals.SQLSelect(strSQL)
        End If
        'add to players table
        strSQL = "INSERT INTO mens_skit.players VALUES ('" & objGlobals.current_season & "',"
        strSQL = strSQL & "'" & lblLeague.Text & "',"
        strSQL = strSQL & "'" & lblHomeTeam.Text & "',"
        strSQL = strSQL & "'" & txtAddHomePlayer.Text & "',NULL,0)"
        myDataReader = objGlobals.SQLSelect(strSQL)

        load_details()
        txtAddHomePlayer.Text = ""
        txtAddHomePlayer.Focus()
    End Sub

    Protected Sub btnAdd2_Click(sender As Object, e As System.EventArgs) Handles btnAdd2.Click
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim Button3 As New System.Web.UI.WebControls.Button
        Dim Button4 As New System.Web.UI.WebControls.Button
        txtAddAwayPlayer.Text = txtAddAwayPlayer.Text.ToUpper
        For iRow As Integer = 1 To gridResult.Rows.Count - 1
            Button3 = gridResult.Rows(iRow).FindControl("AwayPlayerSelected")
            Button4 = gridResult.Rows(iRow).FindControl("AwayPlayerAvailable")
            If Button3.Text = txtAddAwayPlayer.Text Or Button4.Text = txtAddAwayPlayer.Text Then
                lblAwayExists.Visible = True
                Exit Sub
            End If
        Next
        lblAwayExists.Visible = False
        'find an available slot to add new player
        Dim Added As Boolean = False
        Dim NewRow As Integer = 0
        For iRow As Integer = 1 To gridResult.Rows.Count - 1
            Button4 = gridResult.Rows(iRow).FindControl("AwayPlayerAvailable")
            If Button4.Text = "" Then
                Button4.Text = txtAddAwayPlayer.Text
                Button4.BackColor = LightGray
                Button4.BorderStyle = WebControls.BorderStyle.Solid
                Button4.BorderColor = Red
                Added = True
                strSQL = "UPDATE mens_skit.fixtures_detail_temp SET away_player_available = '" + txtAddAwayPlayer.Text + "' WHERE fixture_id = " & fixture_id & " AND ID =  " + iRow.ToString
                myDataReader = objGlobals.SQLSelect(strSQL)
                Exit For
            Else
                NewRow = NewRow + 1
            End If
        Next
        If Not Added Then
            NewRow = NewRow + 1
            strSQL = "INSERT INTO mens_skit.fixtures_detail_temp VALUES (" & fixture_id & ",'','',null,null,null,'','" & txtAddAwayPlayer.Text & "')"
            myDataReader = objGlobals.SQLSelect(strSQL)
        End If
        'add to players table
        strSQL = "INSERT INTO mens_skit.players VALUES ('" & objGlobals.current_season & "',"
        strSQL = strSQL & "'" & lblLeague.Text & "',"
        strSQL = strSQL & "'" & lblAwayTeam.Text & "',"
        strSQL = strSQL & "'" & txtAddAwayPlayer.Text & "',NULL,0)"
        myDataReader = objGlobals.SQLSelect(strSQL)

        load_details()
        txtAddAwayPlayer.Text = ""
        txtAddAwayPlayer.Focus()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click
        Dim txt1 As New System.Web.UI.WebControls.TextBox
        Dim txt2 As New System.Web.UI.WebControls.TextBox
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        're-update in case scores have been changed
        For iRow As Integer = 1 To 12
            txt1 = gridResult.Rows(iRow).FindControl("txtHomeRollTotal")
            update_grid_home(iRow, Val(txt1.Text))
        Next

        For iRow As Integer = 1 To 12
            txt2 = gridResult.Rows(iRow).FindControl("txtAwayRollTotal")
            update_grid_away(iRow, Val(txt2.Text))
        Next

        calc_roll_totals()
        colour_totals()

        load_details()
        update_fixture_details()
        update_player_stats("sp_update_player_stats")

        'update league AND team positions
        strSQL = "EXEC mens_skit.sp_update_league_position '" & lblLeague.Text & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        strSQL = "EXEC mens_skit.sp_update_team_position '" & lblLeague.Text & " ','" & lblHomeTeam.Text & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        strSQL = "EXEC mens_skit.sp_update_team_position '" & lblLeague.Text & "','" & lblAwayTeam.Text & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        calc_roll_totals()
        colour_totals()
        If (HomeRollsTotal > AwayRollsTotal And HomePoints <= AwayPoints) Or (HomeRollsTotal < AwayRollsTotal And HomePoints >= AwayPoints) Or (HomeRollsTotal = AwayRollsTotal And HomePoints <> AwayPoints) Then
            lstErrors.Items.Clear()
            lstErrors.Items.Add("Result is different from the Original Result.")
            lstErrors.Items.Add("Click 'Re-Update' to update the Fixture result.")
            lstErrors.Items.Add("Fixture details have been saved")
            lstErrors.Visible = True
            btnReUpdate.Visible = True
            btnReUpdate.PostBackUrl = "~/Mens_Skit/Admin/Fixture Result.aspx?ID= " & fixture_id & "&Week=" & FixtureWeek
            btnReUpdate.Focus()
            btnUpdate.Visible = False
        Else
            update_header()

            '19.11.18 - update fixture_combined table
            Call objGlobals.update_fixtures_combined("mens_skit")

            If TeamSelected Is Nothing Then
                Response.Redirect("~/Mens_Skit/Default.aspx?Week=" & FixtureWeek)
            Else
                Response.Redirect("~/Mens_Skit/Team Fixtures.aspx?League=" & lblLeague.Text & "&Team=" & TeamSelected)
            End If
        End If
    End Sub

    Sub update_player_stats(inStoredProcedure As String)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim myDataReader2 As oledbdatareader
        Dim tempSeason As String = objGlobals.get_current_season
        inStoredProcedure = "mens_skit." & inStoredProcedure
        'update the home team players
        strSQL = "SELECT player FROM mens_skit.vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblHomeTeam.Text & "' AND player NOT LIKE 'A N OTHER%' "
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            strSQL = "EXEC " & inStoredProcedure & " '" & tempSeason & "','" & lblLeague.Text & "','" & lblHomeTeam.Text & "','" & myDataReader.Item("player") & "'," & fixture_id.ToString
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While

        'update the away team players
        strSQL = "SELECT player FROM mens_skit.vw_players WHERE league = '" & lblLeague.Text & "' AND team = '" & lblAwayTeam.Text & "' AND player NOT LIKE 'A N OTHER%' "
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            strSQL = "EXEC " & inStoredProcedure & " '" & tempSeason & "','" & lblLeague.Text & "','" & lblAwayTeam.Text & "','" & myDataReader.Item("player") & "'," & fixture_id.ToString
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
    End Sub

    Sub update_header()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim home_result As String
        Dim away_result As String
        home_result = Replace(HomeRollsWon, ".5", "½")
        home_result = Replace(home_result, "0½", "½")
        away_result = Replace(AwayRollsWon, ".5", "½")
        away_result = Replace(away_result, "0½", "½")

        strSQL = "UPDATE mens_skit.vw_fixtures Set home_rolls_won = " & HomeRollsWon & ",away_rolls_won = " & AwayRollsWon
        strSQL = strSQL & ",home_points = " & Val(lblHomePoints.Text) & ",away_points = " & Val(lblAwayPoints.Text)
        strSQL = strSQL & ",home_rolls_total= " & HomeRollsTotal & ",away_rolls_total = " & AwayRollsTotal
        strSQL = strSQL & ",home_result = '" & lblHomePoints.Text & " - " & lblAwayPoints.Text & "'"
        strSQL = strSQL & ",away_result = '" & lblAwayPoints.Text & " - " & lblHomePoints.Text & "'"
        strSQL = strSQL & ",home_rolls_result = '" & home_result & " - " & away_result & "'"
        strSQL = strSQL & ",away_rolls_result = '" & away_result & " - " & home_result & "'"
        strSQL = strSQL & ",status = 2 WHERE fixture_id = " & fixture_id
        myDataReader = objGlobals.SQLSelect(strSQL)
    End Sub


    Sub update_fixture_details()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        strSQL = "DELETE FROM mens_skit.vw_fixtures_detail WHERE fixture_id = " & fixture_id
        myDataReader = objGlobals.SQLSelect(strSQL)

        strSQL = "INSERT INTO mens_skit.fixtures_detail "
        strSQL = strSQL & "SELECT '" & objGlobals.current_season & "',"
        strSQL = strSQL & fixture_id & ","
        strSQL = strSQL & "'" & lblLeague.Text & "',"
        strSQL = strSQL & "'" & lblDate.Text & "',"
        strSQL = strSQL & "GETDATE(), "
        strSQL = strSQL & FixtureWeek & ","
        strSQL = strSQL & "match,"
        strSQL = strSQL & "'" & lblHomeTeam.Text & "',"
        strSQL = strSQL & "home_player_selected,"
        strSQL = strSQL & "home_roll_total,"
        strSQL = strSQL & "'" & lblAwayTeam.Text & "',"
        strSQL = strSQL & "away_player_selected,"
        strSQL = strSQL & "away_roll_total,"
        strSQL = strSQL & "CASE WHEN away_roll_total >= 30 THEN 1 ELSE 0 END,"
        strSQL = strSQL & "null" 'fixture short date (updated below)
        strSQL = strSQL & " FROM  mens_skit.fixtures_detail_temp WHERE fixture_id = " & fixture_id & " AND match <= 12 ORDER BY match"
        myDataReader = objGlobals.SQLSelect(strSQL)

        'update the fixture_calendar date FROM mens_skit.vw_fixtures
        strSQL = "UPDATE mens_skit.vw_fixtures_detail SET fixture_calendar = (SELECT fixture_calendar FROM mens_skit.vw_fixtures WHERE fixture_id = " & fixture_id & ") WHERE fixture_id = " & fixture_id
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

        'update the fixture_calendar short date FROM mens_skit.vw_fixtures
        strSQL = "UPDATE mens_skit.vw_fixtures_detail SET fixture_short_date = (SELECT fixture_short_date FROM mens_skit.vw_fixtures WHERE fixture_id = " & fixture_id & ") WHERE fixture_id = " & fixture_id
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()
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

    Private Sub enter_skittles_score_player(inScore As String)
        Dim txt1 As New System.Web.UI.WebControls.TextBox
        Dim txt2 As New System.Web.UI.WebControls.TextBox

        Dim ScoreEntered As Boolean = False
        For iRow As Integer = 1 To 12
            txt1 = gridResult.Rows(iRow).FindControl("txtHomeRollTotal")
            If txt1.Text = "" Then
                txt1.Text = inScore
                txt1.BackColor = DarkCyan
                update_grid_home(iRow, Val(inScore))
                ScoreEntered = True
                calc_roll_totals()
                If iRow < 12 Then
                    gridResult.Rows(iRow + 1).Cells(2).BackColor = Blue
                    Exit For
                Else
                    colour_totals()
                    gridResult.Rows(1).Cells(4).BackColor = Blue
                End If
            End If
        Next

        If ScoreEntered Then Exit Sub

        For iRow As Integer = 1 To 12
            txt2 = gridResult.Rows(iRow).FindControl("txtAwayRollTotal")
            If txt2.Text = "" Then
                txt2.Text = inScore
                txt2.BackColor = DarkCyan
                update_grid_away(iRow, Val(inScore))
                calc_roll_totals()
                If iRow < 12 Then
                    gridResult.Rows(iRow + 1).Cells(4).BackColor = Blue
                    Exit For
                Else
                    colour_totals()
                    btnUpdate.Focus()
                End If
            End If
        Next

    End Sub

    Protected Sub btnReset_Click(sender As Object, e As System.EventArgs) Handles btnReset.Click

    End Sub

    Protected Sub btnReUpdate_Click(sender As Object, e As System.EventArgs) Handles btnReUpdate.Click

    End Sub
End Class
