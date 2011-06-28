<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/StudentSite.Master"
    Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.StudentDetailsViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavigationBar" runat="server">
    <li>
        <%= Html.ActionLink(((BEPeriodo)Session["VistaPeriodo"]).Nombre,"SwitchPeriod",new {PeriodoId = ((BEPeriodo)Session["VistaPeriodo"]).PeriodoId} )%></li>
    <li>
        <%= Html.Encode(Model.Curso.Codigo + " - " + Model.Trabajo.Nombre) %></li>
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
                width: '538px',
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

        $('#divInstrucciones').hide();
        $('#divRubricas').hide();
        $('#toggleInstrucciones').click(function() { $('#divInstrucciones').toggle('slow'); });
        $('#toggleRubricas').click(function() { $('#divRubricas').toggle('slow'); });

        }); 
        
    </script>

    <%Boolean EsActivo = (Model.Trabajo.FechaInicio == null || Model.Trabajo.FechaInicio <= DateTime.Today);
      Boolean EsEvaluado = (Model.Grupo != null && Model.Grupo.Nota != "NE" && Model.Grupo.Nota != "IN");  
    %>
    <!-- FIN DE JAVASCRIPT -->
    <h3 style="text-align: center">
        <%= Html.Encode(Model.Curso.Codigo + " " + Model.Curso.Nombre) %></h3>
    <h2 style="text-align: center">
        <%= Html.Encode(Model.Trabajo.Nombre) %></h2>
    
    <% Html.RenderPartial("ProgramResult", Model.ResultadoPrograma == null ? new BEResultadoPrograma() : Model.ResultadoPrograma); %>
    <% %>
    <%-------- FIN DE PARTE DE GRUPOS --------%>
    <% if (Model.Trabajo.EsGrupal)
       {
           if (Model.Grupo == null)
           {%>
    
    <div class = "curso">
    <h1>GRUPO</h1>
        Todavia no esta en ningun grupo.
    <%= Html.ActionLink("Crear grupo", "GroupCreate", new { TrabajoId = Model.Trabajo.TrabajoId })%><br />
    <br />
    </div>
    <%}
           else
           {
               if (!EsEvaluado)
               {
                   Html.RenderPartial("GroupMemberListWithAction", new ePortafolioMVC.ViewModels.SharedGroupMemberListViewModel()
                           {
                               Alumno = Model.Alumno,
                               AlumnosGrupo = Model.AlumnosGrupo,
                               Grupo = Model.Grupo,
                               Trabajo = Model.Trabajo
                           });
               }
               else
               {
                   Html.RenderPartial("GroupMemberListWithoutAction", new ePortafolioMVC.ViewModels.SharedGroupMemberListViewModel()
                   {
                       Alumno = Model.Alumno,
                       AlumnosGrupo = Model.AlumnosGrupo,
                       Grupo = Model.Grupo,
                       Trabajo = Model.Trabajo
                   });
               }

               if ((EsActivo && !EsEvaluado) && Model.Alumno.AlumnoId == Model.Grupo.Lider.AlumnoId && Model.AlumnosSinGrupo.Count() > 0)
               {
                   //El alumno logueado es lider y existen alumnos sin grupo
                   using (Html.BeginForm("AddStudent", "Student", new { TrabajoId = Model.Trabajo.TrabajoId }, FormMethod.Post))
                   {%>
    <%= Html.ValidationSummary(true) %>
    <div>
        <br />
        <%= Html.DropDownList("AlumnoId", Model.AlumnosSinGrupo)%>
        <%= Html.Hidden("GrupoId", Model.Grupo.GrupoId)%>
        <input type="submit" class="Button" value="Agregar" />
    </div>
    <%} %>
    <p>
        <%--<input type="submit"  class="button"  value="Save" />--%>
    </p>
    <%}
           }
       }%>
    <div class="curso">

            <a id="toggleRubricas"><h1>RUBRICAS</h1></a>
            
                    <div id="divRubricas">
        <%foreach (KeyValuePair<BERubrica, List<BECriterio>> dictionaryItem in Model.CriteriosRubrica)
          {
              Html.RenderPartial("Rubrica", new ePortafolioMVC.ViewModels.SharedRubricaViewModel()
              {
                  Criterios = dictionaryItem.Value,
                  Editable = false,
                  ResultadoRubricas = Model.ResultadosRubricas,
                  Rubrica = dictionaryItem.Key
              });
          } %>
          </div>
    </div>
    <%-------- INICIO DE PARTE DE ARCHIVOS --------%>
    <% if (Model.Grupo != null)
       {
           using (Html.BeginForm("UploadFile", "Student", new { TrabajoId = Model.Trabajo.TrabajoId }, FormMethod.Post, new { enctype = "multipart/form-data" }))
           {%>
    <%= Html.ValidationSummary(true)%>
    <%  if (!EsEvaluado)
          {
              Html.RenderPartial("FileListWithAction", new ePortafolioMVC.ViewModels.SharedFileListViewModel()
                            {
                                ArchivosGrupo = Model.ArchivosGrupo,
                                Trabajo = Model.Trabajo
                            });
          }
          else
          {
              Html.RenderPartial("FileListWithoutAction", new ePortafolioMVC.ViewModels.SharedFileListViewModel()
              {
                  ArchivosGrupo = Model.ArchivosGrupo,
                  Trabajo = Model.Trabajo
              });
          }
      if (EsActivo && !EsEvaluado)
      { %>
    <br />
    <input class="fileButton" name="fileToUpload" type="file" title="Archivo" value="Buscar archivo" />
    <input type="submit" class="Button" value="Subir" />
    <br />
    <%  } %>
    <%}
       }%>
    <%-------- FIN DE PARTE DE ARCHIVOS --------%>
    <%-------- INICIO DE PARTE DE INSTRUCCIONES --------%>
    <% if (Model.Trabajo.Instrucciones != null && Model.Trabajo.Instrucciones.Trim() != "")
       {
    %>
    <% using (Html.BeginForm())
       {%>
    <%= Html.ValidationSummary(true) %>
    <div class="curso">
        <a id="toggleInstrucciones"><h1>INSTRUCCIONES</h1></a>
        <div id="divInstrucciones">
            <span class="yui-skin-sam">
                <textarea id="Instrucciones" name="Instrucciones" rows="20" cols="75"> 
            <%= Html.Encode(Model.Trabajo.Instrucciones) %>
            </textarea>
            </span>
        </div>
    </div>
    <br />
    <%} %>
    <%} %>
    <%-------- FIN DE PARTE DE INSTRUCCIONES --------%>
    <div>
        <%= Html.ActionLink("Regresar", "Index") %>
    </div>
</asp:Content>
