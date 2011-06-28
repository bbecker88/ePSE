<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolio.ViewModel.MostrarConsolidadoNotasViewModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode("ePortafolio SE | Consolidado de Notas")%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CiclosContent" runat="server">
    <% Html.RenderPartial("MenuCiclosDisponibles",Model.Trabajo.PeriodoId); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <% Html.RenderPartial("CabeceraDetalleTrabajo", Model.Trabajo);%>
    
    <% foreach(var Seccion in Model.SeccionesDictado) {%>
      <div class="block">        
        <h2><%= Seccion.SeccionId %> </h2>
        <%var AlumnosSeccion = Model.AlumnosCursoSeccionesDictado.Where(x => x.SeccionId == Seccion.SeccionId).OrderBy(x => x.NombreAlumno).ToList();    
        if (AlumnosSeccion.Count == 0) {%> 
        <p style="padding-left:3px"><%= Html.Encode("La sección no tiene alumnos registrados") %></p>
        <%}else{%>
   
        <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
            <thead>
                <tr>
                    <th width="40"></th>
                    <th width="290">Nombre</th>
                    <th width="50">Nota</th>
                </tr>
            </thead>
            <tbody>
            <%  
                var odd = true; var N = 0;
                foreach (var Alumno in AlumnosSeccion){
                    odd = !odd; N++;
                    var AlumnoGrupo = Model.AlumnosGruposTrabajo.SingleOrDefault(x=>x.AlumnoId == Alumno.AlumnoId);
                    var Nota = AlumnoGrupo != null ? AlumnoGrupo.Nota : "NE";
                    var ClaseFila = odd ? "odd" : "";
                    %>
                    <tr class="<%= ClaseFila %>">
                        <td ><%= N %></td> 
                        <td class="nombre"><span class="codigo"><%= Alumno.AlumnoId%></span><%= Alumno.NombreAlumno %></td>
                        <td><%= Nota%></td>
                    </tr>
            <%   }
               }%>
            </tbody>
        </table>
       </div>
   <% }%>
    


</asp:Content>
