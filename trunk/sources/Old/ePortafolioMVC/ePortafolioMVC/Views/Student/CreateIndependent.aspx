<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/StudentSite.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.Models.Entities.BETrabajo>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavigationBar" runat="server">
    <li>
        <%= Html.ActionLink(((BEPeriodo)Session["VistaPeriodo"]).Nombre,"SwitchPeriod",new {PeriodoId = ((BEPeriodo)Session["VistaPeriodo"]).PeriodoId} )%></li>
    <li>
        <%= Html.Encode("Crear trabajo independiente") %></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3 style="text-align: center">
        <%= Html.Encode("Crear trabajo independiente") %></h3>
    <% using (Html.BeginForm())
       {%>
    <div class="curso">
        <h1>
            Título</h1>
        <%= Html.TextBoxFor(x=>x.Nombre) %>
        <br />
        <input type="submit" class="Button" value="Crear" />
    </div>
    <%} %>
    <div>
        <%= Html.ActionLink("Regresar", "Index") %>
    </div>
</asp:Content>
