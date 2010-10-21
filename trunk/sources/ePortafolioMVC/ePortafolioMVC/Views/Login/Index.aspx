<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.Models.UserAutentication>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Ingreso al sistema</h2>
    <% using (Html.BeginBorder())
       {%>
    <% using (Html.BeginForm())
       {%>
    <%= Html.ValidationSummary(true) %>
    <div class="editor-label">
        <%= Html.LabelFor(model => model.User) %>
    </div>
    <div class="editor-field">
        <%= Html.TextBoxFor(model => model.User) %>
        <%= Html.ValidationMessageFor(model => model.User) %>
    </div>
    <div class="editor-label">
        <%= Html.LabelFor(model => model.Password) %>
    </div>
    <div class="editor-field">
        <%= Html.TextBoxFor(model => model.Password) %>
        <%= Html.ValidationMessageFor(model => model.Password) %>
    </div>
    <a href="https://intranet.upc.edu.pe/Recordatorio.asp">¿Olvidaste tu clave?</a>
    <p>
    <br />
        <input class="button" type="submit" value="Entrar" />
    </p>
    <% } %>
    <% } %>
    <div>
        <%= Html.ActionLink("Ir al inicio", "Index","Home") %>
    </div>
</asp:Content>
