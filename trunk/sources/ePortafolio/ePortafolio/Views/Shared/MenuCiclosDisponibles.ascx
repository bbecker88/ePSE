<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.String>" %>

	<ul class="listav">
	    <% var PeriodosMatriculados = (List<ePortafolio.Models.SSIA.Entities.PeriodosBE>)Session.Get(GlobalKey.PeriodosMatriculados);
             if (PeriodosMatriculados != null)
                foreach (var periodo in PeriodosMatriculados){
                    if(periodo.PeriodoId == Model){%>
                        <li><a href="<%= Url.Action("MostrarTrabajos", new { PeriodoId = periodo.PeriodoId })%>" class="current"><span class="fix-icon"><%= periodo.PeriodoId%></span></a></li>
                    <%} else {%>
                        <li><%= Html.ActionLink(periodo.PeriodoId, "MostrarTrabajos", new { PeriodoId = periodo.PeriodoId })%></li>
                    <%} %>
	            <%} %>
    </ul>
