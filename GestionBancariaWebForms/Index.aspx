﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Site.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>
<%@ Register Assembly="Controles" Namespace="Controles" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>
        <cc1:header id="Header" header_text="Log In" image_url="~/Images/login.png" runat="server" />
    </h1>
    <p>
        para ingresar al sistema debe ser un usuario registrado</p>
    <p>
        Si ya se encuentra registrado, ingrese su nombre de usuario y contraseña.</p>
    <p>
      <%--  <asp:LinkButton ID="lnkbuttonRegistro" PostBackUrl="~/Registro.aspx" runat="server"
            Text="Registrarse en el sistema"></asp:LinkButton></p>--%>
</asp:Content>

