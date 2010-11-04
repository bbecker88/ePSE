<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.DetalleTrabajoProfesorViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Details</h2>
            <% using (Html.BeginBorder())
               {%>
               <%= Html.ActionLink("Generar consolidado de notas", "Grades", new { TrabajoId = Model.Trabajo.TrabajoId})%>
               <br /><br />
       <%} %>
        
    <% using (Html.BeginBorder())
       {%>
    <% using (Html.BeginForm())
       {%>
    <h3>
        Grupos</h3>
    <br />
    <table class="table">
        <tr>
            <th>
                Estado
            </th>
            <th style="width: 200px;">
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
        <% foreach (var grupo in Model.ListGrupos)
           { %>
           
        <tr>
            
                <%if(grupo.ArchivosGrupos.Count!=0) {%>
            <td class="entregado"> Entregado
                <%} else {%>
            <td class="pendiente"> Pendiente
                <%} %>
            </td>
            <td style="width: 200px;">
                <table>
                    <% foreach (var integrante in grupo.AlumnosGrupos)
                       { %>
                    <tr>
                        <td style="text-align: left;">
                            <%= Html.Encode(integrante.Alumno.Nombre) %>
                        </td>
                    </tr>
                    <%} %>
                </table>
            </td>
            <td>
                <% foreach (var integrante in grupo.AlumnosGrupos)
                   { %>
                <%if (integrante.EsLider)
                  { %>
                <img src="<%=Url.Content("~/Content/images/imgGrupoLider.png")%>" />
                <%}
                  else
                  { %>
                <img src="<%=Url.Content("~/Content/images/imgGrupoMiembro.png")%>" />
                <%} %>
                <br />
                <%} %>
            </td>
            <td>
                <%= Html.Encode(grupo.Nota) %>
            </td>
            <td>
                <%= Html.ActionLink("Descargar Todo", "DownloadFiles", new { GrupoId = grupo.GrupoId})%>
                <br />
                <%= Html.ActionLink("Evaluar", "GradeAssignment", new { GrupoId = grupo.GrupoId})%>
            </td>
        </tr>
        <%  } %>
    </table>
    <br />
    <br />
    <h3>
       Alumnos sin grupo</h3>
        <br />
    <% foreach (var integrante in Model.ListAlumnosSinGrupo)
       { %>
    <img src="<%=Url.Content("~/Content/images/imgGrupoSin.png")%>" /> <%= Html.Encode(integrante.Nombre) %>
    <br />
    <%} %>
    <% } %>
    <% } %>
    <div>
        <%= Html.ActionLink("Regresar", "Index") %>
    </div>
</asp:Content>
