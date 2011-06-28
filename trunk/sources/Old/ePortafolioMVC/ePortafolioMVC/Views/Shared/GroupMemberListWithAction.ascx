<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolioMVC.ViewModels.SharedGroupMemberListViewModel>" %>
<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<% int alternate = 0; %>
<div class="curso">
    <h1>
        INTEGRANTES</h1>
    <% if (Model.AlumnosGrupo.Count == 0)
       { %>
    El grupo no tiene integrantes registrados.
    <%}
       else
       { %>
    <table border="0" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th width="70">
                    Codigo
                </th>
                <th width="320">
                    Nombre
                </th>
                <th width="40">
                    Rol
                </th>
                <th width="40">
                    Acciones
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (BEAlumno Alumno in Model.AlumnosGrupo)
               { %>
            <%= (alternate++) % 2 == 0 ? "<tr class=\"gray\">" : "<tr>"%>
            <td>
                <%= Html.Encode(Alumno.AlumnoId)%>
            </td>
            <td style="text-align: left; padding-left: 8px;">
                <%= Html.Encode(Alumno.Nombre)%>
            </td>
            <td>
                <% if (Alumno.AlumnoId == Model.Grupo.Lider.AlumnoId)
                   {%>
                <a title="L&iacute;der"><span class="rol lider"><h5>l&iacute;der</h5></span></a>
                <%}
                   else
                   {%>
                <a title="Integrante"><span class="rol normal"><h5>normal</h5></span></a>
                <%}%>
            </td>
            <td>
                <% if (Model.Grupo.Lider.AlumnoId == Model.Alumno.AlumnoId && Alumno.AlumnoId != Model.Alumno.AlumnoId)
                   {
                       //El alumno logueado es lider y el alumno analizado no es el logueado%>
                <%= Html.ActionLink("Eliminar", "DeleteStudent", new { GrupoId = Model.Grupo.GrupoId, AlumnoId = Alumno.AlumnoId , TrabajoId=Model.Trabajo.TrabajoId})%>
                <%= Html.ActionLink("Lider", "MakeLeader", new { GrupoId = Model.Grupo.GrupoId, AlumnoId = Alumno.AlumnoId, TrabajoId = Model.Trabajo.TrabajoId })%>
                <%}
                   else if (Model.Grupo.Lider.AlumnoId != Model.Alumno.AlumnoId && Alumno.AlumnoId == Model.Alumno.AlumnoId)
                   {
                       //El alumno logueado no es lider y el alumno analizado es el logueado%>
                <%= Html.ActionLink("Retirar", "WithdrawStudent", new { GrupoId = Model.Grupo.GrupoId, AlumnoId = Alumno.AlumnoId })%>
                <%}
                   else if (Model.Grupo.Lider.AlumnoId == Model.Alumno.AlumnoId && Alumno.AlumnoId == Model.Alumno.AlumnoId)
                   {
                       //El alumno logueado es lider y el alumno analizado es el logueado%>
                <%= Html.ActionLink("Elimin. Grupo", "DeleteGroup", new {GrupoId = Model.Grupo.GrupoId})%>
                <%}%>
            </td>
            </tr>
            <%  } %>
        </tbody>
    </table>
    <%} %>
</div>
