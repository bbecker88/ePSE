<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/EmptySite.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolio.ViewModel.EditarEvaluacionGrupoViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Evaluar Grupo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    Evaluacion de grupo: <br />
    Nota actual: 
    
    <%= Model.Grupo.EvaluacionId != null ? Html.ActionLink(Model.Grupo.Nota, "VerEvaluacion", "Evaluacion", new { EvaluacionId = Model.Grupo.EvaluacionId, GrupoRetornoId = Model.Grupo.GrupoId }, null).ToHtmlString() : Model.Grupo.Nota %>
    <%= Html.ActionLink("Evaluar", "EvaluarGrupo", "Evaluacion", new { GrupoId = Model.Grupo.GrupoId, GrupoRetornoId = Model.Grupo.GrupoId }, null)%>

   
    <br />

        <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
        <thead>
        	<tr>
                <th width="420">Nombre</th>
                <th width="50">Nota</th>
                <th width="60">Acciones</th>
            </tr>
        </thead>
        <tbody>
        <% 
            var odd= true;
            foreach(var Alumno in Model.Alumnos){%>
          <% odd = !odd;
             var ClaseFila = odd?"odd":"";
             var AlumnoGrupo = Model.AlumnosGrupo.SingleOrDefault(x => x.AlumnoId == Alumno.AlumnoId); 
         %>
            <tr class="<%= ClaseFila %>">
                <td class="nombre"><span class="codigo"><%= Alumno.AlumnoId%></span><%= Alumno.Nombre%></td>
                <td><%= AlumnoGrupo.EvaluacionId != null ? Html.ActionLink(AlumnoGrupo.Nota, "VerEvaluacion", "Evaluacion", new { EvaluacionId = AlumnoGrupo.EvaluacionId, GrupoRetornoId = Model.Grupo.GrupoId }, null).ToHtmlString() : AlumnoGrupo.Nota%>  </td>
                <td><%= Html.ActionLink("Evaluar", "EvaluarAlumnoGrupo", "Evaluacion", new { AlumnoId = AlumnoGrupo.AlumnoId, GrupoId = Model.Grupo.GrupoId, GrupoRetornoId = Model.Grupo.GrupoId }, null)%></td>
            </tr>
           <%}%>
       	</tbody>
    	</table>
</asp:Content>