<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RubricOn.ViewModel.ListarVersionesRubricaViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode("Listar versiones")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode("Listar versiones")%></h2>
    
    <% var VersionActual = Model.Rubricas.FirstOrDefault(x=>x.EsActual);
       var RubricaId = Model.Rubricas.First().RubricaId;%>
    
    <table>
        <tr>
            <th>
                Rubrica
            </th>
            <td>
                <%= VersionActual!=null?VersionActual.RubricaId:"N/A" %>
            </td> 
         </tr>
         <tr>
            <th>
                Version Actual
            </th>
            <td>
                <%= VersionActual!=null?VersionActual.Version:"N/A" %>
            </td>            
         </tr>
         <tr>
            <th>
                Artefacto
            </th>
            <td>
                <%= Html.Encode(Model.TipoArtefacto.TipoArtefacto)%>
            </td>            
         </tr>
    </table>
    <br />
    
    <table style="width:100%">
        <tr>
            <th>
                Version
            </th>
            <th>
                Descripcion
            </th>
            <th>
                Tipo
            </th>
            <th>
                Creador
            </th>
            <th>
                Fecha Creacion
            </th>
            <th>
                Acciones
            </th>
        </tr>

    <% foreach (var item in Model.Rubricas) {
           var estiloFila = item.EsActual ? "style=\"background-color:#d0ffcb; \"" : "";
           %>
    
        <tr <%= estiloFila%>>
            <td>
                <%= Html.Encode(item.Version) %>
            </td>
            <td>
                <%= Html.Encode(item.Descripcion) %>
            </td>
            <td>
                <%= Html.Encode(item.TipoRubrica) %>
            </td>
            <td>
                <%= Html.Encode(item.CreadorId) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.FechaCreacion)) %>
            </td>            
            <td>
                <% if(!item.EsActual){%>
                    <%= Html.ActionLink("Actual", "CambiarVersionActualRubrica", new { RubricaId = item.RubricaId, Version = item.Version, TipoArtefacto = Model.TipoArtefacto.TipoArtefacto}) %> |  
                <%} %>
                <% if(!Model.VersionesConEvaluaciones.Contains(item.Version)){%>
                    <%= Html.ActionLink("Diseñar", "DisenarVersionSegundaRubrica", new { RubricaId = item.RubricaId, Version = item.Version, TipoArtefacto = Model.TipoArtefacto.TipoArtefacto}) %> |
                    <%= Html.ActionLink("Eliminar", "EliminarVersionRubrica", new { RubricaId = item.RubricaId, Version = item.Version, TipoArtefacto = Model.TipoArtefacto.TipoArtefacto}) %> |
                <%} %>
                <%= Html.ActionLink("Ver", "VerVersionRubrica", new { RubricaId = item.RubricaId, Version = item.Version, TipoArtefacto = Model.TipoArtefacto.TipoArtefacto, EvaluacionId = 0}) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Crear nueva versión", "CrearVersionRubrica", new { RubricaId = RubricaId, TipoArtefacto = Model.TipoArtefacto.TipoArtefacto })%>
    </p>

</asp:Content>
