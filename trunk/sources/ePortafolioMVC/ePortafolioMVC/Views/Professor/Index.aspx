<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.ProfessorIndexViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Lista de cursos</h2>
    <% using (Html.BeginBorder())
       {%>
    <% foreach (KeyValuePair<BECurso,List<BETrabajo>> dictionaryItem in Model.TrabajosCurso)
       { %>
    <h3>
        <%= Html.Encode(dictionaryItem.Key.Codigo + " " + dictionaryItem.Key.Nombre)%></h3>
    <%if (dictionaryItem.Value.Count == 0) //El curso no tiene trabajos registrados
      {%>
    No existen trabajos registrados para este curso.<br />
    <%}%>
    <%if (dictionaryItem.Value.Count != 0) //El curso tiene trabajos registrados
      {%>
    <table class="table">
        <tr>
            <th style="width: 270px;">
                Trabajo
            </th>
            <th>
                Inicio
            </th>
            <th>
                Fin
            </th>
            <th>
                Modo
            </th>
            <th>
                Opciones
            </th>
        </tr>
        <% foreach (BETrabajo Trabajo in dictionaryItem.Value)
           { %>
        <%if (Trabajo.FechaFin != null && Trabajo.FechaFin < DateTime.Today)
          {
              //El trabajo ya no esta vigente%>
        <tr class="ya-no-vigente">
            <%}
          else if (Trabajo.FechaInicio != null && Trabajo.FechaInicio > DateTime.Today)
          {
              //El trabajo aun no esta vigente{%>
            <tr class="aun-no-vigente">
                <%}
          else if ((Trabajo.FechaInicio <= DateTime.Today || Trabajo.FechaInicio == null) && (Trabajo.FechaFin >= DateTime.Today || Trabajo.FechaFin == null))
          {
              //El trabajo esta vigente%>
                <tr class="vigente">
                    <%} %>
                    <td style="text-align: left;">
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
                        <img src="<%=Url.Content("~/Content/images/imgGrupal.png")%>" title="Grupal"/>
                        <%} else{%>
                        <img src="<%=Url.Content("~/Content/images/imgIndividual.png")%>" title="Individual"/>
                        <%}%>
                        
                        
                    </td>
                    <td>
                    
                    <%if (dictionaryItem.Key.Coordinador.ProfesorId == Model.ActualProfesor.ProfesorId)
                      {%>
                        <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgEditarTrabajo.png"), "Edit", "Editar", new { TrabajoId = Trabajo.TrabajoId })%>
                        <%} %>
                        <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgRubricas.png"), "GradeAssignment", "Ver rubricas", new { TrabajoId = Trabajo.TrabajoId, GrupoId = 0, Origen = "Index", EsEditable = false })%>
                        <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgConsolidado.png"), "Grades", "Ver consolidado de notas", new { TrabajoId = Trabajo.TrabajoId, Origen = "Index" })%>
                    </td>
                </tr>
                <%  } %>
    </table>
    <% } %>
    <br />
    <% } %>
    <%} %>
</asp:Content>
