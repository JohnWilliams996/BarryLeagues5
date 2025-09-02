Imports System.Data.OleDb
Imports System.Drawing.Color
Imports System.Data
Imports System.Web

'Imports MySql.Data
'Imports MySql.Data.MySqlClient


Public Class MasterPage
    Inherits System.Web.UI.MasterPage

    Private dt As DataTable
    Private dr As DataRow
    Private gRow As Integer
    Private objGlobals As New Globals


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("mens_skit")
        objGlobals.CurrentUser = "mens_skit_user"
        objGlobals.CurrentSchema = "mens_skit."
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
                DBExportHL.Visible = True
                DBImportHL.Visible = True
                RegiserHL.Visible = True
                ActivityHL.Visible = True
                ClubsHL.Visible = True
                LadiesSkitHL.Visible = True
                AllSkitHL.Visible = True
                lblAdmin.Visible = True
                lblAdmin.Text = "Admin Logged-in" '
            Else
                btnLogout.Visible = False
                CupResultsHL.Visible = False
                DBExportHL.Visible = False
                DBImportHL.Visible = False
                RegiserHL.Visible = False
                ActivityHL.Visible = False
                ClubsHL.Visible = False
                LadiesSkitHL.Visible = False
                AllSkitHL.Visible = False
            End If
            HomeHL.NavigateUrl = "~/Mens_Skit/Default.aspx?Week=" & objGlobals.GetCurrentWeek
            ClubsHL.NavigateUrl = "~/Clubs/Default.aspx?Week=" & objGlobals.GetCurrentWeek
            LadiesSkitHL.NavigateUrl = "~/Ladies_Skit/Default.aspx?Week=" & objGlobals.GetCurrentWeek
        End If
    End Sub


    Sub load_last_updated()
        Dim yyyy As Integer
        Dim mm As Integer
        Dim dd As Integer
        Dim t_ime As String
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        strSQL = "SELECT date_time_changed FROM mens_skit.last_changed"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If Not myDataReader.HasRows Then
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
    End Sub

    Protected Sub btnAdmin_Click(sender As Object, e As System.EventArgs) Handles btnAdmin.Click
        If lblAdmin.Text <> "Admin Logged-in" Then
            lblAdmin.Visible = True
            lblAdmin.Text = "Admin Password : "
            txtPassword.Visible = True
            btnGo.Visible = True
            btnCancel.Visible = True
            txtPassword.Focus()
        End If
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
            DBExportHL.Visible = True
            DBImportHL.Visible = True
            RegiserHL.Visible = True
            ActivityHL.Visible = True
            ClubsHL.Visible = True
            LadiesSkitHL.Visible = True
            AllSkitHL.Visible = True


            'create a cookie
            Dim aCookie As New HttpCookie("lastVisit")
            aCookie.Value = DateTime.Now.ToString
            aCookie.Expires = DateTime.Now.AddDays(1)
            Response.Cookies.Add(aCookie)
            'Refresh the home page
            Response.Redirect("~/Mens_Skit/Default.aspx?Week=" & objGlobals.GetCurrentWeek)
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
        CupResultsHL.Visible = False
        btnLogout.Visible = False
        DBExportHL.Visible = False
        DBImportHL.Visible = False
        RegiserHL.Visible = False
        ActivityHL.Visible = False
        ClubsHL.Visible = False
        LadiesSkitHL.Visible = False
        AllSkitHL.Visible = False

        'Refresh the home page
        Response.Redirect("~/Mens_Skit/Default.aspx?Week=" & objGlobals.GetCurrentWeek)

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
        Dim Division1Team(20) As String
        Dim Division2Team(20) As String
        Dim Division3Team(20) As String

        dt = New DataTable
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Division1Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Division2Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Division3Team", GetType(System.String)))
        'hard-code the teams to make the load quicker !! - use Excel spreasheet call MY TEAMS FOR WEBSITE.xlsx and SQL script MY TEAMS FOR WEBSITE.sql 
        dt.Rows.Add("49ers", "ALTERNATIVES", "")
        dt.Rows.Add("BILLY BLUEBIRDS", "B.T.U.", "")
        dt.Rows.Add("BILLY FLYERS", "BARRY PARK B", "")
        dt.Rows.Add("BILLY LADS", "BRITISH RAIL", "")
        dt.Rows.Add("BILLY WHIZZ", "CASTLE BOYS", "")
        dt.Rows.Add("BLUE MONKEY", "COLCOT BOYS", "")
        dt.Rows.Add("CANNON BALLS", "EX SERVICEMEN", "")
        dt.Rows.Add("CAVALIERS", "HAIRY LEMONS", "")
        dt.Rows.Add("GARYS EXILES", "LOBSTERS", "")
        dt.Rows.Add("GEEST", "PARK DRAGONS", "")
        dt.Rows.Add("MERRYMEN", "PARK LADS", "")
        dt.Rows.Add("MIGHTY DOCKERS", "POSTIES", "")
        dt.Rows.Add("ROYALISTS", "ROCK N BOWLERS", "")
        dt.Rows.Add("WEST END RANGERS", "TAKING LIBERTIES", "")
        dt.Rows.Add("WILLIE WOMBLES", "THE CELTS", "")
        dt.Rows.Add("WITCHILL NOMADS", "THE SLAMMERS", "")
        dt.Rows.Add("", "WITCHILL POACHERS", "")


        gridTeams.DataSource = dt
        gridTeams.DataBind()


    End Sub

    Protected Sub gridTeams_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTeams.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hLink1 As New HyperLink
            hLink1.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=DIVISION 1&Team=" & Replace(e.Row.Cells(0).Text, "&#39;", Chr(34))
            hLink1.ForeColor = Cyan
            hLink1.Text = e.Row.Cells(0).Text
            e.Row.Cells(0).Controls.Add(hLink1)

            Dim hLink2 As New HyperLink
            hLink2.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=DIVISION 2&Team=" & Replace(e.Row.Cells(1).Text, "&#39;", Chr(34))
            hLink2.ForeColor = Cyan
            hLink2.Text = e.Row.Cells(1).Text
            e.Row.Cells(1).Controls.Add(hLink2)

            Dim hLinK3 As New HyperLink
            hLinK3.NavigateUrl = "~/Mens_Skit/Team Fixtures.aspx?League=DIVISION 3&Team=" & Replace(e.Row.Cells(2).Text, "&#39;", Chr(34))
            hLinK3.ForeColor = Cyan
            hLinK3.Text = e.Row.Cells(2).Text
            e.Row.Cells(2).Controls.Add(hLinK3)
        End If
    End Sub


End Class

