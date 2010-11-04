<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.CalificarTrabajoProfesorViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Evaluar trabajo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Details</h2>
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
           { %>
        <tr>
            <td style="width: 500px; text-align: left;">
                <%= Html.Encode(criterio.Nombre) %>
            </td>
            <td style="width: 70px;">
                <%=Html.Encode(criterio.Valor.ToString("F2") )%>
                <%=Html.RadioButton(rubrica.RubricaId.ToString(), criterio.CriterioId,Model.ListResultados.Any(r => r.CriterioId == criterio.CriterioId))%>
            </td>
        </tr>
        <%  } %>
    </table>
    <br />
    <%  } %>
    <input type="submit" class="button" id="submitButton" value="Grabar" />
    <% } %>
    <% } %>
    <div>
        <%= Html.ActionLink("Regresar", "Details", new{id =  Model.Grupo.TrabajoId})%>
    </div>
</asp:Content>
