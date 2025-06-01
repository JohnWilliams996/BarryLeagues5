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
        'Call objGlobals.open_connection("all_skit")
        objGlobals.CurrentUser = "all_skit_user"
        objGlobals.CurrentSchema = "all_skit."
        If Not Request.Cookies("lastVisit") Is Nothing Then
            objGlobals.AdminLogin = True
        Else
            objGlobals.AdminLogin = False
        End If
        If Not IsPostBack Then
            SeasonLbl.Text = objGlobals.get_current_season
            HomeHL.NavigateUrl = "~/All_Skit/Default.aspx?Week=" & objGlobals.GetCurrentWeek
            Clubs1HL.NavigateUrl = "~/Clubs/Default.aspx?Week=" & objGlobals.GetCurrentWeek
            MensSkitHL.NavigateUrl = "~/Mens_Skit/Default.aspx?Week=" & objGlobals.GetCurrentWeek
        End If
    End Sub


End Class

