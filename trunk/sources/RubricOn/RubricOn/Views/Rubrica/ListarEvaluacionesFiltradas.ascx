<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<RubricOn.ViewModel.ListarEvaluacionesFiltradasViewModel>" %>

    <table style="width:100%">
        <tr>
            <th>
                Id
            </th>
            <th>
                Artefacto
            </th>
            <th>
                Rubrica
            </th>
            <th>
                Version
            </th>
            <th>
                Evaluado
            </th>
            <th>
                Evaluador
            </th>
            <th>
                Fecha
            </th>
            <th>
                Resultado
            </th>
            <th>
                Acciones
            </th>
        </tr>

    <% foreach (var item in Model.Evaluaciones) {
           var Resultado = Model.Resultados.SingleOrDefault(x => x.EvaluacionId == item.EvaluacionId);
           var ResultadoText = Resultado != null ? Resultado.Resultado : "N/A";
           %>
    
        <tr>
            <td>
                <%= Html.ActionLink(item.EvaluacionId.ToString("D6"), "VerVersionRubrica", new { RubricaId = item.RubricaId, Version = item.Version, TipoArtefacto = item.TipoArtefacto, EvaluacionId = item.EvaluacionId })%>
            </td>                        
            <td>
                <%= Html.ActionLink(item.TipoArtefacto, "ListarEvaluaciones", new { TipoArtefacto = item.TipoArtefacto})%>
            </td>
            <td>
                <%= Html.ActionLink(item.RubricaId, "ListarEvaluaciones", new { RubricaId = item.RubricaId })%>
            </td>
            <td>
                <%= Html.ActionLink(item.Version, "ListarEvaluaciones", new { Version = item.Version, RubricaId = item.RubricaId, TipoArtefacto = item.TipoArtefacto })%>
            </td>
            <td>
                <%= Html.ActionLink(item.CodigoEvaluadoId, "ListarEvaluaciones", new { CodigoEvaluadoId = item.CodigoEvaluadoId })%>
            </td>
            <td>
                <%= Html.ActionLink(item.CodigoEvaluadorId, "ListarEvaluaciones", new { CodigoEvaluadorId = item.CodigoEvaluadorId })%>
            </td>

            <td>
                <span title="<%= Html.Encode(item.FechaEvaluacion.ToString()) %>"><%= Html.Encode(item.FechaEvaluacion.ToShortDateString()) %></span>
            </td>
            <td>
                <%= Html.Encode(ResultadoText)%>
            </td>
            
            <td>
                <%= Html.ActionLink("Ver", "VerVersionRubrica", new { RubricaId = item.RubricaId, Version = item.Version, TipoArtefacto = item.TipoArtefacto, EvaluacionId = item.EvaluacionId}) %>|
                <%= Html.ActionLink("Eliminar", "EliminarEvaluacion", new { EvaluacionId = item.EvaluacionId}) %>
            </td>
        </tr>    
    <% } %>

    </table>

