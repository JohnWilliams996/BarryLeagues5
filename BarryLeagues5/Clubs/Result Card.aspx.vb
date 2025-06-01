Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
Partial Class Clubs_ResultCard
    Inherits System.Web.UI.Page
    Private dt As DataTable
    Private dr As DataRow
    Private gRow As Integer
    Private objGlobals As New Globals
    Private HomePlayersTotal As Integer
    Private AwayPlayersTotal As Integer
    Private HomePoints As Double
    Private AwayPoints As Double
    Private HomePointsDeducted As Integer
    Private AwayPointsDeducted As Integer
    Private TotalHome As Integer
    Private TotalAway As Integer
    Private FixtureStatus As Integer
    Private FixtureDate As String
    Private FixtureFullDate As String
    Private FixtureWeek As Integer
    Private FixtureLeague As String
    Private FixtureDetail As Boolean
    Private FixtureHomeTeam As String
    Private FixtureAwayTeam As String
    Private Result As String = ""
    Private home_result As String = ""
    Private Shared PrevPage As String = String.Empty

    Private CompID As Integer
    Private Week As Integer
    Private FixtureType As String = "League"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        objGlobals.CurrentSchema = "clubs."
        If Not IsPostBack Then Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
        'btnClose.Attributes.Add("onClick", "javascript:history.back(); return false;")
        CompID = Request.QueryString("CompID")
        If Not IsPostBack Then
            If Request.UrlReferrer Is Nothing Then
                PrevPage = "~/Clubs/Default.aspx"
            Else
                PrevPage = Request.UrlReferrer.ToString()
            End If

            rbView.Items.Add("Landscape")
            rbView.Items.Add("Portrait")
            rbView.SelectedIndex = 0
        End If
        objGlobals.CurrentSchema = "clubs."
        objGlobals.LeagueSelected = Request.QueryString("League")
        objGlobals.TeamSelected = Request.QueryString("Team")
        objGlobals.PlayerSelected = Request.QueryString("Player")
        Call load_fixture_result()
        Select Case Left(FixtureLeague, 4)
            Case "SKIT"
                Call load_skittles_header()
                Call load_skittles_result()
                Call load_skittles_totals()
                gridSkittlesResult.Visible = True
            Case "CRIB"
                Call load_crib_header()
                Call load_crib_result()
                Call load_crib_totals()
                gridCribResult.Visible = True
            Case "SNOO"
                Call load_snooker_header()
                Call load_snooker_result()
                Call load_snooker_totals()
                gridSnookerResult.Visible = True
        End Select

        If FixtureStatus = 2 Then
            Call show_comments()
            imgCard.ImageUrl = "~/Clubs/ResultsCards/" & CompID.ToString & ".jpg"
        End If
    End Sub
    Sub show_comments()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        strSQL = "SELECT comments FROM clubs.fixtures_comments WHERE season = '" & objGlobals.current_season & "' AND fixture_id = " & CompID.ToString
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            lblComments.Text = myDataReader.Item("comments")
        End While
    End Sub
    Function get_fixture_week() As Integer
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        strSQL = "SELECT week_number FROM clubs.vw_fixtures WHERE fixture_id=" & CompID
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            get_fixture_week = myDataReader.Item("week_number")
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
            FixtureStatus = myDataReader.Item("status")
            btnClose.Visible = True
            If myDataReader.Item("status") < 2 Then
                lblNoCard.Visible = True
                lblComments.Visible = False
            Else
                lblNoCard.Visible = False
                lblComments.Visible = True
            End If

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
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

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
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

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

        gRow = 0
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
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

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

        gRow = 0
        gridSnookerResult.Visible = True
        gridSnookerResult.DataSource = dt
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
    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Response.Redirect(PrevPage)
    End Sub
    Private Sub rbView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbView.SelectedIndexChanged
        Select Case rbView.SelectedIndex
            Case 0
                imgCard.Width = 700
                imgCard.Height = 450
            Case 1
                imgCard.Width = 450
                Select Case Left(FixtureLeague, 4)
                    Case "SKIT"
                        imgCard.Height = 600
                    Case Else
                        imgCard.Height = 555
                End Select
        End Select
    End Sub
End Class
