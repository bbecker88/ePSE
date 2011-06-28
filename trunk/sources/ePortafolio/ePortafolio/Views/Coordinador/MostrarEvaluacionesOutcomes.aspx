<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolio.ViewModel.Coordinador.MostrarEvaluacionesOutcomesViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MostrarEvaluacionesOutcomes
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="CiclosContent" runat="server">
    <% Html.RenderPartial("MenuCiclosDisponibles", Session.Get(GlobalKey.ActualPeriodoId)); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="block">
<h2>
        Mostrar evaluaciones de Outcomes</h2>
        <% using (Ajax.BeginForm("MostrarEvaluacionesOutcomesFiltradas", new AjaxOptions() { UpdateTargetId = "EvaluacionesFiltradasDiv" }))
           { %>
        
    <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
        <tbody>
            <tr>
                <td style="text-align: left">
                    Periodo corte:<br />
                    <%= Html.DropDownListFor(x => x.PeriodoId, new SelectList(Model.Periodos, "PeriodoId", "PeriodoId"), "Seleccionar", new { style = "width:200px" })%>
                </td>
                <td style="text-align: left">
                    Evaluador:
                    <br />
                    <%= Html.TextBoxFor(x => x.ProfesorId, new { style = "width:200px" })%>
                </td>
                
                </tr>
                <tr>
                <td style="text-align: left">
                    Outcome:<br />
                    <%= Html.DropDownListFor(x => x.OutcomeId, new SelectList(Model.Outcomes, "OutcomeId", "Outcome"), "Seleccionar", new { style = "width:200px" })%>
                </td>
                <td style="text-align: left">
                    Alumno:
                    <br />
                    <%= Html.TextBoxFor(x => x.AlumnoId, new { style = "width:200px" })%>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <%=Html.Submit("Ver detalle", new { @class = "btn btn_search rounded" })%>
                    <%=Html.ActionLink("Agregar", "AgregarEvaluacionesOutcomes", null, new { @class = "btn btn_add rounded" })%>
                </td>
            </tr>
        </tbody>
    </table>
    <%} %>
    
    <br />
    <div id="EvaluacionesFiltradasDiv">
        <% Html.RenderAction("MostrarEvaluacionesOutcomesFiltradas", new { AlumnoId = Model.AlumnoId, ProfesorId = Model.ProfesorId, PeriodoId = Model.PeriodoId, OutcomeId = Model.OutcomeId}); %>
    </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="WelcomeContent" runat="server">
</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="BreadcrumbsContent" runat="server">
</asp:Content>
