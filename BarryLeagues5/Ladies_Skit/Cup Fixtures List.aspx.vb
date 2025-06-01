Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
Imports System.Net
'Imports MySql.Data.MySqlClient

Partial Class Cup_Fixtures_List
    Inherits System.Web.UI.Page
    Private dt As DataTable
    Private dr As DataRow
    Private gRow As Integer
    Private CompName As String
    Private objGlobals As New Globals
    Private PlayerRequired As Boolean
    Private PrelimRound As Boolean = False
    Private MaxRound As Integer
    Private RoundName(6) As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("ladies_skit")
        objGlobals.CurrentUser = "ladies_skit_user"
        objGlobals.CurrentSchema = "ladies_skit."
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)

        CompName = Request.QueryString("Comp")

        Call load_options(gridOptions)
        Call show_comp_name()
        If CupPage(CompName) = "1" Then     '24.9.19 only show the draw if the cup_page_on_web flag is set
            Call load_results()
        Else
            btnPDF.Visible = False
            lblNoDraw.Visible = True
        End If
    End Sub

    Private Function CupPage(inComp As String) As String
        CupPage = ""
        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        strSQL = "SELECT cup_page_on_web FROM " & objGlobals.CurrentSchema & "vw_comps_web WHERE Competition = '" & inComp & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            Return CStr(myDataReader.Item("cup_page_on_web"))
        End While
        objGlobals.close_connection()

    End Function

    Private Sub show_comp_name()
        Dim labelColour As System.Drawing.Color
        labelColour = LightBlue

        lblCompName.Visible = True
        lblCompName.Text = CompName
        lblCompName.ForeColor = labelColour
    End Sub

    Private Sub load_options(ByRef inGrid As GridView)
        dt = New DataTable
        dt.Columns.Add(New DataColumn("Comp Name", GetType(System.String)))

        Dim strSQL As String
        Dim myDataReader As oledbdatareader

        strSQL = "EXEC ladies_skit.sp_get_options_AR"
        myDataReader = objGlobals.SQLSelect(strSQL)

        While myDataReader.Read()
            dr = dt.NewRow
            dr("Comp Name") = myDataReader.Item("Comp")
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
                    hLink.NavigateUrl = "~/ladies_skit/Alan Rosser Cup Rules.aspx"
                ElseIf e.Row.Cells(0).Text = "PLAYOFFS" Then
                    hLink.NavigateUrl = "~/ladies_skit/Alan Rosser Cup.aspx?Group=PLAYOFFS"
                ElseIf InStr(e.Row.Cells(0).Text, "GROUP", CompareMethod.Text) = 0 Then
                    hLink.NavigateUrl = "~/ladies_skit/Cup Fixtures List.aspx?Comp=" & dt.Rows(gRow)(0).ToString
                Else
                    hLink.NavigateUrl = "~/ladies_skit/Alan Rosser Cup.aspx?Group=" & dt.Rows(gRow)(0).ToString
                End If

                hLink.Text = e.Row.Cells(0).Text
                hLink.ForeColor = Cyan
                e.Row.Cells(0).Controls.Add(hLink)
                e.Row.CssClass = "cell"
            End If
            gRow = gRow + 1
        End If
    End Sub

    Private Sub load_results()
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim LastRound As Integer = 0
        Dim PrelimCount As Integer = 0
        dt = New DataTable

        'add header row
        dr = dt.NewRow
        dt.Columns.Add(New DataColumn("Match", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Home Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Away Team", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Date", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Venue", GetType(System.String)))

        strSQL = "EXEC ladies_skit.sp_get_draw '" & objGlobals.current_season & "','" & CompName & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)
        gridResults.DataSource = myDataReader
        gridResults.DataBind()

        If gridResults.Rows.Count = 3 Then
            lblNoDraw.Visible = True
            gridResults.Visible = False
        Else
            lblNoDraw.Visible = False
        End If
    End Sub

    Protected Sub gridResults_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResults.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If InStr(e.Row.Cells(1).Text, "Round") > 0 Or InStr(e.Row.Cells(1).Text, "Final") > 0 Or
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


    Sub write_PDF_download(ByVal inFilepath As String)
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        Dim l_param_in_names(2) As String
        Dim l_param_in_values(2) As String

        l_param_in_names(0) = "@inLeague"
        l_param_in_values(0) = objGlobals.LeagueSelected
        l_param_in_names(1) = "@inTeam"
        l_param_in_values(1) = ""
        l_param_in_names(2) = "@inFilepath"
        l_param_in_values(2) = Replace(inFilepath, "'", """")

        strSQL = "EXEC [ladies_skit].[sp_write_download_PDF] '" & l_param_in_values(0) & "','" & l_param_in_values(1) & "','" & l_param_in_values(2) & "'"
        myDataReader = objGlobals.SQLSelect(strSQL)

        objGlobals.close_connection()
    End Sub


    Protected Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Dim FilePath As String = Server.MapPath("CupDraws") & "\"
        Dim filename As String = IIf(Strings.Left(CompName, 12) <> "HOLME TOWERS", CompName, "HOLME TOWERS CUP") + ".pdf" 'remove all versions of the H/Towers cup, and save as HOLME TOWERS
        Dim PDFfile As String = FilePath + filename

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
