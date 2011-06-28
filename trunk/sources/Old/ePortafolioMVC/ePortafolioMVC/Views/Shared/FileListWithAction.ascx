<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolioMVC.ViewModels.SharedFileListViewModel>" %>
<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<% int alternate = 0; %>
<div class="curso">
    <h1>
        ARCHIVOS</h1>
    <% if (Model.ArchivosGrupo.Count == 0)
       { %>
    El grupo no tiene archivos registrados.
    <%}
       else
       { %>
    <table border="0" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th width="220">
                    Nombre
                </th>
                <th width="70">
                    Fecha
                </th>
                <th width="90">
                    Subido por
                </th>
                <th width="40">
                    Opciones
                </th>
            </tr>
        </thead>
        <tbody>
            <%
                foreach (BEArchivo Archivo in Model.ArchivosGrupo)
                { 
            %>
            <%= (alternate++) % 2 == 0 ? "<tr class=\"gray\">" : "<tr>"%>
            <td style="text-align: left; padding-left:8px;">
                <%= Html.Encode(Archivo.Nombre)%>
            </td>
            <td>
                <%= Html.Encode(Archivo.FechaSubido.Value.ToShortDateString())%>
            </td>
            <td>
                <span title="<%= Html.Encode(Archivo.Alumno.Nombre)%>">
                    <%= Html.Encode(Archivo.Alumno.AlumnoId)%>
                </span>
            </td>
            <td>
                <a href="<%= Html.Encode(Url.Content("~"+Archivo.Ruta))%>">
                    <img src="<%= Url.Content("~/Content/images/imgDownload.png")%>" title="Descargar" /></a>
                <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgDelete.png"), "DeleteFile", "Eliminar", new { TrabajoId = Model.Trabajo.TrabajoId, ArchivoId = Archivo.ArchivoId })%>
            </td>
            </tr>
            <%  } %>
        </tbody>
    </table>
    <%} %>
</div>
