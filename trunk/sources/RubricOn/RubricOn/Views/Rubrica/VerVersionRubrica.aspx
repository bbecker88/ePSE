<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RubricOn.ViewModel.VerVersionRubricaViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode("Ver rúbrica")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode("Ver rúbrica")%></h2>
    
      <table>
        <tr>
            <th>
                Rubrica
            </th>
            <td>
                <%= Model.Rubrica.RubricaId %>
            </td> 
         </tr>
         <tr>
            <th>
                Version
            </th>
            <td>
                <%= Model.Rubrica.Version%>
            </td>            
         </tr>
         <tr>
            <th>
                Artefacto
            </th>
            <td>
                <%= Model.Rubrica.TipoArtefacto%>
            </td>            
         </tr>
    </table>
  <br />
  <%if (Model.MostrarDetallesEvaluacion) {%>
      <table>
         <tr>
            <th>
                Código del evaluado
            </th>
            <td>
                <%= Model.Evaluacion.CodigoEvaluadoId%>
            </td>            
         </tr>
         <tr>
            <th>
                Código del evaluador
            </th>
            <td>
                <%= Model.Evaluacion.CodigoEvaluadorId%>
            </td>            
         </tr>
                  <tr>
            <th>
                Fecha de evaluación
            </th>
            <td>
                <%= Model.Evaluacion.FechaEvaluacion%>
            </td>            
         </tr>
        <tr>
            <th>
                Resultado
            </th>
            <td>
                <%= Model.Resultado.Resultado%>
            </td> 
         </tr>
    </table>
    <br />
    <%} %>
    
    <style>
        .tdCriterioSeleccionadoPar{ background-color:#ffa9a9;}
        .tdCriterioSeleccionadoImpar{ background-color:#ffc8c8;}
        
        .tdCriterioPar{ background-color:#DDE9FF;}
        .tdCriterioImpar{ background-color:#f5faff;}
        
        .tdCategoriaPar{ background-color:#E8FF9B;}
        .tdCategoriaImpar{ background-color:#ccff9b;}
     </style>
    
    <% var EsCategoriaPar = true;%>
    <% var EsAspectoPar = true;%>
    <% foreach (var categoria in Model.Categorias.OrderBy(x => x.Orden)){ %>
        <% EsCategoriaPar = !EsCategoriaPar;%>
        <% var aspectos = Model.Aspectos.Where(x => x.CategoriaRubricaId == categoria.CategoriaRubricaId).OrderBy(x=>x.Orden).ToList();%>
        <% var maximo = aspectos.Sum(x => Model.Criterios.Where(c => c.AspectoRubricaId == x.AspectoRubricaId).Max(m => m.Valor));%>
        <table style="width:100%">    
            <% foreach (var aspecto in aspectos){ %>
            <% EsAspectoPar = !EsAspectoPar;%>
            <% var criterios = Model.Criterios.Where(x => x.AspectoRubricaId == aspecto.AspectoRubricaId).OrderBy(x => x.Orden).ToList();
               var tdWidthStyle = " width:" + (100.0 / (criterios.Count+1)*1.0) + "%; ";

               var tdCategoriaClass = EsCategoriaPar ? " tdCategoriaPar " : " tdCategoriaImpar ";
               var tdCriterioClass = EsAspectoPar ? " tdCriterioPar " : " tdCriterioImpar ";

               var tdCriterioSeleccionadoClass = EsAspectoPar ? " tdCriterioSeleccionadoPar " : " tdCriterioSeleccionadoImpar ";
                   %>                  
                <tr>
                    <% if(aspectos.First() == aspecto){ %>                   
                        <td rowspan="<%= aspectos.Count()*2 %>" style=" <%= tdWidthStyle%>" class="<%= tdCategoriaClass %>">
                            <%= categoria.Nombre %><br />
                            <b><%= maximo%> Puntos</b>
                            
                        </td>
                    <%} %>
                    <% foreach (var criterio in criterios){ %>
                    <% var tdClass = Model.MostrarDetallesEvaluacion && Model.Respuestas != null && Model.Respuestas.Any(x => x.CriterioRubricaId == criterio.CriterioRubricaId) ? tdCriterioSeleccionadoClass : tdCriterioClass;  %>
                        <td style=" <%= tdWidthStyle%>" class="<%= tdClass %>"><%= criterio.Nombre%></td>
                    <%} %>
                </tr>
                <tr>
                    <% foreach (var criterio in criterios){ %>
                    <% var tdClass = Model.MostrarDetallesEvaluacion && Model.Respuestas != null && Model.Respuestas.Any(x => x.CriterioRubricaId == criterio.CriterioRubricaId) ? tdCriterioSeleccionadoClass : tdCriterioClass;  %>
                         <td style=" <%= tdWidthStyle%>" class="<%= tdClass %>"><b><%= criterio.Valor%> Puntos</b></td>
                    <%} %>            
                </tr>
                
            <%} %>
        </table>    
        <br />
    <%} %>

</asp:Content>
