<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolio.ViewModel.MostrarDetalleArchivosViewModel>" %>

<div id="MostrarDetalleArchivosContainer">
  <div class="block">
    <%= Html.ShowTempMessage() %>
    
    
    <% if(Model.Archivos.Count == 0) {%>
        <p style="padding-left:3px">El trabajo no tiene archivos registrados</p>
    <%} else { %>
    <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
            <thead>
                <tr>
                    <th width="110">Fecha</th>
                    <th width="240">Nombre</th>
                    <th width="70">Alumno</th>
                    <th width="120">Acciones</th>
                </tr>
            </thead>
            <tbody>
            <%var odd = true;
              foreach(var Archivo in Model.Archivos){
              odd=!odd;
              var ClaseFila = odd ? "odd" : "";%>
                <tr class="<%=ClaseFila %>">
                    <td><%= Archivo.FechaSubida.HasValue?Archivo.FechaSubida.Value.ToString():"N/A" %></td>
                    <td class="nombre"><%= Html.ActionLink(Archivo.Nombre, "DescargarArchivo", new { GrupoId = Model.Grupo.GrupoId, ArchivoId = Archivo.ArchivoId }, new { @class = "documento"  })%></td>     
                    <td><%= Archivo.AlumnoId %></td>
                    <td> 
                        <%= Html.ActionLink("TEXTOREMOVER", "DescargarArchivo", new { GrupoId = Model.Grupo.GrupoId, ArchivoId = Archivo.ArchivoId }, new { @class = "btn_download rounded" }).ToHtmlString().Replace("TEXTOREMOVER", "&nbsp")%> 
                        <% if(Model.Habilitado) { %>
                            <%= Ajax.ActionLink("TEXTOREMOVER", "EliminarArchivoGrupo", new { GrupoId = Model.Grupo.GrupoId, ArchivoId = Archivo.ArchivoId }, new AjaxOptions { OnSuccess = "updatePlaceholder", UpdateTargetId = "MostrarDetalleArchivosContainer" }, new { @class = "btn_cancel rounded" }).ToHtmlString().Replace("TEXTOREMOVER","&nbsp")%>    
                        <% } %>
                    </td>
                </tr>
               <%}%>
           </tbody>
    </table>
    <%}%>
    
    <% if(Model.Habilitado) { %>
    <a id="btnAgregarArchivo" class="btn btn_add rounded">Agregar archivo</a>
    <% }%>
    
    <% if(Model.Archivos.Count>0) { %>
        <%= Html.ActionLink("Descargar todo", "DescargarArchivos", new { GrupoId = Model.Grupo.GrupoId }, new { @class = "btn btn_zip rounded" })%>    
    <% }%>
    <% if(Model.Habilitado) { %>
    <div id="AgregarTrabajo" style="display:none">
        <% using (Html.BeginForm("AgregarArchivoGrupo","Estudiante",FormMethod.Post, new { enctype = "mulitipart/form-data", id="myForm" }))
           {%>
           
           <%= Html.Hidden("GrupoId",Model.Grupo.GrupoId) %>
                    Archivo: <%= Html.File("ArchivoCarga", "Agregar", new { @class = "btn btn_search rounded" })%>
                    <%= Html.Submit("Aceptar", new { @class = "btn btn_check rounded" })%>
        <%} %>
    </div>
        <script src="<%= Url.Content("~/Scripts/jquery.form.js")%>" type="text/javascript"></script>
        <script type="text/javascript">
            var options = {
                iframe: 'true',
                url: '<%= Url.Action("AgregarArchivoGrupo","Estudiante") %>',
                target:'#MostrarDetalleArchivosContainer'
            };
            
            $(document).ready(function() {
                $('#myForm').ajaxForm(options);
            }); 
        </script> 
        
    <script>
        $('document').ready(function() {
        $('#AgregarTrabajo').hide();
        $('#btnAgregarArchivo').click(function() { $('#AgregarTrabajo').toggle('slow') });
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