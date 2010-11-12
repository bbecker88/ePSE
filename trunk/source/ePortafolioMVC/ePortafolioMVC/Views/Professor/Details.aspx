<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.DetalleTrabajoProfesorViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3 style="text-align: center">
        <%= Html.Encode(Model.Trabajo.CursoId + " " + Model.Trabajo.Curso.Nombre) %></h3>
    <h2 style="text-align: center">
        <%= Html.Encode(Model.Trabajo.Nombre) %></h2>
    <h3 style="text-align: center">
        <%= Html.Encode("Detalles") %></h4>
        <%bool alt = false; %>
        <% using (Html.BeginBorder())
           {%>
        <h3>
            Detalles del trabajo</h3>
        <br />
        <table class="table">
            <tr>
                <th style="width: 270px">
                    Nombre
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
            <tr>
                <td style="text-align: left">
                    <%= Html.Encode(Model.Trabajo.Nombre) %>
                </td>
                <td>
                    <%= Html.Encode(Model.Trabajo.FechaInicio.HasValue?Model.Trabajo.FechaInicio.Value.ToShortDateString():"-") %>
                </td>
                <td>
                    <%= Html.Encode(Model.Trabajo.FechaFin.HasValue ? Model.Trabajo.FechaFin.Value.ToShortDateString() : "-")%>
                </td>
                <td>
                    <%if (Model.Trabajo.EsGrupal)
                      { %>
                    <img src="<%=Url.Content("~/Content/images/imgGrupal.png")%>"  title="Grupal"/>
                    <%}
                      else
                      { %>
                    <img src="<%=Url.Content("~/Content/images/imgIndividual.png")%>" title="Individual"/>
                    <%} %>
                </td>
            </tr>
        </table>
        <%} %>
        <% using (Html.BeginBorder())
           {%>
        <h3>
            Acciones</h3>
        <br />
        <%= Html.ActionLink("Ver consolidado de notas", "Grades", new { TrabajoId = Model.Trabajo.TrabajoId,Origen="Details" }, new { style="font-size:medium;"})%>
        <br />
        <br />
        <%} %>
        <% using (Html.BeginBorder())
           {%>
        <% using (Html.BeginForm())
           {%>
        <h3>
            <%=Html.Encode("Entregados ("+Model.ListGruposEntregado.Count.ToString()+")") %>
        </h3>
        <br />
        <table class="table">
            <tr class="entregado">
                <th style="width: 270px;">
                    Integrantes
                </th>
                <th>
                    Rol
                </th>
                <th>
                    Nota
                </th>
                <th>
                    Opciones
                </th>
            </tr>
            <% foreach (var grupo in Model.ListGruposEntregado)
               { alt=!alt;
                   %>
            <tr class="<%= alt?"alt":"" %>">
            
            
                </td>
                <td style="text-align:left">
                    <% foreach (var integrante in grupo.AlumnosGrupos)
                       { %>
                    <%= Html.Encode(integrante.Alumno.Nombre) %> <br />
                    <%} %>
                </td>
                <td>
                    <% foreach (var integrante in grupo.AlumnosGrupos)
                       { %>
                    <%if (integrante.EsLider)
                      { %>
                    <img src="<%=Url.Content("~/Content/images/imgGrupoLider.png")%>"  title="Lider de grupo"/>
                    <%}
                      else
                      { %>
                    <img src="<%=Url.Content("~/Content/images/imgGrupoMiembro.png")%>"  title="Miembro de grupo"/>
                    <%} %>
                    <br />
                    <%} %>
                </td>
                <td>
                    <%= Html.Encode(grupo.Nota) %>
                </td>
                <td>
                    <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgDownload.png"),"DownloadFiles","Descargar", new { GrupoId = grupo.GrupoId })%>
                    <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgCheck.png"), "GradeAssignment", "Evaluar", new {TrabajoId=Model.Trabajo.TrabajoId, GrupoId = grupo.GrupoId, Origen = "Details", Editable = true })%>
                </td>
            </tr>
            <%  } %>
        </table>
        <br />
        <br />
        <%if (!Model.Trabajo.FechaFin.HasValue || Model.Trabajo.FechaFin.Value >= DateTime.Today)
          { %>
        <h3>
            <%=Html.Encode("Pendientes ("+Model.ListGruposPendiente.Count.ToString()+")") %>
        </h3>
        <%}
          else
          {%>
        <h3>
            <%=Html.Encode("No entregados (" + Model.ListGruposPendiente.Count.ToString() + ")")%>
        </h3>
        <%}%>
        <br />
        <table class="table">
            <tr class="noentregado">
                <th style="width: 270px;">
                    Integrantes
                </th>
                <th>
                    Rol
                </th>
                <th>
                    Nota
                </th>
                <th >
                    Opciones
                </th>
            </tr>
            <% foreach (var grupo in Model.ListGruposPendiente)
               {
                   alt=!alt;
                   %>
            <tr class="<%= alt?"alt":"" %>">
                <td style="text-align:left">
                        <% foreach (var integrante in grupo.AlumnosGrupos)
                           { %>
                                <%= Html.Encode(integrante.Alumno.Nombre) %>
                                <br />
                        <%} %>
                </td>
                <td>
                    <% foreach (var integrante in grupo.AlumnosGrupos)
                       { %>
                    <%if (integrante.Grupo.Trabajo == null)
                      { %>
                    <img src="<%=Url.Content("~/Content/images/imgGrupoSin.png")%>" title="Sin grupo" />
                    <%}
                      else if (integrante.EsLider)
                      { %>
                    <img src="<%=Url.Content("~/Content/images/imgGrupoLider.png")%>" title="Lider de grupo"/>
                    <%}
                      else
                      { %>
                    <img src="<%=Url.Content("~/Content/images/imgGrupoMiembro.png")%>" title="Miembro de grupo"/>
                    <%} %>
                    <br />
                    <%} %>
                </td>
                <td>
                    <%= Html.Encode(grupo.Nota) %>
                </td>
                <td>
                    <%if (grupo.Trabajo != null)
                      { %>
                    <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgCheck.png"), "GradeAssignment", "Evaluar", new {TrabajoId=Model.Trabajo.TrabajoId, GrupoId = grupo.GrupoId, Origen = "Details", Editable = true })%>
                    <%}%>
                </td>
            </tr>
            <%  } %>
        </table>
        <br />
        <%= Html.Encode("* IN = Evaluacion imcompleta / NE = No evaluado / NA = No aplica") %>  
        <br />
        <% } %>
        <% } %>
        <div>
            <%= Html.ActionLink("Regresar", "Index") %>
        </div>
</asp:Content>
