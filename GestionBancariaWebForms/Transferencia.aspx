<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SiteCliente.master" AutoEventWireup="true"
    CodeFile="Transferencia.aspx.cs" Inherits="Transferencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Realizar transferencia</h1>
    <table>
        <tr>
            <td>
                Cuenta origen
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlCuentas">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Cuenta destino
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtCuentaDestino"></asp:TextBox><asp:Button Text="Buscar" runat="server" ID="btnBuscaar"
                    OnClick="btnBuscaar_Click" />
            </td>
        </tr>
        <tr>
            <td>
                Titular
            </td>
            <td>
                <asp:Label ID="lblTitular" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Monto
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtMonto">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Moneda
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlMoneda">
                    <asp:ListItem runat="server" Text="USD" Value="USD"></asp:ListItem>
                    <asp:ListItem runat="server" Text="UYU" Value="UYU"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
              <asp:Button runat="server" ID="btnTransfer" Text="Transferir" 
                    onclick="btnTransfer_Click"  />
            </td>
        </tr>
    </table>

    <asp:Label runat="server" id="lblInfo"></asp:Label>

</asp:Content>
