Imports System.Data.OleDb
Imports System.Drawing.Color
Imports System.Data
Imports System.Net

'Imports MySql.Data.MySqlClient

Partial Class [Default]
    Inherits System.Web.UI.Page
    Private dt As DataTable
    Private dr As DataRow
    Private gRow As Integer
    Private objGlobals As New Globals
    Private CurrentWeek As Integer
    Private ShowChampions As Boolean
    Private current_season As String
    Private ThisLeague As String = ""
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
    Private FixtureID As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("clubs")
        objGlobals.CurrentUser = "clubs_user"
        objGlobals.CurrentSchema = "clubs."

        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        CurrentWeek = Request.QueryString("Week")
        CompID = Request.QueryString("CompID")
        'remove the code below if the instructions in the label need to be visible
        'lblLibs.Visible = True
        'lblLibs.BackColor = System.Drawing.Color.FromArgb(&H1B, &H1B, &H1B)
        'lblLibs.BorderStyle = BorderStyle.None
        If Not IsPostBack Then
            Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
            Call load_next_meeting()
            Call load_stats()
            current_season = objGlobals.get_current_season()
            lblHonours.Text = Replace(objGlobals.last_season, "_20", "/") & " Honours"
            HyperLinkCD.Text = Replace(current_season, "_20", "/") & " Clubs & Delegates"
            MoreHL.NavigateUrl = "~/Clubs/Honours.aspx"
            HyperLinkCD.NavigateUrl = "~/Clubs/Delegates.aspx"
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
            dt.Columns.Add(New DataColumn("Stats", GetType(System.String)))
            dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
            dt.Columns.Add(New DataColumn("Pld", GetType(System.String)))
            dt.Columns.Add(New DataColumn("Pts", GetType(System.String)))
            'dt.Columns.Add(New DataColumn("Number Nines", GetType(System.String)))
            dt.Columns.Add(New DataColumn("Show Champions", GetType(System.String)))

            Call load_latest_tables("CRIB DIVISION 1")
            Call load_latest_tables("SKITTLES DIVISION 1")
            Call load_latest_tables("SNOOKER DIVISION 1")
            Call load_latest_tables("SNOOKER DIVISION 2")
            gRow = 0
            gridTables.DataSource = dt
            gridTables.DataBind()

            '4.8.14 - Add latest stats to homepage
            Call load_latest_stats("CRIB DIVISION 1")
            Call load_latest_stats("SKITTLES DIVISION 1")
            Call load_latest_stats("SNOOKER DIVISION 1")
            Call load_latest_stats("SNOOKER DIVISION 2")
            '4.8.14 - End

            Call load_current_comp("CRIB", "Team KO", lblCribKO, lblCribKODate, hlCribKO)
            Call load_current_comp("CRIB", "Pairs", lblCribPairs, lblCribPairsDate, hlCribPairs)
            Call load_current_comp("SKITTLES", "12-a-Side Team KO", lblSkittles12aSide, lblSkittles12aSideDate, hlSkittles12aSide)
            Call load_current_comp("SKITTLES", "6-a-Side Team KO", lblSkittles6aSide, lblSkittles6aSideDate, hlSkittles6aSide)
            Call load_current_comp("SNOOKER - DIVISION 1", "Team KO", lblSnooker1KO, lblSnooker1KODate, hlSnooker1KO)
            Call load_current_comp("SNOOKER - DIVISION 1", "Singles", lblSnooker1Singles, lblSnooker1SinglesDate, hlSnooker1Singles)
            Call load_current_comp("SNOOKER - DIVISION 1", "Doubles", lblSnooker1Doubles, lblSnooker1DoublesDate, hlSnooker1Doubles)
            Call load_current_comp("SNOOKER - DIVISION 1", "3-a-Side", lblSnooker13aSide, lblSnooker13aSideDate, hlSnooker13aSide)
            Call load_current_comp("SNOOKER - DIVISION 2", "Team KO", lblSnooker2KO, lblSnooker2KODate, hlSnooker2KO)
            Call load_current_comp("SNOOKER - DIVISION 2", "Singles", lblSnooker2Singles, lblSnooker2SinglesDate, hlSnooker2Singles)
            Call load_current_comp("SNOOKER - DIVISION 2", "Doubles", lblSnooker2Doubles, lblSnooker2DoublesDate, hlSnooker2Doubles)
            Call load_current_comp("SNOOKER - DIVISION 2", "3-a-Side", lblSnooker23aSide, lblSnooker23aSideDate, hlSnooker23aSide)

            'If CurrentWeek = 0 Then CurrentWeek = objGlobals.GetCurrentWeek
            Call load_weeks()
            'Call show_current_week_text(CurrentWeek)

            Call load_honours()
            Call load_venues()
            ' Else
            'CurrentWeek = Val(Mid(btnWeek.Text, 6, 2))
            'CurrentWeek = Val(Mid(ddWeeks.Text, 6, 2))

            Call load_latest_card()

            Dim strIPAddress As String = GetIPAddress()
            Dim strSQL As String
            Dim myDataReader As OleDbDataReader
            Dim myDataReader1 As OleDbDataReader

            strSQL = "EXEC clubs.sp_get_result_refresh_flag '" & strIPAddress & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            While myDataReader.Read()
                chkRefresh.Checked = myDataReader.Item("refresh")
                If chkRefresh.Checked Then
                    strSQL = "EXEC clubs.sp_update_result_refresh_flag '" & strIPAddress & "',1"
                    myDataReader1 = objGlobals.SQLSelect(strSQL)
                    TimerRefresh.Enabled = True
                    TimerRefresh_Tick(Me, e)
                Else
                    strSQL = "EXEC clubs.sp_update_result_refresh_flag '" & strIPAddress & "',0"
                    myDataReader1 = objGlobals.SQLSelect(strSQL)
                    TimerRefresh.Enabled = False
                End If

            End While
            objGlobals.close_connection()

        End If
    End Sub

    Sub load_next_meeting()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim UKDateTime As Date = objGlobals.LondonDate(DateTime.UtcNow)
        Dim UKDate As String = UKDateTime.ToShortDateString
        strSQL = "SELECT TOP 1 meeting_venue,meeting_date FROM clubs.league_meetings WHERE CONVERT(VARCHAR(8),GETDATE(),112) <= date8 ORDER BY date8"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            lblNextMeeting.Text = myDataReader.Item("meeting_date") & " @ " & myDataReader.Item("meeting_venue")
        End While
        objGlobals.close_connection()
    End Sub

    Sub load_stats()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim UKDateTime As Date = objGlobals.LondonDate(DateTime.UtcNow)
        Dim UKDate As String = UKDateTime.ToShortDateString
        strSQL = "EXEC clubs.sp_get_stats '" + UKDate + "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            txtStatsToday.Text = myDataReader.Item("today").ToString
            txtStatsWeek.Text = myDataReader.Item("week").ToString
            txtStatsSeason.Text = myDataReader.Item("season").ToString
        End While
        objGlobals.close_connection()
    End Sub


    Sub load_latest_stats(inLeague As String)
        Dim Top10Player(99) As String
        Dim Top10Stat(99) As Double
        Dim strSQL As String = ""
        Dim myDataReader As OleDbDataReader
        Dim PlayerCount As Integer

        Select Case inLeague
            Case "CRIB DIVISION 1"
                Dim LeagueSeries = chtLeague_Crib.Series("League")
                LeagueSeries.IsValueShownAsLabel = True
                LeagueSeries.LabelForeColor = White
                LeagueSeries.CustomProperties = "BarLabelStyle=Right"
                LeagueSeries.Url = "~/Clubs/Stats.aspx?League=CRIB DIVISION 1"
                With chtLeague_Crib
                    .Visible = True
                    .ChartAreas(0).AxisX.LabelStyle.ForeColor = White
                    .ChartAreas(0).AxisY.LabelStyle.ForeColor = White
                    .ChartAreas(0).AxisX.TitleForeColor = LightGreen
                    .ChartAreas(0).AxisY.TitleForeColor = LightGreen
                    .ChartAreas(0).AxisX.Interval = 1
                    .ChartAreas(0).AxisY.Maximum = 5
                    .ChartAreas(0).AxisX.MajorGrid.Enabled = False
                    .ChartAreas(0).AxisY.MajorGrid.Enabled = False
                    .ChartAreas(0).AxisX.LabelStyle.Font = New System.Drawing.Font("Arial", 8)
                    .ChartAreas(0).Area3DStyle.Inclination = 1
                    .ChartAreas(0).Area3DStyle.Perspective = 0
                    .ChartAreas(0).BackColor = DarkGray
                    .Titles.Add("Top 10 - Average Pts Per Game")
                    .Titles.Item(0).ForeColor = LightGreen
                    .Titles.Item(0).Font = New System.Drawing.Font("Arial", 10)
                    .Titles.Add("Click on Green Bars for More Info")
                    .Titles.Item(1).ForeColor = LightGreen
                    .Titles.Item(1).Font = New System.Drawing.Font("Arial", 9)
                    .ChartAreas(0).AxisY.Title = "Average Points per Game"
                    strSQL = "SELECT player,points_per_game FROM clubs.vw_player_stats_crib WHERE league_pos > 0 AND league_pos <= 10 ORDER BY league_pos,total_played DESC"
                    myDataReader = objGlobals.SQLSelect(strSQL)
                    Dim i As Integer = 0
                    While myDataReader.Read
                        i = i + 1
                        Top10Player(i) = myDataReader.Item("player")
                        Top10Stat(i) = myDataReader.Item("points_per_game")
                    End While
                    objGlobals.close_connection()

                    PlayerCount = i
                    For i = PlayerCount To 1 Step -1
                        LeagueSeries.Points.AddXY(Top10Player(i), Top10Stat(i))
                        If i = PlayerCount Then .ChartAreas(0).AxisY.Minimum = Top10Stat(i) - 0.2
                        If i = 1 Then .ChartAreas(0).AxisY.Maximum = Top10Stat(i)
                    Next
                    .Height = 24 * PlayerCount
                    Dim myFont As New System.Drawing.Font("Arial", 7)
                    For Each pt As DataVisualization.Charting.DataPoint In LeagueSeries.Points
                        pt.Font = myFont
                    Next
                End With

            Case "SKITTLES DIVISION 1"
                Dim LeagueSeries = chtLeague_Skittles.Series("League")
                LeagueSeries.IsValueShownAsLabel = True
                LeagueSeries.LabelForeColor = White
                LeagueSeries.CustomProperties = "BarLabelStyle=Right"
                LeagueSeries.Url = "~/Clubs/Stats.aspx?League=SKITTLES DIVISION 1"
                With chtLeague_Skittles
                    .Visible = True
                    .ChartAreas(0).AxisX.LabelStyle.ForeColor = White
                    .ChartAreas(0).AxisY.LabelStyle.ForeColor = White
                    .ChartAreas(0).AxisX.TitleForeColor = LightGreen
                    .ChartAreas(0).AxisY.TitleForeColor = LightGreen
                    .ChartAreas(0).AxisX.Interval = 1
                    .ChartAreas(0).AxisX.MajorGrid.Enabled = False
                    .ChartAreas(0).AxisY.MajorGrid.Enabled = False
                    .ChartAreas(0).AxisX.LabelStyle.Font = New System.Drawing.Font("Arial", 8)
                    .ChartAreas(0).Area3DStyle.Inclination = 1
                    .ChartAreas(0).Area3DStyle.Perspective = 0
                    .ChartAreas(0).BackColor = DarkGray
                    .Titles.Add("Best Team Average Score")
                    .Titles.Item(0).ForeColor = LightGreen
                    .Titles.Item(0).Font = New System.Drawing.Font("Arial", 10)
                    .Titles.Add("Click on Green Bars for More Info")
                    .Titles.Item(1).ForeColor = LightGreen
                    .Titles.Item(1).Font = New System.Drawing.Font("Arial", 9)
                    .ChartAreas(0).AxisY.Title = "Average Pins per Game"
                    strSQL = "SELECT player,average FROM clubs.vw_player_stats_skittles WHERE team_pos = 1 ORDER BY team"
                    myDataReader = objGlobals.SQLSelect(strSQL)
                    Dim i As Integer = 0
                    Dim BestStat As Double = 0
                    Dim WorstStat As Double = 99
                    While myDataReader.Read
                        i = i + 1
                        Top10Player(i) = myDataReader.Item("player")
                        Top10Stat(i) = myDataReader.Item("average")
                        If Top10Stat(i) > BestStat Then BestStat = Top10Stat(i)
                        If Top10Stat(i) < WorstStat Then WorstStat = Top10Stat(i)
                    End While
                    PlayerCount = i
                    .ChartAreas(0).AxisY.Minimum = WorstStat
                    .ChartAreas(0).AxisY.Maximum = BestStat
                    For i = PlayerCount To 1 Step -1
                        LeagueSeries.Points.AddXY(Top10Player(i), Top10Stat(i))
                    Next
                    .Height = 25 * PlayerCount
                    Dim myFont As New System.Drawing.Font("Arial", 7)
                    For Each pt As DataVisualization.Charting.DataPoint In LeagueSeries.Points
                        pt.Font = myFont
                    Next
                    .ChartAreas(0).AxisY.Minimum = WorstStat - 0.4
                End With
            Case "SNOOKER DIVISION 1"
                Dim LeagueSeries = chtLeague_Snooker1.Series("League")
                LeagueSeries.IsValueShownAsLabel = True
                LeagueSeries.LabelForeColor = White
                LeagueSeries.CustomProperties = "BarLabelStyle=Right"
                LeagueSeries.Url = "~/Clubs/Stats.aspx?League=SNOOKER DIVISION 1"
                With chtLeague_Snooker1
                    .Visible = True
                    .ChartAreas(0).AxisX.LabelStyle.ForeColor = White
                    .ChartAreas(0).AxisY.LabelStyle.ForeColor = White
                    .ChartAreas(0).AxisX.TitleForeColor = LightGreen
                    .ChartAreas(0).AxisY.TitleForeColor = LightGreen
                    .ChartAreas(0).AxisX.Interval = 1
                    '.ChartAreas(0).AxisY.Maximum = 100
                    .ChartAreas(0).AxisX.MajorGrid.Enabled = False
                    .ChartAreas(0).AxisY.MajorGrid.Enabled = False
                    .ChartAreas(0).AxisX.LabelStyle.Font = New System.Drawing.Font("Arial", 8)
                    .ChartAreas(0).Area3DStyle.Inclination = 1
                    .ChartAreas(0).Area3DStyle.Perspective = 0
                    .ChartAreas(0).BackColor = DarkGray
                    .Titles.Add("Top 10 - Win %")
                    .Titles.Item(0).ForeColor = LightGreen
                    .Titles.Item(0).Font = New System.Drawing.Font("Arial", 10)
                    .Titles.Add("Click on Green Bars for More Info")
                    .Titles.Item(1).ForeColor = LightGreen
                    .Titles.Item(1).Font = New System.Drawing.Font("Arial", 9)
                    .ChartAreas(0).AxisY.Title = "Win %"
                    strSQL = "SELECT player,total_won_percent FROM clubs.vw_player_stats_snooker WHERE league='SNOOKER DIVISION 1' AND league_pos > 0 AND league_pos <= 10 ORDER BY league_pos,total_played DESC"
                    myDataReader = objGlobals.SQLSelect(strSQL)
                    Dim i As Integer = 0
                    While myDataReader.Read
                        i = i + 1
                        Top10Player(i) = myDataReader.Item("player")
                        Top10Stat(i) = myDataReader.Item("total_won_percent")
                    End While
                    objGlobals.close_connection()

                    PlayerCount = i
                    For i = PlayerCount To 1 Step -1
                        If i = PlayerCount Then .ChartAreas(0).AxisY.Minimum = Top10Stat(i) - 5
                        If i = 1 Then .ChartAreas(0).AxisY.Maximum = Top10Stat(i)
                        LeagueSeries.Points.AddXY(Top10Player(i), Top10Stat(i))
                    Next
                    Dim myFont As New System.Drawing.Font("Arial", 7)
                    For Each pt As DataVisualization.Charting.DataPoint In LeagueSeries.Points
                        pt.Font = myFont
                    Next
                    .Height = 24 * PlayerCount
                End With
            Case "SNOOKER DIVISION 2"
                Dim LeagueSeries = chtLeague_Snooker2.Series("League")
                LeagueSeries.IsValueShownAsLabel = True
                LeagueSeries.LabelForeColor = White
                LeagueSeries.CustomProperties = "BarLabelStyle=Right"
                LeagueSeries.Url = "~/Clubs/Stats.aspx?League=SNOOKER DIVISION 2"
                With chtLeague_Snooker2
                    .Visible = True
                    .ChartAreas(0).AxisX.LabelStyle.ForeColor = White
                    .ChartAreas(0).AxisY.LabelStyle.ForeColor = White
                    .ChartAreas(0).AxisX.TitleForeColor = LightGreen
                    .ChartAreas(0).AxisY.TitleForeColor = LightGreen
                    .ChartAreas(0).AxisX.Interval = 1
                    '.ChartAreas(0).AxisY.Maximum = 100
                    .ChartAreas(0).AxisX.MajorGrid.Enabled = False
                    .ChartAreas(0).AxisY.MajorGrid.Enabled = False
                    .ChartAreas(0).AxisX.LabelStyle.Font = New System.Drawing.Font("Arial", 8)
                    .ChartAreas(0).Area3DStyle.Inclination = 1
                    .ChartAreas(0).Area3DStyle.Perspective = 0
                    .ChartAreas(0).BackColor = DarkGray
                    .Titles.Add("Top 10 - Win %")
                    .Titles.Item(0).ForeColor = LightGreen
                    .Titles.Item(0).Font = New System.Drawing.Font("Arial", 10)
                    .Titles.Add("Click on Green Bars for More Info")
                    .Titles.Item(1).ForeColor = LightGreen
                    .Titles.Item(1).Font = New System.Drawing.Font("Arial", 9)
                    .ChartAreas(0).AxisY.Title = "Win %"
                    strSQL = "SELECT player,total_won_percent FROM clubs.vw_player_stats_snooker WHERE league='SNOOKER DIVISION 2' AND league_pos > 0 AND league_pos <= 10 ORDER BY league_pos,total_played DESC"
                    myDataReader = objGlobals.SQLSelect(strSQL)
                    Dim i As Integer = 0
                    While myDataReader.Read
                        i = i + 1
                        Top10Player(i) = myDataReader.Item("player")
                        Top10Stat(i) = myDataReader.Item("total_won_percent")
                    End While
                    objGlobals.close_connection()

                    PlayerCount = i
                    For i = PlayerCount To 1 Step -1
                        If i = PlayerCount Then .ChartAreas(0).AxisY.Minimum = Top10Stat(i) - 5
                        If i = 1 Then .ChartAreas(0).AxisY.Maximum = Top10Stat(i)
                        LeagueSeries.Points.AddXY(Top10Player(i), Top10Stat(i))
                    Next
                    Dim myFont As New System.Drawing.Font("Arial", 7)
                    For Each pt As DataVisualization.Charting.DataPoint In LeagueSeries.Points
                        pt.Font = myFont
                    Next
                    .Height = 24 * PlayerCount
                End With
        End Select

    End Sub

    Sub load_venues()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        ddlVenues.ClearSelection()

        strSQL = "SELECT DISTINCT(venue) FROM clubs.vw_teams WHERE long_name <> 'BYE' ORDER BY venue"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddlVenues.Items.Add(myDataReader.Item("venue"))
        End While
        objGlobals.close_connection()

    End Sub

    Sub load_current_comp(ByVal inLeague As String, ByVal inComp As String, ByVal inCompName As Label, ByVal inDate As Label, ByVal inHL As HyperLink)

        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

        strSQL = "SELECT league,played_by,text,url,comp FROM clubs.current_comps WHERE league = '" & inLeague & "' AND comp = '" & inComp & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            inDate.Visible = True
            inCompName.Visible = True
            inCompName.Text = inComp
            If InStr(UCase(myDataReader.Item("played_by")), "NOT TO BE PLAYED") = 0 Then
                inDate.Text = myDataReader.Item("played_by")
            End If
            If Not IsDBNull(myDataReader.Item("played_by")) Then
                inHL.Visible = True
                inHL.NavigateUrl = myDataReader.Item("url")
                inHL.Text = myDataReader.Item("text")
            End If
        End While
        objGlobals.close_connection()


    End Sub

    Function MaxWeek() As Integer
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        strSQL = "SELECT MAX(week_number) FROM clubs.vw_weeks"
        MaxWeek = -1
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            MaxWeek = myDataReader.Item(0)
        End While
        objGlobals.close_connection()

    End Function

    Protected Sub load_weeks()
        Dim WeekText As String
        Dim CommenceDate As DateTime
        Dim EndDate As DateTime
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        ddWeeks.ClearSelection()
        strSQL = "SELECT week_number,week_commences FROM clubs.vw_weeks ORDER BY week_number"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            CommenceDate = myDataReader.Item("week_commences")
            EndDate = DateAdd(DateInterval.Day, 4, CommenceDate)
            WeekText = "Week " & myDataReader.Item("week_number").ToString & " : "
            WeekText = WeekText & objGlobals.AddSuffix(Left(Format(CommenceDate, "d MMM"), 2)) & Format(CommenceDate, " MMM") & " - "
            WeekText = WeekText & objGlobals.AddSuffix(Left(Format(EndDate, "d MMM"), 2)) & Format(EndDate, " MMM")
            ddWeeks.Items.Add(WeekText)
            If myDataReader.Item("week_number") = CurrentWeek Then ddWeeks.Text = WeekText
        End While
        objGlobals.close_connection()
        Call load_week_results(CurrentWeek)
    End Sub

    Sub show_current_week_text(ByVal inWeek As Integer)
        If inWeek <> -1 AndAlso inWeek - 1 < ddWeeks.Items.Count Then
            ddWeeks.BorderStyle = BorderStyle.Solid
            btnOutstanding.BorderStyle = BorderStyle.None
            ddWeeks.Text = ddWeeks.Items(inWeek - 1).Text 'show the current week
            ddWeeks.BackColor = DarkBlue
        End If
        btnOutstanding.BackColor = Black

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
            Case 41 : btn41.BackColor = DarkBlue
            Case 42 : btn42.BackColor = DarkBlue
            Case 43 : btn43.BackColor = DarkBlue
            Case 44 : btn44.BackColor = DarkBlue
            Case 45 : btn45.BackColor = DarkBlue
        End Select
    End Sub

    Sub load_latest_tables(inLeague As String)
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim iRow As Integer = 0

        strSQL = "SELECT a.pos as Pos,a.team as Team ,a.pld as Pld ,a.pts as Pts,a.deducted as Deducted,b.show_champions as ShowChampions,''" 'c.number_nines as Number_Nines "
        strSQL = strSQL & "FROM clubs.vw_tables a "
        strSQL = strSQL & "INNER JOIN clubs.vw_leagues b ON b.league = '" & inLeague & "' "
        'strSQL = strSQL & "LEFT OUTER JOIN clubs.vw_skittles_number_nines c ON c.team = a.team" & " "
        strSQL = strSQL & "WHERE a.league = '" & inLeague & "' ORDER BY a.pos"
        myDataReader = objGlobals.SQLSelect(strSQL)

        Select Case inLeague
            Case "CRIB DIVISION 1"
                dt.Rows.Add("CRIB DIVISION 1", "FIXTURES", "", "", "")
                dt.Rows.Add("", "Team", "Pld", "Pts", "")
            Case "SKITTLES DIVISION 1"
                dt.Rows.Add("", "", "", "", "")
                dt.Rows.Add("SKITTLES DIVISION 1", "FIXTURES", "", "", "")
                'dt.Rows.Add("", "Team", "Pld", "Pts", "9+")
                dt.Rows.Add("", "Team", "Pld", "Pts", "")
            Case "SNOOKER DIVISION 1"
                dt.Rows.Add("", "", "", "", "")
                dt.Rows.Add("SNOOKER DIVISION 1", "FIXTURES", "", "", "")
                dt.Rows.Add("", "Team", "Pld", "Pts", "")
            Case "SNOOKER DIVISION 2"
                dt.Rows.Add("", "", "", "", "")
                dt.Rows.Add("SNOOKER DIVISION 2", "FIXTURES", "", "", "")
                dt.Rows.Add("", "Team", "Pld", "Pts", "")
        End Select

        While myDataReader.Read()
            With gridTables
                dr = dt.NewRow
                dr("Stats") = "Stats"
                dr("Team") = myDataReader.Item("team")
                dr("Pld") = myDataReader.Item("pld")
                dr("Pts") = myDataReader.Item("pts") & " "
                If Left(inLeague, 4) = "SKIT" Then
                    dr("Pts") = Format(myDataReader.Item("pts"), "##0.0")
                End If
                If myDataReader.Item("deducted") > 0 Then
                    dr("Pts") = "*" & dr("Pts")
                End If
                If myDataReader.Item("pos") = 1 And myDataReader.Item("ShowChampions") = dr("Team") Then
                    dr("Show Champions") = "Y"
                Else
                    dr("Show Champions") = "N"
                End If
                'If Left(inLeague, 4) = "SKIT" Then
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

    End Sub

    Protected Sub btnOutstanding_Click(sender As Object, e As System.EventArgs) Handles btnOutstanding.Click
        btn1.BackColor = Black : btn2.BackColor = Black : btn3.BackColor = Black : btn4.BackColor = Black : btn5.BackColor = Black : btn6.BackColor = Black : btn7.BackColor = Black : btn8.BackColor = Black : btn9.BackColor = Black : btn10.BackColor = Black
        btn11.BackColor = Black : btn12.BackColor = Black : btn13.BackColor = Black : btn14.BackColor = Black : btn15.BackColor = Black : btn16.BackColor = Black : btn17.BackColor = Black : btn18.BackColor = Black : btn19.BackColor = Black : btn20.BackColor = Black
        btn21.BackColor = Black : btn22.BackColor = Black : btn23.BackColor = Black : btn24.BackColor = Black : btn25.BackColor = Black : btn26.BackColor = Black : btn27.BackColor = Black : btn28.BackColor = Black : btn29.BackColor = Black : btn30.BackColor = Black
        btn31.BackColor = Black : btn32.BackColor = Black : btn33.BackColor = Black : btn34.BackColor = Black : btn35.BackColor = Black : btn36.BackColor = Black : btn37.BackColor = Black : btn38.BackColor = Black : btn39.BackColor = Black : btn40.BackColor = Black
        btn41.BackColor = Black : btn42.BackColor = Black : btn43.BackColor = Black : btn44.BackColor = Black : btn45.BackColor = Black
        btnOutstanding.BorderStyle = BorderStyle.Solid
        btnOutstanding.BackColor = DarkBlue
        ddWeeks.BorderStyle = BorderStyle.None
        ddWeeks.BackColor = Black
        Call load_week_results(-1)
    End Sub

    Sub load_week_results(ByVal inWeek As Integer)
        Dim strSQL As String
        'Dim LastDate As Date = Nothing
        Dim LastDate As String = Nothing
        Dim FixtureType As String = Nothing
        Dim myDataReader As OleDbDataReader
        Dim FixtureResult(9999) As String
        'strSQL = "SELECT  league,REPLACE(fixture_date,'W/C','Monday') AS fixture_date,fixture_calendar,home_team_name,home_result, away_team_name,home_points_deducted,away_points_deducted,fixture_id,status,week_number "
        'strSQL = strSQL & "FROM clubs.vw_fixtures "
        'If inWeek <> -1 Then
        '    strSQL = strSQL & "WHERE week_number = " & inWeek
        'Else
        '    strSQL = strSQL & "WHERE (status = -1 OR status = 1)"
        '    strSQL = strSQL & " AND ISNUMERIC(home_team) = 1"
        'End If
        'strSQL = strSQL & " AND home_team_name <> 'BYE'"
        'strSQL = strSQL & " AND away_team_name <> 'BYE'"
        'strSQL = strSQL & " ORDER BY fixture_calendar,league,home_team"
        'myDataReader = objGlobals.SQLSelect(strSQL)

        strSQL = "SELECT league,fixture_date,fixture_calendar,home_team_name,home_result,away_team_name,home_points_deducted,away_points_deducted,fixture_id,status,week_number,fixture_type,venue,fixture_neutral "
        strSQL = strSQL & "FROM clubs.fixtures_combined "
        strSQL = strSQL & "WHERE season = '" & objGlobals.get_current_season & "' "
        If inWeek <> -1 Then
            strSQL = strSQL & "AND week_number = " & inWeek
        Else
            strSQL = strSQL & "AND (status = -1 OR status = 1)"
            strSQL = strSQL & " AND fixture_type = 'L'"
        End If
        strSQL = strSQL & " ORDER BY fixture_calendar,fixture_date DESC,league"
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
        dt.Columns.Add(New DataColumn("Fixture ID2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Status", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Week Number", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture Type", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Venue", GetType(System.String)))

        gRow = 0
        gridResults.Visible = True
        While myDataReader.Read()
            If LastDate = Nothing Or LastDate <> myDataReader.Item("fixture_date") Then 'myDataReader.Item("fixture_calendar") Then
                'LastDate = myDataReader.Item("fixture_calendar")
                LastDate = myDataReader.Item("fixture_date")
                dr = dt.NewRow
                dr("League") = ""
                dr("Fixture Date") = ""
                dr("Home Team Name") = ""
                dr("Home Result") = ""
                dr("Away Team Name") = ""
                dr("Fixture ID") = ""
                dr("Fixture ID2") = ""
                dr("Status") = ""
                dr("Week Number") = ""
                dr("Fixture Type") = ""
                gRow = gRow + 1
                dt.Rows.Add(dr)

                dr = dt.NewRow
                dr("League") = myDataReader.Item("fixture_date")
                dr("Fixture Date") = ""
                dr("Home Team Name") = ""
                dr("Home Result") = ""
                dr("Away Team Name") = ""
                dr("Fixture ID") = ""
                dr("Fixture ID2") = ""
                dr("Status") = ""
                dr("Week Number") = ""
                dr("Fixture Type") = ""
                gRow = gRow + 1
                dt.Rows.Add(dr)
            End If
            With gridResults
                dr = dt.NewRow
                dr("League") = myDataReader.Item("league")
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
                FixtureResult(myDataReader.Item("fixture_id")) = myDataReader.Item("home_result")
                If myDataReader.Item("status") = -1 Then
                    FixtureResult(myDataReader.Item("fixture_id")) = "Postponed"
                Else
                    FixtureResult(myDataReader.Item("fixture_id")) = myDataReader("home_result")
                End If
                If objGlobals.AdminLogin Then
                    dr("Fixture ID") = myDataReader.Item("fixture_id")
                Else
                    'If (myDataReader.Item("home_result") = "0 - 0" Or myDataReader.Item("home_result") = "< winner" Or myDataReader.Item("home_result") = "winner >" Or myDataReader.Item("home_result") = "not played") _
                    '    And myDataReader.Item("home_points_deducted") + myDataReader.Item("away_points_deducted") = 0 Then
                    '    dr("Fixture ID") = ""
                    'Else
                    '    dr("Fixture ID") = "View Card"
                    'End If
                    If InStr(myDataReader.Item("league"), "CUP") = 0 And myDataReader.Item("status") >= 1 And Not (myDataReader.Item("home_result") = "< winner" Or myDataReader.Item("home_result") = "winner >" Or myDataReader.Item("home_result") = "not played") Then
                        dr("Fixture ID") = "View Card"
                    Else
                        dr("Fixture ID") = ""
                    End If
                End If
                dr("Fixture ID2") = myDataReader.Item("fixture_id")
                dr("Status") = myDataReader.Item("status")
                dr("Week Number") = myDataReader.Item("week_number")
                dr("Fixture Type") = myDataReader.Item("fixture_type")
                dr("Venue") = IIf(myDataReader.Item("fixture_neutral") = 0, myDataReader.Item("venue"), myDataReader.Item("venue") + " (N)")
                gRow = gRow + 1
                dt.Rows.Add(dr)
            End With
        End While
        objGlobals.close_connection()

        Dim irow As Integer
        Dim ThisResult As String
        gRow = 0
        gridResults.DataSource = dt
        gridResults.DataBind()


        With gridResults
            For irow = 0 To .Rows.Count - 1
                If Left(.Rows(irow).Cells(0).Text, 5) <> "&nbsp" And Left(.Rows(irow).Cells(0).Text, 4) <> "SKIT" And Left(.Rows(irow).Cells(0).Text, 4) <> "SNOO" And Left(.Rows(irow).Cells(0).Text, 4) <> "CRIB" Then
                    'date row
                    With .Rows(irow).Cells(0)
                        .BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                        .ForeColor = System.Drawing.Color.FromArgb(&HE4, &HBB, &H18)
                        .HorizontalAlign = HorizontalAlign.Center
                        .ColumnSpan = 6
                        .Font.Size = 12
                    End With
                Else
                    With .Rows(irow).Cells(0)
                        .ColumnSpan = 1
                        Select Case Left(.Text, 4)
                            Case "SNOO"
                                .ForeColor = LightGreen
                            Case "SKIT"
                                .ForeColor = System.Drawing.ColorTranslator.FromHtml("#6699FF")
                            Case "CRIB"
                                .ForeColor = Yellow
                        End Select
                    End With
                    If .Rows(irow).Cells(9).Text = "O" Then  'open week ?
                        .Rows(irow).Cells(2).ColumnSpan = 4
                        .Rows(irow).Cells(2).HorizontalAlign = HorizontalAlign.Center
                    End If
                    With .Rows(irow).Cells(3)
                        ThisResult = FixtureResult(Val(gridResults.Rows(irow).Cells(6).Text))
                        Select Case .Text
                            Case "0 - 0", "versus"
                                .BackColor = gridResults.BackColor
                                If Left(gridResults.Rows(irow).Cells(2).Text, 4) <> "OPEN" And gridResults.Rows(irow).Cells(4).Text <> "&nbsp;" Then
                                    If ThisResult <> "Postponed" Then
                                        .ForeColor = LightGreen
                                        .Text = "versus"
                                    Else
                                        .ForeColor = Red
                                        .Text = "Postponed"
                                    End If
                                Else
                                    .Text = ""
                                End If
                            Case "*0 - 0*"
                                If ThisResult <> "Postponed" Then
                                    .ForeColor = LightGreen
                                    .Text = "*versus*"
                                Else
                                    .ForeColor = Red
                                    .Text = "Postponed"
                                End If
                            Case "*0 - 0"
                                If ThisResult <> "Postponed" Then
                                    .ForeColor = LightGreen
                                    .Text = "*versus"
                                Else
                                    .ForeColor = Red
                                    .Text = "Postponed"
                                End If
                            Case "0 - 0*"
                                If ThisResult <> "Postponed" Then
                                    .ForeColor = LightGreen
                                    .Text = "versus*"
                                Else
                                    .ForeColor = Red
                                    .Text = "Postponed"
                                End If
                            Case Else
                                If .Text <> "&nbsp;" And .Text <> "" Then
                                    If Val(Left(ThisResult, 2)) > Val(Right(ThisResult, 2)) Or gridResults.Rows(irow).Cells(3).Text = "&lt; winner" Then
                                        gridResults.Rows(irow).Cells(2).BorderColor = Green
                                    End If
                                    If Val(Left(ThisResult, 2)) < Val(Right(ThisResult, 2)) Or gridResults.Rows(irow).Cells(3).Text = "winner &gt;" Then
                                        gridResults.Rows(irow).Cells(4).BorderColor = Green
                                    End If
                                    gridResults.Rows(irow).Cells(3).BackColor = DarkGreen
                                    gridResults.Rows(irow).Cells(3).ForeColor = White
                                End If
                        End Select
                    End With
                End If
            Next irow
            '.Columns(5).Visible = False
            'Columns(6).Visible = False
            'Columns(7).Visible = False
            '.Columns(8).Visible = False
            '.Columns(9).Visible = False
        End With

        Call show_current_week_text(inWeek)
    End Sub

    Sub load_honours()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

        strSQL = "SELECT league,team FROM clubs.tables WHERE pos = 1 AND season = '" & objGlobals.get_last_season & "' ORDER BY league"
        myDataReader = objGlobals.SQLSelect(strSQL)
        dt = New DataTable

        dt.Columns.Add(New DataColumn("League", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
        While myDataReader.Read
            With gridHonours
                dr = dt.NewRow
                dr("League") = myDataReader.Item(0)
                dr("Team") = myDataReader.Item(1)
                dt.Rows.Add(dr)
            End With
        End While
        objGlobals.close_connection()

        gridHonours.DataSource = dt
        gridHonours.DataBind()
        Dim irow As Integer
        With gridHonours
            .HeaderRow.ForeColor = System.Drawing.Color.Tan
            .HeaderRow.Cells(0).HorizontalAlign = HorizontalAlign.Left
            .HeaderRow.Cells(1).HorizontalAlign = HorizontalAlign.Left
            .Font.Name = "Arial"
            For irow = 0 To .Rows.Count - 1
                With .Rows(irow).Cells(0)
                    .HorizontalAlign = HorizontalAlign.Left
                    .Wrap = False
                    Select Case Left(.Text, 4)
                        Case "SNOO"
                            .ForeColor = LightGreen
                        Case "SKIT"
                            .ForeColor = System.Drawing.ColorTranslator.FromHtml("#6699FF")
                        Case "CRIB"
                            .ForeColor = Yellow
                    End Select
                End With
                With .Rows(irow).Cells(1)
                    .Wrap = False
                    .ForeColor = Cyan
                    .HorizontalAlign = HorizontalAlign.Left
                End With
            Next irow
        End With

    End Sub

    Private Sub gridTables_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTables.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If dt.Rows(gRow)(0) <> "" Then
                Select Case Left(dt.Rows(gRow)(0), 4)
                    Case "CRIB"
                        ThisLeague = dt.Rows(gRow)(0)
                        Dim hLink0 As New HyperLink
                        hLink0.NavigateUrl = "~/Clubs/League Tables.aspx?League=" & ThisLeague
                        hLink0.Text = dt.Rows(gRow)(0)
                        hLink0.ForeColor = Yellow

                        e.Row.Cells(0).BorderColor = gridTables.BackColor
                        e.Row.Cells(0).Controls.Add(hLink0)
                        e.Row.Cells(0).ColumnSpan = 2
                        e.Row.Cells(0).Font.Size = 10
                        e.Row.Cells(0).Font.Bold = True
                        e.Row.Cells(0).BackColor = gridTables.BackColor

                        Dim hLink1 As New HyperLink
                        hLink1.NavigateUrl = "~/Clubs/League Fixtures.aspx?League=" & ThisLeague
                        hLink1.Text = dt.Rows(gRow)(1)
                        hLink1.ForeColor = Yellow

                        e.Row.Cells(1).Controls.Add(hLink1)
                        e.Row.Cells(1).ColumnSpan = 2
                        e.Row.Cells(3).Visible = False
                        e.Row.Cells(4).Visible = False
                        e.Row.Cells(1).Font.Size = 9
                        e.Row.Cells(1).Font.Bold = True
                    Case "SKIT"
                        ThisLeague = dt.Rows(gRow)(0)
                        Dim hLink0 As New HyperLink
                        hLink0.NavigateUrl = "~/Clubs/League Tables.aspx?League=" & ThisLeague
                        hLink0.Text = dt.Rows(gRow)(0)
                        hLink0.ForeColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)

                        e.Row.Cells(0).BorderColor = gridTables.BackColor
                        e.Row.Cells(0).Controls.Add(hLink0)
                        e.Row.Cells(0).ColumnSpan = 2
                        e.Row.Cells(0).Font.Size = 10
                        e.Row.Cells(0).Font.Bold = True
                        e.Row.Cells(0).BackColor = gridTables.BackColor

                        Dim hLink1 As New HyperLink
                        hLink1.NavigateUrl = "~/Clubs/League Fixtures.aspx?League=" & ThisLeague
                        hLink1.Text = dt.Rows(gRow)(1)
                        hLink1.ForeColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)

                        e.Row.Cells(1).Controls.Add(hLink1)
                        e.Row.Cells(1).ColumnSpan = 2
                        e.Row.Cells(3).Visible = False
                        e.Row.Cells(4).Visible = False
                        e.Row.Cells(1).Font.Size = 9
                        e.Row.Cells(1).Font.Bold = True
                    Case "SNOO"
                        ThisLeague = dt.Rows(gRow)(0)
                        Dim hLink0 As New HyperLink
                        hLink0.NavigateUrl = "~/Clubs/League Tables.aspx?League=" & ThisLeague
                        hLink0.Text = dt.Rows(gRow)(0)
                        hLink0.ForeColor = LightGreen

                        e.Row.Cells(0).BorderColor = gridTables.BackColor
                        e.Row.Cells(0).Controls.Add(hLink0)
                        e.Row.Cells(0).ColumnSpan = 2
                        e.Row.Cells(0).Font.Size = 10
                        e.Row.Cells(0).Font.Bold = True
                        e.Row.Cells(0).BackColor = gridTables.BackColor

                        Dim hLink1 As New HyperLink
                        hLink1.NavigateUrl = "~/Clubs/League Fixtures.aspx?League=" & ThisLeague
                        hLink1.Text = dt.Rows(gRow)(1)
                        hLink1.ForeColor = LightGreen

                        e.Row.Cells(1).Controls.Add(hLink1)
                        e.Row.Cells(1).ColumnSpan = 2
                        e.Row.Cells(3).Visible = False
                        e.Row.Cells(4).Visible = False
                        e.Row.Cells(1).Font.Size = 9
                        e.Row.Cells(1).Font.Bold = True
                    Case "Stat"   'teams
                        Dim hLink0 As New HyperLink
                        hLink0.NavigateUrl = "~/Clubs/Stats.aspx?League=" & ThisLeague & "&Team=" & dt.Rows(gRow)(1)
                        hLink0.ForeColor = Black
                        hLink0.Text = "Stats"
                        e.Row.Cells(0).Controls.Add(hLink0)

                        Dim hLink1 As New HyperLink
                        hLink1.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=" & ThisLeague & "&Team=" & dt.Rows(gRow)(1)
                        hLink1.ForeColor = Cyan
                        hLink1.Text = dt.Rows(gRow)(1)
                        If dt.Rows(gRow)(4).ToString = "Y" Then     'Highlight champions ?
                            e.Row.BackColor = DarkRed
                            hLink1.ForeColor = White
                            e.Row.Cells(1).ForeColor = White
                            e.Row.Cells(2).ForeColor = White
                            e.Row.Cells(3).ForeColor = White
                        End If
                        e.Row.Cells(1).Controls.Add(hLink1)
                End Select
            Else
                e.Row.Cells(0).BackColor = gridTables.BackColor
                e.Row.Cells(0).BorderColor = gridTables.BackColor
                Select Case Left(dt.Rows(gRow)(1), 4)
                    Case "Team"
                        e.Row.Cells(1).ForeColor = Tan
                        e.Row.Cells(2).ForeColor = Tan
                        e.Row.Cells(3).ForeColor = Tan
                        e.Row.Cells(3).BackColor = gridTables.BackColor
                        e.Row.Cells(4).ForeColor = Tan
                    Case ""
                        e.Row.Cells(0).ColumnSpan = 5
                        e.Row.Cells(1).Visible = False
                        e.Row.Cells(2).Visible = False
                        e.Row.Cells(3).Visible = False
                        e.Row.Cells(4).Visible = False
                End Select
            End If
            gRow = gRow + 1
        End If
    End Sub


    Protected Sub gridResults_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResults.RowDataBound
        Dim RowLeague As String

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If Not IsDBNull(dt.Rows(gRow)(3)) Then
                If Not dt.Rows(gRow)(2).ToString.Contains("OPEN WEEK") AndAlso Not dt.Rows(gRow)(2).ToString.Contains("CHRISTMAS") Then
                    Dim hLink1 As New HyperLink
                    Dim hLink2 As New HyperLink
                    Dim hLink3 As New HyperLink
                    RowLeague = Replace(dt.Rows(gRow)(0).ToString, "  ", " DIVISION ")

                    If dt.Rows(gRow)(2).ToString.Contains(" CUP ") OrElse dt.Rows(gRow)(2).ToString.Contains(" PLAYOFF") Then
                        e.Row.Cells(2).ColumnSpan = 3
                        e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
                        If dt.Rows(gRow)(2).ToString.Contains("CRIB CUP") Then hLink1.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=CRIB DIVISION 1&Comp=CUP - TEAM KO"
                        If dt.Rows(gRow)(2).ToString.Contains("PAIRS CUP") Then hLink1.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=CRIB DIVISION 1&Comp=CUP - PAIRS"
                        If dt.Rows(gRow)(2).ToString.Contains("12-A-SIDE CUP") Then hLink1.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SKITTLES DIVISION 1&Comp=CUP - 12-A-SIDE TEAM KO"
                        If dt.Rows(gRow)(2).ToString.Contains("6-A-SIDE CUP") Then hLink1.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SKITTLES DIVISION 1&Comp=CUP - 6-A-SIDE TEAM KO"
                        If dt.Rows(gRow)(2).ToString.Contains("SNOOKER 1 CUP") Then hLink1.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 1&Comp=CUP - TEAM KO"
                        If dt.Rows(gRow)(2).ToString.Contains("SNOOKER 2 CUP") Then hLink1.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 2&Comp=CUP - TEAM KO"
                        If dt.Rows(gRow)(2).ToString.Contains("SNOOKER 1 3-A-SIDE CUP") Then hLink1.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 1&Comp=CUP - 3-A-SIDE"
                        If dt.Rows(gRow)(2).ToString.Contains("SNOOKER 2 3-A-SIDE CUP") Then hLink1.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 2&Comp=CUP - 3-A-SIDE"
                        If dt.Rows(gRow)(2).ToString.Contains("SNOOKER 1 DOUBLES CUP") Then hLink1.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 1&Comp=CUP - DOUBLES"
                        If dt.Rows(gRow)(2).ToString.Contains("SNOOKER 2 DOUBLES CUP") Then hLink1.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 2&Comp=CUP - DOUBLES"
                        If dt.Rows(gRow)(2).ToString.Contains("SNOOKER 1 SINGLES CUP") Then hLink1.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 1&Comp=CUP - SINGLES"
                        If dt.Rows(gRow)(2).ToString.Contains("SNOOKER 2 SINGLES CUP") Then hLink1.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 2&Comp=CUP - SINGLES"
                    Else
                        hLink1.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=" & RowLeague & "&Team=" & dt.Rows(gRow)(2).ToString
                    End If

                    hLink1.Text = e.Row.Cells(2).Text
                    hLink1.ForeColor = Cyan
                    hLink1.Attributes.Add("onmouseover", "this.className='cell'")
                    e.Row.Cells(2).Controls.Add(hLink1)
                    e.Row.Cells(2).CssClass = "cell"

                    hLink2.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=" & RowLeague & "&Team=" & dt.Rows(gRow)(4).ToString
                    hLink2.Text = e.Row.Cells(4).Text
                    hLink2.ForeColor = Cyan
                    'hLink2.BackColor = gridResults.BackColor
                    e.Row.Cells(4).Controls.Add(hLink2)
                    If Not IsDBNull(dt.Rows(gRow)(6)) Then
                        If dt.Rows(gRow)(6) <> "" Then
                            Dim Status As Integer = Val(dt.Rows(gRow)(7))
                            Select Case Status
                                Case 2
                                    If objGlobals.AdminLogin Then
                                        hLink3.NavigateUrl = "~/Clubs/Admin/Add Edit Result.aspx?ID=" & Val(dt.Rows(gRow)(6)).ToString & "&Week=" & dt.Rows(gRow)(8)
                                    Else
                                        hLink3.NavigateUrl = "~/Clubs/Result Card.aspx?CompID=" & Val(dt.Rows(gRow)(6)) & "&Week=" & Mid(ddWeeks.Text, 6, 2)
                                    End If
                                    hLink3.ForeColor = LightGreen
                                Case 1
                                    If objGlobals.AdminLogin Then
                                        hLink3.NavigateUrl = "~/Clubs/Admin/Add Edit Result.aspx?ID=" & Val(dt.Rows(gRow)(6)).ToString & "&Week=" & dt.Rows(gRow)(8)
                                    Else
                                        hLink3.NavigateUrl = "~/Clubs/Result Card.aspx?CompID=" & Val(dt.Rows(gRow)(6)) & "&Week=" & Mid(ddWeeks.Text, 6, 2)
                                    End If
                                    hLink3.ForeColor = Orange
                                Case 0
                                    hLink3.NavigateUrl = "~/Clubs/Admin/Fixture Result.aspx?ID=" & Val(dt.Rows(gRow)(6)).ToString & "&Week=" & dt.Rows(gRow)(8)
                                    hLink3.ForeColor = White
                                Case -1
                                    If objGlobals.AdminLogin Then
                                        hLink3.NavigateUrl = "~/Clubs/Admin/Fixture Result.aspx?ID=" & Val(dt.Rows(gRow)(6)).ToString & "&Week=" & dt.Rows(gRow)(8)
                                    Else
                                        hLink3.NavigateUrl = "~/Clubs/Default.aspx?Week=" & dt.Rows(gRow)(8) & "&CompID=" & dt.Rows(gRow)(6)
                                    End If
                                    hLink3.ForeColor = White
                            End Select
                            hLink3.Text = e.Row.Cells(5).Text
                            e.Row.Cells(5).Controls.Add(hLink3)
                        End If
                    End If
                Else
                    e.Row.Cells(2).ColumnSpan = 3
                    e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
                End If

                Dim hLink4 As New HyperLink
                '23/1/14 - add link to league
                If Left(dt.Rows(gRow)(0).ToString, 4) = "CRIB" Or Left(dt.Rows(gRow)(0).ToString, 4) = "SKIT" Or Left(dt.Rows(gRow)(0).ToString, 4) = "SNOO" Then
                    hLink4.NavigateUrl = "~/Clubs/League Tables.aspx?&League=" & dt.Rows(gRow)(0)
                    hLink4.Text = e.Row.Cells(0).Text
                    Select Case Left(dt.Rows(gRow)(0).ToString, 4)
                        Case "CRIB"
                            hLink4.ForeColor = Yellow
                        Case "SKIT"
                            hLink4.ForeColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)
                        Case "SNOO"
                            hLink4.ForeColor = LightGreen
                    End Select
                    e.Row.Cells(0).Controls.Add(hLink4)
                End If
                '23/11/18 - add links to cup comps
                If InStr(dt.Rows(gRow)(0).ToString, " CUP ", CompareMethod.Text) > 0 Then
                    hLink4.Text = e.Row.Cells(0).Text
                    Select Case Left(dt.Rows(gRow)(0).ToString, 4)
                        Case "CRIB"
                            If dt.Rows(gRow)(0).ToString.Contains("CRIB CUP") Then hLink4.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=CRIB DIVISION 1&Comp=CUP - TEAM KO"
                            If dt.Rows(gRow)(0).ToString.Contains("PAIRS CUP") Then hLink4.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=CRIB DIVISION 1&Comp=CUP - PAIRS"
                            hLink4.ForeColor = Yellow
                        Case "SKIT"
                            If dt.Rows(gRow)(0).ToString.Contains("12-A-SIDE CUP") Then hLink4.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SKITTLES DIVISION 1&Comp=CUP - 12-A-SIDE TEAM KO"
                            If dt.Rows(gRow)(0).ToString.Contains("6-A-SIDE CUP") Then hLink4.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SKITTLES DIVISION 1&Comp=CUP - 6-A-SIDE TEAM KO"
                            hLink4.ForeColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)
                        Case "SNOO"
                            If dt.Rows(gRow)(0).ToString.Contains("SNOOKER 1 CUP") Then hLink4.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 1&Comp=CUP - TEAM KO"
                            If dt.Rows(gRow)(0).ToString.Contains("SNOOKER 1 SINGLES CUP") Then hLink4.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 1&Comp=CUP - SINGLES"
                            If dt.Rows(gRow)(0).ToString.Contains("SNOOKER 1 DOUBLES CUP") Then hLink4.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 1&Comp=CUP - DOUBLES"
                            If dt.Rows(gRow)(0).ToString.Contains("SNOOKER 1 3-A-SIDE CUP") Then hLink4.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 1&Comp=CUP - 3-A-SIDE"
                            If dt.Rows(gRow)(0).ToString.Contains("SNOOKER 2 CUP") Then hLink4.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 2&Comp=CUP - TEAM KO"
                            If dt.Rows(gRow)(0).ToString.Contains("SNOOKER 2 SINGLES CUP") Then hLink4.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 2&Comp=CUP - SINGLES"
                            If dt.Rows(gRow)(0).ToString.Contains("SNOOKER 2 DOUBLES CUP") Then hLink4.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 2&Comp=CUP - DOUBLES"
                            If dt.Rows(gRow)(0).ToString.Contains("SNOOKER 2 3-A-SIDE CUP") Then hLink4.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=SNOOKER DIVISION 2&Comp=CUP - 3-A-SIDE"
                            hLink4.ForeColor = LightGreen
                    End Select
                    e.Row.Cells(0).Controls.Add(hLink4)
                End If

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


    'Protected Sub imgFacebook_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgFacebook.Click
    '    Dim mailFrom As String = "barryleagues@barryleagues.co.uk"
    '    Dim mailSubject As String = "Barry Leagues Facebook Group request pending"
    '    Dim mailBody As String = "Request sent"

    '    Dim myMail2 As New System.Net.Mail.MailMessage(mailFrom, "jonathanwilliams996@msn.com", mailSubject, mailBody)
    '    myMail2.IsBodyHtml = False

    '    Dim MailClient As System.Net.Mail.SmtpClient = New System.Net.Mail.SmtpClient("smtpout.europe.secureserver.net")
    '    MailClient.UseDefaultCredentials = False
    '    MailClient.Credentials = New System.Net.NetworkCredential("barryleagues@barryleagues.co.uk", "Tallycam106")
    '    MailClient.Send(myMail2)
    '    MailClient.EnableSsl = True
    'End Sub


    Protected Sub btnGo_Click(sender As Object, e As System.EventArgs) Handles btnGo.Click
        Response.Redirect("~/Clubs/Club Fixtures.aspx?Club=" & ddlVenues.Text)
    End Sub


    Protected Sub ddWeeks_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddWeeks.SelectedIndexChanged
        CurrentWeek = Val(Mid(ddWeeks.Text, 6, 2))
        Call load_week_results(CurrentWeek)
    End Sub

    Protected Sub ddWeeks_TextChanged(sender As Object, e As System.EventArgs) Handles ddWeeks.TextChanged
        CurrentWeek = Val(Mid(ddWeeks.Text, 6, 2))
        Call load_week_results(CurrentWeek)
    End Sub

    Sub write_PDF_download(ByVal inFilepath As String)
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim l_param_in_names(2) As String
        Dim l_param_in_values(2) As String

        l_param_in_names(0) = "@inLeague"
        l_param_in_values(0) = "LEAGUE RULES " & objGlobals.current_season
        l_param_in_names(1) = "@inTeam"
        l_param_in_values(1) = ""
        l_param_in_names(2) = "@inFilepath"
        l_param_in_values(2) = Replace(inFilepath, "'", """")

        strSQL = "EXEC [clubs].[sp_write_download_PDF] '" & l_param_in_values(0) & "','" & l_param_in_values(1) & "','" & l_param_in_values(2) & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        objGlobals.close_connection()
    End Sub

    Protected Sub btnRules_Click(sender As Object, e As EventArgs) Handles btnRules.Click
        Dim FilePath As String = Server.MapPath("LeagueRules") & "\"
        Dim filename As String = "RULES_" & objGlobals.current_season & ".pdf"
        Dim PDFfile As String = FilePath + filename

        Dim client As New WebClient
        Dim ByteArray As Byte() = client.DownloadData(PDFfile)

        'write details for PDF_downloads
        write_PDF_download(PDFfile)

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
    Protected Sub TimerRefresh_Tick(sender As Object, e As EventArgs) Handles TimerRefresh.Tick
        CurrentWeek = Val(Mid(ddWeeks.Text, 6, 2))
        Call load_week_results(CurrentWeek)

        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim UpdatedCount As Integer
        Dim RecordCount As Integer


        strSQL = "SELECT COUNT(*) AS RecordCount FROM clubs.latest_results WHERE ip_address = ''"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            RecordCount = myDataReader.Item("RecordCount")
        End While

        lblLatest.Text = "Latest " + RecordCount.ToString + " League Results"

        Dim strIPAddress As String = GetIPAddress()

        strSQL = "EXEC clubs.sp_update_latest_results_ip_address '" & strIPAddress & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        strSQL = "SELECT TOP 1 row_number,fixture_id FROM clubs.latest_results WHERE refreshed = 0 AND ip_address = '" & strIPAddress & "' ORDER BY row_number"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If Not myDataReader.HasRows Then
            strSQL = "UPDATE clubs.latest_results SET refreshed = 0 WHERE ip_address = '" & strIPAddress & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
        Else
            While myDataReader.Read()
                UpdatedCount = myDataReader.Item("row_number")
                lblCurrent.Text = UpdatedCount.ToString + " of " + RecordCount.ToString
                CompID = myDataReader.Item("fixture_id")

                strSQL = "UPDATE clubs.latest_results SET refreshed = 1 WHERE fixture_id = " & CompID.ToString & " AND ip_address = '" & strIPAddress & "'"
                myDataReader = objGlobals.SQLSelect(strSQL)
                Call show_card()
            End While

        End If

        objGlobals.close_connection()
    End Sub
    Private Function GetIPAddress() As String

        GetIPAddress = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")

        If String.IsNullOrEmpty(GetIPAddress) Then
            GetIPAddress = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
        End If

    End Function
    Protected Sub load_latest_card()
        gridResults.Visible = False
        lblLatest.Visible = False
        lblCurrent.Visible = False
        chkRefresh.Visible = False
        btnView.Visible = False

        CurrentWeek = Val(Mid(ddWeeks.Text, 6, 2))

        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim UpdatedCount As Integer
        Dim RecordCount As Integer

        strSQL = "UPDATE clubs.latest_results SET refreshed = 0"
        myDataReader = objGlobals.SQLSelect(strSQL)

        strSQL = "SELECT COUNT(*) AS RecordCount FROM clubs.latest_results WHERE ip_address = ''"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If myDataReader.HasRows Then
            While myDataReader.Read()
                RecordCount = myDataReader.Item("RecordCount")
            End While

            lblLatest.Text = "Latest " + RecordCount.ToString + " League Results"
            lblLatest.Width = 230
            UpdatedCount = 1
            lblCurrent.Text = UpdatedCount.ToString + " of " + RecordCount.ToString

            strSQL = "SELECT TOP 1 fixture_id FROM clubs.latest_results"
            myDataReader = objGlobals.SQLSelect(strSQL)
            While myDataReader.Read()
                CompID = myDataReader.Item("fixture_id")
                Call show_card()
            End While
        End If

        objGlobals.close_connection()
    End Sub

    Sub show_card()
        gridResults.Visible = True
        lblLatest.Visible = True
        lblCurrent.Visible = True
        chkRefresh.Visible = True
        btnView.Visible = True

        gridSkittlesResult.Visible = False
        gridCribResult.Visible = False
        gridSnookerResult.Visible = False

        Call load_fixture_result()
        Select Case Left(FixtureLeague, 4)
            Case "SKIT"
                Call load_skittles_header()
                Call load_skittles_result()
                Call load_skittles_totals()
                gridSkittlesResult.Visible = True
                btnView.Text = "View Card " & gridSkittlesResult.Rows(0).Cells(4).Text & " "
            Case "CRIB"
                Call load_crib_header()
                Call load_crib_result()
                Call load_crib_totals()
                gridCribResult.Visible = True
                btnView.Text = "View Card " & gridCribResult.Rows(0).Cells(4).Text & " "
            Case "SNOO"
                Call load_snooker_header()
                Call load_snooker_result()
                Call load_snooker_totals()
                gridSnookerResult.Visible = True
                btnView.Text = "View Card " & gridSnookerResult.Rows(0).Cells(4).Text & " "
        End Select
    End Sub
    Sub load_fixture_result()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

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
            'FixtureStatus = myDataReader.Item("status")
            'btnClose.Visible = True
            'If myDataReader.Item("status") < 2 Then
            '    lblNoCard.Visible = True
            '    lblPlayerStats.Visible = False
            'Else
            '    lblNoCard.Visible = False
            '    lblPlayerStats.Visible = True
            'End If

        End While
        objGlobals.close_connection()

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
    End Sub

    Sub load_skittles_result()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

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

        gRow = 0
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


        dr = dt.NewRow
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Home Player") = "Pair"
        dr("Home Points") = "Score"
        dr("Away Player") = "Pair"
        dr("Away Points") = "Score"
        dt.Rows.Add(dr)
    End Sub

    Sub load_crib_result()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

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
        dr("Home Points") = TotalHome
        dr("Away Points") = TotalAway
        dr("Away Player") = "POINTS :"
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Home Player") = "POINTS DEDUCTED :"
        dr("Home Points") = HomePointsDeducted
        dr("Away Player") = "POINTS DEDUCTED :"
        dr("Away Points") = AwayPointsDeducted
        dt.Rows.Add(dr)

        gRow = 0
        gridCribResult.Visible = True
        gridCribResult.DataSource = dt
        For i As Integer = 1 To 7
            dr = dt.NewRow
            dr("Home Player") = ""
            dr("Home Points") = ""
            dr("Away Points") = ""
            dr("Away Player") = ""
            dt.Rows.Add(dr)
        Next
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


        dr = dt.NewRow
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Home Player") = "Player"
        dr("Home Points") = "Score"
        dr("Away Player") = "Player"
        dr("Away Points") = "Score"
        dt.Rows.Add(dr)
    End Sub

    Sub load_snooker_result()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

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
        dr("Home Points") = TotalHome
        dr("Away Points") = TotalAway
        dr("Away Player") = "POINTS :"
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Home Player") = "POINTS DEDUCTED :"
        dr("Home Points") = HomePointsDeducted
        dr("Away Player") = "POINTS DEDUCTED :"
        dr("Away Points") = AwayPointsDeducted
        dt.Rows.Add(dr)

        gRow = 0
        gridSnookerResult.Visible = True
        gridSnookerResult.DataSource = dt
        For i As Integer = 1 To 8
            dr = dt.NewRow
            dr("Home Player") = ""
            dr("Home Points") = ""
            dr("Away Points") = ""
            dr("Away Player") = ""
            dt.Rows.Add(dr)
        Next
        gridSnookerResult.DataBind()
    End Sub

    Private Sub gridSkittlesResult_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSkittlesResult.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Select Case e.Row.Cells(1).Text
                Case "PLAYERS TOTAL :"
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
                                e.Row.Cells(4).Font.Size = 14
                                e.Row.Cells(4).Font.Bold = True
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
                                e.Row.Cells(0).ColumnSpan = 4
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
                        Select Case gRow
                            Case 6, 8, 10
                                e.Row.Cells(0).RowSpan = 2
                                e.Row.Cells(2).RowSpan = 2
                                e.Row.Cells(4).RowSpan = 2
                            Case 7, 9, 11
                                e.Row.Cells(0).Visible = False
                                e.Row.Cells(2).Visible = False
                                e.Row.Cells(4).Visible = False
                            Case > 11
                                e.Row.Cells(0).ColumnSpan = 5
                                e.Row.Cells(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#1B1B1B")
                                e.Row.Cells(0).BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B1B1B")
                                e.Row.Cells(1).Visible = False
                                e.Row.Cells(2).Visible = False
                                e.Row.Cells(3).Visible = False
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
                                e.Row.Cells(4).Font.Size = 14
                                e.Row.Cells(4).Font.Bold = True
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
                    Select Case gRow
                        Case 1
                            e.Row.Cells(0).Font.Bold = True
                            e.Row.Cells(0).Font.Size = 14
                            e.Row.Cells(0).ColumnSpan = 4
                            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
                            e.Row.Cells(1).Visible = False
                            e.Row.Cells(2).Visible = False
                            e.Row.Cells(3).Visible = False
                            e.Row.Cells(4).Font.Size = 14
                            e.Row.Cells(4).Font.Bold = True
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
                        Case > 10
                            e.Row.Cells(0).ColumnSpan = 5
                            e.Row.Cells(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#1B1B1B")
                            e.Row.Cells(0).BorderColor = System.Drawing.ColorTranslator.FromHtml("#1B1B1B")
                            e.Row.Cells(1).Visible = False
                            e.Row.Cells(2).Visible = False
                            e.Row.Cells(3).Visible = False
                            e.Row.Cells(4).Visible = False
                    End Select
            End Select
        Else
            e.Row.Visible = False
        End If
        gRow = gRow + 1

    End Sub

    Protected Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Select Case True
            Case gridCribResult.Visible
                Response.Redirect("~/Clubs/Result Card.aspx?CompID=" & gridCribResult.Rows(0).Cells(4).Text)
            Case gridSkittlesResult.Visible
                Response.Redirect("~/Clubs/Result Card.aspx?CompID=" & gridSkittlesResult.Rows(0).Cells(4).Text)
            Case gridSnookerResult.Visible
                Response.Redirect("~/Clubs/Result Card.aspx?CompID=" & gridSnookerResult.Rows(0).Cells(4).Text)
        End Select
    End Sub
    Protected Sub chkRefresh_CheckedChanged(sender As Object, e As EventArgs) Handles chkRefresh.CheckedChanged
        Dim strIPAddress As String = GetIPAddress()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

        If chkRefresh.Checked Then
            strSQL = "EXEC clubs.sp_update_result_refresh_flag '" & strIPAddress & "',1"
            myDataReader = objGlobals.SQLSelect(strSQL)
            TimerRefresh.Enabled = True
            TimerRefresh_Tick(Me, e)
        Else
            strSQL = "EXEC clubs.sp_update_result_refresh_flag '" & strIPAddress & "',0"
            myDataReader = objGlobals.SQLSelect(strSQL)
            TimerRefresh.Enabled = False
        End If

        objGlobals.close_connection()

        load_week_results(Val(Mid(ddWeeks.Text, 6, 2)))
    End Sub

End Class
