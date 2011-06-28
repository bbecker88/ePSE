<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ProfessorSite.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.ProfessorIndexViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
            <% using (Html.BeginForm())
           {
               foreach (KeyValuePair<BECurso, List<BETrabajo>> dictionaryItemPeriodo in Model.TrabajosCurso)
               {
                   Html.RenderPartial("AssignmentList", new ePortafolioMVC.ViewModels.ProfessorAssignmentListViewModel()
                          {
                              Curso = dictionaryItemPeriodo.Key,
                              ActualProfesor = Model.ActualProfesor,
                              Trabajos = dictionaryItemPeriodo.Value
                          });
               }
           } %>
</asp:Content>
