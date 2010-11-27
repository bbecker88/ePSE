<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.StudentDetailsViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- INICIO DE ARCHIVOS NECESARIOS DEL COMPONENTE YUI: RICH TEXT EDITOR -->
    <link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.8.1/build/menu/assets/skins/sam/menu.css" />
    <link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.8.1/build/button/assets/skins/sam/button.css" />
    <link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.8.1/build/fonts/fonts-min.css" />
    <link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.8.1/build/container/assets/skins/sam/container.css" />
    <link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.8.1/build/editor/assets/skins/sam/editor.css" />

    <script type="text/javascript" src="http://yui.yahooapis.com/2.8.1/build/yahoo-dom-event/yahoo-dom-event.js"></script>

    <script type="text/javascript" src="http://yui.yahooapis.com/2.8.1/build/element/element-min.js"></script>

    <script type="text/javascript" src="http://yui.yahooapis.com/2.8.1/build/container/container-min.js"></script>

    <script type="text/javascript" src="http://yui.yahooapis.com/2.8.1/build/menu/menu-min.js"></script>

    <script type="text/javascript" src="http://yui.yahooapis.com/2.8.1/build/button/button-min.js"></script>

    <script type="text/javascript" src="http://yui.yahooapis.com/2.8.1/build/editor/editor-min.js"></script>

    <!-- FIN DE ARCHIVOS NECESARIOS DEL COMPONENTE YUI: RICH TEXT EDITOR -->
    <!-- INICIO DE JAVASCRIPT -->

    <script type="text/javascript">

        $('document').ready(function() {
            var Dom = YAHOO.util.Dom,
        Event = YAHOO.util.Event;

            var myConfig = {
                height: '300px',
                width: '555px',
                animate: true,
                toolbar: false,
                dompath: true,
                focusAtStart: false
            };

            var myEditor = new YAHOO.widget.Editor('Instrucciones', myConfig);
            myEditor.render();
        });
    </script>

    <script type="text/javascript">

        $('document').ready(function() {
            $('#AddStudent').hide();
            $('#AddStudentLink').click(function() { $('#AddStudentLink').hide(); $('#AddStudent').show('slow'); });

            $('#divInstrucciones').hide();
            $('#toggleInstrucciones').click(function() { $('#divInstrucciones').toggle('slow'); });

        });
        
    </script>
    <%Boolean EsVigente = (Model.Trabajo.FechaInicio == null || Model.Trabajo.FechaInicio <= DateTime.Today) && (Model.Trabajo.FechaFin == null || Model.Trabajo.FechaFin >= DateTime.Today);
      Boolean EsFinalizado = (Model.Trabajo.FechaFin != null && Model.Trabajo.FechaFin < DateTime.Today);  
         %>
    <!-- FIN DE JAVASCRIPT -->
    <h3 style="text-align: center">
        <%= Html.Encode(Model.Curso.Codigo + " " + Model.Curso.Nombre) %></h3>
    <h2 style="text-align: center">
        <%= Html.Encode(Model.Trabajo.Nombre) %></h2>
    <h3 style="text-align: center">
        <%= Html.Encode("Detalles") %></h3>
        
<%= Html.ResultadoProgramaTable(Model.ResultadoPrograma) %>
        
        <%-------- INICIO DE PARTE DE GRUPOS --------%>
        <% if (Model.Trabajo.EsGrupal)
           {
               using (Html.BeginBorder())
               {%>
        <%if (Model.Grupo == null)
          {%>
        <h3>
            Todavia no esta en ningun grupo.</h3>
        <%= Html.ActionLink("Crear grupo", "GroupCreate", new { TrabajoId = Model.Trabajo.TrabajoId })%><br />
        <br />
        <%}
          else
          {%>
        <h3>
            Integrantes:</h3>
        <div id="integrantes">
            <table class="table">
                <tr>
                    <th>
                        Codigo
                    </th>
                    <th>
                        Nombre
                    </th>
                    <th>
                        Rol
                    </th>
                    <th>
                        Acciones
                    </th>
                </tr>
                <% foreach (BEAlumno Alumno in Model.AlumnosGrupo)
                   { %>
                <tr id="StudentRow<%= Alumno.AlumnoId%>">
                    <td>
                        <%= Html.Encode(Alumno.AlumnoId)%>
                    </td>
                    <td style="width: 300px; text-align: left;">
                        <%= Html.Encode(Alumno.Nombre)%>
                    </td>
                    <td>
                        <% if (Alumno.AlumnoId == Model.Grupo.Lider.AlumnoId)
                           {%>
                        <img src="<%=Url.Content("~/Content/images/imgGrupoLider.png")%>" title="Lider de grupo" />
                        <%}
                           else
                           {%>
                        <img src="<%=Url.Content("~/Content/images/imgGrupoMiembro.png")%>" title="Miembo de grupo" />
                        <%}%>
                    </td>
                    <td>
                        <% if (Model.Grupo.Lider.AlumnoId == Model.Alumno.AlumnoId && Alumno.AlumnoId != Model.Alumno.AlumnoId)
                           {
                               //El alumno logueado es lider y el alumno analizado no es el logueado%>
                        <%= Html.ActionLink("Eliminar", "DeleteStudent", new { GrupoId = Model.Grupo.GrupoId, AlumnoId = Alumno.AlumnoId , TrabajoId=Model.Trabajo.TrabajoId})%>
                        <%= Html.ActionLink("Lider", "MakeLeader", new { GrupoId = Model.Grupo.GrupoId, AlumnoId = Alumno.AlumnoId, TrabajoId = Model.Trabajo.TrabajoId })%>
                        <%}
                           else if (Model.Grupo.Lider.AlumnoId != Model.Alumno.AlumnoId && Alumno.AlumnoId == Model.Alumno.AlumnoId)
                           {
                               //El alumno logueado no es lider y el alumno analizado es el logueado%>
                        <%= Html.ActionLink("Retirar", "WithdrawStudent", new { GrupoId = Model.Grupo.GrupoId, AlumnoId = Alumno.AlumnoId })%>
                        <%}
                           else if (Model.Grupo.Lider.AlumnoId == Model.Alumno.AlumnoId && Alumno.AlumnoId == Model.Alumno.AlumnoId)
                           {
                               //El alumno logueado es lider y el alumno analizado es el logueado%>
                        <%= Html.ActionLink("Elimin. Grupo", "DeleteGroup", new {GrupoId = Model.Grupo.GrupoId})%>
                        <%}%>
                    </td>
                </tr>
                <%  } %>
            </table>
        </div>
        <% if ((EsVigente || EsFinalizado) && Model.Alumno.AlumnoId == Model.Grupo.Lider.AlumnoId && Model.AlumnosSinGrupo.Count() > 0)
           {
               //El alumno logueado es lider y existen alumnos sin grupo%>
        <% using (Html.BeginForm("AddStudent", "Student", new { TrabajoId = Model.Trabajo.TrabajoId }, FormMethod.Post))
           {%>
        <%= Html.ValidationSummary(true) %>
        <div>
            <br />
            <a id="AddStudentLink" href="#">Agregar</a>
            <div id="AddStudent">
                <%= Html.DropDownList("AlumnoId", Model.AlumnosSinGrupo)%>
                <%= Html.Hidden("GrupoId", Model.Grupo.GrupoId)%>
                <br /><br /><input type="submit" class = "button" value="Agregar" />
            </div>
        </div>
        <%} %>
        <p>
            <%--<input type="submit"  class="button"  value="Save" />--%>
        </p>
        <%}%>
        <% } %>
        <% } %>
        <% } %>
        <%-------- FIN DE PARTE DE GRUPOS --------%>
        <%-------- INICIO DE PARTE DE ARCHIVOS --------%>
        <% if (Model.Grupo != null)
           {
               using (Html.BeginBorder())
               {%>
        <% using (Html.BeginForm("UploadFile", "Student", new { TrabajoId = Model.Trabajo.TrabajoId }, FormMethod.Post, new { enctype = "multipart/form-data" }))
           {%>
        <%= Html.ValidationSummary(true)%>
        <%if (Model.ArchivosGrupo.Count == 0)
          {%>
        <h3>
            Todavia no se han subido archivos.</h3>
        <%}
          else
          {%>
        <div id="archivosSubidos">
            <h3>
                Archivos:</h3>
            <table class="table">
                <tr>
                    <th>
                        Nombre
                    </th>
                    <th>
                        Fecha
                    </th>
                    <th>
                        Subido por
                    </th>
                    <th>
                        Opciones
                    </th>
                </tr>
                <% foreach (BEArchivo Archivo in Model.ArchivosGrupo)
                   { %>
                <tr id="Tr1">
                    <td style="width: 300px; text-align: left;">
                        <%= Html.Encode(Archivo.Nombre)%>
                    </td>
                    <td>
                        <%= Html.Encode(Archivo.FechaSubido.Value.ToShortDateString())%>
                    </td>
                    <td>
                        <span title="<%= Html.Encode(Archivo.Alumno.Nombre)%>">
                            <%= Html.Encode(Archivo.Alumno.AlumnoId)%>
                        </span>
                    </td>
                    <td>
                        <a href="<%= Html.Encode(Url.Content("~"+Archivo.Ruta))%>">
                            <img src="<%= Url.Content("~/Content/images/imgDownload.png")%>" title="Descargar" /></a>
                        <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgDelete.png"), "DeleteFile", "Eliminar", new { TrabajoId = Model.Trabajo.TrabajoId, ArchivoId = Archivo.ArchivoId })%>
                    </td>
                </tr>
                <%  } %>
            </table>
        </div>
        <%  } %>
        <%if (EsVigente || EsFinalizado)
          { %>
        <br />

        <input class="fileButton" name="fileToUpload" type="file" title="Archivo" value="Buscar archivo" /><br />
        <br />
        <input type="submit" class="button" value="Subir" />
        <br />
        <%  } %>
        <br />
        <%} %>
        <%}
       }%>
        <%-------- FIN DE PARTE DE ARCHIVOS --------%>
        <%-------- INICIO DE PARTE DE INSTRUCCIONES --------%>
        <% if (Model.Trabajo.Instrucciones != null && Model.Trabajo.Instrucciones.Trim() != "")
           {
               using (Html.BeginBorder())
               {%>
        <% using (Html.BeginForm())
           {%>
        <%= Html.ValidationSummary(true) %>
        <a id="toggleInstrucciones">Ver instrucciones</a>
        <br />
        <div id="divInstrucciones">
            <br />
            <span class="yui-skin-sam">
                <textarea id="Instrucciones" name="Instrucciones" rows="20" cols="75"> 
            <%=Model.Trabajo.Instrucciones %>    
            </textarea>
            </span>
        </div>
        <br />
        <%} %><%} %>
        <%} %>
        <%-------- FIN DE PARTE DE INSTRUCCIONES --------%>
        <div>
            <%= Html.ActionLink("Regresar", "Index") %>
        </div>
</asp:Content>
