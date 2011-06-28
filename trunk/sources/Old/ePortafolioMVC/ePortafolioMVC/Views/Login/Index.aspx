<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LoginSite.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolioMVC.Models.UserAutentication>" %>

<%@ Import Namespace="ePortafolioMVC.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Ingreso
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
        <% using (Html.BeginForm())
       {%>
                        <table border="0" cellpadding="0" cellspacing="0">
                    <tbody>
                    <tr>
                    	<td>Usuario</td>
                
                        <td><input type="text" name="User" class="TextBox user"/></td>
                    </tr>
                    <tr>
                    	<td>Contrase&ntilde;a</td>
                
                        <td><input type="password" name="Password" class="TextBox pass"/></td>
                    </tr>              
                    <tr>
                    	<td></td>
               
                        <td><input type="submit" class="Button" value="Ingresar"/></td>
                    </tr>                        
                    </tbody>
                    </table>  
        <% } %>
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

</asp:Content>
