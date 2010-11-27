<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.ProfessorGradeAssignment>" %>

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
        <%= Html.Encode("Rubricas") %></h4>
        <%bool alt = false; %>
        <% using (Html.BeginBorder())
           {%>
        <h3>
            Detalles del trabajo</h3>
        <br />
        <table class="table">
            <tr>
                <th style="width: 270px">
                    Nombre
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
            <tr>
                <td style="text-align: left">
                    <%= Html.Encode(Model.Trabajo.Nombre) %>
                </td>
                <td>
                    <%= Html.Encode(Model.Trabajo.FechaInicio.HasValue ? Model.Trabajo.FechaInicio.Value.ToShortDateString() : "-")%>
                </td>
                <td>
                    <%= Html.Encode(Model.Trabajo.FechaFin.HasValue ? Model.Trabajo.FechaFin.Value.ToShortDateString() : "-")%>
                </td>
                <td>
                    <%if (Model.Trabajo.EsGrupal)
                      { %>
                    <img src="<%=Url.Content("~/Content/images/imgGrupal.png")%>"  title="Grupal"/>
                    <%}
                      else
                      { %>
                    <img src="<%=Url.Content("~/Content/images/imgIndividual.png")%>" title="Individual"/>
                    <%} %>
                </td>
            </tr>
        </table>
        <%} %>
        
        
         <% if (Model.Grupo != null)
            {

                using (Html.BeginBorder())
                {%>
        <h3>
            Archivos</h3>
                  <table class="table">
                <tr>
                    <th>
                        Nombre
                    </th>
                    <th>
                        Fecha
                    </th>
                    <th>
                        Subido por
                    </th>
                    <th>
                        Opciones
                    </th>
                </tr>
                <% foreach (BEArchivo Archivo in Model.Archivos)
                   { %>
                <tr id="Tr1">
                    <td style="width: 300px; text-align: left;">
                        <%= Html.Encode(Archivo.Nombre)%>
                    </td>
                    <td>
                        <%= Html.Encode(Archivo.FechaSubido.Value.ToShortDateString())%>
                    </td>
                    <td>
                        <span title="<%= Html.Encode(Archivo.Alumno.Nombre)%>">
                            <%= Html.Encode(Archivo.Alumno.AlumnoId)%>
                        </span>
                    </td>
                    <td>
                        <a href="<%= Html.Encode(Url.Content("~"+Archivo.Ruta))%>">
                            <img src="<%= Url.Content("~/Content/images/imgDownload.png")%>" title="Descargar" /></a>
                        <%= Html.ActionLinkImage(Url.Content("~/Content/images/imgDelete.png"), "DeleteFile", "Eliminar", new { TrabajoId = Model.Trabajo.TrabajoId, ArchivoId = Archivo.ArchivoId })%>
                    </td>
                </tr>
                <%  } %>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td><%= Html.ActionLink("Descargar Todo", "DownloadFiles", new { GrupoId = Model.Grupo.GrupoId })%></td>
                </tr>
            </table>
            
            

        <%}
            }%>   

        
        
        
        
        
        
        
    <% using (Html.BeginBorder())
       {%>
    <% using (Html.BeginForm())
       {%>
    <h3>
        Rubricas</h3>
    <br />
    <%foreach (KeyValuePair<BERubrica, List<BECriterio>> dictionaryItem in Model.CriteriosRubrica)
      { %>
    <table class="table">
        <tr>
            <th style="width: 500px; text-align: left;">
                <%=Html.Encode(dictionaryItem.Key.Nombre)%>
            </th>
            <th style="width: 70px;">
                Puntaje
            </th>
        </tr>
        <% foreach (BECriterio Criterio in dictionaryItem.Value)
           { 
               bool selected = Model.ResultadosRubricas.Any(r => r.CriterioId == Criterio.CriterioId && r.RubricaId == dictionaryItem.Key.RubricaId);
               %>
        <tr class="<%= selected?"selected":""%>">
            <td style="width: 500px; text-align: left;">
                <%= Html.Encode(Criterio.Nombre)%>
            </td>
            <td style="width: 70px;">
                <%=Html.Encode(Criterio.Valor.ToString("F2"))%>
                <% if (Model.EsEditable)
                   {%>
                <%=Html.RadioButton(Criterio.Rubrica.RubricaId.ToString(), Criterio.CriterioId, selected)%>
                <%} %>
            </td>
        </tr>
        <%  } %>
    </table>
    <br />
    <%  } %>                <% if (Model.EsEditable)
                   {%>
    <input type="submit" class="button" id="submitButton" value="Grabar" /> <%} %>
    <% } %>
    <% } %>
    <div>
      <%switch(Model.Origen){ %>
        <%case "Details":%><%= Html.ActionLink("Regresar", "Details", new{TrabajoId =  Model.Trabajo.TrabajoId})%><%break; %>
        <%case "Index":%><%= Html.ActionLink("Regresar", "Index")%><%break; %>
        <%}%>
    </div>
</asp:Content>
