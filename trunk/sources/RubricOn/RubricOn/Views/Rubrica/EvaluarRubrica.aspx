<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RubricOn.ViewModel.EvaluarRubricaViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode("Aplicar rúbrica")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode("Aplicar rúbrica")%></h2>
    
    <script src="<%= Url.Content("~/Scripts/jquery-ui/jquery-ui-1.8.12.custom.js")%>" type="text/javascript"></script>
    <link href="<%= Url.Content("~/Content/ui-lightness/jquery-ui-1.8.12.custom.css")%>" rel="stylesheet" type="text/css" />
        
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
    
     <style>
        .tdCriterioSeleccionadoPar{ background-color:#ffa9a9;}
        .tdCriterioSeleccionadoImpar{ background-color:#ccff9b;}
        
        .tdCriterioPar{ background-color:#DDE9FF;}
        .tdCriterioImpar{ background-color:#f5faff;}
        
        .tdCategoriaPar{ background-color:#E8FF9B;}
        .tdCategoriaImpar{ background-color:#ccff9b;}

     </style>
    
    
    <% using(Html.BeginForm()){ %>
    
        <%= Html.Hidden("Version",Model.Rubrica.Version) %>
        
        <% var EsCategoriaPar = true;%>
        <% var EsAspectoPar = true;%>
        <% foreach (var categoria in Model.Categorias.OrderBy(x => x.Orden)){ %>
            <% EsCategoriaPar = !EsCategoriaPar;%>
            <% var aspectos = Model.Aspectos.Where(x => x.CategoriaRubricaId == categoria.CategoriaRubricaId).OrderBy(x=>x.Orden).ToList();%>
            <% var maximo = aspectos.Sum(x => Model.Criterios.Where(c => c.AspectoRubricaId == x.AspectoRubricaId).Max(m => m.Valor));%>
            <table style="width:100%">    
                <% foreach (var aspecto in aspectos){ %>
                <% EsAspectoPar = !EsAspectoPar;%>
                <% var criterios = Model.Criterios.Where(x => x.AspectoRubricaId == aspecto.AspectoRubricaId).OrderBy(x => x.Orden).ToList();
                   var tdWidthStyle = " width:" + (100.0 / (criterios.Count+1)*1.0) + "%; ";

                   var tdCategoriaClass = EsCategoriaPar ? " tdCategoriaPar " : " tdCategoriaImpar ";
                   var tdCriterioClass = EsAspectoPar ? " tdCriterioPar " : " tdCriterioImpar ";

                       %>                  
                    <tr>
                        <% if(aspectos.First() == aspecto){ %>                   
                            <td rowspan="<%= aspectos.Count()*2 %>" style=" <%= tdWidthStyle%>" class="<%= tdCategoriaClass %>">
                                <%= categoria.Nombre %><br />
                                <b><%= maximo%> Puntos</b>
                                
                            </td>
                        <%} %>
                        <% foreach (var criterio in criterios){ %>
                            <td style=" <%= tdWidthStyle%>" class="<%= tdCriterioClass %>"><%= criterio.Nombre%></td>
                        <%} %>
                    </tr>
                    <tr>
                        <% foreach (var criterio in criterios){ %>
                        <% var checkBoxName = "EvalA" + aspecto.AspectoRubricaId + "-C" + criterio.CriterioRubricaId; %>
                        <td style=" <%= tdWidthStyle%>" class="<%= tdCriterioClass %>">
                        <%= Html.CheckBox(checkBoxName, new { id = checkBoxName})%>
                            <label style="width: 100%" for="<%=checkBoxName%>">
                                <b><%= criterio.Valor%> Puntos</b>
                            </label>
                        <%} %>     
                        </td>       
                    </tr>
                    
                <%} %>
            </table>    
            <br />
        <%} %>
        
        <%= Html.Submit("Enviar") %>
        
     <%} %>
     
    
      <script type="text/javascript">

          $('document').ready(function() {

              var Lines = new Array();
              var NActive = 1;

              $("[id^=Eval]").button();
              $(".checkboxformat").buttonset();
              $("[id^=Eval]").click(function() {
                  var id = this.name;
                  var objCheckbox = $('#' + id);
                  var lineName = id.split('-')[0];
                  var isChecked = objCheckbox.is(":checked"); //valor actual del checkbox -> depues de actualizar

                  if (!(lineName in Lines)) {
                      Lines[lineName] = new Array();
                  }

                  if (isChecked) { //El checkbox ha sido seleccionado
                      if (Lines[lineName].length == NActive) { //Ya existen dos seleccinados, el primero se tiene que ir
                          $('#' + Lines[lineName][0]).attr('checked', false).button("refresh");
                          Lines[lineName].shift();
                          Lines[lineName][NActive - 1] = id;
                      }
                      else { //todavia no hay dos seleccinados, se agrega nomas
                          Lines[lineName][Lines[lineName].length] = id;
                      }
                  }

                  if (!isChecked) { //El checkbox ha sido deseleccionado

                      var index = 0;
                      for (var i = 0; i < Lines[lineName].length; i++) {
                          if (Lines[lineName][i] == id)
                              index = i;
                      }

                      Lines[lineName].splice(index, 1);
                  }

                  if (Lines[lineName].length != NActive) {
                      $('[id^=' + lineName + '-]').parent().removeClass('completo').addClass('incompleto');
                  }
                  else {
                      $('[id^=' + lineName + '-]').parent().removeClass('incompleto').addClass('completo');
                  }
              });
          });
    </script>
    <script type="text/javascript">

        function verificarRespuesta () {

            var count = 0;

            $("[id^=Eval]").each(function () {
                if ($(this).is(":checked")) {
                    count++;
                }
            });

            if(count!=<%= Model.Aspectos.Count() %>)
            {
                $("#dialog-message").dialog('open');
                return false;
            }
            return true;
        };

    </script>
    <script type="text/javascript">
        $('document').ready(function() {
            $("form").submit(function() {
                return verificarRespuesta();
            });

            $("#dialog-message").dialog({
                autoOpen: false,
                resizable: false,
                draggable: false,
                modal: true,
                buttons: {
                    Aceptar: function() {
                        $(this).dialog("close");
                    }
                }
            });
        });
    </script>
    <div id="dialog-message" title="Prueba incompleta">
        <p>
            Debe responder todas las pregunas.
        </p>
    </div>
     
     
</asp:Content>
