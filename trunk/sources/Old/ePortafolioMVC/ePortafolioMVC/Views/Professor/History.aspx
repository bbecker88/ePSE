<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.ProfessorHistoryViewModel>" %>

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
       
       function toggleSingle(CursoId,PeriodoId) {
           $('[id*="CURSO' + CursoId + '.' + PeriodoId + '"]').toggle();
       }
       function hideAllCurso() {
           $('[id*="CURSO"]').hide();
       }
    </script>
    
    <a href="#" onclick="showAll()">TODO</a>
    
    <% foreach(BEPeriodo Periodo in Model.Periodos)
       {%>
        <a href="#" onclick="showSingle('<%=Html.Encode(Periodo.PeriodoId)%>')"> <%= Html.Encode(Periodo.Nombre)%></a>
    <%} %>
    <br />
    <br />
       
    <% foreach (BEPeriodo Periodo in Model.Periodos)
       { %>
    <span id="PERIODO<%= Periodo.PeriodoId %>">
    <br />
        <h1>
            <%= Html.Encode(Periodo.Nombre)%></h1>
        <% 
    List<BECurso> CursosPeriodo = Model.Cursos.Where(c => Model.Trabajos.Any(t => t.Periodo.PeriodoId == Periodo.PeriodoId && c.CursoId == t.Curso.CursoId)).ToList();
    foreach (BECurso Curso in CursosPeriodo)
    { %>
        <br />
        <h3>
            <a href="#" onclick="toggleSingle('<%= Html.Encode(Curso.CursoId)%>','<%= Html.Encode(Periodo.PeriodoId)%>')"> <%= Html.Encode(Curso.Codigo)%> <%= Html.Encode(Curso.Nombre)%></a></h3>
            <span id="CURSO<%= Html.Encode(Curso.CursoId)%>.<%= Html.Encode(Periodo.PeriodoId)%>">
            
            
        <table class="table">
            <% 
    List<BETrabajo> TrabajosCursoPeriodo = Model.Trabajos.Where(t => t.Curso.CursoId == Curso.CursoId && t.Periodo.PeriodoId == Periodo.PeriodoId).ToList();
    foreach (BETrabajo Trabajo in TrabajosCursoPeriodo)
    {
            %>
            <tr>
            <td style="width:20px;"></td>
                <td style="text-align: left;">
                    <h2><%= Html.ActionLink(Trabajo.Nombre, "Details", new { TrabajoId = Trabajo.TrabajoId })%></h2>
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
