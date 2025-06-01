Imports System.Data.OleDb
'Imports MySql.Data.MySqlClient

Partial Class Admin_Website_Activity
    Inherits System.Web.UI.Page
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
            txtSQL.Text = "NOT AUTHORIZED"
            txtSQL.Enabled = False
            gridActivity.Visible = False
            btnRefresh.Visible = False
            Call objGlobals.store_page("ATTACK FROM " & Request.Url.OriginalString, objGlobals.AdminLogin)
            Exit Sub
        End If
        If Not IsPostBack Then
            txtSQL.Text = SqlDataSource1.SelectCommand
        Else
            If InStr(UCase(txtSQL.Text), "PAGE_VISITS", CompareMethod.Text) = 0 Then
                Exit Sub
            Else
                SqlDataSource1.SelectCommand = txtSQL.Text
            End If
        End If

        SqlDataSourcemy_ip_addresses.ConnectionString = objGlobals.getSQLConnectionString
        lblIPExists.Visible = False
        lblNoIP.Visible = False
    End Sub

    Protected Sub btnRefresh_Click(sender As Object, e As System.EventArgs) Handles btnRefresh.Click
        If Not objGlobals.AdminLogin Or
            InStr(UCase(txtSQL.Text), "PAGE_VISITS", CompareMethod.Text) = 0 Or
            InStr(UCase(txtSQL.Text), "INSERT", CompareMethod.Text) > 0 Or
            InStr(UCase(txtSQL.Text), "DELETE", CompareMethod.Text) > 0 Or
            InStr(UCase(txtSQL.Text), "UPDATE", CompareMethod.Text) > 0 Or
            InStr(UCase(txtSQL.Text), "DROP", CompareMethod.Text) > 0 Or
            InStr(UCase(txtSQL.Text), "TRUNCATE", CompareMethod.Text) > 0 Then

            txtSQL.Text = "NOT AUTHORIZED"
            txtSQL.Enabled = False
            gridActivity.Visible = False
            btnRefresh.Visible = False
            Exit Sub
        End If

        gridActivity.DataBind()
    End Sub

    Protected Sub gridActivity_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridActivity.RowDataBound
        Dim NewURL As String = ""
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If e.Row.Cells(1).Text <> "GoogleBot Crawler" Then
                Dim hLink1 As New HyperLink
                NewURL = e.Row.Cells(1).Text
                NewURL = Replace(NewURL, "amp;", "")
                NewURL = Replace(NewURL, "&quot;", """")
                hLink1.NavigateUrl = NewURL
                hLink1.Text = e.Row.Cells(1).Text
                If Left(e.Row.Cells(1).Text, 6) <> "ATTACK" Then
                    hLink1.ForeColor = Drawing.Color.White
                Else
                    hLink1.ForeColor = Drawing.Color.Red
                End If
                e.Row.Cells(1).Controls.Add(hLink1)
                e.Row.CssClass = "cell"
            End If
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        If txtIPAddress.Text = "" Then
            lblNoIP.Visible = True
            Exit Sub
        End If

        strSQL = "SELECT * FROM  mens_skit.my_ip_addresses WHERE ip_address = '" + txtIPAddress.Text + "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If myDataReader.HasRows Then
            lblIPExists.Visible = True
            Exit Sub
        End If

        strSQL = "INSERT INTO mens_skit.my_ip_addresses VALUES ("
        strSQL = strSQL + "'" + txtIPAddress.Text + "',"
        strSQL = strSQL + "'" + txtLocation.Text + "')"
        myDataReader = objGlobals.SQLSelect(strSQL)

        Response.Redirect("~/Mens_Skit/Admin/Website Activity.aspx")
    End Sub

End Class
