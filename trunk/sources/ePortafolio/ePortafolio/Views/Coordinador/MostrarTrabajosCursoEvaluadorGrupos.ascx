<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolio.ViewModel.MostrarTrabajosCursoEvaluadorGrupos>" %>

<% using (Ajax.BeginForm("MostrarGruposCursoEvaluadorGrupos", new AjaxOptions() { UpdateTargetId = "divGruposTrabajo", HttpMethod = "Get", OnSuccess = "updatePlaceholder" }))
   { %>
<%= Html.HiddenFor(x=>x.CursoId) %>
Trabajo: <%= Html.DropDownListFor(x => x.TrabajoId, new SelectList(Model.Trabajos, "TrabajoId", "Nombre"), "Seleccionar", new { style="width:300px"})%>
<%= Html.Submit("Buscar", new { @class = "btn btn_search rounded" })%>

<%} %>




<div id="divGruposTrabajo"></div>