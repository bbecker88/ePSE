<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolio.ViewModel.MostrarTrabajosEstudianteViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode("ePortafolio SE | Trabajos Obligatorios")%>
</asp:Content>


<asp:Content ID="Content6" ContentPlaceHolderID="CiclosContent" runat="server">
    <% Html.RenderPartial("MenuCiclosDisponibles",Model.Periodo.PeriodoId); %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BreadcrumbsContent" runat="server">
    <ul class="listah">
    	<li><a href="/" class="logo-30"></a></li>
    	<li><a href="/" ><%= "Trabajos Obligatorios"%></a></li>
        <li><a href="/" ><%= Model.Periodo!=null?Model.Periodo.PeriodoId:""%></a></li>
    </ul>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="WelcomeContent" runat="server" >
<div class="sombra"></div>
    <ul class="listah">
    	<li class="none">
        	<h1><%= Model.Periodo!=null?Model.Periodo.PeriodoId:""%></h1>
            <span><%= Model.Alumno.NombreCarrera %></span>
    		<span><%= Model.CursosAlumno.Count %> Cursos matriculados</span>
        </li>
        
        <%foreach (var Curso in Model.CursosAlumno)
          {%>
          <li class="rounded"><a href="#<%= Curso.CodigoCurso.Trim() %>"><%= Html.Encode(Curso.NombreCurso.ToUpper()) %></a>
        	<span><%= Model.Trabajos.Where(x=>x.CursoId == Curso.CursoId).Count()%> Trabajo(s) registrados</span>
            <span><%= Html.Encode(String.Format("Curso: {0}", Curso.CodigoCurso.ToUpper()))%></span>
            <span><%= Html.Encode(String.Format("Sección: {0}", Curso.SeccionId.ToUpper()))%></span>
        </li>
        <%} %>
    </ul>
    <div class="clear"></div>
    <div class="sombra-inv"></div>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%foreach (var Curso in Model.CursosAlumno) {%>
       <div class="block"><a id="<%= Curso.CodigoCurso.Trim() %>"></a>
            	<h2><%= Html.Encode(String.Format("{0} - {1}",Curso.CodigoCurso,Curso.NombreCurso).ToUpper()) %></h2>
            	<span class="desc"><%= Html.Encode(String.Format("Sección: {0}", Curso.SeccionId))%></span>
       
       <% var TrabajosCurso = Model.Trabajos.Where(x=>x.CursoId == Curso.CursoId).ToList();
          if (TrabajosCurso.Count == 0){%>
          <p style="padding-left:3px">El curso no tiene trabajos registrados</p>          
          <%} else {%>
                <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
                <thead>
                	<tr>
                        <th width="24">Rol</th>
                        <th width="220">Trabajo</th>
                        <th width="70">Inicio</th>
                        <th width="70">Fin</th>
                        <th width="70">Nota</th>
                        <th width="36">Modo</th> 
                        <th width="40">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                <% 
                    var odd= true;
                    foreach(var Trabajo in TrabajosCurso){%>
                  <% odd = !odd;
                     var GrupoTrabajo = Model.Grupos.SingleOrDefault(x => x.TrabajoId == Trabajo.TrabajoId); 
                     var EstaEntregado = Model.GruposConArchivosEntregados.Any(x => x.TrabajoId == Trabajo.TrabajoId);
                     var ClaseFila = odd?"odd":"";
                     var AlumnoGrupo = GrupoTrabajo == null ? null : Model.GruposAlumno.SingleOrDefault(x => x.GrupoId == GrupoTrabajo.GrupoId);
                     var Nota = AlumnoGrupo == null ? "-" : AlumnoGrupo.Nota;
                     var TituloRol = GrupoTrabajo==null?"Sin Grupo":GrupoTrabajo.LiderId == Model.Alumno.AlumnoId?"Líder":"Integrante";
                     var ClaseRol = GrupoTrabajo == null ? "" : GrupoTrabajo.LiderId == Model.Alumno.AlumnoId ? "lider" : "normal";
                     var TituloGrupal = Trabajo.EsGrupal ? "Grupal" : "Individual";
                     var ClaseGrupal = Trabajo.EsGrupal ? "grupal" : "";
                     var RutaVerRubricaEvaluada = GrupoTrabajo != null && AlumnoGrupo.EvaluacionId != null ? Url.Action("VerEvaluacion", "Evaluacion", new { EvaluacionId = AlumnoGrupo.EvaluacionId }) : Url.Action("VerRubricaTrabajo", "Evaluacion", new { TrabajoId = Trabajo.TrabajoId });
                 %>
                    <tr class="<%= ClaseFila %>">
                        <td><a title="<%= Html.Encode(TituloRol) %>" class="rol <%= ClaseRol %>"><span >líder</span></a></td>
                        <td><%= Html.ActionLink(Trabajo.Nombre, "MostrarDetalleTrabajo", new { TrabajoId = Trabajo.TrabajoId }, new { @class="nombre"})%></td>
                        <td><%= Trabajo.FechaInicio.HasValue?Trabajo.FechaInicio.Value.ToShortDateString():"-" %></td>
                        <td><%= Trabajo.FechaFin.HasValue ? Trabajo.FechaFin.Value.ToShortDateString() : "-"%></td>
                        <td><%= Nota %></td>
                        <td><a title="<%= Html.Encode(TituloGrupal) %>"class="mod <%= ClaseGrupal %>"><span >grupal</span></a></td>
                        <td><%= Html.Link("Rúbrica", RutaVerRubricaEvaluada, true)%> </td>
                    </tr>
                   <%}%>
               	</tbody>
            	</table>
           <%}%>
            </div><!-- FIN CURSO -->
            <br />
        <%}%>
 
 </asp:Content>
