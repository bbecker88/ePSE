<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolioMVC.ViewModels.SharedRubricaViewModel>" %>
<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<% int alternate = 0; %>
<% if (Model.Criterios.Count != 0)
   { %>
    <table border="0" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th colspan="2" style="text-align: left; padding-left: 8px;">
                    <%= Html.Encode(Model.Rubrica.Nombre) %>
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (BECriterio Criterio in Model.Criterios)
               {
                   Boolean selected = Model.ResultadoRubricas.Any(rr => rr.RubricaId == Model.Rubrica.RubricaId && rr.CriterioId == Criterio.CriterioId);
            %>
            <%= (alternate++) % 2 == 0 ? "<tr class=\"gray\">" : "<tr>"%>
            <td width="450" style="text-align: left; padding-left: 8px;">
                <%= Html.Encode(Criterio.Nombre)%>
            </td>
            <td>
                <%if (selected)
                  { %>
                <b><%=Html.Encode(Criterio.Valor.ToString("F2"))%></b>
                <%} else {%>
                <%=Html.Encode(Criterio.Valor.ToString("F2"))%>
                <%} %>
                
                <% if (Model.Editable)
                   {%>
                <%=Html.RadioButton(Model.Rubrica.RubricaId.ToString(), Criterio.CriterioId, selected)%>
                <%} %>
            </td>
            <%} %>
        </tbody>
    </table>

<%} %>