Imports System.Data.OleDb

Partial Class League_Selection
    Inherits System.Web.UI.Page

    Private objGlobals As New Globals

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not Request.Cookies("lastVisit") Is Nothing Then
                objGlobals.AdminLogin = True
            Else
                objGlobals.AdminLogin = False
            End If
        End If
    End Sub


    Protected Sub btnClubs_Click(sender As Object, e As EventArgs) Handles btnClubs.Click
        objGlobals.CurrentSchema = "clubs."
        Response.Redirect("~/Clubs/Default.aspx?Week=" & objGlobals.GetCurrentWeek.ToString)
    End Sub
    Protected Sub btnMens_Skittles_Click(sender As Object, e As EventArgs) Handles btnMens_Skittles.Click
        objGlobals.CurrentSchema = "mens_skit."
        Response.Redirect("~/Mens_Skit/Default.aspx?Week=" & objGlobals.GetCurrentWeek.ToString)
    End Sub
End Class
