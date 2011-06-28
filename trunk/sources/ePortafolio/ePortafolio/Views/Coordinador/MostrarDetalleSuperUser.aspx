<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >


<head>
    <style>
        *
        {
            margin: 0;
            padding: 0;
        }
        html, body
        {
            height: 100%;
            width: 100%;
            overflow: hidden;
        }
        table
        {
            height: 100%;
            width: 100%;
            table-layout: static;
            border-collapse: collapse;
        }
        iframe
        {
            height: 100%;
            width: 100%;
        }
        .header
        {
            background-color: #6A8198;
            padding:2px;
            text-align:center;
        }
        .content
        {
            height: 100%;
        }
    </style>
    <link href="<%= Url.Content("~/Content/css/clean.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Url.Content("~/Content/css/main.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%= Url.Content("~/Scripts/jquery-1.4.1.js")%>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jquery-ui/jquery.effects.core.js")%>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jquery-ui/jquery.effects.fold.js")%>" type="text/javascript"></script>
    
    <title>MostrarDetalleSuperUser</title>
</head>
<body>
    <table>
        <tr>
            <td class="header">
            
                    <%= Html.ShowTempMessage() %>
                    <% using(Html.BeginForm()){ %>
                        <span style="color:#f9f9f9;font-weight:bold; font-size:14px; padding-right:5px; text-transform:uppercase;"><%= Html.Encode("Modo de exploración")%></span>
                        <%= Html.TextBox("UsuarioId") %>
                        <%= Html.Submit("Ver perfil",new {@class = "btn btn_check rounded"})%>
                        <a href="" onclick="javascript:window.frames['ifm'].location.reload(); return false;" class="btn btn_wizard rounded" style="height: 16px;position:relative;top:1px;left:-7px">Refrescar</a>
                        <%= Html.ActionLink("Salir", "ReestablecerControlCoordinador", null, new { @class = "btn btn_cancel rounded", style="height: 16px;position:relative;top:1px;left:-14px"})%>
                        
                    <%} %>
            </td>
        </tr>
        <tr>
            <td class="content">
                <iframe id="ifm" style="width:100%;" src="<%= Url.Action("Home","Home") %>" frameborder="0">El navegador no soporta iframes.</iframe>
            </td>
        </tr>
    </table>
</body>
</html>


