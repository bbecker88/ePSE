<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolioMVC.ViewModels.ProfessorAssignmentListViewModel>" %>
<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>

<% int alternate = 0; %>
<div class="curso">
    <h1>
        <%= Html.Encode(Model.Curso.Codigo + " " + Model.Curso.Nombre) %></h1>
    <% if (Model.Trabajos.Count == 0)
       { %>
    El curso no tiene trabajos registrados.
    <%}
       else
       { %>
    <table border="0" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
            
                <th width="260">
                    Trabajo
                </th>
                <th width="70">
                    Inicio
                </th>
                <th width="70">
                    Fin
                </th>

                <th width="36">
                    Modo
                </th>
                <th width="70">
                    Opciones
                </th>
            </tr>
        </thead>
        <tbody>
        <% foreach (BETrabajo Trabajo in Model.Trabajos)
           {
               Boolean EsVigente = (Trabajo.FechaInicio == null || Trabajo.FechaInicio <= DateTime.Today) && (Trabajo.FechaFin == null || Trabajo.FechaFin >= DateTime.Today);
               Boolean EsFinalizado = (Trabajo.FechaFin != null && Trabajo.FechaFin < DateTime.Today);
            %>
        <%= (alternate++) % 2 == 0 ? "<tr class=\"gray\">" : "<tr>"%>
                    <td style="text-align: left; padding-left:8px;">
                        <%= Html.ActionLink(Trabajo.Nombre, "Details", new { TrabajoId = Trabajo.TrabajoId })%>
                    </td>
                    <td>
                        <%= Html.Encode(Trabajo.FechaInicio.HasValue ? Trabajo.FechaInicio.Value.ToShortDateString() : "-")%>
                    </td>
                    <td>
                        <%= Html.Encode(Trabajo.FechaFin.HasValue ? Trabajo.FechaFin.Value.ToShortDateString() : "-")%>
                    </td>
                    
                    <td>
                        <%if (Trabajo.EsGrupal)
                          {%>
                        <a title="Grupal"><span class="mod grupal"><h5>grupal</h5>
                        </span></a>
                        <%}
                          else
                          {%>
                        <a title="Individual"><span class="mod"><h5>individual</h5></span></a>
                        <%}%>
                    </td>
                    <td>
                    
                    <%if (Model.Curso.Coordinador.ProfesorId == Model.ActualProfesor.ProfesorId)
                      {%>
                        <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgEditarTrabajo.png"), "Edit", "Editar", new { TrabajoId = Trabajo.TrabajoId })%>
                        <%} %>
                        <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgRubricas.png"), "GradeAssignment", "Ver rubricas", new { TrabajoId = Trabajo.TrabajoId, GrupoId = 0, Origen = "Index", EsEditable = false })%>
                        <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgConsolidado.png"), "Grades", "Ver consolidado de notas", new { TrabajoId = Trabajo.TrabajoId, Origen = "Index" })%>
                    </td>
                </tr>
                <%  } %>
                </tbody>
    </table>
    <%} %>
</div>