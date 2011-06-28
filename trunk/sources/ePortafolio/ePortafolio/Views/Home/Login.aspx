<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Login.Master" Inherits="System.Web.Mvc.ViewPage<ePortafolio.ViewModel.LoginViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="loginBox">
        <h2>Login</h2>
        
        <%Html.BeginForm();%>
        <table border="0" cellpadding="0" cellspacing="0" class="rounded">
            <tbody>
                <tr>
                    <td><%= Html.Encode("Usuario") %>
                    <%= Html.TextBoxFor(x=>x.Usuario)%></td>   
                </tr>
                <tr>
                    <td><%= Html.Encode("Contraseña") %>
                    <%= Html.TextBoxFor(x=>x.Contraseña)%></td>   
                </tr>
                <tr>
                    <td>
                        <%= Html.Link("¿Olvidó su contraseña?", "https://intranet.upc.edu.pe/LoginIntranet/frmOC002.aspx", true, new { @class = "fleft" })%>
                        <%= Html.Submit("Entrar", new { @class="btn fright rounded"})%>
                    </td>
                </tr>
            </tbody>
        </table>
        <%Html.EndForm();%> 
       <div class="clear"></div>
    </div>   

</asp:Content>
