<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Alumno.aspx.vb" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="height: 437px">
    <form id="form1" runat="server">
    <div style="height: 434px; background-color: #FFFF99;">
    
        Bienvenido
        <asp:Label ID="usuario" runat="server" Font-Bold="True" Font-Underline="True" ForeColor="Blue"></asp:Label>
        <br />
        <br />
        Seleccionar asignatura:<br />
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" CausesValidation="False" Text="Ver Tareas" UseSubmitBehavior="False" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" BorderColor="Blue" AutoGenerateSelectButton="True">
        </asp:GridView>
        <br />
        <br />
        <br />
        <br />
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/salir.jpg" Width="80px" />
    
    </div>
    </form>
</body>
</html>
