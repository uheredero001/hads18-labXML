<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InstanciarTarea.aspx.vb" Inherits="WebApplication1.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="height: 525px">
    <form id="form1" runat="server">
    <div style="height: 530px; background-color: #FF33CC;">
    
        <asp:Image ID="Image1" runat="server" Height="248px" ImageAlign="Right" ImageUrl="~/minions-tareas.jpeg" Width="267px" />
        Alumno:
        <asp:Label ID="Label4" runat="server" BackColor="White" Font-Size="Larger" Text="Label"></asp:Label>
        <br />
        <br />
        Cod. tarea:
        <asp:Label ID="Label1" runat="server" Text="Label" BackColor="Yellow" Font-Size="Larger"></asp:Label>
        <br />
        <br />
        Descripcion tarea: <asp:Label ID="Label2" runat="server" Text="Label" BackColor="Yellow" Font-Size="Larger"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        Horas estimadas: <asp:Label ID="Label3" runat="server" Text="Label" BackColor="Yellow" Font-Size="Larger"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        Horas realizadas:
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="*" Font-Size="Larger" ForeColor="#000066"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Debe ser 1 cifra" ForeColor="#000066" ValidationExpression="\d{1}"></asp:RegularExpressionValidator>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Instanciar" />
        <br />
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" ForeColor="Blue" PostBackUrl="~/Alumno.aspx">Volver</asp:LinkButton>
    
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    
        <br />
        <br />
        <asp:Label ID="Label5" runat="server"></asp:Label>
    
    </div>
    </form>
</body>
</html>
