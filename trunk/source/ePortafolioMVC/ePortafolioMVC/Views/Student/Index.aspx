﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ePortafolioMVC.Models.Curso>>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Lista de cursos</h2>
    <% using (Html.BeginBorder())
       {%>
    <% using (Html.BeginForm())
       {%>
    <%= Html.ValidationSummary(true) %>
    <% foreach (var item in Model)
       { %>
    <h3>
        <%= Html.Encode(item.CursoId + " " + item.Nombre) %></h3>
    <%  var AlumnoId = ((ePortafolioMVC.Models.UserInfo)Session["UserInfo"]).Codigo;
        var trabajos = item.Trabajos.Where(t => (t.FechaInicio == null || t.FechaInicio <= DateTime.Now) && (t.FechaFin == null || t.FechaFin >= DateTime.Now));
        if (trabajos.Count() == 0) //El curso no tiene trabajos habilitados
        {%>
    No existen trabajos registrados para este curso.<br />
    <%}%>
    <%if (trabajos.Count() != 0) //El curso tiene trabajos habilitados
      {%>
    <table class="table">
        <tr>
            <th>
                Rol
            </th>
            <th style="width: 270px">
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

        </tr>
        <% foreach (var trabajo in trabajos)
           { %>
        <% var grupoAlumno = trabajo.Grupos.SingleOrDefault(g => g.AlumnosGrupos.Any(a => a.AlumnoId.ToString() == AlumnoId));
                   if (grupoAlumno == null || trabajo.Grupos.SingleOrDefault(g => g.ArchivosGrupos.Any(ag => ag.GrupoId == grupoAlumno.GrupoId))==null) {%>
        <tr class="aun-no-vigente">
            <%} else
              {%>
            <tr class="vigente">
                <%}%>
                <td>
                    <% var grupo = trabajo.Grupos.SingleOrDefault(g => g.AlumnosGrupos.Any(a => a.AlumnoId.ToString() == AlumnoId));
                   if (grupo == null)
                   {%>
                    <img src="<%=Url.Content("~/Content/images/imgGrupoSin.png")%>" title="Sin grupo" />
                    <%
                    }
                   else
                   {
                       if (grupo.AlumnosGrupos.SingleOrDefault(g => g.AlumnoId.ToString() == AlumnoId).EsLider)
                       {%>
                    <img src="<%=Url.Content("~/Content/images/imgGrupoLider.png")%>" title="Lider de grupo" />
                    <%
                    }
                       else
                       {%>
                    <img src="<%=Url.Content("~/Content/images/imgGrupoMiembro.png")%>" title="Miembo de grupo" />
                    <%
                    }
                   }
               
                    %>
                </td>
                <td style="text-align: left;">
                    <%= Html.ActionLink(trabajo.Nombre, "Details", new { id = trabajo.TrabajoId })%>
                </td>
                <td>
                    <%= Html.Encode(trabajo.FechaInicio.HasValue?trabajo.FechaInicio.Value.ToShortDateString():"-") %>
                </td>
                <td>
                    <%= Html.Encode(trabajo.FechaFin.HasValue ? trabajo.FechaFin.Value.ToShortDateString() : "-")%>
                </td>
                <td>
                    <%if (trabajo.EsGrupal)
                  {%>
                    <img src="<%=Url.Content("~/Content/images/imgGrupal.png")%>" title="Grupal" />
                    <%}
                  else
                  {%>
                    <img src="<%=Url.Content("~/Content/images/imgIndividual.png")%>" title="Individual" />
                    <%}%>
                </td>

            </tr>
            <%  } %>
    </table>
    <% } %>
    <br />
    <% } %>
    <%} %>
    <%} %>
</asp:Content>