<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ExposeSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Mensaje
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%= Html.ShowTempMessage() %>

</asp:Content>
