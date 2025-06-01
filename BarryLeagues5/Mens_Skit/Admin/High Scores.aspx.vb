
Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
'Imports MySql.Data.MySqlClient

Partial Class Admin_High_Scores
    Inherits System.Web.UI.Page
    Private dt As DataTable = New DataTable
    Private dr As DataRow = Nothing
    Private gRow As Integer
    Private objGlobals As New Globals

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("mens_skit")
        objGlobals.CurrentUser = "mens_skit_user"
        objGlobals.CurrentSchema = "mens_skit."
        SqlDataSource1.ConnectionString = objGlobals.getSQLConnectionString
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
            lblAdd.Text = "NOT AUTHORIZED"
            GridView2.Visible = False
            btnAddHS.Visible = False
            lblHomeAway.Visible = False
            lblLeague.Visible = False
            lblPrint.Visible = False
            lblScore.Visible = False
            lblTeam.Visible = False
            lblPlayer.Visible = False
            txtScore.Visible = False
            ddLeague.Visible = False
            ddPlayer.Visible = False
            ddTeam.Visible = False
            ddPrint.Visible = False
            ddHomeAway.Visible = False
            Call objGlobals.store_page("ATTACK FROM " & Request.Url.OriginalString, objGlobals.AdminLogin)
            Exit Sub
        End If
        If Not IsPostBack Then
            Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
            ddPrint.Items.Add("Yes")
            ddPrint.Items.Add("No")
            Call load_leagues()
            Call load_home_away()
            Call load_teams()
            Call load_players()
        End If
    End Sub

    Sub load_leagues()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        ddLeague.Items.Clear()
        strSQL = "SELECT * FROM mens_skit.vw_leagues ORDER BY league"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddLeague.Items.Add(myDataReader.Item(0))
        End While
    End Sub

    Sub load_home_away()
        ddHomeAway.Items.Clear()
        ddPlayer.Visible = True
        lblPlayer.Visible = True
        ddHomeAway.Items.Add("SKIT")
    End Sub

    Sub load_teams()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        ddTeam.Items.Clear()
        strSQL = "SELECT DISTINCT(long_name) FROM mens_skit.vw_teams WHERE league = '" & ddLeague.Text & "' AND long_name <> 'BYE'"
        strSQL = strSQL + " ORDER BY long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddTeam.Items.Add(myDataReader.Item(0))
        End While
    End Sub

    Sub load_players()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        ddPlayer.Items.Clear()
        strSQL = "SELECT player FROM mens_skit.vw_players WHERE league = '" & ddLeague.Text & "' AND team = '" & ddTeam.Text & "'"
        strSQL = strSQL + " ORDER BY Player"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddPlayer.Items.Add(myDataReader.Item(0))
        End While
    End Sub

    Protected Sub ddLeague_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddLeague.SelectedIndexChanged
        Call load_home_away()
        Call load_teams()
        Call load_players()
    End Sub

    Protected Sub ddTeam_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddTeam.SelectedIndexChanged
        Call load_players()
    End Sub

    Protected Sub btnAddHS_Click(sender As Object, e As System.EventArgs) Handles btnAddHS.Click
        Dim tempSeason As String
        tempSeason = objGlobals.get_current_season

        If txtScore.Text = "" Then
            MsgBox("No Score Entered")
            Exit Sub
        End If

        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        strSQL = "INSERT INTO mens_skit.high_scores VALUES ("
        strSQL = strSQL + "'" + tempSeason + "',"
        strSQL = strSQL + "'" + ddLeague.Text + "',"
        strSQL = strSQL + "'" + ddHomeAway.Text + "',"
        strSQL = strSQL + "'" + ddPlayer.Text + "',"
        strSQL = strSQL + "'" + ddTeam.Text + "',"
        strSQL = strSQL + txtScore.Text
        If ddPrint.Text = "Yes" Then
            strSQL = strSQL + ",1)"
        Else
            strSQL = strSQL + ",0)"
        End If
        myDataReader = objGlobals.SQLSelect(strSQL)
        Response.Redirect("~/Mens_Skit/Admin/High Scores.aspx")
    End Sub

End Class
