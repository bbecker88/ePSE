<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.ReporteNotasTrabajoProfesorViewModel>" %>
<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Notas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Consolidado de notas</h2>
    <% using (Html.BeginBorder())
       {%>
    <% using (Html.BeginForm())
       {%>
    <h3>
        Notas</h3>
    <br />
    <table class="table">
        <tr>
            <th style="width: 500px; text-align: left;">
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
    <%= Html.Encode("* IN = Evaluacion imcompleta / NE = No evaluado") %>  
    <br />
    <br />
    <input type="submit" class="button" id="submitButton" value="Grabar" />
    <% } %>
    <% } %>
    <div>
        <%= Html.ActionLink("Regresar", "Details", new{id =  Model.Trabajo.TrabajoId})%>
    </div>
</asp:Content>
