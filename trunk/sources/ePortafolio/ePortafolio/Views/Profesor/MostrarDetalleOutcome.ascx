<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolio.ViewModel.MostrarDetalleOutcomeProfesorViewModel>" %>

<% var GUID = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); %>
<div id="MostrarDetalleOutcomeContainer<%= GUID %>">

    <div class="block">
        <div >
            <a id="<%= Model.Outcome.OutcomeId %>"></a>
            <h1><b><%= Model.Outcome.Outcome %></b></h1>
            <%= Model.Outcome.Descripcion %>
        </div>
    
        <div >           
            <%if (Model.Alumnos.Count==0){ %>
                <p style="padding-left:3px">No hay alumnos por evaluar.</p>
            <%} else { %>
            <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
                <thead>
                    <tr>
                        <th width="30"></th>
                        <th width="280">Nombre</th>
                        <th width="70">Nota</th>
                        <th width="160">Acciones</th>
                    </tr>
                </thead>    
                <tbody>
                <%var odd = true; var N = 0;
                  foreach(var alumno in Model.Alumnos){
                      odd = !odd; N++;
                      var ClaseFila = odd ? "odd" : "";
                      var EvaluacionProfesor = Model.EvaluacionesOutcomeProfesor.Where(x => x.OutcomeId == Model.Outcome.OutcomeId && x.AlumnoId == alumno.AlumnoId).FirstOrDefault();

                      var RutaVerRubricaEvaluada = EvaluacionProfesor.EvaluacionId != null ? Url.Action("VerEvaluacion", "Evaluacion", new { EvaluacionId = EvaluacionProfesor.EvaluacionId }) :
                                                                                             Url.Action("VerRubricaOutcome", "Evaluacion", new { OutcomeId = Model.Outcome.OutcomeId});

                      var RutaEvaluarRubrica = Url.Action("EvaluarOutcomeAlumno", "Evaluacion", new { AlumnoId = alumno.AlumnoId, OutcomeId = Model.Outcome.OutcomeId });
                      
                      var Nota = EvaluacionProfesor != null? EvaluacionProfesor.Nota:"NE";
                      %>
                    <tr class="<%= ClaseFila %>">
                        <td><%= N%></td>
                        <td class="nombre"><span class="codigo"><%= alumno.AlumnoId%></span><%= alumno.Nombre%></td>
                        <td><%= Nota%></td>
                        
                        <td>    
                        <%=  Html.ActionLink("Descargar","DescargarOutcome","Profesor",new{AlumnoId = alumno.AlumnoId, OutcomeId= Model.Outcome.OutcomeId},true)%> |
                        <%=  RutaEvaluarRubrica != null ? "" + Html.Link("Evaluar", RutaEvaluarRubrica,true) : ""%> 
                                <%=  RutaVerRubricaEvaluada != null && Nota!="NE" ? " | " + Html.Link("Ver Evaluación", RutaVerRubricaEvaluada,true) : ""%> </td>                           
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