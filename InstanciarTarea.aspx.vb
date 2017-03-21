Imports System.Data.SqlClient

Public Class WebForm3
    Inherits System.Web.UI.Page
    Dim dap As SqlDataAdapter
    Dim dst As DataSet
    Dim DataTable As DataTable
    Dim bd As New accesodatosSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("user") Is Nothing) Or (Session("rol") Is Nothing) Or (Session("rol").Equals("A") = False) Then
            Response.Redirect("Inicio.aspx")
        ElseIf (Not IsPostBack) Then
            Label1.Text = Request.QueryString("codTarea")
            Label2.Text = Request.QueryString("desc")
            Label3.Text = Request.QueryString("HEstimadas")
            Label4.Text = Session("user")

            Dim st = "select * from EstudiantesTareas where Email='" & Session("user") & "' "
            Dim conClsf As SqlConnection = New SqlConnection(bd.stringConexion())
            dap = New SqlDataAdapter(st, conClsf)
            dst = New DataSet
            Dim cbuilder As New SqlCommandBuilder(dap)
            dap.Fill(dst, "TareasInstanciadas")
            DataTable = dst.Tables("TareasInstanciadas")
            GridView1.DataSource = DataTable
            GridView1.DataBind()
            GridView1.Visible = True
            Session("adaptador") = dap
            Session("dataSet") = dst
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        dap = Session("adaptador")
        dst = Session("dataSet")
        DataTable = dst.Tables("TareasInstanciadas")
        Dim row As DataRow = DataTable.NewRow()
        row("Email") = Session("user").ToString
        row("CodTarea") = Label1.Text
        row("HEstimadas") = Integer.Parse(Label3.Text)
        row("HReales") = Integer.Parse(TextBox1.Text)
        DataTable.Rows.Add(row)
        dap.Update(dst, "TareasInstanciadas")
        dst.AcceptChanges()
        GridView1.DataSource = DataTable
        GridView1.DataBind()
        Button1.Enabled = False
        Label5.Text = "Tarea instanciada"
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Response.Redirect("Alumno.aspx")
    End Sub
End Class