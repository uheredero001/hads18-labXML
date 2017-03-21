Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports System.Xml

Public Class ExportarTareas
    Inherits System.Web.UI.Page
    Dim dap As SqlDataAdapter
    Dim dst As DataSet
    Dim Data As New DataTable
    Dim bd As New accesodatosSQL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            If (Session("user") Is Nothing Or Session("rol") Is Nothing Or Not Session("rol").Equals("P")) Then
                Response.Redirect("Inicio.aspx")
            Else
                Try
                    Dim asignaturas As SqlDataReader
                    asignaturas = bd.getAsignaturasProfesor(Session("user"))
                    DropDownList1.Items.Clear()
                    While asignaturas.Read
                        DropDownList1.Items.Add(asignaturas.Item("codigo"))
                    End While
                    DropDownList1.SelectedIndex = 0
                    asignaturas.Close()
                    Session("asigselec") = DropDownList1.SelectedItem.Text
                    DropDownList1.Visible = True
                Catch ex As Exception
                    Label1.Text = ex.Message
                End Try
            End If
        End If
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        Session("asigselec") = DropDownList1.SelectedItem.Text
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim st = "select T.Codigo,T.Descripcion,T.HEstimadas,T.TipoTarea,T.Explotacion from (TareasGenericas T inner join Asignaturas A on T.CodAsig=A.codigo) left join ProfesoresGrupo P on P.codigogrupo=T.Codigo where A.codigo='" & Session("asigselec") & "'"
        Dim conClsf As SqlConnection = New SqlConnection(bd.stringConexion())
        dap = New SqlDataAdapter(st, conClsf)
        dst = New DataSet
        Dim cbuilder As New SqlCommandBuilder(dap)
        dap.Fill(dst, "TareasGenericas")
        Data = dst.Tables("TareasGenericas")
        GridView1.DataSource = Data
        GridView1.DataBind()
        GridView1.Visible = True
        Session("datos") = dst
        Session("adaptador") = dap
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim xd As New XmlDocument()
        Dim tbl As New DataTable
        Dim settings As XmlWriterSettings = New XmlWriterSettings()
        dap = Session("adaptador")
        dst = Session("datos")
        tbl = dst.Tables("TareasGenericas")
        tbl.TableName = "Tareas"
        Dim row As DataRow
        Try
            Using writer As XmlWriter = XmlWriter.Create(Server.MapPath("App_Data\" & Session("asigselec") & ".xml").ToString, settings)
                writer.WriteStartDocument()
                writer.WriteStartElement("tareas")
                writer.WriteAttributeString("xmlns", "has", Nothing, "http://ji.ehu.es/has")
                For Each row In tbl.Rows
                    writer.WriteStartElement("tarea")
                    writer.WriteElementString("codigo", row.Item("codigo"))
                    writer.WriteElementString("descripcion", row.Item("descripcion"))
                    writer.WriteElementString("hestimadas", row.Item("hestimadas"))
                    writer.WriteElementString("explotacion", row.Item("explotacion"))
                    writer.WriteElementString("tipotarea", row.Item("tipotarea"))
                    writer.WriteEndElement()
                Next
                writer.WriteEndElement()
                writer.WriteEndDocument()
            End Using
            Label1.Text = "Exportado con exito en xml."

        Catch ex As Exception
            Label1.Text = ex.Message
        End Try

    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Exportamos en JSON
        Dim tbl As New DataTable
        dap = Session("adaptador")
        dst = Session("datos")
        tbl = dst.Tables("TareasGenericas")
        tbl.TableName = "Tareas"
        Dim serializer As New JavaScriptSerializer()
        Try
            For Each row In tbl.Rows

            Next
            Label1.Text = "Exportaro JSON con exito"
        Catch ex As Exception
            Label1.Text = ex.Message
        End Try


    End Sub
End Class