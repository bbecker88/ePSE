<%@ Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolio.ViewModel.Coordinador.MostrarEvaluacionesOutcomesFiltradasViewModel>" %>


    <% if(Model.Evaluaciones.Count == 0){ %>
        <p style="padding-left:3px">No existen evaluaciones de outcomes registrados.</p>          
    <%} else { %>

    <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
        <thead>
            <tr>
                <th width="40">Periodo</th>
                <th width="40">Outcome</th>
                <th width="60">Profesor</th>
                <th width="280">Alumno</th>
                <th width="40">Nota</th>
            </tr>
        </thead>
        <tbody>
        
           <%var odd = true;

             var EvaluacionesFormat = Model.Evaluaciones.Select(x=>new { PeriodoId = x.PeriodoId,
                                                                         OutcomeId = x.OutcomeId,
                                                                         Outcome = Model.Outcomes.First(o=>o.OutcomeId==x.OutcomeId).Outcome,
                                                                         ProfesorId = x.ProfesorId,
                                                                         AlumnoId = x.AlumnoId,
                                                                         Alumno = Model.Alumnos.First(a => a.AlumnoId == x.AlumnoId).Nombre,
                                                                         Nota = x.Nota,
                                                                         EvaluacionId = x.EvaluacionId})
                                                        .OrderByDescending(x=>x.PeriodoId)
                                                        .ThenBy(x=>x.Outcome)
                                                        .ThenBy(x=>x.ProfesorId)
                                                        .ThenBy(x=>x.Alumno)
                                                        .ToList();

             foreach (var Evaluacion in EvaluacionesFormat){
                 odd = !odd; 
                 var ClaseFila = odd ? "odd" : "";
                 var RutaVerRubricaEvaluada = Evaluacion.EvaluacionId != null ? Url.Action("VerEvaluacion", "Evaluacion", new { EvaluacionId = Evaluacion.EvaluacionId }) : "";
                 %>
                <tr class="<%= ClaseFila %>">
                    <td><%= Evaluacion.PeriodoId %></td>
                    <td><%= Evaluacion.Outcome %></td>
                    <td><%= Evaluacion.ProfesorId %></td>
                    <td class="nombre"><span class="codigo"><%= Evaluacion.AlumnoId %></span><%= Evaluacion.Alumno %></td>
                    <td><%= Evaluacion.EvaluacionId != null ? Html.Link(Evaluacion.Nota, RutaVerRubricaEvaluada, true) : Evaluacion.Nota%></td>
                </tr>
        <% }%>
        </tbody>
    </table>
<%} %>