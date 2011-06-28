<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolio.ViewModel.MostrarTrabajosProfesorViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode("ePortafolio SE | Trabajos")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CiclosContent" runat="server">
    <% Html.RenderPartial("MenuCiclosDisponibles", Model.Periodo.PeriodoId); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BreadcrumbsContent" runat="server">
    <ul class="listah">
    	<li><a href="/" class="logo-30"></a></li>
    	<li><a href="#" >Trabajos Obligatorios</a></li>
    	<li><a href="#" ><%= Model.Periodo != null ? Model.Periodo.PeriodoId : "" %></a></li>
    </ul>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="WelcomeContent" runat="server" >
<div class="sombra"></div>
    <ul class="listah">
    	<li class="none">
        	<h1><%= Model.Periodo!=null?Model.Periodo.PeriodoId:""%></h1>
            <span><%= Html.Encode("Ing. de Software y Sistemas de Información") %></span>
    		<span><%= Model.CursosDictado.Count %> Cursos asociados</span>
        </li>
        
        <%foreach (var Curso in Model.CursosDictado)
          {%>
          <li class="rounded"><a href="#<%= Curso.CodigoCurso %>"><%= Html.Encode(Curso.NombreCurso.ToUpper()) %></a>
        	<span><%= Model.Trabajos.Where(x=>x.CursoId == Curso.CursoId).Count()%> Trabajo(s) pendientes</span>
            <span><%= Html.Encode(String.Format("Profesor: {0}", Curso.NombreProfesor))%></span>
            <span><%= Html.Encode(String.Format("{0} - {1}", Curso.CodigoCurso, Curso.SeccionId).ToUpper())%></span>
        </li>
        <%} %>
    </ul>
    <div class="clear"></div>
    <div class="sombra-inv"></div>
</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">


    <%foreach (var Curso in Model.Cursos) {%>
    <% var EsTutor = Model.CursosTutor.Any(x => x.CursoId == Curso.CursoId);
       var EsProfesor = Model.CursosDictado.Any(x => x.CursoId == Curso.CursoId) || Model.CursosExtraordinarios.Any(x => x.CursoId == Curso.CursoId); %>
       
       <div class="block"><a id="<%= Curso.Codigo %>"></a>
       <h2><%= Html.Encode(String.Format("{0} - {1}",Curso.Codigo,Curso.Nombre).ToUpper()) %></h2>
       
       <% var TrabajosCurso = Model.Trabajos.Where(x=>x.CursoId == Curso.CursoId).ToList();
          if (TrabajosCurso.Count == 0){%>
          <p style="padding-left:3px">El curso no tiene trabajos registrados</p>          
          <%} else {%>       
            <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
                <thead>
                	<tr>
                        <th width="36">Modo</th> 
                        <th width="260">Trabajo</th>
                        <th width="70">Inicio</th>
                        <th width="70">Fin</th>
                        <th width="94">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                <% 
                    var odd= true;
                    foreach(var Trabajo in TrabajosCurso){%>
                  <% odd = !odd;
                     var ClaseFila = odd?"odd":"";
                     var TituloGrupal = Trabajo.EsGrupal ? "Grupal" : "Individual";
                     var ClaseGrupal = Trabajo.EsGrupal ? "grupal" : "";
                     var RutaRubrica = Url.Action("VerRubricaTrabajo", "Evaluacion", new { TrabajoId = Trabajo.TrabajoId });
                 %>
                    <tr class="<%= ClaseFila %>">
                        <td><a title="<%= Html.Encode(TituloGrupal) %>"class="mod <%= ClaseGrupal %>"><span >grupal</span></a></td>
                        <td><%= Html.ActionLink(Trabajo.Nombre, "MostrarDetalleTrabajo", new { TrabajoId = Trabajo.TrabajoId }, new { @class="nombre"})%></td>
                        <td><%= Trabajo.FechaInicio.HasValue?Trabajo.FechaInicio.Value.ToShortDateString():"-" %></td>
                        <td><%= Trabajo.FechaFin.HasValue ? Trabajo.FechaFin.Value.ToShortDateString() : "-"%></td>
                        <td>
                            <%= Html.Link("Rúbrica",RutaRubrica,true) %> |                            
                            <%= EsTutor ? Html.ActionLink("Editar", "EditarTrabajo", new { TrabajoId = Trabajo.TrabajoId }) : MvcHtmlString.Empty %> 
                            <%= EsTutor && EsProfesor ? " | ":""%>    
                            <%= EsProfesor ? Html.ActionLink("Notas", "MostrarConsolidadoNotas", new { TrabajoId = Trabajo.TrabajoId }) : MvcHtmlString.Empty %></td>
                    </tr>
                   <%}%>
               	</tbody>
            	</table>
           <%}%>
            </div><!-- FIN CURSO -->
            <br />
        <%}%>
 </asp:Content>
