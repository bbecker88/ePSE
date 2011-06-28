<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolioMVC.Models.Entities.BETrabajo>" %>
<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<% int alternate = 0; %>
<div class="curso">
    <h1>
        DETALLES DEL TRABAJO</h1>
    <table border="0" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th width="260">
                    Nombre
                </th>
                <th width="70">
                    Inicio
                </th>
                <th width="70">
                    Fin
                </th>
                <th width="36">
                    Modo
                </th>
            </tr>
        </thead>
        <tbody>
            <%= (alternate++) % 2 == 0 ? "<tr class=\"gray\">" : "<tr>"%>
            <td style="text-align: left; padding-left: 8px;">
                <%= Html.Encode(Model.Nombre)%>
            </td>
            <td>
                <%= Html.Encode(Model.FechaInicio.HasValue ? Model.FechaInicio.Value.ToShortDateString() : "-")%>
            </td>
            <td>
                <%= Html.Encode(Model.FechaFin.HasValue ? Model.FechaFin.Value.ToShortDateString() : "-")%>
            </td>
            <td>
                <%if (Model.EsGrupal)
                          {%>
                <a title="Grupal"><span class="mod grupal">
                    <h5>
                        grupal</h5>
                </span></a>
                <%}
                          else
                          {%>
                <a title="Individual"><span class="mod">
                    <h5>
                        individual</h5>
                </span></a>
                <%}%>
            </td>
            </tr>
        </tbody>
    </table>
</div>
