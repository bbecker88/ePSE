<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolio.ViewModel.MostrarTrabajosIndependientesViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Html.Encode("ePortafolio SE | Trabajos Independientes")%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="CiclosContent" runat="server">
    <% Html.RenderPartial("MenuCiclosDisponibles",Session.Get(GlobalKey.ActualPeriodoId).ToString()); %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BreadcrumbsContent" runat="server">
    <ul class="listah">
    	<li><a href="/" class="logo-30"></a></li>
    	<li><a href="/" ><%= "Trabajos Independientes"%></a></li>
    </ul>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

      <div class="block">
    	    <h2><%= Html.Encode("Trabajos independientes") %></h2>
    	    <span class="desc"><%= Html.Encode(String.Format("{0} trabajos registrados", Model.TrabajosIndependientes.Count))%></span>


            <%if(Model.TrabajosIndependientes.Count==0 ){%>
                <p style="padding-left:3px">No hay trabajos independientes registrados</p>          
            <%}else{ %>
            <table border="0" cellpadding="0" cellspacing="0"  class="rounded">
            <thead>
        	    <tr>
                    <th width="24">Rol</th>
                    <th width="436">Trabajo</th>                        
                    <th width="70">Periodo</th>
                </tr>
            </thead>
            <tbody>
            <% 
                var odd= true;
                foreach(var Trabajo in Model.TrabajosIndependientes){%>
              <% odd = !odd;
                 var ClaseFila = odd?"odd":"";
                 var GrupoTrabajo = Model.Grupos.SingleOrDefault(x => x.TrabajoId == Trabajo.TrabajoId); 
                 var TituloRol = GrupoTrabajo==null?"Sin Grupo":GrupoTrabajo.LiderId == Model.Alumno.AlumnoId?"Líder":"Integrante";
                 var ClaseRol = GrupoTrabajo == null ? "" : GrupoTrabajo.LiderId == Model.Alumno.AlumnoId ? "lider" : "normal";

             %>
                <tr class="<%= ClaseFila %>">
                    <td><a title="<%= Html.Encode(TituloRol) %>" class="rol <%= ClaseRol %>"><span >líder</span></a></td>
                    <td><%= Html.ActionLink(Trabajo.Nombre, "MostrarDetalleTrabajo", new { TrabajoId = Trabajo.TrabajoId }, new { @class="nombre"})%></td>
                    <td><%= Trabajo.PeriodoId %></td>
                </tr>
               <%}%>
       	    </tbody>
    	    </table>
    	    <%}%>
        </div><!-- FIN CURSO -->
       
        <div class="block">
            <a id="btnAgregar" class="btn btn_add rounded">Agregar trabajo</a>
            <div id="AgregarTrabajoIndependiente">
            <% using (Html.BeginForm("CrearGrupoIndependiente","Estudiante"))
               {%>
                Nombre del trabajo independiente: <%= Html.TextBox("NombreTrabajoIndependiente")%>
                <%= Html.Submit("Crear Trabajo",new {@class="btn rounded"}) %>
            <%} %>
            </div>
        </div>
       
        <script>
           $('document').ready(function() {
               $('#AgregarTrabajoIndependiente').hide();
               $('#btnAgregar').click(function() { $('#AgregarTrabajoIndependiente').toggle('slow') });
           });
        </script>
        
</asp:Content>

