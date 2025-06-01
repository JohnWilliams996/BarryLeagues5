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
    Private GroupName As String

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
        objGlobals.LeagueSelected = Request.QueryString("League")
        GroupName = Request.QueryString("Group")
        If Not IsPostBack Then
            load_options(gridOptions)
            lblNoDraw.Visible = False
            lblARCup.Text = "ALAN ROSSER CUP GROUPS"
            gridResults.ViewStateMode = False
            If GroupName = "ALL GROUPS" Then
                load_group_table("GROUP A", gridGroupATable)
                load_group_table("GROUP B", gridGroupBTable)
                load_group_table("GROUP C", gridGroupCTable)
                load_group_table("GROUP D", gridGroupDTable)
                load_group_table("GROUP E", gridGroupETable)
                load_group_table("GROUP F", gridGroupFTable)
                load_group_table("GROUP G", gridGroupGTable)
                load_group_table("GROUP H", gridGroupHTable)

                load_group_fixtures("GROUP A", gridGroupAFixtures, lblGroupAFixtures)
                load_group_fixtures("GROUP B", gridGroupBFixtures, lblGroupBFixtures)
                load_group_fixtures("GROUP C", gridGroupCFixtures, lblGroupCFixtures)
                load_group_fixtures("GROUP D", gridGroupDFixtures, lblGroupDFixtures)
                load_group_fixtures("GROUP E", gridGroupEFixtures, lblGroupEFixtures)
                load_group_fixtures("GROUP F", gridGroupFFixtures, lblGroupFFixtures)
                load_group_fixtures("GROUP G", gridGroupGFixtures, lblGroupGFixtures)
                load_group_fixtures("GROUP H", gridGroupHFixtures, lblGroupHFixtures)
            ElseIf Left(GroupName, 5) = "GROUP" Then
                lblGroupATable.Text = GroupName & " TABLE"
                lblGroupBTable.Visible = False
                lblGroupCTable.Visible = False
                lblGroupDTable.Visible = False
                lblGroupETable.Visible = False
                lblGroupFTable.Visible = False
                lblGroupGTable.Visible = False
                lblGroupHTable.Visible = False
                lblGroupBFixtures.Visible = False
                lblGroupCFixtures.Visible = False
                lblGroupDFixtures.Visible = False
                lblGroupEFixtures.Visible = False
                lblGroupFFixtures.Visible = False
                lblGroupGFixtures.Visible = False
                lblGroupHFixtures.Visible = False
                load_group_table(GroupName, gridGroupATable)
                load_group_fixtures(GroupName, gridGroupAFixtures, lblGroupAFixtures)
            ElseIf GroupName = "PLAYOFFS" Then
                load_playoffs()
            End If
        End If
    End Sub

    Sub load_options(ByRef inGrid As GridView)
        dt = New DataTable
        dt.Columns.Add(New DataColumn("Comp Name", GetType(System.String)))

        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        strSQL = "EXEC mens_skit.sp_get_options_AR"
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            dr = dt.NewRow
            dr("Comp Name") = myDataReader.Item("Comp")
            dt.Rows.Add(dr)
        End While
        inGrid.DataSource = dt
        inGrid.DataBind()
    End Sub

    Sub load_playoffs()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim LastRound As Integer = 0
        Dim PrelimCount As Integer = 0
        dt = New DataTable

        lblARCup.Text = "ALAN ROSSER CUP PLAYOFFS"

        lblGroupAFixtures.Visible = False
        lblGroupBFixtures.Visible = False
        lblGroupCFixtures.Visible = False
        lblGroupDFixtures.Visible = False
        lblGroupEFixtures.Visible = False
        lblGroupFFixtures.Visible = False
        lblGroupGFixtures.Visible = False
        lblGroupHFixtures.Visible = False

        lblGroupATable.Visible = False
        lblGroupBTable.Visible = False
        lblGroupCTable.Visible = False
        lblGroupDTable.Visible = False
        lblGroupETable.Visible = False
        lblGroupFTable.Visible = False
        lblGroupGTable.Visible = False
        lblGroupHTable.Visible = False

        gridGroupAFixtures.Visible = False
        gridGroupBFixtures.Visible = False
        gridGroupCFixtures.Visible = False
        gridGroupDFixtures.Visible = False
        gridGroupEFixtures.Visible = False
        gridGroupFFixtures.Visible = False
        gridGroupGFixtures.Visible = False
        gridGroupHFixtures.Visible = False

        gridGroupATable.Visible = False
        gridGroupBTable.Visible = False
        gridGroupCTable.Visible = False
        gridGroupDTable.Visible = False
        gridGroupETable.Visible = False
        gridGroupFTable.Visible = False
        gridGroupGTable.Visible = False
        gridGroupHTable.Visible = False

        'add header row
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Match", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Date", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Venue", GetType(System.String)))

        strSQL = "EXEC mens_skit.sp_get_draw '" & objGlobals.current_season & "','ALAN ROSSER CUP'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        gridResults.DataSource = myDataReader
        gridResults.DataBind()


        If gridResults.Rows.Count = 3 Then
            lblNoDraw.Visible = True
            gridResults.Visible = False
        Else
            lblNoDraw.Visible = False
            gridResults.Visible = True
        End If
    End Sub

    Sub load_group_table(ByRef inGroup As String, ByRef inGrid As GridView)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        dt = New DataTable

        'add header row
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Pos", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Pld", GetType(System.String)))
        dt.Columns.Add(New DataColumn("W", GetType(System.String)))
        dt.Columns.Add(New DataColumn("D", GetType(System.String)))
        dt.Columns.Add(New DataColumn("L", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Pts", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Rolls", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Pins", GetType(System.String)))

        strSQL = "EXEC mens_skit.sp_get_group_table_AR '" + inGroup + "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            dr = dt.NewRow
            dr("Pos") = myDataReader.Item(0)
            dr("Team") = myDataReader.Item(1)
            dr("Pld") = myDataReader.Item(2)
            dr("W") = myDataReader.Item(3)
            dr("D") = myDataReader.Item(4)
            dr("L") = myDataReader.Item(5)
            dr("Pts") = myDataReader.Item(6)
            dr("Rolls") = myDataReader.Item(7)
            dr("Pins") = myDataReader.Item(8)
            dt.Rows.Add(dr)
        End While
        inGrid.DataSource = dt
        inGrid.DataBind()

    End Sub

    Sub load_group_fixtures(ByRef inGroup As String, ByRef inGrid As GridView, ByRef inVenue As Label)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        dt = New DataTable

        'add header row
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Date", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team1", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Result", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Team2", GetType(System.String)))
        dt.Columns.Add(New DataColumn("RollsResult", GetType(System.String)))
        dt.Columns.Add(New DataColumn("PinsResult", GetType(System.String)))
        dt.Columns.Add(New DataColumn("FixtureID", GetType(System.String)))

        strSQL = "EXEC mens_skit.sp_get_group_fixtures_AR '" + inGroup + "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            inVenue.Text = inGroup + " FIXTURES - VENUE: " + myDataReader.Item("Venue")

            dr = dt.NewRow
            dr("Date") = myDataReader.Item(0)
            dr("Team1") = myDataReader.Item(1)
            dr("Result") = myDataReader.Item(2)
            dr("Team2") = myDataReader.Item(3)
            dr("RollsResult") = myDataReader.Item(4)
            dr("PinsResult") = myDataReader.Item(5)
            dr("FixtureID") = myDataReader.Item(6)
            dt.Rows.Add(dr)
        End While
        gRow = 0
        inGrid.DataSource = dt
        inGrid.DataBind()

    End Sub

    Private Sub gridOptions_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridOptions.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If dt.Rows(gRow)(0) = "Other Competitions" Or dt.Rows(gRow)(0) = "Alan Rosser Cup" Then
                e.Row.Cells(0).ForeColor = Gray
            Else
                Dim hLink As New HyperLink
                If e.Row.Cells(0).Text = "CUP RULES" Then
                    hLink.NavigateUrl = "~/Mens_Skit/Alan Rosser Cup Rules.aspx"
                ElseIf e.Row.Cells(0).Text = "PLAYOFFS" Then
                    hLink.NavigateUrl = "~/Mens_Skit/Alan Rosser Cup.aspx?Group=PLAYOFFS"
                ElseIf InStr(e.Row.Cells(0).Text, "GROUP", CompareMethod.Text) = 0 Then
                    hLink.NavigateUrl = "~/Mens_Skit/Cup Fixtures List.aspx?Comp=" & dt.Rows(gRow)(0).ToString
                Else
                    hLink.NavigateUrl = "~/Mens_Skit/Alan Rosser Cup.aspx?Group=" & dt.Rows(gRow)(0).ToString
                End If

                hLink.Text = e.Row.Cells(0).Text
                hLink.ForeColor = Cyan
                e.Row.Cells(0).Controls.Add(hLink)
                e.Row.CssClass = "cell"
            End If
            gRow = gRow + 1
        End If
    End Sub


    Protected Sub gridResults_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResults.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
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
                Select Case e.Row.Cells(2).Text
                    Case "&lt; winner"
                        e.Row.Cells(2).ForeColor = Red
                        e.Row.Cells(3).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    Case "winner &gt;"
                        e.Row.Cells(2).ForeColor = Red
                        e.Row.Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    Case "not played"
                        e.Row.Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(2).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(3).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                        e.Row.Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                End Select
                If Left(e.Row.Cells(1).Text, 5) = "Match" Then
                    e.Row.Cells(1).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    e.Row.Cells(2).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    e.Row.Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    e.Row.Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                End If
                If Left(e.Row.Cells(3).Text, 5) = "Match" Then
                    e.Row.Cells(2).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    e.Row.Cells(3).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    e.Row.Cells(4).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                    e.Row.Cells(5).ForeColor = System.Drawing.Color.FromArgb(&H55, &H55, &H55)
                End If
            End If
        End If
    End Sub

    Protected Sub gridGroupAFixtures_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridGroupAFixtures.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If objGlobals.AdminLogin Then
                e.Row.Cells(6).Visible = True
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Mens_Skit/Admin/Fixture Result AR.aspx?ID=" & Val(dt.Rows(gRow)(6)).ToString & "&Fixture=" & dt.Rows(gRow)(1) & " v " & dt.Rows(gRow)(3) & "&Group=" & GroupName
                hLink1.Text = e.Row.Cells(6).Text
                hLink1.ForeColor = White
                e.Row.Cells(6).Controls.Add(hLink1)
            Else
                e.Row.Cells(6).Visible = False
            End If
            If e.Row.Cells(2).Text = "versus" Then
                e.Row.Cells(2).BackColor = gridGroupAFixtures.BackColor
                e.Row.Cells(2).ForeColor = LightGreen
            ElseIf e.Row.Cells(2).Text = "Postponed" Then
                e.Row.Cells(2).BackColor = gridGroupAFixtures.BackColor
                e.Row.Cells(2).ForeColor = Red
            Else
                e.Row.Cells(4).ForeColor = Red
                e.Row.Cells(5).ForeColor = Red
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridGroupBFixtures_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridGroupBFixtures.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If objGlobals.AdminLogin Then
                e.Row.Cells(6).Visible = True
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Mens_Skit/Admin/Fixture Result AR.aspx?ID=" & Val(dt.Rows(gRow)(6)).ToString & "&Fixture=" & dt.Rows(gRow)(1) & " v " & dt.Rows(gRow)(3) & "&Group=" & GroupName
                hLink1.Text = e.Row.Cells(6).Text
                hLink1.ForeColor = White
                e.Row.Cells(6).Controls.Add(hLink1)
            Else
                e.Row.Cells(6).Visible = False
            End If
            If e.Row.Cells(2).Text = "versus" Then
                e.Row.Cells(2).BackColor = gridGroupBFixtures.BackColor
                e.Row.Cells(2).ForeColor = LightGreen
            ElseIf e.Row.Cells(2).Text = "Postponed" Then
                e.Row.Cells(2).BackColor = gridGroupBFixtures.BackColor
                e.Row.Cells(2).ForeColor = Red
            Else
                e.Row.Cells(4).ForeColor = Red
                e.Row.Cells(5).ForeColor = Red
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridGroupCFixtures_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridGroupCFixtures.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If objGlobals.AdminLogin Then
                e.Row.Cells(6).Visible = True
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Mens_Skit/Admin/Fixture Result AR.aspx?ID=" & Val(dt.Rows(gRow)(6)).ToString & "&Fixture=" & dt.Rows(gRow)(1) & " v " & dt.Rows(gRow)(3) & "&Group=" & GroupName
                hLink1.Text = e.Row.Cells(6).Text
                hLink1.ForeColor = White
                e.Row.Cells(6).Controls.Add(hLink1)
            Else
                e.Row.Cells(6).Visible = False
            End If
            If e.Row.Cells(2).Text = "versus" Then
                e.Row.Cells(2).BackColor = gridGroupCFixtures.BackColor
                e.Row.Cells(2).ForeColor = LightGreen
            ElseIf e.Row.Cells(2).Text = "Postponed" Then
                e.Row.Cells(2).BackColor = gridGroupCFixtures.BackColor
                e.Row.Cells(2).ForeColor = Red
            Else
                e.Row.Cells(4).ForeColor = Red
                e.Row.Cells(5).ForeColor = Red
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridGroupDFixtures_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridGroupDFixtures.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If objGlobals.AdminLogin Then
                e.Row.Cells(6).Visible = True
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Mens_Skit/Admin/Fixture Result AR.aspx?ID=" & Val(dt.Rows(gRow)(6)).ToString & "&Fixture=" & dt.Rows(gRow)(1) & " v " & dt.Rows(gRow)(3) & "&Group=" & GroupName
                hLink1.Text = e.Row.Cells(6).Text
                hLink1.ForeColor = White
                e.Row.Cells(6).Controls.Add(hLink1)
            Else
                e.Row.Cells(6).Visible = False
            End If
            If e.Row.Cells(2).Text = "versus" Then
                e.Row.Cells(2).BackColor = gridGroupDFixtures.BackColor
                e.Row.Cells(2).ForeColor = LightGreen
            ElseIf e.Row.Cells(2).Text = "Postponed" Then
                e.Row.Cells(2).BackColor = gridGroupDFixtures.BackColor
                e.Row.Cells(2).ForeColor = Red
            Else
                e.Row.Cells(4).ForeColor = Red
                e.Row.Cells(5).ForeColor = Red
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridGroupEFixtures_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridGroupEFixtures.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If objGlobals.AdminLogin Then
                e.Row.Cells(6).Visible = True
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Mens_Skit/Admin/Fixture Result AR.aspx?ID=" & Val(dt.Rows(gRow)(6)).ToString & "&Fixture=" & dt.Rows(gRow)(1) & " v " & dt.Rows(gRow)(3) & "&Group=" & GroupName
                hLink1.Text = e.Row.Cells(6).Text
                hLink1.ForeColor = White
                e.Row.Cells(6).Controls.Add(hLink1)
            Else
                e.Row.Cells(6).Visible = False
            End If
            If e.Row.Cells(2).Text = "versus" Then
                e.Row.Cells(2).BackColor = gridGroupEFixtures.BackColor
                e.Row.Cells(2).ForeColor = LightGreen
            ElseIf e.Row.Cells(2).Text = "Postponed" Then
                e.Row.Cells(2).BackColor = gridGroupEFixtures.BackColor
                e.Row.Cells(2).ForeColor = Red
            Else
                e.Row.Cells(4).ForeColor = Red
                e.Row.Cells(5).ForeColor = Red
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridGroupFFixtures_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridGroupFFixtures.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If objGlobals.AdminLogin Then
                e.Row.Cells(6).Visible = True
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Mens_Skit/Admin/Fixture Result AR.aspx?ID=" & Val(dt.Rows(gRow)(6)).ToString & "&Fixture=" & dt.Rows(gRow)(1) & " v " & dt.Rows(gRow)(3) & "&Group=" & GroupName
                hLink1.Text = e.Row.Cells(6).Text
                hLink1.ForeColor = White
                e.Row.Cells(6).Controls.Add(hLink1)
            Else
                e.Row.Cells(6).Visible = False
            End If
            If e.Row.Cells(2).Text = "versus" Then
                e.Row.Cells(2).BackColor = gridGroupFFixtures.BackColor
                e.Row.Cells(2).ForeColor = LightGreen
            ElseIf e.Row.Cells(2).Text = "Postponed" Then
                e.Row.Cells(2).BackColor = gridGroupFFixtures.BackColor
                e.Row.Cells(2).ForeColor = Red
            Else
                e.Row.Cells(4).ForeColor = Red
                e.Row.Cells(5).ForeColor = Red
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridGroupGFixtures_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridGroupGFixtures.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If objGlobals.AdminLogin Then
                e.Row.Cells(6).Visible = True
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Mens_Skit/Admin/Fixture Result AR.aspx?ID=" & Val(dt.Rows(gRow)(6)).ToString & "&Fixture=" & dt.Rows(gRow)(1) & " v " & dt.Rows(gRow)(3) & "&Group=" & GroupName
                hLink1.Text = e.Row.Cells(6).Text
                hLink1.ForeColor = White
                e.Row.Cells(6).Controls.Add(hLink1)
            Else
                e.Row.Cells(6).Visible = False
            End If
            If e.Row.Cells(2).Text = "versus" Then
                e.Row.Cells(2).BackColor = gridGroupGFixtures.BackColor
                e.Row.Cells(2).ForeColor = LightGreen
            ElseIf e.Row.Cells(2).Text = "Postponed" Then
                e.Row.Cells(2).BackColor = gridGroupGFixtures.BackColor
                e.Row.Cells(2).ForeColor = Red
            Else
                e.Row.Cells(4).ForeColor = Red
                e.Row.Cells(5).ForeColor = Red
            End If
            gRow = gRow + 1
        End If
    End Sub

    Protected Sub gridGroupHFixtures_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridGroupHFixtures.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If objGlobals.AdminLogin Then
                e.Row.Cells(6).Visible = True
                Dim hLink1 As New HyperLink
                hLink1.NavigateUrl = "~/Mens_Skit/Admin/Fixture Result AR.aspx?ID=" & Val(dt.Rows(gRow)(6)).ToString & "&Fixture=" & dt.Rows(gRow)(1) & " v " & dt.Rows(gRow)(3) & "&Group=" & GroupName
                hLink1.Text = e.Row.Cells(6).Text
                hLink1.ForeColor = White
                e.Row.Cells(6).Controls.Add(hLink1)
            Else
                e.Row.Cells(6).Visible = False
            End If
            If e.Row.Cells(2).Text = "versus" Then
                e.Row.Cells(2).BackColor = gridGroupHFixtures.BackColor
                e.Row.Cells(2).ForeColor = LightGreen
            ElseIf e.Row.Cells(2).Text = "Postponed" Then
                e.Row.Cells(2).BackColor = gridGroupHFixtures.BackColor
                e.Row.Cells(2).ForeColor = Red
            Else
                e.Row.Cells(4).ForeColor = Red
                e.Row.Cells(5).ForeColor = Red
            End If
            gRow = gRow + 1
        End If
    End Sub



End Class
