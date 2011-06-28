<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RubricOn.Models.RubricOn.Entities.RubricasBE>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode("Aplicar rúbrica")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode("Aplicar rúbrica")%></h2>



    <% using(Html.BeginForm()){ %>
    <table>
        <tr>
            <th>
                Rubrica
            </th>
            <td>
                <%= Model.RubricaId %>
            </td> 
         </tr>
         <tr>
            <th>
                Artefacto
            </th>
            <td>
                <%= Model.TipoArtefacto%>
            </td>            
         </tr>
    </table>
  <br />
  
      <table>
        <tr>
            <th>
                Evaluado
            </th>
            <td>
                <%= Html.TextBox("CodigoEvaluadoId")%>
            </td> 
         </tr>
         <tr>
            <th>
                Evaluador
            </th>
            <td>
                <%= Html.TextBox("CodigoEvaluadorId")%>
            </td>            
         </tr>
    </table>
    <br />
    <%= Html.Submit("Iniciar evaluacion")%>
    <% }%>
  

</asp:Content>
