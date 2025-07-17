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
        'Call objGlobals.open_connection("clubs")
        objGlobals.CurrentUser = "clubs_user"
        objGlobals.CurrentSchema = "clubs."

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
            gridCrib1.Visible = False
            gridSkittles1.Visible = False
            gridSnooker1.Visible = False
            gridSnooker2.Visible = False
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
            chk3aSide1.Visible = False
            chk3aSide2.Visible = False
            chk3aSide3.Visible = False
            chk3aSide4.Visible = False
            chkPairs1.Visible = False
            chkPairs2.Visible = False
            chkPairs3.Visible = False
            chkPairs4.Visible = False
            chkPairs5.Visible = False
            chkPairs6.Visible = False
            chk6aSide1.Visible = False
            chk6aSide2.Visible = False
            chk6aSide3.Visible = False
            chkContact.Visible = False
            chkSingles.Visible = False
            chkStillIn3aSide.Visible = False
            chkStillInPairs.Visible = False
            chkStillInSingles.Visible = False
            lblChangeInstr.Visible = False
            txtAddPlayer.Visible = False
            txtChangeNewName.Visible = False
            txtPhone.Visible = False
            Call objGlobals.store_page("ATTACK FROM " & Request.Url.OriginalString, objGlobals.AdminLogin)
            Exit Sub
        End If
        SqlDataSourceCrib1.ConnectionString = Replace(objGlobals.getSQLConnectionString, "Provider", "Data Source")
        SqlDataSourceSkittles1.ConnectionString = Replace(objGlobals.getSQLConnectionString, "Provider", "Data Source")
        'SqlDataSourceSkittles2.ConnectionString = objGlobals.getSQLConnectionString
        SqlDataSourceSnooker1.ConnectionString = Replace(objGlobals.getSQLConnectionString, "Provider", "Data Source")
        SqlDataSourceSnooker2.ConnectionString = Replace(objGlobals.getSQLConnectionString, "Provider", "Data Source")
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
        strSQL = "SELECT * FROM clubs.vw_leagues ORDER BY league"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddAddLeague.Items.Add(myDataReader.Item(0))
        End While
        objGlobals.close_connection()

    End Sub

    Sub load_change_leagues()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        ddChangeLeague.Items.Clear()
        strSQL = "SELECT * FROM clubs.vw_leagues ORDER BY league"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddChangeLeague.Items.Add(myDataReader.Item(0))
        End While
        objGlobals.close_connection()

    End Sub

    Sub load_add_teams()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        ddAddTeam.Items.Clear()
        strSQL = "SELECT DISTINCT(long_name) FROM clubs.vw_teams WHERE league = '" & ddAddLeague.Text & "' AND long_name <> 'BYE'"
        strSQL = strSQL + " ORDER BY long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddAddTeam.Items.Add(myDataReader.Item(0))
        End While
        objGlobals.close_connection()

        Call load_add_players()
    End Sub

    Sub load_change_teams()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        ddChangeTeam.Items.Clear()
        strSQL = "SELECT DISTINCT(long_name) FROM clubs.vw_teams WHERE league = '" & ddChangeLeague.Text & "' AND long_name <> 'BYE'"
        strSQL = strSQL + " ORDER BY long_name"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddChangeTeam.Items.Add(myDataReader.Item(0))
        End While
        objGlobals.close_connection()

    End Sub

    Sub reset_inputs()
        lblChangeInstr.Visible = False
        txtAddPlayer.Text = ""
        txtChangeNewName.Text = ""
        txtPhone.Text = ""
        txtAddPlayer.BackColor = White
        txtAddPlayer.ForeColor = Black
        chkSingles.Checked = False
        chkContact.Checked = False
        chkPairs1.Checked = False
        chkPairs2.Checked = False
        chkPairs3.Checked = False
        chkPairs4.Checked = False
        chkPairs5.Checked = False
        chkPairs6.Checked = False
        chk3aSide1.Checked = False
        chk3aSide2.Checked = False
        chk3aSide3.Checked = False
        chk3aSide4.Checked = False
        chk6aSide1.Checked = False
        chk6aSide2.Checked = False
        chk6aSide3.Checked = False
        chkStillInSingles.Checked = False
        chkStillInPairs.Checked = False
        chkStillIn3aSide.Checked = False
        txtAddPlayer.Focus()
    End Sub


    Protected Sub ddAddLeague_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddAddLeague.SelectedIndexChanged
        Call load_add_teams()
        Call reset_inputs()
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
        strSQL = "SELECT player FROM clubs.vw_players WHERE league = '" & ddChangeLeague.Text & "' AND team = '" & ddChangeTeam.Text & "'"
        strSQL = strSQL + " ORDER BY Player"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            ddChangePlayer.Items.Add(myDataReader.Item(0))
        End While
        objGlobals.close_connection()

    End Sub

    Sub load_add_players()
        gridCrib1.Visible = False
        gridSkittles1.Visible = False
        gridSnooker1.Visible = False
        gridSnooker2.Visible = False
        chkSingles.Visible = False
        chkPairs1.Visible = False
        chkPairs2.Visible = False
        chkPairs3.Visible = False
        chkPairs4.Visible = False
        chkPairs5.Visible = False
        chkPairs6.Visible = False
        chk3aSide1.Visible = False
        chk3aSide2.Visible = False
        chk3aSide3.Visible = False
        chk3aSide4.Visible = False
        chkStillInSingles.Visible = False
        chkStillInPairs.Visible = False
        chkStillIn3aSide.Visible = False
        Select Case ddAddLeague.Text
            Case "CRIB DIVISION 1"
                chkPairs1.Visible = True
                chkPairs2.Visible = True
                chkPairs3.Visible = True
                chkPairs4.Visible = True
                chkPairs5.Visible = True
                chkPairs6.Visible = True
                chkStillInPairs.Visible = True
                chkContact.Visible = True
                gridCrib1.Visible = True
                gridCrib1.DataBind()
            Case "SKITTLES DIVISION 1"
                chkContact.Visible = True
                chk6aSide1.Visible = True
                chk6aSide2.Visible = True
                chk6aSide3.Visible = True
                gridSkittles1.Visible = True
                gridSkittles1.DataBind()
            Case "SNOOKER DIVISION 1"
                chkSingles.Visible = True
                chkContact.Visible = True
                chkPairs1.Visible = True
                chkPairs2.Visible = True
                chkPairs3.Visible = True
                chkPairs4.Visible = True
                chkPairs5.Visible = True
                chkPairs6.Visible = True
                chk3aSide1.Visible = True
                chk3aSide2.Visible = True
                chk3aSide3.Visible = True
                chk3aSide4.Visible = True
                chkStillInSingles.Visible = True
                chkStillInPairs.Visible = True
                chkStillIn3aSide.Visible = True
                gridSnooker1.Visible = True
                gridSnooker1.DataBind()
            Case "SNOOKER DIVISION 2"
                chkSingles.Visible = True
                chkContact.Visible = True
                chkPairs1.Visible = True
                chkPairs2.Visible = True
                chkPairs3.Visible = True
                chkPairs4.Visible = True
                chkPairs5.Visible = True
                chkPairs6.Visible = True
                chk3aSide1.Visible = True
                chk3aSide2.Visible = True
                chk3aSide3.Visible = True
                chk3aSide4.Visible = True
                chkStillInSingles.Visible = True
                chkStillInPairs.Visible = True
                chkStillIn3aSide.Visible = True
                gridSnooker2.Visible = True
                gridSnooker2.DataBind()
        End Select


    End Sub

    Protected Sub btnAddPlayer_Click(sender As Object, e As System.EventArgs) Handles btnAddPlayer.Click
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim tempSeason As String
        Dim Added As Boolean = False
        tempSeason = objGlobals.get_current_season
        If Trim(txtAddPlayer.Text) = "" Then
            txtAddPlayer.Text = "NO PLAYER NAME !"
            txtAddPlayer.Focus()
            Exit Sub
        End If
        txtAddPlayer.Text = UCase(RTrim(txtAddPlayer.Text))

        'Check to see if entry already exists
        strSQL = "SELECT Phone FROM clubs.vw_players WHERE league = '" + ddAddLeague.Text + "'"
        strSQL = strSQL + " AND team = '" + ddAddTeam.Text + "'"
        strSQL = strSQL + " AND player = '" + txtAddPlayer.Text + "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read
            Added = True
            txtAddPlayer.Text = txtAddPlayer.Text + " ALREADY ADDED"
            If Not IsDBNull(myDataReader.Item("phone")) Then
                txtPhone.Text = myDataReader.Item("phone")
            End If
            txtAddPlayer.Focus()
        End While
        objGlobals.close_connection()
        If Added Then Exit Sub

        strSQL = "INSERT INTO clubs.players VALUES ("
        strSQL = strSQL + "'" + tempSeason + "',"
        strSQL = strSQL + "'" + ddAddLeague.Text + "',"
        strSQL = strSQL + "'" + ddAddTeam.Text + "',"
        strSQL = strSQL + "'" + txtAddPlayer.Text + "',"
        If Trim(txtPhone.Text) <> "" Then
            strSQL = strSQL + "'" + txtPhone.Text + "',"
        Else
            strSQL = strSQL + "NULL,"
        End If
        If chkSingles.Checked Then strSQL = strSQL & "1," Else strSQL = strSQL & "0,"

        If chkPairs1.Checked Then strSQL = strSQL & "1," Else strSQL = strSQL & "0,"
        If chkPairs2.Checked Then strSQL = strSQL & "1," Else strSQL = strSQL & "0,"
        If chkPairs3.Checked Then strSQL = strSQL & "1," Else strSQL = strSQL & "0,"
        If chkPairs4.Checked Then strSQL = strSQL & "1," Else strSQL = strSQL & "0,"
        If chkPairs5.Checked Then strSQL = strSQL & "1," Else strSQL = strSQL & "0,"
        If chkPairs6.Checked Then strSQL = strSQL & "1," Else strSQL = strSQL & "0,"

        If chk3aSide1.Checked Then strSQL = strSQL & "1," Else strSQL = strSQL & "0,"
        If chk3aSide2.Checked Then strSQL = strSQL & "1," Else strSQL = strSQL & "0,"
        If chk3aSide3.Checked Then strSQL = strSQL & "1," Else strSQL = strSQL & "0,"
        If chk3aSide4.Checked Then strSQL = strSQL & "1," Else strSQL = strSQL & "0,"

        If chkStillInSingles.Checked Then strSQL = strSQL & "1," Else strSQL = strSQL & "0,"
        If chkStillInPairs.Checked Then strSQL = strSQL & "1," Else strSQL = strSQL & "0,"
        If chkStillIn3aSide.Checked Then strSQL = strSQL & "1," Else strSQL = strSQL & "0,"

        If Not chk6aSide1.Checked And Not chk6aSide2.Checked And Not chk6aSide3.Checked Then
            If chkContact.Checked Then strSQL = strSQL & "1,0,0,0)" Else strSQL = strSQL & "0,0,0,0)" 'added contact ?
        Else
            If chkContact.Checked Then strSQL = strSQL & "1," Else strSQL = strSQL & "0," 'added contact ?
            If chk6aSide1.Checked Then
                strSQL = strSQL & "1,0,0)"
            ElseIf chk6aSide2.Checked Then
                strSQL = strSQL & "0,1,0)"
            ElseIf chk6aSide3.Checked Then
                strSQL = strSQL & "0,0,1)"
            End If
        End If

        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

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
        strSQL = "EXEC clubs.sp_change_player_name '" & objGlobals.current_season & "','" & ddChangeLeague.Text & "','" & ddChangeTeam.Text & "','" & ddChangePlayer.Text & "','" & txtChangeNewName.Text & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        objGlobals.close_connection()

        lblChangeInstr.Visible = True
        Call load_add_players()       'Refresh the players
    End Sub
End Class
