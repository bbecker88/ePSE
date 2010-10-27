<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ePortafolioMVC.Models.Curso>>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Lista de cursos</h2>
    <% using (Html.BeginBorder())
       {%>
    <% foreach (var item in Model)
       { %>
    <h3>
        <%= Html.Encode(item.CursoId + " " + item.Nombre) %></h3>
    <%if (item.Trabajos.Count == 0) //El curso no tiene trabajos registrados
      {%>
    No existen trabajos registrados para este curso.<br />
    <%}%>
    <%if (item.Trabajos.Count != 0) //El curso tiene trabajos registrados
      {%>
    <table class="table">
        <tr>
            <th>
                Detalles
            </th>
            <th style="width: 200px;">
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
                Editar
            </th>
        </tr>
        <% foreach (var trabajo in item.Trabajos)
           { %>
        <%if (trabajo.FechaFin != null && trabajo.FechaFin < DateTime.Today)
          {
              //El trabajo ya no esta vigente%>
        <tr class="ya-no-vigente">
            <%}
          else if (trabajo.FechaInicio != null && trabajo.FechaInicio > DateTime.Today)
          {
              //El trabajo aun no esta vigente{%>
            <tr class="aun-no-vigente">
                <%}
          else if ((trabajo.FechaInicio <= DateTime.Today || trabajo.FechaInicio == null) && (trabajo.FechaFin >= DateTime.Today || trabajo.FechaFin == null))
          {
              //El trabajo esta vigente%>
                <tr class="vigente">
                    <%} %>
                    <td>
                        <%= Html.ActionLink("Detalles", "Details", new { id = trabajo.TrabajoId})%>
                    </td>
                    <td style="width: 200px; text-align: left;">
                        <%= Html.Encode(trabajo.Nombre) %>
                    </td>
                    <td>
                        <%= Html.Encode(trabajo.FechaInicio.HasValue?trabajo.FechaInicio.Value.ToShortDateString():"-") %>
                    </td>
                    <td>
                        <%= Html.Encode(trabajo.FechaFin.HasValue ? trabajo.FechaFin.Value.ToShortDateString() : "-")%>
                    </td>
                    <td>
                    
                        <%if(trabajo.EsGrupal) {%>
                        <img src="<%=Url.Content("~/Content/images/imgGrupal.png")%>" title="Grupal"/>
                        <%} else{%>
                        <img src="<%=Url.Content("~/Content/images/imgIndividual.png")%>" title="Individual"/>
                        <%}%>
                        
                        
                    </td>
                    <td>
                        <a href="<%= Url.Action("Edit", "Professor", new { id = trabajo.TrabajoId })%>">
                            <img src="<%=Url.Content("~/Content/images/imgEditarTrabajo.png")%>" /></a>
                    </td>
                </tr>
                <%  } %>
    </table>
    <% } %>
    <br />
    <% } %>
    <%} %>
</asp:Content>
