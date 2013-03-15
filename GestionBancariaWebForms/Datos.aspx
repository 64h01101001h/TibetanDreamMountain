<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SiteCliente.master" AutoEventWireup="true"
    CodeFile="Datos.aspx.cs" Inherits="Datos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Cambio de contraseña</h1>
    <table>
        <tr>
            <td>
                Contraseña actual
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPassActual" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Contraseña nueva
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPassNueva1" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Repita nueva contraseña
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPassNueva2" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button runat="server" ID="btnCambiar" Text="Cambiar" onclick="btnCambiar_Click"></asp:Button>
            </td>
        </tr>

    </table>
    <asp:Label runat="server" id="lblInfo"></asp:Label>
</asp:Content>
