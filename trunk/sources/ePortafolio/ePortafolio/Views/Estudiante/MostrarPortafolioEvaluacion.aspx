<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolio.ViewModel.MostrarPortafolioEvaluacionEstudianteViewModel>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="CiclosContent" runat="server">
	<ul class="listav">
	    <li><a class="current"><span class="fix-icon">Outcomes</span></a></li>
	    <% var Outcomes = Model.OutcomesAlumno.Select(x=>new {OutcomeId = x.OutcomeId, NombreOutcome=x.NombreOutcome}).OrderBy(x=>x.NombreOutcome).Distinct();
        foreach (var Outcome in Outcomes){%>
                <li><a href="#<%= Outcome.OutcomeId%>"><%= Outcome.NombreOutcome%></a></li>
	    <%} %>
    </ul>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode("ePortafolio SE | Portafolio de Evaluación")%>
</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="BreadcrumbsContent" runat="server">
    <ul class="listah">
    	<li><a href="/" class="logo-30"></a></li>
    	<li><a href="/" ><%= Html.Encode("Portafolio de Evaluación")%></a></li>
    </ul>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <% foreach(var outcome in Model.OutcomesAlumno) {%>
            <div >
                <% Html.RenderPartial("MostrarDetalleOutcome", new ePortafolio.ViewModel.MostrarDetalleOutcomeEstudianteViewModel(outcome.OutcomeId, Model.Alumno.AlumnoId)); %>
            </div>
        <%} %>
        
        <div style="clear:both;"></div>
    </div>

</asp:Content>
