Public Class TareasGenericasProfesor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("user") Is Nothing) Or (Session("rol") Is Nothing) Or (Session("rol").Equals("P") = False) Then
            Response.Redirect("Inicio.aspx")
        Else


        End If
    End Sub

End Class