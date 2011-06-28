<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolioMVC.ViewModels.StudentIndependentAssignmentListViewModel>" %>
<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>

<% int alternate = 0; %>
<div class="curso">
    
    <% if (Model.Trabajos.Count != 0)
       { %>
    <h1>
        <%= Html.Encode("Trabajos Independientes") %></h1>
        
    <table border="0" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th width="24">
                    Rol
                </th>
                <th width="260">
                    Trabajo
                </th>
                <th width="70">
                    Inicio
                </th>
                <th width="70">
                    Fin
                </th>
                <th width="70">
                    Nota
                </th>
                <th width="36">
                    Modo
                </th>
            </tr>
        </thead>
        <tbody>
        <% foreach (BETrabajo Trabajo in Model.Trabajos)
           {
               BEGrupo Grupo = Model.GruposTrabajosIndependientes.SingleOrDefault(g => g.Trabajo.TrabajoId == Trabajo.TrabajoId);
               String Nota = "-";
            %>
        <%= (alternate++) % 2 == 0 ? "<tr class=\"gray\">" : "<tr>"%>
        <td>
                        <% if (Grupo == null)
                           {%>
                        <a title="Sin grupo"><span class="rol"><h5>sin grupo</h5></span></a>
                        <%
                            }
                           else
                           {
                               if (Grupo.Lider.AlumnoId == Model.ActualAlumno.AlumnoId)
                               {%>
                        <a title="L&iacute;der"><span class="rol lider"><h5>l&iacute;der</h5></span></a>
                        <%
                            }
                           else
                           {%>
                        <a title="Integrante"><span class="rol normal"><h5>normal</h5></span></a>
                        <%
                            }
                       }
                        %>
                    </td>
                    <td style="text-align: left; padding-left:8px;">
                        <%= Html.ActionLink(Trabajo.Nombre, "Details", new { TrabajoId = Trabajo.TrabajoId })%>
                    </td>
                    <td>
                        <%= Html.Encode(Trabajo.FechaInicio.HasValue ? Trabajo.FechaInicio.Value.ToShortDateString() : "-")%>
                    </td>
                    <td>
                        <%= Html.Encode(Trabajo.FechaFin.HasValue ? Trabajo.FechaFin.Value.ToShortDateString() : "-")%>
                    </td>
                    <td>
                        <%= Nota%>
                    </td>
                    <td>
                        <%if (Trabajo.EsGrupal)
                          {%>
                        <a title="Grupal"><span class="mod grupal"><h5>grupal</h5>
                        </span></a>
                        <%}
                          else
                          {%>
                        <a title="Individual"><span class="mod"><h5>individual</h5></span></a>
                        <%}%>
                    </td>
                </tr>
                <%  } %>
                </tbody>
    </table>
    <%} %>
</div>