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
    Private last_season As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("ladies_skit")
        objGlobals.CurrentUser = "ladies_skit_user"
        objGlobals.CurrentSchema = "ladies_skit."
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
        last_season = objGlobals.get_last_season

        If Not IsPostBack Then
            rbSeasons.ClearSelection()
            Dim strSQL As String
            Dim myDataReader As OleDbDataReader

            strSQL = "SELECT DISTINCT(season) FROM ladies_skit.leagues WHERE show_champions <> 'NO CHAMPION' ORDER BY season DESC"
            myDataReader = objGlobals.SQLSelect(strSQL)
            While myDataReader.Read()
                rbSeasons.Items.Add(myDataReader.Item(0))
            End While
            rbSeasons.SelectedIndex = 0
            Call load_all_honours()
        End If
    End Sub

    Sub load_all_honours()

        lblLeague.Text = rbSeasons.Text & " League Honours"
        lblCup.Text = rbSeasons.Text & " Cup Honours"


        dt = New DataTable
        dr = dt.NewRow

        dt.Columns.Add(New DataColumn("League", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Winners", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Runners-Up", GetType(System.String)))
        dt.Columns.Add(New DataColumn("High Scores Lit", GetType(System.String)))
        dt.Columns.Add(New DataColumn("High Scores Player", GetType(System.String)))
        dt.Columns.Add(New DataColumn("High Scores Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("High Scores Score", GetType(System.Int32)))

        Call load_league_honours()

        Call load_high_scores()

        dt = New DataTable
        dr = dt.NewRow

        dt.Columns.Add(New DataColumn("Cup", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Winners", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Runners-Up", GetType(System.String)))

        Call load_cup_honours()

    End Sub

    Sub load_league_honours()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

        strSQL = "SELECT * FROM ladies_skit.honours WHERE season = '" & rbSeasons.Text & "' AND type = 'L' ORDER BY league_name"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            dr = dt.NewRow
            dr("League") = myDataReader.Item("league_name") & ":"
            dr("Winners") = myDataReader.Item("league_winners")
            dr("Runners-Up") = myDataReader.Item("league_runners_up")
            dt.Rows.Add(dr)
            dt.Rows.Add(dt.NewRow)
        End While

    End Sub
    Sub load_high_scores()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

        strSQL = "SELECT * FROM ladies_skit.honours WHERE season = '" & rbSeasons.Text & "' AND type = 'H' ORDER BY hs_home_away DESC"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            dr = dt.NewRow
            dr("High Scores Lit") = myDataReader.Item("hs_home_away") & ":"
            dr("High Scores Player") = myDataReader.Item("hs_player")
            dr("High Scores Team") = myDataReader.Item("hs_team")
            dr("High Scores Score") = myDataReader.Item("hs_score")
            dt.Rows.Add(dr)
            dt.Rows.Add(dt.NewRow)
        End While
        gridLeague.DataSource = dt
        gridLeague.DataBind()

    End Sub
    Sub load_cup_honours()
        Dim HomeDrawNo As String = Nothing
        Dim AwayDrawNo As String = Nothing
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

        strSQL = "SELECT * FROM ladies_skit.honours WHERE season = '" & rbSeasons.Text & "' AND type = 'C' ORDER BY cup_name"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            dr = dt.NewRow
            dr("Cup") = myDataReader.Item("cup_name") & ":"
            dr("Winners") = myDataReader.Item("cup_winners")
            dr("Runners-Up") = myDataReader.Item("cup_runners_up")
            dt.Rows.Add(dr)
            dt.Rows.Add(dt.NewRow)
        End While
        gridCup.DataSource = dt
        gridCup.DataBind()

    End Sub

    Sub add_blank_row(ByRef inGrid As GridView)
        ' Add blank row in-between sports
        dt.Rows.Add(dt.NewRow)
        inGrid.DataSource = dt
        inGrid.DataBind()
    End Sub


    Protected Sub rbSeasons_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbSeasons.SelectedIndexChanged
        Call load_all_honours()
    End Sub
End Class
