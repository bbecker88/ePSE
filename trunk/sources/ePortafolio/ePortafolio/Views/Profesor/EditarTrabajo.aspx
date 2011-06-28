<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolio.ViewModel.EditarTrabajoViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditarTrabajo
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CiclosContent" runat="server">
    <% Html.RenderPartial("MenuCiclosDisponibles",Model.Trabajo.PeriodoId); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <link href="<%= Url.Content("~/Content/jquery.ui/jquery-ui-1.8.12.custom.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Url.Content("~/Scripts/cleditor/jquery.cleditor.css") %>" rel="stylesheet" type="text/css" />

    <script src="<%= Url.Content("~/Scripts/jquery-ui/jquery-ui-1.8.12.custom.js")%>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jquery-ui/i18n/jquery.ui.datepicker-es.js")%>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/cleditor/jquery.cleditor.min.js")%>" type="text/javascript"></script>
    
    <% using (Html.BeginForm()){ %>            
    <div class="block">
      <h2><%= Html.Encode(Model.Curso.Codigo + " - " + Model.Trabajo.Nombre) %></h2>
    
      <table border="0" cellpadding="0" cellspacing="0"  class="rounded centered" style="width:410px">
        <tr>
            <td width="80">Nombre:</td>
            <td width="80"><%= Html.TextBoxFor(x => x.Trabajo.Nombre)%></td>
            <td width="80">Modo:</td>
            <td width="80">
                    <%= Html.RadioButtonFor(x => x.Trabajo.EsGrupal, true, new {id = "TrabajoGrupal" })%> 
                    <label for="TrabajoGrupal">Grupal</label>
                    <%= Html.RadioButtonFor(x => x.Trabajo.EsGrupal, false, new {id = "TrabajoIndividual" })%> 
                    <label for="TrabajoIndividual">Individual</label>
            </td>
        </tr>
        <tr>
            <td width="80">Inicio:</td>
            <td width="80"><%= Html.TextBox("Trabajo.FechaInicio", Model.Trabajo.FechaInicio.HasValue ? Model.Trabajo.FechaInicio.Value.ToShortDateString() : String.Empty)%></td>
            <td width="80">Fin:</td>
            <td width="80"><%= Html.TextBox("Trabajo.FechaFin", Model.Trabajo.FechaFin.HasValue ? Model.Trabajo.FechaFin.Value.ToShortDateString() : String.Empty)%></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align:center;"><%= Html.TextAreaFor(x => x.Trabajo.Instrucciones)%></td>
        </tr>
        <tr>
            <td colspan="4"><%= Html.Submit("Guardar cambios", new { @class="btn btn_save rounded"})%></td>
        </tr>
      </table>
    </div>
    <%} %>


  <script type="text/javascript">

      $(document).ready(function() {
          $("[name='Trabajo.Instrucciones']").cleditor({ width: 720 });
          $("[name='Trabajo.FechaInicio']").datepicker($.datepicker.regional["es"]);
          $("[name='Trabajo.FechaFin']").datepicker($.datepicker.regional["es"]);
      });

    </script>

</asp:Content>
