Imports System.Data.OleDb
Imports System.Drawing.Color
Imports System.Data
'Imports MySql.Data.MySqlClient

Partial Class Default1
    Inherits System.Web.UI.Page
    Private dt As DataTable
    Private dr As DataRow
    Private gRow As Integer
    Private objGlobals As New Globals
    Private CurrentWeek As Integer
    Private ShowChampions As Boolean
    Private current_season As String
    Private ThisLeague As String = ""
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
        'Call objGlobals.open_connection("all_skit")
        objGlobals.CurrentUser = "all_skit_user"
        objGlobals.CurrentSchema = "all_skit."
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        CurrentWeek = Request.QueryString("Week")
        CompID = Request.QueryString("CompID")
        If Not IsPostBack Then
            Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
            current_season = objGlobals.get_current_season()
            Select Case objGlobals.LiveTestFlag()
                Case 3   ' WEBSITE - LIVE db
                    lblLiveTest.Visible = False
                    lblWorkHome.Visible = False
                Case 2   ' WEBSITE - TEST db
                    lblLiveTest.Text = "Database: WWW - TEST"
                Case 1   ' Local LIVE DB
                    lblLiveTest.Text = "Database: Local LIVE db"
                Case 0   ' Locat TEST DB
                    lblLiveTest.Text = "Database: Local TEST db"
            End Select
            If objGlobals.WorkHomeFlag = "W" Then
                lblWorkHome.Text = "Work-Home: WORK"
            Else
                lblWorkHome.Text = "Work-Home: HOME"
            End If

            dt = New DataTable
            dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
            dt.Columns.Add(New DataColumn("Home Night", GetType(System.String)))
            dt.Columns.Add(New DataColumn("Venue", GetType(System.String)))
            Call load_latest_tables("CLUBS")
            Call load_latest_tables("LADIES 1")
            Call load_latest_tables("LADIES 2")
            Call load_latest_tables("LADIES 3")
            Call load_latest_tables("LADIES 4")
            Call load_latest_tables("MENS 1")
            Call load_latest_tables("MENS 2")
            Call load_latest_tables("MENS 3")
            gRow = 0
            gridTables.DataSource = dt
            gridTables.DataBind()

            If CurrentWeek = 0 Then CurrentWeek = objGlobals.GetCurrentWeek
            Call load_weeks()
            CurrentWeek = Val(Mid(ddWeeks.Text, 6, 2))


        End If
    End Sub

    Protected Sub load_weeks()
        Dim WeekText As String
        Dim CommenceDate As DateTime
        Dim EndDate As DateTime
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        ddWeeks.ClearSelection()
        strSQL = "SELECT week_number,week_commences FROM all_skit.vw_weeks ORDER BY week_number"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            CommenceDate = myDataReader.Item("week_commences")
            EndDate = DateAdd(DateInterval.Day, 4, CommenceDate)
            WeekText = "Week " & myDataReader.Item("week_number").ToString & " : "
            WeekText = WeekText & objGlobals.AddSuffix(Left(Format(CommenceDate, "d MMM"), 2)) & Format(CommenceDate, " MMM") & " - "
            WeekText = WeekText & objGlobals.AddSuffix(Left(Format(EndDate, "d MMM"), 2)) & Format(EndDate, " MMM")
            ddWeeks.Items.Add(WeekText)
        End While
        objGlobals.close_connection()
        Call load_week_results(CurrentWeek)
    End Sub

    Function MaxWeek() As Integer
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        strSQL = "SELECT MAX(week_number) FROM all_skit.vw_weeks"
        MaxWeek = -1
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            MaxWeek = myDataReader.Item(0)
        End While
    End Function

    Sub show_current_week_text(ByVal inWeek As Integer)
        If inWeek >= 0 AndAlso inWeek - 1 < ddWeeks.Items.Count Then
            ddWeeks.BorderStyle = BorderStyle.Solid
            ddWeeks.Text = ddWeeks.Items(inWeek - 1).Text 'show the current week
            ddWeeks.BackColor = DarkBlue
        End If

        btn1.BackColor = Black : btn2.BackColor = Black : btn3.BackColor = Black : btn4.BackColor = Black : btn5.BackColor = Black : btn6.BackColor = Black : btn7.BackColor = Black : btn8.BackColor = Black : btn9.BackColor = Black : btn10.BackColor = Black
        btn11.BackColor = Black : btn12.BackColor = Black : btn13.BackColor = Black : btn14.BackColor = Black : btn15.BackColor = Black : btn16.BackColor = Black : btn17.BackColor = Black : btn18.BackColor = Black : btn19.BackColor = Black : btn20.BackColor = Black
        btn21.BackColor = Black : btn22.BackColor = Black : btn23.BackColor = Black : btn24.BackColor = Black : btn25.BackColor = Black : btn26.BackColor = Black : btn27.BackColor = Black : btn28.BackColor = Black : btn29.BackColor = Black : btn30.BackColor = Black
        btn31.BackColor = Black : btn32.BackColor = Black : btn33.BackColor = Black : btn34.BackColor = Black : btn35.BackColor = Black : btn36.BackColor = Black : btn37.BackColor = Black : btn38.BackColor = Black : btn39.BackColor = Black : btn40.BackColor = Black
        btn41.BackColor = Black : btn42.BackColor = Black : btn43.BackColor = Black : btn44.BackColor = Black : btn45.BackColor = Black
        Select Case inWeek
            Case 1 : btn1.BackColor = DarkBlue
            Case 2 : btn2.BackColor = DarkBlue
            Case 3 : btn3.BackColor = DarkBlue
            Case 4 : btn4.BackColor = DarkBlue
            Case 5 : btn5.BackColor = DarkBlue
            Case 6 : btn6.BackColor = DarkBlue
            Case 7 : btn7.BackColor = DarkBlue
            Case 8 : btn8.BackColor = DarkBlue
            Case 9 : btn9.BackColor = DarkBlue
            Case 10 : btn10.BackColor = DarkBlue
            Case 11 : btn11.BackColor = DarkBlue
            Case 12 : btn12.BackColor = DarkBlue
            Case 13 : btn13.BackColor = DarkBlue
            Case 14 : btn14.BackColor = DarkBlue
            Case 15 : btn15.BackColor = DarkBlue
            Case 16 : btn16.BackColor = DarkBlue
            Case 17 : btn17.BackColor = DarkBlue
            Case 18 : btn18.BackColor = DarkBlue
            Case 19 : btn19.BackColor = DarkBlue
            Case 20 : btn20.BackColor = DarkBlue
            Case 21 : btn21.BackColor = DarkBlue
            Case 22 : btn22.BackColor = DarkBlue
            Case 23 : btn23.BackColor = DarkBlue
            Case 24 : btn24.BackColor = DarkBlue
            Case 25 : btn25.BackColor = DarkBlue
            Case 26 : btn26.BackColor = DarkBlue
            Case 27 : btn27.BackColor = DarkBlue
            Case 28 : btn28.BackColor = DarkBlue
            Case 29 : btn29.BackColor = DarkBlue
            Case 30 : btn30.BackColor = DarkBlue
            Case 31 : btn31.BackColor = DarkBlue
            Case 32 : btn32.BackColor = DarkBlue
            Case 33 : btn33.BackColor = DarkBlue
            Case 34 : btn34.BackColor = DarkBlue
            Case 35 : btn35.BackColor = DarkBlue
            Case 36 : btn36.BackColor = DarkBlue
            Case 37 : btn37.BackColor = DarkBlue
            Case 38 : btn38.BackColor = DarkBlue
            Case 39 : btn39.BackColor = DarkBlue
            Case 40 : btn40.BackColor = DarkBlue
            Case 41 : btn42.BackColor = DarkBlue
            Case 42 : btn42.BackColor = DarkBlue
            Case 43 : btn43.BackColor = DarkBlue
            Case 44 : btn44.BackColor = DarkBlue
            Case 45 : btn45.BackColor = DarkBlue
        End Select
    End Sub

    Sub load_latest_tables(inLeague As String)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim iRow As Integer = 0

        strSQL = "SELECT long_name,home_night,venue FROM all_skit.vw_teams WHERE league = '" & inLeague & "' AND long_name <> 'BYE' ORDER BY long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)
        dt.Rows.Add(inLeague, "", "")
        dt.Rows.Add("Team", "Home Night", "Venue")
        gRow = 0
        While myDataReader.Read()
            With gridTables
                iRow = iRow + 1
                dr = dt.NewRow
                dr("Team") = myDataReader.Item("long_name")
                dr("Home Night") = myDataReader.Item("home_night")
                dr("Venue") = myDataReader.Item("venue")
                dt.Rows.Add(dr)
            End With
        End While

        dt.Rows.Add("", "", "")


    End Sub

    Sub load_week_results(ByVal inWeek As Integer)
        Dim strSQL As String
        'Dim LastDate As Date = Nothing
        Dim LastDate As String = Nothing
        Dim myDataReader As oledbdatareader
        Dim FixtureResult(9999) As String

        strSQL = "SELECT league,fixture_date,home_team_name,home_result,away_team_name,fixture_id,venue,status FROM all_skit.vw_fixtures_combined "
        strSQL = strSQL & "WHERE season = '" & objGlobals.get_current_season & "' "
        strSQL = strSQL & " AND week_number = " & inWeek
        'strSQL = strSQL & " AND (home_team_name <> 'BYE' AND away_team_name <> 'BYE')"
        strSQL = strSQL & " ORDER BY fixture_calendar,fixture_date DESC,venue" 'league"
        myDataReader = objGlobals.SQLSelect(strSQL)

        myDataReader = objGlobals.SQLSelect(strSQL)
        If Not myDataReader.HasRows Then
            objGlobals.close_connection()
            gridResults.Visible = False
            Call show_current_week_text(inWeek)
            Exit Sub
        End If
        dt = New DataTable
        dt.Columns.Add(New DataColumn("League", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture Date", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Team Name", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Team Name", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture ID", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Venue", GetType(System.String)))
        gRow = 0
        gridResults.Visible = True
        While myDataReader.Read()
            If LastDate = Nothing Or LastDate <> myDataReader.Item("fixture_date") Then 'myDataReader.Item("fixture_calendar") Then
                LastDate = myDataReader.Item("fixture_date")
                dr = dt.NewRow
                dr("League") = ""
                dr("Fixture Date") = ""
                dr("Home Team Name") = ""
                dr("Home Result") = ""
                dr("Away Team Name") = ""
                dr("Fixture ID") = ""
                dr("Venue") = ""
                gRow = gRow + 1
                dt.Rows.Add(dr)

                dr = dt.NewRow
                dr("League") = myDataReader.Item("fixture_date")
                dr("Fixture Date") = ""
                dr("Home Team Name") = ""
                dr("Home Result") = ""
                dr("Away Team Name") = ""
                dr("Fixture ID") = ""
                dr("Venue") = ""
                gRow = gRow + 1
                dt.Rows.Add(dr)
            End If
            With gridResults
                dr = dt.NewRow
                dr("League") = myDataReader.Item("league")
                dr("Home Team Name") = myDataReader.Item("home_team_name")
                dr("Home Result") = IIf(myDataReader.Item("status") <> -1, myDataReader.Item("home_result"), "postponed")
                dr("Away Team Name") = myDataReader.Item("away_team_name")
                dr("Fixture ID") = myDataReader.Item("fixture_id")
                dr("Venue") = myDataReader.Item("venue")
                gRow = gRow + 1
                dt.Rows.Add(dr)
            End With
        End While
        Dim irow As Integer
        gRow = 0
        gridResults.DataSource = dt
        gridResults.DataBind()
        With gridResults
            For irow = 0 To .Rows.Count - 1
                If Left(.Rows(irow).Cells(0).Text, 5) <> "&nbsp" _
                    And Left(.Rows(irow).Cells(0).Text, 5) <> "CLUBS" _
                    And Left(.Rows(irow).Cells(0).Text, 6) <> "LADIES" _
                    And Left(.Rows(irow).Cells(0).Text, 4) <> "MENS" _
                    And Left(.Rows(irow).Cells(0).Text, 8) <> "DIVISION" _
                    And Left(.Rows(irow).Cells(0).Text, 8) <> "SKITTLES" _
                    And Left(.Rows(irow).Cells(0).Text, 3) <> "ALL" _
                    And Left(.Rows(irow).Cells(0).Text, 8) <> "A ROSSER" _
                    And Left(.Rows(irow).Cells(0).Text, 11) <> "ALAN ROSSER" _
                    And Left(.Rows(irow).Cells(0).Text, 10) <> "G MITCHELL" _
                    And Left(.Rows(irow).Cells(0).Text, 13) <> "GARY MITCHELL" _
                    And Left(.Rows(irow).Cells(0).Text, 5) <> "HOLME" _
                    And Left(.Rows(irow).Cells(0).Text, 10) <> "CUP FINALS" _
                    And Left(.Rows(irow).Cells(0).Text, 8) <> "H TOWERS" _
                    And Left(.Rows(irow).Cells(0).Text, 7) <> "ALLFORM" _
                    And Left(.Rows(irow).Cells(0).Text, 8) <> "CHAMPION" _
                    And Left(.Rows(irow).Cells(0).Text, 11) <> "SECREATRIES" _
                    And Left(.Rows(irow).Cells(0).Text, 7) <> "JUBILEE" _
                    And Left(.Rows(irow).Cells(0).Text, 5) <> "PAIRS" Then
                    'date row
                    With .Rows(irow).Cells(0)
                        .BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                        .ForeColor = System.Drawing.Color.FromArgb(&HE4, &HBB, &H18)
                        .HorizontalAlign = HorizontalAlign.Center
                        .ColumnSpan = 12
                        .Font.Size = 12
                    End With
                Else
                    .Rows(irow).Cells(0).ForeColor = System.Drawing.ColorTranslator.FromHtml("#6699FF")
                    With .Rows(irow).Cells(3)
                        Select Case .Text
                            Case "versus"
                                .ForeColor = LightGreen
                            Case Else
                                .ForeColor = Red
                        End Select
                    End With
                    With .Rows(irow).Cells(2)
                        If InStr(.Text, "OPEN WEEK", CompareMethod.Text) > 0 Then
                            .ColumnSpan = 3
                            .HorizontalAlign = HorizontalAlign.Center
                            gridResults.Rows(irow).Cells(3).Text = ""
                            gridResults.Rows(irow).Cells(4).Text = ""
                            gridResults.Rows(irow).Cells(5).Text = ""
                        End If
                    End With
                End If
                If gridResults.Rows(irow).Cells(5).Text = "0" Then 'blank fixture_id = 0 (open/cup weeks)
                    gridResults.Rows(irow).Cells(5).Text = ""
                End If
            Next irow
        End With
        Call show_current_week_text(inWeek)

    End Sub

 
    Private Sub gridTables_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTables.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If dt.Rows(gRow)(0) <> "" Then
                Select Case Left(dt.Rows(gRow)(0), 4)
                    Case "LADI", "MENS", "CLUB"
                        ThisLeague = dt.Rows(gRow)(0)
                        Dim hLink0 As New HyperLink
                        hLink0.NavigateUrl = "~/All_Skit/League Tables.aspx?League=" & ThisLeague
                        hLink0.Text = dt.Rows(gRow)(0)
                        hLink0.ForeColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)

                        e.Row.Cells(0).BorderColor = gridTables.BackColor
                        e.Row.Cells(0).Controls.Add(hLink0)
                        e.Row.Cells(0).Font.Size = 10
                        e.Row.Cells(0).Font.Bold = True
                        e.Row.Cells(0).BackColor = gridTables.BackColor

                    Case Else   'teams
                        If Left(dt.Rows(gRow)(0), 4) <> "Team" Then
                            Dim hLink1 As New HyperLink
                            hLink1.NavigateUrl = "~/All_Skit/Team Fixtures.aspx?League=" & ThisLeague & "&Team=" & dt.Rows(gRow)(0)
                            hLink1.ForeColor = Cyan
                            hLink1.Text = dt.Rows(gRow)(0)
                            e.Row.Cells(0).Font.Size = 8
                            e.Row.Cells(1).Font.Size = 8
                            e.Row.Cells(0).Controls.Add(hLink1)
                        Else
                            e.Row.Cells(0).ForeColor = Gray
                            e.Row.Cells(1).ForeColor = Gray
                            e.Row.Cells(2).ForeColor = Gray
                            e.Row.Cells(0).Font.Size = 8
                            e.Row.Cells(1).Font.Size = 8
                            e.Row.Cells(2).Font.Size = 8
                        End If
                End Select
            Else
                e.Row.Cells(0).BackColor = gridTables.BackColor
                e.Row.Cells(0).BorderColor = gridTables.BackColor
                Select Case Left(dt.Rows(gRow)(1), 4)
                    Case "Team"
                        e.Row.Cells(1).ForeColor = Tan
                        e.Row.Cells(2).ForeColor = Tan
                    Case ""
                        e.Row.Cells(0).ColumnSpan = 5
                        e.Row.Cells(1).Visible = False
                        e.Row.Cells(2).Visible = False
                End Select
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub btn1_Click(sender As Object, e As System.EventArgs) Handles btn1.Click
        CurrentWeek = Val(btn1.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn2_Click(sender As Object, e As System.EventArgs) Handles btn2.Click
        CurrentWeek = Val(btn2.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn3_Click(sender As Object, e As System.EventArgs) Handles btn3.Click
        CurrentWeek = Val(btn3.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn4_Click(sender As Object, e As System.EventArgs) Handles btn4.Click
        CurrentWeek = Val(btn4.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn5_Click(sender As Object, e As System.EventArgs) Handles btn5.Click
        CurrentWeek = Val(btn5.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn6_Click(sender As Object, e As System.EventArgs) Handles btn6.Click
        CurrentWeek = Val(btn6.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn7_Click(sender As Object, e As System.EventArgs) Handles btn7.Click
        CurrentWeek = Val(btn7.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn8_Click(sender As Object, e As System.EventArgs) Handles btn8.Click
        CurrentWeek = Val(btn8.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn9_Click(sender As Object, e As System.EventArgs) Handles btn9.Click
        CurrentWeek = Val(btn9.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn10_Click(sender As Object, e As System.EventArgs) Handles btn10.Click
        CurrentWeek = Val(btn10.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn11_Click(sender As Object, e As System.EventArgs) Handles btn11.Click
        CurrentWeek = Val(btn11.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn12_Click(sender As Object, e As System.EventArgs) Handles btn12.Click
        CurrentWeek = Val(btn12.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn13_Click(sender As Object, e As System.EventArgs) Handles btn13.Click
        CurrentWeek = Val(btn13.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn14_Click(sender As Object, e As System.EventArgs) Handles btn14.Click
        CurrentWeek = Val(btn14.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn15_Click(sender As Object, e As System.EventArgs) Handles btn15.Click
        CurrentWeek = Val(btn15.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn16_Click(sender As Object, e As System.EventArgs) Handles btn16.Click
        CurrentWeek = Val(btn16.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn17_Click(sender As Object, e As System.EventArgs) Handles btn17.Click
        CurrentWeek = Val(btn17.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn18_Click(sender As Object, e As System.EventArgs) Handles btn18.Click
        CurrentWeek = Val(btn18.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn19_Click(sender As Object, e As System.EventArgs) Handles btn19.Click
        CurrentWeek = Val(btn19.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn20_Click(sender As Object, e As System.EventArgs) Handles btn20.Click
        CurrentWeek = Val(btn20.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn21_Click(sender As Object, e As System.EventArgs) Handles btn21.Click
        CurrentWeek = Val(btn21.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn22_Click(sender As Object, e As System.EventArgs) Handles btn22.Click
        CurrentWeek = Val(btn22.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn23_Click(sender As Object, e As System.EventArgs) Handles btn23.Click
        CurrentWeek = Val(btn23.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn24_Click(sender As Object, e As System.EventArgs) Handles btn24.Click
        CurrentWeek = Val(btn24.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn25_Click(sender As Object, e As System.EventArgs) Handles btn25.Click
        CurrentWeek = Val(btn25.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn26_Click(sender As Object, e As System.EventArgs) Handles btn26.Click
        CurrentWeek = Val(btn26.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn27_Click(sender As Object, e As System.EventArgs) Handles btn27.Click
        CurrentWeek = Val(btn27.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn28_Click(sender As Object, e As System.EventArgs) Handles btn28.Click
        CurrentWeek = Val(btn28.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn29_Click(sender As Object, e As System.EventArgs) Handles btn29.Click
        CurrentWeek = Val(btn29.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn30_Click(sender As Object, e As System.EventArgs) Handles btn30.Click
        CurrentWeek = Val(btn30.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn31_Click(sender As Object, e As System.EventArgs) Handles btn31.Click
        CurrentWeek = Val(btn31.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn32_Click(sender As Object, e As System.EventArgs) Handles btn32.Click
        CurrentWeek = Val(btn32.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn33_Click(sender As Object, e As System.EventArgs) Handles btn33.Click
        CurrentWeek = Val(btn33.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn34_Click(sender As Object, e As System.EventArgs) Handles btn34.Click
        CurrentWeek = Val(btn34.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn35_Click(sender As Object, e As System.EventArgs) Handles btn35.Click
        CurrentWeek = Val(btn35.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn36_Click(sender As Object, e As System.EventArgs) Handles btn36.Click
        CurrentWeek = Val(btn36.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn37_Click(sender As Object, e As System.EventArgs) Handles btn37.Click
        CurrentWeek = Val(btn37.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn38_Click(sender As Object, e As System.EventArgs) Handles btn38.Click
        CurrentWeek = Val(btn38.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn39_Click(sender As Object, e As System.EventArgs) Handles btn39.Click
        CurrentWeek = Val(btn39.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn40_Click(sender As Object, e As System.EventArgs) Handles btn40.Click
        CurrentWeek = Val(btn40.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn41_Click(sender As Object, e As System.EventArgs) Handles btn41.Click
        CurrentWeek = Val(btn41.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn42_Click(sender As Object, e As System.EventArgs) Handles btn42.Click
        CurrentWeek = Val(btn42.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn43_Click(sender As Object, e As System.EventArgs) Handles btn43.Click
        CurrentWeek = Val(btn43.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn44_Click(sender As Object, e As System.EventArgs) Handles btn44.Click
        CurrentWeek = Val(btn44.Text)
        Call load_week_results(CurrentWeek)
    End Sub
    Protected Sub btn45_Click(sender As Object, e As System.EventArgs) Handles btn45.Click
        CurrentWeek = Val(btn45.Text)
        Call load_week_results(CurrentWeek)
    End Sub

    Protected Sub ddWeeks_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddWeeks.SelectedIndexChanged
        CurrentWeek = Val(Mid(ddWeeks.Text, 6, 2))
        Call load_week_results(CurrentWeek)
    End Sub

    Protected Sub ddWeeks_TextChanged(sender As Object, e As System.EventArgs) Handles ddWeeks.TextChanged
        CurrentWeek = Val(Mid(ddWeeks.Text, 6, 2))
        Call load_week_results(CurrentWeek)
    End Sub

    Protected Sub gridResults_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResults.RowDataBound
        Dim RowLeague As String
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Not IsDBNull(dt.Rows(gRow)(3)) Then
                RowLeague = dt.Rows(gRow)(0).ToString
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/All_Skit/Team Fixtures.aspx?League=" & RowLeague & "&Team=" & dt.Rows(gRow)(2).ToString
                hLink1.Text = e.Row.Cells(2).Text
                hLink1.ForeColor = Cyan
                hLink1.Attributes.Add("onmouseover", "this.className='cell'")
                e.Row.Cells(2).Controls.Add(hLink1)
                e.Row.Cells(2).CssClass = "cell"

                Dim hLink2 As New HyperLink
                hLink2.NavigateUrl = "~/All_Skit/Team Fixtures.aspx?League=" & RowLeague & "&Team=" & dt.Rows(gRow)(4).ToString
                hLink2.Text = e.Row.Cells(4).Text
                hLink2.ForeColor = Cyan
                e.Row.Cells(4).Controls.Add(hLink2)

                '23/1/14 - add link to league
                If Left(dt.Rows(gRow)(0).ToString, 4) = "CLUB" Or Left(dt.Rows(gRow)(0).ToString, 4) = "LADI" Or Left(dt.Rows(gRow)(0).ToString, 4) = "MENS" Then
                    Dim hLink4 As New HyperLink
                    hLink4.NavigateUrl = "~/All_Skit/League Tables.aspx?&League=" & dt.Rows(gRow)(0)
                    hLink4.Text = e.Row.Cells(0).Text
                    hLink4.ForeColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)
                    e.Row.Cells(0).Controls.Add(hLink4)
                End If
            End If
            gRow = gRow + 1
        End If
    End Sub

End Class
