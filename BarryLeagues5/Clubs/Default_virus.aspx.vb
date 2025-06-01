
Partial Class Clubs_Default
    Inherits System.Web.UI.Page

    Protected Sub btnClose_Click(sender As Object, e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("~/Clubs/Default.aspx?Week=0")
    End Sub
End Class
