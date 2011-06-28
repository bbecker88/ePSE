<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RubricOn.ViewModel.DisenarVersionRubricaViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	DisenarVersionSegundaRubrica
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <script src="<%= Url.Content("~/Scripts/cleditor/jquery.cleditor.min.js")%>" type="text/javascript"></script>
        <script src="<%= Url.Content("~/Scripts/jquery-ui/jquery-ui-1.8.12.custom.js")%>" type="text/javascript"></script>
        <script src="<%= Url.Content("~/Scripts/jquery-ui/jquery.ui.slider.js")%>" type="text/javascript"></script>

        <link href="<%= Url.Content("~/Scripts/cleditor/jquery.cleditor.css") %>" rel="stylesheet" type="text/css" />
        <link href="<%= Url.Content("~/Content/ui-lightness/jquery-ui-1.8.12.custom.css") %>" rel="stylesheet" type="text/css" />

    <h2>DisenarVersionSegundaRubrica</h2>

	<style type="text/css"> 
		#demo-frame > div.demo { padding: 10px !important; }
		table td
		{
		    padding:0px;
		}
	</style>

    <script type="text/javascript">

        $('document').ready(function() {

            var cId = 1;
            var aId = 1;

            //BIND
            $(".cleditor_criterio").each(function() {
                var cols = $(this).parent().parent().children().length;
                var the_width = 930 / (cols - 1);
                $(this).cleditor({
                    width: the_width,
                    height: 100,
                    controls: "bold italic underline | undo redo | pastetext "
                });
            });

            $(".delete_aspecto").click(function() {
                $(this).parent().parent().html('');
            });

            $(".cleditor_categoria").cleditor({
                width: 950,
                height: 100,
                controls: "bold italic underline | undo redo | pastetext "
            });

            $("#slider").slider({
                value: 3,
                min: 2,
                max: 6,
                step: 1,
                slide: function(event, ui) {
                    $("#amount").html(ui.value);
                }
            });

            $("#amount").html($("#slider").slider("value"));

            $("#agregar_categoria").click(function() {
                var container = $('#diseno_rubrica');

                var category_text = $('.cleditor_categoria').attr('value');
                $('.cleditor_categoria').cleditor();
                var outcome = $("[name='CategoriaOutcomeId']").attr('value');
                var cols = parseInt($("#amount").html(), 10);

                if (category_text == "" || outcome == "" || cols == "") {
                    alert('Debe llenar todos los campos');
                    return false;
                }

                var tableHtml =
                     "<table style=\"width:955px;\" cellspacing=\"0\" border=\"0\" id=\"cId_" + cId + "\">" +
                        "<tr>" +
                            "<td colspan=\"" + (cols + 1) + "\" style=\"background-color:#e3c3e3; padding-top:5px;padding-bottom:5px\"><b style=\"color:black\">" + outcome + ")&nbsp</b>" + category_text + "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td colspan=\"" + (cols + 1) + "\" style=\"text-align:center\">" +
                                "<input type=\"button\" value = \"Agregar\" id=\"agregar_aspecto_" + cId + "\"/>" +
                            "</td>" +
                        "</tr>" +
                     "</table>" +
                     "<br/>";

                container.append(tableHtml);

                $("#agregar_aspecto_" + cId).click(function() {
                    var cols = parseInt($(this).parent().attr('colspan'), 10);
                    var innerHtml = "<td style=\"width:25px; background-color:#e3c3e3;\"><a class=\"delete_aspecto\">X</a></td>";

                    var the_width = 930 / (cols - 1);

                    for (i = 1; i < cols; i++) {
                        innerHtml = innerHtml + "<td>" +
                                                    "<textarea name=\"TA1\" class = \"cleditor_criterio\">" + cId + "</textarea>" +
                                                    "<span style=\"position:relative; top:5px; left:14px;\">Valor</span>" +
                                                    "<input type=\"text\" name=\"VA1\" value=\"" + cId + "\" style=\"float:right;width:" + (the_width - 60) + "px\"/>" +
                                                "</td>";
                    }

                    innerHtml = "<tr>" + innerHtml + "</tr>";

                    $(this).parent().parent().before(innerHtml);

                    //REBIND
                    $(".cleditor_criterio").cleditor({
                        width: the_width,
                        height: 100,
                        controls: "bold italic underline | undo redo | pastetext "
                    });

                    $(".delete_aspecto").click(function() {
                        $(this).parent().parent().html('');
                    });

                    aId++;
                });

                $("#agregar_aspecto_" + cId).click();
                cId++;
            });
        });
    </script>


    <div id="diseno_rubrica">
        <% foreach(var categoria in Model.Categorias){  %>
            <% var cols = Model.Aspectos.Where(x => x.CategoriaRubricaId == categoria.CategoriaRubricaId).Max(x => Model.Criterios.Count(c => c.AspectoRubricaId == x.AspectoRubricaId)); %>
             <table style="width:955px;" cellspacing="0" border="0" id="cId_4">
                <tbody>
                    <tr>
                        <td colspan="<%= cols + 1%>" style="background-color:#e3c3e3; padding-top:5px;padding-bottom:5px">
                            <b style="color:black"><%= categoria.OutcomeId + ")" %>&nbsp;</b><%= categoria.Nombre%>
                        </td>
                    </tr>   
            <% var Aspectos = Model.Aspectos.Where(x => x.CategoriaRubricaId == categoria.CategoriaRubricaId).ToList(); %>
            <% foreach(var aspecto in Aspectos){  %>
                    <tr>
                        <td style="width:25px; background-color:#e3c3e3;">
                            <a class="delete_aspecto" id="delete_aspecto">X</a>
                        </td>
                <% var Criterios = Model.Criterios.Where(x => x.AspectoRubricaId == aspecto.AspectoRubricaId).ToList(); %>
                <% var the_width = 930 / (cols);%>
                <% foreach(var criterio in Criterios){  %>
                        <td>
                            <textarea name="TA1" class = "cleditor_criterio"><%= criterio.Nombre %></textarea>
                            <span style="position:relative; top:5px; left:14px;">Valor</span>
                            <input type="text" name="VA1" value="<%= criterio.Valor %>" style="float:right;width:<%=the_width-60%>px"/>
                        </td>
                <%} %>
                    </tr>
            <%} %>
                    <tr>
                        <td colspan="<%= cols + 1%>" style="text-align:center">
                            <input type="button" value="Agregar" id="agregar_aspecto">
                        </td>
                    </tr>
                </tbody>
             </table>
        <%} %>
    </div>
    <br />

    <table style="width:950px"> 
        <tr>
            <td style="width:200px"> 
            <%= Html.Encode("Outcome asociado")%> <br />
            <%= Html.DropDownList("CategoriaOutcomeId", new SelectList(Model.Outcomes, "OutcomeId", "OutcomeId"), "Seleccionar", new { style = "width:200px" })%>
            </td>
            
            <td style="width:200px">
                <span  style="position:relative;top:-5px;">
                    <%= Html.Encode("Número de criterios")%><span id="amount" style="border:0; color:#f6931f; font-weight:bold; padding-left:5px">$50</span>
                </span>
                <div id="slider" style="position:relative;top:2px;"></div> 
            </td>
            <td style="text-align:right;">
                <input type="button" value = "Agregar" id="agregar_categoria"/>
            </td>
        </tr>
        <tr>
            <td colspan="3">
            <%= Html.Encode("Descripción de la categoría")%><br />
            <%= Html.TextArea("CategoriaDescripcion", new { @class = "cleditor_categoria" })%>
            </td>
        </tr>
    </table>
    
    
    
    
    
    

</asp:Content>
