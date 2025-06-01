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

End Class
