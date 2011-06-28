<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolio.ViewModel.MostrarResultadoBusquedaEvaluadorGruposViewModel>" %>


    <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
        <thead>
            <tr>
                <th width="40">Codigo</th>
                <th width="300">Nombre</th>
            </tr>
        </thead>
        <tbody class="paging">
        
           <%var odd = true; var N = -1;
             Model.Cursos = Model.Cursos.OrderBy(x => x.NombreCurso).ToList();
             foreach (var Curso in Model.Cursos){
                 odd = !odd; N++;
                 var ClaseFila = odd ? "odd" : "";
                 var Style = N > 9 ? "display:none" : "";
                 %>
                <tr class="<%= ClaseFila %>" id="rowId_<%=N.ToString("D2")%>" style="<%= Style%>">
                    <td><%= Curso.CodigoCurso %></td>
                    <td class="nombre"> <%= Ajax.ActionLink(Curso.NombreCurso, "MostrarTrabajosCursoEvaluadorGrupos", new { CursoId = Curso.CursoId }, new AjaxOptions() { UpdateTargetId = "divTrabajosCurso", OnSuccess = "updatePlaceholder" })%></td>
                </tr>
        <% }%>
        </tbody>
    </table>
    
    <%  var numPaginas = Model.Cursos.Count / 10;
        if(numPaginas>1)
        for(int i=0;i<=numPaginas;i++){ %>
            <a href="javascript:mostrarResultados(<%= i%>);"><%= i+1 %></a>
    <%} %>


    <script>
   
        function mostrarResultados(row) {
            $('[id^="rowId_"]').hide();
            var i = 0;

            for (i = 0; i < 10; i++) {
                $('[id="rowId_' + row + '' + i + '"]').show();
            }
        }
    </script> 
    
    <div id="divTrabajosCurso"></div>