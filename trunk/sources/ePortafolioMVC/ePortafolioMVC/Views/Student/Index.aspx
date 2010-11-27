<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.StudentIndexViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Lista de cursos</h2>
    <% using (Html.BeginBorder())
       {%>
    <% using (Html.BeginForm())
       {%>
       
    <%= Html.ValidationSummary(true) %>
    <% foreach (KeyValuePair<BECurso, List<BETrabajo>> dictionaryItem in Model.TrabajosCurso)
       { %>
    <h3>
        <%= Html.Encode(dictionaryItem.Key.Codigo + " " + dictionaryItem.Key.Nombre)%></h3>
    <%  
        if (dictionaryItem.Value.Count() == 0) //El curso no tiene trabajos habilitados
        {%>
    No existen trabajos registrados para este curso.<br />
    <%}%>
    <%if (dictionaryItem.Value.Count() != 0) //El curso tiene trabajos habilitados
      {%>
    <table class="table">
        <tr>
            <th>
                Rol
            </th>
            <th style="width: 270px">
                Trabajo
            </th>
            <th>
                Inicio
            </th>
            <th>
                Fin
            </th>
            <th>
                Modo
            </th>
        </tr>
        <% foreach (BETrabajo Trabajo in dictionaryItem.Value)
           {
               BEGrupo GrupoEntregado = Model.GruposTrabajosEntregados.SingleOrDefault(g => g.Trabajo.TrabajoId == Trabajo.TrabajoId);
               BEGrupo GrupoPendiente = Model.GruposTrabajosPendientes.SingleOrDefault(g => g.Trabajo.TrabajoId == Trabajo.TrabajoId);
               BEGrupo Grupo = GrupoEntregado == null ? GrupoPendiente : GrupoEntregado;
               Boolean EsVigente = (Trabajo.FechaInicio == null || Trabajo.FechaInicio <= DateTime.Today) && (Trabajo.FechaFin == null || Trabajo.FechaFin >= DateTime.Today);
               Boolean EsFinalizado = (Trabajo.FechaFin != null && Trabajo.FechaFin < DateTime.Today);

               if (GrupoEntregado != null && (EsVigente||EsFinalizado))
               {
                   //El trabajo ya no esta vigente%>
        <tr class="vigente">
            <%}
               else if (EsVigente || EsFinalizado)
        {
            //El trabajo aun no esta vigente{%>
            <tr class="aun-no-vigente">
                <%}
        else
        {
            //El trabajo esta vigente%>
                <tr class="ya-no-vigente">
                    <%} %>
                    <td>
                        <% if (Grupo == null)
                           {%>
                        <img src="<%=Url.Content("~/Content/images/imgGrupoSin.png")%>" title="Sin grupo" />
                        <%
                            }
                           else
                           {
                               if (Grupo.Lider.AlumnoId == Model.ActualAlumno.AlumnoId)
                               {%>
                        <img src="<%=Url.Content("~/Content/images/imgGrupoLider.png")%>" title="Lider de grupo" />
                        <%
                            }
                       else
                       {%>
                        <img src="<%=Url.Content("~/Content/images/imgGrupoMiembro.png")%>" title="Miembo de grupo" />
                        <%
                            }
                   }
                        %>
                    </td>
                    <td style="text-align: left;">
                        <%= Html.ActionLink(Trabajo.Nombre, "Details", new { TrabajoId = Trabajo.TrabajoId })%>
                    </td>
                    <td>
                        <%= Html.Encode(Trabajo.FechaInicio.HasValue ? Trabajo.FechaInicio.Value.ToShortDateString() : "-")%>
                    </td>
                    <td>
                        <%= Html.Encode(Trabajo.FechaFin.HasValue ? Trabajo.FechaFin.Value.ToShortDateString() : "-")%>
                    </td>
                    <td>
                        <%if (Trabajo.EsGrupal)
                          {%>
                        <img src="<%=Url.Content("~/Content/images/imgGrupal.png")%>" title="Grupal" />
                        <%}
                          else
                          {%>
                        <img src="<%=Url.Content("~/Content/images/imgIndividual.png")%>" title="Individual" />
                        <%}%>
                    </td>
                </tr>
                <%  } %>
    </table>
    <% } %>
    <br />
    <% } %>
    <%} %>
    <%} %>
</asp:Content>
