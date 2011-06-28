<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolio.ViewModel.Coordinador.AgregarEvaluacionesOutcomesViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AgregarEvaluacionesOutcomes
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="CiclosContent" runat="server">
    <% Html.RenderPartial("MenuCiclosDisponibles", Session.Get(GlobalKey.ActualPeriodoId)); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="block">
    <h2>
        Agregar evaluaciones de Outcomes</h2>
        <% using(Html.BeginForm()){ %>
        
    <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
        <tbody>
            <tr>
                <td style="text-align: left">
                    Periodo corte:<br />
                    <%= Html.DropDownListFor(x => x.PeriodoId, new SelectList(Model.Periodos, "PeriodoId", "PeriodoId"), "Seleccionar", new { style = "width:200px" })%>
                </td>
                <td style="text-align: left">
                    Outcome:<br />
                    <%= Html.DropDownListFor(x => x.OutcomeId, new SelectList(Model.Outcomes, "OutcomeId", "Outcome"), "Seleccionar", new { style = "width:200px" })%>
                </td>
                <td style="text-align: left">
                    Evaluador:
                    <br />
                    <%= Html.TextBoxFor(x => x.ProfesorId, new { style = "width:200px" })%>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left">
                    Alumnos:<br />
                    <%= Html.TextAreaFor(x => x.AlumnosId, new { style = "width:671px; font-family: Arial; height:100px" })%>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <%=Html.Submit("Guardar",new{@class = "btn btn_save rounded"}) %>
                    <%=Html.ActionLink("Cancelar", "MostrarEvaluacionesOutcomes", "Coordinador", null, new { @class = "btn btn_cancel rounded" })%>
                </td>
            </tr>
        </tbody>
    </table>
    <%} %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="WelcomeContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BreadcrumbsContent" runat="server">
</asp:Content>
