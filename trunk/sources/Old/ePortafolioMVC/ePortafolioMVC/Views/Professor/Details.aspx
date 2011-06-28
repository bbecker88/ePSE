<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ProfessorSite.Master"
    Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.ProfessorDetailsViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3 style="text-align: center">
        <%= Html.Encode(Model.Curso.Codigo + " " + Model.Curso.Nombre) %></h3>
    <h2 style="text-align: center">
        <%= Html.Encode(Model.Trabajo.Nombre) %></h2>
    <h3 style="text-align: center">
        <%= Html.Encode("Detalles") %></h3>
    <%bool alt = false; %>
    <% Html.RenderPartial("ProgramResult", Model.ResultadoPrograma == null ? new BEResultadoPrograma() : Model.ResultadoPrograma); %>
    <% Html.RenderPartial("AssignmentDetail", Model.Trabajo); %>
    <div class="curso">
        <h1>
            ACCIONES</h1>
        <h1>
            <%= Html.ActionLink("Ver consolidado de notas", "Grades", new { TrabajoId = Model.Trabajo.TrabajoId, Origen = "Details" }, new { style = "font-size:medium;" })%>
            &nbsp&nbsp&nbsp
            <%= Html.ActionLink("Ver rubricas", "GradeAssignment", new { TrabajoId = Model.Trabajo.TrabajoId, GrupoId = 0, Origen = "Details", EsEditable = false }, new { style = "font-size:medium;" })%></h1>
    </div>

    <% Html.RenderPartial("StudentListDone", new ePortafolioMVC.ViewModels.ProfessorStudentListDone()
                                                                {
                                                                    AlumnosGrupos = Model.AlumnosGrupoEntregados
                                                                }); %>

    <% Html.RenderPartial("StudentListPending", new ePortafolioMVC.ViewModels.ProfessorStudentListPending()
                                                                {
                                                                    AlumnosGrupos = Model.AlumnosGrupoPendientes
                                                                }); %>
    <div>
        <%= Html.ActionLink("Regresar", "Index") %>
    </div>
</asp:Content>
