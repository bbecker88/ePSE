<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.TrabajoViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Individual YUI CSS files -->
    <link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.8.1/build/menu/assets/skins/sam/menu.css" />
    <link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.8.1/build/button/assets/skins/sam/button.css" />
    <link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.8.1/build/fonts/fonts-min.css" />
    <link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.8.1/build/container/assets/skins/sam/container.css" />
    <link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.8.1/build/editor/assets/skins/sam/editor.css" />
    <!-- Individual YUI JS files -->

    <script type="text/javascript" src="http://yui.yahooapis.com/2.8.1/build/yahoo-dom-event/yahoo-dom-event.js"></script>

    <script type="text/javascript" src="http://yui.yahooapis.com/2.8.1/build/element/element-min.js"></script>

    <script type="text/javascript" src="http://yui.yahooapis.com/2.8.1/build/container/container-min.js"></script>

    <script type="text/javascript" src="http://yui.yahooapis.com/2.8.1/build/menu/menu-min.js"></script>

    <script type="text/javascript" src="http://yui.yahooapis.com/2.8.1/build/button/button-min.js"></script>

    <script type="text/javascript" src="http://yui.yahooapis.com/2.8.1/build/editor/editor-min.js"></script>

        
    <script src="<%=Url.Content("~/Scripts/jquery-ui/js/jquery-ui-1.8.6.custom.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Scripts/jquery-ui/js/jquery.ui.datepicker-es.js")%>" type="text/javascript"></script>
    <link href="<%=Url.Content("~/Scripts/jquery-ui/css/blitzer/jquery-ui-1.8.6.custom.css")%>" rel="stylesheet" type="text/css" />


    <script type="text/javascript">
        dateFormatStringDefault: 'mm/dd/yy'
        $(function() {
        $("#FechaInicio").datepicker($.datepicker.regional["es"]);
        $("#FechaFin").datepicker($.datepicker.regional["es"]);
        });


        $('document').ready(function() {

            var Dom = YAHOO.util.Dom,
        Event = YAHOO.util.Event;

            var myConfig = {
                height: '300px',
                width: '555px',
                animate: true,
                dompath: true,
                focusAtStart: true,
                autoHeight: true
            };

            var myEditor = new YAHOO.widget.Editor('trabajo.Instrucciones', myConfig);
            myEditor.render();

            var submitButton = $('#submitButton');
            submitButton.click(function() {
                myEditor.saveHTML();
            });

        });
    </script>

    <h3 style="text-align: center">
        <%= Html.Encode(Model.trabajo.CursoId + " " + Model.trabajo.Curso.Nombre)%></h3>
    <h2 style="text-align: center">
        <%= Html.Encode(Model.trabajo.Nombre)%></h2>
    <h3 style="text-align: center">
        <%= Html.Encode("Editar trabajo") %></h4>
    <% using (Html.BeginBorder())
       {%>
    <% using (Html.BeginForm())
       {%>
       
       <table class="table">
       <tr>
        <th>Inicio</th>
        <th>Fin</th>
        <th>Modo</th>
       </tr>
       <tr>
        <td><%= Html.TextBox("FechaInicio",Model.trabajo.FechaInicio.HasValue?Model.trabajo.FechaInicio.Value.ToShortDateString():"")%></td>
        <td><%= Html.TextBox("FechaFin", Model.trabajo.FechaFin.HasValue ? Model.trabajo.FechaFin.Value.ToShortDateString() : "")%></td>
        <td>
        <%=Html.RadioButton("EsGrupal", true,Model.trabajo.EsGrupal)%><img title="Grupal" src="<%=Url.Content("~/Content/images/imgGrupal.png")%>" />
        <%=Html.RadioButton("EsGrupal", false,!Model.trabajo.EsGrupal)%><img title="Individual" src="<%=Url.Content("~/Content/images/imgIndividual.png")%>" />
        </td>
       </tr>
       
       
       </table>
       
<br />
        <span class="yui-skin-sam">
            <textarea id="trabajo.Instrucciones" name="trabajo.Instrucciones" rows="20" cols="75"> 
            <%= Html.Encode(Model.trabajo.Instrucciones) %>
            </textarea>
        </span>
        <br />
        <p>
            <input type="submit" class="button" id="submitButton" value="Save"/>
           
        </p>
    <% } %>
    <% } %>
    <div>
        <%= Html.ActionLink("Regresar", "Index") %>
    </div>
</asp:Content>
