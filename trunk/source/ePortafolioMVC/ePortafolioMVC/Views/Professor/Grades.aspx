<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.ReporteNotasTrabajoProfesorViewModel>" %>
<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Notas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3 style="text-align: center">
        <%= Html.Encode(Model.Trabajo.CursoId + " " + Model.Trabajo.Curso.Nombre) %></h3>
    <h2 style="text-align: center">
        <%= Html.Encode(Model.Trabajo.Nombre) %></h2>
    <h3 style="text-align: center">
        <%= Html.Encode("Consolidado de Notas") %></h4>
    <% using (Html.BeginBorder())
       {%>
    <% using (Html.BeginForm())
       {%>
    <h3>
        Notas</h3>
    <br />
    <table class="table">
        <tr>
            <th style="width: 500px;">
                Nombre
            </th>
            <th style="width: 70px;">
                Nota
            </th>
        </tr>
        <% foreach (var alumnoNota in Model.ListAlumnoNotas)
           { %>
        <tr>
            <td style="width: 500px; text-align: left;">
                <%= Html.Encode(alumnoNota.Alumno.Nombre) %>
            </td>
            <td style="width: 70px;">
                <%= Html.Encode(alumnoNota.Nota) %>
            </td>
        </tr>
        <%  } %>
    </table>
    <br />
    <%= Html.Encode("* IN = Evaluacion imcompleta / NE = No evaluado / NA = No aplica")%>  
    <br />
    <br />
    <input type="submit" class="button" id="submitButton" value="Imprimir" />
    <% } %>
    <% } %>
    <div>
    
    <%switch(Model.Origen){ %>
        <%case "Details":%><%= Html.ActionLink("Regresar", "Details", new{id =  Model.Trabajo.TrabajoId})%><%break; %>
        <%case "Index":%><%= Html.ActionLink("Regresar", "Index")%><%break; %>
        <%}%>
    </div>
</asp:Content>
