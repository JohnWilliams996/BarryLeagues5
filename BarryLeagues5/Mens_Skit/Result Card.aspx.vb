Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
Partial Class Mens_Skit_ResultCard
    Inherits System.Web.UI.Page
    Private dt As DataTable
    Private dr As DataRow
    Private gRow As Integer
    Private objGlobals As New Globals
    Private HomePlayersTotal As Integer
    Private AwayPlayersTotal As Integer
    Private HomePoints As Integer
    Private AwayPoints As Integer
    Private HomeRollsWon As Double
    Private AwayRollsWon As Double
    Private HomeRollTotal As Integer
    Private AwayRollTotal As Integer
    Private FixtureLeague As String
    Private FixtureHomeTeam As String
    Private FixtureAwayTeam As String
    Private Status As Integer
    Private CompID As Integer
    Private Week As Integer
    Private Shared PrevPage As String = String.Empty

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        objGlobals.CurrentSchema = "mens_skit."
        If Not IsPostBack Then Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
        Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
        'btnClose.Attributes.Add("onClick", "javascript:history.back(); return false;")
        CompID = Request.QueryString("CompID")
        If Not IsPostBack Then
            If Request.UrlReferrer Is Nothing Then
                PrevPage = "~/Mens_Skit/Default.aspx"
            Else
                PrevPage = Request.UrlReferrer.ToString()
            End If

            rbView.Items.Add("Landscape")
            rbView.Items.Add("Portrait")
            rbView.SelectedIndex = 0
        End If
        objGlobals.CurrentSchema = "mens_skit."
        objGlobals.LeagueSelected = Request.QueryString("League")
        objGlobals.TeamSelected = Request.QueryString("Team")
        objGlobals.PlayerSelected = Request.QueryString("Player")
        Call load_card()
        If Status = 2 Then
            Call colour_rolls()
            Call colour_thirties()
            Call colour_high_scores()
            Call colour_totals()
            Call show_comments()
            imgCard.ImageUrl = "~/Mens_Skit/ResultsCards/" & CompID.ToString & ".jpg"
        Else
            imgCard.Visible = False
            lblComments.Text = "Result Card Not yet Submitted"
        End If
    End Sub

    Sub show_comments()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        strSQL = "Select comments FROM mens_skit.fixtures_comments WHERE season = '" & objGlobals.current_season & "' AND fixture_id = " & CompID.ToString
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            lblComments.Text = myDataReader.Item("comments")
        End While
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
                gridResult.Rows(gRow).Cells(1).ForeColor = White
            End If
            If Val(gridResult.Rows(gRow).Cells(6).Text) = away_high_score Then
                gridResult.Rows(gRow).Cells(5).BackColor = Blue
                gridResult.Rows(gRow).Cells(6).BackColor = Blue
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
                    Dim hLink1 As New HyperLink
                    hLink1.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & FixtureLeague & "&Team=" & FixtureHomeTeam & "&Player=" & dt.Rows(gRow)(0).ToString
                    hLink1.ForeColor = Black
                    hLink1.Text = e.Row.Cells(0).Text
                    hLink1.BackColor = gridResult.BackColor
                    e.Row.Cells(0).Controls.Add(hLink1)
                    If objGlobals.PlayerSelected = e.Row.Cells(0).Text Then
                        hLink1.BackColor = Red
                    End If
                    Dim hLink2 As New HyperLink
                    hLink2.NavigateUrl = "~/Mens_Skit/Stats.aspx?League=" & FixtureLeague & "&Team=" & FixtureAwayTeam & "&Player=" & dt.Rows(gRow)(5).ToString
                    hLink2.ForeColor = Black
                    hLink2.Text = e.Row.Cells(5).Text
                    hLink2.BackColor = gridResult.BackColor
                    e.Row.Cells(5).Controls.Add(hLink2)
                    If objGlobals.PlayerSelected = e.Row.Cells(5).Text Then
                        hLink2.BackColor = Red
                    End If

            End Select
            gRow = gRow + 1
        Else
            e.Row.Visible = False
        End If
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
                imgCard.Height = 600
        End Select
    End Sub
End Class
