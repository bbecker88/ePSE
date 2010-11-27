<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.ProfessorDetailsViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3 style="text-align: center">
        <%= Html.Encode(Model.Curso.Codigo + " " + Model.Curso.Nombre) %></h3>
    <h2 style="text-align: center">
        <%= Html.Encode(Model.Trabajo.Nombre) %></h2>
    <h3 style="text-align: center">
        <%= Html.Encode("Detalles") %></h4>
        <%bool alt = false; %>
        
        <%= Html.ResultadoProgramaTable(Model.ResultadoPrograma) %>
        
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
            <%=Html.Encode("Entregados ("+Model.AlumnosGrupoEntregados.Count.ToString()+")") %>
        </h3>
        <br />
        <table class="table">
            <tr class="entregado">
                
                <%if (Model.Trabajo.EsGrupal)
                  { %>
                <th style="width: 270px;">
                <%}
                  else
                  {%>
                <th style="width: 365px;">  
                <%} %>
                    Integrantes
                </th>
                <%if (Model.Trabajo.EsGrupal)
                  { %>
                <th>
                    Rol
                </th>
                <%} %>
                <th>
                    Nota
                </th>
                <th>
                    Opciones
                </th>
            </tr>
            <% foreach (KeyValuePair<BEGrupo, List<BEAlumno>> dictionaryItem in Model.AlumnosGrupoEntregados)
               { alt=!alt;
                   %>
            <tr class="<%= alt?"alt":"" %>">

                </td>
                <td style="text-align:left">
                    <% foreach (BEAlumno Alumno in dictionaryItem.Value)
                       { %>
                    <%= Html.Encode(Alumno.Nombre)%> <br />
                    <%} %>
                </td>
                <%if(Model.Trabajo.EsGrupal){ %>
                <td>
                    <% foreach (BEAlumno Alumno in dictionaryItem.Value)
                       { %>
                    <%if (dictionaryItem.Key.Lider.AlumnoId == Alumno.AlumnoId)
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
                    <%} %>
                <td>
                    <%= Html.Encode(dictionaryItem.Key.Nota)%>
                </td>
                <td>
                    <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgDownload.png"), "DownloadFiles", "Descargar", new { GrupoId = dictionaryItem.Key.GrupoId })%>
                    <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgCheck.png"), "GradeAssignment", "Evaluar", new { TrabajoId = Model.Trabajo.TrabajoId, GrupoId = dictionaryItem.Key.GrupoId, Origen = "Details", EsEditable = true })%>
                </td>
            </tr>
            <%  } %>
        </table>
        <br />
        <br />
        <%if (!Model.Trabajo.FechaFin.HasValue || Model.Trabajo.FechaFin.Value >= DateTime.Today)
          { %>
        <h3>
            <%=Html.Encode("Pendientes ("+Model.AlumnosGrupoPendientes.Count.ToString()+")") %>
        </h3>
        <%}
          else
          {%>
        <h3>
            <%=Html.Encode("No entregados (" + Model.AlumnosGrupoPendientes.Count.ToString() + ")")%>
        </h3>
        <%}%>
        <br />
        <table class="table">
            <tr class="noentregado">
                <th style="width: 550px;">
                    Integrantes
                </th>
            </tr>
            <% foreach (KeyValuePair<BEGrupo, List<BEAlumno>> dictionaryItem in Model.AlumnosGrupoPendientes)
               {
                   alt=!alt;
                   %>
            <tr class="<%= alt?"alt":"" %>">
                <td style="text-align:left">
                        <% foreach (BEAlumno Alumno in dictionaryItem.Value)
                           { %>
                                <%= Html.Encode(Alumno.Nombre) %>
                                <br />
                        <%} %>
                </td>
            </tr>
            <%  } %>
        </table>
        <br />
        <%= Html.Encode("* EI = Evaluacion incompleta / NE = No evaluado") %>  
        <br />
        <% } %>
        <% } %>
        <div>
            <%= Html.ActionLink("Regresar", "Index") %>
        </div>
</asp:Content>
