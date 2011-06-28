<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RubricOn.ViewModel.ListarEvaluacionesViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode("Evaluaciones")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode("Evaluaciones")%></h2>
    
        <style>
        
            select
            {
                width:100%;
            }
       
        </style>
    
    <% using (Ajax.BeginForm("ListarEvaluacionesFiltradas", new AjaxOptions() { UpdateTargetId = "EvaluacionesFiltradasDiv" })){%>
    
    <table>
        <tr>
            <th>
                Artefacto
            </th>
            <td colspan="3">
                <%= Html.DropDownListFor(x => x.TipoArtefacto, new SelectList(Model.TiposArtefacto.ToDictionary(x => x.TipoArtefacto, x => x.TipoArtefacto + " - " + x.Descripcion), "key", "value"), "Seleccionar")%>
            </td> 
         </tr>
         <tr>
            <th>
                Rubrica
            </th>
            <td>
                <%= Html.TextBoxFor(x=>x.RubricaId)%>
            </td> 
            <th>
                Version
            </th>
            <td>
                <%= Html.TextBoxFor(x => x.Version)%>
            </td>  
         </tr>
         <tr>
            <th>
                Evaluado
            </th>
            <td>
                <%= Html.TextBoxFor(x => x.CodigoEvaluadoId)%>
            </td>            
            <th>
                Evaluador
            </th>
            <td>
                <%= Html.TextBoxFor(x => x.CodigoEvaluadorId)%>
            </td>            
         </tr>
         <tr>
            <th>
                Fecha Inicio
            </th>
            <td>
                <%= Html.TextBoxFor(x => x.FechaInicio)%>
            </td>            
            <th>
                Fecha Fin
            </th>
            <td>
                <%= Html.TextBoxFor(x => x.FechaFin)%>
            </td>            
         </tr>
    </table>
    
    <%= Html.Submit("Ver detalle") %>
    <%} %>
    <br />
    <div id="EvaluacionesFiltradasDiv">
        <% Html.RenderAction("ListarEvaluacionesFiltradas", new { TipoArtefacto = Model.TipoArtefacto, RubricaId = Model.RubricaId, Version = Model.Version, CodigoEvaluadoId = Model.CodigoEvaluadoId, CodigoEvaluadorId = Model.CodigoEvaluadorId, FechaInicio = Model.FechaInicio, FechaFin = Model.FechaFin }); %>
    </div>
    
    

</asp:Content>


