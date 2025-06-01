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
    Private Pins(20) As Double
    Private Points_Rolls_Pins(20) As Double
    Private HomePoints As Double
    Private AwayPoints As Double
    Private FixtureDate As String
    Private FixtureFullDate As String
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
    Private FixtureStatus As Integer
    Private GroupName As String
    Private myDataReader As OleDbDataReader


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("mens_skit")
        objGlobals.CurrentUser = "mens_skit_user"
        objGlobals.CurrentSchema = "mens_skit."

        btnUpdateHeader.Visible = True

        CompID = Request.QueryString("ID")
        GroupName = Request.QueryString("Group")
        lblFixture.Text = Request.QueryString("Fixture")

        btnOK.Visible = False
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
            Call objGlobals.store_page(Request.Url.OriginalString, True)
        Else
            btnBack.Visible = False
            btnUpdateHeader.Visible = False

            btnCancel.Visible = False

            objGlobals.AdminLogin = False
            Call objGlobals.store_page("ATTACK FROM " & Request.Url.OriginalString, objGlobals.AdminLogin)
            '16.9.14 - only allow ADMIN in this screen
            Exit Sub
        End If
        btnBack.Visible = True
        btnBack.Attributes.Add("onClick", "javascript:history.back(); return false;")

        If objGlobals.PlayerSelected <> Nothing Then
            btnBack.Visible = False
        End If

        strSQL = "SELECT *,CONVERT(VARCHAR(10),fixture_calendar,112) AS Fixture_YMD FROM mens_skit.vw_fixtures_AR WHERE fixture_id=" & CompID
        myDataReader = objGlobals.SQLSelect(strSQL)
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
            FixtureDate = myDataReader.item("fixture_ymd")
            FixtureHomeTeam = myDataReader.Item("home_team_name")
            FixtureAwayTeam = myDataReader.Item("away_team_name")
            FixtureFullDate = "Date : " & myDataReader.Item("fixture_date")
            FixtureStatus = myDataReader.Item("status")
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
        End While

        'FixtureStatus = FixtureStatus()

        If Not IsPostBack Then
            If home_roll_1 = 0 Then txtHomeRoll1.BackColor = LightBlue
            rbResults.Enabled = True
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
        End If

        'If objGlobals.AdminLogin Then
        Call load_table(gridTable, "Tables_AR")
        '    lblTableAfter.Visible = False
        'Else
        '    'Call create_tables_before()
        '    'Call load_table(gridTable, "before_tables")
        '    'Call create_tables_after(FixtureDate)
        '    'Call load_table(gridTable2, "after_tables")
        'End If


        If FixtureStatus >= 1 Then
            'Call load_result()
            'rbResults.Visible = False
            'Call load_totals()
            'If FixtureStatus >= 1 Then
            '    Call load_rolls()
            'End If
        Else
            'gRow = 0
            'gridResult.Visible = True
            'gridResult.DataSource = dt
            'gridResult.DataBind()
        End If

        If objGlobals.AdminLogin Then
            rbResults.Visible = True
        End If
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
            strSQL = "UPDATE mens_skit.vw_fixtures_AR SET"
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
            strSQL = "UPDATE mens_skit.vw_fixtures_AR SET"
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
        strSQL = strSQL & " FROM mens_skit.vw_teams_AR t"
        strSQL = strSQL & " JOIN mens_skit.vw_home_tables_AR h ON (h.League = t.League AND h.Team = t.long_name)"
        strSQL = strSQL & " JOIN mens_skit.vw_away_tables_AR a ON (a.League = t.League AND a.Team = t.long_name)"
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

        Call sort_table()
        Call re_update_table("Tables_AR")
        Call load_table(gridTable2, "Tables_AR")

        Dim UKDateTime As Date = objGlobals.LondonDate(DateTime.UtcNow)
        Dim UKDate As String = Format(UKDateTime.Year, "0000") + Format(UKDateTime.Month, "00") + Format(UKDateTime.Day, "00")
        Dim UKTime As String = Format(UKDateTime.Hour, "00") & Format(UKDateTime.Minute, "00")

        If InStr(inResult, "0-0") > 0 Then NewStatus = 0

        '28.7.14 - add Status
        If InStr(inResult, "0-0") > 0 Or inResult = "Postponed" Then
            'reset status if no result
            strSQL = "UPDATE mens_skit.vw_fixtures_AR SET Status = " & NewStatus & " WHERE fixture_id = " & CompID
            myDataReader = objGlobals.SQLSelect(strSQL)
            'strSQL = "DELETE FROM mens_skit.vw_fixtures_detail WHERE fixture_id = " & CompID
            'myDataReader = objGlobals.SQLSelect(strSQL)
            'Call update_player_stats("sp_update_player_stats")

            ''re-update league AND team positions
            'strSQL = "EXEC mens_skit.sp_update_league_position '" & FixtureLeague & "'"
            'myDataReader = objGlobals.SQLSelect(strSQL)

            'strSQL = "EXEC mens_skit.sp_update_team_position '" & FixtureLeague & "','" & FixtureHomeTeam & "'"
            'myDataReader = objGlobals.SQLSelect(strSQL)

            'strSQL = "EXEC mens_skit.sp_update_team_position '" & FixtureLeague & "','" & FixtureAwayTeam & "'"
            'myDataReader = objGlobals.SQLSelect(strSQL)
        Else
            strSQL = "UPDATE mens_skit.vw_fixtures_AR SET Status = 1 WHERE fixture_id = " & CompID
            myDataReader = objGlobals.SQLSelect(strSQL)
        End If
        '28.7.14 - end

        '19.11.18 - update fixture_combined table
        Call objGlobals.update_fixtures_combined("mens_skit")

        strSQL = "UPDATE mens_skit.last_changed SET date_time_changed = '" & UKDate & UKTime & "'"

        myDataReader = objGlobals.SQLSelect(strSQL)

        btnCancel.Visible = False

    End Sub

    Protected Function get_end_week_date(ByVal inWeek As Integer) As String
        get_end_week_date = ""
        strSQL = "SELECT week_commences FROM mens_skit.vw_weeks WHERE week_number = " & inWeek
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            get_end_week_date = Format(DateAdd(DateInterval.Day, 6, myDataReader.Item(0)), "yyyyMMdd")
        End While
    End Function

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

    Sub load_table(inGrid As GridView, inTable As String)

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
                inGrid.Rows(i).Cells(7).ForeColor = Gray
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
        Response.Redirect("~/Mens_Skit/Alan Rosser Cup.aspx?Group=" & GroupName)
    End Sub

    Protected Sub btnOK_Click(sender As Object, e As System.EventArgs) Handles btnOK.Click
        Response.Redirect("~/Mens_Skit/Alan Rosser Cup.aspx?Group=" & GroupName)
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

        Dim ok_update As Boolean = True

        If rbResults.SelectedIndex = -1 Then
            rbResults.BackColor = LightSalmon
            Exit Sub
        Else
            rbResults.BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
        End If

        Call update_result(rbResults.SelectedItem.Text)

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
        End If
        btnOK.Visible = True
        btnUpdateHeader.Visible = False
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
End Class
