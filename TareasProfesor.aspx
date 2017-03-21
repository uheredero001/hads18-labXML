<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TareasProfesor.aspx.vb" Inherits="WebApplication1.TareasGenericasProfesor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color: #FFFF66">
    
        Seleccionar la asignatura:<br />
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="AsignaturasProfe">
        </asp:DropDownList>
        <asp:SqlDataSource ID="AsignaturasProfe" runat="server" ConnectionString="<%$ ConnectionStrings:HADS18_usuariosConnectionString %>" SelectCommand="SELECT Asignaturas.Nombre FROM Asignaturas INNER JOIN GruposClase ON Asignaturas.codigo = GruposClase.codigoasig INNER JOIN ProfesoresGrupo ON ProfesoresGrupo.codigogrupo = GruposClase.codigo
WHERE ProfesoresGrupo.email=profe">
            <SelectParameters>
                <asp:SessionParameter Name="profe" SessionField="user" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Insertar Nueva Tarea" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="BaseDeDatosHads18">
        </asp:GridView>
        <asp:SqlDataSource ID="BaseDeDatosHads18" runat="server" ConnectionString="<%$ ConnectionStrings:HADS18_usuariosConnectionString %>" SelectCommand="SELECT TareasGenericas.Codigo, TareasGenericas.Descripcion, TareasGenericas.CodAsig, TareasGenericas.HEstimadas, TareasGenericas.Explotacion, TareasGenericas.TipoTarea FROM ProfesoresGrupo INNER JOIN GruposClase ON ProfesoresGrupo.codigogrupo = GruposClase.codigo INNER JOIN TareasGenericas ON TareasGenericas.CodAsig = GruposClase.codigoasig
WHERE ProfesoresGrupo.email=profe">
            <SelectParameters>
                <asp:SessionParameter Name="profe" SessionField="user" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True" ForeColor="Blue">Volver</asp:HyperLink>
        <br />
    
    </div>
    </form>
</body>
</html>
