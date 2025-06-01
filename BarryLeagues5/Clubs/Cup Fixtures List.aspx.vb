Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
Imports System.Net
'Imports MySql.Data.MySqlClient

Partial Class Cup_Fixtures_List
    Inherits System.Web.UI.Page
    Private dt As DataTable
    Private dr As DataRow
    Private gRow As Integer
    Private CompName As String
    Private CompSelected As String
    Private CompSelected2 As String
    Private objGlobals As New Globals
    Private PlayerRequired As Boolean
    Private PrelimRound As Boolean = False
    Private MaxRound As Integer
    Private RoundName(6) As String
    Private NotToBePlayed As Boolean
    Private NotToBePlayed_Row As Integer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("clubs")
        objGlobals.CurrentUser = "clubs_user" : objGlobals.CurrentSchema = "clubs."

        Dim l As Integer
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)

        NotToBePlayed = False
        NotToBePlayed_Row = 999
        CompSelected = Request.QueryString("Comp")
        objGlobals.LeagueSelected = Request.QueryString("League")
        l = Len(CompSelected)
        CompName = Mid(CompSelected, 7, l - 6)
        CompSelected2 = objGlobals.LeagueSelected & " - " & CompName
        CompSelected2 = Replace(CompSelected2, " KO", "")
        If CompName = "TEAM KO" Or CompName = "6-A-SIDE TEAM KO" Or CompName = "12-A-SIDE TEAM KO" Then
            PlayerRequired = False
        Else
            PlayerRequired = True
        End If
        CompName = CompSelected2

        '9.4.14 - get rid of 'draw' view
        'btnDrawView.PostBackUrl = "~/Clubs/Cup Fixtures" & CupPage(CompSelected) & ".aspx?League=" & objGlobals.LeagueSelected & "&Comp=" & CompSelected

        'btnListView.PostBackUrl = "~/Clubs/Cup Fixtures List.aspx?League=" & objGlobals.LeagueSelected & "&Comp=" & CompSelected

        Call load_options(gridOptions)
        Call show_comp_name()
        If CupPage(CompSelected) = "1" Then     '24.9.19 only show the draw if the cup_page_on_web flag is set
            If PlayerRequired Then
                Call load_results_player()
            Else
                Call load_results_team()
            End If
        Else
            btnPDF.Visible = False
            lblNoDraw.Visible = True
        End If
    End Sub

    Private Sub show_comp_name()
        Dim labelColour As System.Drawing.Color
        Select Case Left(objGlobals.LeagueSelected, 4)
            Case "CRIB"
                labelColour = Yellow
            Case "SNOO"
                labelColour = LightGreen
            Case "SKIT"
                labelColour = LightBlue
        End Select

        lblCompName.Visible = True
        lblCompName.Text = CompSelected2 & " KNOCKOUT CUP"
        lblCompName.ForeColor = labelColour
    End Sub

    Sub load_options(ByRef inGrid As GridView)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        strSQL = "SELECT long_name,home_night,venue FROM " & objGlobals.CurrentSchema & "vw_teams WHERE league = '" & objGlobals.LeagueSelected & "' AND long_name <> 'BYE' ORDER BY long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)

        dt = New DataTable

        dt.Columns.Add(New DataColumn("Long Name", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Night", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Venue", GetType(System.String)))
        dt.Rows.Add("LEAGUE TABLE")
        dt.Rows.Add("FULL LEAGUE FIXTURES")
        dt.Rows.Add("Team", "Home Night", "Venue")
        While myDataReader.Read()
            With inGrid
                dr = dt.NewRow
                dr("Long Name") = myDataReader.Item("long_name")
                dr("Home Night") = myDataReader.Item("home_night")
                dr("Venue") = myDataReader.Item("venue")
                dt.Rows.Add(dr)
            End With
        End While
        objGlobals.close_connection()

        dr = dt.NewRow
        dr("Long Name") = "Competitions"
        dt.Rows.Add(dr)
        Select Case Left(objGlobals.LeagueSelected, 4)
            Case "CRIB"
                dt.Rows.Add("CUP - TEAM KO")
                dt.Rows.Add("CUP - PAIRS")
            Case "SKIT"
                dt.Rows.Add("CUP - 12-A-SIDE TEAM KO")
                dt.Rows.Add("CUP - 6-A-SIDE TEAM KO")
            Case "SNOO"
                dt.Rows.Add("CUP - TEAM KO")
                dt.Rows.Add("CUP - SINGLES")
                dt.Rows.Add("CUP - DOUBLES")
                dt.Rows.Add("CUP - 3-A-SIDE")
        End Select
        gRow = 0
        inGrid.DataSource = dt
        inGrid.DataBind()

        lblLibs.Visible = False
        If Left(objGlobals.LeagueSelected, 4) <> "SKIT" Then
            inGrid.Columns(2).Visible = False
            If Left(objGlobals.LeagueSelected, 4) = "SNOO" And PlayerRequired Then
                lblLibs.Visible = True
            End If
        End If
    End Sub

    Private Sub gridOptions_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridOptions.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hLink As New HyperLink
            If dt.Rows(gRow)(0).ToString = "LEAGUE TABLE" Then
                hLink.NavigateUrl = "~/Clubs/League Tables.aspx?League=" & objGlobals.LeagueSelected
                e.Row.Cells(0).ColumnSpan = 2
                e.Row.Cells(1).Visible = False
            ElseIf Left(dt.Rows(gRow)(0).ToString, 3) = "CUP" Then
                hLink.NavigateUrl = "~/Clubs/Cup Fixtures List.aspx?League=" & objGlobals.LeagueSelected & "&Comp=" & dt.Rows(gRow)(0).ToString
                e.Row.Cells(0).ColumnSpan = 2
                e.Row.Cells(1).Visible = False
            ElseIf Left(dt.Rows(gRow)(0).ToString, 4) = "FULL" Then
                hLink.NavigateUrl = "~/Clubs/League Fixtures.aspx?League=" & objGlobals.LeagueSelected
                e.Row.Cells(0).ColumnSpan = 2
                e.Row.Cells(1).Visible = False
            Else
                If dt.Rows(gRow)(0) = "Team" Or dt.Rows(gRow)(0) = "Competitions" Then
                    e.Row.Cells(0).Font.Size = 10
                    e.Row.Cells(0).ForeColor = Gray
                    e.Row.Cells(1).Font.Size = 10
                    e.Row.Cells(1).ForeColor = Gray
                    e.Row.Cells(2).Font.Size = 10
                    e.Row.Cells(2).ForeColor = Gray
                Else
                    e.Row.Cells(1).Font.Size = 10
                    e.Row.Cells(2).Font.Size = 10
                End If
                hLink.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(0).ToString
            End If
            If e.Row.Cells(0).Text <> "Team" And dt.Rows(gRow)(0) <> "Competitions" Then
                hLink.Text = e.Row.Cells(0).Text
                hLink.ForeColor = Cyan
                e.Row.Cells(0).Controls.Add(hLink)
                e.Row.CssClass = "cell"
            End If
            gRow = gRow + 1
        End If
    End Sub

    Private Function CupPage(inComp As String) As String
        inComp = Replace(inComp, "CUP", "")
        inComp = Replace(inComp, "KO", "")
        CupPage = ""
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        strSQL = "SELECT cup_page_on_web FROM " & objGlobals.CurrentSchema & "vw_comps_web WHERE Competition = '" & objGlobals.LeagueSelected & inComp & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            Return CStr(myDataReader.Item("cup_page_on_web"))
        End While
        objGlobals.close_connection()

    End Function

    Sub check_prelim_max_round()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim i As Integer
        MaxRound = 0
        RoundName(0) = ""
        PrelimRound = False


        strSQL = "SELECT TOP 1 played_by FROM " & objGlobals.CurrentSchema & "vw_prelim WHERE Competition = '" & CompName & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            PrelimRound = True
            RoundName(0) = "Prelim Round - " + myDataReader.Item(0)
        End While
        objGlobals.close_connection()

        If PrelimRound Then 'we have a prelim round, but do we want to show it yet ? (before start of season)
            strSQL = "SELECT prelim_draw_on_web FROM " & objGlobals.CurrentSchema & "vw_comps_web WHERE Competition = '" & CompName & "' AND prelim_draw_on_web = 0"
            myDataReader = objGlobals.SQLSelect(strSQL)
            While myDataReader.Read()
                PrelimRound = False
            End While
            objGlobals.close_connection()
        End If

        strSQL = "SELECT MAX(round) FROM " & objGlobals.CurrentSchema & "vw_draws WHERE Competition = '" & CompName & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            If IsDBNull(myDataReader.Item(0)) Then
                'RoundName(1) = "1st Round Draw Yet to be Made"
                Exit Sub
            Else
                MaxRound = myDataReader.Item(0)
            End If
        End While
        objGlobals.close_connection()

        'store the dates for each round
        strSQL = "SELECT round_1,round_2,round_3,quarter_finals,semi_finals,final FROM " & objGlobals.CurrentSchema & "vw_comps_web WHERE Competition = '" & CompName & "' AND main_draw_on_web = 1"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If myDataReader.HasRows Then
            While myDataReader.Read()
                If myDataReader.Item(0) <> "n/a" Then
                    i = i + 1
                    RoundName(i) = "1st Round - " + myDataReader.Item(0)
                End If
                If myDataReader.Item(1) <> "n/a" Then
                    i = i + 1
                    RoundName(i) = "2nd Round - " + myDataReader.Item(1)
                End If
                If myDataReader.Item(2) <> "n/a" Then
                    i = i + 1
                    RoundName(i) = "3rd Round - " + myDataReader.Item(2)
                End If
                If myDataReader.Item(3) <> "n/a" Then
                    i = i + 1
                    RoundName(i) = "Quarter-Finals - " + myDataReader.Item(3)
                End If
                If myDataReader.Item(4) <> "n/a" Then
                    i = i + 1
                    RoundName(i) = "Semi-Finals - " + myDataReader.Item(4)
                End If
                If myDataReader.Item(5) <> "n/a" Then
                    i = i + 1
                    RoundName(i) = "Final - " + myDataReader.Item(5)
                End If
            End While
        Else
            MaxRound = 0
        End If

        objGlobals.close_connection()

    End Sub

    Private Sub load_results_team()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim LastRound As Integer = 0
        Dim PrelimCount As Integer = 0
        dt = New DataTable

        'add header row
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Match", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Date", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Venue", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Winner", GetType(System.String)))

        strSQL = "EXEC clubs.sp_get_draw '" & objGlobals.current_season & "','" & CompName & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        gridResults_Team.DataSource = myDataReader
        gridResults_Team.DataBind()

        If gridResults_Team.Rows.Count = 3 Then
            lblNoDraw.Visible = True
            gridResults_Team.Visible = False
        Else
            lblNoDraw.Visible = False
        End If
    End Sub

    Sub load_results_player()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim LastRound As Integer = 0
        Dim PrelimCount As Integer = 0
        dt = New DataTable

        'add header row
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("MatchNo", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Outcome", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("HomeDraw", GetType(System.String)))
        dt.Columns.Add(New DataColumn("AwayDraw", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Info", GetType(System.String)))
        'btnDrawView.Visible = True     9.4.14 - get rid of 'draw' view
        Call check_prelim_max_round()
        gridResults_Player.Columns(6).Visible = True
        gridResults_Player.Columns(7).Visible = True
        gridResults_Player.Columns(8).Visible = True
        gridResults_Player.Columns(9).Visible = True

        If MaxRound <> 0 Or PrelimRound Then      ' has the draw been created ?
            While (Not PrelimRound And LastRound < MaxRound) Or (PrelimRound And LastRound <= MaxRound)
                If Not PrelimRound Then
                    strSQL = "SELECT match_no,a.player,a.team,b.player,b.team,result,c.home_draw,c.away_draw,round,info"
                    strSQL = strSQL & " FROM " & objGlobals.CurrentSchema & "vw_draws c"
                    strSQL = strSQL & " LEFT OUTER JOIN " & objGlobals.CurrentSchema & "vw_entries a ON a.competition = c.competition AND a.draw_no = RIGHT(c.home_draw,2)"
                    strSQL = strSQL & " LEFT OUTER JOIN " & objGlobals.CurrentSchema & "vw_entries b ON b.competition = c.competition AND b.draw_no = RIGHT(c.away_draw,2)"
                    strSQL = strSQL & " WHERE c.competition = '" & CompName & "'"
                    strSQL = strSQL & " ORDER BY match_no"
                Else
                    strSQL = "SELECT match_no,home_player,home_team,away_player,away_team,result,info FROM " & objGlobals.CurrentSchema & "vw_prelim WHERE competition = '" & CompName & "' ORDER BY match_no"
                End If

                myDataReader = objGlobals.SQLSelect(strSQL)
                While myDataReader.Read()
                    With gridResults_Player
                        dr = dt.NewRow
                        If Not PrelimRound Then
                            If myDataReader.Item("round") > LastRound Then
                                LastRound = myDataReader.Item("round")
                                dr("MatchNo") = ""
                                dr("Home Player") = ""
                                dr("Home Team") = ""
                                dr("Away Player") = ""
                                dr("Away Team") = ""
                                dt.Rows.Add(dr)
                                dr = dt.NewRow

                                dr("MatchNo") = ""
                                If Not PlayerRequired Then
                                    dr("Home Player") = ""
                                    dr("Home Team") = RoundName(LastRound)
                                Else
                                    dr("Home Player") = RoundName(LastRound)
                                    dr("Home Team") = ""
                                End If
                                dr("Away Player") = ""
                                dr("Away Team") = ""
                                dt.Rows.Add(dr)
                                dr = dt.NewRow
                            End If
                        Else
                            If PrelimCount = 0 Then
                                dr("MatchNo") = ""
                                dr("Home Player") = ""
                                dr("Home Team") = ""
                                dr("Away Player") = ""
                                dr("Away Team") = ""
                                dt.Rows.Add(dr)
                                dr = dt.NewRow

                                dr("MatchNo") = ""
                                If Not PlayerRequired Then
                                    dr("Home Player") = ""
                                    dr("Home Team") = RoundName(0)
                                Else
                                    dr("Home Player") = RoundName(0)
                                    dr("Home Team") = ""
                                End If
                                dr("Away Player") = ""
                                dr("Away Team") = ""
                                dt.Rows.Add(dr)
                                dr = dt.NewRow
                                PrelimCount = PrelimCount + 1
                            End If
                        End If
                        If Not PrelimRound Then
                            dr("MatchNo") = myDataReader.Item(0)
                            If Left(myDataReader.Item(6), 1) <> "M" Then
                                If myDataReader.Item(6) = " 99" Then
                                    dr("Home Player") = "Prev Match Not Played"
                                    dr("Home Team") = "Prev Match Not Played"
                                Else
                                    dr("Home Player") = myDataReader.Item(1)
                                    dr("Home Team") = myDataReader.Item(2)
                                End If
                            Else
                                dr("Home Team") = "Match " & Val(Right(myDataReader.Item(6), 2)) & " Winner"
                            End If

                            If Left(myDataReader.Item(7), 1) <> "M" Then
                                If myDataReader.Item(7) = " 99" Then
                                    dr("Away Player") = "Prev Match Not Played"
                                    dr("Away Team") = "Prev Match Not Played"
                                Else
                                    dr("Away Player") = myDataReader.Item(3)
                                    dr("Away Team") = myDataReader.Item(4)
                                End If
                            Else
                                If PlayerRequired Then
                                    dr("Away Player") = "Match " & Val(Right(myDataReader.Item(7), 2)) & " Winner"
                                Else
                                    dr("Away Team") = "Match " & Val(Right(myDataReader.Item(7), 2)) & " Winner"
                                End If
                            End If
                            dr("HomeDraw") = myDataReader.Item(6)
                            dr("AwayDraw") = myDataReader.Item(7)
                            dr("Info") = myDataReader.Item(9)
                        Else
                            dr("MatchNo") = "P" + PrelimCount.ToString
                            dr("Home Player") = myDataReader.Item(1)
                            dr("Home Team") = myDataReader.Item(2)
                            dr("Away Player") = myDataReader.Item(3)
                            dr("Away Team") = myDataReader.Item(4)
                            dr("Info") = myDataReader.Item(6)
                        End If

                        dr("Result") = myDataReader.Item(5)
                        dt.Rows.Add(dr)
                        If PrelimRound Then
                            PrelimCount = PrelimCount + 1
                            If MaxRound = 0 Then                ' No main draw, just prelim
                                LastRound = MaxRound + 1        ' force end of draw
                                'btnListView.Visible = False    9.4.14 - get rid of 'draw' view
                                'btnDrawView.Visible = False    9.4.14 - get rid of 'draw' view
                            End If
                        End If
                    End With
                End While
                PrelimRound = False
            End While
            objGlobals.close_connection()

            lblNoDraw.Visible = False
            gridResults_Player.DataSource = dt
            gridResults_Player.DataBind()
            Call colour_grid_player()
        Else
            'btnListView.Visible = False    9.4.14 - get rid of 'draw' view
            'btnDrawView.Visible = False    9.4.14 - get rid of 'draw' view
            lblNoDraw.Visible = True
        End If
    End Sub

    Sub colour_grid_player()
        Dim iRow As Integer = 0
        With gridResults_Player
            If Not PlayerRequired Then
                .HeaderRow.Cells(1).Text = ""
                .HeaderRow.Cells(4).Text = ""
            End If

            For iRow = 0 To .Rows.Count - 1
                .Font.Size = 10
                Select Case .Rows(iRow).Cells(6).Text
                    Case "X"
                        If iRow < NotToBePlayed_Row Then
                            If Left(.Rows(iRow).Cells(2).Text, 5) <> "match" Then
                                .Rows(iRow).Cells(1).ForeColor = White
                                .Rows(iRow).Cells(2).ForeColor = Cyan
                            Else
                                .Rows(iRow).Cells(1).ForeColor = Red
                                .Rows(iRow).Cells(2).ForeColor = Red
                            End If
                            .Rows(iRow).Cells(3).Text = "versus"
                            .Rows(iRow).Cells(3).ForeColor = System.Drawing.Color.FromArgb(&H0, &HCC, &H66)

                            If Left(.Rows(iRow).Cells(4).Text, 5) <> "match" And Left(.Rows(iRow).Cells(5).Text, 5) <> "match" Then
                                .Rows(iRow).Cells(4).ForeColor = White
                                .Rows(iRow).Cells(5).ForeColor = Cyan
                            Else
                                If PlayerRequired Then
                                    .Rows(iRow).Cells(4).ForeColor = Red
                                Else
                                    .Rows(iRow).Cells(5).ForeColor = Red
                                End If
                            End If
                        Else
                            .Rows(iRow).Cells(3).Text = "versus"
                            If Left(.Rows(iRow).Cells(2).Text, 5) <> "Match" Then
                                .Rows(iRow).Cells(1).ForeColor = White
                                .Rows(iRow).Cells(2).ForeColor = Cyan
                                .Rows(iRow).Cells(3).ForeColor = System.Drawing.Color.FromArgb(&H0, &HCC, &H66)
                            Else
                                .Rows(iRow).Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                                .Rows(iRow).Cells(2).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                                .Rows(iRow).Cells(3).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                                .Rows(iRow).Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                                .Rows(iRow).Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                            End If

                            If Left(.Rows(iRow).Cells(4).Text, 5) <> "Match" And Left(.Rows(iRow).Cells(5).Text, 5) <> "Match" Then
                                .Rows(iRow).Cells(4).ForeColor = White
                                .Rows(iRow).Cells(5).ForeColor = Cyan
                            Else
                                .Rows(iRow).Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                                .Rows(iRow).Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                            End If
                            .Rows(iRow).Cells(1).BackColor = System.Drawing.Color.FromArgb(83, 0, 0)      ' dark red
                            .Rows(iRow).Cells(2).BackColor = System.Drawing.Color.FromArgb(83, 0, 0)      ' dark red
                            .Rows(iRow).Cells(3).BackColor = System.Drawing.Color.FromArgb(83, 0, 0)      ' dark red
                            .Rows(iRow).Cells(4).BackColor = System.Drawing.Color.FromArgb(83, 0, 0)      ' dark red
                            .Rows(iRow).Cells(5).BackColor = System.Drawing.Color.FromArgb(83, 0, 0)      ' dark red
                        End If
                    Case "N"
                        .Rows(iRow).Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(2).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(3).Text = "not played"
                        .Rows(iRow).Cells(3).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    Case "H"
                        .Rows(iRow).Cells(1).ForeColor = White
                        .Rows(iRow).Cells(2).ForeColor = Cyan
                        .Rows(iRow).Cells(3).Text = "< winner"
                        .Rows(iRow).Cells(3).ForeColor = Red
                        .Rows(iRow).Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    Case "A"
                        .Rows(iRow).Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(2).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(3).Text = "winner >"
                        .Rows(iRow).Cells(3).ForeColor = Red
                        .Rows(iRow).Cells(4).ForeColor = White
                        .Rows(iRow).Cells(5).ForeColor = Cyan
                End Select
                If Not PlayerRequired Then
                    .Rows(iRow).Cells(1).Text = ""
                    .Rows(iRow).Cells(4).Text = ""
                End If
                If InStr(CompName, "3-A-SIDE") > 0 And .Rows(iRow).Cells(3).Text <> "&nbsp;" Then
                    .Rows(iRow).Cells(1).Wrap = True
                    .Rows(iRow).Cells(4).Wrap = True
                    .Rows(iRow).Cells(1).Width = 200
                    .Rows(iRow).Cells(4).Width = 200
                End If
            Next
            .Columns(6).Visible = False
            .Columns(7).Visible = False
            .Columns(8).Visible = False
        End With
    End Sub

    Protected Sub gridResults_Player_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResults_Player.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If InStr(e.Row.Cells(1).Text, "Round") > 0 Or InStr(e.Row.Cells(1).Text, "Final") > 0 Or
                InStr(e.Row.Cells(2).Text, "Round") > 0 Or InStr(e.Row.Cells(2).Text, "Final") > 0 Then
                If InStr(e.Row.Cells(1).Text, "NOT TO BE PLAYED") > 0 Or InStr(e.Row.Cells(2).Text, "NOT TO BE PLAYED") > 0 Or NotToBePlayed = True Then
                    If Not NotToBePlayed Then
                        NotToBePlayed = True
                        NotToBePlayed_Row = e.Row.RowIndex
                    End If
                    e.Row.Cells(1).BackColor = System.Drawing.Color.FromArgb(83, 0, 0)      ' dark red
                    e.Row.Cells(2).BackColor = System.Drawing.Color.FromArgb(83, 0, 0)      ' dark red
                Else
                    e.Row.BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                End If
                If PlayerRequired Then
                    If InStr(e.Row.Cells(1).Text, "NOT TO BE PLAYED") > 0 Or NotToBePlayed = True Then
                        If Not NotToBePlayed Then
                            NotToBePlayed = True
                            NotToBePlayed_Row = e.Row.RowIndex
                        End If
                    End If
                    e.Row.Cells(1).ForeColor = System.Drawing.Color.FromArgb(&HE4, &HBB, &H18)
                    e.Row.Cells(1).Font.Size = 12
                    e.Row.Cells(1).ColumnSpan = 4
                Else
                    If InStr(e.Row.Cells(2).Text, "NOT TO BE PLAYED") > 0 Or NotToBePlayed = True Then
                        If Not NotToBePlayed Then
                            NotToBePlayed = True
                            NotToBePlayed_Row = e.Row.RowIndex
                        End If
                    End If
                    e.Row.Cells(2).ForeColor = System.Drawing.Color.FromArgb(&HE4, &HBB, &H18)
                    e.Row.Cells(2).Font.Size = 12
                    e.Row.Cells(2).ColumnSpan = 4
                End If
            End If
        End If
    End Sub

    Protected Sub gridResults_Team_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResults_Team.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If InStr(e.Row.Cells(1).Text, "Round") > 0 Or InStr(e.Row.Cells(1).Text, "Final") > 0 Or
                InStr(e.Row.Cells(2).Text, "Round") > 0 Or InStr(e.Row.Cells(2).Text, "Final") > 0 Then
                e.Row.Cells(1).Text = e.Row.Cells(1).Text & " - " & e.Row.Cells(2).Text
                e.Row.Cells(2).Text = ""
                e.Row.Cells(6).Text = ""
                e.Row.BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                e.Row.Cells(1).ForeColor = Tan
                e.Row.Cells(1).Font.Size = 12
                e.Row.Cells(2).ForeColor = Tan
                e.Row.Cells(1).Font.Size = 12
                e.Row.Cells(1).ColumnSpan = 4
                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Left
            Else
                e.Row.Cells(1).ColumnSpan = 1
                Select Case e.Row.Cells(2).Text
                    Case "versus"
                        e.Row.Cells(2).ForeColor = System.Drawing.Color.FromArgb(&H0, &HCC, &H66)
                    Case "not played"
                        e.Row.Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(2).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(3).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    Case "&lt; winner"
                        e.Row.Cells(2).ForeColor = Red
                        e.Row.Cells(3).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    Case "winner &gt;"
                        e.Row.Cells(2).ForeColor = Red
                        e.Row.Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    Case Else   'result entered ?
                        If e.Row.Cells(6).Text = "H" Then
                            e.Row.Cells(2).ForeColor = Red
                            e.Row.Cells(3).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                            e.Row.Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                            e.Row.Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        ElseIf e.Row.Cells(6).Text = "A" Then
                            e.Row.Cells(2).ForeColor = Red
                            e.Row.Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                            e.Row.Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                            e.Row.Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        End If
                End Select
                If Left(e.Row.Cells(1).Text, 5) = "Match" Then
                    e.Row.Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    e.Row.Cells(2).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    e.Row.Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    e.Row.Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                End If
                If Left(e.Row.Cells(3).Text, 5) = "Match" Then
                    e.Row.Cells(2).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    e.Row.Cells(3).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    e.Row.Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    e.Row.Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                End If
            End If
        End If
    End Sub

    Sub write_PDF_download(ByVal inFilepath As String)
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim l_param_in_names(2) As String
        Dim l_param_in_values(2) As String

        l_param_in_names(0) = "@inLeague"
        l_param_in_values(0) = objGlobals.LeagueSelected
        l_param_in_names(1) = "@inTeam"
        l_param_in_values(1) = ""
        l_param_in_names(2) = "@inFilepath"
        l_param_in_values(2) = Replace(inFilepath, "'", """")

        strSQL = "EXEC [clubs].[sp_write_download_PDF] '" & l_param_in_values(0) & "','" & l_param_in_values(1) & "','" & l_param_in_values(2) & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        objGlobals.close_connection()
    End Sub


    Protected Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Dim FilePath As String = Server.MapPath("CupDraws") & "\"
        Dim filename As String = CompName & ".pdf"
        Dim PDFfile As String = FilePath + filename

        'write details for PDF_downloads
        write_PDF_download(PDFfile)

        Dim client As New WebClient
        Dim ByteArray As Byte() = client.DownloadData(PDFfile)

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
End Class
