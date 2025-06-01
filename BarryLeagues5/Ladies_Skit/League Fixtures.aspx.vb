Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
'Imports MySql.Data.MySqlClient

Partial Class League_Fixtures
    Inherits System.Web.UI.Page
    Private dt As DataTable
    Private dr As DataRow
    Private gRow As Integer
    Private objGlobals As New Globals
    Private labelColour As System.Drawing.Color

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("ladies_skit")
        objGlobals.CurrentUser = "ladies_skit_user"
        objGlobals.CurrentSchema = "ladies_skit."
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        If Not IsPostBack Then
            Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
            objGlobals.LeagueSelected = Request.QueryString("League")
            lblLeague.Text = objGlobals.LeagueSelected
            labelColour = LightBlue
            lblLeague.ForeColor = labelColour
            Call load_leagues()
            Call load_fixtures()
        End If
    End Sub

    Sub load_leagues()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        strSQL = "SELECT league FROM ladies_skit.vw_leagues ORDER BY league"
        myDataReader = objGlobals.SQLSelect(strSQL)
        gRow = 0
        dt = New DataTable

        dt.Columns.Add(New DataColumn("League", GetType(System.String)))
        While myDataReader.Read()
            With gridLeagues
                dr = dt.NewRow
                dr("League") = myDataReader.Item("league")
                dt.Rows.Add(dr)
            End With
        End While

        gridLeagues.DataSource = dt
        gridLeagues.DataBind()
    End Sub


    Sub load_fixtures()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim HorA(99) As String
        Dim FixtureDate(99) As String
        Dim PointsFor(99) As Double
        Dim PointsAgainst(99) As Double
        Dim Result(99) As String
        Dim HomeNight(20) As String
        Dim Phone(20) As String
        Dim TeamName(20) As String
        Dim Team As Integer = 0
        Dim TeamCount As Integer = 0
        Dim WkCount As Integer

        strSQL = "SELECT long_name,home_night,phone_number FROM ladies_skit.vw_teams "
        strSQL = strSQL & " WHERE league = '" & objGlobals.LeagueSelected & "' AND long_name <> 'BYE' ORDER BY team_number"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            TeamCount = TeamCount + 1
            TeamName(TeamCount) = myDataReader.Item(0)
            HomeNight(TeamCount) = myDataReader.Item(1)
            Phone(TeamCount) = myDataReader.Item(2)
        End While
        dt = New DataTable

        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk1", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk3", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk4", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk5", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk6", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk7", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk8", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk9", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk10", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk11", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk12", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk13", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk14", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk15", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk16", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk17", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk18", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk19", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk20", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk21", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk22", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk23", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk24", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk25", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk26", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk27", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk28", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk29", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk30", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk31", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk32", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk33", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk34", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk35", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk36", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk37", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk38", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk39", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Wk40", GetType(System.String)))
        For Team = 1 To TeamCount
            strSQL = "SELECT a.week_number,fixture_calendar,CASE WHEN b.short_name IS NULL THEN 'OPEN' ELSE b.short_name END as Opponents,'Home' as 'HA',a.home_points as PointsFor,a.away_points as PointsAgainst,a.home_result as Result,a.home_team"
            strSQL = strSQL & " FROM ladies_skit.vw_fixtures a "
            strSQL = strSQL & " LEFT OUTER JOIN ladies_skit.vw_teams b ON a.away_team_name = b.long_name AND b.league = a.league"
            strSQL = strSQL & " WHERE a.League = '" & objGlobals.LeagueSelected & "' AND (a.home_team_name = '" & TeamName(Team) & "' OR ISNUMERIC(a.home_team) = 0) "
            strSQL = strSQL & " UNION "
            strSQL = strSQL & " SELECT a.week_number,fixture_calendar,b.short_name as Opponents,'Away',a.away_points,a.home_points,a.away_result,a.home_team"
            strSQL = strSQL & " FROM ladies_skit.vw_fixtures a "
            strSQL = strSQL & " INNER JOIN ladies_skit.vw_teams b ON a.home_team_name = b.long_name AND b.league = a.league"
            strSQL = strSQL & " WHERE a.League = '" & objGlobals.LeagueSelected & "'"
            strSQL = strSQL & " AND a.away_team_name = '" & TeamName(Team) & "'"

            myDataReader = objGlobals.SQLSelect(strSQL)
            dr = dt.NewRow
            WkCount = 0
            While myDataReader.Read()
                WkCount = WkCount + 1
                FixtureDate(WkCount) = objGlobals.LondonDate(myDataReader.Item("fixture_calendar")).ToString("d MMM")
                'If myDataReader.Item("Opponents") <> "OPEN" And myDataReader.Item("Opponents") <> "BYE" Then
                If IsNumeric(myDataReader.Item("home_team")) And myDataReader.Item("Opponents") <> "BYE" Then
                    HorA(WkCount) = myDataReader.Item("HA")
                Else
                    HorA(WkCount) = ""
                End If
                PointsFor(WkCount) = myDataReader.Item("PointsFor")
                PointsAgainst(WkCount) = myDataReader.Item("PointsAgainst")
                Result(WkCount) = myDataReader.Item("result")
                dr.Item(WkCount) = myDataReader.Item("Opponents")
            End While
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Team") = TeamName(Team)
            Dim i As Integer
            With gridResults
                For i = 1 To WkCount
                    dr.Item(i) = HorA(i)
                Next i
            End With
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Team") = "Night : " + HomeNight(Team)
            With gridResults
                For i = 1 To WkCount
                    dr.Item(i) = FixtureDate(i)
                Next i
            End With
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Team") = "Phone : " + Phone(Team)
            With gridResults
                For i = 1 To WkCount
                    If PointsFor(i) + PointsAgainst(i) > 0 Then
                        Select Case PointsFor(i) - PointsAgainst(i)
                            Case Is > 0
                                dr.Item(i) = "Won"
                            Case Is < 0
                                dr.Item(i) = "Lost"
                            Case Is = 0
                                dr.Item(i) = "Draw"
                        End Select
                    Else
                        dr.Item(i) = ""
                    End If
                Next i
            End With
            dt.Rows.Add(dr)

            dr = dt.NewRow
            With gridResults
                For i = 1 To WkCount
                    If PointsFor(i) + PointsAgainst(i) > 0 Then
                        dr.Item(i) = Result(i)
                    Else
                        dr.Item(i) = ""
                    End If
                Next i
            End With
            dt.Rows.Add(dr)

        Next Team
        gRow = 0
        gridResults.DataSource = dt
        gridResults.DataBind()

        'tidy the grid
        Dim iCol As Integer
        Dim iRow As Integer
        Dim iRow2 As Integer
        Dim BackColour As System.Drawing.Color
        With gridResults
            For iRow = 2 To .Rows.Count Step 5
                For iCol = 0 To WkCount
                    If iCol = 0 Then
                        If iRow Mod 2 = 1 Then
                            BackColour = Black
                        Else
                            BackColour = System.Drawing.Color.FromArgb(&H11, &H11, &H11)
                        End If
                    Else
                        .Rows(iRow).Cells(iCol).Wrap = False    ' un-wrap the date column
                        Select Case .Rows(iRow - 1).Cells(iCol).Text
                            Case "Home"
                                BackColour = System.Drawing.Color.FromArgb(&H14, &H14, &H14)
                            Case "Away"
                                BackColour = System.Drawing.Color.FromArgb(&H1F, &H1F, &H1F)
                            Case Else
                                BackColour = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                        End Select
                    End If
                    For iRow2 = iRow - 2 To iRow + 2
                        .Rows(iRow2).Cells(iCol).BackColor = BackColour
                        If (iRow2 = iRow + 1 Or iRow2 = iRow + 2) And iCol > 0 And .Rows(iRow2).Cells(iCol).Text <> "&nbsp;" Then
                            .Rows(iRow2).Cells(iCol).BackColor = DarkBlue
                            .Rows(iRow2).Cells(iCol).Font.Size = 9
                        End If
                    Next

                Next
            Next
        End With
    End Sub

    Private Sub gridLeagues_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridLeagues.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hLink As New HyperLink
            hLink.NavigateUrl = "~/Ladies_Skit/League Fixtures.aspx?League=" & dt.Rows(gRow)(0).ToString
            hLink.Text = e.Row.Cells(0).Text
            labelColour = LightBlue
            hLink.ForeColor = labelColour
            e.Row.Cells(0).Controls.Add(hLink)
            e.Row.CssClass = "cell"
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridResults_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResults.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If gRow Mod 5 = 1 Then
                Dim hLink As New HyperLink
                hLink.NavigateUrl = "~/Ladies_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(0).ToString
                hLink.Text = e.Row.Cells(0).Text
                hLink.ForeColor = Cyan
                e.Row.Cells(0).Controls.Add(hLink)
                'e.Row.CssClass = "row"
            End If
            gRow = gRow + 1
        End If
    End Sub


End Class
