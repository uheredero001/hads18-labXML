Imports System.Data.SqlClient

Public Class accesodatosSQL
    Private Shared conexion As New SqlConnection
    Private Shared comando As New SqlCommand

    Public Shared Function conectar() As String
        Try
            conexion.ConnectionString = "Data Source=tcp:hads-18.database.windows.net,1433;Initial Catalog=HADS18_usuarios;Persist Security Info=True;User ID=HADS18@hads-18;Password=4QYzSiq7"
            conexion.Open()
        Catch ex As Exception
            Return "ERROR DE CONEXIÓN: " + ex.Message
        End Try
        Return "CONEXION OK"
    End Function

    Public Shared Function stringConexion() As String
        Return "Data Source=tcp:hads-18.database.windows.net,1433;Initial Catalog=HADS18_usuarios;Persist Security Info=True;User ID=HADS18@hads-18;Password=4QYzSiq7"
    End Function

    Public Shared Function insertar(ByVal Nombre As String, ByVal email As String,
                                    ByVal Apellidos As String, ByVal Password As String,
                                    ByVal Pregunta As String, ByVal Respuesta As String,
                                    ByVal numconfir As String) As String
        Dim st = "insert into Usuarios (email,nombre,apellidos,pregunta,
    respuesta,pass,numconfir) 
            values ('" & email & " ','" & Nombre & "','" & Apellidos & "',
'" & Pregunta & "','" & Respuesta & "','" & Password & "','" & numconfir & "')"
        Dim numregs As Integer
        conectar()
        comando = New SqlCommand(st, conexion)
        Try
            numregs = comando.ExecuteNonQuery()
        Catch ex As Exception
            Return ex.Message
        End Try
        Return (numregs & " registro(s) insertado(s) en la BD ")
    End Function

    Public Shared Sub cerrarconexion()
        conexion.Close()
    End Sub

    Public Shared Function comprobarLogin(ByVal email As String, ByVal password As String) As String
        Dim numregs As Integer
        Dim st = "select count(*) from Usuarios where email='" & email & "' and pass='" & password & "' and confirmado=1"
        conectar()
        comando = New SqlCommand(st, conexion)
        Try
            numregs = comando.ExecuteScalar()
        Catch ex As Exception
            Return ex.Message
        End Try
        If (numregs = 1) Then
            Return "Ok"
        Else
            Return "Error"
        End If
    End Function

    Public Shared Function confirmarRegistro(ByVal email As String, ByVal num As String) As String
        Dim numregs As Integer
        Dim st = "update Usuarios set confirmado=1 where email='" & email & "' and numconfir='" & num & "'"
        conectar()
        comando = New SqlCommand(st, conexion)
        Try
            numregs = comando.ExecuteNonQuery()
        Catch ex As Exception
            Return ex.Message
        End Try
        If (numregs = 1) Then
            Return "Ok"
        Else
            Return "Error"
        End If
    End Function

    Public Shared Function existeUserConPregunta(ByVal email As String, ByVal Pregunta As String, ByVal Respuesta As String) As String
        Dim numregs As Integer
        Dim st = "select count(*) from Usuarios where email='" & email & "' and pregunta='" & Pregunta & "' and respuesta='" & Respuesta & "'"
        conectar()
        comando = New SqlCommand(st, conexion)
        Try
            numregs = comando.ExecuteScalar()
        Catch ex As Exception
            Return ex.Message
        End Try
        If (numregs = 1) Then
            Return "Ok"
        Else
            Return "Error"
        End If
    End Function

    Public Shared Function cambiaPassword(ByVal email As String, ByVal newPass As String) As String
        Dim numregs As Integer
        Dim st = "update Usuarios set pass='" & newPass & "' where email='" & email & "' and confirmado=1"
        conectar()
        comando = New SqlCommand(st, conexion)
        Try
            numregs = comando.ExecuteNonQuery()
        Catch ex As Exception
            Return ex.Message
        End Try
        If (numregs = 1) Then
            Return "Ok"
        Else
            Return "Error"
        End If
    End Function

    Public Shared Function rol(ByVal email As String) As String
        Dim st = "select * from Usuarios where email='" & email & "'"
        Dim data As SqlDataReader
        conectar()
        comando = New SqlCommand(st, conexion)
        Try
            data = comando.ExecuteReader()
            If (data.Read()) Then
                Return data.Item("tipo")
            Else
                Return "Error"
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Shared Function getAsignaturasAlumno(ByVal email As String) As SqlDataReader
        Dim st = "select * from ((Asignaturas A inner join GruposClase G on A.codigo=G.codigoasig)inner join EstudiantesGrupo E on E.Grupo=G.codigo) inner join Usuarios U on E.email=U.email where U.email='" & email & "'"
        Dim data As SqlDataReader
        conectar()
        comando = New SqlCommand(st, conexion)
        Try
            data = comando.ExecuteReader()
            Return data
        Catch ex As Exception
        End Try
    End Function

    Public Shared Function getTareasInstanciadas(ByVal email As String) As SqlDataReader
        Dim st = "select * from EstudiantesTareas where Email='" & email & "'"
        Dim data As SqlDataReader
        conectar()
        comando = New SqlCommand(st, conexion)
        Try
            data = comando.ExecuteReader()
            Return data
        Catch ex As Exception
        End Try
    End Function

    Public Shared Function insertTarea(ByVal email As String, ByVal codTarea As String, ByVal horasEstimadas As Integer, ByVal horasReales As Integer) As String
        Dim st = "insert into EstudiantesTareas (Email,CodTarea,HEstimadas,HReales) values 
                    ('" & email & "','" & codTarea & "','" & horasEstimadas & "','" & horasReales & "')"
        Dim data As String
        Dim numregs As Integer
        conectar()
        comando = New SqlCommand(st, conexion)
        Try
            numregs = comando.ExecuteNonQuery()
        Catch ex As Exception
            Return ex.Message
        End Try
        Return (numregs & " registro(s) insertado(s) en la BD ")
    End Function

    Public Shared Function getAsignaturasProfesor(ByVal email As String) As SqlDataReader
        Dim st = "select * from (Asignaturas left join GruposClase on Asignaturas.codigo=GruposClase.codigoasig)left join ProfesoresGrupo on GruposClase.codigo=ProfesoresGrupo.codigogrupo where ProfesoresGrupo.email='" & email & "'"
        Dim data As SqlDataReader
        conectar()
        comando = New SqlCommand(st, conexion)
        Try
            data = comando.ExecuteReader()
            Return data
        Catch ex As Exception
        End Try
    End Function




End Class

