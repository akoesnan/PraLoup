<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProjectSafari.Models.HomeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Project Safari Beta Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="left">
    Welcome to the Project Safari Home Page!</div>
    <div id="right">
    <p>Click Here to sign in with Facebook</p>
    <p>
    <input type="image" src="Content/facebook.png"  value="foobar" onclick="window.location='<%=Model.redirecturl %>'" />
    </p></div>
    <div class="clear"></div>
</asp:Content>
