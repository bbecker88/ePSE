<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolioMVC.Models.Entities.BEResultadoPrograma>" %>
<% int alternate = 0; %>
<%if (Model != null)
  { %>
<div class="curso">
    <h1>
        RESULTADO DE PROGRAMA</h1>
    <table border="0" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th width="70">
                    Codigo
                </th>
                <th width="370">
                    Resultado
                </th>
            </tr>
        </thead>
        <tbody>
            <%= (alternate++) % 2 == 0 ? "<tr class=\"gray\">" : "<tr>"%>
            <td>
                <%= Html.Encode(Model.Codigo)%>
            </td>
            <td style="text-align: left; padding-left: 8px;">
                <%= Html.Encode(Model.Descripcion)%>
            </td>
            </tr>
        </tbody>
    </table>
</div>
<%} %>