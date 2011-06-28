<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolioMVC.ViewModels.ProfessorStudentListDone>" %>
<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<% int alternate = 0; %>
<div class="curso">
    <h1>
        ENTREGADOS (<%=  Model.AlumnosGrupos.Count.ToString() %>)</h1>
    <table border="0" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th width="330">
                    Alumnos
                </th>

                <th width="70">
                    Nota
                </th>
                <th width="36">
                    Opciones
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (KeyValuePair<BEGrupo, List<BEAlumno>> dictionaryItem in Model.AlumnosGrupos)
               {%>
            <%= (alternate++) % 2 == 0 ? "<tr class=\"gray\">" : "<tr>"%>
            <td style="text-align: left; padding-left: 8px;">
                <%foreach (BEAlumno Alumno in dictionaryItem.Value)
                  {%>
                <%= Html.Encode(Alumno.Nombre)%><br />
                <%} %>
            </td>
            <td>
                <%= dictionaryItem.Key.Nota%>
            </td>
            <td>
                <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgDownload.png"), "DownloadFiles", "Descargar", new { GrupoId = dictionaryItem.Key.GrupoId })%>
                <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgCheck.png"), "GradeAssignment", "Evaluar", new { TrabajoId = dictionaryItem.Key.Trabajo.TrabajoId, GrupoId = dictionaryItem.Key.GrupoId, Origen = "Details", EsEditable = true })%>
            </td>
            </tr>
            <%} %>
        </tbody>
    </table>
    <br />
    <%= Html.Encode("* EI = Evaluacion incompleta / NE = No evaluado") %>
</div>
