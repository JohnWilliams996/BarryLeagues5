Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
'Imports MySql.Data.MySqlClient

Partial Class Register_Players
    Inherits System.Web.UI.Page

    Private dt As DataTable = New DataTable
    Private dr As DataRow = Nothing
    Private gRow As Integer
    Private objGlobals As New Globals


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("ladies_skit")
        objGlobals.CurrentUser = "ladies_skit_user"
        objGlobals.CurrentSchema = "ladies_skit."
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
            lblAddUpdateDelete.Text = "NOT AUTHORIZED"
            lblAddUpdateDelete.Visible = True
            btnAddPlayer.Visible = False
            btnChangePlayer.Visible = False
            ddAddLeague.Visible = False
            ddAddTeam.Visible = False
            ddChangeLeague.Visible = False
            ddChangePlayer.Visible = False
            ddChangeTeam.Visible = False
            gridDivision1.Visible = False
            gridDivision2.Visible = False
            lblAddLeague.Visible = False
            lblAddPlayer.Visible = False
            lblAddTeam.Visible = False
            lblPhone.Visible = False
            lblNewName.Visible = False
            lblRegisteredPlayers.Visible = False
            lblChangePlayerName.Visible = False
            lblChangeInstr.Visible = False
            lblChangeLeague.Visible = False
            lblChangePlayer.Visible = False
            lblChangeTeam.Visible = False

            lblChangeInstr.Visible = False
            txtAddPlayer.Visible = False
            txtChangeNewName.Visible = False
            txtPhone.Visible = False
            Call objGlobals.store_page("ATTACK FROM " & Request.Url.OriginalString, objGlobals.AdminLogin)
            Exit Sub
        End If
        SqlDataSourceDivision1.ConnectionString = Replace(objGlobals.getSQLConnectionString, "Provider", "Data Source")
        SqlDataSourceDivision2.ConnectionString = Replace(objGlobals.getSQLConnectionString, "Provider", "Data Source")
        SqlDataSourceDivision3.ConnectionString = Replace(objGlobals.getSQLConnectionString, "Provider", "Data Source")
        SqlDataSourceDivision4.ConnectionString = Replace(objGlobals.getSQLConnectionString, "Provider", "Data Source")
        If Not IsPostBack Then
            Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
            Call load_add_leagues()
            Call load_add_teams()
            Call load_change_leagues()
            Call load_change_teams()
            Call load_change_players()
            Call reset_inputs()
        End If
    End Sub

    Sub load_add_leagues()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        ddAddLeague.Items.Clear()
        strSQL = "SELECT * FROM ladies_skit.vw_leagues ORDER BY league"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddAddLeague.Items.Add(myDataReader.Item(0))
        End While
    End Sub

    Sub load_change_leagues()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        ddChangeLeague.Items.Clear()
        strSQL = "SELECT * FROM ladies_skit.vw_leagues ORDER BY league"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddChangeLeague.Items.Add(myDataReader.Item(0))
        End While
    End Sub

    Sub load_add_teams()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        ddAddTeam.Items.Clear()
        strSQL = "SELECT DISTINCT(long_name) FROM ladies_skit.vw_teams WHERE league = '" & ddAddLeague.Text & "' AND long_name <> 'BYE'"
        strSQL = strSQL + " ORDER BY long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddAddTeam.Items.Add(myDataReader.Item(0))
        End While
        Call load_add_players()
    End Sub

    Sub load_change_teams()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        ddChangeTeam.Items.Clear()
        strSQL = "SELECT DISTINCT(long_name) FROM ladies_skit.vw_teams WHERE league = '" & ddChangeLeague.Text & "' AND long_name <> 'BYE'"
        strSQL = strSQL + " ORDER BY long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddChangeTeam.Items.Add(myDataReader.Item(0))
        End While
    End Sub

    Sub reset_inputs()
        lblChangeInstr.Visible = False
        txtAddPlayer.Text = ""
        txtChangeNewName.Text = ""
        txtPhone.Text = ""
        txtAddPlayer.BackColor = White
        txtAddPlayer.ForeColor = Black
        txtAddPlayer.Focus()
    End Sub

    Protected Sub ddAddLeague_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddAddLeague.SelectedIndexChanged
        Call load_add_teams()
    End Sub

    Protected Sub ddAddTeam_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddAddTeam.SelectedIndexChanged
        Call load_add_players()
        Call reset_inputs()
    End Sub

    Protected Sub ddChangeLeague_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddChangeLeague.SelectedIndexChanged
        Call load_change_teams()
        Call load_change_players()
    End Sub

    Protected Sub ddChangeTeam_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddChangeTeam.SelectedIndexChanged
        Call load_change_players()
        Call reset_inputs()
    End Sub

    Protected Sub ddChangePlayer_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddChangePlayer.SelectedIndexChanged
        txtChangeNewName.Focus()
    End Sub

    Sub load_change_players()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        ddChangePlayer.Items.Clear()
        strSQL = "SELECT player FROM ladies_skit.vw_players WHERE league = '" & ddChangeLeague.Text & "' AND team = '" & ddChangeTeam.Text & "'"
        strSQL = strSQL + " ORDER BY Player"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddChangePlayer.Items.Add(myDataReader.Item(0))
        End While
    End Sub

    Sub load_add_players()
        gridDivision1.Visible = False
        gridDivision2.Visible = False
        gridDivision3.Visible = False
        Select Case ddAddLeague.Text
            Case "DIVISION 1"
                gridDivision1.Visible = True
                gridDivision1.DataBind()
            Case "DIVISION 2"
                gridDivision2.Visible = True
                gridDivision2.DataBind()
            Case "DIVISION 3"
                gridDivision3.Visible = True
                gridDivision3.DataBind()
        End Select
    End Sub

    Protected Sub btnAddPlayer_Click(sender As Object, e As System.EventArgs) Handles btnAddPlayer.Click
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim tempSeason As String
        tempSeason = objGlobals.get_current_season
        If Trim(txtAddPlayer.Text) = "" Then
            txtAddPlayer.Text = "NO PLAYER NAME !"
            txtAddPlayer.Focus()
            Exit Sub
        End If
        txtAddPlayer.Text = UCase(RTrim(txtAddPlayer.Text))

        'Check to see if entry already exists
        strSQL = "SELECT Phone FROM ladies_skit.vw_players WHERE league = '" + ddAddLeague.Text + "'"
        strSQL = strSQL + " AND team = '" + ddAddTeam.Text + "'"
        strSQL = strSQL + " AND player = '" + txtAddPlayer.Text + "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            txtAddPlayer.Text = txtAddPlayer.Text + " ALREADY ADDED"
            If Not IsDBNull(myDataReader.Item("phone")) Then
                txtPhone.Text = myDataReader.Item("phone")
            End If
            txtAddPlayer.Focus()
            Exit Sub
        End While

        strSQL = "INSERT INTO ladies_skit.players VALUES ("
        strSQL = strSQL + "'" + tempSeason + "',"
        strSQL = strSQL + "'" + ddAddLeague.Text + "',"
        strSQL = strSQL + "'" + ddAddTeam.Text + "',"
        strSQL = strSQL + "'" + txtAddPlayer.Text + "',"
        If Trim(txtPhone.Text) <> "" Then
            strSQL = strSQL + "'" + txtPhone.Text + "',"
        Else
            strSQL = strSQL + "NULL,"
        End If
        strSQL = strSQL & "0)"      'contact

        myDataReader = objGlobals.SQLSelect(strSQL)

        Call load_add_players()
        Call reset_inputs()
    End Sub


    Protected Sub btnChangePlayer_Click(sender As Object, e As System.EventArgs) Handles btnChangePlayer.Click
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        If Trim(txtChangeNewName.Text) = "" Or txtChangeNewName.Text = "NO PLAYER NAME !" Then
            txtChangeNewName.Text = "NO PLAYER NAME !"
            txtChangeNewName.Focus()
            Exit Sub
        End If
        txtChangeNewName.Text = UCase(txtChangeNewName.Text)
        strSQL = "EXEC ladies_skit.sp_Change_Player_Name '" & objGlobals.current_season & "','" & ddChangeLeague.Text & "','" & ddChangeTeam.Text & "','" & ddChangePlayer.Text & "','" & txtChangeNewName.Text & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        lblChangeInstr.Visible = True
        Call load_add_players()       'Refresh the players
    End Sub
End Class
