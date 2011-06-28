<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolio.ViewModel.MostrarDetalleTrabajoProfesorViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode("ePortafolio SE | Detalles de Trabajo")%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CiclosContent" runat="server">
    <% Html.RenderPartial("MenuCiclosDisponibles", Model.Trabajo.PeriodoId); %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="WelcomeContent" runat="server" >


<script src="<%= Url.Content("~/Scripts/highcharts/highcharts.js")%>" type="text/javascript"></script>
    
<div class="sombra"></div>
    <ul class="listah">
    	<li class="none">
        	<h1><%= Model.Curso.Codigo + " - " + Model.Trabajo.Codigo%></h1>
            <span><%= Html.Encode(Model.Curso.Nombre) %></span>
            <span><%= Html.Encode(Model.Trabajo.Nombre) %></span>
    		<span><%= Model.SeccionesDictado.Count %> secciones</span>
        </li>
        
        <%foreach (var Seccion in Model.SeccionesDictado)
          {%>
          
        <% var NumEntregados = Model.AlumnosGrupos.Where(x=>Model.GruposEntregados.Any(ge=>ge.GrupoId==x.GrupoId && ge.SeccionId == Seccion.SeccionId)).Count();%>
        <% var NumNoEntregados = Model.AlumnosCursoSeccionesDictado.Count(x => x.SeccionId == Seccion.SeccionId && !Model.AlumnosGrupos.Select(ag => ag.AlumnoId).Contains(x.AlumnoId));%>
        
        <% var NumEvaluados = Model.AlumnosGrupos.Where(x => Model.GruposEntregados.Any(ge => ge.GrupoId == x.GrupoId && ge.SeccionId == Seccion.SeccionId) && (x.Nota != null && x.Nota != String.Empty && x.Nota != "NE")).Count();%>  
        <% if(NumEntregados==0 && NumNoEntregados==0) continue;%>  
          <li class="rounded"><a href="#<%= Seccion.SeccionId %>"><%= Html.Encode(Seccion.SeccionId)%></a>
        	<span><%= NumEntregados %> alumno(s) entregaron</span>
        	<span><%= NumNoEntregados%> alumno(s) no entregaron</span>
            <span><%= Html.Encode(String.Format("{0} alumno(s) por evaluar", NumEntregados-NumEvaluados))%></span>
            <span>
            
            
            <div id="container<%= Seccion.SeccionId%>" style="height:40px; width:240px"> </div>
            
            <script>
                var chart;
                $(document).ready(function() {
                    chart = new Highcharts.Chart({

                        colors: [
                            '#AA4643',
                            '#4572A7',
                            '#EEEEEE',
                            '#89A54E'],
                        chart: {
                        renderTo: 'container<%= Seccion.SeccionId%>',
                            defaultSeriesType: 'bar',
                            spacingTop: 0,
                            spacingBottom: 0,
                            spacingLeft: 0,
                            spacingRight: 0,
                            marginTop: 0,
                            marginBottom: 0,
                            marginLeft: -1,
                            marginRight: 0,
                            backgroundColor: '#d5d5d5'
                        },
                        title: {
                            text: ''
                        },
                        xAxis: {
                            categories: [' ', ' '],
                            labels: { enabled: false },
                            tickInterval: 0,
                            gridLineWidth: 0
                        },
                        yAxis: {
                            min: 0,
                            title: {
                                text: ''
                            },
                            labels: { enabled: false },
                            gridLineWidth: 0
                        },
                        legend: {
                            backgroundColor: '#FFFFFF',
                            reversed: true,
                            enabled: false
                        },
                        tooltip: {
                            formatter: function() {
                                return this.series.name + ': ' + this.y + ' (' + Math.round(this.percentage) + '%)';
                            }
                        },
                        credits: {
                            enabled: false
                        },
                        plotOptions: {
                            series: {
                                stacking: 'percent',
                                borderWidth: 0,
                                borderColor: 'black',
                                shadow: false
                            }
                        },
                        series: [{
                            name: 'No entregados',
                            data: [<%= NumNoEntregados%>, 0]
                        }, {
                            name: 'Entregados',
                            data: [<%= NumEntregados%>, 0]
                        }, {
                            name: 'No evaluados',
                            data: [0, <%= NumEntregados-NumEvaluados%>]
                        }, {
                            name: 'Evaluados',
                            data: [0, <%= NumEvaluados%>]
                                }]
                        });
                    });
                </script>            
            </span>
        </li>
        <%} %>
    </ul>
    <div class="clear"></div>
    <div class="sombra-inv"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("CabeceraDetalleTrabajo", Model.Trabajo);%>
    
    <div class="block">
    <%= Html.Link("Ver Rúbrica", Url.Action("VerRubricaTrabajo", "Evaluacion", new { TrabajoId = Model.Trabajo.TrabajoId }),true, new {@class="btn btn_chart rounded"})%> 
    
    
    <%  foreach(var Seccion in Model.SeccionesDictado) {%>
        
        
        <a id="<%= Seccion.SeccionId %>"></a><h2><%= Seccion.SeccionId %> </h2>
        
        <% var GruposEntregadosSeccion =  Model.GruposEntregados.Where(x => x.SeccionId == Seccion.SeccionId).ToList();%>
        <% var AlumnosNoEntregados = Model.AlumnosCursoSeccionesDictado.Where(x => x.SeccionId == Seccion.SeccionId && !Model.AlumnosGrupos.Select(ag => ag.AlumnoId).Contains(x.AlumnoId)).OrderBy(x=>x.NombreAlumno).ToList();%>
        
        
        <% if (GruposEntregadosSeccion.Count==0 && AlumnosNoEntregados.Count==0){ %>
                <p style="padding-left:3px"><%= Html.Encode("La sección no tiene alumnos registrados") %></p>
        <% } %>
        
        
        <% if (GruposEntregadosSeccion.Count>0){ %>
        <span class="desc"><%= Html.Encode(String.Format("Entregados: {0} {1}(s)", GruposEntregadosSeccion.Count,Model.Trabajo.EsGrupal?"grupos":"alumnos"))%> </span>
        
        <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
            <thead>
                <tr>
                    <th width="40"></th>
                    <th>Nombre</th>
                    <th>Nota Base</th>
                    <th>Nota Final</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
            
               <%var odd= true; var N = 0;
                 foreach (var GrupoEntregado in GruposEntregadosSeccion){
                     odd = !odd; N++;
                     var ClaseFila = odd ? "odd" : "";
                     var RutaEvaluarRubrica = Model.Trabajo.EsGrupal ? Url.Action("EditarEvaluacionGrupo", new { GrupoId = GrupoEntregado.GrupoId }) : Url.Action("EvaluarGrupo", "Evaluacion", new { GrupoId = GrupoEntregado.GrupoId });
                     var AlumnosGrupoId = Model.AlumnosGrupos.Where(x => x.GrupoId == GrupoEntregado.GrupoId).ToList().Select(x => x.AlumnoId.ToLower().Trim()).ToList();
                     var AlumnosGrupoDetalle = Model.AlumnosCursoSeccionesDictado.Where(x => AlumnosGrupoId.Contains(x.AlumnoId.ToLower().Trim())).OrderBy(x => x.NombreAlumno).ToList();
                     %>
                    <tr class="<%= ClaseFila %>">
                        <td><%= N %></td>
                        <td class="nombre">
                        <%  if (GrupoEntregado.NombreTrabajo != String.Empty){%>
                                   <span style="color:Black;"><%= GrupoEntregado.NombreTrabajo%></span><br/>
                            <% }%>
                        <%  foreach (var Alumno in AlumnosGrupoDetalle){%>
                               <span class="codigo"><%= Alumno.AlumnoId%></span><%= Alumno.NombreAlumno%><br/>
                        <% }%>        
                        </td> 
                        <td><%= GrupoEntregado.Nota %></td>
                        <td>
                        <%  if(GrupoEntregado.NombreTrabajo!=String.Empty){%>
                                   <br/>
                        <% }%>
                        <%  foreach (var Alumno in AlumnosGrupoDetalle){
                                var AlumnoGrupo = Model.AlumnosGrupos.SingleOrDefault(x => x.AlumnoId.ToLower().Trim() == Alumno.AlumnoId.ToLower().Trim());
                                var RutaVerRubricaEvaluada = AlumnoGrupo!=null && AlumnoGrupo.EvaluacionId != null ? Url.Action("VerEvaluacion", "Evaluacion", new { EvaluacionId = AlumnoGrupo.EvaluacionId }) : "";
                                %>
                               <%= AlumnoGrupo.EvaluacionId != null ? Html.Link(AlumnoGrupo.Nota, RutaVerRubricaEvaluada, true) : AlumnoGrupo.Nota%>  <br/>
                        <% }%>   
                        </td>
                        <td><%= Html.ActionLink("Descargar","DescargarArchivos","Estudiante",new {GrupoId = GrupoEntregado.GrupoId},null) %>
                            <% if(!Model.GruposEntregadosExoneradosId.Contains(GrupoEntregado.GrupoId)) {%>
                                <%=  " | " + Html.Link("Evaular", RutaEvaluarRubrica,true)%>  
                            <%} %>
                            </td>
                    </tr>
            <% }%>
            </tbody>
        </table>
        <%} %>
        
        
        
        <%if (AlumnosNoEntregados.Count>0){ %>
        <span class="desc"><%= Html.Encode(String.Format("No entregados: {0} persona(s)", AlumnosNoEntregados.Count))%> </span>
        <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
            <thead>
                <tr>
                    <th width="40"></th>
                    <th>Nombre</th>
                </tr>
            </thead>
            <tbody>
            <% 
           var odd = true; var N = 0;
           foreach(var Alumno in AlumnosNoEntregados) {
               odd = !odd; N++;
               var ClaseFila = odd ? "odd" : "";%>
                <tr class="<%= ClaseFila %>">
                    <td><%= N %></td>
                    <td class="nombre">
                        <span class="codigo"><%= Alumno.AlumnoId%></span><%= Alumno.NombreAlumno %></br>
                    </td> 
                </tr>
                    <% }%>
            </tbody>
        </table>
      <% }%>  
    <% }%>  
    </div>
</asp:Content>

