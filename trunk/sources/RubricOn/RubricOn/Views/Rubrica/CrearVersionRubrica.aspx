<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RubricOn.ViewModel.CrearVersionRubricaViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<% if(!Model.EsNuevo){%>
        <%= Html.Encode("Crear nueva versión")%>
    <%} %>
    <% if(Model.EsNuevo){%>
        <%= Html.Encode("Crear nueva rúbrica")%>
    <%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% if(!Model.EsNuevo){%>
        <h2><%= Html.Encode("Crear nueva versión")%></h2>
    <%} %>
    <% if(Model.EsNuevo){%>
        <h2><%= Html.Encode("Crear nueva rúbrica")%></h2>
    <%} %>
    

    <% using (Html.BeginForm()) {%>
        <%= Html.HiddenFor(model=>model.EsNuevo) %>
        <% var htmlAttributes = !Model.EsNuevo?new {disabled=true}:null; %>
        
        <% if(!Model.EsNuevo){%>
            <%= Html.HiddenFor(x=>x.Rubrica.TipoArtefacto) %>
            <%= Html.HiddenFor(x=>x.Rubrica.RubricaId) %>
        <%} %>
        
        
        <style>
        
            input[type="text"]
            {
                width:300px;
            }

            select
            {
                width:302px;
            }


            textarea
            {
            width:296px;
            font-family: Arial;
            font-size: 13px;
            }
        
        </style>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.Label("Rubrica")%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Rubrica.RubricaId, htmlAttributes)%>
            </div>
            
            <div class="editor-label">
                <%= Html.Label("Artefacto")%>
            </div>
            <div class="editor-field">
                <%= Html.DropDownListFor(x => x.Rubrica.TipoArtefacto, new SelectList(Model.TiposArtefactos.ToDictionary(x => x.TipoArtefacto, x => x.TipoArtefacto + " - " + x.Descripcion), "key", "value"), "Seleccionar")%>
            </div>
           
            <div class="editor-label">
                <%= Html.Label("Descripcion")%>
            </div>
            <div class="editor-field">
                <%= Html.TextAreaFor(model => model.Rubrica.Descripcion)%>
            </div>

            <div class="editor-label">
                <%= Html.Label("Tipo")%>
            </div>
            <div class="editor-field">
                <%= Html.DropDownListFor(model => model.Rubrica.TipoRubrica, new SelectList(Model.Rubrica.TiposRubrica,"key","value"),"Seleccionar") %>
            </div>
            
            <div class="editor-label">
                <%= Html.Label("Version")%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Rubrica.Version)%>
            </div>
            
            <div class="editor-label">
                <%= Html.Label("Creador")%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Rubrica.CreadorId)%>
            </div>
            
                <input type="submit" value="Crear" />
        </fieldset>

    <% } %>

</asp:Content>

