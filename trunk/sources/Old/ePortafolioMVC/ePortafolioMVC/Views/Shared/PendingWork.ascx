<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="ePortafolioMVC.Helpers" %>
<%@ Import Namespace="ePortafolioMVC.Models.Entities" %>
<ul>

<script>

    function MostrarTodosTrabajos() {
        $('[id*="trabajoPendiente.mas"]').hide();
        $('[id*="trabajoPendiente.menos"]').show();
        $('[id*="trabajoPendiente.tp"]').show('clip');
    };

    function MostrarPocosTrabajos() {
        $('[id*="trabajoPendiente.menos"]').hide();
        $('[id*="trabajoPendiente.mas"]').show();
        $('[id*="trabajoPendiente.tp"]').hide('clip');
    };

</script>


    <%  
        var TrabajosPendientes = (List<BETrabajo>)Session["TrabajosPendientes"];
        int cont = 0;
        foreach (BETrabajo Trabajo in TrabajosPendientes)
        {
            cont++;
            if (cont > 6)
                break;
            
            if (cont < 4)
      {%>
    <li id="trabajoPendiente.<%= cont %>">
        <%}
      else
      {%>
        <li id="trabajoPendiente.tp<%= cont %>" style="display: none;">
            <%} %>
            <span title="<%= Html.Encode(Trabajo.Curso.Nombre) %>">
            <%= Html.ActionLink(Trabajo.Curso.Codigo + " - " + Trabajo.Nombre, "Details", new { TrabajoId = Trabajo.TrabajoId })%></span>
<br />
            <span>Fecha de entrega:
                <%= Html.Encode(!Trabajo.FechaFin.HasValue ? "-" : Trabajo.FechaFin.Value.ToString("dd-MMM"))%></span>
        </li>
        <%} %>
        <li id="trabajoPendiente.mas" class="otro"><a onclick="MostrarTodosTrabajos()">Ver m&aacute;s +</a> </li>
        <li id="trabajoPendiente.menos" class="otro" style="display: none;"><a onclick="MostrarPocosTrabajos()">Ver menos -</a> </li>
</ul>
