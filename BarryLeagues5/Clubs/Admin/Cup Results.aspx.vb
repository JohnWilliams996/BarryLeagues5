Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
'Imports MySql.Data.MySqlClient

Partial Class Admin_Cup_Results
    Inherits System.Web.UI.Page
    Private dt As DataTable = New DataTable
    Private dr As DataRow = Nothing
    Private gRow As Integer
    Private objGlobals As New Globals
    Private PrelimRound As Boolean = False
    Private PlayerRequired As Boolean
    Private CompName As String
    Private MaxRound As Integer
    Private RoundName(6) As String

    Private HomePlayer(10) As String
    Private HomeTeam(10) As String
    Private HomeLeague(10) As String
    Private HomePlayerCount As Integer

    Private AwayPlayer(10) As String
    Private AwayTeam(10) As String
    Private AwayLeague(10) As String
    Private AwayPlayerCount As Integer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("clubs")
        objGlobals.CurrentUser = "clubs_user"
        objGlobals.CurrentSchema = "clubs."

        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            lbComps.Items.Add("NOT AUTHORIZED")
            gridResults.Visible = False
            objGlobals.AdminLogin = False
            Call objGlobals.store_page("ATTACK FROM " & Request.Url.OriginalString, objGlobals.AdminLogin)
            Exit Sub
        End If
        If Not IsPostBack Then
            Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
            Call load_comps()
        End If
    End Sub

    Sub load_comps()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        lbComps.ClearSelection()
        strSQL = "SELECT Competition FROM clubs.vw_Comps ORDER BY Competition"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            lbComps.Items.Add(myDataReader.Item(0))
        End While
        objGlobals.close_connection()

    End Sub

    Sub check_prelim_max_round()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim i As Integer
        MaxRound = 0
        RoundName(0) = ""
        PrelimRound = False
        strSQL = "SELECT TOP 1 played_by FROM clubs.vw_prelim WHERE Competition = '" & CompName & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            PrelimRound = True
            RoundName(0) = "Prelim Round - " + myDataReader.Item(0)
        End While
        objGlobals.close_connection()

        strSQL = "SELECT MAX(round) FROM clubs.vw_draws WHERE Competition = '" & CompName & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            If IsDBNull(myDataReader.Item(0)) Then
                'MaxRound = 1
                'RoundName(1) = "1st Round Draw Yet to be Made"
                Exit Sub
            Else
                MaxRound = myDataReader.Item(0)
            End If
        End While
        objGlobals.close_connection()

        'store the dates for each round
        strSQL = "SELECT round_1,round_2,round_3,quarter_finals,semi_finals,final FROM clubs.vw_comps_web WHERE Competition = '" & CompName & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
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
        objGlobals.close_connection()

    End Sub

    Sub load_results()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim LastRound As Integer = 0
        Dim PrelimCount As Integer = 0
        dt = New DataTable

        PlayerRequired = True
        If InStr(CompName, "TEAM", CompareMethod.Text) > 0 Then
            PlayerRequired = False
        End If
        If InStr(CompName, "6-A-SIDE", CompareMethod.Text) > 0 Then
            PlayerRequired = False
        End If

        If PlayerRequired Then
            lblPrelim.Visible = True
        Else
            lblPrelim.Visible = False
        End If

        'add header row
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("MatchNo", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("HomeDraw", GetType(System.String)))
        dt.Columns.Add(New DataColumn("AwayDraw", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Info", GetType(System.String)))
        dt.Columns.Add(New DataColumn("RoundNo", GetType(System.String)))

        gridResults.Columns(9).Visible = True
        gridResults.Columns(10).Visible = True
        gridResults.Columns(11).Visible = True
        gridResults.Columns(12).Visible = True

        Call check_prelim_max_round()
        If MaxRound <> 0 Or PrelimRound Then      ' has the draw been created ?
            While (Not PrelimRound And LastRound < MaxRound) Or (PrelimRound And LastRound <= MaxRound)
                If Not PrelimRound Then
                    strSQL = "SELECT match_no,a.player,a.team,b.player,b.team,result,c.home_draw,c.away_draw,round,info"
                    strSQL = strSQL & " FROM clubs.vw_draws c"
                    strSQL = strSQL & " LEFT OUTER JOIN clubs.vw_entries a ON a.competition = c.competition AND a.draw_no = RIGHT(c.home_draw,2)"
                    strSQL = strSQL & " LEFT OUTER JOIN clubs.vw_entries b ON b.competition = c.competition AND b.draw_no = RIGHT(c.away_draw,2)"
                    strSQL = strSQL & " WHERE c.competition = '" & CompName & "'"
                    strSQL = strSQL & " ORDER BY match_no"
                Else
                    strSQL = "SELECT match_no,home_player,home_team,away_player,away_team,result,info FROM clubs.vw_prelim WHERE competition = '" & CompName & "' ORDER BY match_no"
                End If

                myDataReader = objGlobals.SQLSelect(strSQL)
                While myDataReader.Read()
                    With gridResults
                        dr = dt.NewRow
                        If Not PrelimRound Then
                            If myDataReader.Item("round") > LastRound Then
                                LastRound = myDataReader.Item("round")
                                dr("MatchNo") = ""
                                dr("Home Player") = ""
                                dr("Home Team") = ""
                                dr("Away Player") = ""
                                dr("Away Team") = ""
                                dr("RoundNo") = ""
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
                                dr("RoundNo") = ""
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
                                dr("RoundNo") = ""
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
                                dr("RoundNo") = ""
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
                            dr("RoundNo") = myDataReader.Item("round")
                        Else
                            dr("MatchNo") = "P" + PrelimCount.ToString
                            dr("Home Player") = myDataReader.Item(1)
                            dr("Home Team") = myDataReader.Item(2)
                            dr("Away Player") = myDataReader.Item(3)
                            dr("Away Team") = myDataReader.Item(4)
                            dr("Info") = myDataReader.Item(6)
                            dr("RoundNo") = ""
                        End If
                        dr("Result") = myDataReader.Item(5)
                        dt.Rows.Add(dr)
                        If PrelimRound Then
                            PrelimCount = PrelimCount + 1
                            If MaxRound = 0 Then        ' No main draw, just prelim
                                LastRound = MaxRound + 1        ' force end of draw
                            End If
                        End If
                    End With
                End While

                PrelimRound = False
            End While
            objGlobals.close_connection()

            gridResults.DataSource = dt
            gridResults.DataBind()
            Call colour_grid()
        Else
            gridResults.Visible = False
        End If
    End Sub

    Sub colour_grid()

        Dim iRow As Integer = 0
        With gridResults
            .Visible = True
            If Not PlayerRequired Then
                .HeaderRow.Cells(1).Text = ""
                .HeaderRow.Cells(7).Text = ""
            End If
            .Font.Size = 10
            For iRow = 0 To .Rows.Count - 1
                Select Case .Rows(iRow).Cells(9).Text
                    Case "X"
                        If Left(.Rows(iRow).Cells(2).Text, 5) <> "match" Then
                            .Rows(iRow).Cells(1).ForeColor = White
                            .Rows(iRow).Cells(2).ForeColor = Cyan
                        Else
                            .Rows(iRow).Cells(2).ForeColor = Red
                        End If
                        If Left(.Rows(iRow).Cells(7).Text, 5) = "match" Or Left(.Rows(iRow).Cells(8).Text, 5) = "match" Then
                            If PlayerRequired Then
                                .Rows(iRow).Cells(7).ForeColor = Red
                            Else
                                .Rows(iRow).Cells(8).ForeColor = Red
                            End If
                        Else
                            .Rows(iRow).Cells(7).ForeColor = White
                            .Rows(iRow).Cells(8).ForeColor = Cyan
                        End If
                    Case "N"
                        .Rows(iRow).Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(2).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(7).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(8).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    Case "H"
                        .Rows(iRow).Cells(1).ForeColor = White
                        .Rows(iRow).Cells(2).ForeColor = Cyan
                        .Rows(iRow).Cells(3).Text = "< winner"
                        .Rows(iRow).Cells(3).HorizontalAlign = HorizontalAlign.Right
                        .Rows(iRow).Cells(3).ForeColor = Red
                        '.Rows(iRow).Cells(3).ForeColor = System.Drawing.Color.FromArgb(&H0, &HCC, &H66)
                        .Rows(iRow).Cells(7).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(8).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    Case "A"
                        .Rows(iRow).Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(2).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(6).Text = "winner >"
                        .Rows(iRow).Cells(6).ForeColor = Red
                        ' .Rows(iRow).Cells(6).ForeColor = System.Drawing.Color.FromArgb(&H0, &HCC, &H66)
                        .Rows(iRow).Cells(7).ForeColor = White
                        .Rows(iRow).Cells(8).ForeColor = Cyan
                End Select
                If .Rows(iRow).Cells(10).Text = " 99" Then
                    .Rows(iRow).Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    .Rows(iRow).Cells(2).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                End If
                If .Rows(iRow).Cells(11).Text = " 99" Then
                    .Rows(iRow).Cells(7).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    .Rows(iRow).Cells(8).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                End If
                If Not PlayerRequired Then
                    .Rows(iRow).Cells(1).Text = ""
                    .Rows(iRow).Cells(7).Text = ""
                End If
            Next
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .Columns(11).Visible = False
        End With
    End Sub

    Protected Sub gridResults_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResults.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lb1 As Button = DirectCast(e.Row.Cells(3).Controls(0), Button)
            Dim lb2 As Button = DirectCast(e.Row.Cells(4).Controls(0), Button)
            Dim lb3 As Button = DirectCast(e.Row.Cells(5).Controls(0), Button)
            Dim lb4 As Button = DirectCast(e.Row.Cells(6).Controls(0), Button)

            If InStr(e.Row.Cells(1).Text, "Round") > 0 _
                Or InStr(e.Row.Cells(1).Text, "Final") > 0 _
                Or InStr(e.Row.Cells(2).Text, "Round") > 0 _
                Or InStr(e.Row.Cells(2).Text, "Final") > 0 Then
                If InStr(e.Row.Cells(1).Text, "NOT TO BE PLAYED") > 0 Or InStr(e.Row.Cells(2).Text, "NOT TO BE PLAYED") > 0 Then
                    e.Row.BackColor = DarkRed
                Else
                    e.Row.BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                End If
                If PlayerRequired Then
                    If InStr(e.Row.Cells(1).Text, "NOT TO BE PLAYED") > 0 Then
                        e.Row.Cells(1).ForeColor = White
                    Else
                        e.Row.Cells(1).ForeColor = System.Drawing.Color.FromArgb(&HE4, &HBB, &H18)
                    End If
                    e.Row.Cells(1).Font.Size = 12
                    e.Row.Cells(1).Font.Bold = False
                    e.Row.Cells(1).ColumnSpan = 8
                Else
                    If InStr(e.Row.Cells(2).Text, "NOT TO BE PLAYED") > 0 Then
                        e.Row.Cells(2).ForeColor = White
                    Else
                        e.Row.Cells(2).ForeColor = System.Drawing.Color.FromArgb(&HE4, &HBB, &H18)
                    End If
                    e.Row.Cells(2).Font.Size = 12
                    e.Row.Cells(2).Font.Bold = False
                    e.Row.Cells(2).ColumnSpan = 8
                End If
            End If
            lb1.Visible = True
            lb2.Visible = True
            lb3.Visible = True
            lb4.Visible = True
            If (Left(e.Row.Cells(2).Text, 5) = "match") Or (Left(e.Row.Cells(8).Text, 5) = "match") Or (Left(e.Row.Cells(7).Text, 5) = "match") _
                Or InStr(e.Row.Cells(1).Text, "Round") > 0 Or InStr(e.Row.Cells(1).Text, "Final") > 0 _
                Or InStr(e.Row.Cells(2).Text, "Round") > 0 Or InStr(e.Row.Cells(2).Text, "Final") > 0 Then
                lb1.Visible = False
                lb2.Visible = False
                lb3.Visible = False
                lb4.Visible = False
            End If
            If e.Row.Cells(1).Text = "&nbsp;" And e.Row.Cells(2).Text = "&nbsp;" Then
                lb1.Visible = False
                lb2.Visible = False
                lb3.Visible = False
                lb4.Visible = False
            End If
            If Left(e.Row.Cells(2).Text, 3) = "BYE" And Left(e.Row.Cells(8).Text, 3) <> "BYE" Then
                lb1.Visible = False
                lb2.Visible = False
            End If
            If Left(e.Row.Cells(8).Text, 3) = "BYE" And Left(e.Row.Cells(2).Text, 3) <> "BYE" Then
                lb2.Visible = False
                lb4.Visible = False
            End If
            If e.Row.Cells(9).Text = "H" Or Right(e.Row.Cells(10).Text, 2) = "99" Then lb1.Visible = False
            If e.Row.Cells(9).Text = "N" Then lb2.Visible = False
            If e.Row.Cells(9).Text = "X" Then lb3.Visible = False
            If e.Row.Cells(9).Text = "A" Or Right(e.Row.Cells(11).Text, 2) = "99" Then lb4.Visible = False
            If Right(e.Row.Cells(10).Text, 2) = "99" Or Right(e.Row.Cells(11).Text, 2) = "99" Then lb2.Visible = False
        End If
    End Sub

    Protected Sub gridResults_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridResults.RowCommand
        Dim PrelimResult As Boolean
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim selectedRow As GridViewRow = gridResults.Rows(index)
        Dim MatchNoCell As TableCell = selectedRow.Cells(0)
        Dim HomeDrawCell As TableCell = selectedRow.Cells(10)
        Dim AwayDrawCell As TableCell = selectedRow.Cells(11)
        Dim RoundNoCell As TableCell = selectedRow.Cells(13)
        Dim MatchNo As Integer
        If Left(MatchNoCell.Text, 1) = "P" Then
            PrelimResult = True
            If Len(MatchNoCell.Text) = 2 Then
                MatchNo = Val(Right(MatchNoCell.Text, 1))
            Else
                MatchNo = Val(Right(MatchNoCell.Text, 2))
            End If
        Else
            PrelimResult = False
            MatchNo = Val(MatchNoCell.Text)
        End If
        Dim HomeDrawNo As String = HomeDrawCell.Text
        Dim AwayDrawNo As String = AwayDrawCell.Text
        Dim RoundNo As Integer = Val(RoundNoCell.Text)

        CompName = lbComps.Text
        Call reset_result(MatchNo, PrelimResult, RoundNo)
        Select Case e.CommandName
            Case "HomeWin"
                Call update_home_win(MatchNo, HomeDrawNo, PrelimResult, RoundNo)
            Case "AwayWin"
                Call update_away_win(MatchNo, AwayDrawNo, PrelimResult, RoundNo)
            Case "NotPlayed"
                Call update_not_played(MatchNo, PrelimResult, RoundNo)
        End Select

        write_cup_draws_FTP()

        Call load_results()
    End Sub

    Sub write_cup_draws_FTP()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim l_param_in_names(0) As String
        Dim l_param_in_values(0) As String
        l_param_in_names(0) = "@inComp"
        l_param_in_values(0) = CompName

        strSQL = "EXEC [clubs].[sp_write_cup_draws_FTP] '" & l_param_in_values(0) & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        objGlobals.close_connection()
    End Sub

    Sub update_home_win(ByVal MatchNo As Integer, ByVal WinningDrawNo As String, ByVal PrelimResult As Boolean, ByVal RoundNo As Integer)
        Dim strSQL As String = ""
        Dim myDataReader As oledbdatareader

        If PrelimResult Then
            strSQL = "UPDATE clubs.prelim SET result = 'H' WHERE competition = '" & CompName & "' AND match_no = " & MatchNo
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            strSQL = "EXEC clubs.sp_update_comp_result '" & objGlobals.get_current_season & "','" & CompName & "','P" & MatchNo.ToString & "','H',0"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            '19.11.18 - update fixture_combined table
            Call objGlobals.update_fixtures_combined("clubs")
            objGlobals.close_connection()

            Exit Sub
        End If

        '02.08.23 - each round is now randomly drawn, so no need to update the next round
        'Dim ThisMatch As String = "M" & Format(MatchNo, "00")
        'Select Case MatchNo Mod 2
        '    Case 1      ' match number is ODD, so their next match will be home
        '        strSQL = "UPDATE clubs.vw_draws SET home_draw = '" & WinningDrawNo & "',prev_home_draw1 = '" & ThisMatch & "'"
        '        strSQL = strSQL & ",result = 'X' WHERE competition = '" & CompName & "' AND home_draw = '" & ThisMatch & "'"
        '    Case 0      ' match number is EVEN, so their next match will be away
        '        strSQL = "UPDATE clubs.vw_draws SET away_draw = '" & WinningDrawNo & "',prev_home_draw2 = '" & ThisMatch & "'"
        '        strSQL = strSQL & ",result = 'X' WHERE competition = '" & CompName & "' AND away_draw = '" & ThisMatch & "'"
        'End Select
        'myDataReader = objGlobals.SQLSelect(strSQL)
        'objGlobals.close_connection()

        ' update current match
        strSQL = "UPDATE clubs.vw_draws SET result = 'H' WHERE Competition = '" & CompName & "' AND match_no = '" & MatchNo & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

        If Not PlayerRequired Then
            strSQL = "EXEC clubs.sp_update_comp_result '" & objGlobals.get_current_season & "','" & CompName & "','" & MatchNo.ToString & "','H'," & RoundNo
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            '19.11.18 - update fixture_combined table
            Call objGlobals.update_fixtures_combined("clubs")
            objGlobals.close_connection()

            Exit Sub
        End If

        strSQL = "EXEC clubs.sp_update_still_in_competition '" & objGlobals.get_current_season & "','" & CompName & "',0," & RoundNo
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

        'flag the away players as losers !
        For intI As Integer = 1 To AwayPlayerCount
            strSQL = "UPDATE clubs.vw_players SET "
            If InStr(CompName, "SINGLES", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_singles = 0 "
            If InStr(CompName, "DOUBLES", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_pairs = 0 "
            If InStr(CompName, "PAIRS", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_pairs = 0 "
            If InStr(CompName, "3-A-SIDE", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_triples = 0 "
            strSQL = strSQL + "WHERE league = '" & AwayLeague(intI) & "'"
            strSQL = strSQL + " AND team = '" & AwayTeam(intI) & "'"
            strSQL = strSQL + " AND player = '" & AwayPlayer(intI) & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()
        Next
    End Sub

    Sub update_away_win(ByVal MatchNo As Integer, ByVal WinningDrawNo As String, ByVal PrelimResult As Boolean, ByVal RoundNo As Integer)
        Dim strSQL As String = ""
        Dim myDataReader As oledbdatareader

        If PrelimResult Then
            strSQL = "UPDATE clubs.prelim SET result = 'A' WHERE competition = '" & CompName & "' AND match_no = " & MatchNo
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            strSQL = "EXEC clubs.sp_update_comp_result '" & objGlobals.get_current_season & "','" & CompName & "','P" & MatchNo.ToString & "','A',0"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            '19.11.18 - update fixture_combined table
            Call objGlobals.update_fixtures_combined("clubs")
            objGlobals.close_connection()

            Exit Sub
        End If

        '02.08.23 - each round is now randomly drawn, so no need to update the next round
        'Dim ThisMatch As String = "M" & Format(MatchNo, "00")
        'Select Case MatchNo Mod 2
        '    Case 1      ' match number is ODD, so their next match will be home
        '        If Val(WinningDrawNo) <> 99 Then
        '            strSQL = "UPDATE clubs.vw_draws SET home_draw = '" & WinningDrawNo & "',prev_away_draw1 = '" & ThisMatch & "'"
        '        Else
        '            strSQL = "UPDATE clubs.vw_draws SET home_draw = '" & WinningDrawNo & "',prev_home_draw1 = '" & ThisMatch & "'"
        '        End If
        '        strSQL = strSQL & ",result = 'X' WHERE Competition = '" & CompName & "' AND home_draw = '" & ThisMatch & "'"
        '    Case 0      ' match number is EVEN, so their next match will be away
        '        If Val(WinningDrawNo) <> 99 Then
        '            strSQL = "UPDATE clubs.vw_draws SET away_draw = '" & WinningDrawNo & "',prev_away_draw2 = '" & ThisMatch & "'"
        '        Else
        '            strSQL = "UPDATE clubs.vw_draws SET away_draw = '" & WinningDrawNo & "',prev_home_draw2 = '" & ThisMatch & "'"
        '        End If
        '        strSQL = strSQL & ",result = 'X' WHERE competition = '" & CompName & "' AND away_draw = '" & ThisMatch & "'"
        'End Select
        'myDataReader = objGlobals.SQLSelect(strSQL)
        'objGlobals.close_connection()

        ' update current match
        strSQL = "UPDATE clubs.vw_draws SET Result = 'A'  WHERE Competition = '" & CompName & "' AND match_no = " & MatchNo
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

        If Not PlayerRequired Then
            strSQL = "EXEC clubs.sp_update_comp_result '" & objGlobals.get_current_season & "','" & CompName & "','" & MatchNo.ToString & "','A'," & RoundNo
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            '19.11.18 - update fixture_combined table
            Call objGlobals.update_fixtures_combined("clubs")
            objGlobals.close_connection()

            Exit Sub
        End If

        strSQL = "EXEC clubs.sp_update_still_in_competition '" & objGlobals.get_current_season & "','" & CompName & "',0," & RoundNo
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

        'flag the home players as losers !
        For intI As Integer = 1 To HomePlayerCount
            strSQL = "UPDATE clubs.vw_players SET "
            If InStr(CompName, "SINGLES", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_singles = 0 "
            If InStr(CompName, "DOUBLES", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_pairs = 0 "
            If InStr(CompName, "PAIRS", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_pairs = 0 "
            If InStr(CompName, "3-A-SIDE", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_triples = 0 "
            strSQL = strSQL + "WHERE league = '" & HomeLeague(intI) & "'"
            strSQL = strSQL + " AND team = '" & HomeTeam(intI) & "'"
            strSQL = strSQL + " AND player = '" & HomePlayer(intI) & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()
        Next
    End Sub

    Sub update_not_played(ByVal MatchNo As Integer, ByVal PrelimResult As Boolean, ByVal RoundNo As Integer)
        Dim strSQL As String = ""
        Dim myDataReader As oledbdatareader

        If PrelimResult Then
            strSQL = "UPDATE clubs.prelim SET result = 'N' WHERE competition = '" & CompName & "' AND match_no = " & MatchNo
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            strSQL = "EXEC clubs.sp_update_comp_result '" & objGlobals.get_current_season & "','" & CompName & "','P" & MatchNo.ToString & "','N',0"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            '19.11.18 - update fixture_combined table
            Call objGlobals.update_fixtures_combined("clubs")
            objGlobals.close_connection()

            Exit Sub
        End If

        '02.08.23 - each round is now randomly drawn, so no need to update the next round
        'Dim ThisMatch As String = "M" & Format(MatchNo, "00")
        'Select Case MatchNo Mod 2
        '    Case 1      ' match number is ODD
        '        strSQL = "UPDATE clubs.vw_draws SET "
        '        strSQL = strSQL & "home_draw = ' 99',prev_home_draw1 = '" & ThisMatch & "'"
        '        strSQL = strSQL & ",result = 'X' WHERE competition = '" & CompName & "' AND home_draw = '" & ThisMatch & "'"
        '    Case 0      ' match number is EVEN
        '        strSQL = "UPDATE clubs.vw_draws SET "
        '        strSQL = strSQL & "away_draw = ' 99',prev_home_draw2 = '" & ThisMatch & "'"
        '        strSQL = strSQL & ",result = 'X' WHERE competition = '" & CompName & "' AND away_draw = '" & ThisMatch & "'"
        'End Select
        'myDataReader = objGlobals.SQLSelect(strSQL)
        'objGlobals.close_connection()

        'update the current match
        strSQL = "UPDATE clubs.vw_draws SET result = 'N' WHERE competition = '" & CompName & "' AND match_no = " & MatchNo
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

        If Not PlayerRequired Then
            strSQL = "EXEC clubs.sp_update_comp_result '" & objGlobals.get_current_season & "','" & CompName & "','" & MatchNo.ToString & "','N',0"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            '19.11.18 - update fixture_combined table
            Call objGlobals.update_fixtures_combined("clubs")
            objGlobals.close_connection()

            Exit Sub
        End If

        strSQL = "EXEC clubs.sp_update_still_in_competition '" & objGlobals.get_current_season & "','" & CompName & "',0," & RoundNo
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

        'flag the home and away players as losers !
        For intI As Integer = 1 To HomePlayerCount
            strSQL = "UPDATE clubs.vw_players SET "
            If InStr(CompName, "SINGLES", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_singles = 0 "
            If InStr(CompName, "DOUBLES", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_pairs = 0 "
            If InStr(CompName, "PAIRS", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_pairs = 0 "
            If InStr(CompName, "3-A-SIDE", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_triples = 0 "
            strSQL = strSQL + "WHERE league = '" & HomeLeague(intI) & "'"
            strSQL = strSQL + " AND team = '" & HomeTeam(intI) & "'"
            strSQL = strSQL + " AND player = '" & HomePlayer(intI) & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()
        Next
        For intI As Integer = 1 To AwayPlayerCount
            strSQL = "UPDATE clubs.vw_players SET "
            If InStr(CompName, "SINGLES", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_singles = 0 "
            If InStr(CompName, "DOUBLES", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_pairs = 0 "
            If InStr(CompName, "PAIRS", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_pairs = 0 "
            If InStr(CompName, "3-A-SIDE", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_triples = 0 "
            strSQL = strSQL + "WHERE league = '" & AwayLeague(intI) & "'"
            strSQL = strSQL + " AND team = '" & AwayTeam(intI) & "'"
            strSQL = strSQL + " AND player = '" & AwayPlayer(intI) & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()
        Next
    End Sub

    Sub reset_result(ByVal MatchNo As Integer, ByVal PrelimResult As Boolean, ByVal RoundNo As Integer)
        Dim NextMatchNo As String = ""
        Dim ThisRound As Integer
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim myDataReader2 As oledbdatareader

        If PrelimResult Then
            strSQL = "UPDATE clubs.prelim SET result = 'X' WHERE competition = '" & CompName & "' AND match_no = " & MatchNo
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            strSQL = "EXEC clubs.sp_update_comp_result '" & objGlobals.get_current_season & "','" & CompName & "','P" & MatchNo.ToString & "','X',0"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            '19.11.18 - update fixture_combined table
            Call objGlobals.update_fixtures_combined("clubs")
            objGlobals.close_connection()

            Exit Sub
        End If

        'reset current match in current round
        strSQL = "SELECT * FROM clubs.vw_draws WHERE competition = '" & CompName & "' AND match_no = " & MatchNo
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            'reset current result
            ThisRound = myDataReader.Item("round")
            NextMatchNo = myDataReader.Item("next_match_no")

            strSQL = "UPDATE clubs.vw_draws SET Result = 'X' WHERE competition = '" & CompName & "' AND match_no = " & MatchNo
            myDataReader2 = objGlobals.SQLSelect(strSQL)
        End While
        objGlobals.close_connection()

        'reset future rounds where another match may have already been played
        While NextMatchNo <> "XXX"
            myDataReader = Nothing
            strSQL = "SELECT * FROM clubs.vw_draws WHERE competition = '" & CompName & "' AND match_no = " & Val(Left(NextMatchNo, 2))
            myDataReader = objGlobals.SQLSelect(strSQL)

            If myDataReader.HasRows Then
                While myDataReader.Read()
                    Select Case Right(NextMatchNo, 1)
                        Case "H"
                            If Left(myDataReader.Item("prev_home_draw1"), 1) = "M" Then
                                strSQL = "UPDATE clubs.vw_draws SET result = 'X',prev_home_draw1 = '   ',home_draw = '" & myDataReader.Item("prev_home_draw1") & "' WHERE competition = '" & CompName & "' AND match_no = " & Val(Left(NextMatchNo, 2))
                                myDataReader2 = objGlobals.SQLSelect(strSQL)
                            Else
                                If Left(myDataReader.Item("prev_away_draw1"), 1) = "M" Then
                                    strSQL = "UPDATE clubs.vw_draws SET result = 'X',prev_away_draw1 = '   ',home_draw = '" & myDataReader.Item("prev_away_draw1") & "' WHERE competition = '" & CompName & "' AND match_no = " & Val(Left(NextMatchNo, 2))
                                    myDataReader2 = objGlobals.SQLSelect(strSQL)
                                End If
                            End If
                        Case "A"
                            If Left(myDataReader.Item("prev_home_draw2"), 1) = "M" Then
                                strSQL = "UPDATE clubs.vw_draws SET result = 'X',prev_home_draw2 = '   ',away_draw = '" & myDataReader.Item("prev_home_draw2") & "' WHERE competition = '" & CompName & "' AND match_no = " & Val(Left(NextMatchNo, 2))
                                myDataReader2 = objGlobals.SQLSelect(strSQL)
                            Else
                                If Left(myDataReader.Item("prev_away_draw2"), 1) = "M" Then
                                    strSQL = "UPDATE clubs.vw_draws SET result = 'X',prev_away_draw2 = '   ',away_draw = '" & myDataReader.Item("prev_away_draw2") & "' WHERE competition = '" & CompName & "' AND match_no = " & Val(Left(NextMatchNo, 2))
                                    myDataReader2 = objGlobals.SQLSelect(strSQL)
                                End If
                            End If
                    End Select
                    NextMatchNo = myDataReader.Item("next_match_no")
                End While
                objGlobals.close_connection()

            Else
                NextMatchNo = "FIN"
            End If
            If NextMatchNo = "FIN" Then NextMatchNo = "XXX"
        End While

        PlayerRequired = True
        If InStr(CompName, "TEAM", CompareMethod.Text) > 0 Then
            PlayerRequired = False
        End If
        If InStr(CompName, "6-A-SIDE", CompareMethod.Text) > 0 Or InStr(CompName, "12-A-SIDE", CompareMethod.Text) > 0 Then
            PlayerRequired = False
        End If

        Call store_match_info(MatchNo)

        If Not PlayerRequired Then
            strSQL = "EXEC clubs.sp_update_comp_result '" & objGlobals.get_current_season & "','" & CompName & "','" & MatchNo.ToString & "','X'," & RoundNo
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()

            '19.11.18 - update fixture_combined table
            Call objGlobals.update_fixtures_combined("clubs")
            objGlobals.close_connection()

            Exit Sub
        End If

        strSQL = "EXEC clubs.sp_update_still_in_competition '" & objGlobals.get_current_season & "','" & CompName & "',0," & RoundNo
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

        'reset the 'StillIn' comp field to be True for the players that have been re-instated at this point
        For intI As Integer = 1 To HomePlayerCount
            strSQL = "UPDATE clubs.vw_players SET "
            If InStr(CompName, "SINGLES", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_singles = 1 "
            If InStr(CompName, "DOUBLES", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_pairs = 1 "
            If InStr(CompName, "PAIRS", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_pairs = 1 "
            If InStr(CompName, "3-A-SIDE", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_triples = 1 "
            strSQL = strSQL + "WHERE league = '" & HomeLeague(intI) & "'"
            strSQL = strSQL + " AND team = '" & HomeTeam(intI) & "'"
            strSQL = strSQL + " AND player = '" & HomePlayer(intI) & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()
        Next
        For intI As Integer = 1 To AwayPlayerCount
            strSQL = "UPDATE clubs.vw_players SET "
            If InStr(CompName, "SINGLES", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_singles = 1 "
            If InStr(CompName, "DOUBLES", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_pairs = 1 "
            If InStr(CompName, "PAIRS", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_pairs = 1 "
            If InStr(CompName, "3-A-SIDE", CompareMethod.Text) > 0 Then strSQL = strSQL + "still_in_triples = 1 "
            strSQL = strSQL + "WHERE league = '" & AwayLeague(intI) & "'"
            strSQL = strSQL + " AND team = '" & AwayTeam(intI) & "'"
            strSQL = strSQL + " AND player = '" & AwayPlayer(intI) & "'"
            myDataReader = objGlobals.SQLSelect(strSQL)
            objGlobals.close_connection()
        Next

    End Sub

    Sub store_match_info(MatchNo As Integer)
        'store for the league, team AND players for the match

        HomePlayerCount = 0
        AwayPlayerCount = 0

        Dim myDataReader As oledbdatareader
        Dim strSQL As String
        strSQL = "SELECT league,team,player FROM clubs.vw_entries WHERE draw_no IN "
        strSQL = strSQL & "(SELECT RIGHT(home_draw,2) FROM clubs.vw_draws WHERE competition = '" & CompName & "' AND match_no = " & MatchNo & ")"
        strSQL = strSQL & " AND competition = '" & CompName & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            HomeLeague(0) = myDataReader.Item("league")
            HomeTeam(0) = myDataReader.Item("team")
            HomePlayer(0) = myDataReader.Item("player")
            If InStr(CompName, "3-A-SIDE", CompareMethod.Text) > 0 Then
                Dim strSplitArr() As String = myDataReader.Item("player").split("/")
                For Each arrStr As String In strSplitArr
                    HomePlayerCount = HomePlayerCount + 1
                    HomeLeague(HomePlayerCount) = myDataReader.Item("league")
                    HomeTeam(HomePlayerCount) = myDataReader.Item("team")
                    HomePlayer(HomePlayerCount) = Trim(arrStr)
                Next
            End If
            If InStr(CompName, "DOUBLES", CompareMethod.Text) > 0 Or InStr(CompName, "PAIRS", CompareMethod.Text) > 0 Then
                Dim strSplitArr() As String = myDataReader.Item("player").split("&")
                For Each arrStr As String In strSplitArr
                    HomePlayerCount = HomePlayerCount + 1
                    HomeLeague(HomePlayerCount) = myDataReader.Item("league")
                    HomeTeam(HomePlayerCount) = myDataReader.Item("team")
                    HomePlayer(HomePlayerCount) = Trim(arrStr)
                Next
            End If
            If InStr(CompName, "SINGLES", CompareMethod.Text) > 0 Then
                HomePlayerCount = HomePlayerCount + 1
                HomeLeague(HomePlayerCount) = myDataReader.Item("league")
                HomeTeam(HomePlayerCount) = myDataReader.Item("team")
                HomePlayer(HomePlayerCount) = myDataReader.Item("player")
            End If
        End While
        objGlobals.close_connection()

        'store for the away player name(s)
        strSQL = "SELECT league,team,player FROM clubs.vw_entries WHERE draw_no IN "
        strSQL = strSQL & "(SELECT RIGHT(away_draw,2) FROM clubs.vw_draws WHERE Competition = '" & CompName & "' AND match_no = " & MatchNo & ")"
        strSQL = strSQL & " AND competition = '" & CompName & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            AwayLeague(0) = myDataReader.Item("league")
            AwayTeam(0) = myDataReader.Item("team")
            AwayPlayer(0) = myDataReader.Item("player")
            If InStr(CompName, "3-A-SIDE", CompareMethod.Text) > 0 Then
                Dim strSplitArr() As String = myDataReader.Item("player").split("/")
                For Each arrStr As String In strSplitArr
                    AwayPlayerCount = AwayPlayerCount + 1
                    AwayLeague(AwayPlayerCount) = myDataReader.Item("league")
                    AwayTeam(AwayPlayerCount) = myDataReader.Item("team")
                    AwayPlayer(AwayPlayerCount) = Trim(arrStr)
                Next
            End If
            If InStr(CompName, "DOUBLES", CompareMethod.Text) > 0 Or InStr(CompName, "PAIRS", CompareMethod.Text) > 0 Then
                Dim strSplitArr() As String = myDataReader.Item("player").split("&")
                For Each arrStr As String In strSplitArr
                    AwayPlayerCount = AwayPlayerCount + 1
                    AwayLeague(AwayPlayerCount) = myDataReader.Item("league")
                    AwayTeam(AwayPlayerCount) = myDataReader.Item("team")
                    AwayPlayer(AwayPlayerCount) = Trim(arrStr)
                Next
            End If
            If InStr(CompName, "SINGLES", CompareMethod.Text) > 0 Then
                AwayPlayerCount = AwayPlayerCount + 1
                AwayLeague(AwayPlayerCount) = myDataReader.Item("league")
                AwayTeam(AwayPlayerCount) = myDataReader.Item("team")
                AwayPlayer(AwayPlayerCount) = myDataReader.Item("player")
            End If
        End While
        objGlobals.close_connection()

    End Sub

    Protected Sub lbComps_TextChanged(sender As Object, e As System.EventArgs) Handles lbComps.TextChanged
        CompName = lbComps.Text
        Call load_results()
    End Sub


    Protected Sub gridResults_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gridResults.SelectedIndexChanged

    End Sub
End Class
