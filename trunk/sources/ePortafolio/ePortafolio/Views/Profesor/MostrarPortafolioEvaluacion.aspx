<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolio.ViewModel.MostrarPortafolioEvaluacionProfesorViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode("ePortafolio SE | Portafolio de Evaluación")%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="CiclosContent" runat="server">
	<ul class="listav">
	    <li><a class="current"><span class="fix-icon">Outcomes</span></a></li>
	    <% var Outcomes = Model.Outcomes.Select(x=>new {OutcomeId = x.OutcomeId, NombreOutcome=x.Outcome}).OrderBy(x=>x.NombreOutcome).Distinct();
        foreach (var Outcome in Outcomes){%>
                <li><a href="#<%= Outcome.OutcomeId%>"><%= Outcome.NombreOutcome%></a></li>
	    <%} %>
    </ul>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <% 
            var OutcomesProfesor = Model.EvaluacionesOutcomeProfesor.Select(x => x.OutcomeId).Distinct().ToList();
            foreach(var outcome in OutcomesProfesor) {
                var EvaluacionesOutcomeProfesor = Model.EvaluacionesOutcomeProfesor.Where(x => x.OutcomeId == outcome).ToList();
                %>
            <div >
                <% Html.RenderPartial("MostrarDetalleOutcome", new ePortafolio.ViewModel.MostrarDetalleOutcomeProfesorViewModel(outcome,Model.PeriodoId,Model.Profesor.ProfesorId, EvaluacionesOutcomeProfesor)); %>
            </div>
        <%} %>
        
        <div style="clear:both;"></div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="WelcomeContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BreadcrumbsContent" runat="server">
    <ul class="listah">
    	<li><a href="/" class="logo-30"></a></li>
    	<li><a href="/" ><%= Html.Encode("Portafolio de Evaluación")%></a></li>
    </ul>
</asp:Content>
