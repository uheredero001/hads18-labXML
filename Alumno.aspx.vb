Imports System.Data.SqlClient

Public Class WebForm1
    Inherits System.Web.UI.Page
    Dim bd As New accesodatosSQL
    Dim data As New DataTable
    Dim dst As New DataSet
    Dim dap As New SqlClient.SqlDataAdapter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If (Session("user") Is Nothing) Or (Session("rol") Is Nothing) Or (Session("rol").Equals("A") = False) Then
                Response.Redirect("Inicio.aspx")
            End If
            Dim result As SqlClient.SqlDataReader
            result = bd.getAsignaturasAlumno(Session("user"))
            DropDownList1.Items.Clear()
            While result.Read
                DropDownList1.Items.Add(result.Item("Nombre"))
            End While
            result.Close()
            DropDownList1.SelectedIndex = 0
            Session("asigSelec") = DropDownList1.SelectedItem.Text
            DropDownList1.Visible = True
            usuario.Text = Session("user")
        Else


        End If
    End Sub


    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Session.Abandon()
        bd.cerrarconexion()
        Response.Redirect("Inicio.aspx")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim st = "select T.Codigo,T.Descripcion,T.HEstimadas,T.TipoTarea from (TareasGenericas T inner join Asignaturas A on T.CodAsig=A.codigo) left join EstudiantesTareas E on E.CodTarea=T.Codigo where A.Nombre='" & Session("asigSelec") & "' and E.Email is null"
        Dim conClsf As SqlConnection = New SqlConnection(bd.stringConexion())
        Dim tareas As SqlDataReader
        tareas = bd.getTareasInstanciadas(Session("user"))
        dap = New SqlDataAdapter(st, conClsf)
        dst = New DataSet
        Dim cbuilder As New SqlCommandBuilder(dap)
        dap.Fill(dst, "TareasGenericas")
        data = dst.Tables("TareasGenericas")
        GridView1.DataSource = data
        GridView1.DataBind()
        GridView1.Visible = True
        tareas.Close()
        Session("datos") = data
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.TextChanged
        Session("asigSelec") = DropDownList1.SelectedItem.Text
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        data = Session("datos")
        Response.Redirect("InstanciarTarea.aspx?codTarea=" + data.Rows(GridView1.SelectedIndex()).Item(0).ToString + "&desc=" + data.Rows(GridView1.SelectedIndex).Item(1).ToString + "&HEstimadas=" + data.Rows(GridView1.SelectedIndex).Item(2).ToString)
    End Sub
End Class