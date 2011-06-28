<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolio.ViewModel.MostrarDetalleOutcomeEstudianteViewModel>" %>

<% var GUID = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); %>
<div id="MostrarDetalleOutcomeContainer<%= GUID %>">

    <div class="block">
        <div>
            <a id="<%= Model.Outcome.OutcomeId %>"></a>
            <h1><b><%= Model.Outcome.Outcome %></b></h1>
            <%= Model.Outcome.Descripcion %>
            
        </div>
        
        <% var RutaVerRubricaEvaluada = Model.EvaluacionesOutcomeProfesor !=null && Model.EvaluacionesOutcomeProfesor.EvaluacionId != null ? Url.Action("VerEvaluacion", "Evaluacion", new { EvaluacionId = Model.EvaluacionesOutcomeProfesor.EvaluacionId }) :
                                                                                             Url.Action("VerRubricaOutcome", "Evaluacion", new { OutcomeId = Model.Outcome.OutcomeId});%>
            <div class="block">
                Nota: <%= Model.EvaluacionesOutcomeProfesor ==null ? "NE": Model.EvaluacionesOutcomeProfesor.Nota%> <%= Html.Link("Ver Rúbrica", RutaVerRubricaEvaluada, true, new { @class = "btn btn_chart rounded" })%> 
            </div>
    
        <div >
            <% if (Model.TrabajosDisponibles.Count > 0){%>
                <a id="btnAgregarTrabajoOutcome<%= GUID %>">(+) Agregar</a>
                <div id="AgregarTrabajoOutcome<%= GUID %>">
                <% using (Ajax.BeginForm("AgregarTrabajoOutcome", new AjaxOptions() {OnSuccess="updatePlaceholder", UpdateTargetId = "MostrarDetalleOutcomeContainer" + GUID }))
                   { %>
                    <%= Html.Hidden("OutcomeId", Model.Outcome.OutcomeId)%>
                    <%= Html.GroupDropList("TrabajoId", Model.GroupDropListItemList, null, new { style="width:300px"})%>
                    <%= Html.Submit("Agregar", new {@class = "btn btn_add rounded" })%>
                <%} %>
                </div>
            <%} %>
            
            <script>
                $('document').ready(function() {
                    $('#AgregarTrabajoOutcome<%= GUID %>').hide();
                    $('#btnAgregarTrabajoOutcome<%= GUID %>').click(function() { $('#AgregarTrabajoOutcome<%= GUID %>').toggle('slow') });
                });
            </script>
            <%= Html.ShowTempMessage() %>
            
            <%if (Model.TrabajosOutcome.Count==0){ %>
                <p style="padding-left:3px">El curso no tiene trabajos registrados</p>
            <%} else { %>
            <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
                <thead>
                    <tr>
                        <th width="70">Periodo</th>
                        <th width="160">Trabajo</th>
                        <th width="240">Curso</th>
                        <th width="70">Acciones</th>
                    </tr>
                </thead>    
                <tbody>
                <%foreach(var trabajoOutcome in Model.TrabajosOutcome){ %>
                <% var trabajo = Model.TrabajosEntregados.SingleOrDefault(x => x.TrabajoId == trabajoOutcome.TrabajoId); %>
                <% if(trabajo == null ) continue;%>
                <% var curso = Model.CursosTrabajos.SingleOrDefault(x => x.CursoId == trabajo.CursoId); %>
                <% if(curso == null) continue;%>
                    <tr>
                        <td><%= trabajo.PeriodoId %></td>
                        <td class="nombre"><%= trabajo.Nombre %></td>
                        <td class="nombre"><%= curso.Codigo + " " + curso.Nombre %></td>
                        <td><%= Ajax.ActionLink("TEXTOREMOVER", "EliminarTrabajoOutcome", new { TrabajoId = trabajo.TrabajoId, OutcomeId = Model.Outcome.OutcomeId }, new AjaxOptions() { OnSuccess = "updatePlaceholder", UpdateTargetId = "MostrarDetalleOutcomeContainer" + GUID }, new { @class = "btn_cancel rounded" }).ToHtmlString().Replace("TEXTOREMOVER", "&nbsp")%></td>
                    </tr>
                 <%} %>
                </tbody>   
            <%} %>    
            </table>
            <script>
                function updatePlaceholder(context) {
                    var html = context.get_data();
                    var placeholder = context.get_updateTarget();
                    $(placeholder).html(html);
                    return false;
                }
            </script>
        </div>
    </div>
</div>