Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
'Imports MySql.Data.MySqlClient

Partial Class Honours
    Inherits System.Web.UI.Page
    Private dt As DataTable
    Private dr As DataRow
    Private gRow As Integer
    Private objGlobals As New Globals
    Private AllSeasons As Boolean = True
    Private Team As String
    Private LastGrid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("mens_skit")
        objGlobals.CurrentUser = "mens_skit_user"
        objGlobals.CurrentSchema = "mens_skit."
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
        Team = Request.QueryString("Team")
        LastGrid = Request.QueryString("Grid")
        If Not IsPostBack Then
            Dim strSQL As String
            Dim myDataReader As oledbdatareader

            ddSeasons.ClearSelection()
            ddSeasons.Items.Add("ALL")

            strSQL = "SELECT DISTINCT(season) FROM mens_skit.honours ORDER BY 1 DESC"
            myDataReader = objGlobals.SQLSelect(strSQL)
            While myDataReader.Read()
                ddSeasons.Items.Add(myDataReader.Item(0))
            End While
            ddSeasons.Text = "ALL"

            rbAll.Items.Add("All League Honours")
            rbAll.Items.Add("All Cup Honours")
            rbAll.Items.Add("All Rolls Honours")
            rbAll.Items.Add("All High Scores")

            If Team = "" Then
                rbAll.SelectedIndex = 0
                AllSeasons = True
                Call load_league_honours_all()
                Call load_honours_summary()
            Else
                Select Case LastGrid
                    Case "League"
                        rbAll.SelectedIndex = 0
                        Call load_league_honours_all()
                    Case "Cup"
                        rbAll.SelectedIndex = 1
                        Call load_cup_honours()
                    Case "Rolls"
                        rbAll.SelectedIndex = 2
                        Call load_rolls_honours_all()
                    Case "HighScores"
                        rbAll.SelectedIndex = 3
                        Call load_high_scores()
                End Select
                Call load_honours_team()
            End If
        End If
    End Sub

    Sub load_all_honours()

        Call load_league_honours()

        Call load_cup_honours()

        Call load_high_scores()

    End Sub

    Sub load_league_honours_all()
        divLeague.Visible = False
        divCup.Visible = False
        divHighScores.Visible = False
        divAllRollsHonours.Visible = False

        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        dt = New DataTable
        dr = dt.NewRow

        dt.Columns.Add(New DataColumn("Season", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Division 1", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Division 2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Division 3", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Division 4", GetType(System.String)))

        strSQL = "EXEC mens_skit.sp_get_league_honours_all"

        myDataReader = objGlobals.SQLSelect(strSQL)
        gridAllLeagueHonours.DataSource = myDataReader
        gridAllLeagueHonours.DataBind()
        divAllLeagueHonours.Visible = True

    End Sub
    Sub load_rolls_honours_all()
        divLeague.Visible = False
        divAllLeagueHonours.Visible = False
        divCup.Visible = False
        divHighScores.Visible = False

        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        dt = New DataTable
        dr = dt.NewRow

        dt.Columns.Add(New DataColumn("Season", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Division 1", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Division 2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Division 3", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Division 4", GetType(System.String)))

        strSQL = "EXEC mens_skit.sp_get_rolls_honours_all"

        myDataReader = objGlobals.SQLSelect(strSQL)
        gridAllRollsHonours.DataSource = myDataReader
        gridAllRollsHonours.DataBind()
        divAllRollsHonours.Visible = True
    End Sub

    Sub load_league_honours()
        rbAll.Visible = False

        divAllLeagueHonours.Visible = False
        divAllRollsHonours.Visible = False
        divCup.Visible = False
        divHighScores.Visible = False
        divSummary.Visible = False

        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim LastSeason As String = ""

        dt = New DataTable
        dr = dt.NewRow

        dt.Columns.Add(New DataColumn("Season", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Winners", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Runners-Up", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Rolls Winner", GetType(System.String)))

        strSQL = "SELECT season,league_name,league_winners,league_runners_up,rolls_winner1,rolls_winner2,rolls_winner3 FROM mens_skit.honours"
        strSQL &= " WHERE season = '" & ddSeasons.Text & "' AND Type = 'L' ORDER BY league_name"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            dr = dt.NewRow
            If myDataReader.Item("Season") <> LastSeason Then
                If LastSeason <> "" Then dt.Rows.Add(dt.NewRow)
                LastSeason = myDataReader.Item("Season")
                dr("Season") = LastSeason
            Else
                dr("Season") = ""
            End If
            dr("League") = myDataReader.Item("league_name")
            dr("Winners") = myDataReader.Item("league_winners")
            dr("Runners-Up") = myDataReader.Item("league_runners_up")
            dr("Rolls Winner") = myDataReader.Item("rolls_winner1")
            If myDataReader.Item("rolls_winner2") <> "" Then dr("Rolls Winner") &= " & " & myDataReader.Item("rolls_winner2")
            If myDataReader.Item("rolls_winner3") <> "" Then dr("Rolls Winner") &= " & " & myDataReader.Item("rolls_winner3")
            dt.Rows.Add(dr)
            If Not AllSeasons Then dt.Rows.Add(dt.NewRow)
        End While

        gridLeague.DataSource = dt
        gridLeague.DataBind()
        divLeague.Visible = True
    End Sub

    Sub load_cup_honours()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim LastSeason As String = ""

        If AllSeasons Then
            divLeague.Visible = False
            divAllLeagueHonours.Visible = False
            divAllRollsHonours.Visible = False
            divHighScores.Visible = False
        End If

        dt = New DataTable
        dr = dt.NewRow

        dt.Columns.Add(New DataColumn("Season", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Cup", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Winners", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Runners-Up", GetType(System.String)))

        strSQL = "SELECT season,cup_name,cup_winners,cup_runners_up FROM mens_skit.honours"
        If Not AllSeasons Then
            strSQL &= " WHERE season = '" & ddSeasons.Text & "' AND Type = 'C' ORDER BY cup_name"
        Else
            strSQL &= " WHERE Type = 'C' ORDER BY season DESC,cup_name"
        End If
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            dr = dt.NewRow
            If myDataReader.Item("Season") <> LastSeason Then
                If LastSeason <> "" Then dt.Rows.Add(dt.NewRow)
                LastSeason = myDataReader.Item("Season")
                dr("Season") = LastSeason
            Else
                dr("Season") = ""
            End If
            dr("Cup") = myDataReader.Item("cup_name")
            dr("Winners") = myDataReader.Item("cup_winners")
            If Not AllSeasons Then dr("Runners-Up") = myDataReader.Item("cup_runners_up")
            dt.Rows.Add(dr)
            If Not AllSeasons Then dt.Rows.Add(dt.NewRow)
        End While
        If AllSeasons Then
            gridCup.Columns(3).Visible = False 'hide the runners-up column for ALL seasons
        Else
            gridCup.Columns(3).Visible = True
        End If
        gridCup.DataSource = dt
        gridCup.DataBind()
        divCup.Visible = True

    End Sub
    Sub load_honours_summary()
        If divTeamHonours.Visible Then divTeamHonours.Visible = False

        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        dt = New DataTable
        dr = dt.NewRow

        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("League Wins", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Allform Wins", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Holme Towers Wins", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Champions Wins", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Brains Cup Wins", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Total", GetType(System.String)))

        strSQL = "EXEC mens_skit.sp_get_honours_summary"

        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            dr = dt.NewRow
            dr("Team") = myDataReader.Item("Team")
            dr("League Wins") = IIf(myDataReader.Item("League Wins") > 0, myDataReader.Item("League Wins"), "")
            dr("Allform Wins") = IIf(myDataReader.Item("Allform Wins") > 0, myDataReader.Item("Allform Wins"), "")
            dr("Holme Towers Wins") = IIf(myDataReader.Item("Holme Towers Wins") > 0, myDataReader.Item("Holme Towers Wins"), "")
            dr("Champions Wins") = IIf(myDataReader.Item("Champions Wins") > 0, myDataReader.Item("Champions Wins"), "")
            dr("Brains Cup Wins") = IIf(myDataReader.Item("Brains Cup Wins") > 0, myDataReader.Item("Brains Cup Wins"), "")
            dr("Total") = myDataReader.Item("Total")
            dt.Rows.Add(dr)
        End While

        gRow = 0
        gridSummary.DataSource = dt
        gridSummary.DataBind()
        divSummary.Visible = True
    End Sub
    Sub load_honours_team()
        divSummary.Visible = False
        If Right(Team, 1).ToUpper = "S" Then
            lblTeamHonours.Text = Team & "' Honours"
        Else
            lblTeamHonours.Text = Team & "'s Honours"
        End If

        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        dt = New DataTable
        dr = dt.NewRow

        dt.Columns.Add(New DataColumn("Season", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Div 1 Winner", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Div 2 Winner", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Div 3 Winner", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Div 4 Winner", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Allform Winner", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Holme Towers Winner", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Champions Winner", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Brains Cup Winner", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Season Total", GetType(System.String)))

        strSQL = "EXEC mens_skit.sp_get_honours_team '" & Team & "'"

        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            dr = dt.NewRow
            dr("Season") = myDataReader.Item("Season")
            dr("Div 1 Winner") = IIf(myDataReader.Item("Div 1 Winner"), "🏆", "")
            dr("Div 2 Winner") = IIf(myDataReader.Item("Div 2 Winner"), "🏆", "")
            dr("Div 3 Winner") = IIf(myDataReader.Item("Div 3 Winner"), "🏆", "")
            dr("Div 4 Winner") = IIf(myDataReader.Item("Div 4 Winner"), "🏆", "")
            dr("Allform Winner") = IIf(myDataReader.Item("Allform Winner"), "🏆", "")
            dr("Holme Towers Winner") = IIf(myDataReader.Item("Holme Towers Winner"), "🏆", "")
            dr("Champions Winner") = IIf(myDataReader.Item("Champions Winner"), "🏆", "")
            dr("Brains Cup Winner") = IIf(myDataReader.Item("Brains Cup Winner"), "🏆", "")
            dr("Season Total") = String.Concat(Enumerable.Repeat("🏆", myDataReader.Item("Season Total")))
            dt.Rows.Add(dr)
        End While
        gridTeamHonours.DataSource = dt
        gridTeamHonours.DataBind()
        divTeamHonours.Visible = True
    End Sub
    Sub load_high_scores()
        If Not AllSeasons Then divSummary.Visible = False

        If AllSeasons Then
            divLeague.Visible = False
            divCup.Visible = False
            divAllLeagueHonours.Visible = False
            divAllRollsHonours.Visible = False
        End If

        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim LastSeason As String = ""

        dt = New DataTable
        dr = dt.NewRow

        dt.Columns.Add(New DataColumn("Season", GetType(System.String)))
        dt.Columns.Add(New DataColumn("High Scores Lit", GetType(System.String)))
        dt.Columns.Add(New DataColumn("High Scores Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("High Scores Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("High Scores Score", GetType(System.String)))

        strSQL = "Select season, hs_home_away, hs_player, hs_team, hs_score FROM mens_skit.honours"
        If Not AllSeasons Then
            strSQL &= " WHERE season = '" & ddSeasons.Text & "' AND Type = 'H' ORDER BY hs_home_away DESC"
            Else
            strSQL &= " WHERE Type = 'H' ORDER BY season DESC,hs_home_away DESC"
        End If
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            dr = dt.NewRow
            If myDataReader.Item("Season") <> LastSeason Then
                If LastSeason <> "" Then dt.Rows.Add(dt.NewRow)
                LastSeason = myDataReader.Item("Season")
                dr("Season") = LastSeason
            Else
                dr("Season") = ""
            End If
            dr("High Scores Lit") = myDataReader.Item("hs_home_away")
            dr("High Scores Player") = myDataReader.Item("hs_player")
            dr("High Scores Team") = myDataReader.Item("hs_team")
            dr("High Scores Score") = myDataReader.Item("hs_score")
            dt.Rows.Add(dr)
            If Not AllSeasons Then dt.Rows.Add(dt.NewRow)
        End While
        gridHighScores.DataSource = dt
        gridHighScores.DataBind()
        divHighScores.Visible = True
    End Sub

    Protected Sub rbAll_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbAll.SelectedIndexChanged
        Select Case rbAll.SelectedIndex
            Case 0 'league
                Call load_league_honours_all()
                Call load_honours_summary()
            Case 1 'cup
                Call load_cup_honours()
                Call load_honours_summary()
            Case 2 'rolls
                Call load_rolls_honours_all()
                Call load_honours_summary()
            Case 3 'high scores
                Call load_high_scores()
                Call load_honours_summary()
        End Select
    End Sub

    Protected Sub ddSeasons_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddSeasons.SelectedIndexChanged
        If ddSeasons.Text <> "ALL" Then
            AllSeasons = False
            divSummary.Visible = False
            Call load_all_honours()
        Else
            AllSeasons = True
            rbAll.Visible = True
            rbAll.SelectedIndex = 0
            Call load_league_honours_all()
            Call load_honours_summary()
        End If
    End Sub

    Private Sub gridSummary_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridSummary.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hLink As New HyperLink
            Dim hLink2 As New HyperLink

            hLink.NavigateUrl = "~/Mens_Skit/Honours.aspx?Team=" & dt.Rows(gRow)(0)
            Select Case True
                Case gridAllLeagueHonours.Visible
                    hLink.NavigateUrl += "&Grid=League"
                Case gridCup.Visible
                    hLink.NavigateUrl += "&Grid=Cup"
                Case gridAllRollsHonours.Visible
                    hLink.NavigateUrl += "&Grid=Rolls"
                Case gridHighScores.Visible
                    hLink.NavigateUrl += "&Grid=HighScores"
            End Select
            hLink.Text = e.Row.Cells(0).Text
            hLink.ForeColor = Cyan
            e.Row.Cells(0).Controls.Add(hLink)

            gRow += 1
        End If
    End Sub
    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        divTeamHonours.Visible = False
        Call load_honours_summary()
    End Sub

End Class
