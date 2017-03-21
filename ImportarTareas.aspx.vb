Imports System.Data.SqlClient
Imports System.Xml

Public Class ImportarAsignaturas
    Inherits System.Web.UI.Page
    Dim dap As SqlDataAdapter
    Dim dst As DataSet
    Dim tbl As New DataTable
    Dim bd As New accesodatosSQL
    Dim xd As New XmlDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            If (Session("user") Is Nothing Or Session("rol") Is Nothing Or Not Session("rol").Equals("P")) Then
                Response.Redirect("Inicio.aspx")
            Else
                Try
                    Dim asignaturas As SqlDataReader
                    Dim xd As New System.Xml.XmlDocument
                    asignaturas = bd.getAsignaturasProfesor(Session("user"))
                    DropDownList1.Items.Clear()
                    While asignaturas.Read
                        DropDownList1.Items.Add(asignaturas.Item("codigo"))
                    End While
                    DropDownList1.SelectedIndex = 0
                    asignaturas.Close()
                    Session("asigselec") = DropDownList1.SelectedItem.Text
                    DropDownList1.Visible = True
                    xd.Load(Server.MapPath("App_Data\" & Session("asigselec") & ".xml").ToString)
                    Xml1.Document = xd
                    Session("doc") = xd
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
        Try
            xd.Load(Server.MapPath("App_Data\" + Session("asigselec") + ".xml"))
            Xml1.Document = xd
            Xml1.TransformSource = Server.MapPath("App_Data\XSLTFile.xsl")
        Catch ex As Exception
            Label1.Text = ex.Message
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim row As DataRow
        Dim num As Integer
        Dim conClsf As SqlConnection = New SqlConnection(bd.stringConexion())
        Dim st = "select * from TareasGenericas"
        dap = New SqlDataAdapter(st, conClsf)
        Dim cbuilder As New SqlCommandBuilder(dap)
        dst = New DataSet
        dap.Fill(dst)
        tbl = dst.Tables("Tareas")
        xd = Session("doc")
        Try
            dap.Fill(dst, "Tareas")
            tbl = dst.Tables("Tareas")
            Session("datos") = dst
            Session("adapter") = dap
            Dim tareas As XmlNodeList
            tareas = xd.GetElementsByTagName("tarea")
            Dim i As Integer
            For i = 0 To tareas.Count - 1
                Dim rowselect() As DataRow
                rowselect = tbl.Select("codigo='" + tareas(i).ChildNodes(0).ChildNodes(0).Value + "'")
                If rowselect.Length < 1 Then
                    Dim rowtareas As DataRow = tbl.NewRow()
                    rowtareas("codigo") = tareas(i).ChildNodes(0).ChildNodes(0).Value
                    rowtareas("descripcion") = tareas(i).ChildNodes(1).ChildNodes(0).Value
                    rowtareas("hestimadas") = tareas(i).ChildNodes(2).ChildNodes(0).Value
                    rowtareas("explotacion") = tareas(i).ChildNodes(3).ChildNodes(0).Value
                    rowtareas("tipotarea") = tareas(i).ChildNodes(4).ChildNodes(0).Value
                    rowtareas("codasig") = Session("asigselec")
                    tbl.Rows.Add(rowtareas)
                End If

            Next
            num = dap.Update(dst, "Tareas")
            dst.AcceptChanges()
            If num > 0 Then
                Label1.Text = "Salvados cambios en la BD (dap.update)"
            Else : Label1.Text = "No hay nuevas tareas en el fichero"
            End If

            'OPCIONAL (DATASET)
            'Dim xtr As XmlReader = XmlReader.Create(Server.MapPath("App_Data\" + Session("asigselec") + ".xml"))
            ' While xtr.Read
            'If xtr.NodeType = XmlNodeType.Element Then
            'If xtr.Name = "codigo" Then
            'xtr.Read()
            'Dim rowselect() As DataRow
            ''rowselect = tbl.Select("codigo='" + xtr.Value + "'")
            'If rowselect.Length < 1 Then
            'Dim rowtareas As DataRow = tbl.NewRow()
            'rowtareas("codigo") = xtr.value
            'xtr.Read()
            'rowtareas("descripcion") = xtr.Value
            'xtr.Read()
            'rowtareas("hestimadas") = xtr.Value
            'xtr.Read()
            'rowtareas("explotacion") = xtr.Value
            'xtr.Read()
            'rowtareas("tipotarea") = xtr.Value
            'tbl.Rows.Add(rowtareas)
            'End If
            'End If
            'End If
            'End While
            'num = dap.Update(dst, "TareasGenericas")
            'dst.AcceptChanges()
            'If num > 0 Then
            ' Label1.Text = "Salvados cambios en la BD (dap.update)"
            ' Else : Label1.Text = "No hay nuevas tareas en el fichero"
            ' End If

        Catch ex As Exception
            Label1.Text = ex.Message
        End Try
    End Sub
End Class