<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolio.ViewModel.MostrarDetalleTrabajoEstudianteViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode("ePortafolio SE | Detalles de Trabajo")%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="CiclosContent" runat="server">
    <% Html.RenderPartial("MenuCiclosDisponibles",Model.Trabajo.PeriodoId); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% if(Model.Trabajo.Iniciativa=="UPC") 
           {
           var RutaVerRubrica = Model.AlumnoGrupo == null || Model.AlumnoGrupo.EvaluacionId == null ? Url.Action("VerRubricaTrabajo", "Evaluacion", new { TrabajoId = Model.Trabajo.TrabajoId }) : Url.Action("VerEvaluacion", "Evaluacion", new { EvaluacionId = Model.AlumnoGrupo.EvaluacionId });%>

        
        <% Html.RenderPartial("CabeceraDetalleTrabajo", Model.Trabajo);%>
    
        <div class="block">
            <%= Html.Link("Ver Rúbrica", RutaVerRubrica,true, new {@class="btn btn_chart rounded"})%>     
        </div>
    <%} %>
    
    <% if(Model.Grupo!=null) {%>
        <% Html.RenderPartial("MostrarDetalleGrupo", new ePortafolio.ViewModel.MostrarDetalleGrupoViewModel(Model.Trabajo.TrabajoId, Model.Alumno.AlumnoId)); %>
        <br />
        <% Html.RenderPartial("MostrarDetalleArchivos", new ePortafolio.ViewModel.MostrarDetalleArchivosViewModel(Model.Trabajo.TrabajoId, Model.Alumno.AlumnoId)); %>
    <%} %>
    
    <% if(Model.Grupo==null) {%>
        
        <% using (Html.BeginForm("CrearGrupo","Estudiante", new { TrabajoId = Model.Trabajo.TrabajoId }))
           { %>
            No se encuentra en ningun crupo. <br />
            
            <h2>Crear grupo:</h2>
            Nombre del trabajo: <%= Html.TextBox("NombreTrabajo") %>
            <%= Html.Submit("Crear Grupo",new {@class="btn rounded"}) %>
        <%} %>
    <%} %>
</asp:Content>
