<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolio.ViewModel.MostrarDetalleGrupoViewModel>" %>

<div id="MostrarDetalleGrupoContainer">

    <div class="block">

    <%= Html.ShowTempMessage() %>

    <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
            <thead>
                <tr>
                    <th width="24">Rol</th>
                    <th width="386">Nombre</th>
                    <th width="120">Acciones</th>
                </tr>
            </thead>
            <tbody>
            <%
              var EsLider = Model.Grupo.LiderId == Model.Alumno.AlumnoId;
              var odd = true;
              foreach(var Alumno in Model.AlumnosGrupo){%>
              <% 
                odd = !odd;
                var ClaseFila = odd ? "odd" : "";
                var TituloRol = Model.Grupo == null ? "Sin Grupo" : Model.Grupo.LiderId == Alumno.AlumnoId ? "Líder" : "Integrante";
                var ClaseRol = Model.Grupo == null ? "" : Model.Grupo.LiderId == Alumno.AlumnoId ? "lider" : "normal";
                var EsActual = Alumno.AlumnoId == Model.Alumno.AlumnoId;
             %>
                <tr class="<%=ClaseFila %>">
                    <td><a title="<%= Html.Encode(TituloRol) %>" class="rol <%= ClaseRol %>"><span >líder</span></a></td>
                    <td class="nombre"><span class="codigo"><%= Alumno.AlumnoId %></span><%= Alumno.Nombre %></td>                
                    <% if (EsActual && EsLider && Model.Habilitado) {%>
                    <td><%= Html.ActionLink("Eliminar " + (Model.EsIndependiente?"Trabajo":"Grupo"), "EliminarGrupo", new { GrupoId = Model.Grupo.GrupoId })%></td>
                    <%} else if (!EsActual  && EsLider && Model.Habilitado){ %>
                    <td><%= Ajax.ActionLink("Remover", "EliminarAlumnoGrupo", new { GrupoId = Model.Grupo.GrupoId, AlumnoId = Alumno.AlumnoId }, new AjaxOptions() { UpdateTargetId = "MostrarDetalleGrupoContainer", OnSuccess= "updatePlaceholder" })%> |
                        <%= Ajax.ActionLink("Lider", "CambiarLiderGrupo", new { GrupoId = Model.Grupo.GrupoId, AlumnoId = Alumno.AlumnoId }, new AjaxOptions() { UpdateTargetId = "MostrarDetalleGrupoContainer", OnSuccess = "updatePlaceholder" })%></td>
                    <%} else if (EsActual  && !EsLider && Model.Habilitado){ %>
                    <td><%= Html.ActionLink("Retirarse", "RetirarseGrupo", new { GrupoId = Model.Grupo.GrupoId})%></td>
                    <%} else{ %>
                    <td>-</td>
                    <%}%>
                </tr>
               <%}%>
           </tbody>
    </table>

    <%if (Model.Habilitado && EsLider && Model.Grupo.ExtraTrabajo.EsGrupal){ %>
    <a id="btnAgregarAlumno" class="btn btn_add rounded">Agregar integrante</a>    
    
    <div id="ListaAlumnosSinGrupo" style="display:none">
        <% using (Ajax.BeginForm("AgregarAlumnosGrupo", new { GrupoId = Model.Grupo.GrupoId }, new AjaxOptions() { UpdateTargetId = "MostrarDetalleGrupoContainer", OnSuccess = "updatePlaceholder" }))
           {%>
        <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
            <% if(!Model.EsIndependiente){%>
                <% for (int i = 0; i < Model.AlumnosSinGrupo.Count;i++ )
                   {
                    var Alumno = Model.AlumnosSinGrupo[i];%>
                    <%= i%2==0?"<tr>":"" %>
                    <td class="nombre"><%= Html.CheckBox("A" + Alumno.AlumnoId)%> <label for="<%= "A" + Alumno.AlumnoId %>"><span class="codigo"><%= Alumno.AlumnoId%></span><%= Alumno.Nombre%></label></td>
                    <%= i % 2 == 1 ? "</tr>" : ""%>
                <%} if (Model.AlumnosSinGrupo.Count % 2 == 1)
                   { %>
                    <td></td> </tr>
                <%} %>
            <%}%>
              <tr >
                <td>
                    <span class="fleft" style="text-align:left; padding-left:25px;">
                        <%= Html.Label("Códigos")%>
                        <%= Html.TextBox("IndependieteAlumnos", "", new { id = "txtIndependieteAlumnos"})%>
                    </span>
                </td>
                <td style="text-align:left; padding-left:25px;">
                    <%= Html.Submit("Agregar",new {@class = "btn btn_add rounded"}) %>
                </td>
              </tr>
        </table>
        <%} %>
    </div>

    <script>
        $('document').ready(function() {
            $('#ListaAlumnosSinGrupo').hide();
            $('#btnAgregarAlumno').click(function() { $('#ListaAlumnosSinGrupo').toggle('slow') });
        });
    </script>
    <%} %>

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