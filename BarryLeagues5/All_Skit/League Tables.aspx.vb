Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
'Imports MySql.Data.MySqlClient

Partial Class League_Tables
    Inherits System.Web.UI.Page
    Private dt As DataTable = New DataTable
    Private dr As DataRow = Nothing
    Private gRow As Integer
    Private objGlobals As New Globals

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("all_skit")
        objGlobals.CurrentUser = "all_skit_user"
        objGlobals.CurrentSchema = "all_skit."
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
        objGlobals.LeagueSelected = Request.QueryString("League")
        lblLeague.Text = "LEAGUE: " & objGlobals.LeagueSelected
        If Not IsPostBack Then
            Call load_options(gridOptions)
        End If
    End Sub


    Sub load_options(ByRef inGrid As GridView)
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

        strSQL = "SELECT long_name,home_night,venue FROM all_skit.vw_teams WHERE league = '" & objGlobals.LeagueSelected & "' AND long_name <> 'BYE' ORDER BY long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)

        dt = New DataTable

        dt.Columns.Add(New DataColumn("Long Name", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Night", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Venue", GetType(System.String)))
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
        inGrid.DataSource = dt
        inGrid.DataBind()
        gRow = 0
    End Sub

    Sub load_table(ByRef inGrid As GridView)
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        dt = New DataTable

        'add header row
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))

        strSQL = "SELECT long_name FROM all_skit.vw_teams WHERE league = '" & objGlobals.LeagueSelected & "' AND long_name <> 'BYE' ORDER BY long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)
        gRow = 0
        Dim iRow As Integer = 0
        While myDataReader.Read()
            With inGrid
                dr = dt.NewRow
                dr("Pos") = "1"
                dr("Team") = myDataReader.Item("long_name")
                dt.Rows.Add(dr)
            End With
        End While

        inGrid.DataSource = dt
        inGrid.DataBind()
        inGrid.Visible = True
        gRow = 0
    End Sub

    Private Sub gridOptions_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridOptions.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hLink As New HyperLink
            Dim hLink2 As New HyperLink

            If dt.Rows(gRow)(0).ToString = "LEAGUE TABLE" Then
                hLink.NavigateUrl = "~/All_Skit/League Tables.aspx?League=" & objGlobals.LeagueSelected
                e.Row.Cells(0).ColumnSpan = 2
                e.Row.Cells(1).Visible = False
                'ElseIf dt.Rows(gRow)(0).ToString = "ALLFORM CUP" Or dt.Rows(gRow)(0).ToString = "HOLME TOWERS CUP" Or dt.Rows(gRow)(0).ToString = "GARY MITCHELL CUP" Then
                '    hLink.NavigateUrl = "~/All_Skit/Cup Fixtures List.aspx?League=" & objGlobals.LeagueSelected & "&Comp=" & dt.Rows(gRow)(0).ToString
                '    e.Row.Cells(0).ColumnSpan = 2
                '    e.Row.Cells(1).Visible = False
                'ElseIf dt.Rows(gRow)(0).ToString = "ALAN ROSSER CUP" Then
                '    hLink.NavigateUrl = "~/All_Skit/Alan Rosser Cup.aspx?Group=ALL GROUPS"
                '    e.Row.Cells(0).ColumnSpan = 2
                '    e.Row.Cells(1).Visible = False
            Else
                If dt.Rows(gRow)(0) = "Team" Or dt.Rows(gRow)(0) = "Competitions" Then
                    e.Row.Cells(0).Font.Size = 8
                    e.Row.Cells(0).ForeColor = Gray
                    e.Row.Cells(1).Font.Size = 8
                    e.Row.Cells(1).ForeColor = Gray
                    e.Row.Cells(2).Font.Size = 8
                    e.Row.Cells(2).ForeColor = Gray
                Else
                    e.Row.Cells(1).Font.Size = 8
                    e.Row.Cells(2).Font.Size = 8
                End If
                hLink.NavigateUrl = "~/All_Skit/Team Fixtures.aspx?League=" & objGlobals.LeagueSelected & "&Team=" & dt.Rows(gRow)(0).ToString
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


End Class
