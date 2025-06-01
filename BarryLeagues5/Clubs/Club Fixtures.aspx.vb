Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
Imports Microsoft.VisualBasic
'Imports MySql.Data.MySqlClient

Partial Class Club_Fixtures
    Inherits System.Web.UI.Page

    Private dt As DataTable
    Private dr As DataRow
    Private gRow As Integer
    Private objGlobals As New Globals
    Private labelColour As System.Drawing.Color
    Private ClubSelected As String
    Private CribTeamCount As Integer
    Private SkittlesTeamCount As Integer
    Private SnookerTeamCount As Integer
    Private CribTeamName(5) As String
    Private SkittlesTeamName(5) As String
    Private SnookerTeamName(5) As String
    Private CribDivision(5) As String
    Private SkittlesDivision(5) As String
    Private SnookerDivision(5) As String
    Private CribHomeNight(5) As String
    Private SkittlesHomeNight(5) As String
    Private SnookerHomeNight(5) As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("clubs")
        objGlobals.CurrentUser = "clubs_user"
        objGlobals.CurrentSchema = "clubs."

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
        Dim myDataReader As OleDbDataReader

        CribTeamCount = 0
        SkittlesTeamCount = 0
        SnookerTeamCount = 0
        strSQL = "SELECT league,long_name,home_night FROM clubs.vw_teams WHERE venue = '" & ClubSelected & "' ORDER BY league,long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            Select Case Left(myDataReader.Item("league"), 4)
                Case "CRIB"
                    CribTeamCount = CribTeamCount + 1
                    CribTeamName(CribTeamCount) = myDataReader.Item("long_name")
                    CribDivision(CribTeamCount) = myDataReader.Item("league")
                    CribHomeNight(CribTeamCount) = myDataReader.Item("home_night")
                Case "SKIT"
                    SkittlesTeamCount = SkittlesTeamCount + 1
                    SkittlesTeamName(SkittlesTeamCount) = myDataReader.Item("long_name")
                    SkittlesDivision(SkittlesTeamCount) = myDataReader.Item("league")
                    SkittlesHomeNight(SkittlesTeamCount) = myDataReader.Item("home_night")
                Case "SNOO"
                    SnookerTeamCount = SnookerTeamCount + 1
                    SnookerTeamName(SnookerTeamCount) = myDataReader.Item("long_name")
                    SnookerDivision(SnookerTeamCount) = myDataReader.Item("league")
                    SnookerHomeNight(SnookerTeamCount) = myDataReader.Item("home_night")
            End Select
        End While
        objGlobals.close_connection()

    End Sub

    Sub load_grid()
        dt = New DataTable

        dt.Columns.Add(New DataColumn("Week", GetType(System.String)))
        dt.Columns.Add(New DataColumn("HiddenDate", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Various", GetType(System.String)))
        dt.Columns.Add(New DataColumn("C1", GetType(System.String)))
        dt.Columns.Add(New DataColumn("C2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("C3", GetType(System.String)))
        dt.Columns.Add(New DataColumn("C4", GetType(System.String)))
        dt.Columns.Add(New DataColumn("C5", GetType(System.String)))
        dt.Columns.Add(New DataColumn("K1", GetType(System.String)))
        dt.Columns.Add(New DataColumn("K2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("K3", GetType(System.String)))
        dt.Columns.Add(New DataColumn("K4", GetType(System.String)))
        dt.Columns.Add(New DataColumn("K5", GetType(System.String)))
        dt.Columns.Add(New DataColumn("S1", GetType(System.String)))
        dt.Columns.Add(New DataColumn("S2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("S3", GetType(System.String)))
        dt.Columns.Add(New DataColumn("S4", GetType(System.String)))
        dt.Columns.Add(New DataColumn("S5", GetType(System.String)))

        Call grid_load_teams()
        Call grid_load_dates()

        gridTeams.DataSource = dt
        gridTeams.DataBind()
        gridTeams.Columns(1).Visible = False

        Call grid_colour_rows()
        Call grid_load_fixtures_crib()
        Call grid_load_fixtures_skittles()
        Call grid_load_fixtures_snooker()
        Call grid_tidy()

    End Sub


    Sub grid_load_teams()

        dr = dt.NewRow
        dr("Week") = ""
        dr("HiddenDate") = ""
        dr("Various") = "Team >"
        dr("C1") = ""
        dr("C2") = ""
        dr("C3") = ""
        dr("C4") = ""
        dr("C5") = ""
        Select Case CribTeamCount
            Case 1 : dr("C1") = CribTeamName(1)
            Case 2 : dr("C1") = CribTeamName(1) : dr("C2") = CribTeamName(2)
            Case 3 : dr("C1") = CribTeamName(1) : dr("C2") = CribTeamName(2) : dr("C3") = CribTeamName(3)
            Case 4 : dr("C1") = CribTeamName(1) : dr("C2") = CribTeamName(2) : dr("C3") = CribTeamName(3) : dr("C4") = CribTeamName(4)
            Case 5 : dr("C1") = CribTeamName(1) : dr("C2") = CribTeamName(2) : dr("C3") = CribTeamName(3) : dr("C4") = CribTeamName(4) : dr("C5") = CribTeamName(5)
        End Select
        dr("K1") = ""
        dr("K2") = ""
        dr("K3") = ""
        dr("K4") = ""
        dr("K5") = ""
        Select Case SkittlesTeamCount
            Case 1 : dr("K1") = SkittlesTeamName(1)
            Case 2 : dr("K1") = SkittlesTeamName(1) : dr("K2") = SkittlesTeamName(2)
            Case 3 : dr("K1") = SkittlesTeamName(1) : dr("K2") = SkittlesTeamName(2) : dr("K3") = SkittlesTeamName(3)
            Case 4 : dr("K1") = SkittlesTeamName(1) : dr("K2") = SkittlesTeamName(2) : dr("K3") = SkittlesTeamName(3) : dr("K4") = SkittlesTeamName(4)
            Case 5 : dr("K1") = SkittlesTeamName(1) : dr("K2") = SkittlesTeamName(2) : dr("K3") = SkittlesTeamName(3) : dr("K4") = SkittlesTeamName(4) : dr("K5") = SkittlesTeamName(5)
        End Select
        dr("S1") = ""
        dr("S2") = ""
        dr("S3") = ""
        dr("S4") = ""
        dr("S5") = ""
        Select Case SnookerTeamCount
            Case 1 : dr("S1") = SnookerTeamName(1)
            Case 2 : dr("S1") = SnookerTeamName(1) : dr("S2") = SnookerTeamName(2)
            Case 3 : dr("S1") = SnookerTeamName(1) : dr("S2") = SnookerTeamName(2) : dr("S3") = SnookerTeamName(3)
            Case 4 : dr("S1") = SnookerTeamName(1) : dr("S2") = SnookerTeamName(2) : dr("S3") = SnookerTeamName(3) : dr("S4") = SnookerTeamName(4)
            Case 5 : dr("S1") = SnookerTeamName(1) : dr("S2") = SnookerTeamName(2) : dr("S3") = SnookerTeamName(3) : dr("S4") = SnookerTeamName(4) : dr("S5") = SnookerTeamName(5)
        End Select
        dt.Rows.Add(dr)



        dr = dt.NewRow
        dr("Week") = ""
        dr("HiddenDate") = ""
        dr("Various") = "League >"
        dr("C1") = ""
        dr("C2") = ""
        dr("C3") = ""
        dr("C4") = ""
        dr("C5") = ""
        Select Case CribTeamCount
            Case 1 : dr("C1") = CribDivision(1)
            Case 2 : dr("C1") = CribDivision(1) : dr("C2") = CribDivision(2)
            Case 3 : dr("C1") = CribDivision(1) : dr("C2") = CribDivision(2) : dr("C3") = CribDivision(3)
            Case 4 : dr("C1") = CribDivision(1) : dr("C2") = CribDivision(2) : dr("C3") = CribDivision(3) : dr("C4") = CribDivision(4)
            Case 5 : dr("C1") = CribDivision(1) : dr("C2") = CribDivision(2) : dr("C3") = CribDivision(3) : dr("C4") = CribDivision(4) : dr("C5") = CribDivision(5)
        End Select
        dr("K1") = ""
        dr("K2") = ""
        dr("K3") = ""
        dr("K4") = ""
        dr("K5") = ""
        Select Case SkittlesTeamCount
            Case 1 : dr("K1") = SkittlesDivision(1)
            Case 2 : dr("K1") = SkittlesDivision(1) : dr("K2") = SkittlesDivision(2)
            Case 3 : dr("K1") = SkittlesDivision(1) : dr("K2") = SkittlesDivision(2) : dr("K3") = SkittlesDivision(3)
            Case 4 : dr("K1") = SkittlesDivision(1) : dr("K2") = SkittlesDivision(2) : dr("K3") = SkittlesDivision(3) : dr("K4") = SkittlesDivision(4)
            Case 5 : dr("K1") = SkittlesDivision(1) : dr("K2") = SkittlesDivision(2) : dr("K3") = SkittlesDivision(3) : dr("K4") = SkittlesDivision(4) : dr("K5") = SkittlesDivision(5)
        End Select
        dr("S1") = ""
        dr("S2") = ""
        dr("S3") = ""
        dr("S4") = ""
        dr("S5") = ""
        Select Case SnookerTeamCount
            Case 1 : dr("S1") = SnookerDivision(1)
            Case 2 : dr("S1") = SnookerDivision(1) : dr("S2") = SnookerDivision(2)
            Case 3 : dr("S1") = SnookerDivision(1) : dr("S2") = SnookerDivision(2) : dr("S3") = SnookerDivision(3)
            Case 4 : dr("S1") = SnookerDivision(1) : dr("S2") = SnookerDivision(2) : dr("S3") = SnookerDivision(3) : dr("S4") = SnookerDivision(4)
            Case 5 : dr("S1") = SnookerDivision(1) : dr("S2") = SnookerDivision(2) : dr("S3") = SnookerDivision(3) : dr("S4") = SnookerDivision(4) : dr("S5") = SnookerDivision(5)
        End Select
        dt.Rows.Add(dr)



        dr = dt.NewRow
        dr("Week") = ""
        dr("HiddenDate") = ""
        dr("Various") = "Night >"
        dr("C1") = ""
        dr("C2") = ""
        dr("C3") = ""
        dr("C4") = ""
        dr("C5") = ""
        Select Case CribTeamCount
            Case 1 : dr("C1") = CribHomeNight(1)
            Case 2 : dr("C1") = CribHomeNight(1) : dr("C2") = CribHomeNight(2)
            Case 3 : dr("C1") = CribHomeNight(1) : dr("C2") = CribHomeNight(2) : dr("C3") = CribHomeNight(3)
            Case 4 : dr("C1") = CribHomeNight(1) : dr("C2") = CribHomeNight(2) : dr("C3") = CribHomeNight(3) : dr("C4") = CribHomeNight(4)
            Case 5 : dr("C1") = CribHomeNight(1) : dr("C2") = CribHomeNight(2) : dr("C3") = CribHomeNight(3) : dr("C4") = CribHomeNight(4) : dr("C5") = CribHomeNight(5)
        End Select
        dr("K1") = ""
        dr("K2") = ""
        dr("K3") = ""
        dr("K4") = ""
        dr("K5") = ""
        Select Case SkittlesTeamCount
            Case 1 : dr("K1") = SkittlesHomeNight(1)
            Case 2 : dr("K1") = SkittlesHomeNight(1) : dr("K2") = SkittlesHomeNight(2)
            Case 3 : dr("K1") = SkittlesHomeNight(1) : dr("K2") = SkittlesHomeNight(2) : dr("K3") = SkittlesHomeNight(3)
            Case 4 : dr("K1") = SkittlesHomeNight(1) : dr("K2") = SkittlesHomeNight(2) : dr("K3") = SkittlesHomeNight(3) : dr("K4") = SkittlesHomeNight(4)
            Case 5 : dr("K1") = SkittlesHomeNight(1) : dr("K2") = SkittlesHomeNight(2) : dr("K3") = SkittlesHomeNight(3) : dr("K4") = SkittlesHomeNight(4) : dr("K5") = SkittlesHomeNight(5)
        End Select
        dr("S1") = ""
        dr("S2") = ""
        dr("S3") = ""
        dr("S4") = ""
        dr("S5") = ""
        Select Case SnookerTeamCount
            Case 1 : dr("S1") = SnookerHomeNight(1)
            Case 2 : dr("S1") = SnookerHomeNight(1) : dr("S2") = SnookerHomeNight(2)
            Case 3 : dr("S1") = SnookerHomeNight(1) : dr("S2") = SnookerHomeNight(2) : dr("S3") = SnookerHomeNight(3)
            Case 4 : dr("S1") = SnookerHomeNight(1) : dr("S2") = SnookerHomeNight(2) : dr("S3") = SnookerHomeNight(3) : dr("S4") = SnookerHomeNight(4)
            Case 5 : dr("S1") = SnookerHomeNight(1) : dr("S2") = SnookerHomeNight(2) : dr("S3") = SnookerHomeNight(3) : dr("S4") = SnookerHomeNight(4) : dr("S5") = SnookerHomeNight(5)
        End Select
        dt.Rows.Add(dr)


    End Sub

    Sub grid_load_dates()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim intI As Integer


        dr = dt.NewRow
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Week") = "Wk"
        dr("HiddenDate") = ""
        dr("Various") = "Date"
        dt.Rows.Add(dr)

        strSQL = "SELECT week_number,week_commences FROM clubs.vw_weeks ORDER BY week_commences"
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
        objGlobals.close_connection()


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

    Sub grid_load_fixtures_crib()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim intI As Integer
        Dim gRow As Integer
        Dim gCol As Integer
        Dim ThisDate As Date
        Dim ThisLeague As String
        Dim ToolTip As String

        gCol = 2
        For intI = 1 To CribTeamCount
            gCol = gCol + 1
            strSQL = "SELECT * FROM clubs.vw_fixtures WHERE league = '" & CribDivision(intI) & "' AND (home_team_name = '" & CribTeamName(intI) & "' OR away_team_name = '" & CribTeamName(intI) & "' OR ISNUMERIC(home_team) = 0) ORDER BY week_number"
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
                            Case CribTeamName(intI)  'Home Fixture
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
                                    ToolTip = ToolTip & CribTeamName(intI) & vbCr
                                    ToolTip = ToolTip & "BYE WEEK"
                                ElseIf myDataReader.Item("home_result") = "0 - 0" Then
                                    ToolTip = ToolTip & CribTeamName(intI) & " v " & vbCr
                                    ToolTip = ToolTip & myDataReader.Item("away_team_name")
                                End If
                                'Case "OPEN"              'Open week
                                '    gridTeams.Rows(gRow).Cells(gCol).Text = "OPEN WEEK"
                                '    ToolTip = ToolTip & CribTeamName(intI) & vbCr
                                '    ToolTip = ToolTip & "OPEN WEEK"
                            Case Else
                                If IsNumeric(myDataReader.Item("home_team")) = False Then
                                    'open week
                                    gridTeams.Rows(gRow).Cells(gCol).Text = myDataReader.Item("home_team_name")
                                    ToolTip = ToolTip & CribTeamName(intI) & vbCr
                                    ToolTip = ToolTip & myDataReader.Item("home_team_name")
                                Else
                                    'Away Fixture
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
                                        ToolTip = ToolTip & CribTeamName(intI) & vbCr
                                        ToolTip = ToolTip & "BYE WEEK"
                                    ElseIf myDataReader.Item("away_result") = "0 - 0" Then
                                        ToolTip = ToolTip & myDataReader.Item("home_team_name") & " v " & vbCr
                                        ToolTip = ToolTip & CribTeamName(intI)
                                    End If
                                End If
                        End Select
                        gridTeams.Rows(gRow).Cells(gCol).ToolTip = ToolTip
                        gridTeams.Rows(gRow + 1).Cells(gCol).ToolTip = ToolTip
                        Exit For
                    End If
                Next
            End While
            objGlobals.close_connection()

        Next

    End Sub

    Sub grid_load_fixtures_skittles()

        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim intI As Integer
        Dim gRow As Integer
        Dim gCol As Integer
        Dim ThisDate As Date
        Dim ThisLeague As String
        Dim ToolTip As String

        gCol = 7
        For intI = 1 To SkittlesTeamCount
            gCol = gCol + 1
            strSQL = "SELECT * FROM clubs.vw_fixtures WHERE league = '" & SkittlesDivision(intI) & "' AND (home_team_name = '" & SkittlesTeamName(intI) & "' OR away_team_name = '" & SkittlesTeamName(intI) & "' OR ISNUMERIC(home_team) = 0) ORDER BY week_number"
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
                            Case SkittlesTeamName(intI)  'Home Fixture
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
                                    ToolTip = ToolTip & SkittlesTeamName(intI) & vbCr
                                    ToolTip = ToolTip & "BYE WEEK"
                                ElseIf myDataReader.Item("home_result") = "0 - 0" Then
                                    ToolTip = ToolTip & SkittlesTeamName(intI) & " v " & vbCr
                                    ToolTip = ToolTip & myDataReader.Item("away_team_name")
                                End If
                                'Case "OPEN"              'Open week
                                '    gridTeams.Rows(gRow).Cells(gCol).Text = "OPEN WEEK"
                                '    ToolTip = ToolTip & SkittlesTeamName(intI) & vbCr
                                '    ToolTip = ToolTip & "OPEN WEEK"
                            Case Else
                                If IsNumeric(myDataReader.Item("home_team")) = False Then
                                    'open week
                                    gridTeams.Rows(gRow).Cells(gCol).Text = myDataReader.Item("home_team_name")
                                    ToolTip = ToolTip & SkittlesTeamName(intI) & vbCr
                                    ToolTip = ToolTip & myDataReader.Item("home_team_name")
                                Else
                                    'Away Fixture
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
                                        ToolTip = ToolTip & SkittlesTeamName(intI) & vbCr
                                        ToolTip = ToolTip & "BYE WEEK"
                                    ElseIf myDataReader.Item("away_result") = "0 - 0" Then
                                        ToolTip = ToolTip & myDataReader.Item("home_team_name") & " v " & vbCr
                                        ToolTip = ToolTip & SkittlesTeamName(intI)
                                    End If
                                End If
                        End Select
                        gridTeams.Rows(gRow).Cells(gCol).ToolTip = ToolTip
                        gridTeams.Rows(gRow + 1).Cells(gCol).ToolTip = ToolTip
                        Exit For
                    End If
                Next
            End While
            objGlobals.close_connection()

        Next

    End Sub

    Sub grid_load_fixtures_snooker()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim intI As Integer
        Dim gRow As Integer
        Dim gCol As Integer
        Dim ThisDate As Date
        Dim ThisLeague As String
        Dim ToolTip As String

        gCol = 12
        For intI = 1 To SnookerTeamCount
            gCol = gCol + 1
            strSQL = "SELECT * FROM clubs.vw_fixtures WHERE league = '" & SnookerDivision(intI) & "' AND (home_team_name = '" & SnookerTeamName(intI) & "' OR away_team_name = '" & SnookerTeamName(intI) & "' OR ISNUMERIC(home_team) = 0) ORDER BY week_number"
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
                            Case SnookerTeamName(intI)  'Home Fixture
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
                                    ToolTip = ToolTip & SnookerTeamName(intI) & vbCr
                                    ToolTip = ToolTip & "BYE WEEK"
                                ElseIf myDataReader.Item("home_result") = "0 - 0" Then
                                    ToolTip = ToolTip & SnookerTeamName(intI) & " v " & vbCr
                                    ToolTip = ToolTip & myDataReader.Item("away_team_name")
                                End If
                                'Case "OPEN"              'Open week
                                '    gridTeams.Rows(gRow).Cells(gCol).Text = "OPEN WEEK"
                                '    ToolTip = ToolTip & SnookerTeamName(intI) & vbCr
                                '    ToolTip = ToolTip & "OPEN WEEK"
                            Case Else
                                If IsNumeric(myDataReader.Item("home_team")) = False Then
                                    'open week
                                    gridTeams.Rows(gRow).Cells(gCol).Text = myDataReader.Item("home_team_name")
                                    ToolTip = ToolTip & SnookerTeamName(intI) & vbCr
                                    ToolTip = ToolTip & myDataReader.Item("home_team_name")
                                Else
                                    'Away Fixture
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
                                        ToolTip = ToolTip & SnookerTeamName(intI) & vbCr
                                        ToolTip = ToolTip & "BYE WEEK"
                                    ElseIf myDataReader.Item("away_result") = "0 - 0" Then
                                        ToolTip = ToolTip & myDataReader.Item("home_team_name") & " v " & vbCr
                                        ToolTip = ToolTip & SnookerTeamName(intI)
                                    End If
                                End If
                        End Select
                        gridTeams.Rows(gRow).Cells(gCol).ToolTip = ToolTip
                        gridTeams.Rows(gRow + 1).Cells(gCol).ToolTip = ToolTip
                        Exit For
                    End If
                Next
            End While
            objGlobals.close_connection()

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
                    If .Cells(0).Text = "&nbsp;" And .Cells(2).Text = "&nbsp;" And .Cells(3).Text = "&nbsp;" And .Cells(4).Text = "&nbsp;" And .Cells(5).Text = "&nbsp;" And .Cells(6).Text = "&nbsp;" And .Cells(7).Text = "&nbsp;" And .Cells(8).Text = "&nbsp;" And .Cells(9).Text = "&nbsp;" And .Cells(10).Text = "&nbsp;" And .Cells(11).Text = "&nbsp;" And .Cells(12).Text = "&nbsp;" And .Cells(13).Text = "&nbsp;" And .Cells(14).Text = "&nbsp;" And .Cells(15).Text = "&nbsp;" And .Cells(16).Text = "&nbsp;" And .Cells(17).Text = "&nbsp;" Then
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
            Select Case Left(gridTeams.Rows(1).Cells(gCol).Text, 4)
                Case "CRIB"
                    gridTeams.Rows(0).Cells(gCol).BackColor = Yellow
                    gridTeams.Rows(1).Cells(gCol).BackColor = Yellow
                    gridTeams.Rows(2).Cells(gCol).BackColor = Yellow
                Case "SKIT"
                    gridTeams.Rows(0).Cells(gCol).BackColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)
                    gridTeams.Rows(1).Cells(gCol).BackColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)
                    gridTeams.Rows(2).Cells(gCol).BackColor = System.Drawing.Color.FromArgb(&H66, &H99, &HFF)
                Case "SNOO"
                    gridTeams.Rows(0).Cells(gCol).BackColor = System.Drawing.Color.FromArgb(&H0, &HCC, &H66)
                    gridTeams.Rows(1).Cells(gCol).BackColor = System.Drawing.Color.FromArgb(&H0, &HCC, &H66)
                    gridTeams.Rows(2).Cells(gCol).BackColor = System.Drawing.Color.FromArgb(&H0, &HCC, &H66)
            End Select
        Next

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

        ddlVenues.Text = lblClub.Text
    End Sub

    Protected Sub ddlVenues_TextChanged(sender As Object, e As System.EventArgs) Handles ddlVenues.TextChanged
        Response.Redirect("~/Clubs/Club Fixtures.aspx?Club=" & ddlVenues.Text)
    End Sub

End Class
