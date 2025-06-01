Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
'Imports MySql.Data.MySqlClient

Partial Class Admin_Cup_Results
    Inherits System.Web.UI.Page
    Private dt As DataTable = New DataTable
    Private dr As DataRow = Nothing
    Private gRow As Integer
    Private objGlobals As New Globals
    Private PrelimRound As Boolean = False
    Private PlayerRequired As Boolean
    Private CompName As String
    Private MaxRound As Integer

    Private HomeTeam(10) As String
    Private HomeLeague(10) As String
    Private AwayTeam(10) As String
    Private AwayLeague(10) As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("mens_skit")
        objGlobals.CurrentUser = "mens_skit_user"
        objGlobals.CurrentSchema = "mens_skit."
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            lbComps.Items.Add("NOT AUTHORIZED")
            gridResults.Visible = False
            objGlobals.AdminLogin = False
            Call objGlobals.store_page("ATTACK FROM " & Request.Url.OriginalString, objGlobals.AdminLogin)
            Exit Sub
        End If
        If Not IsPostBack Then
            Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
            Call load_comps()
        End If
    End Sub

    Sub load_comps()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        lbComps.ClearSelection()
        strSQL = "SELECT Competition FROM mens_skit.vw_Comps ORDER BY Competition"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            lbComps.Items.Add(myDataReader.Item(0))
        End While
    End Sub

      Sub load_results()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        dt = New DataTable

        'add header row
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Match", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Date", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Venue", GetType(System.String)))

        strSQL = "EXEC mens_skit.sp_get_draw '" & objGlobals.current_season & "','" & CompName & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        gridResults.DataSource = myDataReader
        gridResults.DataBind()
        Call colour_grid()

    End Sub

    Sub colour_grid()
        With gridResults
            .Visible = True
            .Font.Size = 10
            For iRow As Integer = 0 To .Rows.Count - 1
                Select Case gridResults.Rows(iRow).Cells(2).Text
                    Case "&lt; winner"
                        .Rows(iRow).Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(3).Text = "< winner"
                        .Rows(iRow).Cells(3).ForeColor = Red
                        .Rows(iRow).Cells(3).HorizontalAlign = HorizontalAlign.Left
                        .Rows(iRow).Cells(1).ForeColor = Cyan
                        .Rows(iRow).Cells(7).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(8).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(9).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        'hide the result in the grid
                        .Rows(iRow).Cells(2).ForeColor = Black
                    Case "winner &gt;"
                        .Rows(iRow).Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(6).Text = "winner >"
                        .Rows(iRow).Cells(6).ForeColor = Red
                        .Rows(iRow).Cells(7).ForeColor = Cyan
                        .Rows(iRow).Cells(8).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(9).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        'hide the result in the grid
                        .Rows(iRow).Cells(2).ForeColor = Black
                    Case "not played"
                        .Rows(iRow).Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(4).Text = "not played"
                        .Rows(iRow).Cells(4).ForeColor = Red
                        .Rows(iRow).Cells(4).HorizontalAlign = HorizontalAlign.Center
                        .Rows(iRow).Cells(7).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(8).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        .Rows(iRow).Cells(9).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        'hide the result in the grid
                        .Rows(iRow).Cells(2).ForeColor = Black
                    Case "versus"
                        .Rows(iRow).Cells(1).ForeColor = Cyan
                        .Rows(iRow).Cells(7).ForeColor = Cyan
                        'hide the result in the grid
                        .Rows(iRow).Cells(2).ForeColor = Black
                    Case Else
                        'round header row
                End Select
            Next
        End With
    End Sub

    Protected Sub gridResults_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResults.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lb1 As Button = DirectCast(e.Row.Cells(3).Controls(0), Button)
            Dim lb2 As Button = DirectCast(e.Row.Cells(4).Controls(0), Button)
            Dim lb3 As Button = DirectCast(e.Row.Cells(5).Controls(0), Button)
            Dim lb4 As Button = DirectCast(e.Row.Cells(6).Controls(0), Button)
            If InStr(e.Row.Cells(1).Text, "Round") > 0 Or InStr(e.Row.Cells(1).Text, "Final") > 0 Or _
                InStr(e.Row.Cells(2).Text, "Round") > 0 Or InStr(e.Row.Cells(2).Text, "Final") > 0 Then
                e.Row.BackColor = System.Drawing.Color.FromArgb(&H33, &H33, &H33)
                e.Row.Cells(1).ForeColor = Tan
                e.Row.Cells(1).Font.Size = 12
                e.Row.Cells(2).ForeColor = Tan
                e.Row.Cells(2).Font.Size = 12
                e.Row.Cells(2).ColumnSpan = 4
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Left
            Else
                If e.Row.Cells(2).Text <> "versus" Then e.Row.Cells(2).ForeColor = Red
            End If
            If e.Row.Cells(0).Text = "&nbsp;" Or e.Row.Cells(1).Text = "&nbsp;" Then
                lb1.Visible = False
                lb2.Visible = False
                lb3.Visible = False
                lb4.Visible = False
            End If
            If Left(e.Row.Cells(1).Text, 3) = "BYE" And Left(e.Row.Cells(3).Text, 3) <> "BYE" Then
                lb1.Visible = False
                lb2.Visible = False
            End If
            If Left(e.Row.Cells(3).Text, 3) = "BYE" And Left(e.Row.Cells(1).Text, 3) <> "BYE" Then
                lb2.Visible = False
                lb4.Visible = False
            End If
        End If
    End Sub

    Protected Sub gridResults_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridResults.RowCommand
        Dim strSQL As String = ""
        Dim myDataReader As OleDbDataReader
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim selectedRow As GridViewRow = gridResults.Rows(index)
        Dim MatchNo As String = selectedRow.Cells(0).Text

        CompName = lbComps.SelectedValue

        Select Case e.CommandName
            Case "HomeWin"
                strSQL = "EXEC mens_skit.sp_update_comp_result '" & objGlobals.current_season & "','" & CompName & "','" & MatchNo & "','H'"
            Case "AwayWin"
                strSQL = "EXEC mens_skit.sp_update_comp_result '" & objGlobals.current_season & "','" & CompName & "','" & MatchNo & "','A'"
            Case "NotPlayed"
                strSQL = "EXEC mens_skit.sp_update_comp_result '" & objGlobals.current_season & "','" & CompName & "','" & MatchNo & "','N'"
            Case "Reset"
                strSQL = "EXEC mens_skit.sp_update_comp_result '" & objGlobals.current_season & "','" & CompName & "','" & MatchNo & "','X'"
        End Select
        myDataReader = objGlobals.SQLSelect(strSQL)

        Call objGlobals.update_fixtures_combined("mens_skit")

        write_cup_draws_FTP()

        Call load_results()
    End Sub
    Sub write_cup_draws_FTP()
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader
        Dim l_param_in_names(0) As String
        Dim l_param_in_values(0) As String
        l_param_in_names(0) = "@inComp"
        l_param_in_values(0) = IIf(Strings.Left(CompName, 12) <> "HOLME TOWERS", CompName, "HOLME TOWERS CUP") 'remove all versions of the H/Towers cup, and save as HOLME TOWERS

        strSQL = "EXEC [mens_skit].[sp_write_cup_draws_FTP] '" & l_param_in_values(0) & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        objGlobals.close_connection()
    End Sub

    Protected Sub lbComps_TextChanged(sender As Object, e As System.EventArgs) Handles lbComps.TextChanged
        CompName = lbComps.Text
        Call load_results()
    End Sub

    Protected Sub gridResults_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gridResults.SelectedIndexChanged

    End Sub
End Class
