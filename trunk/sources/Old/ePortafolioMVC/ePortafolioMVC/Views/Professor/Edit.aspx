<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ProfessorSite.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.Models.Entities.BETrabajo>" %>

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

    <script src="<%=Url.Content("~/Scripts/jquery-ui/js/jquery-ui-1.8.6.custom.js")%>"
        type="text/javascript"></script>

    <script src="<%=Url.Content("~/Scripts/jquery-ui/js/jquery.ui.datepicker-es.js")%>"
        type="text/javascript"></script>

    <link href="<%=Url.Content("~/Scripts/jquery-ui/css/blitzer/jquery-ui-1.8.6.custom.css")%>"
        rel="stylesheet" type="text/css" />

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
                width: '538px',
                animate: true,
                dompath: true,
                focusAtStart: true,
                autoHeight: true
            };

            var myEditor = new YAHOO.widget.Editor('Instrucciones', myConfig);
            myEditor.render();

            var submitButton = $('#submitButton');
            submitButton.click(function() {
                myEditor.saveHTML();
           
           
            });

        });
    </script>

    <% int alternate = 0; %>
    <h3 style="text-align: center">
        <%= Html.Encode(Model.Curso.Codigo + " " + Model.Curso.Nombre)%></h3>
    <h2 style="text-align: center">
        <%= Html.Encode(Model.Nombre)%></h2>
    <h3 style="text-align: center">
        <%= Html.Encode("Editar trabajo") %></h4>
        <% using (Html.BeginForm())
           {%>
        <div class="curso">
            <h1>
                DETALLES</h1>
            <table border="0" cellpadding="0" cellspacing="0">
                <thead>
                    <tr>
                        <th width="180">
                            Inicio
                        </th>
                        <th width="180">
                            Fin
                        </th>
                        <th width="180">
                            Modo
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <%= (alternate++) % 2 == 0 ? "<tr class=\"gray\">" : "<tr>"%>
                    <td>
                        <%= Html.TextBox("FechaInicio",Model.FechaInicio.HasValue?Model.FechaInicio.Value.ToShortDateString():"")%>
                    </td>
                    <td>
                        <%= Html.TextBox("FechaFin", Model.FechaFin.HasValue ? Model.FechaFin.Value.ToShortDateString() : "")%>
                    </td>
                    <td>
                        <input id="EsGrupal" name="EsGrupal" type="radio" value="True" <%= Model.EsGrupal?"checked=\"checked\"":""%>><img title="Grupal" src="<%=Url.Content("~/Content/Style/images/group.png")%>" /></input>                        <input id="EsGrupal" name="EsGrupal" type="radio" value="False" <%= !Model.EsGrupal?"checked=\"checked\"":""%> /><img title="Individual" src="<%=Url.Content("~/Content/Style/images/single.png")%>" /></input>
                    </td>
                    </tr>
                </tbody>
            </table>
        </div>
<div class="curso">
            <h1>
                INSTRUCCIONES</h1>
        <span class="yui-skin-sam">
            <textarea id="Instrucciones" name="Instrucciones" rows="20" cols="75"> 
            <%= Html.Encode(Model.Instrucciones) %>
            </textarea>
        </span>
        </div>
        <p>
            <input type="submit" class="Button" id="submitButton" value="Guardar" />
        </p>
        
        <% } %>
        <div>
            <%= Html.ActionLink("Regresar", "Index") %>
        </div>
</asp:Content>
