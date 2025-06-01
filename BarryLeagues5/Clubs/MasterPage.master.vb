Imports System.Data.OleDb
Imports System.Drawing.Color
Imports System.Data
Imports System.Web

'Imports MySql.Data.MySqlClient

Public Class MasterPage
    Inherits System.Web.UI.MasterPage

    Private dt As DataTable
    Private dr As DataRow
    Private gRow As Integer
    Private objGlobals As New Globals


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("clubs")
        objGlobals.CurrentUser = "clubs_user"
        objGlobals.CurrentSchema = "clubs."

        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        If Not IsPostBack Then
            SeasonLbl.Text = objGlobals.get_current_season
            Call load_last_updated()
            txtPassword.Visible = False
            btnGo.Visible = False
            btnCancel.Visible = False
            lblAdmin.Visible = False
            If objGlobals.AdminLogin Then
                btnLogout.Visible = True
                HighScoresHL.Visible = True
                DBImportHL.Visible = True
                CupResultsHL.Visible = True
                RegiserHL.Visible = True
                ActivityHL.Visible = True
                MensSkitHL.Visible = True
                LadiesSkitHL.Visible = True
                AllSkitHL.Visible = True
                lblAdmin.Visible = True
                lblAdmin.Text = "Admin Logged-in" '
            Else
                btnLogout.Visible = False
                HighScoresHL.Visible = False
                DBImportHL.Visible = False
                CupResultsHL.Visible = False
                RegiserHL.Visible = False
                ActivityHL.Visible = False
                LadiesSkitHL.Visible = False
                MensSkitHL.Visible = False
                AllSkitHL.Visible = False
            End If
            HomeHL.NavigateUrl = "~/Clubs/Default.aspx?Week=" & objGlobals.GetCurrentWeek
            MensSkitHL.NavigateUrl = "~/Mens_Skit/Default.aspx?Week=" & objGlobals.GetCurrentWeek
            LadiesSkitHL.NavigateUrl = "~/Ladies_Skit/Default.aspx?Week=" & objGlobals.GetCurrentWeek
        End If
    End Sub


    Sub load_last_updated()
        Dim yyyy As Integer
        Dim mm As Integer
        Dim dd As Integer
        Dim t_ime As String
        Dim strSQL As String
        Dim myDataReader As OleDbDataReader

        strSQL = "SELECT date_time_changed FROM clubs.last_changed"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If Not myDataReader.HasRows Then
            objGlobals.close_connection()
            Exit Sub
        End If

        While myDataReader.Read()
            yyyy = Val(Left(myDataReader.Item(0), 4))
            mm = Val(Mid(myDataReader.Item(0), 5, 2))
            dd = Val(Mid(myDataReader.Item(0), 7, 2))
            t_ime = Mid(myDataReader.Item(0), 9, 2) & ":" & Right(myDataReader.Item(0), 2)
            Dim thisDate1 = New DateTime(yyyy, mm, dd)
            LastUpdateLbl.Text = thisDate1.ToString("dddd ") & objGlobals.AddSuffix(dd) & thisDate1.ToString(" MMMM") & " " & t_ime
        End While
        objGlobals.close_connection()

    End Sub

    Protected Sub btnAdmin_Click(sender As Object, e As System.EventArgs) Handles btnAdmin.Click
        lblAdmin.Visible = True
        lblAdmin.Text = "Admin Password : "
        txtPassword.Visible = True
        btnGo.Visible = True
        btnCancel.Visible = True
        txtPassword.Focus()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        lblAdmin.Visible = False
        txtPassword.Visible = False
        btnGo.Visible = False
        btnCancel.Visible = False

        'create a cookie with a date earlier than today (deletes it)
        Dim aCookie As New HttpCookie("lastVisit")
        aCookie.Value = DateTime.Now.ToString
        aCookie.Expires = DateTime.Now.AddDays(-1)
        Response.Cookies.Add(aCookie)

    End Sub

    Protected Sub btnGo_Click(sender As Object, e As System.EventArgs) Handles btnGo.Click
        Dim appSettings As NameValueCollection = ConfigurationManager.AppSettings

        If txtPassword.Text <> appSettings(4) Then
            lblAdmin.Text = "*** INVALID ***"
            objGlobals.AdminLogin = False
            txtPassword.Focus()
        Else
            objGlobals.AdminLogin = True
            lblAdmin.Text = "Admin Logged-in"
            txtPassword.Visible = False
            btnGo.Visible = False
            btnCancel.Visible = False
            btnLogout.Visible = True
            CupResultsHL.Visible = True
            HighScoresHL.Visible = True
            DBImportHL.Visible = True
            RegiserHL.Visible = True
            ActivityHL.Visible = True
            MensSkitHL.Visible = True
            LadiesSkitHL.Visible = True
            AllSkitHL.Visible = True

            'create a cookie
            Dim aCookie As New HttpCookie("lastVisit")
            aCookie.Value = DateTime.Now.ToString
            aCookie.Expires = DateTime.Now.AddDays(1)
            Response.Cookies.Add(aCookie)
            'Refresh the home page
            Response.Redirect("~/Clubs/Default.aspx?Week=" & objGlobals.GetCurrentWeek)
        End If

    End Sub

    Protected Sub txtPassword_TextChanged(sender As Object, e As System.EventArgs) Handles txtPassword.TextChanged
        ' force the GO button to be clicked on CR
        Call btnGo_Click(txtPassword, e)
    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As System.EventArgs) Handles btnLogout.Click
        'create a cookie with a date earlier than today (deletes it)
        Dim aCookie As New HttpCookie("lastVisit")
        aCookie.Value = DateTime.Now.ToString
        aCookie.Expires = DateTime.Now.AddDays(-1)
        Response.Cookies.Add(aCookie)

        btnAdmin.Visible = True
        lblAdmin.Visible = False
        btnLogout.Visible = False
        CupResultsHL.Visible = False
        HighScoresHL.Visible = False
        DBImportHL.Visible = False
        RegiserHL.Visible = False
        ActivityHL.Visible = False
        MensSkitHL.Visible = False
        AllSkitHL.Visible = False

        'Refresh the home page
        Response.Redirect("~/Clubs/Default.aspx?Week=" & objGlobals.GetCurrentWeek)

    End Sub

    Protected Sub btnTeams_Click(sender As Object, e As System.EventArgs) Handles btnTeams.Click
        If Not gridTeams.Visible Then
            Call load_teams()
            gridTeams.Visible = True
        Else
            gridTeams.Visible = False
        End If
    End Sub

    Sub load_teams()
        Dim Crib1Team(20) As String
        Dim Skittles1Team(20) As String
        Dim Snooker1Team(20) As String
        Dim Snooker2Team(20) As String
        Dim Crib1TeamCount As Integer = 0
        Dim Skittles1TeamCount As Integer = 0
        Dim Snooker1TeamCount As Integer = 0
        Dim Snooker2TeamCount As Integer = 0

        dt = New DataTable
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Crib1Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Skittles1Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Snooker1Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Snooker2Team", GetType(System.String)))
        'hard-code the teams to make the load quicker !! - use Excel spreasheet call MY TEAMS FOR WEBSITE.xlsx and SQL script MY TEAMS FOR WEBSITE.sql 
        dt.Rows.Add("BARRY TOWN FC", "ALLSORTS", "CADOXTON CONS 'B'", "BLUE MONKEY")
        dt.Rows.Add("BILLY CRIBBERS", "BARRIES BOYS", "CADOXTON CONS 'Z'", "HARBOUR 'B'")
        dt.Rows.Add("CADOXTON CONS", "BARRY TOWN FC", "HARBOUR 'A'", "LIBERATORS")
        dt.Rows.Add("LIBERALS", "BREAKAWAYS", "SERVICES SOCIAL 'B'", "MARKET STREET 'C'")
        dt.Rows.Add("MOORS BOYS", "CAMBRIAN FLYERS", "WEST END 'C'", "MARKET STREET 'D'")
        dt.Rows.Add("RHOOSE SOCIAL", "J J WINDOWS", "WYNDHAM CONS 'A'", "SAVOY 'B'")
        dt.Rows.Add("WEST END", "RHOOSE SOCIAL 'B'", "WYNDHAM WANDERERS", "WEST END 'B'")
        dt.Rows.Add("WYNDHAM CONS", "RUGBY CLUB", "", "WYNDHAM CONS 'D'")
        dt.Rows.Add("", "SEA VIEW EXILES", "", "")
        dt.Rows.Add("", "WEST END 'C'", "", "")
        dt.Rows.Add("", "WEST END 'D'", "", "")


        gridTeams.DataSource = dt
        gridTeams.DataBind()

    End Sub

    Protected Sub gridTeams_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTeams.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hLink1 As New HyperLink
            hLink1.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=CRIB DIVISION 1&Team=" & Replace(e.Row.Cells(0).Text, "&#39;", Chr(34))
            hLink1.ForeColor = Cyan
            hLink1.Text = e.Row.Cells(0).Text
            e.Row.Cells(0).Controls.Add(hLink1)

            Dim hLink2 As New HyperLink
            hLink2.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=SKITTLES DIVISION 1&Team=" & Replace(e.Row.Cells(1).Text, "&#39;", Chr(34))
            hLink2.ForeColor = Cyan
            hLink2.Text = e.Row.Cells(1).Text
            e.Row.Cells(1).Controls.Add(hLink2)

            Dim hLinK3 As New HyperLink
            hLinK3.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=SNOOKER DIVISION 1&Team=" & Replace(e.Row.Cells(2).Text, "&#39;", Chr(34))
            hLinK3.ForeColor = Cyan
            hLinK3.Text = e.Row.Cells(2).Text
            e.Row.Cells(2).Controls.Add(hLinK3)

            Dim hLinK4 As New HyperLink
            hLinK4.NavigateUrl = "~/Clubs/Team Fixtures.aspx?League=SNOOKER DIVISION 2&Team=" & Replace(e.Row.Cells(3).Text, "&#39;", Chr(34))
            hLinK4.ForeColor = Cyan
            hLinK4.Text = e.Row.Cells(3).Text
            e.Row.Cells(3).Controls.Add(hLinK4)
        End If
    End Sub
End Class

