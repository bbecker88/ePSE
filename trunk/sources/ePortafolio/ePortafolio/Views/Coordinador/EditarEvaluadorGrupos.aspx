<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditarEvaluadorGrupos
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="CiclosContent" runat="server">
    <% Html.RenderPartial("MenuCiclosDisponibles", Session.Get(GlobalKey.ActualPeriodoId)); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="block">
    <h2>EditarEvaluadorGrupos</h2>



    <% using (Ajax.BeginForm("MostrarResultadoBusquedaEvaluadorGrupos", new AjaxOptions() { UpdateTargetId = "divResultadoBusqueda", OnSuccess = "updatePlaceholder" }))
       { %>

    Filtro de curso : <%= Html.TextBox("Filtro")%>
    
    <%= Html.Submit("Buscar", new { @class = "btn btn_search rounded" })%>
    
    <%} %>
    
    <div id="divResultadoBusqueda"></div>    
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="WelcomeContent" runat="server">
</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="BreadcrumbsContent" runat="server">
</asp:Content>
