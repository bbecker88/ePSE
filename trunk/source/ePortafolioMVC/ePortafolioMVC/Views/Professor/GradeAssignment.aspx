<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.CalificarTrabajoProfesorViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Evaluar trabajo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3 style="text-align: center">
        <%= Html.Encode(Model.Trabajo.CursoId + " " + Model.Trabajo.Curso.Nombre)%></h3>
    <h2 style="text-align: center">
        <%= Html.Encode(Model.Trabajo.Nombre) %></h2>
    <h3 style="text-align: center">
        <%= Html.Encode("Rubricas") %></h4>
        <%bool alt = false; %>
        <% using (Html.BeginBorder())
           {%>
        <h3>
            Detalles del trabajo</h3>
        <br />
        <table class="table">
            <tr>
                <th style="width: 270px">
                    Nombre
                </th>
                <th>
                    Inicio
                </th>
                <th>
                    Fin
                </th>
                <th>
                    Modo
                </th>
            </tr>
            <tr>
                <td style="text-align: left">
                    <%= Html.Encode(Model.Trabajo.Nombre) %>
                </td>
                <td>
                    <%= Html.Encode(Model.Trabajo.FechaInicio.HasValue ? Model.Trabajo.FechaInicio.Value.ToShortDateString() : "-")%>
                </td>
                <td>
                    <%= Html.Encode(Model.Trabajo.FechaFin.HasValue ? Model.Trabajo.FechaFin.Value.ToShortDateString() : "-")%>
                </td>
                <td>
                    <%if (Model.Trabajo.EsGrupal)
                      { %>
                    <img src="<%=Url.Content("~/Content/images/imgGrupal.png")%>"  title="Grupal"/>
                    <%}
                      else
                      { %>
                    <img src="<%=Url.Content("~/Content/images/imgIndividual.png")%>" title="Individual"/>
                    <%} %>
                </td>
            </tr>
        </table>
        <%} %>
        
    <% using (Html.BeginBorder())
       {%>
    <% using (Html.BeginForm())
       {%>
    <h3>
        Rubricas</h3>
    <br />
    <%foreach (var rubrica in Model.ListRubricas)
      { %>
    <table class="table">
        <tr>
            <th style="width: 500px; text-align: left;">
                <%=Html.Encode(rubrica.Nombre)%>
            </th>
            <th style="width: 70px;">
                Puntaje
            </th>
        </tr>
        <% foreach (var criterio in rubrica.CriteriosRubricas)
           { 
               var selected = Model.ListResultados.Any(r => r.CriterioId == criterio.CriterioId);
               %>
        <tr class="<%= selected?"selected":""%>">
            <td style="width: 500px; text-align: left;">
                <%= Html.Encode(criterio.Nombre) %>
            </td>
            <td style="width: 70px;">
                <%=Html.Encode(criterio.Valor.ToString("F2") )%>
                <% if (Model.Editable)
                   {%>
                <%=Html.RadioButton(rubrica.RubricaId.ToString(), criterio.CriterioId, selected)%>
                <%} %>
            </td>
        </tr>
        <%  } %>
    </table>
    <br />
    <%  } %>                <% if (Model.Editable)
                   {%>
    <input type="submit" class="button" id="submitButton" value="Grabar" /> <%} %>
    <% } %>
    <% } %>
    <div>
      <%switch(Model.Origen){ %>
        <%case "Details":%><%= Html.ActionLink("Regresar", "Details", new{id =  Model.Grupo.TrabajoId})%><%break; %>
        <%case "Index":%><%= Html.ActionLink("Regresar", "Index")%><%break; %>
        <%}%>
    </div>
</asp:Content>
