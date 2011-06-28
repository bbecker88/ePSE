<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<RubricOn.ViewModel.ListarRubricasArtefactoViewModel>" %>

<% var CurrentDiv = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 6); %>
<div id="<%= CurrentDiv %>">

    <table style="width:100%">
        <tr>
            <th>
                Artefacto
            </th>
            <th>
                Rubrica
            </th>
            <th>
                Descripcion
            </th>
            <th>
                Tipo
            </th>
            <th>
                Version
            </th>
            <th>
                Fecha Creacion
            </th>
            <th></th>
        </tr>

    <% foreach (var item in Model.Rubricas) { %>
    
        <tr>
            <td>
                <%= Html.Encode(item.TipoArtefacto)%>
            </td>
            <td>
                <%= Html.ActionLink(item.RubricaId, "ListarVersionesRubrica", new {RubricaId = item.RubricaId, TipoArtefacto = item.TipoArtefacto })%>
            </td>
            <td>
                <%= Html.Encode(item.Descripcion) %>
            </td>
            <td>
                <%= Html.Encode(item.TipoRubrica) %>
            </td>
            <td>
                <%= Html.Encode(item.Version) %>
            </td>
            <td>
                <span title="<%= Html.Encode(item.FechaCreacion.ToString()) %>"><%= Html.Encode(item.FechaCreacion.ToShortDateString())%></span>
            </td>            
            <td>
                <%= Html.ActionLink("Versiones", "ListarVersionesRubrica", new {RubricaId = item.RubricaId, TipoArtefacto = item.TipoArtefacto })%>|
                <%= Html.ActionLink("Ver", "VerVersionRubrica", new { RubricaId = item.RubricaId, Version = item.Version, TipoArtefacto = item.TipoArtefacto, EvaluacionId = 0}) %> |
                <%= Html.ActionLink("Aplicar", "InicioEvaluacionRubrica", new { RubricaId = item.RubricaId, TipoArtefacto = item.TipoArtefacto})%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Crear nueva rúbrica", "CrearVersionRubrica", new { RubricaId = "", TipoArtefacto = Model.TipoArtefacto!=null?Model.TipoArtefacto.TipoArtefacto:""})%>
    </p>
</div>