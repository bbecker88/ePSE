<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RubricOn.ViewModel.ListarRubricasViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode("Listar rúbricas")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode("Listar rúbricas")%></h2>
    
    <% using (Ajax.BeginForm("ListarRubricasArtefacto", new AjaxOptions (){UpdateTargetId = "ListaRubricasArtefacto" }))
       { %>
        <%= Html.DropDownListFor(x => x.TipoArtefacto, new SelectList(Model.TiposArtefacto.ToDictionary(x=>x.TipoArtefacto,x=>x.TipoArtefacto + " - " + x.Descripcion),"key","value"),"Seleccionar",new {style = "width:300px"})%>
        <%= Html.Submit("Ver detalle")%>
    <%} %>
    <br />
    <div id="ListaRubricasArtefacto">
        <% Html.RenderAction("ListarRubricasArtefacto", new { TipoArtefacto = ""} ); %>
    </div>

</asp:Content>
