<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SiteCliente.master" AutoEventWireup="true"
    CodeFile="ConsultaMovimientos.aspx.cs" Inherits="ConsultaMovimientos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Consulta movimientos</h1>
    <asp:Label ID="lblDesc" runat="server" Text="Ver movimientos a partir del"></asp:Label>
    <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
    <br />
    <asp:Repeater ID="CuentasListRepeater" runat="server" OnItemCommand="CuentasListRepeater_ItemCommand">
        <HeaderTemplate>
            <table style="width: 100%">
                <tr>
                    <th>
                    </th>
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
                    <asp:LinkButton runat="server" Text="Ver Movimientos" ID="lnkCuentaid" CommandName="SELECCIONAR"
                        CommandArgument='<%# Eval("IDCUENTA")%>'></asp:LinkButton>
                </td>
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
    <asp:Label ID="lblFechaEspecifica" runat="server" Text="Filtrar resultados para fecha"></asp:Label><asp:TextBox
        ID="txtFechaEspecifica" runat="server"></asp:TextBox>
    <asp:Button Text="Filtrar" runat="server" ID="btnFiltrar" OnClick="btnFiltrar_Click" />
    <asp:Xml ID="XmlMovimientos" runat="server"></asp:Xml>
    <br />
    <br />
    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
</asp:Content>
