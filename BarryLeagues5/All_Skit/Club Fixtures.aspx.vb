Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
'Imports MySql.Data.MySqlClient

Partial Class Club_Fixtures
    Inherits System.Web.UI.Page

    Private dt As DataTable
    Private dr As DataRow
    Private gRow As Integer
    Private objGlobals As New Globals
    Private labelColour As System.Drawing.Color
    Private ClubSelected As String
    Private TeamCount As Integer
    Private TeamName(14) As String
    Private Division(14) As String
    Private HomeNight(14) As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("all_skit")
        objGlobals.CurrentUser = "all_skit_user"
        objGlobals.CurrentSchema = "all_skit."
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        If Not IsPostBack Then
            Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
            ClubSelected = Request.QueryString("Club")
            lblClub.Text = ClubSelected
            btnBack1.Attributes.Add("onClick", "javascript:history.back(); return false;")
            btnBack2.Attributes.Add("onClick", "javascript:history.back(); return false;")
            Call load_venues()
            Call load_teams()
            Call load_grid()
        End If
    End Sub

    Sub load_teams()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        TeamCount = 0
        strSQL = "SELECT league,long_name,home_night FROM all_skit.vw_teams WHERE venue = '" & ClubSelected & "' ORDER BY league,long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            TeamCount = TeamCount + 1
            TeamName(TeamCount) = myDataReader.Item("long_name")
            Division(TeamCount) = myDataReader.Item("league")
            HomeNight(TeamCount) = myDataReader.Item("home_night")
        End While

        objGlobals.close_connection()


    End Sub

    Sub load_grid()
        dt = New DataTable

        dt.Columns.Add(New DataColumn("Week", GetType(System.String)))
        dt.Columns.Add(New DataColumn("HiddenDate", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Various", GetType(System.String)))
        dt.Columns.Add(New DataColumn("K1", GetType(System.String)))
        dt.Columns.Add(New DataColumn("K2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("K3", GetType(System.String)))
        dt.Columns.Add(New DataColumn("K4", GetType(System.String)))
        dt.Columns.Add(New DataColumn("K5", GetType(System.String)))
        dt.Columns.Add(New DataColumn("K6", GetType(System.String)))
        dt.Columns.Add(New DataColumn("K7", GetType(System.String)))
        dt.Columns.Add(New DataColumn("K8", GetType(System.String)))
        dt.Columns.Add(New DataColumn("K9", GetType(System.String)))
        dt.Columns.Add(New DataColumn("K10", GetType(System.String)))

        Call grid_load_teams()
        Call grid_load_dates()

        gridTeams.DataSource = dt
        gridTeams.DataBind()
        gridTeams.Columns(1).Visible = False

        Call grid_colour_rows()
        Call grid_load_fixtures()
        Call grid_tidy()

    End Sub

 
    Sub grid_load_teams()

        dr = dt.NewRow
        dr("Week") = ""
        dr("HiddenDate") = ""
        dr("Various") = "Team >"
        dr("K1") = ""
        dr("K2") = ""
        dr("K3") = ""
        dr("K4") = ""
        dr("K5") = ""
        dr("K6") = ""
        dr("K7") = ""
        dr("K8") = ""
        dr("K9") = ""
        dr("K10") = ""
        Select Case TeamCount
            Case 1 : dr("K1") = TeamName(1)
            Case 2 : dr("K1") = TeamName(1) : dr("K2") = TeamName(2)
            Case 3 : dr("K1") = TeamName(1) : dr("K2") = TeamName(2) : dr("K3") = TeamName(3)
            Case 4 : dr("K1") = TeamName(1) : dr("K2") = TeamName(2) : dr("K3") = TeamName(3) : dr("K4") = TeamName(4)
            Case 5 : dr("K1") = TeamName(1) : dr("K2") = TeamName(2) : dr("K3") = TeamName(3) : dr("K4") = TeamName(4) : dr("K5") = TeamName(5)
            Case 6 : dr("K1") = TeamName(1) : dr("K2") = TeamName(2) : dr("K3") = TeamName(3) : dr("K4") = TeamName(4) : dr("K5") = TeamName(5) : dr("K6") = TeamName(6)
            Case 7 : dr("K1") = TeamName(1) : dr("K2") = TeamName(2) : dr("K3") = TeamName(3) : dr("K4") = TeamName(4) : dr("K5") = TeamName(5) : dr("K6") = TeamName(6) : dr("K7") = TeamName(7)
            Case 8 : dr("K1") = TeamName(1) : dr("K2") = TeamName(2) : dr("K3") = TeamName(3) : dr("K4") = TeamName(4) : dr("K5") = TeamName(5) : dr("K6") = TeamName(6) : dr("K7") = TeamName(7) : dr("K8") = TeamName(8)
            Case 9 : dr("K1") = TeamName(1) : dr("K2") = TeamName(2) : dr("K3") = TeamName(3) : dr("K4") = TeamName(4) : dr("K5") = TeamName(5) : dr("K6") = TeamName(6) : dr("K7") = TeamName(7) : dr("K8") = TeamName(8) : dr("K9") = TeamName(9)
            Case 10 : dr("K1") = TeamName(1) : dr("K2") = TeamName(2) : dr("K3") = TeamName(3) : dr("K4") = TeamName(4) : dr("K5") = TeamName(5) : dr("K6") = TeamName(6) : dr("K7") = TeamName(7) : dr("K8") = TeamName(8) : dr("K9") = TeamName(9) : dr("K10") = TeamName(10)
        End Select
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Week") = ""
        dr("HiddenDate") = ""
        dr("Various") = "League >"

        dr("K1") = ""
        dr("K2") = ""
        dr("K3") = ""
        dr("K4") = ""
        dr("K5") = ""
        dr("K6") = ""
        dr("K7") = ""
        dr("K8") = ""
        dr("K9") = ""
        dr("K10") = ""
        Select Case TeamCount
            Case 1 : dr("K1") = Division(1)
            Case 2 : dr("K1") = Division(1) : dr("K2") = Division(2)
            Case 3 : dr("K1") = Division(1) : dr("K2") = Division(2) : dr("K3") = Division(3)
            Case 4 : dr("K1") = Division(1) : dr("K2") = Division(2) : dr("K3") = Division(3) : dr("K4") = Division(4)
            Case 5 : dr("K1") = Division(1) : dr("K2") = Division(2) : dr("K3") = Division(3) : dr("K4") = Division(4) : dr("K5") = Division(5)
            Case 6 : dr("K1") = Division(1) : dr("K2") = Division(2) : dr("K3") = Division(3) : dr("K4") = Division(4) : dr("K5") = Division(5) : dr("K6") = Division(6)
            Case 7 : dr("K1") = Division(1) : dr("K2") = Division(2) : dr("K3") = Division(3) : dr("K4") = Division(4) : dr("K5") = Division(5) : dr("K6") = Division(6) : dr("K7") = Division(7)
            Case 8 : dr("K1") = Division(1) : dr("K2") = Division(2) : dr("K3") = Division(3) : dr("K4") = Division(4) : dr("K5") = Division(5) : dr("K6") = Division(6) : dr("K7") = Division(7) : dr("K8") = Division(8)
            Case 9 : dr("K1") = Division(1) : dr("K2") = Division(2) : dr("K3") = Division(3) : dr("K4") = Division(4) : dr("K5") = Division(5) : dr("K6") = Division(6) : dr("K7") = Division(7) : dr("K8") = Division(8) : dr("K9") = Division(9)
            Case 10 : dr("K1") = Division(1) : dr("K2") = Division(2) : dr("K3") = Division(3) : dr("K4") = Division(4) : dr("K5") = Division(5) : dr("K6") = Division(6) : dr("K7") = Division(7) : dr("K8") = Division(8) : dr("K9") = Division(9) : dr("K10") = Division(10)
        End Select
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Week") = ""
        dr("HiddenDate") = ""
        dr("Various") = "Night >"

        dr("K1") = ""
        dr("K2") = ""
        dr("K3") = ""
        dr("K4") = ""
        dr("K5") = ""
        dr("K6") = ""
        dr("K7") = ""
        dr("K8") = ""
        dr("K9") = ""
        dr("K10") = ""
        Select Case TeamCount
            Case 1 : dr("K1") = HomeNight(1)
            Case 2 : dr("K1") = HomeNight(1) : dr("K2") = HomeNight(2)
            Case 3 : dr("K1") = HomeNight(1) : dr("K2") = HomeNight(2) : dr("K3") = HomeNight(3)
            Case 4 : dr("K1") = HomeNight(1) : dr("K2") = HomeNight(2) : dr("K3") = HomeNight(3) : dr("K4") = HomeNight(4)
            Case 5 : dr("K1") = HomeNight(1) : dr("K2") = HomeNight(2) : dr("K3") = HomeNight(3) : dr("K4") = HomeNight(4) : dr("K5") = HomeNight(5)
            Case 6 : dr("K1") = HomeNight(1) : dr("K2") = HomeNight(2) : dr("K3") = HomeNight(3) : dr("K4") = HomeNight(4) : dr("K5") = HomeNight(5) : dr("K6") = HomeNight(6)
            Case 7 : dr("K1") = HomeNight(1) : dr("K2") = HomeNight(2) : dr("K3") = HomeNight(3) : dr("K4") = HomeNight(4) : dr("K5") = HomeNight(5) : dr("K6") = HomeNight(6) : dr("K7") = HomeNight(7)
            Case 8 : dr("K1") = HomeNight(1) : dr("K2") = HomeNight(2) : dr("K3") = HomeNight(3) : dr("K4") = HomeNight(4) : dr("K5") = HomeNight(5) : dr("K6") = HomeNight(6) : dr("K7") = HomeNight(7) : dr("K8") = HomeNight(8)
            Case 9 : dr("K1") = HomeNight(1) : dr("K2") = HomeNight(2) : dr("K3") = HomeNight(3) : dr("K4") = HomeNight(4) : dr("K5") = HomeNight(5) : dr("K6") = HomeNight(6) : dr("K7") = HomeNight(7) : dr("K8") = HomeNight(8) : dr("K9") = HomeNight(9)
            Case 10 : dr("K1") = HomeNight(1) : dr("K2") = HomeNight(2) : dr("K3") = HomeNight(3) : dr("K4") = HomeNight(4) : dr("K5") = HomeNight(5) : dr("K6") = HomeNight(6) : dr("K7") = HomeNight(7) : dr("K8") = HomeNight(8) : dr("K9") = HomeNight(9) : dr("K10") = HomeNight(10)
        End Select
        dt.Rows.Add(dr)

    End Sub

      Sub grid_load_dates()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim intI As Integer


        dr = dt.NewRow
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Week") = "Wk"
        dr("HiddenDate") = ""
        dr("Various") = "Date"
        dt.Rows.Add(dr)

        strSQL = "SELECT week_number,week_commences FROM all_skit.vw_weeks ORDER BY week_commences"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            For intI = 0 To 4
                dr = dt.NewRow
                dr("Week") = myDataReader.Item(0)
                dr("HiddenDate") = DateAdd(DateInterval.Day, intI, myDataReader.Item(1))
                dr("Various") = DateAdd(DateInterval.Day, intI, myDataReader.Item(1)).ToString("ddd dd MMM")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("Week") = ""
                dr("HiddenDate") = ""
                dr("Various") = ""
                dt.Rows.Add(dr)
            Next
        End While

    End Sub

    Sub grid_colour_rows()
        Dim gRow As Integer
        Dim BackColour As System.Drawing.Color

        For gRow = 5 To gridTeams.Rows.Count - 1 Step 2
            If gRow Mod 4 = 1 Or gRow Mod 4 = 2 Then
                BackColour = Black
            Else
                BackColour = System.Drawing.Color.FromArgb(&H11, &H11, &H11)
            End If
            gridTeams.Rows(gRow).BackColor = BackColour
            gridTeams.Rows(gRow + 1).BackColor = BackColour
        Next

    End Sub

    Sub grid_load_fixtures()

        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim intI As Integer
        Dim gRow As Integer
        Dim gCol As Integer
        Dim ThisDate As Date
        Dim ThisLeague As String
        Dim ToolTip As String

        gCol = 2
        For intI = 1 To TeamCount
            gCol = gCol + 1
            strSQL = "SELECT * FROM all_skit.vw_fixtures WHERE (home_team_name = '" & TeamName(intI) & "' OR away_team_name = '" & TeamName(intI) & "' OR ISNUMERIC(home_team) = 0) AND league = '" & Division(intI) & "' ORDER BY week_number"
            myDataReader = objGlobals.SQLSelect(strSQL)
            While myDataReader.Read()
                ThisDate = myDataReader.Item("fixture_calendar")
                ThisLeague = myDataReader.Item("league")
                For gRow = 5 To gridTeams.Rows.Count - 1 Step 2
                    If gridTeams.Rows(gRow).Cells(1).Text = ThisDate Then
                        ToolTip = myDataReader.Item("fixture_date")
                        ToolTip = ToolTip & vbCr & ThisLeague & vbCr
                        gridTeams.Rows(gRow).Cells(gCol).ForeColor = Cyan
                        Select Case myDataReader.Item("home_team_name") ' check home team name
                            Case TeamName(intI)  'Home Fixture
                                gridTeams.Rows(gRow).Cells(gCol).Text = myDataReader.Item("away_team_name") & " (h)"
                                If myDataReader.Item("away_team_name") <> "BYE" And myDataReader.Item("home_result") <> "0 - 0" Then
                                    ToolTip = ToolTip & myDataReader.Item("home_team_name") & " " & myDataReader.Item("home_points") & vbCr
                                    ToolTip = ToolTip & myDataReader.Item("away_team_name") & " " & myDataReader.Item("away_points")
                                    Select Case myDataReader.Item("home_points") - myDataReader.Item("away_points")
                                        Case Is > 0
                                            gridTeams.Rows(gRow + 1).Cells(gCol).Text = "WON " & myDataReader.Item("home_result")
                                        Case Is = 0
                                            gridTeams.Rows(gRow + 1).Cells(gCol).Text = "DRAW " & myDataReader.Item("home_result")
                                        Case Is < 0
                                            gridTeams.Rows(gRow + 1).Cells(gCol).Text = "LOST " & myDataReader.Item("home_result")
                                    End Select
                                ElseIf myDataReader.Item("away_team_name") = "BYE" Then
                                    gridTeams.Rows(gRow).Cells(gCol).Text = "BYE WEEK"
                                    ToolTip = ToolTip & TeamName(intI) & vbCr
                                    ToolTip = ToolTip & "BYE WEEK"
                                ElseIf myDataReader.Item("home_result") = "0 - 0" Then
                                    ToolTip = ToolTip & TeamName(intI) & " v " & vbCr
                                    ToolTip = ToolTip & myDataReader.Item("away_team_name")
                                End If
                                'Case "OPEN"              'Open week
                                '    gridTeams.Rows(gRow).Cells(gCol).Text = "OPEN WEEK"
                                '    ToolTip = ToolTip & TeamName(intI) & vbCr
                                '    ToolTip = ToolTip & "OPEN WEEK"
                            Case Else                'Away Fixture
                                If IsNumeric(myDataReader.Item("home_team")) = False Then
                                    'open week
                                    gridTeams.Rows(gRow).Cells(gCol).Text = myDataReader.Item("home_team_name")
                                    ToolTip = ToolTip & TeamName(intI) & vbCr
                                    ToolTip = ToolTip & myDataReader.Item("home_team_name")
                                Else
                                    gridTeams.Rows(gRow).Cells(gCol).Text = myDataReader.Item("home_team_name") & " (a)"
                                    If myDataReader.Item("home_team_name") <> "BYE" And myDataReader.Item("away_result") <> "0 - 0" Then
                                        ToolTip = ToolTip & myDataReader.Item("home_team_name") & " " & myDataReader.Item("home_points") & vbCr
                                        ToolTip = ToolTip & myDataReader.Item("away_team_name") & " " & myDataReader.Item("away_points")
                                        Select Case myDataReader.Item("away_points") - myDataReader.Item("home_points")
                                            Case Is > 0
                                                gridTeams.Rows(gRow + 1).Cells(gCol).Text = "WON " & myDataReader.Item("away_result")
                                            Case Is = 0
                                                gridTeams.Rows(gRow + 1).Cells(gCol).Text = "DRAW " & myDataReader.Item("away_result")
                                            Case Is < 0
                                                gridTeams.Rows(gRow + 1).Cells(gCol).Text = "LOST " & myDataReader.Item("away_result")
                                        End Select
                                    ElseIf myDataReader.Item("home_team_name") = "BYE" Then
                                        gridTeams.Rows(gRow).Cells(gCol).Text = "BYE WEEK"
                                        ToolTip = ToolTip & TeamName(intI) & vbCr
                                        ToolTip = ToolTip & "BYE WEEK"
                                    ElseIf myDataReader.Item("away_result") = "0 - 0" Then
                                        ToolTip = ToolTip & myDataReader.Item("home_team_name") & " v " & vbCr
                                        ToolTip = ToolTip & TeamName(intI)
                                    End If
                                End If
                        End Select
                        gridTeams.Rows(gRow).Cells(gCol).ToolTip = ToolTip
                        gridTeams.Rows(gRow + 1).Cells(gCol).ToolTip = ToolTip
                        Exit For
                    End If
                Next
            End While
        Next

    End Sub

    Sub grid_tidy()
        Dim gRow As Integer
        Dim gCol As Integer
        Dim visRows As Integer = 0

        Dim UKDateTime As Date = objGlobals.LondonDate(DateTime.UtcNow)
        Dim UKDate As String = Format(UKDateTime.Year, "0000") + Format(UKDateTime.Month, "00") + Format(UKDateTime.Day, "00")
        Dim UKTime As String = Format(UKDateTime.Hour, "00") & Format(UKDateTime.Minute, "00")


        'remove dates with no result
        With gridTeams
            For gRow = 5 To .Rows.Count - 1
                With .Rows(gRow)
                    If .Cells(0).Text = "&nbsp;" And .Cells(2).Text = "&nbsp;" And .Cells(3).Text = "&nbsp;" And .Cells(4).Text = "&nbsp;" And .Cells(5).Text = "&nbsp;" And .Cells(6).Text = "&nbsp;" And .Cells(7).Text = "&nbsp;" And .Cells(8).Text = "&nbsp;" And .Cells(9).Text = "&nbsp;" And .Cells(10).Text = "&nbsp;" And .Cells(11).Text = "&nbsp;" And .Cells(12).Text = "&nbsp;" Then
                        .Visible = False
                    Else
                        visRows = visRows + 1
                    End If
                End With
            Next gRow
        End With

        'colour the labels
        gridTeams.Rows(0).Cells(2).ForeColor = Yellow
        gridTeams.Rows(1).Cells(2).ForeColor = Yellow
        gridTeams.Rows(2).Cells(2).ForeColor = Yellow

        gridTeams.Rows(4).Cells(0).ForeColor = Yellow
        gridTeams.Rows(4).Cells(2).ForeColor = Yellow

        'colour the heading rows, depending on sport
        For gCol = 3 To gridTeams.Columns.Count - 1

            gridTeams.Rows(0).Cells(gCol).ForeColor = Black
            gridTeams.Rows(1).Cells(gCol).ForeColor = Black
            gridTeams.Rows(2).Cells(gCol).ForeColor = Black

            gridTeams.Rows(0).Cells(gCol).Font.Bold = True
            gridTeams.Rows(1).Cells(gCol).Font.Bold = True
            gridTeams.Rows(2).Cells(gCol).Font.Bold = True
            gridTeams.Rows(0).Cells(gCol).BackColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)
            gridTeams.Rows(1).Cells(gCol).BackColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)
            gridTeams.Rows(2).Cells(gCol).BackColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)
        Next

    End Sub

    Sub load_venues()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        ddlVenues.ClearSelection()

        strSQL = "SELECT DISTINCT(venue) FROM all_skit.vw_teams WHERE long_name <> 'BYE' ORDER BY venue"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddlVenues.Items.Add(myDataReader.Item("venue"))
        End While
        ddlVenues.Text = lblClub.Text
    End Sub

    Protected Sub ddlVenues_TextChanged(sender As Object, e As System.EventArgs) Handles ddlVenues.TextChanged
        Response.Redirect("~/All_Skit/Club Fixtures.aspx?Club=" & ddlVenues.Text)
    End Sub

End Class
