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

    <script src="../../Scripts/jquery-1.4.1.js" type="text/javascript"></script>

    <script type="text/javascript">

        $('document').ready(function() {
            var Dom = YAHOO.util.Dom,
        Event = YAHOO.util.Event;

            var myConfig = {
                height: '300px',
                width: '550px',
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

    <h2>
        Editar</h2>
    <% using (Html.BeginBorder())
       {%>
    <% using (Html.BeginForm())
       {%>
    <%= Html.ValidationSummary(true) %>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.trabajo.Nombre)%>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.trabajo.Nombre)%>
            <%= Html.ValidationMessageFor(model => model.trabajo.Nombre)%>
        </div>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.trabajo.FechaInicio)%>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.trabajo.FechaInicio)%>
            <%= Html.ValidationMessageFor(model => model.trabajo.FechaInicio)%>
        </div>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.trabajo.FechaFin)%>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.trabajo.FechaFin)%>
            <%= Html.ValidationMessageFor(model => model.trabajo.FechaFin)%>
        </div>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.trabajo.EsGrupal)%>
        </div>
        <div class="editor-field">
            <% foreach (var option in Model.EsGrupal)
               { %>
            <%=Html.RadioButton("EsGrupal", option.Value.ToString(),option.Selected)%><img src="<%=Url.Content("~/Content/images/")%><%=option.Text %>" />
            <% } %>
            <%= Html.ValidationMessageFor(model => model.trabajo.EsGrupal)%>
        </div>
        <span class="yui-skin-sam">
            <textarea id="trabajo.Instrucciones" name="trabajo.Instrucciones" rows="20" cols="75"> 
            <%= Html.Encode(Model.trabajo.Instrucciones) %>
            </textarea>
        </span>
        <br />
        <p>
            <input type="submit" class="button" id="submitButton" value="Save"/>
            <br />
        </p>
    <% } %>
    <% } %>
    <div>
        <%= Html.ActionLink("Regresar", "Index") %>
    </div>
</asp:Content>
