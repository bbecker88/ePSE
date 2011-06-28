<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RubricOn.ViewModel.DisenarVersionRubricaViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode("Diseñar rúbrica")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode("Diseñar rúbrica")%></h2>

    <script src="<%= Url.Content("~/Scripts/jquery-dynamic-form.js")%>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jquery-ui/jquery-ui-1.8.12.custom.js")%>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jquery-ui/jquery.ui.sortable.js")%>" type="text/javascript"></script>
    <link href="<%= Url.Content("~/Content/ui-lightness/jquery-ui-1.8.12.custom.css")%>" rel="stylesheet" type="text/css" />        
    
    <style>
        .ui-sortable-placeholder{
        background-color:black;
        }
    
    </style>
<%--    
	<script>
	    $(function() {
	    $(".sortable1").sortable();
	    $(".sortable1").sortable("option", "containment", 'parent');
	    $(".sortable1").sortable("option", "items", '.sortableitem1');
	    $(".sortable1").sortable("option", "opacity", 0.6);
	    $(".sortable1").disableSelection();
    });
    </script>
    <script>
        $(function() {
	    $(".sortable2").sortable();
	    $(".sortable2").sortable("option", "containment", 'parent');
	    $(".sortable2").sortable("option", "items", '.sortableitem2');
	    $(".sortable2").sortable("option", "opacity", 0.6);
	    $(".sortable2").disableSelection();
	    });
	</script>--%>
    
    <table>
        <tr>
            <th>
                Rubrica
            </th>
            <td>
                <%= Model.Rubrica.RubricaId %>
            </td> 
         </tr>
         <tr>
            <th>
                Version
            </th>
            <td>
                <%= Model.Rubrica.Version%>
            </td>            
         </tr>
         <tr>
            <th>
                Artefacto
            </th>
            <td>
                <%= Model.Rubrica.TipoArtefacto%>
            </td>            
         </tr>
    </table>
    <br />

    <% using (Ajax.BeginForm(new AjaxOptions() {UpdateTargetId = "ResultadoDiv" })){ %>
    
    <div id="categoriaTemplate" class = "sortable2" style="background-color:#E8FF9B; width:100%; border:2px green dashed; padding:10px;margin:2px">
        <%= Html.TextArea("categoriaText", "Descripcion categoria", new { style = "width:100%" })%>
        <div id="aspectoTemplate" class = "sortable1 sortableitem2" style="background-color:#DDE9FF;padding:5px; border:1px blue dashed;margin:2px">
            <% for (int i = 0; i < 3; i++)
               {%>
                <div id="criterioTemplate<%=i%>" class="sortableitem1" style="background-color:#FFF9DB; vertical-align:top;width:160px;text-align:center; border: 1px solid gray; margin:5px;float:left;padding:5px">                
                    <%= Html.TextArea("criterioText" + i, "Descripcion criterio " + i, new { style = "height:100px; width:150px" })%>
                    <br />
                    <%= Html.TextBox("criterioValor" + i, i, new { style = "width:150px" })%>
                </div>
            <% }%>
            <br />
            <div style = "width:100%;text-align:center;"><a id="aspectoRem" href="">[-ASP]</a> <a id="aspectoAdd" href="">[+ASP]</a></div>
        <div style="clear:both"></div>    
        </div>
        <br />
    </div>
        <div style = "width:100%; text-align:center"><a id="categoriaRem" href="">[-CAT]</a> <a id="categoriaAdd" href="">[+CAT]</a></div>
        <%= Html.Submit("Enviar") %>
   <% }%>
    
    <div id= "ResultadoDiv"></div>
    
<%--    <div id="categoriaTemplate" style="background-color:#E8FF9B; width:100%">
        <%= Html.TextArea("categoriaText","Descripcion categoria") %>
        
        <div id="aspectoTemplate" style="background-color:#DDE9FF; width:800px">
        

            <%= Html.TextArea("aspectoText","Descripcion categoria") %>
                        
            <div id="criterioTemplate" style="background-color:#FFF9DB; width:200px;display:inline;">                
                <%= Html.TextArea("criterioText", "Descripcion categoria")%>
                <%= Html.TextBox("criterioValor","0") %>
            </div>
            <p><span><a id="criterioRem" href="">[-]</a> <a id="criterioAdd" href="">[+]</a></span></p>
        </div>
        <p><span><a id="aspectoRem" href="">[-]</a> <a id="aspectoAdd" href="">[+]</a></span></p>
    </div>
    <p><span><a id="categoriaRem" href="">[-]</a> <a id="categoriaAdd" href="">[+]</a></span></p>
--%>
    <script>

    $('document').ready(function(){

    $("#categoriaTemplate").dynamicForm("#categoriaAdd", "#categoriaRem", { limit: 90 });
    $("#aspectoTemplate").dynamicForm("#aspectoAdd", "#aspectoRem", { limit: 90 });
    $("#criterioTemplate").dynamicForm("#criterioAdd", "#criterioRem", { limit: 90 });         
    });


    </script>
    
    
</asp:Content>
