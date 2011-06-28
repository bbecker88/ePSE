<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/StudentSite.Master"
    Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.StudentIndexViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavigationBar" runat="server">
    <li>
        <%= Html.Encode(((BEPeriodo)Session["VistaPeriodo"]).Nombre) %></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="titulos">
    </div>
    <div class="cursos_pendientes">
        <% using (Html.BeginForm())
           {
               foreach (KeyValuePair<BECurso, List<BETrabajo>> dictionaryItemPeriodo in Model.TrabajosCurso)
               {
                   Html.RenderPartial("AssignmentList", new ePortafolioMVC.ViewModels.StudentAssignmentListViewModel()
                          {
                              Curso = dictionaryItemPeriodo.Key,
                              ActualAlumno = Model.ActualAlumno,
                              GruposTrabajosEntregados = Model.GruposTrabajosEntregados,
                              GruposTrabajosPendientes = Model.GruposTrabajosPendientes,
                              Trabajos = dictionaryItemPeriodo.Value
                          });
               }

               Html.RenderPartial("IndependentAssignmentList", new ePortafolioMVC.ViewModels.StudentIndependentAssignmentListViewModel()
               {
                   ActualAlumno = Model.ActualAlumno,
                   GruposTrabajosIndependientes = Model.GruposTrabajosIndependientes,
                   Trabajos = Model.TrabajosIndependientes
               });
           } %>
    </div>
</asp:Content>
