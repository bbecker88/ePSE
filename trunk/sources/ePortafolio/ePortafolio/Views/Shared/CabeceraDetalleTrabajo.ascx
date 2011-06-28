<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ePortafolio.Models.ePortafolio.Entities.TrabajosBE>" %>


<%
    var TituloGrupal = Model.EsGrupal ? "Grupal" : "Individual";
    var ClaseGrupal = Model.EsGrupal ? "grupal" : "";
 %>
 <div class="block">
     <table border="0" cellpadding="0" cellspacing="0"  class="rounded centered" style="width:410px">
        <tr>
            <td width="80">Nombre:</td>
            <td width="200"><%= Model.Nombre %></td>
            <td width="80">Modo:</td>
            <td width="50"><a title="<%= Html.Encode(TituloGrupal) %>"class="mod <%= ClaseGrupal %>"><span >grupal</span></a></td>
        </tr>
        
        <tr>
            <td>Inicio:</td>
            <td><%= Model.FechaInicio.HasValue?Model.FechaInicio.Value.ToShortDateString():"-" %></td>
            <td>Fin:</td>
            <td><%= Model.FechaFin.HasValue ? Model.FechaFin.Value.ToShortDateString() : "-"%></td>
        </tr>
        
    </table>
    <%if(Model.Instrucciones != "") {%>
        <script>
            $('document').ready(function() {
                $('#aInstrucciones').click(function() {
                    $('#divInstrucciones').toggle();
                });
            });
            
        </script>
    
        <a id="aInstrucciones" class="btn btn_flag rounded">Instrucciones</a>
        <div id="divInstrucciones" style="display:none; width:680px; margin-left:10px; padding-left:10px; border-left:5px solid #C6CBCF;">
            <%= Model.Instrucciones%>
        </div>
    <%} %>
</div>