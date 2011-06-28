<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.StudentHistoryViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    History
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Historial de trabajos</h2>
    <% using (Html.BeginBorder())
       {%>

    <script src="<%=Url.Content("~/Scripts/jquery-ui/js/jquery.effects.core.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Scripts/jquery-ui/js/jquery-ui-1.8.6.custom.js")%>"
        type="text/javascript"></script>

    <script type="text/javascript">

        $('document').ready(function() { hideAllCurso(); });

        function hideAll() {
            $('[id*="PERIODO"]').hide();
        }

        function showAll() {
            $('[id*="PERIODO"]').show();
        }

        function showSingle(PeriodoId) {
            hideAll();
            $('[id*="PERIODO' + PeriodoId + '"]').show();
        }

        function toggleSingle(CursoId, PeriodoId) {
            $('[id*="CURSO' + CursoId + '.' + PeriodoId + '"]').toggle();
        }

        function hideAllCurso() {
            $('[id*="CURSO"]').hide();
        }
       
    </script>

    <a href="#" onclick="showAll()">TODO</a>
    <% foreach (BEPeriodo Periodo in Model.Periodos)
       {%>
    <a href="#" onclick="showSingle('<%=Html.Encode(Periodo.PeriodoId)%>')">
        <%= Html.Encode(Periodo.Nombre)%></a>
    <%} %>
    <br />
    <% foreach (BEPeriodo Periodo in Model.Periodos)
       { %>
    <span id="PERIODO<%= Periodo.PeriodoId %>">
        <h1>
            <br />
            <%= Html.Encode(Periodo.Nombre)%></h1>
        <% 
            List<BECurso> CursosPeriodo = Model.Cursos.Where(c => Model.Trabajos.Any(t => t.Periodo.PeriodoId == Periodo.PeriodoId && c.CursoId == t.Curso.CursoId)).ToList();
            foreach (BECurso Curso in CursosPeriodo)
            { %>
        <br />
        <h3>
            <a href="#" onclick="toggleSingle('<%= Html.Encode(Curso.CursoId)%>','<%= Html.Encode(Periodo.PeriodoId)%>')">
                <%= Html.Encode(Curso.Codigo)%>
                <%= Html.Encode(Curso.Nombre)%></a></h3>
        <span id="CURSO<%= Html.Encode(Curso.CursoId)%>.<%= Html.Encode(Periodo.PeriodoId)%>">
            <table class="table">

                <% 
                    List<BETrabajo> TrabajosCursoPeriodo = Model.Trabajos.Where(t => t.Curso.CursoId == Curso.CursoId && t.Periodo.PeriodoId == Periodo.PeriodoId).ToList();
                    foreach (BETrabajo Trabajo in TrabajosCursoPeriodo)
                    {
                        BEGrupo Grupo = Model.Grupos.SingleOrDefault(g => g.Trabajo.TrabajoId == Trabajo.TrabajoId);
                %>
                <tr>
                    <td style="text-align: left;width: 20px;">
                    </td>
                    <td style="text-align: left;width: 350px;">
                    <h2 style="text-align: left">
                        <%= Html.ActionLink(Trabajo.Nombre, "Details", new { TrabajoId = Trabajo.TrabajoId })%></h2>
                    </td>
                    <td style="text-align: center;width: 50px;">
                        <%= Html.Encode(Grupo.Nota)%>
                    </td>
                    <td style="text-align: center;width: 50px;">
                        <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgDownload.png"), "DownloadFiles","Professor", "Descargar", new { GrupoId = Grupo.GrupoId })%>
                    </td>
                </tr>
                
                <%  } %>
            </table>
        </span>
        <% } %>
        <br />
    </span>
    <% } %>
    <br />
    <% } %>
</asp:Content>
