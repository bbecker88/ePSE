<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ProfessorSite.Master"
    Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.ProfessorGradesViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Notas
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationBar" runat="server">
    <li><%= Html.ActionLink(((BEPeriodo)Session["VistaPeriodo"]).Nombre,"SwitchPeriod",new {PeriodoId = ((BEPeriodo)Session["VistaPeriodo"]).PeriodoId} )%></li>
    <li ><%= Html.ActionLink(Model.Curso.Codigo + " - " + Model.Trabajo.Nombre, "Details", new { TrabajoId = Model.Trabajo.TrabajoId })%></li>
    <li>Consolidado de notas</li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3 style="text-align: center">
        <%= Html.Encode(Model.Curso.Codigo + " " + Model.Curso.Nombre) %></h3>
    <h2 style="text-align: center">
        <%= Html.Encode(Model.Trabajo.Nombre) %></h2>
    <h3 style="text-align: center">
        <%= Html.Encode("Consolidado de Notas") %></h3>
    <% using (Html.BeginForm())
       {%>
    <% int alternate = 0; %>
    <div class="curso">
        <h1 id="TitleNotas">
            NOTAS</h1>

        <script type="text/javascript">
            function hideAll() {
                $('[id*="SECCION"]').hide();
            }

            function showAll() {
                $('[id*="SECCION"]').show();
                $('#TitleNotas').text("NOTAS");
            }

            function showSingle(SeccionId) {
                hideAll();
                $('[id*="SECCION' + SeccionId + '"]').show();
                $('#TitleNotas').text("NOTAS (SECCION " + SeccionId + ")");
            }
       
        </script>

        <% if (Model.Secciones.Count > 1)
           {%>
        <a href="#" onclick="showAll()">TODO</a>
        <%
            foreach (BESeccion Seccion in Model.Secciones)
            {%>
        <a href="#" onclick="showSingle('<%=Html.Encode(Seccion.SeccionId)%>')">
            <%= Html.Encode(Seccion.Nombre)%></a>
        <%}%>
        <br />
        <%} %>
        <table border="0" cellpadding="0" cellspacing="0">
            <thead>
                <tr>
                    <th width="260">
                        Alumno
                    </th>
                    <th width="70">
                        Nota
                    </th>
                </tr>
            </thead>
            <tbody>
                <% foreach (KeyValuePair<BEAlumno, BEGrupo> dictionaryItem in Model.AlumnosGrupos)
                   {%>
                <tr id="SECCION<%= Html.Encode(dictionaryItem.Value.Seccion.SeccionId)%>" <%= (alternate++) % 2 == 0 ? "class=\"gray\"" : ""%>>
                    <td style="text-align: left; padding-left: 8px;">
                        <%= Html.Encode(dictionaryItem.Key.Nombre)%>
                    </td>
                    <td>
                        <%= Html.Encode(dictionaryItem.Value.Nota) %>
                    </td>
                </tr>
                <%  } %>
            </tbody>
        </table>
        <br />
        <%= Html.Encode("* EI = Evaluacion incompleta / NE = No evaluado") %>
    </div>
    <br />
    <input type="submit" class="Button" id="submitButton" value="Imprimir" />
    <% } %>
    <div>
        <%switch (Model.Origen)
          { %>
        <%case "Details":%><%= Html.ActionLink("Regresar", "Details", new { TrabajoId = Model.Trabajo.TrabajoId })%><%break; %>
        <%case "Index":%><%= Html.ActionLink("Regresar", "Index")%><%break; %>
        <%}%>
    </div>
</asp:Content>
