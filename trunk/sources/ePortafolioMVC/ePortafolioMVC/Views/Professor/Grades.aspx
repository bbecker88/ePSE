<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.ProfessorGradesViewModel>" %>
<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Notas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3 style="text-align: center">
        <%= Html.Encode(Model.Curso.Codigo + " " + Model.Curso.Nombre) %></h3>
    <h2 style="text-align: center">
        <%= Html.Encode(Model.Trabajo.Nombre) %></h2>
    <h3 style="text-align: center">
        <%= Html.Encode("Consolidado de Notas") %></h4>
    <% using (Html.BeginBorder())
       {%>
    <% using (Html.BeginForm())
       {%>
    <h3 id="TitleNotas">
        Notas</h3>
    <br />
    
   <script type="text/javascript">
       function hideAll() {
           $('[id*="SECCION"]').hide();
       }

       function showAll() {
           $('[id*="SECCION"]').show();
           $('#TitleNotas').text("Notas");
       }

       function showSingle(SeccionId) {
           hideAll();
           $('[id*="SECCION' + SeccionId + '"]').show();
           $('#TitleNotas').text("Notas (Seccion "+SeccionId+")");
       }
       
    </script>
    
    <a href="#" onclick="showAll()">TODO</a>
    
    <% foreach(BESeccion Seccion in Model.Secciones)
       {%>
        <a href="#" onclick="showSingle('<%=Html.Encode(Seccion.SeccionId)%>')"> <%= Html.Encode(Seccion.Nombre) %></a>
    <%} %>
    <br />
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
        <% foreach (KeyValuePair<BEGrupo, List<BEAlumno>> dictionaryItem in Model.AlumnosGrupoNotas)
           {
               foreach (BEAlumno Alumno in dictionaryItem.Value){%>
        <tr id="SECCION<%= Html.Encode(dictionaryItem.Key.Seccion.SeccionId) %>">
            <td style="width: 500px; text-align: left;">
                <%= Html.Encode(Alumno.Nombre)%>
            </td>
            <td style="width: 70px;">
                <%= Html.Encode(dictionaryItem.Key.Nota) %>
            </td>
        </tr>
        <%  } %>
        <%  } %>
    </table>
    <br />
    <%= Html.Encode("* EI = Evaluacion incompleta / NE = No evaluado") %>  
    <br />
    <br />
    <input type="submit" class="button" id="submitButton" value="Imprimir" />
    <% } %>
    <% } %>
    <div>
    
    <%switch(Model.Origen){ %>
        <%case "Details":%><%= Html.ActionLink("Regresar", "Details", new { TrabajoId = Model.Trabajo.TrabajoId })%><%break; %>
        <%case "Index":%><%= Html.ActionLink("Regresar", "Index")%><%break; %>
        <%}%>
    </div>
</asp:Content>
