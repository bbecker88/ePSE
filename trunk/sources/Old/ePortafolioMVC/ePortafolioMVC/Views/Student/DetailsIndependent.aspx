<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/StudentSite.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.ViewModels.StudentDetailsIndependentViewModel>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavigationBar" runat="server">
    <li>
        <%= Html.ActionLink(((BEPeriodo)Session["VistaPeriodo"]).Nombre,"SwitchPeriod",new {PeriodoId = ((BEPeriodo)Session["VistaPeriodo"]).PeriodoId} )%></li>
    <li>
        <%= Html.Encode(Model.Trabajo.Nombre) %></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

        <h3 style="text-align: center">
        <%= Html.Encode("Trabajo Independiente") %></h3>
    <h2 style="text-align: center">
        <%= Html.Encode(Model.Trabajo.Nombre) %></h2>
    
    
    <!-- FIN DE PARTE DE GRUPOS -->
    
    
    <% if (Model.Trabajo.EsGrupal)
       {
           if (Model.Grupo == null)
           {%>
    
    <div class = "curso">
    <h1>GRUPO</h1>
        Todavia no esta en ningun grupo.
    <%= Html.ActionLink("Crear grupo", "GroupCreate", new { TrabajoId = Model.Trabajo.TrabajoId })%><br />
    <br />
    </div>
    <%}
           else
           {
               Html.RenderPartial("GroupMemberListWithAction", new ePortafolioMVC.ViewModels.SharedGroupMemberListViewModel()
                   {
                       Alumno = Model.Alumno,
                       AlumnosGrupo = Model.AlumnosGrupo,
                       Grupo = Model.Grupo,
                       Trabajo = Model.Trabajo
                   });

               if (Model.Alumno.AlumnoId == Model.Grupo.Lider.AlumnoId)
               {
                   //El alumno logueado es lider y existen alumnos sin grupo
    using (Html.BeginForm("AddStudentIndependent", "Student", new { TrabajoId = Model.Trabajo.TrabajoId }, FormMethod.Post))
    {%>
    <%= Html.ValidationSummary(true) %>
    <div>
        <br />
        <%= Html.TextBox("AlumnoId")%>
        <%= Html.Hidden("GrupoId", Model.Grupo.GrupoId)%>
        <input type="submit" class="Button" value="Agregar" />
    </div>
    <%} %>
    <p>
        <%--<input type="submit"  class="button"  value="Save" />--%>
    </p>
    <%}
           }
       }%>

    <%-------- INICIO DE PARTE DE ARCHIVOS --------%>
    <% if (Model.Grupo != null)
       {
           using (Html.BeginForm("UploadFile", "Student", new { TrabajoId = Model.Trabajo.TrabajoId }, FormMethod.Post, new { enctype = "multipart/form-data" }))
           {%>
    <%= Html.ValidationSummary(true)%>
    <%  Html.RenderPartial("FileListWithAction", new ePortafolioMVC.ViewModels.SharedFileListViewModel()
                            {
                                ArchivosGrupo = Model.ArchivosGrupo,
                                Trabajo = Model.Trabajo
                            });
          %>
      
    <br />
    <input class="fileButton" name="fileToUpload" type="file" title="Archivo" value="Buscar archivo" />
    <input type="submit" class="Button" value="Subir" />
    <br />
    
         <%}
       }%>
    <%-------- FIN DE PARTE DE ARCHIVOS --------%>
    <div>
        <%= Html.ActionLink("Regresar", "Index") %>
    </div>
</asp:Content>
