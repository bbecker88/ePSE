<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ProfessorSite.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.ProfessorGradeAssignmentViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Evaluar trabajo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3 style="text-align: center">
        <%= Html.Encode(Model.Curso.Codigo + " " + Model.Curso.Nombre)%></h3>
    <h2 style="text-align: center">
        <%= Html.Encode(Model.Trabajo.Nombre) %></h2>
    <h3 style="text-align: center">
        <%= Html.Encode("Rubricas") %></h3>
        
        
        <% Html.RenderPartial("AssignmentDetail", Model.Trabajo); %>
        
         <% if (Model.Grupo != null)
            {
                Html.RenderPartial("FileListWithoutAction", new ePortafolioMVC.ViewModels.SharedFileListViewModel()
                {
                    ArchivosGrupo = Model.Archivos,
                    Trabajo = Model.Trabajo
                });
            }%>   

    <% using (Html.BeginForm())
       {%>
    
    <div class="curso">
    <h1>
        RUBRICAS</h1>
    
    <%foreach (KeyValuePair<BERubrica, List<BECriterio>> dictionaryItem in Model.CriteriosRubrica)
      {
          Html.RenderPartial("Rubrica", new ePortafolioMVC.ViewModels.SharedRubricaViewModel()
          {
              Criterios = dictionaryItem.Value,
              Editable = Model.EsEditable,
              ResultadoRubricas = Model.ResultadosRubricas,
              Rubrica = dictionaryItem.Key
          });
      } %> 
      </div>               
    
    
    
    <% if (Model.EsEditable)
                   {%>
                   <br />
    <input type="submit" class="Button" id="submitButton" value="Grabar" />
    <% } %>
    <% } %>
    <div>
      <%switch(Model.Origen){ %>
        <%case "Details":%><%= Html.ActionLink("Regresar", "Details", new{TrabajoId =  Model.Trabajo.TrabajoId})%><%break; %>
        <%case "Index":%><%= Html.ActionLink("Regresar", "Index")%><%break; %>
        <%}%>
    </div>
</asp:Content>
