Imports System.Data.OleDb
'Imports MySql.Data.MySqlClient

Partial Class Admin_Database_Import
    Inherits System.Web.UI.Page
    Private objGlobals As New Globals

    Protected Sub btnImport_Click(sender As Object, e As System.EventArgs) Handles btnImport.Click
        Dim myDataReader As oledbdatareader
        Dim strSQL As String
        Dim txt As String = txtSQL.Text
        Dim Lines As String() = txt.Split(New [Char]() {ControlChars.Lf, ControlChars.Cr}, StringSplitOptions.RemoveEmptyEntries)
        For i = 0 To Lines.Count - 1
            strSQL = Lines(i)
            myDataReader = objGlobals.SQLSelect(strSQL)
        Next
        txtSQL.Font.Size = 14
        txtSQL.Text = "IMPORT COMPLETE"
        btnImport.Visible = False
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("ladies_skit")
        objGlobals.CurrentUser = "ladies_skit_user"
        objGlobals.CurrentSchema = "ladies_skit."
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
            lblInfo.Visible = False
            txtSQL.Text = "NOT AUTHORIZED"
            txtSQL.Enabled = False
            btnImport.Visible = False
            Call objGlobals.store_page("ATTACK FROM " & Request.Url.OriginalString, objGlobals.AdminLogin)
            Exit Sub
        End If
        Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
        txtSQL.Focus()
    End Sub
End Class
