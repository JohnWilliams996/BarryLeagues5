Imports System.Data.OleDb
Imports System.Drawing.Color
Imports System.Data
'Imports MySql.Data.MySqlClient

Partial Class Delegates
    Inherits System.Web.UI.Page
    Private dt As New DataTable
    Private dr As DataRow
    Private objGlobals As New Globals

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Call objGlobals.open_connection("ladies_skit")
        objGlobals.CurrentUser = "ladies_skit_user"
        objGlobals.CurrentSchema = "ladies_skit."
        Dim strSQL As String
        Dim myDataReader As oledbdatareader
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        Call objGlobals.store_page(Request.Url.OriginalString, objGlobals.AdminLogin)

        strSQL = "SELECT * FROM ladies_skit.Delegates ORDER BY Club"
        myDataReader = objGlobals.SQLSelect(strSQL)
        If Not myDataReader.HasRows Then
            objGlobals.close_connection()
            Exit Sub
        End If
        Dim dt As DataTable = New DataTable

        dt.Columns.Add(New DataColumn("Club", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Tel No.", GetType(System.String)))
        dt.Columns.Add(New DataColumn("Delegate", GetType(System.String)))

        While myDataReader.Read()
            With gridDelegates
                dr = dt.NewRow
                dr("Club") = myDataReader.Item("Club")
                dr("Tel No.") = myDataReader.Item("phone")
                dr("Delegate") = myDataReader.Item("Delegate")
                dt.Rows.Add(dr)
                .DataSource = dt
                .DataBind()
            End With
        End While
        Dim irow As Integer
        With gridDelegates
            .HeaderRow.ForeColor = System.Drawing.Color.Tan
            .HeaderRow.Cells(0).HorizontalAlign = HorizontalAlign.Left
            .HeaderRow.Cells(1).HorizontalAlign = HorizontalAlign.Center
            .HeaderRow.Cells(2).HorizontalAlign = HorizontalAlign.Left
            .Font.Name = "Arial"
            For irow = 0 To .Rows.Count - 1
                With .Rows(irow).Cells(0)
                    .HorizontalAlign = HorizontalAlign.Left
                    .Width = 250
                    .ForeColor = Cyan
                End With
                With .Rows(irow).Cells(1)
                    .ForeColor = White
                    .HorizontalAlign = HorizontalAlign.Center
                    .Width = 150
                End With
                With .Rows(irow).Cells(2)
                    .ForeColor = LightGreen
                    .HorizontalAlign = HorizontalAlign.Left
                    .Width = 300
                End With
            Next

        End With

    End Sub

End Class
