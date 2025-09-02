Imports System.Data.OleDb
Imports System.Drawing.Color
Imports System.Data
Imports System.Net

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
    Private HomePlayersTotal As Integer
    Private AwayPlayersTotal As Integer
    Private HomePoints As Integer
    Private AwayPoints As Integer
    Private HomeRollsWon As Double
    Private AwayRollsWon As Double
    Private HomeRollTotal As Integer
    Private AwayRollTotal As Integer
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
    Private Status As Integer
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
        CurrentWeek = Request.QueryString("Week")
        CompID = Request.QueryString("CompID")
        'lblCorona.Visible = True
        If Not IsPostBack Then
            Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)

            divUpdate.Visible = False
            Call load_next_meeting()
            Call load_stats()
            current_season = objGlobals.get_current_season()
            lblHonours.Text = Replace(objGlobals.last_season, "_20", "/") & " Honours"
            'HyperLinkCD.Text = Replace(objGlobals.current_season, "_20", "/") & " Delegates"
            MoreHL.NavigateUrl = "~/Mens_Skit/Honours.aspx"
            'HyperLinkCD.NavigateUrl = "~/Mens_Skit/Delegates.aspx"
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
            dt.Columns.Add(New DataColumn("Number_Rolls", GetType(System.String)))
            dt.Columns.Add(New DataColumn("Pins", GetType(System.String)))
            dt.Columns.Add(New DataColumn("Number_Thirties", GetType(System.String)))
            dt.Columns.Add(New DataColumn("Show Champions", GetType(System.String)))

            Call load_latest_tables("DIVISION 1")
            Call load_latest_tables("DIVISION 2")
            'Call load_latest_tables("DIVISION 3")
            gRow = 0
            gridTables.DataSource = dt
            gridTables.DataBind()

            'Call load_current_comp("Alan Rosser Cup", lblAlanRosser, lblAlanRosserDate, hlAlanRosser)
            Call load_current_comp("Allform Cup", lblAllform, lblAllformDate, hlAllform)
            Call load_current_comp("Holme Towers Cup (Nomination)", lblHolmeTowers, lblHolmeTowersDate, hlHolmeTowers)
            'Call load_current_comp("Gary Mitchell Cup", lblGaryMitchell, lblGaryMitchellDate, hlGaryMitchell)

            '4.8.14 - Add latest stats to homepage
            Call load_latest_stats("DIVISION 1")
            Call load_latest_stats("DIVISION 2")
            'Call load_latest_stats("DIVISION 3")
            '4.8.14 - End

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
            Dim myDataReader As oledbdatareader
            Dim myDataReader1 As oledbdatareader

            strSQL = "EXEC mens_skit.sp_get_result_refresh_flag '" & strIPAddress & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            While myDataReader.Read()
                chkRefresh.Checked = myDataReader.Item("refresh")
                If chkRefresh.Checked Then
                    strSQL = "EXEC mens_skit.sp_update_result_refresh_flag '" & strIPAddress & "',1"
                    myDataReader1 = objGlobals.SQLSelect(strSQL)
                    TimerRefresh.Enabled = True
                    TimerRefresh_Tick(Me, e)
                Else
                    strSQL = "EXEC mens_skit.sp_update_result_refresh_flag '" & strIPAddress & "',0"
                    myDataReader1 = objGlobals.SQLSelect(strSQL)
                    TimerRefresh.Enabled = False
                End If

            End While
            objGlobals.close_connection()

        End If
    End Sub
    Protected Sub btnRegistration_Click(sender As Object, e As EventArgs) Handles btnRegistration.Click
        Dim FilePath As String = Server.MapPath("MeetingMinutes") & "\"
        Dim filename As String = "RegistrationForm.pdf"
        Dim PDFfile As String = FilePath + filename

        Dim client As New WebClient
        Dim ByteArray As Byte() = client.DownloadData(PDFfile)

        'write details for PDF_downloads
        'write_PDF_download(PDFfile)

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
    Sub load_next_meeting()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim UKDateTime As Date = objGlobals.LondonDate(DateTime.UtcNow)
        Dim UKDate As String = UKDateTime.ToShortDateString
        strSQL = "SELECT TOP 1 meeting_venue,meeting_date FROM mens_skit.league_meetings WHERE CONVERT(VARCHAR(8),GETDATE(),112) <= date8 ORDER BY date8"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            lblNextMeeting.Text = myDataReader.Item("meeting_date") & " @ " & myDataReader.Item("meeting_venue")
        End While
        objGlobals.close_connection()
    End Sub

    Protected Sub load_weeks()
        Dim WeekText As String
        Dim CommenceDate As DateTime
        Dim EndDate As DateTime
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        ddWeeks.ClearSelection()
        strSQL = "SELECT week_number,week_commences FROM mens_skit.vw_weeks ORDER BY week_number"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            CommenceDate = myDataReader.Item("week_commences")
            'tempcode to extend week 1
            'If myDataReader.Item("week_number") <> 1 Then
            EndDate = DateAdd(DateInterval.Day, 4, CommenceDate)
            'Else
            'EndDate = DateAdd(DateInterval.Day, 11, CommenceDate)
            'End If
            WeekText = "Week " & myDataReader.Item("week_number").ToString & " : "
            WeekText = WeekText & objGlobals.AddSuffix(Left(Format(CommenceDate, "d MMM"), 2)) & Format(CommenceDate, " MMM") & " - "
            WeekText = WeekText & objGlobals.AddSuffix(Left(Format(EndDate, "d MMM"), 2)) & Format(EndDate, " MMM")
            ddWeeks.Items.Add(WeekText)
            If myDataReader.Item("week_number") = CurrentWeek Then ddWeeks.Text = WeekText
        End While
        objGlobals.close_connection()
        Call load_week_results(CurrentWeek)
    End Sub

    Sub load_stats()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        strSQL = "EXEC mens_skit.sp_get_stats '" + objGlobals.current_season + "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            txtStatsToday.Text = myDataReader.Item("today").ToString
            txtStatsWeek.Text = myDataReader.Item("week").ToString
            txtStatsSeason.Text = myDataReader.Item("season").ToString
        End While
    End Sub


    Sub load_latest_stats(inLeague As String)
        Dim Top10Player(99) As String
        Dim Top10Stat(99) As Double
        Dim strSQL As String = ""
        Dim myDataReader As oledbdatareader
        Dim PlayerCount As Integer

        Select Case inLeague
            Case "DIVISION 1"
                Dim LeagueSeries = chtLeague_Division1.Series("League")
                LeagueSeries.IsValueShownAsLabel = True
                LeagueSeries.LabelForeColor = White
                LeagueSeries.CustomProperties = "BarLabelStyle=Right"
                LeagueSeries.Url = "~/Mens_Skit/Stats.aspx?League=DIVISION 1"
                With chtLeague_Division1
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
                    strSQL = "SELECT player,average FROM mens_skit.vw_player_stats WHERE league='DIVISION 1' AND team_pos = 1 ORDER BY team"
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
            Case "DIVISION 2"
                Dim LeagueSeries = chtLeague_Division2.Series("League")
                LeagueSeries.IsValueShownAsLabel = True
                LeagueSeries.LabelForeColor = White
                LeagueSeries.CustomProperties = "BarLabelStyle=Right"
                LeagueSeries.Url = "~/Mens_Skit/Stats.aspx?League=DIVISION 2"
                With chtLeague_Division2
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
                    strSQL = "SELECT player,average FROM mens_skit.vw_player_stats WHERE league='DIVISION 2' AND team_pos = 1 ORDER BY team"
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
            Case "DIVISION 3"
                Dim LeagueSeries = chtLeague_Division3.Series("League")
                LeagueSeries.IsValueShownAsLabel = True
                LeagueSeries.LabelForeColor = White
                LeagueSeries.CustomProperties = "BarLabelStyle=Right"
                LeagueSeries.Url = "~/Mens_Skit/Stats.aspx?League=DIVISION 3"
                With chtLeague_Division3
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
                    strSQL = "SELECT player,average FROM mens_skit.vw_player_stats WHERE league='DIVISION 3' AND team_pos = 1 ORDER BY team"
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
        End Select

    End Sub

    Sub load_venues()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        ddlVenues.ClearSelection()

        strSQL = "SELECT DISTINCT(venue) FROM mens_skit.vw_teams WHERE long_name <> 'BYE' ORDER BY venue"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddlVenues.Items.Add(myDataReader.Item("venue"))
        End While

    End Sub

    Sub load_current_comp(ByVal inComp As String, ByVal inCompName As Label, ByVal inDate As Label, ByVal inHL As HyperLink)

        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        strSQL = "SELECT league,played_by,text,url,comp FROM mens_skit.current_comps WHERE comp = '" & inComp & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            inCompName.Visible = True
            inCompName.Text = inComp
            inDate.Visible = True
            inDate.Text = myDataReader.Item("played_by")
            If Not IsDBNull(myDataReader.Item("played_by")) Then
                inHL.Visible = True
                inHL.NavigateUrl = myDataReader.Item("url")
                inHL.Text = myDataReader.Item("text")
            End If
        End While

    End Sub

    Function MaxWeek() As Integer
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        strSQL = "SELECT MAX(week_number) FROM mens_skit.vw_weeks"
        MaxWeek = -1
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            MaxWeek = myDataReader.Item(0)
        End While
    End Function

    Sub show_current_week_text(ByVal inWeek As Integer)
        If inWeek >= 0 AndAlso inWeek - 1 < ddWeeks.Items.Count Then
            ddWeeks.BorderStyle = BorderStyle.Solid
            'show the current week
            If inWeek > 0 Then
                ddWeeks.Text = ddWeeks.Items(inWeek - 1).Text
            Else
                ddWeeks.Text = ddWeeks.Items(0).Text
            End If
            ddWeeks.BackColor = DarkBlue
        End If
        btnOutstanding.BackColor = Black

        'btn0.BackColor = Black
        btn1.BackColor = Black : btn2.BackColor = Black : btn3.BackColor = Black : btn4.BackColor = Black : btn5.BackColor = Black : btn6.BackColor = Black : btn7.BackColor = Black : btn8.BackColor = Black : btn9.BackColor = Black : btn10.BackColor = Black
        btn11.BackColor = Black : btn12.BackColor = Black : btn13.BackColor = Black : btn14.BackColor = Black : btn15.BackColor = Black : btn16.BackColor = Black : btn17.BackColor = Black : btn18.BackColor = Black : btn19.BackColor = Black : btn20.BackColor = Black
        btn21.BackColor = Black : btn22.BackColor = Black : btn23.BackColor = Black : btn24.BackColor = Black : btn25.BackColor = Black : btn26.BackColor = Black : btn27.BackColor = Black : btn28.BackColor = Black : btn29.BackColor = Black : btn30.BackColor = Black
        btn31.BackColor = Black : btn32.BackColor = Black : btn33.BackColor = Black : btn34.BackColor = Black : btn35.BackColor = Black : btn36.BackColor = Black : btn37.BackColor = Black : btn38.BackColor = Black : btn39.BackColor = Black : btn40.BackColor = Black
        btn41.BackColor = Black : btn42.BackColor = Black : btn43.BackColor = Black : btn44.BackColor = Black : btn45.BackColor = Black
        Select Case inWeek
            'Case 0 : btn0.BackColor = DarkBlue
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
        Dim myDataReader As oledbdatareader
        Dim iRow As Integer = 0

        strSQL = "SELECT a.Pos as Pos,a.Team as Team ,a.Pld as Pld, a.W as W, a.D as D, a.L as L ,a.Pts as Pts,b.show_champions as ShowChampions,"
        strSQL = strSQL & "ISNULL(d.Number_Thirties,0) as Number_Thirties,"
        strSQL = strSQL & "ISNULL(e.Number_Rolls,0) as Number_Rolls,"
        strSQL = strSQL & "a.Pins as Pins "
        strSQL = strSQL & "FROM mens_skit.vw_tables a "
        strSQL = strSQL & "INNER JOIN mens_skit.vw_leagues b ON b.League = '" & inLeague & "' "
        strSQL = strSQL & "LEFT OUTER JOIN mens_skit.vw_Number_Thirties d ON d.Team = a.Team" & " "
        strSQL = strSQL & "LEFT OUTER JOIN mens_skit.vw_Number_Rolls e ON e.Team = a.Team" & " "
        strSQL = strSQL & "WHERE a.League = '" & inLeague & "' AND a.Team <> 'BYE' ORDER BY a.Pos"
        myDataReader = objGlobals.SQLSelect(strSQL)
        dt.Rows.Add(inLeague, "", "FIXTURES", "", "")
        dt.Rows.Add("", "Team", "Pld", "  Pts", "Rolls", "Pins", "30+")
        gRow = 0
        While myDataReader.Read()
            With gridTables
                iRow = iRow + 1
                dr = dt.NewRow
                dr("Stats") = "Stats"
                dr("Team") = myDataReader.Item("team")
                dr("Pld") = myDataReader.Item("pld")
                dr("Pts") = myDataReader.Item("pts")
                If myDataReader.Item("pos") = 1 And myDataReader.Item("ShowChampions") = dr("Team") Then
                    dr("Show Champions") = "Y"
                Else
                    dr("Show Champions") = "N"
                End If
                dr("Pins") = myDataReader.Item("Pins")
                dr("Number_Rolls") = myDataReader.Item("Number_Rolls")
                If myDataReader.Item("Number_Thirties") > 0 Then
                    dr("Number_Thirties") = myDataReader.Item("Number_Thirties")
                End If
                dt.Rows.Add(dr)
            End With
        End While

        dt.Rows.Add("", "", "", "", "", "")


    End Sub

    Protected Sub btnOutstanding_Click(sender As Object, e As System.EventArgs) Handles btnOutstanding.Click
        btn1.BackColor = Black : btn2.BackColor = Black : btn3.BackColor = Black : btn4.BackColor = Black : btn5.BackColor = Black : btn6.BackColor = Black : btn7.BackColor = Black : btn8.BackColor = Black : btn9.BackColor = Black : btn10.BackColor = Black
        btn11.BackColor = Black : btn12.BackColor = Black : btn13.BackColor = Black : btn14.BackColor = Black : btn15.BackColor = Black : btn16.BackColor = Black : btn17.BackColor = Black : btn18.BackColor = Black : btn19.BackColor = Black : btn20.BackColor = Black
        btn21.BackColor = Black : btn22.BackColor = Black : btn23.BackColor = Black : btn24.BackColor = Black : btn25.BackColor = Black : btn26.BackColor = Black : btn27.BackColor = Black : btn28.BackColor = Black : btn29.BackColor = Black : btn30.BackColor = Black
        btn31.BackColor = Black : btn32.BackColor = Black : btn33.BackColor = Black : btn34.BackColor = Black : btn35.BackColor = Black : btn36.BackColor = Black : btn37.BackColor = Black : btn38.BackColor = Black : btn39.BackColor = Black
        lblVenueInstruction.Visible = False
        gridVenues.Visible = False
        btnWeekFixtures.BackColor = Black
        btnVenueFixtures.BackColor = Black
        btnOutstanding.BackColor = DarkBlue
        Call load_week_results(-1)
    End Sub

    Sub load_week_results(ByVal inWeek As Integer)
        If btnVenueFixtures.BackColor = DarkBlue Then
            Call show_current_week_text(inWeek)
            Call load_venue_fixtures()
            Exit Sub
        End If
        Dim strSQL As String
        Dim LastDate As String = Nothing
        Dim myDataReader As oledbdatareader
        Dim FixtureResult(9999) As String

        strSQL = "SELECT league,fixture_date,fixture_calendar,home_team_name,home_result,away_team_name,fixture_id,status,week_number,home_rolls_result,venue "
        strSQL = strSQL & "FROM mens_skit.fixtures_combined "
        strSQL = strSQL & "WHERE season = '" & objGlobals.get_current_season & "'"
        If inWeek <> -1 Then
            btnWeekFixtures.BackColor = DarkBlue
            btnVenueFixtures.BackColor = Black
            lblVenueInstruction.Visible = False
            gridVenues.Visible = False
            strSQL = strSQL & "AND week_number = " & inWeek
        Else
            strSQL = strSQL & "AND (status = -1 OR status = 1)"
            strSQL = strSQL & " AND fixture_type = 'L'"
        End If

        strSQL = strSQL & " ORDER BY fixture_calendar,fixture_date DESC,league"
        myDataReader = objGlobals.SQLSelect(strSQL)

        lblNoOutstanding.Visible = False
        If Not myDataReader.HasRows Then
            objGlobals.close_connection()
            If inWeek = -1 Then
                lblNoOutstanding.Visible = True
            End If
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
        dt.Columns.Add(New DataColumn("Home Rolls Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture ID", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Fixture ID2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Status", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Week Number", GetType(System.String)))
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
                dr("Home Rolls Result") = ""
                dr("Away Team Name") = ""
                dr("Fixture ID") = ""
                dr("Fixture ID2") = ""
                dr("Status") = ""
                dr("Week Number") = ""
                dr("Venue") = ""
                gRow = gRow + 1
                dt.Rows.Add(dr)

                dr = dt.NewRow
                dr("League") = myDataReader.Item("fixture_date")
                dr("Fixture Date") = ""
                dr("Home Team Name") = ""
                dr("Home Result") = ""
                dr("Home Rolls Result") = ""
                dr("Away Team Name") = ""
                dr("Fixture ID") = ""
                dr("Fixture ID2") = ""
                dr("Status") = ""
                dr("Week Number") = ""
                dr("Venue") = ""
                gRow = gRow + 1
                dt.Rows.Add(dr)
            End If
            With gridResults
                dr = dt.NewRow
                dr("League") = myDataReader.Item("league")
                dr("Home Team Name") = myDataReader.Item("home_team_name")
                dr("Home Result") = myDataReader.Item("home_result")
                If myDataReader.Item("status") >= 1 Then
                    dr("Home Rolls Result") = myDataReader.Item("home_rolls_result")
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
                    If myDataReader.Item("home_result") = "0 - 0" Then
                        dr("Fixture ID") = ""
                    ElseIf (Left(myDataReader.Item("league"), 4) = "DIVI" Or InStr(myDataReader.Item("league"), "ROSSER GROUP", CompareMethod.Text) > 0) And myDataReader.Item("status") >= 1 Then
                        dr("Fixture ID") = "View Card"
                    End If
                End If
                dr("Fixture ID2") = myDataReader.Item("fixture_id")
                dr("Status") = myDataReader.Item("status")
                dr("Week Number") = myDataReader.Item("week_number")
                dr("Venue") = myDataReader.Item("venue")
                gRow = gRow + 1
                dt.Rows.Add(dr)
            End With
        End While
        Dim irow As Integer
        Dim ThisResult As String
        gRow = 0
        gridResults.DataSource = dt
        gridResults.DataBind()
        With gridResults
            For irow = 0 To .Rows.Count - 1
                If Left(.Rows(irow).Cells(0).Text, 5) <> "&nbsp" _
                    And Left(.Rows(irow).Cells(0).Text, 4) <> "MENS" _
                    And Left(.Rows(irow).Cells(0).Text, 4) <> "DIVI" _
                    And Left(.Rows(irow).Cells(0).Text, 8) <> "A ROSSER" _
                    And Left(.Rows(irow).Cells(0).Text, 11) <> "ALAN ROSSER" _
                    And Left(.Rows(irow).Cells(0).Text, 10) <> "G MITCHELL" _
                    And Left(.Rows(irow).Cells(0).Text, 13) <> "GARY MITCHELL" _
                    And Left(.Rows(irow).Cells(0).Text, 10) <> "CUP FINALS" _
                    And Left(.Rows(irow).Cells(0).Text, 5) <> "HOLME" _
                    And Left(.Rows(irow).Cells(0).Text, 8) <> "CHAMPION" _
                    And Left(.Rows(irow).Cells(0).Text, 11) <> "SECREATRIES" _
                    And Left(.Rows(irow).Cells(0).Text, 7) <> "JUBILEE" _
                      And Left(.Rows(irow).Cells(0).Text, 7) <> "ALLFORM" Then
                    'date row
                    With .Rows(irow).Cells(0)
                        .BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                        .ForeColor = System.Drawing.Color.FromArgb(&HE4, &HBB, &H18)
                        .HorizontalAlign = HorizontalAlign.Center
                        .ColumnSpan = 7
                        .Font.Size = 12
                    End With
                Else
                    With .Rows(irow).Cells(0)
                        .ColumnSpan = 1
                        .ForeColor = System.Drawing.ColorTranslator.FromHtml("#6699FF")
                    End With
                    With .Rows(irow).Cells(3)
                        ThisResult = FixtureResult(Val(gridResults.Rows(irow).Cells(7).Text))
                        Select Case .Text
                            Case "0 - 0", "versus"
                                .BackColor = gridResults.BackColor
                                'If Left(gridResults.Rows(irow).Cells(2).Text, 4) <> "OPEN" And gridResults.Rows(irow).Cells(5).Text <> "&nbsp;" Then
                                If gridResults.Rows(irow).Cells(4).Text = "&nbsp;" Then
                                    gridResults.Rows(irow).Cells(3).Visible = False
                                Else
                                    '.ColumnSpan = 2
                                    If ThisResult <> "Postponed" Then
                                        .ForeColor = LightGreen
                                        .Text = "versus"
                                    Else
                                        .ForeColor = Red
                                        .Text = "Postponed"
                                    End If
                                End If
                                'Else
                                '    .Text = ""
                                'End If
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
        End With
        Call show_current_week_text(inWeek)
    End Sub

    Sub load_honours()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        strSQL = "SELECT league,team FROM mens_skit.tables WHERE pos = 1 AND season = '" & objGlobals.get_last_season & "' ORDER BY league"
        myDataReader = objGlobals.SQLSelect(strSQL)
        dt = New DataTable

        dt.Columns.Add(New DataColumn("league", GetType(System.String)))
        dt.Columns.Add(New DataColumn("team", GetType(System.String)))
        While myDataReader.Read
            With gridHonours
                dr = dt.NewRow
                dr("league") = myDataReader.Item(0)
                dr("team") = myDataReader.Item(1)
                dt.Rows.Add(dr)
            End With
        End While
        gridHonours.DataSource = dt
        gridHonours.DataBind()
        If myDataReader.HasRows Then
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
                        .ForeColor = System.Drawing.ColorTranslator.FromHtml("#6699FF")
                    End With
                    With .Rows(irow).Cells(1)
                        .Wrap = False
                        .ForeColor = Cyan
                        .HorizontalAlign = HorizontalAlign.Left
                    End With
                Next irow
            End With
        End If
    End Sub

    Private Sub gridTables_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTables.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If dt.Rows(gRow)(0) <> "" Then
                Select Case Left(dt.Rows(gRow)(0), 4)
                    Case "DIVI"
                        ThisLeague = dt.Rows(gRow)(0)
                        Dim hLink0 As New HyperLink
                        hLink0.NavigateUrl = "~/Mens_Skit/League Tables.aspx?League=" & ThisLeague
                        hLink0.Text = dt.Rows(gRow)(0)
                        hLink0.ForeColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)

                        e.Row.Cells(0).BorderColor = gridTables.BackColor
                        e.Row.Cells(0).Controls.Add(hLink0)
                        e.Row.Cells(0).ColumnSpan = 2
                        e.Row.Cells(0).Font.Size = 10
                        e.Row.Cells(0).Font.Bold = True
                        e.Row.Cells(0).BackColor = gridTables.BackColor

                        Dim hLink1 As New HyperLink
                        hLink1.NavigateUrl = "~/Mens_Skit/League Fixtures.aspx?League=" & ThisLeague
                        hLink1.Text = dt.Rows(gRow)(2)
                        hLink1.ForeColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)

                        e.Row.Cells(2).Controls.Add(hLink1)
                        e.Row.Cells(2).ColumnSpan = 2
                        e.Row.Cells(3).Visible = False
                        e.Row.Cells(4).Visible = False
                        e.Row.Cells(2).Font.Size = 10
                        e.Row.Cells(2).Font.Bold = True
                    Case "Stat"   'teams
                        Dim hLink0 As New HyperLink
                        hLink0.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & ThisLeague & "&Team=" & dt.Rows(gRow)(1)
                        hLink0.ForeColor = Black
                        hLink0.Text = "Stats"
                        e.Row.Cells(0).Controls.Add(hLink0)

                        Dim hLink1 As New HyperLink
                        hLink1.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & ThisLeague & "&Team=" & dt.Rows(gRow)(1)
                        hLink1.ForeColor = Cyan
                        hLink1.Text = dt.Rows(gRow)(1)
                        If dt.Rows(gRow)(7).ToString = "Y" Then     'Highlight champions ?
                            e.Row.CssClass = "cell"
                            e.Row.BackColor = DarkRed
                            hLink1.ForeColor = White
                            e.Row.Cells(1).ForeColor = White : e.Row.Cells(1).BackColor = DarkRed
                            e.Row.Cells(2).ForeColor = White : e.Row.Cells(2).BackColor = DarkRed
                            e.Row.Cells(3).ForeColor = White : e.Row.Cells(3).BackColor = DarkRed
                            e.Row.Cells(4).ForeColor = White : e.Row.Cells(4).BackColor = DarkRed
                            e.Row.Cells(5).ForeColor = White : e.Row.Cells(5).BackColor = DarkRed
                            e.Row.Cells(6).ForeColor = White : e.Row.Cells(6).BackColor = DarkRed
                        End If
                        e.Row.Cells(2).Font.Size = 10
                        e.Row.Cells(3).Font.Size = 10
                        e.Row.Cells(4).Font.Size = 8
                        e.Row.Cells(5).Font.Size = 8
                        e.Row.Cells(6).Font.Size = 8
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
                        e.Row.Cells(5).ForeColor = Tan
                        e.Row.Cells(6).ForeColor = Tan
                        'e.Row.Cells(7).ForeColor = Tan
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
                'If Left(dt.Rows(gRow)(2), 4) <> "OPEN" Or Not IsDBNull(dt.Rows(gRow)(5)) Then
                If dt.Rows(gRow)(10) <> "OPEN" Then        'venue not OPEN ?
                    ''e.Row.CssClass = "row"
                    RowLeague = dt.Rows(gRow)(0).ToString
                    Dim hLink1 As New HyperLink
                    hLink1.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & RowLeague & "&Team=" & dt.Rows(gRow)(2).ToString
                    hLink1.Text = e.Row.Cells(2).Text
                    hLink1.ForeColor = Cyan
                    hLink1.Attributes.Add("onmouseover", "this.className='cell'")
                    e.Row.Cells(2).Controls.Add(hLink1)
                    e.Row.Cells(2).CssClass = "cell"

                    Dim hLink2 As New HyperLink
                    hLink2.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=" & RowLeague & "&Team=" & dt.Rows(gRow)(4).ToString
                    hLink2.Text = e.Row.Cells(4).Text
                    hLink2.ForeColor = Cyan
                    e.Row.Cells(4).Controls.Add(hLink2)
                    If Not IsDBNull(dt.Rows(gRow)(7)) Then
                        If dt.Rows(gRow)(7) <> "" Then
                            Dim hLink3 As New HyperLink
                            Dim Status As Integer = Val(dt.Rows(gRow)(8))
                            Select Case Status
                                Case 2
                                    e.Row.Cells(3).Font.Size = 10
                                    If objGlobals.AdminLogin Then
                                        If InStr(dt.Rows(gRow)(0), "ROSSER", CompareMethod.Text) = 0 Then
                                            hLink3.NavigateUrl = "~/Mens_Skit/Admin/Add Edit Result 2.aspx?ID=" & Val(dt.Rows(gRow)(7)).ToString & "&Week=" & dt.Rows(gRow)(9)
                                        Else
                                            hLink3.NavigateUrl = "~/Mens_Skit/Admin/Fixture Result AR.aspx?ID=" & Val(dt.Rows(gRow)(7)).ToString & "&Fixture=" & dt.Rows(gRow)(2) & " v " & dt.Rows(gRow)(4) & "&Group=PLAYOFFS"
                                        End If
                                    Else
                                        If InStr(dt.Rows(gRow)(0), "ROSSER", CompareMethod.Text) = 0 Then
                                            'hLink3.NavigateUrl = "~/Mens_Skit/Default.aspx?Week=" & dt.Rows(gRow)(9) & "&CompID=" & dt.Rows(gRow)(7) & "&FixtureType=League"
                                            hLink3.NavigateUrl = "~/Mens_Skit/Result Card.aspx?CompID=" & dt.Rows(gRow)(7) & "&Week=" & Mid(ddWeeks.Text, 6, 2)
                                        Else
                                            hLink3.NavigateUrl = "~/Mens_Skit/Default.aspx?Week=" & dt.Rows(gRow)(9) & "&CompID=" & dt.Rows(gRow)(7) & "&FixtureType=ARosser"
                                        End If
                                    End If
                                    hLink3.ForeColor = LightGreen
                                Case 1
                                    e.Row.Cells(3).Font.Size = 10
                                    If objGlobals.AdminLogin Then
                                        If InStr(dt.Rows(gRow)(0), "ROSSER", CompareMethod.Text) = 0 Then
                                            hLink3.NavigateUrl = "~/Mens_Skit/Admin/Add Edit Result 2.aspx?ID=" & Val(dt.Rows(gRow)(7)).ToString & "&Week=" & dt.Rows(gRow)(9)
                                        Else
                                            hLink3.NavigateUrl = "~/Mens_Skit/Admin/Fixture Result AR.aspx?ID=" & Val(dt.Rows(gRow)(7)).ToString & "&Fixture=" & dt.Rows(gRow)(2) & " v " & dt.Rows(gRow)(4) & "&Group=PLAYOFFS"
                                        End If
                                    Else
                                        If InStr(dt.Rows(gRow)(0), "ROSSER", CompareMethod.Text) = 0 Then
                                            'hLink3.NavigateUrl = "~/Mens_Skit/Default.aspx?Week=" & dt.Rows(gRow)(9) & "&CompID=" & dt.Rows(gRow)(7) & "&FixtureType=League"
                                            hLink3.NavigateUrl = "~/Mens_Skit/Result Card.aspx?CompID=" & dt.Rows(gRow)(7)
                                        Else
                                            hLink3.NavigateUrl = "~/Mens_Skit/Default.aspx?Week=" & dt.Rows(gRow)(9) & "&CompID=" & dt.Rows(gRow)(7) & "&FixtureType=ARosser"
                                        End If
                                    End If
                                    hLink3.ForeColor = Orange
                                Case 0, -1
                                    If objGlobals.AdminLogin Then
                                        If InStr(dt.Rows(gRow)(0), "ROSSER", CompareMethod.Text) = 0 Then
                                            hLink3.NavigateUrl = "~/Mens_Skit/Admin/Fixture Result.aspx?ID=" & Val(dt.Rows(gRow)(7)).ToString & "&Week=" & dt.Rows(gRow)(9) ' & "&Fixture=" & dt.Rows(gRow)(2) & " v " & dt.Rows(gRow)(4) & "&Group=PLAYOFFS"
                                        Else
                                            hLink3.NavigateUrl = "~/Mens_Skit/Admin/Fixture Result AR.aspx?ID=" & Val(dt.Rows(gRow)(7)).ToString & "&Fixture=" & dt.Rows(gRow)(2) & " v " & dt.Rows(gRow)(4) & "&Group=PLAYOFFS"
                                        End If
                                    End If
                                    hLink3.ForeColor = White
                            End Select
                            hLink3.Text = e.Row.Cells(6).Text
                            e.Row.Cells(6).Controls.Add(hLink3)
                        End If
                    End If

                Else
                    e.Row.Cells(7).Visible = False
                    e.Row.Cells(10).Visible = False
                End If

                '23/1/14 - add link to league
                If Left(dt.Rows(gRow)(0).ToString, 4) = "DIVI" Then
                    Dim hLink4 As New HyperLink
                    hLink4.NavigateUrl = "~/Mens_Skit/League Tables.aspx?&League=" & dt.Rows(gRow)(0)
                    hLink4.Text = e.Row.Cells(0).Text
                    hLink4.ForeColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)
                    e.Row.Cells(0).Controls.Add(hLink4)
                End If
                '23/11/18 - add link to Alan Rosser Cup
                If Left(dt.Rows(gRow)(0).ToString, 8) = "A ROSSER" Then
                    Dim hLink4 As New HyperLink
                    hLink4.NavigateUrl = "~/Mens_Skit/Alan Rosser Cup.aspx?&Group=" & Right(dt.Rows(gRow)(0), 7)
                    hLink4.Text = e.Row.Cells(0).Text
                    hLink4.ForeColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)
                    e.Row.Cells(0).Controls.Add(hLink4)
                End If
                '23/11/18 - add link to Gary Mitchell Cup
                If Left(dt.Rows(gRow)(0).ToString, 10) = "G MITCHELL" Then
                    Dim hLink4 As New HyperLink
                    hLink4.NavigateUrl = "~/Mens_Skit/Cup Fixtures List.aspx?Comp=GARY MITCHELL CUP"
                    hLink4.Text = e.Row.Cells(0).Text
                    hLink4.ForeColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)
                    e.Row.Cells(0).Controls.Add(hLink4)
                End If
                '23/11/18 - add link to Allform Cup
                If Left(dt.Rows(gRow)(0).ToString, 7) = "ALLFORM" Then
                    Dim hLink4 As New HyperLink
                    hLink4.NavigateUrl = "~/Mens_Skit/Cup Fixtures List.aspx?Comp=ALLFORM CUP"
                    hLink4.Text = e.Row.Cells(0).Text
                    hLink4.ForeColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)
                    e.Row.Cells(0).Controls.Add(hLink4)
                End If
                '23/11/18 - add link to Holme Towers Cup
                If Left(dt.Rows(gRow)(0).ToString, 5) = "HOLME" Then
                    Dim hLink4 As New HyperLink
                    hLink4.NavigateUrl = "~/Mens_Skit/Cup Fixtures List.aspx?Comp=HOLME TOWERS CUP (NOMINATION)"
                    hLink4.Text = e.Row.Cells(0).Text
                    hLink4.ForeColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)
                    e.Row.Cells(0).Controls.Add(hLink4)
                End If
            End If
            gRow = gRow + 1
        End If
    End Sub


    'Protected Sub btn0_Click(sender As Object, e As System.EventArgs) Handles btn0.Click
    '    CurrentWeek = Val(btn0.Text)
    '    Call load_week_results(CurrentWeek)
    'End Sub
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

    Protected Sub btnGo_Click(sender As Object, e As System.EventArgs) Handles btnGo.Click
        Response.Redirect("~/Mens_Skit/Club Fixtures.aspx?Club=" & ddlVenues.Text)
    End Sub


    Protected Sub ddWeeks_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddWeeks.SelectedIndexChanged
        CurrentWeek = Val(Mid(ddWeeks.Text, 6, 2))
        Call load_week_results(CurrentWeek)
    End Sub

    Protected Sub ddWeeks_TextChanged(sender As Object, e As System.EventArgs) Handles ddWeeks.TextChanged
        CurrentWeek = Val(Mid(ddWeeks.Text, 6, 2))
        Call load_week_results(CurrentWeek)
    End Sub

    Protected Sub TimerRefresh_Tick(sender As Object, e As EventArgs) Handles TimerRefresh.Tick
        CurrentWeek = Val(Mid(ddWeeks.Text, 6, 2))
        Call load_week_results(CurrentWeek)

        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim UpdatedCount As Integer
        Dim RecordCount As Integer

        strSQL = "SELECT COUNT(*) AS RecordCount FROM mens_skit.latest_results WHERE ip_address = ''"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            RecordCount = myDataReader.Item("RecordCount")
        End While

        lblLatest.Text = "Latest " + RecordCount.ToString + " League Results"

        Dim strIPAddress As String = GetIPAddress()

        strSQL = "EXEC mens_skit.sp_update_latest_results_ip_address '" & strIPAddress & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        strSQL = "SELECT TOP 1 row_number,fixture_id FROM mens_skit.latest_results WHERE refreshed = 0 AND ip_address = '" & strIPAddress & "' ORDER BY row_number"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If Not myDataReader.HasRows Then
            strSQL = "UPDATE mens_skit.latest_results SET refreshed = 0 WHERE ip_address = '" & strIPAddress & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
        Else
            While myDataReader.Read()
                UpdatedCount = myDataReader.Item("row_number")
                lblCurrent.Text = UpdatedCount.ToString + " of " + RecordCount.ToString
                CompID = myDataReader.Item("fixture_id")

                strSQL = "UPDATE mens_skit.latest_results SET refreshed = 1 WHERE fixture_id = " & CompID.ToString & " AND ip_address = '" & strIPAddress & "'"
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
        Dim myDataReader As oledbdatareader
        Dim UpdatedCount As Integer
        Dim RecordCount As Integer

        strSQL = "UPDATE mens_skit.latest_results SET refreshed = 0"
        myDataReader = objGlobals.SQLSelect(strSQL)

        strSQL = "SELECT COUNT(*) AS RecordCount FROM mens_skit.latest_results  WHERE ip_address = ''"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            RecordCount = myDataReader.Item("RecordCount")
        End While
        If RecordCount = 0 Then Exit Sub
        lblLatest.Text = "Latest " + RecordCount.ToString + " League Results"
        lblLatest.Width = 230
        UpdatedCount = 1
        lblCurrent.Text = UpdatedCount.ToString + " of " + RecordCount.ToString

        'strSQL = "TRUNCATE TABLE mens_skit.latest_results"
        strSQL = "SELECT TOP 1 fixture_id FROM mens_skit.latest_results WHERE ip_address = ''"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            CompID = myDataReader.Item("fixture_id")
            Call show_card()
        End While

        objGlobals.close_connection()
    End Sub

    Sub show_card()
        Call load_card()
        Call colour_rolls()
        Call colour_thirties()
        Call colour_high_scores()
        Call colour_totals()

        btnView.Text = "View Card " & gridResult.Rows(0).Cells(6).Text & " "
        gridResults.Visible = True
        lblLatest.Visible = True
        lblCurrent.Visible = True
        chkRefresh.Visible = True
        btnView.Visible = True
    End Sub

    Sub load_card()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        dt = New DataTable

        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Home Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Score", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Roll", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Roll No", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Roll", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Score", GetType(System.String)))

        strSQL = "EXEC mens_skit.sp_get_result_card " + CompID.ToString
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            Status = myDataReader.Item("status")
            If Status = 2 Then
                dr = dt.NewRow
                dr("Home Player") = myDataReader.Item("home_player")
                dr("Home Score") = myDataReader.Item("home_score")
                dr("Home Roll") = myDataReader.Item("home_roll")
                dr("Roll No") = myDataReader.Item("roll_no")
                dr("Away Roll") = myDataReader.Item("away_roll")
                dr("Away Player") = myDataReader.Item("away_player")
                dr("Away Score") = myDataReader.Item("away_score")
                If myDataReader.Item("ID") = 5 Then
                    HomeRollsWon = myDataReader.Item("home_roll")
                    AwayRollsWon = myDataReader.Item("away_roll")
                End If
                If myDataReader.Item("ID") = 13 Then
                    HomeRollTotal = myDataReader.Item("home_roll")
                    AwayRollTotal = myDataReader.Item("away_roll")
                End If
                If myDataReader.Item("ID") = 18 Then
                    HomePoints = myDataReader.Item("home_roll")
                    AwayPoints = myDataReader.Item("away_roll")
                End If
                dt.Rows.Add(dr)
                gRow = 0
                gridResult.DataSource = dt
                gridResult.DataBind()
                gridResult.Visible = True
            End If
        End While
    End Sub


    Sub colour_high_scores()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim home_high_score As Integer
        Dim away_high_score As Integer

        strSQL = "SELECT MAX(home_points) FROM mens_skit.vw_fixtures_detail WHERE fixture_id = " & CompID & " AND home_player NOT LIKE 'A N OTHER%'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            home_high_score = myDataReader.Item(0)
        End While
        strSQL = "SELECT MAX(away_points) FROM mens_skit.vw_fixtures_detail WHERE fixture_id = " & CompID & " AND away_player NOT LIKE 'A N OTHER%'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            away_high_score = myDataReader.Item(0)
        End While
        For gRow As Integer = 4 To 15
            If Val(gridResult.Rows(gRow).Cells(1).Text) = home_high_score Then
                gridResult.Rows(gRow).Cells(0).BackColor = Blue
                gridResult.Rows(gRow).Cells(1).BackColor = Blue
                gridResult.Rows(gRow).Cells(0).ForeColor = White
                gridResult.Rows(gRow).Cells(1).ForeColor = White
            End If
            If Val(gridResult.Rows(gRow).Cells(6).Text) = away_high_score Then
                gridResult.Rows(gRow).Cells(5).BackColor = Blue
                gridResult.Rows(gRow).Cells(6).BackColor = Blue
                gridResult.Rows(gRow).Cells(5).ForeColor = White
                gridResult.Rows(gRow).Cells(6).ForeColor = White
            End If
        Next
    End Sub

    Sub colour_thirties()
        For gRow As Integer = 4 To 15
            If Val(gridResult.Rows(gRow).Cells(6).Text) >= 30 Then
                gridResult.Rows(gRow).Cells(6).BackColor = LightGreen
            End If
        Next
    End Sub

    Sub colour_rolls()
        For gRow As Integer = 6 To 11
            If Val(gridResult.Rows(gRow).Cells(2).Text) > Val(gridResult.Rows(gRow).Cells(4).Text) Then
                gridResult.Rows(gRow).Cells(2).BackColor = LightGreen
            End If
            If Val(gridResult.Rows(gRow).Cells(2).Text) < Val(gridResult.Rows(gRow).Cells(4).Text) Then
                gridResult.Rows(gRow).Cells(4).BackColor = LightGreen
            End If
        Next
    End Sub

    Sub colour_totals()
        If HomePlayersTotal > AwayPlayersTotal Then
            gridResult.Rows(16).Cells(2).BackColor = Green
            gridResult.Rows(16).Cells(2).ForeColor = White
        End If
        If HomePlayersTotal < AwayPlayersTotal Then
            gridResult.Rows(16).Cells(4).BackColor = Green
            gridResult.Rows(16).Cells(4).ForeColor = White
        End If
        If HomePoints > AwayPoints Then
            gridResult.Rows(17).Cells(2).BackColor = Green
            gridResult.Rows(17).Cells(2).ForeColor = White
        End If
        If HomePoints < AwayPoints Then
            gridResult.Rows(17).Cells(4).BackColor = Green
            gridResult.Rows(17).Cells(4).ForeColor = White
        End If
        If HomeRollTotal > AwayRollTotal Then
            gridResult.Rows(12).Cells(2).BackColor = Green
            gridResult.Rows(12).Cells(2).ForeColor = White
        End If
        If HomeRollTotal < AwayRollTotal Then
            gridResult.Rows(12).Cells(4).BackColor = Green
            gridResult.Rows(12).Cells(4).ForeColor = White
        End If
        If HomeRollsWon > AwayRollsWon Then
            gridResult.Rows(4).Cells(2).BackColor = Green
            gridResult.Rows(4).Cells(2).ForeColor = White
        End If
        If HomeRollsWon < AwayRollsWon Then
            gridResult.Rows(4).Cells(4).BackColor = Green
            gridResult.Rows(4).Cells(4).ForeColor = White
        End If
    End Sub
    Private Sub gridResult_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResult.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            e.Row.Cells(3).BackColor = Drawing.Color.LightGray
            Select Case gRow
                Case 0
                    e.Row.Cells(0).Font.Bold = True
                    e.Row.Cells(0).ColumnSpan = 6
                    e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
                    For iCell As Integer = 1 To 5
                        e.Row.Cells(iCell).Visible = False
                    Next
                    e.Row.Cells(0).Font.Size = 14
                    e.Row.Cells(6).Font.Size = 14
                    e.Row.Cells(6).Font.Bold = True
                Case 1
                    FixtureHomeTeam = Replace(e.Row.Cells(0).Text, "Home Team: ", "")
                    FixtureAwayTeam = Replace(e.Row.Cells(4).Text, "Away Team: ", "")
                    e.Row.Font.Bold = True
                    e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
                    e.Row.Cells(0).ColumnSpan = 3
                    e.Row.Cells(1).Visible = False
                    e.Row.Cells(2).Visible = False
                    e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Left
                    e.Row.Cells(4).ColumnSpan = 3
                    e.Row.Cells(5).Visible = False
                    e.Row.Cells(6).Visible = False
                    e.Row.Cells(1).BorderStyle = BorderStyle.None
                    e.Row.Cells(2).BorderStyle = BorderStyle.None
                    e.Row.Cells(6).BorderStyle = BorderStyle.None
                Case 2
                    FixtureLeague = IIf(e.Row.Cells(0).Text = "Division: ONE", "DIVISION 1", "DIVISION 2")
                    e.Row.Font.Bold = True
                    e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
                    e.Row.Cells(0).ColumnSpan = 3
                    e.Row.Cells(1).Visible = False
                    e.Row.Cells(2).Visible = False
                    e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Left
                    e.Row.Cells(4).ColumnSpan = 3
                    e.Row.Cells(5).Visible = False
                    e.Row.Cells(6).Visible = False
                    e.Row.Cells(1).BorderStyle = BorderStyle.None
                    e.Row.Cells(2).BorderStyle = BorderStyle.None
                    e.Row.Cells(6).BorderStyle = BorderStyle.None
                Case 3
                    e.Row.Font.Bold = True
                    e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Center
                Case 16, 17
                    e.Row.Font.Bold = True
                    e.Row.Cells(0).ColumnSpan = 2
                    e.Row.Cells(1).Visible = False
                    e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(5).ColumnSpan = 2
                    e.Row.Cells(6).Visible = False
                    e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Left
                    If gRow = 16 Then
                        HomePlayersTotal = Val(e.Row.Cells(2).Text)
                        AwayPlayersTotal = Val(e.Row.Cells(4).Text)
                    End If
                Case 4 To 15
                    If gRow = 4 Or gRow = 5 Or gRow = 11 Or gRow = 12 Or gRow = 14 Or gRow = 15 Then
                        e.Row.Cells(2).Font.Bold = True
                        e.Row.Cells(4).Font.Bold = True
                    End If
                    If gRow >= 6 And gRow <= 10 Then
                        e.Row.Cells(3).Font.Bold = True
                    End If

            End Select
            gRow = gRow + 1
        Else
            e.Row.Visible = False
        End If
    End Sub
    Protected Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Response.Redirect("~/Mens_Skit/Result Card.aspx?CompID=" & gridResult.Rows(0).Cells(6).Text)
    End Sub
    Protected Sub chkRefresh_CheckedChanged(sender As Object, e As EventArgs) Handles chkRefresh.CheckedChanged
        Dim strIPAddress As String = GetIPAddress()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        If chkRefresh.Checked Then
            strSQL = "EXEC mens_skit.sp_update_result_refresh_flag '" & strIPAddress & "',1"
            myDataReader = objGlobals.SQLSelect(strSQL)
            TimerRefresh.Enabled = True
            TimerRefresh_Tick(Me, e)
        Else
            strSQL = "EXEC mens_skit.sp_update_result_refresh_flag '" & strIPAddress & "',0"
            myDataReader = objGlobals.SQLSelect(strSQL)
            TimerRefresh.Enabled = False
        End If

        objGlobals.close_connection()

        load_week_results(Val(Mid(ddWeeks.Text, 6, 2)))

    End Sub
    Protected Sub btnWeekFixtures_Click(sender As Object, e As EventArgs) Handles btnWeekFixtures.Click
        gridVenues.Visible = False
        lblNoOutstanding.Visible = False
        lblVenueInstruction.Visible = False
        btnWeekFixtures.BackColor = DarkBlue
        btnVenueFixtures.BackColor = Black
        btnOutstanding.BackColor = Black
        CurrentWeek = Val(Mid(ddWeeks.Text, 6, 2))
        Call load_week_results(CurrentWeek)
    End Sub

    Private Sub btnVenueFixtures_Click(sender As Object, e As EventArgs) Handles btnVenueFixtures.Click
        lblNoOutstanding.Visible = False
        lblVenueInstruction.Visible = True
        btnWeekFixtures.BackColor = Black
        btnVenueFixtures.BackColor = DarkBlue
        btnOutstanding.BackColor = Black
        Call load_venue_fixtures()
    End Sub
    Sub load_venue_fixtures()
        gridResults.Visible = False
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        dt = New DataTable
        dr = dt.NewRow

        dt.Columns.Add(New DataColumn("Venue", GetType(System.String)))
        dt.Columns.Add(New DataColumn("mon", GetType(System.String)))
        dt.Columns.Add(New DataColumn("tue", GetType(System.String)))
        dt.Columns.Add(New DataColumn("wed", GetType(System.String)))
        dt.Columns.Add(New DataColumn("thu", GetType(System.String)))
        dt.Columns.Add(New DataColumn("fri", GetType(System.String)))

        CurrentWeek = Val(Mid(ddWeeks.Text, 6, 2))
        strSQL = "EXEC all_skit.sp_get_venues_summary '" & objGlobals.get_current_season & "'," & CurrentWeek.ToString

        myDataReader = objGlobals.SQLSelect(strSQL)
        gridVenues.Visible = True
        gridVenues.DataSource = myDataReader
        gridVenues.DataBind()
    End Sub

    Private Sub gridVenues_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridVenues.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            e.Row.Cells(0).ForeColor = Drawing.Color.Cyan
            If e.Row.Cells(0).Text = "Venue" Then
                For i As Integer = 0 To 5
                    e.Row.Cells(i).ForeColor = Drawing.Color.Tan
                    e.Row.Cells(i).Font.Bold = True
                    e.Row.Cells(i).Font.Size = 10
                Next
            End If
            If e.Row.Cells(1).Text.StartsWith("AVAILABLE") Then e.Row.Cells(1).ForeColor = Drawing.Color.Gold
            If e.Row.Cells(2).Text.StartsWith("AVAILABLE") Then e.Row.Cells(2).ForeColor = Drawing.Color.Gold
            If e.Row.Cells(3).Text.StartsWith("AVAILABLE") Then e.Row.Cells(3).ForeColor = Drawing.Color.Gold
            If e.Row.Cells(4).Text.StartsWith("AVAILABLE") Then e.Row.Cells(4).ForeColor = Drawing.Color.Gold
            If e.Row.Cells(5).Text.StartsWith("AVAILABLE") Then e.Row.Cells(5).ForeColor = Drawing.Color.Gold
        End If
    End Sub
End Class
