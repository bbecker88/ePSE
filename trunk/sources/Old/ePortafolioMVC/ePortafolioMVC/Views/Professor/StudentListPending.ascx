<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolioMVC.ViewModels.ProfessorStudentListPending>" %>
<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<% int alternate = 0; %>
<div class="curso">
    <h1>
        NO ENTREGADOS (<%=  Model.AlumnosGrupos.Count.ToString() %>)</h1>
    <table border="0" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th width="436">
                    Alumnos
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (KeyValuePair<BEGrupo, List<BEAlumno>> dictionaryItem in Model.AlumnosGrupos)
               {%>
            <%= (alternate++) % 2 == 0 ? "<tr class=\"gray\">" : "<tr>"%>
            <td style="text-align: left; padding-left: 8px;">
                <%foreach (BEAlumno Alumno in dictionaryItem.Value)
                  {%>
                <%= Html.Encode(Alumno.Nombre)%><br />
                <%} %>
            </td>
            </tr>
            <%} %>
        </tbody>
    </table>
    
</div>
