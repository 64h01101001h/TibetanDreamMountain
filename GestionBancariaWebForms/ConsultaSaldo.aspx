<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SiteCliente.master" AutoEventWireup="true"
    CodeFile="ConsultaSaldo.aspx.cs" Inherits="ConsultaSaldo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Consulta saldo</h1>
    <asp:Repeater ID="CuentasListRepeater" runat="server">
        <HeaderTemplate>
            <table style="width: 100%">
                <tr>
                    <th>
                        <asp:Label runat="server" ID="lblNumCuenta" Text="Num Cuenta"></asp:Label>
                    </th>
                    <th>
                        <asp:Label runat="server" ID="lblNombre" Text="Sucursal"></asp:Label>
                    </th>
                    <th>
                        <asp:Label runat="server" ID="lblMoneda" Text="Moneda"></asp:Label>
                    </th>
                    <th>
                        <asp:Label runat="server" ID="lblSaldo" Text="Saldo"></asp:Label>
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# Eval("IDCUENTA")%>
                </td>
                <td>
                    <%# Eval("SUCURSAL.NOMBRE") %>
                </td>
                <td>
                    <%# Eval("MONEDA") %>
                </td>
                <td>
                    <%# Eval("SALDO") %>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Label runat="server" ID="lblInfo"></asp:Label>
</asp:Content>
