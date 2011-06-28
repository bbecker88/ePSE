<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolio.ViewModel.MostrarGruposCursoEvaluadorGruposViewModel>" %>



    <div id="mostrarGruposContainer"> 
    
    <%= Html.ShowTempMessage() %>
    <% if(Model.Grupos.Count == 0){%>
        <p style="padding-left:3px">El trabajo no tiene grupos registrados</p>          
    <%}else{ %>
    
        <% using (Ajax.BeginForm(new AjaxOptions() { UpdateTargetId = "mostrarGruposContainer", OnSuccess = "updatePlaceholder" }))
           {%>

        <%= Html.HiddenFor(x => x.TrabajoId) %>

        <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
                <thead>
                    <tr>
                        <th width="20"></th>
                        <th width="200">Nombre</th>
                        <th width="40">Nota</th>
                        <th width="150">Evaluador asignado</th>
                        <th width="70">Asignar a</th>
                    </tr>
                </thead>
                <tbody>
                   <%var odd= true; var N = 0;
                     foreach (var Grupo in Model.Grupos){
                         odd = !odd; N++;
                         var ClaseFila = odd ? "odd" : "";
                         
                         var AlumnosGrupoId = Model.AlumnosGrupos.Where(x => x.GrupoId == Grupo.GrupoId).ToList().Select(x => x.AlumnoId);
                         var AlumnosGrupoDetalle = Model.Alumnos.Where(x => AlumnosGrupoId.Contains(x.AlumnoId)).OrderBy(x => x.Nombre).ToList();
                         var Evaluador = Model.EvaluacionesGruposProfesor.SingleOrDefault(x => x.GrupoId == Grupo.GrupoId);
                         var Profesor = Evaluador == null ? null: Model.Profesores.SingleOrDefault(x => x.ProfesorId == Evaluador.ProfesorId);
                         %>
                        <tr class="<%= ClaseFila %>">
                            <td><%= N %></td>
                            <td class="nombre">
                            <%  if(Grupo.NombreTrabajo!=String.Empty){%>
                                   <span style="color:Black;"><%= Grupo.NombreTrabajo%></span><br/>
                            <% }%>
                            <%  foreach (var Alumno in AlumnosGrupoDetalle){%>
                                   <span class="codigo"><%= Alumno.AlumnoId %></span><%= Alumno.Nombre%><br/>
                            <% }%>        
                            </td> 
                            <td>
                            <%  if(Grupo.NombreTrabajo!=String.Empty){%>
                                   <br/>
                            <% }%>
                                <%  foreach (var Alumno in AlumnosGrupoDetalle){
                                        var AlumnoGrupo = Model.AlumnosGrupos.SingleOrDefault(x=>x.AlumnoId == Alumno.AlumnoId);
                                        var RutaVerRubricaEvaluada = AlumnoGrupo.EvaluacionId != null ? Url.Action("VerEvaluacion","Evaluacion", new { EvaluacionId = AlumnoGrupo.EvaluacionId }) : "";
                                        %>
                                       <%= AlumnoGrupo.EvaluacionId != null ? Html.Link(AlumnoGrupo.Nota, RutaVerRubricaEvaluada, true) : AlumnoGrupo.Nota%>  <br/>
                                <% }%>   
                            </td>
                            <td class="nombre">
                                <%= Html.Encode(Profesor!=null?Profesor.Nombre:"")%>
                            </td>
                            
                            <td>
                            <%= Html.TextBox("EVAL_" + Grupo.GrupoId, Evaluador != null ? Evaluador.ProfesorId : "", new { style="width:50px"})%>
                            <%if(Profesor != null) {%>
                            
                            <% }%>
                            </td>
                            
                            
                        </tr>
                <% }%>
                </tbody>
            </table>
            
            <%= Html.Submit("Guardar", new { @class = "btn btn_save rounded" }) %>
        <%}%>
    <%}%>
    </div>