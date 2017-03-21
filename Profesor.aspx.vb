Public Class WebForm2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If (Session("user") Is Nothing) Or (Session("rol") Is Nothing) Or (Session("rol").Equals("P") = False) Then
                Response.Redirect("Inicio.aspx")
            Else
                Label1.Text = Session("user")
            End If
        Else

        End If

    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Session.Abandon()
        Response.Redirect("Inicio.aspx")
    End Sub
End Class