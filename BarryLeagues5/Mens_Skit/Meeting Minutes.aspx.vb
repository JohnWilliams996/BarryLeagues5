Imports System.Data.OleDb
Imports System.Drawing.Color
Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Diagnostics

'Imports MySql.Data.MySqlClient


Partial Class Mens_Skit_Meeting_Minutes
    Inherits System.Web.UI.Page
    Private dt As New DataTable
    Private dr As DataRow
    Private objGlobals As New Globals

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("mens_skit")
        objGlobals.CurrentUser = "mens_skit_user"
        objGlobals.CurrentSchema = "mens_skit."
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)
        If Not IsPostBack Then
            strSQL = "SELECT meeting_date FROM mens_skit.league_meetings WHERE filename IS NOT NULL ORDER BY Date8 DESC"
            myDataReader = objGlobals.SQLSelect(strSQL)
            If Not myDataReader.HasRows Then
                objGlobals.close_connection()
                Exit Sub
            End If

            While myDataReader.Read()
                rbMeetings.Items.Add(myDataReader.Item(0))
            End While
            rbMeetings.Items(0).Selected = True
        End If
        objGlobals.close_connection()

        txtMinutes.Visible = False
    End Sub

    Protected Sub btnOpenWebsite_Click(sender As Object, e As System.EventArgs) Handles btnOpenWebsite.Click
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        strSQL = "SELECT filename FROM mens_skit.league_meetings WHERE meeting_date = '" & rbMeetings.SelectedValue & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            Dim sr As StreamReader
            Dim FilePath As String = Server.MapPath("MeetingMinutes") & "\"
            sr = File.OpenText(FilePath & myDataReader.Item(0) & ".txt")
            Dim strContents As String = sr.ReadToEnd()
            txtMinutes.Text = strContents
            sr.Close()
        End While

        objGlobals.close_connection()
        txtMinutes.Visible = True
    End Sub
    Sub write_PDF_download(ByVal inFilepath As String)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim l_param_in_names(2) As String
        Dim l_param_in_values(2) As String

        l_param_in_names(0) = "@inLeague"
        l_param_in_values(0) = "MEETING MINUTES"
        l_param_in_names(1) = "@inTeam"
        l_param_in_values(1) = rbMeetings.SelectedValue
        l_param_in_names(2) = "@inFilepath"
        l_param_in_values(2) = Replace(inFilepath, "'", """")

        strSQL = "EXEC [mens_skit].[sp_write_download_PDF] '" & l_param_in_values(0) & "','" & l_param_in_values(1) & "','" & l_param_in_values(2) & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        objGlobals.close_connection()
    End Sub

    Protected Sub btnOpenPDF_Click(sender As Object, e As EventArgs) Handles btnOpenPDF.Click
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim PDFfile As String = ""
        strSQL = "SELECT filename FROM mens_skit.league_meetings WHERE meeting_date = '" & rbMeetings.SelectedValue & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        While myDataReader.Read()
            PDFfile = Server.MapPath("MeetingMinutes") & "\" & myDataReader.Item(0) & ".pdf"
        End While
        objGlobals.close_connection()

        'write details for PDF_downloads
        write_PDF_download(PDFfile)

        Dim client As New WebClient
        Dim ByteArray As Byte() = client.DownloadData(PDFfile)

        Response.Clear()
        'Send the file to the output stream
        Response.Buffer = True
        'Try and ensure the browser always opens the file and doesn’t just prompt to "open/save”.
        Response.AddHeader("Content-Length", ByteArray.Length.ToString())
        Response.AddHeader("Content-Disposition", "inline filename=" + PDFfile)
        Response.AddHeader("Expires", "0")
        Response.AddHeader("Pragma", "cache")
        Response.AddHeader("Cache-Control", "private")

        'Set the output stream to the correct content type (PDF).
        Response.ContentType = "application/pdf"

        'Output the file
        Response.BinaryWrite(ByteArray)

        'Flushing the Response to display the serialized data
        'to the client browser.
        Response.Flush()
    End Sub

End Class
