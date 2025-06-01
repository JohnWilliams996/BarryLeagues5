Imports System.Drawing.Color
Imports System.Data
Imports System.Data.OleDb
Imports System.IO

'Imports MySql.Data.MySqlClient

Partial Class Honours
    Inherits System.Web.UI.Page
    Private dt As DataTable
    Private dr As DataRow
    Private gRow As Integer
    Private objGlobals As New Globals
    Private last_season As String

    Private Sub load_sheet(ByVal inSeason As String)
        HonoursJPG.ImageUrl = "~/Clubs/Honours/HONOURS WEB " & inSeason & ".jpg"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            rbSeasons.Items.Add("2024-2025")
            rbSeasons.Items.Add("2023-2024")
            rbSeasons.Items.Add("2022-2023")
            rbSeasons.Items.Add("2021-2022")
            rbSeasons.Items.Add("2018-2019")
            rbSeasons.Items.Add("2017-2018")
            rbSeasons.Items.Add("2016-2017")
            rbSeasons.SelectedIndex = 0
            load_sheet(rbSeasons.Items(0).Text)
        End If
    End Sub

    Protected Sub rbSeasons_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rbSeasons.SelectedIndexChanged
        load_sheet(rbSeasons.SelectedValue)
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("~/Clubs/Default.aspx")
    End Sub
End Class
